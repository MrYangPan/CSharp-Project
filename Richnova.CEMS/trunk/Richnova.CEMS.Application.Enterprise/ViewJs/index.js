$.ajaxSetup({ cache: false });

//解决IE不支持JSON
if (typeof (JSON) == 'undefined') {
    $.getScript('/Scripts/json2.js');
}

var wrapper = {};
var appSettings ={};
appSettings.homeTabTitle ="我的桌面";
wrapper.init = function (settings) {
    appSettings = settings;
    $("#login-user").html(appSettings.userName);

    $('.loginOut').click(wrapper.logout);
    //$('.changepwd').click(wrapper.changePassword);
    //$('.myconfig').click(wrapper.mysettings);
    $('#closeMenu').menu({ onClick: wrapper.rightMenuClick });

     $.ajax({ type: 'POST', url: 'api/Auth/Menu/GetMenus', success: wrapper.initMenu });

     $('#center-tabs').tabs({
         tools: [{ iconCls: 'icon-screen_full', handler: wrapper.setFullScreen },
                 { iconCls: 'panel-tool-close', handler: wrapper.tabClose }],
         onContextMenu: wrapper.tabContextMenu
     });
};

wrapper.logout = function () {
    $.messager.confirm($.fn.common.systemInfo, $.fn.login.logoutConfirm, function (r) {
        if (r) location.href = '/Login/Logout';
    });
};

wrapper.mysettings = function () {
    wrapper.openTab("个人设置", "/sys/config", "icon icon-wrench_orange");
};

wrapper.infoShow = function (title, msg) {
    $.messager.show({
        title: title,
        msg: msg,
        timeout: 1500,
        showType: 'slide',
        style: {
            right: '',
            top: document.body.scrollTop + document.documentElement.scrollTop,
            bottom: ''
        }
    });
};

wrapper.openTab = function (subtitle, url, icon) {
    if (!url || url == '#') return false;
    var $tab = $('#center-tabs');
    var tabCount = $tab.tabs("tabs").length;
    var hasTab = $tab.tabs('exists', subtitle);
    if ((tabCount <= appSettings.maxTabCount) || hasTab)
        wrapper.openTabHandler($tab, hasTab, subtitle, url, icon);
    else
        $.messager.confirm($.fn.common.systemInfo, $.fn.index.tooManyTabsConfirm, function (b) {
            if (b) wrapper.openTabHandler($tab, hasTab, subtitle, url, icon);
        });
    return true;
};

wrapper.openTabHandler = function ($tab, hasTab, subtitle, url, icon) {
    if (!hasTab) {
        $tab.tabs('add', {
            title: subtitle, href: url, iconCls: icon, closable: true });
    } else {
        $tab.tabs('select', subtitle);
    }
};

wrapper.initMenu = function (d) {
    if (!d || !d.length) {
        $.messager.alert($.fn.common.systemInfo, $.fn.index.noAuthorization, "warning", function() {
            location.href = '/Login';
        });
        return;
    }

    $('body').data('menulist', d);
    var menus = d;

    switch (appSettings.navigation) {
        case "tree":
            wrapper.menuTree(menus);
            break;
        case "menubutton":
            wrapper.menuButton(menus);
            break;
        case "accordion":
            wrapper.menuAccordion(menus);
            break;
        default:
            wrapper.menuButton(menus);
            break;
    }
};

wrapper.menuAccordion = function (menus) {
};

wrapper.menuTree = function (menus) {
};

wrapper.menuButton = function (menus) {
    var menulist = "";
    var childMenu = '';
    var allMenus = "";
    $.each(menus, function (i, n) {
        menulist += utils.formatString('<a href="javascript:void(0)" id="mb{0}" class="easyui-menubutton" menu="#mm{0}" iconCls="{1}">{2}</a>',
            (i + 1), n.iconCls, n.text);
            childMenu += '<div id="mm' + (i + 1) + '" style="width:120px;">';
            if ((n.children || []).length > 0) {
                childMenu += wrapper.menuButtonChild(n);
            }
            childMenu += '</div>';
    });
    allMenus = menulist + childMenu;
    $('#wnav').append(allMenus);
    var northPanel = $('body').layout('panel', 'north');
    northPanel.panel('resize');

    var mb = $('#wnav .easyui-menubutton').menubutton();
    $.each(mb, function (i, n) {
        $($(n).menubutton('options').menu).menu({
            onClick: function (item) {
                var tabTitle = item.text;
                var url = item.id;
                var icon = item.iconCls;
                wrapper.openTab(tabTitle, url, icon);
                return false;
            }
        });
    });
};

wrapper.menuButtonChild = function (n) {
    var str = '';
    $.each(n.children, function (j, o) {
        if (o.children.length > 0) {
            str += '<div>';
            str += '<span iconCls="' + o.iconCls + '">' + o.text + '</span><div style="width:120px;">';
            str += wrapper.menuButtonChild(o);
            str += '</div></div>';
        } else
            str += '<div iconCls="' + o.iconCls + '" id="' + o.url + '">' + o.text + '</div>';
    });
    return str;
};

wrapper.tabContextMenu = function (e, title) {
    $('#closeMenu').menu('show', { left: e.pageX, top: e.pageY });
    $('#center-tabs').tabs('select', title);
    e.preventDefault();
};

wrapper.tabClose = function () {
    if (confirm('确认要关闭所有窗口吗？'))
        wrapper.rightMenuClick({ id: 'closeall' });
};

wrapper.getTabTitles = function ($tab) {
    var titles = [];
    var tabs = $tab.tabs('tabs');
    $.each(tabs, function () { titles.push($(this).panel('options').title); });
    return titles;
};

wrapper.setFullScreen = function () {
    var that = $(this);
    if (that.find('.icon-screen_full').length) {
        that.find('.icon-screen_full').removeClass('icon-screen_full').addClass('icon-screen_actual');
        $('[region=north],[region=west]').panel('close');
        var panels = $('body').data().layout.panels;
        panels.north.length = 0;
        panels.west.length = 0;
        if (panels.expandWest) {
            panels.expandWest.length = 0;
            $(panels.expandWest[0]).panel('close');
        }
        $('body').layout('resize');
    } else if ($(this).find('.icon-screen_actual').length) {
        that.find('.icon-screen_actual').removeClass('icon-screen_actual').addClass('icon-screen_full');
        $('[region=north],[region=west]').panel('open');
        panels = $('body').data().layout.panels;
        panels.north.length = 1;
        panels.west.length = 1;
        if ($(panels.west[0]).panel('options').collapsed) {
            panels.expandWest.length = 1;
            $(panels.expandWest[0]).panel('open');
            $('body').layout('panel', 'west').panel('resize');
        }
        $('body').layout('panel', 'north').panel('resize');
        $('body').layout('panel', 'center').panel('resize');
        //$('body').layout('resize');
    }
};

wrapper.rightMenuClick = function (item) {
    var $tab = $('#center-tabs');
    var currentTab = $tab.tabs('getSelected');
    var titles = wrapper.getTabTitles($tab);

    switch (item.id) {
        case "close":
            var currtab_title = currentTab.panel('options').title;
            $tab.tabs('close', currtab_title);
            break;
        case "closeall":
            $.each(titles, function () {
                if (this != appSettings.homeTabTitle)
                    $tab.tabs('close', this);
            });
            break;
        case "closeother":
            var currtab_title = currentTab.panel('options').title;
            $.each(titles, function () {
                if (this != currtab_title && this != appSettings.homeTabTitle)
                    $tab.tabs('close', this);
            });
            break;
        case "closeright":
            var tabIndex = $tab.tabs('getTabIndex', currentTab);
            if (tabIndex == titles.length - 1) {
                alert('亲，后边没有啦 ^@^!!');
                return false;
            }
            $.each(titles, function (i) {
                if (i > tabIndex && this != appSettings.homeTabTitle)
                    $tab.tabs('close', this);
            });

            break;
        case "closeleft":
            var tabIndex = $tab.tabs('getTabIndex', currentTab);
            if (tabIndex == 1) {
                alert('亲，前边那个上头有人，咱惹不起哦。 ^@^!!');
                return false;
            }
            $.each(titles, function (i) {
                if (i < tabIndex && this != appSettings.homeTabTitle)
                    $tab.tabs('close', this);
            });
            break;
        case "exit":
            $('#closeMenu').menu('hide');
            break;
    }
};

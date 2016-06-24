menu = {};
menu.settings = {};
menu.editRow = {};
menu.combotreeData = {};
menu.init = function (settings) {
    menu.settings = settings;
    menu.doLoad();
};

menu.clearForm = function () {
    $('#menu-form').form('clear');
};

menu.dialogOpen = function (edit) {
    $('#menu-dlg').dialog({
        closed: true,modal: true,shadow: true,cache: false,
        title: edit ? '编辑菜单':'新建菜单',
        onClose: function() { $(this).dialog('destroy'); },
        buttons: [
            {text: '确定',iconCls: 'icon-ok',handler: function() {
                    menu.doSave();
                    $('#menu-dlg').dialog('close');
                }
            }, {text: '取消',iconCls: 'icon-cancel',handler: function() {
                    $('#menu-dlg').dialog('close');
                }
            }
        ]
    }).dialog('open');
};

/** 菜单 **/
menu.doLoad = function () {
    $('#menu-treegrid').treegrid({
        url: menu.settings.urls.query,
        border: true,rownumbers: true,fitColumns: true,singleSelect: true,collapsible: true,animate:true,
        idField: 'Id',treeField: 'Name',sort: 'OrdeIndex',
        loadMsg: appSettings.msg.loading,
        onDblClickRow: function (r) {
            $('#menu-dlg').panel('refresh', menu.settings.urls.edit);
            $('#menu-form').form('load', r);
        }, onLoadSuccess: function (r, d) {
            menu.combotreeData = d.rows;
        }
    });
};

menu.doReload = function () {
    $('#menu-treegrid').treegrid('reload');
};

menu.doEdit = function () {
    menu.editRow = $('#menu-treegrid').treegrid('getSelected');
    if (!menu.editRow) {
        wrapper.infoShow(appSettings.msg.info, appSettings.msg.noSelected);
    } else {
        $('#menu-edit').panel('refresh', menu.settings.urls.edit);
        $('#menu-form').form('load', menu.editRow);
    }
};

menu.doCreate = function () {
    menu.editRow = {};
    $('#menu-edit').panel('refresh', menu.settings.urls.edit);
};

menu.doSave = function () {
    if (!$('#menu-form').form('validate')) return;
    $.messager.progress();
    $.post(menu.settings.urls.save, $('#menu-form').serializeArray(), function (result) {
        $.messager.progress('close');
        if (result.success) {
            wrapper.infoShow(appSettings.msg.info, appSettings.msg.saveSuccess);
            menu.doReload();
        } else {
            wrapper.infoShow(appSettings.msg.info, appSettings.msg.saveFail);
        }
    });
};

menu.doDelete = function () {
    var row = $('#menu-treegrid').treegrid('getSelected');
    if (!row) {
        wrapper.infoShow(appSettings.msg.info, appSettings.msg.noSelected);
    } else {
        $.messager.confirm(appSettings.msg.warnning, menu.settings.msg.confirmDeleteMenu, function(r) {
            if (r) {
                $.post(menu.settings.urls.delete + "?id=" + row.Id, function(result) {
                    if (result.success) {
                        wrapper.infoShow(appSettings.msg.info, appSettings.msg.deleteSuccess);
                        menu.doReload();
                    } else {
                        wrapper.infoShow(appSettings.msg.info, appSettings.msg.deleteFail);
                    }
                });
            }
        });
    }
};

menu.doMenuUp = function () {
    //var row = $('#menu-treegrid').treegrid('getSelected');
    //if (!row) return false;
    //var level = $('#menu-tree').treegrid('getLevel');
};

menu.doMenuDown = function () {

};

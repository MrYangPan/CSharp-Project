user = {};
user.settings = {};

user.init = function (settings) {
    user.settings = settings;
    user.doLoad();
};

user.clearForm = function() {
    $('#user-form').form('clear');
    $('#pwd_1').val("");
    $('#pwd_2').val("");
    $('#rolePick-grid').datagrid('loadData', { total: 0, rows: [] });
};

user.enableForm = function (b) {
    if (b) {
        $('#user-edit').removeClass('view-table').addClass('edit-table');
        $('#user-edit input').attr("disabled", false);
        $('#user-form').find('.easyui-linkbutton').linkbutton('enable');
    } else {
        $('#user-edit').removeClass('edit-table').addClass('view-table');
        $('#user-edit input').attr("disabled", true);
        $('#user-form').find('.easyui-linkbutton').linkbutton('disable');
    }
};

user.doLoad = function () {
    $('#user-grid').datagrid({
        url: user.settings.urls.query,
        border: true,
        rownumbers: true,
        fitColumns: true,
        singleSelect: true,
        animate: true,
        loadMsg: appSettings.msg.loading,
        onSelect: function (i, r) {
            $('#user-form').form('load', r);
            $('#rolePick-grid').datagrid('loadData', { rows: r.Roles });
            user.enableForm(false);
        },
        onLoadSuccess: function (d) {
            if (d.total > 0) $(this).datagrid('selectRow', 0);
            user.enableForm(false);
        }
    });
};

user.doReload = function () {
    $('#user-grid').datagrid('reload');
    user.clearForm();
    user.enableForm(false);
};

user.doEdit = function() {
    var row = $('#user-grid').datagrid('getSelected');
    if (!row) {
        wrapper.infoShow(appSettings.msg.info, appSettings.msg.noSelected);
        return false;
    }
    user.enableForm(true);
};

user.doCreate = function () {
    user.clearForm();
    user.enableForm(true);
};

user.doSave = function() {
    if (!$('#user-form').form('validate')) return false;
    var pwd1 = $('#pwd_1').val();
    var pwd2 = $('#pwd_2').val();
    if (pwd1 != "" && pwd1 != pwd2) {
        wrapper.infoShow(appSettings.msg.info, "两次输入的密码不一致");
        return false;
    }
    $.messager.progress();
    $.post(user.settings.urls.save, $('#user-form').serializeArray(), function(result) {
        if (result.success) {
            wrapper.infoShow(appSettings.msg.info, appSettings.msg.saveSuccess);
            user.doReload();
        } else {
            wrapper.infoShow(appSettings.msg.info, appSettings.msg.saveFail);
        }
    });
    $.messager.progress('close');
};

user.doDelete = function () {
    var row = $('#user-grid').datagrid('getSelected');
    if (!row) {
        wrapper.infoShow(appSettings.msg.info, appSettings.msg.noSelected);
        return false;
    }
    $.messager.confirm(appSettings.msg.warnning, appSettings.msg.deleteConfirm, function (r) {
        if (r) {
            $.post(user.settings.urls.delete + "?id=" + row.Id, function (result) {
                if (result.success) {
                    wrapper.infoShow(appSettings.msg.info, appSettings.msg.deleteSuccess);
                    user.doReload();
                } else {
                    wrapper.infoShow(appSettings.msg.info, appSettings.msg.deleteFail);
                }
            });
        }
    });
};

user.doAddRole = function () {
    $('#user-rolePick-dlg').panel('refresh', user.settings.urls.rolePickDlg);
};

user.doRemoveRole = function () {
    var row = $('#user-role-grid').datagrid('getSelected');
    if (row) {
        var index = $('#user-role-grid').datagrid('getRowIndex', row);
        $('#user-role-grid').datagrid('deleteRow', index);
    }
        
};
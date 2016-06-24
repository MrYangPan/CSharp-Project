role = {};
role.settings = {};

role.init = function (settings) {
    role.settings = settings;
    role.doLoad();
};

role.clearForm = function () {
    $('#role-form').form('clear');
    //$('#rolePick-grid').datagrid('loadData', { total: 0, rows: [] });
};

role.enableForm = function (b) {
    if (b) {
        $('#role-edit').removeClass('view-table').addClass('edit-table');
        $('#role-edit input').attr("disabled", false);
        $('#role-form').find('.easyui-linkbutton').linkbutton('enable');
    } else {
        $('#role-edit').removeClass('edit-table').addClass('view-table');
        $('#role-edit input').attr("disabled", true);
        $('#role-form').find('.easyui-linkbutton').linkbutton('disable');
    }
};

role.doLoad = function () {
    $('#role-grid').datagrid({
        url: role.settings.urls.query,
        border: true,
        rownumbers: true,
        fitColumns: true,
        singleSelect: true,
        animate: true,
        sort: 'OrdeIndex',
        loadMsg: appSettings.msg.loading,
        onSelect: function (i, r) {
            $('#role-form').form('load', r);
            role.enableForm(false);
        },
        onLoadSuccess: function (d) {
            if (d.total > 0) $(this).datagrid('selectRow', 0);
            role.enableForm(false);
        }
    });
};

role.doReload = function () {
    $('#role-grid').datagrid('reload');
    user.clearForm();
    role.enableForm(false);
};

role.doEdit = function () {
    var row = $('#role-grid').datagrid('getSelected');
    if (!row) {
        wrapper.infoShow(appSettings.msg.info, appSettings.msg.noSelected);
        return false;
    }
    role.enableForm(true);
};

role.doCreate = function () {
    role.clearForm();
    role.enableForm(true);
};

role.doSave = function () {
    if (!$('#role-form').form('validate')) return false;
    $.messager.progress();
    $.post(role.settings.urls.save, $('#role-form').serializeArray(), function (result) {
        if (result.success) {
            wrapper.infoShow(appSettings.msg.info, appSettings.msg.saveSuccess);
            role.doReload();
        } else {
            wrapper.infoShow(appSettings.msg.info, appSettings.msg.saveFail);
        }
    });
    $.messager.progress('close');
};

role.doDelete = function () {
    var row = $('#role-grid').datagrid('getSelected');
    if (!row) {
        wrapper.infoShow(appSettings.msg.info, appSettings.msg.noSelected);
        return false;
    }
    $.messager.confirm(appSettings.msg.warnning, appSettings.msg.deleteConfirm, function (r) {
        if (r) {
            $.post(role.settings.urls.delete + "?id=" + row.Id, function (result) {
                if (result.success) {
                    wrapper.infoShow(appSettings.msg.info, appSettings.msg.deleteSuccess);
                    role.doReload();
                } else {
                    wrapper.infoShow(appSettings.msg.info, appSettings.msg.deleteFail);
                }
            });
        }
    });
};

Industry = {};
Industry.settings = {};

Industry.init = function (settings) {
    Industry.settings = settings;
    Industry.doLoad();
};

Industry.clearForm = function () {
    $('#role-form').form('clear');
};

Industry.enableForm = function (b) {
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

Industry.doLoad = function () {
    $('#role-grid').datagrid({
        url: Industry.settings.urls.query,
        border: true,
        rownumbers: true,
        fitColumns: true,
        singleSelect: true,
        animate: true,
        sort: 'OrdeIndex',
        loadMsg: appSettings.msg.loading,
        onSelect: function (i, r) {
            $('#role-form').form('load', r);
            Industry.enableForm(false);
        },
        onLoadSuccess: function (d) {
            if (d.total > 0) $(this).datagrid('selectRow', 0);
            Industry.enableForm(false);
        }
    });
};

Industry.doReload = function () {
    $('#industry-datagrid').datagrid('reload');
    Industry.clearForm();
    Industry.enableForm(false);
};

Industry.doEdit = function () {
    var row = $('#industry-datagrid').datagrid('getSelected');
    if (!row) {
        wrapper.infoShow(appSettings.msg.info, appSettings.msg.noSelected);
        return false;
    }
    Industry.enableForm(true);
};

Industry.doCreate = function () {
    Industry.clearForm();
    Industry.enableForm(true);
};

Industry.doSave = function () {
    if (!$('#industry-datagrid').form('validate')) return;
    $.messager.progress();
    $.post(Industry.settings.urls.save, $('#industry-form').serializeArray(), function (result) {
        $.messager.progress('close');
        if (result.success) {
            wrapper.infoShow(appSettings.msg.info, appSettings.msg.saveSuccess);
            Industry.doReload();
        } else {
            wrapper.infoShow(appSettings.msg.info, appSettings.msg.saveFail);
        }
    });
};

Industry.doDelete = function () {
    var row = $('industry-datagrid').datagrid('getSelected');
    if (!row) {
        wrapper.infoShow(appSettings.msg.info, appSettings.msg.noSelected);
    } else {
        $.messager.confirm(appSettings.msg.warnning, Industry.settings.msg.confirmDeletearea, function (r) {
            if (r) {
                $.post(Industry.settings.urls.delete + "?id=" + row.Id, function (result) {
                    if (result.success) {
                        wrapper.infoShow(appSettings.msg.info, appSettings.msg.deleteSuccess);
                        Industry.doReload();
                    } else {
                        wrapper.infoShow(appSettings.msg.info, appSettings.msg.deleteFail);
                    }
                });
            }
        });
    }
};
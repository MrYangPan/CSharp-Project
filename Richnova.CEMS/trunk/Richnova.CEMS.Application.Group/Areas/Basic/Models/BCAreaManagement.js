area = {};
area.settings = {};
area.editRow = {};
area.combotreeData = {};
area.init = function (settings) {
    area.settings = settings;
    area.doLoad();
};

area.clearForm = function () {
    $('#area-form').form('clear');
};

area.dialogOpen = function (edit) {
    $('#area-dlg').dialog({
        closed: true, modal: true, shadow: true, cache: false,
        title: edit ? '编辑' : '新建',
        onClose: function () { $(this).dialog('destroy'); },
        buttons: [
            {
                text: '确定', iconCls: 'icon-ok', handler: function () {
                    area.doSave();
                    $('#area-dlg').dialog('close');
                }
            }, {
                text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#area-dlg').dialog('close');
                }
            }
        ]
    }).dialog('open');
};

/** 菜单 **/
area.doLoad = function () {
    $('#area-datagrid').datagrid({
        url: area.settings.urls.grid,
        border: true,
        rownumbers: true,
        fitColumns: true,
        singleSelect: true,
        animate: true,
        sort: 'Level',
        loadMsg: appSettings.msg.loading,
        onSelect: function (i, r) {
            $('#area-form').form('load', r);
        },
        onLoadSuccess: function (d) {
        }
    });
};

area.doReload = function () {
    $('#area-datagrid').datagrid('reload');
};

area.doEdit = function () {
    area.editRow = $('#area-datagrid').datagrid('getSelected');
    if (!area.editRow) {
        wrapper.infoShow(appSettings.msg.info, appSettings.msg.noSelected);
    } else {
        $.post(area.settings.urls.edit + '?id=' + area.editRow.Id, function (model) {
          
        });
        //$('#area-edit').panel('refresh', area.settings.urls.edit);
        //$('#area-form').form('load', area.editRow);
    }
    $('#ParentId').combotree({
        //获取数据URL  
        url: area.settings.urls.treeData,
        //选择树节点触发事件  
        onSelect: function (node) {
            //返回树对象  
            var tree = $(this).tree;
            //选中的节点是否为叶子节点,如果不是叶子节点,清除选中  
            var isLeaf = tree('isLeaf', node.target);
            if (!isLeaf) {
                //清除选中  
                $('#ParentId').combotree('clear');
            }
        }
    });
};

area.doCreate = function () {
    area.editRow = {};
    $('#area-edit').panel('refresh', area.settings.urls.edit);
    $('#ParentId').combotree({
        //获取数据URL  
        url: area.settings.urls.treeData,
        //选择树节点触发事件  
        onSelect: function (node) {
            //返回树对象  
            var tree = $(this).tree;
            //选中的节点是否为叶子节点,如果不是叶子节点,清除选中  
            var isLeaf = tree('isLeaf', node.target);
            if (!isLeaf) {
                //清除选中  
                $('#ParentId').combotree('clear');
            }
        }
    });
};

area.doSave = function () {
    if (!$('#area-form').form('validate')) return;
    $.messager.progress();
    $.post(area.settings.urls.save, $('#area-form').serializeArray(), function (result) {
        $.messager.progress('close');
        if (result.success) {
            wrapper.infoShow(appSettings.msg.info, appSettings.msg.saveSuccess);
            area.doReload();
        } else {
            wrapper.infoShow(appSettings.msg.info, appSettings.msg.saveFail);
        }
    });
};

area.doDelete = function () {
    var row = $('#area-datagrid').datagrid('getSelected');
    if (!row) {
        wrapper.infoShow(appSettings.msg.info, appSettings.msg.noSelected);
    } else {
        $.messager.confirm(appSettings.msg.warnning, area.settings.msg.confirmDeleteArea, function (r) {
            if (r) {
                $.post(area.settings.urls.delete + "?id=" + row.Id, function (result) {
                    if (result.success) {
                        wrapper.infoShow(appSettings.msg.info, appSettings.msg.deleteSuccess);
                        area.doReload();
                    } else {
                        wrapper.infoShow(appSettings.msg.info, appSettings.msg.deleteFail);
                    }
                });
            }
        });
    }
};
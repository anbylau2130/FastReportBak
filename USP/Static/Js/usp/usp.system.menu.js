(function () {
    usp.namespace("usp.sysytem.menu");
    usp.sysytem.menu.initMenusDataGrid = function (id, url, name) {
        $(id).datagrid({
            url: url,
            title: '菜单信息',
            queryParams: { actionName: 'datagrid', name: name },
            iconCls: 'icon-clientService',
            fit: true,
            toolbar: '#tb',
            nowrap: false,
            striped: true,
            frozenColumns: [[{
                field: 'Name',
                title: '菜单名称',
                width: 150
            }, {
                field: 'Clazz',
                title: 'Class',
                width: 300
            }]],
            columns: [
                [{
                    field: 'Area',
                    title: 'Area',
                    width: 100
                },
                    {
                        field: 'Controller',
                        title: 'Controller',
                        width: 150
                    }, {
                        field: 'Method',
                        title: 'Method',
                        width: 130
                    }, {
                        field: 'Url',
                        title: '链接',
                        width: 200
                    }, {
                        field: 'Parameter',
                        title: '参数',
                        width: 100
                    }, {
                        field: 'Auditor',
                        title: '审核者',
                        width: 100
                    }, {
                        field: 'Canceler',
                        title: '注销者',
                        width: 100
                    }]
            ],
            pagination: true,
            rownumbers: true,
            singleSelect: true,

        });
    }

    usp.sysytem.menu.search = function (id, toolbar) {
        var $toolbar = $(toolbar), params = {};

        $toolbar.find('.js-param').each(function (i) {
            var obj = $(this);

            if (obj.hasClass('easyui-datebox')) {
                params[obj.attr('comboname')] = $toolbar.find("[name='" + obj.attr('comboname') + "']").val();
            }
            else {
                params[obj.attr('textboxname')] = $toolbar.find("[name='" + obj.attr('textboxname') + "']").val();
            }
        })
        params['actionName'] = 'datagrid';

        $(id).datagrid('load', params);
    };

    //编辑菜单
    usp.sysytem.menu.edit = function (selector) {
        var obj = $(selector).datagrid('getSelected');
        if (obj) {
            location.href = '/System/Menu/Edit?id=' + obj.ID;
        }
        else {
            toastr.warning("请选择您要编辑的数据!");
        }
    }

    //审核
    usp.sysytem.menu.audit = function (selector) {
        var obj = $(selector).datagrid('getSelected');

        if (obj) {
            $.messager.confirm('提示信息', '您确定要审核通过吗?', function (r) {
                if (r) {
                    location.href = '/System/Menu/Audit?id=' + obj.ID;
                }
            });
        }
        else {
            toastr.warning("请选择您要审核通过的数据!");
        }
    }

    //删除菜单
    usp.sysytem.menu.cancel = function (selector) {
        var obj = $(selector).datagrid('getSelected');

        if (obj) {
            $.messager.confirm('提示信息', '您确定要删除吗?', function (r) {
                if (r) {
                    $.ajax({
                        url: '/System/Menu/Cancel',
                        type: 'post',
                        dataType: 'json',
                        data: { id: obj.ID },
                        success: function (res) {
                            if (res.flag) {
                                toastr.success(res.message);
                                $(selector).datagrid('reload');
                            } else {
                                toastr.error(res.message);
                            }
                        },
                        error: function () {
                            toastr.error('您没有操作权限！');
                        }
                    });
                }
            });
        }
        else {
            toastr.warning("请选择您要删除的数据!");
        }
    }

    usp.sysytem.menu.initMenuCombotree = function (menuId, url) {
        $(menuId).combotree({
            url: url,
            method: 'post',
            lines: true,
            queryParams: { actionName: 'menutree', menu: $(menuId).val() },
            onClick: function (node) {
                $(menuId).val(node.id);
            },
            onLoadSuccess: function () {
                //修改时选中菜单并展开到节点
                var menuid = $(menuId).val();
                if (menuid != '0' && menuid != '') {
                    var $tree = $(menuId).combotree('tree');
                    var obj = $tree.tree('find', menuid);
                    if (obj) {
                        $tree.tree('select', obj.target);
                        $tree.tree("expandTo", obj.target);
                    }
                }
            }
        });
    }

    usp.sysytem.menu.checkMenuName = function (menuName, idSelector, parentSelector, url, error) {
        $(menuName).blur(function () {
            var name = $(menuName).val();
            if (name) {
                $.post(url, { actionName: 'checkname', id: $(idSelector).val(), name: name, parent: $(parentSelector).val() }, function (res) {
                    $('#errorname').text(res.message).show();
                }, 'json');
            }
        })
    }
})(this);
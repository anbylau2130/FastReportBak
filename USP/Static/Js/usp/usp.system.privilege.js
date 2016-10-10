(function () {
    usp.namespace("usp.sysytem.privilege");

    ////列表页////
    usp.sysytem.privilege.initGrid = function (dgId, dgUrl, dgToolBar, menuId) {
        $(dgId).datagrid({
            url: dgUrl,
            queryParams: { actionName: 'datagrid', menuId: $(menuId).val() },
            title: '权限信息',
            iconCls: 'icon-clientService',
            fit: true,
            nowrap: false,
            striped: true,
            idField: 'ID',
            columns: [
                [{
                    field: 'Name',
                    title: '名称',
                    width: 100
                }, {
                    field: 'MenuName',
                    title: '菜单',
                    width: 100
                }, {
                    field: 'Url',
                    title: '地址',
                    width: 300
                }, {
                    field: 'Canceler',
                    title: '状态',
                    width: 60,
                    align: 'center',
                    formatter: function (val) {
                        if (val == null)
                            return "正常";
                        else
                            return "注销";
                    }
                }]
            ],
            pagination: true,
            rownumbers: true,
            singleSelect: true,
            toolbar: dgToolBar
            //toolbar: [
            //{
            //    iconCls: 'icon-add',
            //    text: '添加',
            //    handler: function () {
            //        location.href = '/System/privilege/AddPrivilege';
            //    }
            //}, '-', {
            //    iconCls: 'icon-edit',
            //    text: '编辑',
            //    handler: function () {
            //        var row = $(id).datagrid('getSelected');
            //        if (row == null) {
            //            $.messager.alert('提醒', '请选择要编辑的权限！', 'warning');
            //        } else {
            //            location.href = '/System/privilege/EditPrivilege/' + row.ID;
            //        }
            //    }
            //}]
        });
    }

    usp.sysytem.privilege.editPrivilege = function () {
        var row = $(usp.sysytem.privilege.dgId).datagrid('getSelected');
        if (row == null) {
            toastr.warning('请选择要编辑的权限！');
            //$.messager.alert('提醒', '请选择要编辑的权限！', 'warning');
        } else {
            location.href = '/System/Privilege/EditPrivilege/' + row.ID;
        }
    };

    usp.sysytem.privilege.cancelPrivilege = function () {
        var row = $(usp.sysytem.privilege.dgId).datagrid('getSelected');
        if (row == null) {
            toastr.warning('请选择要注销的权限！');
            //$.messager.alert('提醒', '请选择要注销的角色！', 'warning');
        } else {
            if (row.Canceler != null) {
                toastr.warning('权限处于注销状态，无需重复操作！');
                return;
            }
            $.messager.confirm('提醒', '确定要注销该权限吗？', function (r) {
                if (r) {
                    $.ajax({
                        url: usp.sysytem.privilege.cancelUrl,
                        type: 'post',
                        dataType: 'json',
                        data: { privilege: row.ID },
                        success: function (d) {
                            if (d.flag) {
                                toastr.success('注销成功！');
                                //$.messager.alert('提醒', '激活成功！', 'info');
                                $(usp.sysytem.privilege.dgId).datagrid('reload');
                            } else {
                                toastr.error(d.message);
                                //$.messager.alert('提醒', d.message, 'error');
                            }
                        },
                        error: function () {
                            toastr.error('您没有操作权限！');
                            //$.messager.alert('提醒', '您没有操作权限！', 'error');
                        }
                    });
                }
            });
        }
    };

    usp.sysytem.privilege.activePrivilege = function () {
        var row = $(usp.sysytem.privilege.dgId).datagrid('getSelected');
        if (row == null) {
            toastr.warning('请选择要激活的权限！');
            //$.messager.alert('提醒', '请选择要激活的角色！', 'warning');
        } else {
            if (row.Canceler == null) {
                toastr.warning('权限处于激活状态，无需重复操作！');
                return;
            }
            $.messager.confirm('提醒', '确定要激活该权限吗？', function (r) {
                if (r) {
                    $.ajax({
                        url: usp.sysytem.privilege.activeUrl,
                        type: 'post',
                        dataType: 'json',
                        data: { privilege: row.ID },
                        success: function (d) {
                            if (d.flag) {
                                toastr.success('激活成功！');
                                //$.messager.alert('提醒', '激活成功！', 'info');
                                $(usp.sysytem.privilege.dgId).datagrid('reload');
                            } else {
                                toastr.error(d.message);
                                //$.messager.alert('提醒', d.message, 'error');
                            }
                        },
                        error: function () {
                            toastr.error('您没有操作权限！');
                            //$.messager.alert('提醒', '您没有操作权限！', 'error');
                        }
                    });
                }
            });
        }
    };

    usp.sysytem.privilege.searchPrivilege = function () {
        $(usp.sysytem.privilege.dgId).datagrid('load', { actionName: 'datagrid', menuId: $(usp.sysytem.privilege.menuId).val() });
    };

    usp.sysytem.privilege.initComboTree = function (comboTree, menuComboTreeUrl) {
        $(comboTree).combotree({
            url: menuComboTreeUrl,
            queryParams: { actionName: 'menucombotree' },
            onClick: function (node) {
                $(comboTree).val(node.id);
            }
        });
    };


    /////添加编辑页////
    usp.sysytem.privilege.initMenuTree = function (menuId, url) {
        console.log($(menuId).val());
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
    };

    usp.sysytem.privilege.checkMenu = function (menuId, errorId) {
        if ($(menuId).val() == '' || $(menuId).val() == '0') {
            $(errorId).text('请选择菜单');
            $(errorId).show();
            return false;
        }
        $(errorId).text('');
        $(errorId).show();
        return true;
    };

    usp.sysytem.privilege.checkPrivilegeName = function () {
        $(usp.sysytem.privilege.nameId).blur(function () {
            var name = $(this).val();
            var privilegeId = $(usp.sysytem.privilege.privilegeId).val();
            if (name != '') {
                $.post(usp.sysytem.privilege.checkNameUrl, { actionName: 'checkname', name: name, privilegeId: privilegeId, menu: $(usp.sysytem.privilege.menuId).val() }, function (d) {
                    $(usp.sysytem.privilege.errorId).text(d.message).show();
                }, 'json');
            }
        });
    };

    //列表页初始化
    usp.sysytem.privilege.init = function (dgId, dgUrl, dgToolBar, menuId, menuComboTreeUrl, cancelUrl, activeUrl) {
        this.dgId = dgId;
        this.dgUrl = dgUrl;
        this.dgToolBar = dgToolBar;
        this.menuId = menuId;
        this.menuComboTreeUrl = menuComboTreeUrl;
        this.cancelUrl = cancelUrl;
        this.activeUrl = activeUrl;
        usp.sysytem.privilege.initGrid(dgId, dgUrl, dgToolBar, menuId);
        usp.sysytem.privilege.initComboTree(menuId, menuComboTreeUrl);
    };

    //添加编辑页初始化
    usp.sysytem.privilege.initAddOrEdit = function (menuId, treeUrl, nameId, errorId, checkNameUrl, privilegeId) {
        this.treeUrl = treeUrl;
        this.menuId = menuId;
        this.nameId = nameId;
        this.errorId = errorId;
        this.checkNameUrl = checkNameUrl;
        this.privilegeId = privilegeId;
        usp.sysytem.privilege.initMenuTree(menuId, treeUrl);
        usp.sysytem.privilege.checkPrivilegeName();
    };
})(this);
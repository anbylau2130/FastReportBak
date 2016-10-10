(function () {
    usp.namespace("usp.sysytem.role");

    ////列表页////
    usp.sysytem.role.initRoleGrid = function (dgId, dgUrl, dgToolBar, roleName) {
        $(dgId).datagrid({
            url: dgUrl,
            queryParams: { actionName: 'datagrid', roleName: $(roleName).val() },
            title: '角色信息',
            iconCls: 'icon-clientService',
            nowrap: false,
            fit: true,
            striped: true,
            idField: "ID",
            pagination: true,
            rownumbers: true,
            singleSelect: true,
            columns: [
            [
            {
                field: 'Name',
                title: '角色名称',
                width: 200
            }, {
                field: 'Remark',
                title: '备注',
                width: 200
            }, {
                field: 'Canceler',
                title: '状态',
                width: 60,
                align:'center',
                formatter: function (val) {
                    if (val == null)
                        return "正常";
                    else
                        return "注销";
                }
            }, {
                field: 'CreateTime',
                title: '创建时间',
                width: 160,
                formatter: function (val) {
                    return usp.ParseUTCDate(val);
                }
            }
            ]
            ],
            toolbar: dgToolBar
            //toolbar: [
            //{
            //    iconCls: 'icon-add',
            //    text: '添加',
            //    handler: function () {
            //        location.href = '/System/Role/AddRole';
            //    }
            //}, '-', {
            //    iconCls: 'icon-edit',
            //    text: '编辑',
            //    handler: function () {
            //        var row = $(id).datagrid('getSelected');
            //        if (row == null) {
            //            $.messager.alert('提醒', '请选择要编辑的角色！', 'warning');
            //        } else {
            //            if (row.Type == true) {
            //                $.messager.alert('提醒', '您不能编辑管理员角色！', 'warning');
            //            } else {
            //                location.href = '/System/Role/EditRole/' + row.ID;
            //            }
            //        }
            //    }
            //}, '-', {
            //    iconCls: 'icon-remove',
            //    text: '注销',
            //    handler: function () {
            //        var row = $(id).datagrid('getSelected');
            //        if (row == null) {
            //            $.messager.alert('提醒', '请选择要注销的角色！', 'warning');
            //        } else {
            //        }
            //    }
            //}, '-', {
            //    iconCls: 'icon-redo',
            //    text: '激活',
            //    handler: function () {
            //        var row = $(id).datagrid('getSelected');
            //        if (row == null) {
            //            $.messager.alert('提醒', '请选择要激活的角色！', 'warning');
            //        } else {
            //        }
            //    }
            //}]
        });
    }
    usp.sysytem.role.editRole = function () {
        var row = $(usp.sysytem.role.dgId).datagrid('getSelected');
        if (row == null) {
            toastr.warning('请选择要编辑的角色！');
            //$.messager.alert('提醒', '请选择要编辑的角色！', 'warning');
        } else {
            location.href = '/System/Role/EditRole/' + row.ID;
        }
    };

    usp.sysytem.role.cancelRole = function () {
        var row = $(usp.sysytem.role.dgId).datagrid('getSelected');
        if (row == null) {
            toastr.warning('请选择要注销的角色！');
            //$.messager.alert('提醒', '请选择要注销的角色！', 'warning');
        } else {
            if (row.Canceler != null) {
                toastr.warning('角色处于注销状态，无需重复操作！');
                return;
            }
            $.messager.confirm('提醒', '确定要注销该角色吗？', function (r) {
                if (r) {
                    $.ajax({
                        url: usp.sysytem.role.cancelUrl,
                        type: 'post',
                        dataType: 'json',
                        data: { role: row.ID },
                        success: function (d) {
                            if (d.flag) {
                                toastr.success('注销成功！');
                                //$.messager.alert('提醒', '激活成功！', 'info');
                                $(usp.sysytem.role.dgId).datagrid('reload');
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

    usp.sysytem.role.activeRole = function () {
        var row = $(usp.sysytem.role.dgId).datagrid('getSelected');
        if (row == null) {
            toastr.warning('请选择要激活的角色！');
            //$.messager.alert('提醒', '请选择要激活的角色！', 'warning');
        } else {
            if (row.Canceler == null) {
                toastr.warning('角色处于激活状态，无需重复操作！');
                return;
            }
            $.messager.confirm('提醒', '确定要激活该角色吗？', function (r) {
                if (r) {
                    $.ajax({
                        url: usp.sysytem.role.activeUrl,
                        type: 'post',
                        dataType: 'json',
                        data: { role: row.ID },
                        success: function (d) {
                            if (d.flag) {
                                toastr.success('激活成功！');
                                //$.messager.alert('提醒', '激活成功！', 'info');
                                $(usp.sysytem.role.dgId).datagrid('reload');
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

    usp.sysytem.role.searchRole = function () {
        $(usp.sysytem.role.dgId).datagrid('load', { actionName: 'datagrid', roleName: $(usp.sysytem.role.roleName).val() });
    };

    /////添加编辑页////
    usp.sysytem.role.initMenuPrivilegeTree = function (treeId, url, roleId) {
        $(treeId).tree({
            url: url,
            method: 'post',
            queryParams: { actionName: 'menutree', role: $(roleId).val() },
            checkbox: true,
            lines: true,
            onCheck: function (node) {
                usp.sysytem.role.setVaule();
            },
            onLoadSuccess: function () {
                usp.sysytem.role.setVaule();
            }
        });
    };

    usp.sysytem.role.setVaule = function () {
        var checknodes = $(usp.sysytem.role.treeId).tree('getChecked', ['checked', 'indeterminate']); //选择和半选择的节点
        var menus = '';
        var privileges = '';
        for (i = 0; i < checknodes.length; i++) {
            var type = checknodes[i].attributes.type;
            var id = checknodes[i].id;
            if (type == 1) {
                if (menus != '') menus += ',';
                menus += id;
            } else {
                if (privileges != '') privileges += ',';
                privileges += id;
            }
        };
        $(usp.sysytem.role.menuId).val(menus);
        $(usp.sysytem.role.privilegeId).val(privileges);
    }

    usp.sysytem.role.checkPrivilieges = function () {
        if ($(usp.sysytem.role.menuId).val() == '') {
            toastr.warning('请选择权限！');
            //$.messager.alert('提醒', '请选择权限！', 'warning');
            return false;
        }
        return true;
    }

    usp.sysytem.role.checkRoleName = function () {
        $(usp.sysytem.role.nameId).blur(function () {
            var name = $(this).val();
            var role = $(usp.sysytem.role.roleId).val();
            if (name != '') {
                $.post(usp.sysytem.role.checkNameUrl, { actionName: 'checkname', name: name, role: role }, function (d) {
                    $(usp.sysytem.role.errorId).text(d.message).show();
                }, 'json');
            }
        });
    };

    //列表页初始化
    usp.sysytem.role.init = function (dgId, dgUrl, dgToolBar, roleName, cancelUrl, activeUrl) {
        this.dgId = dgId;
        this.dgUrl = dgUrl;
        this.dgToolBar = dgToolBar;
        this.roleName = roleName;
        this.cancelUrl = cancelUrl;
        this.activeUrl = activeUrl;
        usp.sysytem.role.initRoleGrid(dgId, dgUrl, dgToolBar, roleName);
    };

    //添加编辑页初始化
    usp.sysytem.role.initAddOrEdit = function (treeId, treeUrl, roleId, menuId, privilegeId, nameId, errorId, checkNameUrl) {
        this.treeId = treeId;
        this.treeUrl = treeUrl;
        this.roleId = roleId;
        this.menuId = menuId;
        this.privilegeId = privilegeId;
        this.nameId = nameId;
        this.errorId = errorId;
        this.checkNameUrl = checkNameUrl;
        usp.sysytem.role.initMenuPrivilegeTree(treeId, treeUrl, roleId);
        usp.sysytem.role.checkRoleName();
    };
})(this);
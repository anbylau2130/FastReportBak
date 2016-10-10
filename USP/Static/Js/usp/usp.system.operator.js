(function () {
    usp.namespace("usp.system.operator");
    usp.system.operator.initGrid = function (id, url, dgToolBar1, dgToolBar2) {
        $(id).datagrid({
            url: url,
            title: '操作员信息',
            queryParams: { type: '', name: '', status: -1, actionName: 'datagrid' },
            iconCls: 'icon-clientService',
            fit: true,
            nowrap: false,
            striped: true,
            frozenColumns: [
                 [{
                     field: 'LoginName',
                     title: '登录名',
                     width: 120
                 }, {
                     field: 'RealName',
                     title: '姓名',
                     width: 120
                 }]
            ],
            columns: [
                [{
                    field: 'Mobile',
                    title: '手机号',
                    width: 100
                }, {
                    field: 'IdCard',
                    title: '身份证号',
                    width: 120
                }, {
                    field: 'Email',
                    title: '邮箱',
                    width: 200
                } ,{
                    field: 'Supplier',
                    title: '供应商编号',
                    width: 200
                },  {
                    field: 'SupplierName',
                    title: '供应商名称',
                    width: 200
                },{
                    field: 'Status',
                    title: '状态',
                    width: 60,
                    formatter: function (val) {
                        if (val == 0) {
                            return "正常";
                        }
                        if (val == 1) {
                            return "冻结";
                        }
                        if (val == 2) {
                            return "注销";
                        }
                        if (val == 3) {
                            return "待审核";
                        }
                        if (val == 4) {
                            return "待验证";
                        }
                        return val;
                    }
                }, {
                    field: 'CreateTime',
                    title: '创建时间',
                    width: 160,
                    formatter: function (val) {
                        return usp.ParseUTCDate(val);
                    }
                }, {
                    field: 'AuditTime',
                    title: '审核时间',
                    width: 160,
                    formatter: function (val) {
                        return usp.ParseUTCDate(val);
                    }
                }, {
                    field: 'CancelTime',
                    title: '注销时间',
                    width: 160,
                    formatter: function (val) {
                        return usp.ParseUTCDate(val);
                    }
                }]
            ],
            toolbar: '#operatortoolbarmenu,#operatortoolbarsearch',
            pagination: true,
            rownumbers: true,
            singleSelect: true
        });
    }

    usp.system.operator.initCombobox = function (comboId, comboUrl) {
        $(comboId).combobox({
            url: comboUrl,
            queryParams: { actionName: 'Combobox' },
            valueField: 'id',
            textField: 'text',
            onSelect: function() {
                usp.system.operator.search();
            }
        });
    }

    usp.system.operator.append = function () {
        location.href = '/System/Operator/Create';
    }

    usp.system.operator.edit = function () {
        var opr = $('#operator').datagrid("getSelected");
        if (opr) {
            location.href = '/System/Operator/Edit?id=' + opr.ID;
        } else {
            $.messager.alert('提示信息', '请选择要编辑的操作员！');
        }
    }

    usp.system.operator.search = function () {
        var type = $('#name').searchbox('getName');
        var name = $('#name').searchbox('getValue');
        var status = $("#status").combobox('getValue');
        $('#operator').datagrid('load', { actionName: 'datagrid', type: type, name: name, status: status });
    }


    usp.system.operator.supplier= function() {
        var opr = $('#operator').datagrid("getSelected");
        if (opr) {
            location.href = '/System/Operator/RelationSupplier?id=' + opr.ID;
        } else {
            $.messager.alert('提示信息', '请选择要编辑的操作员！');
        }
    }
    usp.system.operator.audit = function () {
        var opr = $('#operator').datagrid("getSelected");
        if (opr) {
            if (opr.Auditor != null) {
                toastr.warning('操作员已通过审核，无需重复操作！');
                return;
            }
            if (opr.Canceler != null) {
                toastr.warning('操作员处于注销状态，无法审核！');
                return;
            }
            $.messager.confirm('提示信息', '确定要审核通过此操作员？', function (r) {
                if (r) {
                    $.ajax({
                        url: usp.system.operator.auditUrl,
                        type: 'post',
                        dataType: 'json',
                        data: { opid: opr.ID },
                        success: function (d) {
                            if (d.flag) {
                                toastr.success('审核通过成功！');
                                $(usp.system.operator.dgId).datagrid('reload');
                            }
                            else {
                                toastr.error(d.message);
                            }
                        },
                        error: function () {
                            toastr.error('您没有操作权限！');
                        }
                    });
                }
            });
        } else {
            toastr.error('请选择要审核通过的操作员！');
        }
    }

    usp.system.operator.cancel = function () {
        var opr = $(usp.system.operator.dgId).datagrid('getSelected');
        if (opr) {
            if (opr.Canceler != null) {
                toastr.warning('权限处于注销状态，无需重复操作！');
                return;
            }
            $.messager.confirm('提示信息', '确定要注销此操作员？', function (r) {
                if (r) {
                    $.ajax({
                        url: usp.system.operator.cancelUrl,
                        type: 'post',
                        dataType: 'json',
                        data: { opid: opr.ID },
                        success: function (d) {
                            if (d.flag) {
                                toastr.success('注销成功！');
                                $(usp.system.operator.dgId).datagrid('reload');
                            }
                            else {
                                toastr.error(d.message);
                            }
                        },
                        error: function () {
                            toastr.error('您没有操作权限！');
                        }
                    })
                }
            });
        } else {
            toastr.error('请选择要注销的操作员！');
        }
    }

    usp.system.operator.active = function () {
        var opr = $('#operator').datagrid("getSelected");
        if (opr) {
            if (opr.Canceler == null) {
                toastr.warning('权限处于激活状态，无需重复操作！');
                return;
            }
            $.messager.confirm('提示信息', '确定要激活此操作员？', function (r) {
                if (r) {
                    $.ajax({
                        url: usp.system.operator.activeUrl,
                        type: 'post',
                        dataType: 'json',
                        data: { opid: opr.ID },
                        success: function (d) {
                            if (d.flag) {
                                toastr.success('激活成功！');
                                $(usp.system.operator.dgId).datagrid('reload');
                            }
                            else {
                                toastr.error(d.message);
                            }
                        },
                        error: function () {
                            toastr.error('您没有操作权限！');
                        }
                    });
                }
            });
        } else {
            toastr.error('请选择要激活的操作员！');
        }
    }

    usp.system.operator.resetPassword = function () {
        var opr = $('#operator').datagrid("getSelected");
        if (opr) {
                    $('#resetpassword').dialog({
                        minimizable: true,
                        maximizable: true,
                        buttons: [{
                            text: '提交',
                            iconCls: 'icon-ok',
                            handler: function () {
                                var newpassword = $('#newpassword').textbox('getValue');
                                $.ajax({
                                    url: usp.system.operator.resetPsdUrl,
                                    type: 'post',
                                    dataType: 'json',
                                    data: { opid: opr.ID, newpwd: newpassword },
                                    success: function (d) {
                                        if (d.flag) {
                                            $('#resetpassword').dialog('close');
                                            toastr.success('重置密码成功！');
                                            $(usp.system.operator.dgId).datagrid('reload');
                                        }
                                        else {
                                            toastr.error(d.message);
                                        }
                                    },
                                    error: function () {
                                        toastr.error('您没有操作权限！');
                                    }
                                });

                            }
                        }, {
                            text: '取消',
                            iconCls: 'icon-cancel',
                            handler: function () {
                                $('#resetpassword').dialog('close');
                            }
                        }]
                    });
                    $('#resetpassword').dialog('open');
            //     }
            //});
        } else {
            $.messager.alert('提示信息', '请选择要重置密码的操作员！');
        }
    }




    /////添加编辑页////
    usp.system.operator.initRoleTree = function (treeId, url) {
        $(treeId).tree({
            url: url,
            method: 'post',
            //queryParams: { actionName: 'menutree', roleid: $(roleId).val() },
            queryParams: { actionName: 'menutree', opid: $('#hdid').val() },
            checkbox: true,
            lines: true,
            onCheck: function (node) {
                usp.system.operator.setVaule();
            },
            onLoadSuccess: function () {
                usp.system.operator.setVaule();
            }
        });
    };

    usp.system.operator.setVaule = function () {
        var checknodes = $('#operatorRole').tree('getChecked', ['checked', 'indeterminate']); //选择和半选择的节点
        var roles = '';
        for (i = 0; i < checknodes.length; i++) {
            var type = checknodes[i].attributes.type;
            var id = checknodes[i].id;
            if (type == 1) {
                if (roles != '') roles += ',';
                roles += id;
            } 
        };
        $('#hdrole').val(roles);
    }

    usp.system.operator.checkCreateLoginName=function() {
        $('#LoginName').blur(function() {
            var name = $('#LoginName').val();
            if (name != '') {
                $.post('/System/Operator/Create', { actionName: 'checkname', name: name }, function (d) {
                    $('#errorname').text(d.message).show();
                }, 'json');
            }
        });
    }

    usp.system.operator.checkEditLoginName = function (oldName) {
        $('#LoginName').blur(function () {
            var name = $('#LoginName').val();
            if (name != '' && name != oldName) {
                $.post('/System/Operator/Edit', { actionName: 'checkname', name: name }, function (d) {
                    $('#errorname').text(d.message).show();
                }, 'json');
            }
        });
    }

    usp.system.operator.checkRole = function () {
        if ($('#hdrole').val() == '') {
            toastr.warning('请选择角色！');
            return false;
        }
        return true;
    }

    //列表页初始化
    usp.system.operator.init = function (dgId, dgUrl, dgToolBar1, dgToolBar2, auditUrl, cancelUrl, activeUrl, resetPsdUrl,comboId,comboUrl,supplierUrl) {
        this.dgId = dgId;
        this.dgUrl = dgUrl;
        this.dgToolBar1 = dgToolBar1;
        this.dgToolBar2 = dgToolBar2;
        this.auditUrl = auditUrl;
        this.cancelUrl = cancelUrl;
        this.activeUrl = activeUrl;
        this.resetPsdUrl = resetPsdUrl;
        usp.system.operator.initGrid(dgId, dgUrl, dgToolBar1, dgToolBar2);
        usp.system.operator.initCombobox(comboId, comboUrl);
    }

})(this);
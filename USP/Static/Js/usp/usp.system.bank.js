(function () {
    usp.namespace("usp.system.bank");
    usp.system.bank.initBankGrid = function (dgId, dgUrl, dgToolBar) {
        $(dgId).datagrid({
            url: dgUrl,
            title: '银行信息',
            queryParams: { name: '', type: -1, item: '' },
            iconCls: 'icon-clientService',
            fit: true,
            nowrap: false,
            striped: true,
            frozenColumns: [
                 [{
                     field: 'Number',
                     title: '银行代码',
                     width: 60
                 }, {
                     field: 'Name',
                     title: '银行名称',
                     width: 120
                 }]
            ],
            columns: [
                [{
                    field: 'ShortName',
                    title: '简写',
                    width: 100
                }, {
                    field: 'NiceName',
                    title: '别名',
                    width: 100
                },
                {
                    field: 'Type',
                    title: '银行卡类型',
                    width: 100,
                    formatter: function (val) {
                        if (val == 1) {
                            return "储蓄卡";
                        }
                        if (val == 2) {
                            return "信用卡";
                        }
                        if (val == 3) {
                            return "支付宝";
                        }
                        return val;
                    }
                }, {
                    field: 'Url',
                    title: '银行图标',
                    width: 100,
                    formatter: function (val) {
                        return "<img src=" + val + " width='50',height='50'/>";
                    }
                }, {
                    field: 'Status',
                    title: '是否可用',
                    width: 60,
                    formatter: function (val) {
                        if (val == 0) {
                            return "可用";
                        }
                        if (val == 1) {
                            return "不可用";
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
                }]
            ],
            toolbar: dgToolBar,
            pagination: true,
            rownumbers: true,
            singleSelect: true
        });
    }

    usp.system.bank.append = function () {
        location.href = '/System/bank/Create';
    }

    usp.system.bank.eidt = function () {
        var bank = $('#bank').datagrid("getSelected");
        if (bank!=null) {            
                    location.href = '/System/bank/Edit?id=' + bank.ID;              
        } else {
            //$.messager.alert('提示信息', '请选择要编辑的银行！');
            toastr.warning('请选择要编辑的银行！');
        }
    }
    
    usp.system.bank.search = function () {
        var name = $('#name').searchbox('getValue');
        var item = $('#name').searchbox('getName');
        var type = $('#type').combobox('getValue');
        $('#bank').datagrid('load', { name: name, type: type,item:item });
    }

    usp.system.bank.cancel = function () {
        var bank = $('#bank').datagrid("getSelected");
        if (bank) {
            $.messager.confirm('提示信息', '确定要注销此银行吗？', function (r) {
                if (r) {
                    if (bank.Canceler != null) {
                        toastr.warning('银行处于注销状态，无需重复操作！');
                        return;
                    }
                    //location.href = '/System/bank/Cancle?id=' + bank.ID;
                    $.ajax({
                        url: usp.system.bank.cancelUrl,
                        type: 'post',
                        dataType: 'json',
                        data: { bankid: bank.ID },
                        success:function(d) {
                            if (d.flag) {
                                toastr.success('注销成功！');
                                $(usp.system.bank.dgId).datagrid('reload');
                            } else {
                                toastr.error(d.message);
                            }
                        },
                        error:function() {
                            toastr.error('您没有操作权限！');
                        }
                    });
                }
            });
        } else {
            //$.messager.alert('提示信息', '请选择要注销的银行！');
            toastr.error('请选择要注销的银行!');
        }
    }

    usp.system.bank.active = function () {
        var bank = $('#bank').datagrid("getSelected");
        if (bank) {
            $.messager.confirm('提示信息', '确定要激活此银行？', function (r) {
                if (r) {
                    if (bank.Canceler == null) {
                        toastr.warning('银行处于激活状态，无需重复操作！');
                        return;
                    }
                    //location.href = '/System/bank/Active?id=' + opr.ID;
                    $.ajax({
                        url: usp.system.bank.activeUrl,
                        type: 'post',
                        dataType: 'json',
                        data: { bankid: bank.ID },
                        success: function (d) {
                            if (d.flag) {
                                toastr.success('激活成功！');
                                $(usp.system.bank.dgId).datagrid('reload');
                            } else {
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
            //$.messager.alert('提示信息', '请选择要激活的银行！');
            toastr.error('请选择要激活的银行！');
        }
    }

    //usp.system.bank.checkType = function () {
    //    var type = $("#type").combobox('getValue');
    //    if(type==null)
    //    {
    //        ModelState.AddModelError("Name", "已存在");
    //    }
    //}

    //usp.system.bank.selectedBankType = function()
    //{
    //    var type = $('#type').combobox('getValue');
    //}

    //usp.system.bank.changelogo = function ()
    //{
    //    //$.messager.alert('Me!');
    //    var imgsrc = $('#Logo').filebox('getValue');
    //    $('#preview').image.src = imgsrc;
    //}

    //检查银行名称是否重复
    usp.system.bank.checkBankName = function (oldName) {
        $('#Name').blur(function () {
            var name = $('#Name').val();
            if (name != '' && name != oldName) {
                $.post('/System/Bank/CheckName', { name: name }, function (d) {
                    $('#errorname').text(d.message).show();
                }, 'json');
            }
        });
    }

    usp.system.bank.checkType = function () {
        if ($('#Type').combobox('getValue') == 0) {
            toastr.warning('请选择银行类型！');
            return false;
        }
        return true;
    }

    //列表页初始化
    usp.system.bank.init = function (dgId, dgUrl, dgToolBar, cancelUrl, activeUrl) {
        this.dgId = dgId;
        this.dgUrl = dgUrl;
        this.dgToolBar = dgToolBar;
        //this.editUrl = editUrl;
        this.cancelUrl = cancelUrl;
        this.activeUrl = activeUrl;
        usp.system.bank.initBankGrid(dgId, dgUrl, dgToolBar);
    }

})(this);
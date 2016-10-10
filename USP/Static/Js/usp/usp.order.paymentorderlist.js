
(function () {
    usp.namespace("usp.order.paymentorderlist");
    usp.order.paymentorderlist.iniCount = 0;
    usp.order.paymentorderlist.initGrid = function (id, url, toolbarmenu, toolbarsearch) {


        $(id).datagrid({
            url: url,
            title: '付款单信息',
            queryParams: { actionName: 'paymentorderlistdatagrid' },
            iconCls: 'icon-clientService',
            fit: true,
            nowrap: false,
            striped: true,
            columns: [
                [
                     //{ field: 'RowNo', title: 'RowNo', width: 150 },
                     //{ field: 'RowCnt', title: 'RowCnt', width: 150 },
                     //{ field: 'FSUPPLIER', title: 'FSUPPLIER', width: 150 },
                     //{ field: '单据类型', title: '单据类型', width: 150 },
                     //{ field: '单据编号', title: '单据编号', width: 150 },
                    
                     //{ field: '收款单位类型', title: '收款单位类型', width: 150 },
                     //{ field: '收款单位', title: '收款单位', width: 150 },
                     //{ field: '币别', title: '币别', width: 150 },
                     //{ field: '应付款', title: '应付款', width: 150 },
                      { field: '付款组织', title: '付款组织', width: 200 },
                      { field: '实付款', title: '实付款', align:'right', width: 300 },
                    
                      {
                          field: '业务日期', title: '业务日期', width: 160, formatter: function (val) {
                              return usp.ParseUTCDate(val);
                          }
                      },
                     //{ field: '结算组织', title: '结算组织', width: 150 },
                     //{ field: '采购部门', title: '采购部门', width: 150 },
                     //{ field: '采购组织', title: '采购组织', width: 150 },
                     //{ field: '采购组', title: '采购组', width: 150 },
                     //{ field: '采购员', title: '采购员', width: 150 }

                ]
            ],
            toolbar: toolbarsearch,//toolbarmenu + "," +
            pagination: true,
            rownumbers: true,
            singleSelect: true,
            onLoadSuccess: function () {
                if (usp.order.paymentorderlist.iniCount === 0) {
                    var fields = $(datagrid).datagrid('getColumnFields');
                    var muit = "";
                    for (var i = 0; i < fields.length; i++) {
                        var opts = $(datagrid).datagrid('getColumnOption', fields[i]);
                        if (opts.title.indexOf("日期") > 0) {
                            continue;
                        }
                        muit += "<div data-options=\"name:'" + fields[i] + "'\" >" + opts.title + "</div>";
                    };
                    $("#mm").html(muit);
                    $('#searchbox').searchbox({
                        prompt: '请输入',
                        searcher: function () {
                            var type = $('#searchbox').searchbox('getName');
                            var name = $('#searchbox').searchbox('getValue');
                            $(datagrid).datagrid('load', { actionName: 'paymentorderlistdatagrid', type: type, name: name, beginTime: $("#beginTime").datebox('getValue'), endTime: $("#endTime").datebox('getValue') });
                        },
                        menu: "#mm",
                        width: 300
                    });
                    usp.order.paymentorderlist.iniCount++;
                }
            }
        });
    }

    usp.order.paymentorderlist.add = function (url) {
        location.href = url;
    }

    usp.order.paymentorderlist.edit = function (datagrid, url) {
        var model = $(datagrid).datagrid("getSelected");
        if (model) {
            location.href = url + model.ID;
        } else {
            $.messager.alert('提示信息', '请选择要编辑数据！');
        }
    }

    usp.order.paymentorderlist.audit = function (datagrid, url) {
        var model = $(datagrid).datagrid("getSelected");
        if (model) {
            if (model.Auditor != null) {
                toastr.warning('数据已通过审核，无需重复操作！');
                return;
            }
            if (model.Canceler != null) {
                toastr.warning('数据处于注销状态，无法审核！');
                return;
            }
            $.messager.confirm('提示信息', '确定要审核通过此操作员？', function (r) {
                if (r) {
                    $.ajax({
                        url: url,
                        type: 'post',
                        dataType: 'json',
                        data: { id: model.ID },
                        success: function (data) {
                            if (data.flag) {
                                toastr.success('审核通过成功！');
                                $(datagrid).datagrid('reload');
                            } else {
                                toastr.error(data.message);
                            }
                        },
                        error: function () {
                            toastr.error('您没有操作权限！');
                        }
                    });
                }
            });
        } else {
            toastr.error('请选择要审核通过的数据！');
        }
    }

    usp.order.paymentorderlist.cancel = function (datagrid, url) {
        var model = $(datagrid).datagrid('getSelected');
        if (model) {
            if (model.Canceler != null) {
                toastr.warning('权限处于注销状态，无需重复操作！');
                return;
            }
            $.messager.confirm('提示信息', '确定要注销此数据？', function (r) {
                if (r) {
                    $.ajax({
                        url: url,
                        type: 'post',
                        dataType: 'json',
                        data: { id: model.ID },
                        success: function (data) {
                            if (data.flag) {
                                toastr.success('注销成功！');
                                $(datagrid).datagrid('reload');
                            } else {
                                toastr.error(data.message);
                            }
                        },
                        error: function () {
                            toastr.error('您没有操作权限！');
                        }
                    })
                }
            });
        } else {
            toastr.error('请选择要注销的数据！');
        }
    }

    usp.order.paymentorderlist.active = function (datagrid, url) {
        var model = $(datagrid).datagrid("getSelected");
        if (model) {
            if (model.Canceler == null) {
                toastr.warning('权限处于激活状态，无需重复操作！');
                return;
            }
            $.messager.confirm('提示信息', '确定要激活此操作员？', function (r) {
                if (r) {
                    $.ajax({
                        url: url,
                        type: 'post',
                        dataType: 'json',
                        data: { id: model.ID },
                        success: function (data) {
                            if (data.flag) {
                                toastr.success('激活成功！');
                                $(datagrid).datagrid('reload');
                            } else {
                                toastr.error(data.message);
                            }
                        },
                        error: function () {
                            toastr.error('您没有操作权限！');
                        }
                    });
                }
            });
        } else {
            toastr.error('请选择要激活的数据！');
        }
    }

    usp.order.paymentorderlist.init = function (id, url, toolbarmenu, toolbarsearch) {
        usp.order.paymentorderlist.initGrid(id, url, toolbarmenu, toolbarsearch);
    }
})(this);
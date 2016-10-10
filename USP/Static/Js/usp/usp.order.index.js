(function () {
    usp.namespace("usp.order.index");
    usp.order.index.iniCount = 0;
    usp.order.index.initGrid = function (id, url, toolbarmenu, toolbarsearch) {


        $(id).datagrid({
            url: url,
            title: '采购单信息',
            queryParams: { actionName: 'orderdatagrid'},
            iconCls: 'icon-clientService',
            fit: true,
            nowrap: false,
            striped: true,
            columns: [
                [
                //{ field: 'FSUPPLIERID', title: 'FSUPPLIERID', width: 150 },
                    //{ field: '单据类型', title: '单据类型', width: 150 },
                    //{ field: '单据编号', title: '单据编号', width: 150 },
                    //{ field: '供应商', title: '供应商', width: 150 },
                    //{ field: '单据状态', title: '单据状态', width: 150 },
                    //{ field: '指定供应商', title: '指定供应商', width: 150 },
                    //{ field: '采购部门', title: '采购部门', width: 150 },
                    //{ field: '采购组织', title: '采购组织', width: 150 },
                    //{ field: '采购组', title: '采购组', width: 150 },
                    //{ field: '采购员', title: '采购员', width: 150 },
                    //{ field: '关闭状态', title: '关闭状态', width: 150 },
                    //{ field: '供货方', title: '供货方', width: 150 },
                    //{ field: '结算方', title: '结算方', width: 150 },
                    //{ field: '收款方', title: '收款方', width: 150 },
                    //{ field: '创建人', title: '创建人', width: 150 },
                    //{
                    //    field: '创建时间', title: '创建时间', width: 150,
                    //    formatter: function (val) {
                    //        return usp.ParseUTCDate(val);
                    //    }
                    //},
                    //{ field: '审核人', title: '审核人', width: 150 },
                    //{
                    //    field: '审核时间', title: '审核时间', width: 150, formatter: function (val) {
                    //        return usp.ParseUTCDate(val);
                    //    }
                    //},
                    //{ field: '交货地点', title: '交货地点', width: 150 },
                    //{ field: '是否赠品', title: '是否赠品', width: 150 },
                    //{ field: '业务关闭', title: '业务关闭', width: 150 },
                    //{ field: '单价', title: '单价', width: 150 },
                    { field: '物料名称', title: '物料名称', width: 150 },
                    { field: '物料编码', title: '物料编码', width: 100 },
                    { field: '物料说明', title: '物料说明', width: 150 },
                    { field: '计量单位', title: '计量单位', width: 60 },
                    { field: '数量', title: '数量', align: 'right', width: 80 },
                    { field: '含税单价', title: '含税单价', align: 'right', width: 130 },
                    { field: '金额', title: '金额', align: 'right', width: 130 },
                      {
                          field: '交货日期', title: '交货日期', width: 160,
                          formatter: function (val) {
                              return usp.ParseUTCDate(val);
                          }
                      },
                      {
                          field: '采购日期', title: '采购日期', width: 160,
                          formatter: function (val) {
                              return usp.ParseUTCDate(val);
                          }
                      }
                    //{ field: '税组合', title: '税组合', width: 150 },
                    //{ field: '价税合计', title: '价税合计', width: 150 },
                    //{ field: '税率名称', title: '税率名称', width: 150 },
                    //{ field: '税额', title: '税额', width: 150 },
                    //{ field: '计入成本金额', title: '计入成本金额', width: 150 },
                    //{ field: '增值税', title: '增值税', width: 150 },
                    //{ field: '卖方代扣代缴', title: '卖方代扣代缴', width: 150 },
                    //{ field: '买方代扣代缴', title: '买方代扣代缴', width: 150 },
                    //{ field: '付款条件', title: '付款条件', width: 150 },
                    //{ field: '折扣率_', title: '折扣率_', width: 150 },
                    //{ field: '税率_', title: '税率_', width: 150 },
                    //{ field: '税务_税率_', title: '税务_税率_', width: 150 },
                    //{ field: '计入成本比例_', title: '计入成本比例', width: 150 }
                ]
            ],
            toolbar: toolbarmenu + "," + toolbarsearch,
            pagination: true,
            rownumbers: true,
            singleSelect: true,
            onLoadSuccess: function () {
                if (usp.order.index.iniCount === 0) {
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
                            $(datagrid).datagrid('load', { actionName: 'orderdatagrid', type: type, name: name, beginTime: $("#beginTime").datebox('getValue'), endTime: $("#endTime").datebox('getValue') });
                        },
                        menu: "#mm",
                        width: 300
                    });
                    usp.order.index.iniCount++;
                }
            }
        });
    }

    usp.order.index.add = function (url) {
        location.href = url;
    }

    usp.order.index.edit = function (datagrid, url) {
        var model = $(datagrid).datagrid("getSelected");
        if (model) {
            location.href = url + model.ID;
        } else {
            $.messager.alert('提示信息', '请选择要编辑数据！');
        }
    }

    usp.order.index.audit = function (datagrid, url) {
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

    usp.order.index.cancel = function (datagrid, url) {
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

    usp.order.index.active = function (datagrid, url) {
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

    usp.order.index.init = function (id, url, toolbarmenu, toolbarsearch) {
        usp.order.index.initGrid(id, url, toolbarmenu, toolbarsearch);
    }
})(this);
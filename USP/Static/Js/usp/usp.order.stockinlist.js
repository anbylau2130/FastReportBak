(function () {
    usp.namespace("usp.order.stockinlist");
    usp.order.stockinlist.iniCount = 0;
    usp.order.stockinlist.initGrid = function (id, url, toolbarmenu, toolbarsearch) {


        $(id).datagrid({
            url: url,
            title: '入库单信息',
            queryParams: { actionName: 'stockinlistdatagrid' },
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
             //{ field: '批号', title: '批号', width: 150 },
             { field: '物料编号', title: '物料编号', width: 150 },
             { field: '物料名称', title: '物料名称', width: 150 },
             { field: '单位', title: '单位', width: 60 },
             { field: '实收数量', title: '实收数量', align: 'right', width: 150 },
             //{ field: '应收数量', title: '应收数量', width: 150 },
             { field: '单价', title: '单价',align:'right', width: 150 },
             { field: '含税单价', title: '含税单价', align: 'right', width: 150 },
             { field: '金额', title: '金额', align: 'right', width: 150 },
              {
                  field: '入库日期', title: '入库日期', width: 160, formatter: function (val) {
                      return usp.ParseUTCDate(val);
                  }
              },
             //{ field: '价税合计', title: '价税合计', width: 150 },
             //{ field: '税率_', title: '税率_', width: 150 },
             //{ field: '税额', title: '税额', width: 150 },
             //{ field: '仓库', title: '仓库', width: 150 },
             //{ field: '是否赠品', title: '是否赠品', width: 150 },
             //{ field: '库存状态', title: '库存状态', width: 150 },
             //{ field: '备注', title: '备注', width: 150 },
             //{ field: '金额_本位币_', title: '金额_本位币_', width: 150 },
             //{ field: '成本价', title: '成本价', width: 150 },
             //{ field: '总成本', title: '总成本', width: 150 },
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
                if (usp.order.stockinlist.iniCount === 0) {
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
                            $(datagrid).datagrid('load', { actionName: 'stockinlistdatagrid', type: type, name: name ,beginTime: $("#beginTime").datebox('getValue'), endTime: $("#endTime").datebox('getValue')});
                        },
                        menu: "#mm",
                        width: 300
                    });
                    usp.order.stockinlist.iniCount++;
                }
            }
        });
    }

    usp.order.stockinlist.add = function (url) {
        location.href = url;
    }

    usp.order.stockinlist.edit = function (datagrid, url) {
        var model = $(datagrid).datagrid("getSelected");
        if (model) {
            location.href = url + model.ID;
        } else {
            $.messager.alert('提示信息', '请选择要编辑数据！');
        }
    }

    usp.order.stockinlist.audit = function (datagrid, url) {
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

    usp.order.stockinlist.cancel = function (datagrid, url) {
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

    usp.order.stockinlist.active = function (datagrid, url) {
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

    usp.order.stockinlist.init = function (id, url, toolbarmenu, toolbarsearch) {
        usp.order.stockinlist.initGrid(id, url, toolbarmenu, toolbarsearch);
    }
})(this);
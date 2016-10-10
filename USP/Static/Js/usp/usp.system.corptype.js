(function () {
    usp.namespace("usp.system.corptype");

    usp.system.corptype.initCorptypeGrid = function (selector1, selector2, url, name) {
        $(selector1).datagrid({
            url: url,
            title: '公司类型信息',
            queryParams: { actionName: 'datagrid', name: name },
            iconCls: 'icon-clientService',
            fit: true,
            toolbar: selector2,
            nowrap: false,
            striped: true,
            columns: [
                [{
                    field: 'Name',
                    title: '公司类型名称',
                    width: 100
                }, {
                    field: 'Remark',
                    title: '备注',
                    width: 150
                },
                    {
                        field: 'Canceler',
                        title: '状态',
                        width: 100,
                        formatter: function (val) {
                            if (val == null) {
                                return "正常";
                            }
                            else {
                                return "注销";
                            }
                        }
                    },
                {
                    field: 'CreateTime',
                    title: '创建时间',
                    width: 160,
                    formatter: function (val) {
                        return usp.ParseUTCDate(val);
                    }
                }
                ]],
            pagination: true,
            rownumbers: true,
            singleSelect: true,

        });
    }

    usp.system.corptype.search = function (selector1, toolbar) {
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
        $(selector1).datagrid('load', params);
    };

    usp.system.corptype.edit = function (select) {
        var row = $(select).datagrid('getSelected');
        if (row == null) {
            toastr.warning('请选择要修改的公司类型！');
        } else {
            location.href = '/System/SysCorpType/Edit?id=' + row.ID;
        }
    };

    usp.system.corptype.cancel = function (select, url) {
        var row = $(select).datagrid('getSelected');
        if (row == null) {
            toastr.warning('请选择要注销的公司类型！');
        } else {
            if (row.Canceler != null) {
                toastr.warning('公司类型处于注销状态，无需重复操作！');
                return;
            }
            $.messager.confirm('提醒', '确定要注销该公司类型吗？', function (r) {
                //if (r) {
                //    $.post(url, { id: row.ID }, function (res) {
                //        if (res.flag) {
                //            toastr.success(res.message);
                //            $(select).datagrid('reload');
                //        }
                //        else {
                //            toastr.success(res.message);
                //        }
                //    }, 'json')
                //}
                if (r) {
                    $.ajax({
                        url:url,
                        type: 'post',
                        dataType: 'json',
                        data: { id: row.ID },
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
    };

    usp.system.corptype.active = function (select, url) {
        var row = $(select).datagrid('getSelected');
        if (row == null) {
            toastr.warning('请选择要激活的公司类型！');
        }
        else {
            if (row.Canceler == null) {
                toastr.warning('公司类型处于激活状态，无需重复操作！');
                return;
            }
            $.messager.confirm('提醒', '确定要激活该公司类型吗？', function (r) {
                //if (r) {
                //    $.post(url, { id: row.ID }, function (res) {
                //        if (res.flag) {
                //            toastr.success(res.message);
                //            $(select).datagrid('reload');
                //        }
                //        else {
                //            toastr.success(res.message);
                //        }
                //    }, 'json')
                //}

                if (r) {
                    $.ajax({
                        url: url,
                        type: 'post',
                        dataType: 'json',
                        data: { id: row.ID },
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
    };

    usp.system.corptype.checkName = function (id, name, url, errorname) {
        $(name).blur(function () {
            if (name) {
                $.post(url, { actionName: 'checkname', id: $(id).val(), name: $(name).val() }, function (res) {
                    $('#errorname').text(res.message).show();
                }, 'json');
            }
        })
    }
})(this);
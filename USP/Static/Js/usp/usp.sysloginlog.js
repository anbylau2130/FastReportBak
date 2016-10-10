(function () {
    usp.namespace("usp.sysloginlog");
    usp.sysloginlog.initLogGrid = function (id, url, startTime, endTime) {
        $(id).datagrid({
            url: url,
            queryParams: { actionName: 'datagrid', startTime: startTime, endTime: endTime },
            title: '登录日志信息',
            iconCls: 'icon-clientService',
            fit: true,
            toolbar: '#tb',
            nowrap: false,
            striped: true,
            columns: [
                [{
                    field: 'Ip',
                    title: '登录ip',
                    width: 150,
                    formatter: function (val, rec) {
                        if (rec.Success == 1) {
                            return '<span style="color:red;">' + val + '</span>';
                        } else {
                            return val;
                        }
                    }
                }, {
                    field: 'LoginAgent',
                    title: '用户代理',
                    width: 300,
                    formatter: function (val, rec) {
                        if (rec.Success == 1) {
                            return '<span style="color:red;">' + val + '</span>';
                        } else {
                            return val;
                        }
                    }
                }, {
                    field: 'Success',
                    title: '登录结果',
                    width: 150,
                    formatter: function (value, row, index) {
                        if (value == 1)
                            return '<span style="color:green;">' + '成功' + '</span>';
                        else
                            return '<span style="color:red;">' + '失败' + '</span>';
                    }
                }, {
                    field: 'Time',
                    title: '登录时间',
                    width: 150,
                    align: 'right',
                    formatter: function (val, rec) {
                        if (rec.Code == rec.Parent) {
                            return '<span style="color:red;">' + usp.ParseUTCDate(val) + '</span>';
                        } else {
                            return usp.ParseUTCDate(val);
                        }
                    }
                }]
            ],
            pagination: true,
            rownumbers: true,
            singleSelect: true
        });
    }

    usp.sysloginlog.search = function (id, toolbar) {
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
    }
})(this);



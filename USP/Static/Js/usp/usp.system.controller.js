(function() {
    usp.namespace("usp.sysytem.controller");
    usp.sysytem.controller.initGrid = function(id, url) {
        $(id).datagrid({
            url: url,
            title: '控制器信息',
            iconCls: 'icon-clientService',
            fit : true,
            nowrap: false,
            striped: true,
            columns: [
                [{
                    field: 'Area',
                    title: 'Area',
                    width: 300
                }, {
                    field: 'Class',
                    title: 'Class',
                    width: 300
                }, {
                    field: 'Controller',
                    title: 'Controller',
                    width: 300
                }, {
                    field: 'Action',
                    title: 'Action',
                    width: 200
                }, {
                    field: 'Params',
                    title: 'Params',
                    width: 300
                }]
            ],
            pagination: true,
            rownumbers: true,
            singleSelect: true
        });
    }
})(this);
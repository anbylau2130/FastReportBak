(function() {
    usp.namespace("usp.sysytem.privilege");
    usp.sysytem.privilege.initGrid = function(id, url) {
        $(id).datagrid({
            url: url,
            title: '权限信息',
            iconCls: 'icon-clientService',
            fit : true,
            nowrap: false,
            striped: true,
            columns: [
                [{
                    field: 'Parent',
                    title: 'Parent',
                    width: 300
                }, {
                    field: 'Name',
                    title: 'Name',
                    width: 300
                }, {
                    field: 'ControllerClass',
                    title: 'ControllerClass',
                    width: 300
                }, {
                    field: 'ControllerArea',
                    title: 'ControllerArea',
                    width: 300
                }, {
                    field: 'ControllerName',
                    title: 'ControllerName',
                    width: 300
                }, {
                    field: 'ControllerAction',
                    title: 'ControllerAction',
                    width: 200
                }, {
                    field: 'ActionParams',
                    title: 'ActionParams',
                    width: 300
                }, {
                    field: 'Url',
                    title: 'Url',
                    width: 300
                }]
            ],
            pagination: true,
            rownumbers: true,
            singleSelect: true
        });
    }
})(this);
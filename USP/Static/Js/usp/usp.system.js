(function() {
    usp.namespace("usp.sysytem");
    usp.sysytem.initPrivilegesGrid = function(id, url) {
        $(id).datagrid({
            url: url,
            title: '权限信息',
            iconCls: 'icon-clientService',
            height: 380,
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

    usp.sysytem.initMenusGrid = function(id, url) {
        $(id).datagrid({
            url: url,
            title: '菜单数据',
            iconCls: 'icon-clientService',
            height: 380,
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

    usp.sysytem.initControllersGrid = function(id, url) {
        $(id).datagrid({
            url: url,
            title: '控制器数据',
            iconCls: 'icon-clientService',
            height: 380,
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
(function() {
    usp.namespace("usp.easyui");
    usp.easyui.initTree = function(id, url) {
        $(id).tree({
            url: url
        });
    }
    usp.easyui.initGrid = function(id, url) {
        $(id).datagrid({
            url: url,
            title: 'datagrid',
            iconCls: 'icon-clientService',
            height:380,
            nowrap: false,
            striped: true,
            frozenColumns: [
                [{
                    field: 'Code',
                    title: 'Code',
                    width: 100,
                    formatter: function(val, rec) {
                        if (rec.Code == rec.Parent) {
                            return '<span style="color:red;">' + val + '</span>';
                        } else {
                            return val;
                        }
                    }
                }, {
                    field: 'Parent',
                    title: 'Parent',
                    width: 100,
                    formatter: function(val, rec) {
                        if (rec.Code == rec.Parent) {
                            return '<span style="color:red;">' + val + '</span>';
                        } else {
                            return val;
                        }
                    }
                }, {
                    field: 'Name',
                    title: 'Name',
                    width: 150,
                    formatter: function(val, rec) {
                        if (rec.Code == rec.Parent) {
                            return '<span style="color:red;">' + val + '</span>';
                        } else {
                            return val;
                        }
                    }
                }]
            ],
            columns: [
                [{
                    field: 'LocationX',
                    title: 'LocationX',
                    width: 200,
                    formatter: function(val, rec) {
                        if (rec.Code == rec.Parent) {
                            return '<span style="color:red;">' + val + '</span>';
                        } else {
                            return val;
                        }
                    }
                }, {
                    field: 'LocationY',
                    title: 'LocationY',
                    width: 200,
                    formatter: function(val, rec) {
                        if (rec.Code == rec.Parent) {
                            return '<span style="color:red;">' + val + '</span>';
                        } else {
                            return val;
                        }
                    }
                }, {
                    field: 'Remark',
                    title: 'Remark',
                    width: 200,
                    formatter: function(val, rec) {
                        if (rec.Code == rec.Parent) {
                            return '<span style="color:red;">' + val + '</span>';
                        } else {
                            return val;
                        }
                    }
                }, {
                    field: 'CreateTime',
                    title: 'CreateTime',
                    width: 160,
                    align: 'right',
                    formatter: function(val, rec) {
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
            singleSelect: true,
            toolbar: [{
                text: '刷  新',
                iconCls: 'icon-reload',
                handler: function() {
                    $(id).datagrid('reload');
                }
            }, '-', {
                text: '查询代理商',
                iconCls: 'icon-search',
                handler: function() {
                    alert("查询代理商");
                }
            }, '-', {
                text: '充 值',
                iconCls: 'icon-dollar',
                handler: function() {
                    alert("充 值");
                }
            }]
        });
    }
    usp.easyui.initTreeGrid = function(id, url) {
    $(id).treegrid({
            url: url,
            idField: 'Code',
            treeField:'Parent',
            height:380,
            nowrap: false,
            columns: [
                [ {
                    field: 'Parent',
                    title: 'Parent',
                    width: 100,
                    formatter: function(val, rec) {
                        if (rec.Code == rec.Parent) {
                            return '<span style="color:red;">' + val + '</span>';
                        } else {
                            return val;
                        }
                    }
                },{
                    field: 'Code',
                    title: 'Code',
                    width: 100,
                    formatter: function(val, rec) {
                        if (rec.Code == rec.Parent) {
                            return '<span style="color:red;">' + val + '</span>';
                        } else {
                            return val;
                        }
                    }
                }, {
                    field: 'Name',
                    title: 'Name',
                    width: 150,
                    formatter: function(val, rec) {
                        if (rec.Code == rec.Parent) {
                            return '<span style="color:red;">' + val + '</span>';
                        } else {
                            return val;
                        }
                    }
                },{
                    field: 'LocationX',
                    title: 'LocationX',
                    width: 200,
                    formatter: function(val, rec) {
                        if (rec.Code == rec.Parent) {
                            return '<span style="color:red;">' + val + '</span>';
                        } else {
                            return val;
                        }
                    }
                }, {
                    field: 'LocationY',
                    title: 'LocationY',
                    width: 200,
                    formatter: function(val, rec) {
                        if (rec.Code == rec.Parent) {
                            return '<span style="color:red;">' + val + '</span>';
                        } else {
                            return val;
                        }
                    }
                }, {
                    field: 'Remark',
                    title: 'Remark',
                    width: 200,
                    formatter: function(val, rec) {
                        if (rec.Code == rec.Parent) {
                            return '<span style="color:red;">' + val + '</span>';
                        } else {
                            return val;
                        }
                    }
                }, {
                    field: 'CreateTime',
                    title: 'CreateTime',
                    width: 160,
                    align: 'right',
                    formatter: function(val, rec) {
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
})(this);


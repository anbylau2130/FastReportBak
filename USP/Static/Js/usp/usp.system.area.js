(function () {
    usp.namespace("usp.sysytem.area");
    usp.sysytem.area.initAreasGrid = function (id, url) {
        $(id).datagrid({
            url: url,
            title: '地区信息',
            iconCls: 'icon-clientService',
            fit: true,
            toolbar: '#tb',
            nowrap: false,
            striped: true,
            columns: [
                [{
                    field: 'Code',
                    title: 'Code',
                    width: 150
                }, {
                    field: 'Name',
                    title: '名称',
                    width: 350
                }]
            ],
            pagination: true,
            rownumbers: true,
            singleSelect: true,

        });
    }

    //usp.sysytem.area.getAreaLinkage = function (selector1, selector2, selector3, selector4, provinceId, areaId, countyId, communityId, urlProvince, urlArea, urlCounty, urlCommunity, urlChildrens) {

    //    var province = $(selector1).combobox({
    //        valueField: 'id',
    //        textField: 'text',
    //        editable: false,
    //        url: urlProvince,
    //        onLoadSuccess: function () {
    //            if (provinceId) {
    //                $(selector1).combobox('select', provinceId);
    //            }
    //        },
    //        onChange: function (newValue, oldValue) {
    //            if (oldValue) {
    //                $.get(urlChildrens, { parentId: newValue }, function (data) {
    //                    areaId = null;
    //                    countyId = null;
    //                    communityId = null;
    //                    $(selector2).combobox("clear").combobox('loadData', data);
    //                    $(selector3).combobox("clear").combobox('loadData', [{ text: '==请选择==' }]).combobox('select', null);
    //                    $(selector4).combobox("clear").combobox('loadData', [{ text: '==请选择==' }]).combobox('select', null);
    //                    if (data) {
    //                        $(selector2).combobox('select', data[0].id);
    //                    }
    //                }, 'json');
    //            }
    //        }
    //    });

    //    var area = $(selector2).combobox({
    //        valueField: 'id',
    //        textField: 'text',
    //        editable: false,
    //        url: urlArea,
    //        onLoadSuccess: function () {
    //            if (areaId) {
    //                $(selector2).combobox('select', areaId);
    //            }
    //        },
    //        onChange: function (newValue, oldValue) {
    //            //console.log('城市，最新值：' + newValue + '；过去值：' + oldValue);
    //            if (!areaId) {
    //                if(newValue){
    //                    $.get(urlChildrens, { parentId: newValue }, function (data) {
    //                        countyId = null;
    //                        communityId = null;
    //                        $(selector3).combobox("clear").combobox('loadData', data);
    //                        $(selector4).combobox("clear").combobox('loadData', [{ text: '==请选择==' }]).combobox('select', null);
    //                        if (data) {
    //                            $(selector3).combobox('select', data[0].id);
    //                        }
    //                    }, 'json');
    //                }
    //            }
    //        }
    //    });

    //    var county = $(selector3).combobox({
    //        valueField: 'id',
    //        textField: 'text',
    //        editable: false,
    //        url: urlCounty,
    //        onLoadSuccess: function () {
    //            if (countyId) {
    //                $(selector3).combobox('select', countyId);
    //            }
    //        },
    //        onChange: function (newValue, oldValue) {
    //           // console.log('区，最新值：' + newValue + '；过去值：' + oldValue);
    //            if (!countyId) {
    //                if (newValue) {
    //                    $.get(urlChildrens, { parentId: newValue }, function (data) {
    //                        communityId = null;
    //                        $(selector4).combobox("clear").combobox('loadData', data);
    //                        if (data) {
    //                            $(selector4).combobox('select', data[0].id);
    //                        }
    //                    }, 'json');
    //                }
    //            }
    //        }
    //    });

    //    var community = $(selector4).combobox({
    //        valueField: 'id',
    //        textField: 'text',
    //        editable: false,
    //        url: urlCommunity,
    //        onLoadSuccess: function () {
    //            if (communityId) {
    //                $(selector4).combobox('select', communityId);
    //            }
    //        },
    //    });
    //}

    usp.sysytem.area.getAreaLinkage = function (selector1, selector2, selector3, selector4, provinceId, areaId, countyId, communityId, urlProvince, urlArea, urlCounty, urlCommunity, urlChildrens) {

        var province = $(selector1).combobox({
            valueField: 'id',
            textField: 'text',
            editable: false,
            url: urlProvince + '?selectedId=' + provinceId,
            onSelect: function (obj) {
                if (obj.id) {
                    $.get(urlChildrens, { parentId: obj.id }, function (data) {
                        area.combobox("clear").combobox('loadData', data);
                        county.combobox("clear").combobox('loadData', [{ id: '', text: '==请选择==', selected: true }]);
                        community.combobox("clear").combobox('loadData', [{ id: '', text: '==请选择==', selected: true }]);
                    }, 'json');
                }
            }
        });

        var area = $(selector2).combobox({
            valueField: 'id',
            textField: 'text',
            editable: false,
            url: urlChildrens + '?parentId=' + provinceId + '&selectedId=' + areaId,
            onSelect: function (obj) {
                //console.log('城市，最新值：' + newValue + '；过去值：' + oldValue);
                if (obj.id) {
                    $.get(urlChildrens, { parentId: obj.id }, function (data) {
                        county.combobox("clear").combobox('loadData', data);
                        community.combobox("clear").combobox('loadData', [{ id: '', text: '==请选择==', selected: true }]);
                    }, 'json');
                }
            }
        });

        if (!areaId) {
            area.combobox('loadData', [{ id: '', text: '==请选择==', selected: true }]);
        }

        var county = $(selector3).combobox({
            valueField: 'id',
            textField: 'text',
            editable: false,
            url: urlChildrens + '?parentId=' + areaId + '&selectedId=' + countyId,
            onSelect: function (obj) {
                // console.log('区，最新值：' + newValue + '；过去值：' + oldValue);
                if (obj.id) {
                    $.get(urlChildrens, { parentId: obj.id }, function (data) {
                        community.combobox("clear").combobox('loadData', data);
                    }, 'json');
                }
            }
        });

        if (!countyId) {
            county.combobox('loadData', [{ id: '', text: '==请选择==', selected: true }]);
        }

        var community = $(selector4).combobox({
            valueField: 'id',
            textField: 'text',
            editable: false,
            url: urlChildrens + '?parentId=' + countyId + '&selectedId=' + communityId,
        });

        if (!communityId) {
            community.combobox('loadData', [{ id: '', text: '==请选择==', selected: true }]);
        }
    }

})(this);
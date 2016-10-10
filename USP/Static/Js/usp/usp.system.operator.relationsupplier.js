(function() {
    usp.namespace("usp.system.operator.relationsupplier");
    /////添加编辑页////
    usp.system.operator.relationsupplier.initSupplierTree = function (treeId, url) {
        $(treeId).tree({
            url: url,
            method: 'post',
            queryParams: { actionName: 'suppliertree' },
            checkbox: true,
            lines: true,
            onCheck: function (node) {
                usp.system.operator.relationsupplier.setVaule();
            },
            onLoadSuccess: function () {
                usp.system.operator.relationsupplier.setVaule();
            }
        });
    };
    usp.system.operator.relationsupplier.setVaule = function () {
        var checknodes = $('#operatorSupplier').tree('getChecked', ['checked', 'indeterminate']); //选择和半选择的节点
        var roles = '';
        for (i = 0; i < checknodes.length; i++) {
            var type = checknodes[i].attributes.type;
            var id = checknodes[i].id;
            if (type == 1) {
                if (roles != '') roles += ',';
                roles += id;
            }
        };
        $('#hdSupplier').val(roles);
    }
})(this)



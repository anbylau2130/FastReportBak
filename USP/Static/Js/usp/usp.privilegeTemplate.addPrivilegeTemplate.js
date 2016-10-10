(function () {
    usp.namespace("usp.privilegeTemplate.addPrivilegeTemplate");

    usp.privilegeTemplate.addPrivilegeTemplate = {
        init: function (privilegesHidden, privilegesTree, privilegesTreeUrl, select) {
            usp.privilegeTemplate.addPrivilegeTemplate.eventInit.treeInit(privilegesHidden, privilegesTree);
            usp.privilegeTemplate.addPrivilegeTemplate.eventInit.selectInit(select, privilegesTreeUrl, privilegesTree, privilegesHidden);
        },
        eventInit: {

            treeInit: function (privileges, privilegesTree) {
                $(privilegesTree).tree({
                    onCheck: function (node) {
                        var nodes = $(privilegesTree).tree('getChecked');
                        var result = '';
                        if (nodes) {
                            for (var i = 0; i < nodes.length; i++) {
                                result += nodes[i].id;
                                if (i === nodes.length - 1) {
                                    result += "";
                                } else {
                                    result += ",";
                                }
                            }
                        }
                        $(privileges).val(result);
                    }
                });
            },
            selectInit: function (id, url, tree, hidden) {

                $.ajax({
                    type: "POST",
                    url: url,
                    data: { actionName: 'privilegetree', corpType: $(id).val() },
                    success: function (result) {
                        if (result.flag) {
                            $(hidden).val(result.attachment.privilegeData);
                            $(tree).tree("loadData", result.attachment.treeData);
                        }
                    }
                });
            }
        }
    }

})(this);
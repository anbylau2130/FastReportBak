(function () {
    usp.namespace("usp.privilegeTemplate.auditorPrivilegeTemplate");

    usp.privilegeTemplate.auditorPrivilegeTemplate = {

        init: function (btnReturnId, privilegesHidden, privilegesTree, privilegesTreeUrl, select) {

            usp.privilegeTemplate.auditorPrivilegeTemplate.eventInit.treeInit(privilegesHidden, privilegesTree);
            usp.privilegeTemplate.auditorPrivilegeTemplate.eventInit.selectInit(select, privilegesTreeUrl, privilegesTree, privilegesHidden);
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
                            $(tree).tree({
                                onLoadSuccess: function () {
                                    $(this).find('span.tree-checkbox').unbind().click(function () {
                                        return false;
                                    });
                                }
                            });
                        }
                    }
                });
            }
        }
    }

})(this);


//(function () {
//    usp.namespace("usp.privilegeTemplate.auditorPrivilegeTemplate");

//    usp.privilegeTemplate.auditorPrivilegeTemplate = {

//        init: function (btnReturnId, privilegesHidden, privilegesTree, privilegesTreeUrl, select, basePage) {
//            usp.privilegeTemplate.auditorPrivilegeTemplate.eventInit.btnInit(btnReturnId, basePage);
//            usp.privilegeTemplate.auditorPrivilegeTemplate.eventInit.treeInit(privilegesHidden, privilegesTree);
//            usp.privilegeTemplate.auditorPrivilegeTemplate.eventInit.selectInit(select, privilegesTreeUrl, privilegesTree, privilegesHidden);
//        },
//        eventInit: {
//            btnInit: function (id, basePage) {
//                $(id).on("click", function () {
//                    location.href = basePage;
//                    return false;
//                });
//            },
//            treeInit: function (privileges, privilegesTree) {
//                $(privilegesTree).tree({
//                    onCheck: function (node) {
//                        var nodes = $(privilegesTree).tree('getChecked');
//                        var result = '';
//                        if (nodes) {
//                            for (var i = 0; i < nodes.length; i++) {
//                                result += nodes[i].id;
//                                if (i === nodes.length - 1) {
//                                    result += "";
//                                } else {
//                                    result += ",";
//                                }
//                            }
//                        }
//                        $(privileges).val(result);
//                    }

//                });
//            },
//            selectInit: function (id, url, tree, hidden) {
//                $(id).on("change", function (a, b, c, d, e) {
//                    console.log(a, b, c, d, e);
//                    $.ajax({
//                        type: "POST",
//                        url: url,
//                        data: { corpType: $(id).val() },
//                        success: function (result) {
//                            if (result.flag) {
//                                $(hidden).val(result.attachment.privilegeData);
//                                $(tree).tree("loadData", result.attachment.treeData);
//                            }
//                        }
//                    });
//                });
//            }
//        }
//    }

//})(this);
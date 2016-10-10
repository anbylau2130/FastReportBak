(function () {
    usp.namespace("usp.privilegeTemplate.privilegeTemplates");

    usp.privilegeTemplate.privilegeTemplates.initGrid = function (id, url, toolbar, corpType) {
        $(id).datagrid({
            url: url,
            queryParams: { actionName: 'datagrid', corpType: corpType },
            title: '权限模板信息',
            iconCls: 'icon-file',
            fit: true,
            nowrap: true,
            striped: true,
            fitColumns: true,
            columns: [
                [
                    { field: 'Name', title: '权限名称' },
                    { field: 'Creator', title: '创建人' },
                    { field: 'CreateTime', title: '创建时间' },
                    { field: 'Auditor', title: '审核人' },
                    { field: 'AuditTime', title: '审核时间' }
                ]
            ],
            toolbar: toolbar,
            pagination: true,
            rownumbers: true,
            singleSelect: true,
        });
    }

    usp.privilegeTemplate.privilegeTemplates.corpTypeInit = function (combCorpType, url, selector2) {
        $(combCorpType).combobox({
            width: 150,
            url: url,
            queryParams: { actionName: 'corptype' },
            method: 'POST',
            valueField: 'id',
            textField: 'text',
            onChange: function (n, o) {
                $(selector2).datagrid('load', { actionName: 'datagrid', corptype: $(combCorpType).combobox('getValue') });
            }
        });
    }

    usp.privilegeTemplate.privilegeTemplates.configClick = function (comboxId, configUrl) {
        if ($(comboxId).combobox("getValue") == null || $(comboxId).combobox("getValue") == '') {
            toastr.warning("请选中公司类型后再进行操作!");
        } else { location.href = configUrl + $(comboxId).combobox("getValue"); }

    }

    usp.privilegeTemplate.privilegeTemplates.auditorClick = function (comboxId, auditorUrl) {

        if ($(comboxId).combobox("getValue") == null || $(comboxId).combobox("getValue") == '') {
            toastr.warning("请选中公司类型后再进行操作!");
        } else { location.href = configUrl + $(comboxId).combobox("getValue"); }

    }

})(this);


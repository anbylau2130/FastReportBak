(function () {
    usp.namespace("usp.sysytem.menutemplate");
    //加载列表数据
    usp.sysytem.menutemplate.initMenutemplateDataGrid = function (selector, url, corptype) {
        $(selector).datagrid({
            url: url,
            queryParams: { actionName: 'datagrid', corptype: corptype },
            title: '菜单模板信息',
            iconCls: 'icon-clientService',
            fit: true,
            toolbar: '#tb',
            nowrap: false,
            striped: true,
            columns: [[
                   {
                       field: 'MenuName',
                       title: '菜单名称',
                       width: 100
                   }, {
                       field: 'Creator',
                       title: '创建人',
                       width: 150
                   }, {
                       field: 'CreateTime',
                       title: '创建时间',
                       width: 160,
                       formatter: function (val, rec) {
                           if (rec.Code == rec.Parent) {
                               return '<span style="color:red;">' + usp.ParseUTCDate(val) + '</span>';
                           } else {
                               return usp.ParseUTCDate(val);
                           }
                       }
                   }, {
                       field: 'Auditor',
                       title: '审核人',
                       width: 100
                   }, {
                       field: 'AuditTime',
                       title: '审核时间',
                       width: 160,
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
            singleSelect: true,
            //onLoadSuccess: function (data) {
            //    if (data.total == 0) {
            //        $('#js-edit').find('.l-btn-text').text('添加');
            //    }
            //    else {
            //        $('#js-edit').find('.l-btn-text').text('编辑');
            //    }
            //}
        });
    }

    //编辑菜单模板
    usp.sysytem.menutemplate.edit = function (corpType) {
        location.href = '/System/SysMenuTemplate/Add?corpType=' + corpType;
    }

    //审核通过模板
    usp.sysytem.menutemplate.audit = function (id) {
        location.href = '/System/SysMenuTemplate/Audit?corpType=' + id;
    }

    //加载列表初始化combobox
    //usp.sysytem.menutemplate.menuCombobox = function (selector1, selector2, url, urlIndex) {
    //    $(selector1).combobox(
    //      {
    //          method: 'post',
    //          url: url,
    //          queryParams: { actionName: 'corptype' },
    //          valueField: 'id',
    //          textField: 'text',
    //          onChange: function (n, o) {
    //              usp.sysytem.menutemplate.initMenutemplateDataGrid(selector2, urlIndex, $(selector1).combobox('getValue'));
    //          }
    //      })
    //}


    usp.sysytem.menutemplate.menuCombobox = function (selector1, url, selector2) {
        $(selector1).combobox(
          {
              method: 'post',
              url: url,
              queryParams: { actionName: 'corptype' },
              valueField: 'id',
              textField: 'text',
              onChange: function (n, o) {
                  $(selector2).datagrid('load', { actionName: 'datagrid', corptype: $(selector1).combobox('getValue') });
              }
          })
    }


    //设置初始化
    usp.sysytem.menutemplate.setMenuTemplate = function (selector1, selector2, url, corpType) {
        $(selector1).tree({
            url: url,
            method: 'post',
            queryParams: { actionName: 'menutree', corpType: corpType },
            checkbox: true,
            lines: true,
            onCheck: function (node) {
                usp.sysytem.menutemplate.setValue(selector1, selector2);
            },
            onLoadSuccess: function () {
                usp.sysytem.menutemplate.setValue(selector1, selector2);
            }
        });
    }

    //审核初始化
    usp.sysytem.menutemplate.auditMenu = function (selector1, selector2, url, corpType) {
        $(selector1).tree({
            url: url,
            method: 'post',
            queryParams: { actionName: 'menutree', corpType: corpType },
            checkbox: true,
            lines: true,
            onCheck: function (node) {
                usp.sysytem.menutemplate.setValue(selector1, selector2);
            },
            onLoadSuccess: function () {
                $(this).find('span.tree-checkbox').unbind().click(function () {
                    return false;
                });
                usp.sysytem.menutemplate.setValue(selector1, selector2);
            }

        });
    }

    usp.sysytem.menutemplate.setValue = function (selector1, selector2) {
        var checknodes = $(selector1).tree('getChecked', ['checked', 'indeterminate']); //选择和半选择的节点
        var menus = '';
        for (i = 0; i < checknodes.length; i++) {
            var type = checknodes[i].attributes.type;
            var id = checknodes[i].id;
            if (menus != '') {
                menus += ',';
            }
            menus += id;
        };
        $(selector2).val(menus);
    }
})(this);

(function () {
    usp.namespace("usp.corporatin.corporations");
    usp.corporatin.corporations.initGrid = function (id, url, addUrl, editUrl, cancelUrl, enableUrl, auditorUrl) {
        $(id).datagrid({
            url: url,
            queryParams: { actionName: 'datagrid', name: '', status: -1 },
            title: '公司信息',
            iconCls: 'icon-file',
            fit: true,
            nowrap: true,
            striped: true,
            pagination: true,
            rownumbers: true,
            singleSelect: true,
            fitColumns: true,
            frozenColumns: [[
                { field: 'Name', title: '公司名称' },
                //{ field: 'BriefName', title: '公司简称' }
                    { field: 'Status', title: '状态' },
            ]
            ],
            columns: [[
                    { field: 'Type', title: '公司类型' },
                    { field: 'Grade', title: '级别级别' },
                    { field: 'Vocation', title: '行业类型' },
                    { field: 'ID', title: '唯一标识', hidden: true },
                    { field: 'Parent', title: '上级公司', hidden: true },
                    { field: 'Priority', title: '公司优先级', hidden: true },
                    { field: 'VirtualIntegral', title: '虚拟积分' },
                    { field: 'RealIntegral', title: '真实积分' },
                    { field: 'FeeType', title: '付费类型' },
                    { field: 'PrepayValve', title: '预付阀值' },
                    { field: 'Balance', title: '余额' },
                    { field: 'FrozenBalance', title: '冻结余额' },
                    { field: 'IncomingBalance', title: '在途余额' },
                    { field: 'Commission', title: '佣金比例' },
                    { field: 'Discount', title: '折扣比例' },
                    { field: 'Province', title: '省份' },
                    { field: 'Area', title: '地区' },
                    { field: 'County', title: '县' },
                    { field: 'Community', title: '社区' },
                    //{ field: 'Address', title: '地址' },

                    { field: 'Creator', title: '创建人' },
                    //{ field: 'CreateTime', title: '创建时间', hidden: true },
                    { field: 'Auditor', title: '审核人' },
                    //{ field: 'AuditTime', title: '审核时间', hidden: true },
                    { field: 'Canceler', title: '注销人' },
                    //{ field: 'CancelTime', title: '注销时间', hidden: true },
                   // { field: 'Certificate', title: '证书名称' },
                    //{ field: 'CertificateNumber', title: '证书号码' },
                    //{ field: 'Ceo', title: '法人代表' },
                    //{ field: 'Postcode', title: '邮编' },
                   // { field: 'Faxcode', title: '传真号' },
                    { field: 'Linkman', title: '联系人' },
                    { field: 'Mobile', title: '联系手机' },
                    { field: 'Email', title: 'Email' },
                    { field: 'Qq', title: '联系QQ' }
                    //{ field: 'Wechat', title: '微信' },
                   // { field: 'Weibo', title: '微博' }
            ]
            ],
            toolbar: [
          {
              text: "添加",
              iconCls: 'icon-add',
              handler: function () {
                  window.location.href = addUrl;
              }
          }, '-',
               {
                   text: "编辑",
                   iconCls: 'icon-edit',
                   handler: function () {
                       var row = $(id).datagrid("getSelected");
                       if (row) {
                           if (row.ID === 0) {
                               toastr.success("不能操作根节点！");
                               return;
                           }
                           window.location.href = editUrl + "?id=" + row.ID;
                       } else {
                           toastr.warning("请先选中要编辑的行，然后操作!");
                       }
                   }
               }, '-',
              {
                  text: "审核",
                  iconCls: 'icon-ok',
                  handler: function () {
                      var row = $(id).datagrid("getSelected");
                      if (row == null) {
                          toastr.warning('请选择要审核的行！');
                      }
                      else {
                          if (row.ID === 0) {
                              toastr.success("不能操作根节点！");
                              return;
                          }
                          else {
                              location.href = auditorUrl + "?id=" + row.ID;
                              //$.messager.confirm('提醒', '确定要审核该公司吗？', function (r) {
                              //    if (r) {
                              //        $.ajax({
                              //            type: "POST",
                              //            url: auditorUrl + "?id=" + row.ID,
                              //            success: function (result) {
                              //                if (result.flag) {
                              //                    toastr.success(result.message);
                              //                    $(id).datagrid('reload');
                              //                } else {
                              //                    toastr.error(result.message);
                              //                }
                              //            },
                              //            error: function () {
                              //                toastr.error('您没有操作权限！');
                              //            }
                              //        });
                              //    } else {
                              //        toastr.warning("请先选中要审核的行，然后操作!");
                              //    }
                              //});
                          }
                      }
                  }
              }, '-', {
                  text: "注销",
                  iconCls: 'icon-remove',
                  handler: function () {
                      var row = $(id).datagrid("getSelected");
                      if (row == null) {
                          toastr.warning("请先选中要注销的行，然后操作!");
                      } else {
                          if (row.ID === 0) {
                              toastr.success("不能操作根节点！");
                              return;
                          }
                          $.messager.confirm('提醒', '确定要注销该公司吗？', function (r) {
                              if (r) {
                                  $.ajax({
                                      type: "POST",
                                      url: cancelUrl + "?id=" + row.ID,
                                      success: function (result) {
                                          if (result.flag) {
                                              toastr.success(result.message);
                                              $(id).datagrid('reload');
                                          } else {
                                              toastr.error(result.message);
                                          }
                                      },
                                      error: function () {
                                          toastr.error('您没有操作权限！');
                                      }
                                  });
                              }
                          })
                      }
                  }
              }, '-', {
                  text: "激活",

                  iconCls: 'icon-redo',
                  handler: function () {
                      var row = $(id).datagrid("getSelected");
                      if (row == null) {
                          toastr.warning("请先选中要激活的行，然后操作!");
                      } else {
                          if (row.ID === 0) {
                              toastr.success("不能操作根节点！");
                              return;
                          }
                          $.messager.confirm('提醒', '确定要激活该公司吗？', function (r) {
                              if (r) {
                                  $.ajax({
                                      type: "POST",
                                      url: enableUrl + "?id=" + row.ID,
                                      success: function (result) {
                                          if (result.flag) {
                                              toastr.success(result.message);
                                              $(id).datagrid('reload');
                                          } else {
                                              toastr.error(result.message);
                                          }
                                      },
                                      error: function () {
                                          toastr.error('您没有操作权限！');
                                      }
                                  });
                              }
                          })
                      }

                  }
              }
              //'-',
              //{
              //    text: "状态: <select width='500px'  data-options='editable:false' id='Status' name='Status'></select>"
              //},
              //'-',
              //{
              //    text: "公司名称:<input class='easyui-textbox' id='btnSearch' />"
              //},
              //{
              //    text: "搜索",
              //    iconCls: "icon-search",
              //    handler: function () {
              //        var options = $(id).datagrid("options");
              //        options.url = url + "?name=" + $("#btnSearch").val();
              //        $(id).datagrid(options);
              //    }
              //}

            ]
            //, '-', {
            //    text: "公司名称:<input class='easyui-textbox' id='btnSearch' />"
            //},
            //{
            //    text: "搜索",
            //    iconCls: "icon-search",
            //    handler: function () {
            //        var options = $(id).datagrid("options");
            //        options.url = url + "?name=" + $("#btnSearch").val();
            //        $(id).datagrid(options);
            //    }
            //}


        });
        $('#corporationsearch').appendTo('.datagrid-toolbar');
    }

    usp.corporatin.corporations.searchCorp = function () {
        //var options = $(id).datagrid("options");
        //options.url = url + "?name=" + $("#btnSearch").val();
        //$(id).datagrid(options);
        var name = $('#corpName').textbox('getValue');
        var status = $('#status').combobox('getValue');
        $('#corporations').datagrid('load', { actionName: 'datagrid', name: name, status: status });
    }


    usp.corporatin.corporations.initCombobox = function (comboId, comboUrl) {
        $(comboId).combobox({
            url: comboUrl,
            queryParams: { actionName: 'Combobox' },
            valueField: 'id',
            textField: 'text',
            onSelect: function () {
                usp.corporatin.corporations.searchCorp();
            }
        });
    }

})(this);



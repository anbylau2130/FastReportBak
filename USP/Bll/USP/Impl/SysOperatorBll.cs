using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Models.Entity;
using USP.Models.POCO;
using USP.Service;
using USP.Utility;
using USP.Common;
using USP.Dal;
using System.Web.UI;

namespace USP.Bll.Impl
{
    public class SysOperatorBll : ISysOperatorBll
    {
        ISysOperatorService sysOperatorService;
        ISysOperatorDal sysOperatorDal;
        ISysUserDal operatorDal;
        //ISupplierBll supplierBll;
        //ISysLoginLogBll sysLoginLogBll;
        ISysLoginLogDal sysLoginLogDal;
        public SysOperatorBll(/*ISupplierBll supplierBll,*/ISysOperatorService sysOperatorService, Dal.ISysOperatorDal sysOperatorDal, ISysUserDal sysUserDal, ISysLoginLogDal sysLoginLogDal)
        {
            this.sysOperatorService = sysOperatorService;
            this.sysOperatorDal = sysOperatorDal;
            this.operatorDal = sysUserDal;
            this.sysLoginLogDal = sysLoginLogDal;
            //this.supplierBll = supplierBll;
        }

        public AjaxResult Login(Login login, HttpContextBase httpContext)
        {
            AjaxResult result = new AjaxResult();

            List<SysOperator> operators;
            List<SysMenu> menus;
            operators = sysOperatorService.Login(login.Name, Util.GetPassword(login.Name, login.Password), httpContext.Session.SessionID, HttpUtil.GetClientIP(httpContext));

            if (operators.Count > 0)
            {
                User user = new User();
                user.SysOperator = operators[0];
                user.SysCorp = sysOperatorService.GetCorp(user.SysOperator.Corp);
                //供应商赋值
                //var supplier = supplierBll.GetSupplierByOperatorId(user.SysOperator.ID);
                //if (supplier != null)
                //{
                //    user.Supplier = supplier;
                //}
                if ((user.SysCorp.Status!=0))
                {
                    result.flag = false;
                    result.message = "公司状态异常，请联系管理员！";
                    return result;
                }
                else if (user.SysOperator.Status != 0)
                {
                    result.flag = false;
                    result.message = "操作员状态异常，请联系管理员！";
                    return result;
                }
                else
                {
                    user.Privileges = sysOperatorService.GetPrivilege(operators[0].ID);
                    menus = sysOperatorService.GetMenu(operators[0].ID);
                    if (menus.Count > 0)
                    {
                        user.Menus = (from menu in menus where menu.Parent == 0 && menu.ID != menu.Parent select new UserMenu(menu, new List<UserMenu>())).ToList();
                        foreach (UserMenu userMenu in user.Menus)
                        {
                            userMenu.Children = (from menu in menus where menu.Parent == userMenu.SysMenu.ID && menu.ID != menu.Parent select new UserMenu(menu, new List<UserMenu>())).ToList();
                        }
                    }
                    if (null == user.Menus)
                    {
                        user.Menus = new List<UserMenu>();
                    }

                    httpContext.Session.Add(Constants.USER_KEY, user);
                    //添加登录日志
                    sysLoginLogDal.Add(new SysLoginLog()
                    {
                        Ip = Common.Common.GetHostAddress(),
                        LoginAgent = System.Web.HttpContext.Current.Request.Browser.Version.ToString(),
                        Operator = user.SysOperator.ID,
                        Success = true,
                        Time = DateTime.Now
                    });
                    result.flag = true;
                }
            }
            else
            {
                result.flag = false;
                result.message = "用户名或密码错误";
                return result;
            }
            return result;
        }

        public AjaxResult CheckSso(HttpContextBase httpContext)
        {
            AjaxResult ajaxResult = new AjaxResult();
            User user = (User)httpContext.Session[Constants.USER_KEY];
            if (user == null)
            {
                ajaxResult.flag = false;
                ajaxResult.message = "session is null";
            }
            else
            {
                String[] result = sysOperatorService.CheckSso(user.SysOperator.ID, httpContext.Session.SessionID).Split(new char[] { '|' });
                ajaxResult.attachment = user.SysOperator;
                ajaxResult.dateTime = DateTime.Now;
                if (Convert.ToBoolean(result[0]))
                {
                    ajaxResult.flag = true;
                    ajaxResult.message = "ok";
                }
                else
                {
                    httpContext.Session.Clear();
                    httpContext.Session.Abandon();
                    ajaxResult.flag = false;
                    ajaxResult.message = "有相同用户登陆或同一机器两用户登陆,您已被系统强制退出!";
                }
            }
            return ajaxResult;
        }

        #region libei
        public SysOperator GetModel(long @operator)
        {
            return sysOperatorDal.GetModel(@operator);
        }

        public int Add(SysOperator model)
        {
            return sysOperatorDal.Add(model);
        }

        public AjaxResult Edit(SysOperator model)
        {
            AjaxResult result = new AjaxResult();
            if (sysOperatorDal.Edit(model) > 0)
            {
                result.flag = true;
                result.message = "恭喜您，编辑成功！";
            }
            else
            {
                result.flag = false;
                result.message = "编辑失败！";
            }
            return result;
        }
        #endregion

        public List<SysRole> GetRoleList(Int64 corpid)
        {
            return sysOperatorService.GetRoles(corpid);
        }

        public SysOperator GetOperatorbyId(Int64 id)
        {
            return sysOperatorService.GetOperatorbyId(id);
        }

        public bool SaveOperator(OperaterAddEdit operators)
        {
            return operatorDal.SaveOperator(operators.Corp, operators.LoginName.Trim(), operators.RealName.Trim(), operators.Password.Trim(), operators.Mobile, operators.IdCard, operators.Email, operators.Creator, operators.Role,operators.Remark);
        }

        public SysOperator GetOperatorbyLoginName(string name)
        {
            return operatorDal.GetOperatorbyLoginName(name);
        }

     

        public DataGrid<Operator_Extend> GetOperatorGrid(int page, int pagesize, string order, string orderType, string userName, string RealName, long role, long status, long corp, long operatorID)
        {
            var result = new DataGrid<Operator_Extend>();
            var strWhere = "1=1";
            if (!string.IsNullOrWhiteSpace(userName))
            {
                strWhere += " and LoginName like '%" + userName + "%' ";
            }
            if (!string.IsNullOrWhiteSpace(RealName))
            {
                strWhere += " and RealName like '%" + RealName + "%' ";
            }
            if (status != -1)
            {
                strWhere += " and Status = " + status;
            }
            if (corp > 0)
            {
                strWhere += " and Corp = " + corp;
            }
            if (operatorID >= 0)
            {
                strWhere += " and ID <> " + operatorID;
            }
            if (corp != 0)
            {
                strWhere += "and [ID] not in(select Operator from SysRoleOperator where Role in (select [ID] from SysRole where Type=1))";//选出不为系统管理员的操作员
            }
            //strWhere += "and [ID] not in(select Operator from SysRoleOperator where Role in (select [ID] from SysRole where Type=1))";//选出不为系统管理员的操作员
            var operators = operatorDal.GetOperatorPage("*", strWhere, "ID ASC", page, pagesize);
            result.rows = operators;
            if (result.rows.Count > 0)
            {
                result.total = (long)result.rows[0].RowCnt;
            }
            return result;
        }

        public List<SysOperatorStatus> GetOperatorStatus()
        {
            return sysOperatorService.GetOperatorStatus();
        }

        public int AlterStatus(SysOperator model, long status, long oprid, int type)
        {
            return operatorDal.AlterStatus(model, status, oprid, type);

        }

        public int AlterPassword(long opid, string newPassword)
        {
            return operatorDal.AlterPassword(opid, newPassword);
        }

        /// <summary>
        /// 获取该公司角色集合
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="role">指定角色</param>
        /// <returns></returns>
        public List<TreeNode> GetUserRoleTree(long corp, long? opid)
        {
            var tree = new List<TreeNode>();
            //用户所在公司所有角色
            var Roles = GetRoleList(corp);
            //勾选角色
            var opRoles = new List<SysRole>();
            if (opid != null)
            {
                var Id = (long)opid;
                opRoles = operatorDal.GetRolesByOperator(Id);
            }
            GetUserRoleTree(tree, Roles, opRoles);

            return tree;
        }

        /// <summary>
        /// 递归组织用户的角色集合
        /// </summary>
        private void GetUserRoleTree(List<TreeNode> tree, List<SysRole> Roles, List<SysRole> userRoles)
        {
            foreach (var role in Roles)
            {
                var rolenode = new TreeNode();
                rolenode.id = role.ID;
                rolenode.text = role.Name;
                rolenode.attributes = new { type = 1 };//1菜单节点  2权限节点
                //节点的选中状态
                if (userRoles.Any(x => x.ID == role.ID))
                {
                    rolenode.@checked = true;
                }
                else
                {
                    rolenode.@checked = false;
                }

                rolenode.children = new List<TreeNode>();

                tree.Add(rolenode);
            }
        }

        public ProcResult EditOperator(OperaterAddEdit model)
        {
            return operatorDal.EditOperator(model);
        }

        public DataGrid<UP_ShowOperatorInfo_Result> GetOperatorPageData( int? pageIndex, int? pageSize,string userName, string RealName, long corp, long status,long operatorID, string strOrder="", string strOrderType="")
        {
            var result = new DataGrid<UP_ShowOperatorInfo_Result>();
            string strWhere = string.Empty;
            if (!string.IsNullOrWhiteSpace(userName))
            {
                strWhere += " and LoginName like '%" + userName + "%' ";
            }
           
            if (!string.IsNullOrWhiteSpace(RealName))
            {
                strWhere += " and RealName like '%" + RealName + "%' ";
            }
            if (status != -1)
            {
                strWhere += " and Status = " + status;
            }
            if (corp > 0)
            {
                strWhere += " and Corp = " + corp;
            }
            if (operatorID >= 0)
            {
                strWhere += " and ID <> " + operatorID;
            }
            if (corp == 0)
            {
                strWhere += " and [ID] in (select Operator from SysRoleOperator where Role in (select [ID] from SysRole where Type=1))";//选出为系统管理员的操作员
            }
            else
            {
                strWhere += " and [ID] not in (select Operator from SysRoleOperator where Role in (select [ID] from SysRole where Type=1))";//选出不为系统管理员的操作员
            }
            result.rows = operatorDal.GetOperatorPageData(Constants.DB_Server, Constants.DB_DataBase, Constants.DB_UID, Constants.DB_PWD, pageIndex, pageSize, strWhere, strOrder, strOrderType);
            if (result.rows.Count > 0)
            {
                result.total = result.rows[0].RowCnt;
            }
            return result;
            //GetOperatorPageData(Constants.DB_Server, Constants.DB_DataBase, Constants.DB_UID, Constants.DB_PWD, page, rows, "", "", "");
        }

        public List<SysOperator> GetAllOperator()
        {
           return sysOperatorDal.GetAllOperator();
        }
    }
}
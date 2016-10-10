using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Models.POCO;
using USP.Models.Entity;

namespace USP.Bll
{
    public interface ISysOperatorBll
    {
        //bool Login(Login login, HttpContextBase httpContext);
        AjaxResult Login(Login login, HttpContextBase httpContext);

        AjaxResult CheckSso(HttpContextBase httpContext);

        #region libei
        USP.Models.Entity.SysOperator GetModel(long @operator);
        int Add(USP.Models.Entity.SysOperator model);
        AjaxResult Edit(USP.Models.Entity.SysOperator model);
        #endregion

        List<SysRole> GetRoleList(Int64 corpid);
        SysOperator GetOperatorbyId(Int64 id);
        bool SaveOperator(OperaterAddEdit operators);
        List<SysOperatorStatus> GetOperatorStatus();
        DataGrid<Operator_Extend> GetOperatorGrid(int page, int pagesize, string order, string orderType, string userName, string RealName, long role, long status, long corp, long operatorID);
        int AlterStatus(SysOperator model, long status, long oprid, int type);
        int AlterPassword(long opid, string newPassword);
        SysOperator GetOperatorbyLoginName(string name);
        List<TreeNode> GetUserRoleTree(long corp, long? opid);
        ProcResult EditOperator(OperaterAddEdit model);
        DataGrid<UP_ShowOperatorInfo_Result> GetOperatorPageData( int? pageIndex, int? pageSize, string userName, string RealName, long status, long corp, long operatorID, string strOrder = "", string strOrderType = "");
        // List<UP_ShowOperatorInfo_Result> GetOperatorPageData(string server, string dataBase, string uID, string pWD, Nullable<int> pageIndex, Nullable<int> pageSize, string whereStr, string strOrder, string strOrderType);
        List<SysOperator> GetAllOperator();
    }
}
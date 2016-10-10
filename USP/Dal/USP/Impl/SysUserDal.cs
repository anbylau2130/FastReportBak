using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using USP.Context;
using USP.Models.Entity;
using USP.Utility;
using USP.Models.POCO;

namespace USP.Dal.Impl
{
    public class SysUserDal : ISysUserDal
    {
        USPEntities db = new USPEntities();

        public SysOperator GetOperatorbyId(Int64 id)
        {
            try
            {
                return db.SysOperator.Find(id);
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                return new SysOperator();
            }
        }

        public bool SaveOperator(long Corp, string LoginName, string RealName, string Password, string Mobile, string IdCard, string Email, long Creator, string Role,string Remark)
        {
            try
            {
                var obj = db.UP_AddOperator(Corp, LoginName, RealName, Password, Mobile, IdCard, Email, Creator, Role, Remark);
                var id = obj.FirstOrDefault();
                if (id == null) return false;
                return id > 0;
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                return false;
            }
        }

        public List<SysOperatorStatus> GetOperatorStatus()
        {
            try
            {
                var role = db.SysOperatorStatus.ToList();
                return role;
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                return new List<SysOperatorStatus>();
            }
        }

        public List<Operator_Extend> GetOperatorPage(string strGetFields, string strWhere, string strOrder, int page, int pageSize)
        {
            var _tbName = "SysOperator";
            return SysPublicFun.GetPage<Operator_Extend>(db, strGetFields, _tbName, strWhere, strOrder, page, pageSize);
        }


        public List<UP_ShowOperatorInfo_Result> GetOperatorPageData(string server, string dataBase, string uID, string pWD, Nullable<int> pageIndex, Nullable<int> pageSize, string whereStr, string strOrder, string strOrderType)
        {
           return db.UP_ShowOperatorInfo(server, dataBase, uID, pWD, pageIndex, pageSize, whereStr, strOrder, strOrderType).ToList<UP_ShowOperatorInfo_Result>();
        }

        public int AlterStatus(SysOperator model, long status, long oprid, int type)
        {
            int result = 0;
            try
            {
                if (model != null)
                {
                    var obj = db.UP_AlterStatus(status, oprid, model.ID, type);
                    var id = obj.FirstOrDefault();
                    if (id == null) return 0;
                    result = 1;
                    return result;
                }
                return result;
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);

                return result;
            }

        }

        public int AlterPassword(long opid, string newPassword)
        {
            int result;
            try
            {
                //var model = db.SysOperator.Find(opid);
                //if (model != null)
                //{
                //    model.Password = newPassword;
                //}
                //db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                //result = db.SaveChanges();

                var obj = db.UP_AlterPassword(opid, newPassword);
                var id = obj.FirstOrDefault();
                if (id == null) return 0;
                result = 1;
                return result;
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                result = 0;
            }
            return result;
        }

        public SysOperator GetOperatorbyLoginName(string name)
        {
            try
            {
                return db.SysOperator.FirstOrDefault(x => x.LoginName == name);
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                return new SysOperator();
            }
        }

        public List<SysRole> GetRolesByOperator(long? opid)
        {
            if (opid != null)
            {
                return db.UP_GetOperatorRole(opid).ToList();
            }
            else
            {
                return null;
            }
        }

        public ProcResult EditOperator(OperaterAddEdit model)
        {
            try
            {
                //var obj = db.UP_EditOperator(iD,loginName, realName, creator, role);
                //var id = obj.FirstOrDefault();
                //if (id != null)
                //{
                //    return true;
                //}
                //return false;
                var result = db.UP_EditOperator(model.ID, model.LoginName,model.RealName, model.Creator, model.Role,model.Mobile,model.IdCard,model.Email).FirstOrDefault();

                return new ProcResult()
                {
                    IsSuccess = result.Split('|')[0] == "true",
                    ProcMsg = result.Split('|')[1]
                };
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                return new ProcResult();
            }
        }

    }
}
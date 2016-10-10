using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using USP.Context;
using USP.Models.Entity;
using USP.Models.POCO;
using USP.Utility;

namespace USP.Dal.Impl
{
    public class SysPrivilegeDal : ISysPrivilegeDal
    {
        readonly USPEntities db = new USPEntities();

        private readonly string _tbName = "SysPrivilege";

        public List<SysPrivilege> GetPrivilegeByOperator(long @operator)
        {
            return db.UP_GetOperatorPrivilege(@operator).ToList();
        }
        public void AddPrivilege(string menu, string parent, string name, string @class, string controller, string area, string action, string parameter, string url)
        {
            db.UP_AddPrivilege(menu, parent, name, @class, area, controller, action, parameter, url);
        }

        public List<SysPrivilege> GetPrivilegeByRole(long @role)
        {
            return db.UP_GetRolePrivilege(@role).ToList();
        }

        /// <summary>
        /// 检测同一菜单权限是否存在
        /// </summary>
        /// <param name="name">权限</param>
        /// <param name="menu">公司</param>
        /// <returns>true 存在 false不存在</returns>
        public bool CheckPrivilegeName(string name, long menu)
        {
            return db.SysPrivilege.Any(x => x.Name == name && x.Menu == menu);
        }

        public SysPrivilege GetPrivilegeByID(long id)
        {
            return db.SysPrivilege.FirstOrDefault(x => x.ID == id);
        }

        public SysPrivilege AddPrivilege(SysPrivilege model)
        {
            try
            {
                db.SysPrivilege.Add(model);
                db.SaveChanges();
                return model;
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                return null;
            }
        }

        public SysPrivilege EditPrivilege(SysPrivilege model)
        {
            try
            {
                db.SysPrivilege.Attach(model);
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return model;
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                return null;
            }
        }

        /// <summary>
        /// 权限分页查询
        /// </summary>
        /// <param name="strGetFields">查询字段</param>
        /// <param name="strWhere">条件</param>
        /// <param name="strOrder">排序字段 ASC DESC</param>
        /// <param name="page">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public List<SysPrivilegeExt> GetSysPrivilegePage(string strGetFields, string strWhere, string strOrder, int page, int pageSize)
        {
            return SysPublicFun.GetPage<SysPrivilegeExt>(db, strGetFields, _tbName, strWhere, strOrder, page, pageSize);
        }

        /// <summary>
        /// 标记删除权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="op"></param>
        /// <returns></returns>
        public bool DeletePrivilege(long id, long op)
        {
            try
            {
                var obj = db.UP_DeletPrivilege(id, op);
                var result = obj.FirstOrDefault();
                if (result == null) return false;
                return result > 0;
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                return false;
            }
        }

        /// <summary>
        /// 激活权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="op"></param>
        /// <returns></returns>
        public bool ActionPrivilege(long id, long op)
        {
            try
            {
                var privilege = db.SysPrivilege.FirstOrDefault(x => x.ID == id);
                if (privilege == null) return false;
                privilege.Canceler = null;
                privilege.CancelTime = null;
                privilege.Creator = op;
                privilege.CreateTime = DateTime.Now;
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                return false;
            }
        }
    }
}

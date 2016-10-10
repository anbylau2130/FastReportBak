using System;
using System.Collections.Generic;
using System.Linq;
using USP.Context;
using USP.Models.Entity;
using USP.Models.POCO;
using USP.Utility;

namespace USP.Dal.Impl
{
    public class SysRoleDal : ISysRoleDal
    {
        readonly USPEntities _db = new USPEntities();

        private readonly string _tbName = "SysRole";

        public List<SysRole> GetSysRoleByOperator(long @operator)
        {
            return _db.UP_GetRoleByOperator(@operator).ToList();
        }
        public bool AddRole(long corp, bool type, string name, string remark, string menus, string privileges, long creator)
        {
            try
            {
                var obj = _db.UP_AddRole(corp, name, type, remark, creator, menus, privileges);
                var role = obj.FirstOrDefault();
                if (role == null) return false;
                return role > 0;
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                return false;
            }
        }
        public bool EditRole(long id, string name, string remark, string menus, string privileges, long creator)
        {
            try
            {
                var obj = _db.UP_EditRole(id, name, remark, creator, menus, privileges);
                var role = obj.FirstOrDefault();
                if (role == null) return false;
                return role > 0;
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                return false;
            }
        }

        public SysRole GetRoleByID(long id)
        {
            return _db.SysRole.FirstOrDefault(x => x.ID == id);
        }

        /// <summary>
        /// 检测同一公司角色名是否存在
        /// </summary>
        /// <param name="name">角色名称</param>
        /// <param name="corp">公司</param>
        /// <returns>true 存在 false不存在</returns>
        public bool CheckRoleName(string name, long corp)
        {
            return _db.SysRole.Any(x => x.Name == name && x.Corp == corp);
        }

        /// <summary>
        /// 角色分页查询
        /// </summary>
        /// <param name="strGetFields">查询字段</param>
        /// <param name="strWhere">条件</param>
        /// <param name="strOrder">排序字段 ASC DESC</param>
        /// <param name="page">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public List<SysRoleExt> GetSysRolePage(string strGetFields, string strWhere, string strOrder, int page, int pageSize)
        {
            return SysPublicFun.GetPage<SysRoleExt>(_db, strGetFields, _tbName, strWhere, strOrder, page, pageSize);
        }

        public bool CancelOrActionRole(long roleId, long @operator)
        {
            try
            {
                var role = _db.SysRole.FirstOrDefault(x => x.ID == roleId);
                if (role == null) return false;
                if (role.Canceler == null && role.CancelTime == null)
                {
                    role.Canceler = @operator;
                    role.CancelTime = DateTime.Now;
                }
                else
                {
                    role.Canceler = null;
                    role.CancelTime = null;
                    role.Creator = @operator;
                    role.CreateTime = DateTime.Now;
                }
                return _db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                return false;
            }
        }

        /// <summary>
        /// 根据公司查询角色
        /// </summary>
        /// <param name="corpid"></param>
        /// <returns></returns>
        public List<SysRole> getRolesByCrop(long corpid)
        {
            try
            {
                var role =new List<SysRole>();
                if (corpid==0)
                {
                    role = _db.SysRole.Where(x =>  x.Canceler == null && x.CancelTime == null &&x.Type).ToList();
                }
                else
                {
                    role = _db.SysRole.Where(x => x.Corp == corpid&&x.Canceler==null&&x.CancelTime==null&&x.Type==false).ToList();

                }
                return role;
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                return new List<SysRole>();
            }
        }
    }
}

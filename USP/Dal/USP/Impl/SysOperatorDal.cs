using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Context;
using USP.Models.Entity;
using USP.Models.POCO;
using USP.Utility;

namespace USP.Dal.Impl
{
    public class SysOperatorDal : ISysOperatorDal
    {
        USPEntities db = new USPEntities();
        public List<SysOperator> Login(string loginName, string password, string session, string ip)
        {
            return db.UP_Login(loginName, password, session, ip).ToList();
        }

        public List<SysMenu> GetMenus(long @operator)
        {
            System.Web.Caching.Cache objCache = System.Web.HttpRuntime.Cache;

            var cacheObj = objCache["menu_" + @operator];

            if (cacheObj == null)
            {
                cacheObj = db.UP_GetOperatorMenu(@operator).ToList();

                objCache.Insert("menu_" + @operator, cacheObj, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromSeconds(1800));
            }

            return (List<SysMenu>)cacheObj;
        }
        public List<SysPrivilege> GetPrivileges(long @operator)
        {
            return db.UP_GetOperatorPrivilege(@operator).ToList();
        }

        public string CheckSso(long @operator, string session)
        {
            return db.UP_CheckSSO(@operator, session).FirstOrDefault();
        }

        public SysCorp GetCorp(long corp)
        {
            return db.SysCorp.FirstOrDefault(x => x.ID == corp);
        }

        public List<SysOperator> GetAllOperator()
        {
            return db.SysOperator.ToList();
        }

        #region libei
        public SysOperator GetModel(long @operator)
        {
            return db.SysOperator.FirstOrDefault(x => x.ID == @operator);
        }

        public int Add(SysOperator model)
        {
            int result;
            try
            {
                db.SysOperator.Add(model);
                result = db.SaveChanges();
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                result = 0;
            }
            return result;
        }

        public int Edit(SysOperator model)
        {
            int result;
            var entity = GetModel(model.ID);
            try
            {
                if (entity != null)
                {
                    entity.Email = model.Email;
                    entity.Weibo = model.Weibo;
                    entity.Address = model.Address;
                }
                db.Entry<SysOperator>(entity).State = System.Data.Entity.EntityState.Modified;
                result = db.SaveChanges();
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                result = 0;
            }
            return result;
        }

        public string ConverIdToName(long id)
        {
            var entitys = db.SysOperator.ToList();
            return "";
        }
        #endregion
    }
}

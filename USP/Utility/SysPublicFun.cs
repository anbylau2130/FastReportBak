using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace USP.Utility
{
    public class SysPublicFun
    {
        /// <summary>
        /// 通用存储过程分页调用
        /// </summary>
        /// <typeparam name="T">接收对象</typeparam>
        /// <param name="db">上下文实例</param>
        /// <param name="strGetFields">查询字段</param>
        /// <param name="tbName">表名</param>
        /// <param name="strWhere">条件</param>
        /// <param name="strOrder">排序字段 ASC</param>
        /// <param name="page">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns>T集合</returns>
        public static List<T> GetPage<T>(DbContext db, string strGetFields, string tbName, string strWhere, string strOrder, int page, int pageSize)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(strGetFields)) strGetFields = "*";
                if (page <= 0) page = 1;
                if (pageSize <= 0) pageSize = 10;
                object[] parames =
                {
                    new SqlParameter("@strGetFields", strGetFields),
                    new SqlParameter("@tblName", tbName),
                    new SqlParameter("@strWhere", strWhere),
                    new SqlParameter("@strOrder", strOrder),
                    new SqlParameter("@PageIndex", page),
                    new SqlParameter("@PageSize", pageSize)
                };

                return
                    db.Database.SqlQuery<T>(
                        "exec UP_ShowPageMultiSort @strGetFields,@tblName,@strWhere,@strOrder,@PageIndex,@PageSize",
                        parames).ToList();
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                return new List<T>();
            }
        }
    }
}
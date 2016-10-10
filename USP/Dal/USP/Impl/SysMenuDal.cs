using System;
using System.Collections.Generic;
using System.Linq;
using USP.Context;
using USP.Models.Entity;
using USP.Models.POCO;
using USP.Utility;

namespace USP.Dal.Impl
{
    public class SysMenuDal : ISysMenuDal
    {
        readonly USPEntities db = new USPEntities();
        public List<SysMenu> getMenuByOperator(long @operator)
        {
            return db.UP_GetOperatorMenu(@operator).ToList();
        }

        public void addMenu(string name, string icon)
        {
            db.UP_AddMenu(name, icon);
        }

        public void addMenuItem(string parent, string name, string icon, string @class, string area, string controller, string action, string parameter, string url)
        {
            db.UP_AddMenuItem(parent, name, icon, @class, area, controller, action, parameter, url);
        }

        public List<SysMenu> getMenuByRole(long @role)
        {
            return db.UP_GetRoleMenu(@role).ToList();
        }

        public List<SysMenu> GetAllMenus()
        {
            return db.SysMenu.ToList();
        }


        #region libei
        public List<SysMenu> getMenuGrid(string name)
        {
            var entitys = db.SysMenu.Where(x => x.Name != "root");

            if (!string.IsNullOrEmpty(name))
            {
                entitys = entitys.Where(x => x.Name.Contains(name));
            }
            return entitys.ToList();
        }

        /// <summary>
        /// 菜单实体
        /// </summary>
        /// <param name="id">菜单主键Id</param>
        /// <returns>SysMenu</returns>
        public SysMenu GetModel(long @id)
        {
            return db.SysMenu.FirstOrDefault(x => x.ID == @id);
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="model">菜单实体</param>
        /// <returns>int</returns>
        public string Add(SysMenu model)
        {
            return db.UP_AddMenus(model.Parent.ToString(), model.Name, model.Icon, model.Clazz, model.Area, model.Controller, model.Method, model.Parameter, model.Url, model.Creator).FirstOrDefault();
        }

        /// <summary>
        /// 编辑菜单
        /// </summary>
        /// <param name="model">菜单实体</param>
        /// <returns>int</returns>
        public string Edit(SysMenu model)
        {
            return db.UP_EditMenus(model.ID, model.Parent.ToString(), model.Name, model.Icon, model.Clazz, model.Area, model.Controller, model.Method, model.Parameter, model.Url).FirstOrDefault();
        }

        public bool IsExistMenuName(long id, long parent, string name)
        {
            bool result = false;
            if (id == 0)
            {
                result = db.SysMenu.Where(x => x.Parent == parent && x.Name == name).Count() > 0 ? true : false;
            }
            else
            {
                result = db.SysMenu.Where(x => x.ID != id && x.Parent == parent && x.Name == name).Count() > 0 ? true : false;
            }
            return result;
        }

        /// <summary>
        /// 审核菜单
        /// </summary>
        /// <param name="id">菜单主键Id</param>
        /// <param name="auditor">操作员Id</param>
        /// <returns>int</returns>
        public int Audit(SysMenu model)
        {
            int result;
            try
            {
                var entity = GetModel(model.ID);
                if (entity != null)
                {
                    entity.Auditor = model.Auditor;
                    entity.AuditTime = DateTime.Now;
                }
                db.Entry<SysMenu>((SysMenu)entity).State = System.Data.Entity.EntityState.Modified;
                result = db.SaveChanges();
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                result = 0;
            }
            return result;
        }

        /// <summary>
        /// 注销菜单
        /// </summary>
        /// <param name="id">菜单zhujianId</param>
        /// <param name="operater">操作员Id</param>
        /// <returns>int</returns>
        public string Cancel(long id, long operater)
        {
            return db.UP_CancelMenu(id, operater).FirstOrDefault();
        }

        //public List<TreeNode> QueryTreeList(string id)
        //{
        //    id = "," + id + ",";

        //    var entitys = db.SysMenu.Where(x => x.Parent == 0 && x.ID > 0 && x.Canceler == null && x.CancelTime == null);

        //    return ToViewModel(id, entitys);
        //}

        //private List<TreeNode> ToViewModel(string id, IEnumerable<SysMenu> model)
        //{
        //    var list = new List<TreeNode>();

        //    foreach (var v in model)
        //    {
        //        var temp = new TreeNode()
        //        {
        //            id = v.ID,
        //            text = v.Name,
        //            state = "open",
        //            @checked = false,
        //            children = new List<TreeNode>(),
        //        };

        //        temp.@checked = id.Contains("," + v.ID + ",");

        //        if (v.ID != 0)
        //        {
        //            var children = db.SysMenu.Where(x => x.Parent == v.ID && x.CancelTime == null && x.Canceler == null);

        //            if (children.Count() > 0)
        //            {
        //                temp.children = ToViewModel(id, children);
        //            }
        //        }
        //        list.Add(temp);

        //    }

        //    return list;
        //}
        #endregion
    }
}

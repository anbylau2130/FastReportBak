using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using USP.Context;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Dal.Impl
{
    public class SupplierGroupDal : ISupplierGroupDal
    {
        K3CloudEntities db = new K3CloudEntities();

        public List<T_BD_SUPPLIERGROUP> GetAll()
        {
            return db.T_BD_SUPPLIERGROUP.ToList();
        }

        public List<TreeNode> GetSupplierGroupsByPId(long id=0)
        {
            var groupresult = from groups in db.T_BD_SUPPLIERGROUP
                join name in db.T_BD_SUPPLIERGROUP_L on groups.FID equals name.FID
                where groups.FPARENTID == id  
                select new TreeNode {id = groups.FID, text = groups.FNUMBER+"("+name.FNAME+")",state= "closed" };
            return groupresult.ToList();
        }

        public List<T_BD_SUPPLIERGROUP_L> GetAllLanguage()
        {
            return db.T_BD_SUPPLIERGROUP_L.ToList();
        }


    }
}
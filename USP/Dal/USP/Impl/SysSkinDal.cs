using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using USP.Context;
using USP.Models.Entity;

namespace USP.Dal.Impl
{
    public class SysSkinDal : ISysSkinDal
    {
        USPEntities db = new USPEntities();
        public List<SysSkin> Getlists()
        {
            return db.SysSkin.Where(x => x.Canceler == null && x.CancelTime == null).ToList();
        }

        public SysSkin GetModelById(long id)
        {
            return db.SysSkin.FirstOrDefault(x => x.ID == id);
        }
    }
}
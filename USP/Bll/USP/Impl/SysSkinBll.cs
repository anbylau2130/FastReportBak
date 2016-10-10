using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using USP.Dal;
using USP.Models.Entity;



namespace USP.Bll.Impl
{
    public class SysSkinBll : ISysSkinBll
    {
        ISysSkinDal sysSkinDal;
        public SysSkinBll(USP.Dal.ISysSkinDal sysSkinDal)
        {
            this.sysSkinDal = sysSkinDal;
        }
        public List<SysSkin> Getlists()
        {
            return sysSkinDal.Getlists();
        }

        public SysSkin GetModelById(long id)
        {
            return sysSkinDal.GetModelById(id);
        }
    }
}
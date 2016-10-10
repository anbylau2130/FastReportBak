using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using USP.Models.Entity;


namespace USP.Dal
{
    public interface ISysSkinDal
    {

         List<SysSkin> Getlists();

         SysSkin GetModelById(long id);
    }
}


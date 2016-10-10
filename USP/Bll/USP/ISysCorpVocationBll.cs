using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Models.Entity;

namespace USP.Bll
{
    public interface ISysCorpVocationBll
    {
        List<SysCorpVocation> GetSysCorpVocationDropDownList();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Models.Entity;

namespace USP.Dal
{
    public interface ISysCorpStatusDal
    {
        List<SysCorpStatus> GetSysCorpStatus();
    }
}

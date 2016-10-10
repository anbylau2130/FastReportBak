using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Models.Entity;

namespace USP.Bll.K3CLoud
{
    public interface IHRORGBll
    {
        List<HXN_HR_ORG> GetORGAll();
        List<HXN_HR_ORG_L> GetORGLanguageALL();
        List<HXN_HR_ORG_Group> GetORGGroupAll();
        List<HXN_HR_ORG_Group_L> GetORGGroupLanguageAll();
    }
}

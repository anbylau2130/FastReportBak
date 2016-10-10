using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Bll
{
    public interface ISysCorpTypeBll
    {
        AjaxResult IsExisName(int id, string name);
        List<SysCorpType> GetSysCorpTypes();

        List<SysCorpType> GetSysCorpTypes(string name);

        List<SelectOption> GetCorpTypeList(long id);

        SysCorpType GetModelById(long corpType);

        bool Add(SysCorpType model);

        bool Edit(SysCorpType model);

        AjaxResult Cancel(int id, long @operator);

        AjaxResult Active(int id, long @operator);
    }
}

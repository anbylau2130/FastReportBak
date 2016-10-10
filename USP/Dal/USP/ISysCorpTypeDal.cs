using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Models.Entity;

namespace USP.Dal
{
    public interface ISysCorpTypeDal
    {

        bool IsExisName(int id, string name);
        List<SysCorpType> GetSysCorpTypes();
        List<SysCorpType> GetSysCorpTypes(string name);

        SysCorpType GetModelById(long corpType);

        bool Add(SysCorpType model);

        bool Edit(SysCorpType model);

        int Cancel(int id, long @operator);

        int Active(int id, long @operator);

    }
}

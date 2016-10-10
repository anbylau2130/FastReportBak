using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Dal
{
    public interface ISysCorpDal
    {
        List<UP_ShowCorp_Result> getCorps(int? pageIndex, int? pageSize, string whereStr, string strOrder, string strOrderType);
        ProcResult AddCorporation(string corpName, long corpType, long @operator, long parentCorp, string loginName, string password);
        bool EditCorporation(SysCorp syscorp);
        bool AuditorCorporation(SysCorp syscorp);

        SysCorp GetCorporation(long ID);
        int CancelCorpotation(long id, long currentOperator);
        int EnableCorpotation(long id, long currentOperator);
        int AuditorCorporation(long id, long auditor);

    }
}

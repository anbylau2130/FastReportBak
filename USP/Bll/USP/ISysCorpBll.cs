using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Bll
{
    public interface ISysCorpBll
    {
        DataGrid<UP_ShowCorp_Result> GetCorpGrid(int? pageIndex, int? pageSize, string whereStr, string strOrder, string strOrderType);

        AjaxResult AddCorporation(string corpName, long corpType, long @operator, long parentCorp,string loginName,string password);

        bool EditCorporation(SysCorp corp);

        SysCorp GetCorporation(long ID);

        AjaxResult AuditorCorporation(long id, long auditor);

        AjaxResult CancelCorpotation(long id, long currentOperator);

        AjaxResult EnableCorpotation(long id, long currentOperator);


    }
}

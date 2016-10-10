using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using Newtonsoft.Json.Linq;
using USP.Dal;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Bll.Impl
{
    public class SysCorpBll : ISysCorpBll
    {
        private ISysCorpDal sysCorpDal;

        public SysCorpBll(ISysCorpDal sysCorpDal)
        {
            this.sysCorpDal = sysCorpDal;
        }

        public DataGrid<UP_ShowCorp_Result> GetCorpGrid(int? pageIndex, int? pageSize, string whereStr, string strOrder, string strOrderType)
        {
            DataGrid<UP_ShowCorp_Result> result = new DataGrid<UP_ShowCorp_Result>();
            result.rows = sysCorpDal.getCorps(pageIndex, pageSize, whereStr, strOrder, strOrderType);
            if (result.rows.Count > 0)
            {
                result.total = (long)result.rows[0].RowCnt;
            }
            return result;
        }

        public AjaxResult AddCorporation(string corpName, long corpType, long @operator, long parentCorp, string loginName, string password)
        {
            ProcResult result = sysCorpDal.AddCorporation(corpName, corpType, @operator, parentCorp, loginName, password);
            return new AjaxResult()
            {
                flag = result.IsSuccess,
                dateTime = DateTime.Now,
                message = result.ProcMsg
            };
        }

        public bool EditCorporation(SysCorp corp)
        {
            if (sysCorpDal.EditCorporation(corp))
            {
                return true;
            }
            return false;
        }

        public SysCorp GetCorporation(long ID)
        {
            return sysCorpDal.GetCorporation(ID);
        }

        public AjaxResult AuditorCorporation(long id, long auditor)
        {

            AjaxResult result = new AjaxResult();
            var entityResult = sysCorpDal.AuditorCorporation(id, auditor);
            switch (entityResult)
            {
                case 0:
                    result.flag = false;
                    result.message = "审核失败！";
                    break;
                case 1:
                    result.flag = true;
                    result.message = "恭喜您，审核通过！";
                    break;
                case -1:
                    result.flag = false;
                    result.message = "审核失败，请查看公司信息是否完善！";
                    break;
                default:
                    result.flag = false;
                    result.message = "审核失败！";
                    break;
            }
            return result;

        }

        public AjaxResult CancelCorpotation(long id, long currentOperator)
        {

            AjaxResult result = new AjaxResult();
            if (sysCorpDal.CancelCorpotation(id, currentOperator) > 0)
            {
                result.flag = true;
                result.message = "恭喜您，注销成功！";
            }
            else
            {
                result.flag = false;
                result.message = "注销失败！";
            }
            return result;

        }

        public AjaxResult EnableCorpotation(long id, long currentOperator)
        {

            AjaxResult result = new AjaxResult();
            if (sysCorpDal.EnableCorpotation(id, currentOperator) > 0)
            {
                result.flag = true;
                result.message = "恭喜您，激活成功！";
            }
            else
            {
                result.flag = false;
                result.message = "激活失败！";
            }
            return result;

        }

    }
}

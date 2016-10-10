using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Dal;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Bll.Impl
{
    public class SysCorpStatusBll : ISysCorpStatusBll
    {
        ISysCorpStatusDal sysCorpStatusDal;
        public SysCorpStatusBll(ISysCorpStatusDal sysCorpStatusDal)
        {
            this.sysCorpStatusDal = sysCorpStatusDal;
        }

        public List<SelectOption> GetSysCorpStatus()
        {
            var entity = sysCorpStatusDal.GetSysCorpStatus();
            List<SelectOption> list = new List<SelectOption>();
            var all = new SelectOption()
            {
                id = "-1",
                text = "全部",
                selected = true
            };
            list.Add(all);
            foreach (var v in entity)
            {
                var temp = new SelectOption()
                {
                    id = v.ID.ToString(),
                    text = v.Name,
                    selected = false
                };
                list.Add(temp);
            }
            return list;
        }
    }
}

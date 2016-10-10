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
    public class SysCorpTypeBll : ISysCorpTypeBll
    {
        ISysCorpTypeDal sysCorpTypeDal;

        public SysCorpTypeBll(ISysCorpTypeDal sysCorpTypeDal)
        {
            this.sysCorpTypeDal = sysCorpTypeDal;
        }

        public List<SysCorpType> GetSysCorpTypes()
        {
            return sysCorpTypeDal.GetSysCorpTypes();
        }

        public AjaxResult IsExisName(int id, string name)
        {
            AjaxResult result = new AjaxResult();
            if (sysCorpTypeDal.IsExisName(id, name))
            {
                result.flag = true;
                result.message = "已经存在公司类型名称！";
            }
            else
            {
                result.flag = false;
                result.message = "";
            }
            return result;
        }

        public List<SysCorpType> GetSysCorpTypes(string name)
        {
            return sysCorpTypeDal.GetSysCorpTypes(name);
        }
        public SysCorpType GetModelById(long corpType)
        {
            return sysCorpTypeDal.GetModelById(corpType);
        }

        public List<SelectOption> GetCorpTypeList(long id)
        {
            var entity = sysCorpTypeDal.GetSysCorpTypes();
            List<SelectOption> list = new List<SelectOption>();
            foreach (var v in entity)
            {
                var temp = new SelectOption()
                {
                    id = v.ID.ToString(),
                    text = v.Name,
                    selected = v.ID == id
                };
                list.Add(temp);
            }
            return list;
        }

        public bool Add(SysCorpType model)
        {
            return sysCorpTypeDal.Add(model);
        }

        public bool Edit(SysCorpType model)
        {
            return sysCorpTypeDal.Edit(model);
        }

        public AjaxResult Cancel(int id, long @operator)
        {
            AjaxResult result = new AjaxResult();

            if (sysCorpTypeDal.Cancel(id, @operator) > 0)
            {
                result.flag = true;
                result.message = "注销成功！";
            }
            else
            {
                result.flag = false;
                result.message = "注销失败！";
            }
            return result;
        }

        public AjaxResult Active(int id, long @operator)
        {
            AjaxResult result = new AjaxResult();

            if (sysCorpTypeDal.Active(id, @operator) > 0)
            {
                result.flag = true;
                result.message = "激活成功！";
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

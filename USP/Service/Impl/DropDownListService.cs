using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using USP.Bll;
using USP.Dal;
using USP.Models.Entity;

namespace USP.Service.Impl
{
    public class DropDownListService : IDropDownListService
    {
        ISysCorpTypeBll sysCorpTypeBll;
        ISysCorpFeeTypeBll sysCorpFeeTypeBll;
        ISysCorpGradeBll sysCorpGradeBll;
        ISysCorpVocationBll sysCorpVocationBll;
        ISysAreaService sysAreaService;

        public DropDownListService(
             ISysCorpTypeBll sysCorpTypeBll,
        ISysCorpFeeTypeBll sysCorpFeeTypeBll,
        ISysCorpGradeBll sysCorpGradeBll,
        ISysCorpVocationBll sysCorpVocationBll,
        ISysAreaService sysAreaService)
        {
            this.sysCorpTypeBll = sysCorpTypeBll;
            this.sysCorpFeeTypeBll = sysCorpFeeTypeBll;
            this.sysCorpGradeBll = sysCorpGradeBll;
            this.sysCorpVocationBll = sysCorpVocationBll;
            this.sysAreaService = sysAreaService;
        }




        public List<SelectListItem> InitCorpTypeDropDownList()
        {
            return sysCorpTypeBll.GetSysCorpTypes().Select(item => new SelectListItem() { Text = item.Name, Value = item.ID.ToString() }).ToList();
        }

        public List<SelectListItem> InitCorpFeeTypeDropDownList()
        {
            return sysCorpFeeTypeBll.GetCorpFeeTypeDropDownList().Select(item => new SelectListItem() { Text = item.Name, Value = item.ID.ToString() }).ToList();
        }

        public List<SelectListItem> InitSysCorpGradeDropDownList()
        {
            return sysCorpGradeBll.GetSysCorpGradeDropDownList().Select(item => new SelectListItem() { Text = item.NAme, Value = item.ID.ToString() }).ToList();
        }

        public List<SelectListItem> InitVocationDropDownList()
        {
            return sysCorpVocationBll.GetSysCorpVocationDropDownList().Select(item => new SelectListItem() { Text = item.Name, Value = item.ID.ToString() }).ToList();
        }

        public List<SelectListItem> InitProvinceDropDownList()
        {
            return sysAreaService.ProvinceDropDownList().Select(item => new SelectListItem() { Text = item.Name, Value = item.Code.ToString() }).ToList();
        }

        public List<SelectListItem> InitCountyDropDownList(string cityCode)
        {
            return sysAreaService.CountyDropDownList(cityCode).Select(item => new SelectListItem() { Text = item.Name, Value = item.Code.ToString() }).ToList();
        }

        public List<SelectListItem> InitCityDropDownList(string provinceCode)
        {
            return sysAreaService.CityDropDownList(provinceCode).Select(item => new SelectListItem() { Text = item.Name, Value = item.Code.ToString() }).ToList();
        }
    }
}

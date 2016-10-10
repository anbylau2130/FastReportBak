using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Models.Entity;
using System.Web.Mvc;

namespace USP.Service
{
    public interface IDropDownListService
    {
        List<SelectListItem> InitCorpTypeDropDownList();

        List<SelectListItem> InitCorpFeeTypeDropDownList();

        List<SelectListItem> InitSysCorpGradeDropDownList();


        List<SelectListItem> InitVocationDropDownList();


        List<SelectListItem> InitProvinceDropDownList();


        List<SelectListItem> InitCountyDropDownList(string cityCode);


        List<SelectListItem> InitCityDropDownList(string provinceCode);

    }
}

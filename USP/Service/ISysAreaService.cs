using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Service
{
    public interface ISysAreaService
    {
        void Create(SysArea area);
        List<SysArea> getAll();
        List<UP_ShowArea_Result> showPage(int page, int rows, string order, string orderType);
        List<UP_ShowProvince_Result> ProvinceDropDownList();
        List<UP_ShowCity_Result> CityDropDownList(string provinceCode);
        List<UP_ShowCounty_Result> CountyDropDownList(string cityCode);


        List<SelectOption> GetProvinces(string selectedId);

        List<SelectOption> GetAreasByParent(string parentId);
        List<SelectOption> GetAreasByParent(string parentId, string selectedId);
    }
}

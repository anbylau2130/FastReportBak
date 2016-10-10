using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Models.Entity;

namespace USP.Dal
{
    public interface ISysAreaDal
    {
        void create(SysArea area);
        List<SysArea> getAll();
        List<UP_ShowArea_Result> showPage(int page,int rows,string order,string orderType);
        List<UP_ShowProvince_Result> ShowProvince();
        List<UP_ShowCity_Result> ShowCity(string provinceCode);
        List<UP_ShowCounty_Result> ShowCounty(string cityCode);


        List<Area> GetProvinces();
        List<Area> GetAreas(string code);
        List<Area> GetCountys(string code);
        List<Area> GetCommunitys(string code);
        List<Area> GetAreasByParent(string parentId);
    }
}

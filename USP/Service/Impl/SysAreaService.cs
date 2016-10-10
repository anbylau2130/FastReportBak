using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Dal;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Service.Impl
{
    
    public class SysAreaService : ISysAreaService
    {
        ISysAreaDal areaDal;

        public SysAreaService(ISysAreaDal areaDal)
        {
            this.areaDal = areaDal;
        }

        public void Create(SysArea area)
        {
            areaDal.create(area);
        }

        public List<SysArea> getAll()
        {
            return areaDal.getAll();
        }
        public List<UP_ShowArea_Result> showPage(int page, int rows, string order, string orderType)
        {
            return areaDal.showPage(page, rows, order, orderType);
        }

        public List<UP_ShowProvince_Result> ProvinceDropDownList()
        {
            return areaDal.ShowProvince();
        }

        public List<UP_ShowCity_Result> CityDropDownList(string provinceCode)
        {
            return areaDal.ShowCity(provinceCode);
        }

        public List<UP_ShowCounty_Result> CountyDropDownList(string cityCode)
        {
            return areaDal.ShowCounty(cityCode);
        }


        public List<SelectOption> GetProvinces(string selectedId)
        {
            var entity = areaDal.GetProvinces();
            List<SelectOption> list = new List<SelectOption>();
            foreach (var v in entity)
            {
                var temp = new SelectOption()
                {
                    id = v.Code,
                    text = v.Name,
                    selected = v.Code == selectedId,
                };
                list.Add(temp);
            }
            return list;
        }


        public List<SelectOption> GetAreasByParent(string parentId)
        {
            var entity = areaDal.GetAreasByParent(parentId);
            List<SelectOption> list = new List<SelectOption>();
            //list.Add(new SelectOption() { text = "==请选择==" });
            foreach (var v in entity)
            {
                var temp = new SelectOption()
                {
                    id = v.Code,
                    text = v.Name
                };
                list.Add(temp);
            }
            return list;
        }

        public List<SelectOption> GetAreasByParent(string parentId, string selectedId)
        {
            var entity = areaDal.GetAreasByParent(parentId);
            List<SelectOption> list = new List<SelectOption>();
            //list.Add(new SelectOption() { text = "==请选择==" });
            foreach (var v in entity)
            {
                var temp = new SelectOption()
                {
                    id = v.Code,
                    text = v.Name,
                    selected = v.Code == selectedId,
                };
                list.Add(temp);
            }
            return list;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Context;
using USP.Models.Entity;
using USP.Utility;

namespace USP.Dal.Impl
{
    public class SysAreaDal : ISysAreaDal
    {
        USPEntities db = new USPEntities();
        public void create(SysArea area)
        {
            try
            {
                db.SysArea.Add(area);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
            }
        }

        public List<SysArea> getAll()
        {
            try
            {
                return db.SysArea.ToList();
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                return new List<SysArea>();
            }
        }
        public List<UP_ShowArea_Result> showPage(int page, int rows, string order, string orderType)
        {
            try
            {
                return db.UP_ShowArea(page, rows, order, orderType).ToList();
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                return new List<UP_ShowArea_Result>();
            }
        }

        public List<UP_ShowProvince_Result> ShowProvince()
        {
            return db.UP_ShowProvince().ToList();
        }

        public List<UP_ShowCity_Result> ShowCity(string provinceCode)
        {
            return db.UP_ShowCity(provinceCode).ToList();
        }

        public List<UP_ShowCounty_Result> ShowCounty(string cityCode)
        {
            return db.UP_ShowCounty(cityCode).ToList();
        }


        public List<Area> GetAreaPageList()
        {
            return db.Area.ToList();
        }

        public List<Area> GetProvinces()
        {
            return db.Area.Where(x => x.Type == 1).ToList();
        }

        public List<Area> GetAreas(string code)
        {
            return db.Area.Where(x => x.Type == 2 && x.Parent == code).ToList();
        }

        public List<Area> GetCountys(string code)
        {
            return db.Area.Where(x => x.Type == 3 && x.Parent == code).ToList();
        }

        public List<Area> GetCommunitys(string code)
        {
            return db.Area.Where(x => x.Type == 4 && x.Parent == code).ToList();
        }

        public List<Area> GetAreasByParent(string parentId)
        {
            return db.Area.Where(x => x.Parent == parentId).ToList();
        }
    }
}
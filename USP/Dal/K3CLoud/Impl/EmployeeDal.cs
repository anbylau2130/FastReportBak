using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Context;
using USP.Models.Entity;
using USP.Utility;

namespace USP.Dal.K3CLoud.Impl
{
    public class EmployeeDal : IEmployeeDal
    {
        K3CloudEntities db = new K3CloudEntities();
        USPEntities usp = new USPEntities();
        public List<HXN_HR_EMPLOYEE> GetEmployees()
        {
            return db.HXN_HR_EMPLOYEE.ToList();
        }

        public HXN_HR_EMPLOYEE_L GetEmployeeNameById(int id)
        {
            return db.HXN_HR_EMPLOYEE_L.Where(x => x.FID == id).First();
        }

        public List<HXN_HR_EMPLOYEE_L> GetEmployeeNames()
        {
            return db.HXN_HR_EMPLOYEE_L.ToList();
        }
        public List<HXN_HR_Tsui_UserInfo_Result> GetUserInfo(string gongHao)
        {
            return db.HXN_HR_Tsui_UserInfo(gongHao).ToList();
        }


        public bool AddUserInfo(UserInfo modelList)
        {
            var result = usp.UserInfo.Add(modelList);
            if (usp.SaveChanges()== 1) { return true; }
            return false;
        }

        public bool DeleteUserInfo(string GongHao)
        {
            try
            {
                using (USPEntities usp = new USPEntities())
                {
                    var models = usp.UserInfo.Where(c => c.员工工号 == GongHao).ToList();
                    usp.UserInfo.RemoveRange(models);
                    usp.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                return false;
            }
        }
    }
}
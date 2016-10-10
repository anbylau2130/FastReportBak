using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Dal.K3CLoud;
using USP.Models.Entity;

namespace USP.Bll.K3CLoud.Impl
{
    public class EmployeeBll:IEmployeeBll
    {
        IEmployeeDal employeeDal;
        public EmployeeBll(IEmployeeDal employeeDal)
        {
            this.employeeDal = employeeDal;
        }

        public HXN_HR_EMPLOYEE_L GetEmployeeNameById(int id)
        {
            return employeeDal.GetEmployeeNameById(id);
        }

        public List<HXN_HR_EMPLOYEE> GetEmployees()
        {
            return employeeDal.GetEmployees();
        }
        public List<HXN_HR_EMPLOYEE_L> GetEmployeeNames()
        {
            return employeeDal.GetEmployeeNames();
        }
        public List<HXN_HR_Tsui_UserInfo_Result> GetUserInfo(string gongHao)
        {
            return employeeDal.GetUserInfo(gongHao);
        }

        public bool AddUserInfo(UserInfo modelList)
        {
            return employeeDal.AddUserInfo(modelList);
        }


        public bool DeleteUserInfo(string GongHao)
        {
            return employeeDal.DeleteUserInfo(GongHao);
        }
    }
}
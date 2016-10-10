using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Models.Entity;

namespace USP.Dal.K3CLoud
{
    public interface IEmployeeDal
    {
        List<HXN_HR_EMPLOYEE> GetEmployees();
        HXN_HR_EMPLOYEE_L GetEmployeeNameById(int id);
        List<HXN_HR_EMPLOYEE_L> GetEmployeeNames();
        List<HXN_HR_Tsui_UserInfo_Result> GetUserInfo(string gongHao);
        bool AddUserInfo(UserInfo modelList);
        bool DeleteUserInfo(string GongHao);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Dal;
using USP.Models.Entity;

namespace USP.Service.Impl
{
    public class SysOperatorService : ISysOperatorService
    {
        ISysOperatorDal sysOperatorDal;
        ISysUserDal operatorDal;
        ISysRoleDal roleDal;
        public SysOperatorService(ISysOperatorDal sysOperatorDal, ISysRoleDal roleDal, ISysUserDal operatorDal)
        {
            this.sysOperatorDal = sysOperatorDal;
            this.roleDal = roleDal;
            this.operatorDal = operatorDal;
        }
        public List<SysOperator> Login(string loginName, string password, string session, string ip)
        {
            return sysOperatorDal.Login(loginName, password, session, ip);
        }
        public List<SysMenu> GetMenu(long @operator)
        {
            return sysOperatorDal.GetMenus(@operator);
        }
        public List<SysPrivilege> GetPrivilege(long @operator)
        {
            return sysOperatorDal.GetPrivileges(@operator);
        }
        public string CheckSso(long @operator, string session)
        {
            return sysOperatorDal.CheckSso(@operator, session);
        }

        public SysCorp GetCorp(long corp)
        {
            return sysOperatorDal.GetCorp(corp);
        }

        public List<SysRole> GetRoles(Int64 corpid)
        {
            return roleDal.getRolesByCrop(corpid);
        }

        public SysOperator GetOperatorbyId(Int64 id)
        {
            return operatorDal.GetOperatorbyId(id);
        }

        public List<SysOperatorStatus> GetOperatorStatus()
        {
            return operatorDal.GetOperatorStatus();
        }
    }
}

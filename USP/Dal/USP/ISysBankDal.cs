using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Dal
{
    public interface ISysBankDal
    {
        List<Bank_Extend> GetBankPage(string strGetFields, string strWhere, string strOrder, int page, int pageSize);
        SysBank GetBankbyName(string name);
        bool SaveBank(string name, string niceName, long? number, string remark, string shortName, string url, long creator, int type);
        int AlterStatus(long bankid, int status, long userid, int type);
        SysBank GetBankbyID(long bankid);
        bool EditBank(long iD, string name, string niceName, long? number, string remark, string shortName, int type, string url,long creator);
    }
}
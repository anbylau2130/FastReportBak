using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Models.Entity;
using USP.Models.POCO;

namespace USP.Bll
{
    public interface ISysBankBll
    {
        DataGrid<Bank_Extend> GetBankGrid(int page, int pagesize, string order, string orderType, string Bankname, long type,string NiceName);
        SysBank GetBankbyName(string name);
        bool SaveBank(BankAddEdit bank);
        int AlterStatus(long bankid, int status, long userid, int type);
        SysBank GetBankbyID(long bankid);
        bool EditBank(BankAddEdit model);
    }
}
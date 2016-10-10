using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Dal;
using USP.Models.POCO;
using USP.Models.Entity;

namespace USP.Bll.Impl
{
    public class SysBankBll: ISysBankBll
    {
        ISysBankDal SysBankDal;
        public SysBankBll(ISysBankDal SysBankDal)
        {
            this.SysBankDal = SysBankDal;
        }

        public DataGrid<Bank_Extend> GetBankGrid(int page, int pagesize, string order, string orderType, string BankName, long type, string NiceName)
        {
            var result = new DataGrid<Bank_Extend>();
            var strWhere = "1=1";
            if (!string.IsNullOrWhiteSpace(BankName))
            {
                strWhere += " and Name like '%" + BankName + "%' ";
            }
            if (!string.IsNullOrWhiteSpace(NiceName))
            {
                strWhere += " and NiceName like '%" + NiceName + "%' ";
            }
            if (type != -1)
            {
                strWhere += " and type = " + type;
            }
            var banks = SysBankDal.GetBankPage("*", strWhere, "ID ASC", page, pagesize);
            result.rows = banks;
            if (result.rows.Count > 0)
            {
                result.total = (long)result.rows[0].RowCnt;
            }
            return result;
        }

        public SysBank GetBankbyName(string name)
        {
            return SysBankDal.GetBankbyName(name);
        }

        public bool SaveBank(BankAddEdit bank)
        {
            return SysBankDal.SaveBank(bank.Name,bank.NiceName,bank.Number,bank.Remark,bank.ShortName,bank.Url,bank.Creator,bank.Type);
        }

        public int AlterStatus(long bankid, int status, long userid, int type)
        {
            return SysBankDal.AlterStatus(bankid, status, userid, type);
        }

        public SysBank GetBankbyID(long bankid)
        {
            return SysBankDal.GetBankbyID(bankid);
        }

        public bool EditBank(BankAddEdit model)
        {
            return SysBankDal.EditBank(model.ID,model.Name,model.NiceName,model.Number,model.Remark,model.ShortName,model.Type,model.Url,model.Creator);
        }
    }
}
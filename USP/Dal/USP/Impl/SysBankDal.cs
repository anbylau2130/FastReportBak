using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USP.Context;
using USP.Models.POCO;
using USP.Utility;
using USP.Models.Entity;

namespace USP.Dal.Impl
{
    public class SysBankDal: ISysBankDal
    {
        USPEntities db = new USPEntities();

        public List<Bank_Extend> GetBankPage(string strGetFields, string strWhere, string strOrder, int page, int pageSize)
        {
            var _tbName = "SysBank";
            return SysPublicFun.GetPage<Bank_Extend>(db, strGetFields, _tbName, strWhere, strOrder, page, pageSize);
        }

        public SysBank GetBankbyName(string name)
        {
            return db.SysBank.FirstOrDefault(x => x.Name == name);
        }

        public bool SaveBank(string name, string niceName, long? number, string remark, string shortName, string url, long creator,int type)
        {
            try
            {
                var obj = db.UP_AddBank(number, name, niceName, shortName,  remark, url, creator,type);
                var id = obj.FirstOrDefault();
                if (id == null) return false;
                return id > 0;
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                return false;
            }
        }

        public int AlterStatus(long bankid, int status, long userid, int type)
        {
            int result = 0;
            try
            {
                var obj = db.SysBank.FirstOrDefault(x => x.ID == bankid);
                if(obj!=null)
                {
                    var bank = db.UP_AlterBankStatus(status, userid, bankid, type);
                    var id = bank.FirstOrDefault();
                    result = 1;
                    return result;
                }
                else
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                LogUtil.Exception("ExceptionLogger", ex);
                return result;
            }

        }

        public SysBank GetBankbyID(long bankid)
        {
            return db.SysBank.FirstOrDefault(x => x.ID == bankid);
        }

        public bool EditBank(long id, string name, string niceName, long? number, string remark, string shortName, int type, string url, long creator)
        {
            try
            {
                var obj = db.UP_EditBank(id,number,name,niceName,shortName,remark,url,creator,type);
                var bank = obj.FirstOrDefault();
                if (bank == null) return false;
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
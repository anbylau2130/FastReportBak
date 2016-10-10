using USP.Models.Entity;

namespace USP.Models.POCO
{
    public class SysPrivilegeExt : SysPrivilege
    {
        public long? RowNo { get; set; }
        public long? RowCnt { get; set; }

        public string MenuName { get; set; }
    }
}
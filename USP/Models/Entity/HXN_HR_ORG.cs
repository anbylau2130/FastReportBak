//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace USP.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    using System.ComponentModel.DataAnnotations;
    [MetadataType(typeof(USP.Models.ViewModel.HXN_HR_ORGMetaData))]
    public partial class HXN_HR_ORG
    {
        public int FID { get; set; }
        public string FNUMBER { get; set; }
        public string FDOCUMENTSTATUS { get; set; }
        public string FFORBIDSTATUS { get; set; }
        public int FMODIFIERID { get; set; }
        public int FCREATORID { get; set; }
        public Nullable<System.DateTime> FCREATEDATE { get; set; }
        public Nullable<System.DateTime> FMODIFYDATE { get; set; }
        public int FCHECKERID { get; set; }
        public Nullable<System.DateTime> FCHECKDATE { get; set; }
        public int FDEPARTMENTID { get; set; }
        public int FPARENTID { get; set; }
        public string FJIDENG { get; set; }
        public int FGROUPID { get; set; }
        public string FTIXI { get; set; }
        public string FMOSHI { get; set; }
    }
}
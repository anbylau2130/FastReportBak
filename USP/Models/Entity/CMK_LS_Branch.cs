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
    [MetadataType(typeof(USP.Models.ViewModel.CMK_LS_BranchMetaData))]
    public partial class CMK_LS_Branch
    {
        public int FID { get; set; }
        public string FFormId { get; set; }
        public string FNUMBER { get; set; }
        public string FDOCUMENTSTATUS { get; set; }
        public string FFORBIDSTATUS { get; set; }
        public int FMODIFIERID { get; set; }
        public int FCREATORID { get; set; }
        public Nullable<System.DateTime> FCREATEDATE { get; set; }
        public Nullable<System.DateTime> FMODIFYDATE { get; set; }
        public int FAPPROVERID { get; set; }
        public Nullable<System.DateTime> FAPPROVEDATE { get; set; }
        public string FCITY { get; set; }
        public string FPROVINCIAL { get; set; }
        public int FORGID { get; set; }
        public string FADDR { get; set; }
        public Nullable<System.DateTime> FOPENDATE { get; set; }
        public Nullable<System.DateTime> FPAYDATE { get; set; }
        public Nullable<System.DateTime> FCHANGDATE { get; set; }
        public Nullable<System.DateTime> FREOPENDATE { get; set; }
        public Nullable<System.DateTime> FCOMPOPENDATE { get; set; }
        public Nullable<System.DateTime> FBEGINHIREDATE { get; set; }
        public Nullable<System.DateTime> FENDHIREDATE { get; set; }
        public Nullable<System.DateTime> FCLOSEDATE { get; set; }
        public string FRANK { get; set; }
        public string FCountry { get; set; }
        public int FSTAFFNUM { get; set; }
        public decimal FAREA { get; set; }
        public decimal FLONGITUDE { get; set; }
        public decimal FLATITUDE { get; set; }
        public string FQUDAO { get; set; }
        public string FDAQU { get; set; }
        public string FORGTYPE { get; set; }
        public string FSHENGQU { get; set; }
        public string FBranchType { get; set; }
        public int FCOUNTRYID { get; set; }
        public int FREGIONID { get; set; }
        public int FCITYID { get; set; }
        public int FCOUNTYID { get; set; }
        public int FSTREETID { get; set; }
        public string FCODE { get; set; }
        public int FPARENTORGID { get; set; }
        public string FISZNBH { get; set; }
    }
}

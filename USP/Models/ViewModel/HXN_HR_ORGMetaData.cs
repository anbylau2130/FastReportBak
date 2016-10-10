//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template made by Louis-Guillaume Morand.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;

namespace USP.Models.ViewModel
{
    
    
    public class HXN_HR_ORGMetaData
    {
        [Required] 
        public virtual int FID
        {
            get;
            set;
        }
    	
        [StringLength(30, ErrorMessage="最多可输入30个字符")]
        [Required] 
        public virtual string FNUMBER
        {
            get;
            set;
        }
    	
        [StringLength(1, ErrorMessage="最多可输入1个字符")]
        [Required] 
        public virtual string FDOCUMENTSTATUS
        {
            get;
            set;
        }
    	
        [StringLength(1, ErrorMessage="最多可输入1个字符")]
        [Required] 
        public virtual string FFORBIDSTATUS
        {
            get;
            set;
        }
        [Required] 
        public virtual int FMODIFIERID
        {
            get;
            set;
        }
        [Required] 
        public virtual int FCREATORID
        {
            get;
            set;
        }
        public virtual Nullable<System.DateTime> FCREATEDATE
        {
            get;
            set;
        }
        public virtual Nullable<System.DateTime> FMODIFYDATE
        {
            get;
            set;
        }
        [Required] 
        public virtual int FCHECKERID
        {
            get;
            set;
        }
        public virtual Nullable<System.DateTime> FCHECKDATE
        {
            get;
            set;
        }
        [Required] 
        public virtual int FDEPARTMENTID
        {
            get;
            set;
        }
        [Required] 
        public virtual int FPARENTID
        {
            get;
            set;
        }
    	
        [StringLength(36, ErrorMessage="最多可输入36个字符")]
        [Required] 
        public virtual string FJIDENG
        {
            get;
            set;
        }
        [Required] 
        public virtual int FGROUPID
        {
            get;
            set;
        }
    	
        [StringLength(36, ErrorMessage="最多可输入36个字符")]
        [Required] 
        public virtual string FTIXI
        {
            get;
            set;
        }
    	
        [StringLength(36, ErrorMessage="最多可输入36个字符")]
        [Required] 
        public virtual string FMOSHI
        {
            get;
            set;
        }
    }
}
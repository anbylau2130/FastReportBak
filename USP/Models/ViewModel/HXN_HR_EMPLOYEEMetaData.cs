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
    
    
    public class HXN_HR_EMPLOYEEMetaData
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
    	
        [StringLength(36, ErrorMessage="最多可输入36个字符")]
        [Required] 
        public virtual string FSEX
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
        public virtual int FGROUPID
        {
            get;
            set;
        }
        public virtual Nullable<System.DateTime> FBIRTHDATE
        {
            get;
            set;
        }
    	
        [StringLength(50, ErrorMessage="最多可输入50个字符")]
        [Required] 
        public virtual string FCARDNUMBER
        {
            get;
            set;
        }
    	
        [StringLength(36, ErrorMessage="最多可输入36个字符")]
        [Required] 
        public virtual string FMARRIAGE
        {
            get;
            set;
        }
    	
        [StringLength(100, ErrorMessage="最多可输入100个字符")]
        [Required] 
        public virtual string FCARDADDRESS
        {
            get;
            set;
        }
    	
        [StringLength(100, ErrorMessage="最多可输入100个字符")]
        [Required] 
        public virtual string FADDRESS
        {
            get;
            set;
        }
    	
        [StringLength(36, ErrorMessage="最多可输入36个字符")]
        [Required] 
        public virtual string FHUKOUTYPE
        {
            get;
            set;
        }
    	
        [StringLength(50, ErrorMessage="最多可输入50个字符")]
        [Required] 
        public virtual string FPHONE
        {
            get;
            set;
        }
    	
        [StringLength(50, ErrorMessage="最多可输入50个字符")]
        [Required] 
        public virtual string FEMAIL
        {
            get;
            set;
        }
    	
        [StringLength(50, ErrorMessage="最多可输入50个字符")]
        [Required] 
        public virtual string FLIANXIREN
        {
            get;
            set;
        }
    	
        [StringLength(50, ErrorMessage="最多可输入50个字符")]
        [Required] 
        public virtual string FLIANXIPHONE
        {
            get;
            set;
        }
    	
        [StringLength(50, ErrorMessage="最多可输入50个字符")]
        [Required] 
        public virtual string FYUANXIAO
        {
            get;
            set;
        }
    	
        [StringLength(50, ErrorMessage="最多可输入50个字符")]
        [Required] 
        public virtual string FZHUANYE
        {
            get;
            set;
        }
    	
        [StringLength(36, ErrorMessage="最多可输入36个字符")]
        [Required] 
        public virtual string FXUELI
        {
            get;
            set;
        }
        public virtual Nullable<System.DateTime> FBIYEDATE
        {
            get;
            set;
        }
    	
        [StringLength(36, ErrorMessage="最多可输入36个字符")]
        [Required] 
        public virtual string FYUANXIAOTYPE
        {
            get;
            set;
        }
    	
        [StringLength(36, ErrorMessage="最多可输入36个字符")]
        [Required] 
        public virtual string FJIAOYUTYPE
        {
            get;
            set;
        }
        public virtual Nullable<System.DateTime> FJOINDATE
        {
            get;
            set;
        }
    	
        [StringLength(36, ErrorMessage="最多可输入36个字符")]
        [Required] 
        public virtual string FYINGPIN
        {
            get;
            set;
        }
    	
        [StringLength(36, ErrorMessage="最多可输入36个字符")]
        [Required] 
        public virtual string FYONGGONGTYPE
        {
            get;
            set;
        }
    	
        [StringLength(36, ErrorMessage="最多可输入36个字符")]
        [Required] 
        public virtual string FNATION
        {
            get;
            set;
        }
    	
        [StringLength(50, ErrorMessage="最多可输入50个字符")]
        [Required] 
        public virtual string FIMAGE
        {
            get;
            set;
        }
        public virtual byte[] F_HXN_IMAGE
        {
            get;
            set;
        }
    	
        [StringLength(36, ErrorMessage="最多可输入36个字符")]
        [Required] 
        public virtual string FFAXIN
        {
            get;
            set;
        }
    	
        [StringLength(50, ErrorMessage="最多可输入50个字符")]
        [Required] 
        public virtual string FWORKCITY
        {
            get;
            set;
        }
    	
        [StringLength(50, ErrorMessage="最多可输入50个字符")]
        [Required] 
        public virtual string FCITY
        {
            get;
            set;
        }
        [Required] 
        public virtual int FGANGWEI
        {
            get;
            set;
        }
    	
        [StringLength(36, ErrorMessage="最多可输入36个字符")]
        [Required] 
        public virtual string FZHIJI
        {
            get;
            set;
        }
    	
        [StringLength(36, ErrorMessage="最多可输入36个字符")]
        [Required] 
        public virtual string FYGSTATE
        {
            get;
            set;
        }
    	
        [StringLength(36, ErrorMessage="最多可输入36个字符")]
        [Required] 
        public virtual string FZHENGZHI
        {
            get;
            set;
        }
    }
}

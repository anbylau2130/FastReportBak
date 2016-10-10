using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace USP.Models.POCO
{
    public class BankAddEdit
    {             
        public long ID { get; set; }

        //[StringLength(50, ErrorMessage = "最多可输入50个字符")]
        [Display(Name = "银行代码")]
        public long? Number { get; set; }

        //[StringLength(50, ErrorMessage = "最多可输入50个字符")]
        [Display(Name = "银行名称")]
        public string Url { get; set; }

        [Display(Name = "银行卡类型")]
        public int Type { get; set; }

        [StringLength(50, ErrorMessage = "最多可输入50个字符")]
        [Required(ErrorMessage = "请输入银行名称")]
        [Display(Name = "银行名称")]
        public string Name { get; set; }

        [StringLength(20, ErrorMessage = "最多可输入20个字符")]
        [Required(ErrorMessage = "请输入简写")]
        [Display(Name = "简写")]
        public string ShortName { get; set; }

        [StringLength(20, ErrorMessage = "最多可输入20个字符")]
        [Required(ErrorMessage = "请输入别称")]
        [Display(Name = "别称")]
        public string NiceName { get; set; }

        [StringLength(20, ErrorMessage = "最多可输入20个字符")]
        [Display(Name = "备注")]
        public string Remark { get; set; }
                
        public long Creator { get; set; }        
    }
}
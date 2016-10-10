using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace USP.Models.POCO
{
    public class OperatorSupplier
    {
        public long ID
        {
            get; set;
        }
        [StringLength(50, ErrorMessage = "最多可输入50个字符")]
        [Required(ErrorMessage = "请输入登录名")]
        [Display(Name = "登录名")]
        public string LoginName { get; set; }

        [StringLength(100, ErrorMessage = "最多可输入100个字符")]
        [Required(ErrorMessage = "请输入姓名")]
        [Display(Name = "姓名")]
        public string RealName { get; set; }


        public long? Supplier
        {
            get; set;
        }

        public long? Orgnization
        {
            get; set;
        }
    }
}
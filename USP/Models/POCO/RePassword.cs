using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace USP.Models.POCO
{
    public class RePassword
    {
        [Required(ErrorMessage = "请输入密码")]
        public  string OldPassword { get; set; }

        [StringLength(100, ErrorMessage = "最多可输入100个字符")]
        [Required(ErrorMessage = "请输入密码")]
        [Display(Name = "密码")]
        public string NewPassword { get; set; }

        [StringLength(100, ErrorMessage = "最多可输入100个字符")]
        [Required(ErrorMessage = "请输入密码")]
        [Display(Name = "确认密码")]
        [Compare("NewPassword", ErrorMessage = "两次输入密码不一致")]
        public string PasswordConfirm { get; set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace USP.Models.POCO
{
    public class OperaterAddEdit
    {
        public long ID { get; set; }

        public long Corp { get; set; }

        [StringLength(50, ErrorMessage = "最多可输入50个字符")]
        [Required(ErrorMessage = "请输入登录名")]
        [Display(Name = "登录名")]
        public string LoginName { get; set; }

        [StringLength(100, ErrorMessage = "最多可输入100个字符")]
        [Required(ErrorMessage = "请输入姓名")]
        [Display(Name = "姓名")]
        public string RealName { get; set; }

        [StringLength(100, ErrorMessage = "最多可输入100个字符")]
        [Required(ErrorMessage = "请输入确认密码")]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "两次输入密码不一致")]
        public string PasswordConfirm { get; set; }

        [StringLength(100, ErrorMessage = "最多可输入100个字符")]
        [Required(ErrorMessage = "请输入密码")]
        [Display(Name = "密码")]
        public string Password { get; set; }
        
        public string Mobile { get; set; }
        
        public string IdCard { get; set; }
        
        public string Email { get; set; }
        
        [Display(Name = "角色")]
        public string Role { get; set; }

        public string Remark { get; set; }

        public long Creator { get; set; }
    }
}
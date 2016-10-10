using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace USP.Models.POCO
{
    public class UspOperator
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

        //[Required(ErrorMessage = "请输入手机号")]
        //[Display(Name = "手机号")]
        //[RegularExpression(@"^(13[0-9]|14[0-9]|15[0-9]|18[0-9])\d{8}$", ErrorMessage = "电话格式有误。")]
        public string Mobile { get; set; }

        //[Required(ErrorMessage = "请输入身份证号码")]
        //[Display(Name = "身份证号码")]
        //[RegularExpression(@"^(\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$", ErrorMessage = "身份证格式有误。")]
        public string IdCard { get; set; }

        //[Required(ErrorMessage = "请输入邮箱")]
        //[EmailAddress(ErrorMessage = "请输入有效邮箱")]
        //[Display(Name = "邮箱")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "请选择角色")]
        [Display(Name = "角色")]
        public string Role { get; set; }

        public long Creator { get; set; }

        //public long Status { get; set; }

    }
}
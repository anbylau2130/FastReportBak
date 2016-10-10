using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace USP.Models.POCO
{
    public class Login
    {
        [Required]
        [Display(Name="用户名")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "密  码")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "验证码")]
        public string Captcha { get; set; }
    }
}
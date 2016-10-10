using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace USP.Models.POCO
{
    public class CorpResult
    {
        [Required]
        [Display(Name = "公司名称")]
        public string CorpName { get; set; }

        [Required]
        [Display(Name = "公司类型")]
        public long CorpType { get; set; }
        [Required]
        [Display(Name = "管理员账号")]
        public string AdminLoginName { get; set; }
        [Required]
        [Display(Name = "管理员密码")]
        public string AdminPassword { get; set; }

        [Required]
        [Display(Name = "确认密码")]
        [System.ComponentModel.DataAnnotations.Compare("AdminPassword",ErrorMessage ="两次密码输入不同")]
        public string AdminPasswordConfirm { get; set; }

        public IEnumerable<SelectListItem> CorpTypeList { get; set; }

    }
}

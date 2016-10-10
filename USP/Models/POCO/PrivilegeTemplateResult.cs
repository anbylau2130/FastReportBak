using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USP.Models.POCO
{
    public class PrivilegeTemplateResult
    {
        [Display(Name = "权限名称")]
        public string Privileges { get; set; }
        [Required]
        [Display(Name = "公司类型")]
        public long CorpType { get; set; }
    }
}

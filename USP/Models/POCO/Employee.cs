using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace USP.Models.POCO
{
    public class Employee
    {
        [Required]
        [Display(Name = "员工工号")]
        public string GongHao
        {
            get; set;
        }
    }
}
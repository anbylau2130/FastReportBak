using System.ComponentModel.DataAnnotations;
namespace USP.Models.POCO
{
    public class PrivilegeAddEdit
    {
        /// <summary>
        /// 权限id
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// 菜单id
        /// </summary>
        [Required(ErrorMessage = "请输入{0}")]
        [Display(Name = "请选择菜单")]
        public long Menu { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        [Required(ErrorMessage = "请输入{0}")]
        [Display(Name = "权限名称")]
        [StringLength(50, ErrorMessage = "最大输入50个字符")]
        public string Name { get; set; }

        [Required(ErrorMessage = "请输入{0}")]
        [Display(Name = "权限链接")]
        [StringLength(100, ErrorMessage = "最大输入100个字符")]
        public string Url { get; set; }

        [Display(Name = "权限说明")]
        [StringLength(120, ErrorMessage = "最大输入120个字符")]
        public string Remark { get; set; }

        public long Creator { get; set; }

        [Display(Name = "Class")]
        [StringLength(100, ErrorMessage = "最多可输入100个字符")]
        [Required(ErrorMessage = "请输入{0}")]
        public virtual string Clazz
        {
            get;
            set;
        }

        [StringLength(100, ErrorMessage = "最多可输入100个字符")]
        [Required(ErrorMessage = "请输入{0}")]
        public virtual string Area
        {
            get;
            set;
        }

        [StringLength(100, ErrorMessage = "最多可输入100个字符")]
        [Required(ErrorMessage = "请输入{0}")]
        public virtual string Controller
        {
            get;
            set;
        }

        [StringLength(100, ErrorMessage = "最多可输入100个字符")]
        [Required(ErrorMessage = "请输入{0}")]
        public virtual string Method
        {
            get;
            set;
        }

        [StringLength(100, ErrorMessage = "最多可输入100个字符")]
        public virtual string Parameter
        {
            get;
            set;
        }
    }
}
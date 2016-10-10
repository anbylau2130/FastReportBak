using System.ComponentModel.DataAnnotations;

namespace USP.Models.POCO
{
    /// <summary>
    /// 角色添加/编辑
    /// </summary>
    public class RoleAddEdit
    {
        /// <summary>
        /// 角色id
        /// </summary>
        public long ID { get; set; }

        [Required(ErrorMessage = "请输入{0}")]
        [Display(Name = "角色名称")]
        [StringLength(50, ErrorMessage = "最大输入50个字符")]
        public string Name { get; set; }

        [Display(Name = "角色说明")]
        [StringLength(120, ErrorMessage = "最大输入120个字符")]
        public string Remark { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        [Display(Name = "权限")]
        [Required(ErrorMessage = "请选择{0}")]
        public string Menus { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public string Privileges { get; set; }

        /// <summary>
        /// 角色类型 true是管理员组 false非
        /// </summary>
        public bool Type { get; set; }

        public long Creator { get; set; }

        public long Corp { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USP.Models.POCO
{
    public  class Corp
    {
        [Required]
        [Display(Name = "唯一标识")]
        public long ID { get; set; }
        [Required]
        [Display(Name = "父公司")]
        public long Parent { get; set; }
        [Display(Name = "公司优先级(0-9)")]
        public byte Priority { get; set; }
        [Display(Name = "公司名称")]
        public string Name { get; set; }
        [Display(Name = "公司简称")]
        public string BriefName { get; set; }
        [Display(Name = "证书名称")]
        public string Certificate { get; set; }
        [Display(Name = "证书号码")]
        public string CertificateNumber { get; set; }
        [Display(Name = "法人代表")]
        public string Ceo { get; set; }
        [Display(Name = "邮编")]
        public string Postcode { get; set; }
        [Display(Name = "传真号")]
        public string Faxcode { get; set; }
        [Display(Name = "联系人")]
        public string Linkman { get; set; }
        [Display(Name = "联系手机")]
        public string Mobile { get; set; }
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "联系QQ")]
        public string Qq { get; set; }
        [Display(Name = "微信")]
        public string Wechat { get; set; }
        [Display(Name = "微博")]
        public string Weibo { get; set; }
        [Display(Name = "虚拟积分")]
        public long VirtualIntegral { get; set; }
        [Display(Name = "真实积分")]
        public long RealIntegral { get; set; }
        [Display(Name = "付费类型")]
        public long FeeType { get; set; }
        [Display(Name = "预付阀值")]
        public decimal PrepayValve { get; set; }
        [Display(Name = "余额")]
        public decimal Balance { get; set; }
        [Display(Name = "冻结余额")]
        public decimal FrozenBalance { get; set; }
        [Display(Name = "在途余额")]
        public decimal IncomingBalance { get; set; }
        [Display(Name = "佣金比例")]
        public decimal Commission { get; set; }
        [Display(Name = "折扣比例")]
        public decimal Discount { get; set; }
        [Display(Name = "省份")]
        public string Province { get; set; }
        [Display(Name = "地区")]
        public string Area { get; set; }
        [Display(Name = "县")]
        public string County { get; set; }
        [Display(Name = "社区")]
        public string Community { get; set; }
        [Display(Name = "地址")]
        public string Address { get; set; }
        [Display(Name = "状态")]
        public long Status { get; set; }
        [Display(Name = "类型")]
        public long Type { get; set; }
        [Display(Name = "级别")]
        public long Grade { get; set; }
        [Display(Name = "行业类型")]
        public long Vocation { get; set; }
        [Display(Name = "预留")]
        public string Reserve { get; set; }
        [Display(Name = "备注")]
        public string Remark { get; set; }
        [Display(Name = "创建人")]
        public long Creator { get; set; }
        [Display(Name = "创建时间")]
        public System.DateTime CreateTime { get; set; }
        [Display(Name = "审核人")]
        public Nullable<long> Auditor { get; set; }
        [Display(Name = "审核时间")]
        public Nullable<System.DateTime> AuditTime { get; set; }
        [Display(Name = "注销人")]
        public Nullable<long> Canceler { get; set; }
        [Display(Name = "注销时间")]
        public Nullable<System.DateTime> CancelTime { get; set; }
        [Display(Name = "公司logo")]
        public string LogoUrl { get; set; }
        [Display(Name = "公司Icon")]
        public string LogoIcon { get; set; }
    }
}

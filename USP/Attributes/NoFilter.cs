using System;
namespace USP.Attributes
{
    /// <summary>
    /// 跳过验证
    /// <creator>zhoushuaijie 2016.02.02</creator>
    /// </summary>
    public class NoFilter : Attribute
    {
        /// <summary>
        /// 跳过过滤器默认true
        /// </summary>
        public bool Flag { get; set; } = true;
    }
}
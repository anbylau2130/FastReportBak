
namespace USP.Models.POCO
{
    using System;
    using System.Collections.Generic;
    
    public partial class Area
    {
        public string Code { get; set; }
        public string Parent { get; set; }
        public string Name { get; set; }
        public string WeatherCode { get; set; }
        public Nullable<decimal> Location_X { get; set; }
        public Nullable<decimal> Location_Y { get; set; }
        public string Reserve { get; set; }
        public string Remark { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public List<TreeNode> children { get; set; }
    }
}

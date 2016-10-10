using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace USP.Models.POCO
{
    public class TreeNode
    {
        public long id { get; set; }
        public string text { get; set; }
        public string state { get; set; }
        public bool @checked { get; set; }
        public object attributes { get; set; }
        public List<TreeNode> children { get; set; }
        public string iconCls
        {
            get;
            set;
        }
    
    }
}
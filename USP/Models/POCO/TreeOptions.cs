using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Ajax.Utilities;

namespace USP.Models.POCO
{
    public class TreeOptions
    {
        /// <summary>
        /// 检索远程数据的URL地址。
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 检索数据的HTTP方法。（POST / GET）
        /// </summary>
        public string method { get; set; }

        /// <summary>
        /// 定义节点在展开或折叠的时候是否显示动画效果。
        /// </summary>
        public bool animate { get; set; }

        /// <summary>
        /// 定义是否在每一个借点之前都显示复选框。
        /// </summary>
        public bool checkbox { get; set; }

        /// <summary>
        /// 定义是否层叠选中状态。
        /// </summary>
        public bool cascadeCheck { get; set; }

        /// <summary>
        /// 定义是否只在末级节点之前显示复选框。
        /// </summary>
        public bool onlyLeafCheck { get; set; }

        /// <summary>
        /// 定义是否显示树控件上的虚线。
        /// </summary>
        public bool lines { get; set; }

        /// <summary>
        /// 定义是否启用拖拽功能。
        /// </summary>
        public bool dnd { get; set; }

        /// <summary>
        /// 节点数据加载。 
        /// </summary>
        public List<TreeNode> data { get; set; }

        /// <summary>
        /// 在请求远程数据的时候增加查询参数并发送到服务器。
        /// </summary>
        public object queryParams { get; set; }

        /// <summary>
        /// 定义如何渲染节点的文本。
        /// </summary>
        public JsFunction formatter { get; set;}

    }

    public class JsFunction
    {
        public string FuncName { get; set; }
        public string[] FuncParams { get; set; }
        public string FuncBody { get; set; }
    }

}

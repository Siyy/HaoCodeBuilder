using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HaoCodeBuilder.Model
{
    public class TreeNodeTag
    {
        /// <summary>
        /// 节点类型
        /// </summary>
        public TreeNodeType Type { get; set; }
        /// <summary>
        /// 节点参数
        /// </summary>
        public object Tag { get; set; }
    }
}

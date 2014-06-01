using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HaoCodeBuilder.Model
{
    public class CodeCreate
    {
        /// <summary>
        /// 数据库服务器ID
        /// </summary>
        public string ServerID { get; set; }

        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DbName { get; set; }

        /// <summary>
        /// 命名空间
        /// </summary>
        public string NameSpace { get; set; }

        /// <summary>
        /// 二级命名空间
        /// </summary>
        public string NameSpace1 { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 类名
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 生成类型
        /// </summary>
        public BuilderType BuilderType { get; set; }

        /// <summary>
        /// 生成方法列表
        /// </summary>
        public List<BuilderMethods> MethodList { get; set; }

        /// <summary>
        /// 类命名空间
        /// </summary>
        public Model.ConfigNameSpaceClass CNSC { get; set; }
    }
}

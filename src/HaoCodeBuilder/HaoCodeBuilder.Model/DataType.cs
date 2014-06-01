using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HaoCodeBuilder.Model
{
    /// <summary>
    /// 数据库类型枚举值
    /// </summary>
    public enum DatabaseType
    {
        /// <summary>
        /// 未设置
        /// </summary>
        Empty,
        SqlServer2000,
        SqlServer2005,
        SqlServer2008,
        Access,
        MySql,
        Sqlite,
        Oracle
    }
    /// <summary>
    /// 树节点类型
    /// </summary>
    public enum TreeNodeType
    { 
        Server,
        DataBase,
        TableNode,
        ViewNode,
        Table,
        View,
        Field
    }
    /// <summary>
    /// 程序生成方式
    /// </summary>
    public enum BuilderType
    { 
        /// <summary>
        /// 常规模式
        /// </summary>
        Default,
        /// <summary>
        /// 工厂模式
        /// </summary>
        Factory
    }
    /// <summary>
    /// 生成方法
    /// </summary>
    public enum BuilderMethods
    { 
        /// <summary>
        /// 增加
        /// </summary>
        Add,
        /// <summary>
        /// 修改
        /// </summary>
        Update,
        /// <summary>
        /// 删除
        /// </summary>
        Delete,
        /// <summary>
        /// 查询所有记录
        /// </summary>
        SelectAll,
        /// <summary>
        /// 查询主键记录
        /// </summary>
        SelectByKey,
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        Exists,
        /// <summary>
        /// 查询记录总数
        /// </summary>
        Count
    }
   
}

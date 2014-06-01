using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaoCodeBuilder.Model;

namespace HaoCodeBuilder.Business
{
    public class Database
    {
        private DatabaseType databaseType = DatabaseType.Empty;
        private IData.IDatabase DatabaseInstance;
        public Database(DatabaseType databaseType)
        {
            this.databaseType = databaseType;
            DatabaseInstance = Factory.Factory.CreateDatabaseInstance(databaseType);
        }
        /// <summary>
        /// 测试数据库连接
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="errMessage">错误信息</param>
        /// <returns></returns>
        public bool TestDatabaseConnnection(string serverID, out string errMessage)
        {
            return DatabaseInstance.TestDatabaseConnnection(serverID, out errMessage);
        }
        /// <summary>
        /// 得到所有数据库
        /// </summary>
        /// <param name="connconnectionString"></param>
        /// <returns></returns>
        public List<string> GetDatabases(string serverID)
        {
            return DatabaseInstance.GetDatabaseList(serverID);
        }
        /// <summary>
        /// 得到数据库所有表
        /// </summary>
        /// <param name="serverID"></param>
        /// <returns></returns>
        public List<Model.Tables> GetTables(string serverID, string dbName)
        {
            return DatabaseInstance.GetTables(serverID, dbName);
        }
        /// <summary>
        /// 得到数据库所有视图
        /// </summary>
        /// <param name="serverID"></param>
        /// <returns></returns>
        public List<Model.Views> GetViews(string serverID, string dbName)
        {
            return DatabaseInstance.GetViews(serverID, dbName);
        }
        /// <summary>
        /// 得到一个表中所有字段
        /// </summary>
        /// <param name="serverID"></param>
        /// <param name="dbName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public List<Model.Fields> GetFields(string serverID, string dbName, string tableName)
        {
            return DatabaseInstance.GetFields(serverID, dbName, tableName);
        }
    }
}

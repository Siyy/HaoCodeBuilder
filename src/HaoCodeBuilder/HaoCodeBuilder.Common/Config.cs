using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaoCodeBuilder.Model;
using System.Xml.Linq;

namespace HaoCodeBuilder.Common
{
    public class Config
    {
        
        /// <summary>
        /// 服务器列表
        /// </summary>
        private static List<Servers> ServerList = new List<Servers>();
        /// <summary>
        /// 添加服务器列表
        /// </summary>
        /// <param name="servers"></param>
        public static void AddServerList(Servers servers)
        {
            var ser = ServerList.Where(p => p.ID == servers.ID);
            if (ser.Count() > 0)
            {
                ServerList.Remove(ser.First());
            }
            ServerList.Add(servers);
        }
        /// <summary>
        /// 获取一个服务器
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Servers GetServer(string id)
        {
            if (id.IsNullOrEmpty()) return null;
            var ser = ServerList.Where(p => p.ID == id.Trim());
            return ser.Count() > 0 ? ser.First() : null;
        }
        /// <summary>
        /// 得到连接字符串
        /// </summary>
        /// <param name="serverID"></param>
        /// <returns></returns>
        public static string GetConnectionString(string serverID, string dbName="")
        {

            var server = GetServer(serverID);
            if (server == null)
            {
                return string.Empty;
            }
            else
            {
                string connString = string.Empty;
                switch (server.Type)
                {
                    case DatabaseType.SqlServer2000:
                    case DatabaseType.SqlServer2005:
                    case DatabaseType.SqlServer2008:
                        connString = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}", server.Server, dbName.IsNullOrEmpty() ? server.DatabaseName.IsNullOrEmpty() ? GetServerDefaultDatabase(server.Type) : server.DatabaseName : dbName, server.UserID, server.Password);
                        break;
                    case DatabaseType.MySql:
                        connString = string.Format("Server={0};{1};Database={2};Uid={3};Pwd={4};", server.Server, server.Port.HasValue && server.Port.Value != -1 ? "Port=" + server.Port.Value : "", dbName.IsNullOrEmpty() ? server.DatabaseName.IsNullOrEmpty() ? GetServerDefaultDatabase(server.Type) : server.DatabaseName : dbName, server.UserID, server.Password);
                        break;
                    case DatabaseType.Sqlite:
                        connString = string.Format("Data Source={0};Pooling=true;FailIfMissing=false;{1}", server.Server, server.Password.IsNullOrEmpty() ? "" : string.Format("Password={0}", server.Password));
                        break;
                }

                return connString;
            }
            
        }

        /// <summary>
        /// 系统默认库设置
        /// </summary>
        private static Dictionary<DatabaseType, string> ServerDefaultDatabase 
        { 
            get 
            {
                Dictionary<DatabaseType, string> dict = new Dictionary<DatabaseType, string>();
                dict.Add(DatabaseType.Access, "");
                dict.Add(DatabaseType.Empty, "");
                dict.Add(DatabaseType.MySql, "mysql");
                dict.Add(DatabaseType.Oracle, "");
                dict.Add(DatabaseType.Sqlite, "");
                dict.Add(DatabaseType.SqlServer2000, "master");
                dict.Add(DatabaseType.SqlServer2005, "master");
                dict.Add(DatabaseType.SqlServer2008, "master");
                return dict;
            } 
        }
        /// <summary>
        /// 得到服务器默认库
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static string GetServerDefaultDatabase(DatabaseType dbType)
        {
            var dict = ServerDefaultDatabase[dbType];
            return dict == null ? "" : dict;
        }
        
    }
}

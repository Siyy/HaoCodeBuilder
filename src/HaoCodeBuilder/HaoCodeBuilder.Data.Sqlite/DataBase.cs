using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;

namespace HaoCodeBuilder.Data.Sqlite
{
    public class DataBase : IData.IDatabase
    {
        /// <summary>
        /// 测试数据库连接
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="errMessage">错误信息</param>
        /// <returns></returns>
        public bool TestDatabaseConnnection(string serverID, out string errMessage)
        {
            SQLiteConnection conn = new SQLiteConnection(Common.Config.GetConnectionString(serverID));
            try
            {
                conn.Open();
                errMessage = string.Empty;
                return true;
            }
            catch (SQLiteException err)
            {
                errMessage = err.Message;
                return false;
            }
            finally
            {
                conn.Dispose();
            }
        }
        /// <summary>
        /// 得到服务器所有数据库
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public List<string> GetDatabaseList(string serverID)
        {
            Model.Servers server = Common.Config.GetServer(serverID);
            List<string> dbList = new List<string>();
            if (server == null)
            {
                return dbList;
            }
            else
            {
                dbList.Add(System.IO.Path.GetFileName(server.Server));
            }
            return dbList;
        }

        /// <summary>
        /// 得到数据库所有表
        /// </summary>
        /// <param name="serverID"></param>
        /// <returns></returns>
        public List<Model.Tables> GetTables(string serverID, string dbName)
        {
            List<Model.Tables> tblList = new List<Model.Tables>();
            using (SQLiteConnection conn = new SQLiteConnection(Common.Config.GetConnectionString(serverID, dbName)))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand("select tbl_name from sqlite_master where type='table' order by name", conn))
                {
                    SQLiteDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        tblList.Add(new Model.Tables() { Name = dr.GetString(0) });
                    }
                    dr.Close();
                    dr.Dispose();
                }
            }
            return tblList;
        }
        /// <summary>
        /// 得到数据库所有视图
        /// </summary>
        /// <param name="serverID"></param>
        /// <returns></returns>
        public List<Model.Views> GetViews(string serverID, string dbName)
        {
            List<Model.Views> viewList = new List<Model.Views>();
            using (SQLiteConnection conn = new SQLiteConnection(Common.Config.GetConnectionString(serverID, dbName)))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand("select tbl_name from sqlite_master where type='view' order by name", conn))
                {
                    SQLiteDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        viewList.Add(new Model.Views() { Name = dr.GetString(0) });
                    }
                    dr.Close();
                    dr.Dispose();
                }
            }
            return viewList;
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
            List<Model.Fields> fieldsList = new List<Model.Fields>();
            using (SQLiteConnection conn = new SQLiteConnection(Common.Config.GetConnectionString(serverID, dbName)))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(string.Format("PRAGMA table_info({0})", tableName), conn))
                {
                    SQLiteDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        fieldsList.Add(new Model.Fields()
                        {
                            Name = dr["name"].ToString(),
                            Type = dr["type"].ToString(),
                            IsNull = "1" != dr["notnull"].ToString(),
                            IsPrimaryKey = "1" == dr["pk"].ToString(),
                            Default = dr["dflt_value"].ToString(),
                            IsIdentity = "1" == dr["pk"].ToString() && "INTEGER" == dr["type"].ToString(),
                            DotNetType = GetFieldType(dr["type"].ToString(), "1" != dr["notnull"].ToString()),
                            DotNetSqlType = GetFieldSqlType(dr["type"].ToString()),
                            Note = "",
                            Length = -1
                        });
                    }
                    dr.Close();
                    dr.Dispose();
                }
            }
            return fieldsList;
        }
       
        
        /// <summary>
        /// 得到字段的常规类型
        /// </summary>
        /// <param name="tName"></param>
        /// <param name="isNull"></param>
        /// <returns></returns>
        private string GetFieldType(string typeName, bool isNull)
        {
            string r = string.Empty;
            if (typeName.IndexOf('(') > 0)
            {
                typeName = typeName.Substring(0, typeName.IndexOf('('));
            }
            switch (typeName.Trim().ToLower())
            {
                case "varchar":
                case "text":
                    r = "string";
                    break;
                case "char":
                    r = "char";
                    break;
                case "boolean":
                    r = isNull ? "bool?" : "bool";
                    break;
                case "integer":
                case "numeric":
                    r = isNull ? "long?" : "long";
                    break;
                case "int":
                    r = isNull ? "int?" : "int";
                    break;
                case "real":
                    r = isNull ? "decimal?" : "decimal";
                    break;
                case "date":
                case "datetime":
                    r = isNull ? "DateTime?" : "DateTime";
                    break;
            }
            return r;
        }

        /// <summary>
        /// 得到字段的SQLiteDbType字符串
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        private string GetFieldSqlType(string typeName)
        {
            if (typeName.IndexOf('(') > 0)
            {
                typeName = typeName.Substring(0, typeName.IndexOf('('));
            }
            string r = string.Empty;
            switch (typeName.Trim().ToLower())
            {
                case "varchar":
                    r = "DbType.AnsiString";
                    break;
                
                case "text":
                    r = "DbType.String";
                    break;
               
                case "char":
                    r = "DbType.String";
                    break;

                case "integer":
                case "numeric":
                    r = "DbType.Int64";
                    break;
               
                case "int":
                    r = "DbType.Int32";
                    break;
                
                case "real":
                    r = "DbType.Decimal";
                    break;
                case "date":
                case "datetime":
                    r = "DbType.DateTime";
                    break;
                
            }
            return r;
        }

        
    }
}

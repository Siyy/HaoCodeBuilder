using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;

namespace HaoCodeBuilder.Data.MySql
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
            MySqlConnection conn = new MySqlConnection(Common.Config.GetConnectionString(serverID));
            try
            {
                conn.Open();
                errMessage = string.Empty;
                return true;
            }
            catch (MySqlException err)
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
            List<string> dbList = new List<string>();
            string connString = Common.Config.GetConnectionString(serverID);
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("show databases", conn))
                {
                    MySqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        dbList.Add(dr.GetString(0));
                    }
                    dr.Close();
                    dr.Dispose();
                }
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
            using (MySqlConnection conn = new MySqlConnection(Common.Config.GetConnectionString(serverID, dbName)))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(string.Format("show full tables from {0} where table_type!='VIEW'", dbName), conn))
                {
                    MySqlDataReader dr = cmd.ExecuteReader();
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
            using (MySqlConnection conn = new MySqlConnection(Common.Config.GetConnectionString(serverID, dbName)))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(string.Format("show full tables from {0} where table_type='VIEW'", dbName), conn))
                {
                    MySqlDataReader dr = cmd.ExecuteReader();
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
            using (MySqlConnection conn = new MySqlConnection(Common.Config.GetConnectionString(serverID, dbName)))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(string.Format("show full fields from {0}", tableName), conn))
                {
                    MySqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        fieldsList.Add(new Model.Fields()
                        {
                            Name = dr[0].ToString(),
                            Type = GetFieldType(dr[1].ToString()),
                            Length = GetFieldLength(dr[1].ToString()),
                            IsNull = "YES" == dr[3].ToString().ToUpper(),
                            IsPrimaryKey = "PRI" == dr[4].ToString().ToUpper(),
                            Default = dr[5].ToString(),
                            IsIdentity = "auto_increment" == dr[6].ToString().ToLower(),
                            DotNetType = GetFieldType(GetFieldType(dr[1].ToString()), "YES" == dr[3].ToString().ToUpper()),
                            DotNetSqlType = GetFieldSqlType(GetFieldType(dr[1].ToString())),
                            Note = dr[8].ToString()
                        });
                    }
                    dr.Close();
                    dr.Dispose();
                }
            }
            return fieldsList;
        }
        /// <summary>
        /// 得到字段类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string GetFieldType(string type)
        {
            if (!type.Contains('('))
            {
                return type;
            }
            return type.Substring(0, type.IndexOf('('));
        }
        /// <summary>
        /// 得到字段长度
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private int GetFieldLength(string type)
        {
            if (!type.Contains('('))
            {
                return -1;
            }
            string len = type.Substring(type.IndexOf('(') + 1).Replace(")", "");
            return len.IsInt() ? len.ToInt() : -1;
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
            switch (typeName.Trim().ToLower())
            {
                case "varchar":
                case "mediumtext":
                case "text":
                case "longtext":
                case "char":
                case "tinytext":
                    r = "string";
                    break;
                case "bit":
                    r = isNull ? "bool?" : "bool";
                    break;
                case "bigint":
                case "mediumint":
                    r = isNull ? "long?" : "long";
                    break;
                case "year":
                case "int":
                case "integer":
                    r = isNull ? "int?" : "int";
                    break;
                case "tinyint":
                    r = isNull ? "byte?" : "byte";
                    break;
                case "smallint":
                    r = isNull ? "short?" : "short";
                    break;
                case "decimal":
                    r = isNull ? "decimal?" : "decimal";
                    break;
                case "float":
                    r = isNull ? "float?" : "float";
                    break;
                case "double":
                    r = isNull ? "double?" : "double";
                    break;
                case "date":
                case "datetime":
                case "timestamp":
                case "time":
                    r = isNull ? "DateTime?" : "DateTime";
                    break;
            }
            return r;
        }

        /// <summary>
        /// 得到字段的MySqlDbType字符串
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        private string GetFieldSqlType(string typeName)
        {
            string r = string.Empty;
            switch (typeName)
            {
                case "varchar":
                    r = "MySqlDbType.VarChar";
                    break;
                case "text":
                    r = "MySqlDbType.Text";
                    break;
                case "longtext":
                    r = "MySqlDbType.LongText";
                    break;
                case "char":
                    r = "MySqlDbType.Char";
                    break;
                case "bigint":
                    r = "MySqlDbType.Int64";
                    break;
                case "bit":
                    r = "MySqlDbType.Bit";
                    break;
                case "int":
                    r = "MySqlDbType.Int32";
                    break;
                case "tinyint":
                    r = "MySqlDbType.Int16";
                    break;
                case "smallint":
                    r = "MySqlDbType.Int16";
                    break;
                case "decimal":
                    r = "MySqlDbType.Decimal";
                    break;
                case "float":
                    r = "MySqlDbType.Float";
                    break;
                case "double":
                    r = "MySqlDbType.Double";
                    break;
                case "datetime":
                    r = "MySqlDbType.DateTime";
                    break;
                case "smalldatetime":
                    r = "MySqlDbType.SmallDateTime";
                    break;
                case "date":
                    r = "MySqlDbType.Date";
                    break;
                case "time":
                    r = "MySqlDbType.Time";
                    break;
                case "year":
                    r = "MySqlDbType.Year";
                    break;
                case "timestamp":
                    r = "MySqlDbType.Timestamp";
                    break;
            }
            return r;
        }

        
    }
}

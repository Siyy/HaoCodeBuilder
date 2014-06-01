using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace HaoCodeBuilder.Data.SqlServer
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
            SqlConnection conn = new SqlConnection(Common.Config.GetConnectionString(serverID));
            try
            {
                conn.Open();
                errMessage = string.Empty;
                return true;
            }
            catch (SqlException err)
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
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("select name from sysdatabases", conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
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
            using (SqlConnection conn = new SqlConnection(Common.Config.GetConnectionString(serverID, dbName)))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT name FROM sysobjects WHERE xtype='u' order by name", conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
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
            using (SqlConnection conn = new SqlConnection(Common.Config.GetConnectionString(serverID, dbName)))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("select name from sys.views order by name", conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
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
            Model.Servers server = Common.Config.GetServer(serverID);
            if (server == null)
            {
                return fieldsList;
            }

            using (SqlConnection conn = new SqlConnection(Common.Config.GetConnectionString(serverID, dbName)))
            {
                conn.Open();
                string sql = server.Type == Model.DatabaseType.SqlServer2000 ? 
                    string.Format(@"SELECT name=a.name,isidentity=case when COLUMNPROPERTY( a.id,a.name,'IsIdentity')=1 then 1 else 0 
                        end,isprimarykey=case when exists(SELECT 1 FROM sysobjects where xtype='PK' and parent_obj=a.id and name 
                        in (SELECT name FROM sysindexes WHERE indid in(SELECT indid FROM sysindexkeys WHERE id = a.id AND colid=a.colid))) 
                        then 1 else 0 end,type=b.name,bbyte=a.length,length=COLUMNPROPERTY(a.id,a.name,'PRECISION'),
                        dec=isnull(COLUMNPROPERTY(a.id,a.name,'Scale'),0),isnull=case when a.isnullable=1 then 1 else 0 end,
                        isdefault=isnull(e.text,''),note=isnull(g.[value],'')FROM syscolumns a left join systypes b on a.xusertype=b.xusertype 
                        inner join sysobjects d on a.id=d.id and d.xtype='U' and d.name<>'dtproperties' left join syscomments e on 
                        a.cdefault=e.id left join sysproperties g on a.id=g.id and a.colid=g.smallid left join sysproperties f on 
                        d.id=f.id and f.smallid=0 where d.name='{0}' order by a.id,a.colorder", tableName) :
                        string.Format(@"select a.name as f_name,b.name as t_name,[length],a.isnullable as is_null from 
                        sys.syscolumns a inner join sys.types b on b.user_type_id=a.xtype where object_id('{0}')=id order by a.colid", tableName);
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (server.Type != Model.DatabaseType.SqlServer2000)
                    {
                        while (dr.Read())
                        {
                            fieldsList.Add(new Model.Fields()
                            {
                                Name = dr.GetString(0),
                                Type = dr.GetString(1),
                                Length = GetFieldLength(dr.GetString(1), dr.GetInt16(2)),
                                IsNull = 1 == dr.GetInt32(3),
                                IsPrimaryKey = IsPrimaryKey(serverID, dbName, tableName, dr.GetString(0)),
                                IsIdentity = IsIdentity(serverID, dbName, tableName, dr.GetString(0)),
                                DotNetType = GetFieldType(dr.GetString(1), 1 == dr.GetInt32(3)),
                                DotNetSqlType = GetFieldSqlType(dr.GetString(1)),
                                Note = GetFieldNote(serverID, dbName, tableName, dr.GetString(0))
                            });
                        }
                    }
                    else
                    {
                        while (dr.Read())
                        {
                            fieldsList.Add(new Model.Fields()
                            {
                                Name = dr["name"].ToString(),
                                Type = dr["type"].ToString(),
                                Length = GetFieldLength(dr["type"].ToString(), dr["length"].ToString().ToInt()),
                                IsNull = "1" == dr["isnull"].ToString(),
                                IsPrimaryKey = "1" == dr["isprimarykey"].ToString(),
                                IsIdentity = "1" == dr["isidentity"].ToString(),
                                DotNetType = GetFieldType(dr["type"].ToString(), "1" == dr["isnull"].ToString()),
                                DotNetSqlType = GetFieldSqlType(dr["type"].ToString()),
                                Note = dr["note"].ToString()
                            });
                        }
                    }
                    dr.Close();
                    dr.Dispose();
                }
            }
            return fieldsList;
        }

        /// <summary>
        /// 判断一个表的某列是否为主键
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="filedName"></param>
        /// <returns></returns>
        private bool IsPrimaryKey(string serverID, string dbName, string tableName, string fieldName)
        {
            using (SqlConnection conn = new SqlConnection(Common.Config.GetConnectionString(serverID, dbName)))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "sp_pkeys";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@table_name", tableName));
                    using (SqlDataAdapter dap = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        dap.Fill(dt);
                        return dt.Select("COLUMN_NAME='" + fieldName + "'").Length > 0;
                    }
                }
            }
        }
        /// <summary>
        /// 判断一个表的某列是否为自增列
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="filedName"></param>
        /// <returns></returns>
        private bool IsIdentity(string serverID, string dbName, string tableName, string fieldName)
        {
            using (SqlConnection conn = new SqlConnection(Common.Config.GetConnectionString(serverID, dbName)))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "select COLUMNPROPERTY(object_id('" + tableName + "'),'" + fieldName + "','IsIdentity')";
                    return "1" == cmd.ExecuteScalar().ToString();
                }
            }
        }
        /// <summary>
        /// 得到一个字段的备注说明
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="filedName"></param>
        /// <returns></returns>
        private string GetFieldNote(string serverID, string dbName, string tableName, string fieldName)
        {
            using (SqlConnection conn = new SqlConnection(Common.Config.GetConnectionString(serverID, dbName)))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"select value from sys.extended_properties a 
                        left join sys.syscolumns b on a.major_id=b.id and a.minor_id=b.colid 
                        where a.name='MS_Description' and object_id('" + tableName + "')=a.major_id and b.name='" + fieldName + "'";
                    object obj = cmd.ExecuteScalar();
                    return obj == null ? string.Empty : obj.ToString();
                }
            }
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
                case "nvarchar":
                case "text":
                case "ntext":
                case "char":
                case "nchar":
                case "xml":
                    r = "string";
                    break;
                case "uniqueidentifier":
                    r = isNull ? "Guid?" : "Guid";
                    break;
                case "bit":
                    r = isNull ? "bool?" : "bool";
                    break;
                case "bigint":
                    r = isNull ? "long?" : "long";
                    break;
                case "int":
                    r = isNull ? "int?" : "int";
                    break;
                case "tinyint":
                    r = isNull ? "byte?" : "byte";
                    break;
                case "smallint":
                    r = isNull ? "short?" : "short";
                    break;
                case "smallmoney":
                case "decimal":
                case "numeric":
                case "money":
                    r = isNull ? "decimal?" : "decimal";
                    break;
                case "real":
                    r = isNull ? "float?" : "float";
                    break;
                case "float":
                    r = isNull ? "double?" : "double";
                    break;
                case "date":
                case "datetime":
                case "datetime2":
                case "smalldatetime":
                case "datetimeoffset":
                    r = isNull ? "DateTime?" : "DateTime";
                    break;
            }
            return r;
        }

        /// <summary>
        /// 得到字段的SqlDbType字符串
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        private string GetFieldSqlType(string typeName)
        {
            string r = string.Empty;
            switch (typeName)
            {
                case "varchar":
                    r = "SqlDbType.VarChar";
                    break;
                case "nvarchar":
                    r = "SqlDbType.NVarChar";
                    break;
                case "text":
                    r = "SqlDbType.Text";
                    break;
                case "ntext":
                    r = "SqlDbType.NText";
                    break;
                case "char":
                    r = "SqlDbType.Char";
                    break;
                case "nchar":
                    r = "SqlDbType.VarChar";
                    break;
                case "uniqueidentifier":
                    r = "SqlDbType.UniqueIdentifier";
                    break;
                case "bigint":
                    r = "SqlDbType.BigInt";
                    break;
                case "bit":
                    r = "SqlDbType.Bit";
                    break;
                case "int":
                    r = "SqlDbType.Int";
                    break;
                case "tinyint":
                    r = "SqlDbType.TinyInt";
                    break;
                case "smallint":
                    r = "SqlDbType.SmallInt";
                    break;
                case "smallmoney":
                    r = "SqlDbType.SmallMoney";
                    break;
                case "numeric":
                case "decimal":
                    r = "SqlDbType.Decimal";
                    break;
                case "float":
                    r = "SqlDbType.Float";
                    break;
                case "money":
                    r = "SqlDbType.Money";
                    break;
                case "real":
                    r = "SqlDbType.Real";
                    break;
                case "datetime":
                    r = "SqlDbType.DateTime";
                    break;
                case "datetime2":
                    r = "SqlDbType.DateTime2";
                    break;
                case "smalldatetime":
                    r = "SqlDbType.SmallDateTime";
                    break;
                case "date":
                    r = "SqlDbType.Date";
                    break;
                case "datetimeoffset":
                    r = "SqlDbType.DateTimeOffset";
                    break;
            }
            return r;
        }

        /// <summary>
        /// 得到字段的参数长度
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private int GetFieldLength(string typeName, int length)
        {
            int r = -1;
            switch (typeName)
            {
                case "varchar":
                    r = length == -1 ? -1 : length;
                    break;
                case "nvarchar":
                    r = length == -1 ? -1 : length;
                    break;
                case "numeric":
                case "decimal":
                case "datetime":
                case "smalldatetime":
                    r = length;
                    break;
            }
            return r;
        }
    }
}

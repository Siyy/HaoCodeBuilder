using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HaoCodeBuilder.Business
{
    internal class Builder_Data
    {
        private IData.ICreateCode createInstance;
        private IData.IDatabase databaseInstance;
        private Import import;
        public Builder_Data(Model.DatabaseType dbType)
        {
            this.createInstance = Factory.Factory.CreateCreateCodeInstance(dbType);
            this.databaseInstance = Factory.Factory.CreateDatabaseInstance(dbType);
            this.import = new Import(dbType);
        }
        /// <summary>
        /// 得到数据层代码
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetDataClass(Model.CodeCreate param)
        {
            Model.Servers server = Common.Config.GetServer(param.ServerID);
            if (server == null)
            {
                return string.Empty;
            }
            List<Model.Fields> fields = databaseInstance.GetFields(server.ID, param.DbName, param.TableName);
            StringBuilder data = new StringBuilder(import.GetImport_Data());

            data.Append("namespace " + param.NameSpace + (param.NameSpace.IsNullOrEmpty()?"": ".") + param.CNSC.Data + (param.NameSpace1.IsNullOrEmpty() ? "" : "." + param.NameSpace1) + "\r\n");
            data.Append("{\r\n");
			data.Append("\tpublic partial class " + param.ClassName + (param.BuilderType == Model.BuilderType.Factory ? " : " + param.NameSpace + (param.NameSpace.IsNullOrEmpty() ? "" : ".") + param.CNSC.Interface + (param.NameSpace1.IsNullOrEmpty() ? "" : "." + param.NameSpace1) + ".I" + param.ClassName : "") + "\r\n");
            data.Append("\t{\r\n");
            data.Append("\t\tprivate DBHelper dbHelper = new DBHelper();\r\n");
            data.Append("\t\t/// <summary>\r\n");
            data.Append("\t\t/// 构造函数\r\n");
            data.Append("\t\t/// </summary>\r\n");
            data.Append("\t\tpublic " + param.ClassName + "()\r\n");
            data.Append("\t\t{\r\n");
            data.Append("\t\t}\r\n");
            

            //新增记录
            if (param.MethodList.Contains(Model.BuilderMethods.Add))
            {
                data.Append(GetAddMethod(fields, param));
            }

            //更新记录
            if (param.MethodList.Contains(Model.BuilderMethods.Update) && fields.Where(p => p.IsPrimaryKey).Count() > 0)
            {
                data.Append(GetUpdateMethod(fields, param));
            }

            //删除记录
            if (param.MethodList.Contains(Model.BuilderMethods.Delete) && fields.Where(p => p.IsPrimaryKey).Count() > 0)
            {
                data.Append(GetDeleteMethod(fields, param));
            }

            //转换List
            data.Append(GetConvertDataReaderToListMethod(fields, param));

            //查询所有记录
            if (param.MethodList.Contains(Model.BuilderMethods.SelectAll))
            {
                data.Append(GetAllMethod(fields, param));
            }

            //查询记录数
            if (param.MethodList.Contains(Model.BuilderMethods.Count))
            {
                data.Append(GetCountMethod(fields, param));
            }

            //查询主键记录
            if (param.MethodList.Contains(Model.BuilderMethods.SelectByKey) && fields.Where(p => p.IsPrimaryKey).Count() > 0)
            {
                data.Append(GetByKeyMethod(fields, param));
            }

            data.Append("\t}\r\n");
            data.Append("}");
            return data.ToString();
        }


        /// <summary>
        /// 新增记录方法
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private string GetAddMethod(List<Model.Fields> fields, Model.CodeCreate param)
        {
            //自增列
            var Identitys = fields.Where(p => p.IsIdentity);
            var NotIdeneitys = fields.Where(p => !p.IsIdentity);
            bool HasIdentity = Identitys.Count() > 0;

            //主键
            var Primarykeys = fields.Where(p => p.IsPrimaryKey);
            var NotPrimarykeys = fields.Where(p => !p.IsPrimaryKey);
            bool HasPrimarykey = Primarykeys.Count() > 0;

            StringBuilder data = new StringBuilder();
            data.Append("\t\t/// <summary>\r\n");
            data.Append("\t\t/// 添加记录\r\n");
            data.Append("\t\t/// </summary>\r\n");
            data.Append("\t\t/// <param name=\"model\">" + param.NameSpace + (param.NameSpace.IsNullOrEmpty()?"": ".") + param.CNSC.Model + (param.NameSpace1.IsNullOrEmpty() ? "" : "." + param.NameSpace1) + "." + param.ClassName + "实体类</param>\r\n");
            data.Append("\t\t/// <returns>" + (HasIdentity ? "新增记录的ID" : "操作所影响的行数") + "</returns>\r\n");
            data.Append("\t\tpublic " + (HasIdentity ? Identitys.First().DotNetType : "int") + " Add(" + param.NameSpace + (param.NameSpace.IsNullOrEmpty()?"": ".") + param.CNSC.Model + (param.NameSpace1.IsNullOrEmpty() ? "" : "." + param.NameSpace1) + "." + param.ClassName + " model)\r\n");
            data.Append("\t\t{\r\n");

            data.Append("\t\t\tstring sql = @\"INSERT INTO " + param.TableName);
            data.Append("\r\n\t\t\t\t(");
            foreach (var field in NotIdeneitys)
            {
                data.Append(field.Name);
                data.Append(field.Name != NotIdeneitys.Last().Name ? "," : "");
            }
            data.Append(") \r\n");
            data.Append("\t\t\t\tVALUES(");
            foreach (var field in NotIdeneitys)
            {
                data.Append(createInstance.GetParamsName(field.Name));
                data.Append(field.Name != NotIdeneitys.Last().Name ? "," : "");
            }
            data.Append(")" + (HasIdentity ? ";\r\n\t\t\t\tSELECT " + createInstance.GetIdentityMethod() + ";" : "") + "\";\r\n");
            data.Append("\t\t\t" + createInstance.GetParamsType() + "[] parameters = new " + createInstance.GetParamsType() + "[]{\r\n");
            foreach (var field in NotIdeneitys)
            {
                
                if (field.IsNull)
                {
                    data.Append("\t\t\t\tmodel." + field.Name + " == null ? ");
                    data.Append("new " + createInstance.GetParamsType() + "(\"" + createInstance.GetParamsName1(field.Name) + "\", " + field.DotNetSqlType + (field.Length != -1 ? ", " + field.Length.ToString() : ", -1") + ") { Value = DBNull.Value } : ");
                    data.Append("new " + createInstance.GetParamsType() + "(\"" + createInstance.GetParamsName1(field.Name) + "\", " + field.DotNetSqlType + (field.Length != -1 ? ", " + field.Length.ToString() : ", -1") + ") { Value = model." + field.Name + " }");
                }
                else
                {
                    data.Append("\t\t\t\tnew " + createInstance.GetParamsType() + "(\"" + createInstance.GetParamsName1(field.Name) + "\", " + field.DotNetSqlType + (field.Length != -1 ? ", " + field.Length.ToString() : ", -1") + ")");
                    data.Append("{ Value = model." + field.Name + " }");
                }
                data.Append(field.Name != NotIdeneitys.Last().Name ? "," : "");
                data.Append("\r\n");
            }

            data.Append("\t\t\t};\r\n");
            //data.Append("\t\t\tDBHelper dbHelper = new DBHelper();\r\n");
            if (HasIdentity)
            {
                data.Append("\t\t\t" + Identitys.First().DotNetType+" maxID;\r\n");
                data.Append("\t\t\treturn " + Identitys.First().DotNetType + ".TryParse(dbHelper.ExecuteScalar(sql, parameters),out maxID) ? maxID : -1;\r\n");
            }
            else
            {
                data.Append("\t\t\treturn dbHelper.Execute(sql, parameters);\r\n");
            }
            data.Append("\t\t}\r\n");

            return data.ToString();
        }

        /// <summary>
        /// 更新记录方法
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private string GetUpdateMethod(List<Model.Fields> fields, Model.CodeCreate param)
        {
            //自增列
            var Identitys = fields.Where(p => p.IsIdentity);
            var NotIdeneitys = fields.Where(p => !p.IsIdentity);
            bool HasIdentity = Identitys.Count() > 0;

            //主键
            var Primarykeys = fields.Where(p => p.IsPrimaryKey);
            var NotPrimarykeys = fields.Where(p => !p.IsPrimaryKey);
            bool HasPrimarykey = Primarykeys.Count() > 0;

            StringBuilder data = new StringBuilder();
            data.Append("\t\t/// <summary>\r\n");
            data.Append("\t\t/// 更新记录\r\n");
            data.Append("\t\t/// </summary>\r\n");
            data.Append("\t\t/// <param name=\"model\">" + param.NameSpace + (param.NameSpace.IsNullOrEmpty()?"": ".") + param.CNSC.Model + (param.NameSpace1.IsNullOrEmpty() ? "" : "." + param.NameSpace1) + "." + param.ClassName + "实体类</param>\r\n");
            data.Append("\t\tpublic int Update(" + param.NameSpace + (param.NameSpace.IsNullOrEmpty()?"": ".") + param.CNSC.Model + (param.NameSpace1.IsNullOrEmpty() ? "" : "." + param.NameSpace1) + "." + param.ClassName + " model)\r\n");
            data.Append("\t\t{\r\n");
            data.Append("\t\t\tstring sql = @\"UPDATE " + param.TableName + " SET \r\n\t\t\t\t");

            foreach (var field in NotPrimarykeys)
            {
                data.Append(field.Name + "=" + createInstance.GetParamsName(field.Name));
                data.Append(field.Name != NotPrimarykeys.Last().Name ? "," : "");
            }
            data.Append("\r\n\t\t\t\tWHERE ");

            foreach (var field in Primarykeys)
            {
                if (field.IsPrimaryKey)
                {
                    data.Append(field.Name + "=" + createInstance.GetParamsName(field.Name));
                    data.Append(field.Name != Primarykeys.Last().Name ? " and " : "");
                }
            }
            data.Append("\";\r\n");
            data.Append("\t\t\t" + createInstance.GetParamsType() + "[] parameters = new " + createInstance.GetParamsType() + "[]{\r\n");
            foreach (var field in NotPrimarykeys)
            {
                if (field.IsNull)
                {
                    data.Append("\t\t\t\tmodel." + field.Name + " == null ? ");
                    data.Append("new " + createInstance.GetParamsType() + "(\"" + createInstance.GetParamsName1(field.Name) + "\", " + field.DotNetSqlType + (field.Length != -1 ? ", " + field.Length.ToString() : ", -1") + ") { Value = DBNull.Value } : ");
                    data.Append("new " + createInstance.GetParamsType() + "(\"" + createInstance.GetParamsName1(field.Name) + "\", " + field.DotNetSqlType + (field.Length != -1 ? ", " + field.Length.ToString() : ", -1") + ") { Value = model." + field.Name + " }");
                }
                else
                {
                    data.Append("\t\t\t\tnew " + createInstance.GetParamsType() + "(\"" + createInstance.GetParamsName1(field.Name) + "\", " + field.DotNetSqlType + (field.Length != -1 ? ", " + field.Length.ToString() : ", -1") + ")");
                    data.Append("{ Value = model." + field.Name + " }");
                }
                data.Append(",");
                data.Append("\r\n");
            }
            foreach (var field in Primarykeys)
            {
                data.Append("\t\t\t\tnew " + createInstance.GetParamsType() + "(\"" + createInstance.GetParamsName1(field.Name) + "\", " + field.DotNetSqlType + (field.Length != -1 ? ", " + field.Length.ToString() : ", -1") + ")");
                data.Append("{ Value = model." + field.Name + " }");
                data.Append(field.Name != Primarykeys.Last().Name ? "," : "");
                data.Append("\r\n");
            }
            data.Append("\t\t\t};\r\n");
            //data.Append("\t\t\tDBHelper dbHelper = new DBHelper();\r\n");
            data.Append("\t\t\treturn dbHelper.Execute(sql, parameters);\r\n");
            data.Append("\t\t}\r\n");

            return data.ToString();
        }
        /// <summary>
        /// 删除记录方法
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private string GetDeleteMethod(List<Model.Fields> fields, Model.CodeCreate param)
        {
            //自增列
            var Identitys = fields.Where(p => p.IsIdentity);
            var NotIdeneitys = fields.Where(p => !p.IsIdentity);
            bool HasIdentity = Identitys.Count() > 0;

            //主键
            var Primarykeys = fields.Where(p => p.IsPrimaryKey);
            var NotPrimarykeys = fields.Where(p => !p.IsPrimaryKey);
            bool HasPrimarykey = Primarykeys.Count() > 0;

            StringBuilder data = new StringBuilder();
            data.Append("\t\t/// <summary>\r\n");
            data.Append("\t\t/// 删除记录\r\n");
            data.Append("\t\t/// </summary>\r\n");
            data.Append("\t\tpublic int Delete(");
            foreach (var field in Primarykeys)
            {
                data.Append(field.DotNetType + " " + field.Name.ToLower());
                data.Append(field.Name != Primarykeys.Last().Name ? ", " : "");
            }
            data.Append(")\r\n");
            data.Append("\t\t{\r\n");
            data.Append("\t\t\tstring sql = \"DELETE FROM " + param.TableName + " WHERE ");
            foreach (var field in Primarykeys)
            {
                data.Append(field.Name + "=" + createInstance.GetParamsName(field.Name));
                data.Append(field.Name != Primarykeys.Last().Name ? " AND " : "");
            }
            data.Append("\";\r\n");
            data.Append("\t\t\t" + createInstance.GetParamsType() + "[] parameters = new " + createInstance.GetParamsType() + "[]{\r\n");
            foreach (var field in Primarykeys)
            {
                data.Append("\t\t\t\tnew " + createInstance.GetParamsType() + "(\"" + createInstance.GetParamsName1(field.Name) + "\", " + field.DotNetSqlType + (field.Length != -1 ? ", " + field.Length.ToString() : "") + "){ Value = " + field.Name.ToLower() + " }");
                data.Append(field.Name != Primarykeys.Last().Name ? "," : "");
                data.Append("\r\n");
            }
            data.Append("\t\t\t};\r\n");
            //data.Append("\t\t\tDBHelper dbHelper = new DBHelper();\r\n");
            data.Append("\t\t\treturn dbHelper.Execute(sql, parameters);\r\n");
            data.Append("\t\t}\r\n");

            return data.ToString();
        }

        /// <summary>
        /// 将DataReader转换为List方法
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private string GetConvertDataReaderToListMethod(List<Model.Fields> fields, Model.CodeCreate param)
        {
            //自增列
            var Identitys = fields.Where(p => p.IsIdentity);
            var NotIdeneitys = fields.Where(p => !p.IsIdentity);
            bool HasIdentity = Identitys.Count() > 0;

            //主键
            var Primarykeys = fields.Where(p => p.IsPrimaryKey);
            var NotPrimarykeys = fields.Where(p => !p.IsPrimaryKey);
            bool HasPrimarykey = Primarykeys.Count() > 0;

            StringBuilder data = new StringBuilder();
            data.Append("\t\t/// <summary>\r\n");
            data.Append("\t\t/// 将DataRedar转换为List\r\n");
            data.Append("\t\t/// </summary>\r\n");
            data.Append("\t\tprivate List<" + param.NameSpace + (param.NameSpace.IsNullOrEmpty()?"": ".") + param.CNSC.Model + (param.NameSpace1.IsNullOrEmpty() ? "" : "." + param.NameSpace1) + "." + param.ClassName + "> DataReaderToList(" + createInstance.GetDataReaderType() + " dataReader)\r\n");
            data.Append("\t\t{\r\n");
            data.Append("\t\t\tList<" + param.NameSpace + (param.NameSpace.IsNullOrEmpty()?"": ".") + param.CNSC.Model + (param.NameSpace1.IsNullOrEmpty() ? "" : "." + param.NameSpace1) + "." + param.ClassName + "> List = new List<" + param.NameSpace + (param.NameSpace.IsNullOrEmpty()?"": ".") + param.CNSC.Model + (param.NameSpace1.IsNullOrEmpty() ? "" : "." + param.NameSpace1) + "." + param.ClassName + ">();\r\n");
            data.Append("\t\t\t" + param.NameSpace + (param.NameSpace.IsNullOrEmpty()?"": ".") + param.CNSC.Model + (param.NameSpace1.IsNullOrEmpty() ? "" : "." + param.NameSpace1) + "." + param.ClassName + " model = null;\r\n");
            data.Append("\t\t\twhile(dataReader.Read())\r\n");
            data.Append("\t\t\t{\r\n");

            data.Append("\t\t\t\tmodel = new " + param.NameSpace + (param.NameSpace.IsNullOrEmpty()?"": ".") + param.CNSC.Model + (param.NameSpace1.IsNullOrEmpty() ? "" : "." + param.NameSpace1) + "." + param.ClassName + "();\r\n");
            int i = 0;
            string nullString = string.Empty;
            foreach (var field in fields)
            {
                nullString = field.IsNull ? "\t\t\t\tif (!dataReader.IsDBNull(" + i + "))\r\n\t\t\t\t\t" : "\t\t\t\t";
                switch (field.DotNetType.Replace("?","").ToLower())
                {
                    case "string":
                        data.Append(nullString + "model." + field.Name + " = dataReader.GetString(" + i.ToString() + ");\r\n");
                        break;
                    case "guid":
                        data.Append(nullString + "model." + field.Name + " = dataReader.GetGuid(" + i.ToString() + ");\r\n");
                        break;
                    case "long":
                        data.Append(nullString + "model." + field.Name + " = dataReader.GetInt64(" + i.ToString() + ");\r\n");
                        break;
                    case "int":
                        data.Append(nullString + "model." + field.Name + " = dataReader.GetInt32(" + i.ToString() + ");\r\n");
                        break;
                    case "short":
                        data.Append(nullString + "model." + field.Name + " = dataReader.GetInt16(" + i.ToString() + ");\r\n");
                        break;
                    case "byte":
                        data.Append(nullString + "model." + field.Name + " = dataReader.GetInt16(" + i.ToString() + ");\r\n");
                        break;
                    case "decimal":
                        data.Append(nullString + "model." + field.Name + " = dataReader.GetDecimal(" + i.ToString() + ");\r\n");
                        break;
                    case "float":
                        data.Append(nullString + "\tmodel." + field.Name + " = dataReader.GetFloat(" + i.ToString() + ");\r\n");
                        break;
                    case "double":
                        data.Append(nullString + "\tmodel." + field.Name + " = dataReader.GetDouble(" + i.ToString() + ");\r\n");
                        break;
                    case "datetime":
                        data.Append(nullString + "model." + field.Name + " = dataReader.GetDateTime(" + i.ToString() + ");\r\n");
                        break;
                    case "bool":
                        data.Append(nullString + "model." + field.Name + " = dataReader.GetBoolean(" + i.ToString() + ");\r\n");
                        break;
                }
                i++;
            }
            data.Append("\t\t\t\tList.Add(model);\r\n");
            data.Append("\t\t\t}\r\n");
            data.Append("\t\t\treturn List;\r\n");
            data.Append("\t\t}\r\n");

            return data.ToString();
        }

        /// <summary>
        /// 查询所有记录方法
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private string GetAllMethod(List<Model.Fields> fields, Model.CodeCreate param)
        {
            StringBuilder data = new StringBuilder();
            data.Append("\t\t/// <summary>\r\n");
            data.Append("\t\t/// 查询所有记录\r\n");
            data.Append("\t\t/// </summary>\r\n");
            data.Append("\t\tpublic List<" + param.NameSpace + (param.NameSpace.IsNullOrEmpty()?"": ".") + param.CNSC.Model + (param.NameSpace1.IsNullOrEmpty() ? "" : "." + param.NameSpace1) + "." + param.ClassName + "> GetAll()\r\n");
            data.Append("\t\t{\r\n");
            data.Append("\t\t\tstring sql = \"SELECT * FROM " + param.TableName + "\";\r\n");
            //data.Append("\t\t\tDBHelper dbHelper = new DBHelper();\r\n");
            data.Append("\t\t\t"+createInstance.GetDataReaderType()+" dataReader = dbHelper.GetDataReader(sql);\r\n");
            data.Append("\t\t\tList<" + param.NameSpace + (param.NameSpace.IsNullOrEmpty()?"": ".") + param.CNSC.Model + (param.NameSpace1.IsNullOrEmpty() ? "" : "." + param.NameSpace1) + "." + param.ClassName + "> List = DataReaderToList(dataReader);\r\n");
            data.Append("\t\t\tdataReader.Close();\r\n");
            //data.Append("\t\t\tdbHelper.Dispose();\r\n");
            data.Append("\t\t\treturn List;\r\n");
            data.Append("\t\t}\r\n");

            return data.ToString();
        }

        /// <summary>
        /// 查询主键记录方法
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private string GetByKeyMethod(List<Model.Fields> fields, Model.CodeCreate param)
        {
            //自增列
            var Identitys = fields.Where(p => p.IsIdentity);
            var NotIdeneitys = fields.Where(p => !p.IsIdentity);
            bool HasIdentity = Identitys.Count() > 0;

            //主键
            var Primarykeys = fields.Where(p => p.IsPrimaryKey);
            var NotPrimarykeys = fields.Where(p => !p.IsPrimaryKey);
            bool HasPrimarykey = Primarykeys.Count() > 0;

            StringBuilder data = new StringBuilder();
            data.Append("\t\t/// <summary>\r\n");
            data.Append("\t\t/// 根据主键查询一条记录\r\n");
            data.Append("\t\t/// </summary>\r\n");
            data.Append("\t\tpublic " + param.NameSpace + (param.NameSpace.IsNullOrEmpty()?"": ".") + param.CNSC.Model + (param.NameSpace1.IsNullOrEmpty() ? "" : "." + param.NameSpace1) + "." + param.ClassName + " Get(");
            foreach (var field in Primarykeys)
            {
                data.Append(field.DotNetType + " " + field.Name.ToLower());
                data.Append(field.Name != Primarykeys.Last().Name ? ", " : "");
            }
            data.Append(")\r\n");
            data.Append("\t\t{\r\n");
            data.Append("\t\t\tstring sql = \"SELECT * FROM " + param.TableName + " WHERE ");
            foreach (var field in Primarykeys)
            {
                data.Append(field.Name + "=" + createInstance.GetParamsName(field.Name));
                data.Append(field.Name != Primarykeys.Last().Name ? " AND " : "");
            }
            data.Append("\";\r\n");
            data.Append("\t\t\t" + createInstance.GetParamsType() + "[] parameters = new " + createInstance.GetParamsType() + "[]{\r\n");
            foreach (var field in Primarykeys)
            {
                data.Append("\t\t\t\tnew " + createInstance.GetParamsType() + "(\"" + createInstance.GetParamsName1(field.Name) + "\", " + field.DotNetSqlType + (field.Length != -1 ? ", " + field.Length.ToString() : "") + "){ Value = " + field.Name.ToLower() + " }");
                data.Append(field.Name != Primarykeys.Last().Name ? "," : "");
                data.Append("\r\n");
            }
            data.Append("\t\t\t};\r\n");
            //data.Append("\t\t\tDBHelper dbHelper = new DBHelper();\r\n");
            data.Append("\t\t\t" + createInstance.GetDataReaderType() + " dataReader = dbHelper.GetDataReader(sql, parameters);\r\n");
            data.Append("\t\t\tList<" + param.NameSpace + (param.NameSpace.IsNullOrEmpty()?"": ".") + param.CNSC.Model + (param.NameSpace1.IsNullOrEmpty() ? "" : "." + param.NameSpace1) + "." + param.ClassName + "> List = DataReaderToList(dataReader);\r\n");
            data.Append("\t\t\tdataReader.Close();\r\n");
            //data.Append("\t\t\tdbHelper.Dispose();\r\n");
            data.Append("\t\t\treturn List.Count > 0 ? List[0] : null;\r\n");
            data.Append("\t\t}\r\n");
            return data.ToString();
        }

        /// <summary>
        /// 查询记录条数方法
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private string GetCountMethod(List<Model.Fields> fields, Model.CodeCreate param)
        {
            StringBuilder data = new StringBuilder();
            data.Append("\t\t/// <summary>\r\n");
            data.Append("\t\t/// 查询记录数\r\n");
            data.Append("\t\t/// </summary>\r\n");
            data.Append("\t\tpublic long GetCount()\r\n");
            data.Append("\t\t{\r\n");
            data.Append("\t\t\tstring sql = \"SELECT COUNT(*) FROM " + param.TableName + "\";\r\n");
            //data.Append("\t\t\tDBHelper dbHelper = new DBHelper();\r\n");
            data.Append("\t\t\tlong count;\r\n");
            data.Append("\t\t\treturn long.TryParse(dbHelper.GetFieldValue(sql), out count) ? count : 0;\r\n");
            data.Append("\t\t}\r\n");
            return data.ToString();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HaoCodeBuilder.Business
{
    internal class Builder_Business
    {
        private IData.ICreateCode createInstance;
        private IData.IDatabase databaseInstance;
        private Import import;
        public Builder_Business(Model.DatabaseType dbType)
        {
            this.createInstance = Factory.Factory.CreateCreateCodeInstance(dbType);
            this.databaseInstance = Factory.Factory.CreateDatabaseInstance(dbType);
            this.import = new Import(dbType);
        }

        /// <summary>
        /// 得到业务层代码
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetBusinessClass(Model.CodeCreate param)
        {
            Model.Servers server = Common.Config.GetServer(param.ServerID);
            if (server == null)
            {
                return string.Empty;
            }
            List<Model.Fields> fields = databaseInstance.GetFields(server.ID, param.DbName, param.TableName);

            //自增列
            var Identitys = fields.Where(p => p.IsIdentity);
            var NotIdeneitys = fields.Where(p => !p.IsIdentity);
            bool HasIdentity = Identitys.Count() > 0;

            //主键
            var Primarykeys = fields.Where(p => p.IsPrimaryKey);
            var NotPrimarykeys = fields.Where(p => !p.IsPrimaryKey);
            bool HasPrimarykey = Primarykeys.Count() > 0;

            StringBuilder business = new StringBuilder(import.GetImport_Business());

            business.Append("namespace " + param.NameSpace + (param.NameSpace.IsNullOrEmpty()?"": ".") + param.CNSC.Business + (param.NameSpace1.IsNullOrEmpty() ? "" : "." + param.NameSpace1) + "\r\n");
            business.Append("{\r\n");
			business.Append("\tpublic partial class " + param.ClassName + "\r\n");
            business.Append("\t{\r\n");
            business.Append("\t\tprivate " + (param.BuilderType == Model.BuilderType.Factory ? param.CNSC.Interface + ".I" : param.CNSC.Data + ".") + param.ClassName + " data" + param.ClassName + ";\r\n");
            business.Append("\t\tpublic " + param.ClassName + "()\r\n");
            business.Append("\t\t{\r\n");
            business.Append("\t\t\tthis.data" + param.ClassName + " = " + (param.BuilderType == Model.BuilderType.Factory ? param.CNSC.Factory + "." + param.ClassName + ".CreateInstance();" : "new " + param.CNSC.Data + "." + param.ClassName+"();") + "\r\n");
            business.Append("\t\t}\r\n");

            if (param.MethodList.Contains(Model.BuilderMethods.Add))
            {
                business.Append("\t\t/// <summary>\r\n");
                business.Append("\t\t/// 新增\r\n");
                business.Append("\t\t/// </summary>\r\n");
                business.Append("\t\tpublic " + (HasIdentity ? Identitys.First().DotNetType : "int") + " Add(" + param.NameSpace + (param.NameSpace.IsNullOrEmpty()?"": ".") + param.CNSC.Model + (param.NameSpace1.IsNullOrEmpty() ? "" : "." + param.NameSpace1) + "." + param.ClassName + " model)\r\n");
                business.Append("\t\t{\r\n");
                business.Append("\t\t\treturn data" + param.ClassName + ".Add(model);\r\n");
                business.Append("\t\t}\r\n");
            }

            if (param.MethodList.Contains(Model.BuilderMethods.Update) && HasPrimarykey)
            {
                business.Append("\t\t/// <summary>\r\n");
                business.Append("\t\t/// 更新\r\n");
                business.Append("\t\t/// </summary>\r\n");
                business.Append("\t\tpublic int Update(" + param.NameSpace + (param.NameSpace.IsNullOrEmpty()?"": ".") + param.CNSC.Model + (param.NameSpace1.IsNullOrEmpty() ? "" : "." + param.NameSpace1) + "." + param.ClassName + " model)\r\n");
                business.Append("\t\t{\r\n");
                business.Append("\t\t\treturn data" + param.ClassName + ".Update(model);\r\n");
                business.Append("\t\t}\r\n");
            }

            if (param.MethodList.Contains(Model.BuilderMethods.SelectAll))
            {
                business.Append("\t\t/// <summary>\r\n");
                business.Append("\t\t/// 查询所有记录\r\n");
                business.Append("\t\t/// </summary>\r\n");
                business.Append("\t\tpublic List<" + param.NameSpace + (param.NameSpace.IsNullOrEmpty() ? "" : ".") + param.CNSC.Model + (param.NameSpace1.IsNullOrEmpty() ? "" : "." + param.NameSpace1) + "." + param.ClassName + "> GetAll()\r\n");
                business.Append("\t\t{\r\n");
                business.Append("\t\t\treturn data" + param.ClassName + ".GetAll();\r\n");
                business.Append("\t\t}\r\n");
            }

            if (param.MethodList.Contains(Model.BuilderMethods.SelectByKey) && HasPrimarykey)
            {
                #region 查询单条记录
                business.Append("\t\t/// <summary>\r\n");
                business.Append("\t\t/// 查询单条记录\r\n");
                business.Append("\t\t/// </summary>\r\n");
                business.Append("\t\tpublic " + param.NameSpace + (param.NameSpace.IsNullOrEmpty() ? "" : ".") + param.CNSC.Model + (param.NameSpace1.IsNullOrEmpty() ? "" : "." + param.NameSpace1) + "." + param.ClassName + " Get(");

                foreach (var field in Primarykeys)
                {
                    business.Append(field.DotNetType + " " + field.Name.ToLower());
                    business.Append(field.Name != Primarykeys.Last().Name ? ", " : "");
                }
                business.Append(")\r\n");
                business.Append("\t\t{\r\n");
                business.Append("\t\t\treturn data" + param.ClassName + ".Get(");

                foreach (var field in Primarykeys)
                {
                    business.Append(field.Name.ToLower());
                    business.Append(field.Name != Primarykeys.Last().Name ? ", " : "");
                }
                business.Append(");\r\n");
                business.Append("\t\t}\r\n");
                #endregion
            }
            if (param.MethodList.Contains(Model.BuilderMethods.Delete) && HasPrimarykey)
            {
                #region 删除
                business.Append("\t\t/// <summary>\r\n");
                business.Append("\t\t/// 删除\r\n");
                business.Append("\t\t/// </summary>\r\n");
                business.Append("\t\tpublic int Delete(");
                foreach (var field in Primarykeys)
                {
                    business.Append(field.DotNetType + " " + field.Name.ToLower());
                    business.Append(field.Name != Primarykeys.Last().Name ? ", " : "");
                }
                business.Append(")\r\n");
                business.Append("\t\t{\r\n");
                business.Append("\t\t\treturn data" + param.ClassName + ".Delete(");
                foreach (var field in Primarykeys)
                {
                    business.Append(field.Name.ToLower());
                    business.Append(field.Name != Primarykeys.Last().Name ? ", " : "");
                }
                business.Append(");\r\n");
                business.Append("\t\t}\r\n");

                #endregion
            }

            if (param.MethodList.Contains(Model.BuilderMethods.Count))
            {
                business.Append("\t\t/// <summary>\r\n");
                business.Append("\t\t/// 查询记录条数\r\n");
                business.Append("\t\t/// </summary>\r\n");
                business.Append("\t\tpublic long GetCount()\r\n");
                business.Append("\t\t{\r\n");
                business.Append("\t\t\treturn data" + param.ClassName + ".GetCount();\r\n");
                business.Append("\t\t}\r\n");
            }

            business.Append("\t}\r\n");
            business.Append("}\r\n");
            return business.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HaoCodeBuilder.Business
{
    internal class Builder_Interface
    {
        private IData.ICreateCode createInstance;
        private IData.IDatabase databaseInstance;
        private Import import;
        public Builder_Interface(Model.DatabaseType dbType)
        {
            this.createInstance = Factory.Factory.CreateCreateCodeInstance(dbType);
            this.databaseInstance = Factory.Factory.CreateDatabaseInstance(dbType);
            this.import = new Import(dbType);
        }

        /// <summary>
        /// 得到接口层代码
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetInterfaceClass(Model.CodeCreate param)
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

            StringBuilder inter = new StringBuilder(import.GetImport_Interface());
            inter.Append("namespace " + param.NameSpace + (param.NameSpace.IsNullOrEmpty()?"": ".") + param.CNSC.Interface + (param.NameSpace1.IsNullOrEmpty() ? "" : "." + param.NameSpace1) + "\r\n");
            inter.Append("{\r\n");

            inter.Append("\tpublic interface I" + param.ClassName + "\r\n");
            inter.Append("\t{\r\n");

            if (param.MethodList.Contains(Model.BuilderMethods.Add))
            {
                inter.Append("\t\t/// <summary>\r\n");
                inter.Append("\t\t/// 新增\r\n");
                inter.Append("\t\t/// </summary>\r\n");
                inter.Append("\t\t" + (HasIdentity ? Identitys.First().DotNetType : "int") + " Add(" + param.NameSpace + (param.NameSpace.IsNullOrEmpty()?"": ".") + param.CNSC.Model + (param.NameSpace1.IsNullOrEmpty() ? "" : "." + param.NameSpace1) + "." + param.ClassName + " model);\r\n\r\n");
            }

            if (param.MethodList.Contains(Model.BuilderMethods.Update) && HasPrimarykey)
            {
                inter.Append("\t\t/// <summary>\r\n");
                inter.Append("\t\t/// 更新\r\n");
                inter.Append("\t\t/// </summary>\r\n");
                inter.Append("\t\tint Update(" + param.NameSpace + (param.NameSpace.IsNullOrEmpty()?"": ".") + param.CNSC.Model + (param.NameSpace1.IsNullOrEmpty() ? "" : "." + param.NameSpace1) + "." + param.ClassName + " model);\r\n\r\n");
            }

            if (param.MethodList.Contains(Model.BuilderMethods.SelectAll))
            {
                inter.Append("\t\t/// <summary>\r\n");
                inter.Append("\t\t/// 查询所有记录\r\n");
                inter.Append("\t\t/// </summary>\r\n");
                inter.Append("\t\tList<" + param.NameSpace + (param.NameSpace.IsNullOrEmpty()?"": ".") + param.CNSC.Model + (param.NameSpace1.IsNullOrEmpty() ? "" : "." + param.NameSpace1) + "." + param.ClassName + "> GetAll();\r\n\r\n");
            }

            if (param.MethodList.Contains(Model.BuilderMethods.SelectByKey) && HasPrimarykey)
            {
                #region 查询单条记录
                inter.Append("\t\t/// <summary>\r\n");
                inter.Append("\t\t/// 查询单条记录\r\n");
                inter.Append("\t\t/// </summary>\r\n");
                inter.Append("\t\tModel" + (param.NameSpace1.IsNullOrEmpty() ? "" : "." + param.NameSpace1) + "." + param.ClassName + " Get(");

                foreach (var field in Primarykeys)
                {
                    inter.Append(field.DotNetType + " " + field.Name.ToLower());
                    inter.Append(field.Name != Primarykeys.Last().Name ? ", " : "");
                }
                inter.Append(");\r\n\r\n");

                #endregion
            }
            if (param.MethodList.Contains(Model.BuilderMethods.Delete) && HasPrimarykey)
            {
                #region 删除
                inter.Append("\t\t/// <summary>\r\n");
                inter.Append("\t\t/// 删除\r\n");
                inter.Append("\t\t/// </summary>\r\n");
                inter.Append("\t\tint Delete(");
                foreach (var field in Primarykeys)
                {
                    inter.Append(field.DotNetType + " " + field.Name.ToLower());
                    inter.Append(field.Name != Primarykeys.Last().Name ? ", " : "");
                }
                inter.Append(");\r\n\r\n");

                #endregion
            }

            if (param.MethodList.Contains(Model.BuilderMethods.Count))
            {
                inter.Append("\t\t/// <summary>\r\n");
                inter.Append("\t\t/// 查询记录条数\r\n");
                inter.Append("\t\t/// </summary>\r\n");
                inter.Append("\t\tlong GetCount();\r\n");

            }

            inter.Append("\t}\r\n");
            inter.Append("}\r\n");
            return inter.ToString();
        }
    }
}

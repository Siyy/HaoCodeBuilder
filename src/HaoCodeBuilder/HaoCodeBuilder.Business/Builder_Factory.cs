using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HaoCodeBuilder.Business
{
    internal class Builder_Factory
    {
        private IData.ICreateCode createInstance;
        private IData.IDatabase databaseInstance;
        private Import import;
        public Builder_Factory(Model.DatabaseType dbType)
        {
            this.createInstance = Factory.Factory.CreateCreateCodeInstance(dbType);
            this.databaseInstance = Factory.Factory.CreateDatabaseInstance(dbType);
            this.import = new Import(dbType);
        }

        /// <summary>
        /// 得到工厂层代码
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetFactoryClass(Model.CodeCreate param)
        {
            Model.Servers server = Common.Config.GetServer(param.ServerID);
            if (server == null)
            {
                return string.Empty;
            }
            List<Model.Fields> fields = databaseInstance.GetFields(server.ID, param.DbName, param.TableName);

            StringBuilder factory = new StringBuilder(import.GetImport_Factory());
            factory.Append("namespace " + param.NameSpace + (param.NameSpace.IsNullOrEmpty()?"": ".") + param.CNSC.Factory + (param.NameSpace1.IsNullOrEmpty() ? "" : "." + param.NameSpace1) + "\r\n");
            factory.Append("{\r\n");

			factory.Append("\tpublic partial class " + param.ClassName + "\r\n");
            factory.Append("\t{\r\n");
            factory.Append("\t\t/// <summary>\r\n");
            factory.Append("\t\t/// 创建实例对象\r\n");
            factory.Append("\t\t/// </summary>\r\n");
            factory.Append("\t\tpublic static " + param.NameSpace + (param.NameSpace.IsNullOrEmpty()?"": ".") + param.CNSC.Interface + (param.NameSpace1.IsNullOrEmpty() ? "" : "." + param.NameSpace1) + ".I" + param.ClassName + " CreateInstance()\r\n"); ;
            factory.Append("\t\t{\r\n");
            factory.Append("\t\t\treturn Factory.CreateInstance(\"" + param.ClassName + "\") as " + param.CNSC.Interface + ".I" + param.ClassName + ";\r\n");
            factory.Append("\t\t}\r\n");
            factory.Append("\t}\r\n");
            factory.Append("}\r\n");

            return factory.ToString();
        }
    }
}

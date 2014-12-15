using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HaoCodeBuilder.Business
{
    internal class Builder_Model
    {
        private IData.ICreateCode createInstance;
        private IData.IDatabase databaseInstance;
        private Import import;
        public Builder_Model(Model.DatabaseType dbType)
        {
            this.createInstance = Factory.Factory.CreateCreateCodeInstance(dbType);
            this.databaseInstance = Factory.Factory.CreateDatabaseInstance(dbType);
            this.import = new Import(dbType);
        }
        /// <summary>
        /// 得到实体层
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetModelClass(Model.CodeCreate param)
        {
            Model.Servers server = Common.Config.GetServer(param.ServerID);
            if (server == null)
            {
                return string.Empty;
            }

            List<Model.Fields> fields = databaseInstance.GetFields(server.ID, param.DbName, param.TableName);
            StringBuilder model = new StringBuilder(import.GetImport_Model());
            model.Append("namespace " + param.NameSpace + (param.NameSpace.IsNullOrEmpty()?"": ".") + param.CNSC.Model + (param.NameSpace1.IsNullOrEmpty() ? "" : "." + param.NameSpace1) + "\r\n");
            model.Append("{\r\n");
            model.Append("\t[Serializable]\r\n");
			model.Append("\tpublic partial class " + param.ClassName + "\r\n");
            model.Append("\t{\r\n");
            foreach (var field in fields)
            {
                model.Append("\t\t/// <summary>\r\n");
                model.Append("\t\t/// " + (field.Note.IsNullOrEmpty() ? field.Name : field.Note) + "\r\n");
                model.Append("\t\t/// </summary>\r\n");
                model.Append("\t\t[DisplayName(\"" + (field.Note.IsNullOrEmpty() ? field.Name : field.Note) + "\")]\r\n");
                model.Append("\t\tpublic " + field.DotNetType + " " + field.Name + " { get; set; }\r\n\r\n");
            }
            model.Append("\t}\r\n");
            model.Append("}\r\n");
            return model.ToString();
        }
    }
}

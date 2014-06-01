using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HaoCodeBuilder.Business
{
    internal class Import
    {
        private IData.ICreateCode createInstance;
        public Import(Model.DatabaseType dataBaseType)
        {
            createInstance = Factory.Factory.CreateCreateCodeInstance(dataBaseType);
        }
        /// <summary>
        /// 得到实体层引用字符串
        /// </summary>
        /// <returns></returns>
        public string GetImport_Model()
        {
            StringBuilder import = new StringBuilder();
            import.Append("using System;\r\n");
            import.Append("using System.ComponentModel;\r\n");
            import.Append("using System.ComponentModel.DataAnnotations;\r\n\r\n");
            return import.ToString();
        }

        /// <summary>
        /// 数据层引用字符串
        /// </summary>
        /// <returns></returns>
        public string GetImport_Data()
        {
            StringBuilder import = new StringBuilder();
            import.Append("using System;\r\n");
            import.Append("using System.Collections.Generic;\r\n");
            import.Append("using System.Text;\r\n");
            import.Append("using System.Data;\r\n");
            import.Append("using " + createInstance.GetDataNameSpace() + ";\r\n\r\n");
            return import.ToString();
        }

        /// <summary>
        /// 得到业务层引用字符串
        /// </summary>
        /// <returns></returns>
        public string GetImport_Business()
        {
            StringBuilder import = new StringBuilder();
            import.Append("using System;\r\n");
            import.Append("using System.Collections.Generic;\r\n");
            import.Append("using System.Text;\r\n\r\n");
            return import.ToString();
        }

        /// <summary>
        /// 得到接口层引用字符串
        /// </summary>
        /// <returns></returns>
        public string GetImport_Interface()
        {
            StringBuilder import = new StringBuilder();
            import.Append("using System;\r\n");
            import.Append("using System.Collections.Generic;\r\n\r\n");
            return import.ToString();
        }

        /// <summary>
        /// 得到工厂层引用字符串
        /// </summary>
        /// <returns></returns>
        public string GetImport_Factory()
        {
            StringBuilder import = new StringBuilder();
            import.Append("using System;\r\n");
            import.Append("using System.Reflection;\r\n\r\n");
            return import.ToString();
        }
    }
}

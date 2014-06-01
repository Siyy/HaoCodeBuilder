using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HaoCodeBuilder.Business
{
    public class CreateCode
    {
        Model.DatabaseType dbType;
        public CreateCode(Model.DatabaseType dataBaseType)
        {
            this.dbType = dataBaseType;
        }

        /// <summary>
        /// 得到实体层代码
        /// </summary>
        /// <returns></returns>
        public string GetModelClass(Model.CodeCreate param)
        {
            return new Builder_Model(dbType).GetModelClass(param);
        }

        /// <summary>
        /// 得到数据层代码
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetDataClass(Model.CodeCreate param)
        {
            return new Builder_Data(dbType).GetDataClass(param);
        }

        /// <summary>
        /// 得到业务层代码
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetBusinessClass(Model.CodeCreate param)
        {
            return new Builder_Business(dbType).GetBusinessClass(param);
        }

        /// <summary>
        /// 得到接口层代码
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetInterfaceClass(Model.CodeCreate param)
        {
            return new Builder_Interface(dbType).GetInterfaceClass(param);
        }

        /// <summary>
        /// 得到工厂层代码
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetFactoryClass(Model.CodeCreate param)
        {
            return new Builder_Factory(dbType).GetFactoryClass(param);
        }

    }
}

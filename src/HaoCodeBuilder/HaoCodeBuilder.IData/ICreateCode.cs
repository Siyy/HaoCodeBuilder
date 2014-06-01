using System;

namespace HaoCodeBuilder.IData
{
    public interface ICreateCode
    {
        /// <summary>
        /// 得到数据层命名空间
        /// </summary>
        /// <returns></returns>
        string GetDataNameSpace();
        /// <summary>
        /// 得到参数名称
        /// </summary>
        /// <returns></returns>
        string GetParamsName(string name);
        /// <summary>
        /// 得到参数名称1
        /// </summary>
        /// <returns></returns>
        string GetParamsName1(string name);
        /// <summary>
        /// 得到参数类型
        /// </summary>
        /// <returns></returns>
        string GetParamsType();
        /// <summary>
        /// 得到DataReader类型
        /// </summary>
        /// <returns></returns>
        string GetDataReaderType();
        /// <summary>
        /// 查询自增ID的方法
        /// </summary>
        /// <returns></returns>
        string GetIdentityMethod();
    }
}

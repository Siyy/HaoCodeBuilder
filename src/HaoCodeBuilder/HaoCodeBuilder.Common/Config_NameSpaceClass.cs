using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace HaoCodeBuilder.Common
{
    public class Config_NameSpaceClass
    {
        public Config_NameSpaceClass()
        {
            XmlFileExists();
        }
        /// <summary>
        /// XML文件路径
        /// </summary>
        private string XmlFile = string.Format("{0}Config\\NameSpaceClass.xml", Func.GetAppPath());
        /// <summary>
        /// 检查配置文件是否存在，没有则创建
        /// </summary>
        private void XmlFileExists()
        {
            FileInfo fiXML = new FileInfo(XmlFile);
            if (!(fiXML.Exists))
            {
                XDocument xelLog = new XDocument(
                    new XDeclaration("1.0", "utf-8", string.Empty),
                    new XElement("root")
                 );
                xelLog.Save(XmlFile);
            }
        }
        /// <summary>
        /// 得到所有类命名空间
        /// </summary>
        /// <returns></returns>
        public List<Model.ConfigNameSpaceClass> GetAll()
        {
            List<Model.ConfigNameSpaceClass> list = new List<Model.ConfigNameSpaceClass>();
            try
            {
                XElement xelem = XElement.Load(XmlFile);
                var queryXML = from xele in xelem.Elements("namespaceclass")
                               select new
                               {
                                   model = xele.Element("model").Value,
                                   data = xele.Element("data").Value,
                                   business = xele.Element("business").Value,
                                   interface1 = xele.Element("interface").Value,
                                   factory = xele.Element("factory").Value
                               };
                foreach (var q in queryXML)
                {
                    list.Add(new Model.ConfigNameSpaceClass()
                    {
                        Model = q.model,
                        Data = q.data,
                        Business = q.business,
                        Factory = q.factory,
                        Interface = q.interface1
                    });
                }
                
            }
            catch { }
            return list;
        }
        /// <summary>
        /// 添加一个类命名空间
        /// </summary>
        /// <param name="cns"></param>
        public bool Add(Model.ConfigNameSpaceClass cns)
        {
            try
            {
                //先删除
                Delete(cns.Model);
                XElement xelem = XElement.Load(XmlFile);
                XElement newLog = new XElement("namespaceclass",
                                      new XElement("model", cns.Model),
                                      new XElement("data", cns.Data),
                                      new XElement("business", cns.Business),
                                      new XElement("interface", cns.Interface),
                                      new XElement("factory", cns.Factory)
                                  );
                xelem.Add(newLog);
                xelem.Save(XmlFile);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 保存类命名空间
        /// </summary>
        /// <param name="cns"></param>
        /// <returns></returns>
        public bool Save(Model.ConfigNameSpaceClass cns, string oldmodel = "")
        {
            if (!oldmodel.IsNullOrEmpty())
                Delete(oldmodel);
            return Add(cns);
        }
        /// <summary>
        /// 删除一个命名空间
        /// </summary>
        /// <param name="namespace1"></param>
        /// <returns></returns>
        public bool Delete(string model)
        {
            try
            {
                XElement xelem = XElement.Load(XmlFile);
                var queryXML = from xele in xelem.Elements("namespaceclass")
                               where xele.Element("model").Value == model
                               select xele;
                queryXML.Remove();
                xelem.Save(XmlFile);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 查询一个命名空间
        /// </summary>
        /// <param name="namespace1"></param>
        /// <returns></returns>
        public Model.ConfigNameSpaceClass Get(string model)
        {
            try
            {
                XElement xelem = XElement.Load(XmlFile);
                var queryXML = from xele in xelem.Elements("namespaceclass")
                               where xele.Element("model").Value == model
                               select new
                               {
                                   model = xele.Element("model").Value,
                                   data = xele.Element("data").Value,
                                   business = xele.Element("business").Value,
                                   interface1 = xele.Element("interface").Value,
                                   factory = xele.Element("factory").Value,
                                   isdefault = xele.Element("isdefault").Value.ToLower()
                               };
                Model.ConfigNameSpaceClass cns = new Model.ConfigNameSpaceClass();
                if (queryXML.Count() > 0)
                {
                    cns.Model = queryXML.First().model;
                    cns.Data = queryXML.First().data;
                    cns.Business = queryXML.First().business;
                    cns.Interface = queryXML.First().interface1;
                    cns.Factory = queryXML.First().factory;
                }
                return cns;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 查询默认命名空间
        /// </summary>
        /// <param name="namespace1"></param>
        /// <returns></returns>
        public Model.ConfigNameSpaceClass GetDefault()
        {
            var list = GetAll();
            if (list.Count == 0)
            {
                return new Model.ConfigNameSpaceClass()
                {
                    Model = "Model",
                    Data = "Data",
                    Business = "Business",
                    Factory = "Factory",
                    Interface = "IData"
                };
            }
            else
            {
                return list.Last();
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace HaoCodeBuilder.Common
{
    public class Config_NameSpace
    {
        public Config_NameSpace()
        {
            XmlFileExists();
        }
        /// <summary>
        /// XML文件路径
        /// </summary>
        private string XmlFile = string.Format("{0}Config\\NameSpace.xml", Func.GetAppPath());
        /// <summary>
        /// 得到所有命名空间
        /// </summary>
        /// <returns></returns>
        public List<Model.ConfigNameSpace> GetAll()
        {
            List<Model.ConfigNameSpace> list = new List<Model.ConfigNameSpace>();
            try
            {
                XElement xelem = XElement.Load(XmlFile);
                var queryXML = from xele in xelem.Elements("namespace")
                               select new
                               {
                                   name1 = xele.Element("namespace1").Value,
                                   name2 = xele.Element("namespace2").Value
                               };
                foreach (var q in queryXML.OrderBy(p=>p.name1))
                {
                    list.Add(new Model.ConfigNameSpace()
                    {
                        Name1 = q.name1,
                        Name2 = q.name2
                    });
                }
            }
            catch { }
            return list;
        }
        /// <summary>
        /// 添加一个命名空间
        /// </summary>
        /// <param name="cns"></param>
        public bool Add(Model.ConfigNameSpace cns)
        {
            try
            {
                //先删除
                Delete(cns.Name1);
                XElement xelem = XElement.Load(XmlFile);
                XElement newLog = new XElement("namespace",
                                      new XElement("namespace1", cns.Name1),
                                      new XElement("namespace2", cns.Name2)
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
        /// 保存命名空间
        /// </summary>
        /// <param name="cns"></param>
        /// <returns></returns>
        public bool Save(Model.ConfigNameSpace cns, string oldname1="")
        {
            if(!oldname1.IsNullOrEmpty())
                Delete(oldname1);
            return Add(cns);
        }
        /// <summary>
        /// 删除一个命名空间
        /// </summary>
        /// <param name="namespace1"></param>
        /// <returns></returns>
        public bool Delete(string namespace1)
        {
            try
            {
                XElement xelem = XElement.Load(XmlFile);
                var queryXML = from xele in xelem.Elements("namespace")
                               where xele.Element("namespace1").Value == namespace1
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
        public Model.ConfigNameSpace Get(string namespace1)
        {
            try
            {
                XElement xelem = XElement.Load(XmlFile);
                var queryXML = from xele in xelem.Elements("namespace")
                               where xele.Element("namespace1").Value == namespace1
                               select new
                               {
                                   name1 = xele.Element("namespace1").Value,
                                   name2 = xele.Element("namespace2").Value,
                                   isdefault = xele.Element("isdefault").Value.ToLower()
                               };
                Model.ConfigNameSpace cns = new Model.ConfigNameSpace();
                if (queryXML.Count() > 0)
                {
                    cns.Name1 = queryXML.First().name1;
                    cns.Name2 = queryXML.First().name2;
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
        public Model.ConfigNameSpace GetDefault()
        {
            var list = GetAll();
            if (list.Count == 0)
            {
                return null;
            }
            else
            {
                return list.Last();
            }

        }

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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace HaoCodeBuilder.Common
{
    public class Config_Directory
    {
        public Config_Directory()
        {
            XmlFileExists();
        }
        /// <summary>
        /// XML文件路径
        /// </summary>
        private string XmlFile = string.Format("{0}Config\\Directory.xml", Func.GetAppPath());
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
        public List<Model.ConfigDirectory> GetAll()
        {
            List<Model.ConfigDirectory> list = new List<Model.ConfigDirectory>();
            try
            {
                XElement xelem = XElement.Load(XmlFile);
                var queryXML = from xele in xelem.Elements("directory")
                               select new
                               {
                                   name = xele.Element("name").Value
                               };
                foreach (var q in queryXML)
                {
                    list.Add(new Model.ConfigDirectory()
                    {
                        Name=q.name
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
        public bool Add(Model.ConfigDirectory cns)
        {
            try
            {
                //先删除
                Delete(cns.Name);
                XElement xelem = XElement.Load(XmlFile);
                XElement newLog = new XElement("directory",
                                      new XElement("name", cns.Name)
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
        public bool Save(Model.ConfigDirectory cns, string oldmodel = "")
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
        public bool Delete(string name)
        {
            try
            {
                XElement xelem = XElement.Load(XmlFile);
                var queryXML = from xele in xelem.Elements("directory")
                               where xele.Element("name").Value == name
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
        public Model.ConfigDirectory Get(string name)
        {
            try
            {
                XElement xelem = XElement.Load(XmlFile);
                var queryXML = from xele in xelem.Elements("directory")
                               where xele.Element("name").Value == name
                               select new
                               {
                                   name = xele.Element("name").Value
                               };
                Model.ConfigDirectory cns = new Model.ConfigDirectory();
                if (queryXML.Count() > 0)
                {
                    cns.Name = queryXML.First().name;
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
        public Model.ConfigDirectory GetDefault()
        {
            var list = GetAll();
            if (list.Count == 0)
            {
                return new Model.ConfigDirectory()
                {
                     Name = ""
                };
            }
            else
            {
                return list.Last();
            }

        }
    }
}

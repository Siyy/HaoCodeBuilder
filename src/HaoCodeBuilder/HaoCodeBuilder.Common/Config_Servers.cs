using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace HaoCodeBuilder.Common
{
    public class Config_Servers
    {
        public Config_Servers()
        {
            XmlFileExists();
        }
        /// <summary>
        /// XML文件路径
        /// </summary>
        private string XmlFile = string.Format("{0}Config\\Servers.xml", Func.GetAppPath());
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
        public List<Model.ConfigServers> GetAll()
        {
            List<Model.ConfigServers> list = new List<Model.ConfigServers>();
            try
            {
                XElement xelem = XElement.Load(XmlFile);
                var queryXML = from xele in xelem.Elements("server")
                               select new
                               {
                                   name = xele.Element("name").Value,
                                   type = xele.Element("type").Value,
                                   servername = xele.Element("servername").Value,
                                   database = xele.Element("database").Value,
                                   uid = xele.Element("uid").Value,
                                   pwd = xele.Element("pwd").Value,
                                   port = xele.Element("port").Value,
                                   file = xele.Element("file").Value
                               };
                foreach (var q in queryXML)
                {
                    list.Add(new Model.ConfigServers()
                    {
                        Name = q.name,
                        Database = q.database,
                        file = q.file,
                        Port = q.port,
                        Pwd = q.pwd.IsNullOrEmpty() ? "" : Common.Encryption.DesDecrypt(q.pwd),
                        Uid = q.uid,
                        Type = q.type,
                        ServerName = q.servername
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
        public bool Add(Model.ConfigServers cns)
        {
            try
            {
                
                //先删除
                Delete(cns.Name);
                XElement xelem = XElement.Load(XmlFile);
                XElement newLog = new XElement("server",
                                      new XElement("name", cns.Name),
                                      new XElement("type", cns.Type),
                                      new XElement("servername", cns.ServerName),
                                      new XElement("database", cns.Database),
                                      new XElement("uid", cns.Uid),
                                      new XElement("pwd", cns.Pwd.IsNullOrEmpty() ? "" : Common.Encryption.DesEncrypt(cns.Pwd)),
                                      new XElement("port", cns.Port),
                                      new XElement("file", cns.file)
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
        public bool Save(Model.ConfigServers cns, string oldmodel = "")
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
                var queryXML = from xele in xelem.Elements("server")
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
        public Model.ConfigServers Get(string name)
        {
            try
            {
                XElement xelem = XElement.Load(XmlFile);
                var queryXML = from xele in xelem.Elements("namespaceclass")
                               where xele.Element("model").Value == name
                               select new
                               {
                                   name = xele.Element("name").Value,
                                   type = xele.Element("type").Value,
                                   servername = xele.Element("servername").Value,
                                   database = xele.Element("database").Value,
                                   uid = xele.Element("uid").Value,
                                   pwd = xele.Element("pwd").Value,
                                   port = xele.Element("port").Value,
                                   file = xele.Element("file").Value
                               };
                Model.ConfigServers cns = new Model.ConfigServers();
                if (queryXML.Count() > 0)
                {
                    var q = queryXML.First();
                    cns.Name = q.servername + q.type;
                    cns.Database = q.database;
                    cns.file = q.file;
                    cns.Port = q.port;
                    cns.Pwd = q.pwd.IsNullOrEmpty() ? "" : Common.Encryption.DesDecrypt(q.pwd);
                    cns.Uid = q.uid;
                    cns.Type = q.type;
                    cns.ServerName = q.servername;
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
        public Model.ConfigServers GetDefault(Model.DatabaseType dbType=Model.DatabaseType.Empty)
        {
            var list = GetAll();
            if (list.Count == 0)
            {
                return null;
            }
            else
            {
                if (dbType == Model.DatabaseType.Empty)
                {
                    return list.Last();
                }
                else
                {
                    var li = list.Where(p => p.Type == dbType.ToString());
                    var rli = li.Count() > 0 ? li.Last() : null;
                    return rli;
                }
            }

        }
    }
}

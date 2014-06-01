using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HaoCodeBuilder.Common
{
    public class Func
    {
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="contents"></param>
        public static void WriteLog(string contents)
        {
            string strFilePath = string.Format("{0}\\Logs\\{1}.txt", AppDomain.CurrentDomain.BaseDirectory,DateTime.Now.ToString("yyyyMMdd"));
            System.IO.FileStream fs = new System.IO.FileStream(strFilePath, System.IO.FileMode.Append);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, System.Text.Encoding.Default);
            sw.WriteLine(string.Format("{0}\t{1}", DateTime.Now.ToString(), contents));
            sw.Close();
            fs.Close();
        }
        /// <summary>
        /// 得到应用程序路径
        /// </summary>
        public static string GetAppPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        /// <summary>
        /// 检查目录是否存在，不存在就创建
        /// </summary>
        /// <param name="path"></param>
        public static string ExistsDirectory(string path)
        {
            string dir = System.IO.Path.GetDirectoryName(path);
            if (!System.IO.Directory.Exists(dir))
            {
                System.IO.Directory.CreateDirectory(dir);
            }
            return path;
        }
    }
}

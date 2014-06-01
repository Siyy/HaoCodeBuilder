using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HaoCodeBuilder.Model;

namespace HaoCodeBuilder
{
    public partial class Form_AddDatabase_Sqlite : Form
    {
        private Model.DatabaseType dbType;
        private Business.Database BLL_Database = null;
        public Form_AddDatabase_Sqlite(Model.DatabaseType dbType)
        {
            InitializeComponent();
            this.dbType = dbType;
            this.BLL_Database = new Business.Database(dbType);
        }

        private void Form_AddDatabase_Sqlite_Load(object sender, EventArgs e)
        {
            var def = new Common.Config_Servers().GetDefault(dbType);
            if (def != null)
            {
                this.textBox_file.Text = def.file;
                this.textBox_pwd.Text = def.Pwd;
            }
        }


        private bool test_link_sqlite(out string errMsg)
        {
            errMsg = string.Empty;
            
            string userpass = this.textBox_pwd.Text;
            string file = this.textBox_file.Text.Replace("/", "\\");

            if (file.IsNullOrEmpty())
            {
                errMsg = "数据库文件为空";
                return false;
            }
            else if (!file.IsPath())
            {
                errMsg = "不是有效的数据库文件";
                return false;
            }

            Common.Config.AddServerList(new Servers()
            {
                ID = string.Format("{0}({1})", dbType.ToString(), file),
                Name = file,
                Type = dbType,
                DatabaseName = file,
                Password = userpass.Trim(),
                Server = file,
                UserID = "",
                Port = -1
            });

            return BLL_Database.TestDatabaseConnnection(string.Format("{0}({1})", dbType.ToString(), file), out errMsg);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.ShowDialog();
            this.textBox_file.Text = this.openFileDialog1.FileName;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form_AddDatabase fa = new Form_AddDatabase();
            fa.ShowDialog();
            this.Close();
        }

        private void button_mysqltest_Click(object sender, EventArgs e)
        {
            this.button_mysqltest.Text = "测试中...";
            this.button_mysqltest.Enabled = false;

            string msg;
            if (!test_link_sqlite(out msg))
            {
                MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("连接成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            this.button_mysqltest.Text = "测试连接";
            this.button_mysqltest.Enabled = true;
        }

        private void button_mysqladd_Click(object sender, EventArgs e)
        {
            string msg;
            if (!test_link_sqlite(out msg))
            {
                MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                Form1.form_Database.AddServer(string.Format("{0}({1})", dbType.ToString(), this.textBox_file.Text.Replace("/", "\\")));
                this.Close();
                AddServer();
            }
        }

        /// <summary>
        /// 记录服务器设置
        /// </summary>
        private void AddServer()
        {
            string file = this.textBox_file.Text.Replace("/", "\\");
            string userpass = this.textBox_pwd.Text;
            Model.ConfigServers cs = new ConfigServers();
            cs.Name = string.Format("{0}({1})", file, dbType.ToString());
            cs.Port = "";
            cs.Database = file;
            cs.file = file;
            cs.Pwd = this.checkBox1.Checked ? userpass : "";
            cs.ServerName = file;
            cs.Type = dbType.ToString();
            cs.Uid = "";
            Form_AddDatabase.AddServerToXml(cs);
        }
    }
}

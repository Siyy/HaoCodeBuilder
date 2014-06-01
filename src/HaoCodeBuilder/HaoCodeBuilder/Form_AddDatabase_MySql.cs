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
    public partial class Form_AddDatabase_MySql : Form
    {
        private Model.DatabaseType dbType;
        private Business.Database BLL_Database = null;
        private IEnumerable<Model.ConfigServers> csList;
        public Form_AddDatabase_MySql(DatabaseType dbType)
        {
            InitializeComponent();
            this.dbType = dbType;
            this.BLL_Database = new Business.Database(dbType);
        }

        private void Form_AddDatabase_MySql_Load(object sender, EventArgs e)
        {
            Common.Config_Servers CS = new Common.Config_Servers();
            csList = CS.GetAll().Where(p => p.Type == this.dbType.ToString());
            foreach (var li in csList)
            {
                this.comboBox_mysql_server.Items.Add(li.ServerName);
            }

            Model.ConfigServers cs = CS.GetDefault(this.dbType);
            if (cs != null)
            {
                this.comboBox_mysql_server.Text = cs.ServerName;
                this.textBox_mysql_pwd.Text = cs.Pwd;
                this.textBox_mysql_uid.Text = cs.Uid;
                this.checkBox1.Checked = !cs.Pwd.IsNullOrEmpty();
            }
        }

        private bool test_link_mysql(out string errMsg)
        {
            errMsg = string.Empty;
            string server = this.comboBox_mysql_server.Text;
            string userid = this.textBox_mysql_uid.Text;
            string userpass = this.textBox_mysql_pwd.Text;
            string prot = this.textBox_mysql_port.Text;

            if (server.IsNullOrEmpty())
            {
                errMsg = "服务器不能为空";
                return false;
            }
            else if (userid.IsNullOrEmpty())
            {
                errMsg = "登录名不能为空";
                return false;
            }
            else if (userpass.IsNullOrEmpty())
            {
                errMsg = "密码不能为空";
                return false;
            }
            else if (this.BLL_Database == null)
            {
                errMsg = "数据库类型为空";
                return false;
            }

            Common.Config.AddServerList(new Servers()
            {
                ID = string.Format("{0}({1})", server.Trim(), dbType.ToString()),
                Name = server.Trim(),
                Type = dbType,
                DatabaseName = "",
                Password = userpass.Trim(),
                Server = server.Trim(),
                UserID = userid.Trim(),
                Port = prot.IsInt() ? prot.ToInt() : -1
            });

            return BLL_Database.TestDatabaseConnnection(string.Format("{0}({1})", server.Trim(), dbType.ToString()), out errMsg);
        }

        private void button_mysqltest_Click(object sender, EventArgs e)
        {
            this.button_mysqltest.Text = "测试中...";
            this.button_mysqltest.Enabled = false;

            string msg;
            if (!test_link_mysql(out msg))
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
            if (!test_link_mysql(out msg))
            {
                MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                Form1.form_Database.AddServer(string.Format("{0}({1})", this.comboBox_mysql_server.Text.Trim(), dbType.ToString()));
                this.Close();
                AddServer();
            }
        }

        /// <summary>
        /// 记录服务器设置
        /// </summary>
        private void AddServer()
        {
            string server = this.comboBox_mysql_server.Text;
            string userid = this.textBox_mysql_uid.Text;
            string userpass = this.textBox_mysql_pwd.Text;
            string prot = this.textBox_mysql_port.Text;
            Model.ConfigServers cs = new ConfigServers();
            cs.Name = string.Format("{0}({1})", server, dbType.ToString());
            cs.Port = prot;
            cs.Database = "";
            cs.file = "";
            cs.Pwd = this.checkBox1.Checked ? userpass : "";
            cs.ServerName = server;
            cs.Type = dbType.ToString();
            cs.Uid = userid;
            Form_AddDatabase.AddServerToXml(cs);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form_AddDatabase fa = new Form_AddDatabase();
            fa.ShowDialog();
            this.Close();
        }

        private void comboBox_mysql_server_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cs = csList.Where(p => p.ServerName == this.comboBox_mysql_server.Text);
            if (cs.Count() > 0)
            {
                this.comboBox_mysql_server.Text = cs.First().ServerName;
                this.textBox_mysql_pwd.Text = cs.First().Pwd;
                this.textBox_mysql_uid.Text = cs.First().Uid;
                this.checkBox1.Checked = !cs.First().Pwd.IsNullOrEmpty();
            }
        }
    }
}

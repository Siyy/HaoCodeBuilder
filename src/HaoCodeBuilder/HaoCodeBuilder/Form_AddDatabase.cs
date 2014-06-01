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
    public partial class Form_AddDatabase : Form
    {
        private DatabaseType DatabaseType = DatabaseType.Empty;
        public Form_AddDatabase()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            initDatabaseType();
            switch (this.DatabaseType)
            { 
                case Model.DatabaseType.SqlServer2000:
                case Model.DatabaseType.SqlServer2005:
                case Model.DatabaseType.SqlServer2008:
                    this.Close();
                    Form_AddDatabase_SqlServer fasql = new Form_AddDatabase_SqlServer(this.DatabaseType);
                    fasql.ShowDialog();
                    break;
                case Model.DatabaseType.MySql:
                    this.Close();
                    Form_AddDatabase_MySql famysql = new Form_AddDatabase_MySql(this.DatabaseType);
                    famysql.ShowDialog();
                    break;
                case Model.DatabaseType.Sqlite:
                    this.Close();
                    Form_AddDatabase_Sqlite fasqlite = new Form_AddDatabase_Sqlite(this.DatabaseType);
                    fasqlite.ShowDialog();
                    break;
            }
        }

        private void initDatabaseType()
        {
            if (this.radioButton_sqlserver2000.Checked)
            {
                this.DatabaseType = DatabaseType.SqlServer2000;
            }
            else if (this.radioButton_sqlserver2005.Checked)
            {
                this.DatabaseType = DatabaseType.SqlServer2005;
            }
            else if (this.radioButton_sqlserver2008.Checked)
            {
                this.DatabaseType = DatabaseType.SqlServer2008;
            }
            else if (this.radioButton_access.Checked)
            {
                this.DatabaseType = DatabaseType.Access;
            }
            else if (this.radioButton_mysql.Checked)
            {
                this.DatabaseType = DatabaseType.MySql;
            }
            else if (this.radioButton_oracle.Checked)
            {
                this.DatabaseType = DatabaseType.Oracle;
            }
            else if (this.radioButton_sqlite.Checked)
            {
                this.DatabaseType = DatabaseType.Sqlite;
            }
        }

        private void Form_AddDatabase_Load(object sender, EventArgs e)
        {
            //设定默认选择项
            var server = new Common.Config_Servers().GetDefault();
            if (server != null)
            {
                foreach (Control c in this.panel_selectdbtype.Controls)
                {
                    if (c.GetType().Name=="RadioButton" && c.Text.Replace(" ", "") == server.Type)
                    {
                        ((RadioButton)c).Checked = true;
                    }
                }
            }
        }

        public static void AddServerToXml(Model.ConfigServers cs)
        {
            new Common.Config_Servers().Add(cs);
        }
       
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HaoCodeBuilder
{
    public partial class Form_SetServers : Form
    {
        public Form_SetServers()
        {
            InitializeComponent();
        }

        private void Form_SetServers_Load(object sender, EventArgs e)
        {
            this.listView1.Columns[0].Width = 239;
            this.listView1.Columns[1].Width = 120;
            this.listView1.Columns[2].Width = 192;
            this.listView1.Columns[3].Width = 112;
            refresh();
        }

        private void refresh()
        {
            this.listView1.Items.Clear();
            var list = new Common.Config_Servers().GetAll();
            foreach (var li in list)
            {
                this.listView1.Items.Add(new ListViewItem(
                        new string[]{
                            li.Name,
                            li.Type,
                            li.ServerName,
                            li.Uid
                        }
                    ));
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                new Common.Config_Servers().Delete(this.listView1.SelectedItems[0].Text);
                refresh();
            }

        }


    }
}

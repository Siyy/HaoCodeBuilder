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
    public partial class Form_SetDirectory : Form
    {
        public Form_SetDirectory()
        {
            InitializeComponent();
        }

        private void Form_SetDirectory_Load(object sender, EventArgs e)
        {
            refresh();
        }

        private void refresh()
        {
            var list = new Common.Config_Directory().GetAll();
            this.listView1.Items.Clear();
            foreach (var li in list)
            {
                this.listView1.Items.Add(new ListViewItem(new string[] { 
                    li.Name
                }));
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
                new Common.Config_Directory().Delete(this.listView1.SelectedItems[0].Text);
                refresh();
            }
        }
    }
}

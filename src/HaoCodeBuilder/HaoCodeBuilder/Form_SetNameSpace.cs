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
    public partial class Form_SetNameSpace : Form
    {
        public Form_SetNameSpace()
        {
            InitializeComponent();
        }
        private Common.Config_NameSpace config_nameSpace = new Common.Config_NameSpace();
        private void Form_SetNameSpace_Load(object sender, EventArgs e)
        {
            this.listView1.Columns[0].Width = 226;
            this.listView1.Columns[1].Width = 226;
            ShowList();
        }

        private void ShowList()
        {
            this.listView1.Items.Clear();
            var nlist = config_nameSpace.GetAll();
            foreach (var list in nlist)
            {
                this.listView1.Items.Add(new ListViewItem(new string[] { 
                    list.Name1,
                    list.Name2
                }));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Model.ConfigNameSpace cns = GetModel();
            if (cns != null)
            {
                if (config_nameSpace.Add(cns))
                {
                    MessageBox.Show("添加成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowList();
                }
                else
                {
                    MessageBox.Show("添加失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private Model.ConfigNameSpace GetModel()
        { 
            string n1 = this.textBox_n1.Text;
            string n2 = this.textBox_n2.Text;

            if (n1.IsNullOrEmpty())
            {
                MessageBox.Show("命名空间不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            
            Model.ConfigNameSpace cns = new Model.ConfigNameSpace();
            cns.Name1 = n1.Trim();
            cns.Name2 = n2.Trim();
            return cns;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var items = listView1.SelectedItems;
            if (items.Count == 0)
                return;
            var item = items[0];
            this.textBox_n1.Text = item.SubItems[0].Text;
            this.textBox_n2.Text = item.SubItems[1].Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ShowList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.textBox_n1.Text.IsNullOrEmpty())
            {
                MessageBox.Show("您没有选择要删除的项!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (config_nameSpace.Delete(this.textBox_n1.Text.Trim()))
            {
                MessageBox.Show("删除成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowList();
            }
            else
            {
                MessageBox.Show("删除失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var items = listView1.SelectedItems;
            if (items.Count == 0)
                return;
            var item = items[0];

            Model.ConfigNameSpace cns = GetModel();
            if (cns != null)
            {
                if (config_nameSpace.Save(cns,item.SubItems[0].Text))
                {
                    MessageBox.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowList();
                }
                else
                {
                    MessageBox.Show("保存失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }



    }
}

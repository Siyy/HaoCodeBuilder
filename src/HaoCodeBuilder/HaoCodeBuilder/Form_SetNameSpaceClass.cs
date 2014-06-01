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
    public partial class Form_SetNameSpaceClass : Form
    {
        public Form_SetNameSpaceClass()
        {
            InitializeComponent();
        }
        private Common.Config_NameSpaceClass CNSC = new Common.Config_NameSpaceClass();
        private void Form_SetNameSpaceClass_Load(object sender, EventArgs e)
        {
            this.listView1.Columns[0].Width = 100;
            this.listView1.Columns[1].Width = 100;
            this.listView1.Columns[2].Width = 100;
            this.listView1.Columns[3].Width = 100;
            this.listView1.Columns[4].Width = 100;
            ShowList();
        }
        private void ShowList()
        {
            this.listView1.Items.Clear();
            var nlist = CNSC.GetAll();
            foreach (var list in nlist)
            {
                this.listView1.Items.Add(new ListViewItem(new string[] { 
                    list.Model,
                    list.Data,
                    list.Business,
                    list.Interface,
                    list.Factory
                }));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Model.ConfigNameSpaceClass cnsc = GetModel();
            if (cnsc != null)
            {
                if (CNSC.Add(cnsc))
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

        private Model.ConfigNameSpaceClass GetModel()
        {
            string model = this.textBox_model.Text;
            string data = this.textBox_data.Text;
            string business = this.textBox_business.Text;
            string interface1 = this.textBox_interface.Text;
            string factory = this.textBox_factory.Text;

            if (model.IsNullOrEmpty())
            {
                MessageBox.Show("实体层命名不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            if (data.IsNullOrEmpty())
            {
                MessageBox.Show("数据层命名不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            if (business.IsNullOrEmpty())
            {
                MessageBox.Show("业务层命名不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

            Model.ConfigNameSpaceClass cnsc = new Model.ConfigNameSpaceClass();
            cnsc.Model = model.Trim();
            cnsc.Data = data.Trim();
            cnsc.Business = business.Trim();
            cnsc.Interface = interface1.Trim();
            cnsc.Factory = factory.Trim();
            return cnsc;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var items = listView1.SelectedItems;
            if (items.Count == 0)
                return;
            var item = items[0];
            Model.ConfigNameSpaceClass cnsc = GetModel();
            if (cnsc != null)
            {
                if (CNSC.Save(cnsc,item.SubItems[0].Text))
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.textBox_model.Text.IsNullOrEmpty())
            {
                MessageBox.Show("您没有选择要删除的项!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (CNSC.Delete(this.textBox_model.Text.Trim()))
            {
                MessageBox.Show("删除成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowList();
            }
            else
            {
                MessageBox.Show("删除失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ShowList();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var items = listView1.SelectedItems;
            if (items.Count == 0)
                return;
            var item = items[0];
            this.textBox_model.Text = item.SubItems[0].Text;
            this.textBox_data.Text = item.SubItems[1].Text;
            this.textBox_business.Text = item.SubItems[2].Text;
            this.textBox_interface.Text = item.SubItems[3].Text;
            this.textBox_factory.Text = item.SubItems[4].Text;
        }
    }
}

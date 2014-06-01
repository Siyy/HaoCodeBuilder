using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace HaoCodeBuilder
{
    public partial class Form_Code_SetDir : Form
    {
        public Form_Code_SetDir()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void Form_Code_SetDir_Load(object sender, EventArgs e)
        {
            Form.CheckForIllegalCrossThreadCalls = false;
            this.label8.Text = "";

            //加载默认命名空间
            Model.ConfigNameSpace cnsDefault = new Common.Config_NameSpace().GetDefault();
            if (cnsDefault != null)
            {
                
            }

            //加载目录
            var dirDefault = new Common.Config_Directory().GetDefault();
            if (dirDefault != null)
            {
                this.textBox_dir.Text = dirDefault.Name;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.ShowDialog();
            this.textBox_dir.Text = this.folderBrowserDialog1.SelectedPath;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.button6.Enabled = false;
            if(!this.textBox_dir.Text.IsPath())
            {
                MessageBox.Show("项目目录为空或不合法!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.button6.Enabled = true;
                return;
            }

            AddDirectory();
            AddNameSpace();

            System.Threading.Thread th = new System.Threading.Thread(CreateToDir);
            th.Start();
        }

        private void AddDirectory()
        {
            new Common.Config_Directory().Add(new Model.ConfigDirectory() { Name = this.textBox_dir.Text.Trim() });
        }

        private void AddNameSpace()
        {
            new Common.Config_NameSpace().Add(new Model.ConfigNameSpace()
            {
                Name1 = "",
                Name2 = ""
            });
        }

        private void CreateToDir()
        {
            List<TreeNode> NodeList = Form1.form_Database.GetTreeView1Selected();
            if (NodeList.Count == 0)
            {
                return;
            }
            TreeNode serverNode = Form1.form_Database.GetRoot(NodeList.First());
            TreeNode dbNode = NodeList.First().Parent.Parent;

            List<Model.BuilderMethods> methods = new List<Model.BuilderMethods>();
            if (checkBox_add.Checked) methods.Add(Model.BuilderMethods.Add);
            if (checkBox_count.Checked) methods.Add(Model.BuilderMethods.Count);
            if (checkBox_delete.Checked) methods.Add(Model.BuilderMethods.Delete);
            if (checkBox_exists.Checked) methods.Add(Model.BuilderMethods.Exists);
            if (checkBox_getall.Checked) methods.Add(Model.BuilderMethods.SelectAll);
            if (checkBox_getbykey.Checked) methods.Add(Model.BuilderMethods.SelectByKey);
            if (checkBox_update.Checked) methods.Add(Model.BuilderMethods.Update);

            Model.Servers server = (Model.Servers)((Model.TreeNodeTag)serverNode.Tag).Tag;
            Business.CreateCode CreateCode = new Business.CreateCode(server.Type);
            Model.CodeCreate param = new Model.CodeCreate();

            param.DbName = ((Model.TreeNodeTag)dbNode.Tag).Tag.ToString();
            param.NameSpace = "";
            param.NameSpace1 = "";
            param.ServerID = server.ID;
            param.BuilderType = this.radioButton1.Checked ? Model.BuilderType.Default : Model.BuilderType.Factory;
            param.MethodList = methods;
            param.CNSC = new Common.Config_NameSpaceClass().GetDefault();

            Business.CreateCode CreateCodeInstince = new Business.CreateCode(server.Type);
            StreamWriter sw;
            string FileName = string.Empty;
            foreach (TreeNode node in NodeList)
            {
                param.TableName = ((Model.TreeNodeTag)node.Tag).Tag.ToString();
                param.ClassName = param.TableName;

                //生成实体类
                FileName = Common.Func.ExistsDirectory(string.Format("{0}\\{1}.{2}\\{3}{4}.cs", this.textBox_dir.Text, param.NameSpace, param.CNSC.Model, param.NameSpace1.IsNullOrEmpty() ? "" : param.NameSpace1 + "\\", param.ClassName));
                sw = File.CreateText(FileName);
                sw.Write(CreateCodeInstince.GetModelClass(param));
                sw.Close();
                sw.Dispose();
                label8.Text = string.Format("生成文件:{0}", FileName);

                //生成数据类
                FileName = Common.Func.ExistsDirectory(string.Format("{0}\\{1}.{2}\\{3}{4}.cs", this.textBox_dir.Text, param.NameSpace, param.CNSC.Data, param.NameSpace1.IsNullOrEmpty() ? "" : param.NameSpace1 + "\\", param.ClassName));
                sw = File.CreateText(FileName);
                sw.Write(CreateCodeInstince.GetDataClass(param));
                sw.Close();
                sw.Dispose();
                label8.Text = string.Format("生成文件:{0}", FileName);


                //生成业务类
                FileName = Common.Func.ExistsDirectory(string.Format("{0}\\{1}.{2}\\{3}{4}.cs", this.textBox_dir.Text, param.NameSpace, param.CNSC.Business, param.NameSpace1.IsNullOrEmpty() ? "" : param.NameSpace1 + "\\", param.ClassName));
                sw = File.CreateText(FileName);
                sw.Write(CreateCodeInstince.GetBusinessClass(param));
                sw.Close();
                sw.Dispose();
                label8.Text = string.Format("生成文件:{0}", FileName);

                if (param.BuilderType == Model.BuilderType.Factory)
                {
                    //生成接口类
                    FileName = Common.Func.ExistsDirectory(string.Format("{0}\\{1}.{2}\\{3}{4}.cs", this.textBox_dir.Text, param.NameSpace, param.CNSC.Interface, param.NameSpace1.IsNullOrEmpty() ? "" : param.NameSpace1 + "\\", param.ClassName));
                    sw = File.CreateText(FileName);
                    sw.Write(CreateCodeInstince.GetInterfaceClass(param));
                    sw.Close();
                    sw.Dispose();
                    label8.Text = string.Format("生成文件:{0}", FileName);

                    //生成工厂类
                    FileName = Common.Func.ExistsDirectory(string.Format("{0}\\{1}.{2}\\{3}{4}.cs", this.textBox_dir.Text, param.NameSpace, param.CNSC.Factory, param.NameSpace1.IsNullOrEmpty() ? "" : param.NameSpace1 + "\\", param.ClassName));
                    sw = File.CreateText(FileName);
                    sw.Write(CreateCodeInstince.GetFactoryClass(param));
                    sw.Close();
                    sw.Dispose();
                    label8.Text = string.Format("生成文件:{0}", FileName);
                }
            }
            MessageBox.Show("生成完成!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.label8.Text = "生成已完成";
            this.button6.Enabled = true;
        }
    }
}

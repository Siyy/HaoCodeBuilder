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
    public partial class Form_Code_SetText : Form
    {
        private TreeNode node = null;
        public Form_Code_SetText()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form_Code_SetText_Load(object sender, EventArgs e)
        {
            var selNode = Form1.form_Database.treeView1.SelectedNode;
            if (selNode == null || (((Model.TreeNodeTag)selNode.Tag).Type != Model.TreeNodeType.Table && ((Model.TreeNodeTag)selNode.Tag).Type != Model.TreeNodeType.View))
            {
                var nodes = Form1.form_Database.GetTreeView1Selected();
                if (nodes.Count > 0)
                {
                    node = nodes.First();
                }
            }
            else
            {
                node = selNode;
            }

            if (node == null)
            {
                this.Close();
            }
            else
            {
                this.textBox2.Text = ((Model.TreeNodeTag)node.Tag).Tag.ToString();
                this.Text = string.Format("生成代码至文本框--表:{0}", this.textBox2.Text);
            }
            this.label7.Text = "";
            Form.CheckForIllegalCrossThreadCalls = false;

            //加载默认命名空间
            Model.ConfigNameSpace cnsDefault = new Common.Config_NameSpace().GetDefault();
            if (cnsDefault != null)
            {
               
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.button1.Enabled = false;
            AddNameSpace();
            CreateCode();
        }

        private void AddNameSpace()
        {
            new Common.Config_NameSpace().Add(new Model.ConfigNameSpace()
            {
                Name1 = "",
                Name2 = ""
            });
        }

        private void CreateCode()
        {
            TreeNode dbNode = node.Parent.Parent;
            TreeNode serverNode = Form1.form_Database.GetRoot(node);
            if (dbNode == null || serverNode == null)
            {
                return;
            }
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
            param.ClassName = this.textBox2.Text.IsNullOrEmpty() ? ((Model.TreeNodeTag)node.Tag).Tag.ToString() : this.textBox2.Text.Trim();
            param.DbName = ((Model.TreeNodeTag)dbNode.Tag).Tag.ToString();
            param.NameSpace = "";
            param.NameSpace1 = "";
            param.ServerID = server.ID;
            param.TableName = ((Model.TreeNodeTag)node.Tag).Tag.ToString();
            param.BuilderType = this.radioButton1.Checked ? Model.BuilderType.Default : Model.BuilderType.Factory;
            param.MethodList = methods;
            param.CNSC = new Common.Config_NameSpaceClass().GetDefault();

            Form_Code_Area fca_model = new Form_Code_Area(CreateCode.GetModelClass(param), string.Format("实体类({0})", param.TableName));
            fca_model.Show(Form1.Instance.dockPanel1);
            
           
            Form_Code_Area fca_data = new Form_Code_Area(CreateCode.GetDataClass(param), string.Format("数据类({0})", param.TableName));
            fca_data.Show(Form1.Instance.dockPanel1);
            

            Form_Code_Area fca_business = new Form_Code_Area(CreateCode.GetBusinessClass(param), string.Format("业务类({0})", param.TableName));
            fca_business.Show(Form1.Instance.dockPanel1);
            

            if (param.BuilderType == Model.BuilderType.Factory)
            {
                Form_Code_Area fca_interface = new Form_Code_Area(CreateCode.GetInterfaceClass(param), string.Format("接口类({0})", param.TableName));
                fca_interface.Show(Form1.Instance.dockPanel1);
                

                Form_Code_Area fca_factory = new Form_Code_Area(CreateCode.GetFactoryClass(param), string.Format("工厂类({0})", param.TableName));
                fca_factory.Show(Form1.Instance.dockPanel1);
                
            }
            this.Close();
        }
    }
}

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
    public partial class Form_Database : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        private delegate void AddTreeNode(bool refresh);
        public Form_Database()
        {
            InitializeComponent();
        }

        private void Form_Database_Load(object sender, EventArgs e)
        {
            Form.CheckForIllegalCrossThreadCalls = false;

            var serverList = new Common.Config_Servers().GetAll();
            foreach (var server in serverList)
            {
               

                Model.Servers ser = new Servers();
                ser.DatabaseName = server.Database;
                ser.ID = server.Name;
                ser.Name = server.ServerName;
                ser.Password = server.Pwd;
                ser.Port = server.Port.IsInt() ? server.Port.ToInt() : -1;
                ser.Server = server.ServerName;
                Model.DatabaseType dbtype;
                ser.Type = Enum.TryParse(server.Type, out dbtype) ? dbtype : DatabaseType.Empty;
                ser.UserID = server.Uid;

                Common.Config.AddServerList(ser);

                TreeNode RootNode = new TreeNode();
                RootNode.Name = server.ServerName;
                RootNode.Text = string.Format("{0}({1}{2})", server.ServerName, server.Type.ToString(), server.Uid.IsNullOrEmpty() ? "" : string.Format("-{0}", server.Uid));
                RootNode.ImageIndex = 0;
                RootNode.SelectedImageIndex = 0;
                RootNode.Tag = new Model.TreeNodeTag() { Type = TreeNodeType.Server, Tag = ser };
                treeView1.Nodes.Add(RootNode);

            }
        }

        public void toolStripButton1_Click(object sender, EventArgs e)
        {
            Form_AddDatabase f_adddb = new Form_AddDatabase();
            f_adddb.ShowDialog();
        }

        /// <summary>
        /// 添加一个服务器到tree
        /// </summary>
        /// <param name="serverID"></param>
        public void AddServer(string serverID)
        {
            Servers server = Common.Config.GetServer(serverID);
            if (server == null)
            {
                return;
            }

            treeView1.Nodes.RemoveByKey(server.ID);

            TreeNode RootNode = new TreeNode();
            RootNode.Name = server.ID;
            RootNode.Text = string.Format("{0}({1}{2})", server.Name, server.Type.ToString(), server.UserID.IsNullOrEmpty() ? "" : string.Format("-{0}", server.UserID));
            RootNode.ImageIndex = 0;
            RootNode.SelectedImageIndex = 0;
            RootNode.Tag = new Model.TreeNodeTag() { Type = TreeNodeType.Server, Tag = server };
            treeView1.Nodes.Add(RootNode);

            Business.Database Database = new Business.Database(server.Type);
            //添加数据库
            List<string> dbList = Database.GetDatabases(serverID);
            foreach (var db in dbList)
            {
                TreeNode dbNode = new TreeNode();
                dbNode.Name = db;
                dbNode.Text = db;
                dbNode.Tag = new Model.TreeNodeTag() { Type = TreeNodeType.DataBase, Tag = db };
                dbNode.ImageIndex = 1;
                dbNode.SelectedImageIndex = 1;
                RootNode.Nodes.Add(dbNode);

                //添加表节点
                TreeNode tblNode = new TreeNode();
                tblNode.Name = "表";
                tblNode.Text = "表";
                tblNode.Tag = new Model.TreeNodeTag() { Type = TreeNodeType.TableNode, Tag = db };
                tblNode.ImageIndex = 4;
                tblNode.SelectedImageIndex = 4;
                dbNode.Nodes.Add(tblNode);

                //添加视图节点
                TreeNode viewNode = new TreeNode();
                viewNode.Name = "视图";
                viewNode.Text = "视图";
                viewNode.Tag = new Model.TreeNodeTag() { Type = TreeNodeType.ViewNode, Tag = db };
                viewNode.ImageIndex = 4;
                viewNode.SelectedImageIndex = 4;
                dbNode.Nodes.Add(viewNode);
            }
            RootNode.Expand();
            Form1.Instance.ShowServerList();
        }

        
        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            AddTreeNode addnode = new AddTreeNode(AddNodes);
            this.treeView1.BeginInvoke(addnode, false);
           
        }
        
        /// <summary>
        /// 加载下级节点
        /// </summary>
        private void AddNodes(bool isRefresh = false)
        {
            
            TreeNode selNode = treeView1.SelectedNode;
            if (selNode == null) return;
            if (selNode.Nodes.Count > 0 && !isRefresh) return;
    
            selNode.Nodes.Clear();
            TreeNode rootNode = GetRoot(selNode);
            if (rootNode == null) return;
            Model.TreeNodeTag rootNodeTag = (Model.TreeNodeTag)rootNode.Tag;
            if (!(rootNodeTag.Tag is Model.Servers)) return;
            Model.Servers server = (Model.Servers)rootNodeTag.Tag;
            Model.TreeNodeTag nodeTag = (Model.TreeNodeTag)selNode.Tag;
            Business.Database Database = new Business.Database(server.Type);
            string msg;
            if (!Database.TestDatabaseConnnection(server.ID, out msg))
            {
                MessageBox.Show(msg, "连接失败", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            switch (nodeTag.Type)
            {
                case TreeNodeType.Server: //服务器加载数据库
                    var dbs = Database.GetDatabases(server.ID);
                    foreach (var db in dbs)
                    {
                        TreeNode dbNode = new TreeNode();
                        dbNode.Name = db;
                        dbNode.Text = db;
                        dbNode.ImageIndex = 1;
                        dbNode.SelectedImageIndex = 1;
                        dbNode.Tag = new Model.TreeNodeTag() { Type = TreeNodeType.DataBase, Tag = db };
                        selNode.Nodes.Add(dbNode);
                    }
                    break;
                case TreeNodeType.DataBase: //数据库加载表视图节点
                    //添加表节点
                    TreeNode tblNode = new TreeNode();
                    tblNode.Name = "表";
                    tblNode.Text = "表";
                    tblNode.Tag = new Model.TreeNodeTag() { Type = TreeNodeType.TableNode, Tag = nodeTag.Tag.ToString()};
                    tblNode.ImageIndex = 4;
                    tblNode.SelectedImageIndex = 4;
                    selNode.Nodes.Add(tblNode);

                    //添加视图节点
                    TreeNode viewNode = new TreeNode();
                    viewNode.Name = "视图";
                    viewNode.Text = "视图";
                    viewNode.Tag = new Model.TreeNodeTag() { Type = TreeNodeType.ViewNode, Tag = nodeTag.Tag.ToString() };
                    viewNode.ImageIndex = 4;
                    viewNode.SelectedImageIndex = 4;
                    selNode.Nodes.Add(viewNode);
                    break;
                case TreeNodeType.TableNode: //表节点加载表
                    var tables = Database.GetTables(server.ID, nodeTag.Tag.ToString());
                    foreach (var table in tables)
                    {
                        TreeNode tblNode1 = new TreeNode();
                        tblNode1.Name = table.Name;
                        tblNode1.Text = table.Name;
                        tblNode1.ImageIndex = 2;
                        tblNode1.SelectedImageIndex = 2;
                        tblNode1.Tag = new Model.TreeNodeTag() { Type = TreeNodeType.Table, Tag = table.Name };
                        selNode.Nodes.Add(tblNode1);
                    }
                    break;
                case TreeNodeType.ViewNode: //视图节点加载视图
                    var views = Database.GetViews(server.ID, nodeTag.Tag.ToString());
                    foreach (var view in views)
                    {
                        TreeNode viewNode1 = new TreeNode();
                        viewNode1.Name = view.Name;
                        viewNode1.Text = view.Name;
                        viewNode1.ImageIndex = 2;
                        viewNode1.SelectedImageIndex = 2;
                        viewNode1.Tag = new Model.TreeNodeTag() { Type = TreeNodeType.View, Tag = view.Name };
                        selNode.Nodes.Add(viewNode1);
                    }
                    break;
                case TreeNodeType.View:
                case TreeNodeType.Table: //表加载字段
                    var fields = Database.GetFields(server.ID, ((Model.TreeNodeTag)selNode.Parent.Tag).Tag.ToString(), ((Model.TreeNodeTag)selNode.Tag).Tag.ToString());
                    foreach (var field in fields)
                    {
                        TreeNode fldNode = new TreeNode();
                        fldNode.Name = field.Name;
                        fldNode.Text = string.Format("{0}({1}{2},{3})", field.Name, field.Type, field.Length != -1 ? "("+field.Length.ToString()+")" : "", field.IsNull ? "null" : "not null");
                        fldNode.ImageIndex = field.IsPrimaryKey ? 5 : 3;
                        fldNode.SelectedImageIndex = field.IsPrimaryKey ? 5 : 3;
                        fldNode.Tag = new Model.TreeNodeTag() { Type = TreeNodeType.Field, Tag = field };
                        selNode.Nodes.Add(fldNode);
                    }
                    break;
            }
            selNode.Expand();
            
        }
        /// <summary>
        /// 得到一个节点的根节点
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public TreeNode GetRoot(TreeNode node)
        {
            TreeNode node1 = node;
            while (node1.Parent != null)
            {
                node1 = node1.Parent;
            }
            return node1;
        }
        /// <summary>
        /// 注销数据库
        /// </summary>
        public void RemoveServer()
        {
            if (treeView1.SelectedNode == null) return;
            TreeNode rootNode = GetRoot(treeView1.SelectedNode);
           
            if (rootNode == null ) return;
            Model.TreeNodeTag tag = (Model.TreeNodeTag)rootNode.Tag;
            if (tag.Type != TreeNodeType.Server) return;
            Model.Servers server = (Model.Servers)tag.Tag;
            new Common.Config_Servers().Delete(server.ID);
            rootNode.Remove();
        }
        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNodes(true);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            RemoveServer();
        }

        private void 注销连接ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveServer();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            AddNodes(true);
        }

        private void 生成代码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowCodeText();
        }

        public void ShowCodeText()
        {
            var Nodes = Form1.form_Database.GetTreeView1Selected();
            if (Nodes.Count == 0)
            {
                var selNode = Form1.form_Database.treeView1.SelectedNode;
                if (selNode == null || (((Model.TreeNodeTag)selNode.Tag).Type != Model.TreeNodeType.Table && ((Model.TreeNodeTag)selNode.Tag).Type != Model.TreeNodeType.View))
                {
                    MessageBox.Show("请选择要生成代码的表或视图!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form1.Instance.ShowServerList();
                    return;
                }
            }

            Form_Code_SetText fcst = new Form_Code_SetText();
            fcst.ShowDialog();
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TreeNode node = e.Node;
            foreach (TreeNode n in node.Nodes)
            {
                n.Checked = node.Checked;
            }
            
        }

        public void ShowCodeDir()
        {
            List<TreeNode> Nodes = Form1.form_Database.GetTreeView1Selected();
            if (Nodes.Count == 0)
            {
                var selNode = Form1.form_Database.treeView1.SelectedNode;
                if (selNode == null || (((Model.TreeNodeTag)selNode.Tag).Type != Model.TreeNodeType.Table && ((Model.TreeNodeTag)selNode.Tag).Type != Model.TreeNodeType.View))
                {
                    MessageBox.Show("请选择要生成代码的表或视图!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form1.Instance.ShowServerList();
                    return;
                }
                else
                {
                    Nodes.Add(selNode);
                }
            }
            Form_Code_SetDir fcsd = new Form_Code_SetDir();
            fcsd.ShowDialog();
        }

        private void 生成代码至目录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowCodeDir();
        }
        /// <summary>
        /// 得到选择节点表集合
        /// </summary>
        /// <returns></returns>
        public List<TreeNode> GetTreeView1Selected()
        {
            List<TreeNode> list = new List<TreeNode>();
            TreeNodeCollection rootNodes = treeView1.Nodes;
            foreach (TreeNode n in rootNodes)
            {
                if (n.Checked && (((Model.TreeNodeTag)n.Tag).Type == TreeNodeType.Table || ((Model.TreeNodeTag)n.Tag).Type == TreeNodeType.View))
                {
                    list.Add(n);
                }
                AddSelectedNodes(n, list);
            }
            return list;
        }

        private void AddSelectedNodes(TreeNode node, List<TreeNode> list)
        {
            TreeNodeCollection nodes = node.Nodes;
            foreach (TreeNode n in nodes)
            {
                if (n.Checked && (((Model.TreeNodeTag)n.Tag).Type == TreeNodeType.Table || ((Model.TreeNodeTag)n.Tag).Type == TreeNodeType.View))
                {
                    list.Add(n);
                }
                AddSelectedNodes(n, list);
            }
        }

        
    }
}

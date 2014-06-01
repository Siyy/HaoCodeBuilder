using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace HaoCodeBuilder
{
    public partial class Form1 : DockContent
    {
        public static Form1 Instance = null;
        public static Form_Database form_Database = null;
        public static Form_Home form_Home = null;
        public static Form_TemplateTree form_TemplateTree = null;
        public Form1()
        {
            InitializeComponent();
            Instance = this;
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            form_Database = new Form_Database();
            form_Database.Show(dockPanel1, DockState.DockLeft);

            form_Home = new Form_Home();
            form_Home.Show(dockPanel1);
            form_Home.Activate();
            
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ShowServerList();
        }

        /// <summary>
        /// 显示服务器资源管理器
        /// </summary>
        public void ShowServerList()
        {
            if (form_Database == null)
            {
                form_Database = new Form_Database();
                form_Database.Show(dockPanel1, DockState.DockLeftAutoHide);
            }
            form_Database.Activate();
        }

       
        /// <summary>
        /// 显示模板管理器
        /// </summary>
        public void ShowTemplate()
        {
            if (form_TemplateTree == null)
            {
                form_TemplateTree = new Form_TemplateTree();
                form_TemplateTree.Show(dockPanel1, DockState.DockLeftAutoHide);
            }
            form_TemplateTree.Activate();
        }

        /// <summary>
        /// 显示起始页
        /// </summary>
        public void ShowHome()
        {
            if (form_Home == null)
            {
                form_Home = new Form_Home();
                form_Home.Show(dockPanel1);
            }
            
            form_Home.Activate();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ShowHome();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            form_Database.ShowCodeText();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            form_Database.ShowCodeDir();
        }

        private void 添加数据库服务器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form_Database.toolStripButton1_Click(sender, e);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void 注销数据库服务器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form_Database.RemoveServer();
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            form_Database.toolStripButton1_Click(sender, e);
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            form_Database.RemoveServer();
        }

        /// <summary>
        /// 退出系统
        /// </summary>
        private void Exit()
        {
            Application.Exit();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Form_SetNameSpace fs = new Form_SetNameSpace();
            fs.ShowDialog();
        }

        private void 命名空间配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton3_Click(sender, e);
        }

        private void 生成选中表至目录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form_Database.ShowCodeDir();
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            Form_SetNameSpaceClass fssc = new Form_SetNameSpaceClass();
            fssc.ShowDialog();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            toolStripButton12_Click(sender, e);
        }

        private void 关于我们ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton10_Click(sender, e);
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            Form_About fa = new Form_About();
            fa.ShowDialog();
        }

        private void 数据库服务器配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton7_Click(sender, e);
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            Form_SetServers fss = new Form_SetServers();
            fss.ShowDialog();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            Form_SetDirectory fsd = new Form_SetDirectory();
            fsd.ShowDialog();
        }

        private void 保存目录配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton6_Click(sender, e);
        }

        private void 生成选中表至文本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form_Database.ShowCodeText();
        }

        private void 起始页ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHome();
        }

        private void 数据库服务器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowServerList();
        }

        private void dockPanel1_ActiveContentChanged(object sender, EventArgs e)
        {

        }

        
    }
}

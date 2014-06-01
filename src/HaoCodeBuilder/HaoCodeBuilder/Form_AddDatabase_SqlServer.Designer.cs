namespace HaoCodeBuilder
{
    partial class Form_AddDatabase_SqlServer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel_sqlserver = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button_sqlserver_add = new System.Windows.Forms.Button();
            this.button_sqlserver_test = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.sqlserver_pwd = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.sqlserver_uid = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.sqlserver_server = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panel_sqlserver.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_sqlserver
            // 
            this.panel_sqlserver.Controls.Add(this.checkBox1);
            this.panel_sqlserver.Controls.Add(this.label8);
            this.panel_sqlserver.Controls.Add(this.label7);
            this.panel_sqlserver.Controls.Add(this.button_sqlserver_add);
            this.panel_sqlserver.Controls.Add(this.button_sqlserver_test);
            this.panel_sqlserver.Controls.Add(this.button2);
            this.panel_sqlserver.Controls.Add(this.sqlserver_pwd);
            this.panel_sqlserver.Controls.Add(this.label5);
            this.panel_sqlserver.Controls.Add(this.sqlserver_uid);
            this.panel_sqlserver.Controls.Add(this.label4);
            this.panel_sqlserver.Controls.Add(this.sqlserver_server);
            this.panel_sqlserver.Controls.Add(this.label3);
            this.panel_sqlserver.Controls.Add(this.label2);
            this.panel_sqlserver.Location = new System.Drawing.Point(12, 12);
            this.panel_sqlserver.Name = "panel_sqlserver";
            this.panel_sqlserver.Size = new System.Drawing.Size(412, 265);
            this.panel_sqlserver.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(171, 37);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 25;
            this.label8.Text = "label8";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(90, 38);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 24;
            this.label7.Text = "数据库版本：";
            // 
            // button_sqlserver_add
            // 
            this.button_sqlserver_add.Location = new System.Drawing.Point(264, 209);
            this.button_sqlserver_add.Name = "button_sqlserver_add";
            this.button_sqlserver_add.Size = new System.Drawing.Size(75, 23);
            this.button_sqlserver_add.TabIndex = 23;
            this.button_sqlserver_add.Text = "确认添加";
            this.button_sqlserver_add.UseVisualStyleBackColor = true;
            this.button_sqlserver_add.Click += new System.EventHandler(this.button_sqlserver_add_Click);
            // 
            // button_sqlserver_test
            // 
            this.button_sqlserver_test.Location = new System.Drawing.Point(183, 209);
            this.button_sqlserver_test.Name = "button_sqlserver_test";
            this.button_sqlserver_test.Size = new System.Drawing.Size(75, 23);
            this.button_sqlserver_test.TabIndex = 22;
            this.button_sqlserver_test.Text = "测试连接";
            this.button_sqlserver_test.UseVisualStyleBackColor = true;
            this.button_sqlserver_test.Click += new System.EventHandler(this.button_sqlserver_test_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(102, 209);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 21;
            this.button2.Text = "上一步";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // sqlserver_pwd
            // 
            this.sqlserver_pwd.Location = new System.Drawing.Point(173, 138);
            this.sqlserver_pwd.Name = "sqlserver_pwd";
            this.sqlserver_pwd.PasswordChar = '*';
            this.sqlserver_pwd.Size = new System.Drawing.Size(161, 21);
            this.sqlserver_pwd.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(126, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 17;
            this.label5.Text = "密码：";
            // 
            // sqlserver_uid
            // 
            this.sqlserver_uid.Location = new System.Drawing.Point(173, 100);
            this.sqlserver_uid.Name = "sqlserver_uid";
            this.sqlserver_uid.Size = new System.Drawing.Size(161, 21);
            this.sqlserver_uid.TabIndex = 16;
            this.sqlserver_uid.Text = "sa";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(114, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 15;
            this.label4.Text = "登录名：";
            // 
            // sqlserver_server
            // 
            this.sqlserver_server.FormattingEnabled = true;
            this.sqlserver_server.Location = new System.Drawing.Point(173, 64);
            this.sqlserver_server.Name = "sqlserver_server";
            this.sqlserver_server.Size = new System.Drawing.Size(161, 20);
            this.sqlserver_server.TabIndex = 14;
            this.sqlserver_server.Text = ".\\SQLEXPRESS";
            this.sqlserver_server.SelectedIndexChanged += new System.EventHandler(this.sqlserver_server_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(114, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "服务器：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(115, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "设置SqlServer连接属性：";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(173, 177);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 26;
            this.checkBox1.Text = "记住密码";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // Form_AddDatabase_SqlServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 289);
            this.Controls.Add(this.panel_sqlserver);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_AddDatabase_SqlServer";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "添加SqlServer数据库服务器";
            this.Load += new System.EventHandler(this.Form_AddDatabase_SqlServer_Load);
            this.panel_sqlserver.ResumeLayout(false);
            this.panel_sqlserver.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_sqlserver;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button_sqlserver_add;
        private System.Windows.Forms.Button button_sqlserver_test;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox sqlserver_pwd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox sqlserver_uid;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox sqlserver_server;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}
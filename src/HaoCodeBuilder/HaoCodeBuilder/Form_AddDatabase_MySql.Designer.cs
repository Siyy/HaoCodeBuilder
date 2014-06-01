namespace HaoCodeBuilder
{
    partial class Form_AddDatabase_MySql
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
            this.panel_mysql = new System.Windows.Forms.Panel();
            this.button_mysqladd = new System.Windows.Forms.Button();
            this.button_mysqltest = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.textBox_mysql_port = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox_mysql_pwd = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox_mysql_uid = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBox_mysql_server = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panel_mysql.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_mysql
            // 
            this.panel_mysql.Controls.Add(this.checkBox1);
            this.panel_mysql.Controls.Add(this.button_mysqladd);
            this.panel_mysql.Controls.Add(this.button_mysqltest);
            this.panel_mysql.Controls.Add(this.button5);
            this.panel_mysql.Controls.Add(this.textBox_mysql_port);
            this.panel_mysql.Controls.Add(this.label12);
            this.panel_mysql.Controls.Add(this.textBox_mysql_pwd);
            this.panel_mysql.Controls.Add(this.label9);
            this.panel_mysql.Controls.Add(this.textBox_mysql_uid);
            this.panel_mysql.Controls.Add(this.label10);
            this.panel_mysql.Controls.Add(this.comboBox_mysql_server);
            this.panel_mysql.Controls.Add(this.label11);
            this.panel_mysql.Controls.Add(this.label6);
            this.panel_mysql.Location = new System.Drawing.Point(12, 12);
            this.panel_mysql.Name = "panel_mysql";
            this.panel_mysql.Size = new System.Drawing.Size(422, 265);
            this.panel_mysql.TabIndex = 3;
            // 
            // button_mysqladd
            // 
            this.button_mysqladd.Location = new System.Drawing.Point(263, 213);
            this.button_mysqladd.Name = "button_mysqladd";
            this.button_mysqladd.Size = new System.Drawing.Size(75, 23);
            this.button_mysqladd.TabIndex = 29;
            this.button_mysqladd.Text = "确认添加";
            this.button_mysqladd.UseVisualStyleBackColor = true;
            this.button_mysqladd.Click += new System.EventHandler(this.button_mysqladd_Click);
            // 
            // button_mysqltest
            // 
            this.button_mysqltest.Location = new System.Drawing.Point(182, 213);
            this.button_mysqltest.Name = "button_mysqltest";
            this.button_mysqltest.Size = new System.Drawing.Size(75, 23);
            this.button_mysqltest.TabIndex = 28;
            this.button_mysqltest.Text = "测试连接";
            this.button_mysqltest.UseVisualStyleBackColor = true;
            this.button_mysqltest.Click += new System.EventHandler(this.button_mysqltest_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(101, 213);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 27;
            this.button5.Text = "上一步";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // textBox_mysql_port
            // 
            this.textBox_mysql_port.Location = new System.Drawing.Point(169, 149);
            this.textBox_mysql_port.Name = "textBox_mysql_port";
            this.textBox_mysql_port.Size = new System.Drawing.Size(161, 21);
            this.textBox_mysql_port.TabIndex = 26;
            this.textBox_mysql_port.Text = "3306";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(122, 152);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 12);
            this.label12.TabIndex = 25;
            this.label12.Text = "端口：";
            // 
            // textBox_mysql_pwd
            // 
            this.textBox_mysql_pwd.Location = new System.Drawing.Point(169, 114);
            this.textBox_mysql_pwd.Name = "textBox_mysql_pwd";
            this.textBox_mysql_pwd.PasswordChar = '*';
            this.textBox_mysql_pwd.Size = new System.Drawing.Size(161, 21);
            this.textBox_mysql_pwd.TabIndex = 24;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(122, 117);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 23;
            this.label9.Text = "密码：";
            // 
            // textBox_mysql_uid
            // 
            this.textBox_mysql_uid.Location = new System.Drawing.Point(169, 76);
            this.textBox_mysql_uid.Name = "textBox_mysql_uid";
            this.textBox_mysql_uid.Size = new System.Drawing.Size(161, 21);
            this.textBox_mysql_uid.TabIndex = 22;
            this.textBox_mysql_uid.Text = "root";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(110, 79);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 21;
            this.label10.Text = "登录名：";
            // 
            // comboBox_mysql_server
            // 
            this.comboBox_mysql_server.FormattingEnabled = true;
            this.comboBox_mysql_server.Location = new System.Drawing.Point(169, 40);
            this.comboBox_mysql_server.Name = "comboBox_mysql_server";
            this.comboBox_mysql_server.Size = new System.Drawing.Size(161, 20);
            this.comboBox_mysql_server.TabIndex = 20;
            this.comboBox_mysql_server.Text = "127.0.0.1";
            this.comboBox_mysql_server.SelectedIndexChanged += new System.EventHandler(this.comboBox_mysql_server_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(110, 43);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 19;
            this.label11.Text = "服务器：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(126, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(131, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "设置MySql连接属性：";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(169, 182);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 30;
            this.checkBox1.Text = "记住密码";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // Form_AddDatabase_MySql
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 289);
            this.Controls.Add(this.panel_mysql);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_AddDatabase_MySql";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "添加MySql数据库服务器";
            this.Load += new System.EventHandler(this.Form_AddDatabase_MySql_Load);
            this.panel_mysql.ResumeLayout(false);
            this.panel_mysql.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_mysql;
        private System.Windows.Forms.Button button_mysqladd;
        private System.Windows.Forms.Button button_mysqltest;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox textBox_mysql_port;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox_mysql_pwd;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox_mysql_uid;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBox_mysql_server;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}
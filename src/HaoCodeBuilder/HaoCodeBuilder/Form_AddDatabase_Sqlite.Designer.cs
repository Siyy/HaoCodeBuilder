namespace HaoCodeBuilder
{
    partial class Form_AddDatabase_Sqlite
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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button_mysqladd = new System.Windows.Forms.Button();
            this.button_mysqltest = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.textBox_pwd = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_file = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel_mysql.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_mysql
            // 
            this.panel_mysql.Controls.Add(this.button1);
            this.panel_mysql.Controls.Add(this.textBox_file);
            this.panel_mysql.Controls.Add(this.label1);
            this.panel_mysql.Controls.Add(this.checkBox1);
            this.panel_mysql.Controls.Add(this.button_mysqladd);
            this.panel_mysql.Controls.Add(this.button_mysqltest);
            this.panel_mysql.Controls.Add(this.button5);
            this.panel_mysql.Controls.Add(this.textBox_pwd);
            this.panel_mysql.Controls.Add(this.label9);
            this.panel_mysql.Controls.Add(this.label6);
            this.panel_mysql.Location = new System.Drawing.Point(18, 12);
            this.panel_mysql.Name = "panel_mysql";
            this.panel_mysql.Size = new System.Drawing.Size(422, 265);
            this.panel_mysql.TabIndex = 4;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(169, 143);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 30;
            this.checkBox1.Text = "记住密码";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // button_mysqladd
            // 
            this.button_mysqladd.Location = new System.Drawing.Point(261, 187);
            this.button_mysqladd.Name = "button_mysqladd";
            this.button_mysqladd.Size = new System.Drawing.Size(75, 23);
            this.button_mysqladd.TabIndex = 29;
            this.button_mysqladd.Text = "确认添加";
            this.button_mysqladd.UseVisualStyleBackColor = true;
            this.button_mysqladd.Click += new System.EventHandler(this.button_mysqladd_Click);
            // 
            // button_mysqltest
            // 
            this.button_mysqltest.Location = new System.Drawing.Point(180, 187);
            this.button_mysqltest.Name = "button_mysqltest";
            this.button_mysqltest.Size = new System.Drawing.Size(75, 23);
            this.button_mysqltest.TabIndex = 28;
            this.button_mysqltest.Text = "测试连接";
            this.button_mysqltest.UseVisualStyleBackColor = true;
            this.button_mysqltest.Click += new System.EventHandler(this.button_mysqltest_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(99, 187);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 27;
            this.button5.Text = "上一步";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // textBox_pwd
            // 
            this.textBox_pwd.Location = new System.Drawing.Point(169, 100);
            this.textBox_pwd.Name = "textBox_pwd";
            this.textBox_pwd.PasswordChar = '*';
            this.textBox_pwd.Size = new System.Drawing.Size(161, 21);
            this.textBox_pwd.TabIndex = 24;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(122, 103);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 23;
            this.label9.Text = "密码：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(126, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(138, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "设置Sqlite连接属性：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(86, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 31;
            this.label1.Text = "数据库文件：";
            // 
            // textBox_file
            // 
            this.textBox_file.Location = new System.Drawing.Point(169, 57);
            this.textBox_file.Name = "textBox_file";
            this.textBox_file.Size = new System.Drawing.Size(161, 21);
            this.textBox_file.TabIndex = 32;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(330, 56);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(34, 23);
            this.button1.TabIndex = 33;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form_AddDatabase_Sqlite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 289);
            this.Controls.Add(this.panel_mysql);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_AddDatabase_Sqlite";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "添加Sqlite数据库服务器";
            this.Load += new System.EventHandler(this.Form_AddDatabase_Sqlite_Load);
            this.panel_mysql.ResumeLayout(false);
            this.panel_mysql.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_mysql;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox_file;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button_mysqladd;
        private System.Windows.Forms.Button button_mysqltest;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox textBox_pwd;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}
namespace HaoCodeBuilder
{
    partial class Form_AddDatabase
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
            this.panel_selectdbtype = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.radioButton_sqlite = new System.Windows.Forms.RadioButton();
            this.radioButton_access = new System.Windows.Forms.RadioButton();
            this.radioButton_mysql = new System.Windows.Forms.RadioButton();
            this.radioButton_oracle = new System.Windows.Forms.RadioButton();
            this.radioButton_sqlserver2000 = new System.Windows.Forms.RadioButton();
            this.radioButton_sqlserver2008 = new System.Windows.Forms.RadioButton();
            this.radioButton_sqlserver2005 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panel_selectdbtype.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_selectdbtype
            // 
            this.panel_selectdbtype.Controls.Add(this.button1);
            this.panel_selectdbtype.Controls.Add(this.radioButton_sqlite);
            this.panel_selectdbtype.Controls.Add(this.radioButton_access);
            this.panel_selectdbtype.Controls.Add(this.radioButton_mysql);
            this.panel_selectdbtype.Controls.Add(this.radioButton_oracle);
            this.panel_selectdbtype.Controls.Add(this.radioButton_sqlserver2000);
            this.panel_selectdbtype.Controls.Add(this.radioButton_sqlserver2008);
            this.panel_selectdbtype.Controls.Add(this.radioButton_sqlserver2005);
            this.panel_selectdbtype.Controls.Add(this.label1);
            this.panel_selectdbtype.Location = new System.Drawing.Point(28, 9);
            this.panel_selectdbtype.Name = "panel_selectdbtype";
            this.panel_selectdbtype.Size = new System.Drawing.Size(377, 243);
            this.panel_selectdbtype.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(129, 204);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "下一步";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // radioButton_sqlite
            // 
            this.radioButton_sqlite.AutoSize = true;
            this.radioButton_sqlite.Location = new System.Drawing.Point(129, 163);
            this.radioButton_sqlite.Name = "radioButton_sqlite";
            this.radioButton_sqlite.Size = new System.Drawing.Size(59, 16);
            this.radioButton_sqlite.TabIndex = 7;
            this.radioButton_sqlite.Text = "Sqlite";
            this.radioButton_sqlite.UseVisualStyleBackColor = true;
            // 
            // radioButton_access
            // 
            this.radioButton_access.AutoSize = true;
            this.radioButton_access.Enabled = false;
            this.radioButton_access.Location = new System.Drawing.Point(129, 141);
            this.radioButton_access.Name = "radioButton_access";
            this.radioButton_access.Size = new System.Drawing.Size(59, 16);
            this.radioButton_access.TabIndex = 6;
            this.radioButton_access.Text = "Access";
            this.radioButton_access.UseVisualStyleBackColor = true;
            // 
            // radioButton_mysql
            // 
            this.radioButton_mysql.AutoSize = true;
            this.radioButton_mysql.Location = new System.Drawing.Point(129, 119);
            this.radioButton_mysql.Name = "radioButton_mysql";
            this.radioButton_mysql.Size = new System.Drawing.Size(53, 16);
            this.radioButton_mysql.TabIndex = 5;
            this.radioButton_mysql.Text = "MySql";
            this.radioButton_mysql.UseVisualStyleBackColor = true;
            // 
            // radioButton_oracle
            // 
            this.radioButton_oracle.AutoSize = true;
            this.radioButton_oracle.Enabled = false;
            this.radioButton_oracle.Location = new System.Drawing.Point(129, 97);
            this.radioButton_oracle.Name = "radioButton_oracle";
            this.radioButton_oracle.Size = new System.Drawing.Size(59, 16);
            this.radioButton_oracle.TabIndex = 4;
            this.radioButton_oracle.Text = "Oracle";
            this.radioButton_oracle.UseVisualStyleBackColor = true;
            // 
            // radioButton_sqlserver2000
            // 
            this.radioButton_sqlserver2000.AutoSize = true;
            this.radioButton_sqlserver2000.Location = new System.Drawing.Point(129, 31);
            this.radioButton_sqlserver2000.Name = "radioButton_sqlserver2000";
            this.radioButton_sqlserver2000.Size = new System.Drawing.Size(113, 16);
            this.radioButton_sqlserver2000.TabIndex = 3;
            this.radioButton_sqlserver2000.Text = "Sql Server 2000";
            this.radioButton_sqlserver2000.UseVisualStyleBackColor = true;
            // 
            // radioButton_sqlserver2008
            // 
            this.radioButton_sqlserver2008.AutoSize = true;
            this.radioButton_sqlserver2008.Checked = true;
            this.radioButton_sqlserver2008.Location = new System.Drawing.Point(129, 75);
            this.radioButton_sqlserver2008.Name = "radioButton_sqlserver2008";
            this.radioButton_sqlserver2008.Size = new System.Drawing.Size(113, 16);
            this.radioButton_sqlserver2008.TabIndex = 2;
            this.radioButton_sqlserver2008.TabStop = true;
            this.radioButton_sqlserver2008.Text = "Sql Server 2008";
            this.radioButton_sqlserver2008.UseVisualStyleBackColor = true;
            // 
            // radioButton_sqlserver2005
            // 
            this.radioButton_sqlserver2005.AutoSize = true;
            this.radioButton_sqlserver2005.Location = new System.Drawing.Point(129, 53);
            this.radioButton_sqlserver2005.Name = "radioButton_sqlserver2005";
            this.radioButton_sqlserver2005.Size = new System.Drawing.Size(113, 16);
            this.radioButton_sqlserver2005.TabIndex = 1;
            this.radioButton_sqlserver2005.Text = "Sql Server 2005";
            this.radioButton_sqlserver2005.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(127, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择数据库类型：";
            // 
            // Form_AddDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 289);
            this.Controls.Add(this.panel_selectdbtype);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_AddDatabase";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "添加数据库服务器";
            this.Load += new System.EventHandler(this.Form_AddDatabase_Load);
            this.panel_selectdbtype.ResumeLayout(false);
            this.panel_selectdbtype.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_selectdbtype;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton radioButton_sqlite;
        private System.Windows.Forms.RadioButton radioButton_access;
        private System.Windows.Forms.RadioButton radioButton_mysql;
        private System.Windows.Forms.RadioButton radioButton_oracle;
        private System.Windows.Forms.RadioButton radioButton_sqlserver2000;
        private System.Windows.Forms.RadioButton radioButton_sqlserver2008;
        private System.Windows.Forms.RadioButton radioButton_sqlserver2005;
        private System.Windows.Forms.Label label1;
    }
}
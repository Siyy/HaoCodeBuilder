namespace HaoCodeBuilder
{
    partial class Form_Code_SetDir
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
            this.checkBox_count = new System.Windows.Forms.CheckBox();
            this.checkBox_exists = new System.Windows.Forms.CheckBox();
            this.checkBox_getbykey = new System.Windows.Forms.CheckBox();
            this.checkBox_getall = new System.Windows.Forms.CheckBox();
            this.checkBox_update = new System.Windows.Forms.CheckBox();
            this.checkBox_delete = new System.Windows.Forms.CheckBox();
            this.checkBox_add = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_dir = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // checkBox_count
            // 
            this.checkBox_count.AutoSize = true;
            this.checkBox_count.Checked = true;
            this.checkBox_count.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_count.Location = new System.Drawing.Point(280, 132);
            this.checkBox_count.Name = "checkBox_count";
            this.checkBox_count.Size = new System.Drawing.Size(96, 16);
            this.checkBox_count.TabIndex = 21;
            this.checkBox_count.Text = "查询记录条数";
            this.checkBox_count.UseVisualStyleBackColor = true;
            // 
            // checkBox_exists
            // 
            this.checkBox_exists.AutoSize = true;
            this.checkBox_exists.Checked = true;
            this.checkBox_exists.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_exists.Location = new System.Drawing.Point(256, 99);
            this.checkBox_exists.Name = "checkBox_exists";
            this.checkBox_exists.Size = new System.Drawing.Size(120, 16);
            this.checkBox_exists.TabIndex = 20;
            this.checkBox_exists.Text = "判断记录是否存在";
            this.checkBox_exists.UseVisualStyleBackColor = true;
            // 
            // checkBox_getbykey
            // 
            this.checkBox_getbykey.AutoSize = true;
            this.checkBox_getbykey.Checked = true;
            this.checkBox_getbykey.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_getbykey.Location = new System.Drawing.Point(166, 132);
            this.checkBox_getbykey.Name = "checkBox_getbykey";
            this.checkBox_getbykey.Size = new System.Drawing.Size(96, 16);
            this.checkBox_getbykey.TabIndex = 19;
            this.checkBox_getbykey.Text = "查询主键记录";
            this.checkBox_getbykey.UseVisualStyleBackColor = true;
            // 
            // checkBox_getall
            // 
            this.checkBox_getall.AutoSize = true;
            this.checkBox_getall.Checked = true;
            this.checkBox_getall.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_getall.Location = new System.Drawing.Point(53, 132);
            this.checkBox_getall.Name = "checkBox_getall";
            this.checkBox_getall.Size = new System.Drawing.Size(96, 16);
            this.checkBox_getall.TabIndex = 18;
            this.checkBox_getall.Text = "查询所有记录";
            this.checkBox_getall.UseVisualStyleBackColor = true;
            // 
            // checkBox_update
            // 
            this.checkBox_update.AutoSize = true;
            this.checkBox_update.Checked = true;
            this.checkBox_update.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_update.Location = new System.Drawing.Point(185, 99);
            this.checkBox_update.Name = "checkBox_update";
            this.checkBox_update.Size = new System.Drawing.Size(48, 16);
            this.checkBox_update.TabIndex = 17;
            this.checkBox_update.Text = "修改";
            this.checkBox_update.UseVisualStyleBackColor = true;
            // 
            // checkBox_delete
            // 
            this.checkBox_delete.AutoSize = true;
            this.checkBox_delete.Checked = true;
            this.checkBox_delete.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_delete.Location = new System.Drawing.Point(117, 99);
            this.checkBox_delete.Name = "checkBox_delete";
            this.checkBox_delete.Size = new System.Drawing.Size(48, 16);
            this.checkBox_delete.TabIndex = 16;
            this.checkBox_delete.Text = "删除";
            this.checkBox_delete.UseVisualStyleBackColor = true;
            // 
            // checkBox_add
            // 
            this.checkBox_add.AutoSize = true;
            this.checkBox_add.Checked = true;
            this.checkBox_add.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_add.Location = new System.Drawing.Point(53, 99);
            this.checkBox_add.Name = "checkBox_add";
            this.checkBox_add.Size = new System.Drawing.Size(48, 16);
            this.checkBox_add.TabIndex = 15;
            this.checkBox_add.Text = "新增";
            this.checkBox_add.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(29, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "生成方法：";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(185, 33);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(119, 16);
            this.radioButton2.TabIndex = 13;
            this.radioButton2.Text = "工厂模式三层架构";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(53, 33);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(95, 16);
            this.radioButton1.TabIndex = 12;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "普通三层架构";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(29, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "生成模式：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(29, 175);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 12);
            this.label3.TabIndex = 22;
            this.label3.Text = "目录设置：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 207);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 23;
            this.label4.Text = "项目目录：";
            // 
            // textBox_dir
            // 
            this.textBox_dir.Location = new System.Drawing.Point(117, 204);
            this.textBox_dir.Name = "textBox_dir";
            this.textBox_dir.Size = new System.Drawing.Size(232, 21);
            this.textBox_dir.TabIndex = 24;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(349, 203);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(33, 23);
            this.button1.TabIndex = 25;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(128, 268);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 38;
            this.button6.Text = "确定生成";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(224, 268);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 39;
            this.button7.Text = "取消关闭";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "选择您的项目路径";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(115, 239);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 45;
            this.label8.Text = "label8";
            // 
            // Form_Code_SetDir
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 317);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox_dir);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkBox_count);
            this.Controls.Add(this.checkBox_exists);
            this.Controls.Add(this.checkBox_getbykey);
            this.Controls.Add(this.checkBox_getall);
            this.Controls.Add(this.checkBox_update);
            this.Controls.Add(this.checkBox_delete);
            this.Controls.Add(this.checkBox_add);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_Code_SetDir";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "生成代码至目录";
            this.Load += new System.EventHandler(this.Form_Code_SetDir_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox_count;
        private System.Windows.Forms.CheckBox checkBox_exists;
        private System.Windows.Forms.CheckBox checkBox_getbykey;
        private System.Windows.Forms.CheckBox checkBox_getall;
        private System.Windows.Forms.CheckBox checkBox_update;
        private System.Windows.Forms.CheckBox checkBox_delete;
        private System.Windows.Forms.CheckBox checkBox_add;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_dir;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label8;
    }
}
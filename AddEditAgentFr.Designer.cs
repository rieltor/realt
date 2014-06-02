namespace realty
{
    partial class AddEditAgentFr
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
            this.Deletepanel = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.cbxAgents = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txbPass2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btSave = new System.Windows.Forms.Button();
            this.chbxAdmin = new System.Windows.Forms.CheckBox();
            this.cbxAgency = new System.Windows.Forms.ComboBox();
            this.txbPhone = new System.Windows.Forms.TextBox();
            this.txbMail = new System.Windows.Forms.TextBox();
            this.txbPassword = new System.Windows.Forms.TextBox();
            this.txbLogin = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txbFIO = new System.Windows.Forms.TextBox();
            this.Deletepanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Deletepanel
            // 
            this.Deletepanel.Controls.Add(this.groupBox1);
            this.Deletepanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Deletepanel.Location = new System.Drawing.Point(0, 275);
            this.Deletepanel.Name = "Deletepanel";
            this.Deletepanel.Size = new System.Drawing.Size(632, 115);
            this.Deletepanel.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.cbxAgents);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(632, 115);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Удаление";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(32, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(425, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Выберите агента, на которого следует перезаписать объекты удаляемого агента";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(32, 76);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(146, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Удалить агента";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbxAgents
            // 
            this.cbxAgents.FormattingEnabled = true;
            this.cbxAgents.Location = new System.Drawing.Point(32, 41);
            this.cbxAgents.Name = "cbxAgents";
            this.cbxAgents.Size = new System.Drawing.Size(261, 21);
            this.cbxAgents.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(632, 275);
            this.panel1.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txbPass2);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.btSave);
            this.groupBox2.Controls.Add(this.chbxAdmin);
            this.groupBox2.Controls.Add(this.cbxAgency);
            this.groupBox2.Controls.Add(this.txbPhone);
            this.groupBox2.Controls.Add(this.txbMail);
            this.groupBox2.Controls.Add(this.txbPassword);
            this.groupBox2.Controls.Add(this.txbLogin);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txbFIO);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(632, 275);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ввод данных";
            // 
            // txbPass2
            // 
            this.txbPass2.Location = new System.Drawing.Point(339, 238);
            this.txbPass2.Name = "txbPass2";
            this.txbPass2.PasswordChar = '*';
            this.txbPass2.Size = new System.Drawing.Size(263, 20);
            this.txbPass2.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(335, 222);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Повтор пароля";
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(32, 236);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(146, 23);
            this.btSave.TabIndex = 5;
            this.btSave.Text = "Сохранить изменения";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // chbxAdmin
            // 
            this.chbxAdmin.AutoSize = true;
            this.chbxAdmin.Location = new System.Drawing.Point(339, 84);
            this.chbxAdmin.Name = "chbxAdmin";
            this.chbxAdmin.Size = new System.Drawing.Size(170, 17);
            this.chbxAdmin.TabIndex = 4;
            this.chbxAdmin.Text = "Является администратором";
            this.chbxAdmin.UseVisualStyleBackColor = true;
            // 
            // cbxAgency
            // 
            this.cbxAgency.FormattingEnabled = true;
            this.cbxAgency.Location = new System.Drawing.Point(32, 87);
            this.cbxAgency.Name = "cbxAgency";
            this.cbxAgency.Size = new System.Drawing.Size(261, 21);
            this.cbxAgency.TabIndex = 3;
            // 
            // txbPhone
            // 
            this.txbPhone.Location = new System.Drawing.Point(339, 139);
            this.txbPhone.Name = "txbPhone";
            this.txbPhone.Size = new System.Drawing.Size(261, 20);
            this.txbPhone.TabIndex = 2;
            // 
            // txbMail
            // 
            this.txbMail.Location = new System.Drawing.Point(32, 139);
            this.txbMail.Name = "txbMail";
            this.txbMail.Size = new System.Drawing.Size(261, 20);
            this.txbMail.TabIndex = 2;
            // 
            // txbPassword
            // 
            this.txbPassword.Location = new System.Drawing.Point(338, 190);
            this.txbPassword.Name = "txbPassword";
            this.txbPassword.PasswordChar = '*';
            this.txbPassword.Size = new System.Drawing.Size(261, 20);
            this.txbPassword.TabIndex = 2;
            // 
            // txbLogin
            // 
            this.txbLogin.Location = new System.Drawing.Point(32, 188);
            this.txbLogin.Name = "txbLogin";
            this.txbLogin.Size = new System.Drawing.Size(261, 20);
            this.txbLogin.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(335, 123);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Телефон";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(335, 172);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Пароль";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Почта";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Логин";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Агентство";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Фамилия Имя Отчество";
            // 
            // txbFIO
            // 
            this.txbFIO.Location = new System.Drawing.Point(32, 39);
            this.txbFIO.Name = "txbFIO";
            this.txbFIO.Size = new System.Drawing.Size(569, 20);
            this.txbFIO.TabIndex = 0;
            // 
            // AddEditAgentFr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 390);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Deletepanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AddEditAgentFr";
            this.Text = "Информация по агенту";
            this.Load += new System.EventHandler(this.AddEditAgentFr_Load);
            this.Deletepanel.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Deletepanel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.CheckBox chbxAdmin;
        private System.Windows.Forms.ComboBox cbxAgency;
        private System.Windows.Forms.TextBox txbMail;
        private System.Windows.Forms.TextBox txbPassword;
        private System.Windows.Forms.TextBox txbLogin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbFIO;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cbxAgents;
        private System.Windows.Forms.TextBox txbPhone;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txbPass2;
        private System.Windows.Forms.Label label8;
    }
}
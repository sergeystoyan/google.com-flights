namespace Cliver.BotGui
{
    partial class CustomConfigControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.Email = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SmtpPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SmtpPort = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SmtpHost = new System.Windows.Forms.TextBox();
            this._0_bViewInputFile = new System.Windows.Forms.Button();
            this.ChooseInputFile = new System.Windows.Forms.Button();
            this.UsersFile = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.group_box.SuspendLayout();
            this.SuspendLayout();
            // 
            // group_box
            // 
            this.group_box.Controls.Add(this._0_bViewInputFile);
            this.group_box.Controls.Add(this.ChooseInputFile);
            this.group_box.Controls.Add(this.UsersFile);
            this.group_box.Controls.Add(this.label17);
            this.group_box.Controls.Add(this.label5);
            this.group_box.Controls.Add(this.SmtpHost);
            this.group_box.Controls.Add(this.label3);
            this.group_box.Controls.Add(this.SmtpPort);
            this.group_box.Controls.Add(this.label2);
            this.group_box.Controls.Add(this.SmtpPassword);
            this.group_box.Controls.Add(this.label1);
            this.group_box.Controls.Add(this.Email);
            this.group_box.Text = "TestCustom";
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 100000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 100;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(30, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 16);
            this.label1.TabIndex = 36;
            this.label1.Text = "Email Address:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Email
            // 
            this.Email.Location = new System.Drawing.Point(123, 23);
            this.Email.Name = "Email";
            this.Email.Size = new System.Drawing.Size(371, 20);
            this.Email.TabIndex = 37;
            this.Email.Text = "L-J-smith@prodigy.net";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 16);
            this.label2.TabIndex = 38;
            this.label2.Text = "SMTP Password:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // SmtpPassword
            // 
            this.SmtpPassword.Location = new System.Drawing.Point(123, 49);
            this.SmtpPassword.Name = "SmtpPassword";
            this.SmtpPassword.Size = new System.Drawing.Size(371, 20);
            this.SmtpPassword.TabIndex = 39;
            this.SmtpPassword.Text = "BOX2531";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(30, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 16);
            this.label3.TabIndex = 40;
            this.label3.Text = "SMTP Port:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // SmtpPort
            // 
            this.SmtpPort.Location = new System.Drawing.Point(123, 101);
            this.SmtpPort.Name = "SmtpPort";
            this.SmtpPort.Size = new System.Drawing.Size(371, 20);
            this.SmtpPort.TabIndex = 41;
            this.SmtpPort.Text = "587";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(30, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 16);
            this.label5.TabIndex = 44;
            this.label5.Text = "SMTP Host:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // SmtpHost
            // 
            this.SmtpHost.Location = new System.Drawing.Point(123, 75);
            this.SmtpHost.Name = "SmtpHost";
            this.SmtpHost.Size = new System.Drawing.Size(371, 20);
            this.SmtpHost.TabIndex = 45;
            this.SmtpHost.Text = "smtp.mail.att.net";
            // 
            // _0_bViewInputFile
            // 
            this._0_bViewInputFile.Location = new System.Drawing.Point(79, 158);
            this._0_bViewInputFile.Name = "_0_bViewInputFile";
            this._0_bViewInputFile.Size = new System.Drawing.Size(76, 23);
            this._0_bViewInputFile.TabIndex = 63;
            this._0_bViewInputFile.Text = "View";
            this._0_bViewInputFile.UseVisualStyleBackColor = true;
            this._0_bViewInputFile.Click += new System.EventHandler(this._0_bViewInputFile_Click);
            // 
            // ChooseInputFile
            // 
            this.ChooseInputFile.Location = new System.Drawing.Point(477, 184);
            this.ChooseInputFile.Name = "ChooseInputFile";
            this.ChooseInputFile.Size = new System.Drawing.Size(24, 23);
            this.ChooseInputFile.TabIndex = 62;
            this.ChooseInputFile.Text = "...";
            this.ChooseInputFile.UseVisualStyleBackColor = true;
            this.ChooseInputFile.Click += new System.EventHandler(this.ChooseInputFile_Click);
            // 
            // UsersFile
            // 
            this.UsersFile.Location = new System.Drawing.Point(11, 184);
            this.UsersFile.Multiline = true;
            this.UsersFile.Name = "UsersFile";
            this.UsersFile.Size = new System.Drawing.Size(460, 47);
            this.UsersFile.TabIndex = 61;
            this.UsersFile.TextChanged += new System.EventHandler(this.UsersFile_TextChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(13, 168);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(56, 13);
            this.label17.TabIndex = 60;
            this.label17.Text = "Users File:";
            this.label17.Click += new System.EventHandler(this.label17_Click);
            // 
            // CustomConfigControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "CustomConfigControl";
            this.group_box.ResumeLayout(false);
            this.group_box.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Email;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox SmtpHost;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox SmtpPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox SmtpPassword;
        private System.Windows.Forms.Button _0_bViewInputFile;
        private System.Windows.Forms.Button ChooseInputFile;
        private System.Windows.Forms.TextBox UsersFile;
        private System.Windows.Forms.Label label17;






    }
}

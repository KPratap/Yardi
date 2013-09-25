namespace YardiConfig
{
    partial class frmConfig
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
            this.txtClientConfig = new System.Windows.Forms.TextBox();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPWD = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSaveConfig = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.txtDownloadFolder = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSetDownloadFolder = new System.Windows.Forms.Button();
            this.fbrDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(74, 159);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Client List File :";
            // 
            // txtClientConfig
            // 
            this.txtClientConfig.Location = new System.Drawing.Point(154, 156);
            this.txtClientConfig.Name = "txtClientConfig";
            this.txtClientConfig.Size = new System.Drawing.Size(366, 20);
            this.txtClientConfig.TabIndex = 1;
            this.txtClientConfig.TextChanged += new System.EventHandler(this.SetChangedFlag);
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(154, 30);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(366, 20);
            this.txtURL.TabIndex = 3;
            this.txtURL.TextChanged += new System.EventHandler(this.SetChangedFlag);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(91, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Yardi URL :";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(154, 72);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(184, 20);
            this.txtUser.TabIndex = 5;
            this.txtUser.TextChanged += new System.EventHandler(this.SetChangedFlag);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(80, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Login UserId :";
            // 
            // txtPWD
            // 
            this.txtPWD.Location = new System.Drawing.Point(154, 114);
            this.txtPWD.Name = "txtPWD";
            this.txtPWD.Size = new System.Drawing.Size(184, 20);
            this.txtPWD.TabIndex = 7;
            this.txtPWD.TextChanged += new System.EventHandler(this.SetChangedFlag);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(64, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Login Password :";
            // 
            // btnSaveConfig
            // 
            this.btnSaveConfig.Location = new System.Drawing.Point(425, 291);
            this.btnSaveConfig.Name = "btnSaveConfig";
            this.btnSaveConfig.Size = new System.Drawing.Size(75, 23);
            this.btnSaveConfig.TabIndex = 8;
            this.btnSaveConfig.Text = "Save";
            this.btnSaveConfig.UseVisualStyleBackColor = true;
            this.btnSaveConfig.Click += new System.EventHandler(this.btnSaveConfig_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(531, 291);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 9;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txtDownloadFolder
            // 
            this.txtDownloadFolder.Location = new System.Drawing.Point(154, 193);
            this.txtDownloadFolder.Name = "txtDownloadFolder";
            this.txtDownloadFolder.ReadOnly = true;
            this.txtDownloadFolder.Size = new System.Drawing.Size(366, 20);
            this.txtDownloadFolder.TabIndex = 11;
            this.txtDownloadFolder.TextChanged += new System.EventHandler(this.SetChangedFlag);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(56, 196);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Download Folder:";
            // 
            // btnSetDownloadFolder
            // 
            this.btnSetDownloadFolder.Location = new System.Drawing.Point(531, 191);
            this.btnSetDownloadFolder.Name = "btnSetDownloadFolder";
            this.btnSetDownloadFolder.Size = new System.Drawing.Size(75, 23);
            this.btnSetDownloadFolder.TabIndex = 12;
            this.btnSetDownloadFolder.Text = "Change..";
            this.btnSetDownloadFolder.UseVisualStyleBackColor = true;
            this.btnSetDownloadFolder.Click += new System.EventHandler(this.btnSetDownloadFolder_Click);
            // 
            // frmConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 337);
            this.Controls.Add(this.btnSetDownloadFolder);
            this.Controls.Add(this.txtDownloadFolder);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSaveConfig);
            this.Controls.Add(this.txtPWD);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtClientConfig);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "frmConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Yardi Configuration Tool";
            this.Load += new System.EventHandler(this.frmConfig_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtClientConfig;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPWD;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSaveConfig;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox txtDownloadFolder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSetDownloadFolder;
        private System.Windows.Forms.FolderBrowserDialog fbrDialog;
    }
}


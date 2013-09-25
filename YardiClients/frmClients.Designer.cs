namespace YardiClients
{
    partial class frmClients
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
            this.components = new System.ComponentModel.Container();
            this.btnExit = new System.Windows.Forms.Button();
            this.txtClientConfig = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lvClients = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmenuClient = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addClientEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtShortName = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.openClients = new System.Windows.Forms.OpenFileDialog();
            this.btnOpenClients = new System.Windows.Forms.Button();
            this.pnlDetl = new System.Windows.Forms.GroupBox();
            this.cboPlatform = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.txtDB = new System.Windows.Forms.TextBox();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.chkEnable = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtYardiId = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cmenuClient.SuspendLayout();
            this.pnlDetl.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(958, 474);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 19;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txtClientConfig
            // 
            this.txtClientConfig.Location = new System.Drawing.Point(119, 13);
            this.txtClientConfig.Name = "txtClientConfig";
            this.txtClientConfig.ReadOnly = true;
            this.txtClientConfig.Size = new System.Drawing.Size(498, 20);
            this.txtClientConfig.TabIndex = 11;
            this.txtClientConfig.TabStop = false;
            this.txtClientConfig.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Client List File :";
            this.label1.Visible = false;
            // 
            // lvClients
            // 
            this.lvClients.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.lvClients.ContextMenuStrip = this.cmenuClient;
            this.lvClients.FullRowSelect = true;
            this.lvClients.GridLines = true;
            this.lvClients.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvClients.HideSelection = false;
            this.lvClients.Location = new System.Drawing.Point(38, 265);
            this.lvClients.MultiSelect = false;
            this.lvClients.Name = "lvClients";
            this.lvClients.Size = new System.Drawing.Size(790, 195);
            this.lvClients.TabIndex = 20;
            this.lvClients.UseCompatibleStateImageBehavior = false;
            this.lvClients.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "RRS Internal Id";
            this.columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Yardi Property Id";
            this.columnHeader2.Width = 120;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "PropertyName";
            this.columnHeader3.Width = 160;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Enabled";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Webservice URL";
            this.columnHeader5.Width = 300;
            // 
            // cmenuClient
            // 
            this.cmenuClient.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addClientEntryToolStripMenuItem,
            this.editToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.cmenuClient.Name = "cmenuClient";
            this.cmenuClient.Size = new System.Drawing.Size(142, 70);
            // 
            // addClientEntryToolStripMenuItem
            // 
            this.addClientEntryToolStripMenuItem.Name = "addClientEntryToolStripMenuItem";
            this.addClientEntryToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.addClientEntryToolStripMenuItem.Text = "Add Client";
            this.addClientEntryToolStripMenuItem.Click += new System.EventHandler(this.addClientEntryToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.editToolStripMenuItem.Text = "Edit Client";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.deleteToolStripMenuItem.Text = "Delete Client";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // txtShortName
            // 
            this.txtShortName.Location = new System.Drawing.Point(115, 23);
            this.txtShortName.Name = "txtShortName";
            this.txtShortName.Size = new System.Drawing.Size(115, 20);
            this.txtShortName.TabIndex = 0;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(116, 49);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(211, 20);
            this.txtName.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(733, 169);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(49, 23);
            this.btnCancel.TabIndex = 26;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(678, 169);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(49, 23);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // openClients
            // 
            this.openClients.FileName = "openFileDialog1";
            // 
            // btnOpenClients
            // 
            this.btnOpenClients.Location = new System.Drawing.Point(630, 10);
            this.btnOpenClients.Name = "btnOpenClients";
            this.btnOpenClients.Size = new System.Drawing.Size(75, 23);
            this.btnOpenClients.TabIndex = 0;
            this.btnOpenClients.Text = "Open..";
            this.btnOpenClients.UseVisualStyleBackColor = true;
            this.btnOpenClients.Visible = false;
            this.btnOpenClients.Click += new System.EventHandler(this.btnOpenClients_Click);
            // 
            // pnlDetl
            // 
            this.pnlDetl.Controls.Add(this.cboPlatform);
            this.pnlDetl.Controls.Add(this.label12);
            this.pnlDetl.Controls.Add(this.txtUrl);
            this.pnlDetl.Controls.Add(this.label6);
            this.pnlDetl.Controls.Add(this.label8);
            this.pnlDetl.Controls.Add(this.label9);
            this.pnlDetl.Controls.Add(this.label10);
            this.pnlDetl.Controls.Add(this.label11);
            this.pnlDetl.Controls.Add(this.txtPassword);
            this.pnlDetl.Controls.Add(this.txtUserId);
            this.pnlDetl.Controls.Add(this.txtDB);
            this.pnlDetl.Controls.Add(this.txtServer);
            this.pnlDetl.Controls.Add(this.chkEnable);
            this.pnlDetl.Controls.Add(this.label5);
            this.pnlDetl.Controls.Add(this.label3);
            this.pnlDetl.Controls.Add(this.label2);
            this.pnlDetl.Controls.Add(this.btnCancel);
            this.pnlDetl.Controls.Add(this.btnSave);
            this.pnlDetl.Controls.Add(this.txtName);
            this.pnlDetl.Controls.Add(this.txtShortName);
            this.pnlDetl.Controls.Add(this.txtYardiId);
            this.pnlDetl.Location = new System.Drawing.Point(38, 16);
            this.pnlDetl.Name = "pnlDetl";
            this.pnlDetl.Size = new System.Drawing.Size(790, 225);
            this.pnlDetl.TabIndex = 27;
            this.pnlDetl.TabStop = false;
            this.pnlDetl.Text = "Client Information";
            // 
            // cboPlatform
            // 
            this.cboPlatform.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPlatform.FormattingEnabled = true;
            this.cboPlatform.Items.AddRange(new object[] {
            "Sql Server",
            "Oracle"});
            this.cboPlatform.Location = new System.Drawing.Point(496, 127);
            this.cboPlatform.Name = "cboPlatform";
            this.cboPlatform.Size = new System.Drawing.Size(121, 21);
            this.cboPlatform.TabIndex = 8;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(10, 173);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(116, 13);
            this.label12.TabIndex = 46;
            this.label12.Text = "Yardi Webservice URL";
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(135, 169);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(494, 20);
            this.txtUrl.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(445, 131);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 43;
            this.label6.Text = "Platform";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(440, 104);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 41;
            this.label8.Text = "Database";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(453, 79);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 13);
            this.label9.TabIndex = 40;
            this.label9.Text = "Server";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(439, 52);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 13);
            this.label10.TabIndex = 39;
            this.label10.Text = "Password";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(449, 29);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(38, 13);
            this.label11.TabIndex = 38;
            this.label11.Text = "UserId";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(494, 49);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(94, 21);
            this.txtPassword.TabIndex = 5;
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(494, 23);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(133, 20);
            this.txtUserId.TabIndex = 4;
            // 
            // txtDB
            // 
            this.txtDB.Location = new System.Drawing.Point(495, 101);
            this.txtDB.Name = "txtDB";
            this.txtDB.Size = new System.Drawing.Size(132, 20);
            this.txtDB.TabIndex = 7;
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(494, 75);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(133, 20);
            this.txtServer.TabIndex = 6;
            // 
            // chkEnable
            // 
            this.chkEnable.AutoSize = true;
            this.chkEnable.Location = new System.Drawing.Point(97, 77);
            this.chkEnable.Name = "chkEnable";
            this.chkEnable.Size = new System.Drawing.Size(107, 17);
            this.chkEnable.TabIndex = 2;
            this.chkEnable.Text = "Enable this Client";
            this.chkEnable.UseVisualStyleBackColor = true;
            this.chkEnable.CheckedChanged += new System.EventHandler(this.chkEnable_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 147);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 30;
            this.label5.Text = "Yardi  Property Id";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "Client Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "RRS Internal ID";
            // 
            // txtYardiId
            // 
            this.txtYardiId.Location = new System.Drawing.Point(137, 143);
            this.txtYardiId.Name = "txtYardiId";
            this.txtYardiId.Size = new System.Drawing.Size(84, 20);
            this.txtYardiId.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(753, 474);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 28;
            this.button1.Text = "Exit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmClients
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 509);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnOpenClients);
            this.Controls.Add(this.pnlDetl);
            this.Controls.Add(this.lvClients);
            this.Controls.Add(this.txtClientConfig);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmClients";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Yardi Client List Maintenance";
            this.Load += new System.EventHandler(this.frmClients_Load);
            this.cmenuClient.ResumeLayout(false);
            this.pnlDetl.ResumeLayout(false);
            this.pnlDetl.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox txtClientConfig;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView lvClients;
        private System.Windows.Forms.TextBox txtShortName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ContextMenuStrip cmenuClient;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addClientEntryToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openClients;
        private System.Windows.Forms.Button btnOpenClients;
        private System.Windows.Forms.GroupBox pnlDetl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkEnable;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtYardiId;
        private System.Windows.Forms.ComboBox cboPlatform;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Button button1;
    }
}


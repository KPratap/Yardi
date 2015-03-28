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
            this.lvClients = new System.Windows.Forms.ListView();
            this.cmenuClient = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addClientEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unconfiguredClientReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtShortName = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.openClients = new System.Windows.Forms.OpenFileDialog();
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
            this.label1 = new System.Windows.Forms.Label();
            this.cboVendors = new System.Windows.Forms.ComboBox();
            this.btnOpenClient = new System.Windows.Forms.Button();
            this.pnlDetlRP = new System.Windows.Forms.GroupBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtBalanceOwed = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtAfterMoveout = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtPMCId_RP = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtEncryptionKey = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtFirstDate = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtEmail_RP = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSiteAddress_RP = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSiteName_RP = new System.Windows.Forms.TextBox();
            this.chkEnable_RP = new System.Windows.Forms.CheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.btnCancel_RP = new System.Windows.Forms.Button();
            this.btnSave_RP = new System.Windows.Forms.Button();
            this.txtPMCName_RP = new System.Windows.Forms.TextBox();
            this.txtInternalId_RP = new System.Windows.Forms.TextBox();
            this.txtSiteId_RP = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.lblTotCli = new System.Windows.Forms.Label();
            this.lblActiveCli = new System.Windows.Forms.Label();
            this.lblInactiveCli = new System.Windows.Forms.Label();
            this.saveReport = new System.Windows.Forms.SaveFileDialog();
            this.label26 = new System.Windows.Forms.Label();
            this.txtPhone1 = new System.Windows.Forms.TextBox();
            this.cmenuClient.SuspendLayout();
            this.pnlDetl.SuspendLayout();
            this.pnlDetlRP.SuspendLayout();
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
            // lvClients
            // 
            this.lvClients.ContextMenuStrip = this.cmenuClient;
            this.lvClients.FullRowSelect = true;
            this.lvClients.GridLines = true;
            this.lvClients.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvClients.HideSelection = false;
            this.lvClients.Location = new System.Drawing.Point(20, 304);
            this.lvClients.MultiSelect = false;
            this.lvClients.Name = "lvClients";
            this.lvClients.Size = new System.Drawing.Size(818, 245);
            this.lvClients.TabIndex = 20;
            this.lvClients.UseCompatibleStateImageBehavior = false;
            this.lvClients.View = System.Windows.Forms.View.Details;
            this.lvClients.Visible = false;
            // 
            // cmenuClient
            // 
            this.cmenuClient.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addClientEntryToolStripMenuItem,
            this.editToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.copyClientToolStripMenuItem,
            this.unconfiguredClientReportToolStripMenuItem});
            this.cmenuClient.Name = "cmenuClient";
            this.cmenuClient.Size = new System.Drawing.Size(220, 114);
            // 
            // addClientEntryToolStripMenuItem
            // 
            this.addClientEntryToolStripMenuItem.Name = "addClientEntryToolStripMenuItem";
            this.addClientEntryToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.addClientEntryToolStripMenuItem.Text = "Add Client";
            this.addClientEntryToolStripMenuItem.Click += new System.EventHandler(this.addClientEntryToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.editToolStripMenuItem.Text = "Edit Client";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.deleteToolStripMenuItem.Text = "Delete Client";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // copyClientToolStripMenuItem
            // 
            this.copyClientToolStripMenuItem.Name = "copyClientToolStripMenuItem";
            this.copyClientToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.copyClientToolStripMenuItem.Text = "Copy Client";
            this.copyClientToolStripMenuItem.Click += new System.EventHandler(this.copyClientToolStripMenuItem_Click);
            // 
            // unconfiguredClientReportToolStripMenuItem
            // 
            this.unconfiguredClientReportToolStripMenuItem.Name = "unconfiguredClientReportToolStripMenuItem";
            this.unconfiguredClientReportToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.unconfiguredClientReportToolStripMenuItem.Text = "Unconfigured Client Report";
            this.unconfiguredClientReportToolStripMenuItem.Click += new System.EventHandler(this.unconfiguredClientReportToolStripMenuItem_Click);
            // 
            // txtShortName
            // 
            this.txtShortName.Location = new System.Drawing.Point(115, 24);
            this.txtShortName.MaxLength = 50;
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
            this.pnlDetl.Location = new System.Drawing.Point(20, 45);
            this.pnlDetl.Name = "pnlDetl";
            this.pnlDetl.Size = new System.Drawing.Size(818, 207);
            this.pnlDetl.TabIndex = 27;
            this.pnlDetl.TabStop = false;
            this.pnlDetl.Text = "Client Details";
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
            this.txtUserId.Location = new System.Drawing.Point(494, 24);
            this.txtUserId.MaxLength = 50;
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
            this.button1.Location = new System.Drawing.Point(753, 555);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 28;
            this.button1.Text = "Exit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Select Vendor";
            // 
            // cboVendors
            // 
            this.cboVendors.FormattingEnabled = true;
            this.cboVendors.Location = new System.Drawing.Point(111, 15);
            this.cboVendors.Margin = new System.Windows.Forms.Padding(2);
            this.cboVendors.Name = "cboVendors";
            this.cboVendors.Size = new System.Drawing.Size(115, 21);
            this.cboVendors.TabIndex = 30;
            this.cboVendors.SelectedIndexChanged += new System.EventHandler(this.cboVendors_SelectedIndexChanged);
            // 
            // btnOpenClient
            // 
            this.btnOpenClient.Location = new System.Drawing.Point(238, 13);
            this.btnOpenClient.Margin = new System.Windows.Forms.Padding(2);
            this.btnOpenClient.Name = "btnOpenClient";
            this.btnOpenClient.Size = new System.Drawing.Size(96, 24);
            this.btnOpenClient.TabIndex = 31;
            this.btnOpenClient.Text = "Open Client List";
            this.btnOpenClient.UseVisualStyleBackColor = true;
            this.btnOpenClient.Click += new System.EventHandler(this.btnOpenClient_Click);
            // 
            // pnlDetlRP
            // 
            this.pnlDetlRP.Controls.Add(this.label26);
            this.pnlDetlRP.Controls.Add(this.txtPhone1);
            this.pnlDetlRP.Controls.Add(this.label22);
            this.pnlDetlRP.Controls.Add(this.txtBalanceOwed);
            this.pnlDetlRP.Controls.Add(this.label21);
            this.pnlDetlRP.Controls.Add(this.txtAfterMoveout);
            this.pnlDetlRP.Controls.Add(this.label20);
            this.pnlDetlRP.Controls.Add(this.label16);
            this.pnlDetlRP.Controls.Add(this.txtPMCId_RP);
            this.pnlDetlRP.Controls.Add(this.label15);
            this.pnlDetlRP.Controls.Add(this.txtEncryptionKey);
            this.pnlDetlRP.Controls.Add(this.label14);
            this.pnlDetlRP.Controls.Add(this.txtFirstDate);
            this.pnlDetlRP.Controls.Add(this.label13);
            this.pnlDetlRP.Controls.Add(this.txtEmail_RP);
            this.pnlDetlRP.Controls.Add(this.label7);
            this.pnlDetlRP.Controls.Add(this.txtSiteAddress_RP);
            this.pnlDetlRP.Controls.Add(this.label4);
            this.pnlDetlRP.Controls.Add(this.txtSiteName_RP);
            this.pnlDetlRP.Controls.Add(this.chkEnable_RP);
            this.pnlDetlRP.Controls.Add(this.label17);
            this.pnlDetlRP.Controls.Add(this.label18);
            this.pnlDetlRP.Controls.Add(this.label19);
            this.pnlDetlRP.Controls.Add(this.btnCancel_RP);
            this.pnlDetlRP.Controls.Add(this.btnSave_RP);
            this.pnlDetlRP.Controls.Add(this.txtPMCName_RP);
            this.pnlDetlRP.Controls.Add(this.txtInternalId_RP);
            this.pnlDetlRP.Controls.Add(this.txtSiteId_RP);
            this.pnlDetlRP.Location = new System.Drawing.Point(20, 63);
            this.pnlDetlRP.Name = "pnlDetlRP";
            this.pnlDetlRP.Size = new System.Drawing.Size(818, 223);
            this.pnlDetlRP.TabIndex = 47;
            this.pnlDetlRP.TabStop = false;
            this.pnlDetlRP.Text = "Client Details";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(480, 159);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(90, 13);
            this.label22.TabIndex = 47;
            this.label22.Text = "Minimum Balance";
            // 
            // txtBalanceOwed
            // 
            this.txtBalanceOwed.Location = new System.Drawing.Point(586, 155);
            this.txtBalanceOwed.MaxLength = 50;
            this.txtBalanceOwed.Name = "txtBalanceOwed";
            this.txtBalanceOwed.Size = new System.Drawing.Size(103, 20);
            this.txtBalanceOwed.TabIndex = 46;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(317, 159);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(100, 13);
            this.label21.TabIndex = 45;
            this.label21.Text = "Days after Moveout";
            // 
            // txtAfterMoveout
            // 
            this.txtAfterMoveout.Location = new System.Drawing.Point(423, 155);
            this.txtAfterMoveout.MaxLength = 50;
            this.txtAfterMoveout.Name = "txtAfterMoveout";
            this.txtAfterMoveout.Size = new System.Drawing.Size(35, 20);
            this.txtAfterMoveout.TabIndex = 44;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(234, 159);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(75, 13);
            this.label20.TabIndex = 43;
            this.label20.Text = "YYYY-MM-DD";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(557, 28);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(42, 13);
            this.label16.TabIndex = 42;
            this.label16.Text = "PMC Id";
            // 
            // txtPMCId_RP
            // 
            this.txtPMCId_RP.AccessibleDescription = "l";
            this.txtPMCId_RP.Location = new System.Drawing.Point(605, 25);
            this.txtPMCId_RP.Name = "txtPMCId_RP";
            this.txtPMCId_RP.Size = new System.Drawing.Size(84, 20);
            this.txtPMCId_RP.TabIndex = 41;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(29, 190);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(78, 13);
            this.label15.TabIndex = 40;
            this.label15.Text = "Encryption Key";
            // 
            // txtEncryptionKey
            // 
            this.txtEncryptionKey.Location = new System.Drawing.Point(114, 186);
            this.txtEncryptionKey.MaxLength = 50;
            this.txtEncryptionKey.Name = "txtEncryptionKey";
            this.txtEncryptionKey.Size = new System.Drawing.Size(205, 20);
            this.txtEncryptionKey.TabIndex = 39;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(56, 158);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(52, 13);
            this.label14.TabIndex = 38;
            this.label14.Text = "First Date";
            // 
            // txtFirstDate
            // 
            this.txtFirstDate.Location = new System.Drawing.Point(113, 154);
            this.txtFirstDate.MaxLength = 50;
            this.txtFirstDate.Name = "txtFirstDate";
            this.txtFirstDate.Size = new System.Drawing.Size(115, 20);
            this.txtFirstDate.TabIndex = 37;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(38, 132);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(73, 13);
            this.label13.TabIndex = 36;
            this.label13.Text = "Email Address";
            // 
            // txtEmail_RP
            // 
            this.txtEmail_RP.Location = new System.Drawing.Point(114, 128);
            this.txtEmail_RP.Name = "txtEmail_RP";
            this.txtEmail_RP.Size = new System.Drawing.Size(344, 20);
            this.txtEmail_RP.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(43, 106);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 13);
            this.label7.TabIndex = 34;
            this.label7.Text = "Site Address";
            // 
            // txtSiteAddress_RP
            // 
            this.txtSiteAddress_RP.Location = new System.Drawing.Point(114, 102);
            this.txtSiteAddress_RP.Name = "txtSiteAddress_RP";
            this.txtSiteAddress_RP.Size = new System.Drawing.Size(344, 20);
            this.txtSiteAddress_RP.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(52, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 32;
            this.label4.Text = "Site Name";
            // 
            // txtSiteName_RP
            // 
            this.txtSiteName_RP.Location = new System.Drawing.Point(114, 75);
            this.txtSiteName_RP.Name = "txtSiteName_RP";
            this.txtSiteName_RP.Size = new System.Drawing.Size(344, 20);
            this.txtSiteName_RP.TabIndex = 2;
            // 
            // chkEnable_RP
            // 
            this.chkEnable_RP.AutoSize = true;
            this.chkEnable_RP.Location = new System.Drawing.Point(235, 52);
            this.chkEnable_RP.Name = "chkEnable_RP";
            this.chkEnable_RP.Size = new System.Drawing.Size(107, 17);
            this.chkEnable_RP.TabIndex = 2;
            this.chkEnable_RP.Text = "Enable this Client";
            this.chkEnable_RP.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(420, 53);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(37, 13);
            this.label17.TabIndex = 30;
            this.label17.Text = "Site Id";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(45, 28);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(61, 13);
            this.label18.TabIndex = 28;
            this.label18.Text = "PMC Name";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(30, 53);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(82, 13);
            this.label19.TabIndex = 27;
            this.label19.Text = "RRS Internal ID";
            // 
            // btnCancel_RP
            // 
            this.btnCancel_RP.Location = new System.Drawing.Point(751, 186);
            this.btnCancel_RP.Name = "btnCancel_RP";
            this.btnCancel_RP.Size = new System.Drawing.Size(49, 23);
            this.btnCancel_RP.TabIndex = 26;
            this.btnCancel_RP.Text = "Cancel";
            this.btnCancel_RP.UseVisualStyleBackColor = true;
            this.btnCancel_RP.Click += new System.EventHandler(this.btnCancel_RP_Click);
            // 
            // btnSave_RP
            // 
            this.btnSave_RP.Location = new System.Drawing.Point(696, 186);
            this.btnSave_RP.Name = "btnSave_RP";
            this.btnSave_RP.Size = new System.Drawing.Size(49, 23);
            this.btnSave_RP.TabIndex = 9;
            this.btnSave_RP.Text = "Save";
            this.btnSave_RP.UseVisualStyleBackColor = true;
            this.btnSave_RP.Click += new System.EventHandler(this.btnSave_RP_Click);
            // 
            // txtPMCName_RP
            // 
            this.txtPMCName_RP.Location = new System.Drawing.Point(114, 24);
            this.txtPMCName_RP.Name = "txtPMCName_RP";
            this.txtPMCName_RP.Size = new System.Drawing.Size(433, 20);
            this.txtPMCName_RP.TabIndex = 0;
            // 
            // txtInternalId_RP
            // 
            this.txtInternalId_RP.Location = new System.Drawing.Point(114, 49);
            this.txtInternalId_RP.MaxLength = 50;
            this.txtInternalId_RP.Name = "txtInternalId_RP";
            this.txtInternalId_RP.Size = new System.Drawing.Size(115, 20);
            this.txtInternalId_RP.TabIndex = 1;
            // 
            // txtSiteId_RP
            // 
            this.txtSiteId_RP.AccessibleDescription = "l";
            this.txtSiteId_RP.Location = new System.Drawing.Point(464, 50);
            this.txtSiteId_RP.Name = "txtSiteId_RP";
            this.txtSiteId_RP.Size = new System.Drawing.Size(84, 20);
            this.txtSiteId_RP.TabIndex = 2;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(17, 565);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(72, 13);
            this.label23.TabIndex = 48;
            this.label23.Text = "Entries Total :";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(138, 565);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(43, 13);
            this.label24.TabIndex = 49;
            this.label24.Text = "Active :";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(235, 565);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(51, 13);
            this.label25.TabIndex = 50;
            this.label25.Text = "Inactive :";
            // 
            // lblTotCli
            // 
            this.lblTotCli.Location = new System.Drawing.Point(92, 566);
            this.lblTotCli.Name = "lblTotCli";
            this.lblTotCli.Size = new System.Drawing.Size(30, 13);
            this.lblTotCli.TabIndex = 51;
            // 
            // lblActiveCli
            // 
            this.lblActiveCli.Location = new System.Drawing.Point(184, 566);
            this.lblActiveCli.Name = "lblActiveCli";
            this.lblActiveCli.Size = new System.Drawing.Size(30, 13);
            this.lblActiveCli.TabIndex = 52;
            // 
            // lblInactiveCli
            // 
            this.lblInactiveCli.Location = new System.Drawing.Point(283, 566);
            this.lblInactiveCli.Name = "lblInactiveCli";
            this.lblInactiveCli.Size = new System.Drawing.Size(30, 13);
            this.lblInactiveCli.TabIndex = 53;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(339, 190);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(78, 13);
            this.label26.TabIndex = 49;
            this.label26.Text = "Contact Phone";
            // 
            // txtPhone1
            // 
            this.txtPhone1.Location = new System.Drawing.Point(424, 186);
            this.txtPhone1.MaxLength = 50;
            this.txtPhone1.Name = "txtPhone1";
            this.txtPhone1.Size = new System.Drawing.Size(99, 20);
            this.txtPhone1.TabIndex = 48;
            // 
            // frmClients
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 591);
            this.Controls.Add(this.lblInactiveCli);
            this.Controls.Add(this.lblActiveCli);
            this.Controls.Add(this.lblTotCli);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.btnOpenClient);
            this.Controls.Add(this.cboVendors);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lvClients);
            this.Controls.Add(this.pnlDetlRP);
            this.Controls.Add(this.pnlDetl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmClients";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RRS Client Maintenance";
            this.Load += new System.EventHandler(this.frmClients_Load);
            this.cmenuClient.ResumeLayout(false);
            this.pnlDetl.ResumeLayout(false);
            this.pnlDetl.PerformLayout();
            this.pnlDetlRP.ResumeLayout(false);
            this.pnlDetlRP.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem copyClientToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboVendors;
        private System.Windows.Forms.Button btnOpenClient;
        private System.Windows.Forms.GroupBox pnlDetlRP;
        private System.Windows.Forms.CheckBox chkEnable_RP;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button btnCancel_RP;
        private System.Windows.Forms.Button btnSave_RP;
        private System.Windows.Forms.TextBox txtPMCName_RP;
        private System.Windows.Forms.TextBox txtInternalId_RP;
        private System.Windows.Forms.TextBox txtSiteId_RP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSiteName_RP;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSiteAddress_RP;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtEmail_RP;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtEncryptionKey;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtFirstDate;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtPMCId_RP;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtBalanceOwed;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtAfterMoveout;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label lblTotCli;
        private System.Windows.Forms.Label lblActiveCli;
        private System.Windows.Forms.Label lblInactiveCli;
        private System.Windows.Forms.ToolStripMenuItem unconfiguredClientReportToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveReport;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txtPhone1;
    }
}


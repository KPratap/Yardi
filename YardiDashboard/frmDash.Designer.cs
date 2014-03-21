namespace YardiDashboard
{
    partial class frmDash
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
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setFileLocationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runUnattendedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExtract = new System.Windows.Forms.ToolStripMenuItem();
            this.excludeHeadersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.includeHeadersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabCtl = new System.Windows.Forms.TabControl();
            this.tabClients = new System.Windows.Forms.TabPage();
            this.lvClients = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.getVersionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getPropertyConfigurationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.retrieveCollectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.retrieveAllCollectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabRetrieval = new System.Windows.Forms.TabPage();
            this.btnClear = new System.Windows.Forms.Button();
            this.lvMsg = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabFileLocations = new System.Windows.Forms.TabPage();
            this.txtLicFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOpenCollFalse = new System.Windows.Forms.Button();
            this.btnOpenColl = new System.Windows.Forms.Button();
            this.btnOpenRaw = new System.Windows.Forms.Button();
            this.txtCollFalse = new System.Windows.Forms.TextBox();
            this.lblCollFalse = new System.Windows.Forms.Label();
            this.txtColl = new System.Windows.Forms.TextBox();
            this.lblColl = new System.Windows.Forms.Label();
            this.txtRawXML = new System.Windows.Forms.TextBox();
            this.lblRawXML = new System.Windows.Forms.Label();
            this.lblRunMode = new System.Windows.Forms.Label();
            this.debugFormatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMain.SuspendLayout();
            this.tabCtl.SuspendLayout();
            this.tabClients.SuspendLayout();
            this.cMenu.SuspendLayout();
            this.tabRetrieval.SuspendLayout();
            this.tabFileLocations.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.actionToolStripMenuItem,
            this.mnuExtract,
            this.helpToolStripMenuItem});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuMain.Size = new System.Drawing.Size(1095, 28);
            this.menuMain.TabIndex = 0;
            this.menuMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restartToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // restartToolStripMenuItem
            // 
            this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            this.restartToolStripMenuItem.Size = new System.Drawing.Size(124, 24);
            this.restartToolStripMenuItem.Text = "Restart";
            this.restartToolStripMenuItem.Click += new System.EventHandler(this.restartToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(124, 24);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // actionToolStripMenuItem
            // 
            this.actionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chToolStripMenuItem,
            this.setFileLocationsToolStripMenuItem,
            this.runUnattendedToolStripMenuItem});
            this.actionToolStripMenuItem.Name = "actionToolStripMenuItem";
            this.actionToolStripMenuItem.Size = new System.Drawing.Size(64, 24);
            this.actionToolStripMenuItem.Text = "Action";
            // 
            // chToolStripMenuItem
            // 
            this.chToolStripMenuItem.Name = "chToolStripMenuItem";
            this.chToolStripMenuItem.Size = new System.Drawing.Size(204, 24);
            this.chToolStripMenuItem.Text = "Maintain Client List";
            this.chToolStripMenuItem.Click += new System.EventHandler(this.chToolStripMenuItem_Click);
            // 
            // setFileLocationsToolStripMenuItem
            // 
            this.setFileLocationsToolStripMenuItem.Name = "setFileLocationsToolStripMenuItem";
            this.setFileLocationsToolStripMenuItem.Size = new System.Drawing.Size(204, 24);
            this.setFileLocationsToolStripMenuItem.Text = "Set File Locations";
            this.setFileLocationsToolStripMenuItem.Click += new System.EventHandler(this.setFileLocationsToolStripMenuItem_Click);
            // 
            // runUnattendedToolStripMenuItem
            // 
            this.runUnattendedToolStripMenuItem.CheckOnClick = true;
            this.runUnattendedToolStripMenuItem.Name = "runUnattendedToolStripMenuItem";
            this.runUnattendedToolStripMenuItem.Size = new System.Drawing.Size(204, 24);
            this.runUnattendedToolStripMenuItem.Text = "Run Unattended";
            this.runUnattendedToolStripMenuItem.Click += new System.EventHandler(this.runUnattendedToolStripMenuItem_Click);
            // 
            // mnuExtract
            // 
            this.mnuExtract.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.excludeHeadersToolStripMenuItem,
            this.includeHeadersToolStripMenuItem,
            this.debugFormatToolStripMenuItem});
            this.mnuExtract.Name = "mnuExtract";
            this.mnuExtract.Size = new System.Drawing.Size(179, 24);
            this.mnuExtract.Text = "Extract Collections Data";
            this.mnuExtract.Click += new System.EventHandler(this.mnuExtract_Click);
            // 
            // excludeHeadersToolStripMenuItem
            // 
            this.excludeHeadersToolStripMenuItem.Name = "excludeHeadersToolStripMenuItem";
            this.excludeHeadersToolStripMenuItem.Size = new System.Drawing.Size(188, 24);
            this.excludeHeadersToolStripMenuItem.Text = "Exclude Headers";
            this.excludeHeadersToolStripMenuItem.Click += new System.EventHandler(this.excludeHeadersToolStripMenuItem_Click);
            // 
            // includeHeadersToolStripMenuItem
            // 
            this.includeHeadersToolStripMenuItem.Name = "includeHeadersToolStripMenuItem";
            this.includeHeadersToolStripMenuItem.Size = new System.Drawing.Size(188, 24);
            this.includeHeadersToolStripMenuItem.Text = "Include Headers";
            this.includeHeadersToolStripMenuItem.Click += new System.EventHandler(this.includeHeadersToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(119, 24);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // tabCtl
            // 
            this.tabCtl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabCtl.Controls.Add(this.tabClients);
            this.tabCtl.Controls.Add(this.tabRetrieval);
            this.tabCtl.Controls.Add(this.tabFileLocations);
            this.tabCtl.Location = new System.Drawing.Point(16, 76);
            this.tabCtl.Margin = new System.Windows.Forms.Padding(4);
            this.tabCtl.Name = "tabCtl";
            this.tabCtl.SelectedIndex = 0;
            this.tabCtl.Size = new System.Drawing.Size(1049, 480);
            this.tabCtl.TabIndex = 1;
            // 
            // tabClients
            // 
            this.tabClients.Controls.Add(this.lvClients);
            this.tabClients.Location = new System.Drawing.Point(4, 25);
            this.tabClients.Margin = new System.Windows.Forms.Padding(4);
            this.tabClients.Name = "tabClients";
            this.tabClients.Padding = new System.Windows.Forms.Padding(4);
            this.tabClients.Size = new System.Drawing.Size(1041, 451);
            this.tabClients.TabIndex = 1;
            this.tabClients.Text = "Client List";
            this.tabClients.UseVisualStyleBackColor = true;
            // 
            // lvClients
            // 
            this.lvClients.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvClients.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.lvClients.ContextMenuStrip = this.cMenu;
            this.lvClients.FullRowSelect = true;
            this.lvClients.GridLines = true;
            this.lvClients.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvClients.HideSelection = false;
            this.lvClients.Location = new System.Drawing.Point(13, 25);
            this.lvClients.Margin = new System.Windows.Forms.Padding(4);
            this.lvClients.Name = "lvClients";
            this.lvClients.Size = new System.Drawing.Size(1009, 397);
            this.lvClients.TabIndex = 21;
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
            this.columnHeader3.Width = 200;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Enabled";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Webservice URL";
            this.columnHeader5.Width = 280;
            // 
            // cMenu
            // 
            this.cMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getVersionToolStripMenuItem,
            this.getPropertyConfigurationsToolStripMenuItem,
            this.retrieveCollectionsToolStripMenuItem,
            this.retrieveAllCollectionsToolStripMenuItem});
            this.cMenu.Name = "cMenu";
            this.cMenu.Size = new System.Drawing.Size(263, 100);
            // 
            // getVersionToolStripMenuItem
            // 
            this.getVersionToolStripMenuItem.Name = "getVersionToolStripMenuItem";
            this.getVersionToolStripMenuItem.Size = new System.Drawing.Size(262, 24);
            this.getVersionToolStripMenuItem.Text = "GetVersion";
            this.getVersionToolStripMenuItem.Click += new System.EventHandler(this.getVersionToolStripMenuItem_Click);
            // 
            // getPropertyConfigurationsToolStripMenuItem
            // 
            this.getPropertyConfigurationsToolStripMenuItem.Name = "getPropertyConfigurationsToolStripMenuItem";
            this.getPropertyConfigurationsToolStripMenuItem.Size = new System.Drawing.Size(262, 24);
            this.getPropertyConfigurationsToolStripMenuItem.Text = "Get Property Configurations";
            this.getPropertyConfigurationsToolStripMenuItem.Click += new System.EventHandler(this.getPropertyConfigurationsToolStripMenuItem_Click);
            // 
            // retrieveCollectionsToolStripMenuItem
            // 
            this.retrieveCollectionsToolStripMenuItem.Name = "retrieveCollectionsToolStripMenuItem";
            this.retrieveCollectionsToolStripMenuItem.Size = new System.Drawing.Size(262, 24);
            this.retrieveCollectionsToolStripMenuItem.Text = "Retrieve Collections";
            this.retrieveCollectionsToolStripMenuItem.Click += new System.EventHandler(this.retrieveCollectionsToolStripMenuItem_Click);
            // 
            // retrieveAllCollectionsToolStripMenuItem
            // 
            this.retrieveAllCollectionsToolStripMenuItem.Name = "retrieveAllCollectionsToolStripMenuItem";
            this.retrieveAllCollectionsToolStripMenuItem.Size = new System.Drawing.Size(262, 24);
            this.retrieveAllCollectionsToolStripMenuItem.Text = "Retrieve All Collections";
            this.retrieveAllCollectionsToolStripMenuItem.Click += new System.EventHandler(this.retrieveAllCollectionsToolStripMenuItem_Click);
            // 
            // tabRetrieval
            // 
            this.tabRetrieval.Controls.Add(this.btnClear);
            this.tabRetrieval.Controls.Add(this.lvMsg);
            this.tabRetrieval.Location = new System.Drawing.Point(4, 25);
            this.tabRetrieval.Margin = new System.Windows.Forms.Padding(4);
            this.tabRetrieval.Name = "tabRetrieval";
            this.tabRetrieval.Size = new System.Drawing.Size(1041, 451);
            this.tabRetrieval.TabIndex = 2;
            this.tabRetrieval.Text = "Retrieval Status";
            this.tabRetrieval.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(27, 9);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(128, 28);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Clear Messages";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lvMsg
            // 
            this.lvMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvMsg.BackColor = System.Drawing.SystemColors.Info;
            this.lvMsg.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6});
            this.lvMsg.FullRowSelect = true;
            this.lvMsg.GridLines = true;
            this.lvMsg.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvMsg.HideSelection = false;
            this.lvMsg.Location = new System.Drawing.Point(27, 44);
            this.lvMsg.Margin = new System.Windows.Forms.Padding(4);
            this.lvMsg.Name = "lvMsg";
            this.lvMsg.Size = new System.Drawing.Size(984, 398);
            this.lvMsg.TabIndex = 1;
            this.lvMsg.UseCompatibleStateImageBehavior = false;
            this.lvMsg.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Message";
            this.columnHeader6.Width = 700;
            // 
            // tabFileLocations
            // 
            this.tabFileLocations.Controls.Add(this.txtLicFile);
            this.tabFileLocations.Controls.Add(this.label1);
            this.tabFileLocations.Controls.Add(this.btnOpenCollFalse);
            this.tabFileLocations.Controls.Add(this.btnOpenColl);
            this.tabFileLocations.Controls.Add(this.btnOpenRaw);
            this.tabFileLocations.Controls.Add(this.txtCollFalse);
            this.tabFileLocations.Controls.Add(this.lblCollFalse);
            this.tabFileLocations.Controls.Add(this.txtColl);
            this.tabFileLocations.Controls.Add(this.lblColl);
            this.tabFileLocations.Controls.Add(this.txtRawXML);
            this.tabFileLocations.Controls.Add(this.lblRawXML);
            this.tabFileLocations.Location = new System.Drawing.Point(4, 25);
            this.tabFileLocations.Margin = new System.Windows.Forms.Padding(4);
            this.tabFileLocations.Name = "tabFileLocations";
            this.tabFileLocations.Padding = new System.Windows.Forms.Padding(4);
            this.tabFileLocations.Size = new System.Drawing.Size(1041, 451);
            this.tabFileLocations.TabIndex = 0;
            this.tabFileLocations.Text = "File Locations";
            this.tabFileLocations.UseVisualStyleBackColor = true;
            // 
            // txtLicFile
            // 
            this.txtLicFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLicFile.Location = new System.Drawing.Point(237, 210);
            this.txtLicFile.Margin = new System.Windows.Forms.Padding(4);
            this.txtLicFile.Name = "txtLicFile";
            this.txtLicFile.ReadOnly = true;
            this.txtLicFile.Size = new System.Drawing.Size(629, 26);
            this.txtLicFile.TabIndex = 32;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(139, 215);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 17);
            this.label1.TabIndex = 31;
            this.label1.Text = "License File";
            // 
            // btnOpenCollFalse
            // 
            this.btnOpenCollFalse.Location = new System.Drawing.Point(892, 162);
            this.btnOpenCollFalse.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenCollFalse.Name = "btnOpenCollFalse";
            this.btnOpenCollFalse.Size = new System.Drawing.Size(100, 28);
            this.btnOpenCollFalse.TabIndex = 30;
            this.btnOpenCollFalse.Text = "Open";
            this.btnOpenCollFalse.UseVisualStyleBackColor = true;
            this.btnOpenCollFalse.Click += new System.EventHandler(this.btnOpenCollFalse_Click);
            // 
            // btnOpenColl
            // 
            this.btnOpenColl.Location = new System.Drawing.Point(892, 117);
            this.btnOpenColl.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenColl.Name = "btnOpenColl";
            this.btnOpenColl.Size = new System.Drawing.Size(100, 28);
            this.btnOpenColl.TabIndex = 29;
            this.btnOpenColl.Text = "Open";
            this.btnOpenColl.UseVisualStyleBackColor = true;
            this.btnOpenColl.Click += new System.EventHandler(this.btnOpenColl_Click);
            // 
            // btnOpenRaw
            // 
            this.btnOpenRaw.Location = new System.Drawing.Point(892, 71);
            this.btnOpenRaw.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenRaw.Name = "btnOpenRaw";
            this.btnOpenRaw.Size = new System.Drawing.Size(100, 28);
            this.btnOpenRaw.TabIndex = 28;
            this.btnOpenRaw.Text = "Open";
            this.btnOpenRaw.UseVisualStyleBackColor = true;
            this.btnOpenRaw.Click += new System.EventHandler(this.btnOpenRaw_Click);
            // 
            // txtCollFalse
            // 
            this.txtCollFalse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCollFalse.Location = new System.Drawing.Point(237, 162);
            this.txtCollFalse.Margin = new System.Windows.Forms.Padding(4);
            this.txtCollFalse.Name = "txtCollFalse";
            this.txtCollFalse.ReadOnly = true;
            this.txtCollFalse.Size = new System.Drawing.Size(629, 26);
            this.txtCollFalse.TabIndex = 27;
            // 
            // lblCollFalse
            // 
            this.lblCollFalse.AutoSize = true;
            this.lblCollFalse.Location = new System.Drawing.Point(75, 167);
            this.lblCollFalse.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCollFalse.Name = "lblCollFalse";
            this.lblCollFalse.Size = new System.Drawing.Size(158, 17);
            this.lblCollFalse.TabIndex = 26;
            this.lblCollFalse.Text = "Collections Status False";
            // 
            // txtColl
            // 
            this.txtColl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtColl.Location = new System.Drawing.Point(237, 118);
            this.txtColl.Margin = new System.Windows.Forms.Padding(4);
            this.txtColl.Name = "txtColl";
            this.txtColl.ReadOnly = true;
            this.txtColl.Size = new System.Drawing.Size(629, 26);
            this.txtColl.TabIndex = 25;
            // 
            // lblColl
            // 
            this.lblColl.AutoSize = true;
            this.lblColl.Location = new System.Drawing.Point(77, 122);
            this.lblColl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblColl.Name = "lblColl";
            this.lblColl.Size = new System.Drawing.Size(149, 17);
            this.lblColl.TabIndex = 24;
            this.lblColl.Text = "Collections Downloads";
            // 
            // txtRawXML
            // 
            this.txtRawXML.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRawXML.Location = new System.Drawing.Point(237, 74);
            this.txtRawXML.Margin = new System.Windows.Forms.Padding(4);
            this.txtRawXML.Name = "txtRawXML";
            this.txtRawXML.ReadOnly = true;
            this.txtRawXML.Size = new System.Drawing.Size(629, 26);
            this.txtRawXML.TabIndex = 23;
            // 
            // lblRawXML
            // 
            this.lblRawXML.AutoSize = true;
            this.lblRawXML.Location = new System.Drawing.Point(132, 79);
            this.lblRawXML.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRawXML.Name = "lblRawXML";
            this.lblRawXML.Size = new System.Drawing.Size(93, 17);
            this.lblRawXML.TabIndex = 22;
            this.lblRawXML.Text = "Raw XML File";
            // 
            // lblRunMode
            // 
            this.lblRunMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRunMode.ForeColor = System.Drawing.Color.Yellow;
            this.lblRunMode.Location = new System.Drawing.Point(131, 43);
            this.lblRunMode.Name = "lblRunMode";
            this.lblRunMode.Size = new System.Drawing.Size(773, 29);
            this.lblRunMode.TabIndex = 2;
            this.lblRunMode.Text = "Run mode";
            this.lblRunMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // debugFormatToolStripMenuItem
            // 
            this.debugFormatToolStripMenuItem.Name = "debugFormatToolStripMenuItem";
            this.debugFormatToolStripMenuItem.Size = new System.Drawing.Size(188, 24);
            this.debugFormatToolStripMenuItem.Text = "Debug Format";
            this.debugFormatToolStripMenuItem.Click += new System.EventHandler(this.debugFormatToolStripMenuItem_Click);
            // 
            // frmDash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1095, 588);
            this.Controls.Add(this.lblRunMode);
            this.Controls.Add(this.tabCtl);
            this.Controls.Add(this.menuMain);
            this.MainMenuStrip = this.menuMain;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1110, 624);
            this.Name = "frmDash";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "YardiDashboard";
            this.Load += new System.EventHandler(this.frmDash_Load);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.tabCtl.ResumeLayout(false);
            this.tabClients.ResumeLayout(false);
            this.cMenu.ResumeLayout(false);
            this.tabRetrieval.ResumeLayout(false);
            this.tabFileLocations.ResumeLayout(false);
            this.tabFileLocations.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TabControl tabCtl;
        private System.Windows.Forms.TabPage tabFileLocations;
        private System.Windows.Forms.TabPage tabClients;
        private System.Windows.Forms.TabPage tabRetrieval;
        private System.Windows.Forms.TextBox txtCollFalse;
        private System.Windows.Forms.Label lblCollFalse;
        private System.Windows.Forms.TextBox txtColl;
        private System.Windows.Forms.Label lblColl;
        private System.Windows.Forms.TextBox txtRawXML;
        private System.Windows.Forms.Label lblRawXML;
        private System.Windows.Forms.ListView lvClients;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Button btnOpenRaw;
        private System.Windows.Forms.Button btnOpenCollFalse;
        private System.Windows.Forms.Button btnOpenColl;
        private System.Windows.Forms.TextBox txtLicFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip cMenu;
        private System.Windows.Forms.ToolStripMenuItem retrieveCollectionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ListView lvMsg;
        private System.Windows.Forms.ToolStripMenuItem mnuExtract;
        private System.Windows.Forms.ToolStripMenuItem getVersionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getPropertyConfigurationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem excludeHeadersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem includeHeadersToolStripMenuItem;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ToolStripMenuItem restartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem retrieveAllCollectionsToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ToolStripMenuItem actionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setFileLocationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runUnattendedToolStripMenuItem;
        private System.Windows.Forms.Label lblRunMode;
        private System.Windows.Forms.ToolStripMenuItem debugFormatToolStripMenuItem;
    }
}


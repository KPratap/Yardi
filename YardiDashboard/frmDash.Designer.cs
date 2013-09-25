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
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuYardiFileLocator = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuYardiClients = new System.Windows.Forms.ToolStripMenuItem();
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
            this.tabRetrieval = new System.Windows.Forms.TabPage();
            this.btnClear = new System.Windows.Forms.Button();
            this.lvMsg = new System.Windows.Forms.ListView();
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
            this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.mnuYardiFileLocator,
            this.mnuYardiClients,
            this.mnuExtract,
            this.helpToolStripMenuItem});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(821, 24);
            this.menuMain.TabIndex = 0;
            this.menuMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restartToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // mnuYardiFileLocator
            // 
            this.mnuYardiFileLocator.Name = "mnuYardiFileLocator";
            this.mnuYardiFileLocator.Size = new System.Drawing.Size(135, 20);
            this.mnuYardiFileLocator.Text = "Change File Locations";
            this.mnuYardiFileLocator.ToolTipText = "Launch Yardi File Locator tool";
            this.mnuYardiFileLocator.Click += new System.EventHandler(this.mnuYardiFileLocator_Click);
            // 
            // mnuYardiClients
            // 
            this.mnuYardiClients.Name = "mnuYardiClients";
            this.mnuYardiClients.Size = new System.Drawing.Size(121, 20);
            this.mnuYardiClients.Text = "Maintain Client List";
            this.mnuYardiClients.ToolTipText = "Launch Yardi Client Setup Tool";
            this.mnuYardiClients.Click += new System.EventHandler(this.mnuYardiClients_Click);
            // 
            // mnuExtract
            // 
            this.mnuExtract.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.excludeHeadersToolStripMenuItem,
            this.includeHeadersToolStripMenuItem});
            this.mnuExtract.Name = "mnuExtract";
            this.mnuExtract.Size = new System.Drawing.Size(143, 20);
            this.mnuExtract.Text = "Extract Collections Data";
            // 
            // excludeHeadersToolStripMenuItem
            // 
            this.excludeHeadersToolStripMenuItem.Name = "excludeHeadersToolStripMenuItem";
            this.excludeHeadersToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.excludeHeadersToolStripMenuItem.Text = "Exclude Headers";
            this.excludeHeadersToolStripMenuItem.Click += new System.EventHandler(this.excludeHeadersToolStripMenuItem_Click);
            // 
            // includeHeadersToolStripMenuItem
            // 
            this.includeHeadersToolStripMenuItem.Name = "includeHeadersToolStripMenuItem";
            this.includeHeadersToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.includeHeadersToolStripMenuItem.Text = "Include Headers";
            this.includeHeadersToolStripMenuItem.Click += new System.EventHandler(this.includeHeadersToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // tabCtl
            // 
            this.tabCtl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabCtl.Controls.Add(this.tabClients);
            this.tabCtl.Controls.Add(this.tabRetrieval);
            this.tabCtl.Controls.Add(this.tabFileLocations);
            this.tabCtl.Location = new System.Drawing.Point(12, 43);
            this.tabCtl.Name = "tabCtl";
            this.tabCtl.SelectedIndex = 0;
            this.tabCtl.Size = new System.Drawing.Size(787, 409);
            this.tabCtl.TabIndex = 1;
            // 
            // tabClients
            // 
            this.tabClients.Controls.Add(this.lvClients);
            this.tabClients.Location = new System.Drawing.Point(4, 22);
            this.tabClients.Name = "tabClients";
            this.tabClients.Padding = new System.Windows.Forms.Padding(3);
            this.tabClients.Size = new System.Drawing.Size(779, 383);
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
            this.lvClients.Location = new System.Drawing.Point(10, 20);
            this.lvClients.Name = "lvClients";
            this.lvClients.Size = new System.Drawing.Size(758, 342);
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
            this.retrieveCollectionsToolStripMenuItem});
            this.cMenu.Name = "cMenu";
            this.cMenu.Size = new System.Drawing.Size(223, 92);
            // 
            // getVersionToolStripMenuItem
            // 
            this.getVersionToolStripMenuItem.Name = "getVersionToolStripMenuItem";
            this.getVersionToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.getVersionToolStripMenuItem.Text = "GetVersion";
            this.getVersionToolStripMenuItem.Click += new System.EventHandler(this.getVersionToolStripMenuItem_Click);
            // 
            // getPropertyConfigurationsToolStripMenuItem
            // 
            this.getPropertyConfigurationsToolStripMenuItem.Name = "getPropertyConfigurationsToolStripMenuItem";
            this.getPropertyConfigurationsToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.getPropertyConfigurationsToolStripMenuItem.Text = "Get Property Configurations";
            this.getPropertyConfigurationsToolStripMenuItem.Click += new System.EventHandler(this.getPropertyConfigurationsToolStripMenuItem_Click);
            // 
            // retrieveCollectionsToolStripMenuItem
            // 
            this.retrieveCollectionsToolStripMenuItem.Name = "retrieveCollectionsToolStripMenuItem";
            this.retrieveCollectionsToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.retrieveCollectionsToolStripMenuItem.Text = "Retrieve Collections";
            this.retrieveCollectionsToolStripMenuItem.Click += new System.EventHandler(this.retrieveCollectionsToolStripMenuItem_Click);
            // 
            // tabRetrieval
            // 
            this.tabRetrieval.Controls.Add(this.btnClear);
            this.tabRetrieval.Controls.Add(this.lvMsg);
            this.tabRetrieval.Location = new System.Drawing.Point(4, 22);
            this.tabRetrieval.Name = "tabRetrieval";
            this.tabRetrieval.Size = new System.Drawing.Size(779, 383);
            this.tabRetrieval.TabIndex = 2;
            this.tabRetrieval.Text = "Retrieval Status";
            this.tabRetrieval.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(20, 7);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(96, 23);
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
            this.lvMsg.Location = new System.Drawing.Point(20, 36);
            this.lvMsg.Name = "lvMsg";
            this.lvMsg.Size = new System.Drawing.Size(739, 324);
            this.lvMsg.TabIndex = 1;
            this.lvMsg.UseCompatibleStateImageBehavior = false;
            this.lvMsg.View = System.Windows.Forms.View.List;
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
            this.tabFileLocations.Location = new System.Drawing.Point(4, 22);
            this.tabFileLocations.Name = "tabFileLocations";
            this.tabFileLocations.Padding = new System.Windows.Forms.Padding(3);
            this.tabFileLocations.Size = new System.Drawing.Size(779, 383);
            this.tabFileLocations.TabIndex = 0;
            this.tabFileLocations.Text = "File Locations";
            this.tabFileLocations.UseVisualStyleBackColor = true;
            // 
            // txtLicFile
            // 
            this.txtLicFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLicFile.Location = new System.Drawing.Point(178, 171);
            this.txtLicFile.Name = "txtLicFile";
            this.txtLicFile.ReadOnly = true;
            this.txtLicFile.Size = new System.Drawing.Size(473, 22);
            this.txtLicFile.TabIndex = 32;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(104, 175);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "License File";
            // 
            // btnOpenCollFalse
            // 
            this.btnOpenCollFalse.Location = new System.Drawing.Point(669, 132);
            this.btnOpenCollFalse.Name = "btnOpenCollFalse";
            this.btnOpenCollFalse.Size = new System.Drawing.Size(75, 23);
            this.btnOpenCollFalse.TabIndex = 30;
            this.btnOpenCollFalse.Text = "Open";
            this.btnOpenCollFalse.UseVisualStyleBackColor = true;
            this.btnOpenCollFalse.Click += new System.EventHandler(this.btnOpenCollFalse_Click);
            // 
            // btnOpenColl
            // 
            this.btnOpenColl.Location = new System.Drawing.Point(669, 95);
            this.btnOpenColl.Name = "btnOpenColl";
            this.btnOpenColl.Size = new System.Drawing.Size(75, 23);
            this.btnOpenColl.TabIndex = 29;
            this.btnOpenColl.Text = "Open";
            this.btnOpenColl.UseVisualStyleBackColor = true;
            this.btnOpenColl.Click += new System.EventHandler(this.btnOpenColl_Click);
            // 
            // btnOpenRaw
            // 
            this.btnOpenRaw.Location = new System.Drawing.Point(669, 58);
            this.btnOpenRaw.Name = "btnOpenRaw";
            this.btnOpenRaw.Size = new System.Drawing.Size(75, 23);
            this.btnOpenRaw.TabIndex = 28;
            this.btnOpenRaw.Text = "Open";
            this.btnOpenRaw.UseVisualStyleBackColor = true;
            this.btnOpenRaw.Click += new System.EventHandler(this.btnOpenRaw_Click);
            // 
            // txtCollFalse
            // 
            this.txtCollFalse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCollFalse.Location = new System.Drawing.Point(178, 132);
            this.txtCollFalse.Name = "txtCollFalse";
            this.txtCollFalse.ReadOnly = true;
            this.txtCollFalse.Size = new System.Drawing.Size(473, 22);
            this.txtCollFalse.TabIndex = 27;
            // 
            // lblCollFalse
            // 
            this.lblCollFalse.AutoSize = true;
            this.lblCollFalse.Location = new System.Drawing.Point(56, 136);
            this.lblCollFalse.Name = "lblCollFalse";
            this.lblCollFalse.Size = new System.Drawing.Size(119, 13);
            this.lblCollFalse.TabIndex = 26;
            this.lblCollFalse.Text = "Collections Status False";
            // 
            // txtColl
            // 
            this.txtColl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtColl.Location = new System.Drawing.Point(178, 96);
            this.txtColl.Name = "txtColl";
            this.txtColl.ReadOnly = true;
            this.txtColl.Size = new System.Drawing.Size(473, 22);
            this.txtColl.TabIndex = 25;
            // 
            // lblColl
            // 
            this.lblColl.AutoSize = true;
            this.lblColl.Location = new System.Drawing.Point(58, 99);
            this.lblColl.Name = "lblColl";
            this.lblColl.Size = new System.Drawing.Size(114, 13);
            this.lblColl.TabIndex = 24;
            this.lblColl.Text = "Collections Downloads";
            // 
            // txtRawXML
            // 
            this.txtRawXML.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRawXML.Location = new System.Drawing.Point(178, 60);
            this.txtRawXML.Name = "txtRawXML";
            this.txtRawXML.ReadOnly = true;
            this.txtRawXML.Size = new System.Drawing.Size(473, 22);
            this.txtRawXML.TabIndex = 23;
            // 
            // lblRawXML
            // 
            this.lblRawXML.AutoSize = true;
            this.lblRawXML.Location = new System.Drawing.Point(99, 64);
            this.lblRawXML.Name = "lblRawXML";
            this.lblRawXML.Size = new System.Drawing.Size(73, 13);
            this.lblRawXML.TabIndex = 22;
            this.lblRawXML.Text = "Raw XML File";
            // 
            // restartToolStripMenuItem
            // 
            this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            this.restartToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.restartToolStripMenuItem.Text = "Restart";
            this.restartToolStripMenuItem.Click += new System.EventHandler(this.restartToolStripMenuItem_Click);
            // 
            // frmDash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 478);
            this.Controls.Add(this.tabCtl);
            this.Controls.Add(this.menuMain);
            this.MainMenuStrip = this.menuMain;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(837, 516);
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
        private System.Windows.Forms.ToolStripMenuItem mnuYardiFileLocator;
        private System.Windows.Forms.ToolStripMenuItem mnuYardiClients;
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
    }
}


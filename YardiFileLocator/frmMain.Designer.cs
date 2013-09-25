namespace YardiFileLocator
{
    partial class frmMain
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
            this.btnSetXMLFolder = new System.Windows.Forms.Button();
            this.txtRawXML = new System.Windows.Forms.TextBox();
            this.lblRawXML = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSetCollFolder = new System.Windows.Forms.Button();
            this.txtColl = new System.Windows.Forms.TextBox();
            this.lblColl = new System.Windows.Forms.Label();
            this.btnCollFalse = new System.Windows.Forms.Button();
            this.txtCollFalse = new System.Windows.Forms.TextBox();
            this.lblCollFalse = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.folderBrw = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // btnSetXMLFolder
            // 
            this.btnSetXMLFolder.Location = new System.Drawing.Point(658, 64);
            this.btnSetXMLFolder.Name = "btnSetXMLFolder";
            this.btnSetXMLFolder.Size = new System.Drawing.Size(75, 23);
            this.btnSetXMLFolder.TabIndex = 0;
            this.btnSetXMLFolder.Text = "Change..";
            this.btnSetXMLFolder.UseVisualStyleBackColor = true;
            this.btnSetXMLFolder.Click += new System.EventHandler(this.btnSetXMLFolder_Click);
            // 
            // txtRawXML
            // 
            this.txtRawXML.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRawXML.Location = new System.Drawing.Point(151, 65);
            this.txtRawXML.Name = "txtRawXML";
            this.txtRawXML.ReadOnly = true;
            this.txtRawXML.Size = new System.Drawing.Size(501, 22);
            this.txtRawXML.TabIndex = 14;
            // 
            // lblRawXML
            // 
            this.lblRawXML.AutoSize = true;
            this.lblRawXML.Location = new System.Drawing.Point(68, 69);
            this.lblRawXML.Name = "lblRawXML";
            this.lblRawXML.Size = new System.Drawing.Size(73, 13);
            this.lblRawXML.TabIndex = 13;
            this.lblRawXML.Text = "Raw XML File";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(22, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(235, 16);
            this.label4.TabIndex = 16;
            this.label4.Text = "Please set the folders for different files:";
            // 
            // btnSetCollFolder
            // 
            this.btnSetCollFolder.Location = new System.Drawing.Point(658, 100);
            this.btnSetCollFolder.Name = "btnSetCollFolder";
            this.btnSetCollFolder.Size = new System.Drawing.Size(75, 23);
            this.btnSetCollFolder.TabIndex = 1;
            this.btnSetCollFolder.Text = "Change..";
            this.btnSetCollFolder.UseVisualStyleBackColor = true;
            this.btnSetCollFolder.Click += new System.EventHandler(this.btnSetCollFolder_Click);
            // 
            // txtColl
            // 
            this.txtColl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtColl.Location = new System.Drawing.Point(151, 101);
            this.txtColl.Name = "txtColl";
            this.txtColl.ReadOnly = true;
            this.txtColl.Size = new System.Drawing.Size(501, 22);
            this.txtColl.TabIndex = 18;
            // 
            // lblColl
            // 
            this.lblColl.AutoSize = true;
            this.lblColl.Location = new System.Drawing.Point(39, 104);
            this.lblColl.Name = "lblColl";
            this.lblColl.Size = new System.Drawing.Size(99, 13);
            this.lblColl.TabIndex = 17;
            this.lblColl.Text = "Collections Extracts";
            // 
            // btnCollFalse
            // 
            this.btnCollFalse.Location = new System.Drawing.Point(658, 136);
            this.btnCollFalse.Name = "btnCollFalse";
            this.btnCollFalse.Size = new System.Drawing.Size(75, 23);
            this.btnCollFalse.TabIndex = 2;
            this.btnCollFalse.Text = "Change..";
            this.btnCollFalse.UseVisualStyleBackColor = true;
            this.btnCollFalse.Click += new System.EventHandler(this.btnCollFalse_Click);
            // 
            // txtCollFalse
            // 
            this.txtCollFalse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCollFalse.Location = new System.Drawing.Point(151, 137);
            this.txtCollFalse.Name = "txtCollFalse";
            this.txtCollFalse.ReadOnly = true;
            this.txtCollFalse.Size = new System.Drawing.Size(501, 22);
            this.txtCollFalse.TabIndex = 21;
            // 
            // lblCollFalse
            // 
            this.lblCollFalse.AutoSize = true;
            this.lblCollFalse.Location = new System.Drawing.Point(4, 141);
            this.lblCollFalse.Name = "lblCollFalse";
            this.lblCollFalse.Size = new System.Drawing.Size(133, 13);
            this.lblCollFalse.TabIndex = 20;
            this.lblCollFalse.Text = "Collections Extracts (False)";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(561, 194);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(658, 194);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 262);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCollFalse);
            this.Controls.Add(this.txtCollFalse);
            this.Controls.Add(this.lblCollFalse);
            this.Controls.Add(this.btnSetCollFolder);
            this.Controls.Add(this.txtColl);
            this.Controls.Add(this.lblColl);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnSetXMLFolder);
            this.Controls.Add(this.txtRawXML);
            this.Controls.Add(this.lblRawXML);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(806, 300);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Yardi File Locator";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSetXMLFolder;
        private System.Windows.Forms.TextBox txtRawXML;
        private System.Windows.Forms.Label lblRawXML;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSetCollFolder;
        private System.Windows.Forms.TextBox txtColl;
        private System.Windows.Forms.Label lblColl;
        private System.Windows.Forms.Button btnCollFalse;
        private System.Windows.Forms.TextBox txtCollFalse;
        private System.Windows.Forms.Label lblCollFalse;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.FolderBrowserDialog folderBrw;

    }
}


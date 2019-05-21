namespace STG
{
    partial class MainWindow
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.homeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateShippingFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importShippedFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.receivingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importInboundContainerFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bBBVendorNumberToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.receivingToolStripMenuItem,
            this.manageToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1165, 24);
            this.menuStrip1.TabIndex = 20;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.homeToolStripMenuItem,
            this.generateShippingFileToolStripMenuItem,
            this.importShippedFileToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // homeToolStripMenuItem
            // 
            this.homeToolStripMenuItem.Name = "homeToolStripMenuItem";
            this.homeToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.homeToolStripMenuItem.Text = "Home";
            this.homeToolStripMenuItem.Click += new System.EventHandler(this.homeToolStripMenuItem_Click);
            // 
            // generateShippingFileToolStripMenuItem
            // 
            this.generateShippingFileToolStripMenuItem.Name = "generateShippingFileToolStripMenuItem";
            this.generateShippingFileToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.generateShippingFileToolStripMenuItem.Text = "Generate Shipping File";
            this.generateShippingFileToolStripMenuItem.Click += new System.EventHandler(this.generateShippingFileToolStripMenuItem_Click);
            // 
            // importShippedFileToolStripMenuItem
            // 
            this.importShippedFileToolStripMenuItem.Name = "importShippedFileToolStripMenuItem";
            this.importShippedFileToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.importShippedFileToolStripMenuItem.Text = " Import Shipped File";
            this.importShippedFileToolStripMenuItem.Click += new System.EventHandler(this.importShippedFileToolStripMenuItem_Click);
            // 
            // receivingToolStripMenuItem
            // 
            this.receivingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importInboundContainerFileToolStripMenuItem});
            this.receivingToolStripMenuItem.Name = "receivingToolStripMenuItem";
            this.receivingToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.receivingToolStripMenuItem.Text = "Receiving";
            // 
            // importInboundContainerFileToolStripMenuItem
            // 
            this.importInboundContainerFileToolStripMenuItem.Name = "importInboundContainerFileToolStripMenuItem";
            this.importInboundContainerFileToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.importInboundContainerFileToolStripMenuItem.Text = "Import Inbound Container File";
            this.importInboundContainerFileToolStripMenuItem.Click += new System.EventHandler(this.importInboundContainerFileToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click_1);
            // 
            // manageToolStripMenuItem
            // 
            this.manageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bBBVendorNumberToolStripMenuItem});
            this.manageToolStripMenuItem.Name = "manageToolStripMenuItem";
            this.manageToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.manageToolStripMenuItem.Text = "Manage";
            // 
            // bBBVendorNumberToolStripMenuItem
            // 
            this.bBBVendorNumberToolStripMenuItem.Name = "bBBVendorNumberToolStripMenuItem";
            this.bBBVendorNumberToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.bBBVendorNumberToolStripMenuItem.Text = "BBB Vendor Number";
            this.bBBVendorNumberToolStripMenuItem.Click += new System.EventHandler(this.BBBVendorNumberToolStripMenuItem_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImage = global::STG.Properties.Resources.STG;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1165, 626);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.Text = "St. George Order Sync";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateShippingFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importShippedFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem homeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem receivingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importInboundContainerFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bBBVendorNumberToolStripMenuItem;
    }
}
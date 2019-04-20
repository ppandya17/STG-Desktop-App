namespace STG
{
    partial class FrmReceiving
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnImportInbound = new System.Windows.Forms.Button();
            this.DgImportInbound = new System.Windows.Forms.DataGridView();
            this.BtnImportInboundContainers = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgImportInbound)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.BtnImportInbound);
            this.groupBox1.Controls.Add(this.DgImportInbound);
            this.groupBox1.Controls.Add(this.BtnImportInboundContainers);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(28, 77);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1207, 656);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Import Inboud File of Containers";
            // 
            // BtnImportInbound
            // 
            this.BtnImportInbound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnImportInbound.Location = new System.Drawing.Point(1024, 34);
            this.BtnImportInbound.Name = "BtnImportInbound";
            this.BtnImportInbound.Size = new System.Drawing.Size(137, 23);
            this.BtnImportInbound.TabIndex = 3;
            this.BtnImportInbound.Text = "Import";
            this.BtnImportInbound.UseVisualStyleBackColor = true;
            this.BtnImportInbound.Click += new System.EventHandler(this.BtnImportInbound_Click);
            // 
            // DgImportInbound
            // 
            this.DgImportInbound.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgImportInbound.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgImportInbound.Location = new System.Drawing.Point(21, 89);
            this.DgImportInbound.Name = "DgImportInbound";
            this.DgImportInbound.Size = new System.Drawing.Size(1170, 554);
            this.DgImportInbound.TabIndex = 2;
            // 
            // BtnImportInboundContainers
            // 
            this.BtnImportInboundContainers.Location = new System.Drawing.Point(133, 34);
            this.BtnImportInboundContainers.Name = "BtnImportInboundContainers";
            this.BtnImportInboundContainers.Size = new System.Drawing.Size(192, 23);
            this.BtnImportInboundContainers.TabIndex = 1;
            this.BtnImportInboundContainers.Text = "Select File";
            this.BtnImportInboundContainers.UseVisualStyleBackColor = true;
            this.BtnImportInboundContainers.Click += new System.EventHandler(this.BtnImportInboundContainers_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select File to Import : ";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(496, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(322, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Add Inbound Container Information for STG";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // FrmReceiving
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1271, 745);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmReceiving";
            this.ShowInTaskbar = false;
            this.Text = "FrmReceiving";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmReceiving_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgImportInbound)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnImportInboundContainers;
        private System.Windows.Forms.DataGridView DgImportInbound;
        private System.Windows.Forms.Button BtnImportInbound;
        private System.Windows.Forms.Label label2;
    }
}
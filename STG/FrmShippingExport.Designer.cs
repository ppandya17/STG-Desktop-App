namespace STG
{
    partial class FrmShippingExport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.version = new System.Windows.Forms.Label();
            this.version_number = new System.Windows.Forms.Label();
            this.sqlDataLoad = new System.Windows.Forms.Button();
            this.dataGridGP = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonExcel = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridSTG = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.TotalQuantity = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtWaveSearch = new System.Windows.Forms.TextBox();
            this.BtnSentSTG = new System.Windows.Forms.Button();
            this.lblVersionNumText = new System.Windows.Forms.Label();
            this.lblVerisonNumber = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridGP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSTG)).BeginInit();
            this.SuspendLayout();
            // 
            // version
            // 
            this.version.AutoSize = true;
            this.version.Location = new System.Drawing.Point(38, 564);
            this.version.Name = "version";
            this.version.Size = new System.Drawing.Size(42, 13);
            this.version.TabIndex = 0;
            this.version.Text = "Version";
            // 
            // version_number
            // 
            this.version_number.AutoSize = true;
            this.version_number.Location = new System.Drawing.Point(87, 564);
            this.version_number.Name = "version_number";
            this.version_number.Size = new System.Drawing.Size(82, 13);
            this.version_number.TabIndex = 1;
            this.version_number.Text = "version_number";
            // 
            // sqlDataLoad
            // 
            this.sqlDataLoad.Location = new System.Drawing.Point(41, 40);
            this.sqlDataLoad.Name = "sqlDataLoad";
            this.sqlDataLoad.Size = new System.Drawing.Size(161, 23);
            this.sqlDataLoad.TabIndex = 1;
            this.sqlDataLoad.Text = "Load Orders";
            this.sqlDataLoad.UseVisualStyleBackColor = true;
            this.sqlDataLoad.Click += new System.EventHandler(this.SqlDataLoad_Click);
            // 
            // dataGridGP
            // 
            this.dataGridGP.AllowUserToAddRows = false;
            this.dataGridGP.AllowUserToDeleteRows = false;
            this.dataGridGP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridGP.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridGP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridGP.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridGP.Location = new System.Drawing.Point(41, 68);
            this.dataGridGP.Name = "dataGridGP";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridGP.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridGP.Size = new System.Drawing.Size(1309, 313);
            this.dataGridGP.TabIndex = 5;
            this.dataGridGP.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridGP_RowPostPaint);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(651, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(222, 19);
            this.label2.TabIndex = 7;
            this.label2.Text = "Orders for St. George Locations";
            // 
            // buttonExcel
            // 
            this.buttonExcel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonExcel.Location = new System.Drawing.Point(434, 387);
            this.buttonExcel.Name = "buttonExcel";
            this.buttonExcel.Size = new System.Drawing.Size(243, 35);
            this.buttonExcel.TabIndex = 8;
            this.buttonExcel.Text = "Download Excel";
            this.buttonExcel.UseVisualStyleBackColor = true;
            this.buttonExcel.Click += new System.EventHandler(this.BtnGenerateExcel);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button2.Location = new System.Drawing.Point(697, 387);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(235, 35);
            this.button2.TabIndex = 9;
            this.button2.Text = "Send File to St. George Warehouse";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.SubmittoSTG);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(579, 435);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 19);
            this.label1.TabIndex = 10;
            this.label1.Text = "Orders Already Sent to STG";
            // 
            // dataGridSTG
            // 
            this.dataGridSTG.AllowUserToAddRows = false;
            this.dataGridSTG.AllowUserToDeleteRows = false;
            this.dataGridSTG.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridSTG.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridSTG.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridSTG.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridSTG.Location = new System.Drawing.Point(41, 480);
            this.dataGridSTG.Name = "dataGridSTG";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridSTG.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridSTG.Size = new System.Drawing.Size(1309, 199);
            this.dataGridSTG.TabIndex = 11;
            this.dataGridSTG.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridSTG_RowPostPaint);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1066, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Total Quantity:";
            // 
            // TotalQuantity
            // 
            this.TotalQuantity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TotalQuantity.AutoSize = true;
            this.TotalQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalQuantity.Location = new System.Drawing.Point(1161, 47);
            this.TotalQuantity.Name = "TotalQuantity";
            this.TotalQuantity.Size = new System.Drawing.Size(14, 13);
            this.TotalQuantity.TabIndex = 13;
            this.TotalQuantity.Text = "0";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 457);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Search By Wave : ";
            // 
            // txtWaveSearch
            // 
            this.txtWaveSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtWaveSearch.Location = new System.Drawing.Point(141, 454);
            this.txtWaveSearch.Name = "txtWaveSearch";
            this.txtWaveSearch.Size = new System.Drawing.Size(151, 20);
            this.txtWaveSearch.TabIndex = 15;
            this.txtWaveSearch.TextChanged += new System.EventHandler(this.TxtWaveSearch_TextChanged);
            // 
            // BtnSentSTG
            // 
            this.BtnSentSTG.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.BtnSentSTG.Location = new System.Drawing.Point(566, 685);
            this.BtnSentSTG.Name = "BtnSentSTG";
            this.BtnSentSTG.Size = new System.Drawing.Size(243, 35);
            this.BtnSentSTG.TabIndex = 16;
            this.BtnSentSTG.Text = "Download Excel";
            this.BtnSentSTG.UseVisualStyleBackColor = true;
            this.BtnSentSTG.Click += new System.EventHandler(this.BtnSentSTG_Click);
            // 
            // lblVersionNumText
            // 
            this.lblVersionNumText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblVersionNumText.AutoSize = true;
            this.lblVersionNumText.Location = new System.Drawing.Point(44, 763);
            this.lblVersionNumText.Name = "lblVersionNumText";
            this.lblVersionNumText.Size = new System.Drawing.Size(48, 13);
            this.lblVersionNumText.TabIndex = 17;
            this.lblVersionNumText.Text = "Version :";
            // 
            // lblVerisonNumber
            // 
            this.lblVerisonNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblVerisonNumber.AutoSize = true;
            this.lblVerisonNumber.Location = new System.Drawing.Point(97, 764);
            this.lblVerisonNumber.Name = "lblVerisonNumber";
            this.lblVerisonNumber.Size = new System.Drawing.Size(35, 13);
            this.lblVerisonNumber.TabIndex = 18;
            this.lblVerisonNumber.Text = "label5";
            // 
            // FrmShippingExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1386, 788);
            this.Controls.Add(this.lblVerisonNumber);
            this.Controls.Add(this.lblVersionNumText);
            this.Controls.Add(this.BtnSentSTG);
            this.Controls.Add(this.txtWaveSearch);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TotalQuantity);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridSTG);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buttonExcel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridGP);
            this.Controls.Add(this.sqlDataLoad);
            this.Controls.Add(this.version_number);
            this.Controls.Add(this.version);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmShippingExport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "St. George Order Sync";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridGP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSTG)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label version;
        private System.Windows.Forms.Label version_number;
        private System.Windows.Forms.Button sqlDataLoad;
        private System.Windows.Forms.DataGridView dataGridGP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonExcel;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridSTG;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label TotalQuantity;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtWaveSearch;
        private System.Windows.Forms.Button BtnSentSTG;
        private System.Windows.Forms.Label lblVersionNumText;
        private System.Windows.Forms.Label lblVerisonNumber;
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace STG
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            this.Visible = false;
            SplashForm.ShowSplashScreen();
            
            SplashForm.CloseForm();
            this.Visible = true;
            this.BringToFront();

        }


        private void generateShippingFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisposeAllInActiveForms();
            FrmShippingExport frmShippingExport = new FrmShippingExport();
            frmShippingExport.MdiParent = this;
            frmShippingExport.Show();
        }

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Really Quit?", "Exit", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {

                Application.Exit();

            }

        }

        private void importShippedFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisposeAllInActiveForms();
            FrmImportShippedData frmImportShippedData = new FrmImportShippedData();
            frmImportShippedData.MdiParent = this;
            frmImportShippedData.Show();
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisposeAllInActiveForms();
        }

        public void DisposeAllInActiveForms()
        {

            foreach (Form frm in this.MdiChildren)
            {
                if (!frm.Focused)
                {
                    frm.Visible = false;
                    frm.Dispose();
                }
            }

        }

        private void importInboundContainerFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisposeAllInActiveForms();
            FrmReceiving frmReceiving = new FrmReceiving();
            frmReceiving.MdiParent = this;
            frmReceiving.Show();
        }

        //private void MainWindow_MdiChildActivate(object sender,
        //                            EventArgs e)
        //{
        //    if (this.ActiveMdiChild == null)
        //        tabForms.Visible = false;
        //    // If no any child form, hide tabControl 
        //    else
        //    {
        //        this.ActiveMdiChild.WindowState =
        //        FormWindowState.Maximized;
        //        // Child form always maximized 

        //        // If child form is new and no has tabPage, 
        //        // create new tabPage 
        //        if (this.ActiveMdiChild.Tag == null)
        //        {
        //            // Add a tabPage to tabControl with child 
        //            // form caption 
        //            TabPage tp = new TabPage(this.ActiveMdiChild
        //                                     .Text);
        //            tp.Tag = this.ActiveMdiChild;
        //            tp.Parent = tabForms;
        //            tabForms.SelectedTab = tp;

        //            this.ActiveMdiChild.Tag = tp;
        //            this.ActiveMdiChild.FormClosed +=
        //                new FormClosedEventHandler(
        //                                ActiveMdiChild_FormClosed);
        //        }

        //        if (!tabForms.Visible) tabForms.Visible = true;
        //    }

        //}
        //private void ActiveMdiChild_FormClosed(object sender,
        //                            FormClosedEventArgs e)
        //{
        //    ((sender as Form).Tag as TabPage).Dispose();
        //}

        //private void tabForms_SelectedIndexChanged(object sender,
        //                                   EventArgs e)
        //{
        //    if ((tabForms.SelectedTab != null) &&
        //        (tabForms.SelectedTab.Tag != null))
        //        (tabForms.SelectedTab.Tag as Form).Select();
        //}


    }
}

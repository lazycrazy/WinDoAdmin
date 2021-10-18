
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinDoControls.Forms;
using WinDoControls.Controls;
using System.Runtime.InteropServices;
using WinDo.Utilities.PublicResource;
using WinDoControls;

namespace WinDo.UI.Utilities.DialogForm
{
    /// <summary>
    /// Class FrmDialog.
    /// Implements the <see cref="WinDoControls.Forms.FrmBase" />
    /// </summary>
    /// <seealso cref="WinDoControls.Forms.FrmBase" />
    public partial class FrmTitleAnd4Words2Btn : FrmBase
    {
        /// <summary>
        /// The BLN enter close
        /// </summary>
        bool blnEnterClose = true;

        public FrmTitleAnd4Words2Btn()
            : this("提示")
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrmShadowDialog" /> class.
        /// </summary>
        /// <param name="strMessage">The string message.</param>
        /// <param name="strTitle">The string title.</param>
        /// <param name="blnShowCancel">if set to <c>true</c> [BLN show cancel].</param>
        /// <param name="blnShowClose">if set to <c>true</c> [BLN show close].</param>
        /// <param name="blnisEnterClose">if set to <c>true</c> [blnis enter close].</param>
        public FrmTitleAnd4Words2Btn(
            string strTitle,
            bool blnShowCancel = true,
            bool blnShowClose = true,
            bool blnisEnterClose = true)
        {
            InitializeComponent();
            base.ShowInTaskbar = false;
            IsShowShadowForm = true;
            InitFormMove(this.lblTitle);
            SetTitleBackColor(WDColors.geekblue6);
            SetTitleColor(Color.White);
            if (!string.IsNullOrWhiteSpace(strTitle))
                lblTitle.Text = strTitle;

            if (blnShowCancel)
            {
                this.btnCancel.Visible = true;
            }
            else
            {
                this.btnCancel.Visible = false;
                this.btnOK.Left = this.btnCancel.Left; //(panel1.Width - this.ucBtnImgOk.Width) / 2;
            }
            //btnCancel.Visible = blnShowCancel;
            //ucSplitLine_V1.Visible = blnShowCancel;
            btnClose.Visible = blnShowClose;
            blnEnterClose = blnisEnterClose;
            //if (blnShowCancel)
            //{
            //    btnOK.BtnForeColor = Color.FromArgb(255, 85, 51);
            //}
            
            //SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_DropSHADOW);
            SizeChanged += new EventHandler(FrmWithTitleAnd2Btn_SizeChanged);

            btnOK.IconName = "I_success";
            btnCancel.IconName = "I_close";

            btnOK.BtnClick += new EventHandler(btnOK_BtnClick);
            btnCancel.BtnClick += new EventHandler(btnCancel_BtnClick);
            ControlHelper.SetCloseBackColor(btnClose);
        }

        protected void FrmWithTitleAnd2Btn_SizeChanged(object sender, EventArgs e)
        {
            var aw = this.btnOK.Width + this.btnCancel.Width + 10;
            this.btnOK.Left = ((panelBottom.Width - aw) / 2);
            this.btnCancel.Left = ((panelBottom.Width - aw) / 2) + (aw - btnCancel.Width);
        }
        protected virtual void SetTitleBackColor(Color color)
        {
            this.panelTitle.BackColor = color;
        }

        protected void SetTitleColor(Color color)
        {
            lblTitle.ForeColor = color;
            //btnClose.BackgroundImage = WinDoImages.GetBtnIconImage("E_icon_close", 38, color);
            //btnClose.BackgroundImageLayout = ImageLayout.Center;
        }

        //const int CS_DropSHADOW = 0x20000; const int GCL_STYLE = (-26); //声明Win32 API 
        //[DllImport("user32.dll", CharSet = CharSet.Auto)] 
        //public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong); 
        //[DllImport("user32.dll", CharSet = CharSet.Auto)] 
        //public static extern int GetClassLong(IntPtr hwnd, int nIndex); 


        protected virtual bool SaveData()
        {
            return false;
        }

        void Save()
        {
            var ok = SaveData();
            if (ok)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }

        }

        /// <summary>
        /// Handles the BtnClick event of the btnOK control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void btnOK_BtnClick(object sender, EventArgs e)
        {
            Save();
        }

        /// <summary>
        /// Handles the BtnClick event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void btnCancel_BtnClick(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        /// <summary>
        /// Handles the MouseDown event of the btnClose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs" /> instance containing the event data.</param>
        private void btnClose_MouseDown(object sender, MouseEventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        /// <summary>
        /// Does the enter.
        /// </summary>
        protected override void DoEnter()
        {
            if (blnEnterClose)
                btnOK_BtnClick(btnOK, EventArgs.Empty);
        }

        /// <summary>
        /// Handles the VisibleChanged event of the FrmDialog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void FrmDialog_VisibleChanged(object sender, EventArgs e)
        {

        }
    }
}

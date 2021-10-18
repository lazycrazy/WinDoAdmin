
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

namespace WinDoControls.Forms
{
    /// <summary>
    /// Class FrmDialog.
    /// Implements the <see cref="WinDoControls.Forms.FrmBase" />
    /// </summary>
    /// <seealso cref="WinDoControls.Forms.FrmBase" />
    public partial class FrmShadowDialog : FrmBase
    {
        /// <summary>
        /// The BLN enter close
        /// </summary>
        bool blnEnterClose = true;
        /// <summary>
        /// Initializes a new instance of the <see cref="FrmShadowDialog" /> class.
        /// </summary>
        /// <param name="strMessage">The string message.</param>
        /// <param name="strTitle">The string title.</param>
        /// <param name="blnShowCancel">if set to <c>true</c> [BLN show cancel].</param>
        /// <param name="blnShowClose">if set to <c>true</c> [BLN show close].</param>
        /// <param name="blnisEnterClose">if set to <c>true</c> [blnis enter close].</param>
        public FrmShadowDialog(
            string strMessage,
            string strTitle,
            bool blnShowCancel = true,
            bool blnShowClose = true,
            bool blnisEnterClose = true)
        {
            InitializeComponent();
            base.ShowInTaskbar = false;
            ControlHelper.SetControlsDouble(this);
            panelTitle.Paint += PanelTitle_Paint;
            IsShowShadowForm = true;
            InitFormMove(this.panelTitle);
            if (!string.IsNullOrWhiteSpace(strTitle))
                LabelTitle = strTitle;
            lblMsg.Text = strMessage;
            if (blnShowCancel)
            {
                this.btnCancel.Visible = true;
            }
            else
            {
                this.btnCancel.Visible = false;
            }
            //btnCancel.Visible = blnShowCancel;
            //ucSplitLine_V1.Visible = blnShowCancel;
            btnClose.Visible = blnShowClose;
            blnEnterClose = blnisEnterClose;
            //if (blnShowCancel)
            //{
            //    btnOK.BtnForeColor = Color.FromArgb(255, 85, 51);
            //}
            Load += new EventHandler(FrmShadowDialog_Load);
            IconName = "I_info";
            IconColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
            //SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_DropSHADOW);
            ControlHelper.SetCloseBackColor(btnClose);
        }

        private void PanelTitle_Paint(object sender, PaintEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_LabelTitle)) return;
            var color = IsLogin ? SystemColors.ControlText : Color.White;
            var rect = panelTitle.ClientRectangle;
            rect.Offset(10, 0);
            using (var brush = new SolidBrush(color))
                e.Graphics.DrawString(_LabelTitle, WDFonts.TextFont, brush, rect, new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center });

        }
        public bool IsLogin = false;

        private string _LabelTitle = "提示";

        public string LabelTitle
        {
            get { return _LabelTitle; }
            set
            {
                _LabelTitle = value;
                panelTitle.Invalidate();
            }
        }
        public string IconName { get; set; }
        public Color IconColor { get; set; }


        void FrmShadowDialog_Load(object sender, EventArgs e)
        {
            lblIcon.Image = WDImages.GetBtnIconImage(IconName, 40, IconColor);
            lblIcon.ImageAlign = ContentAlignment.MiddleRight;
            if (!this.btnCancel.Visible)
                this.btnOK.Left = this.btnCancel.Left; //(panel1.Width - this.ucBtnImgOk.Width) / 2;
        }

        //const int CS_DropSHADOW = 0x20000; const int GCL_STYLE = (-26); //声明Win32 API 
        //[DllImport("user32.dll", CharSet = CharSet.Auto)] 
        //public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong); 
        //[DllImport("user32.dll", CharSet = CharSet.Auto)] 
        //public static extern int GetClassLong(IntPtr hwnd, int nIndex); 



        #region 显示一个模式信息框

        public static DialogResult ShowDialog(
            IWin32Window owner,
            string strMessage,
            string strTitle = "提示",
            bool blnShowCancel = true,
            bool isShowMaskDialog = false,
            bool blnShowClose = true,
            bool blnIsEnterClose = true,
            Size? deviationSize = null)
        {
            DialogResult result = DialogResult.Cancel;
            if (owner == null || (owner is System.Windows.Forms.Control && (owner as System.Windows.Forms.Control).IsDisposed))
            {
                var frm = new FrmShadowDialog(strMessage, strTitle, blnShowCancel, blnShowClose, blnIsEnterClose)
                {
                    StartPosition = FormStartPosition.CenterScreen,
                    IsShowMaskDialog = isShowMaskDialog,
                    //TopMost = true
                };
                if (deviationSize != null)
                {
                    frm.Width += deviationSize.Value.Width;
                    frm.Height += deviationSize.Value.Height;
                }
                result = frm.ShowDialog();
            }
            else
            {
                if (owner is System.Windows.Forms.Control)
                {
                    owner = (owner as System.Windows.Forms.Control).FindForm();
                }
                var frm = new FrmShadowDialog(strMessage, strTitle, blnShowCancel, blnShowClose, blnIsEnterClose)
                {
                    StartPosition = (owner != null) ? FormStartPosition.CenterParent : FormStartPosition.CenterScreen,
                    IsShowMaskDialog = isShowMaskDialog,
                    //TopMost = true
                };
                if (deviationSize != null)
                {
                    frm.Width += deviationSize.Value.Width;
                    frm.Height += deviationSize.Value.Height;
                }
                result = frm.ShowDialog(owner);
            }
            return result;
        }

        public static DialogResult ShowErrDialog(
            IWin32Window owner,
            string strMessage,
            string strTitle = "错误",
            bool blnShowCancel = true,
            bool isShowMaskDialog = false,
            bool blnShowClose = true,
            bool blnIsEnterClose = true,
            Size? deviationSize = null, string iconName = "I_close", bool IsLogin = false)
        {

            DialogResult result = DialogResult.Cancel;
            if (owner == null || (owner is System.Windows.Forms.Control && (owner as System.Windows.Forms.Control).IsDisposed))
            {
                var frm = new FrmShadowDialog(strMessage, strTitle, blnShowCancel, blnShowClose, blnIsEnterClose)
                {
                    StartPosition = FormStartPosition.CenterScreen,
                    IsShowMaskDialog = isShowMaskDialog,
                    //TopMost = true
                };
                if (deviationSize != null)
                {
                    frm.Width += deviationSize.Value.Width;
                    frm.Height += deviationSize.Value.Height;
                }
                frm.IconName = iconName;
                frm.IsLogin = IsLogin;
                frm.IconColor = WDColors.ErrorRedColor;

                frm.BackColor = WDColors.WhiteBackColor;
                if (IsLogin)
                {
                    frm.panelTitle.BackColor = Color.Transparent;
                }
                result = frm.ShowDialog();
            }
            else
            {
                if (owner is System.Windows.Forms.Control)
                {
                    owner = (owner as System.Windows.Forms.Control).FindForm();
                }
                var frm = new FrmShadowDialog(strMessage, strTitle, blnShowCancel, blnShowClose, blnIsEnterClose)
                {
                    StartPosition = (owner != null) ? FormStartPosition.CenterParent : FormStartPosition.CenterScreen,
                    IsShowMaskDialog = isShowMaskDialog,
                    //TopMost = true
                };
                if (deviationSize != null)
                {
                    frm.Width += deviationSize.Value.Width;
                    frm.Height += deviationSize.Value.Height;
                }
                frm.IconName = iconName;
                frm.IsLogin = IsLogin;
                frm.IconColor = WDColors.ErrorRedColor;
                if (IsLogin)
                {
                    frm.panelTitle.BackColor = Color.Transparent;
                }
                frm.BackColor = WDColors.WhiteBackColor;
                result = frm.ShowDialog(owner);
            }
            return result;
        }


        //警告提示框
        public static DialogResult ShowAskDialog(
           IWin32Window owner,
           string strMessage,
           string strTitle = "提示",
           bool blnShowCancel = true,
           bool isShowMaskDialog = false,
           bool blnShowClose = true,
           bool blnIsEnterClose = true,
           Size? deviationSize = null, string iconName = "I_question", bool IsLogin = false, string OKText = "确定")
        {
            DialogResult result = DialogResult.Cancel;

            if (owner == null || (owner is System.Windows.Forms.Control && (owner as System.Windows.Forms.Control).IsDisposed))
            {
                var frm = new FrmShadowDialog(strMessage, strTitle, blnShowCancel, blnShowClose, blnIsEnterClose)
                {
                    StartPosition = FormStartPosition.CenterScreen,
                    IsShowMaskDialog = isShowMaskDialog,
                    //TopMost = true
                };
                if (deviationSize != null)
                {
                    frm.Width += deviationSize.Value.Width;
                    frm.Height += deviationSize.Value.Height;
                }
                frm.btnOK.BtnText = OKText;
                frm.IconName = iconName;
                frm.IsLogin = IsLogin;
                //frm.lblTitle.ForeColor = SystemColors.ControlText;
                frm.IconColor = WDColors.AskYellowColor;
                //frm.panelTitle.BackColor = Color.Transparent;
                frm.BackColor = WDColors.WhiteBackColor;
                result = frm.ShowDialog();
            }
            else
            {
                if (owner is System.Windows.Forms.Control)
                {
                    owner = (owner as System.Windows.Forms.Control).FindForm();
                }
                var frm = new FrmShadowDialog(strMessage, strTitle, blnShowCancel, blnShowClose, blnIsEnterClose)
                {
                    StartPosition = (owner != null) ? FormStartPosition.CenterParent : FormStartPosition.CenterScreen,
                    IsShowMaskDialog = isShowMaskDialog,
                    //TopMost = true
                };
                frm.IsLogin = IsLogin;
                if (deviationSize != null)
                {
                    frm.Width += deviationSize.Value.Width;
                    frm.Height += deviationSize.Value.Height;
                }
                frm.btnOK.BtnText = OKText;
                frm.IconName = iconName;
                //frm.lblTitle.ForeColor = SystemColors.ControlText;
                frm.IconColor = WDColors.AskYellowColor;
                //frm.panelTitle.BackColor = Color.Transparent;
                frm.BackColor = WDColors.WhiteBackColor;
                result = frm.ShowDialog(owner);
            }
            return result;
        }
        //警告提示框
        public static DialogResult ShowWarningDialog(
           IWin32Window owner,
           string strMessage,
           string strTitle = "警告",
           bool blnShowCancel = true,
           bool isShowMaskDialog = false,
           bool blnShowClose = true,
           bool blnIsEnterClose = true,
           Size? deviationSize = null, string iconName = "I_warning", bool IsLogin = false)
        {
            DialogResult result = DialogResult.Cancel;
            if (owner == null || (owner is System.Windows.Forms.Control && (owner as System.Windows.Forms.Control).IsDisposed))
            {
                var frm = new FrmShadowDialog(strMessage, strTitle, blnShowCancel, blnShowClose, blnIsEnterClose)
                {
                    StartPosition = FormStartPosition.CenterScreen,
                    IsShowMaskDialog = isShowMaskDialog,
                    //TopMost = true
                };
                if (deviationSize != null)
                {
                    frm.Width += deviationSize.Value.Width;
                    frm.Height += deviationSize.Value.Height;
                }
                frm.IconName = iconName;
                //frm.lblTitle.ForeColor = SystemColors.ControlText;
                frm.IconColor = WDColors.WarningOrangeColor;
                //frm.panelTitle.BackColor = Color.Transparent;
                frm.BackColor = WDColors.WhiteBackColor;
                result = frm.ShowDialog();
            }
            else
            {
                if (owner is System.Windows.Forms.Control)
                {
                    owner = (owner as System.Windows.Forms.Control).FindForm();
                }
                var frm = new FrmShadowDialog(strMessage, strTitle, blnShowCancel, blnShowClose, blnIsEnterClose)
                {
                    StartPosition = (owner != null) ? FormStartPosition.CenterParent : FormStartPosition.CenterScreen,
                    IsShowMaskDialog = isShowMaskDialog,
                    //TopMost = true
                };
                if (deviationSize != null)
                {
                    frm.Width += deviationSize.Value.Width;
                    frm.Height += deviationSize.Value.Height;
                }
                frm.IconName = iconName;
                //frm.lblTitle.ForeColor = SystemColors.ControlText;
                frm.IconColor = WDColors.WarningOrangeColor;
                //frm.panelTitle.BackColor = Color.Transparent;
                frm.BackColor = WDColors.WhiteBackColor;
                result = frm.ShowDialog(owner);
            }
            return result;
        }


        #endregion

        /// <summary>
        /// Handles the BtnClick event of the btnOK control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void btnOK_BtnClick(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
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
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
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

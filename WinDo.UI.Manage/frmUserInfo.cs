using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinDoControls.Forms;
using WinDo.UI.Utilities.DialogForm;
using WinDoControls.Controls;
using WinDo.Utilities.PublicResource;
using WinDoControls;

using WinDo.UI.Utilities;
using WinDo.Utilities;

namespace WinDo.UI.Manage
{
    public partial class frmUserInfo : FrmTitleAnd2Btn
    {
        public frmUserInfo()
            : base("基本资料")
        {
            InitializeComponent();
            StartPosition = FormStartPosition.Manual;
            SetStyle(ControlStyles.DoubleBuffer, true);
            Load += new EventHandler(frmChangePasswrod_Load);
            verification.AnchorLocation = AnchorTipsLocation.BOTTOM;
            verification.TextAlignment = StringAlignment.Center;
            verification.Verificationed += new VerificationComponent.VerificationedHandle(verification_Verificationed);
            verification.SetVerificationModel(this.txtPhone.valueControl, WinDoControls.Controls.VerificationModel.Phone);
            verification.SetVerificationRequired(this.txtPhone.valueControl, true, "必填项不能为空");
            //verification.SetVerificationModel(ucTextBoxClear1, WD_Controls.Controls.VerificationModel.Phone);
            //verification.SetVerificationRequired(ucTextBoxClear1, true, "请输入手机号");

            //ControlHelper.FreezeControl(this, true);

            txtPhone.valueControl.RectColor = WDColors.ErrorRedColor;
            txtPhone.valueControl.TextChanged += new EventHandler(valueControl_TextChanged);
            txtPhone.valueControl.txtInput.MaxLength = 20;
            //txtPhone.valueControl.IsShowRect = false;
            //lblName.valueControl.Font = lblName.label.Font = YkdTextFonts.BoldTextFont;
            //lblUserName.valueControl.Font = lblUserName.label.Font = YkdTextFonts.BoldTextFont;
            //lblUserTitle.valueControl.Font = lblUserTitle.label.Font = YkdTextFonts.BoldTextFont;
            //txtPhone.valueControl.Font = txtPhone.label.Font = YkdTextFonts.BoldTextFont;
            this.BackColor = WDColors.SelectedBackColor;
            base.SetTitleBackColor(WDColors.SelectedBackColor);
            base.SetTitleColor(WDColors.geekblue6);
            base.btnOK.IconName = "I_save";
            //ControlHelper.FreezeControl(this, false);
            //txtPhone.valueControl.txtInput.KeyUp += new KeyEventHandler(txtInput_KeyUp);
            this.Location = new Point(FormHelper.MainForm.Right - this.Width - 2, 74);
        }

        void txtInput_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Tab)
            //    base.bt.Focus();
        }

        void valueControl_TextChanged(object sender, EventArgs e)
        {
            txtPhone.valueControl.IsErrorColor = string.IsNullOrWhiteSpace(txtPhone.valueControl.InputText);
        }


        VerificationComponent verification = new VerificationComponent();

        //List<Control> lstErrorControl = new List<Control>();

        //private void graphicalOverlay1_Paint(object sender, PaintEventArgs e)
        //{
        //    if (lstErrorControl.Count > 0)
        //    {
        //        e.Graphics.SetGDIHigh();
        //        foreach (Control control in lstErrorControl)
        //        {
        //            {
        //                var p = control.Location;
        //                p.Offset(control.Parent.Location);
        //                var path = ControlHelper.CreateRoundedRectanglePath(new Rectangle(p.X - 1, p.Y - 1, control.Width + 2, control.Height + 2), 4);
        //                e.Graphics.DrawPath(new Pen(new SolidBrush(Color.FromArgb(100, 255, 0, 0)), 2), path);
        //            }
        //        }
        //    }
        //}
        void Clear()
        {
            lblName.valueControl.Text = "";
            lblUserName.valueControl.Text = "";
            lblUserTitle.valueControl.Text = "";
            txtPhone.valueControl.InputText = "";
        }

        void frmChangePasswrod_Load(object sender, EventArgs e)
        {
            Clear();
            lblName.valueControl.Text = WinDo.Utilities.PublicRes.CurUser.RealName;
            lblUserName.valueControl.Text = WinDo.Utilities.PublicRes.CurUser.LoginName;
            var dic = PublicRes.GetDicByTypeAndVal("职务", WinDo.Utilities.PublicRes.CurUser.Title_ID.AsString());
            lblUserTitle.valueControl.Text = dic == null ? PublicRes.CurUser.Title_ID.AsString() : dic.SysDes;
            if (PublicRes.IsAdministrator())
                lblUserTitle.valueControl.Text = "管理员";
            txtPhone.valueControl.InputText = WinDo.Utilities.PublicRes.CurUser.Phone;
            txtPhone.Focus();
            txtPhone.valueControl.txtInput.Select(txtPhone.valueControl.InputText.Length, 0);
        }

        void verification_Verificationed(VerificationEventArgs e)
        {
            if (e.VerificationControl == txtPhone.valueControl)
            {
                var ctrl = e.VerificationControl as WDTextBoxClear;
                ctrl.IsErrorColor = !e.IsVerifySuccess;
            }
            //if (!e.IsVerifySuccess)
            //{
            //    lstErrorControl.Add(e.VerificationControl);
            //}
        }




        protected override bool SaveData()
        {
            bool success = false;
            //lstErrorControl.Clear();
            if (!verification.Verification()) return false;

            try
            {
            }
            catch (Exception ex)
            {
                FrmTips.ShowTipsError(FormHelper.MainForm, "保存失败");
                //FrmTips.ShowTipsInfo(this, "Info提示信息");
                //FrmTips.ShowTipsSuccess(this, "Success提示信息");
                //FrmTips.ShowTipsWarning(this, "Warning提示信息");
                //FrmTips.ShowTips(this, "自定义提示信息", 2000, true, ContentAlignment.MiddleCenter, null, TipsSizeMode.Medium, new Size(300, 50), TipsState.Success);
                return false;
            }
            if (success)
            {
                FrmTips.ShowTipsSuccess(FormHelper.MainForm, "已保存");
                WinDo.Utilities.PublicRes.CurUser.Phone = txtPhone.valueControl.InputText;
            }
            //更新到数据库和缓存
            return success;
        }

    }
}

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

using WinDo.Utilities;
using WinDo.UI.Utilities;
using WinDoControls;

namespace WinDo.UI.Manage
{
    public partial class frmChangePassword : FrmTitleAnd2Btn
    {
        public frmChangePassword()
            : base("修改密码")
        {
            InitializeComponent();
            ControlHelper.SetControlsDouble(this);
            Load += new EventHandler(frmChangePasswrod_Load);
            verification.Verificationed += new VerificationComponent.VerificationedHandle(verification_Verificationed);
            verification.AnchorLocation = AnchorTipsLocation.BOTTOM;
            verification.TextAlignment = StringAlignment.Center;
            verification.SetVerificationModel(this.txtOldPwd.valueControl, WinDoControls.Controls.VerificationModel.Custom);
            verification.SetVerificationCustomRegex(this.txtOldPwd.valueControl, @"^[^\u4E00-\u9FFF.]*$");
            verification.SetVerificationErrorMsg(this.txtOldPwd.valueControl, "请输入字母数字或其它字符");
            verification.SetVerificationModel(this.txtNewPwd.valueControl, WinDoControls.Controls.VerificationModel.Custom);
            verification.SetVerificationCustomRegex(this.txtNewPwd.valueControl, @"^[^\u4E00-\u9FFF.]*$");
            verification.SetVerificationErrorMsg(this.txtNewPwd.valueControl, "请输入字母数字或其它字符");
            verification.SetVerificationModel(this.txtConfirmPwd.valueControl, WinDoControls.Controls.VerificationModel.Custom);
            verification.SetVerificationCustomRegex(this.txtConfirmPwd.valueControl, @"^[^\u4E00-\u9FFF.]*$");
            verification.SetVerificationErrorMsg(this.txtConfirmPwd.valueControl, "请输入字母数字或其它字符");

            verification.SetVerificationRequired(this.txtOldPwd.valueControl, true, "必填项不能为空");
            verification.SetVerificationRequired(this.txtNewPwd.valueControl, true, "必填项不能为空");
            verification.SetVerificationRequired(this.txtConfirmPwd.valueControl, true, "必填项不能为空");
            base.btnOK.BtnText = "确定";
            this.BackColor = WDColors.SelectedBackColor;
            base.SetTitleBackColor(WDColors.SelectedBackColor);
            base.SetTitleColor(WDColors.geekblue6);

            //Clear();
            txtOldPwd.valueControl.txtInput.PasswordChar = '*';
            txtNewPwd.valueControl.txtInput.PasswordChar = '*';
            txtConfirmPwd.valueControl.txtInput.PasswordChar = '*';
            txtOldPwd.valueControl.txtInput.PromptText = "请输入密码";
            txtNewPwd.valueControl.txtInput.PromptText = "请输入新密码";
            txtConfirmPwd.valueControl.txtInput.PromptText = "再输入一次";
            txtOldPwd.valueControl.txtInput.MaxLength = 20;
            txtNewPwd.valueControl.txtInput.MaxLength = 20;
            txtConfirmPwd.valueControl.txtInput.MaxLength = 20;
            txtOldPwd.valueControl.TextChanged += new EventHandler(valueControl_TextChanged);
            txtNewPwd.valueControl.TextChanged += new EventHandler(valueControl_TextChanged1);
            txtConfirmPwd.valueControl.TextChanged += new EventHandler(valueControl_TextChanged2);
            //txtOldPwd.valueControl.txtInput.ShortcutsEnabled = false;
            //txtNewPwd.valueControl.txtInput.ShortcutsEnabled = false;
            //txtConfirmPwd.valueControl.txtInput.ShortcutsEnabled = false;
            StartPosition = FormStartPosition.Manual;
            this.Location = new Point(FormHelper.MainForm.Right - this.Width - 2, 74);
        }

        private void TxtInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = RegexValidatorHelper.IsMatch(e.KeyChar.ToString(), Pattern.CHINESE);
            //e.Handled = Char.IsPunctuation(e.KeyChar) ||
            //         //Char.IsSeparator(e.KeyChar) ||
            //         Char.IsSymbol(e.KeyChar);
        }

        void valueControl_TextChanged(object sender, EventArgs e)
        {
            txtOldPwd.valueControl.IsErrorColor = string.IsNullOrWhiteSpace(txtOldPwd.valueControl.InputText);
        }
        void valueControl_TextChanged1(object sender, EventArgs e)
        {
            txtNewPwd.valueControl.IsErrorColor = string.IsNullOrWhiteSpace(txtNewPwd.valueControl.InputText);
        }
        void valueControl_TextChanged2(object sender, EventArgs e)
        {
            txtConfirmPwd.valueControl.IsErrorColor = string.IsNullOrWhiteSpace(txtConfirmPwd.valueControl.InputText);
        }

        VerificationComponent verification = new VerificationComponent();

        void Clear()
        {
            txtOldPwd.valueControl.InputText = "";
            txtNewPwd.valueControl.InputText = "";
            txtConfirmPwd.valueControl.InputText = "";
        }

        void frmChangePasswrod_Load(object sender, EventArgs e)
        {
        }

        void verification_Verificationed(VerificationEventArgs e)
        {
            var ctrl = e.VerificationControl as WDTextBoxClear;
            if (ctrl != null)
                ctrl.IsErrorColor = !e.IsVerifySuccess;
        }



        protected override bool SaveData()
        {
            if (!verification.Verification()) return false;

            FrmTips.ShowTipsSuccess(FormHelper.MainForm, "已保存");

            //更新到数据库和缓存
            return true;
        }

    }
}

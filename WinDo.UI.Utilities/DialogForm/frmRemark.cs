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

namespace WinDo.Control.DialogForm
{
    public partial class frmRemark : FrmTitleAnd2Btn
    {
        /// <summary>
        /// 文本框内容
        /// </summary>
        public string InputText
        {
            get { return txtRemark.InputText; }
            set { txtRemark.InputText = value; }
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            set { lblTitle.Text = value; }
        }

        public void SetConfirmBtn()
        {
            btnOK.BtnText = "确定";
            btnOK.IconName = "I_success";
        }

        public void SetReturn()
        {
            
        }

        public frmRemark(bool IsRequired = false)
            : base("备注")
        {
            InitializeComponent();
            Load += new EventHandler(frmChangePasswrod_Load);
            verification.Verificationed += new VerificationComponent.VerificationedHandle(verification_Verificationed);
            if (IsRequired)
            {
                verification.SetVerificationRequired(this.txtRemark, true, "请输入备注");
                //txtRemark.TextChanged += new EventHandler(valueControl_TextChanged);
            }
            txtRemark.txtInput.ScrollBars = ScrollBars.Vertical;
        }

        void valueControl_TextChanged(object sender, EventArgs e)
        {
            txtRemark.IsErrorColor = string.IsNullOrWhiteSpace(txtRemark.InputText);
        }

        VerificationComponent verification = new VerificationComponent();


        void frmChangePasswrod_Load(object sender, EventArgs e)
        {
        }

        void verification_Verificationed(VerificationEventArgs e)
        {
            var ctrl = e.VerificationControl as WDTextBoxClear;
            if (ctrl != null)
                ctrl.IsErrorColor = !e.IsVerifySuccess;
        }

        public int Sch_Id = 0;
        public int Pat_ID = 0;

        protected override bool SaveData()
        {
            bool success = false;
            txtRemark.InputText = txtRemark.InputText.Trim();
            if (!verification.Verification())
                return false;
            var note = txtRemark.InputText.Trim();
            //if (note.Length == 0)
            //{
            //    if (FrmShadowDialog.ShowAskDialog(FormHelper.MainForm, "录入空的备注？") == System.Windows.Forms.DialogResult.Cancel)
            //        return false;
            //}
            //检查消息最大长度
            var curLength = StringHelper.NumChar(note);
            var maxCharLenght = 256;
            if (curLength > maxCharLenght)
            {
                WinDoControls.Forms.FrmAnchorTips.ShowTips(txtRemark, string.Format("最多可输入{0}个汉字", maxCharLenght / 2), AnchorTipsLocation.RIGHT, WDColors.ErrorTipRedColor, autoCloseTime: 3000, foreColor: WDColors.WhiteColor, blnTopMost: false, alignment: StringAlignment.Center);
                txtRemark.IsErrorColor = true;
                return false;
            }
            InputText = txtRemark.InputText;
            //更新到数据库和缓存
            return true;
        }

        private void btnCancel_BtnClick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

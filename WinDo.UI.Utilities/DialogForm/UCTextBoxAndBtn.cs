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
using WinDo.Utilities.PublicResource;

namespace WinDo.UI.Utilities.DialogForm
{
    public partial class UCTextBoxAndBtn : WDCtrlBase
    {

        public UCTextBoxAndBtn()
        {
            InitializeComponent();
            //BorderStyleType = ButtonBorderStyle.Solid;
            //BorderStyleColor = WinDo.Utilities.PublicResource.YkdBasisColors.MainColor;
            //BorderStyleSize = 1;
            this.IsShowRect = true;
            RectColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
            SizeChanged += new EventHandler(FrmQueryWithOk_SizeChanged);
            btnOK.BtnClick += new EventHandler(btnOk_BtnClick);
            btnOK.IconName = "I_search";
            txtInput.txtInput.MaxLength = 30; 
            //WindowState = FormWindowState.Normal;
            Load += new EventHandler(FrmTextBoxWithOk_Load);
            //txtInput.TextChanged += new EventHandler(valueControl_TextChanged);
            txtInput.txtInput.KeyUp += new KeyEventHandler(valueControl_KeyUp);
        }

        void valueControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOk_BtnClick(btnOK, EventArgs.Empty);
        }

        void valueControl_TextChanged(object sender, EventArgs e)
        {
            txtInput.IsErrorColor = string.IsNullOrWhiteSpace(txtInput.InputText);
        }

        public WDTextBoxClear InputTextBox { get { return txtInput; } }
        public string TextValue { get { return txtInput.InputText; } }
        private bool _mustInput = true;

        public bool MustInput
        {
            get { return _mustInput; }
            set { _mustInput = value; }
        }
        void FrmTextBoxWithOk_Load(object sender, EventArgs e)
        {

            if (_mustInput)
                verification.SetVerificationRequired(this.txtInput, true, "必填项不能为空");
            verification.Verificationed += new VerificationComponent.VerificationedHandle(verification_Verificationed);


        }
        void verification_Verificationed(VerificationEventArgs e)
        {
            var ctrl = e.VerificationControl as WDTextBoxClear;
            if (ctrl != null)
            {
                ctrl.IsErrorColor = !e.IsVerifySuccess;
                e.IsProcessed = true;
            }
        }


        public WDBtnImg2WordsYS BtnOK { get { return btnOK; } }

        public VerificationComponent verification = new VerificationComponent();

        //protected override void DoEnter()
        //{
        //    btnOk_BtnClick(btnOK, EventArgs.Empty);
        //}
        void btnOk_BtnClick(object sender, EventArgs e)
        {
            if (!verification.Verification()) return;
            //this.DialogResult = DialogResult.OK;
            if (InputOKClick != null)
                InputOKClick(txtInput.InputText, null);
        }

        [Description("点击确定按钮事件"), Category("自定义")]
        public event EventHandler InputOKClick;

        void FrmQueryWithOk_SizeChanged(object sender, EventArgs e)
        {
            //btnOK.Left = (this.Width - btnOK.Width) / 2;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}

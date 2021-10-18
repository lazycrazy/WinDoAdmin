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
using WinDoControls;

namespace WinDo.UI.Utilities.DialogForm
{
    public partial class FrmTextBoxWithOk : FrmBase
    {
        bool EnterAsOk = true;

        public FrmTextBoxWithOk()
        {
            InitializeComponent();
            base.ShowInTaskbar = false;
            InitFormMove(this.lblTitle);
            IsShowShadowForm = true;
            BorderStyleType = ButtonBorderStyle.Solid;
            BorderStyleColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
            BorderStyleSize = 1;
            SizeChanged += new EventHandler(FrmQueryWithOk_SizeChanged);
            //btnOK.BtnClick += new EventHandler(btnOk_BtnClick);
            txtInput.valueControl.txtInput.MaxLength = 30;
            WindowState = FormWindowState.Normal;
            Load += new EventHandler(FrmTextBoxWithOk_Load);
            txtInput.valueControl.TextChanged += new EventHandler(valueControl_TextChanged);
            //txtInput.valueControl.txtInput.KeyUp += new KeyEventHandler(valueControl_KeyUp);
            ControlHelper.SetCloseBackColor(btnClose);
        }

        public FrmTextBoxWithOk(bool _EnterAsOk)
            : this()
        {
            EnterAsOk = _EnterAsOk;
        }


        //void valueControl_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter && EnterAsOk)
        //    {
        //        if (verification.Verification())
        //            btnOK.OnBtnClick(btnOK, EventArgs.Empty);
        //    }
        //}

        void valueControl_TextChanged(object sender, EventArgs e)
        {
            txtInput.valueControl.IsErrorColor = string.IsNullOrWhiteSpace(txtInput.valueControl.InputText);
        }

        void FrmTextBoxWithOk_Load(object sender, EventArgs e)
        {

            if (_mustInput)
                verification.SetVerificationRequired(this.txtInput.valueControl, true, "必填项不能为空");
            verification.Verificationed += new VerificationComponent.VerificationedHandle(verification_Verificationed);

        }
        void verification_Verificationed(VerificationEventArgs e)
        {
            var ctrl = e.VerificationControl as WDTextBoxClear;
            if (ctrl != null)
                ctrl.IsErrorColor = !e.IsVerifySuccess;
        }

        private bool _mustInput = true;
        public bool MustInput
        {
            get { return _mustInput; }
            set
            {
                _mustInput = value;
                txtInput.IsRequired = _mustInput;
            }
        }
        public WDBtnImg2WordsYS BtnOK { get { return btnOK; } }
        public WDLabelTextBox InputTextBox { get { return txtInput; } }
        public VerificationComponent verification = new VerificationComponent();
        public string Title
        {
            get { return lblTitle.Text; }
            set { lblTitle.Text = value; }
        }

        protected override void DoEnter()
        {
            if (EnterAsOk)
            {
                if (verification.Verification())
                    btnOK.OnBtnClick(btnOK, EventArgs.Empty);
            }
        }



        //void btnOk_BtnClick(object sender, EventArgs e)
        //{
        //    if (!verification.Verification()) return;
        //    if (CheckInput != null)
        //        CheckInput();
        //    this.DialogResult = DialogResult.OK;
        //}

        void FrmQueryWithOk_SizeChanged(object sender, EventArgs e)
        {
            btnOK.Left = (this.Width - btnOK.Width) / 2;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}

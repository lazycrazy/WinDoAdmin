using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinDoControls.Controls;
using WinDo.Utilities.PublicResource;

namespace WinDoControls.Controls
{
    public partial class WDSearchBox : WDCtrlBase
    {
        public WDSearchBox()
        {
            InitializeComponent();
            ucBtnImg0WordsYS1.BtnClick += new EventHandler(OnBtnClick);
            ucTextBoxClear1.txtInput.TextChanged += new EventHandler(txtInput_TextChanged);
            ucTextBoxClear1.txtInput.KeyPress += new KeyPressEventHandler(txtInput_KeyPress);
        }
        public event EventHandler TextChanged;
        void txtInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                OnBtnClick(this, null);
            }
        }

        public void SetSearchButtonCursor(Cursor cursor)
        {
            ucBtnImg0WordsYS1.Cursor = cursor;
        }

        void txtInput_TextChanged(object sender, EventArgs e)
        {
            //文本为空时，清空选择值对象
            if (ucTextBoxClear1.txtInput.Text.Trim().Length == 0)
                SelectedObj = null;
            if (TextChanged != null)
            {
                TextChanged(this, e);
            }
            var foreColor = ucTextBoxClear1.txtInput.Enabled ? Color.Black : (txtInput.Text.Contains("请选择") ? WDColors.GrayTextColor : Color.Black);
            ucTextBoxClear1.txtInput.ForeColor = foreColor;
        }
        [Description("按钮点击事件"), Category("自定义")]
        public event EventHandler BtnClick;

        public void OnBtnClick(object sender, EventArgs e)
        {
            if (BtnClick != null)
                BtnClick(sender, e);
        }
        private Color defaultRectColor = WDColors.GrayRectColor;

        private bool isErrorColor = false;
        [Description("是否为错误状态"), Category("自定义")]
        public bool IsErrorColor
        {
            get
            {
                return isErrorColor;
            }
            set
            {
                isErrorColor = value;
                this.RectColor = value ? WDColors.ErrorRedColor : defaultRectColor;
            }
        }

        public object SelectedObj
        {
            get;
            set;
        }

        public TextBoxEx txtInput
        {
            get { return ucTextBoxClear1.txtInput; }
        }

        public string InputText
        {
            get { return this.ucTextBoxClear1.InputText; }
            set { this.ucTextBoxClear1.InputText = value; }
        }

        public string PromteText
        {
            get { return ucTextBoxClear1.PromptText; }
            set
            {

                ucTextBoxClear1.PromptText = value;
            }
        }

        public bool EnableInput
        {
            get { return ucTextBoxClear1.Enabled; }
            set
            {
                ucTextBoxClear1.txtInput.BackColor = value ? Color.White : WDColors.GrayTextBackColor;
                ucTextBoxClear1.txtInput.ForeColor = Color.Black;//value ? Color.Black : YkdBasisColors.GrayTextColor;
                ucTextBoxClear1.txtInput.Enabled = value;
                this.FillColor = ucTextBoxClear1.FillColor = ucTextBoxClear1.txtInput.BackColor;
                ucTextBoxClear1.txtInput.ReadOnly = !value;
            }
        }


    }
}

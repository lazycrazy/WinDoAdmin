
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinDoControls.Controls
{
    /// <summary>
    /// Class TextBoxEx.
    /// Implements the <see cref="System.Windows.Forms.TextBox" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.TextBox" />
    public partial class TextBoxEx : TextBox
    {

        private Color backColor = Color.White;
        /// <summary>
        /// Enable的背景顏色
        /// </summary>
        public new Color BackColor
        {
            get { return backColor; }
            set
            {
                backColor = value;
                base.BackColor = value;
            }
        }

        private Color foreColor = Color.Black;
        /// <summary>
        /// Enable的字體顏色
        /// </summary>
        public new Color ForeColor
        {
            get { return foreColor; }
            set
            {
                foreColor = value;
                base.ForeColor = value;
            }
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            //设置Enabled为False时
            if (this.Enabled)
            {
                this.SetStyle(ControlStyles.UserPaint, false);
            }
            else
                this.SetStyle(ControlStyles.UserPaint, true);
            this.Invalidate();
        }

        //描绘TextBox
        protected override void OnPaint(PaintEventArgs e)
        {
            if (Enabled)
            {
                base.OnPaint(e);
                return;
            }
            if (e == null) return;
            using (System.Drawing.Brush b = new System.Drawing.SolidBrush(this.ForeColor))
            {
                //StringFormat sf = new StringFormat();
                //sf.LineAlignment = StringAlignment.Center;
                //sf.Alignment = StringAlignment.Near;
                //绘制背景
                //e.Graphics.Clear(BackColor);
                //描绘字符串
                //e.Graphics.DrawString(this.Text, this.Font, b, -1, 1);
                e.Graphics.DrawString(this.Text, this.Font, b, this.ClientRectangle);
            }
        }


        /// <summary>
        /// The BLN focus
        /// </summary>
        private bool blnFocus = false;

        /// <summary>
        /// The prompt text
        /// </summary>
        private string _promptText = string.Empty;

        /// <summary>
        /// The prompt font
        /// </summary>
        private Font _promptFont = new Font("微软雅黑", 13f, FontStyle.Regular, GraphicsUnit.Pixel);

        /// <summary>
        /// The prompt color
        /// </summary>
        private Color _promptColor = Color.Gray;

        /// <summary>
        /// My rectangle
        /// </summary>
        private Rectangle _myRectangle = Rectangle.FromLTRB(1, 3, 1000, 3);

        /// <summary>
        /// The input type
        /// </summary>
        private TextInputType _inputType = TextInputType.NotControl;

        /// <summary>
        /// The regex pattern
        /// </summary>
        private string _regexPattern = "";

        /// <summary>
        /// The m string old value
        /// </summary>
        private string m_strOldValue = string.Empty;

        /// <summary>
        /// The maximum value
        /// </summary>
        private decimal _maxValue = 1000000m;

        /// <summary>
        /// The minimum value
        /// </summary>
        private decimal _minValue = -1000000m;

        /// <summary>
        /// The decimal length
        /// </summary>
        private int _decLength = 2;

        /// <summary>
        /// 水印文字
        /// </summary>
        /// <value>The prompt text.</value>
        [Description("水印文字"), Category("自定义")]
        public string PromptText
        {
            get
            {
                return this._promptText;
            }
            set
            {
                this._promptText = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the prompt font.
        /// </summary>
        /// <value>The prompt font.</value>
        [Description("水印字体"), Category("自定义")]
        public Font PromptFont
        {
            get
            {
                return this._promptFont;
            }
            set
            {
                //this._promptFont = value;
            }
        }

        /// <summary>
        /// Gets or sets the color of the prompt.
        /// </summary>
        /// <value>The color of the prompt.</value>
        [Description("水印颜色"), Category("自定义")]
        public Color PromptColor
        {
            get
            {
                return this._promptColor;
            }
            set
            {
                this._promptColor = value;
            }
        }

        /// <summary>
        /// Gets or sets my rectangle.
        /// </summary>
        /// <value>My rectangle.</value>
        public Rectangle MyRectangle
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the old text.
        /// </summary>
        /// <value>The old text.</value>
        public string OldText
        {
            get;
            set;
        }

        public string TempText
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of the input.
        /// </summary>
        /// <value>The type of the input.</value>
        [Description("获取或设置一个值，该值指示文本框中的文本输入类型。")]
        public TextInputType InputType
        {
            get
            {
                return this._inputType;
            }
            set
            {
                this._inputType = value;
                if (value != TextInputType.NotControl)
                {
                    TextChanged -= new EventHandler(this.TextBoxEx_TextChanged);
                    TextChanged += new EventHandler(this.TextBoxEx_TextChanged);
                }
                else
                {
                    TextChanged -= new EventHandler(this.TextBoxEx_TextChanged);
                }
            }
        }
        /// <summary>
        /// 获取或设置一个值，该值指示当输入类型InputType=Regex时，使用的正则表达式。
        /// </summary>
        /// <value>The regex pattern.</value>
        [Description("获取或设置一个值，该值指示当输入类型InputType=Regex时，使用的正则表达式。")]
        public string RegexPattern
        {
            get
            {
                return this._regexPattern;
            }
            set
            {
                this._regexPattern = value;
            }
        }
        /// <summary>
        /// 当InputType为数字类型时，能输入的最大值
        /// </summary>
        /// <value>The maximum value.</value>
        [Description("当InputType为数字类型时，能输入的最大值。")]
        public decimal MaxValue
        {
            get
            {
                return this._maxValue;
            }
            set
            {
                this._maxValue = value;
            }
        }
        /// <summary>
        /// 当InputType为数字类型时，能输入的最小值
        /// </summary>
        /// <value>The minimum value.</value>
        [Description("当InputType为数字类型时，能输入的最小值。")]
        public decimal MinValue
        {
            get
            {
                return this._minValue;
            }
            set
            {
                this._minValue = value;
            }
        }
        /// <summary>
        /// 当InputType为数字类型时，能输入的最小值
        /// </summary>
        /// <value>The length of the decimal.</value>
        [Description("当InputType为数字类型时，小数位数。")]
        public int DecLength
        {
            get
            {
                return this._decLength;
            }
            set
            {
                this._decLength = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextBoxEx" /> class.
        /// </summary>
        public TextBoxEx()
        {
            this.InitializeComponent();
            base.GotFocus += new EventHandler(this.TextBoxEx_GotFocus);
            base.MouseUp += new MouseEventHandler(this.TextBoxEx_MouseUp);
            base.KeyPress += TextBoxEx_KeyPress;

            base.MouseHover += TxtInput_MouseHover;
            base.MouseLeave += TxtInput_MouseLeave;
        }
        private void TxtInput_MouseLeave(object sender, EventArgs e)
        {
            CloseTips();
        }
        public bool IsShowOverlength = false;

        Forms.FrmAnchorTips _frmAnchorTips;
        void CloseTips()
        {
            if (_frmAnchorTips != null)
            {
                _frmAnchorTips.Close();
                _frmAnchorTips = null;
            }
        }
        private void TxtInput_MouseHover(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.Text))
            {
                CloseTips();
                return;
            }
            if (!IsShowOverlength)
                return;
            var tw = TextRenderer.MeasureText(this.Text, this.Font).Width;
            if (tw <= this.Width - 8)
            {
                CloseTips();
                return;
            }
            var tips = this.Text;
            if (_frmAnchorTips == null)
                _frmAnchorTips = Forms.FrmAnchorTips.ShowTips(this, tips, Forms.AnchorTipsLocation.BOTTOM, WinDo.Utilities.PublicResource.WDColors.TaskListTip, autoCloseTime: 6000);
        }
        /// <summary>
        /// Handles the KeyPress event of the TextBoxEx control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs" /> instance containing the event data.</param>
        void TextBoxEx_KeyPress(object sender, KeyPressEventArgs e)
        {
            //以下代码  取消按下回车或esc的“叮”声
            if (!Multiline)
                if (e.KeyChar == System.Convert.ToChar(13) || e.KeyChar == System.Convert.ToChar(27))
                {
                    e.Handled = true;
                }
        }

        /// <summary>
        /// Handles the MouseUp event of the TextBoxEx control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs" /> instance containing the event data.</param>
        private void TextBoxEx_MouseUp(object sender, MouseEventArgs e)
        {
            //if (this.blnFocus)
            //{
            //    base.SelectAll();
            //    this.blnFocus = false;
            //}
        }

        /// <summary>
        /// Handles the GotFocus event of the TextBoxEx control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void TextBoxEx_GotFocus(object sender, EventArgs e)
        {
            //this.blnFocus = true;
            //base.SelectAll();
            base.Select(base.TextLength, 0);//光标定位到文本最后
            base.ScrollToCaret();//滚动到光标处
        }

        /// <summary>
        /// Handles the TextChanged event of the TextBoxEx control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void TextBoxEx_TextChanged(object sender, EventArgs e)
        {
            if (this.Text == "")
            {
                this.m_strOldValue = this.Text;
            }
            else if (this.m_strOldValue != this.Text)
            {
                if (!ControlHelper.CheckInputType(this.Text, this._inputType, this._maxValue, this._minValue, this._decLength, this._regexPattern))
                {
                    int num = base.SelectionStart;
                    if (this.m_strOldValue.Length < this.Text.Length)
                    {
                        num--;
                    }
                    else
                    {
                        num++;
                    }
                    base.TextChanged -= new EventHandler(this.TextBoxEx_TextChanged);
                    this.Text = this.m_strOldValue;
                    base.TextChanged += new EventHandler(this.TextBoxEx_TextChanged);
                    if (num < 0)
                    {
                        num = 0;
                    }
                    base.SelectionStart = num;
                }
                else
                {
                    this.m_strOldValue = this.Text;
                }
            }
        }


        private void PaintPlacehold()
        {
            if (string.IsNullOrEmpty(this.Text) && !string.IsNullOrEmpty(this._promptText))
            {
                using (Graphics graphics = Graphics.FromHwnd(base.Handle))
                {
                    if (this.Text.Length == 0 && !string.IsNullOrEmpty(this.PromptText))
                    {
                        TextFormatFlags textFormatFlags = TextFormatFlags.EndEllipsis;
                        if (Multiline)
                            textFormatFlags |= TextFormatFlags.Top;
                        else
                            textFormatFlags |= TextFormatFlags.VerticalCenter;
                        if (this.RightToLeft == RightToLeft.Yes)
                        {
                            textFormatFlags |= (TextFormatFlags.Right | TextFormatFlags.RightToLeft);
                        }
                        TextRenderer.DrawText(graphics, this.PromptText, this._promptFont, base.ClientRectangle, this._promptColor, textFormatFlags);
                    }
                }
            }
        }

        /// <summary>
        /// 处理 Windows 消息。
        /// </summary>
        /// <param name="m">一个 Windows 消息对象。</param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 15 || m.Msg == 7 || m.Msg == 8 || m.Msg == 0xF || m.Msg == 0x133)
            {
                PaintPlacehold();
            }
        }

        /// <summary>
        /// Handles the <see cref="E:TextChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            base.Invalidate();
        }
    }

    [ToolboxItem(false)]
    public partial class TextBoxEx1 : TextBox
    {

        /// <summary>
        /// The BLN focus
        /// </summary>
        private bool blnFocus = false;

        /// <summary>
        /// The prompt text
        /// </summary>
        private string _promptText = string.Empty;

        /// <summary>
        /// The prompt font
        /// </summary>
        private Font _promptFont = new Font("微软雅黑", 15f, FontStyle.Regular, GraphicsUnit.Pixel);

        /// <summary>
        /// The prompt color
        /// </summary>
        private Color _promptColor = Color.Gray;

        /// <summary>
        /// My rectangle
        /// </summary>
        private Rectangle _myRectangle = Rectangle.FromLTRB(1, 3, 1000, 3);

        /// <summary>
        /// The input type
        /// </summary>
        private TextInputType _inputType = TextInputType.NotControl;

        /// <summary>
        /// The regex pattern
        /// </summary>
        private string _regexPattern = "";

        /// <summary>
        /// The m string old value
        /// </summary>
        private string m_strOldValue = string.Empty;

        /// <summary>
        /// The maximum value
        /// </summary>
        private decimal _maxValue = 1000000m;

        /// <summary>
        /// The minimum value
        /// </summary>
        private decimal _minValue = -1000000m;

        /// <summary>
        /// The decimal length
        /// </summary>
        private int _decLength = 2;

        /// <summary>
        /// 水印文字
        /// </summary>
        /// <value>The prompt text.</value>
        [Description("水印文字"), Category("自定义")]
        public string PromptText
        {
            get
            {
                return this._promptText;
            }
            set
            {
                this._promptText = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the prompt font.
        /// </summary>
        /// <value>The prompt font.</value>
        [Description("水印字体"), Category("自定义")]
        public Font PromptFont
        {
            get
            {
                return this._promptFont;
            }
            set
            {
                this._promptFont = value;
            }
        }

        /// <summary>
        /// Gets or sets the color of the prompt.
        /// </summary>
        /// <value>The color of the prompt.</value>
        [Description("水印颜色"), Category("自定义")]
        public Color PromptColor
        {
            get
            {
                return this._promptColor;
            }
            set
            {
                this._promptColor = value;
            }
        }

        /// <summary>
        /// Gets or sets my rectangle.
        /// </summary>
        /// <value>My rectangle.</value>
        public Rectangle MyRectangle
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the old text.
        /// </summary>
        /// <value>The old text.</value>
        public string OldText
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of the input.
        /// </summary>
        /// <value>The type of the input.</value>
        [Description("获取或设置一个值，该值指示文本框中的文本输入类型。")]
        public TextInputType InputType
        {
            get
            {
                return this._inputType;
            }
            set
            {
                this._inputType = value;
                if (value != TextInputType.NotControl)
                {
                    TextChanged -= new EventHandler(this.TextBoxEx_TextChanged);
                    TextChanged += new EventHandler(this.TextBoxEx_TextChanged);
                }
                else
                {
                    TextChanged -= new EventHandler(this.TextBoxEx_TextChanged);
                }
            }
        }
        /// <summary>
        /// 获取或设置一个值，该值指示当输入类型InputType=Regex时，使用的正则表达式。
        /// </summary>
        /// <value>The regex pattern.</value>
        [Description("获取或设置一个值，该值指示当输入类型InputType=Regex时，使用的正则表达式。")]
        public string RegexPattern
        {
            get
            {
                return this._regexPattern;
            }
            set
            {
                this._regexPattern = value;
            }
        }
        /// <summary>
        /// 当InputType为数字类型时，能输入的最大值
        /// </summary>
        /// <value>The maximum value.</value>
        [Description("当InputType为数字类型时，能输入的最大值。")]
        public decimal MaxValue
        {
            get
            {
                return this._maxValue;
            }
            set
            {
                this._maxValue = value;
            }
        }
        /// <summary>
        /// 当InputType为数字类型时，能输入的最小值
        /// </summary>
        /// <value>The minimum value.</value>
        [Description("当InputType为数字类型时，能输入的最小值。")]
        public decimal MinValue
        {
            get
            {
                return this._minValue;
            }
            set
            {
                this._minValue = value;
            }
        }
        /// <summary>
        /// 当InputType为数字类型时，能输入的最小值
        /// </summary>
        /// <value>The length of the decimal.</value>
        [Description("当InputType为数字类型时，小数位数。")]
        public int DecLength
        {
            get
            {
                return this._decLength;
            }
            set
            {
                this._decLength = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextBoxEx" /> class.
        /// </summary>
        public TextBoxEx1()
        {
            base.GotFocus += new EventHandler(this.TextBoxEx_GotFocus);
            base.MouseUp += new MouseEventHandler(this.TextBoxEx_MouseUp);
            base.KeyPress += TextBoxEx_KeyPress;
        }

        /// <summary>
        /// Handles the KeyPress event of the TextBoxEx control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs" /> instance containing the event data.</param>
        void TextBoxEx_KeyPress(object sender, KeyPressEventArgs e)
        {
            //以下代码  取消按下回车或esc的“叮”声
            //if (e.KeyChar == System.Convert.ToChar(13) || e.KeyChar == System.Convert.ToChar(27))
            //{
            //    e.Handled = true;
            //}
        }

        /// <summary>
        /// Handles the MouseUp event of the TextBoxEx control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs" /> instance containing the event data.</param>
        private void TextBoxEx_MouseUp(object sender, MouseEventArgs e)
        {
            //if (this.blnFocus)
            //{
            //    base.SelectAll();
            //    this.blnFocus = false;
            //}
        }

        /// <summary>
        /// Handles the GotFocus event of the TextBoxEx control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void TextBoxEx_GotFocus(object sender, EventArgs e)
        {
            //this.blnFocus = true;
            //base.SelectAll();
            base.Select(base.TextLength, 0);//光标定位到文本最后
            base.ScrollToCaret();//滚动到光标处
        }

        /// <summary>
        /// Handles the TextChanged event of the TextBoxEx control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void TextBoxEx_TextChanged(object sender, EventArgs e)
        {
            if (this.Text == "")
            {
                this.m_strOldValue = this.Text;
            }
            else if (this.m_strOldValue != this.Text)
            {
                if (!ControlHelper.CheckInputType(this.Text, this._inputType, this._maxValue, this._minValue, this._decLength, this._regexPattern))
                {
                    int num = base.SelectionStart;
                    if (this.m_strOldValue.Length < this.Text.Length)
                    {
                        num--;
                    }
                    else
                    {
                        num++;
                    }
                    base.TextChanged -= new EventHandler(this.TextBoxEx_TextChanged);
                    this.Text = this.m_strOldValue;
                    base.TextChanged += new EventHandler(this.TextBoxEx_TextChanged);
                    if (num < 0)
                    {
                        num = 0;
                    }
                    base.SelectionStart = num;
                }
                else
                {
                    this.m_strOldValue = this.Text;
                }
            }
        }


        private void PaintPlacehold()
        {
            if (string.IsNullOrEmpty(this.Text) && !string.IsNullOrEmpty(this._promptText))
            {
                using (Graphics graphics = Graphics.FromHwnd(base.Handle))
                {
                    if (this.Text.Length == 0 && !string.IsNullOrEmpty(this.PromptText))
                    {
                        TextFormatFlags textFormatFlags = TextFormatFlags.EndEllipsis;
                        if (Multiline)
                            textFormatFlags |= TextFormatFlags.Top;
                        else
                            textFormatFlags |= TextFormatFlags.VerticalCenter;
                        if (this.RightToLeft == RightToLeft.Yes)
                        {
                            textFormatFlags |= (TextFormatFlags.Right | TextFormatFlags.RightToLeft);
                        }
                        TextRenderer.DrawText(graphics, this.PromptText, this._promptFont, base.ClientRectangle, this._promptColor, textFormatFlags);
                    }
                }
            }
        }

        /// <summary>
        /// 处理 Windows 消息。
        /// </summary>
        /// <param name="m">一个 Windows 消息对象。</param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 15 || m.Msg == 7 || m.Msg == 8 || m.Msg == 0xF || m.Msg == 0x133)
            {
                PaintPlacehold();
            }
        }

        /// <summary>
        /// Handles the <see cref="E:TextChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            base.Invalidate();
        }
    }
}

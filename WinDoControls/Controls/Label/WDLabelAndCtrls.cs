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
using WinDoControls;
using WinDo.Utilities;

namespace WinDoControls.Controls
{

    public partial class LabelAndCtrls<T> : Panel where T : System.Windows.Forms.Control, new()
    {

        public WDRedALabel label;
        public T valueControl;
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.Style &= ~0x02000000;  // Turn off WS_CLIPCHILDREN
        //        return cp;
        //    }
        //}

        public LabelAndCtrls(string label_text)
            : base()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            GotFocus += new EventHandler(UCLabelAndControls_GotFocus);
            BackColor = Color.Transparent;

            AutoSize = false;
            this.Padding = new Padding(1);
            base.Height = 32;

            Font = WDFonts.TextFont;
            BorderStyle = System.Windows.Forms.BorderStyle.None;
            //FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            label = new WDRedALabel();
            label.BackColor = Color.Transparent;
            label.TextAlign = ContentAlignment.MiddleRight;
            label.TabStop = false;
            label.Font = WDFonts.TextFont;
            label.TextValue = label_text;
            //label.AutoSize = true;
            label.ShowRedAsterisk = true;
            label.Width = 100;
            label.Margin = new System.Windows.Forms.Padding(0);
            label.Padding = new System.Windows.Forms.Padding(0);
            label.Height = Height;
            label.Dock = DockStyle.Left;
            //label.Anchor = AnchorStyles.Left;
            //label.TextAlign = ContentAlignment.MiddleLeft;

            Controls.Add(label);

            valueControl = new T();
            valueControl.Width = 150;
            valueControl.TabStop = true;
            valueControl.TabIndex = 0;
            valueControl.Dock = DockStyle.Fill;
            valueControl.Padding = new System.Windows.Forms.Padding(1);
            valueControl.Margin = new Padding(0);
            valueControl.Height = Height;
            valueControl.Font = WDFonts.TextFont;
            Controls.Add(valueControl);
            valueControl.BringToFront();
            base.Width = label.Width + valueControl.Width;
            LabelCtrlsSpace = 10;
        }

        void UCLabelAndControls_GotFocus(object sender, EventArgs e)
        {
            if (valueControl != null)
                valueControl.Focus();
        }

        [Description("显示红星"), Category("自定义"), DefaultValue(true)]
        public bool IsRequired
        {
            get { return this.label.ShowRedAsterisk; }
            set
            {
                this.label.ShowRedAsterisk = value;
            }
        }



        private TransparentPanel maskPanel = null;
        private bool _readOnly = false;
        public bool ReadOnly
        {
            get { return _readOnly; }
            set
            {
                _readOnly = value;
                this.TabStop = !_readOnly;
                if (valueControl != null)
                {
                    this.valueControl.TabStop = !_readOnly;
                    if (valueControl is WDTextBoxClear)
                    {
                        (valueControl as WDTextBoxClear).txtInput.ReadOnly = _readOnly;
                        (valueControl as WDTextBoxClear).txtInput.TabStop = !_readOnly;
                    }
                    else if (valueControl is WDCombox)
                    {
                        (valueControl as WDCombox).txtInput.ReadOnly = _readOnly;
                        (valueControl as WDCombox).txtInput.TabStop = !_readOnly;
                    }
                    else if (valueControl is WDComboxGrid)
                    {
                        (valueControl as WDComboxGrid).txtInput.ReadOnly = _readOnly;
                        (valueControl as WDComboxGrid).txtInput.TabStop = !_readOnly;
                    }

                    else if (valueControl is WDSearchBox)
                    {
                        (valueControl as WDSearchBox).txtInput.ReadOnly = _readOnly;
                        (valueControl as WDSearchBox).txtInput.TabStop = !_readOnly;
                    }
                }
                if (!DesignMode)
                {
                    if (_readOnly && maskPanel == null)
                    {
                        maskPanel = new TransparentPanel();
                        maskPanel.TabStop = false;
                        maskPanel.Size = this.Size;
                        maskPanel.Cursor = Cursors.No;
                        //maskPanel.BackColor = Color.Transparent;
                        //maskPanel.Parent = this;
                        maskPanel.Visible = false;
                        maskPanel.Dock = DockStyle.None;
                        //maskPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                        this.Controls.Add(maskPanel);
                        this.SizeChanged += (sender, e) => { maskPanel.Size = this.Size; };

                        //maskPanel.Location = this.Location;
                    }
                    if (maskPanel != null)
                        if (_readOnly)
                        {
                            maskPanel.Visible = true;
                            maskPanel.BringToFront();
                        }
                        else
                        {
                            maskPanel.Visible = false;
                        }
                }
            }
        }

        [Description("标签文本"), Category("自定义"), DefaultValue(100)]
        public string LabelText
        {
            get { return this.label.TextValue; }
            set
            {
                this.label.TextValue = value;
            }
        }
        [Description("标签宽度"), Category("自定义"), DefaultValue(100)]
        public int LabelWidth
        {
            get { return this.label.Width; }
            set
            {
                this.label.Width = value;
            }
        }
        [Description("第一个控件宽度"), Category("自定义"), DefaultValue(150)]
        public int FirstCtrlWidth
        {
            get { return this.Controls.Count > 1 ? this.Controls[1].Width : 0; }
            set
            {
                if (this.Controls.Count > 1)
                    this.Controls[1].Width = value;
            }
        }

        [Description("控件间距"), Category("自定义")]
        public int CtrlsSpace
        {
            get { return this.Controls.Count > 1 ? this.Controls[1].Margin.Right : 0; }
            set
            {
                foreach (System.Windows.Forms.Control item in this.Controls)
                {
                    if (item != label)
                        item.Margin = new System.Windows.Forms.Padding(item.Margin.Left, item.Margin.Top, value, item.Margin.Bottom);
                }
            }
        }

        [Description("标签与控件间距"), Category("自定义")]
        public int LabelCtrlsSpace
        {
            get { return label.Margin.Right; }
            set
            {
                label.Margin = new System.Windows.Forms.Padding(label.Margin.Left, label.Margin.Top, value, label.Margin.Bottom);
            }
        }
          

        public virtual object GetSelectValue() { return null; }
        public virtual void SetSelectValue(object value) { }
        public virtual object GetTextByValue(object value) { return value; }
    }

    public partial class WDLabelLabel : LabelAndCtrls<Label>
    {
        public WDLabelLabel()
            : this("标签")
        { }
        public WDLabelLabel(string label_text)
            : base(label_text)
        {
            valueControl.AutoSize = false;
            valueControl.TextAlign = ContentAlignment.MiddleLeft;
        }
        public override void SetSelectValue(object value)
        {
            valueControl.Text = value == null ? "" : value.ToString();
        }
        public override object GetSelectValue()
        {
            return valueControl.Text;
        }
        public override object GetTextByValue(object value)
        {
            return value;
        }
    }

    public partial class WDLabelTextBox : LabelAndCtrls<WDTextBoxClear>
    {
        public WDLabelTextBox()
            : this("文本框")
        { }
        public WDLabelTextBox(string label_text)
            : base(label_text)
        {
            valueControl.Padding = new System.Windows.Forms.Padding(0);
        }
        public override object GetSelectValue()
        {
            if (Trim)
            {
                var v = ControlHelper.GetInputQueryText(valueControl.InputText);
                valueControl.InputText = v;
                return v;
            }
            return valueControl.InputText;
        }
        public bool IsErrorColor
        {
            get { return valueControl.IsErrorColor; }
            set
            {
                valueControl.IsErrorColor = value;
            }
        }

        public bool Trim = false;
        public override void SetSelectValue(object value)
        {

            valueControl.InputText = value == null ? "" : value.ToString();
        }

        public override object GetTextByValue(object value)
        {
            return value;
        }
    }
    public partial class WDLabelMultiLineTextBox : LabelAndCtrls<WDTextBoxClear>
    {
        public WDLabelMultiLineTextBox()
            : this("多行文本框")
        { }
        public WDLabelMultiLineTextBox(string label_text)
            : base(label_text)
        {
            label.TextAlign = ContentAlignment.TopRight;
            valueControl.Padding = new System.Windows.Forms.Padding(0);
            this.Height *= 2;
            valueControl.MultiLine = true;
        }

        public override object GetSelectValue()
        {
            return valueControl.InputText;
        }

        public override void SetSelectValue(object value)
        {
            valueControl.InputText = value == null ? "" : value.ToString();
        }

        public override object GetTextByValue(object value)
        {
            return value;
        }
    }

    public partial class WDLabelComboBox : LabelAndCtrls<WDCombox>
    {
        public WDLabelComboBox()
            : this("下拉选择框")
        {
            valueControl.txtInput.PromptText = "请选择";
        }
        public WDLabelComboBox(string label_text)
            : base(label_text)
        { }
        public override object GetSelectValue()
        {
            if (valueControl.BoxStyle == ComboBoxStyle.DropDownList)
            {
                //只能下拉选择
                return string.IsNullOrWhiteSpace(valueControl.SelectedValue) ? ("请选择" == valueControl.TextValue.Trim() ? "" : valueControl.TextValue.Trim()) : valueControl.SelectedValue;
            }
            else
            {
                //可以输入
                return "请选择" == valueControl.TextValue.Trim() ? "" : valueControl.TextValue.Trim();
            }
        }

        public override void SetSelectValue(object value)
        {
            valueControl.SelectedValue = (value == null ? null : value.ToString());
            if (string.IsNullOrWhiteSpace(valueControl.SelectedValue) && value != null)
            {
                valueControl.TextValue = value.ToString();
            }
        }



        public override object GetTextByValue(object value)
        {
            if (value == null) return "";
            var v = valueControl.GetTextByValue(value);
            if ("请选择" == v.ToString()) return "";
            return v;
        }
    }


    public partial class WDLabelComboBox2 : LabelAndCtrls<UCCombox2>
    {
        public WDLabelComboBox2()
            : this("下拉选择框")
        {
            valueControl.txtInput.PromptText = "请选择";
        }
        public WDLabelComboBox2(string label_text)
            : base(label_text)
        { }
        public override object GetSelectValue()
        {
            return valueControl.SelectedDataRow;
        }

        public override void SetSelectValue(object value)
        {
            valueControl.TextValue = value.ToString();
        }

        public override object GetTextByValue(object value)
        {
            return null;
        }
    }

    public partial class WDLabelComboxGrid : LabelAndCtrls<WDComboxGrid>
    {
        public WDLabelComboxGrid()
            : this("下拉搜索选择框")
        {
            valueControl.txtInput.PromptText = "请选择";
        }
        public WDLabelComboxGrid(string label_text)
            : base(label_text)
        { }
        public override object GetSelectValue()
        {
            return valueControl.SelectedValue;
        }

        public override void SetSelectValue(object value)
        {
            valueControl.SelectedValue = value.AsString();
        }
        public override object GetTextByValue(object value)
        {
            return valueControl.GetTextByValue(value);
        }


    }

    public partial class WDLabelCheckComboxGrid : LabelAndCtrls<WDCheckComboxGrid>
    {
        public WDLabelCheckComboxGrid()
            : this("下拉多选框")
        { }
        public WDLabelCheckComboxGrid(string label_text)
            : base(label_text)
        {
            valueControl.txtInput.PromptText = "请选择";
        }
        public override object GetSelectValue()
        {
            return valueControl.SelectKeyValues == null ? "" : string.Join(",", valueControl.SelectKeyValues);
        }
        public override void SetSelectValue(object value)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                valueControl.SelectKeyValues = null;
                return;
            }
            valueControl.SelectKeyValues = value.ToString().Trim().Split(new string[] { ",", "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public override object GetTextByValue(object value)
        {
            return valueControl.GetTextByValue(value);
        }
    }

    public partial class WDLabelDateTimePicker : LabelAndCtrls<WDDateTimePicker>
    {

        public event EventHandler CloseUp;

        public WDLabelDateTimePicker()
            : this("日期选择框")
        {

        }
        public WDLabelDateTimePicker(string label_text)
            : base(label_text)
        {
            //valueControl.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            valueControl.SetCoverRightSideLine();

            valueControl.CloseUp += ValueControl_CloseUp;
        }

        private void ValueControl_CloseUp(object sender, EventArgs e)
        {
            if (CloseUp != null)
            {
                CloseUp(valueControl, e);
            }
        }

        public override object GetSelectValue()
        {
            return valueControl.DTP.Value;
        }
        public override void SetSelectValue(object value)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                valueControl.DTP.Value = null;
                return;
            }
            valueControl.DTP.Value = DateTime.Parse(value.ToString());
        }

        public override object GetTextByValue(object value)
        {
            return value;
        }
    }


    public partial class WDLabelTimePicker : LabelAndCtrls<UCTimePicker>
    {

        public event EventHandler CloseUp;

        public WDLabelTimePicker()
            : this("时间选择框")
        {

        }
        public WDLabelTimePicker(string label_text)
            : base(label_text)
        {
            //valueControl.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            valueControl.SetCoverRightSideLine();

            valueControl.CloseUp += ValueControl_CloseUp;
        }

        private void ValueControl_CloseUp(object sender, EventArgs e)
        {
            if (CloseUp != null)
            {
                CloseUp(valueControl, e);
            }
        }

        public override object GetSelectValue()
        {
            return valueControl.DTP.Value;
        }
        public override void SetSelectValue(object value)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                valueControl.DTP.Value = null;
                return;
            }
            valueControl.DTP.Value = DateTime.Parse(value.ToString());
        }

        public override object GetTextByValue(object value)
        {
            return value;
        }
    }

    public partial class WDLabelSearchBox : LabelAndCtrls<WDSearchBox>
    {
        public WDLabelSearchBox()
            : this("搜索框")
        { }
        public WDLabelSearchBox(string label_text)
            : base(label_text)
        {
            //valueControl.Padding = new System.Windows.Forms.Padding(1, 1, 1, 1);
            valueControl.txtInput.PromptText = "请选择";
        }
        public override object GetSelectValue()
        {
            return valueControl.InputText;
        }
        public override void SetSelectValue(object value)
        {
            valueControl.InputText = value == null ? "" : value.ToString();
            if (string.IsNullOrWhiteSpace(valueControl.InputText))
                valueControl.SelectedObj = null;
        }
        public override object GetTextByValue(object value)
        {
            return value;
        }
    }

    public partial class WDLabelRadioButton : LabelAndCtrls<WDRadioButton>
    {
        public WDLabelRadioButton()
            : this("单选框")
        {

            panel = new UCControlBaseWithError();
            panel.Padding = new System.Windows.Forms.Padding(5, 1, 1, 1);
            panel.ConerRadius = 1;
            panel.IsRadius = true;
            panel.IsShowRect = true;
            panel.Dock = DockStyle.Fill;
            Controls.Add(panel);
            panel.BringToFront();
            valueControl.Dispose();
            valueControl = null;
        }
        public WDLabelRadioButton(string label_text)
            : base(label_text)
        {
        }

        private string[] btnDescs;
        private UCControlBaseWithError panel;
        public bool IsErrorColor
        {
            get { return panel.IsErrorColor; }
            set
            {
                panel.IsErrorColor = value;
            }
        }

        [Description("需要显示的按钮文字"), Category("自定义")]
        public string[] BtnDecs
        {
            get { return btnDescs; }
            set
            {
                if (value != null)
                {
                    btnDescs = value;
                    ControlHelper.ClearChildControls(panel);

                    //                var btns = @"001,第1项
                    //002,第2项
                    //003,第3项
                    //004,第4项
                    //005,第5项
                    //".Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList().Select((v, i) =>


                    var btns = value.ToList().Select((v, i) =>
                        {
                            var ivs = v.Trim().Split(',');
                            var width = TextRenderer.MeasureText(ivs[ivs.Length > 1 ? 1 : 0], WDFonts.TextFont).Width;
                            return new WDRadioButton()
                            {
                                Font = WDFonts.TextFont,
                                TextValue = ivs[ivs.Length > 1 ? 1 : 0],
                                KeyValue = ivs[0],
                                Width = width + 28,
                                Margin = new Padding(1),
                                Padding = new Padding(0),
                                AllowCancel = _allowCancel
                            };
                        });
                    foreach (var btn in btns)
                    {
                        btn.Dock = DockStyle.Left;
                        panel.Controls.Add(btn);
                        btn.BringToFront();
                    }
                    panel.BringToFront();
                    btnValues = btnDescs.ToList().Select((v, i) =>
                    {
                        var ivs = v.Trim().Split(',');
                        return ivs[0];
                    }).ToArray();
                    this.Invalidate();
                }
            }
        }

        private string[] btnValues;
        [Browsable(false)]
        public string[] BtnValues
        {
            get { return btnValues; }
        }

        [Browsable(false)]
        public List<WDRadioButton> EditControls
        {
            get
            {

                return panel.Controls.Cast<System.Windows.Forms.Control>().Where(c => c is WDRadioButton).Cast<WDRadioButton>().ToList();
            }
        }

        public override object GetSelectValue()
        {
            var ctrl = EditControls.FirstOrDefault(c => c.Checked);
            if (ctrl != null)
                return ctrl.KeyValue;
            return "";
        }

        public override void SetSelectValue(object value)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                foreach (var item in EditControls)
                {
                    item.Checked = false;
                }
                return;
            }

            var ctrl = EditControls.FirstOrDefault(c => c.KeyValue == value.ToString());
            if (ctrl != null)
                ctrl.Checked = true;
        }

        public override object GetTextByValue(object value)
        {
            var ctrl = EditControls.FirstOrDefault(c => c.KeyValue == value.AsString());
            if (ctrl != null)
                return ctrl.TextValue;
            return "";
        }

        private bool _allowCancel = true;
        public bool AllowCancel
        {
            get { return _allowCancel; }
            set
            {
                _allowCancel = value;
                EditControls.ForEach(i =>
                {
                    i.AllowCancel = value;
                });
            }
        }
    }

    public partial class WDLabelCheckBoxSingle : LabelAndCtrls<WDCheckBox>
    {
        public WDLabelCheckBoxSingle()
            : this("勾选框")
        { }
        public WDLabelCheckBoxSingle(string label_text)
            : base(label_text)
        {
            valueControl.TextValue = "";
        }

        public override object GetSelectValue()
        {
            return valueControl.Checked ? 1 : 0;//1 已勾选  0 未勾选
        }
        public override void SetSelectValue(object value)
        {
            valueControl.Checked = value.AsString("0") == "1";
        }
        public override object GetTextByValue(object value)
        {
            return value.AsString("0") == "1" ? "已勾选" : "未勾选";
        }
    }
    public partial class WDLabelCheckBox : LabelAndCtrls<WDCheckBox>
    {
        public WDLabelCheckBox()
            : this("多选框")
        {

            panel = new UCControlBaseWithError();
            panel.Padding = new System.Windows.Forms.Padding(5, 1, 1, 1);
            panel.ConerRadius = 1;
            panel.IsRadius = true;
            panel.IsShowRect = true;
            panel.Dock = DockStyle.Fill;
            Controls.Add(panel);
            panel.BringToFront();
            valueControl.Dispose();
            valueControl = null;
        }
        public WDLabelCheckBox(string label_text)
            : base(label_text)
        { }

        private string[] btnDescs;
        private UCControlBaseWithError panel;
        public bool IsErrorColor
        {
            get { return panel.IsErrorColor; }
            set
            {
                panel.IsErrorColor = value;
            }
        }
        [Description("需要显示的按钮文字"), Category("自定义")]
        public string[] BtnDecs
        {
            get { return btnDescs; }
            set
            {
                if (value != null)
                {
                    btnDescs = value;

                    ControlHelper.ClearChildControls(panel);
                    var btns = value.ToList().Select((v, i) =>
                    {
                        var ivs = v.Trim().Split(',');
                        var width = TextRenderer.MeasureText(ivs[ivs.Length > 1 ? 1 : 0], WDFonts.TextFont).Width;
                        return new WDCheckBox()
                        {
                            Font = WDFonts.TextFont,
                            TextValue = ivs[ivs.Length > 1 ? 1 : 0],
                            KeyValue = ivs[0],
                            Width = width + 28,
                            Margin = new Padding(1),
                            Padding = new Padding(0)
                        };
                    });
                    panel.Dock = DockStyle.Fill;

                    foreach (var btn in btns)
                    {
                        btn.Dock = DockStyle.Left;
                        panel.Controls.Add(btn);
                        btn.BringToFront();
                    }
                    panel.BringToFront();
                    btnValues = btnDescs.ToList().Select((v, i) =>
                    {
                        var ivs = v.Trim().Split(',');
                        return ivs[0];
                    }).ToArray();
                    this.Invalidate();
                }
            }
        }

        private string[] btnValues;
        [Browsable(false)]
        public string[] BtnValues
        {
            get { return btnValues; }
        }

        [Browsable(false)]
        public List<WDCheckBox> EditControls
        {
            get { return panel.Controls.Cast<System.Windows.Forms.Control>().Where(c => c is WDCheckBox).Cast<WDCheckBox>().ToList(); }
        }
        public override object GetSelectValue()
        {
            var ctrls = EditControls.Where(c => c.Checked).ToList();
            if (ctrls.Count > 0)
                return string.Join(",", ctrls.Select(c => c.KeyValue));
            return "";
        }

        public override void SetSelectValue(object value)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                foreach (var item in EditControls)
                {
                    item.Checked = false;
                }
                return;
            }
            var vs = value.ToString().Trim().Split(new string[] { ",", "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            var ctrls = EditControls.Where(c => vs.Contains(c.KeyValue));
            foreach (var item in ctrls)
            {
                item.Checked = true;
            }
        }

        public override object GetTextByValue(object value)
        {
            var keys = value.AsString().Split(',').Select(k => k.Trim());
            return EditControls.Where(c => keys.Contains(c.KeyValue)).CommaSeparate(c => c.TextValue);
        }

    }


    public partial class WDLabelSwitch : LabelAndCtrls<WDSwitch>
    {
        public WDLabelSwitch()
            : this("开关")
        { }
        public WDLabelSwitch(string label_text)
            : base(label_text)
        {
            valueControl.TrueColor = WDColors.Green6;
        }

        public override object GetSelectValue()
        {
            return valueControl.Checked ? 1 : 0;//1 已勾选  0 未勾选
        }
        public override void SetSelectValue(object value)
        {
            valueControl.Checked = value.AsString("0") == "1";
        }
        public override object GetTextByValue(object value)
        {
            return value.AsString("0") == "1" ? "开" : "关";
        }
    }
}

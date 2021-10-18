














using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using WinDo.Utilities.PublicResource;
using System.Runtime.InteropServices;

namespace WinDoControls.Controls
{





    [DefaultEvent("SelectedChangedEvent")]
    public partial class WDCombox : WDCtrlBase
    {



        Color _ForeColor = Color.Black;// Color.FromArgb(64, 64, 64);


        //ucComboxGrid1.BoxStyle = ComboBoxStyle.DropDownList;//模糊查询
        //ucComboxGrid1.GridColumns = new List<DataGridViewColumnEntity> {
        //    new DataGridViewColumnEntity() { DataField = "Value", HeadText = "", Width = ucComboxGrid1.Width, WidthType = SizeType.Absolute, TextAlign = ContentAlignment.MiddleLeft }
        //    };
        //ucComboxGrid1.IsShowHead = false;
        //ucComboxGrid1.TextField = "Value";
        //var ms = BLL.Machine.Instance.GetModelList(" RowStatus=1 ").OrderByDescending(r => r.IsActive).ThenBy(r => r.Machine_Type).ThenBy(o => o.OrderByNo).Select(o =>
        //{
        //    var name = o.Machine_Name.Trim();
        //    if (o.IsActive == 0)
        //        name += "(停用)";
        //    return
        //    new KeyValuePair<string, string>(o.Machine_ID.ToString(), name);
        //}).ToList();
        //ucComboxGrid1.GridDataSource = ms;
        //ucComboxGrid1.SelectedChangedEvent += (s, e) => { label.Text = ucComboxGrid1.SelectedText; };
        ////ucComboxGrid1.SetSelectValue(doctor.DoctorGroupName.AsString());
        public void ArrowClick()
        {
            click_MouseDown(null, null);
        }

        /// <summary>
        /// 可以输入的最大文本数
        /// </summary>
        public int MaxLenth
        {
            get
            {
                return this.txtInput.MaxLength;
            }
            set
            {
                this.txtInput.MaxLength = value;
            }
        }
         

        [Description("文字颜色"), Category("自定义")]
        public override Color ForeColor
        {
            get
            {
                return _ForeColor;
            }
            set
            {
                _ForeColor = value;
                //lblInput.ForeColor = value;
                txtInput.ForeColor = value;
            }
        }



        [Description("选中事件"), Category("自定义")]
        public event EventHandler SelectedChangedEvent;



        [Description("文本改变事件"), Category("自定义")]
        public event EventHandler TextChangedEvent;




        private ComboBoxStyle _BoxStyle = ComboBoxStyle.DropDown;




        [Description("控件样式"), Category("自定义")]
        public ComboBoxStyle BoxStyle
        {
            get { return _BoxStyle; }
            set
            {
                _BoxStyle = value;
                //if (value == ComboBoxStyle.DropDownList)
                //{
                //    lblInput.Visible = true;
                //    txtInput.Visible = false;
                //}
                //else
                //{
                //    lblInput.Visible = false;
                //    txtInput.Visible = true;
                //}
                txtInput.ReadOnly = false;
                txtInput.Enabled = true;
                if (this._BoxStyle == ComboBoxStyle.DropDownList)
                {
                    txtInput.ReadOnly = true;
                    txtInput.Enabled = false;
                    txtInput.BackColor = _BackColor;
                    //base.FillColor = _BackColor;
                    //base.RectColor = _BackColor;
                    base.FillColor = Color.White;
                    base.RectColor = Color.FromArgb(220, 220, 220);
                }
                else
                {
                    txtInput.BackColor = Color.White;
                    base.FillColor = Color.White;
                    base.RectColor = Color.FromArgb(220, 220, 220);
                }
            }
        }

        public void ChangeColorByEnable()
        {
            if (Enabled)
            {
                base.FillColor = WinDo.Utilities.PublicResource.WDColors.WhiteColor;
                this.txtInput.BackColor = WinDo.Utilities.PublicResource.WDColors.WhiteColor;
                this.txtInput.ForeColor = WinDo.Utilities.PublicResource.WDColors.BlackColor;
                panel1.BackColor = WinDo.Utilities.PublicResource.WDColors.WhiteColor;
            }
            else
            {
                base.FillColor = WinDo.Utilities.PublicResource.WDColors.GrayColor;
                this.txtInput.BackColor = WinDo.Utilities.PublicResource.WDColors.GrayColor;
                this.txtInput.ForeColor = WinDo.Utilities.PublicResource.WDColors.GrayColor;
                panel1.BackColor = WinDo.Utilities.PublicResource.WDColors.GrayColor;
            }
        }

        public void ShowDropDownIcon()
        {
            panel1.Visible = this._BoxStyle != ComboBoxStyle.Simple;
            this.txtInput.PromptText = panel1.Visible ? "请选择" : "";
            if (panel1.Visible)
            {
                txtInput.Width = this.Width - panel1.Width - 8;
            }
            else
            {
                txtInput.Width = this.Width - 10;
            }
        }
        private bool useAsLabel = false;
        public void HideDropDownIconAndBorder()
        {
            useAsLabel = true;
            panel1.Visible = false;
            this.txtInput.PromptText = "";
            txtInput.Width = this.Width - 10;
            IsShowRect = false;
        }
        public void ShowDropDownIconAndBorder()
        {
            useAsLabel = false;
            panel1.Visible = true;
            this.txtInput.PromptText = "请选择";
            txtInput.Width = this.Width - panel1.Width - 8;
            IsShowRect = true;
        }

        [DllImport("user32", EntryPoint = "HideCaret")]
        private static extern bool HideCaret(IntPtr hWnd);

        private Font _Font = WDFonts.TextFont; // new Font("微软雅黑", 12);




        private Color focusBorderColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;

        private Color defaultRectColor = WDColors.GrayRectColor;


        private bool isErrorColor = false;


        [Description("是否为错误状态"), Category("自定义")]
        public bool IsErrorColor
        {
            get { return isErrorColor; }
            set
            {
                isErrorColor = value;
                base.RectColor = value ? WDColors.ErrorRedColor : defaultRectColor; //this.Focused ? focusBorderColor : defaultRectColor;
            }
        }





        [Description("字体"), Category("自定义")]
        public new Font Font
        {
            get { return _Font; }
            set
            {
                _Font = value;
                //lblInput.Font = value;
                txtInput.Font = value;
                //txtInput.PromptFont = value;
                this.txtInput.Location = new Point(this.txtInput.Location.X, (this.Height - txtInput.Height) / 2);
                //this.lblInput.Location = new Point(this.lblInput.Location.X, (this.Height - lblInput.Height) / 2);
            }
        }






        [Obsolete("不再可用的属性")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        private new Color FillColor
        {
            get;
            set;
        }





        [Obsolete("不再可用的属性")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        private new Color RectColor
        {
            get;
            set;
        }




        private string _TextValue = "";




        [Description("文字"), Category("自定义")]
        public string TextValue
        {
            get { return _TextValue; }
            set
            {
                _TextValue = value;
                //if (lblInput.Text != value)
                //    lblInput.Text = value;
                if (txtInput.Text != value)
                    txtInput.Text = value;
            }
        }




        private List<KeyValuePair<string, string>> _source = null;




        [Description("数据源"), Category("自定义")]
        public List<KeyValuePair<string, string>> Source
        {
            get { return _source; }
            set
            {
                _source = value;
                _selectedIndex = -1;
                _selectedValue = "";
                _selectedItem = new KeyValuePair<string, string>();
                _selectedText = "";
                //lblInput.Text = "";
                txtInput.Text = "";
            }
        }




        private KeyValuePair<string, string> _selectedItem = new KeyValuePair<string, string>();




        private int _selectedIndex = -1;




        [Description("选中的数据下标"), Category("自定义")]
        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                if (value < 0 || _source == null || _source.Count <= 0 || value >= _source.Count)
                {
                    _selectedIndex = -1;
                    _selectedValue = "";
                    _selectedItem = new KeyValuePair<string, string>();
                    SelectedText = "";
                }
                else
                {
                    _selectedIndex = value;
                    _selectedItem = _source[value];
                    _selectedValue = _source[value].Key;
                    SelectedText = _source[value].Value;
                }
            }
        }




        private string _selectedValue = "";


        public string PreSelectedValue = "";


        [Description("选中的值"), Category("自定义")]
        public string SelectedValue
        {
            get
            {
                return _selectedValue;
            }
            set
            {
                if (_source == null || _source.Count <= 0)
                {
                    SelectedText = "";
                    _selectedValue = "";
                    _selectedIndex = -1;
                    _selectedItem = new KeyValuePair<string, string>();
                }
                else
                {
                    for (int i = 0; i < _source.Count; i++)
                    {
                        if (_source[i].Key == value)
                        {
                            _selectedValue = value;
                            _selectedIndex = i;
                            _selectedItem = _source[i];
                            SelectedText = _source[i].Value;
                            return;
                        }
                    }
                    _selectedValue = "";
                    _selectedIndex = -1;
                    _selectedItem = new KeyValuePair<string, string>();
                    SelectedText = "";
                }
            }
        }

        public virtual object GetTextByValue(object value)
        {
            if (value == null || _source == null || _source.Count <= 0) return value;

            if (_source.Exists(kv => !string.IsNullOrWhiteSpace(kv.Key) && kv.Key == value.ToString()))
                return _source.First(kv => kv.Key == value.ToString()).Value;
            return value;
        }

        public virtual object GetValueByText(object Text)
        {
            if (Text == null || _source == null || _source.Count <= 0) return Text;

            if (_source.Exists(kv => !string.IsNullOrWhiteSpace(kv.Value) && kv.Value == Text.ToString()))
                return _source.First(kv => kv.Value == Text.ToString()).Key;
            return Text;
        }


        private string _selectedText = "";




        [Description("选中的文本"), Category("自定义")]
        public string SelectedText
        {
            get { return _selectedText; }
            private set
            {
                _selectedText = value;
                //lblInput.Text = _selectedText;
                txtInput.Text = _selectedText;
                if (SelectedChangedEvent != null)
                {
                    SelectedChangedEvent(this, null);
                }
            }
        }




        private int _ItemWidth = 70;




        [Description("项宽度"), Category("自定义")]
        public int ItemWidth
        {
            get { return _ItemWidth; }
            set { _ItemWidth = value; }
        }




        protected int _dropPanelHeight = -1;




        [Description("下拉面板高度"), Category("自定义")]
        public int DropPanelHeight
        {
            get { return _dropPanelHeight; }
            set { _dropPanelHeight = value; }
        }


        protected int _dropPanelWidth = -1;


        [Description("下拉面板宽度"), Category("自定义")]
        public int DropPanelWidth
        {
            get { return _dropPanelWidth; }
            set { _dropPanelWidth = value; }
        }






        [Obsolete("不再可用的属性,如需要改变背景色，请使用BackColorExt")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        private new Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = Color.Transparent;
            }
        }




        private Color _BackColor = Color.White;// Color.FromArgb(240, 240, 240);




        [Description("背景色"), Category("自定义")]
        public Color BackColorExt
        {
            get
            {
                return _BackColor;
            }
            set
            {
                if (value == Color.Transparent)
                    return;
                _BackColor = value;
                //lblInput.BackColor = value;

                if (this._BoxStyle == ComboBoxStyle.DropDownList)
                {
                    txtInput.BackColor = value;
                    //base.FillColor = value;
                    //base.RectColor = value;
                    base.FillColor = Color.White;
                    base.RectColor = Color.FromArgb(220, 220, 220);
                }
                else
                {
                    txtInput.BackColor = Color.White;
                    base.FillColor = Color.White;
                    base.RectColor = Color.FromArgb(220, 220, 220);
                }
            }
        }




        private Color triangleColor = Color.FromArgb(255, 77, 59);




        [Description("三角颜色"), Category("自定义")]
        public Color TriangleColor
        {
            get { return triangleColor; }
            set
            {
                triangleColor = value;
                Bitmap bit = new Bitmap(12, 10);
                Graphics g = Graphics.FromImage(bit);
                g.SetGDIHigh();
                GraphicsPath path = new GraphicsPath();
                path.AddLines(new Point[]
                {
                    new Point(1,1),
                    new Point(11,1),
                    new Point(6,10),
                    new Point(1,1)
                });
                g.FillPath(new SolidBrush(value), path);
                g.Dispose();
                panel1.BackgroundImage = bit;
            }
        }
        public void SetMiniArrowDown()
        {
            this.panel1.Width = 24;
            this.panel1.Left = this.Width - this.panel1.Width - 2;
            this.txtInput.Width += 16;
            this.txtInput.BringToFront();
        }
        public WDCombox()
        {
            InitializeComponent();
            txtInput.MouseDown += TxtInput_MouseDown;

            //txtInput.LostFocus += new EventHandler(UCCombox_LostFocus);
            //lblInput.BackColor = _BackColor;
            if (this._BoxStyle == ComboBoxStyle.DropDownList)
            {
                txtInput.BackColor = _BackColor;
                base.FillColor = _BackColor;
                base.RectColor = _BackColor;
            }
            else
            {
                txtInput.BackColor = Color.White;
                base.FillColor = Color.White;
                base.RectColor = Color.FromArgb(220, 220, 220);
            }
            base.BackColor = Color.Transparent;
        }

        //private bool allowAddNewValue = false;

        //public bool AllowAddNewValue
        //{
        //    get { return allowAddNewValue; }
        //    set { allowAddNewValue = value; }
        //}

        //void UCCombox_LostFocus(object sender, EventArgs e)
        //{
        //    if (!allowAddNewValue)
        //    {
        //        TextValue = this.SelectedText;
        //    }
        //}


        private void TxtInput_MouseDown(object sender, MouseEventArgs e)
        {
            click_MouseDown(sender, e);
        }

        private void UCComboBox_SizeChanged(object sender, EventArgs e)
        {
            this.txtInput.Location = new Point(this.txtInput.Location.X, (this.Height - txtInput.Height) / 2);
            //this.lblInput.Location = new Point(this.lblInput.Location.X, (this.Height - lblInput.Height) / 2);
        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            TextValue = txtInput.Text;

            //_selectedValue = "";
            //_selectedIndex = -1;
            //_selectedItem = new KeyValuePair<string, string>();

            if (TextChangedEvent != null)
            {
                TextChangedEvent(this, null);
            }
        }


        public ContentAlignment TextAlignment = ContentAlignment.MiddleLeft;


        protected virtual void click_MouseDown(object sender, MouseEventArgs e)
        {
            if (ComboBoxStyle.Simple == BoxStyle || useAsLabel)
            {
                txtInput.Focus();
                return;
            }
            this.Focus();
            if (_frmAnchor == null || _frmAnchor.IsDisposed || !_frmAnchor.Visible)
            {
                if (this.Source != null && this.Source.Count > 0)
                {
                    int intRow = 0;
                    int intCom = 1;
                    var p = this.Parent.PointToScreen(this.Location);
                    intRow = this.Source.Count;

                    var pnl = new WDCombPanel();
                    pnl.DGV.Font = this.Font;
                    int intWidth = this.Width / intCom;
                    Size size = new Size(intCom * intWidth, Math.Min(200, intRow * this.Height + 6));
                    if (_dropPanelWidth > 0)
                    {
                        size.Width = _dropPanelWidth;
                    }
                    if (_dropPanelHeight > 0)
                    {
                        size.Height = _dropPanelHeight;
                    }
                    pnl.Size = size;
                    pnl.DGV.RowTemplate.Height = this.Height;
                    pnl.SelectSourceEvent += ucTime_SelectSourceEvent;
                    pnl.DGV.DataSource = Source.Select(s => new KeyValuePair<string, string>(s.Key, s.Value)).ToList();

                    _frmAnchor = new Forms.FrmAnchor(this, pnl, isNotFocus: false);
                    _frmAnchor.Load += (a, b) => { (a as Form).Size = size; };
                    _frmAnchor.FormClosed += _frmAnchor_FormClosed;
                    ControlHelper.SetDouble(pnl);
                    _frmAnchor.Show(this.FindForm());
                    if (!string.IsNullOrWhiteSpace(_selectedValue))
                        pnl.SetSelect(_selectedValue);

                    pnl.TabStop = true;
                    pnl.DGV.TabStop = true;
                    pnl.DGV.Focus();
                }
            }
            else
            {
                if (_frmAnchor != null && !_frmAnchor.IsDisposed && _frmAnchor.Visible)
                    _frmAnchor.Close();
            }
            if (BoxStyle != ComboBoxStyle.DropDownList)
                txtInput.Focus();
        }

        public event EventHandler PopupFormClosed;
        protected void _frmAnchor_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (PopupFormClosed != null)
                PopupFormClosed(this, null);
        }

        protected Forms.FrmAnchor _frmAnchor;

        protected void CloseFrmAnchor1()
        {
            if (_frmAnchor != null)
            {
                _frmAnchor.Close();
                _frmAnchor = null;
            }
        }

        void ucTime_SelectSourceEvent(object sender, EventArgs e)
        {
            if (_frmAnchor != null && !_frmAnchor.IsDisposed && _frmAnchor.Visible)
            {
                PreSelectedValue = SelectedValue;
                SelectedValue = sender.ToString();
                if (_frmAnchor != null && !_frmAnchor.IsDisposed && _frmAnchor.Visible)
                    _frmAnchor.Close();
            }
        }

        private void UCComboBox_Load(object sender, EventArgs e)
        {
            panel1.BackgroundImage = FontImages.GetImage((FontIcons)Enum.Parse(typeof(FontIcons), "I_arrow_down_fill"), 14, ColorTranslator.FromHtml("#CCCCCC"));
            if (this._BoxStyle == ComboBoxStyle.DropDownList)
            {
                txtInput.BackColor = _BackColor;
                //base.FillColor = _BackColor;
                //base.RectColor = _BackColor;
                base.FillColor = Color.White;
                base.RectColor = Color.FromArgb(220, 220, 220);
            }
            else
            {
                txtInput.BackColor = Color.White;
                base.FillColor = Color.White;
                base.RectColor = Color.FromArgb(220, 220, 220);
            }
            this.txtInput.GotFocus += TxtInput_GotFocus;
            //txtInput.LostFocus += TxtInput_LostFocus;
        }

        private void TxtInput_LostFocus(object sender, EventArgs e)
        {
            if (_frmAnchor != null && !_frmAnchor.IsDisposed && _frmAnchor.Visible)
            {
                _frmAnchor.Close();
                _frmAnchor = null;
            }
        }

        private void TxtInput_GotFocus(object sender, EventArgs e)
        {
            if (this._BoxStyle == ComboBoxStyle.DropDownList)
                HideCaret(((TextBox)sender).Handle);
        }
    }

    public class UCCombox2 : WDCombox
    {
        protected override void click_MouseDown(object sender, MouseEventArgs e)
        {
            if (ComboBoxStyle.Simple == BoxStyle)
            {
                txtInput.Focus();
                return;
            }
            this.Focus();
            SelectedDataRow = null;
            if (_frmAnchor == null || _frmAnchor.IsDisposed || !_frmAnchor.Visible)
            {
                if (this.Source != null && this.Source.Count > 0)
                {
                    int intRow = 0;
                    int intCom = 1;
                    var p = this.Parent.PointToScreen(this.Location);
                    intRow = this.Source.Count;

                    var pnl = new UCCombPanel2();
                    pnl.HasSubData = row =>
                    {
                        var r = (KeyValuePair<string, string>)(row);
                        return this.SubSource.Any(s => s.Item1 == r.Key);
                    };
                    pnl.SetHovePopuSub(true);
                    pnl.DGV.Font = this.Font;
                    int intWidth = this.Width / intCom;
                    Size size = new Size(intCom * intWidth, Math.Min(200, intRow * this.Height + 6));
                    if (_dropPanelWidth > 0)
                    {
                        size.Width = _dropPanelWidth;
                    }
                    if (_dropPanelHeight > 0)
                    {
                        size.Height = _dropPanelHeight;
                    }
                    pnl.Size = size;
                    pnl.DGV.RowTemplate.Height = this.Height;
                    pnl.SelectSourceEvent += openChild;
                    pnl.DGV.DataSource = Source.Select(s => new KeyValuePair<string, string>(s.Key, s.Value)).ToList();

                    _frmAnchor = new Forms.FrmAnchor(this, pnl, isNotFocus: false);
                    _frmAnchor.Load += (a, b) => { (a as Form).Size = size; };
                    _frmAnchor.scrollClose = false;
                    _frmAnchor.KeyDownClose = true;
                    _frmAnchor.FormClosed += (a, b) =>
                    {
                        CloseFrmAnchor2();
                        _frmAnchor_FormClosed(null, null);
                    };
                    _frmAnchor.CloseTick = () =>
                    {
                        IntPtr _cPtr = ControlHelper.GetForegroundWindow();
                        if (_cPtr != _frmAnchor.Handle && _cPtr != this.Handle && (_frmAnchor2 != null && _cPtr != _frmAnchor2.Handle))
                        {
                            _frmAnchor.Close();
                            _frmAnchor = null;
                        }
                    };
                    ControlHelper.SetDouble(pnl);
                    _frmAnchor.Show(this.FindForm());

                    pnl.TabStop = true;
                    pnl.DGV.TabStop = true;
                    pnl.DGV.Focus();

                }
            }
            else
            {
                if (_frmAnchor != null && !_frmAnchor.IsDisposed && _frmAnchor.Visible)
                    _frmAnchor.Close();
            }
            if (BoxStyle != ComboBoxStyle.DropDownList)
                txtInput.Focus();
        }

        public List<Tuple<string, string, string, string>> SubSource
        {
            get
            {
                return _subSource;
            }
            set
            {
                _subSource = value;
                Source = _subSource.Select(l => new KeyValuePair<string, string>(l.Item1, l.Item2)).Distinct().ToList();
            }
        }
        public List<Tuple<string, string, string, string>> _subSource = new List<Tuple<string, string, string, string>>();
        void openChild(object sender, EventArgs e)
        {
            CloseFrmAnchor2();
            int intRow = 0;
            int intCom = 1;
            var p = _frmAnchor.Location;
            var ls = this.SubSource.Where(i => i.Item1 == sender.ToString()).Select(s => new { Value = s.Item4, Row = s }).ToList();
            intRow = ls.Count;
            if (intRow == 0)
                return;

            var pnl = new UCCombPanel2();
            pnl.SetHovePopuSub(false);
            pnl.SelectRowMode = true;
            pnl.DGV.Font = this.Font;
            int intWidth = this.Width;
            Size size = new Size(intCom * intWidth, Math.Min(200, intRow * this.Height + 6));
            if (_dropPanelWidth > 0)
            {
                size.Width = _dropPanelWidth;
            }
            if (_dropPanelHeight > 0)
            {
                size.Height = _dropPanelHeight;
            }
            pnl.Size = size;
            pnl.DGV.RowTemplate.Height = this.Height;
            pnl.SelectSourceEvent += pane2_SelectSourceEvent;
            pnl.DGV.DataSource = ls;

            _frmAnchor2 = new Forms.FrmAnchor(this, pnl, new Point(_frmAnchor.Width, 0), isNotFocus: false);
            _frmAnchor2.scrollClose = false;
            _frmAnchor2.Load += (a, b) =>
            {
                (a as Form).Size = size;
            };
            _frmAnchor2.FormClosed += (a, b) =>
            {
                //CloseFrmAnchor2();
                //_frmAnchor_FormClosed(null, null);
            };
            ControlHelper.SetDouble(pnl);
            _frmAnchor.m_childControl = _frmAnchor;
            _frmAnchor2.Show(this._frmAnchor);

            pnl.TabStop = true;
            pnl.DGV.TabStop = true;
            pnl.DGV.Focus();

        }
        Forms.FrmAnchor _frmAnchor2;

        void CloseFrmAnchor2()
        {
            if (_frmAnchor2 != null)
            {
                _frmAnchor2.Close();
                _frmAnchor2 = null;
            }
        }

        public Tuple<string, string, string, string> SelectedDataRow;
        void pane2_SelectSourceEvent(object sender, EventArgs e)
        {
            if (_frmAnchor2 != null && !_frmAnchor2.IsDisposed && _frmAnchor2.Visible)
            {
                SelectedDataRow = (Tuple<string, string, string, string>)(((dynamic)sender).Row);
                this.txtInput.Text = $"{SelectedDataRow.Item2}/{SelectedDataRow.Item4}";
                CloseFrmAnchor2();
                CloseFrmAnchor1();
            }
        }
    }
}

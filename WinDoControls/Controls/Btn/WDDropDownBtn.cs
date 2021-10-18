














using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WinDoControls.Controls
{





    [DefaultEvent("BtnClick")]
    public partial class WDDropDownBtn : WDBtnImg
    {



        Forms.FrmAnchor _frmAnchor;



        private int _dropPanelHeight = -1;



        public new event EventHandler BtnClick;




        [Description("下拉框高度"), Category("自定义")]
        public int DropPanelHeight
        {
            get { return _dropPanelHeight; }
            set { _dropPanelHeight = value; }
        }

        private int _dropPanelWidth = -1;

        [Description("下拉框宽度"), Category("自定义")]
        public int DropPanelWidth
        {
            get { return _dropPanelWidth; }
            set { _dropPanelWidth = value; }
        }

        private string[] btns;




        [Description("需要显示的按钮文字"), Category("自定义")]
        public string[] Btns
        {
            get { return btns; }
            set { btns = value; }
        }




        [Obsolete("不再可用的属性")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override Image Image
        {
            get;
            set;
        }




        [Obsolete("不再可用的属性")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override ContentAlignment ImageAlign
        {
            get;
            set;
        }




        [Description("按钮字体颜色"), Category("自定义")]
        public override Color BtnForeColor
        {
            get
            {
                return base.BtnForeColor;
            }
            set
            {
                base.BtnForeColor = value;
                var bit = WinDoControls.FontImages.GetImage((WinDoControls.FontIcons)Enum.Parse(typeof(WinDoControls.FontIcons), "E_arrow_triangle_down"), 20, value);
                this.lbl.Image = bit;
            }
        }




        public WDDropDownBtn()
        {
            InitializeComponent();
            IsShowTips = false;
            this.lbl.ImageAlign = ContentAlignment.MiddleRight;
            base.BtnClick += UCDropDownBtn_BtnClick;
        }




        public System.Drawing.ContentAlignment TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

        void UCDropDownBtn_BtnClick(object sender, EventArgs e)
        {
            if (_frmAnchor == null || _frmAnchor.IsDisposed || _frmAnchor.Visible == false)
            {

                if (Btns != null && Btns.Length > 0)
                {
                    int intRow = btns.Length;
                    var p = this.Parent.PointToScreen(this.Location);
                    UCItemPanel ucTime = new UCItemPanel();
                    ucTime.TextAlignment = TextAlignment;
                    ucTime.UseHoverColor = true;
                    ucTime.IsShowBorder = true;
                    int intWidth = this.Width;
                    ucTime.ItemHeight = 40;
                    Size size = new Size(intWidth, (intRow * ucTime.ItemHeight) + 4);
                    ucTime.Size = size;
                    if (_dropPanelWidth > 0)
                    {
                        size.Width = _dropPanelWidth;
                    }
                    ucTime.FirstEvent = true;
                    ucTime.SelectSourceEvent += ucTime_SelectSourceEvent;
                    List<KeyValuePair<string, string>> lst = new List<KeyValuePair<string, string>>();
                    foreach (var item in Btns)
                    {
                        lst.Add(new KeyValuePair<string, string>(item, item));
                    }
                    ucTime.Source = lst;
                    ucTime.Row = intRow;
                    ucTime.Column = 1;


                    var deviation = new Point(this.Width - _dropPanelWidth, 0);
                    _frmAnchor = new Forms.FrmAnchor(this, ucTime, deviation);
                    _frmAnchor.Load += (a, b) => { (a as Form).Size = size; };
                    ControlHelper.SetDouble(ucTime);
                    _frmAnchor.Show(this.FindForm());

                }
            }
            else
            {
                _frmAnchor.Close();
            }
        }

        protected bool _isRightExpand = false;






        void ucTime_SelectSourceEvent(object sender, EventArgs e)
        {
            if (_frmAnchor != null && !_frmAnchor.IsDisposed && _frmAnchor.Visible)
            {

                _frmAnchor.Close();

                if (BtnClick != null)
                {
                    BtnClick(sender.ToString(), e);
                }
            }
        }
    }
}

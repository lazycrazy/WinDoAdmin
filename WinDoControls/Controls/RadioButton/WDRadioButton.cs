using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinDo.Utilities.PublicResource;
using WinDoControls.IconSvg;

namespace WinDoControls.Controls
{

    [DefaultEvent("CheckedChangeEvent")]
    public partial class WDRadioButton : Control
    {



        [Description("选中改变事件"), Category("自定义")]
        public event EventHandler CheckedChangeEvent;


        private string _Text = "单选按钮";




        [Description("文本"), Category("自定义")]
        public string TextValue
        {
            get { return _Text; }
            set
            {
                _Text = value;
                this.Text = _Text;
                this.Invalidate();
            }
        }

        private string _KeyValue = "主键值";

        [Description("主键值"), Category("自定义")]
        public string KeyValue
        {
            get { return _KeyValue; }
            set
            {
                _KeyValue = value;
            }
        }
         
        private bool _checked = false;


        //private Image CheckedIcon = FontImages.GetImage((WD_Controls.FontIcons)Enum.Parse(typeof(WD_Controls.FontIcons), "A_fa_dot_circle_o"), 25, BasisColors.MainColor);


        [Description("是否选中"), Category("自定义")]
        public bool Checked
        {
            get
            {
                return _checked;
            }
            set
            {
                if (_checked != value)
                {
                    _checked = value;


                    SetCheck(value);
                    this.Invalidate();
                    if (CheckedChangeEvent != null)
                    {
                        CheckedChangeEvent(this, null);
                    }
                }
            }
        }




        private string _groupName;





        [Description("分组名称"), Category("自定义")]
        public string GroupName
        {
            get { return _groupName; }
            set { _groupName = value; }
        }











        public WDRadioButton() : base()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.Size = new Size(80, 24);
            this.Font = WDFonts.TextFont;
            this.BackColor = Color.FromArgb(0, 0, 0, 0);
            this.Click += UCRadioButton1_Click;
        }

        private void UCRadioButton1_Click(object sender, EventArgs e)
        {
            this.Focus();
            this.Checked = AllowCancel ? !this.Checked : true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var img = _checked ?
                WinDoControls.Properties.Resources.RadioOn :
                WinDoControls.Properties.Resources.RadioOff;
            var irect = this.ClientRectangle;
            var imgWidth = 20;
            irect.Width = imgWidth;
            e.Graphics.DrawImage(img, irect.GetCenterRangeLocation(img.Size));
            var sf = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
            var rect = this.ClientRectangle;
            rect.Width -= imgWidth;
            rect.Offset(imgWidth + 2, 0);
            using (var brush = new SolidBrush(ForeColor))
                e.Graphics.DrawString(TextValue, this.Font, brush, rect, sf);
        }

        public bool UseDisable = false;
        private void SetCheck(bool bln)
        {
            if (!bln)
                return;
            if (this.Parent != null)
            {
                foreach (Control c in this.Parent.Controls)
                {
                    if (c is WDRadioButton && c != this)
                    {
                        WDRadioButton uc = (WDRadioButton)c;
                        if (UseDisable)
                        {
                            if (_groupName == uc.GroupName && uc.Checked)
                            {
                                uc.Checked = false;
                                return;
                            }
                        }
                        else
                        {
                            if (_groupName == uc.GroupName && uc.Checked && uc.Enabled)
                            {
                                uc.Checked = false;
                                return;
                            }
                        }
                    }
                }
            }
        }



        bool _allowCancel = true;
        [Description("允许取消"), Category("自定义"), DefaultValue(true)]
        public bool AllowCancel { get { return _allowCancel; } set { _allowCancel = value; } }

        private void Radio_MouseDown(object sender, MouseEventArgs e)
        {
            this.Focus();
            this.Checked = AllowCancel ? !this.Checked : true;
        }






        private void UCRadioButton_Load(object sender, EventArgs e)
        {

            if (this.Parent != null && this._checked)
            {
                foreach (Control c in this.Parent.Controls)
                {
                    if (c is WDRadioButton && c != this)
                    {
                        WDRadioButton uc = (WDRadioButton)c;
                        if (_groupName == uc.GroupName && uc.Checked && uc.Enabled)
                        {
                            Checked = false;
                            return;
                        }
                    }
                }
            }
        }
    }
}

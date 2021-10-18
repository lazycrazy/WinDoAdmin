using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinDo.Utilities.PublicResource;

namespace WinDoControls.Controls
{






    [DefaultEvent("CheckedChangeEvent")]

    public partial class WDCheckBox : Control
    {



        [Description("选中改变事件"), Category("自定义")]
        public event EventHandler CheckedChangeEvent;

         

        private Color _ForeColor = Color.FromArgb(62, 62, 62);









        private string _Text = "复选框";




        [Description("文本"), Category("自定义")]
        public string TextValue
        {
            get { return _Text; }
            set
            {
                _Text = value;
                Text = _Text;
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

        public void SetChecked(bool ischecked)
        {
            _checked = ischecked;
            Invalidate();
        }


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
                    Invalidate();

                    if (CheckedChangeEvent != null)
                    {
                        CheckedChangeEvent(this, null);
                    }
                }
            }
        }



        public WDCheckBox() : base()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.Size = new Size(70, 24);
            this.Font = WDFonts.TextFont;
            this.BackColor = Color.FromArgb(0, 0, 0, 0);
            this.Click += UCCheckBox_Click;
        }

        private void UCCheckBox_Click(object sender, EventArgs e)
        {
            Checked = !Checked;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var img = _checked ?
                WinDoControls.Properties.Resources.CheckedBox
                : WinDoControls.Properties.Resources.UnCheckedBox;
            var irect = this.ClientRectangle;// e.ClipRectangle;
            irect.Width = img.Width;
            e.Graphics.DrawImage(img, irect.GetCenterRangeLocation(img.Size));
            var sf = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
            var rect = this.ClientRectangle;
            rect.Width -= img.Width;
            rect.Offset(img.Width + 2, 0);
            using (var brush = new SolidBrush(ForeColor))
                e.Graphics.DrawString(TextValue, this.Font, brush, rect, sf);
        }
    }



    [DefaultEvent("CheckedChangeEvent")]

    public partial class UCCheckBoxC : Control
    {
        [Description("选中改变事件"), Category("自定义")]
        public event EventHandler CheckedChangeEvent;

        private bool _checked = false;

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

                    var img = _checked ? IconSvg.SVGIcons.Icon(IconSvg.IconNames.check_square_fill, WDColors.Green6) : IconSvg.SVGIcons.Icon(IconSvg.IconNames.square);
                    this.BackgroundImage = img;
                    Invalidate();

                    if (CheckedChangeEvent != null)
                    {
                        CheckedChangeEvent(this, null);
                    }
                }
            }
        }

        public UCCheckBoxC() : base()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.Size = new Size(20, 20);
            var img = IconSvg.SVGIcons.Icon(IconSvg.IconNames.square, WDColors.Green6);
            this.BackgroundImage = img;
            this.BackgroundImageLayout = ImageLayout.Zoom;
            this.Font = WDFonts.TextFont;
            this.BackColor = Color.FromArgb(0, 0, 0, 0);
            this.Click += UCCheckBox_Click;
        }

        private void UCCheckBox_Click(object sender, EventArgs e)
        {
            Checked = !Checked;
        }
    }
}

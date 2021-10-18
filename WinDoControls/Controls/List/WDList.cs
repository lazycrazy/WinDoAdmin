using WinDo.Utilities;
using WinDo.Utilities.PublicResource;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinDoControls;
using WinDoControls.Forms;

namespace WinDoControls.Controls
{
    /// <summary>
    ///  this.AwesomeList.Items = Enumerable.Range(1, 10).Select(x => new AwesomeListItem() { Text = string.Concat(Enumerable.Repeat(x.ToString(), 10)) }).ToList();
    ///  this.AwesomeList.ItemClick += UcListV1_ItemClick;
    /// </summary>
    public class WDList : System.Windows.Forms.Control
    {

        public static List<Image> Icons = new List<Image> {
            WDImages.GetBtnIconImage("I_exclamation_cycle",color:WDColors.ErrorRedColor),
            WDImages.GetBtnIconImage("I_success_fill",color:ColorTranslator.FromHtml("#04de01")),
            WDImages.GetBtnIconImage("I_NA",20, color:WDColors.OrangeColor),

            WDImages.GetBtnIconImage("I_exclamation_cycle",color:Color.White),
            WDImages.GetBtnIconImage("I_success_fill",color:Color.White),
            WDImages.GetBtnIconImage("I_NA",20, color:Color.White),

            WDImages.GetBtnIconImage("I_linked",color:WDColors.geekblue6),
            WDImages.GetBtnIconImage("I_linked",color:Color.White),
            WDImages.GetBtnIconImage("I_clear_link",color:WDColors.geekblue6),
            WDImages.GetBtnIconImage("I_clear_link",color:Color.White),

            WDImages.GetBtnIconImage("I_edit_bold",color:Color.White),
            WDImages.GetBtnIconImage("I_success",30,color:WDColors.StatusBlue),
            WDImages.GetBtnIconImage("I_commit",color:Color.White),
            WDImages.GetBtnIconImage("I_my",14,color:WDColors.geekblue6),
            WDImages.GetBtnIconImage("I_my",14,color:Color.White),
            WDImages.GetBtnIconImage("I_mygroup",14,color:WDColors.geekblue6),
            WDImages.GetBtnIconImage("I_mygroup",14,color:Color.White),


            WDImages.GetBtnIconImage("I_ertong",20,WDColors.ErrorRedColor),

        };

        public static Image ImageError = Icons[0];
        public static Image ImageSuccess = Icons[1];
        public static Image ImageNA = Icons[2];
        public static Image ImageErrorWhite = Icons[3];
        public static Image ImageSuccessWhite = Icons[4];
        public static Image ImageNAWhite = Icons[5];

        public static Image ImagLinked = Icons[6];
        public static Image ImagLinkedWhite = Icons[7];
        public static Image ImagClearLink = Icons[8];
        public static Image ImagClearLinkWhite = Icons[9];

        public static Image ImageEdit = Icons[10];
        public static Image ImageSuccessLarge = Icons[11];
        public static Image ImageCommit = Icons[12];
        public static Size BtnSize = new Size(74, 30);

        public static Image ImageMy = Icons[13];
        public static Image ImageMyWhite = Icons[14];
        public static Image ImageMyGroup = Icons[15];
        public static Image ImageMyGroupWhite = Icons[16];

        public static Image ImageErTong = Icons[17];

        public static StringFormat StringFormatTopCenter = new StringFormat() { FormatFlags = StringFormatFlags.NoWrap, Trimming = StringTrimming.EllipsisCharacter, Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        public static StringFormat StringFormatCenter = new StringFormat() { FormatFlags = StringFormatFlags.NoWrap, Trimming = StringTrimming.EllipsisCharacter, Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        public static StringFormat StringFormatLeft = new StringFormat() { FormatFlags = StringFormatFlags.NoWrap, Trimming = StringTrimming.EllipsisCharacter, Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
        public static StringFormat StringFormatRight = new StringFormat() { FormatFlags = StringFormatFlags.NoWrap, Trimming = StringTrimming.EllipsisCharacter, Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };

        public event ListVItemClick ItemClick;

        private WinDoListItem currentItem;
        public WinDoListItem CurrentItem
        {
            get { return currentItem; }
            set
            {
                currentItem = value;
                this.OnItemClick(currentItem, null);
                this.Invalidate();
            }
        }
        public WinDoListItem HoveredItem;
        public int MaxWidth = 0;
        public int FixItemWidth = 0;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        /// <summary>
        /// 项目列表
        /// </summary>
        public List<WinDoListItem> Items
        {
            get { return _Items; }
            set
            {
                _Items = value;

                this.Invalidate();
            }
        }

        protected int _ItemHeight = 30;
        /// <summary>
        /// 项目高度
        /// </summary>
        public int ItemHeight
        {
            get { return _ItemHeight; }
            set
            {
                _ItemHeight = value;
                this.Invalidate();
            }
        }
        public bool AutoWrap = false;
        public bool NeedChangeCursor = true;
        private bool highLightCurrentItem = false;

        public bool HighLightCurrentItem
        {
            get { return highLightCurrentItem; }
            set
            {
                highLightCurrentItem = value;
                this.Invalidate();
            }
        }
        public int LeftOffset = 0;
        public int TopOffset = 0;
        public int LeftRightSpace = 20;
        public WDList()
        {
            ControlHelper.SetDouble(this);
            this.BackColor = Color.LightBlue;
            this.Size = new Size(100, 30);
            //this.Font = WinDo.Utilities.PublicResource.YkdTextFonts.TextFont;
            this.MouseClick += UCListV_MouseClick;
            this.MouseMove += UCListV_MouseMove;
            this.MouseLeave += UCListV_MouseLeave;
        }
        FrmAnchorTips _frmAnchorTips = null;

        void CloseOverLengthTips()
        {
            if (_frmAnchorTips != null)
            {
                _frmAnchorTips.Close();
                _frmAnchorTips = null;
            }
        }
        private void UCListV_MouseLeave(object sender, EventArgs e)
        {
            CloseOverLengthTips();
        }

        private void UCListV_MouseMove(object sender, MouseEventArgs e)
        {
            if (Items == null)
            {
                CurrentItem = null;
                HoveredItem = null;
                CloseOverLengthTips();
                return;
            }
            var item = Items.FirstOrDefault(i => i.ClientRectangle.Contains(e.Location));
            if (item == null)
            {
                this.Cursor = Cursors.Default;
                HoveredItem = null;
                CloseOverLengthTips();
                return;
            }
            else
            {
                if (NeedChangeCursor)
                    this.Cursor = Cursors.Hand;
                HoveredItem = item;
                if (!string.IsNullOrWhiteSpace(HoveredItem.OverLengthTips))
                {
                    var overRect = HoveredItem.ClientRectangle;
                    overRect.Offset(this.Parent.PointToScreen(this.Location));
                    if (_frmAnchorTips != null && _frmAnchorTips.RectControl != overRect)
                    {
                        CloseOverLengthTips();
                    }
                    var tips = string.Join<string>("\r\n", StringHelper.Split(HoveredItem.OverLengthTips, 50));
                    if (_frmAnchorTips == null)
                        _frmAnchorTips = FrmAnchorTips.ShowTips(overRect, tips, AnchorTipsLocation.BOTTOM, WDColors.TaskListTip, autoCloseTime: 6000);
                }
            }
            this.Invalidate();
        }

        public void OnItemClick(WinDoListItem item, MouseEventArgs e)
        {
            if (item == null) return;
            if (ItemClick != null)
                ItemClick(item, e);
        }
        private void UCListV_MouseClick(object sender, MouseEventArgs e)
        {
            if (Items == null)
            {
                currentItem = null;
                HoveredItem = null;
                return;
            }
            var item = Items.FirstOrDefault(i => i.ClientRectangle.Contains(e.Location));
            if (item == null) return;
            CurrentItem = item;
            this.Invalidate();
        }




        public List<WinDoListItem> _Items = new List<WinDoListItem>();

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_Items == null || _Items.Count == 0)
            {
                base.OnPaint(e);
                return;
            }
            var g = e.Graphics;
            g.SetGDIHigh();
            RepositionItems(g);
            DefaultDrawItems(g);
            base.OnPaint(e);
        }

        protected void DefaultAdjustItemRect(ref Rectangle rect, WinDoListItem item)
        {
            if (FixItemWidth > 0)
            {
                rect.Width = FixItemWidth;
                return;
            }
            rect.Width += LeftRightSpace;
            if (AdjustItemRect != null)
            {
                AdjustItemRect(ref rect, item);
                return;
            }
            return;
        }

        protected void DefaultDrawItems(Graphics g)
        {
            if (DrawItems != null)
            {
                DrawItems(g, this);
                return;
            }
            var format = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            foreach (var item in this.Items)
            {
                if (item == CurrentItem)
                {
                    g.DrawRectangle(Pens.Blue, item.ClientRectangle);
                    g.FillRectangle(Brushes.Blue, item.ClientRectangle);
                    g.DrawString(item.Text, this.Font, Brushes.White, item.ClientRectangle, format);
                }
                else
                {
                    g.DrawRectangle(Pens.Black, item.ClientRectangle);
                    g.DrawString(item.Text, this.Font, Brushes.Red, item.ClientRectangle, format);
                }
            }
        }


        public RefAction<Rectangle, WinDoListItem> AdjustItemRect;
        public Action<Graphics, WDList> DrawItems;

        protected virtual void RepositionItems(Graphics g)
        {
            int xoffset = LeftOffset;
            int yoffset = TopOffset;
            var maxWidth = this.MaxWidth > 0 ? this.MaxWidth : this.Width;
            foreach (var item in this.Items)
            {
                string text = item.Text;
                if (string.IsNullOrEmpty(text))
                    text = " ";
                SizeF textSize = g.MeasureString(text, this.Font);
                int width = (int)Math.Ceiling(textSize.Width);
                int height = _ItemHeight;
                Rectangle rect = new Rectangle(xoffset, yoffset, width, height);

                DefaultAdjustItemRect(ref rect, item);

                xoffset += rect.Width;
                if (AutoWrap && xoffset > maxWidth + LeftOffset)
                {
                    xoffset = LeftOffset + rect.Width;
                    yoffset += _ItemHeight;
                    rect.X = LeftOffset;
                    rect.Y = yoffset;
                }
                item.ClientRectangle = rect;
            }
        }
    }

    /// <summary>
    /// 比对明细左边列表
    /// </summary>
    public class WinDoList_1 : WDList
    {


        public List<string> Labels = new List<string>
        {
            "QQQQQQ",
            "KKKKKK",
            "AAAAAA",
        };
        public WinDoList_1()
        {
            base.AutoWrap = true;
            base.BackColor = WDColors.SelectedBackColor;
            base.ItemHeight = 40;
            base.Font = WinDo.Utilities.PublicResource.WDFonts.TextFontBold;
            base.AdjustItemRect = (ref Rectangle rect, WinDoListItem item) =>
            {
                rect.Width = this.Width;
            };
            base.Items = Labels.Select((l, i) => new WinDoListItem() { Text = l }).ToList();
            base.DrawItems = DrawLeftList;
        }

        private void DrawLeftList(Graphics g, WDList sender)
        {
            var format = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
            foreach (var item in sender.Items)
            {
                var rect = item.ClientRectangle;
                if (item == sender.CurrentItem)
                {
                    rect.Height -= 2;
                    using (var brush = new SolidBrush(WinDo.Utilities.PublicResource.WDColors.geekblue6))
                        g.FillRectangle(brush, rect);
                    //if (base.HoveredItem == item)
                    //{
                    //    using (var brush = new SolidBrush(Color.FromArgb(10, WinDo.Utilities.PublicResource.YkdBasisColors.MainColor)))
                    //        g.FillRectangle(brush, rect);
                    //}
                    if (item.CurRightIcon != null)
                    {
                        var rRect = rect;
                        rRect.Offset(rRect.Width - 10 - item.CurRightIcon.Width, (rRect.Height - item.CurRightIcon.Height) / 2);
                        g.DrawImage(item.CurRightIcon, rRect.Location);
                    }
                    rect.Height += 2;
                    rect.Offset(10, 0);
                    g.DrawString(item.Text, sender.Font, Brushes.White, rect, format);
                }
                else
                {
                    rect.Height -= 1;
                    g.FillRectangle(Brushes.White, rect);
                    //if (base.HoveredItem == item)
                    //{
                    //    using (var brush = new SolidBrush(Color.FromArgb(10, Color.White)))
                    //        g.FillRectangle(brush, rect);
                    //}
                    if (item.RightIcon != null)
                    {
                        var rRect = rect;
                        rRect.Offset(rRect.Width - 10 - item.RightIcon.Width, (rRect.Height - item.RightIcon.Height) / 2);
                        g.DrawImage(item.RightIcon, rRect.Location);
                    }
                    rect.Height += 1;
                    rect.Offset(10, 0);
                    using (var brush = new SolidBrush(WinDo.Utilities.PublicResource.WDColors.geekblue6))
                        g.DrawString(item.Text, sender.Font, brush, rect, format);
                }
            }
        }

    }


    /// <summary>
    /// 处方比对 顶部列表
    /// </summary>
    public class WinDoList_3 : WDList
    {
        public List<string> Labels = new List<string>
        {
        };
        public WinDoList_3()
        {
            base.NeedChangeCursor = false;
            base.Size = new Size(140, 20);
            base.BackColor = Color.White;
            base.ItemHeight = 20;
            base.Font = WinDo.Utilities.PublicResource.WDFonts.TextFontBold;
            base.AdjustItemRect = (ref Rectangle rect, WinDoListItem item) =>
            {
                if (item.RightIcon != null)
                    rect.Width += item.RightIcon.Width + 5;
            };
            base.Items = Labels.Select((l, i) => new WinDoListItem() { Text = l, RightIcon = WinDoList_1.Icons[i % 3], CurRightIcon = WinDoList_1.Icons[i % 3] }).ToList();
            base.DrawItems = DrawItem;
        }

        private void DrawItem(Graphics g, WDList sender)
        {
            var format = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
            foreach (var item in sender.Items)
            {
                var rect = item.ClientRectangle;
                g.FillRectangle(Brushes.White, rect);
                if (item.RightIcon != null)
                {
                    var rRect = rect;
                    rRect.Offset(rRect.Width - 5 - item.RightIcon.Width, (rRect.Height - item.RightIcon.Height) / 2);
                    g.DrawImage(item.RightIcon, rRect.Location);
                }

                using (var brush = new SolidBrush(WDColors.StatusBlue))
                {
                    var lRect = rect;
                    lRect.Width = 5;
                    lRect.Height = 15;
                    lRect.Offset(0, (rect.Height - lRect.Height) / 2);
                    g.FillRectangle(brush, lRect);
                    rect.Offset(10, 0);
                    g.DrawString(item.Text, WinDo.Utilities.PublicResource.WDFonts.TextFontBold, brush, rect, format);
                }
            }
        }
    }

    /// <summary>
    /// 带边框，可选择列表
    /// </summary>
    public class WinDoList_4 : WDList
    {
        public List<string> Labels = new List<string>
        {
        };
        public WinDoList_4()
        {
            base.LeftOffset = 1;
            base.TopOffset = 1;
            base.Size = new Size(340, 40);
            base.BackColor = Color.White;
            base.ForeColor = Color.Black;
            base.ItemHeight = 40;
            base.Font = WinDo.Utilities.PublicResource.WDFonts.TextFontBold;
            base.AdjustItemRect = (ref Rectangle rect, WinDoListItem item) =>
            {
                if (item.RightIcon != null)
                    rect.Width += item.RightIcon.Width + 5;
            };
            base.Items = Labels.Select((l, i) => new WinDoListItem() { Text = l, RightIcon = WinDoList_1.Icons[i % 3], CurRightIcon = WinDoList_1.Icons[i % 3] }).ToList();
            base.DrawItems = DrawItem;
        }

        private void DrawItem(Graphics g, WDList sender)
        {
            int borderWidth = 2;
            var mainColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
            var format = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            foreach (var item in sender.Items)
            {
                var rect = item.ClientRectangle;
                rect.Height -= borderWidth;
                using (var pen = new Pen(mainColor, borderWidth))
                {
                    g.DrawRectangle(pen, rect);
                }
                if (item == sender.CurrentItem)
                {
                    using (var brush = new SolidBrush(mainColor))
                        g.FillRectangle(brush, rect);
                    if (item.CurRightIcon != null)
                    {
                        var rRect = rect;
                        rRect.Offset(rRect.Width - item.CurRightIcon.Width - 5, (rRect.Height - item.CurRightIcon.Height) / 2);
                        g.DrawImage(item.CurRightIcon, rRect.Location);
                        rect.Width -= item.CurRightIcon.Width - 5;
                    }
                    g.DrawString(item.Text, this.Font, Brushes.White, rect, format);
                }
                else
                {
                    if (item.RightIcon != null)
                    {
                        var rRect = rect;
                        rRect.Offset(rRect.Width - item.RightIcon.Width - 5, (rRect.Height - item.RightIcon.Height) / 2);
                        g.DrawImage(item.RightIcon, rRect.Location);
                        rect.Width -= item.RightIcon.Width - 5;
                    }
                    using (var brush = new SolidBrush(this.ForeColor))
                        g.DrawString(item.Text, this.Font, brush, rect, format);
                }
            }
        }
    }


    /// <summary>
    /// 带边框，可选择列表
    /// </summary>
    public class WinDoList_5 : WDList
    {
        public List<string> Labels = new List<string>
        {
        };
        public WinDoList_5()
        {
            base.AutoWrap = true;
            base.LeftOffset = 1;
            base.TopOffset = 1;
            base.Size = new Size(170, 230);
            base.BackColor = Color.White;
            base.ItemHeight = 40;
            base.Font = WinDo.Utilities.PublicResource.WDFonts.TextFontBold;
            base.AdjustItemRect = (ref Rectangle rect, WinDoListItem item) =>
            {
                rect.Width = this.Width;
            };
            base.Items = Labels.Select((l, i) => new WinDoListItem() { Text = l, RightIcon = WinDoList_1.Icons[i % 3], CurRightIcon = WinDoList_1.Icons[i % 3] }).ToList();
            base.DrawItems = DrawItem;
        }

        private void DrawItem(Graphics g, WDList sender)
        {
            int borderWidth = 2;
            var mainColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
            var format = new StringFormat() { FormatFlags = StringFormatFlags.NoWrap, Trimming = StringTrimming.EllipsisCharacter, Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
            foreach (var item in sender.Items)
            {
                var rect = item.ClientRectangle;
                rect.Width -= borderWidth;
                using (var pen = new Pen(mainColor, borderWidth))
                {
                    g.DrawRectangle(pen, rect);
                }
                if (item == sender.CurrentItem)
                {
                    using (var brush = new SolidBrush(mainColor))
                        g.FillRectangle(brush, rect);
                    if (item.CurRightIcon != null)
                    {
                        var rRect = rect;
                        rRect.Offset(rRect.Width - item.CurRightIcon.Width - 5, (rRect.Height - item.CurRightIcon.Height) / 2);
                        g.DrawImage(item.CurRightIcon, rRect.Location);
                        rect.Width -= item.CurRightIcon.Width;
                    }
                    rect.Offset(10, 0);
                    rect.Width -= 10;
                    g.DrawString(item.Text, this.Font, Brushes.White, rect, format);
                    if (g.MeasureString(item.Text, this.Font).Width > rect.Width)
                    {
                        item.OverLengthTips = item.Text;
                    }
                }
                else
                {
                    if (item.RightIcon != null)
                    {
                        var rRect = rect;
                        rRect.Offset(rRect.Width - item.RightIcon.Width - 5, (rRect.Height - item.RightIcon.Height) / 2);
                        g.DrawImage(item.RightIcon, rRect.Location);
                        rect.Width -= item.CurRightIcon.Width;
                    }
                    rect.Offset(10, 0);
                    rect.Width -= 10;
                    g.DrawString(item.Text, this.Font, Brushes.Black, rect, format);
                    if (g.MeasureString(item.Text, this.Font).Width > rect.Width)
                    {
                        item.OverLengthTips = item.Text;
                    }
                }
            }
        }
    }




    /// <summary>
    /// 签字结果顶部列表
    /// </summary>
    public class WinDoList_6 : WDList
    {
        public List<string> Labels = new List<string>
        {
            "    "
        };
        public WinDoList_6()
        {
            base.Size = new Size(140, 20);
            base.BackColor = WDColors.SelectedBackColor;
            base.ItemHeight = 40;
            base.Font = WinDo.Utilities.PublicResource.WDFonts.TextFontBold;
            base.AdjustItemRect = (ref Rectangle rect, WinDoListItem item) =>
            {
                if (item.RightIcon != null)
                    rect.Width += item.RightIcon.Width + 5;
            };
            base.Items = Labels.Select((l, i) => new WinDoListItem() { Text = l, RightIcon = WinDoList_1.Icons[i % 3], CurRightIcon = WinDoList_1.Icons[i % 3] }).ToList();
            base.DrawItems = DrawItem;
        }

        private void DrawItem(Graphics g, WDList sender)
        {
            var format = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
            foreach (var item in sender.Items)
            {
                var rect = item.ClientRectangle;
                if (CurrentItem == item)
                    g.FillRectangle(Brushes.White, rect);
                if (item.RightIcon != null)
                {
                    var rRect = rect;
                    rRect.Offset(rRect.Width - 5 - item.RightIcon.Width, (rRect.Height - item.RightIcon.Height) / 2);
                    g.DrawImage(item.RightIcon, rRect.Location);
                }
                var color = (CurrentItem == item) ? WDColors.StatusBlue : WDColors.GrayTextColor;

                using (var brush = new SolidBrush(color))
                {
                    var lRect = rect;
                    lRect.Width = 5;
                    lRect.Height = 15;
                    lRect.Offset(10, (rect.Height - lRect.Height) / 2);
                    g.FillRectangle(brush, lRect);
                    rect.Offset(20, 0);
                    g.DrawString(item.Text, sender.Font, brush, rect, format);
                }
            }
        }
    }
    public class WinDoList_ABC : WDList
    {
        public WinDoList_ABC()
        {
            base.Size = new Size(140, 20);
            base.BackColor = WDColors.SelectedBackColor;
            base.ItemHeight = 40;
            base.Font = WinDo.Utilities.PublicResource.WDFonts.TextFont;
            base.AdjustItemRect = (ref Rectangle rect, WinDoListItem item) =>
            {
                rect.Width += 10;
            };
            this.Items = new List<WinDoListItem>() {
                new WinDoListItem() { Text = "图像引导" } ,
                new WinDoListItem() { Text = "射野明细" } ,
                new WinDoListItem() { Text = "Break Point" } ,
                };
            this.CurrentItem = this.Items[0];
            base.DrawItems = DrawItem;
        }

        private void DrawItem(Graphics g, WDList sender)
        {
            var format = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
            foreach (var item in sender.Items)
            {
                var rect = item.ClientRectangle;
                if (CurrentItem == item)
                    g.FillRectangle(Brushes.White, rect);
                var color = (CurrentItem == item) ? WDColors.StatusBlue : WDColors.GrayTextColor;

                using (var brush = new SolidBrush(color))
                {
                    var lRect = rect;
                    lRect.Width = 5;
                    lRect.Height = 15;
                    lRect.Offset(15, (rect.Height - lRect.Height) / 2);
                    g.FillRectangle(brush, lRect);
                    rect.Offset(25, 0);
                    g.DrawString(item.Text, sender.Font, brush, rect, format);
                }
            }
        }
    }


    public class UCColorSelect : System.Windows.Forms.Control
    {
        /// <summary>
        /// 数据源
        /// </summary>
        public List<KeyValuePair<string, object>> DataSource
        {
            get { return dataSource; }
            set
            {
                dataSource = value;
            }
        }

        private KeyValuePair<string, object> currentItem;
        public KeyValuePair<string, object> CurrentItem
        {
            get { return currentItem; }
            set
            {
                currentItem = value;
                this.Invalidate();
            }
        }

        public UCColorSelect()
        {
            this.BackColor = Color.White;
            this.MouseClick += UCColorSelect_MouseClick;

        }
        FrmAnchor _frmAnchor;


        private List<KeyValuePair<string, object>> dataSource = new List<KeyValuePair<string, object>>();


        protected override void OnPaint(PaintEventArgs e)
        {
            var brect = this.ClientRectangle;
            brect.Width -= 2;
            brect.Height -= 2;
            var g = e.Graphics;
            g.SetGDIHigh();
            using (var pen = new Pen(WDColors.GrayRectColor))
                ControlHelper.DrawRoundRectangle(g, pen, brect, 2);
            if (!string.IsNullOrWhiteSpace(currentItem.Key))
            {
                var rect = e.ClipRectangle;
                rect.Offset(3, 3);
                rect.Width -= 8;
                rect.Height -= 8;
                using (var brush = new SolidBrush(ColorTranslator.FromHtml(currentItem.Key)))
                    ControlHelper.FillRoundRectangle(g, brush, rect, 2);
            }

        }

        private void UCColorSelect_MouseClick(object sender, MouseEventArgs e)
        {
            this.Focus();
            if (_frmAnchor == null || _frmAnchor.IsDisposed || !_frmAnchor.Visible)
            {
                if (this.dataSource != null && this.dataSource.Count > 0)
                {
                    var vlist = new WDList();
                    vlist.AutoWrap = true;
                    vlist.Size = new Size(this.Width, 150);
                    vlist.BackColor = Color.White;
                    vlist.MaxWidth = this.Width;
                    vlist.FixItemWidth = 50;
                    vlist.Paint += (object ss, PaintEventArgs ee) =>
                    {
                        var brect = vlist.ClientRectangle;
                        brect.Width -= 2;
                        brect.Height -= 2;
                        using (var pen = new Pen(WDColors.GrayRectColor))
                            ControlHelper.DrawRoundRectangle(ee.Graphics, pen, brect, 2);
                    };
                    vlist.DrawItems += (Graphics g, WDList s) =>
                       {
                           foreach (var item in s.Items)
                           {
                               var rect = item.ClientRectangle;
                               if (item == s.CurrentItem)
                               {
                                   rect.Size = new Size(30, 30);
                                   rect.Offset(item.ClientRectangle.GetCenterRangeLocation(rect.Size));
                                   using (var brush = new SolidBrush(WDColors.geekblue6))
                                       g.FillRectangle(brush, rect);
                                   var frect = item.ClientRectangle;
                                   frect.Size = new Size(20, 20);
                                   frect.Offset(item.ClientRectangle.GetCenterRangeLocation(frect.Size));
                                   using (var brush = new SolidBrush(ColorTranslator.FromHtml(item.Text)))
                                       g.FillRectangle(brush, frect);
                               }
                               else
                               {
                                   rect.Size = new Size(20, 20);
                                   rect.Offset(item.ClientRectangle.GetCenterRangeLocation(rect.Size));
                                   using (var brush = new SolidBrush(ColorTranslator.FromHtml(item.Text)))
                                       g.FillRectangle(brush, rect);
                               }
                           }
                       };
                    vlist.ItemClick += (WinDoListItem item, MouseEventArgs ee) =>
                    {
                        if (item == null) return;
                        this.CurrentItem = item.Data;

                        if (!(_frmAnchor == null || _frmAnchor.IsDisposed || !_frmAnchor.Visible))
                            _frmAnchor.Close();

                    };
                    if (dataSource != null)
                        vlist.Items = dataSource.Select(i => new WinDoListItem() { Text = i.Key, Data = i }).ToList();
                    if (!string.IsNullOrWhiteSpace(currentItem.Key))
                    {
                        vlist.CurrentItem = vlist.Items.FirstOrDefault(i => i.Text == currentItem.Key);
                    }

                    _frmAnchor = new FrmAnchor(this, vlist);
                    _frmAnchor.Load += (a, b) => { (a as Form).Size = vlist.Size; };

                    _frmAnchor.Show(this.FindForm());
                }
            }
            else
            {
                _frmAnchor.Close();
            }
        }
    }

    /// <summary>
    /// 我的和我组
    /// </summary>
    public class UCMyGroupTab : WDList
    {
        public List<string> Labels = new List<string>
        {
            "我的",
            "我组",
        };
        public UCMyGroupTab()
        {
            base.LeftOffset = 1;
            base.TopOffset = 1;
            base.Size = new Size(186, 34);
            base.BackColor = Color.White;
            base.ItemHeight = 34;
            base.Font = WinDo.Utilities.PublicResource.WDFonts.TextFontBold;
            base.AdjustItemRect = (ref Rectangle rect, WinDoListItem item) =>
            {
                rect.Width = 92;
            };
            var ls = new[] {
            new WinDoListItem() { Text = "我的", LeftIcon = WDList.ImageMy, CurLeftIcon = WDList.ImageMyWhite } ,
            new WinDoListItem() { Text = "我组", LeftIcon = WDList.ImageMyGroup, CurLeftIcon = WDList.ImageMyGroupWhite }};
            base.Items = ls.ToList();
            base.DrawItems = DrawItem;
        }

        private void DrawItem(Graphics g, WDList sender)
        {
            int borderWidth = 2;
            var mainColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
            var format = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
            foreach (var item in sender.Items)
            {
                var rect = item.ClientRectangle;
                rect.Height -= borderWidth;
                using (var pen = new Pen(mainColor, borderWidth))
                {
                    g.DrawRectangle(pen, rect);
                }
                if (item == sender.CurrentItem)
                {
                    using (var brush = new SolidBrush(mainColor))
                        g.FillRectangle(brush, rect);
                    var all_width = g.MeasureString(item.Text, Font).Width + item.CurLeftIcon.Width;
                    var lRect = rect;
                    lRect.Offset(((lRect.Width - all_width) / 2).ToInt(), ((lRect.Height - item.CurLeftIcon.Height) / 2).ToInt());
                    g.DrawImage(item.CurLeftIcon, lRect.Location);
                    lRect.X += item.CurLeftIcon.Width;
                    lRect.Y = rect.Y;
                    g.DrawString(item.Text, this.Font, Brushes.White, lRect, format);
                }
                else
                {
                    var all_width = g.MeasureString(item.Text, Font).Width + item.LeftIcon.Width;
                    var lRect = rect;
                    lRect.Offset(((lRect.Width - all_width) / 2).ToInt(), ((lRect.Height - item.LeftIcon.Height) / 2).ToInt());
                    g.DrawImage(item.LeftIcon, lRect.Location);
                    lRect.X += item.LeftIcon.Width;
                    lRect.Y = rect.Y;
                    using (var bursh = new SolidBrush(mainColor))
                        g.DrawString(item.Text, this.Font, bursh, lRect, format);
                }
            }
        }
    }


    /// <summary>
    /// 带边框和Tip
    /// </summary>
    public class UCWithBorderAndTipTab : WDList
    {
        public List<string> Labels = new List<string>
        {
            "未完成",
            "已完成",
            "全部",
        };
        public UCWithBorderAndTipTab()
        {
            base.LeftOffset = 1;
            base.TopOffset = 1;
            base.Size = new Size(186, 34);
            base.BackColor = Color.White;
            base.ItemHeight = 34;
            base.Font = WinDo.Utilities.PublicResource.WDFonts.TextFontBold;
            base.AdjustItemRect = (ref Rectangle rect, WinDoListItem item) =>
            {
                if (!string.IsNullOrWhiteSpace(item.TipText))
                {
                    var tipWidth = TextRenderer.MeasureText(item.TipText, this.Font).Width;
                    rect.Width += tipWidth;
                }
            };
            var ls = Labels.Select(l => new WinDoListItem() { Text = l });
            base.Items = ls.ToList();
            base.DrawItems = DrawItem;
        }

        private void DrawItem(Graphics g, WDList sender)
        {
            int borderWidth = 2;
            var mainColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
            foreach (var item in sender.Items)
            {
                var rect = item.ClientRectangle;
                rect.Height -= borderWidth;
                var tipWidth = 0;
                if (!string.IsNullOrWhiteSpace(item.TipText))
                    tipWidth = (int)g.MeasureString(item.TipText, Font).Width;
                using (var pen = new Pen(mainColor, borderWidth))
                {
                    g.DrawRectangle(pen, rect);
                }
                if (item == sender.CurrentItem)
                {
                    using (var brush = new SolidBrush(mainColor))
                        g.FillRectangle(brush, rect);
                    var lRect = rect;
                    if (tipWidth > 0)
                        lRect.Width -= tipWidth + 8;
                    g.DrawString(item.Text, this.Font, Brushes.White, lRect, StringFormatCenter);
                    if (tipWidth > 0)
                    {
                        lRect.Offset(lRect.Width, 0);
                        g.DrawString(item.TipText, this.Font, Brushes.White, lRect, StringFormatLeft);
                    }
                }
                else
                {
                    var lRect = rect;
                    if (tipWidth > 0)
                        lRect.Width -= tipWidth + 8;
                    using (var brush = new SolidBrush(mainColor))
                        g.DrawString(item.Text, this.Font, brush, lRect, StringFormatCenter);
                    if (tipWidth > 0)
                    {
                        lRect.Offset(lRect.Width, 0);
                        using (var brush = new SolidBrush(item.TipColor == Color.Empty ? WDColors.Green6 : item.TipColor))
                            g.DrawString(item.TipText, this.Font, brush, lRect, StringFormatLeft);
                    }
                }
            }
        }
    }


    /// <summary>
    /// 客户列表Tabs
    /// </summary>
    public class UCPatientListTabs : WDList
    {
        public List<string> Labels = new List<string>
        {
        };
        public UCPatientListTabs()
        {
            base.BackColor = WDColors.GrayBackColorF4;
            base.ItemHeight = 46;
            base.AutoWrap = true;
            base.Font = WinDo.Utilities.PublicResource.WDFonts.TextFont;
            base.AdjustItemRect = (ref Rectangle rect, WinDoListItem item) =>
            {
                //rect.Width += 10;
                if (!string.IsNullOrEmpty(item.TipText))
                {
                    var width = TextRenderer.MeasureText(item.TipText, this.Font).Width;
                    rect.Width += width;
                }
            };
            base.DrawItems = DrawItem;
            this.SizeChanged += UcListV1_SizeChanged;
        }

        public void UcListV1_SizeChanged(object sender, EventArgs e)
        {
            var widthSum = this.Width;
            using (var g = this.CreateGraphics())
            {
                widthSum = this.Items.Sum(i =>
                (int)Math.Ceiling(g.MeasureString(i.Text, this.Font).Width) + 3 + LeftRightSpace +
                  (!string.IsNullOrEmpty(i.TipText) ?
                   (int)Math.Ceiling(g.MeasureString(i.TipText, this.Font).Width) : 0
                  ));
            }

            if (this.Width < widthSum)
            {
                this.Height = ItemHeight * 2;
            }
            else
                this.Height = ItemHeight;
            this.Invalidate();
        }
        public bool DrawBottomLine = true;
        private void DrawItem(Graphics g, WDList sender)
        {
            var format = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            foreach (var item in sender.Items)
            {
                var rect = item.ClientRectangle;
                if (item == sender.CurrentItem)
                {
                    g.FillRectangle(Brushes.White, rect);
                    var bRect = rect;
                    bRect.Height = 4;
                    var bannerColor = item.BannerColor.HasValue ? item.BannerColor.Value : item.TipColor;
                    using (var brush = new SolidBrush(bannerColor))
                        g.FillRectangle(brush, bRect);
                    if (!string.IsNullOrEmpty(item.TipText))
                    {
                        var txtsize = TextRenderer.MeasureText(item.TipText, this.Font);
                        var width = txtsize.Width + 4;
                        var tRect = rect;
                        tRect.Offset(tRect.Width - width - 10, (tRect.Height - txtsize.Height) / 2);
                        tRect.Width = width;
                        tRect.Height = txtsize.Height;
                        using (var brush = new SolidBrush(item.TipColor))
                        {
                            if (item.TipText.Length == 1)
                                g.FillEllipse(brush, tRect);
                            else
                                WinDoControls.ControlHelper.FillRoundRectangle(g, brush, tRect, 9);
                        }
                        tRect.Offset(1, 1);
                        g.DrawString(item.TipText, WDFonts.TextFont12, Brushes.White, tRect, format);
                        rect.Width -= width;
                    }
                    g.DrawString(item.Text, this.Font, Brushes.Black, rect, format);
                    using (var pen = new Pen(WDColors.GrayRectColor))
                    {
                        g.DrawLine(pen, new Point(item.ClientRectangle.X, item.ClientRectangle.Y + 4), new Point(item.ClientRectangle.X, item.ClientRectangle.Bottom));
                        g.DrawLine(pen, new Point(item.ClientRectangle.Right, item.ClientRectangle.Y + 4), new Point(item.ClientRectangle.Right, item.ClientRectangle.Bottom));
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(item.TipText))
                    {
                        var txtsize = TextRenderer.MeasureText(item.TipText, this.Font);
                        var width = txtsize.Width + 4;
                        var tRect = rect;
                        tRect.Offset(tRect.Width - width - 10, (tRect.Height - txtsize.Height) / 2);
                        tRect.Width = width;
                        tRect.Height = txtsize.Height;
                        using (var brush = new SolidBrush(item.TipColor))
                        {
                            if (item.TipText.Length == 1)
                                g.FillEllipse(brush, tRect);
                            else
                                WinDoControls.ControlHelper.FillRoundRectangle(g, brush, tRect, 9);
                        }
                        tRect.Offset(1, 1);
                        g.DrawString(item.TipText, WDFonts.TextFont12, Brushes.White, tRect, format);
                        rect.Width -= width;
                    }
                    g.DrawString(item.Text, this.Font, Brushes.Black, rect, format);
                    if (DrawBottomLine)
                        using (var pen = new Pen(WDColors.GrayRectColor))
                        {
                            g.DrawLine(pen, new Point(item.ClientRectangle.X, item.ClientRectangle.Bottom - 1), new Point(item.ClientRectangle.Right, item.ClientRectangle.Bottom - 1));
                        }
                }
            }
        }

        public void SetStyle()
        {
            base.DrawItems = DrawItem1;
        }
        private void DrawItem1(Graphics g, WDList sender)
        {
            var format = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            foreach (var item in sender.Items)
            {
                var rect = item.ClientRectangle;
                if (item == sender.CurrentItem)
                {
                    g.FillRectangle(Brushes.White, rect);
                    var bRect = rect;
                    bRect.Height = 4;
                    var bannerColor = item.BannerColor.HasValue ? item.BannerColor.Value : item.TipColor;
                    using (var brush = new SolidBrush(bannerColor))
                        g.FillRectangle(brush, bRect);
                    if (!string.IsNullOrEmpty(item.TipText))
                    {
                        var txtsize = TextRenderer.MeasureText(item.TipText, this.Font);
                        var width = Math.Max(txtsize.Width - 4, 21);
                        var tRect = rect;
                        tRect.Offset(tRect.Width - width - 8, (tRect.Height - txtsize.Height) / 2);
                        tRect.Width = width;
                        tRect.Height = txtsize.Height;
                        using (var brush = new SolidBrush(item.TipColor))
                        {
                            if (item.TipText.Length <= 2)
                            {
                                tRect.Size = new Size(24, 24);
                                tRect.Y = (rect.Height - tRect.Height) / 2;
                                g.FillEllipse(brush, tRect);
                            }
                            else
                                WinDoControls.ControlHelper.FillRoundRectangle(g, brush, tRect, 9);
                        }
                        tRect.Offset(0, 1);
                        g.DrawString(item.TipText, WDFonts.TextFontBold12, Brushes.White, tRect, format);
                        rect.Width -= width;
                    }
                    g.DrawString(item.Text, WDFonts.TextFontBold, Brushes.Black, rect, format);
                    using (var pen = new Pen(WDColors.GrayRectColor))
                    {
                        g.DrawLine(pen, new Point(item.ClientRectangle.X, item.ClientRectangle.Y + 4), new Point(item.ClientRectangle.X, item.ClientRectangle.Bottom));
                        g.DrawLine(pen, new Point(item.ClientRectangle.Right, item.ClientRectangle.Y + 4), new Point(item.ClientRectangle.Right, item.ClientRectangle.Bottom));
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(item.TipText))
                    {
                        var txtsize = TextRenderer.MeasureText(item.TipText, this.Font);
                        var width = Math.Max(txtsize.Width - 4, 21);
                        var tRect = rect;
                        tRect.Offset(tRect.Width - width - 8, (tRect.Height - txtsize.Height) / 2);
                        tRect.Width = width;
                        tRect.Height = txtsize.Height;
                        using (var brush = new SolidBrush(item.TipColor))
                        {
                            if (item.TipText.Length <= 2)
                            {
                                tRect.Size = new Size(24, 24);
                                tRect.Y = (rect.Height - tRect.Height) / 2;
                                g.FillEllipse(brush, tRect);
                            }
                            else
                                WinDoControls.ControlHelper.FillRoundRectangle(g, brush, tRect, 9);
                        }
                        tRect.Offset(0, 1);
                        g.DrawString(item.TipText, WDFonts.TextFontBold12, Brushes.White, tRect, format);
                        rect.Width -= width;
                    }
                    using (var brush = new SolidBrush(WDColors.GrayTextColor))
                        g.DrawString(item.Text, WDFonts.TextFontBold, brush, rect, format);
                    using (var pen = new Pen(WDColors.GrayRectColor))
                    {
                        g.DrawLine(pen, new Point(item.ClientRectangle.X, item.ClientRectangle.Bottom - 1), new Point(item.ClientRectangle.Right, item.ClientRectangle.Bottom - 1));
                    }
                }
            }
        }

    }



    /// <summary>
    /// 客户列表右键
    /// </summary>
    public class PT_RightKey : WDList
    {
        public PT_RightKey()
        {
            if (MyIcons == null)
            {
                MyIcons = new Dictionary<string, Image>();
                MyIcons.Add("自动勾画", WDImages.GetBtnIconImage("I_bqgh", 14));
                MyIcons.Add("靶区勾画", WDImages.GetBtnIconImage("I_zdgh", 14));
                MyIcons.Add("特殊情况", WDImages.GetBtnIconImage("I_exclamation_delta_c", 14));
                MyIcons.Add("发消息", WDImages.GetBtnIconImage("I_send_sms", 14));
                MyIcons.Add("隐藏", WDImages.GetBtnIconImage("I_hide", 14));
                MyIcons.Add("激活", WDImages.GetBtnIconImage("I_jihuo", 14));
                MyIcons.Add("验证", WDImages.GetBtnIconImage("I_shtg", 14));
                MyIcons.Add("领取计划", WDImages.GetBtnIconImage("I_lqjh", 14));
                MyIcons.Add("计划待评估", WDImages.GetBtnIconImage("I_dpg", 14));
                MyIcons.Add("MOSAIQ签字    ", WDImages.GetBtnIconImage("I_cfqz", 14));
                MyIcons.Add("取消领取", WDImages.GetBtnIconImage("I_shbtg", 14));
                MyIcons.Add("勾画开发", WDImages.GetBtnIconImage("I_ghys", 14));
                MyIcons.Add("审核通过", WDImages.GetBtnIconImage("I_shtg", 14, ColorTranslator.FromHtml("#01b009")));
                MyIcons.Add("审核驳回", WDImages.GetBtnIconImage("I_shbtg", 14, ColorTranslator.FromHtml("#e30404")));

                MyIcons.Add("评估通过", WDImages.GetBtnIconImage("I_shtg", 14, ColorTranslator.FromHtml("#01b009")));
                MyIcons.Add("评估驳回", WDImages.GetBtnIconImage("I_shbtg", 14, ColorTranslator.FromHtml("#e30404")));

                MyIcons.Add("复审通过", WDImages.GetBtnIconImage("I_shtg", 14, ColorTranslator.FromHtml("#01b009")));
                MyIcons.Add("复审驳回", WDImages.GetBtnIconImage("I_shbtg", 14, ColorTranslator.FromHtml("#e30404")));
                MyIcons.Add("驳回", WDImages.GetBtnIconImage("I_shbtg", 14, ColorTranslator.FromHtml("#e30404")));

                MyIcons.Add("查看费用", WDImages.GetBtnIconImage("I_charge_doc", 14));
                MyIcons.Add("查看收费单", WDImages.GetBtnIconImage("I_charge_doc", 14));
                MyIcons.Add("查看收费", WDImages.GetBtnIconImage("I_charge_doc", 14));
                MyIcons.Add("查看医嘱", WDImages.GetBtnIconImage("I_danjuguanli", 14));
                MyIcons.Add("查看医嘱单", WDImages.GetBtnIconImage("I_danjuguanli", 14));
                MyIcons.Add("填写备注", WDImages.GetBtnIconImage("I_add_remark", 14));
                MyIcons.Add("添加备注", WDImages.GetBtnIconImage("I_add_remark", 14));
                MyIcons.Add("忽略", WDImages.GetBtnIconImage("I_ignore", 14));
                MyIcons.Add("取消忽略", WDImages.GetBtnIconImage("I_ignore_cancel", 14));
                MyIcons.Add("后缴费收费", WDImages.GetBtnIconImage("I_charge_doc", 14));
                MyIcons.Add("打印申请单", WDImages.GetBtnIconImage("I_print", 14));
                MyIcons.Add("打印收费单", WDImages.GetBtnIconImage("I_print", 14));
                MyIcons.Add("打印退费单", WDImages.GetBtnIconImage("I_print", 14));
                MyIcons.Add("取消已收费", WDImages.GetBtnIconImage("I_cancel_charge", 14));
                MyIcons.Add("打印造影剂", WDImages.GetBtnIconImage("I_print", 14));
                MyIcons.Add("打印", WDImages.GetBtnIconImage("I_print", 14));
                MyIcons.Add("忽略预警", WDImages.GetBtnIconImage("I_ignore_warning", 14));
                MyIcons.Add("预约调整", WDImages.GetBtnIconImage("I_edit", 14));
                MyIcons.Add("流转到计划设计", WDImages.GetBtnIconImage("I_to_plan", 14));

                MyIcons.Add("退费", WDImages.GetBtnIconImage("I_tuifei", 14));
                MyIcons.Add("取消执行", WDImages.GetBtnIconImage("I_cancel_charge", 14));
                MyIcons.Add("驳回至未处理", WDImages.GetBtnIconImage("I_fanhui", 14));
                MyIcons.Add("收费备注", WDImages.GetBtnIconImage("I_add_remark", 14));
                //------------------------------------------------------------------
                MyIcons.Add("自动勾画_H", WDImages.GetBtnIconImage("I_bqgh", 14, Color.White));
                MyIcons.Add("靶区勾画_H", WDImages.GetBtnIconImage("I_zdgh", 14, Color.White));
                MyIcons.Add("特殊情况_H", WDImages.GetBtnIconImage("I_exclamation_delta_c", 14, Color.White));
                MyIcons.Add("发消息_H", WDImages.GetBtnIconImage("I_send_sms", 14, Color.White));
                MyIcons.Add("隐藏_H", WDImages.GetBtnIconImage("I_hide", 14, Color.White));
                MyIcons.Add("激活_H", WDImages.GetBtnIconImage("I_jihuo", 14, Color.White));
                MyIcons.Add("领取计划_H", WDImages.GetBtnIconImage("I_lqjh", 14, Color.White));
                MyIcons.Add("计划待评估_H", WDImages.GetBtnIconImage("I_dpg", 14, Color.White));
                MyIcons.Add("MOSAIQ签字    _H", WDImages.GetBtnIconImage("I_cfqz", 14, Color.White));
                MyIcons.Add("取消领取_H", WDImages.GetBtnIconImage("I_shbtg", 14, Color.White));
                MyIcons.Add("勾画开发_H", WDImages.GetBtnIconImage("I_ghys", 14, Color.White));
                MyIcons.Add("审核通过_H", WDImages.GetBtnIconImage("I_shtg", 14, ColorTranslator.FromHtml("#01b009")));
                MyIcons.Add("审核驳回_H", WDImages.GetBtnIconImage("I_shbtg", 14, ColorTranslator.FromHtml("#e30404")));

                MyIcons.Add("评估通过_H", WDImages.GetBtnIconImage("I_shtg", 14, ColorTranslator.FromHtml("#01b009")));
                MyIcons.Add("评估驳回_H", WDImages.GetBtnIconImage("I_shbtg", 14, ColorTranslator.FromHtml("#e30404")));

                MyIcons.Add("复审通过_H", WDImages.GetBtnIconImage("I_shtg", 14, ColorTranslator.FromHtml("#01b009")));
                MyIcons.Add("复审驳回_H", WDImages.GetBtnIconImage("I_shbtg", 14, ColorTranslator.FromHtml("#e30404")));

                MyIcons.Add("驳回_H", WDImages.GetBtnIconImage("I_shbtg", 14, ColorTranslator.FromHtml("#e30404")));

                MyIcons.Add("押金", WDImages.GetBtnIconImage("I_Deposit", 14));
                MyIcons.Add("退款", WDImages.GetBtnIconImage("I_Refund", 14));
            }
            base.BackColor = WDColors.SelectedBackColor;
            base.ItemHeight = 40;
            base.MaxWidth = 120;
            base.AutoWrap = true;
            base.Font = WinDo.Utilities.PublicResource.WDFonts.TextFont;
            base.AdjustItemRect = (ref Rectangle rect, WinDoListItem item) =>
            {
                rect.Width = this.Width;
                if (item.Text == Style.SPLIT_LINE)
                    rect.Height = 3;
            };
            base.DrawItems = DrawItem;

            //base.IsShowRect = true;
            //base.IsRadius = true;
            //base.ConerRadius = 1;
            //base.RectColor = WinDo.Utilities.PublicResource.YkdBasisColors.MainColor;
            //base.Padding = new Padding(1);
            //KeyPanel.Dock = DockStyle.Fill;
            //this.Controls.Add(KeyPanel);
            //KeyPanel.ItemHeight = 40;
            //KeyPanel.FixItemWidth = this.Width;
            //KeyPanel.AdjustItemRect = (ref Rectangle rect, AwesomeListItem item) =>
            //{
            //    if (item.Text == LINE)
            //        rect.Height = 1;
            //};
        }
        protected override void RepositionItems(Graphics g)
        {
            int xoffset = LeftOffset;
            int yoffset = TopOffset;
            var maxWidth = this.MaxWidth > 0 ? this.MaxWidth : this.Width;
            foreach (var item in this.Items)
            {
                string text = item.Text;
                if (string.IsNullOrEmpty(text))
                    text = " ";
                SizeF textSize = g.MeasureString(text, this.Font);
                int width = (int)Math.Ceiling(textSize.Width);
                var itemHeight = _ItemHeight;
                if (item.Text == Style.SPLIT_LINE)
                    itemHeight = 3;
                int height = itemHeight;
                Rectangle rect = new Rectangle(xoffset, yoffset, width, height);

                DefaultAdjustItemRect(ref rect, item);

                //xoffset += rect.Width;
                //if (AutoWrap && xoffset > maxWidth + LeftOffset)
                {
                    //xoffset = LeftOffset + rect.Width;
                    rect.X = LeftOffset;
                    rect.Y = yoffset;
                    yoffset += rect.Height;
                }
                item.ClientRectangle = rect;
            }
        }
        Color HoverBackColor = ColorTranslator.FromHtml("#98dcf0");
        Color PassColor = ColorTranslator.FromHtml("#01b009");
        Color RejectColor = ColorTranslator.FromHtml("#e30404");


        public static Dictionary<string, Image> MyIcons;

        private void DrawItem(Graphics g, WDList sender)
        {
            var format = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
            foreach (var item in sender.Items)
            {
                if (item.Text == Style.SPLIT_LINE)
                {
                    using (var pen = new Pen(WDColors.geekblue6))
                        g.DrawLine(pen, 4, item.ClientRectangle.Y + 2, item.ClientRectangle.Right - 4, item.ClientRectangle.Y + 2);
                    continue;
                }
                var rect = item.ClientRectangle;
                if (item == sender.HoveredItem)
                {
                    using (var brush = new SolidBrush(HoverBackColor))
                        g.FillRectangle(brush, rect);
                    if (item.CurLeftIcon != null)
                    {
                        var icon = item.CurLeftIcon;
                        var rRect = rect;
                        rRect.Offset(rRect.X + 14, (rRect.Height - icon.Height) / 2);
                        g.DrawImage(icon, rRect.Location);
                    }
                    else if (MyIcons.ContainsKey(item.Text))
                    {
                        var icon = MyIcons[item.Text];
                        var rRect = rect;
                        rRect.Offset(rRect.X + 14, (rRect.Height - icon.Height) / 2);
                        g.DrawImage(icon, rRect.Location);
                    }
                    var trect = rect;
                    trect.Offset(40, 0);
                    var txtColor = Color.Black;
                    if (item.Text.Contains("通过"))
                        txtColor = PassColor;
                    if (item.Text.Contains("驳回"))
                        txtColor = RejectColor;
                    using (var brush = new SolidBrush(txtColor))
                        g.DrawString(item.Text, this.Font, brush, trect, format);
                }
                else
                {
                    if (item.LeftIcon != null)
                    {
                        var icon = item.LeftIcon;
                        var rRect = rect;
                        rRect.Offset(rRect.X + 14, (rRect.Height - icon.Height) / 2);
                        g.DrawImage(icon, rRect.Location);
                    }
                    else if (MyIcons.ContainsKey(item.Text))
                    {
                        var icon = MyIcons[item.Text];
                        var rRect = rect;
                        rRect.Offset(rRect.X + 14, (rRect.Height - icon.Height) / 2);
                        g.DrawImage(icon, rRect.Location);
                    }
                    var trect = rect;
                    trect.Offset(40, 0);
                    var txtColor = Color.Black;
                    if (item.Text.Contains("通过"))
                        txtColor = PassColor;
                    if (item.Text.Contains("驳回"))
                        txtColor = RejectColor;
                    using (var brush = new SolidBrush(txtColor))
                        g.DrawString(item.Text, this.Font, brush, trect, format);
                }
            }
        }
        public int GetHeight()
        {
            return Items.Where(i => i.Text != Style.SPLIT_LINE).Count() * this.ItemHeight + 3;
        }
        public void AutoHeight()
        {
            this.Height = Items.Where(i => i.Text != Style.SPLIT_LINE).Count() * this.ItemHeight + 3;
        }
    }



    /// <summary>
    /// 单据Tabs控件
    /// </summary>
    public class ActivityTabs : WDList
    {
        public ActivityTabs()
        {
            this.Font = WDFonts.TextFontBold;
            this.BackColor = WDColors.GrayBackColorF7;
            this.AdjustItemRect = _AdjustItemRect;
            this.DrawItems = _DrawItems;
        }

        //带边框式样
        void _AdjustItemRect(ref Rectangle rect, WinDoListItem item)
        {
            if (item.LeftIcon != null)
                rect.Width += 20;
            if (item.RightIcon != null)
                rect.Width += 20;
        }
        StringFormat format = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

        void _DrawItems(Graphics g, WDList sender)
        {
            foreach (var item in sender.Items)
            {
                var rect = item.ClientRectangle;
                var txtwidth = (int)(g.MeasureString(item.Text, sender.Font).Width);
                var space = (rect.Width - txtwidth) / 2;
                if (item == sender.CurrentItem)
                {
                    g.FillRectangle(Brushes.White, rect);
                    var bRect = rect;
                    bRect.Height = 4;
                    using (var brush = new SolidBrush(WDColors.BtnSelectedLine))
                        g.FillRectangle(brush, bRect);
                    if (item.CurLeftIcon != null)
                    {
                        var lRect = rect;
                        lRect.Offset((space - item.CurLeftIcon.Width) / 2 + 5, (lRect.Height - item.CurLeftIcon.Height) / 2);
                        if (item.CurRightIcon == null)
                            lRect.Offset(4, 0);
                        g.DrawImage(item.CurLeftIcon, lRect.Location);
                    }
                    if (item.CurRightIcon != null)
                    {
                        var rRect = rect;
                        rRect.Offset(rRect.Width - space + ((space - item.CurRightIcon.Width) / 2) - 5, (rRect.Height - item.CurRightIcon.Height) / 2);
                        if (item.CurLeftIcon == null)
                            rRect.Offset(-5, 0);
                        g.DrawImage(item.CurRightIcon, rRect.Location);
                    }
                    var txtrect = rect;
                    if (item.CurRightIcon == null && item.CurLeftIcon != null)
                        txtrect.Offset(item.CurLeftIcon.Width / 2, 0);
                    if (item.CurLeftIcon == null && item.CurRightIcon != null)
                        txtrect.Offset(-(item.CurRightIcon.Width / 2), 0);
                    g.DrawString(item.Text, sender.Font, Brushes.Black, txtrect, format);
                }
                else
                {
                    if (item.LeftIcon != null)
                    {
                        var lRect = rect;
                        lRect.Offset((space - item.CurLeftIcon.Width) / 2 + 5, (lRect.Height - item.LeftIcon.Height) / 2);
                        if (item.RightIcon == null)
                            lRect.Offset(4, 0);
                        g.DrawImage(item.LeftIcon, lRect.Location);
                    }
                    if (item.RightIcon != null)
                    {
                        var rRect = rect;
                        rRect.Offset(rRect.Width - space + ((space - item.CurRightIcon.Width) / 2) - 5, (rRect.Height - item.RightIcon.Height) / 2);
                        if (item.LeftIcon == null)
                            rRect.Offset(-5, 0);
                        g.DrawImage(item.RightIcon, rRect.Location);
                    }
                    var txtrect = rect;
                    if (item.RightIcon == null && item.LeftIcon != null)
                        txtrect.Offset(item.LeftIcon.Width / 2, 0);
                    if (item.LeftIcon == null && item.RightIcon != null)
                        txtrect.Offset(-(item.RightIcon.Width / 2), 0);
                    using (var brush = new SolidBrush(WDColors.GrayTextColor))
                        g.DrawString(item.Text, sender.Font, brush, txtrect, format);
                }
            }
        }
    }

    /// <summary>
    /// 排队叫号Tabs
    /// </summary>
    public class CallTabs : WDList
    {
        public CallTabs()
        {
            this.Font = WDFonts.TextFontBold;
            this.BackColor = WDColors.GrayBackColorF7;
            this.ItemHeight = this.Height;
            this.AdjustItemRect = _AdjustItemRect;
            this.DrawItems = _DrawItems;
        }

        //带边框式样
        void _AdjustItemRect(ref Rectangle rect, WinDoListItem item)
        {
            if (item.TipText != null)
            {
                var tipWidth = TextRenderer.MeasureText(item.TipText, this.Font).Width;
                rect.Width += tipWidth;

            }
        }
        StringFormat format = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        StringFormat RightSF = new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };

        void _DrawItems(Graphics g, WDList sender)
        {
            foreach (var item in sender.Items)
            {
                var rect = item.ClientRectangle;
                var txtwidth = (int)(g.MeasureString(item.Text, sender.Font).Width);
                var space = (rect.Width - txtwidth) / 2;
                if (item == sender.CurrentItem)
                {
                    g.FillRectangle(Brushes.White, rect);
                    var bRect = rect;
                    bRect.Height = 4;
                    using (var brush = new SolidBrush(WDColors.BtnSelectedLine))
                        g.FillRectangle(brush, bRect);
                    var tipWidth = 0;
                    if (item.TipText != null)
                    {
                        tipWidth = (int)g.MeasureString(item.TipText, sender.Font).Width;
                        var trect = rect;
                        trect.Width -= 10;
                        using (var brush = new SolidBrush(WDColors.StatusBlue))
                            g.DrawString(item.TipText, this.Font, brush, trect, RightSF);
                    }
                    var txtrect = rect;
                    if (item.TipText != null)
                        txtrect.Offset(-tipWidth / 2, 0);
                    g.DrawString(item.Text, sender.Font, Brushes.Black, txtrect, format);
                }
                else
                {
                    var tipWidth = 0;
                    if (item.TipText != null)
                    {
                        tipWidth = (int)g.MeasureString(item.TipText, sender.Font).Width;
                        var trect = rect;
                        trect.Width -= 10;
                        using (var brush = new SolidBrush(WDColors.StatusBlue))
                            g.DrawString(item.TipText, this.Font, brush, trect, RightSF);
                    }
                    var txtrect = rect;
                    if (item.TipText != null)
                        txtrect.Offset(-tipWidth / 2, 0);
                    using (var brush = new SolidBrush(WDColors.GrayTextColor))
                        g.DrawString(item.Text, sender.Font, brush, txtrect, format);
                }
            }
        }
    }

    /// <summary>
    /// 按钮组
    /// </summary>
    public class ButtonList : WDList
    {
        public ButtonList()
        {
            this.Font = WDFonts.TextFont;
            this.BackColor = WDColors.WhiteBackColor;
            this.ItemHeight = this.Height;
            this.DrawItems = _DrawItems;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var rect = this.ClientRectangle;
            rect.Width = this.Items.Sum(i => i.ClientRectangle.Width);
            rect.Height = ItemHeight - 1;
            using (var pen = new Pen(WDColors.GrayRectColor))
                ControlHelper.DrawRoundRectangle(e.Graphics, pen, rect, 2);
        }

        StringFormat format = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        StringFormat RightSF = new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };

        void _DrawItems(Graphics g, WDList sender)
        {
            foreach (var item in sender.Items)
            {
                var rect = item.ClientRectangle;
                if (item != sender.Items.First())
                {
                    using (var pen = new Pen(WDColors.GrayRectColor))
                        g.DrawLine(pen, rect.Left, rect.Top, rect.Left, rect.Bottom);
                }
                if (item == sender.CurrentItem)
                {
                    var rect1 = rect;
                    rect1.Height += 1;
                    using (var brush = new SolidBrush(WDColors.geekblue6))
                        g.FillRectangle(brush, rect1);
                    g.DrawString(item.Text, sender.Font, Brushes.White, rect, format);
                }
                else
                {
                    g.DrawString(item.Text, sender.Font, Brushes.Black, rect, format);
                }
            }
        }
    }


    public static class Style
    {
        public static string SPLIT_LINE = "splitline";
        public static Image ActivityCommitedImage = WDImages.GetBtnIconImage("I_success_fill", 20, WDColors.StatusBlue);
        public static Image ActivityRegisterImage = WDImages.GetBtnIconImage("I_register", 20, WDColors.BlackColor);
        public static Image ActivityRegistedImage = WDImages.GetBtnIconImage("I_registed", 20, WDColors.BlackColor);
        #region SetStyle

        /// <summary>
        /// 顶部带引导条,左右有图标
        /// </summary>
        public static void SetHasTopBannerAndIcon(this WDList ucList)
        {
            ucList.BackColor = Color.Gray;
            //带边框式样
            ucList.AdjustItemRect = (ref Rectangle rect, WinDoListItem item) =>
            {
                if (item.LeftIcon != null)
                    rect.Width += 20;
                if (item.RightIcon != null)
                    rect.Width += 20;
            };
            ucList.DrawItems = (g, sender) =>
            {
                var format = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                foreach (var item in sender.Items)
                {
                    var rect = item.ClientRectangle;


                    if (item == sender.CurrentItem)
                    {
                        g.FillRectangle(Brushes.White, rect);
                        var bRect = rect;
                        bRect.Height = 2;
                        g.FillRectangle(Brushes.Blue, bRect);
                        if (item.CurLeftIcon != null)
                        {
                            var lRect = rect;
                            lRect.Offset(10, (lRect.Height - item.CurLeftIcon.Height) / 2);
                            g.DrawImage(item.CurLeftIcon, lRect.Location);
                        }
                        if (item.CurRightIcon != null)
                        {
                            var rRect = rect;
                            rRect.Offset(rRect.Width - 10 - 10, (rRect.Height - item.CurRightIcon.Height) / 2);
                            g.DrawImage(item.CurRightIcon, rRect.Location);
                        }

                        g.DrawString(item.Text, ucList.Font, Brushes.Black, rect, format);
                    }
                    else
                    {
                        g.FillRectangle(Brushes.Gray, rect);
                        if (item.LeftIcon != null)
                        {
                            var lRect = rect;
                            lRect.Offset(10, (lRect.Height - item.LeftIcon.Height) / 2);
                            g.DrawImage(item.LeftIcon, lRect.Location);
                        }
                        if (item.RightIcon != null)
                        {
                            var rRect = rect;
                            rRect.Offset(rRect.Width - 10 - 10, (rRect.Height - item.RightIcon.Height) / 2);
                            g.DrawImage(item.RightIcon, rRect.Location);
                        }

                        g.DrawString(item.Text, ucList.Font, Brushes.Black, rect, format);
                    }
                }
            };
        }

        /// <summary>
        /// 顶部带引导条
        /// </summary>
        public static void SetHasTopBanner(this WDList ucList, bool showTipCycle = false)
        {
            ucList.BackColor = WDColors.GrayBackColorF7;
            ucList.AdjustItemRect = (ref Rectangle rect, WinDoListItem item) =>
            {
                if (!string.IsNullOrEmpty(item.TipText))
                {
                    var width = TextRenderer.MeasureText(item.TipText, ucList.Font).Width + 4;
                    rect.Width += width;
                }
            };
            ucList.DrawItems = (g, sender) =>
            {
                var format = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                foreach (var item in sender.Items)
                {
                    var rect = item.ClientRectangle;
                    if (item == sender.CurrentItem)
                    {
                        g.FillRectangle(Brushes.White, rect);
                        var bRect = rect;
                        bRect.Height = 2;
                        using (var brush = new SolidBrush(WDColors.BannerBlueColor))
                            g.FillRectangle(brush, bRect);
                        if (!string.IsNullOrEmpty(item.TipText))
                        {
                            var txtsize = TextRenderer.MeasureText(item.TipText, ucList.Font);
                            var width = txtsize.Width + 4;
                            var tRect = rect;
                            tRect.Offset(tRect.Width - width, (tRect.Height - txtsize.Height) / 2);
                            tRect.Width = width;
                            tRect.Height = txtsize.Height + 4;
                            if (showTipCycle)
                                g.FillEllipse(new SolidBrush(ucList.BackColor), tRect);
                            g.DrawString(item.TipText, ucList.Font, Brushes.Blue, tRect, format);
                            rect.Width -= width;
                        }
                        g.DrawString(item.Text, ucList.Font, Brushes.Black, rect, format);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(item.TipText))
                        {
                            var txtsize = TextRenderer.MeasureText(item.TipText, ucList.Font);
                            var width = txtsize.Width + 4;
                            var tRect = rect;
                            tRect.Offset(tRect.Width - width, (tRect.Height - txtsize.Height) / 2);
                            tRect.Width = width;
                            tRect.Height = txtsize.Height + 4;
                            if (showTipCycle)
                                g.FillEllipse(new SolidBrush(ucList.BackColor), tRect);
                            g.DrawString(item.TipText, ucList.Font, Brushes.Blue, tRect, format);
                            rect.Width -= width;
                        }
                        using (var brush = new SolidBrush(WDColors.GrayTextColor))
                            g.DrawString(item.Text, ucList.Font, brush, rect, format);
                    }
                }
            };
        }
        /// <summary>
        /// 左边带引导条
        /// </summary>
        public static void SetHasLeftBanner(this WDList ucList)
        {
            ucList.AdjustItemRect = (ref Rectangle rect, WinDoListItem item) =>
            {
                if (item.RightIcon != null)
                    rect.Width += 20;
            };
            //带边框式样
            ucList.DrawItems = (g, sender) =>
            {
                var format = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                foreach (var item in sender.Items)
                {
                    var rect = item.ClientRectangle;

                    var bRect = rect;
                    bRect.Offset(10, (rect.Height - 10) / 2);
                    bRect.Width = 4;
                    bRect.Height = 10;
                    if (item == sender.CurrentItem)
                    {
                        g.FillRectangle(Brushes.White, rect);
                        g.FillRectangle(Brushes.Blue, bRect);
                        if (item.CurRightIcon != null)
                        {
                            var rRect = rect;
                            rRect.Offset(rRect.Width - 10 - 10, (rRect.Height - item.CurRightIcon.Height) / 2);
                            g.DrawImage(item.CurRightIcon, rRect.Location);
                            rect.Width -= 20;
                        }
                        rect.Offset(5, 2);
                        g.DrawString(item.Text, ucList.Font, Brushes.Blue, rect, format);
                    }
                    else
                    {
                        g.FillRectangle(Brushes.Gray, rect);
                        g.FillRectangle(Brushes.Black, bRect);
                        if (item.RightIcon != null)
                        {
                            var rRect = rect;
                            rRect.Offset(rRect.Width - 10 - 10, (rRect.Height - item.RightIcon.Height) / 2);
                            g.DrawImage(item.RightIcon, rRect.Location);
                            rect.Width -= 20;
                        }
                        rect.Offset(5, 2);
                        g.DrawString(item.Text, ucList.Font, Brushes.Black, rect, format);
                    }
                }
            };
        }

        /// <summary>
        /// 带边框
        /// </summary>
        /// <param name="ucList"></param>
        /// <param name="hasBorder"></param>
        /// <param name="curHighLight"></param>
        public static void SetDefaultStyle(this WDList ucList, bool hasBorder = true, bool curHighLight = true)
        {
            //带边框式样
            ucList.LeftOffset = hasBorder ? 1 : 0;
            ucList.TopOffset = hasBorder ? 1 : 0;
            ucList.AdjustItemRect = (ref Rectangle rect, WinDoListItem item) =>
            {
                if (item.RightIcon != null)
                    rect.Width += 20;
            };
            ucList.DrawItems = (g, sender) =>
            {
                var format = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                foreach (var item in sender.Items)
                {
                    var rect = item.ClientRectangle;

                    if (hasBorder)
                        using (var pen = new Pen(Color.DarkBlue, 2))
                        {
                            g.DrawRectangle(pen, rect);
                        }
                    if (curHighLight && item == sender.CurrentItem)
                    {
                        g.FillRectangle(Brushes.DarkBlue, rect);
                        if (item.CurRightIcon != null)
                        {
                            var rRect = rect;
                            rRect.Offset(rRect.Width - 10 - 10, (rRect.Height - item.CurRightIcon.Height) / 2);
                            g.DrawImage(item.CurRightIcon, rRect.Location);
                            rect.Width -= 20;
                        }
                        g.DrawString(item.Text, ucList.Font, Brushes.White, rect, format);
                    }
                    else
                    {
                        rect.Offset(1, 1);
                        rect.Width -= 2;
                        rect.Height -= 2;
                        g.FillRectangle(Brushes.White, rect);

                        if (item.RightIcon != null)
                        {
                            var rRect = rect;
                            rRect.Offset(rRect.Width - 10 - 10, (rRect.Height - item.RightIcon.Height) / 2);
                            g.DrawImage(item.RightIcon, rRect.Location);
                            rect.Width -= 20;
                        }
                        g.DrawString(item.Text, ucList.Font, Brushes.DarkBlue, rect, format);
                    }
                }
            };
        }

        #endregion

    }

    public delegate void ListVItemClick(WinDoListItem item, MouseEventArgs e);
    public delegate void RefAction<T1, T2>(ref T1 arg1, T2 arg2);
    public class WinDoListItem
    {
        public List<WinDoListItem> Items { get; set; }
        public string Text { get; set; }
        public string TipText { get; set; }
        /// <summary>
        /// 鼠标悬浮提示
        /// </summary>
        public string OverLengthTips { get; set; }
        public dynamic Data { get; set; }
        public Image LeftIcon { get; set; }
        public Image RightIcon { get; set; }
        public Color? BannerColor { get; set; }
        public Color TipColor { get; set; }
        public string Code { get; set; }
        /// <summary>
        /// 关联的控件
        /// </summary>
        public System.Windows.Forms.Control RelationControl { get; set; }
        public Image CurLeftIcon { get; set; }
        public Image CurRightIcon { get; set; }

        public Rectangle ClientRectangle { get; set; }
    }
}

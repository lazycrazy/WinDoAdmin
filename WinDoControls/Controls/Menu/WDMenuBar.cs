


using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;

namespace WinDoControls.Controls
{








    public class WDPopupMenu : WinDoControls.Controls.WDMenu
    {
        #region Constants
        private static readonly int checkAreaWidth = 15;
        #endregion Constants

        #region Constructor



        public WDPopupMenu()
        {
            this.alwaysShowPopup = true;
            this.isPopupStyleMenu = true;
            this.MenuTimerInterval = 300;
            this.Size = new Size(80, 10);
            this.BackColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
        }
        #endregion Constructor

        #region Public methods










        public void TrackPopupMenu(Control parent, WDMenuAlignment alignX, WDMenuAlignment alignY, int x, int y)
        {
            if (parent == null || this.MenuItems.Count == 0)
                return;

            this.Parent = parent;
            this.ParentMenu = null;

            this.Visible = true;
            this.ParentMenuItem = null;

            int x2 = x;
            int y2 = y;
            switch (alignX)
            {
                case WDMenuAlignment.LeftAlign:
                    x2 = x - 1;
                    break;
                case WDMenuAlignment.CenterAlign:
                    x2 = x - this.ClientRectangle.Width / 2;
                    break;
                case WDMenuAlignment.RightAlign:
                    x2 = x - this.ClientRectangle.Width + 1;
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
            switch (alignY)
            {
                case WDMenuAlignment.TopAlign:
                    y2 = y - 1;
                    break;
                case WDMenuAlignment.VCenterAlign:
                    y2 = y - this.ClientRectangle.Height / 2;
                    break;
                case WDMenuAlignment.BottomAlign:
                    y2 = y - this.ClientRectangle.Height + 1;
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
            this.Location = new Point(x2, y2);

            this.BringToFront();
        }









        public void TrackPopupMenu(Control parent, int x, int y)
        {
            this.TrackPopupMenu(parent, WDMenuAlignment.LeftAlign, WDMenuAlignment.TopAlign, x, y);
        }
        #endregion Public methods

        #region Protected Dispose method



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }
            base.Dispose(disposing);
        }
        #endregion Protected Dispose method

        #region Private methods





        private void AdjustMenuItemRect(ref Rectangle rect)
        {
            return;
            rect.Y -= 2;
            rect.Height += 4;
            rect.X -= 3;
            rect.Width += 7;
        }





        private void AdjustMenuSize(int col)
        {
            if (this.MenuItems.Count == 0)
                return;


            int newWidth = 80;
            if (this.ParentMenu != null && !this.ParentMenu.IsPopupMenu)
                newWidth = Math.Max(newWidth, this.ParentMenuItem.ClientRectangle.Width);
            int newHeight = 0;

            int xpad = 0;
            int ypad = 0;
            bool hasSubMenu = false;

            if (this.HasCheckOrRadioItems())
                xpad += WDPopupMenu.checkAreaWidth + 3;

            for (int i = 0; i < this.MenuItems.Count; ++i)
            {
                WDMenuItem item = this.MenuItems.GetAt(i);
                if (col == 1)
                    newWidth = Math.Max(newWidth, item.ClientRectangle.Width + xpad);
                else
                    newWidth = Math.Max(90, item.ClientRectangle.Width + xpad);
                if (item.MenuItems.Count > 0)
                    hasSubMenu = true;
                newHeight = Math.Max(newHeight, item.ClientRectangle.Bottom);
            }


            newHeight += ypad;


            if (this.ClientRectangle.Width != newWidth ||
                 this.ClientRectangle.Height != newHeight)
            {
                this.Size = new Size(newWidth * col, newHeight);
            }
        }










        private void DrawMenuItemTextR(Graphics g, string text, Font font, Color color, Rectangle r, WDMenuItem item)
        {
            using (Brush brush = new SolidBrush(color))
            {
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;

                Rectangle rnew = new Rectangle(r.X, r.Y, r.Width, r.Height);
                g.DrawString(text, font, brush, (RectangleF)rnew, sf);
            }
            //DrawIconImage(g, item);
            if (item.MenuItems.Count > 0)
                this.DrawSubMenuArrow(g, item, this.currMenuItem == item ? this.HoverTextColor : this.TextColor);

        }






        private void DrawMenuItem(Graphics g, WDMenuItem item)
        {
            if (item.ClientRectangle.Width < 2)
                return;

            if (item.Style == WDMenuItemStyle.Separator)
            {

                int y = (item.ClientRectangle.Top + item.ClientRectangle.Bottom) / 2;
                int x1 = item.ClientRectangle.Left;
                int x2 = item.ClientRectangle.Right - 1;
                this.DrawSeparator(g, this.SeparatorColor, x1, y, x2, y);
            }
            else
            {

                Color tcolor = this.TextColor;
                if (!item.Enabled)
                    tcolor = this.DisabledTextColor;
                if (item.Style == WDMenuItemStyle.Check)
                    this.DrawCheck(g, tcolor, item);
                else if (item.Style == WDMenuItemStyle.Radio)
                    this.DrawRadio(g, tcolor, item);


                if (this.currMenuItem == item)
                {

                    if (this.EnableHoverBackDrawing)
                        this.DrawBackground(g, item.ClientRectangle, this.HoverBackGradientColor);
                    if (this.EnableHoverBorderDrawing)
                        this.DrawBorder(g, item.ClientRectangle, this.HoverBorderColor);
                    this.DrawMenuItemTextR(g, item.Text, this.HoverFont, this.HoverTextColor,
                        item.ClientRectangle, item);

                }
                else
                {

                    this.DrawMenuItemTextR(g, item.Text, this.Font, tcolor,
                        item.ClientRectangle, item);
                }
            }
        }








        private void DrawPartialLine(Graphics g)
        {
            if (this.ParentMenuItem == null)
                return;

            Color color;
            if (this.popupMenu == null)
                color = this.BackColor;
            else
                color = this.popupMenu.BackColor;
            using (Pen pen = new Pen(color, 0))
            {
                g.DrawLine(pen, 1, 0, this.ParentMenuItem.ClientRectangle.Width - 2, 0);
            }
        }








        private void DrawSubMenuArrow(Graphics g, WDMenuItem item, Color color)
        {
            var tSize = GetTextSize(g, item.Text);
            int x = tSize.Width + (item.ClientRectangle.Width - tSize.Width) / 2;
            int y = item.ClientRectangle.Top + item.ClientRectangle.Height / 2;
            //using (Pen pen = new Pen(color, 0))
            //{
            //    g.DrawLine(pen, x, y, x, y + 4);
            //    g.DrawLine(pen, x + 1, y + 1, x + 1, y + 3);
            //    g.DrawLine(pen, x + 2, y + 2, x + 1, y + 2);
            //}  
            var img = IconSvg.SVGIcons.Icon(IconSvg.IconNames.play_fill, color, 20);
            g.DrawImage(img, new Point(x, y - img.Height / 2));
        }



        private Size GetTextSize(Graphics g, string text)
        {
            SizeF textSize = g.MeasureString(text, this.Font);
            SizeF hoverTextSize = g.MeasureString(text, this.HoverFont);
            return new Size((int)(Math.Max(textSize.Width, hoverTextSize.Width)), (int)(Math.Max(textSize.Height, hoverTextSize.Height)));
        }

        private void DrawIconImage(Graphics g, WDMenuItem item)
        {
            if (string.IsNullOrWhiteSpace(item.MenuInfo.IconName)) return;
            var tSize = GetTextSize(g, item.Text);
            int x = (item.ClientRectangle.Width - tSize.Width) / 2;
            int y = (item.ClientRectangle.Height - tSize.Height) / 2;
            var img = WinDoControls.FontImages.GetImage((WinDoControls.FontIcons)Enum.Parse(typeof(WinDoControls.FontIcons), item.MenuInfo.IconName), 14, this.currMenuItem == item ? this.BackColor : Color.White);
            g.DrawImage(img, new Point(item.ClientRectangle.X + x - 11, item.ClientRectangle.Y + y));
        }







        private void DrawCheck(Graphics g, Color color, WDMenuItem item)
        {
            const int size = 11;

            int x = item.ClientRectangle.X - WDPopupMenu.checkAreaWidth - 3;
            int y = item.ClientRectangle.Y + item.ClientRectangle.Height / 2 - size / 2;

            Rectangle r = new Rectangle(x, y, size, size);
            this.DrawBackground(g, r, this.BackColor);

            using (Pen pen = new Pen(color, 0))
            {
                g.DrawRectangle(pen, r);
                if (item.Checked)
                {
                    SmoothingMode oldMode = g.SmoothingMode;
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.DrawLine(pen, x + 2, y + 7, x + 4, y + 9);
                    g.DrawLine(pen, x + 4, y + 9, x + 9, y + 4);
                    g.SmoothingMode = oldMode;
                }
            }
        }







        private void DrawRadio(Graphics g, Color color, WDMenuItem item)
        {
            if (!item.Radio)
                return;

            const int size = 5;

            int x = item.ClientRectangle.X - WDPopupMenu.checkAreaWidth / 2 - size / 2 - 5;
            int y = item.ClientRectangle.Y + item.ClientRectangle.Height / 2 - size / 2;

            Rectangle r = new Rectangle(x, y, size, size);
            using (Brush brush = new SolidBrush(color))
            {
                g.FillRectangle(brush, r);
            }
        }







        private bool IsMouseNearSubMenuArrow(int x, int y)
        {
            if (this.currMenuItem == null || this.currMenuItem.MenuItems.Count == 0)
                return false;

            int xtol = 15;
            Rectangle rect = this.currMenuItem.ClientRectangle;
            if (rect.Width < xtol)
                return false;
            rect.X = rect.Right - xtol;
            rect.Width = xtol;
            if (rect.Contains(x, y))
                return true;

            return false;
        }
        #endregion Private methods

        #region AwesomeMenu overrides




        protected override void DrawMenu(Graphics g)
        {

            DrawBackground(g, this.ClientRectangle, this.BackColor);
            if (this.HasCheckOrRadioItems())
            {

                Rectangle r = new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Y,
                    WDPopupMenu.checkAreaWidth + 4, this.ClientRectangle.Height);
                DrawBackground(g, r, this.BackColor);
            }


            if (this.EnableBorderDrawing)
                DrawBorder(g, this.ClientRectangle, this.BorderColor);
            if (this.ParentMenu != null && !this.ParentMenu.IsPopupMenu)
                DrawPartialLine(g);


            for (int i = 0; i < this.MenuItems.Count; ++i)
            {
                WDMenuItem item = this.MenuItems.GetAt(i);
                Debug.Assert(item != null);

                DrawMenuItem(g, item);
            }
        }







        protected override void RepositionMenuItems(Graphics g)
        {
            int xoffset = this.LeftMargin;
            int yoffset = this.TopMargin;

            if (this.HasCheckOrRadioItems())
                xoffset += WDPopupMenu.checkAreaWidth + 3;

            var txtHeight = g.MeasureString(" ", this.Font).Height;
            var allHeight = this.MenuItems.Count * (txtHeight + ItemPaddingH + 4);


            for (int i = 0; i < this.MenuItems.Count; ++i)
            {
                WDMenuItem item = this.MenuItems.GetAt(i);
                Debug.Assert(item != null);

                string text = item.Text;
                if (text == String.Empty)
                    text = " ";
                SizeF textSize = g.MeasureString(text, this.Font);
                SizeF hoverTextSize = g.MeasureString(text, this.HoverFont);
                Rectangle rect = new Rectangle(xoffset, yoffset,
                    Math.Max((int)(Math.Max(textSize.Width, hoverTextSize.Width) + 50), ItemMaxWidth),
                    (int)(Math.Max(textSize.Height, hoverTextSize.Height) + ItemPaddingH));

                AdjustMenuItemRect(ref rect);
                if (item.Style == WDMenuItemStyle.Separator)
                {
                    rect.Offset(0, -this.MenuItemSpacing / 2);
                    rect.Height = 3;
                }
                item.ClientRectangle = rect;

                if (item.Style == WDMenuItemStyle.Separator)
                    yoffset += rect.Height;
                else
                    yoffset += (this.MenuItemSpacing + (int)rect.Height);
            }

            var col = 1;
            var curScreenHeight = Screen.FromControl(this).WorkingArea.Height;
            if (this.Bottom > curScreenHeight)
            {
                col = 2;
            }
            AdjustMenuSize(col);
            if (col == 2)
            {
                //调整宽度
                var w = this.Width / 2;
                var oy = curScreenHeight - this.Top;
                Rectangle firstResetRect = Rectangle.Empty;
                for (int j = 0; j < this.MenuItems.Count; ++j)
                {
                    WDMenuItem item = this.MenuItems.GetAt(j);
                    Rectangle rect = item.ClientRectangle;
                    if (rect.Bottom > oy)
                    {
                        if (firstResetRect == Rectangle.Empty)
                        {
                            firstResetRect = rect;
                        }
                        rect.Offset(w, -firstResetRect.Y);
                    }
                    rect.Width = w;
                    item.ClientRectangle = rect;
                }

            }
            else
            {
                for (int j = 0; j < this.MenuItems.Count; ++j)
                {
                    WDMenuItem item = this.MenuItems.GetAt(j);
                    Rectangle rect = item.ClientRectangle;
                    rect.Width = this.ClientRectangle.Width - rect.X;
                    item.ClientRectangle = rect;
                }
            }
        }




        protected override void CreatePopupMenu()
        {
            if (this.popupMenu == null)
            {
                popupMenu = new WDPopupMenu();
            }
            popupMenu.BackColor = this.BackColor;
            popupMenu.BorderColor = this.BorderColor;
            popupMenu.SeparatorColor = this.SeparatorColor;
            popupMenu.TextColor = this.TextColor;

            popupMenu.HoverBackGradientColor = this.HoverBackGradientColor;
            popupMenu.HoverBorderColor = this.HoverBorderColor;
            popupMenu.HoverTextColor = this.HoverTextColor;

            popupMenu.DisabledTextColor = this.DisabledTextColor;

            popupMenu.Font = this.Font.Clone() as Font;
            popupMenu.HoverFont = this.HoverFont.Clone() as Font;

            popupMenu.EnableBorderDrawing = this.EnableBorderDrawing;
            popupMenu.EnableHoverBorderDrawing = this.EnableHoverBorderDrawing;
            popupMenu.EnableHoverBackDrawing = this.EnableHoverBackDrawing;
            popupMenu.LeftMargin = this.LeftMargin;
            popupMenu.TopMargin = this.TopMargin;
            popupMenu.MenuItemSpacing = this.MenuItemSpacing;

            popupMenu.MenuTimerInterval = this.MenuTimerInterval;
            popupMenu.ItemPaddingH = this.ItemPaddingH;
            popupMenu.Cursor = this.Cursor;
        }





        protected override void GetPopupMenuScreenLocation(ref Point ptScreen)
        {
            if (this.currMenuItem == null)
                return;

            Point ptClient = new Point(this.ClientRectangle.Right, this.currMenuItem.ClientRectangle.Top);


            ptScreen = this.PointToScreen(ptClient);
        }
        #endregion AwesomeMenu overrides

        #region Mouse event handlers





        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (this.IsMouseNearSubMenuArrow(e.X, e.Y))
            {
                this.ShowPopupMenu();
            }
        }
        #endregion Mouse event handlers
    }







    public class WDMenuBar : WinDoControls.Controls.WDMenu
    {
        #region Properties






        [Category("Behavior"),
         DefaultValue(false),
         Description("Enable or disable auto-showing of top-level popup menus.")]
        public bool AlwaysShowPopupMenu
        {
            get { return this.alwaysShowPopup; }
            set { this.alwaysShowPopup = value; }
        }
        #endregion Properties

        #region Constructor
        public WDMenuBar()
        {
            TopLevel = false;
            this.isPopupStyleMenu = false;
            SizeChanged += new EventHandler(AwesomeMenuBar_SizeChanged);
        }

        public static string MoreItemText = "更多";

        private WDMenuItemList HideMenuItems = new WDMenuItemList();
        private WDMenuItem MoreItem = new WDMenuItem(new WDMenuInfo() { Text = MoreItemText, IconName = "I_more" });

        void AwesomeMenuBar_SizeChanged(object sender, EventArgs e)
        {
            return;
        }

        protected override void UpdateCurrentMenuItem(int x, int y)
        {
            WDMenuItem newItem = null;



            for (int i = 0; i < this.MenuItems.Count; ++i)
            {
                WDMenuItem item = this.MenuItems.GetAt(i);
                Debug.Assert(item != null);

                if (!HideMenuItems.HasItem(item) && item.Selectable &&
                     item.Enabled &&
                     item.ClientRectangle.Contains(x, y))
                {
                    newItem = item;
                    break;
                }
            }
            if (newItem != null)
            {
                this.currMenuItem = newItem;
                this.NotifyCurrentMenuItemChange();
            }
        }
        #endregion Constructor

        #region Protected Dispose method



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }
            base.Dispose(disposing);
        }
        #endregion Protected Dispose method

        #region Private methods




        private void DrawDesignMode(Graphics g)
        {
            if (!this.DesignMode)
                return;

            string text = "AwesomeMenuBar";
            this.DrawMenuItemText(g, text, this.Font,
                this.TextColor, this.LeftMargin, this.TopMargin);
        }










        private void DrawMenuItemTextR(Graphics g, string text, Font font, Color color, Rectangle r, WDMenuItem item)
        {
            using (Brush brush = new SolidBrush(color))
            {
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;

                Rectangle rnew = new Rectangle(r.X + (item.MenuItems != null && item.MenuItems.Count == 0 ? 10 : 6), r.Y, r.Width, r.Height);
                g.DrawString(text, font, brush, (RectangleF)rnew, sf);
            }

            DrawIconImage(g, item);
            if (item.MenuItems.Count > 0)
                this.DrawSubMenuArrow(g, item, this.currMenuItem == item ? this.HoverTextColor : this.TextColor);

        }

        private void DrawSubMenuArrow(Graphics g, WDMenuItem item, Color color)
        {
            var tSize = GetTextSize(g, item.Text);
            int x = item.ClientRectangle.Left + tSize.Width + (item.ClientRectangle.Width - tSize.Width) / 2 + 4;
            var img = WinDoControls.FontImages.GetImage((WinDoControls.FontIcons)Enum.Parse(typeof(WinDoControls.FontIcons), "E_arrow_triangle_down"), (int)this.Font.Size, color);
            int y = item.ClientRectangle.Top + (item.ClientRectangle.Height - img.Height) / 2;
            g.DrawImage(img, new Point(x, y));
        }

        private Size GetTextSize(Graphics g, string text)
        {
            SizeF textSize = g.MeasureString(text, this.Font);
            SizeF hoverTextSize = g.MeasureString(text, this.HoverFont);
            return new Size((int)(Math.Max(textSize.Width, hoverTextSize.Width)), (int)(Math.Max(textSize.Height, hoverTextSize.Height)));
        }

        private void DrawIconImage(Graphics g, WDMenuItem item)
        {
            if (string.IsNullOrWhiteSpace(item.MenuInfo.IconName)) return;
            var tSize = GetTextSize(g, item.Text);
            int x = (item.ClientRectangle.Width - tSize.Width) / 2;
            try
            {
                var img = WinDoControls.FontImages.GetImage((WinDoControls.FontIcons)Enum.Parse(typeof(WinDoControls.FontIcons), item.MenuInfo.IconName), (int)this.Font.Size, this.currMenuItem == item ? this.BackColor : Color.White);
                g.DrawImage(img, new Point(item.ClientRectangle.X + (x - img.Width) / 2 + 6, item.ClientRectangle.Y + (item.ClientRectangle.Height - img.Height) / 2));
            }
            catch (Exception)
            {
            }
        }






        private void DrawMenuItem(Graphics g, WDMenuItem item)
        {
            if (item.ClientRectangle.Width < 2)
                return;
            if (MoreItem == item && !(this.Width < this.MenuItems.SumWidth()))
            {
                HideMenuItems.Clear();
                this.MenuItems.Remove(MoreItem);
                return;
            }
            if (HideMenuItems.HasItem(item)) return;

            if (item.Style == WDMenuItemStyle.Separator)
            {
                int x = (item.ClientRectangle.Left + item.ClientRectangle.Right) / 2;
                int y1 = item.ClientRectangle.Top + 3;
                int y2 = item.ClientRectangle.Bottom - 3;
                this.DrawSeparator(g, this.SeparatorColor, x, y1, x, y2);
            }
            else if (this.currMenuItem == item && !this.ignoreNextMouseMove)
            {
                if (this.IsPopupMenuVisible() && this.currMenuItem.MenuItems.Count > 0)
                {
                    this.DrawBackground(g, item.ClientRectangle, this.HoverBackGradientColor);
                    if (this.EnableBorderDrawing)
                        this.DrawBorder(g, item.ClientRectangle, this.popupMenu.BorderColor);

                    this.DrawMenuItemTextR(g, item.Text, this.HoverFont, this.HoverTextColor, item.ClientRectangle, item);
                }
                else
                {
                    if (this.EnableHoverBackDrawing)
                        this.DrawBackground(g, item.ClientRectangle, this.HoverBackGradientColor);
                    if (this.EnableHoverBorderDrawing)
                        this.DrawBorder(g, item.ClientRectangle, this.HoverBorderColor);
                    this.DrawMenuItemTextR(g, item.Text, this.HoverFont, this.HoverTextColor, item.ClientRectangle, item);
                }
            }
            else
            {
                Color color = this.TextColor;
                if (!item.Enabled)
                    color = this.DisabledTextColor;
                if (this.EnableBorderDrawing)
                    this.DrawBorder(g, item.ClientRectangle, this.BorderColor);
                this.DrawMenuItemTextR(g, item.Text, this.Font, color, item.ClientRectangle, item);
            }
        }






        private void AdjustMenuItemRect(ref Rectangle rect)
        {
            return;
            rect.Y -= 2;
            rect.Height += 4;
            rect.X -= 3;
            rect.Width += 7;


            if (this.Font != null && this.Font.Bold)
                rect.Width += 3;
            else if (this.HoverFont != null && this.HoverFont.Bold)
                rect.Width += 3;
        }
        #endregion Private methods

        #region WinDoMenu overrides




        protected override void DrawMenu(Graphics g)
        {

            DrawBackground(g, this.ClientRectangle, this.BackColor);


            if (this.EnableBorderDrawing)
                DrawBorder(g, this.ClientRectangle, this.BorderColor);


            if (this.DesignMode)
            {
                this.DrawDesignMode(g);
                return;
            }


            for (int i = 0; i < this.MenuItems.Count; ++i)
            {
                WDMenuItem item = this.MenuItems.GetAt(i);
                Debug.Assert(item != null);

                DrawMenuItem(g, item);
            }
        }





        protected override void RepositionMenuItems(Graphics g)
        {
            if (!this.MenuItems.HasItem(MoreItem))
                this.MenuItems.Add(MoreItem);

            int xoffset = this.LeftMargin;
            int yoffset = this.TopMargin;

            for (int i = 0; i < this.MenuItems.Count; ++i)
            {
                WDMenuItem item = this.MenuItems.GetAt(i);
                Debug.Assert(item != null);

                string text = item.Text;
                if (text == String.Empty)
                    text = " ";
                SizeF textSize = g.MeasureString(text, this.Font);
                SizeF hoverTextSize = g.MeasureString(text, this.HoverFont);
                var widthT = Math.Max((int)(Math.Max(textSize.Width, hoverTextSize.Width) + 50), ItemMaxWidth);
                //var mxaSubWidth = Math.Max((int)(g.MeasureString(item.MenuItems.GetMaxLengthText(), this.Font).Width) + 50, ItemMaxWidth);
                //var width = Math.Max(widthT, mxaSubWidth);
                var width = widthT;// + ((item.MenuItems == null || item.MenuItems.Count == 0) ? 0 : 20);

                Rectangle rect = new Rectangle(xoffset, yoffset,
                  width,
                    (int)(Math.Max(textSize.Height, hoverTextSize.Height) + 28));

                AdjustMenuItemRect(ref rect);
                if (item.Style == WDMenuItemStyle.Separator)
                {
                    rect.Offset(-this.MenuItemSpacing / 2, 0);
                    rect.Width = 3;
                }
                item.ClientRectangle = rect;

                if (item.Style == WDMenuItemStyle.Separator)
                    xoffset += rect.Width;
                else
                    xoffset += (this.MenuItemSpacing + rect.Width);
            }

            ShowMoreItem();
        }

        void ShowMoreItem()
        {

            HideMenuItems.Clear();
            if (this.Width < this.MenuItems.SumWidth())
            {
                int width = 0;
                for (int i = 0; i < this.MenuItems.Count - 1; i++)
                {
                    var item = this.MenuItems.GetAt(i);
                    width += item.ClientRectangle.Width;
                    if (this.Width <= width + ItemMaxWidth)
                        HideMenuItems.Add(item);
                }
                this.MoreItem.MenuItems = CopyMenuItems(HideMenuItems);
                MoreItem.ClientRectangle = new Rectangle(HideMenuItems.GetAt(0).ClientRectangle.X, 0, MoreItem.ClientRectangle.Width, MoreItem.ClientRectangle.Height);
            }
        }

        private WDMenuItemList CopyMenuItems(WDMenuItemList menuItemList)
        {
            var list = new WDMenuItemList();
            for (int i = 0; i < menuItemList.Count; i++)
            {
                var item = menuItemList.GetAt(i);
                var newItem = new WDMenuItem() { Text = item.Text, MenuInfo = item.MenuInfo, MenuItems = item.MenuItems };
                if (item.HasClickHandler)
                    newItem.Click += new EventHandler((sender, args) => { item.NotifyClickEvent(); });
                list.Add(newItem);
            }
            return list;
        }




        protected override void CreatePopupMenu()
        {
            if (this.popupMenu == null)
            {
                popupMenu = new WDPopupMenu();
            }

            popupMenu.BorderColor = this.HoverBorderColor;
            popupMenu.SeparatorColor = this.SeparatorColor;
            popupMenu.TextColor = this.TextColor;

            popupMenu.HoverBackGradientColor = this.HoverBackGradientColor;
            popupMenu.HoverBorderColor = this.HoverBorderColor;
            popupMenu.HoverTextColor = this.HoverTextColor;

            popupMenu.DisabledTextColor = this.DisabledTextColor;

            popupMenu.Font = this.Font.Clone() as Font;
            popupMenu.HoverFont = this.HoverFont.Clone() as Font;

            popupMenu.EnableBorderDrawing = this.EnableBorderDrawing;
            popupMenu.EnableHoverBorderDrawing = this.EnableHoverBorderDrawing;
            popupMenu.EnableHoverBackDrawing = this.EnableHoverBackDrawing;
            popupMenu.TopMargin = this.TopMargin;
            popupMenu.LeftMargin = this.LeftMargin;

            popupMenu.MenuTimerInterval = this.MenuTimerInterval;

            popupMenu.Cursor = this.Cursor;
        }





        protected override void GetPopupMenuScreenLocation(ref Point ptScreen)
        {
            if (this.currMenuItem == null)
                return;

            Rectangle rect = this.currMenuItem.ClientRectangle;
            Point ptClient = rect.Location;
            ptClient.Y += rect.Height;
            ptScreen = this.PointToScreen(ptClient);
        }
        #endregion AwesomeMenu overrides

        #region Mouse event handlers




        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button != MouseButtons.Left)
                return;

            if (this.currMenuItem == null ||
                 this.currMenuItem.MenuItems.Count == 0 ||
                 this.AlwaysShowPopupMenu)
                return;


            if (this.needShowPopupMenu)
            {
                this.needShowPopupMenu = false;
                this.Invalidate();
                this.HidePopupMenu();
            }
            else
            {
                this.needShowPopupMenu = true;
                if (currMenuItem != null && !currMenuItem.ClientRectangle.Contains(e.X, e.Y))
                {
                    currMenuItem = null;
                    this.Invalidate();
                    this.HidePopupMenu();
                    return;
                }
                this.Invalidate();

                this.ShowPopupMenu();
            }
        }
        #endregion Mouse event handlers
    }
}



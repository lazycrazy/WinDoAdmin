


using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using WinDoControls.Forms;

namespace WinDoControls.Controls
{





    public enum WDMenuAlignment
    {
        CenterAlign,
        LeftAlign,
        RightAlign,
        BottomAlign,
        TopAlign,
        VCenterAlign
    }





    public class WDMenuItemList
    {
        #region Constants
        private static readonly int MaxItemCount = 50;
        #endregion Constants

        #region Private fields
        private IList<WDMenuItem> menuItems = new List<WDMenuItem>();
        #endregion Private fields

        #region Properties



        public int Count
        {
            get { return this.menuItems.Count; }
        }
        #endregion Properties

        #region Constructor
        public WDMenuItemList()
        {
        }
        #endregion Constructor

        #region Public methods






        public bool Add(WDMenuItem item)
        {
            if (!CanAddItem())
                return false;

            this.menuItems.Add(item);
            return true;
        }








        public WDMenuItem Add(string text, System.EventHandler clickHandler)
        {
            if (!CanAddItem())
                return null;

            WDMenuItem item = new WDMenuItem();
            item.Text = text;
            if (clickHandler != null)
                item.Click += clickHandler;
            if (!this.Add(item))
                return null;

            return item;
        }









        public bool AddSeparator()
        {
            if (!CanAddItem())
                return false;

            WDMenuItem item = new WDMenuItem();
            item.Selectable = false;
            item.Style = WDMenuItemStyle.Separator;
            item.Text = String.Empty;
            if (!this.Add(item))
                return false;

            return true;
        }



        public void Clear()
        {
            this.menuItems.Clear();
        }

        public int SumWidth()
        {
            return this.menuItems.Where(m => m.MenuInfo.Text != WDMenuBar.MoreItemText).Sum(m => m.ClientRectangle.Width);
        }


        public bool HasItem(WDMenuItem item)
        {
            return this.menuItems.Contains(item);
        }



        public string GetMaxLengthText()
        {
            if (menuItems.Count == 0) return "";
            var maxlen = menuItems.Max(m => m.Text.Length);
            return menuItems.First(m => m.Text.Length == maxlen).Text;
        }


        public WDMenuItem GetAt(int index)
        {
            WDMenuItem item = null;
            if (0 <= index && index < this.menuItems.Count)
            {
                item = this.menuItems[index];
            }
            return item;
        }






        public bool Remove(WDMenuItem item)
        {
            if (!this.menuItems.Contains(item))
                return false;

            this.menuItems.Remove(item);
            return true;
        }






        public bool RemoveAt(int index)
        {
            if (index < 0 || index >= this.menuItems.Count)
                return false;

            this.menuItems.RemoveAt(index);
            return true;
        }
        #endregion Public methods

        #region Private methods




        private bool CanAddItem()
        {
            if (this.menuItems.Count < WDMenuItemList.MaxItemCount)
                return true;

            return false;
        }
        #endregion Private methods

        #region ToString override





        public override string ToString()
        {
            if (this.menuItems.Count == 0)
            {
                return "AwesomeMenuItemList: Empty";
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("AwesomeMenuItemList:");
                foreach (object obj in this.menuItems)
                {
                    WDMenuItem item = obj as WDMenuItem;
                    sb.Append('\n');
                    sb.Append(item.ToString());
                }
                return sb.ToString();
            }
        }
        #endregion ToString override
    }





    public enum WDMenuItemStyle
    {
        Regular,
        Separator,
        Check,
        Radio
    }

    public class WDMenuInfo
    {
        public string Text { get; set; }
        public string IconName { get; set; }
        public string CommandText { get; set; }
        public string FormName { get; set; }
        public string AssemblyName { get; set; }
        public string ModuleFormCode { get; set; }
        public Control RelationForm { get; set; }
    }








    public class WDMenuItem
    {
        #region Events
        public event System.EventHandler Click;
        #endregion Events

        #region Private fields
        private WDMenuItemStyle style = WDMenuItemStyle.Regular;

        private Rectangle clientRectangle;

        private bool enabled = true;
        private bool selectable = true;

        private bool isChecked = false;
        private bool radio = false;


        private WDMenuItemList menuItems = new WDMenuItemList();
        #endregion Private fields

        #region Properties

        public WDMenuInfo MenuInfo { get; set; }




        public WDMenuItemStyle Style
        {
            get { return this.style; }
            set { this.style = value; }
        }




        public Rectangle ClientRectangle
        {
            get { return this.clientRectangle; }
            set { this.clientRectangle = value; }
        }








        public bool Enabled
        {
            get { return this.enabled; }
            set { this.enabled = value; }
        }








        public bool Selectable
        {
            get { return this.selectable; }
            set { this.selectable = value; }
        }




        public bool Checked
        {
            get { return this.isChecked; }
            set { this.isChecked = value; }
        }




        public bool Radio
        {
            get { return this.radio; }
            set { this.radio = value; }
        }


        public object Data { get; set; }

        public string Key { get; set; }


        public string Text { get; set; }




        public WDMenuItemList MenuItems
        {
            get { return this.menuItems; }
            set { this.menuItems = value; }
        }




        public bool HasClickHandler
        {
            get { return this.Click != null; }
        }
        #endregion Properties

        #region Constructor
        public WDMenuItem()
        {
        }

        public WDMenuItem(WDMenuInfo info)
        {
            this.MenuInfo = info;
            this.Text = info.Text;
        }
        #endregion Constructor

        #region Public methods

        public WDMenuItem AddChildItem(WDMenuInfo info)
        {
            var newItem = new WDMenuItem(info);
            this.MenuItems.Add(newItem);
            return newItem;
        }






        public void NotifyClickEvent()
        {
            if (this.Enabled && this.Selectable)
            {
                System.EventHandler tmpClick = this.Click;
                if (tmpClick != null)
                    tmpClick(this, System.EventArgs.Empty);
            }
        }
        #endregion Public methods

        #region ToString override




        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (this.Style == WDMenuItemStyle.Separator)
                sb.Append("Separator");
            else
                sb.Append(this.Text);

            return sb.ToString();
        }
        #endregion ToString override
    }








    public class CurrentMenuItemChangeEventArgs : System.EventArgs
    {
        #region Private fields
        private WDMenuItem currentMenuItem;
        #endregion Private fields

        #region Properties





        public WDMenuItem CurrentMenuItem
        {
            get { return this.currentMenuItem; }
            set { this.currentMenuItem = value; }
        }
        #endregion Properties

        #region Constructor
        public CurrentMenuItemChangeEventArgs()
        {
        }
        #endregion Constructor
    }






    public delegate void CurrentMenuItemChangeEventHandler(object sender, CurrentMenuItemChangeEventArgs e);





    public class WinDoMenuDesigner : System.Windows.Forms.Design.ControlDesigner
    {
        #region PreFilterProperties method





        protected override void PreFilterProperties(System.Collections.IDictionary properties)
        {
            properties.Remove("BackColor");
            properties.Remove("ForeColor");
            properties.Remove("Text");
        }
        #endregion PreFilterProperties method
    }









    [DesignerAttribute(typeof(WinDoMenuDesigner))]
    [ToolboxItem(false)]
    public abstract class WDMenu : System.Windows.Forms.Form
    {
        #region Events





        protected event System.EventHandler RequestHide;








        public event WinDoControls.Controls.CurrentMenuItemChangeEventHandler CurrentMenuItemChange;
        #endregion Events

        #region Static fields
        protected static int ItemMaxWidth = 80;
        private static int nextMenuId = 1;
        private static readonly Color defaultBackGradientColor = (WinDo.Utilities.PublicResource.WDColors.geekblue6);
        private static readonly Color defaultHoverBackGradientColor = (Color.White);
        #endregion Static fields

        #region Private fields
        private int menuId;

        private Color backGradientColor;
        private Color borderColor;
        private Color separatorColor;
        private Color textColor;

        private Color hoverBackGradientColor;
        private Color hoverBorderColor;
        private Color hoverTextColor;

        private Color disabledTextColor;

        private Font hoverFont;

        private bool enableBorderDrawing;
        private bool enableHoverBorderDrawing;
        private bool enableHoverBackDrawing;

        private int leftMargin;
        private int topMargin;
        private int menuItemSpacing;

        private bool isMouseEntered;
        private bool isMousePressed;

        private int menuTimerInterval;

        private WDMenu parentMenu;
        private WDMenuItem parentMenuItem;

        private WDMenuItemList menuItems;
        #endregion Private fields

        #region Protected fields
        protected System.Windows.Forms.Timer menuTimer;

        protected WDMenuItem currMenuItem;

        protected bool alwaysShowPopup;
        protected bool needShowPopupMenu;
        protected WDMenu popupMenu;
        protected bool isPopupStyleMenu;

        protected bool ignoreNextMouseMove;
        #endregion Protected fields

        #region Properties

        public int ItemPaddingH { get; set; } = 28;





        [BrowsableAttribute(false)]
        public int MenuId
        {
            get { return this.menuId; }
        }





        [Category("Appearance"),
         Description("The background color for the menu.")]
        public Color BackColor
        {
            get { return this.backGradientColor; }
            set
            {
                if (this.backGradientColor != value)
                {
                    this.backGradientColor = value;
                    this.InvalidateMenu();
                }
            }
        }




        [Category("Appearance"),
         Description("The border color for the menu.")]
        public Color BorderColor
        {
            get { return this.borderColor; }
            set { this.UpdateColorProperty(ref this.borderColor, value); }
        }






        [Category("Appearance"),
         Description("The color used to draw separator menu items.")]
        public Color SeparatorColor
        {
            get { return this.separatorColor; }
            set { this.UpdateColorProperty(ref this.separatorColor, value); }
        }





        [Category("Appearance"),
         Description("The color used to draw menu item text.")]
        public Color TextColor
        {
            get { return this.textColor; }
            set { this.UpdateColorProperty(ref this.textColor, value); }
        }





        [Category("Appearance"),
         Description("The background color for the hover state.")]
        public Color HoverBackGradientColor
        {
            get { return this.hoverBackGradientColor; }
            set
            {
                if (this.hoverBackGradientColor != value)
                {
                    this.hoverBackGradientColor = value;
                    this.InvalidateMenu();
                }
            }
        }





        [Category("Appearance"),
         Description("The border color of a menu item in the hover (highlighted) state.")]
        public Color HoverBorderColor
        {
            get { return this.hoverBorderColor; }
            set { this.UpdateColorProperty(ref this.hoverBorderColor, value); }
        }





        [Category("Appearance"),
         Description("The text color for a menu item in the hover (highlighted) state.")]
        public Color HoverTextColor
        {
            get { return this.hoverTextColor; }
            set { this.UpdateColorProperty(ref this.hoverTextColor, value); }
        }




        [Category("Appearance"),
         Description("The text color for a disabled menu item.")]
        public Color DisabledTextColor
        {
            get { return this.disabledTextColor; }
            set { this.UpdateColorProperty(ref this.disabledTextColor, value); }
        }






        [Category("Appearance"),
         Description("The font for drawing menu item text while in the hover (highlighted) state.")]
        public Font HoverFont
        {
            get { return this.hoverFont; }
            set
            {
                if (this.hoverFont != null)
                    this.hoverFont.Dispose();
                this.hoverFont = value;
                this.InvalidateMenu();
            }
        }







        [Category("Appearance"),
         DefaultValue(true),
         Description("Enable or disable drawing of menu border.")]
        public bool EnableBorderDrawing
        {
            get { return this.enableBorderDrawing; }
            set { this.UpdateBoolProperty(ref this.enableBorderDrawing, value); }
        }




        [Category("Appearance"),
         DefaultValue(true),
         Description("Enable or disable drawing of hover rectangle border.")]
        public bool EnableHoverBorderDrawing
        {
            get { return this.enableHoverBorderDrawing; }
            set { this.UpdateBoolProperty(ref this.enableHoverBorderDrawing, value); }
        }







        [Category("Appearance"),
         DefaultValue(true),
         Description("Enable or disable drawing of the hover state background color.")]
        public bool EnableHoverBackDrawing
        {
            get { return this.enableHoverBackDrawing; }
            set { this.UpdateBoolProperty(ref this.enableHoverBackDrawing, value); }
        }






        [Category("Appearance"),
         DefaultValue(6),
         Description("Specify the x-offset in pixels to begin drawing text for the first menu item.")]
        public int LeftMargin
        {
            get { return this.leftMargin; }
            set
            {
                if (this.leftMargin != value)
                {
                    this.leftMargin = value;
                    this.InvalidateMenu();
                }
            }
        }






        [Category("Appearance"),
         DefaultValue(7),
         Description("Specify the y-offset in pixels to begin drawing text for the first menu item.")]
        public int TopMargin
        {
            get { return this.topMargin; }
            set
            {
                if (this.topMargin != value)
                {
                    this.topMargin = value;
                    this.InvalidateMenu();
                }
            }
        }





        [Category("Appearance"),
         DefaultValue(4),
         Description("Specify the spacing between two menu items in pixels.")]
        public int MenuItemSpacing
        {
            get { return this.menuItemSpacing; }
            set
            {
                if (this.menuItemSpacing != value)
                {
                    this.menuItemSpacing = value;
                    this.InvalidateMenu();
                }
            }
        }




        [BrowsableAttribute(false)]
        protected bool IsMouseEntered
        {
            get { return this.isMouseEntered; }
        }




        [BrowsableAttribute(false)]
        protected bool IsMousePressed
        {
            get { return this.isMousePressed; }
        }





        [Category("Behavior"),
         DefaultValue(500),
         Description("The timer interval in milliseconds for auto-closing a menu.")]
        public int MenuTimerInterval
        {
            get { return this.menuTimerInterval; }
            set { this.menuTimerInterval = value; }
        }




        [BrowsableAttribute(false)]
        public bool IsPopupMenu
        {
            get { return this.isPopupStyleMenu; }
        }




        [BrowsableAttribute(false)]
        public WDMenu Popup
        {
            get
            {
                if (this.popupMenu == null)
                {
                    this.CreatePopupMenu();
                    if (this.popupMenu != null)
                        this.popupMenu.RequestHide += new System.EventHandler(this.popupMenu_RequestHide);
                }
                return this.popupMenu;
            }
        }





        [BrowsableAttribute(false)]
        public WDMenu ParentMenu
        {
            get { return this.parentMenu; }
            set { this.parentMenu = value; }
        }





        [BrowsableAttribute(false)]
        public WDMenuItem ParentMenuItem
        {
            get { return this.parentMenuItem; }
            set { this.parentMenuItem = value; }
        }




        [BrowsableAttribute(false)]
        public WDMenuItemList MenuItems
        {
            get { return this.menuItems; }
            set { this.menuItems = value; }
        }
        #endregion Properties

        #region ShouldSerialize property methods





        private bool ShouldSerializeBackGradientColor()
        {

            if (this.BackColor == WDMenu.defaultBackGradientColor)
                return false;

            return true;
        }






        private bool ShouldSerializeHoverBackGradientColor()
        {

            if (this.HoverBackGradientColor == WDMenu.defaultHoverBackGradientColor)
                return false;

            return true;
        }
        #endregion ShouldSerialize property methods

        #region Reset property methods



        private void ResetBackGradientColor()
        {
            this.BackColor = WDMenu.defaultBackGradientColor;
        }




        private void ResetHoverBackGradientColor()
        {
            this.HoverBackGradientColor = WDMenu.defaultHoverBackGradientColor;
        }
        #endregion Reset property methods

        #region Constructor



        protected WDMenu()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.ShowInTaskbar = false;
            this.Padding = new Padding();
            this.menuId = WDMenu.nextMenuId++;

            this.backGradientColor = WDMenu.defaultBackGradientColor;
            this.borderColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
            this.separatorColor = Color.FromArgb(192, 192, 192);
            this.textColor = Color.FromArgb(192, 192, 192);

            this.hoverBackGradientColor = WDMenu.defaultHoverBackGradientColor;
            this.hoverBorderColor = this.TextColor;
            this.hoverTextColor = this.TextColor;

            this.disabledTextColor = Color.FromArgb(141, 151, 165);

            this.Font = new Font("微软雅黑", 16, FontStyle.Regular, GraphicsUnit.Pixel);
            this.hoverFont = this.Font.Clone() as Font;

            this.enableBorderDrawing = false;
            this.enableHoverBorderDrawing = false;
            this.enableHoverBackDrawing = true;

            this.leftMargin = 0;
            this.topMargin = 0;
            this.menuItemSpacing = 4;

            this.isMouseEntered = false;
            this.isMousePressed = false;

            this.menuTimerInterval = 500;

            this.parentMenu = null;
            this.parentMenuItem = null;

            this.menuItems = new WDMenuItemList();


            this.menuTimer = new System.Windows.Forms.Timer();
            this.menuTimer.Tick += new System.EventHandler(this.MenuTimerCallback);

            this.currMenuItem = null;

            this.alwaysShowPopup = false;
            this.needShowPopupMenu = false;
            this.popupMenu = null;
            this.isPopupStyleMenu = false;

            this.ignoreNextMouseMove = false;

            this.RequestHide = null;
            this.CurrentMenuItemChange = null;


            this.TabStop = false;


            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

        }
        #endregion Constructor

        #region Protected Dispose method



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

                if (this.hoverFont != null)
                {
                    this.hoverFont.Dispose();
                    this.hoverFont = null;
                }
                if (this.menuTimer != null)
                {
                    this.menuTimer.Dispose();
                    this.menuTimer = null;
                }
                if (this.popupMenu != null)
                {
                    this.popupMenu.Dispose();
                    this.popupMenu = null;
                }
            }
            base.Dispose(disposing);
        }
        #endregion Protected Dispose method

        #region Protected helper methods






        protected bool IsMouseInPopupMenu()
        {
            if (this.popupMenu == null)
                return false;
            var popupMenu = this.popupMenu;
            if (popupMenu.IsMouseEntered)
                return true;

            return popupMenu.IsMouseInPopupMenu();
        }





        protected void ShowPopupMenu()
        {

            //this.HidePopupMenu();


            if (this.currMenuItem == null ||
                 this.currMenuItem.MenuItems.Count == 0 ||
                 !this.currMenuItem.Enabled ||
                 !this.currMenuItem.Selectable
                 )
            {
                return;
            }


            if (this.popupMenu == null)
            {
                this.CreatePopupMenu();
                if (this.popupMenu == null)
                    return;
                this.popupMenu.RequestHide += new System.EventHandler(this.popupMenu_RequestHide);
            }
            if (popupMenu.ParentMenu != this)
                popupMenu.ParentMenu = this;


            popupMenu.MenuItems.Clear();
            for (int i = 0; i < this.currMenuItem.MenuItems.Count; ++i)
            {
                WDMenuItem item = this.currMenuItem.MenuItems.GetAt(i);
                popupMenu.MenuItems.Add(item);
            }


            Point ptScreen = new Point(0, 0);
            this.GetPopupMenuScreenLocation(ref ptScreen);



            popupMenu.ParentMenuItem = this.currMenuItem;
            popupMenu.Location = ptScreen;
            //this.popupMenu.Visible = true;
            //this.popupMenu.BringToFront();
            var mainForm = this.FindForm();
            if (!mainForm.TopLevel)
                mainForm = mainForm.Parent.FindForm();
            if (!this.popupMenu.Visible)
                this.popupMenu.Show(mainForm);
        }





        protected void HidePopupMenu()
        {
            if (this.popupMenu == null)
                return;

            this.popupMenu.Close();
            this.popupMenu = null;
        }




        protected void DrawBackground(Graphics g, Rectangle rect, Color color)
        {
            if (rect.Width == 0 || rect.Height == 0)
                return;

            using (Brush brush = new SolidBrush(color))
            {
                g.FillRectangle(brush, rect);
            }
        }

        protected void DrawBorder(Graphics g, Rectangle rect, Color color)
        {
            if (rect.Width < 2 || rect.Height < 2)
                return;

            using (Pen pen = new Pen(color, 0))
            {
                Rectangle r = new Rectangle(rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
                g.DrawRectangle(pen, r);
            }
        }











        protected void DrawMenuItemText(Graphics g, string text, Font font, Color color, int x, int y)
        {
            using (Brush brush = new SolidBrush(color))
            {
                g.DrawString(text, font, brush, x, y);
            }
        }










        protected void DrawSeparator(Graphics g, Color color, int x1, int y1, int x2, int y2)
        {
            using (Pen pen = new Pen(color, 0))
            {
                g.DrawLine(pen, x1, y1, x2, y2);
            }
        }






        protected void StartMenuTimer()
        {
            if (this.menuTimer != null)
            {
                this.menuTimer.Stop();
                this.menuTimer.Interval = this.menuTimerInterval;
                this.menuTimer.Start();
            }
        }




        protected void StopMenuTimer()
        {
            if (this.menuTimer != null)
            {
                this.menuTimer.Stop();
            }
        }







        protected void UpdateBoolProperty(ref bool oldValue, bool newValue)
        {
            if (oldValue != newValue)
            {
                oldValue = newValue;
                this.InvalidateMenu();
            }
        }







        protected void UpdateColorProperty(ref Color oldColor, Color newColor)
        {
            if (oldColor != newColor)
            {
                oldColor = newColor;
                this.InvalidateMenu();
            }
        }







        protected virtual void UpdateCurrentMenuItem(int x, int y)
        {
            WDMenuItem newItem = null;



            for (int i = 0; i < this.MenuItems.Count; ++i)
            {
                WDMenuItem item = this.MenuItems.GetAt(i);
                Debug.Assert(item != null);

                if (item.Selectable &&
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





        protected bool IsPopupMenuVisible()
        {
            if (this.popupMenu == null)
                return false;

            return true;
        }





        protected WDMenu GetTopMenu()
        {
            WDMenu topMenu = null;
            WDMenu currMenu = this;
            while (currMenu != null)
            {
                topMenu = currMenu;
                currMenu = currMenu.ParentMenu;
            }

            return topMenu;
        }







        protected WDMenu GetTopPopupMenu()
        {
            WDMenu topPopup = null;
            WDMenu currMenu = this;
            while (currMenu != null && currMenu.IsPopupMenu)
            {
                topPopup = currMenu;
                currMenu = currMenu.ParentMenu;
            }

            return topPopup;
        }





        protected void NotifyCurrentMenuItemChange()
        {
            WDMenu topMenu = this.GetTopMenu();
            Debug.Assert(topMenu != null);
            if (topMenu.CurrentMenuItemChange != null) { topMenu.CurrentMenuItemChange(topMenu, new CurrentMenuItemChangeEventArgs() { CurrentMenuItem = this.currMenuItem }); };
        }





        protected void ResetCurrentMenuItem()
        {
            WDMenuItem prevItem = this.currMenuItem;
            this.currMenuItem = null;
            if (prevItem != null)
                this.NotifyCurrentMenuItemChange();
        }




        protected void InvalidateMenu()
        {
            if (this.Parent == null)
                return;

            this.Invalidate();
        }






        protected bool HasCheckOrRadioItems()
        {
            for (int i = 0; i < this.MenuItems.Count; ++i)
            {
                WDMenuItem item = this.MenuItems.GetAt(i);
                if (item.Style == WDMenuItemStyle.Check ||
                     item.Style == WDMenuItemStyle.Radio)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion Protected helper methods

        #region Mouse event handlers




        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            this.isMouseEntered = true;
            this.StopMenuTimer();
        }





        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            this.isMouseEntered = false;
            this.StartMenuTimer();
        }





        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            WDMenuItem prevItem = this.currMenuItem;
            UpdateCurrentMenuItem(e.X, e.Y);
            if (prevItem != this.currMenuItem)
            {
                this.Invalidate();
                if (this.needShowPopupMenu || this.alwaysShowPopup)
                {
                    if (prevItem != null && prevItem.MenuItems.Count > 0)
                    {
                        this.HidePopupMenu();
                    }
                    this.ShowPopupMenu();
                }
            }
        }





        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button != MouseButtons.Left)
                return;

            this.isMousePressed = true;
        }





        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.Button != MouseButtons.Left)
                return;

            this.isMousePressed = false;
            this.ignoreNextMouseMove = false;

            if (this.currMenuItem == null)
                return;




            if (this.currMenuItem.MenuItems.Count != 0)
                return;



            if (!this.currMenuItem.HasClickHandler)
                return;



            WDMenuItem currItem = this.currMenuItem;
            this.ResetCurrentMenuItem();
            this.Invalidate();


            if (this.parentMenuItem != null)
            {
                WDMenu topPopup = this.GetTopPopupMenu();
                if (topPopup != null)
                {
                    topPopup.HidePopupMenu();



                    System.EventHandler evh = topPopup.RequestHide;
                    if (evh != null)
                        evh(topPopup, EventArgs.Empty);
                    else
                        topPopup.Close();
                }
            }
            else if (this.ParentMenu == null && this.IsPopupMenu)
            {

                this.HidePopupMenu();
                this.Close();
            }
            else
            {
                //this.ignoreNextMouseMove = true;
            }


            currItem.NotifyClickEvent();
            this.needShowPopupMenu = false;
        }
        #endregion Mouse event handlers

        #region Paint event handler




        protected override void OnPaint(PaintEventArgs e)
        {

            base.OnPaint(e);


            Graphics g = e.Graphics;
            RepositionMenuItems(g);


            DrawMenu(g);
            this.ignoreNextMouseMove = false;
        }
        #endregion Paint event handler

        #region Visibility event handler





        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            this.StopMenuTimer();
            this.isMouseEntered = false;
            this.isMousePressed = false;
            this.ResetCurrentMenuItem();
            this.ignoreNextMouseMove = false;
        }
        #endregion Visibility event handler

        #region Menu timer callback





        protected void MenuTimerCallback(object obj, EventArgs e)
        {
            if (this.menuTimer == null)
                return;

            this.StopMenuTimer();



            if (!this.IsMouseEntered && !this.IsMouseInPopupMenu())
            {
                this.ResetCurrentMenuItem();
                this.needShowPopupMenu = false;
                this.Invalidate();
                this.HidePopupMenu();

                System.EventHandler evh = this.RequestHide;
                if (this.parentMenuItem != null && evh != null)
                {

                    evh(this, EventArgs.Empty);
                }
                else if (this.IsPopupMenu)
                {

                    this.Close();
                }
            }
        }
        #endregion Menu timer callback

        #region RequestHide event handler





        protected void popupMenu_RequestHide(object obj, System.EventArgs e)
        {



            if (!this.IsPopupMenu && this.IsMouseEntered)
            {
                if (this.popupMenu != null &&
                     this.popupMenu.ParentMenuItem == this.currMenuItem)
                {
                    return;
                }
            }

            this.needShowPopupMenu = false;

            if (this.ParentMenuItem == null && !this.IsPopupMenu)
                this.ResetCurrentMenuItem();
            this.Invalidate();

            this.HidePopupMenu();


            if (!this.IsMouseEntered)
            {
                System.EventHandler evh = this.RequestHide;
                if (evh != null)
                    evh(this, System.EventArgs.Empty);
                else if (this.IsPopupMenu)
                    this.Close();
            }
        }
        #endregion RequestHide event handler

        #region ToString override




        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Menu");
            sb.Append(this.MenuId);

            return sb.ToString();
        }
        #endregion ToString override

        #region Abstract methods
        protected abstract void DrawMenu(Graphics g);
        protected abstract void RepositionMenuItems(Graphics g);
        protected abstract void CreatePopupMenu();
        protected abstract void GetPopupMenuScreenLocation(ref Point ptScreen);
        #endregion Abstract methods
    }
}



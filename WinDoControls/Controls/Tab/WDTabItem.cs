using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinDoControls.Controls;
using System.Drawing;
using WinDo.Utilities.PublicResource;
using System.Windows.Forms;
using WinDoControls;
using WinDoControls.Forms;

namespace WinDoControls.Controls
{
    public class WDTabItem : System.Windows.Forms.Control
    {
        public BaseForm Form
        {
            get { return _pageForm; }
        }
        static Image CloseImage = WDImages.X_Black;
        private BaseForm _pageForm = null;
        private TabPage _tabPage = null;
        private WDTablessControl _tablessControl;
        private System.Windows.Forms.Control _parentControl = null;
        public WDTabItem(System.Windows.Forms.Control parentControl, string text, BaseForm pageForm, WDTablessControl tablessControl, TabPage tabPage)
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.Font = WDFonts.TextFont;
            this.Text = text;
            _parentControl = parentControl;
            _tablessControl = tablessControl;
            this._pageForm = pageForm;
            _tabPage = tabPage;
            this.Width = Math.Max(_minWidth, TextRenderer.MeasureText(text, this.Font).Width + 40);
            this.Padding = new System.Windows.Forms.Padding(0);
            this.Margin = new System.Windows.Forms.Padding(0);
            WinDoControls.Forms.FrmTips.ClearTips();
            this.Height = 28;
            CloseRect = new Rectangle(this.Width - 22, (this.Height - 18) / 2, 18, 18);
            if (this._pageForm.RelationForm != null)
            {
                var rForm = _parentControl.Controls.Cast<WDTabItem>().FirstOrDefault(i => i.Form == this._pageForm.RelationForm);
                if (rForm != null)
                {
                    var prevIndex = _parentControl.Controls.IndexOf(rForm);
                    _parentControl.Controls.Add(this);
                    _parentControl.Controls.SetChildIndex(this, prevIndex + 1);
                }
                else
                    _parentControl.Controls.Add(this);
            }
            else
                _parentControl.Controls.Add(this);
            this.Paint += TabItem_Paint;
            this.MouseClick += TabItem_MouseClick;
            this.MouseMove += TabItem_MouseMove;
        }

        private void TabItem_MouseMove(object sender, MouseEventArgs e)
        {
            this.Cursor = CloseRect.Contains(e.Location) ? Cursors.Hand : Cursors.Default;
        }

        Rectangle CloseRect;

        private void TabItem_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString(this.Text, this.Font, Brushes.Black, this.ClientRectangle, WDList.StringFormatCenter);
            e.Graphics.DrawImage(CloseImage, CloseRect.Location);
            using (var p = new Pen(WDColors.GrayColor))
                e.Graphics.DrawLine(p, this.Width - 1, 0, this.Width - 1, this.Height);
        }

        private void TabItem_MouseClick(object sender, MouseEventArgs e)
        {
            WinDoControls.Forms.FrmTips.ClearTips();
            if (CloseRect.Contains(e.Location))
            {
                OnlblClose_Click();
                return;
            }
            Selected = true;
        }

        public void OnlblClose_Click()
        {
            lblClose_Click(null, null);
        }

        void lblClose_Click(object sender, EventArgs e)
        {
            var items = _parentControl.Controls.Cast<WDTabItem>().Where(i => i != this).ToList();
            if (items.All(i => !i.Visible))
            {
                _parentControl.SuspendLayout();
                foreach (var item in items.Where(i => !i.Visible))
                    item.Visible = true;
                _parentControl.ResumeLayout(true);
            }
            if (_pageForm.RelationForm != null)
            {
                var rForm = _parentControl.Controls.Cast<WDTabItem>().FirstOrDefault(i => i.Form == _pageForm.RelationForm);
                if (rForm != null)
                {
                    rForm.Selected = true;
                    _tablessControl.TabPages.Remove(this._tabPage);
                    _tabPage.Dispose();
                    this.Dispose();
                    return;
                }
            }
            _parentControl.Controls.Remove(this);
            if (_selected)
                if (_parentControl.Controls.Count > 0)
                {
                    var tabItem = _parentControl.Controls[_parentControl.Controls.Count - 1] as WDTabItem;

                    tabItem.Selected = true;
                }
            if (_parentControl.Controls.Count == 0)
            {
                _tablessControl.Parent.Visible = false;
            }
            _tablessControl.TabPages.Remove(this._tabPage);
            _tabPage.Dispose();
            this.Dispose();
        }

        public string ItemText
        {
            get { return this.Text; }
            set
            {
                this.Text = value;
                this.Width = Math.Max(_minWidth, TextRenderer.MeasureText(this.Text, this.Font).Width + 50);
            }
        }

        private bool _selected = false;
        public bool Selected
        {
            get { return _selected; }
            set
            {
                if (_selected == value)
                    return;
                _selected = value;
                if (_selected)
                {
                    //隐藏其它的
                    foreach (WDTabItem item in _parentControl.Controls.Cast<WDTabItem>().Where(c => c != this))
                    {
                        item._selected = false;
                        item.BackColor = Color.Transparent;
                        item.Invalidate();
                    }
                }
                this.BackColor = _selected ? _selectedBackColor : Color.Transparent;
                this.Invalidate();
                _tablessControl.SelectedTab = _tabPage;
            }
        }

        private Color _selectedBackColor = WDColors.LightPinkBackColor;
        public Color SelectedBackColor
        {
            get { return _selectedBackColor; }
            set { _selectedBackColor = value; }
        }

        private int _minWidth = 100;
        public int MinWidth
        {
            get { return _minWidth; }
            set
            {
                _minWidth = value;
                if (this.Width < _minWidth)
                    this.Width = _minWidth;
            }
        }

    }
}

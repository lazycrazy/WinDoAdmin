using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinDoControls.Controls;

namespace WinDoControls.Forms
{
    public partial class BaseForm : Form
    {
        //主界面
        public BaseForm MainForm = null;
        public BaseForm()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
        }
        public bool IsClose = false;
        public System.Windows.Forms.Control RelationForm;

        public string ModuleFormCode;

        public virtual BaseForm OpenNewForm(WDMenuInfo menuInfo, Action<BaseForm> beforeLoadAction = null)
        {
            return null;
        }

        public virtual void LoadMenuItems()
        {

        }

        public virtual void CloseTab(System.Windows.Forms.Control form)
        {

        }
        public virtual void CloseTabWithFormName(string formName)
        {

        }

        public virtual void CloseTabWithTabText(string tabText)
        {
        }

        public virtual bool ExistsTab(string tabText)
        {
            return false;
        }

        public void DrawBorder(PaintEventArgs e, Rectangle rct, Color color, int BorderWidth)
        {
            ControlPaint.DrawBorder(e.Graphics, rct,
color, BorderWidth, ButtonBorderStyle.Solid, //左边
color, BorderWidth, ButtonBorderStyle.Solid, //上边
color, BorderWidth, ButtonBorderStyle.Solid, //右边
color, BorderWidth, ButtonBorderStyle.Solid);//底边
        }
    }
}

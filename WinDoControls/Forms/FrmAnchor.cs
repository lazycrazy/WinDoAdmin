















using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinDoControls.Forms
{








    public partial class FrmAnchor : Form, IMessageFilter
    {




        Control m_parentControl = null;
        public Control m_childControl = null;



        private bool blnDown = true;



        Size m_size;



        Point? m_deviation;



        bool m_isNotFocus = true;
        #region 构造函数










        public FrmAnchor(Control parentControl, Control childControl, Point? deviation = null, bool isNotFocus = true)
        {
            m_isNotFocus = isNotFocus;
            m_parentControl = parentControl;
            InitializeComponent();
            this.Size = childControl.Size;
            this.HandleCreated += FrmDownBoard_HandleCreated;
            this.HandleDestroyed += FrmDownBoard_HandleDestroyed;
            this.Controls.Add(childControl);
            childControl.Dock = DockStyle.Fill;

            m_size = childControl.Size;
            m_deviation = deviation;

            if (parentControl.FindForm() != null)
            {
                Form frmP = parentControl.FindForm();
                if (!frmP.IsDisposed)
                {
                    frmP.LocationChanged += frmP_LocationChanged;
                }
            }
            parentControl.LocationChanged += frmP_LocationChanged;
        }









        public FrmAnchor(Control parentControl, Size size, Point? deviation = null, bool isNotFocus = true)
        {
            m_isNotFocus = isNotFocus;
            m_parentControl = parentControl;
            InitializeComponent();
            this.Size = size;
            this.HandleCreated += FrmDownBoard_HandleCreated;
            this.HandleDestroyed += FrmDownBoard_HandleDestroyed;

            m_size = size;
            m_deviation = deviation;
        }






        void frmP_LocationChanged(object sender, EventArgs e)
        {
            if (HideClose)
                this.Close();
            else
                this.Hide();
        }
        #endregion






        private void FrmDownBoard_HandleDestroyed(object sender, EventArgs e)
        {
            Application.RemoveMessageFilter(this);
        }






        private void FrmDownBoard_HandleCreated(object sender, EventArgs e)
        {
            Application.AddMessageFilter(this);
        }

        #region 无焦点窗体






        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private extern static IntPtr SetActiveWindow(IntPtr handle);



        private const int WM_ACTIVATE = 0x006;



        private const int WM_ACTIVATEAPP = 0x01C;



        private const int WM_NCACTIVATE = 0x086;



        private const int WA_INACTIVE = 0;



        private const int WM_MOUSEACTIVATE = 0x21;



        private const int MA_NOACTIVATE = 3;




        protected override void WndProc(ref Message m)
        {
            if (m_isNotFocus)
            {
                if (m.Msg == WM_MOUSEACTIVATE)
                {
                    m.Result = new IntPtr(MA_NOACTIVATE);
                    return;
                }
                else if (m.Msg == WM_NCACTIVATE)
                {
                    if (((int)m.WParam & 0xFFFF) != WA_INACTIVE)
                    {
                        if (m.LParam != IntPtr.Zero)
                        {
                            SetActiveWindow(m.LParam);
                        }
                        else
                        {
                            SetActiveWindow(IntPtr.Zero);
                        }
                    }
                }
            }
            base.WndProc(ref m);
        }

        #endregion


        public bool HideClose = false;
        public bool AllowMouseOnParent = true;
        public bool KeyDownClose = false;

        private const int WM_NCLBUTTONDOWN = 0x00A1;
        private const int WM_HSCROLL = 0x0114;
        private const int WM_VSCROLL = 0x0115;
        private const int WM_MOUSEWHEEL = 0x020A;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_SYSKEYDOWN = 0x0104;

        public bool scrollClose = true;
        public bool PreFilterMessage(ref Message m)
        {
            if (KeyDownClose)
            {
                if (m.Msg == WM_SYSKEYDOWN || m.Msg == WM_KEYDOWN)
                    this.Close();
            }
            if (!this.Visible)
                return false;
            if (m.Msg != 0x0201 && m.Msg != WM_NCLBUTTONDOWN && m.Msg != WM_HSCROLL && m.Msg != WM_VSCROLL && m.Msg != WM_MOUSEWHEEL)
                return false;
            if ((m.Msg == WM_VSCROLL || m.Msg == WM_MOUSEWHEEL) && !scrollClose)
            {
                return false;
            }
            if (AllowMouseOnParent)
            {
                bool onParent = this.m_parentControl.RectangleToScreen(this.m_parentControl.ClientRectangle).Contains(MousePosition);
                bool onChild = false;
                if (this.m_childControl != null)
                {
                    onChild = this.m_childControl.RectangleToScreen(this.m_childControl.ClientRectangle).Contains(MousePosition);
                    if (m.Msg == 0x0201 && onChild)
                        return false;
                }
                if (m.Msg == 0x0201 && onParent)
                    return false;
            }
            var pt = this.PointToClient(MousePosition);
            var onCtrl = this.ClientRectangle.Contains(pt);
            if (HideClose && !onCtrl)
                this.Close();
            else
                this.Visible = onCtrl;
            return false;
        }






        private void FrmAnchor_Load(object sender, EventArgs e)
        {

        }






        public bool CalcHeightByParent = true;
        private void FrmAnchor_VisibleChanged(object sender, EventArgs e)
        {
            timer1.Enabled = this.Visible;
            if (this.Visible)
            {
                Point p = m_parentControl.Parent.PointToScreen(m_parentControl.Location);
                int intX = 0;
                int intY = p.Y;
                if (CalcHeightByParent)
                {
                    var curScreen = Screen.FromControl(m_parentControl);
                    if (p.Y + m_parentControl.Height + m_size.Height > curScreen.WorkingArea.Height)
                    {
                        intY = p.Y - m_size.Height - 1;
                        blnDown = false;
                    }
                    else
                    {
                        intY = p.Y + m_parentControl.Height + 1;
                        blnDown = true;
                    }

                    if (p.X + m_size.Width > curScreen.WorkingArea.Width)
                    {
                        intX = curScreen.WorkingArea.Width - m_size.Width;

                    }
                    else
                    {
                        intX = p.X;
                    }
                }
                //if (m_deviation.HasValue)
                //{
                //    intX += m_deviation.Value.X;
                //    intY += m_deviation.Value.Y;
                //}
                //this.Location = new Point(intX, intY);
                var pp = new Point(p.X, intY);
                if (m_deviation.HasValue)
                {
                    pp.Offset(m_deviation.Value.X, m_deviation.Value.Y);
                }
                this.Location = pp;
            }
        }




        public Action CloseTick;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Owner != null)
            {
                if (CloseTick != null)
                {
                    CloseTick();
                    return;
                }

                Control topForm = this.Owner;
                while (topForm != null && topForm.Parent != null)
                    topForm = topForm.Parent;

                IntPtr _ptr = ControlHelper.GetForegroundWindow();
                if (_ptr != topForm.Handle && _ptr != this.Handle)
                {
                    if (HideClose)
                        this.Close();
                    else
                        this.Hide();
                }
            }
        }
    }
}

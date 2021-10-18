














using WinDo.Utilities.PublicResource;
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





    public partial class FrmTips : FrmBase
    {

        bool m_isNotFocus = true;




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

        private ContentAlignment m_showAlign = ContentAlignment.BottomLeft;





        public ContentAlignment ShowAlign
        {
            get { return m_showAlign; }
            set { m_showAlign = value; }
        }




        private static List<FrmTips> m_lstTips = new List<FrmTips>();




        private int m_CloseTime = 0;





        public int CloseTime
        {
            get { return m_CloseTime; }
            set
            {
                m_CloseTime = value;
                if (value > 0)
                    timer1.Interval = value;
            }
        }


        Size PicSize = new Size(48, 48);
        Image IconImage;

        Color TextForeColor;
        string Msg;
        StringFormat sf = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };

        public FrmTips()
        {
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            InitializeComponent();
            this.Paint += FrmTips_Paint;
        }

        private void FrmTips_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SetGDIHigh();
            var rect = e.ClipRectangle;
            rect.Width = 80;
            var p = rect.GetCenterRangeLocation(IconImage.Size);
            e.Graphics.DrawImage(IconImage, p);
            rect = e.ClipRectangle;
            rect.Width -= (80 + 5);
            rect.Height -= 10;
            rect.Offset(80, 5);
            using (var brush = new SolidBrush(TextForeColor))
            {
                e.Graphics.DrawString(Msg, WDFonts.TextFont16, brush, rect, this.sf);
            }
        }

        #region 清理提示框






        public static void ClearTips()
        {
            for (int i = m_lstTips.Count - 1; i >= 0; i--)
            {
                FrmTips current = m_lstTips[i];
                if (!current.IsDisposed)
                {
                    current.Close();
                    current.Dispose();
                }
            }
            m_lstTips.Clear();
        }
        #endregion




        public void ResetTimer()
        {
            if (m_CloseTime > 0)
            {
                timer1.Enabled = false;
                timer1.Enabled = true;
            }
        }



        private static KeyValuePair<string, FrmTips> m_lastTips = new KeyValuePair<string, FrmTips>();














        static Image SuccessImage = WinDoControls.FontImages.GetImage((WinDoControls.FontIcons)Enum.Parse(typeof(WinDoControls.FontIcons), "I_success"), 36, Color.White);  //WD_Controls.Properties.Resources.success;
        static Image ErrorImage = WinDoControls.FontImages.GetImage((WinDoControls.FontIcons)Enum.Parse(typeof(WinDoControls.FontIcons), "I_close"), 36, Color.White);  //WD_Controls.Properties.Resources.error;
        public static FrmTips ShowTips(
            Form frm,
            string strMsg,
            int intAutoColseTime = 0,
            bool blnShowCoseBtn = true,
            ContentAlignment align = ContentAlignment.BottomLeft,
            Point? point = null,
            TipsSizeMode mode = TipsSizeMode.Small,
            Size? size = null,
            TipsState state = TipsState.Default)
        {

            if (m_lastTips.Key == strMsg + state && !m_lastTips.Value.IsDisposed && m_lastTips.Value.Visible)
            {
                m_lastTips.Value.ResetTimer();
                return m_lastTips.Value;
            }
            else
            {
                FrmTips frmTips = new FrmTips();
                switch (mode)
                {
                    case TipsSizeMode.Small:
                        frmTips.Size = new Size(350, 35);
                        break;
                    case TipsSizeMode.Medium:
                        frmTips.Size = new Size(350, 50);
                        break;
                    case TipsSizeMode.Large:
                        frmTips.Size = new Size(362, 90);
                        break;
                    case TipsSizeMode.None:
                        if (!size.HasValue)
                        {
                            frmTips.Size = new Size(350, 35);
                        }
                        else
                        {
                            frmTips.Size = size.Value;
                        }
                        break;
                }

                if (state == TipsState.Error)
                    frmTips.BackColor = WDColors.ErrorTipRedColor;
                else if (state == TipsState.Success)
                    frmTips.BackColor = WDColors.Green6;
                else
                    frmTips.BackColor = Color.FromArgb((int)state);

                if (state == TipsState.Default)
                {
                    frmTips.TextForeColor = SystemColors.ControlText;
                }
                else
                {
                    frmTips.TextForeColor = Color.White;
                }
                switch (state)
                {
                    case TipsState.Default:
                        frmTips.IconImage = WinDoControls.Properties.Resources.success;
                        break;
                    case TipsState.Success:
                        frmTips.IconImage = SuccessImage;
                        break;
                    case TipsState.Info:
                        frmTips.IconImage = WinDoControls.Properties.Resources.success;
                        break;
                    case TipsState.Warning:
                        frmTips.IconImage = WinDoControls.Properties.Resources.warning;
                        break;
                    case TipsState.Error:
                        frmTips.IconImage = ErrorImage;
                        //frmTips.Width += 100;
                        break;
                    default:
                        frmTips.IconImage = WinDoControls.Properties.Resources.success;
                        break;
                }

                frmTips.Msg = strMsg;
                frmTips.CloseTime = intAutoColseTime;


                frmTips.ShowAlign = align;
                frmTips.Owner = frm;
                FrmTips.m_lstTips.Add(frmTips);
                FrmTips.ReshowTips();
                frmTips.Show(frm);
                if (frm != null && !frm.IsDisposed)
                {
                    //ControlHelper.SetForegroundWindow(frm.Handle);
                }

                m_lastTips = new KeyValuePair<string, FrmTips>(strMsg + state, frmTips);
                return frmTips;
            }
        }

        #region 刷新显示






        public static void ReshowTips()
        {
            lock (FrmTips.m_lstTips)
            {
                FrmTips.m_lstTips.RemoveAll(p => p.IsDisposed == true);
                var enumerable = from p in FrmTips.m_lstTips
                                 group p by new
                                 {
                                     p.ShowAlign
                                 };
                //Size size = Screen.FromPoint(Cursor.Position).WorkingArea.Size;
                var curScreen = Screen.FromPoint(Cursor.Position);
                Size size = curScreen.WorkingArea.Size;
                foreach (var item in enumerable)
                {

                    List<FrmTips> list = FrmTips.m_lstTips.FindAll((FrmTips p) => p.ShowAlign == item.Key.ShowAlign);
                    for (int i = 0; i < list.Count; i++)
                    {
                        FrmTips frmTips = list[i];
                        if (frmTips.InvokeRequired)
                        {
                            frmTips.BeginInvoke(new MethodInvoker(delegate ()
                            {
                                switch (item.Key.ShowAlign)
                                {
                                    case ContentAlignment.BottomCenter:
                                        frmTips.Location = new Point((size.Width - frmTips.Width) / 2, size.Height - 100 - (i + 1) * (frmTips.Height + 10));
                                        break;
                                    case ContentAlignment.BottomLeft:
                                        frmTips.Location = new Point(10, size.Height - 100 - (i + 1) * (frmTips.Height + 10));
                                        break;
                                    case ContentAlignment.BottomRight:
                                        frmTips.Location = new Point(size.Width - frmTips.Width - 10, size.Height - 100 - (i + 1) * (frmTips.Height + 10));
                                        break;
                                    case ContentAlignment.MiddleCenter:
                                        frmTips.Location = new Point((size.Width - frmTips.Width) / 2, size.Height - (size.Height - list.Count * (frmTips.Height + 10)) / 2 - (i + 1) * (frmTips.Height + 10));
                                        break;
                                    case ContentAlignment.MiddleLeft:
                                        frmTips.Location = new Point(10, size.Height - (size.Height - list.Count * (frmTips.Height + 10)) / 2 - (i + 1) * (frmTips.Height + 10));
                                        break;
                                    case ContentAlignment.MiddleRight:
                                        frmTips.Location = new Point(size.Width - frmTips.Width - 10, size.Height - (size.Height - list.Count * (frmTips.Height + 10)) / 2 - (i + 1) * (frmTips.Height + 10));
                                        break;
                                    case ContentAlignment.TopCenter:
                                        frmTips.Location = new Point((size.Width - frmTips.Width) / 2, 10 + (i + 1) * (frmTips.Height + 10));
                                        break;
                                    case ContentAlignment.TopLeft:
                                        frmTips.Location = new Point(10, 10 + (i + 1) * (frmTips.Height + 10));
                                        break;
                                    case ContentAlignment.TopRight:
                                        frmTips.Location = new Point(size.Width - frmTips.Width - 10, 10 + (i + 1) * (frmTips.Height + 10));
                                        break;
                                    default:
                                        break;
                                }
                            }));
                            var p = frmTips.Location;
                            p.Offset(curScreen.Bounds.X, 0);
                            frmTips.Location = p;
                        }
                        else
                        {
                            switch (item.Key.ShowAlign)
                            {
                                case ContentAlignment.BottomCenter:
                                    frmTips.Location = new Point((size.Width - frmTips.Width) / 2, size.Height - 100 - (i + 1) * (frmTips.Height + 10));
                                    break;
                                case ContentAlignment.BottomLeft:
                                    frmTips.Location = new Point(10, size.Height - 100 - (i + 1) * (frmTips.Height + 10));
                                    break;
                                case ContentAlignment.BottomRight:
                                    frmTips.Location = new Point(size.Width - frmTips.Width - 10, size.Height - 100 - (i + 1) * (frmTips.Height + 10));
                                    break;
                                case ContentAlignment.MiddleCenter:
                                    frmTips.Location = new Point((size.Width - frmTips.Width) / 2, size.Height - (size.Height - list.Count * (frmTips.Height + 10)) / 2 - (i + 1) * (frmTips.Height + 10));
                                    break;
                                case ContentAlignment.MiddleLeft:
                                    frmTips.Location = new Point(10, size.Height - (size.Height - list.Count * (frmTips.Height + 10)) / 2 - (i + 1) * (frmTips.Height + 10));
                                    break;
                                case ContentAlignment.MiddleRight:
                                    frmTips.Location = new Point(size.Width - frmTips.Width - 10, size.Height - (size.Height - list.Count * (frmTips.Height + 10)) / 2 - (i + 1) * (frmTips.Height + 10));
                                    break;
                                case ContentAlignment.TopCenter:
                                    frmTips.Location = new Point((size.Width - frmTips.Width) / 2, 10 + (i + 1) * (frmTips.Height + 10));
                                    break;
                                case ContentAlignment.TopLeft:
                                    frmTips.Location = new Point(10, 10 + (i + 1) * (frmTips.Height + 10));
                                    break;
                                case ContentAlignment.TopRight:
                                    frmTips.Location = new Point(size.Width - frmTips.Width - 10, 10 + (i + 1) * (frmTips.Height + 10));
                                    break;
                                default:
                                    break;
                            }
                            var p = frmTips.Location;
                            p.Offset(curScreen.Bounds.X, 0);
                            frmTips.Location = p;
                        }

                    }
                }
            }
        }
        #endregion






        private void FrmTips_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_lastTips.Value == this)
                m_lastTips = new KeyValuePair<string, FrmTips>();
            m_lstTips.Remove(this);
            ReshowTips();

            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                if (Application.OpenForms[i].IsDisposed || !Application.OpenForms[i].Visible || Application.OpenForms[i] is FrmTips)
                {
                    continue;
                }
                else
                {
                    Timer t = new Timer();
                    t.Interval = 100;
                    var frm = Application.OpenForms[i];
                    t.Tick += (a, b) =>
                    {
                        t.Enabled = false;
                        //if (!frm.IsDisposed)
                        //    ControlHelper.SetForegroundWindow(frm.Handle);
                    };
                    t.Enabled = true;
                    break;
                }
            }
        }






        private void FrmTips_Load(object sender, EventArgs e)
        {
            if (m_CloseTime > 0)
            {
                this.timer1.Interval = m_CloseTime;
                this.timer1.Enabled = true;
            }
        }






        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
            this.Close();
        }






        private void btnClose_MouseDown(object sender, MouseEventArgs e)
        {
            this.timer1.Enabled = false;
            this.Close();
        }







        public static FrmTips ShowTipsSuccess(Form frm, string strMsg)
        {
            return FrmTips.ShowTips(frm, strMsg, 2000, false, ContentAlignment.TopCenter, null, TipsSizeMode.Large, null, TipsState.Success);
        }







        public static FrmTips ShowTipsError(Form frm, string strMsg)
        {
            return FrmTips.ShowTips(frm, strMsg, 2000, false, ContentAlignment.TopCenter, null, TipsSizeMode.Large, null, TipsState.Error);
        }







        public static FrmTips ShowTipsInfo(Form frm, string strMsg)
        {
            return FrmTips.ShowTips(frm, strMsg, 2000, false, ContentAlignment.TopCenter, null, TipsSizeMode.Large, null, TipsState.Info);
        }






        public static FrmTips ShowTipsWarning(Form frm, string strMsg)
        {
            return FrmTips.ShowTips(frm, strMsg, 2000, false, ContentAlignment.TopCenter, null, TipsSizeMode.Large, null, TipsState.Warning);
        }






        private void FrmTips_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            GC.Collect();
        }

    }




    public enum TipsSizeMode
    {



        Small,



        Medium,



        Large,



        None
    }



    public enum TipsState
    {



        Default = -1,



        Success = -6566849,



        Info = -12477983,



        Warning = -357816,



        Error = -1097849
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinDo.UI.Main
{

    public partial class frmLoginBackground : Form
    {

        public static string Guid = "XEF1375E8-5756-4A4C-8F8A-6F02AF2CECC6X";
        public frmLoginBackground()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            var img = Properties.Resources.login_shadow;
            this.Size = img.Size;
            this.Text = Guid;
            Bitmap bg = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bg);

            //var img = Properties.Resources.a;
            g.DrawImage(img, 0, 0, img.Width, img.Height);
            g.Dispose();
            SetBits(bg);
            //Load += Form3_Load;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            var frm = new Form() { BackColor = Color.Blue, TransparencyKey = Color.Blue, Opacity = 0.01, FormBorderStyle = FormBorderStyle.None };
            frm.Size = this.Size;
            frm.TopMost = true;
            frm.Location = new Point(100, 100);
            frm.LocationChanged += (s, ee) =>
            {
                this.Location = frm.Location;
            };
            this.Owner = frm;
            var btn = new Button() { Text = "点我" };

            btn.Anchor = AnchorStyles.None;
            btn.Size = new Size(50, 25);
            btn.Click += (s, ee) => { MessageBox.Show("xxx"); };
            frm.Controls.Add(btn);
            frm.Show();
        }
        #region UpdateLayeredWindow

        #region 重写窗体的 CreateParams 属性
        const int WS_EX_APPWINDOW = 0x00040000;
        const int WS_EX_TOOLWINDOW = 0x00000080;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00080000;  //  WS_EX_LAYERED 扩展样式      

                cp.ExStyle &= (~WS_EX_APPWINDOW);
                cp.ExStyle |= WS_EX_TOOLWINDOW;

                //无边框任务栏窗口最小化
                const int WS_MINIMIZEBOX = 0x00020000;  // Winuser.h中定义
                //CreateParams cp = base.CreateParams;
                cp.Style = cp.Style | WS_MINIMIZEBOX;   // 允许最小化操作
                return cp;
            }
        }
        #endregion

        /// <summary>
        /// Class Win32.
        /// </summary>
        class Win32
        {
            /// <summary>
            /// Struct Size
            /// </summary>
            [StructLayout(LayoutKind.Sequential)]
            public struct Size
            {
                /// <summary>
                /// The cx
                /// </summary>
                public Int32 cx;
                /// <summary>
                /// The cy
                /// </summary>
                public Int32 cy;

                /// <summary>
                /// Initializes a new instance of the <see cref="Size" /> struct.
                /// </summary>
                /// <param name="x">The x.</param>
                /// <param name="y">The y.</param>
                public Size(Int32 x, Int32 y)
                {
                    cx = x;
                    cy = y;
                }
            }

            /// <summary>
            /// Struct BLENDFUNCTION
            /// </summary>
            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public struct BLENDFUNCTION
            {
                /// <summary>
                /// The blend op
                /// </summary>
                public byte BlendOp;
                /// <summary>
                /// The blend flags
                /// </summary>
                public byte BlendFlags;
                /// <summary>
                /// The source constant alpha
                /// </summary>
                public byte SourceConstantAlpha;
                /// <summary>
                /// The alpha format
                /// </summary>
                public byte AlphaFormat;
            }

            /// <summary>
            /// Struct Point
            /// </summary>
            [StructLayout(LayoutKind.Sequential)]
            public struct Point
            {
                /// <summary>
                /// The x
                /// </summary>
                public Int32 x;
                /// <summary>
                /// The y
                /// </summary>
                public Int32 y;

                /// <summary>
                /// Initializes a new instance of the <see cref="Point" /> struct.
                /// </summary>
                /// <param name="x">The x.</param>
                /// <param name="y">The y.</param>
                public Point(Int32 x, Int32 y)
                {
                    this.x = x;
                    this.y = y;
                }
            }

            /// <summary>
            /// The ac source over
            /// </summary>
            public const byte AC_SRC_OVER = 0;
            /// <summary>
            /// The ulw alpha
            /// </summary>
            public const Int32 ULW_ALPHA = 2;
            /// <summary>
            /// The ac source alpha
            /// </summary>
            public const byte AC_SRC_ALPHA = 1;

            /// <summary>
            /// Creates the compatible dc.
            /// </summary>
            /// <param name="hDC">The h dc.</param>
            /// <returns>IntPtr.</returns>
            [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
            public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

            /// <summary>
            /// Gets the dc.
            /// </summary>
            /// <param name="hWnd">The h WND.</param>
            /// <returns>IntPtr.</returns>
            [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
            public static extern IntPtr GetDC(IntPtr hWnd);

            /// <summary>
            /// Selects the object.
            /// </summary>
            /// <param name="hDC">The h dc.</param>
            /// <param name="hObj">The h object.</param>
            /// <returns>IntPtr.</returns>
            [DllImport("gdi32.dll", ExactSpelling = true)]
            public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObj);

            /// <summary>
            /// Releases the dc.
            /// </summary>
            /// <param name="hWnd">The h WND.</param>
            /// <param name="hDC">The h dc.</param>
            /// <returns>System.Int32.</returns>
            [DllImport("user32.dll", ExactSpelling = true)]
            public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

            /// <summary>
            /// Deletes the dc.
            /// </summary>
            /// <param name="hDC">The h dc.</param>
            /// <returns>System.Int32.</returns>
            [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
            public static extern int DeleteDC(IntPtr hDC);

            /// <summary>
            /// Deletes the object.
            /// </summary>
            /// <param name="hObj">The h object.</param>
            /// <returns>System.Int32.</returns>
            [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
            public static extern int DeleteObject(IntPtr hObj);

            /// <summary>
            /// Updates the layered window.
            /// </summary>
            /// <param name="hwnd">The HWND.</param>
            /// <param name="hdcDst">The HDC DST.</param>
            /// <param name="pptDst">The PPT DST.</param>
            /// <param name="psize">The psize.</param>
            /// <param name="hdcSrc">The HDC source.</param>
            /// <param name="pptSrc">The PPT source.</param>
            /// <param name="crKey">The cr key.</param>
            /// <param name="pblend">The pblend.</param>
            /// <param name="dwFlags">The dw flags.</param>
            /// <returns>System.Int32.</returns>
            [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
            public static extern int UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pptSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags);

            /// <summary>
            /// Exts the create region.
            /// </summary>
            /// <param name="lpXform">The lp xform.</param>
            /// <param name="nCount">The n count.</param>
            /// <param name="rgnData">The RGN data.</param>
            /// <returns>IntPtr.</returns>
            [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
            public static extern IntPtr ExtCreateRegion(IntPtr lpXform, uint nCount, IntPtr rgnData);
        }
        #region API调用
        public void SetBits(Bitmap bitmap)//调用UpdateLayeredWindow（）方法。this.BackgroundImage为你事先准备的带透明图片。
        {
            //if (!haveHandle) return;

            if (!Bitmap.IsCanonicalPixelFormat(bitmap.PixelFormat) || !Bitmap.IsAlphaPixelFormat(bitmap.PixelFormat))
                throw new ApplicationException("图片必须是32位带Alhpa通道的图片。");

            IntPtr oldBits = IntPtr.Zero;
            IntPtr screenDC = Win32.GetDC(IntPtr.Zero);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr memDc = Win32.CreateCompatibleDC(screenDC);

            try
            {
                Win32.Point topLoc = new Win32.Point(Left, Top);
                Win32.Size bitMapSize = new Win32.Size(bitmap.Width, bitmap.Height);
                Win32.BLENDFUNCTION blendFunc = new Win32.BLENDFUNCTION();
                Win32.Point srcLoc = new Win32.Point(0, 0);

                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
                oldBits = Win32.SelectObject(memDc, hBitmap);

                blendFunc.BlendOp = Win32.AC_SRC_OVER;
                blendFunc.SourceConstantAlpha = 255;
                blendFunc.AlphaFormat = Win32.AC_SRC_ALPHA;
                blendFunc.BlendFlags = 0;

                Win32.UpdateLayeredWindow(Handle, screenDC, ref topLoc, ref bitMapSize, memDc, ref srcLoc, 0, ref blendFunc, Win32.ULW_ALPHA);
            }
            finally
            {
                if (hBitmap != IntPtr.Zero)
                {
                    Win32.SelectObject(memDc, oldBits);
                    Win32.DeleteObject(hBitmap);
                }
                Win32.ReleaseDC(IntPtr.Zero, screenDC);
                Win32.DeleteDC(memDc);
            }
        }
        #endregion

        #endregion

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLoginBackground));
            this.SuspendLayout();
            // 
            // frmLoginBackground
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLoginBackground";
            this.ResumeLayout(false);

        }
    }


}

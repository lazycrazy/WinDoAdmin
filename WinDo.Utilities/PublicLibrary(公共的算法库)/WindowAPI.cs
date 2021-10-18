using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace WinDo.Utilities
{
    /// <summary>
    /// WindowAPI
    /// 操作Windows Core常用公共方法    
    /// </summary>
    public class WindowAPI
    {
        #region //当鼠标按下拖动时调用的方法
        [DllImport("user32.dll")]
        static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        public static void MouseMoveWindow(IntPtr hWnd)
        {
            ReleaseCapture();
            SendMessage(hWnd, 274, 61458, 0);
        }
        #endregion

        #region //当窗体初始化时调用的方法（显示窗体）
        [DllImport("user32")]
        static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);

        public static void ShowWindow(IntPtr hWnd, int ShowType)
        {
            switch (ShowType)
            {
                case 0://从中间出来
                    AnimateWindow(hWnd, 500, 0x0010 | 0x20000);
                    break;
                case 1://从下面退出
                    AnimateWindow(hWnd, 500, 0x40000 | 0x10000 | 0x0008);
                    break;
                case 2: break;
            }
        }
        #endregion

        #region //屏幕取色要用到的GDI

        [DllImport("gdi32.dll")]
        static public extern uint GetPixel(IntPtr hDC, int XPos, int YPos);

        [DllImport("gdi32.dll")]
        static public extern IntPtr CreateDC(string driverName, string deviceName, string output, IntPtr lpinitData);

        [DllImport("gdi32.dll")]
        static public extern bool DeleteDC(IntPtr DC);

        static public byte GetRValue(uint color)
        {
            return (byte)color;
        }

        static public byte GetGValue(uint color)
        {
            return ((byte)(((short)(color)) >> 8));
        }

        static public byte GetBValue(uint color)
        {
            return ((byte)((color) >> 16));
        }

        static public byte GetAValue(uint color)
        {
            return ((byte)((color) >> 24));
        }

        static public Color GetColorOfScreen(Point screenPoint)
        {
            IntPtr displayDC = CreateDC("DISPLAY", null, null, IntPtr.Zero);
            uint colorref = GetPixel(displayDC, screenPoint.X, screenPoint.Y);
            DeleteDC(displayDC);

            byte Red = GetRValue(colorref);
            byte Green = GetGValue(colorref);
            byte Blue = GetBValue(colorref);

            return Color.FromArgb(Red, Green, Blue);
        }
        #endregion

        //根据对应的鼠标坐标获取句柄
        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(Point Point);

        /// <summary>
        /// 设置鼠标位置API
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);
    }
}
















using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace WinDoControls.Controls
{
    
    
    
    public class win32
    {

        
        
        
        public const int WM_MOUSEMOVE = 0x0200;
        
        
        
        public const int WM_LBUTTONDOWN = 0x0201;
        
        
        
        public const int WM_LBUTTONUP = 0x0202;
        
        
        
        public const int WM_RBUTTONDOWN = 0x0204;
        
        
        
        public const int WM_LBUTTONDBLCLK = 0x0203;

        
        
        
        public const int WM_MOUSELEAVE = 0x02A3;



        
        
        
        public const int WM_PAINT = 0x000F;
        
        
        
        public const int WM_ERASEBKGND = 0x0014;

        
        
        
        public const int WM_PRINT = 0x0317;

        
        

        
        
        
        public const int WM_HSCROLL = 0x0114;
        
        
        
        public const int WM_VSCROLL = 0x0115;


        
        
        
        public const int EM_GETSEL = 0x00B0;
        
        
        
        public const int EM_LINEINDEX = 0x00BB;
        
        
        
        public const int EM_LINEFROMCHAR = 0x00C9;

        
        
        
        public const int EM_POSFROMCHAR = 0x00D6;



        
        
        
        
        
        
        
        
        [DllImport("USER32.DLL", EntryPoint = "PostMessage")]
        public static extern bool PostMessage(IntPtr hwnd, uint msg,
            IntPtr wParam, IntPtr lParam);

        

        
        
        
        
        
        
        
        
        
        [DllImport("USER32.DLL", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hwnd, int msg, IntPtr wParam,
            IntPtr lParam);




        
        
        
        
        [DllImport("USER32.DLL", EntryPoint = "GetCaretBlinkTime")]
        public static extern uint GetCaretBlinkTime();




        
        
        
        const int WM_PRINTCLIENT = 0x0318;

        
        
        
        const long PRF_CHECKVISIBLE = 0x00000001L;
        
        
        
        const long PRF_NONCLIENT = 0x00000002L;
        
        
        
        const long PRF_CLIENT = 0x00000004L;
        
        
        
        const long PRF_ERASEBKGND = 0x00000008L;
        
        
        
        const long PRF_CHILDREN = 0x00000010L;
        
        
        
        const long PRF_OWNED = 0x00000020L;

        


        
        
        
        
        
        
        public static bool CaptureWindow(System.Windows.Forms.Control control,
                                ref System.Drawing.Bitmap bitmap)
        {
            

            Graphics g2 = Graphics.FromImage(bitmap);

            
            int meint = (int)(PRF_CLIENT | PRF_ERASEBKGND); 
            System.IntPtr meptr = new System.IntPtr(meint);

            System.IntPtr hdc = g2.GetHdc();
            win32.SendMessage(control.Handle, win32.WM_PRINT, hdc, meptr);

            g2.ReleaseHdc(hdc);
            g2.Dispose();

            return true;

        }



    }
}

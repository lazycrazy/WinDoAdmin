using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WinDo.Utilities
{    
    public static class ClsImeHelper
    {
        //API函数声明
        [DllImport("imm32.dll")]
        public static extern IntPtr ImmGetContext(IntPtr hwnd);

        [DllImport("imm32.dll")]
        public static extern bool ImmGetOpenStatus(IntPtr himc);

        [DllImport("imm32.dll")]
        public static extern bool ImmSetOpenStatus(IntPtr himc, bool b);

        [DllImport("imm32.dll")]
        public static extern bool ImmGetConversionStatus(IntPtr himc, ref int lpdw, ref int lpdw2);

        [DllImport("imm32.dll")]
        public static extern int ImmSimulateHotKey(IntPtr hwnd, int lngHotkey);
        public const int IME_CMODE_FULLSHAPE = 0x8;
        public const int IME_CHOTKEY_SHAPE_TOGGLE = 0x11;

        /// <summary>
        /// 重载SetIme，传入Form
        /// </summary>
        /// <param name="frm">窗体</param>
        public static void SetIme(Form frm)
        {
            frm.Paint += new PaintEventHandler(frm_Paint);
            ChangeAllControl(frm);
        }

        /// <summary>
        /// 重载SetIme，传入Control
        /// </summary>
        /// <param name="ctl">控件</param>
        public static void SetIme(Control ctl)
        {
            ChangeAllControl(ctl);
        }

        /// <summary>
        /// 重载SetIme，传入对象句柄
        /// </summary>
        /// <param name="Handel">对象句柄</param>
        public static void SetIme(IntPtr Handel)
        {
            ChangeControlIme(Handel);
        }

        private static void ChangeAllControl(Control ctl)
        {
            //在控件的的Enter事件中触发来调整输入法状态
            ctl.Enter += new EventHandler(ctl_Enter);
            //遍历子控件，使每个控件都用上Enter的委托处理
            foreach (Control ctlChild in ctl.Controls)
                ChangeAllControl(ctlChild);
        }

        static void frm_Paint(object sender, PaintEventArgs e)
        {
            /*********************************************************
             * 使用Pain事件，而不用Load事件或Activated事件，是基于下列考虑：
             *  1、在您的Form中，有些控件可能是运行时动态添加的
             *  2、在您的Form中，使用到了非.NET的OCX控件
             *  3、Form调用子Form的时候，Activated事件根本不会触发
             *********************************************************/
            ChangeControlIme(sender);
        }

        /// <summary>
        /// 控件的Enter处理程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void ctl_Enter(object sender, EventArgs e)
        {
            ChangeControlIme(sender);
        }

        private static void ChangeControlIme(object sender)
        {
            Control ctl = (Control)sender;
            ChangeControlIme(ctl.Handle);
        }

        /// <summary>
        /// 下面这个函数才是真正检查输入法的全角半角状态
        /// </summary>
        /// <param name="h"></param>
        private static void ChangeControlIme(IntPtr h)
        {
            IntPtr HIme = ImmGetContext(h);
            if (ImmGetOpenStatus(HIme))  //如果输入法处于打开状态
            {
                int iMode = 0;
                int iSentence = 0;
                bool bSuccess = ImmGetConversionStatus(HIme, ref iMode, ref iSentence);  //检索输入法信息

                if (bSuccess)
                {
                    if ((iMode & IME_CMODE_FULLSHAPE) > 0)   //如果是全角
                    {
                        ImmSimulateHotKey(h, IME_CHOTKEY_SHAPE_TOGGLE);  //转换成半角
                    }
                }
            }
        }
    }
}

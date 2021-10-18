using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using WinDoControls.Controls;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using WinDo.Utilities.PublicResource;
using WinDo.Utilities;
using WinDoControls.Forms;

namespace WinDoControls
{
    /// <summary>
    /// Class ControlHelper.
    /// </summary>
    public static class ControlHelper
    {


        #region 控件文本超出宽度，自动tips提示

        public static void ShowTipsOnOverLength(System.Windows.Forms.Control ctrl)
        {
            FrmAnchorTips _frmAnchorTips = null;
            ctrl.MouseHover += (s, e) =>
            {
                if (_frmAnchorTips != null)
                {
                    _frmAnchorTips.Close();
                    _frmAnchorTips = null;
                }
                var txt = ctrl.Text;
                var tw = TextRenderer.MeasureText(txt, ctrl.Font).Width;
                if (ctrl.Width >= tw)
                    return;
                var tips = string.Join<string>("\r\n", StringHelper.Split(txt, 50));
                if (_frmAnchorTips == null)
                    _frmAnchorTips = FrmAnchorTips.ShowTips(ctrl, tips, AnchorTipsLocation.BOTTOM, WDColors.TaskListTip, autoCloseTime: 6000);
            };
            ctrl.MouseLeave += (s, e) =>
            {
                if (_frmAnchorTips != null)
                {
                    _frmAnchorTips.Close();
                    _frmAnchorTips = null;
                }
            };
        }



        #endregion
        /// <summary>
        /// 异步访问UI线程
        /// </summary>
        /// <param name="uiElement"></param>
        /// <param name="updater"></param>
        public static void SafeBeginInvoke(this System.Windows.Forms.Control uiElement, Action updater)
        {
            if (uiElement != null && !uiElement.Disposing && !uiElement.IsDisposed && uiElement.IsHandleCreated && (uiElement.FindForm() != null && uiElement.FindForm().IsHandleCreated))
            {
                if (uiElement.InvokeRequired)
                {
                    uiElement.BeginInvoke((Action)(() => { SafeBeginInvoke(uiElement, updater); }));
                }
                else
                {
                    updater();
                }
            }
        }
        public static void SetControlsDouble(System.Windows.Forms.Control control)
        {
            SetControlsAction(control, WinDoControls.ControlHelper.SetDouble);
        }
        /// <summary>
        /// 设置日期控件格式
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="dt"></param>
        public static void SetDTPCtrlFormat(NullableDateTimePicker t1, DateTime? dt = null)
        {
            t1.Format = DateTimePickerFormat.Custom;
            t1.CustomFormat = SystemInfo.DateFormat;
            t1.Value = dt;
        }

        #region "画矩形框"

        public static Bitmap GetBitmapByColor(int width, int height, Color color)
        {
            Bitmap bmp = new Bitmap(width, height);
            using (Graphics graph = Graphics.FromImage(bmp))
            {
                Rectangle ImageSize = new Rectangle(0, 0, width, height);
                using (var brush = new SolidBrush(color))
                {
                    graph.FillRectangle(brush, ImageSize);
                }
            }
            return bmp;
        }
        public static void DrawRectBtn(Graphics g, Rectangle container, int rectWidth, int rectHeight, Color rectFillColor, Color rectColor, Image img)
        {
            var temp = g.SmoothingMode;
            //使绘图质量最高，即消除锯齿
            g.SetGDIHigh();
            var rect = new Rectangle(container.X, container.Y, rectWidth, rectHeight);
            var x = (container.Width - rect.Width) / 2;
            var y = (container.Height - rect.Height) / 2;
            rect.Offset(x, y);
            using (var brush = new SolidBrush(rectFillColor))
            {
                WinDoControls.ControlHelper.FillRoundRectangle(g, brush, rect, 2);
            }
            using (var pen = new Pen(rectColor))
            {
                WinDoControls.ControlHelper.DrawRoundRectangle(g, pen, rect, 2);
            }
            x = rect.X + (rect.Width - img.Width) / 2;
            y = rect.Y + (rect.Height - img.Height) / 2;
            g.DrawImage(img, x, y + 1);
            g.SmoothingMode = temp;
        }

        public static void DrawRectBtnWithText(Graphics g, Rectangle container, int rectWidth, int rectHeight, Color rectFillColor, Color rectColor, string txt, Font textFont, Color textColor, Image icon)
        {
            var temp = g.SmoothingMode;
            //使绘图质量最高，即消除锯齿
            g.SetGDIHigh();
            var rect = container;
            rect.Size = new Size(rectWidth, rectHeight);
            rect.Offset(container.GetCenterRangeLocation(rect.Size));
            using (var brush = new SolidBrush(rectFillColor))
            {
                WinDoControls.ControlHelper.FillRoundRectangle(g, brush, rect, 2);
            }
            using (var pen = new Pen(rectColor))
            {
                WinDoControls.ControlHelper.DrawRoundRectangle(g, pen, rect, 2);
            }
            var iRect = rect;
            iRect.Offset(10, (iRect.Height - icon.Height) / 2);
            g.DrawImage(icon, iRect.X, iRect.Y);
            using (var brush = new SolidBrush(textColor))
            {
                rect.Offset(icon.Width, 0);
                rect.Width -= icon.Width;
                g.DrawString(txt, textFont, brush, rect, SFCenter);
            }
            g.SmoothingMode = temp;
        }

        /// <summary>
        /// 画一个带背景的矩形
        /// </summary>
        /// <param name="g"></param>
        /// <param name="container"></param>
        /// <param name="rectWidth"></param>
        /// <param name="rectHeight"></param>
        /// <param name="rectColor"></param>
        /// <param name="txt"></param>
        /// <param name="textFont"></param>
        /// <param name="textColor"></param>
        public static void DrawRectFlag(Graphics g, Rectangle container, int rectWidth, int rectHeight, Color rectFillColor, Color rectColor, string txt, Font textFont, Color textColor)
        {
            var temp = g.SmoothingMode;
            //使绘图质量最高，即消除锯齿
            g.SetGDIHigh();
            var rect = new Rectangle(container.X, container.Y, rectWidth, rectHeight);
            var x = (container.Width - rect.Width) / 2;
            var y = (container.Height - rect.Height) / 2;
            rect.Offset(x, y);
            using (var brush = new SolidBrush(rectFillColor))
            {
                WinDoControls.ControlHelper.FillRoundRectangle(g, brush, rect, 2);
            }
            using (var pen = new Pen(rectColor))
            {
                WinDoControls.ControlHelper.DrawRoundRectangle(g, pen, rect, 2);
            }
            using (var brush = new SolidBrush(textColor))
            {
                // FormatFlags = StringFormatFlags.NoWrap,
                g.DrawString(txt, textFont, brush, container, SFCenter);
            }
            g.SmoothingMode = temp;
        }
        public static StringFormat SFCenter = new StringFormat()
        {
            FormatFlags = StringFormatFlags.NoWrap,
            Trimming = StringTrimming.EllipsisCharacter,
            LineAlignment = StringAlignment.Center,
            Alignment = StringAlignment.Center
        };
        #endregion
        /// <summary>
        /// 可以查空格，有输入的去除前后空格
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetInputQueryText(string text, bool allowWhiteSpace = false)
        {
            if (allowWhiteSpace)
            {
                if (text == null || text.Length == 0) return "";
                if (text.Trim().Length == 0) return text;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(text)) return "";
            }
            var txt = text.Trim().Replace("%", "[%]").Replace("_", "[_]");
            return txt;
        }

        public static void SwichPicture(System.Windows.Forms.Control box, Image img, Image imgH)
        {
            box.MouseEnter += (s, e) =>
            {
                box.BackgroundImage = imgH;
            };

            box.MouseLeave += (s, e) =>
            {
                box.BackgroundImage = img;
            };
        }

        public static void SetCloseBackColor(Panel panel, bool isMainForm = false)
        {
            panel.Cursor = Cursors.Hand;
            panel.BackgroundImage = null;
            panel.Dock = DockStyle.None;
            panel.Size = new Size(36, 30);
            if (isMainForm)
            {
                panel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            }
            else
            {
                panel.Parent.SizeChanged += (s, e) =>
                {
                    panel.Location = new Point((panel.Parent.Width - panel.Size.Width - 5), (panel.Parent.Height - panel.Size.Height) / 2);
                };
                panel.Location = new Point((panel.Parent.Width - panel.Size.Width - 5), (panel.Parent.Height - panel.Size.Height) / 2);
            }

            panel.BringToFront();
            panel.Paint += (s, e) =>
            {
                var img = WDImages.X;
                e.Graphics.DrawImage(img, new Point((panel.ClientRectangle.Width - img.Width) / 2, (panel.ClientRectangle.Height - img.Height) / 2));
            };
            panel.MouseEnter += (s, e) =>
            {
                panel.BackColor = WDColors.red5;
            };
            panel.MouseLeave += (s, e) =>
            {
                panel.BackColor = isMainForm ? WDColors.geekblue6 : Color.Transparent;
            };
        }
        public static bool ControlIsVisible(System.Windows.Forms.Control control)
        {
            if (control == null || !control.Visible)
                return false;
            System.Windows.Forms.Control pCtrl = control;
            while (pCtrl != null)
            {
                var ctrl = pCtrl as ScrollableControl;
                if (ctrl != null && ctrl.AutoScroll)
                {
                    break;
                }
                pCtrl = pCtrl.Parent;
            }
            if (pCtrl == null)
                return true;
            var container = pCtrl as ScrollableControl;
            if (container != null && container.AutoScroll)
            {
                var ctrlPtS = control.Parent.PointToScreen(control.Location);
                var ctrlPtC = container.PointToClient(ctrlPtS);
                return container.ClientRectangle.Contains(ctrlPtC);
            }
            return true;
        }

        public static Form GetThisTabForm(Control thisControl)
        {
            if (thisControl == null) return null;

            while (thisControl.Parent != null && !(thisControl.Parent is TabPage))
            {
                thisControl = thisControl.Parent;
            }
            if (thisControl.Parent != null && thisControl.Parent is TabPage)
                return thisControl as Form;
            return null;
        }

        /// <summary>
        /// 清除子控件
        /// </summary>
        /// <param name="panel"></param>
        public static void ClearChildControls(Control panel)
        {
            while (panel.Controls.Count > 0)
            {
                if (panel.Controls[0] != null)
                    panel.Controls[0].Dispose();
            }
        }

        public static void SetDouble(Control control)
        {
            control.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic).SetValue(control, true, null);
        }


        public static void SetControlsAction(Control control, Action<Control> setAction)
        {
            if (control == null) return;
            setAction(control);
            if (control.Controls == null) return;
            //遍历所有控件
            foreach (Control sub in control.Controls)
            {
                SetControlsAction(sub, setAction);
            }
        }
        /// <summary>
        /// 画圆角矩形
        /// </summary>
        /// <param name="g"></param>
        /// <param name="pen"></param>
        /// <param name="rect"></param>
        /// <param name="cornerRadius"></param>
        public static void DrawRoundRectangle(Graphics g, Pen pen, Rectangle rect, int cornerRadius)
        {
            using (GraphicsPath path = rect.CreateRoundedRectanglePath(cornerRadius))
            {
                g.DrawPath(pen, path);
            }
        }

        /// <summary>
        /// 填充圆角矩形
        /// </summary>
        /// <param name="g"></param>
        /// <param name="brush"></param>
        /// <param name="rect"></param>
        /// <param name="cornerRadius"></param>
        public static void FillRoundRectangle(Graphics g, Brush brush, Rectangle rect, int cornerRadius)
        {
            using (GraphicsPath path = rect.CreateRoundedRectanglePath(cornerRadius))
            {
                g.FillPath(brush, path);
            }
        }

        /// <summary>
        /// 对象复制
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.");
            }

            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        #region 设置控件Enabled，切不改变控件颜色

        public static void SetControlEnabled(this Control c, bool enabled)
        {
            if (!c.IsDisposed)
            {
                if (enabled)
                {
                    ControlHelper.SetWindowLong(c.Handle, -16, -134217729 & ControlHelper.GetWindowLong(c.Handle, -16));
                }
                else
                {
                    ControlHelper.SetWindowLong(c.Handle, -16, 134217728 + ControlHelper.GetWindowLong(c.Handle, -16));
                }
            }
        }


        public static void SetControlEnableds(Control[] cs, bool enabled)
        {
            for (int i = 0; i < cs.Length; i++)
            {
                Control c = cs[i];
                SetControlEnabled(c, enabled);
            }
        }
        #endregion
        /// <summary>
        /// Sets the window long.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="nIndex">Index of the n.</param>
        /// <param name="wndproc">The wndproc.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("user32.dll ")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int wndproc);

        /// <summary>
        /// Gets the window long.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="nIndex">Index of the n.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("user32.dll ")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        /// <summary>
        /// Gets the foreground window.
        /// </summary>
        /// <returns>IntPtr.</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();

        /// <summary>
        /// Threads the base call back.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="obj">The object.</param>
        private static void ThreadBaseCallBack(Control parent, object obj)
        {
            if (obj is Exception)
            {
                if (parent != null)
                {
                    ThreadInvokerControl(parent, delegate
                    {
                        Exception ex = obj as Exception;
                    });
                }
            }
        }
        /// <summary>
        /// 委托调用主线程控件
        /// </summary>
        /// <param name="parent">主线程控件</param>
        /// <param name="action">修改控件方法</param>
        public static void ThreadInvokerControl(Control parent, Action action)
        {
            if (parent != null)
            {
                if (parent.InvokeRequired)
                {
                    parent.BeginInvoke(action);
                }
                else
                {
                    action();
                    SetForegroundWindow(parent.Handle);
                }
            }
        }

        /// <summary>
        /// Sets the foreground window.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);



        /// <summary>
        /// Converts to array.
        /// </summary>
        /// <param name="controls">The controls.</param>
        /// <returns>Control[].</returns>
        public static Control[] ToArray(this System.Windows.Forms.Control.ControlCollection controls)
        {
            if (controls == null || controls.Count <= 0)
                return new Control[0];
            List<Control> lst = new List<Control>();
            foreach (Control item in controls)
            {
                lst.Add(item);
            }
            return lst.ToArray();
        }


        #region 根据控件宽度截取字符串

        public static string GetSubString(
            string strSource,
            float fltControlWidth,
            System.Drawing.Graphics g,
            System.Drawing.Font font)
        {
            try
            {
                fltControlWidth = fltControlWidth - 20;
                strSource = strSource.Trim();
                while (true)
                {

                    System.Drawing.SizeF sizeF = g.MeasureString(strSource.Replace(" ", "A"), font);
                    if (sizeF.Width > fltControlWidth)
                    {
                        strSource = strSource.TrimEnd('…');
                        if (strSource.Length <= 1)
                            return "";
                        strSource = strSource.Substring(0, strSource.Length - 1).Trim() + "…";
                    }
                    else
                    {
                        return strSource;
                    }
                }
            }
            finally
            {
                g.Dispose();
            }
        }
        #endregion

        #region 获取字符串宽度

        public static int GetStringWidth(
           string strSource,
           System.Drawing.Graphics g,
           System.Drawing.Font font)
        {
            string[] strs = strSource.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            float fltWidth = 0;
            foreach (var item in strs)
            {
                System.Drawing.SizeF sizeF = g.MeasureString(strSource.Replace(" ", "A"), font);
                if (sizeF.Width > fltWidth)
                    fltWidth = sizeF.Width;
            }

            return (int)fltWidth;
        }
        #endregion

        #region 动画特效
        /// <summary>
        /// Animates the window.
        /// </summary>
        /// <param name="whnd">The WHND.</param>
        /// <param name="dwtime">The dwtime.</param>
        /// <param name="dwflag">The dwflag.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [DllImport("user32.dll")]
        public static extern bool AnimateWindow(IntPtr whnd, int dwtime, int dwflag);
        //dwflag的取值如下
        /// <summary>
        /// The aw hor positive
        /// </summary>
        public const Int32 AW_HOR_POSITIVE = 0x00000001;
        //从左到右显示
        /// <summary>
        /// The aw hor negative
        /// </summary>
        public const Int32 AW_HOR_NEGATIVE = 0x00000002;
        //从右到左显示
        /// <summary>
        /// The aw ver positive
        /// </summary>
        public const Int32 AW_VER_POSITIVE = 0x00000004;
        //从上到下显示
        /// <summary>
        /// The aw ver negative
        /// </summary>
        public const Int32 AW_VER_NEGATIVE = 0x00000008;
        //从下到上显示
        /// <summary>
        /// The aw center
        /// </summary>
        public const Int32 AW_CENTER = 0x00000010;
        //若使用了AW_HIDE标志，则使窗口向内重叠，即收缩窗口；否则使窗口向外扩展，即展开窗口
        /// <summary>
        /// The aw hide
        /// </summary>
        public const Int32 AW_HIDE = 0x00010000;
        //隐藏窗口，缺省则显示窗口
        /// <summary>
        /// The aw activate
        /// </summary>
        public const Int32 AW_ACTIVATE = 0x00020000;
        //激活窗口。在使用了AW_HIDE标志后不能使用这个标志
        /// <summary>
        /// The aw slide
        /// </summary>
        public const Int32 AW_SLIDE = 0x00040000;
        //使用滑动类型。缺省则为滚动动画类型。当使用AW_CENTER标志时，这个标志就被忽略
        /// <summary>
        /// The aw blend
        /// </summary>
        public const Int32 AW_BLEND = 0x00080000;
        //透明度从高到低
        #endregion

        #region 检查文本控件输入类型是否有效

        public static bool CheckInputType(
            string strValue,
            TextInputType inputType,
            decimal decMaxValue = default(decimal),
            decimal decMinValue = default(decimal),
            int intLength = 2,
            string strRegexPattern = null)
        {
            bool result;
            switch (inputType)
            {
                case TextInputType.NotControl:
                    result = true;
                    return result;
                case TextInputType.UnsignNumber:
                    if (string.IsNullOrEmpty(strValue))
                    {
                        result = true;
                        return result;
                    }
                    else
                    {
                        if (strValue.IndexOf("-") >= 0)
                        {
                            result = false;
                            return result;
                        }
                    }
                    break;
                case TextInputType.PositiveNumber:
                    if (string.IsNullOrEmpty(strValue))
                    {
                        result = true;
                        return result;
                    }
                    else
                    {
                        if (!Regex.IsMatch(strValue, "^\\d*(\\.?\\d*)?$"))
                        {
                            result = false;
                            return result;
                        }
                    }
                    break;
                case TextInputType.Number:
                    if (string.IsNullOrEmpty(strValue))
                    {
                        result = true;
                        return result;
                    }
                    else
                    {
                        if (!Regex.IsMatch(strValue, "^-?\\d*(\\.?\\d*)?$"))
                        {
                            result = false;
                            return result;
                        }
                    }
                    break;
                case TextInputType.Integer:
                    if (string.IsNullOrEmpty(strValue))
                    {
                        result = true;
                        return result;
                    }
                    else
                    {
                        if (!Regex.IsMatch(strValue, "^-?\\d*$"))
                        {
                            result = false;
                            return result;
                        }
                    }
                    break;
                case TextInputType.PositiveInteger:
                    if (string.IsNullOrEmpty(strValue))
                    {
                        result = true;
                        return result;
                    }
                    else
                    {
                        if (!Regex.IsMatch(strValue, "^\\d+$"))
                        {
                            result = false;
                            return result;
                        }
                    }
                    break;
                case TextInputType.Regex:
                    result = (string.IsNullOrEmpty(strRegexPattern) || Regex.IsMatch(strValue, strRegexPattern));
                    return result;
            }
            if (strValue == "-")
            {
                return true;
            }
            decimal d;
            if (!decimal.TryParse(strValue, out d))
            {
                result = false;
            }
            else if (d < decMinValue || d > decMaxValue)
            {
                result = false;
            }
            else
            {
                if (inputType == TextInputType.Number || inputType == TextInputType.UnsignNumber || inputType == TextInputType.PositiveNumber)
                {
                    if (strValue.IndexOf(".") >= 0)
                    {
                        string text = strValue.Substring(strValue.IndexOf("."));
                        if (text.Length > intLength + 1)
                        {
                            result = false;
                            return result;
                        }
                    }
                }
                result = true;
            }
            return result;
        }
        #endregion

        #region 冻结控件
        /// <summary>
        /// The m LST freeze control
        /// </summary>
        static Dictionary<Control, bool> m_lstFreezeControl = new Dictionary<Control, bool>();

        public static void FreezeControl(Control control, bool blnToFreeze)
        {
            if (blnToFreeze && control.IsHandleCreated && control.Visible && !control.IsDisposed && (!m_lstFreezeControl.ContainsKey(control) || (m_lstFreezeControl.ContainsKey(control) && m_lstFreezeControl[control] == false)))
            {
                m_lstFreezeControl[control] = true;
                control.Disposed += control_Disposed;
                NativeMethods.SendMessage(control.Handle, 11, 0, 0);
            }
            else if (!blnToFreeze && !control.IsDisposed && m_lstFreezeControl.ContainsKey(control) && m_lstFreezeControl[control] == true)
            {
                m_lstFreezeControl.Remove(control);
                NativeMethods.SendMessage(control.Handle, 11, 1, 0);
                control.Invalidate(true);
            }
        }

        /// <summary>
        /// Handles the Disposed event of the control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        static void control_Disposed(object sender, EventArgs e)
        {
            try
            {
                if (m_lstFreezeControl.ContainsKey((Control)sender))
                    m_lstFreezeControl.Remove((Control)sender);
            }
            catch { }
        }
        #endregion

        /// <summary>
        /// 设置GDI高质量模式抗锯齿
        /// </summary>
        /// <param name="g">The g.</param>
        public static void SetGDIHigh(this Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;  //使绘图质量最高，即消除锯齿
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;
        }

        public static Point GetCenterRangeLocation(this Rectangle rect, Size size)
        {
            return new Point((rect.Width - size.Width) / 2, (rect.Height - size.Height) / 2);
        }
        /// <summary>
        /// 根据矩形和圆得到一个圆角矩形Path
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="cornerRadius">The corner radius.</param>
        /// <returns>GraphicsPath.</returns>
        public static GraphicsPath CreateRoundedRectanglePath(this Rectangle rect, int cornerRadius)
        {
            GraphicsPath roundedRect = new GraphicsPath();
            roundedRect.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
            roundedRect.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
            roundedRect.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
            roundedRect.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
            roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            roundedRect.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);
            roundedRect.CloseFigure();
            return roundedRect;
        }

        /// <summary>
        /// Creates the rounded rectangle path.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="cornerRadius">The corner radius.</param>
        /// <returns>GraphicsPath.</returns>
        public static GraphicsPath CreateRoundedRectanglePath(this RectangleF rect, int cornerRadius)
        {
            GraphicsPath roundedRect = new GraphicsPath();
            roundedRect.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
            roundedRect.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
            roundedRect.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
            roundedRect.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
            roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            roundedRect.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);
            roundedRect.CloseFigure();
            return roundedRect;
        }
        /// <summary>
        /// Gets the colors.
        /// </summary>
        /// <value>The colors.</value>
        public static Color[] Colors { get; private set; }

        static ControlHelper()
        {
            List<Color> list = new List<Color>();
            list.Add(Color.FromArgb(55, 162, 218));
            list.Add(Color.FromArgb(50, 197, 233));
            list.Add(Color.FromArgb(103, 224, 227));
            list.Add(Color.FromArgb(159, 230, 184));
            list.Add(Color.FromArgb(255, 219, 92));
            list.Add(Color.FromArgb(255, 159, 127));
            list.Add(Color.FromArgb(251, 114, 147));
            list.Add(Color.FromArgb(224, 98, 174));
            list.Add(Color.FromArgb(230, 144, 209));
            list.Add(Color.FromArgb(231, 188, 243));
            list.Add(Color.FromArgb(157, 150, 245));
            list.Add(Color.FromArgb(131, 120, 234));
            list.Add(Color.FromArgb(150, 191, 255));

            list.Add(Color.FromArgb(243, 67, 54));
            list.Add(Color.FromArgb(156, 39, 176));
            list.Add(Color.FromArgb(103, 58, 183));
            list.Add(Color.FromArgb(63, 81, 181));
            list.Add(Color.FromArgb(33, 150, 243));
            list.Add(Color.FromArgb(0, 188, 211));
            list.Add(Color.FromArgb(3, 169, 244));
            list.Add(Color.FromArgb(0, 150, 136));
            list.Add(Color.FromArgb(139, 195, 74));
            list.Add(Color.FromArgb(76, 175, 80));
            list.Add(Color.FromArgb(204, 219, 57));
            list.Add(Color.FromArgb(233, 30, 99));
            list.Add(Color.FromArgb(254, 234, 59));
            list.Add(Color.FromArgb(254, 192, 7));
            list.Add(Color.FromArgb(254, 152, 0));
            list.Add(Color.FromArgb(255, 87, 34));
            list.Add(Color.FromArgb(121, 85, 72));
            list.Add(Color.FromArgb(158, 158, 158));
            list.Add(Color.FromArgb(96, 125, 139));
            list.Add(Color.FromArgb(252, 117, 85));
            list.Add(Color.FromArgb(172, 113, 191));
            list.Add(Color.FromArgb(115, 131, 253));
            list.Add(Color.FromArgb(78, 206, 255));
            list.Add(Color.FromArgb(121, 195, 82));
            list.Add(Color.FromArgb(255, 163, 28));
            list.Add(Color.FromArgb(255, 185, 15));
            list.Add(Color.FromArgb(255, 181, 197));
            list.Add(Color.FromArgb(255, 110, 180));
            list.Add(Color.FromArgb(255, 69, 0));
            list.Add(Color.FromArgb(255, 48, 48));
            list.Add(Color.FromArgb(154, 205, 50));
            list.Add(Color.FromArgb(155, 205, 155));
            list.Add(Color.FromArgb(154, 50, 205));
            list.Add(Color.FromArgb(131, 111, 255));
            list.Add(Color.FromArgb(124, 205, 124));
            list.Add(Color.FromArgb(0, 206, 209));
            list.Add(Color.FromArgb(0, 178, 238));
            list.Add(Color.FromArgb(56, 142, 142));

            Type typeFromHandle = typeof(Color);
            PropertyInfo[] properties = typeFromHandle.GetProperties();
            PropertyInfo[] array = properties;
            for (int i = 0; i < array.Length; i++)
            {
                PropertyInfo propertyInfo = array[i];
                if (propertyInfo.PropertyType == typeof(Color) && (propertyInfo.Name.StartsWith("Dark") || propertyInfo.Name.StartsWith("Medium")))
                {
                    object value = propertyInfo.GetValue(null, null);
                    list.Add((Color)value);
                }
            }
            Colors = list.ToArray();
        }
        /// <summary>
        /// Draws the string.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="s">The s.</param>
        /// <param name="font">The font.</param>
        /// <param name="brush">The brush.</param>
        /// <param name="point">The point.</param>
        /// <param name="format">The format.</param>
        /// <param name="angle">The angle.</param>
        public static void DrawString(Graphics g, string s, Font font, Brush brush, PointF point, StringFormat format, float angle)
        {
            Matrix transform = g.Transform;
            Matrix transform2 = g.Transform;
            transform2.RotateAt(angle, point);
            g.Transform = transform2;
            g.DrawString(s, font, brush, point, format);
            g.Transform = transform;
        }

        /// <summary>
        /// Gets the rhombus from rectangle.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <returns>Point[].</returns>
        public static Point[] GetRhombusFromRectangle(Rectangle rect)
        {
            return new Point[5]
            {
                new Point(rect.X, rect.Y + rect.Height / 2),
                new Point(rect.X + rect.Width / 2, rect.Y + rect.Height - 1),
                new Point(rect.X + rect.Width - 1, rect.Y + rect.Height / 2),
                new Point(rect.X + rect.Width / 2, rect.Y),
                new Point(rect.X, rect.Y + rect.Height / 2)
            };
        }

        /// <summary>
        /// Computes the paint location y.
        /// </summary>
        /// <param name="max">The maximum.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="height">The height.</param>
        /// <param name="value">The value.</param>
        /// <returns>System.Single.</returns>
        public static float ComputePaintLocationY(int max, int min, int height, int value)
        {
            if ((float)(max - min) == 0f)
            {
                return height;
            }
            return (float)height - (float)(value - min) * 1f / (float)(max - min) * (float)height;
        }

        /// <summary>
        /// Computes the paint location y.
        /// </summary>
        /// <param name="max">The maximum.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="height">The height.</param>
        /// <param name="value">The value.</param>
        /// <returns>System.Single.</returns>
        public static float ComputePaintLocationY(float max, float min, float height, float value)
        {
            if (max - min == 0f)
            {
                return height;
            }
            return height - (value - min) / (max - min) * height;
        }


        /// <summary>
        /// Paints the coordinate divide.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="penLine">The pen line.</param>
        /// <param name="penDash">The pen dash.</param>
        /// <param name="font">The font.</param>
        /// <param name="brush">The brush.</param>
        /// <param name="sf">The sf.</param>
        /// <param name="degree">The degree.</param>
        /// <param name="max">The maximum.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <param name="up">Up.</param>
        /// <param name="down">Down.</param>
        public static void PaintCoordinateDivide(Graphics g, System.Drawing.Pen penLine, System.Drawing.Pen penDash, Font font, System.Drawing.Brush brush, StringFormat sf, int degree, int max, int min, int width, int height, int left = 60, int right = 8, int up = 8, int down = 8)
        {
            for (int i = 0; i <= degree; i++)
            {
                int value = (max - min) * i / degree + min;
                int num = (int)ComputePaintLocationY(max, min, height - up - down, value) + up + 1;
                g.DrawLine(penLine, left - 1, num, left - 4, num);
                if (i != 0)
                {
                    g.DrawLine(penDash, left, num, width - right, num);
                }
                g.DrawString(value.ToString(), font, brush, new Rectangle(-5, num - font.Height / 2, left, font.Height), sf);
            }
        }

        /// <summary>
        /// Paints the triangle.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="brush">The brush.</param>
        /// <param name="point">The point.</param>
        /// <param name="size">The size.</param>
        /// <param name="direction">The direction.</param>
        public static void PaintTriangle(Graphics g, System.Drawing.Brush brush, Point point, int size, GraphDirection direction)
        {
            Point[] array = new Point[4];
            switch (direction)
            {
                case GraphDirection.Leftward:
                    array[0] = new Point(point.X, point.Y - size);
                    array[1] = new Point(point.X, point.Y + size);
                    array[2] = new Point(point.X - 2 * size, point.Y);
                    break;
                case GraphDirection.Rightward:
                    array[0] = new Point(point.X, point.Y - size);
                    array[1] = new Point(point.X, point.Y + size);
                    array[2] = new Point(point.X + 2 * size, point.Y);
                    break;
                case GraphDirection.Upward:
                    array[0] = new Point(point.X - size, point.Y);
                    array[1] = new Point(point.X + size, point.Y);
                    array[2] = new Point(point.X, point.Y - 2 * size);
                    break;
                default:
                    array[0] = new Point(point.X - size, point.Y);
                    array[1] = new Point(point.X + size, point.Y);
                    array[2] = new Point(point.X, point.Y + 2 * size);
                    break;
            }
            array[3] = array[0];
            g.FillPolygon(brush, array);
        }

        /// <summary>
        /// Paints the triangle.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="brush">The brush.</param>
        /// <param name="point">The point.</param>
        /// <param name="size">The size.</param>
        /// <param name="direction">The direction.</param>
        public static void PaintTriangle(Graphics g, System.Drawing.Brush brush, PointF point, int size, GraphDirection direction)
        {
            PointF[] array = new PointF[4];
            switch (direction)
            {
                case GraphDirection.Leftward:
                    array[0] = new PointF(point.X, point.Y - (float)size);
                    array[1] = new PointF(point.X, point.Y + (float)size);
                    array[2] = new PointF(point.X - (float)(2 * size), point.Y);
                    break;
                case GraphDirection.Rightward:
                    array[0] = new PointF(point.X, point.Y - (float)size);
                    array[1] = new PointF(point.X, point.Y + (float)size);
                    array[2] = new PointF(point.X + (float)(2 * size), point.Y);
                    break;
                case GraphDirection.Upward:
                    array[0] = new PointF(point.X - (float)size, point.Y);
                    array[1] = new PointF(point.X + (float)size, point.Y);
                    array[2] = new PointF(point.X, point.Y - (float)(2 * size));
                    break;
                default:
                    array[0] = new PointF(point.X - (float)size, point.Y);
                    array[1] = new PointF(point.X + (float)size, point.Y);
                    array[2] = new PointF(point.X, point.Y + (float)(2 * size));
                    break;
            }
            array[3] = array[0];
            g.FillPolygon(brush, array);
        }

        /// <summary>
        /// Adds the array data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <param name="data">The data.</param>
        /// <param name="max">The maximum.</param>
        public static void AddArrayData<T>(ref T[] array, T[] data, int max)
        {
            if (data == null || data.Length == 0)
            {
                return;
            }
            if (array.Length == max)
            {
                Array.Copy(array, data.Length, array, 0, array.Length - data.Length);
                Array.Copy(data, 0, array, array.Length - data.Length, data.Length);
            }
            else if (array.Length + data.Length > max)
            {
                T[] array2 = new T[max];
                for (int i = 0; i < max - data.Length; i++)
                {
                    array2[i] = array[i + (array.Length - max + data.Length)];
                }
                for (int j = 0; j < data.Length; j++)
                {
                    array2[array2.Length - data.Length + j] = data[j];
                }
                array = array2;
            }
            else
            {
                T[] array3 = new T[array.Length + data.Length];
                for (int k = 0; k < array.Length; k++)
                {
                    array3[k] = array[k];
                }
                for (int l = 0; l < data.Length; l++)
                {
                    array3[array3.Length - data.Length + l] = data[l];
                }
                array = array3;
            }
        }

        /// <summary>
        /// Converts the size.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <param name="angle">The angle.</param>
        /// <returns>SizeF.</returns>
        public static SizeF ConvertSize(SizeF size, float angle)
        {
            System.Drawing.Drawing2D.Matrix matrix = new System.Drawing.Drawing2D.Matrix();
            matrix.Rotate(angle);
            PointF[] array = new PointF[4];
            array[0].X = (0f - size.Width) / 2f;
            array[0].Y = (0f - size.Height) / 2f;
            array[1].X = (0f - size.Width) / 2f;
            array[1].Y = size.Height / 2f;
            array[2].X = size.Width / 2f;
            array[2].Y = size.Height / 2f;
            array[3].X = size.Width / 2f;
            array[3].Y = (0f - size.Height) / 2f;
            matrix.TransformPoints(array);
            float num = float.MaxValue;
            float num2 = float.MinValue;
            float num3 = float.MaxValue;
            float num4 = float.MinValue;
            PointF[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                PointF pointF = array2[i];
                if (pointF.X < num)
                {
                    num = pointF.X;
                }
                if (pointF.X > num2)
                {
                    num2 = pointF.X;
                }
                if (pointF.Y < num3)
                {
                    num3 = pointF.Y;
                }
                if (pointF.Y > num4)
                {
                    num4 = pointF.Y;
                }
            }
            return new SizeF(num2 - num, num4 - num3);
        }



        /// <summary>
        /// Gets the pow.
        /// </summary>
        /// <param name="digit">The digit.</param>
        /// <returns>System.Int32.</returns>
        private static int GetPow(int digit)
        {
            int num = 1;
            for (int i = 0; i < digit; i++)
            {
                num *= 10;
            }
            return num;
        }

        /// <summary>
        /// Calculates the maximum section from.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>System.Int32.</returns>
        public static double CalculateMaxSectionFrom(double[] values)
        {
            double num = values.Max();
            return CalculateMaxSection(num);
        }

        public static double CalculateMaxSectionFrom(double[][] values)
        {
            double num = values.Max(p => p.Max());
            return CalculateMaxSection(num);
        }

        private static double CalculateMaxSection(double num)
        {
            if (num <= 5)
            {
                return 5;
            }
            if (num <= 10)
            {
                return 10;
            }
            int digit = num.ToString().Length - 2;
            int num2 = int.Parse(num.ToString().Substring(0, 2));
            if (num2 < 12)
            {
                return 12 * GetPow(digit);
            }
            if (num2 < 14)
            {
                return 14 * GetPow(digit);
            }
            if (num2 < 16)
            {
                return 16 * GetPow(digit);
            }
            if (num2 < 18)
            {
                return 18 * GetPow(digit);
            }
            if (num2 < 20)
            {
                return 20 * GetPow(digit);
            }
            if (num2 < 22)
            {
                return 22 * GetPow(digit);
            }
            if (num2 < 24)
            {
                return 24 * GetPow(digit);
            }
            if (num2 < 26)
            {
                return 26 * GetPow(digit);
            }
            if (num2 < 28)
            {
                return 28 * GetPow(digit);
            }
            if (num2 < 30)
            {
                return 30 * GetPow(digit);
            }
            if (num2 < 40)
            {
                return 40 * GetPow(digit);
            }
            if (num2 < 50)
            {
                return 50 * GetPow(digit);
            }
            if (num2 < 60)
            {
                return 60 * GetPow(digit);
            }
            if (num2 < 80)
            {
                return 80 * GetPow(digit);
            }
            return 100 * GetPow(digit);
        }

        /// <summary>
        /// Gets the color light.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns>System.Drawing.Color.</returns>
        public static System.Drawing.Color GetColorLight(System.Drawing.Color color)
        {
            return System.Drawing.Color.FromArgb(color.R + (255 - color.R) * 40 / 100, color.G + (255 - color.G) * 40 / 100, color.B + (255 - color.B) * 40 / 100);
        }

        /// <summary>
        /// Gets the color light five.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns>System.Drawing.Color.</returns>
        public static System.Drawing.Color GetColorLightFive(System.Drawing.Color color)
        {
            return System.Drawing.Color.FromArgb(color.R + (255 - color.R) * 50 / 100, color.G + (255 - color.G) * 50 / 100, color.B + (255 - color.B) * 50 / 100);
        }

        /// <summary>
        /// Gets the points from.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <param name="soureWidth">Width of the soure.</param>
        /// <param name="sourceHeight">Height of the source.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="dx">The dx.</param>
        /// <param name="dy">The dy.</param>
        /// <returns>PointF[].</returns>
        public static PointF[] GetPointsFrom(string points, float soureWidth, float sourceHeight, float width, float height, float dx = 0f, float dy = 0f)
        {
            string[] array = points.Split(new char[1]
            {
                ' '
            }, StringSplitOptions.RemoveEmptyEntries);
            PointF[] array2 = new PointF[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                int num = array[i].IndexOf(',');
                float num2 = Convert.ToSingle(array[i].Substring(0, num));
                float num3 = Convert.ToSingle(array[i].Substring(num + 1));
                array2[i] = new PointF(width * (num2 + dx) / soureWidth, height * (num3 + dy) / sourceHeight);
            }
            return array2;
        }

      

        #region 滚动条    English:scroll bar
        static uint SB_HORZ = 0x0;
        static uint SB_VERT = 0x1;
        static uint SB_CTL = 0x2;
        static uint SB_BOTH = 0x3;
        [DllImport("user32.dll", SetLastError = true, EntryPoint = "GetScrollInfo")]
        private static extern int GetScrollInfo(IntPtr hWnd, uint idObject, ref SCROLLINFO psbi);

        /// <summary>
        ///获取水平滚动条信息
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <returns>Scrollbarinfo.</returns>
        public static SCROLLINFO GetHScrollBarInfo(IntPtr hWnd)
        {
            SCROLLINFO info = new SCROLLINFO();
            info.cbSize = (uint)Marshal.SizeOf(info);
            info.fMask = (int)ScrollInfoMask.SIF_ALL;
            int intRef = GetScrollInfo(hWnd, SB_HORZ, ref info);
            return info;
        }
        /// <summary>
        /// 获取垂直滚动条信息
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <returns>Scrollbarinfo.</returns>
        public static SCROLLINFO GetVScrollBarInfo(IntPtr hWnd)
        {
            SCROLLINFO info = new SCROLLINFO();
            info.cbSize = (uint)Marshal.SizeOf(info);
            info.fMask = (int)ScrollInfoMask.SIF_ALL;
            int intRef = GetScrollInfo(hWnd, SB_VERT, ref info);
            return info;
        }
        public struct SCROLLINFO
        {
            public uint cbSize;
            public uint fMask;
            public int nMin;
            public int nMax;
            public uint nPage;
            public int nPos;
            public int nTrackPos;
        }
        public enum ScrollInfoMask : uint
        {
            SIF_RANGE = 0x1,
            SIF_PAGE = 0x2,
            SIF_POS = 0x4,
            SIF_DISABLENOSCROLL = 0x8,
            SIF_TRACKPOS = 0x10,
            SIF_ALL = (SIF_RANGE | SIF_PAGE | SIF_POS | SIF_TRACKPOS),
        }
        #endregion


        /// <summary>
        /// 返回指定图片中的非透明区域；
        /// </summary>
        /// <param name="img">位图</param>
        /// <returns></returns>
        public static GraphicsPath CalculateControlGraphicsPath(Bitmap bitmap, Color? colorTransparent = null)
        {
            // Create GraphicsPath for our bitmap calculation 
            //创建 GraphicsPath
            GraphicsPath graphicsPath = new GraphicsPath();
            // Use the top left pixel as our transparent color 
            //使用左上角的一点的颜色作为我们透明色

            Color _colorTransparent = bitmap.GetPixel(0, 0);
            if (colorTransparent != null && colorTransparent != Color.Transparent && colorTransparent != Color.Empty)
                _colorTransparent = colorTransparent.Value;
            // This is to store the column value where an opaque pixel is first found. 
            // This value will determine where we start scanning for trailing opaque pixels.
            //第一个找到点的X
            int colOpaquePixel = 0;
            // Go through all rows (Y axis) 
            // 偏历所有行（Y方向）
            for (int row = 0; row < bitmap.Height; row++)
            {
                // Reset value 
                //重设
                colOpaquePixel = 0;
                // Go through all columns (X axis) 
                //偏历所有列（X方向）
                for (int col = 0; col < bitmap.Width; col++)
                {
                    // If this is an opaque pixel, mark it and search for anymore trailing behind 
                    //如果是不需要透明处理的点则标记，然后继续偏历
                    if (bitmap.GetPixel(col, row) != _colorTransparent)
                    {
                        // Opaque pixel found, mark current position
                        //记录当前
                        colOpaquePixel = col;
                        // Create another variable to set the current pixel position 
                        //建立新变量来记录当前点
                        int colNext = col;
                        // Starting from current found opaque pixel, search for anymore opaque pixels 
                        // trailing behind, until a transparent   pixel is found or minimum width is reached 
                        ///从找到的不透明点开始，继续寻找不透明点,一直到找到或则达到图片宽度 
                        for (colNext = colOpaquePixel; colNext < bitmap.Width; colNext++)
                            if (bitmap.GetPixel(colNext, row) == _colorTransparent)
                                break;
                        // Form a rectangle for line of opaque   pixels found and add it to our graphics path 
                        //将不透明点加到graphics path
                        graphicsPath.AddRectangle(new Rectangle(colOpaquePixel, row, colNext - colOpaquePixel, 1));
                        // No need to scan the line of opaque pixels just found 
                        col = colNext;
                    }
                }
            }
            // Return calculated graphics path 
            return graphicsPath;
        }
    }
}

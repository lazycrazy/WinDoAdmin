
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Diagnostics;


namespace WinDoControls.Controls
{

    //自定义控件：垂直滚动条
    //作者：Sunday若雪   时间：2016年12月1日
    //控件介绍：无上下箭头 无右键菜单 滑道和滑块颜色可调 滑块大小可调
    //SliderWidthPercent（滑块宽度与控件宽度的比例）：改变值为0-1之间的浮点数
    //SliderHeight(滑块长度)： SliderHeight的最短长度也有限制 为了保证滑块显示美观   SliderHeight最短为Wdith（控件宽度）
    //Wdith(控件宽度)：Wdith的设定的最小宽度为2 最大宽度为100  太大宽度也没什么用
    //Height(控件长度)：注意  由于MaxValue固定值为100 当控件可滑动部分长度小于100像素时为了保证算法运行正常 Height（控件长度）会随着SliderHeight的增大而变大

    /*使用注意事项：必须开启SetProcessDPIAware函数，防止UI自动放大*/

    /*特别说明 由于调试的时候调试器时刻在监视程序运行 或者说是由调试器发送执行指令使程序运行 程序运行速度
     * 会大幅度下降 导致控件重绘速度变慢 所以如果调试时滑动滑块发卡 那么程序脱离调试器一般就不会发卡了*/
    //float length = (float)panel1.Height / (float)pictureBox2.Height * (float)panel1.Height;
    //sundayRXScrollBar1.SliderHeight = (int) length;
    //        this.sundayRXScrollBar1.ValueChanged += SundayRXScrollBar1_ValueChanged;
    //        pictureBox2.MouseWheel += PictureBox2_MouseWheel;
    //private void PictureBox2_MouseWheel(object sender, MouseEventArgs e)
    //{
    //    if (e.Delta > 0)
    //    {
    //        this.sundayRXScrollBar1.Value -= this.sundayRXScrollBar1.SmallChange * 5;
    //    }
    //    else
    //    {
    //        this.sundayRXScrollBar1.Value += this.sundayRXScrollBar1.SmallChange * 5;
    //    }
    //}
    //private void SundayRXScrollBar1_ValueChanged(object sender, EventArgs e)
    //{
    //    this.pictureBox2.Top = -Convert.ToInt32((float)sundayRXScrollBar1.Value / 100 * (float)(this.pictureBox2.Height - panel1.Height));
    //}

    public class SundayRXScrollBar : UserControl
    {


        //标志性变量*****************************************************************
        //
        protected bool MouseDownSliderFlag = false;//鼠标按下滑块标志
        protected bool MouseOverSliderFlag = false;//鼠标在滑块上方标志
        protected bool MouseDownOverSliderFlag = false;//鼠标按下滑块并在滑块上方移动标志

        //小范围使用的全局变量*******************************************************
        //
        protected int DIstance;//鼠标按下的位置与滑块顶端的距离

        protected int LastSliderPointY;//SliderPointY刷新之前的值 每次在刷新SliderPointY之前都要刷新该值

        //大范围使用的全局变量********************************************************
        //

        /*必须注意这些大范围使用的全局变量之间的关联关系 
         * 任何一个该变量的组成变量的改动都要重新计算这个变量的值*/

        protected int SliderPointX;//Width  SliderWidthPercent_
        protected int SliderPointY;   //Value  Heigth  SliderHeight_
        protected int SliderWidth; //Width   SliderWidthPercent_
        protected Point MouseDownPoint;
        protected Point MousePoint;

        //大范围使用的方法*******************************************************
        //一些变量的值的更新方法
        protected void GetSliderPointX()//作为更新SliderPointX的值的方法
        {
            if (SliderWidthPercent_ == 1)
            {
                SliderPointX = 0;
            }
            else
            {
                SliderPointX = (int)(Width * (1 - SliderWidthPercent_) / 2) + 1;
            }
        }

        protected void GetSliderPointY()//作为更新SliderPointY的值的方法
        {
            if (MouseDownOverSliderFlag == false)//当使用鼠标拖动使得Value改变时不要用再用Value计算SliderPointY
            {
                LastSliderPointY = SliderPointY;
                //计算出准确位置的浮点数
                float WantSliberPointY = Value_ * ((float)(Height - SliderHeight_) / 100);
                //取这个浮点数的左右整数
                int WantSliberPointY_Up = (int)WantSliberPointY;
                int WantSliberPointY_Down = WantSliberPointY_Up + 1;
                //用这两个整数假设作为当前滑块位置Y坐标，分别进行反计算出Value
                float WantSliberPointY_Up_FloatValue = WantSliberPointY_Up / ((float)(Height - SliderHeight_) / 100);
                float WantSliberPointY_Down_FloatValue = WantSliberPointY_Down / ((float)(Height - SliderHeight_) / 100);

                int WantSliberPointY_Up_IntValue = (int)WantSliberPointY_Up_FloatValue;
                int WantSliberPointY_Down_IntValue = (int)WantSliberPointY_Down_FloatValue;

                if (WantSliberPointY_Up_FloatValue > WantSliberPointY_Up_IntValue)
                {
                    WantSliberPointY_Up_IntValue++;
                }
                if (WantSliberPointY_Down_FloatValue > WantSliberPointY_Down_IntValue)
                {
                    WantSliberPointY_Down_IntValue++;
                }

                //判断两个假设Value值哪个是Value，给SliderPointY赋值

                if (Value_ == WantSliberPointY_Up_IntValue)
                {
                    SliderPointY = WantSliberPointY_Up;
                }
                else
                {
                    SliderPointY = WantSliberPointY_Down;
                }
            }
        }

        protected void GetSliderWidth()//作为更新SliderWidth的值的方法
        {
            if (SliderWidthPercent_ == 1)//由于浮点数计算的误差 对于SliderWidthPercent=1的情况进行特殊处理
            {
                SliderWidth = Width;
            }
            else                                          //普通情况的浮点数计算
            {
                SliderWidth = (int)(Width * SliderWidthPercent_) - 1;
            }
        }

        protected void GetValue()//作为更新Value的值的方法
        {
            float SliderPointY_FloatValue = SliderPointY / ((float)(Height - SliderHeight_) / 100);

            int SliderPointY_IntValue = (int)SliderPointY_FloatValue;
            if (SliderPointY_FloatValue > SliderPointY_IntValue)
            {
                SliderPointY_IntValue++;
            }
            Value = SliderPointY_IntValue;
        }

        protected void GetMousePoint()//作为更新MousePoint的值的方法
        {
            MousePoint = PointToClient(Cursor.Position);
        }

        protected void GetMouseDownPoint()//作为更新MouseDownPoint的值的方法
        {
            MouseDownPoint = PointToClient(Cursor.Position);
        }

        //判断鼠标所在位置方法
        protected bool IsMouseOverSlider()//判断鼠标是否在滑块上方
        {
            GetMousePoint();
            if (MousePoint.Y >= SliderPointY && MousePoint.Y <= SliderPointY + SliderHeight_ && MousePoint.X >= SliderPointX && MousePoint.X <= SliderPointX + SliderWidth)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected bool IsMouseOverBottom()//判断鼠标是否在滑道上
        {
            GetMousePoint();
            if (MousePoint.Y < SliderPointY || MousePoint.Y > SliderPointY + SliderHeight_)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected bool IsMouseOverControlEdge()//判断鼠标是否处于控件边缘部分
        {
            GetMousePoint();
            if (MousePoint.X == 0 || MousePoint.X == Width - 1 || MousePoint.Y == 0 || MousePoint.Y == Height - 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //小范围使用的方法*************************************************************
        //

        protected void OnMouseOverSliderEvent()//鼠标在滑块上方悬浮时的事件
        {
            if (IsMouseOverSlider())
            {
                MouseOverSliderFlag = true;
            }
            else
            {
                MouseOverSliderFlag = false;
            }
            if (IsMouseOverControlEdge())
            {
                MouseOverSliderFlag = false;
            }
            Invalidate(new Region(new RectangleF(new Point(SliderPointX, SliderPointY), new Size(SliderWidth, SliderHeight_))));
        }

        //窗体设计器所添加或重写的变量******************************************************
        //

        /*设计器变量和内部变量使用规则：读数据使用内部变量（变量名带_的）
         * 写数据使用设计器变量*/
        /*直接给这些内部变量赋值相当于给了控件属性的初始值*/
        protected Color BottomColor_ = Color.FromArgb(62, 62, 66);
        protected double SliderWidthPercent_ = 0.5;
        protected Color SliderColor_ = Color.FromArgb(104, 104, 104);
        protected int SliderHeight_ = 100;
        protected Color MouseDownSliderColor_ = Color.FromArgb(239, 235, 239);
        protected Color MouseOverSliderColor_ = Color.FromArgb(158, 158, 158);
        protected int Value_ = 0;
        protected int SmallChange_ = 1;

        /*让变量显示在设计器中 连接设计器变量和内部变量  更改这两个变量的任
         * 何一个另一个都会被更改并触发Set中的代码  */
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("外观"), Description("滑道颜色")]
        public Color BottomColor
        {
            set/*在内部变量被赋值之前设计器变量还没有被更改*/
            {/*无论更改设计器变量 还是更改内部变量 都是先将要更改值的传递给value*/

                //输入安全性效验代码 对value的值进行操作

                BottomColor_ = value;//把value值赋给BottomColor_ 

                //对组成变量含有该变量的关联变量进行值的更新

                Invalidate();                 //重绘控件

                //处理与该变量相关的事件

            }
            get { return BottomColor_; }//把BottomColor_值赋给BottomColor
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("外观"), Description("滑块宽度相对于控件宽度的百分比 范围0-1")]
        public double SliderWidthPercent
        {
            set
            {
                if (value > 1)
                {
                    SliderWidthPercent_ = 1;
                }
                else if (value < 0)
                {
                    SliderWidthPercent_ = 0;
                }
                else
                {
                    SliderWidthPercent_ = value;
                }
                //处理关联变量SliderPointX
                GetSliderPointX();
                //处理关联变量SliderWidth
                GetSliderWidth();

                Invalidate();
            }
            get { return SliderWidthPercent_; }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("外观"), Description("滑块的颜色")]
        public Color SliderColor
        {
            set
            {
                SliderColor_ = value;
                Invalidate();
            }
            get { return SliderColor_; }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("外观"), Description("当指针按下滑块时滑块的颜色")]
        public Color MouseDownSliderColor
        {
            set
            {
                MouseDownSliderColor_ = value;
                Invalidate();
            }
            get { return MouseDownSliderColor_; }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("外观"), Description("当指针悬浮于滑块上时滑块的颜色")]
        public Color MouseOverSliderColor
        {
            set
            {
                MouseOverSliderColor_ = value;
                Invalidate();
            }
            get { return MouseOverSliderColor_; }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("行为"), Description("滚动条当前值  范围0-100")]
        public int Value
        {
            set
            {
                // 传入值合法性效验
                if (value > 100)
                {
                    value = 100;
                }
                else if (value < 0)
                {
                    value = 0;
                }
                //赋值
                Value_ = value;
                //处理关联变量SliderPointY
                GetSliderPointY();
                //重绘控件
                /*重绘控件必须要注意重绘控件所需的时间 只重绘改变的区域 不改变的不重绘*/
                if (SliderPointY - LastSliderPointY > 0)
                {
                    Invalidate(new Region(new RectangleF(new Point(SliderPointX, LastSliderPointY), new Size(SliderWidth, SliderPointY + SliderHeight_ - LastSliderPointY + 2))));
                }
                else if (SliderPointY - LastSliderPointY < 0)
                {
                    //由于存在向上滑动时滑块的上半圆会先消失一次的BUG 所以我改成了从顶部 到上一次滑块底边位置全部重画 如果发生卡顿 请更改绘制区域 减小重绘时间
                    Invalidate(new Region(new RectangleF(new Point(SliderPointX, 0), new Size(SliderWidth, LastSliderPointY + SliderHeight_))));
                }
                //处理事件ValueChanged
                if (ValueChanged != null)
                    ValueChanged(this, new EventArgs());
                //处理事件Scroll
                base.OnScroll(new ScrollEventArgs(ScrollEventType.ThumbPosition, Value_));
            }
            get
            {
                return Value_;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("外观"), Description("滚动条长度 ")]
        public int SliderHeight
        {
            set
            {
                // 输入合法性效验
                if (value <= Width)
                {
                    value = Width;
                }
                //if (Height - value < 100)
                //{
                //    Height = value + 100;
                //}

                SliderHeight_ = value;
                //处理关联变量SliderPointY
                GetSliderPointY();

                Invalidate();
            }
            get { return SliderHeight_; }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("行为"), Description("滑轮滚动时的Value的改变值 ")]
        public int SmallChange
        {
            set
            {
                if (value < 0)
                {
                    value = SmallChange;
                }
                SmallChange_ = value;
            }
            get
            {
                return SmallChange_;
            }
        }

        //窗体设计器所废除的变量******************************************************
        //

        /*重写继承类中的变量或函数要用override关键字 并且这个变量或函数应当允许被重写*/
        /*设置Browsable(false) 设计器中将不再显示该变量 由于重写了变量改变时所运行的代码
         * 与该变量任何相关的事件都将消失*/
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(false)]
        public override bool AutoScroll { set; get; }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(false)]
        public override bool AutoSize { set; get; }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(false)]
        public override Size MaximumSize { set; get; }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(false)]
        public override Size MinimumSize { set; get; }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(false)]
        public override Color BackColor { set; get; }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(false)]
        public override Font Font { get { return new Font("微软雅黑", 1); } }//这里好像必须要给个返回值 否则会提示该控件没有实例化

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(false)]
        public override Color ForeColor { set; get; }

        //窗体设计器所添加或重写的事件*******************************
        //

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("操作"), Description("当Value值改变时发生")]
        public event EventHandler ValueChanged = null;


        public SundayRXScrollBar()
        {
            SetStyle(ControlStyles.ResizeRedraw, true);//当控件大小改变时自动重绘           
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);//开启双缓冲  防止重绘发生的闪烁
            this.Size = new System.Drawing.Size(21, 150);//这个大小设置是为了能够在工具箱添加控件的时候让控件重绘一次 显示正确
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (Size.Height - SliderHeight_ < 100)//要求滑块可滑动部分像素长度不得小于100
            {
                Height = SliderHeight_ + 100;
            }
            if (Size.Width > 100)//限制最大宽度为100
            {
                Width = 100;
            }
            if (Size.Width < 2)//限制最小宽度为2
            {
                Width = 2;
            }
            //处理关联变量SliderPointY
            GetSliderPointY();
            //处理关联变量SliderWidth
            GetSliderWidth();
            //处理关联变量SliderPointX
            GetSliderPointX();

            base.OnSizeChanged(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //设置绘制质量为最低 为了保证滑块在滑动时显示流畅

            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;

            //绘制底色 * *****************************************************************************

            SolidBrush Bottom_SolidBrush = new SolidBrush(BottomColor_);
            Rectangle Bottom_Rc = new Rectangle(0, 0, Width, Height);
            e.Graphics.FillRectangle(Bottom_SolidBrush, Bottom_Rc);
            Bottom_SolidBrush.Dispose();

            //绘制滑块******************************************************************************
            SolidBrush Slider_SolidBrush = new SolidBrush(SliderColor_);
            if (MouseDownSliderFlag)//选择画笔颜色时被按下的优先级比悬浮于上的优先级高
            {
                Slider_SolidBrush = new SolidBrush(MouseDownSliderColor_);
            }
            else
            {
                if (MouseOverSliderFlag)
                {
                    Slider_SolidBrush = new SolidBrush(MouseOverSliderColor_);
                }
            }
            //绘制滑块的上半圆部分

            Rectangle Slider_Rc = new Rectangle(SliderPointX - 1, SliderPointY - 1, SliderWidth + 1, SliderWidth + 1);
            e.Graphics.FillPie(Slider_SolidBrush, Slider_Rc, 0, -180);

            //绘制滑块的矩形长条部分
            e.Graphics.FillRectangle(Slider_SolidBrush, SliderPointX, SliderPointY + (float)SliderWidth / 2 - 1, SliderWidth, SliderHeight_ - SliderWidth + 1);

            //绘制滑块的下半圆部分
            Slider_Rc = new Rectangle(SliderPointX - 1, SliderPointY + SliderHeight_ - SliderWidth - 1, SliderWidth + 1, SliderWidth + 1);
            e.Graphics.FillPie(Slider_SolidBrush, Slider_Rc, 0, 179);

            //释放画笔资源
            Slider_SolidBrush.Dispose();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GetMouseDownPoint();//取得相对于控件的鼠标按下位置

                //判断是否单击在了滑块上（具体看滚动条的形状与大小务必自行更改代码适应）

                if (IsMouseOverSlider())
                {
                    DIstance = MouseDownPoint.Y - SliderPointY;//得到鼠标按下是与滑块顶部的距离
                    MouseDownSliderFlag = true;//滑块被按下标志使能
                                               //更改滑块颜色为高亮 重绘控件
                    Invalidate(new Region(new RectangleF(new Point(SliderPointX, SliderPointY), new Size(SliderWidth, SliderHeight_))));
                }

                //判断是否单击在了滑道上
                else if (IsMouseOverBottom())
                {
                    GetMouseDownPoint();
                    int WantSliderPointY = MouseDownPoint.Y - SliderHeight_ / 2;
                    if (WantSliderPointY < 0)
                    {
                        WantSliderPointY = 0;
                    }
                    else if (WantSliderPointY + SliderHeight_ - SliderHeight_ / 2 > Height)
                    {
                        WantSliderPointY = Height - (SliderHeight_ - SliderHeight_ / 2);
                    }
                    LastSliderPointY = SliderPointY;
                    SliderPointY = WantSliderPointY;
                    GetValue();
                    Invalidate();//再刷一下 不然不会变
                }
            }

            //判断是否单击在了向下箭头上

            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (MouseDownSliderFlag)
            {
                MouseDownOverSliderFlag = true;

                GetMousePoint();
                LastSliderPointY = SliderPointY;
                SliderPointY = MousePoint.Y - DIstance;
                if (SliderPointY < 0)
                {
                    SliderPointY = 0;
                }
                else if (SliderPointY > Height - SliderHeight_)
                {
                    SliderPointY = Height - SliderHeight_;
                }
                //处理关联变量Vlaue
                GetValue();

                //关闭鼠标按下并移动标志
                MouseDownOverSliderFlag = false;
            }
            else
            {
                OnMouseOverSliderEvent();
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (MouseDownSliderFlag)
            {
                MouseDownSliderFlag = false;
                MouseDownOverSliderFlag = false;
            }
            Invalidate(new Region(new RectangleF(new Point(SliderPointX, SliderPointY), new Size(SliderWidth, SliderHeight_))));
            base.OnMouseUp(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            //计算Value值
            if (e.Delta > 0)
            {
                if (Value - SmallChange_ < 0)
                {
                    Value = 0;
                }
                else
                {
                    Value = Value - SmallChange_;
                }
            }
            else
            {
                if (Value + SmallChange_ > 100)
                {
                    Value = 100;
                }
                else
                {
                    Value = Value + SmallChange_;
                }
            }
            //处理当鼠标位于滑块上时的事件
            OnMouseOverSliderEvent();
        }
    }
}
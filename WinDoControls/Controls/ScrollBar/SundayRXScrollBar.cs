
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

    //�Զ���ؼ�����ֱ������
    //���ߣ�Sunday��ѩ   ʱ�䣺2016��12��1��
    //�ؼ����ܣ������¼�ͷ ���Ҽ��˵� �����ͻ�����ɫ�ɵ� �����С�ɵ�
    //SliderWidthPercent����������ؼ���ȵı��������ı�ֵΪ0-1֮��ĸ�����
    //SliderHeight(���鳤��)�� SliderHeight����̳���Ҳ������ Ϊ�˱�֤������ʾ����   SliderHeight���ΪWdith���ؼ���ȣ�
    //Wdith(�ؼ����)��Wdith���趨����С���Ϊ2 �����Ϊ100  ̫����Ҳûʲô��
    //Height(�ؼ�����)��ע��  ����MaxValue�̶�ֵΪ100 ���ؼ��ɻ������ֳ���С��100����ʱΪ�˱�֤�㷨�������� Height���ؼ����ȣ�������SliderHeight����������

    /*ʹ��ע��������뿪��SetProcessDPIAware��������ֹUI�Զ��Ŵ�*/

    /*�ر�˵�� ���ڵ��Ե�ʱ�������ʱ���ڼ��ӳ������� ����˵���ɵ���������ִ��ָ��ʹ�������� ���������ٶ�
     * �������½� ���¿ؼ��ػ��ٶȱ��� �����������ʱ�������鷢�� ��ô�������������һ��Ͳ��ᷢ����*/
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


        //��־�Ա���*****************************************************************
        //
        protected bool MouseDownSliderFlag = false;//��갴�»����־
        protected bool MouseOverSliderFlag = false;//����ڻ����Ϸ���־
        protected bool MouseDownOverSliderFlag = false;//��갴�»��鲢�ڻ����Ϸ��ƶ���־

        //С��Χʹ�õ�ȫ�ֱ���*******************************************************
        //
        protected int DIstance;//��갴�µ�λ���뻬�鶥�˵ľ���

        protected int LastSliderPointY;//SliderPointYˢ��֮ǰ��ֵ ÿ����ˢ��SliderPointY֮ǰ��Ҫˢ�¸�ֵ

        //��Χʹ�õ�ȫ�ֱ���********************************************************
        //

        /*����ע����Щ��Χʹ�õ�ȫ�ֱ���֮��Ĺ�����ϵ 
         * �κ�һ���ñ�������ɱ����ĸĶ���Ҫ���¼������������ֵ*/

        protected int SliderPointX;//Width  SliderWidthPercent_
        protected int SliderPointY;   //Value  Heigth  SliderHeight_
        protected int SliderWidth; //Width   SliderWidthPercent_
        protected Point MouseDownPoint;
        protected Point MousePoint;

        //��Χʹ�õķ���*******************************************************
        //һЩ������ֵ�ĸ��·���
        protected void GetSliderPointX()//��Ϊ����SliderPointX��ֵ�ķ���
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

        protected void GetSliderPointY()//��Ϊ����SliderPointY��ֵ�ķ���
        {
            if (MouseDownOverSliderFlag == false)//��ʹ������϶�ʹ��Value�ı�ʱ��Ҫ������Value����SliderPointY
            {
                LastSliderPointY = SliderPointY;
                //�����׼ȷλ�õĸ�����
                float WantSliberPointY = Value_ * ((float)(Height - SliderHeight_) / 100);
                //ȡ�������������������
                int WantSliberPointY_Up = (int)WantSliberPointY;
                int WantSliberPointY_Down = WantSliberPointY_Up + 1;
                //������������������Ϊ��ǰ����λ��Y���꣬�ֱ���з������Value
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

                //�ж���������Valueֵ�ĸ���Value����SliderPointY��ֵ

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

        protected void GetSliderWidth()//��Ϊ����SliderWidth��ֵ�ķ���
        {
            if (SliderWidthPercent_ == 1)//���ڸ������������� ����SliderWidthPercent=1������������⴦��
            {
                SliderWidth = Width;
            }
            else                                          //��ͨ����ĸ���������
            {
                SliderWidth = (int)(Width * SliderWidthPercent_) - 1;
            }
        }

        protected void GetValue()//��Ϊ����Value��ֵ�ķ���
        {
            float SliderPointY_FloatValue = SliderPointY / ((float)(Height - SliderHeight_) / 100);

            int SliderPointY_IntValue = (int)SliderPointY_FloatValue;
            if (SliderPointY_FloatValue > SliderPointY_IntValue)
            {
                SliderPointY_IntValue++;
            }
            Value = SliderPointY_IntValue;
        }

        protected void GetMousePoint()//��Ϊ����MousePoint��ֵ�ķ���
        {
            MousePoint = PointToClient(Cursor.Position);
        }

        protected void GetMouseDownPoint()//��Ϊ����MouseDownPoint��ֵ�ķ���
        {
            MouseDownPoint = PointToClient(Cursor.Position);
        }

        //�ж��������λ�÷���
        protected bool IsMouseOverSlider()//�ж�����Ƿ��ڻ����Ϸ�
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

        protected bool IsMouseOverBottom()//�ж�����Ƿ��ڻ�����
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

        protected bool IsMouseOverControlEdge()//�ж�����Ƿ��ڿؼ���Ե����
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

        //С��Χʹ�õķ���*************************************************************
        //

        protected void OnMouseOverSliderEvent()//����ڻ����Ϸ�����ʱ���¼�
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

        //�������������ӻ���д�ı���******************************************************
        //

        /*������������ڲ�����ʹ�ù��򣺶�����ʹ���ڲ���������������_�ģ�
         * д����ʹ�����������*/
        /*ֱ�Ӹ���Щ�ڲ�������ֵ�൱�ڸ��˿ؼ����Եĳ�ʼֵ*/
        protected Color BottomColor_ = Color.FromArgb(62, 62, 66);
        protected double SliderWidthPercent_ = 0.5;
        protected Color SliderColor_ = Color.FromArgb(104, 104, 104);
        protected int SliderHeight_ = 100;
        protected Color MouseDownSliderColor_ = Color.FromArgb(239, 235, 239);
        protected Color MouseOverSliderColor_ = Color.FromArgb(158, 158, 158);
        protected int Value_ = 0;
        protected int SmallChange_ = 1;

        /*�ñ�����ʾ��������� ����������������ڲ�����  ������������������
         * ��һ����һ�����ᱻ���Ĳ�����Set�еĴ���  */
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("���"), Description("������ɫ")]
        public Color BottomColor
        {
            set/*���ڲ���������ֵ֮ǰ�����������û�б�����*/
            {/*���۸������������ ���Ǹ����ڲ����� �����Ƚ�Ҫ����ֵ�Ĵ��ݸ�value*/

                //���밲ȫ��Ч����� ��value��ֵ���в���

                BottomColor_ = value;//��valueֵ����BottomColor_ 

                //����ɱ������иñ����Ĺ�����������ֵ�ĸ���

                Invalidate();                 //�ػ�ؼ�

                //������ñ�����ص��¼�

            }
            get { return BottomColor_; }//��BottomColor_ֵ����BottomColor
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("���"), Description("����������ڿؼ���ȵİٷֱ� ��Χ0-1")]
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
                //�����������SliderPointX
                GetSliderPointX();
                //�����������SliderWidth
                GetSliderWidth();

                Invalidate();
            }
            get { return SliderWidthPercent_; }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("���"), Description("�������ɫ")]
        public Color SliderColor
        {
            set
            {
                SliderColor_ = value;
                Invalidate();
            }
            get { return SliderColor_; }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("���"), Description("��ָ�밴�»���ʱ�������ɫ")]
        public Color MouseDownSliderColor
        {
            set
            {
                MouseDownSliderColor_ = value;
                Invalidate();
            }
            get { return MouseDownSliderColor_; }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("���"), Description("��ָ�������ڻ�����ʱ�������ɫ")]
        public Color MouseOverSliderColor
        {
            set
            {
                MouseOverSliderColor_ = value;
                Invalidate();
            }
            get { return MouseOverSliderColor_; }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("��Ϊ"), Description("��������ǰֵ  ��Χ0-100")]
        public int Value
        {
            set
            {
                // ����ֵ�Ϸ���Ч��
                if (value > 100)
                {
                    value = 100;
                }
                else if (value < 0)
                {
                    value = 0;
                }
                //��ֵ
                Value_ = value;
                //�����������SliderPointY
                GetSliderPointY();
                //�ػ�ؼ�
                /*�ػ�ؼ�����Ҫע���ػ�ؼ������ʱ�� ֻ�ػ�ı������ ���ı�Ĳ��ػ�*/
                if (SliderPointY - LastSliderPointY > 0)
                {
                    Invalidate(new Region(new RectangleF(new Point(SliderPointX, LastSliderPointY), new Size(SliderWidth, SliderPointY + SliderHeight_ - LastSliderPointY + 2))));
                }
                else if (SliderPointY - LastSliderPointY < 0)
                {
                    //���ڴ������ϻ���ʱ������ϰ�Բ������ʧһ�ε�BUG �����Ҹĳ��˴Ӷ��� ����һ�λ���ױ�λ��ȫ���ػ� ����������� ����Ļ������� ��С�ػ�ʱ��
                    Invalidate(new Region(new RectangleF(new Point(SliderPointX, 0), new Size(SliderWidth, LastSliderPointY + SliderHeight_))));
                }
                //�����¼�ValueChanged
                if (ValueChanged != null)
                    ValueChanged(this, new EventArgs());
                //�����¼�Scroll
                base.OnScroll(new ScrollEventArgs(ScrollEventType.ThumbPosition, Value_));
            }
            get
            {
                return Value_;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("���"), Description("���������� ")]
        public int SliderHeight
        {
            set
            {
                // ����Ϸ���Ч��
                if (value <= Width)
                {
                    value = Width;
                }
                //if (Height - value < 100)
                //{
                //    Height = value + 100;
                //}

                SliderHeight_ = value;
                //�����������SliderPointY
                GetSliderPointY();

                Invalidate();
            }
            get { return SliderHeight_; }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("��Ϊ"), Description("���ֹ���ʱ��Value�ĸı�ֵ ")]
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

        //������������ϳ��ı���******************************************************
        //

        /*��д�̳����еı�������Ҫ��override�ؼ��� ���������������Ӧ��������д*/
        /*����Browsable(false) ������н�������ʾ�ñ��� ������д�˱����ı�ʱ�����еĴ���
         * ��ñ����κ���ص��¼�������ʧ*/
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
        public override Font Font { get { return new Font("΢���ź�", 1); } }//����������Ҫ��������ֵ �������ʾ�ÿؼ�û��ʵ����

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(false)]
        public override Color ForeColor { set; get; }

        //�������������ӻ���д���¼�*******************************
        //

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("����"), Description("��Valueֵ�ı�ʱ����")]
        public event EventHandler ValueChanged = null;


        public SundayRXScrollBar()
        {
            SetStyle(ControlStyles.ResizeRedraw, true);//���ؼ���С�ı�ʱ�Զ��ػ�           
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);//����˫����  ��ֹ�ػ淢������˸
            this.Size = new System.Drawing.Size(21, 150);//�����С������Ϊ���ܹ��ڹ�������ӿؼ���ʱ���ÿؼ��ػ�һ�� ��ʾ��ȷ
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (Size.Height - SliderHeight_ < 100)//Ҫ�󻬿�ɻ����������س��Ȳ���С��100
            {
                Height = SliderHeight_ + 100;
            }
            if (Size.Width > 100)//���������Ϊ100
            {
                Width = 100;
            }
            if (Size.Width < 2)//������С���Ϊ2
            {
                Width = 2;
            }
            //�����������SliderPointY
            GetSliderPointY();
            //�����������SliderWidth
            GetSliderWidth();
            //�����������SliderPointX
            GetSliderPointX();

            base.OnSizeChanged(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //���û�������Ϊ��� Ϊ�˱�֤�����ڻ���ʱ��ʾ����

            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;

            //���Ƶ�ɫ * *****************************************************************************

            SolidBrush Bottom_SolidBrush = new SolidBrush(BottomColor_);
            Rectangle Bottom_Rc = new Rectangle(0, 0, Width, Height);
            e.Graphics.FillRectangle(Bottom_SolidBrush, Bottom_Rc);
            Bottom_SolidBrush.Dispose();

            //���ƻ���******************************************************************************
            SolidBrush Slider_SolidBrush = new SolidBrush(SliderColor_);
            if (MouseDownSliderFlag)//ѡ�񻭱���ɫʱ�����µ����ȼ����������ϵ����ȼ���
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
            //���ƻ�����ϰ�Բ����

            Rectangle Slider_Rc = new Rectangle(SliderPointX - 1, SliderPointY - 1, SliderWidth + 1, SliderWidth + 1);
            e.Graphics.FillPie(Slider_SolidBrush, Slider_Rc, 0, -180);

            //���ƻ���ľ��γ�������
            e.Graphics.FillRectangle(Slider_SolidBrush, SliderPointX, SliderPointY + (float)SliderWidth / 2 - 1, SliderWidth, SliderHeight_ - SliderWidth + 1);

            //���ƻ�����°�Բ����
            Slider_Rc = new Rectangle(SliderPointX - 1, SliderPointY + SliderHeight_ - SliderWidth - 1, SliderWidth + 1, SliderWidth + 1);
            e.Graphics.FillPie(Slider_SolidBrush, Slider_Rc, 0, 179);

            //�ͷŻ�����Դ
            Slider_SolidBrush.Dispose();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GetMouseDownPoint();//ȡ������ڿؼ�����갴��λ��

                //�ж��Ƿ񵥻����˻����ϣ����忴����������״���С������и��Ĵ�����Ӧ��

                if (IsMouseOverSlider())
                {
                    DIstance = MouseDownPoint.Y - SliderPointY;//�õ���갴�����뻬�鶥���ľ���
                    MouseDownSliderFlag = true;//���鱻���±�־ʹ��
                                               //���Ļ�����ɫΪ���� �ػ�ؼ�
                    Invalidate(new Region(new RectangleF(new Point(SliderPointX, SliderPointY), new Size(SliderWidth, SliderHeight_))));
                }

                //�ж��Ƿ񵥻����˻�����
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
                    Invalidate();//��ˢһ�� ��Ȼ�����
                }
            }

            //�ж��Ƿ񵥻��������¼�ͷ��

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
                //�����������Vlaue
                GetValue();

                //�ر���갴�²��ƶ���־
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
            //����Valueֵ
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
            //�������λ�ڻ�����ʱ���¼�
            OnMouseOverSliderEvent();
        }
    }
}
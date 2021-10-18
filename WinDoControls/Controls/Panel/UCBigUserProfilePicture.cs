using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinDo.Utilities.PublicResource;
using WinDoControls.Controls;

namespace WinDoControls.Controls
{
    public partial class UCBigUserProfilePicture : UserControl
    {
        public UCBigUserProfilePicture()
        {

            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            InitializeComponent();

            //ucBtnImg2.BtnClick += new EventHandler(UCBigUserProfilePicture_Click);
            panelCamera.Click += new EventHandler(UCBigUserProfilePicture_Click);
            SizeChanged += new EventHandler(UCBigUserProfilePicture_SizeChanged);
            panelCamera.Cursor = Cursors.Hand;
            panelCamera.Image = WDImages.GetBtnIconImage("I_camera_fill", 20, WDColors.Green6);
            panelCamera.ImageAlign = ContentAlignment.MiddleCenter;
            panelCamera.BackColor = Color.Transparent;
            ResetProfilePicture();
            SetCameraLocation(new Point(this.Bounds.Width - 36, this.Bounds.Height - 36));
            ShowCamera = true;
            //ucBtnImg2.BackColor = Color.White;
        }

        protected virtual void UCBigUserProfilePicture_SizeChanged(object sender, EventArgs e)
        {
            SetCameraLocation(new Point(this.Bounds.Width - 36, this.Bounds.Height - 36));
        }
        //public UCBtnImg IMG { get { return this.ucBtnImg2; } }


        [Description("点击事件"), Category("自定义")]
        public event EventHandler BtnClick;

        void UCBigUserProfilePicture_Click(object sender, EventArgs e)
        {
            if (BtnClick != null)
                BtnClick(sender, e);
        }
        public virtual void ResetProfilePicture()
        {
            SetProfilePicture(WDImages.GetBtnIconImage("I_user_fill", 160, WDColors.GrayBackColor));
        }
        public void SetProfilePicture(Image img)
        {
            this.BackgroundImage = img;
            this.BackgroundImageLayout = ImageLayout.Zoom;
        }
        public void SetProfilePictureF(Image img)
        {
            this.BackgroundImage = img;
            this.BackgroundImageLayout = ImageLayout.Center;
        }
        protected void SetCameraLocation(Point point)
        {
            panelCamera.Location = point;
        }
        [Description("显示照相机"), Category("自定义")]
        public bool ShowCamera
        {
            get { return this.panelCamera.Visible; }
            set { this.panelCamera.Visible = value; panelCamera.BringToFront(); }
        }
        Color _BorderColor = WDColors.GrayRectColor;
        int _BorderSize = 1;
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            ControlPaint.DrawBorder(e.Graphics,
                            this.ClientRectangle,
                            this._BorderColor,
                            this._BorderSize,
                            ButtonBorderStyle.Solid,
                            this._BorderColor,
                            this._BorderSize,
                            ButtonBorderStyle.Solid,
                           this._BorderColor,
                            this._BorderSize,
                            ButtonBorderStyle.Solid,
                            this._BorderColor,
                            this._BorderSize,
                            ButtonBorderStyle.Solid);
        }
    }

    public class UCSmallUserProfilePicture : UCBigUserProfilePicture
    {
        public UCSmallUserProfilePicture()
        {
            this.Size = new Size(80, 80);
            base.SetProfilePicture(WDImages.GetBtnIconImage("I_user_fill", 100, WDColors.GrayBackColor));
            base.SetCameraLocation(new Point(this.Width - 30, this.Height - 30));
        }

        public override void ResetProfilePicture()
        {
            SetProfilePicture(WDImages.GetBtnIconImage("I_user_fill", 100, WDColors.GrayBackColor));
        }

        protected override void UCBigUserProfilePicture_SizeChanged(object sender, EventArgs e)
        {
            base.SetCameraLocation(new Point(this.Width - 30, this.Height - 30));
        }
    }


}

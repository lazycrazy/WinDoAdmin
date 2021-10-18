using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinDoControls.Controls;
using WinDo.Utilities.PublicResource;
using WinDo.Utilities;

namespace WinDoControls.Controls
{
    public partial class WDDateTimePicker : WDCtrlBase
    {
        public event EventHandler CloseUp;
        public Action BeforePopup;

        public WDDateTimePicker()
            : base()
        {
            InitializeComponent();
            ucInnerDateTimePicker1 = new WDInnerDateTimePicker();
            SetBtn();
            ucInnerDateTimePicker1.Width = this.Width - 10;
            ucInnerDateTimePicker1.Location = new Point(5, (this.Height - ucInnerDateTimePicker1.Height) / 2);
            ucInnerDateTimePicker1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            Controls.Add(ucInnerDateTimePicker1);
            this.SizeChanged += new EventHandler(UCDateTimePicker_SizeChanged);
            //ucInnerDateTimePicker1.dtp.Enabled = false;
            ucBtnImg0Words1.BtnClick += new EventHandler(ucBtnImg0Words1_BtnClick);
        }
        //private void tsdd_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        //{
        //    //this.Invalidate();
        //    this.DisplayStatus = false;
        //    if (CloseUp != null)
        //        CloseUp(this, EventArgs.Empty);
        //}

        public static Image ImageCalender = WDImages.GetBtnIconImage("I_calender", 20, color: ColorTranslator.FromHtml("#676767"));

        protected virtual void SetBtn()
        {
            ucBtnImg0Words1.Image = ImageCalender;
        }
        public void SetCoverRightSideLine()
        {
            ucBtnImg0Words1.Left = this.Width - ucBtnImg0Words1.Width;
        }


        protected virtual void ucBtnImg0Words1_BtnClick(object sender, EventArgs e)
        {
            if (BeforePopup != null)
                BeforePopup();
            if (_frmAnchor == null || _frmAnchor.IsDisposed || _frmAnchor.Visible == false)
            {
                if (_frmAnchor != null && !_frmAnchor.IsDisposed)
                {
                    _frmAnchor.Close();
                    _frmAnchor = null;
                }
                var datePicker = new WinDoControls.Controls.DatePickerExt();
                datePicker.BottomBarBtnBackColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
                datePicker.BottomBarBtnBackDisabledColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
                datePicker.BottomBarBtnBackEnterColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
                datePicker.DateBackSelectedColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
                datePicker.DateNormalForeColor = WDColors.BlackColor;
                datePicker.DatePastForeColor = WDColors.LinkColor;
                datePicker.TopBarBackColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
                datePicker.MinMaxTip = false;
                //this.tsdd = new ToolStripDropDown() { Padding = Padding.Empty };
                //this.tsch = new ToolStripControlHost(this.datePicker) { Margin = Padding.Empty, Padding = Padding.Empty };
                //tsdd.Items.Add(this.tsch);

                //this.tsdd.Closed += new ToolStripDropDownClosedEventHandler(this.tsdd_Closed);
                datePicker.BottomBarConfirmClick += (ss, ee) =>
                {
                    if (datePicker.MinValue.Date <= datePicker.Value.Date && datePicker.Value.Date <= datePicker.MaxValue.Date)
                    {
                        this.DTP.Value = datePicker.Value.Date;
                        //if (this.DTP.Value != null)
                        //{
                        //    DateTime dtTemp = this.DTP.Value.Value;

                        //    this.DTP.Value = DateTime.Parse(datePicker.Value.Date.ToString("yyyy-MM-dd ") + dtTemp.ToString("HH:mm:ss"));
                        //}
                        //else
                        //{
                        //    this.DTP.Value = DateTime.Parse(datePicker.Value.Date.ToString("yyyy-MM-dd ") + DateTime.Now.ToString("HH:mm:ss"));
                        //}
                    }

                    OnCloseUp(this, EventArgs.Empty);
                    //this.tsdd.Close();
                    //this.Invalidate();
                    _frmAnchor.Close();
                    _frmAnchor = null;
                };
                DTP.MinDate = DTP.MinDate.Date;
                DTP.MaxDate = DTP.MaxDate.Date;
                datePicker.MinValue = DTP.MinDate.Date;
                datePicker.MaxValue = DTP.MaxDate.Date;
                datePicker.Show();
                _frmAnchor = new WinDoControls.Forms.FrmAnchor(this, datePicker);
                _frmAnchor.Owner = this.FindForm();
                _frmAnchor.Show();
                datePicker.Value = this.DTP.Value.HasValue ? this.DTP.Value.Value.Date : DateTime.Now.Date;
            }
            else
            {
                _frmAnchor.Close();
                _frmAnchor = null;

            }
        }
        public void OnCloseUp(object sender, EventArgs e)
        {
            if (CloseUp != null)
                CloseUp(this, e);
        }
        protected WinDoControls.Forms.FrmAnchor _frmAnchor;

        WDInnerDateTimePicker ucInnerDateTimePicker1;
        //private ToolStripDropDown tsdd = null;
        //private bool DisplayStatus = false;
        //private ToolStripControlHost tsch = null;
        //void ucBtnImg0Words1_BtnClick(object sender, EventArgs e)
        //{
        //    DTP.MinDate = DTP.MinDate.Date;
        //    DTP.MaxDate = DTP.MaxDate.Date; 
        //    datePicker.MinValue = DTP.MinDate.Date;
        //    datePicker.MaxValue = DTP.MaxDate.Date;
        //    datePicker.Value = this.DTP.Value.HasValue ? this.DTP.Value.Value.Date : ServerDateTimeHelper.CommonBLL.Now.AsDateTime().Date;
        //    if (!this.DisplayStatus)
        //    {
        //        tsdd.Show(this.PointToScreen(new Point(0, this.Height + 2)));
        //    }
        //    //ucInnerDateTimePicker1.dtp.Focus();
        //    //SendKeys.Send("{F4}");
        //}
        public void ShowWeekInfo()
        {
            ucBtnImg0Words1.Visible = false;
        }
        public void SetMiniIcon()
        {
            ucBtnImg0Words1.Width = 34;
        }
        public NullableDateTimePicker DTP
        {
            get
            {
                return ucInnerDateTimePicker1.dtp;
            }
        }
        void UCDateTimePicker_SizeChanged(object sender, EventArgs e)
        {
            this.ucInnerDateTimePicker1.Location = new Point(this.ucInnerDateTimePicker1.Location.X, (this.Height - ucInnerDateTimePicker1.Height) / 2);
        }

        //private bool _allowEdit = false;
        //public bool AllowEdit
        //{
        //    get { return _allowEdit; }
        //    set
        //    {
        //        _allowEdit = value;
        //        ucInnerDateTimePicker1.dtp.Enabled = _allowEdit;
        //    }
        //}

        private Color focusBorderColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;

        private Color defaultRectColor = WDColors.GrayRectColor;


        private bool isErrorColor = false;

        private bool _readOnly = false;
        public bool ReadOnly
        {
            get { return _readOnly; }
            set
            {
                _readOnly = value;
                DTP.ReadOnly = value;
            }
        }

        [Description("是否为错误状态"), Category("自定义")]
        public bool IsErrorColor
        {
            get { return isErrorColor; }
            set
            {
                isErrorColor = value;
                this.RectColor = value ? WDColors.ErrorRedColor : this.Focused ? focusBorderColor : defaultRectColor;
                this.ucBtnImg0Words1.RectColor = value ? WDColors.ErrorRedColor : this.Focused ? focusBorderColor : defaultRectColor;
            }
        }
    }

    public partial class UCTimePicker : WDDateTimePicker
    {
        public static Image ImageTime = WDImages.GetBtnIconImage("I_time", 20, color: ColorTranslator.FromHtml("#676767"));
        protected override void SetBtn()
        {
            ucBtnImg0Words1.Image = ImageTime;
            DTP.Format = DateTimePickerFormat.Custom;
            DTP.CustomFormat = "HH:mm";
        }

        protected override void ucBtnImg0Words1_BtnClick(object sender, EventArgs e)
        {
            if (_frmAnchor == null || _frmAnchor.IsDisposed || _frmAnchor.Visible == false)
            {
                if (_frmAnchor != null && !_frmAnchor.IsDisposed)
                {
                    _frmAnchor.Close();
                    _frmAnchor = null;
                }
                var frm = new frmSelectTime(0, 0);
                frm.TopLevel = false;
                _frmAnchor = new WinDoControls.Forms.FrmAnchor(this, frm);
                frm.FormClosed += (ss, ee) =>
                {
                    //设置时间
                    if (frm.Time.Length > 0)
                    {
                        var time = frm.Time.Split(':');
                        DTP.Value = DateTime.Now.Date.AddHours(time[0].AsDouble()).AddMinutes(time[1].AsDouble());
                    }
                    else
                    {
                        DTP.Value = null;
                    }
                    OnCloseUp(this, EventArgs.Empty);
                    if (_frmAnchor == null || _frmAnchor.IsDisposed) return;
                    _frmAnchor.Close(); _frmAnchor = null;
                };
                //frmAnchor.VisibleChanged += FrmAnchor_VisibleChanged;
                _frmAnchor.Owner = this.FindForm();
                frm.Show();
                _frmAnchor.Show();
            }
            else
            {
                _frmAnchor.Close();
                _frmAnchor = null;

            }
        }
    }

}

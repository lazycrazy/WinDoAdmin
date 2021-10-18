using MIP.UI.Utilities.CommonUI;
using MIP.UI.Utilities.Special;
using MIP.Utilities;
using MIP.Utilities.PublicResource;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YKD_Controls;
using YKD_Controls.Forms;

namespace MIP.UI.Utilities.DialogForm
{
    public partial class frmShowDayData : FrmWith2WordsOk
    {



        /// <summary>
        /// 计算中断天数
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable SetInterruptDays(DataTable dt)
        {
            var CloseMachineTime = MIP.Utilities.PublicRes.GetConfig("C_SCHEDULE_010", "02:00");
            var now = ServerDateTimeHelper.GetServerDateTime().AsDateTime();
            var serverDate = now.Date.ToString("yyyy-MM-dd");
            DataTable myDt = dt;
            foreach (DataRow dr in myDt.Rows)
            {
                string LastTreatDate = dr["LastTreatDate"].ToString();
                if (LastTreatDate.Contains("1900") || string.IsNullOrEmpty(LastTreatDate))
                    continue;
                string TreatLocation = dr["TreatLocation"].ToString();
                if (string.IsNullOrEmpty(TreatLocation))
                    continue;

                List<string> allDate = GetTimeList(CloseMachineTime, now, LastTreatDate, serverDate);
                int IDays = 0;
                List<MIP.Model.Holiday> holidays = MIP.BLL.TechnicianListBll.Instance.GetHolidayModelList(LastTreatDate, serverDate);
                string location = MIP.BLL.TechnicianListBll.Instance.GetMachineLocation(TreatLocation);
                foreach (string oneDate in allDate)
                {
                    Tuple<bool, string> returnTus = MIP.BLL.TechnicianListBll.Instance.IsHoliday(LastTreatDate, serverDate, TreatLocation, oneDate, holidays, location);
                    if (!returnTus.Item1)
                    {
                        IDays++;
                    }
                }
                if (IDays != 0)
                {
                    dr["InterruptDays"] = IDays;
                }
            }
            return myDt;
        }


        public static List<string> GetTimeList(string CloseMachineTime, DateTime now, string rq1, string rq2)
        {
            List<string> timeList = new List<string>();
            //首先保证 rq1<=rq2
            //CloseMachineTime关机时间
            //CloseMachineTime = "2:00";
            DateTime dt = Convert.ToDateTime(rq1);   //最后治疗日期带时间
            DateTime dt1 = Convert.ToDateTime(dt.ToString("t"));    //最后治疗时间
            DateTime dt2 = Convert.ToDateTime(CloseMachineTime);     //关机时间

            DateTime dtrq2 = now;  //当前日期带时间
            DateTime dtrq21 = Convert.ToDateTime(dtrq2.ToString("t"));  //当前时间
            DateTime time1 = new DateTime();

            DateTime dtYMD = Convert.ToDateTime(Convert.ToDateTime(dtrq2).ToString("d")); //当前日期  不带时间
            DateTime dtLST = Convert.ToDateTime(Convert.ToDateTime(rq1).ToString("d"));  //最后治疗日期不带时间
            DateTime time2 = Convert.ToDateTime(dtrq2.ToString("d") + " " + CloseMachineTime);
            //当前时间减最后治疗日期的关机时间大于2
            TimeSpan ts = dtrq2 - Convert.ToDateTime(Convert.ToDateTime(rq1).ToString("d") + " " + CloseMachineTime); //两个日期相减
            int days = ts.Days;
            if (DateTime.Compare(dtYMD, dtLST) > 0)   //当前日期必须大于最近治疗日期
            {
                //最后治疗时间小于关机时间  //&& 当前时间大于关机时间  && 当前日期大于最后治疗日期
                //最后治疗时间小于关机时间   &&当前时间大于关机时间
                if (DateTime.Compare(dt1, dt2) < 0 && DateTime.Compare(dtrq21, dt2) > 0)
                    time1 = Convert.ToDateTime(Convert.ToDateTime(rq1).ToString("d") + " " + CloseMachineTime);
                else if (days == 1 && DateTime.Compare(dtrq21, dt2) < 0)
                    time1 = Convert.ToDateTime(Convert.ToDateTime(rq1).ToString("d") + " " + CloseMachineTime).AddDays(2);
                else if (days > 1)
                    time1 = Convert.ToDateTime(Convert.ToDateTime(rq1).ToString("d") + " " + CloseMachineTime).AddDays(1);
                else
                    time1 = time2;

                while (time1 < time2)
                {
                    timeList.Add(time1.ToString("yyyy-MM-dd"));
                    time1 = time1.AddDays(1);
                }
            }
            return timeList;
        }

        public frmShowDayData()
        {
            InitializeComponent();
            Load += FrmShowDayData_Load;
            btnOk.BtnClick += BtnOk_BtnClick;

            btnLeft.BtnClick += BtnLeft_BtnClick;
            btnRight.BtnClick += BtnRight_BtnClick;
            this.dtpScheduleDate.DTP.Nullable = false;
            FormHelper.SetDTPCtrlFormat(dtpScheduleDate.DTP);

            dtpScheduleDate.ReadOnly = true;
            dtpScheduleDate.DTP.ValueChanged += DTP_ValueChanged;
            flowLayoutPanel1.Visible = false;
            this.dataGridView1.CellFormatting += DataGridView1_CellFormatting;
            if (this.GetType().Name == "frmShowDayData_SY")
            {
                this.dtpScheduleDate.DTP.MaxDate = ServerDateTimeHelper.GetServerDateTime().AsDateTime().AddDays(-1);
                btnRight.Enabled = false;
            }
        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var dgv = sender as DataGridView;
            var col = dgv.Columns[e.ColumnIndex];
            //var cols = new[] { "ID", "姓名" };
            //if (!cols.Contains(col.HeaderText))
            //    return;
            if (dgv.Rows[e.RowIndex].DataBoundItem == null) return;
            dynamic dr = dgv.Rows[e.RowIndex].DataBoundItem;
            if (dr.HasCRB > 0)
            {
                e.CellStyle.ForeColor = YkdBasisColors.CRBColor;
                e.CellStyle.Font = YkdTextFonts.TextFontBold;
            }
        }

        public static void DrawChargeStatus(Graphics g, Rectangle rec, int chargeStatus, bool selected)
        {
            var cycleColor = ChargeInfo.StatusCycleColor[chargeStatus];
            //var ofx = (rec.Width - g.MeasureString("缴费状态", YkdTextFonts.TextFont).Width) / 2;
            //rec.Offset((int)ofx, 0);
            g.SetGDIHigh();
            using (var brush = new SolidBrush(cycleColor))
                g.FillEllipse(brush, rec.X + 5, rec.Y + ((rec.Height - 12) / 2), 12, 12);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            var txtRect = rec;
            txtRect.Offset(23, 0);
            txtRect.Width -= 23;
            var foreColor = ChargeInfo.StatusTextColor[chargeStatus];
            if (selected)
                foreColor = Color.White;
            using (var brush = new SolidBrush(foreColor))
                g.DrawString(ChargeStatusEnum.GetPropertyNameByValue(chargeStatus), YkdTextFonts.TextFont, brush, txtRect, UI.Utilities.Controls.UCListV.StringFormatLeft);
        }
        private void DTP_ValueChanged(object sender, EventArgs e)
        {
            var date = dtpScheduleDate.DTP.Value;
            var now = ServerDateTimeHelper.GetServerDateTime().AsDateTime();
            if (this.GetType().Name == "frmShowDayData_SY")
                if (date.Value.Date >= now.Date.AddDays(-1))
                {
                    btnRight.Enabled = false;
                    //return;
                }
                else
                {
                    btnRight.Enabled = true;
                }

            Search();
        }

        internal void SetInfo(int v1, int v2, int v3, int v4,int v5)
        {
            label3.Text = v1.ToString("0");
            label5.Text = v2.ToString("0");
            label7.Text = v3.ToString("0");
            label8.Text = v4.ToString("0");
            label10.Text = v5.ToString("0");

        }
        FrmAnchorTips frmAnchorTips;

        private void CloseTips()
        {
            if (frmAnchorTips != null)
            {
                frmAnchorTips.Close();
                frmAnchorTips = null;
            }
        }
        protected void SetSpeicalInfo(DataGridViewColumn col)
        {
            col.FixColumnWidth(22);
            col.SetOperationButtons(
            (dgv1, e) =>
            {
                dynamic dr = dgv1.Rows[e.RowIndex].DataBoundItem;
                if (dr.SpecialInfoCount <= 0)
                {
                    return;
                }
                var cell = dgv1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                var rect_width = YkdImages.DeltaImg.Width;
                var rect_height = YkdImages.DeltaImg.Height;
                var x = (cell.Size.Width - rect_width) / 2;
                var y = (cell.Size.Height - rect_height) / 2;
                var rect = new Rectangle(x, y, rect_width, rect_height);
                if (rect.Contains(e.Location))
                {
                    if (dr == null) return;
                    var patientid = dr.PatientID;
                    var patientName = dr.PatientName;
                    CloseTips();
                    frmSpecialInfoItem.OpenPatientSpicialForm(patientid, patientName, null);
                    Search();
                }

            },
            (dgv1, e) =>
            {
                dynamic dr = dgv1.Rows[e.RowIndex].DataBoundItem;
                if (dr.SpecialInfoCount <= 0 || dr.SpecialInfoCount==null)
                {
                    return;
                }
                var img = YkdImages.DeltaImg;
                var p = new Point(e.CellBounds.X + (e.CellBounds.Width - img.Width) / 2, e.CellBounds.Y + (e.CellBounds.Height - img.Height) / 2);
                e.Graphics.DrawImage(img, p);
            },
            (dgv1, e) =>
            {
                dynamic dr = dgv1.Rows[e.RowIndex].DataBoundItem;
                if (dr.SpecialInfoCount <= 0)
                {
                    dgv1.Cursor = Cursors.Default;
                    return;
                }
                var cell = dgv1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                var rect_width = YkdImages.DeltaImg.Width;
                var rect_height = YkdImages.DeltaImg.Height;
                var x = (cell.Size.Width - rect_width) / 2;
                var y = (cell.Size.Height - rect_height) / 2;
                var rect = new Rectangle(x, y, rect_width, rect_height);
                if (!rect.Contains(e.Location))
                {
                    dgv1.Cursor = Cursors.Default;
                    CloseTips();
                    return;
                }
                dgv1.Cursor = Cursors.Hand;
                var cellRect = dgv1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                var dgvPoint1 = dgv1.Parent.PointToScreen(dgv1.Location);
                cellRect.Offset(dgvPoint1);
                if (frmAnchorTips != null && frmAnchorTips.RectControl != cellRect)
                {
                    CloseTips();
                }
                string patientid = dr.PatientID;
                if (frmAnchorTips == null)
                    frmAnchorTips = FrmAnchorTips_TS.ShowWithIconTips(cellRect, patientid, AnchorTipsLocation.BOTTOM, YkdBasisColors.TaskListTip, autoCloseTime: 6000);

            });
        }


        protected void SetZDTSInfo(DataGridViewColumn col)
        {
            col.SetColumnCustomDraw((dgv1, e) =>
            {
                e.PaintBackground(e.CellBounds, true);
                dynamic dr = dgv1.Rows[e.RowIndex].DataBoundItem;
                string qty = dr.ZDTS == 0 ? "" : dr.ZDTS.ToString();
                if (string.IsNullOrEmpty(qty))
                    return;
                var g = e.Graphics;
                var rect = e.CellBounds;
                g.SetGDIHigh();
                using (var brush = new SolidBrush(Color.Red))
                {
                    if (qty.Length < 3)
                    {
                        var tRect = rect;
                        tRect.Width = 25;
                        tRect.Height = 25;
                        tRect.Offset((rect.Width - tRect.Width) / 2, (rect.Height - tRect.Height) / 2);
                        g.FillEllipse(brush, tRect);
                        g.DrawString(qty, YkdTextFonts.TextFont12, Brushes.White, tRect, StringFormatCenter);
                    }
                    else
                    {
                        var txtsize = TextRenderer.MeasureText(qty, YkdTextFonts.TextFont);
                        var width = txtsize.Width + 4;
                        var tRect = rect;
                        tRect.Width = width;
                        tRect.Height = txtsize.Height + 2;
                        tRect.Offset((rect.Width - tRect.Width) / 2, (rect.Height - tRect.Height) / 2);
                        YKD_Controls.ControlHelper.FillRoundRectangle(g, brush, tRect, 9);
                        g.DrawString(qty, YkdTextFonts.TextFont12, Brushes.White, tRect, StringFormatCenter);
                    }
                }

                //var radius = 25;
                //using (var brush = new SolidBrush(cycleColor.Value))
                //    g.FillEllipse(brush, rect.X + ((rect.Width - radius) / 2), rect.Y + ((rect.Height - radius) / 2), radius, radius);
                //g.DrawString(qty.ToString(), Utilities.PublicResource.YkdTextFonts.TextFont, Brushes.White, e.CellBounds, UI.Utilities.Controls.UCListV.StringFormatCenter);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

            });
        }


        public void SetKeZhiXingYuEr(DataGridViewColumn col)
        {
            col.SetOperationButtons((dgv, e) =>
            {
            }, (dgv, e) =>
            {
                e.PaintBackground(e.CellBounds, true);
                dynamic dr = dgv.Rows[e.RowIndex].DataBoundItem;
                string CanExeBalance = dr.CanExeBalance == 0 ? "" : dr.CanExeBalance.ToString();
                //var CanExeBalance = (ls == null || ls.Rows.Count == 0) ? "" : ls.Rows[e.RowIndex]["CanExeBalance"].ToString();
                var g = e.Graphics;
                var cycleColor = GetExecQtyCycleColor(CanExeBalance.ToInt());
                if (cycleColor == null)
                {
                    e.PaintContent(e.CellBounds);
                    return;
                }
                var rect = e.CellBounds;
                g.SetGDIHigh();
                var txt = string.IsNullOrEmpty(CanExeBalance.ToString()) ? "0" : CanExeBalance.ToString();
                using (var brush = new SolidBrush(cycleColor.Value))
                {
                    if (txt.Length < 3)
                    {
                        var tRect = rect;
                        tRect.Width = 25;
                        tRect.Height = 25;
                        tRect.Offset((rect.Width - tRect.Width) / 2, (rect.Height - tRect.Height) / 2);
                        g.FillEllipse(brush, tRect);
                        g.DrawString(txt, YkdTextFonts.TextFont12, Brushes.White, tRect, StringFormatCenter);
                    }
                    else
                    {
                        var txtsize = TextRenderer.MeasureText(txt, YkdTextFonts.TextFont);
                        var width = txtsize.Width + 4;
                        var tRect = rect;
                        tRect.Width = width;
                        tRect.Height = txtsize.Height + 2;
                        tRect.Offset((rect.Width - tRect.Width) / 2, (rect.Height - tRect.Height) / 2);
                        YKD_Controls.ControlHelper.FillRoundRectangle(g, brush, tRect, 9);
                        g.DrawString(txt, YkdTextFonts.TextFont12, Brushes.White, tRect, StringFormatCenter);
                    }
                }
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            }, (dgv, e) =>
            {
                if (e.RowIndex < 0) return;
                var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                var ls = (dgv.DataSource as DataTable);
                var CanExeBalance = (ls == null || ls.Rows.Count == 0) ? "" : ls.Rows[e.RowIndex]["CanExeBalance"].ToString();

                var PCI_Qty_Pay = (ls == null || ls.Rows.Count == 0) ? "" : ls.Rows[e.RowIndex]["PCI_Qty_Pay"].ToString();
                var PCI_Qty_Refund = (ls == null || ls.Rows.Count == 0) ? "" : ls.Rows[e.RowIndex]["PCI_Qty_Refund"].ToString();
                var PCI_Qty_Exe = (ls == null || ls.Rows.Count == 0) ? "" : ls.Rows[e.RowIndex]["PCI_Qty_Exe"].ToString();

                string tipStr = " 缴费：" + PCI_Qty_Pay + "\r\n 退费：" + PCI_Qty_Refund + "\r\n 执行：" + PCI_Qty_Exe;

                var txt = CanExeBalance.ToString();
                var x1 = (cell.Size.Width) / 2 - 9;
                var y1 = (cell.Size.Height) / 2 - 5;
                var rect1 = new Rectangle(x1, y1, 20, 9);
                if (rect1.Contains(e.Location))
                {
                    dgv.Cursor = Cursors.Hand;
                    ShowBtnTips(e, dgv, rect1, tipStr);
                }
                else
                {
                    dgv.Cursor = Cursors.Default;
                    if (frmAnchorTips != null && frmAnchorTips.RectControl != rect1)
                    {
                        frmAnchorTips.Close();
                        frmAnchorTips = null;
                    }
                }

            });
        }
        private void ShowBtnTips(DataGridViewCellMouseEventArgs e, DataGridView dgv, Rectangle rect1, string tips)
        {
            var cellRect = dgv.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
            var dgvPoint1 = dgv.Parent.PointToScreen(dgv.Location);
            cellRect.Offset(dgvPoint1);
            rect1.Offset(cellRect.Location);
            if (frmAnchorTips != null && frmAnchorTips.RectControl != rect1)
            {
                frmAnchorTips.Close();
                frmAnchorTips = null;
            }
            if (frmAnchorTips == null)
                frmAnchorTips = FrmAnchorTips.ShowTips(rect1, tips, AnchorTipsLocation.BOTTOM, YkdBasisColors.TaskListTip, autoCloseTime: 5000, alignment: StringAlignment.Center);
        }
        Color? GetExecQtyCycleColor(int qty)
        {
            var M = PublicRes.GetConfig("C_CHARGE_SP_EXE_M", "0").AsInt(0);
            if (qty <= M)
                return Color.Red;
            var N = PublicRes.GetConfig("C_CHARGE_SP_EXE_N", "1").AsInt(1);
            if (M < qty && qty <= N)
                return ColorTranslator.FromHtml("#ff8e00");
            return null;
        }

        private void BtnLeft_BtnClick(object sender, EventArgs e)
        {
            var date = dtpScheduleDate.DTP.Value;
            if (!date.HasValue) return;
            var now = ServerDateTimeHelper.GetServerDateTime().AsDateTime();
            if (this.GetType().Name == "frmShowDayData_SY")
                if (date.Value.Date.AddDays(-1) < now.Date)
                {
                    btnRight.Enabled = true;
                }
            dtpScheduleDate.DTP.Value = date.Value.Date.AddDays(-1);
        }

        private void BtnRight_BtnClick(object sender, EventArgs e)
        {
            var date = dtpScheduleDate.DTP.Value;
            var now = ServerDateTimeHelper.GetServerDateTime().AsDateTime();
            if (this.GetType().Name == "frmShowDayData_SY")
                if (date.Value.Date.AddDays(1) == now.Date.AddDays(-1))
                {
                    btnRight.Enabled = false;
                    dtpScheduleDate.DTP.Value = date.Value.Date.AddDays(1);
                    return;
                }
            if (!date.HasValue) return;
            try
            {
                dtpScheduleDate.DTP.Value = date.Value.Date.AddDays(1);
            }
            catch (Exception ex)
            {
                btnRight.Enabled = false;
            }
            
        }

        private void BtnOk_BtnClick(object sender, EventArgs e)
        {
            this.Close();
        }

        public Model.Machine CurMachine;
        private void FrmShowDayData_Load(object sender, EventArgs e)
        {
            string subClassName = this.GetType().Name;
            SetControls(CurMachine.Machine_Name);
            var now = ServerDateTimeHelper.GetServerDateTime().AsDateTime();
            dtpScheduleDate.DTP.Value = now.Date.AddDays(AddDays);
            this.lblDayDesc.Focus();
            if (this.GetType().Name == "frmShowDayData_SY")
            {
                btnRight.Enabled = false;
            }
            VerticalScrollIndex = -1;
        }
        public int VerticalScrollIndex = -1;
        public void Search()
        {
            if (!dtpScheduleDate.DTP.Value.HasValue)
                return;
            var date = dtpScheduleDate.DTP.Value.Value.Date;

            var time = PublicRes.GetConfig("C_SCHEDULE_010", "00:00");
            var StartTime = DateTime.Parse(date.Date.ToString("yyyy-MM-dd") + " " + time + ":00");
            var EndTime = StartTime.AddDays(1);
            Search(StartTime, EndTime);
        }
        public virtual void Search(DateTime StartTime, DateTime EndTime)
        {

        }

        public virtual void SetControls(string machineName)
        {

        }


        private int addDays = 0;
        public int AddDays
        {
            get { return addDays; }
            set { addDays = value; }
        }

        public DataGridView DGV
        {
            get { return this.dataGridView1; }
        }

        public string Title
        {
            get { return this.lblTitle.Text; }
            set { this.lblTitle.Text = value; }
        }


        public string DayDesc
        {
            get { return this.lblDayDesc.Text; }
            set { this.lblDayDesc.Text = value; }
        }
        protected StringFormat StringFormatCenter = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                VerticalScrollIndex = e.NewValue;
            }
        }
    }


    public class frmShowDayData_SY : frmShowDayData
    {
        public override void SetControls(string machineName)
        {
            this.DayDesc = "预约日期：";
            this.Title = "爽约人次 - " + machineName;
            this.AddDays = -1;

            var dgv = DGV;
            DataGridViewHelper.SetDefaultStyle(dgv, true);

            var col = dgv.AddColumn("序号", "IDX");
            col.CellFormatting((dgv1, e) =>
            {
                e.Value = e.RowIndex + 1;
            });
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            col = dgv.AddColumn("预约日期", "App_DtTm").FixColumnWidth(140);
            col.CellFormatting((dgv1, e) =>
            {
                dynamic dr = dgv1.Rows[e.RowIndex].DataBoundItem;
                var date = (dr.App_DtTm as DateTime?);
                if (date != null)
                {
                    e.Value = date.Value.ToString("yyyy-MM-dd HH:mm");
                }
            });
            col = dgv.AddColumn("ID", "PatientID");
            col.FixColumnWidth(100);
            col.ShowTipsOnOverLength();
            //欠费标识
            if (ChargeInfo.ChargeTypeConfig > 0)
            {
                col = dgv.AddColumn("", "ChargeStatus");
                col.FixColumnWidth(30);
            }
            //特殊情况标识
            col = dgv.AddColumn("", "");
            SetSpeicalInfo(col);

            col = dgv.AddColumn("姓名", "PatientName");
            col.ColumnAligment(DataGridViewContentAlignment.MiddleLeft);
            col.FixColumnWidth(100);
            col.ShowTipsOnOverLength();
            col = dgv.AddColumn("联系电话", "Telephone");
            col.ShowTipsOnOverLength();
            col = dgv.AddColumn("治疗技术", "FunctionCode");
            col = dgv.AddColumn("中断天数", "ZDTS");
            SetZDTSInfo(col);
            col = dgv.AddColumn("最近治疗", "LastTreatDate").FixColumnWidth(140);
            col.CellFormatting((dgv1, e) =>
            {
                dynamic dr = dgv1.Rows[e.RowIndex].DataBoundItem;
                var date = (dr.LastTreatDate as DateTime?);
                if (date != null)
                {
                    e.Value = date.Value.ToString("yyyy-MM-dd HH:mm");
                }
            });
            col = dgv.AddColumn("主管医生", "Attending");


            dgv.SetCellValue(new List<string>() { "IDX", "App_DtTm", "SpecialInfoCount", "ChargeStatus", "LastTreatDate", "ZDTS" });
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                #region 排序

                if (column.DataPropertyName == "App_DtTm" ||
                    column.DataPropertyName == "ZDTS" ||
                    column.DataPropertyName == "LastTreatDate")
                {
                    column.SortMode = DataGridViewColumnSortMode.Programmatic;
                    column.CustomSort((dgv2, e, asc) =>
                    {
                        var ls = dgv.DataSource as List<dynamic>;
                        if (ls == null) return;

                        //DataView dv = (dgv.DataSource as DataTable).DefaultView;
                        string fieldName = dgv.Columns[e.ColumnIndex].DataPropertyName;
                        customOrderStr = customOrderStr == "" || customOrderStr.Contains("Asc") ? fieldName + " Desc" : fieldName + " Asc";
                        dgv.DataSource = asc ? ls.OrderBy(l => ((IDictionary<string, object>)l)[fieldName]).ToList() : ls.OrderByDescending(l => ((IDictionary<string, object>)l)[fieldName]).ToList();
                    });
                }


                #endregion
            }
        }
        static string customOrderStr = "";

        public override void Search(DateTime StartTime, DateTime EndTime)
        {
            if (this.CurMachine == null)
                return;
            var rs = BLL.TechnicianListBll.Instance.GetSYRC(new { StartTime, EndTime, this.CurMachine.Machine_ID }, customOrderStr);
            DGV.DataSource = rs;
            var dt = new DataTable();
            dt.Columns.Add("LastTreatDate", typeof(string));
            dt.Columns.Add("TreatLocation", typeof(string));
            dt.Columns.Add("InterruptDays", typeof(int));
            foreach (dynamic dr in rs)
            {
                var nr = dt.NewRow();
                nr["LastTreatDate"] = dr.LastTreatDate;
                nr["TreatLocation"] = dr.TreatLocation;
                nr["InterruptDays"] = 0;
                dt.Rows.Add(nr);
            }
            SetInterruptDays(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rs[i].ZDTS = dt.Rows[i]["InterruptDays"];
            }

            if (VerticalScrollIndex > -1 && DGV.Rows.Count > VerticalScrollIndex)
                DGV.FirstDisplayedScrollingRowIndex = VerticalScrollIndex;

        }
    }


    public class frmShowDayData_ZL : frmShowDayData
    {
        public override void SetControls(string machineName)
        {

            this.DayDesc = "治疗日期：";
            this.Title = "治疗人次-" + machineName;
            this.AddDays = -1;

            var dgv = DGV;
            DataGridViewHelper.SetDefaultStyle(dgv, true);
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            var col = dgv.AddColumn("序号", "IDX");
            col.CellFormatting((dgv1, e) =>
            {
                e.Value = e.RowIndex + 1;
            });
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            col = dgv.AddColumn("病区", "WardName");
            col = dgv.AddColumn("ID", "PatientID");
            col.FixColumnWidth(100);
            col.ShowTipsOnOverLength();
            //欠费标识
            if (ChargeInfo.ChargeTypeConfig > 0)
            {
                col = dgv.AddColumn("", "ChargeStatus");
                col.FixColumnWidth(30);
            }
            //特殊情况标识
            col = dgv.AddColumn("", "");
            SetSpeicalInfo(col);

            col = dgv.AddColumn("姓名", "PatientName");
            col.ColumnAligment(DataGridViewContentAlignment.MiddleLeft);
            col.FixColumnWidth(100);
            col.ShowTipsOnOverLength();
            col = dgv.AddColumn("SITE", "Site");
            col.ShowTipsOnOverLength();
            col = dgv.AddColumn("技术", "Function_Name");
            col = dgv.AddColumn("野数", "FLDQty");
            col = dgv.AddColumn("治疗日期", "TreatDate").FixColumnWidth(140);
            col.CellFormatting((dgv1, e) =>
            {
                dynamic dr = dgv1.Rows[e.RowIndex].DataBoundItem;
                var date = (dr.TreatDate as DateTime?);
                if (date != null)
                {
                    e.Value = date.Value.ToString("yyyy-MM-dd HH:mm");
                }
            });
            col = dgv.AddColumn("执行人", "TechnicianName");
            col = dgv.AddColumn("主管医生", "Attending");

            dgv.SetCellValue(new List<string>() { "IDX", "SpecialInfoCount", "ChargeStatus", "TreatDate", });

            foreach (DataGridViewColumn column in dgv.Columns)
            {
                #region 排序
                if (column.DataPropertyName == "WardName" ||
                    column.DataPropertyName == "TreatDate")
                {
                    column.SortMode = DataGridViewColumnSortMode.Programmatic;
                    column.CustomSort((dgv2, e, asc) =>
                    {
                        var dgv22 = col.DataGridView;
                        var ls = dgv22.DataSource as List<ZLRCSubModel>;
                        if (ls == null) return;
                        string fieldName = dgv22.Columns[e.ColumnIndex].DataPropertyName;
                        customOrderStr = customOrderStr == "" || customOrderStr.Contains("Asc") ? fieldName + " Desc" : fieldName + " Asc";
                        if (fieldName == "WardName")
                        {
                            dgv22.DataSource = asc ? ls.OrderBy(l => l.WardName).ToList() : ls.OrderByDescending(l => l.WardName).ToList();
                        }
                        if (fieldName == "TreatDate")
                        {
                            dgv22.DataSource = asc ? ls.OrderBy(l => l.TreatDate).ToList() : ls.OrderByDescending(l => l.TreatDate).ToList();
                        }
                        //dgv22.DataSource = asc ? ls.OrderBy(l => ((IDictionary<string, object>)l)[fieldName]).ToList() : ls.OrderByDescending(l => ((IDictionary<string, object>)l)[fieldName]).ToList();

                    });
                }
                #endregion
            }
        }
        static string customOrderStr = "";
        public override void Search(DateTime StartTime, DateTime EndTime)
        {
            string BaseWebServiceUri = PublicRes.GetConfig("C_SCHEDULE_018", "");
            string Uri = "";
            if (string.IsNullOrEmpty(BaseWebServiceUri))
                return;
            string[] urlList = BaseWebServiceUri.Split('=');
            if (urlList.Length <= 1)
                return;
            Uri = urlList[1];
            string requestJson = "{\"ServiceName\":\"SearchMachineTreatDetailInfo\",\"Param\":{\"Machine_ID\":" + this.CurMachine.Machine_ID + ",\"StartDate\":\"" + ((DateTime)StartTime).ToString("yyyy-MM-dd") + "\",\"EndDate\":\"" + ((DateTime)EndTime).AddDays(-1).ToString("yyyy-MM-dd") + "\",\"SearchDateType\":2}}";
            //string requestJson = "{\"ServiceName\":\"SearchMachineTreatInfo\",\"Param\":{\"Machine_ID\":" + "3" + ",\"StartDate\":\"2020-08-02\",\"EndDate\":\"2020-08-02\",\"SearchDateType\":2}}";
            LogHelper.WriteHourLog("MIP.Control.TechnicianList LoadTreatMentCountData ", "调用治疗人次接口入参：" + requestJson);
            string strResponse = MIP.UI.Utilities.FormHelper.PostUrl(Uri, requestJson);
            LogHelper.WriteHourLog("MIP.Control.TechnicianList LoadTreatMentCountData ", "调用治疗人次接口出参：" + strResponse);
            ZLRCRootModel myModel = JsonHelper.JsonToObject<ZLRCRootModel>(strResponse);
            if (myModel == null) return;
            if (myModel.ResultCode != 1 || myModel.ResponseModel == null) return;
            List<string> patientList = myModel.ResponseModel.Select(x => x.PatientID).ToList();
            string s1 = string.Join("','", patientList.ToArray());
            DataSet rs = BLL.TechnicianListBll.Instance.GetPatientSpecielByIDS(s1);
            foreach (ZLRCSubModel zLRCSub in myModel.ResponseModel)
            {
                if (rs != null && rs.Tables[0] != null && rs.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < rs.Tables[0].Rows.Count; i++)
                    {
                        if (zLRCSub.PatientID == rs.Tables[0].Rows[i]["PatientID"].ToString())
                        {
                            zLRCSub.HasCRB = rs.Tables[0].Rows[i]["HasCRB2"]==null || string.IsNullOrEmpty(rs.Tables[0].Rows[i]["HasCRB2"].ToString())?0: rs.Tables[0].Rows[i]["HasCRB2"].ToInt();
                            zLRCSub.SpecialInfoCount = rs.Tables[0].Rows[i]["SpecialInfoCount"] == null || string.IsNullOrEmpty(rs.Tables[0].Rows[i]["SpecialInfoCount"].ToString()) ? 0 : rs.Tables[0].Rows[i]["SpecialInfoCount"].ToInt();
                        }
                    }
                }
                else
                {
                    zLRCSub.HasCRB = 0;
                    zLRCSub.SpecialInfoCount = 0;
                }
            }

            List<ZLRCSubModel> orderList = myModel.ResponseModel.OrderBy(o => o.TreatDate).ToList();
            if (customOrderStr.Contains("WardName"))
            {
                if (customOrderStr.Contains("Asc"))
                {
                    orderList = myModel.ResponseModel.OrderBy(o => o.WardName).ToList();
                }
                else
                {
                    orderList = myModel.ResponseModel.OrderByDescending(o => o.WardName).ToList();
                }
            }
            if (customOrderStr.Contains("TreatDate"))
            {
                if (customOrderStr.Contains("Asc"))
                {
                    orderList = myModel.ResponseModel.OrderBy(o => o.TreatDate).ToList();
                }
                else
                {
                    orderList = myModel.ResponseModel.OrderByDescending(o => o.TreatDate).ToList();
                }
            }

            //var rs = BLL.TechnicianListBll.Instance.GetZLRC(new { StartTime, EndTime, this.CurMachine.Machine_ID });
            DGV.DataSource = orderList;

            if (VerticalScrollIndex > -1 && DGV.Rows.Count > VerticalScrollIndex)
                DGV.FirstDisplayedScrollingRowIndex = VerticalScrollIndex;
        }
    }


    public class frmShowDayData_YY : frmShowDayData
    {
        public override void SetControls(string machineName)
        {
            this.DayDesc = "预约日期：";
            this.Title = "预约人次-" + machineName;
            var dgv = DGV;
            flowLayoutPanel1.Visible = true;
            DataGridViewHelper.SetDefaultStyle(dgv, true);
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            var col = dgv.AddColumn("序号", "IDX");
            col.CellFormatting((dgv1, e) =>
            {
                e.Value = e.RowIndex + 1;
            });
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            col = dgv.AddColumn("预约日期", "App_DtTm").FixColumnWidth(140);
            col.CellFormatting((dgv1, e) =>
            {
                dynamic dr = dgv1.Rows[e.RowIndex].DataBoundItem;
                var date = (dr.App_DtTm as DateTime?);
                if (date != null)
                {
                    e.Value = date.Value.ToString("yyyy-MM-dd HH:mm");
                }
            });
            col = dgv.AddColumn("ID", "PatientID");
            col.FixColumnWidth(130);
            col.ShowTipsOnOverLength();
            //欠费标识
            if (ChargeInfo.ChargeTypeConfig > 0)
            {
                col = dgv.AddColumn("", "ChargeStatus");
                col.FixColumnWidth(30);
            }
            //特殊情况标识
            col = dgv.AddColumn("", "");
            SetSpeicalInfo(col);
            col = dgv.AddColumn("姓名", "PatientName");
            col.ColumnAligment(DataGridViewContentAlignment.MiddleLeft);
            col.FixColumnWidth(100);
            col.ShowTipsOnOverLength();
            col = dgv.AddColumn("治疗技术", "FunctionCode").FixColumnWidth(120);
            col.ShowTipsOnOverLength();
            col = dgv.AddColumn("预约备注", "Note");
            col.ShowTipsOnOverLength();
            col = dgv.AddColumn("排队状态", "CallStatus").FixColumnWidth(100);
            col.CellFormatting((dgv1, e) =>
            {
                dynamic dr = dgv1.Rows[e.RowIndex].DataBoundItem;
                int? s = dr.CallStatus;
                if (s == null)
                    return;
                //but 13667  正在呼叫改为  已呼叫
                if (s == 3) s = 4;
                e.Value = Enum.Parse(typeof(CallStatus), s.AsString("0"));
            });
            //int C_SYSTEM_008 = PublicRes.GetConfig("C_SYSTEM_008", "0").ToInt();
            //if (C_SYSTEM_008 != 0)
            //{
            //    col = dgv.AddColumn("可执行余额", "KZXYE").FixColumnWidth(145);
            //    SetKeZhiXingYuEr(col);
            //}
            //col = dgv.AddColumn("中断天数", "ZDTS").FixColumnWidth(120);
            //SetZDTSInfo(col);
            //col = dgv.AddColumn("最近治疗", "LastTreatDate").FixColumnWidth(140);
            //col.CellFormatting((dgv1, e) =>
            //{
            //    dynamic dr = dgv1.Rows[e.RowIndex].DataBoundItem;
            //    var date = (dr.LastTreatDate as DateTime?);
            //    if (date != null)
            //    {
            //        e.Value = date.Value.ToString("yyyy-MM-dd HH:mm");
            //    }
            //});
            col = dgv.AddColumn("主管医生", "Attending").FixColumnWidth(100);
            dgv.SetCellValue(new List<string>() { "IDX", "App_DtTm", "CallStatus", "SpecialInfoCount", "ChargeStatus", "LastTreatDate", "ZDTS" });
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                #region 排序
                if (column.DataPropertyName == "App_DtTm" ||
                    column.DataPropertyName == "LastTreatDate" ||
                    column.DataPropertyName == "ZDTS" ||
                    column.DataPropertyName == "KZXYE")
                {
                    column.SortMode = DataGridViewColumnSortMode.Programmatic;
                    column.CustomSort((dgv2, e, asc) =>
                    {
                        var ls = dgv.DataSource as List<dynamic>;
                        if (ls == null) return;
                        string fieldName = dgv.Columns[e.ColumnIndex].DataPropertyName;
                        customOrderStr = customOrderStr == "" || customOrderStr.Contains("Asc") ? fieldName + " Desc" : fieldName + " Asc";
                        dgv.DataSource = asc ? ls.OrderBy(l => ((IDictionary<string, object>)l)[fieldName]).ToList() : ls.OrderByDescending(l => ((IDictionary<string, object>)l)[fieldName]).ToList();
                    });
                }
                #endregion
            }
        }
        static string customOrderStr = "";

        public override void Search(DateTime StartTime, DateTime EndTime)
        {
            var rs = BLL.TechnicianListBll.Instance.GetYYRC(new { StartTime, EndTime, this.CurMachine.Machine_ID }, customOrderStr);
            DGV.DataSource = rs;


            var dt = new DataTable();
            dt.Columns.Add("LastTreatDate", typeof(string));
            dt.Columns.Add("TreatLocation", typeof(string));
            dt.Columns.Add("InterruptDays", typeof(int));
            foreach (dynamic dr in rs)
            {
                var nr = dt.NewRow();
                nr["LastTreatDate"] = dr.LastTreatDate;
                nr["TreatLocation"] = dr.TreatLocation;
                nr["InterruptDays"] = 0;
                dt.Rows.Add(nr);
            }
            SetInterruptDays(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rs[i].ZDTS = dt.Rows[i]["InterruptDays"];
            }

            var v1 = rs.Count(r => r.CallStatus == (int)CallStatus.未报到);
            var v2 = rs.Count(r => r.CallStatus == (int)CallStatus.已报到);
            var v3 = rs.Count(r => r.CallStatus == (int)CallStatus.已呼叫 || r.CallStatus == (int)CallStatus.正在呼叫);
            var v4 = rs.Count(r => r.CallStatus == (int)CallStatus.已候诊);
            var v5 = rs.Count(r => r.CallStatus == (int)CallStatus.已过号);
            SetInfo(v1, v2, v3, v4, v5);

            if (VerticalScrollIndex > -1 && DGV.Rows.Count > VerticalScrollIndex)
                DGV.FirstDisplayedScrollingRowIndex = VerticalScrollIndex;
        }
    }




    /// <summary>
    /// 0:未报道 1:已报道 2:已侯诊 3正在呼叫 4已呼叫 5撤回  9已过号
    /// </summary>
    public enum CallStatus
    {
        //0未报到
        未报到 = 0,
        //1已报道
        已报到 = 1,
        //2已候诊
        已候诊 = 2,
        //3正在呼叫
        正在呼叫 = 3,
        //4已呼叫
        已呼叫 = 4,
        //5撤回
        已撤回 = 5,
        //9已过号
        已过号 = 9,
        //99强制过号
        强制过号 = 99

    }




    public class ZLRCRootModel
    {
        public int ResultCode { get; set; }
        public string ResultInfo { get; set; }
        public List<ZLRCSubModel> ResponseModel { get; set; }
    }

    public class ZLRCSubModel
    {
        public string IDX { get; set; }

        public string Machine_Name { get; set; }
        public string WardName { get; set; }
        public string PatientID { get; set; }
        public string PatientName { get; set; }
        public string Site { get; set; }
        public string Function_Name { get; set; }
        public string FLDQty { get; set; }
        public string TreatDate { get; set; }
        public string TechnicianName { get; set; }
        public string Attending { get; set; }
        public string Professor { get; set; }
        public string DoctorGroup_Name { get; set; }

        public string Activity_ID { get; set; }
        public string ChargeStatus { get; set; }
        public int HasCRB { get; set; }
        public int SpecialInfoCount { get; set; }
    }



}

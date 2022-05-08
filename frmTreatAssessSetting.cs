using MIP.DAL;
using MIP.UI.Utilities;
using MIP.UI.Utilities.Controls;
using MIP.UI.Utilities.DialogForm;
using MIP.Utilities;
using MIP.Utilities.PublicResource;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YKD_Controls;

namespace MIP.UI.Manage
{
    public partial class frmTreatAssessSetting : BaseForm
    {
        public frmTreatAssessSetting()
        {
            InitializeComponent();
            this.ucListV1.Font = YkdTextFonts.TextFontBold;
            this.ucListV1.SetHasTopBanner();
            List<UCListVItem> tab = new List<UCListVItem>();
            if (PublicRes.HasMenu("TreatAssessSetting"))
            {
                tab.Add(new UCListVItem() { Text = "检验数据配置" });
                ucListV1.ItemClick += UcListV1_ItemClick;
                tab.Add(new UCListVItem() { Text = "中断预警配置" });
                this.ucListV1.Items = tab;
            }
            else
            {
                ucListV1.Visible = false;
            }

            SetDGV();
            btnAddJiaoyan.BtnClick += BtnAddJiaoyan_BtnClick;
            btnAddYujing.BtnClick += BtnAddYujing_BtnClick;
            Load += FrmSMSManage_Load;
            txtDiag.valueControl.BtnClick += new EventHandler(lblDiagnosisSearch_BtnClick);
            txtDiag.valueControl.TextChanged += new EventHandler(GiveFeedbackEventHandler);
            this.txtScreen.txtInput.KeyDown += txtScreen_KeyDown;
            this.txtScreen2.txtInput.KeyPress += new KeyPressEventHandler(this.txtScreen2_KeyPress);
            dgvInterruptWarning.CellPainting += dgv_CellPainting;
            dgvDataVerify.CellPainting += dgv_CellPainting;
        }

        private void txtScreen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtScreen_SearchClick(null, null);
            }
        }

        private void ValueControl_SelectedChangedEvent(object sender, EventArgs e)
        {
            LoadData("");
            LoadData2("", "");
        }


        private void BtnAddJiaoyan_BtnClick(object sender, EventArgs e)
        {
            OpenJiaoyanForm();
        }

        private void BtnAddYujing_BtnClick(object sender, EventArgs e)
        {
            OpenYujingForm();
        }

        private void OpenJiaoyanForm(string DataVerifyItemName = null)
        {
            //打开新建
            var frm = new frmTreatAssessSettingDVEdit();
            //frm.TopLevel = true;
            //frm.FormBorderStyle = FormBorderStyle.None;
            //frm.Size = this.Size;
            //frm.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            frm.DataVerifyItemName = DataVerifyItemName;
            frm.FormClosing += Frm_FormClosing;
            frm.BringToFront();
            //this.Controls.Add(frm);
            if (DialogResult.OK == frm.ShowDialog())
            {
                LoadData("");
            }

        }

        private void OpenYujingForm(string InterruptWarningID = null)
        {
            //打开新建
            var frm = new frmTreatAssessSettingIWEdit();
            //frm.TopLevel = true;
            //frm.FormBorderStyle = FormBorderStyle.None;
            //frm.Size = this.Size;
            //frm.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            frm.InterruptWarningID = InterruptWarningID;
            frm.FormClosing += Frm_FormClosing;
            frm.BringToFront();
            //this.Controls.Add(frm);
            if (DialogResult.OK == frm.ShowDialog())
            {
                LoadData2("", "");
            }

        }

        private void Frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LoadData("");
            LoadData2("", "");
        }

        private void FrmSMSManage_Load(object sender, EventArgs e)
        {
            //LoadTemplates();

            //IniMachhine();
            //btnSearch.OnBtnClick(null, null);
            if (PublicRes.HasMenu("TreatAssessSetting"))
            {
                ucListV1.CurrentItem = ucListV1.Items[0];
            }
            else
            {

                panelTemplate.Visible = false;
                panelSendLog.Dock = DockStyle.Fill;
                //this.btnSearch.OnBtnClick(null, null);
                panelSendLog.Visible = true;
            }
        }

        private void LoadData(string txtInputText, string txtInputText2 = "", string dicCode = "")
        {
            Task.Factory.StartNew(() =>
            {
                txtInputText = txtInputText.Trim();
                //数据校验
                string sql = "select DataVerifyID,DataVerifyItemName,WarningCondition + ' ' + WarningValue as WarningValue,WarningInfo,Notice,Suggest from DataVerifyConfig ";
                if (!string.IsNullOrEmpty(txtInputText))
                {
                    sql = sql + " where DataVerifyItemName like '%{0}%' or WarningCondition like '%{0}%' or WarningValue like '%{0}%' or WarningInfo like '%{0}%' or Notice like '%{0}%' or Suggest like '%{0}%'";
                    sql = string.Format(sql, txtInputText);
                }
                sql = sql + " order by DataVerifyItemName,OrderNo";
                DataTable dt = DbHelperSQL.QueryTable(sql);

                this.SafeBeginInvoke(() =>
                {
                    dgvDataVerify.DataSource = dt;
                });
            }).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    FormHelper.ShowTipsError("加载数据检验失败，" + t.Exception.InnerException.Message);
                    LogHelper.WriteException(t.Exception);
                    return;
                }
            });
        }
        void dgv_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (e.ColumnIndex != 1)
                return;
            var dgv = sender as DataGridView;
            var dt = dgv.DataSource as DataTable;
            if (dt == null || dt.Rows.Count == 0)
                return;
            var colField = dgv.Columns[e.ColumnIndex].DataPropertyName;
            var curValue = (dgv.Rows[e.RowIndex].DataBoundItem as DataRowView)[colField].ToString();
            if (string.IsNullOrWhiteSpace(curValue))
                return;
            var rect = GetRectByValue(dgv, e.RowIndex, e.ColumnIndex, curValue, dt, colField);
            if (rect == Rectangle.Empty)
                return;
            rect.Offset(e.CellBounds.Location);
            e.PaintBackground(e.CellBounds, true);
            e.Graphics.FillRectangle(Brushes.White, rect);
            using (var p = new Pen(dgv.GridColor))
                e.Graphics.DrawRectangle(p, rect);
            e.Graphics.DrawString(curValue, YkdTextFonts.TextFont, Brushes.Black, rect, UCListV.StringFormatCenter);
            e.Handled = true;
            return;
        }

        private void LoadData2(string txtInputText2, string dicCode)
        {
            Task.Factory.StartNew(() =>
            {
                txtInputText2 = txtInputText2.Trim();
                //中断预警
                string sql = "select DiagnosisCode + ' ' + DiagnosisName as DiagnosisName,* from InterruptWarningConfig where 1 = 1 ";
                if (!string.IsNullOrEmpty(txtInputText2))
                {
                    sql = sql + " and DiagnosisCode + DiagnosisName + convert(varchar,ab) + convert(varchar,K) + convert(varchar,Tdelay) + Notice1 + Suggest1 + Notice2 + Suggest2 like '%{0}%'";
                    sql = string.Format(sql, txtInputText2);
                }
                if (!string.IsNullOrEmpty(dicCode))
                {
                    sql = sql + " and DiagnosisCode + ' ' + DiagnosisName = '{0}'";
                    sql = string.Format(sql, dicCode);
                }
                sql = sql + " order by OrderNo";
                DataTable dt2 = DbHelperSQL.QueryTable(sql);

                DataTable iwDt = new DataTable();
                iwDt.Columns.Add("InterruptWarningID");
                iwDt.Columns.Add("DiagnosisName");
                iwDt.Columns.Add("ab");
                iwDt.Columns.Add("K");
                iwDt.Columns.Add("Tdelay");
                iwDt.Columns.Add("Interrupt");
                iwDt.Columns.Add("Notice");
                iwDt.Columns.Add("Suggest");
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    iwDt.Rows.Add(dt2.Rows[i]["InterruptWarningID"].ToString(), dt2.Rows[i]["DiagnosisName"].ToString(), dt2.Rows[i]["ab"].ToString()
                        , dt2.Rows[i]["K"].ToString(), dt2.Rows[i]["Tdelay"].ToString(), "中断天数(工作日)<= 剩余治疗次数", dt2.Rows[i]["Notice1"].ToString(), dt2.Rows[i]["Suggest1"].ToString());
                    iwDt.Rows.Add(dt2.Rows[i]["InterruptWarningID"].ToString(), dt2.Rows[i]["DiagnosisName"].ToString(), dt2.Rows[i]["ab"].ToString()
                       , dt2.Rows[i]["K"].ToString(), dt2.Rows[i]["Tdelay"].ToString(), "中断天数(工作日)> 剩余治疗次数", dt2.Rows[i]["Notice2"].ToString(), dt2.Rows[i]["Suggest2"].ToString());
                }
                this.SafeBeginInvoke(() =>
                {
                    dgvInterruptWarning.DataSource = iwDt;
                });
            }).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    FormHelper.ShowTipsError("加载数据检验失败，" + t.Exception.InnerException.Message);
                    LogHelper.WriteException(t.Exception);
                    return;
                }
            });
        }
        private void UcListV1_ItemClick(UCListVItem item, MouseEventArgs e)
        {
            panelSendLog.Dock = DockStyle.Fill;
            panelTemplate.Dock = DockStyle.Fill;
            if (item.Text == ucListV1.Items[0].Text)
            {
                panelSendLog.Visible = false;
                LoadData("");
                panelTemplate.Visible = true;
            }
            else
            {
                panelTemplate.Visible = false;
                LoadData2("", "");
                panelSendLog.Visible = true;
            }
        }


        Tuple<string, Size>[] Btns = new Tuple<string, Size>[] {
        new Tuple<string, Size>("编辑", new Size(60, 28)),
        new Tuple<string, Size>("删除", new Size(60, 28))};

        private void SetDGV()
        {
            //数据校验
            dgvDataVerify.BackgroundColor = Color.White;
            dgvDataVerify.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGridViewHelper.SetDefaultStyle(this.dgvDataVerify, false);
            dgvDataVerify.AddColumn("检验数据ID", "DataVerifyID").FixColumnWidth(0).Visible = false;
            dgvDataVerify.AddColumn("数据名称", "DataVerifyItemName").ShowTipsOnOverLength().FixColumnWidth(150);
            dgvDataVerify.AddColumn("预警值", "WarningValue").FixColumnWidth(150);
            dgvDataVerify.AddColumn("预警信息", "WarningInfo").ShowTipsOnOverLength().FixColumnWidth(150);
            dgvDataVerify.AddColumn("智能提醒", "Notice").ShowTipsOnOverLength();
            dgvDataVerify.AddColumn("治疗建议", "Suggest").ShowTipsOnOverLength();
            dgvDataVerify.AddColumn("操作", "Operation").FixColumnWidth(300)
                .SetOperationButtons((dgv, e) =>
            {
                var dt = dgv.DataSource as DataTable;
                if (dt == null || dt.Rows.Count == 0)
                    return;
                var colField = "DataVerifyItemName";
                var curValue = (dgv.Rows[e.RowIndex].DataBoundItem as DataRowView)[colField].ToString();
                if (string.IsNullOrWhiteSpace(curValue))
                    return;
                var rect = GetRectByValue(dgv, e.RowIndex, e.ColumnIndex, curValue, dt, colField);
                if (rect == Rectangle.Empty)
                    return;
                var curRowRect = dgv.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                rect.Offset(curRowRect.Location);
                var rects = GetBtnRects(dgv, rect);
                var p = e.Location;
                p.Offset(curRowRect.Left - rect.Left, curRowRect.Top - rect.Top);
                var btn = rects.FirstOrDefault(r => r.Value.Key.Contains(p));
                if (btn != null && btn.HasValue)
                {
                    Operation(btn.Value.Value, curValue);
                }
            }, (dgv, e) =>
            {
                var dt = dgv.DataSource as DataTable;
                if (dt == null || dt.Rows.Count == 0)
                    return;
                var colField = "DataVerifyItemName";
                var curValue = (dgv.Rows[e.RowIndex].DataBoundItem as DataRowView)[colField].ToString();

                if (string.IsNullOrWhiteSpace(curValue))
                    return;
                var rect = GetRectByValue(dgv, e.RowIndex, e.ColumnIndex, curValue, dt, colField);
                if (rect == Rectangle.Empty)
                    return;
                rect.Offset(e.CellBounds.Location);

                e.PaintBackground(e.CellBounds, true);

                e.Graphics.FillRectangle(Brushes.White, rect);
                using (var p = new Pen(dgv.GridColor))
                    e.Graphics.DrawRectangle(p, rect);
                var rects = GetBtnRects(dgv, rect);
                foreach (var item in rects)
                {
                    var rectBtn = item.Value.Key;
                    rectBtn.Offset(rect.Location);
                    var color = item.Value.Value == Btns[1].Item1 ? YkdBasisColors.RedColor : YkdBasisColors.GrayRectColor;
                    var fcolor = item.Value.Value != Btns[1].Item1 ? Color.Black : YkdBasisColors.RedColor;
                    var txt = item.Value.Value;
                    FormHelper.DrawRectFlag(e.Graphics, rectBtn, item.Value.Key.Width, item.Value.Key.Height, Color.White, color, txt, YkdTextFonts.TextFont, fcolor);
                }
            }, (dgv, e) =>
            {
                var dt = dgv.DataSource as DataTable;
                if (dt == null || dt.Rows.Count == 0)
                    return;
                var colField = "DataVerifyItemName";
                var curValue = (dgv.Rows[e.RowIndex].DataBoundItem as DataRowView)[colField].ToString();
                if (string.IsNullOrWhiteSpace(curValue))
                    return;

                var rect = GetRectByValue(dgv, e.RowIndex, e.ColumnIndex, curValue, dt, colField);
                if (rect == Rectangle.Empty)
                    return;
                var curRowRect = dgv.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                rect.Offset(curRowRect.Location);
                var rects = GetBtnRects(dgv, rect);
                var p = e.Location;
                p.Offset(curRowRect.Left - rect.Left, curRowRect.Top - rect.Top);
                var btn = rects.FirstOrDefault(r => r.Value.Key.Contains(p));
                if (btn != null && btn.HasValue)
                {
                    dgv.Cursor = Cursors.Hand;
                    return;
                }
                dgv.Cursor = Cursors.Default;
            });
            //网格线
            dgvDataVerify.GridColor = Color.FromArgb(211, 211, 211);
            dgvDataVerify.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dgvDataVerify.AdvancedCellBorderStyle.All = DataGridViewAdvancedCellBorderStyle.Single;
            dgvDataVerify.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvDataVerify.AdvancedColumnHeadersBorderStyle.All = DataGridViewAdvancedCellBorderStyle.Single;

            //中断预警
            dgvInterruptWarning.BackgroundColor = Color.White;
            dgvInterruptWarning.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGridViewHelper.SetDefaultStyle(this.dgvInterruptWarning, false);
            dgvInterruptWarning.AddColumn("ID", "InterruptWarningID").FixColumnWidth(0).Visible = false;
            dgvInterruptWarning.AddColumn("病种", "DiagnosisName").ShowTipsOnOverLength().FixColumnWidth(300);
            dgvInterruptWarning.AddColumn("α/β", "ab").FixColumnWidth(150);
            dgvInterruptWarning.AddColumn("K", "K").FixColumnWidth(150);
            dgvInterruptWarning.AddColumn("Tdelay", "Tdelay").FixColumnWidth(150);
            dgvInterruptWarning.AddColumn("中断天数", "Interrupt").ShowTipsOnOverLength();
            dgvInterruptWarning.AddColumn("预警提示", "Notice").ShowTipsOnOverLength();
            dgvInterruptWarning.AddColumn("治疗建议", "Suggest").ShowTipsOnOverLength();
            dgvInterruptWarning.AddColumn("操作", "Operation").FixColumnWidth(200)
                .SetOperationButtons((dgv, e) =>
            {
                var dt = dgv.DataSource as DataTable;
                if (dt == null || dt.Rows.Count == 0)
                    return;
                var colField = "InterruptWarningID";
                var curValue = (dgv.Rows[e.RowIndex].DataBoundItem as DataRowView)[colField].ToString();
                if (string.IsNullOrWhiteSpace(curValue))
                    return;

                var rect = GetRectByValue(dgv, e.RowIndex, e.ColumnIndex, curValue, dt, colField);
                if (rect == Rectangle.Empty)
                    return;
                var curRowRect = dgv.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                rect.Offset(curRowRect.Location);
                var rects = GetBtnRects(dgv, rect);
                var p = e.Location;
                p.Offset(curRowRect.Left - rect.Left, curRowRect.Top - rect.Top);
                var btn = rects.FirstOrDefault(r => r.Value.Key.Contains(p));
                if (btn != null && btn.HasValue)
                {
                    Operation2(btn.Value.Value, curValue);
                }
            }, (dgv, e) =>
            {
                var dt = dgv.DataSource as DataTable;
                if (dt == null || dt.Rows.Count == 0)
                    return;
                var colField = "InterruptWarningID";
                var curValue = (dgv.Rows[e.RowIndex].DataBoundItem as DataRowView)[colField].ToString();
                if (string.IsNullOrWhiteSpace(curValue))
                    return;
                var rect = GetRectByValue(dgv, e.RowIndex, e.ColumnIndex, curValue, dt, colField);
                if (rect == Rectangle.Empty)
                    return;
                rect.Offset(e.CellBounds.Location);
                e.PaintBackground(e.CellBounds, true);
                e.Graphics.FillRectangle(Brushes.White, rect);
                using (var p = new Pen(dgv.GridColor))
                    e.Graphics.DrawRectangle(p, rect);

                var rects = GetBtnRects(dgv, rect);
                foreach (var item in rects)
                {
                    var rectBtn = item.Value.Key;
                    rectBtn.Offset(rect.Location);
                    var color = item.Value.Value == Btns[1].Item1 ? YkdBasisColors.RedColor : YkdBasisColors.GrayRectColor;
                    var fcolor = item.Value.Value != Btns[1].Item1 ? Color.Black : YkdBasisColors.RedColor;
                    var txt = item.Value.Value;
                    FormHelper.DrawRectFlag(e.Graphics, rectBtn, item.Value.Key.Width, item.Value.Key.Height, Color.White, color, txt, YkdTextFonts.TextFont, fcolor);
                }
                e.Handled = true;
            }, (dgv, e) =>
            {
                var dt = dgv.DataSource as DataTable;
                if (dt == null || dt.Rows.Count == 0)
                    return;
                var colField = "InterruptWarningID";
                var curValue = (dgv.Rows[e.RowIndex].DataBoundItem as DataRowView)[colField].ToString();
                if (string.IsNullOrWhiteSpace(curValue))
                    return;

                var rect = GetRectByValue(dgv, e.RowIndex, e.ColumnIndex, curValue, dt, colField);
                if (rect == Rectangle.Empty)
                    return;
                var curRowRect = dgv.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                rect.Offset(curRowRect.Location);
                var rects = GetBtnRects(dgv, rect);
                var p = e.Location;
                p.Offset(curRowRect.Left - rect.Left, curRowRect.Top - rect.Top);
                var btn = rects.FirstOrDefault(r => r.Value.Key.Contains(p));
                if (btn != null && btn.HasValue)
                {
                    dgv.Cursor = Cursors.Hand;
                    return;
                }
                dgv.Cursor = Cursors.Default;
            });
            //网格线
            dgvInterruptWarning.GridColor = Color.FromArgb(211, 211, 211);
            dgvInterruptWarning.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dgvInterruptWarning.AdvancedCellBorderStyle.All = DataGridViewAdvancedCellBorderStyle.Single;
            dgvInterruptWarning.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvInterruptWarning.AdvancedColumnHeadersBorderStyle.All = DataGridViewAdvancedCellBorderStyle.Single;
            //this.dataGridView1.Location = new System.Drawing.Point(20, 0);
        }

        private Rectangle GetRectByValue(DataGridView dgv, int curRowIndex, int curColumnIndex, string curValue, DataTable dt, string colField)
        {
            var firstRowIdx = curRowIndex - 1;
            while (firstRowIdx >= 0)
            {
                if (dt.Rows[firstRowIdx][colField].ToString() != curValue)
                    break;
                firstRowIdx--;
            }
            var lastRowIdx = curRowIndex + 1;
            while (lastRowIdx < dt.Rows.Count)
            {
                if (dt.Rows[lastRowIdx][colField].ToString() != curValue)
                    break;
                lastRowIdx++;
            }
            if (firstRowIdx == lastRowIdx)
            {
                return Rectangle.Empty;
            }
            var height = ((lastRowIdx - 1) - (firstRowIdx + 1) + 1) * dgv.RowTemplate.Height;
            var offsetY = ((curRowIndex) - (firstRowIdx + 1)) * dgv.RowTemplate.Height;
            var rect = new Rectangle(0, -offsetY, dgv.Columns[curColumnIndex].Width, height);
            rect.Offset(-1, -1);
            return rect;
        }

        private void Operation2(string cmd, string InterruptWarningID)
        {
            if (InterruptWarningID == null || InterruptWarningID == "") return;
            try
            {
                switch (cmd)
                {
                    case "删除":
                        if (FrmShadowDialog.ShowAskDialog(this, string.Format("确定删除该配置吗？", cmd), "确认") == DialogResult.Cancel) return;
                        DbHelperSQL.ExecuteSql("delete InterruptWarningConfig where InterruptWarningID = '" + InterruptWarningID + "'");
                        LoadData2("", "");
                        FormHelper.ShowTipsSuccess("操作成功");
                        break;
                    case "编辑":
                        OpenYujingForm(InterruptWarningID);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                FormHelper.ShowTipsError("操作失败" + ex.Message);
            }
        }

        private void Operation(string cmd, string DataVerifyItemName)
        {
            if (DataVerifyItemName == null || DataVerifyItemName == "") return;
            try
            {
                switch (cmd)
                {
                    case "删除":
                        if (FrmShadowDialog.ShowAskDialog(this, string.Format("确定要删除该预警值配置吗？", cmd), "确认") == DialogResult.Cancel) return;
                        //row.Edit_DtTm = ServerDateTimeHelper.GetServerDateTime().AsDateTime();
                        //row.Edit_User = PublicRes.userinfo.UserID.AsString("");
                        //new BLL.SMSTemplate().Update(row);
                        //FormHelper.ShowTipsSuccess("删除：" + DataVerifyItemName);
                        DbHelperSQL.ExecuteSql("delete DataVerifyConfig where DataVerifyItemName = '" + DataVerifyItemName + "'");
                        LoadData("");
                        FormHelper.ShowTipsSuccess("操作成功");
                        break;
                    case "编辑":
                        OpenJiaoyanForm(DataVerifyItemName);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                FormHelper.ShowTipsError("操作失败" + ex.Message);
            }
        }

        private List<KeyValuePair<Rectangle, string>?> GetBtnRects(DataGridView dgv, Rectangle cell)
        {
            var all_width = Btns.Sum(b => b.Item2.Width) + DataGridViewHelper.Space_Width * 2;
            var rect_height = Btns[0].Item2.Height;
            var x1 = (cell.Width - all_width) / 2;
            var y1 = (cell.Height - rect_height) / 2;
            var rect1 = new Rectangle(x1, y1, Btns[0].Item2.Width, rect_height);
            var rect2 = rect1;
            rect2.Offset(Btns[0].Item2.Width + DataGridViewHelper.Space_Width, 0);
            rect2.Width = Btns[1].Item2.Width;
            return new List<KeyValuePair<Rectangle, string>?>() {
                new KeyValuePair<Rectangle, string>(rect1, Btns[0].Item1),
                new KeyValuePair<Rectangle, string>(rect2, Btns[1].Item1), };
        }

        void GiveFeedbackEventHandler(object sender, EventArgs e)
        {
            LoadData2("", txtDiag.valueControl.InputText);
        }

        /// <summary>
        /// 诊断选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lblDiagnosisSearch_BtnClick(object sender, EventArgs e)
        {
            var frm = new frmDiagnosisQuery()
            {
                Text = "诊断查询",
                StartPosition = FormStartPosition.CenterScreen,
                IsShowMaskDialog = false,
                WindowState = FormWindowState.Normal
            };
            frm.SetTitle("诊断查询");
            var dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel) return;

            if (dr == DialogResult.OK)
            {
                this.txtDiag.valueControl.SelectedObj = null;
                if (frm.SelectedICD10 == null)
                    return;
                this.txtDiag.valueControl.SelectedObj = new { Code = frm.SelectedICD10.ICD10code, Value = frm.SelectedICD10.Chinese };
                txtDiag.SetSelectValue(frm.SelectedICD10.ICD10code + " " + frm.SelectedICD10.Chinese);

            }
            else if (dr == DialogResult.Yes)
            {
                txtDiag.SetSelectValue(null);
            }
        }

        private void txtScreen2_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtScreen2_SearchClick(null, null);
            }
        }

        private void txtScreen_SearchClick(object sender, EventArgs e)
        {
            LoadData(txtScreen.txtInput.Text);
        }

        private void txtScreen2_SearchClick(object sender, EventArgs e)
        {
            LoadData2(txtScreen2.txtInput.Text, txtDiag.GetSelectValue().ToString());
        }
    }
}

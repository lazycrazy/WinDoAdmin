using WinDo.Model;
using WinDo.UI.Utilities;
using WinDo.Utilities;
using WinDo.Utilities.PublicResource;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinDoControls.Controls;
using WinDoControls.Forms;
using WinDoControls;
using WinDo.Utilities.Mock;

namespace WinDo.UI.Manage
{
    public partial class frmSystemDicManage : BaseForm
    {
        private int comselectIndex;
        private string textOld = string.Empty;
        private bool checkPage = true;
        public frmSystemDicManage()
        {
            InitializeComponent();
            //下拉框
            SetCombBox();
            //列表
            SetDgvSystemDicList();
            comselectIndex = 0;
            ucdbPagerControl1.PageChanged += new Action(page1_PageChanged);
            //comSystemDic.valueControl.SelectedChangedEvent += new EventHandler(Com_SelectedChangedEvent);
            //ucTextBoxClearKeyWord.TextChanged += new EventHandler(ucTextBox_TextChanged);
            dataGridView1.CellMouseDoubleClick += DataGridView1_CellMouseDoubleClick;
        }

        private void DataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dynamic dr = dataGridView1.Rows[e.RowIndex].DataBoundItem;
            Edit(dr);
        }
        void Edit(dynamic dr)
        {
            var frmsystemDicEdit = new frmSystemDicEdit();
            frmsystemDicEdit.cSystemDicCode = dr.DicGroupCode;
            frmsystemDicEdit.Title = dr.Module + "-" + dr.DicGroupName;
            frmsystemDicEdit.UserNote = dr.UserNote;
            frmsystemDicEdit.Remark = dr.Remark;

            frmsystemDicEdit.SystemGroupDicID = dr.SystemGroupDicID;

            if (DialogResult.OK == frmsystemDicEdit.ShowDialog())
            {
                Task.Factory.StartNew(() =>
                {
                    ClientCache.Instance.InitSystemDicList();
                });
                try
                {
                    FrmTips.ShowTipsSuccess(this.MainForm, "操作成功");
                    checkPage = false;
                    btnQuery_BtnClick(null, null);
                    checkPage = true;
                }
                catch (Exception ex)
                {
                    FrmTips.ShowTipsError(this.MainForm, "操作失败" + ex.Message);
                }
            }
        }
        void page1_PageChanged()
        {
            checkPage = false;
            btnQuery_BtnClick(null, null);
            checkPage = true;
        }
        private void frmSystemDicManage_Load(object sender, EventArgs e)
        {

            // ucdbPagerControl1.PageIndex=1;
            Task.Factory.StartNew(() =>
            {
                //刷新权限缓存
                ClientCache.Instance.RefreshMenuAndPrivileges();
                Thread.Sleep(10);
                this.BeginInvoke((Action)(() =>
                {
                    btnQuery.OnBtnClick(null, null);
                }));
                Thread.Sleep(10);
                this.BeginInvoke((Action)(() =>
                {
                    Privileges_SelectedItemEvent(null, null);
                }));
            });
        }
        void Privileges_SelectedItemEvent(object sender, EventArgs e)
        {
            btnQuery.OnBtnClick(btnQuery, EventArgs.Empty);
        }

        private void btnQuery_BtnClick(object sender, EventArgs e)
        {

            if (checkPage)
            {
                ucdbPagerControl1.PageIndex = 1;
            }
            //查询数据库
            int pageIdx = ucdbPagerControl1.PageIndex;
            //获得每页显示的记录数
            int pageSize = ucdbPagerControl1.PageSize;
            //计算显示记录的开始值
            int startIdx = (pageIdx - 1) * pageSize;
            //计算显示记录的结束值
            int endIdx = pageIdx * pageSize;
            //获得从开始值到结束值的记录

            //dgvSystemDicList.ShowIsQuery();
            var keyWord = ucTextBoxClearKeyWord.txtInput.Text.Trim();
            var sysType = comSystemDic.valueControl.SelectedValue;

            Task.Factory.StartNew(() =>
            {
                var ll = MockData.SystemDicGroup.Where(d=>d.Module=="系统").ToList();
                var totalCount = ll.Count;//总数
                this.SafeBeginInvoke(() =>
                {
                    ucdbPagerControl1.TotalCount = totalCount;
                    dataGridView1.DataSource = ll;
                });
            });
        }

        private void SetCombBox()
        {
            comSystemDic.valueControl.BoxStyle = ComboBoxStyle.DropDownList;
            //var SystemDicSysType = PublicRes.lstSystemdicSysType.Select(o => new KeyValuePair<string, string>(o.SysType, o.SysType)).ToList();

            var SystemDicSysType = PublicRes.lstSystemDicGroup.Select(o => new KeyValuePair<string, string>(o.Module, o.Module)).ToList();

            SystemDicSysType.Insert(0, FormHelper.NullItem);
            comSystemDic.valueControl.Source = SystemDicSysType.Distinct().ToList();

        }

        private void SetDgvSystemDicList()
        {
            List<DataGridViewColumnEntity> lstCulumns = new List<DataGridViewColumnEntity>();
            lstCulumns.Add(new DataGridViewColumnEntity() { DataField = "Module", HeadText = "模块", Width = 100, WidthType = SizeType.Percent, TextAlign = ContentAlignment.MiddleCenter, AllowSort = true });
            lstCulumns.Add(new DataGridViewColumnEntity() { DataField = "DicGroupName", HeadText = "字典名", Width = 100, WidthType = SizeType.Percent, TextAlign = ContentAlignment.MiddleCenter, AllowSort = true });
            lstCulumns.Add(new DataGridViewColumnEntity() { DataField = "DicGroupCode", HeadText = "Code", Width = 100, WidthType = SizeType.Percent, TextAlign = ContentAlignment.MiddleCenter, AllowSort = true });
            lstCulumns.Add(new DataGridViewColumnEntity() { DataField = "UserNote", HeadText = "解释说明", Width = 100, WidthType = SizeType.Percent, TextAlign = ContentAlignment.MiddleCenter });
            lstCulumns.Add(new DataGridViewColumnEntity() { DataField = "Remark", HeadText = "备注", Width = 100, WidthType = SizeType.Percent, TextAlign = ContentAlignment.MiddleCenter });
            lstCulumns.Add(new DataGridViewColumnEntity() { DataField = "", HeadText = "操作", Width = 300, WidthType = SizeType.Absolute, TextAlign = ContentAlignment.MiddleCenter });


            foreach (var col in lstCulumns)
            {
                var dCol = dataGridView1.AddColumn(col.HeadText, col.DataField);
                dCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dCol.ShowTipsOnOverLength();
            }
            DataGridViewHelper.SetDefaultStyle(dataGridView1, true);
            dataGridView1.SetCellValue();
            dataGridView1.Columns[dataGridView1.Columns.Count - 1].FixColumnWidth(100);
            dataGridView1.Columns[dataGridView1.Columns.Count - 1]
                .SetOperationButtons((dgv, e) =>
                {
                    var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    var rects = DataGridViewHelper.GetBtnsRects(dgv, cell, Btns);
                    dynamic dr = dgv.Rows[e.RowIndex].DataBoundItem;
                    if (dr == null) return;
                    var rect = rects.FirstOrDefault(r => r.Key.Contains(e.Location));
                    if (!rect.Equals(default(KeyValuePair<Rectangle, string>)))
                    {
                        if (rect.Value == Btns[0].Item1)
                        {
                            Edit(dr);
                        }
                    }
                }, (dgv, e) =>
                {
                    var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    var rects = DataGridViewHelper.GetBtnsRects(dgv, cell, Btns);
                    foreach (var item in rects)
                    {
                        var rect = item.Key;
                        var dr = dgv.Rows[e.RowIndex].DataBoundItem;
                        if (dr == null) return;
                        rect.Offset(e.CellBounds.Location);
                        var color = WDColors.GrayRectColor;
                        var fcolor = Color.Black;
                        var txt = item.Value;
                        ControlHelper.DrawRectFlag(e.Graphics, rect, item.Key.Width, item.Key.Height, Color.White, color, txt,
                            WDFonts.TextFont, fcolor
                            );
                    }
                }, (dgv, e) =>
                {
                    var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    var rects = DataGridViewHelper.GetBtnsRects(dgv, cell, Btns);
                    var rect = rects.FirstOrDefault(r => r.Key.Contains(e.Location));
                    if (!rect.Equals(default(KeyValuePair<Rectangle, string>)))
                    {
                        dgv.Cursor = Cursors.Hand;
                        return;
                    }
                    dgv.Cursor = Cursors.Default;
                });
        }


        Tuple<string, Size>[] Btns = new Tuple<string, Size>[] {
        new Tuple<string, Size>("编辑", new Size(60, 28))
        };

        private void frmSystemDicManage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                checkPage = false;
                btnQuery_BtnClick(null, null);
                checkPage = true;
            }
        }
    }
}

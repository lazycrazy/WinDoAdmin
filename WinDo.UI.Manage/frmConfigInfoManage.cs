using WinDo.Model;
using WinDo.UI.Utilities;
using WinDo.Utilities;
using WinDo.Utilities.PublicResource;
using Newtonsoft.Json;
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
    public partial class frmConfigInfoManage : BaseForm
    {
        private bool checkPage = true;
        public frmConfigInfoManage()
        {
            InitializeComponent();
            //下拉框
            SetCombBox();
            //列表
            SetDgvSystemDicList();
            ucdbPagerControl1.PageChanged += new Action(page1_PageChanged);
        }
        void page1_PageChanged()
        {
            checkPage = false;
            LoadData();
            checkPage = true;
        }
        private void frmSystemDicManage_Load(object sender, EventArgs e)
        {
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

        private string soryType = " asc ";
        private string roderColumn = " CGroup,Cname ";

        private void btnQuery_BtnClick(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
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

            var keyWord = ucTextBoxClearKeyWord.txtInput.Text.TrimStart().TrimEnd();
            var sysType = comConfigGroup.valueControl.SelectedValue;

            Task.Factory.StartNew(() =>
            {
                //dynamic ups = configBL.GetAllMsgInfoList(sysType, keyWord, startIdx + 1, endIdx, soryType, roderColumn);
                //var totalCount = ups.Entity.TotalCount;//总数
                //var lst = ups.Entity.List as List<dynamic>;
                //var ll = lst.ToList();
                var ll = MockData.Configs.Where(c => c.CGroup == "排队叫号" && c.Type == 1).ToList();
                var totalCount = ll.Count;//总数
                if (!this.Disposing && this.IsHandleCreated)
                    this.BeginInvoke((Action)(() =>
                    {
                        ucdbPagerControl1.TotalCount = totalCount;
                        dataGridView1.DataSource = ll;
                    }));
            }).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    FormHelper.ShowTipsError(t.Exception.Message);
                }
            });
        }

        private void SetCombBox()
        {
            comConfigGroup.valueControl.BoxStyle = ComboBoxStyle.DropDownList;
            var configGroup = PublicRes.lstConfigCGroup.Select(o => new KeyValuePair<string, string>(o.CGroup, o.CGroup)).ToList();
            configGroup.Insert(0, FormHelper.NullItem);
            comConfigGroup.valueControl.Source = configGroup;
        }

        private void SetDgvSystemDicList()
        {
            List<DataGridViewColumnEntity> lstCulumns = new List<DataGridViewColumnEntity>();
            lstCulumns.Add(new DataGridViewColumnEntity() { DataField = "CGroup", HeadText = "模块", Width = 100, WidthType = SizeType.Percent, TextAlign = ContentAlignment.MiddleCenter, AllowSort = true });
            lstCulumns.Add(new DataGridViewColumnEntity() { DataField = "CName", HeadText = "名称", Width = 100, WidthType = SizeType.Percent, TextAlign = ContentAlignment.MiddleCenter, AllowSort = true });
            lstCulumns.Add(new DataGridViewColumnEntity() { DataField = "Ckey", HeadText = "Code", Width = 100, WidthType = SizeType.Percent, TextAlign = ContentAlignment.MiddleCenter, AllowSort = true });
            lstCulumns.Add(new DataGridViewColumnEntity() { DataField = "Value", HeadText = "配置值", Width = 100, WidthType = SizeType.Percent, TextAlign = ContentAlignment.MiddleCenter });
            //PRD文档上，没有这一列
            lstCulumns.Add(new DataGridViewColumnEntity() { DataField = "Note", HeadText = "配置说明", Width = 100, WidthType = SizeType.Percent, TextAlign = ContentAlignment.MiddleCenter, AllowSort = true });
            lstCulumns.Add(new DataGridViewColumnEntity() { DataField = "UserNote", HeadText = "备注", Width = 100, WidthType = SizeType.Percent, TextAlign = ContentAlignment.MiddleCenter });
            lstCulumns.Add(new DataGridViewColumnEntity() { DataField = "", HeadText = "操作", Width = 300, WidthType = SizeType.Absolute, TextAlign = ContentAlignment.MiddleCenter });

            foreach (var col in lstCulumns)
            {
                var dCol = dataGridView1.AddColumn(col.HeadText, col.DataField);
                dCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dCol.ShowTipsOnOverLength();
            }
            DataGridViewHelper.SetDefaultStyle(dataGridView1, true);
            dataGridView1.CellMouseDoubleClick += DataGridView1_CellMouseDoubleClick;
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

        void Edit(dynamic dr)
        {
            if (dr == null)
                return;
            var frmConfigEdit = new frmConfigEdit();
            frmConfigEdit.cConfigID = dr.ID;
            frmConfigEdit.Title = dr.CGroup + "-" + dr.CName;
            frmConfigEdit.Value = dr.Value;
            frmConfigEdit.KeyValueType = Convert.ToInt32(dr.KeyValueType == null ? 1 : dr.KeyValueType);//理论上来说，这个值不可能为非数值类型  1：文本，2下拉
            frmConfigEdit.Note = dr.Note;
            frmConfigEdit.UserNote = dr.UserNote;
            if (Convert.ToInt32(dr.KeyValueType) == 2)
            {
                if (dr.KeyValues == null)
                {
                    dr.KeyValues = 0;
                }
                var vaList = JsonConvert.DeserializeObject<Dictionary<string, string>>(dr.KeyValues);
                frmConfigEdit.ComboxValues = new List<KeyValuePair<string, string>>();
                foreach (var item in vaList)
                {
                    frmConfigEdit.ComboxValues.Add(item);
                }
                frmConfigEdit.Height = 600;
            }

            if (DialogResult.OK == frmConfigEdit.ShowDialog())
            {
                Task.Factory.StartNew(() =>
                {
                    ClientCache.Instance.InitConfigList();
                });
                try
                {
                    FrmTips.ShowTipsSuccess(this.MainForm, "操作成功");
                    checkPage = false;
                    LoadData();
                    checkPage = true;
                }
                catch (Exception ex)
                {
                    FrmTips.ShowTipsError(this.MainForm, "操作失败" + ex.Message);
                }
            }
        }
        private void DataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dynamic dr = dataGridView1.Rows[e.RowIndex].DataBoundItem;
            Edit(dr);
        }

        Tuple<string, Size>[] Btns = new Tuple<string, Size>[] {
        new Tuple<string, Size>("编辑", new Size(60, 28))
        };


        private void frmConfigInfoManage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                checkPage = false;
                LoadData();
                checkPage = true;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinDo.UI.Utilities;

using WinDoControls.Controls;
using WinDo.Utilities;
using System.Runtime.InteropServices;
using WinDoControls.Forms;
using WinDo.UI.Utilities.DialogForm;
using WinDo.Utilities.PublicResource;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using WinDoControls.IconSvg;
using WinDoControls;
using WinDo.Utilities.Mock;
using WinDo.Utilities.PublicLibrary;

namespace WinDo.UI.Manage
{
    public partial class frmUserManage : BaseForm
    {
        public frmUserManage()
        {
            InitializeComponent();
            ControlHelper.SetControlsDouble(this);
            this.ucListV_UserManage1.Font = WDFonts.TextFontBold;
            ucListV_UserManage1.Items = new List<WinDoListItem>() {
                new WinDoListItem(){ Text="用户列表",Data=this.tabPage1},
                new WinDoListItem(){ Text="权限矩阵",Data=this.tabPage2}
            };
            ucListV_UserManage1.ItemClick += UcListV1_ItemClick;
            ucListV_UserManage1.CurrentItem = ucListV_UserManage1.Items[0];
            ucListV_UserManage1.BackColor = SystemColors.Control;

            SetPrivileges();
            SetDgvUserList();
            SetDgvMatrix();
            SetCombBox();

            this.ucMyGroupTab1.ItemClick += UcMyGroupTab1_ItemClick;
            this.ucMyGroupTab1.ForeColor = WDColors.geekblue6;
            Load += new EventHandler(frmUserManage_Load);

            txtLoginName.valueControl.txtInput.KeyPress += TxtInput_KeyPress;
            txtRealName.valueControl.txtInput.KeyPress += TxtInput_KeyPress;
            txtLoginName1.valueControl.txtInput.KeyPress += TxtInput1_KeyPress;
            txtRealName1.valueControl.txtInput.KeyPress += TxtInput1_KeyPress;
        }

        private void UcMyGroupTab1_ItemClick(WinDoListItem item, MouseEventArgs e)
        {
            ResetDgvMatrixColumns();
            btnQuery1_BtnClick(null, null);
        }

        private void UcListV1_ItemClick(WinDoListItem item, MouseEventArgs e)
        {
            this.tablessControl1.SelectedTab = item.Data as TabPage;
        }

        void LoadUserHomePageConfig()
        {
            //加载所有用户主页配置项
            UserHomePageConfig = MockData.Configs.Where(c => c.Ckey == FormHelper.UserHomePageKey && c.Type == 3).ToList();
        }

        List<Model.Config> UserHomePageConfig = new List<Model.Config>();

        private void DgvMatrix1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            OpenUserDetails(sender as DataGridView, e);
        }

        private void TxtInput1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnQuery1_BtnClick(null, null);
            }
        }
        private void TxtInput_KeyPress(object sender, KeyPressEventArgs e)

        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnQuery_BtnClick(null, null);
            }
        }

        void page1_PageChanged()
        {
            btnQuery_BtnClick(null, null);
        }
        void page2_PageChanged()
        {
            btnQuery1_BtnClick(null, null);
        }

        private void SetCombBox()
        {
            combTitle.valueControl.BoxStyle = ComboBoxStyle.DropDownList;
            combRole.valueControl.BoxStyle = ComboBoxStyle.DropDownList;
            combTitle1.valueControl.BoxStyle = ComboBoxStyle.DropDownList;
            combRole1.valueControl.BoxStyle = ComboBoxStyle.DropDownList;
            FormHelper.BindComboBoxByDic("职务", combTitle.valueControl, true);

            FormHelper.BindComboBoxByDic("角色", combRole.valueControl, true);

            FormHelper.BindComboBoxByDic("职务", combTitle1.valueControl, true);
            FormHelper.BindComboBoxByDic("角色", combRole1.valueControl, true);
        }
        private void SetPrivileges()
        {
            ucMyGroupTab1.Items = PublicRes.lstModule.Where(m => m.IsMenu == 0)
                .Select(p => new WinDoListItem() { Text = p.ModuleName, Code = p.ModuleCode })
                .Distinct().ToList();
        }
        private void Dgv_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0) return;
            DataGridView dgv = sender as DataGridView;
            if (dgv.Columns[e.ColumnIndex].HeaderText == "操作") return;
            if (dgv.Columns[e.ColumnIndex].SortMode == DataGridViewColumnSortMode.Programmatic)
            {
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    if (dgv.Columns[e.ColumnIndex] == col) continue;
                    col.HeaderCell.SortGlyphDirection = SortOrder.None;
                }
                string columnBindingName = dgv.Columns[e.ColumnIndex].DataPropertyName;
                bool sortByValue = (dgv == dgvUserList1 ? (e.ColumnIndex < 9) : (e.ColumnIndex < 4));
                switch (dgv.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection)
                {
                    case System.Windows.Forms.SortOrder.None:
                    case System.Windows.Forms.SortOrder.Ascending:
                        CustomSort(dgv, sortByValue, columnBindingName, 1);
                        dgv.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.Descending;
                        break;
                    case SortOrder.Descending:
                        CustomSort(dgv, sortByValue, columnBindingName, 0);
                        dgv.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.Ascending;
                        break;
                }
            }
        }

        private void CustomSort(DataGridView dgv, bool sortByValue, string dataField, int sort)
        {
            var ls = dgv.DataSource as List<dynamic>;
            if (ls == null) return;
            if (sortByValue)
            {
                dgv.DataSource = sort == 0 ? ls.OrderBy(l => ((IDictionary<string, object>)l)[dataField]).ToList() : ls.OrderByDescending(l => ((IDictionary<string, object>)l)[dataField]).ToList();
            }
            else
            {
                dgv.DataSource = sort == 0 ? ls.OrderBy(l => ((dynamic)((IDictionary<string, object>)l)[dataField]).HasPrivilege).ToList() : ls.OrderByDescending(l => ((dynamic)((IDictionary<string, object>)l)[dataField]).HasPrivilege).ToList();
            }
        }

        private void Dgv_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 4) return;
            var dgv = sender as DataGridView;
            var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (cell != null)
            {
                var rect_width = WinDoControls.Properties.Resources.UnCheckedBox.Width;
                var rect_height = WinDoControls.Properties.Resources.UnCheckedBox.Height;
                var x = (cell.Size.Width - rect_width) / 2;
                var y = (cell.Size.Height - rect_height) / 2;
                var rect = new Rectangle(x, y, rect_width, rect_height);
                if (rect.Contains(e.Location))
                {
                    var dr = dgv.Rows[e.RowIndex].DataBoundItem as IDictionary<string, object>;
                    dynamic o = dr[dgv.Columns[e.ColumnIndex].DataPropertyName];
                    if (o != null)
                    {
                        o.Changed = true;
                        o.HasPrivilege = !o.HasPrivilege;
                    }
                }
            }
        }

        private void Dgv_CellMousve(object sender, DataGridViewCellMouseEventArgs e)
        {
            SetHandelCursor(sender, e);
            if (e.RowIndex < 0 && e.ColumnIndex >= 4)
            {
                var d = sender as DataGridView;
                var tips = d.Columns[e.ColumnIndex].ToolTipText;
                if (!string.IsNullOrWhiteSpace(tips))
                {
                    var cellRect = d.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    var dgvPoint1 = d.Parent.PointToScreen(d.Location);
                    cellRect.Offset(dgvPoint1);
                    if (frmAnchorTips != null && frmAnchorTips.RectControl != cellRect)
                    {
                        frmAnchorTips.Close();
                        frmAnchorTips = null;
                    }
                    if (frmAnchorTips == null)
                        frmAnchorTips = FrmAnchorTips.ShowTips(cellRect, tips, AnchorTipsLocation.BOTTOM, WDColors.TaskListTip, autoCloseTime: 6000);
                }
                return;
            }
            if (e.RowIndex < 0 || e.ColumnIndex < 4) return;
            var dgv = sender as DataGridView;
            var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (cell != null)
            {
                var rect_width = WinDoControls.Properties.Resources.UnCheckedBox.Width;
                var rect_height = WinDoControls.Properties.Resources.UnCheckedBox.Height;
                var x = (cell.Size.Width - rect_width) / 2;
                var y = (cell.Size.Height - rect_height) / 2;
                var rect = new Rectangle(x, y, rect_width, rect_height);
                var dr = dgv.Rows[e.RowIndex].DataBoundItem as IDictionary<string, object>;
                if (rect.Contains(e.Location))
                {
                    dgv.Cursor = Cursors.Hand;
                    var cellRect = dgv.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    var dgvPoint1 = dgv.Parent.PointToScreen(dgv.Location);
                    cellRect.Offset(dgvPoint1);
                    if (frmAnchorTips != null && frmAnchorTips.RectControl != cellRect)
                    {
                        frmAnchorTips.Close();
                        frmAnchorTips = null;
                    }
                    var tips = cell.OwningColumn.ToolTipText;
                    if (frmAnchorTips == null)
                        frmAnchorTips = FrmAnchorTips.ShowTips(cellRect, tips, AnchorTipsLocation.BOTTOM, WDColors.TaskListTip, autoCloseTime: 6000);
                }
                else
                {
                    dgv.Cursor = Cursors.Default;
                }
            }
        }

        private void SetHandelCursor(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var dgv = sender as DataGridView;
            var colHeadText = dgv.Columns[e.ColumnIndex].HeaderText;
            if (IsUserNameColumn(colHeadText))
            {
                var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                var dr = dgv.Rows[e.RowIndex].DataBoundItem as IDictionary<string, object>;
                if (dr == null) return;
                string value = dr[dgv.Columns[e.ColumnIndex].DataPropertyName].ToString();
                var size = TextRenderer.MeasureText(value, dgv.RowsDefaultCellStyle.Font);
                var rect = new Rectangle((cell.Size.Width - size.Width) / 2, (cell.Size.Height - size.Height) / 2, size.Width, size.Height);
                if (rect.Contains(e.Location))
                    dgv.Cursor = Cursors.Hand;
                else
                    dgv.Cursor = Cursors.Default;
            }
        }

        FrmAnchorTips frmAnchorTips;

        private void Dgv_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 4) return;
            e.PaintBackground(e.CellBounds, true);
            var dgv = sender as DataGridView;
            var dr = dgv.Rows[e.RowIndex].DataBoundItem as IDictionary<string, object>;
            var field = dgv.Columns[e.ColumnIndex].DataPropertyName;
            if (!dr.ContainsKey(field))
                return;
            dynamic o = dr[field];
            if (o != null)
            {
                var v = o.HasPrivilege;
                var b = v as bool?;
                var img = (b == null || b == false) ? WinDoControls.Properties.Resources.UnCheckedBox : WinDoControls.Properties.Resources.CheckedBox;
                var p = new Point(e.CellBounds.X + (e.CellBounds.Width - img.Width) / 2, e.CellBounds.Y + (e.CellBounds.Height - img.Height) / 2);
                e.Graphics.DrawImage(img, p);
                e.Handled = true;
            }
        }

        private void Dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            SetCellValue(sender, e, 4);
        }

        private static void SetCellValue(object sender, DataGridViewCellFormattingEventArgs e, int showIdx)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var dgv = sender as DataGridView;
            var dr = dgv.Rows[e.RowIndex].DataBoundItem as IDictionary<string, object>;
            if (e.ColumnIndex < showIdx)
            {
                var v = dr[dgv.Columns[e.ColumnIndex].DataPropertyName];
                if (v != null)
                    e.Value = v.ToString().Trim();
            }
            var colHeadText = dgv.Columns[e.ColumnIndex].HeaderText;
            //if (IsUserNameColumn(colHeadText))
            //    e.CellStyle.ForeColor = WinDo.Utilities.PublicResource.YkdBasisColors.GridLinkColor;
        }

        DataGridViewTextBoxColumn AddCol(DataGridView dgv, string headText, string dataField, string tips)
        {
            var col = new DataGridViewTextBoxColumn();
            col.SortMode = DataGridViewColumnSortMode.Programmatic;
            col.ToolTipText = tips;
            col.Name = "col_" + dataField;
            col.DataPropertyName = dataField;
            col.HeaderText = headText;
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dgv.Columns.Add(col);
            return col;
        }

        private void SetDgvMatrix()
        {
            DataGridViewHelper.SetDefaultStyle(dgvMatrix1, true);
            DataGridViewHelper.SetEmptyText(dgvMatrix1);
            dgvMatrix1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvMatrix1.ScrollBars = ScrollBars.Both;
            dgvMatrix1.CellMouseClick += DgvMatrix1_CellMouseClick;
            dgvMatrix1.CellFormatting += Dgv_CellFormatting;
            dgvMatrix1.CellPainting += Dgv_CellPainting;
            dgvMatrix1.CellMouseMove += Dgv_CellMousve;
            dgvMatrix1.CellMouseClick += Dgv_CellMouseClick;
            dgvMatrix1.CellMouseLeave += Dgv_CellMouseLeave;
        }
        private void Dgv_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (frmAnchorTips != null)
            {
                frmAnchorTips.Close();
                frmAnchorTips = null;
            }
        }
        void ResetDgvMatrixColumns()
        {
            dgvMatrix1.DataSource = null;
            List<DataGridViewColumnEntity> lstCulumns = new List<DataGridViewColumnEntity>();
            //固定四列
            lstCulumns.Add(new DataGridViewColumnEntity() { DataField = "LoginName", HeadText = "用户名", Width = 150, WidthType = SizeType.Absolute, TextAlign = ContentAlignment.MiddleCenter, AllowSort = true });
            lstCulumns.Add(new DataGridViewColumnEntity() { DataField = "RealName", HeadText = "用户姓名", Width = 150, WidthType = SizeType.Absolute, TextAlign = ContentAlignment.MiddleCenter, AllowSort = true });
            lstCulumns.Add(new DataGridViewColumnEntity() { DataField = "Title", HeadText = "职务", Width = 130, WidthType = SizeType.Absolute, TextAlign = ContentAlignment.MiddleCenter, AllowSort = true });
            lstCulumns.Add(new DataGridViewColumnEntity() { DataField = "RoleNameDes", HeadText = "角色", Width = 150, WidthType = SizeType.Absolute, TextAlign = ContentAlignment.MiddleCenter, AllowSort = true });


            var module = this.ucMyGroupTab1.CurrentItem.Code;

            //菜单列
            var menus = PublicRes.lstModule.Where(m => m.RefModuleCode == module && m.IsMenu == 1 && m.ModuleType == 2).Select(m => new { m.ModuleID, m.ModuleCode, m.ModuleName });
            foreach (var m in menus)
            {
                var width = Math.Max(TextRenderer.MeasureText(m.ModuleName, WDFonts.TextFont).Width, 100);
                lstCulumns.Add(new DataGridViewColumnEntity() { DataField = "M_" + m.ModuleCode, HeadText = m.ModuleName, TipText = m.ModuleName + "菜单有权限可见", Width = width + 20, WidthType = SizeType.Absolute, TextAlign = ContentAlignment.MiddleCenter, AllowSort = true });
            }
            //权限列
            var pcols = PublicRes.lstPrivilege.Where(p => p.Module_Code == module).Select(p => new { p.Privilege_Name, p.Privilege_Code, p.Privilege_Note });
            foreach (var p in pcols)
            {
                var width = Math.Max(TextRenderer.MeasureText(p.Privilege_Name, WDFonts.TextFont).Width, 100);
                lstCulumns.Add(new DataGridViewColumnEntity() { DataField = "P_" + p.Privilege_Code, HeadText = p.Privilege_Name, TipText = "权限" + p.Privilege_Name + "的说明", Width = width + 20, WidthType = SizeType.Absolute, TextAlign = ContentAlignment.MiddleCenter, AllowSort = true });
            }
            dgvMatrix1.Columns.Clear();
            foreach (var col in lstCulumns)
            {
                var nc = AddCol(dgvMatrix1, col.HeadText, col.DataField, col.TipText);
                nc.CustomSort((dgv, e, asc) =>
                {
                    var ls = dgv.DataSource as List<dynamic>;
                    if (ls == null) return;
                    string fieldName = dgv.Columns[e.ColumnIndex].DataPropertyName;
                    if (e.ColumnIndex < 4)
                    {
                        dgv.DataSource = asc ? ls.OrderBy(l => ((IDictionary<string, object>)l)[fieldName]).ToList() : ls.OrderByDescending(l => ((IDictionary<string, object>)l)[fieldName]).ToList();
                    }
                    else
                    {
                        dgv.DataSource = asc ? ls.OrderBy(l => ((dynamic)((IDictionary<string, object>)l)[fieldName]).HasPrivilege).ToList() : ls.OrderByDescending(l => ((dynamic)((IDictionary<string, object>)l)[fieldName]).HasPrivilege).ToList();
                    }
                });
            }
            dgvMatrix1.Columns[3].Frozen = true;
            dgvMatrix1.Columns[0].DefaultCellStyle.ForeColor = WDColors.GridLinkColor;
            dgvMatrix1.Columns[1].DefaultCellStyle.ForeColor = WDColors.GridLinkColor;
            var col4 = dgvMatrix1.Columns.Cast<DataGridViewColumn>().Take(4);
            foreach (var item in col4)
            {
                item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                item.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                item.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        private void SetDgvUserList()
        {
            DataGridViewHelper.SetDefaultStyle(dgvUserList1, true);
            DataGridViewHelper.SetEmptyText(dgvUserList1);

            this.dgvUserList1.CellMouseMove += DgvUserList1_CellMouseMove;
            this.dgvUserList1.CellMouseClick += DgvUserList1_CellMouseClick;
            this.dgvUserList1.CellMouseLeave += Dgv_CellMouseLeave;
            var colT = dgvUserList1.AddColumn("用户名", "LoginName")
                        .CellFormatting((dgv, ee) =>
                        {
                            dynamic dr = dgv.Rows[ee.RowIndex].DataBoundItem;
                            string txt = dr.LoginName;
                            ee.Value = txt.AsString("").Trim();
                        });
            colT.DefaultCellStyle.ForeColor = WDColors.GridLinkColor;
            colT = dgvUserList1.AddColumn("用户姓名", "RealName")
                        .CellFormatting((dgv, ee) =>
                        {
                            dynamic dr = dgv.Rows[ee.RowIndex].DataBoundItem;
                            string txt = dr.RealName;
                            ee.Value = txt.AsString("").Trim();
                        });
            colT.DefaultCellStyle.ForeColor = WDColors.GridLinkColor;
            colT.ShowTipsOnOverLength();

            dgvUserList1.AddColumn("职务", "Title");
            dgvUserList1.AddColumn("角色", "RoleNameDes");
            colT.ShowTipsOnOverLength();
            colT.MinimumWidth = 100;
            var colHomePage = dgvUserList1.AddColumn("默认主页", "HomePageDesc").FixColumnWidth(130);
            colHomePage.DefaultCellStyle.ForeColor = WinDo.Utilities.PublicResource.WDColors.GridLinkColor;
            dgvUserList1.AddColumn("状态", "Status");
            SetOperationCol();


            dgvUserList1.SetCellValue(new List<string> { "LoginName", "RealName", "HomePage" });
            foreach (DataGridViewColumn col in dgvUserList1.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.Programmatic;
                //col.ToolTipText = tips;
                if (col.HeaderText == "操作") continue;
                col.CustomSort((dgv, e, asc) =>
                {
                    var ls = dgv.DataSource as List<dynamic>;
                    if (ls == null) return;
                    string fieldName = dgv.Columns[e.ColumnIndex].DataPropertyName;
                    dgv.DataSource = asc ? ls.OrderBy(l => ((IDictionary<string, object>)l)[fieldName]).ToList() : ls.OrderByDescending(l => ((IDictionary<string, object>)l)[fieldName]).ToList();
                });
            }
            dgvUserList1.Columns["col_Status"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            var lastCol = dgvUserList1.Columns[dgvUserList1.Columns.Count - 1];
            lastCol.Width = 360;
            lastCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        }
        /// <summary>
        /// 设置操作列
        /// </summary>
        private void SetOperationCol()
        {
            var colOP = dgvUserList1.AddColumn("操作", "");

            Tuple<string, Size>[] Btns = new Tuple<string, Size>[] {
                new Tuple<string, Size>("授权", new Size(53, 26)),
            new Tuple<string, Size>("批量授权", new Size(78, 26)),
            };
            int space_width = 10;

            colOP.SetOperationButtons((dgv, e) =>
            {

                var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell != null)
                {
                    var all_width = Btns.Sum(b => b.Item2.Width) + space_width * 2;
                    var rect_height = Btns[0].Item2.Height;
                    var x1 = (cell.Size.Width - all_width) / 2;
                    var y1 = (cell.Size.Height - rect_height) / 2;
                    var rect1 = new Rectangle(x1, y1, Btns[0].Item2.Width, rect_height);
                    var rect2 = new Rectangle(x1 + Btns[0].Item2.Width + space_width, y1, Btns[1].Item2.Width, rect_height);
                    var dr = dgv.Rows[e.RowIndex].DataBoundItem;
                    if (rect1.Contains(e.Location))
                    {
                        Operation(Btns[0].Item1, dr);
                    }
                    else if (rect2.Contains(e.Location))
                    {
                        Operation(Btns[1].Item1, dr);
                    }
                }
            }, (dgv, e) =>
            {
                e.PaintBackground(e.CellBounds, true);

                var all_width = Btns.Sum(b => b.Item2.Width) + space_width * 2;
                var rect_height = Btns[0].Item2.Height;
                var x = e.CellBounds.X + (e.CellBounds.Width - all_width) / 2;
                var y = e.CellBounds.Y + (e.CellBounds.Height - rect_height) / 2;
                var container = new Rectangle(x, y, Btns[0].Item2.Width, rect_height);
                ControlHelper.DrawRectFlag(e.Graphics, container, Btns[0].Item2.Width, rect_height, Color.White, WDColors.GrayRectColor, Btns[0].Item1, WDFonts.TextFont, SystemColors.ControlText);
                container.Offset(Btns[0].Item2.Width + space_width, 0);
                container.Width = Btns[1].Item2.Width;
                ControlHelper.DrawRectFlag(e.Graphics, container, Btns[1].Item2.Width, rect_height, Color.White, WDColors.GrayRectColor, Btns[1].Item1, WDFonts.TextFont, SystemColors.ControlText);
                container.Offset(Btns[1].Item2.Width + space_width, 0);
                e.Handled = true;
            }, (dgv, e) =>
             {
                 var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                 if (cell != null)
                 {
                     var all_width = Btns.Sum(b => b.Item2.Width) + space_width * 2;
                     var rect_height = Btns[0].Item2.Height;
                     var x1 = (cell.Size.Width - all_width) / 2;
                     var y1 = (cell.Size.Height - rect_height) / 2;
                     var rect1 = new Rectangle(x1, y1, Btns[0].Item2.Width, rect_height);
                     var rect2 = new Rectangle(x1 + Btns[0].Item2.Width + space_width, y1, Btns[1].Item2.Width, rect_height);
                     var dr = dgv.Rows[e.RowIndex].DataBoundItem;
                     if (rect1.Contains(e.Location) || rect2.Contains(e.Location))
                     {
                         dgv.Cursor = Cursors.Hand;
                     }
                     else
                         dgv.Cursor = Cursors.Default;
                 }
             });

        }

        private static bool IsUserNameColumn(string headText)
        {
            return ("用户名" == headText || "用户姓名" == headText);
        }



        private void DgvUserList1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var dgv = sender as DataGridView;
            var colHeadText = dgv.Columns[e.ColumnIndex].HeaderText;
            if (IsUserNameColumn(colHeadText))
            {
                var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                var dr = dgv.Rows[e.RowIndex].DataBoundItem as IDictionary<string, object>;
                if (dr == null) return;
                string value = dr[dgv.Columns[e.ColumnIndex].DataPropertyName].ToString();
                var size = TextRenderer.MeasureText(value, dgv.RowsDefaultCellStyle.Font);
                var rect = new Rectangle((cell.Size.Width - size.Width) / 2, (cell.Size.Height - size.Height) / 2, size.Width, size.Height);
                if (rect.Contains(e.Location))
                    dgv.Cursor = Cursors.Hand;
                else
                    dgv.Cursor = Cursors.Default;
            }
            else if (colHeadText == "默认主页")
            {
                var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                dynamic dr = dgv.Rows[e.RowIndex].DataBoundItem;
                int userid = dr.UserID;
                var txt = "无";
                var conf = UserHomePageConfig.FirstOrDefault(c => c.KeyOwner == userid.ToString());
                if (conf != null)
                {
                    var menu = PublicRes.lstModule.FirstOrDefault(m => m.IsMenu == 1 && m.ModuleCode == conf.Value);
                    if (menu != null)
                        txt = menu.ModuleName;
                }

                var size = TextRenderer.MeasureText(txt, dgv.RowsDefaultCellStyle.Font);
                var rect = new Rectangle((cell.Size.Width - size.Width) / 2, (cell.Size.Height - size.Height) / 2, size.Width, size.Height);
                if (rect.Contains(e.Location))
                    dgv.Cursor = Cursors.Hand;
                else
                    dgv.Cursor = Cursors.Default;
            }
        }

        private void DgvUserList1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var dgv = sender as DataGridView;
            var col = dgv.Columns[e.ColumnIndex];


            OpenUserDetails(dgv, e);
            OpenUserHomePage(dgv, e);
        }

        private void OpenUserHomePage(DataGridView dgv, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var col = dgv.Columns[e.ColumnIndex];
            if (col.HeaderText != "默认主页") return;
            var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
            dynamic dr = dgv.Rows[e.RowIndex].DataBoundItem;
            int userid = dr.UserID;
            var txt = "无";
            var conf = UserHomePageConfig.FirstOrDefault(c => c.KeyOwner == userid.ToString());
            if (conf != null)
            {
                var menu = PublicRes.lstModule.FirstOrDefault(m => m.IsMenu == 1 && m.ModuleCode == conf.Value);
                if (menu != null)
                    txt = menu.ModuleName;
            }

            var size = TextRenderer.MeasureText(txt, dgv.RowsDefaultCellStyle.Font);
            var rect = new Rectangle((cell.Size.Width - size.Width) / 2, (cell.Size.Height - size.Height) / 2, size.Width, size.Height);
            if (!rect.Contains(e.Location)) return;
            OpenUserHomePage(dr, e.RowIndex);
        }

        private void OpenUserHomePage(dynamic dr, int rowidx)
        {
            var frm = new frmHomePageSetting();
            frm.UserID = dr.UserID;
            frm.ShowDialog(this);
            LoadUserHomePageConfig();
            SetHomePageDesc(dr);
            dgvUserList1.InvalidateRow(rowidx);
        }

        void OpenUserDetails(DataGridView dgv, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var col = dgv.Columns[e.ColumnIndex];
            if (!IsUserNameColumn(col.HeaderText)) return;
            var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
            var dr = dgv.Rows[e.RowIndex].DataBoundItem as IDictionary<string, object>;
            if (dr == null) return;
            string value = dr[dgv.Columns[e.ColumnIndex].DataPropertyName].ToString();
            var size = TextRenderer.MeasureText(value, dgv.RowsDefaultCellStyle.Font);
            var rect = new Rectangle((cell.Size.Width - size.Width) / 2, (cell.Size.Height - size.Height) / 2, size.Width, size.Height);
            if (!rect.Contains(e.Location)) return;
            OpenUserDetails(dr);
        }


        private void Operation(string cmd, dynamic dr)
        {
            switch (cmd)
            {
                case "授权":
                    //创建用户授权
                    var frmUP = new frmUserPrivileges(dr.UserID);
                    frmUP.TopLevel = false;
                    frmUP.Size = this.Size;
                    frmUP.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                    this.Controls.Add(frmUP);
                    //设置用户信息，刷新页面
                    frmUP.Show();
                    frmUP.BringToFront();
                    break;
                case "批量授权":
                    var batchPrivileges = new frmBatchAuthority();
                    batchPrivileges.cUserID = dr.UserID;
                    batchPrivileges.Title = "将【" + dr.RealName + "】的权限批量复制授权给：";
                    if (DialogResult.OK == batchPrivileges.ShowDialog())
                    {
                        try
                        {
                            var userIDs = batchPrivileges.SelectedUserIDs;
                            FrmTips.ShowTipsSuccess(this.MainForm, "批量授权成功");
                        }
                        catch (Exception ex)
                        {
                            FrmTips.ShowTipsError(this.MainForm, "批量授权失败" + ex.Message);
                        }
                    }
                    break;

                case "用户名":
                case "用户姓名":
                    OpenUserDetails(dr);
                    break;
                default:
                    break;
            }
        }

        private void OpenUserDetails(dynamic dr)
        {
            var cPanel = new Panel();
            cPanel.Size = this.Size;
            cPanel.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            this.Controls.Add(cPanel);
            cPanel.BringToFront();
            //创建用户详细信息页面
            var frm = new frmUserDetailInfo();
            frm.UserID = dr.UserID;
            frm.TopLevel = false;
            frm.Size = this.Size;
            frm.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            this.Controls.Add(frm);
            frm.Show();
            frm.FormClosing += new FormClosingEventHandler(frmUserDetailInfo_FormClosing);
            frm.BringToFront();
            cPanel.Visible = false;
            cPanel.Dispose();
        }

        void frmUserDetailInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                ClientCache.Instance.InitUserList();
            });
            btnQuery_BtnClick(null, null);
        }

        void frmUserManage_Load(object sender, EventArgs e)
        {
            ActionHelper.Delay(500).ContinueWith(t =>
            {
                //刷新权限缓存
                ClientCache.Instance.RefreshMenuAndPrivileges();
                this.SafeBeginInvoke((Action)(() =>
                {
                    btnQuery.OnBtnClick(null, null);
                }));
                this.SafeBeginInvoke((Action)(() =>
                {
                    this.ucMyGroupTab1.CurrentItem = this.ucMyGroupTab1.Items[0];
                }));
            });
        }

        private void btnQuery_BtnClick(object sender, EventArgs e)
        {
            dgvUserList1.DataSource = null;
            foreach (DataGridViewColumn col in dgvUserList1.Columns)
            {
                col.HeaderCell.SortGlyphDirection = SortOrder.None;
            }
            ////查询数据库
            //int pageIdx = page1.PageIndex;
            ////获得每页显示的记录数
            //int pageSize = page1.PageSize;
            ////计算显示记录的开始值
            int startIdx = 0;// (pageIdx - 1) * pageSize;
            //计算显示记录的结束值
            int endIdx = 5000;// pageIdx * pageSize;
            //获得从开始值到结束值的记录

            var logName = ControlHelper.GetInputQueryText(txtLoginName.valueControl.InputText);
            var reaName = ControlHelper.GetInputQueryText(txtRealName.valueControl.InputText);
            var title = combTitle.valueControl.SelectedValue;
            var role = combRole.valueControl.SelectedValue;
            Task.Factory.StartNew(() =>
            {
                LoadUserHomePageConfig();
                var lst = MockData.UserManageInfos;
                var ll = lst.OrderByDescending(l => l.IsActive)
                            .ThenBy(l => l.Title_ID)
                            .ThenBy(l => l.LoginName.Trim())
                            .Select(l => l.AsDynamic()).ToList();
                foreach (dynamic dr in ll)
                {
                    SetHomePageDesc(dr);
                }

                this.SafeBeginInvoke((Action)(() =>
                {
                    //page1.TotalCount = totalCount;
                    dgvUserList1.DataSource = ll;
                }));
            }).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    FormHelper.ShowTipsError("加载用户数据失败，" + t.Exception.InnerException.Message);
                    LogHelper.WriteException(t.Exception);
                    return;
                }
            });
        }

        void SetHomePageDesc(dynamic dr)
        {
            var txt = "无";
            int userid = dr.UserID;
            var conf = UserHomePageConfig.FirstOrDefault(c => c.KeyOwner == userid.ToString());
            if (conf != null)
            {
                var menu = PublicRes.lstModule.FirstOrDefault(m => m.IsMenu == 1 && m.ModuleCode == conf.Value);
                if (menu != null)
                    txt = menu.ModuleName;
            }
            dr.HomePageDesc = txt;
        }

        private void btnQuery1_BtnClick(object sender, EventArgs e)
        {
            dgvMatrix1.DataSource = null;
            foreach (DataGridViewColumn col in dgvMatrix1.Columns)
            {
                col.HeaderCell.SortGlyphDirection = SortOrder.None;
            }
            ////查询数据库
            //int pageIdx = page2.PageIndex;
            ////获得每页显示的记录数
            //int pageSize = page2.PageSize;
            //计算显示记录的开始值
            int startIdx = 0;// (pageIdx - 1) * pageSize;
            //计算显示记录的结束值
            int endIdx = 5000;// pageIdx * pageSize;
            //获得从开始值到结束值的记录

            var logName = ControlHelper.GetInputQueryText(txtLoginName1.valueControl.InputText);
            var reaName = ControlHelper.GetInputQueryText(txtRealName1.valueControl.InputText);
            var title = combTitle1.valueControl.SelectedValue;
            var role = combRole1.valueControl.SelectedValue;
            var module = this.ucMyGroupTab1.CurrentItem.Code;
            Task.Factory.StartNew(() =>
            {
                //用户列表
                var users = MockData.UserManageInfos.OrderByDescending(l => l.IsActive).ThenBy(l => l.Title_ID).ThenBy(l => l.LoginName.Trim()).ToList();
                var ups = MockData.UserPrivileges.Where(p => p.Module_Code == module).ToList();
                var mods = MockData.Modules.Where(m => m.RefModuleCode == module).Select(m => m.ModuleID);
                var ums = MockData.UserModules.Where(m => mods.Contains(m.ModuleID));
                //模块菜单
                var menus = PublicRes.lstModule.Where(m => m.RefModuleCode == module).Select(m => new { Privilege_ID = m.ModuleID, m.ModuleCode, Privilege_Note = m.ModuleName + "菜单有权限可见" });
                var dynamicUsers = users.Select(u => u.AsDynamic()).ToList();
                foreach (var m in menus)
                {
                    foreach (var user in dynamicUsers)
                    {
                        var userid = user.UserID;
                        //用户是否有这个权限
                        dynamic checkBoxData = new System.Dynamic.ExpandoObject();
                        checkBoxData.IsMenu = true;
                        checkBoxData.HasPrivilege = ums.Any(um => um.UserID == userid && um.ModuleID == m.Privilege_ID);//用户是否有这个菜单
                        checkBoxData.Privilege_ID = m.Privilege_ID;
                        checkBoxData.Privilege_Note = m.ModuleCode + "权限说明";// m.Privilege_Note;
                        checkBoxData.Changed = false;
                        checkBoxData.UserID = userid;
                        checkBoxData.cuserid = PublicRes.CurUser.UserID;
                        var u = user as IDictionary<string, object>;
                        u["M_" + m.ModuleCode] = checkBoxData;
                    }
                }

                //模块权限列
                var pids = PublicRes.lstPrivilege.Where(p => p.Module_Code == module).Select(p => new { p.Privilege_ID, p.Privilege_Code, p.Privilege_Note });
                foreach (var p in pids)
                {
                    foreach (var user in dynamicUsers)
                    {
                        var userid = user.UserID;
                        //用户是否有这个权限
                        dynamic checkBoxData = new System.Dynamic.ExpandoObject();
                        checkBoxData.IsMenu = false;
                        checkBoxData.HasPrivilege = ups.Exists(up => up.UserID == userid && up.Privilege_ID == p.Privilege_ID);//用户是否有这个权限
                        checkBoxData.Privilege_ID = p.Privilege_ID;
                        checkBoxData.Privilege_Note = p.Privilege_Code + "权限说明";// p.Privilege_Note;
                        checkBoxData.Changed = false;
                        checkBoxData.UserID = userid;
                        checkBoxData.cuserid = PublicRes.CurUser.UserID;
                        var u = user as IDictionary<string, object>;
                        u["P_" + p.Privilege_Code] = checkBoxData;
                    }
                }
                this.SafeBeginInvoke((Action)(() =>
                {
                    //page2.TotalCount = rs.Entity.TotalCount;
                    dgvMatrix1.DataSource = dynamicUsers;
                }));
            }).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    FormHelper.ShowTipsError("加载用户权限失败，" + t.Exception.InnerException.Message);
                    LogHelper.WriteException(t.Exception);
                    return;
                }
            });
        }

        private void btnSave_BtnClick(object sender, EventArgs e)
        {
            if (dgvMatrix1.DataSource == null) return;
            var changedCheckbox = new List<object>();
            //获取点击了的checkbox
            var users = dgvMatrix1.DataSource as List<object>;
            if (users == null) return;
            foreach (var user in users)
            {
                var u = (IDictionary<string, object>)user;
                foreach (var v in u.Values)
                {
                    if (v is System.Dynamic.ExpandoObject && ((IDictionary<string, object>)v)["Changed"].AsBool() == true)
                    {
                        changedCheckbox.Add(v);
                    }
                }
            }
            if (changedCheckbox.Count == 0)
            {
                FrmTips.ShowTipsError(this.MainForm, "数据没有变动");
                return;
            }
            try
            {
                var dels = changedCheckbox;//删除所有变更项，避免主键冲突
                var inss = changedCheckbox.Where(i => ((IDictionary<string, object>)i)["HasPrivilege"].AsBool() == true).ToList();
                //SetUserPrivileges(dels, inss);
                FrmTips.ShowTipsSuccess(this.MainForm, "保存成功");
                //刷新权限缓存
                ClientCache.Instance.RefreshMenuAndPrivileges();
            }
            catch (Exception ex)
            {
                FrmTips.ShowTipsError(this.MainForm, "保存失败" + ex.Message);

            }


        }
    }

}

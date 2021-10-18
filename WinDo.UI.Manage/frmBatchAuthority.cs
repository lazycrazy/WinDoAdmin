using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinDoControls.Forms;
using WinDo.UI.Utilities.DialogForm;
using WinDoControls.Controls;
using WinDo.Utilities;
using WinDo.UI.Utilities;
using System.Threading.Tasks;
using System.Threading;
using WinDo.Utilities.PublicResource;
using WinDoControls;
using WinDo.Utilities.Mock;

namespace WinDo.UI.Manage
{
    public partial class frmBatchAuthority : FrmTitleAnd4Words2Btn
    {
        public frmBatchAuthority()
        {
            InitializeComponent();
            DataGridViewHelper.SetDefaultStyle(dgv2, true);
            DataGridViewHelper.SetEmptyText(dgv2);
            dgv2.SetCellValue();
            ControlHelper.SetControlsDouble(this);
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;
            Load += new EventHandler(frmDiagnosisQuery_Load);
            btnOK.BtnClick += new EventHandler(btnOK_BtnClick);
            btnCancel.BtnClick += new EventHandler(btnCancel_BtnClick);
        }

        protected override void DoEnter()
        {
            btnQuery.OnBtnClick(null, null);
        }

        private void Dgv2_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex != 0) return;
            var dgv = sender as DataGridView;
            var img_width = 10;
            var img_height = 10;
            var cell = e.RowIndex == -1 ? dgv.Columns[e.ColumnIndex].HeaderCell : dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
            var rect = new Rectangle((cell.Size.Width - img_width) / 2, (cell.Size.Height - img_width) / 2, img_width, img_height);
            if (rect.Contains(e.Location))
                this.Cursor = Cursors.Hand;
            else
                this.Cursor = Cursors.Default;
        }

        private void Dgv2_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex != 0) return;
            var dgv = sender as DataGridView;
            var rect_width = 10;
            var rect_height = 10;
            var cell = dgv.Columns[e.ColumnIndex].HeaderCell;
            var x = (cell.Size.Width - rect_width) / 2;
            var y = (cell.Size.Height - rect_height) / 2;
            var rect = new Rectangle(x, y, rect_width, rect_height);
            if (rect.Contains(e.Location))
            {
                var ls = (dgv.DataSource as IEnumerable<dynamic>);
                var all_checked = ls.All(r => r.Checked);
                foreach (var item in ls)
                {
                    item.Checked = !all_checked;
                }
                dgv.InvalidateColumn(e.ColumnIndex);
            }
        }


        private void Dgv_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex != 0 || e.RowIndex < 0) return;
            var dgv = sender as DataGridView;
            var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (cell != null)
            {
                var rect_width = 10;
                var rect_height = 10;
                var x = (cell.Size.Width - rect_width) / 2;
                var y = (cell.Size.Height - rect_height) / 2;
                var rect = new Rectangle(x, y, rect_width, rect_height);
                if (rect.Contains(e.Location))
                {
                    var dr = dgv.Rows[e.RowIndex].DataBoundItem as IDictionary<string, object>;
                    var dataFiled = dgv.Columns[e.ColumnIndex].DataPropertyName;
                    dynamic chk = dr[dataFiled];
                    dr[dataFiled] = !chk;
                    dgv.InvalidateCell(0, -1);
                }
            }
        }

        private void Dgv2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 1) return;
            var dgv = sender as DataGridView;
            var dr = dgv.Rows[e.RowIndex].DataBoundItem as IDictionary<string, object>;
            dynamic o = dr[dgv.Columns[e.ColumnIndex].DataPropertyName];
            e.Value = o;
        }

        private void Dgv2_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex != 0) return;
            var dgv = sender as DataGridView;
            if (e.RowIndex == -1)
            {
                e.PaintBackground(e.CellBounds, true);
                var ls = dgv.DataSource as IEnumerable<dynamic>;
                var all_checked = (ls == null || ls.Count() == 0) ? false : ls.All(r => r.Checked);
                var img_a = !all_checked ? WinDoControls.Properties.Resources.UnCheckedBox : WinDoControls.Properties.Resources.UnCheckedBox;
                var p_a = new Point(e.CellBounds.X + (e.CellBounds.Width - img_a.Width) / 2, e.CellBounds.Y + (e.CellBounds.Height - img_a.Height) / 2);
                e.Graphics.DrawImage(img_a, p_a);
                e.Handled = true;
                return;
            }
            if (e.RowIndex < 0) return;
            e.PaintBackground(e.CellBounds, true);

            var dr = dgv.Rows[e.RowIndex].DataBoundItem as IDictionary<string, object>;
            var v = dr[dgv.Columns[e.ColumnIndex].DataPropertyName];
            var b = v as bool?;
            var img = (b == null || b == false) ? WinDoControls.Properties.Resources.UnCheckedBox : WinDoControls.Properties.Resources.CheckedBox;
            var p = new Point(e.CellBounds.X + (e.CellBounds.Width - img.Width) / 2, e.CellBounds.Y + (e.CellBounds.Height - img.Height) / 2);
            e.Graphics.DrawImage(img, p);
            e.Handled = true;
        }

        public string Title { set { base.lblTitle.Text = value; } }
        public int cUserID { get; set; }

        void btnCancel_BtnClick(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        void btnOK_BtnClick(object sender, EventArgs e)
        {
            if (SelectedUserIDs.Count == 0)
            {
                FrmShadowDialog.ShowWarningDialog(this, "请勾选用户", "请选择", false);
                return;
            }
            if (DialogResult.Cancel == FrmShadowDialog.ShowAskDialog(this, "确定将权限批量复制授权吗？", "确认授权")) return;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        public List<int> SelectedUserIDs
        {
            get
            {
                var rs = new List<int>();
                var ls = dgv2.DataSource as IEnumerable<dynamic>;
                if (ls != null)
                    rs = ls.Where(l => l.Checked).Select(l => (int)l.UserID).ToList();
                return rs;
            }
        }

        void frmDiagnosisQuery_Load(object sender, EventArgs e)
        {

            var col = dgv2.AddColumn("选择", "Checked").FixColumnWidth(40);
            DataGridViewHelper.SetCheckBoxColumn(col, (dgv, rowIndex, colIndex) =>
            {
                if (rowIndex == -1)
                {
                    var ls = dgv.DataSource as IEnumerable<dynamic>;
                    var all_checked = (ls == null || ls.Count() == 0) ? false : ls.All(r => r.Checked);
                    return all_checked;
                }
                var dr = dgv.Rows[rowIndex].DataBoundItem as IDictionary<string, object>;
                var v = dr[dgv.Columns[colIndex].DataPropertyName] as bool?;
                return v.HasValue ? v.Value : false;
            },
                (dgv, rowIndex, colIndex) =>
                {
                    if (rowIndex == -1)
                    {
                        var ls = (dgv.DataSource as IEnumerable<dynamic>);
                        var all_checked = ls.All(r => r.Checked);
                        foreach (var item in ls)
                        {
                            item.Checked = !all_checked;
                        }
                        return;
                    }
                    var dr = dgv.Rows[rowIndex].DataBoundItem as IDictionary<string, object>;
                    var dataFiled = dgv.Columns[colIndex].DataPropertyName;
                    dynamic chk = dr[dataFiled];
                    dr[dataFiled] = !chk;
                });
            dgv2.AddColumn("用户名", "LoginName").FixColumnWidth(150);
            dgv2.AddColumn("用户姓名", "RealName");
            dgv2.AddColumn("角色", "").FixColumnWidth(150);


            combTitle.valueControl.BoxStyle = ComboBoxStyle.DropDownList;
            combRole.valueControl.BoxStyle = ComboBoxStyle.DropDownList;
            FormHelper.BindComboBoxByDic("职务", combTitle.valueControl, true);

            FormHelper.BindComboBoxByDic("角色", combRole.valueControl, true);
            btnQuery_BtnClick(null, null);
        }

        private void btnQuery_BtnClick(object sender, EventArgs e)
        {

            dgv2.DataSource = null;
            //查询 
            var reaName = ControlHelper.GetInputQueryText(txtUserName.valueControl.InputText);
            var title = combTitle.valueControl.SelectedValue;
            var role = combRole.valueControl.SelectedValue;
            Task.Factory.StartNew(() =>
            {
                var rs = MockData.Users.Where(u => u.UserID != cUserID);
                var ls = rs.Select(d =>
                {
                    dynamic r = d;
                    dynamic n = new System.Dynamic.ExpandoObject();
                    n.Checked = false;
                    n.UserID = r.UserID;
                    n.LoginName = r.LoginName == null ? "" : r.LoginName.ToString().Trim();
                    n.RealName = r.RealName;
                    return n;
                }).OrderBy(r => r.LoginName).ToList();

                this.SafeBeginInvoke((Action)(() =>
                {
                    dgv2.DataSource = ls;
                }));
            });
        }




    }
}

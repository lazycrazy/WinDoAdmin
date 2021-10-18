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
using WinDoControls.Forms;
using WinDo.Utilities;
using WinDo.Utilities.PublicResource;
using System.Threading.Tasks;
using WinDoControls;
using WinDo.Utilities.Mock;

namespace WinDo.UI.Manage
{
    public partial class frmUserPrivileges : BaseForm
    {
        int _userID;
        public frmUserPrivileges(int userid)
            : this()
        {
            _userID = userid;
        }
        public frmUserPrivileges()
        {
            InitializeComponent();
            Load += frmUserPrivileges_Load;
            this.ucControlBase1.IsShowRect = false;
            SetDGV();
        }

        private void SetDGV()
        {
            DataGridViewHelper.SetDefaultStyle(this.dataGridView1, true);
            this.dataGridView1.RowTemplate.Height = 120;
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.GridColor = WinDo.Utilities.PublicResource.WDColors.GrayRectColor;
            //网格线
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            dataGridView1.AdvancedCellBorderStyle.All = DataGridViewAdvancedCellBorderStyle.Single;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView1.AdvancedColumnHeadersBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.Single;
            dataGridView1.CellMouseLeave += (dgv1, e) =>
            {
                CloseTips();
            };
            dataGridView1.ColumnHeaderMouseDoubleClick += DataGridView1_ColumnHeaderMouseDoubleClick;
            dataGridView1.MouseWheel += DataGridView1_MouseWheel;

            var col = this.dataGridView1.AddColumn("模块权限", "ModuleName")
                        .CellFormatting((dgv, e) =>
                        {
                            dynamic dr = dgv.Rows[e.RowIndex].DataBoundItem;
                            e.Value = dr.ModuleName;
                        });
            col.FillWeight = 10;
            col = this.dataGridView1.AddColumn("菜单权限", "Menus");
            col.FillWeight = 25;
            col.SetOperationButtons(
                (dgv, e) =>
                    {
                        dynamic dr = dgv.Rows[e.RowIndex].DataBoundItem;
                        //选中
                        foreach (dynamic menu in dr.Menus)
                        {
                            Rectangle rect = menu.ClientRectangle;
                            rect.Height -= 10;
                            rect.Width -= 2;
                            rect.Offset(1, 5);
                            if (rect.Contains(e.Location))
                            {
                                menu.HasPrivilege = menu.HasPrivilege ? false : true;
                                menu.Changed = true;
                                break;
                            }
                        }
                        dgv.InvalidateCell(e.ColumnIndex, e.RowIndex);
                    },
                (dgv, e) =>
                    {
                        dynamic dr = dgv.Rows[e.RowIndex].DataBoundItem;
                        var sf = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
                        foreach (dynamic menu in dr.Menus)
                        {
                            Rectangle rect = menu.ClientRectangle;
                            rect.Offset(e.CellBounds.Location);
                            //e.Graphics.DrawRectangle(Pens.Black, rect);
                            var brush = dgv.Rows[e.RowIndex].Selected ? Brushes.White : Brushes.Black;
                            rect.Offset(22, 0);
                            e.Graphics.DrawString(menu.Menu.ModuleName, WDFonts.TextFont, brush, rect, sf);
                            var img = menu.HasPrivilege ? WinDoControls.Properties.Resources.CheckedBox : WinDoControls.Properties.Resources.UnCheckedBox;
                            var p = rect.Location;
                            p.Offset(-17, (rect.Height - img.Height) / 2);
                            e.Graphics.DrawImage(img, p);
                        }
                    },
                (dgv, e) =>
                    {
                        dynamic dr = dgv.Rows[e.RowIndex].DataBoundItem;

                        var first = (dr.Menus as IEnumerable<dynamic>)
                            .FirstOrDefault(m =>
                            {
                                var rect = ((Rectangle)(m.ClientRectangle));
                                rect.Height -= 10;
                                rect.Width -= 2;
                                rect.Offset(1, 5);
                                return rect.Contains(e.Location);
                            });
                        if (first == null)
                        {
                            dgv.Cursor = Cursors.Default;
                            CloseTips();
                            return;
                        }
                        dgv.Cursor = Cursors.Hand;


                        var cellRect = ((Rectangle)(first.ClientRectangle));
                        var cRect = dgv.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                        cellRect.Offset(cRect.Location);
                        var dgvPoint1 = dgv.Parent.PointToScreen(dgv.Location);
                        cellRect.Offset(dgvPoint1);
                        //cellRect.Offset(((Rectangle)(first.ClientRectangle)).Location);
                        if (_frmAnchorTips != null && _frmAnchorTips.RectControl != cellRect)
                        {
                            CloseTips();
                        }
                        var tips = first.Privilege_Note;
                        if (_frmAnchorTips == null)
                            _frmAnchorTips = FrmAnchorTips.ShowTips(cellRect, tips, AnchorTipsLocation.BOTTOM, WDColors.TaskListTip, autoCloseTime: 6000);


                    });
            col = this.dataGridView1.AddColumn("操作权限", "Privileges");
            col.FillWeight = 65;
            col.SetOperationButtons(
                (dgv, e) =>
                    {
                        dynamic dr = dgv.Rows[e.RowIndex].DataBoundItem;
                        //选中
                        foreach (dynamic privilege in dr.Privileges)
                        {
                            Rectangle rect = privilege.ClientRectangle;
                            rect.Height -= 10;
                            rect.Width -= 2;
                            rect.Offset(1, 5);
                            if (rect.Contains(e.Location))
                            {
                                privilege.HasPrivilege = privilege.HasPrivilege ? false : true;
                                privilege.Changed = true;
                                break;
                            }
                        }
                        dgv.InvalidateCell(e.ColumnIndex, e.RowIndex);
                    },
                (dgv, e) =>
                    {
                        dynamic dr = dgv.Rows[e.RowIndex].DataBoundItem;
                        var sf = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
                        foreach (dynamic privilege in dr.Privileges)
                        {
                            Rectangle rect = privilege.ClientRectangle;
                            rect.Offset(e.CellBounds.Location);
                            //e.Graphics.DrawRectangle(Pens.Black, rect);
                            var brush = dgv.Rows[e.RowIndex].Selected ? Brushes.White : Brushes.Black;
                            rect.Offset(22, 0);
                            e.Graphics.DrawString(privilege.Privilege_Name, WDFonts.TextFont, brush, rect, sf);
                            var img = privilege.HasPrivilege ? WinDoControls.Properties.Resources.CheckedBox : WinDoControls.Properties.Resources.UnCheckedBox;
                            var p = rect.Location;
                            p.Offset(-17, (rect.Height - img.Height) / 2);
                            e.Graphics.DrawImage(img, p);
                        }
                    },
                (dgv, e) =>
                    {
                        dynamic dr = dgv.Rows[e.RowIndex].DataBoundItem;

                        var first = (dr.Privileges as IEnumerable<dynamic>)
                            .FirstOrDefault(m =>
                            {
                                var rect = ((Rectangle)(m.ClientRectangle));
                                rect.Height -= 10;
                                rect.Width -= 2;
                                rect.Offset(1, 5);
                                return rect.Contains(e.Location);
                            });
                        if (first == null)
                        {
                            dgv.Cursor = Cursors.Default;
                            CloseTips();
                            return;
                        }
                        dgv.Cursor = Cursors.Hand;


                        var cellRect = ((Rectangle)(first.ClientRectangle));
                        var cRect = dgv.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                        cellRect.Offset(cRect.Location);
                        var dgvPoint1 = dgv.Parent.PointToScreen(dgv.Location);
                        cellRect.Offset(dgvPoint1);
                        //cellRect.Offset(((Rectangle)(first.ClientRectangle)).Location);
                        if (_frmAnchorTips != null && _frmAnchorTips.RectControl != cellRect)
                        {
                            CloseTips();
                        }
                        var tips = first.Privilege_Note;
                        if (_frmAnchorTips == null)
                            _frmAnchorTips = FrmAnchorTips.ShowTips(cellRect, tips, AnchorTipsLocation.BOTTOM, WDColors.TaskListTip, autoCloseTime: 6000);


                    });
        }

        private void DataGridView1_MouseWheel(object sender, MouseEventArgs e)
        {
            // Hack alert!  Through reflection, we know that the passed
            // in event argument is actually a handled mouse event argument,
            // allowing us to handle this event ourselves.
            // See http://tinyurl.com/54o7lc for more info.
            HandledMouseEventArgs handledE = (HandledMouseEventArgs)e;
            handledE.Handled = true;
            // Do the scrolling manually.  Move just one row at a time.
            int rowIndex = this.dataGridView1.FirstDisplayedScrollingRowIndex;
            if (rowIndex < 0)
                return;
            dataGridView1.FirstDisplayedScrollingRowIndex =
                e.Delta < 0 ?
                    Math.Min(rowIndex + 1, dataGridView1.RowCount - 1) :
                    Math.Max(rowIndex - 1, 0);
        }

        private void DataGridView1_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            if (e.ColumnIndex < 1)
                return;
            if (PublicRes.CurUser.LoginName.Trim().ToLower() != "administrator")
                return;
            var selRows = this.dataGridView1.SelectedRows;
            if (selRows.Count == 0)
                return;
            dynamic dr = selRows[0].DataBoundItem;
            if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "菜单权限")
            {
                bool chk = false;
                if ((dr.Menus as IEnumerable<dynamic>).Any(m => m.HasPrivilege))
                    chk = true;
                foreach (dynamic menu in dr.Menus)
                {
                    menu.HasPrivilege = !chk;
                    menu.Changed = true;
                }
                this.dataGridView1.InvalidateRow(selRows[0].Index);
                return;
            }
            if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "操作权限")
            {
                bool chk = false;
                if ((dr.Privileges as IEnumerable<dynamic>).Any(p => p.HasPrivilege))
                    chk = true;
                foreach (dynamic privilege in dr.Privileges)
                {
                    privilege.HasPrivilege = !chk;
                    privilege.Changed = true;
                }
                this.dataGridView1.InvalidateRow(selRows[0].Index);
                return;
            }
        }

        void CloseTips()
        {
            if (_frmAnchorTips != null)
            {
                _frmAnchorTips.Close();
                _frmAnchorTips = null;
            }
        }
        FrmAnchorTips _frmAnchorTips = null;


        void LoadUser()
        {
            lblUser.Text = "";
            this.dataGridView1.DataSource = null;
            Task.Factory.StartNew(() =>
            {
                dynamic user = MockData.Users.FirstOrDefault(u => u.UserID == _userID);
                if (user == null) return;

                //获取菜单模块列表，获取功能权限列表
                var ums = MockData.UserModules.Where(um => um.UserID == _userID).ToList();
                var ups = MockData.UserPrivileges.Where(up => up.UserID == _userID).ToList();
                var modules = WinDo.Utilities.PublicRes.lstModule.Where(m => m.IsMenu == 0);//0模块
                List<dynamic> rows = new List<dynamic>();
                foreach (var module in modules)
                {
                    dynamic row = new System.Dynamic.ExpandoObject();

                    //用户是否有菜单权限
                    var Menus = new List<dynamic>();
                    var allMenus = WinDo.Utilities.PublicRes.lstModule.Where(m => m.IsMenu == 1 && m.ModuleType == 2 && m.RefModuleCode.Trim().ToLower() == module.ModuleCode.Trim().ToLower()).OrderBy(m => m.SortCode);
                    foreach (var m in allMenus)//模块下的菜单
                    {
                        dynamic menu = new System.Dynamic.ExpandoObject();
                        //menu.Module = module;
                        menu.Menu = m;
                        menu.HasPrivilege = ums.Exists(um => um.ModuleID == m.ModuleID);//用户有这个菜单权限
                        menu.Changed = false;
                        menu.Privilege_Note = string.Format("有权限可见{0}，否则不可见", m.ModuleName.Trim());
                        menu.ModuleName = m.ModuleName.Trim();
                        menu.UserID = _userID;
                        menu.cuserid = WinDo.Utilities.PublicRes.CurUser.UserID;
                        menu.ModuleID = m.ModuleID;
                        Menus.Add(menu);
                    }

                    //用户是否有功能权限
                    var Privileges = new List<dynamic>();
                    var allPrivileges = WinDo.Utilities.PublicRes.lstPrivilege.Where(p => p.Module_Code.Trim().ToLower() == module.ModuleCode.Trim().ToLower()).OrderBy(p => p.Privilege_ID);
                    dynamic yjjhdpg = null; //右键计划待评估 前移动
                    dynamic ycjhbrhz = null; //右键计划待评估 前移动
                    dynamic ycjhbzhz = null; //右键计划待评估 前移动
                    foreach (var p in allPrivileges)//模块下的功能权限
                    {
                        dynamic Privilege = new System.Dynamic.ExpandoObject();
                        //Privilege.Module = module;
                        //Privilege.Privilege = p;
                        Privilege.HasPrivilege = ups.Exists(up => up.Privilege_ID == p.Privilege_ID);//用户有这个功能权限
                        Privilege.Privilege_Note = p.Privilege_Note;
                        Privilege.UserID = _userID;
                        Privilege.Changed = false;
                        Privilege.Privilege_Name = p.Privilege_Name.Trim();
                        Privilege.cuserid = WinDo.Utilities.PublicRes.CurUser.UserID;
                        Privilege.Privilege_ID = p.Privilege_ID;
                        if (p.Privilege_Name == "右键计划待评估")
                        {
                            yjjhdpg = Privilege;
                            continue;
                        }
                        if (p.Privilege_Name == "隐藏/激活本人客户")
                        {
                            ycjhbrhz = Privilege;
                            continue;
                        }
                        if (p.Privilege_Name == "隐藏/激活本组客户")
                        {
                            ycjhbzhz = Privilege;
                            continue;
                        }

                        Privileges.Add(Privilege);
                    }
                    if (yjjhdpg != null)
                    {
                        var preIdx = Privileges.FindIndex(p => p.Privilege_Name == "右键评估通过");
                        if (preIdx > 0)
                            Privileges.Insert(preIdx, yjjhdpg);
                    }
                    if (ycjhbrhz != null)
                    {
                        var preIdx = Privileges.FindIndex(p => p.Privilege_Name == "隐藏/激活所有客户");
                        if (preIdx > 0)
                            Privileges.Insert(preIdx + 1, ycjhbrhz);
                    }
                    if (ycjhbzhz != null)
                    {
                        var preIdx = Privileges.FindIndex(p => p.Privilege_Name == "隐藏/激活本人客户");
                        if (preIdx > 0)
                            Privileges.Insert(preIdx + 1, ycjhbzhz);
                    }
                    row.ModuleName = module.ModuleName.Trim();
                    row.Menus = Menus;
                    row.Privileges = Privileges;

                    rows.Add(row);
                }


                this.SafeBeginInvoke(() =>
                {
                    lblUser.Text = $"用户：{user.LoginName?.Trim() ?? ""}，{ user.RealName?.Trim() ?? ""}";

                    var minHeight = 120;
                    var itemHeight = 30;

                    var colWidth = dataGridView1.Columns["col_Menus"].Width - 5;
                    foreach (var r in rows)
                    {
                        var menus = r.Menus as IEnumerable<dynamic>;
                        r.Height = minHeight;
                        if (menus.Count() == 0)
                            continue;

                        var x = 0;
                        var y = 0;
                        int itemWidth = menus.Max(m => TextRenderer.MeasureText(m.ModuleName.ToString(), WDFonts.TextFont).Width) + 20;
                        var rowHeight = (menus.Count() / (colWidth / itemWidth) + 1) * itemHeight;
                        rowHeight = Math.Max(minHeight, rowHeight);
                        r.Height = rowHeight;
                        foreach (dynamic item in menus)
                        {
                            if (y + itemHeight > rowHeight)
                            {
                                x += itemWidth;
                                y = 0;
                            }
                            item.ClientRectangle = new Rectangle(x, y, itemWidth, itemHeight);
                            y += itemHeight;
                        }
                    }

                    colWidth = dataGridView1.Columns["col_Privileges"].Width - 10;
                    foreach (var r in rows)
                    {
                        var privileges = r.Privileges as IEnumerable<dynamic>;
                        if (privileges.Count() == 0)
                            continue;
                        var x = 0;
                        var y = 0;
                        int itemWidth = privileges.Max(p => TextRenderer.MeasureText(p.Privilege_Name, WDFonts.TextFont).Width) + 20;
                        var rowHeight = (privileges.Count() / (colWidth / itemWidth) + 1) * itemHeight;
                        rowHeight = Math.Max(r.Height, rowHeight);
                        r.Height = rowHeight;
                        foreach (dynamic item in privileges)
                        {
                            if (y + itemHeight > rowHeight)
                            {
                                x += itemWidth;
                                y = 0;
                            }
                            item.ClientRectangle = new Rectangle(x, y, itemWidth, itemHeight);
                            y += itemHeight;
                        }
                    }

                    dataGridView1.DataSource = rows;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        dynamic dr = row.DataBoundItem;
                        row.Height = dr.Height;
                    }
                });
            });
        }
        void frmUserPrivileges_Load(object sender, EventArgs e)
        {
            LoadUser();
        }


        void frmUserPrivileges_SizeChanged(object sender, EventArgs e)
        {
            btnSave.Location = new Point((this.Width - btnSave.Width) / 2 - btnSave.Width, btnSave.Location.Y);
            btnReturn.Location = new Point((this.Width - btnSave.Width) / 2 + btnSave.Width, btnSave.Location.Y);
            panel3.Invalidate();
        }

        private void btnReturn_BtnClick(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_BtnClick(object sender, EventArgs e)
        {

            if (this.dataGridView1.DataSource == null) return;
            var changedCheckboxMenus = new List<dynamic>();
            var changedCheckboxPrivileges = new List<dynamic>();
            //获取点击了的checkbox
            var modules = this.dataGridView1.DataSource as List<dynamic>;
            if (modules == null) return;
            foreach (dynamic module in modules)
            {
                foreach (var menu in module.Menus)
                {
                    if (menu.Changed == true)
                    {
                        changedCheckboxMenus.Add(menu);
                    }
                }
                foreach (var privilege in module.Privileges)
                {
                    if (privilege.Changed == true)
                    {
                        changedCheckboxPrivileges.Add(privilege);
                    }
                }
            }
            if (changedCheckboxMenus.Count == 0 && changedCheckboxPrivileges.Count == 0)
            {
                FrmTips.ShowTipsSuccess(this.MainForm, "操作成功");
                this.Close();
                return;
            }
            try
            {
                //保存数据处理，只保留必要属性
                var insMenus = modules.SelectMany<dynamic, dynamic>(m => m.Menus)
                    .Where(i => i.HasPrivilege == true).Select(i => ((int)i.ModuleID)).ToList();
                var insPrivileges = modules.SelectMany<dynamic, dynamic>(m => m.Privileges)
                    .Where(i => i.HasPrivilege == true).Select(i => (int)i.Privilege_ID).ToList();
                //添加上菜单父节点
                var selectedMenus = modules.SelectMany<dynamic, dynamic>(m => m.Menus)
                    .Where(i => i.HasPrivilege == true).Select(i => i.Menu as Model.ModuleInfo).ToList();
                var menus = new List<int>();
                foreach (var m in selectedMenus)
                {
                    GetParentMenus(menus, m);
                }
                var pModules = menus.Distinct();
                insMenus.AddRange(pModules);
                //保存此用户的菜单权限和功能权限
                //SetUserMenuAndPrivileges(_userID, insMenus, insPrivileges);
                FrmTips.ShowTipsSuccess(this.MainForm, "保存成功");
                //刷新当前用户权限
                ClientCache.Instance.SetLoginUserInfo(WinDo.Utilities.PublicRes.CurUser);
                FormHelper.MainForm.LoadMenuItems();
                //LoadUser();
                this.Close();
            }
            catch (Exception ex)
            {
                FrmTips.ShowTipsError(this.MainForm, "保存失败" + ex.Message);
            }
        }

        private void GetParentMenus(List<int> menus, Model.ModuleInfo m)
        {
            //查找父级module
            var pModule = WinDo.Utilities.PublicRes.lstModule.FirstOrDefault(i => i.ModuleCode == m.ModuleParentCode);
            if (pModule == null) return;
            menus.Add(pModule.ModuleID);
            GetParentMenus(menus, pModule);
        }
    }
}

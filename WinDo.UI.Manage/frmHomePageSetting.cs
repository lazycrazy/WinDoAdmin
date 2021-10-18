using WinDo.UI.Utilities;
using WinDo.UI.Utilities.DialogForm;
using WinDo.Utilities;
using WinDo.Utilities.PublicResource;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinDoControls;
using WinDo.Utilities.Mock;

namespace WinDo.UI.Manage
{
    public partial class frmHomePageSetting : FrmTitleAnd2Btn
    {
        public frmHomePageSetting() : base("默认主页")
        {
            InitializeComponent();
            SetDGV();

            Load += FrmHomePageSetting_Load;
        }

        public int UserID = 0;

        Model.Config HomePageConfig;
        void GetUserHomePageConfig()
        {
            //加载用户主页配置项
            HomePageConfig = MockData.Configs
                .FirstOrDefault(c => c.Ckey == FormHelper.UserHomePageKey && c.Type == 3 && c.KeyOwner == UserID.ToString());
        }

        private void FrmHomePageSetting_Load(object sender, EventArgs e)
        {
            var moduleCode = "";
            if (UserID > 0)
            {
                GetUserHomePageConfig();
            }
            else
            {
                HomePageConfig = PublicRes.lstConfig.FirstOrDefault(c => c.Ckey == FormHelper.ClientHomePageKey);
            }
            if (HomePageConfig != null)
                moduleCode = HomePageConfig.Value;
            BindDGVData(moduleCode);
        }

        protected override bool SaveData()
        {
            try
            {
                //选择的ModuleCode
                var moduleCode = "";
                var drs = this.dataGridView1.DataSource as IEnumerable<dynamic>;
                var selectedRow = drs.FirstOrDefault(r => (r.Menus as IEnumerable<dynamic>).Any(m => m.Checked));
                if (selectedRow != null)
                {
                    var sMenu = (selectedRow.Menus as IEnumerable<dynamic>).First(m => m.Checked);
                    moduleCode = sMenu.Menu.ModuleCode;
                }
                var now = DateTime.Now;
                //保存数据
                if (HomePageConfig != null)
                {
                    HomePageConfig.Value = moduleCode;
                    HomePageConfig.Edit_User = PublicRes.CurUser.UserID.AsString();
                    HomePageConfig.Edit_DtTm = now;
                    HomePageConfig.ConfigWay = 2;
                }
                else
                {
                    if (UserID > 0)
                    {
                        //新增用户主页配置 
                        var nc = new Model.Config();
                        nc.Ckey = FormHelper.UserHomePageKey;
                        nc.Type = 3;
                        nc.KeyOwner = UserID.ToString();
                        nc.Value = moduleCode;

                        nc.Note = "用户默认主页";
                        nc.CGroup = "系统";
                        nc.Create_User = PublicRes.CurUser.UserID.AsString();
                        nc.Create_DtTm = now;
                        nc.Edit_User = nc.Create_User;
                        nc.Edit_DtTm = nc.Create_DtTm;
                        nc.ConfigWay = 2;

                        MockData.Configs.Add(nc);
                    }
                    else
                    {
                        //新增客户端主页配置 
                        var nc = new Model.Config();
                        nc.Ckey = FormHelper.ClientHomePageKey;
                        nc.Type = 2;
                        nc.KeyOwner = WinDo.Utilities.MachineInfoHelper.GetMacAddress();
                        nc.Value = moduleCode;

                        nc.Note = "客户端默认主页";
                        nc.CGroup = "系统";
                        nc.Create_User = PublicRes.CurUser.UserID.AsString();
                        nc.Create_DtTm = now;
                        nc.Edit_User = nc.Create_User;
                        nc.Edit_DtTm = nc.Create_DtTm;
                        nc.ConfigWay = 2;

                        MockData.Configs.Add(nc);
                    }
                }
                MockData.Save(nameof(MockData.Configs));
                FormHelper.ShowTipsSuccess();
            }
            catch (Exception ex)
            {
                FormHelper.ShowTipsError("操作失败,错误" + ex.Message);
                LogHelper.WriteException(ex);
                return false;
            }
            return true;
        }

        private void SetDGV()
        {
            DataGridViewHelper.SetDefaultStyle(this.dataGridView1, true);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AddColumn("模块", "ModuleName")
                .FixColumnWidth(200)
                .CellFormatting((dgv, ee) =>
                {
                    dynamic dr = dgv.Rows[ee.RowIndex].DataBoundItem;
                    ee.Value = dr.ModuleName;
                });
            var col = dataGridView1.AddColumn("子模块", "Menus");


            dataGridView1.CellPainting += DataGridView1_CellPainting;
            dataGridView1.CellMouseClick += DataGridView1_CellMouseClick;
            dataGridView1.CellMouseMove += DataGridView1_CellMouseMove;

        }


        private void BindDGVData(string moduleCode)
        {
            var rowHeight = this.dataGridView1.RowTemplate.Height;
            var modules = WinDo.Utilities.PublicRes.lstModule.Where(m => m.IsMenu == 0).OrderBy(m => m.SortCode);//0模块
            var ls = modules.Where(m => m.ModuleName != "工具").Select(m =>
            {
                var menus = PublicRes.lstModule.Where(me => me.IsMenu == 1 && me.ModuleType == 2 && me.RefModuleCode == m.ModuleCode)
                            .Select(me =>
                            {
                                dynamic o = new System.Dynamic.ExpandoObject();
                                o.Menu = me;
                                o.Checked = me.ModuleCode == moduleCode;
                                return o;
                            }).ToList();

                var colWidth = dataGridView1.Columns["col_Menus"].Width - 10;
                var x = 0;
                var y = 0;
                foreach (dynamic item in menus)
                {
                    var itemWidth = TextRenderer.MeasureText(item.Menu.ModuleName, WDFonts.TextFont).Width + 30;

                    if (x + itemWidth > colWidth)
                    {
                        x = 0;
                        y += rowHeight;
                    }
                    item.ClientRectangle = new Rectangle(x, y, itemWidth, rowHeight);
                    x += itemWidth;
                }

                dynamic no = new System.Dynamic.ExpandoObject();
                no.ModuleName = m.ModuleName;
                no.Module = m;
                no.Menus = menus;
                no.Height = y + rowHeight;
                return no;
            }).ToList();

            this.dataGridView1.DataSource = ls.Where(r => r.Menus.Count > 0).ToList();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                dynamic dr = row.DataBoundItem;
                row.Height = dr.Height;
            }
        }

        private void DataGridView1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            var dgv = sender as DataGridView;
            if (e.RowIndex < 0 || e.ColumnIndex < 1)
            {
                dgv.Cursor = Cursors.Default;
                return;
            }

            var col = dgv.Columns[e.ColumnIndex];
            if (col.DataPropertyName != "Menus")
                return;

            dynamic dr = dgv.Rows[e.RowIndex].DataBoundItem;

            dgv.Cursor = (dr.Menus as IEnumerable<dynamic>)
                .Any(m => ((Rectangle)(m.ClientRectangle)).Contains(e.Location)) ? Cursors.Hand : Cursors.Default;

        }

        private void DataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;
            //单元格，里的某个元素
            var dgv = sender as DataGridView;
            var col = dgv.Columns[e.ColumnIndex];
            if (col.DataPropertyName != "Menus")
                return;

            dynamic dr = dgv.Rows[e.RowIndex].DataBoundItem;
            //选中
            foreach (dynamic menu in dr.Menus)
            {
                Rectangle rect = menu.ClientRectangle;
                if (rect.Contains(e.Location))
                {
                    var drs = dgv.DataSource as IEnumerable<dynamic>;
                    foreach (dynamic row in drs)
                    {
                        foreach (dynamic menuTmp in row.Menus)
                        {
                            if (menu == menuTmp)
                                continue;
                            menuTmp.Checked = false;
                        }
                    }
                    menu.Checked = menu.Checked ? false : true;
                }
            }
            dgv.InvalidateColumn(1);
        }

        private void DataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;
            var dgv = sender as DataGridView;
            var col = dgv.Columns[e.ColumnIndex];
            if (col.DataPropertyName != "Menus")
                return;
            e.PaintBackground(e.CellBounds, true);
            dynamic dr = dgv.Rows[e.RowIndex].DataBoundItem;
            var sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            foreach (dynamic menu in dr.Menus)
            {
                Rectangle rect = menu.ClientRectangle;
                rect.Offset(e.CellBounds.Location);
                //e.Graphics.DrawRectangle(Pens.Black, rect);
                var brush = dgv.Rows[e.RowIndex].Selected ? Brushes.White : Brushes.Black;
                rect.Offset(10, 0);
                e.Graphics.DrawString(menu.Menu.ModuleName, WDFonts.TextFont, brush, rect, sf);
                var img = menu.Checked ? WinDoControls.Properties.Resources.CheckedBox : WinDoControls.Properties.Resources.UnCheckedBox;
                var p = rect.Location;
                p.Offset(-6, (rect.Height - img.Height) / 2);
                e.Graphics.DrawImage(img, p);
            }
            e.Handled = true;
        }
    }
}

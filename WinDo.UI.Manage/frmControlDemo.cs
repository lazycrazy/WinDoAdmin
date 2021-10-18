using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinDo.UI.Utilities;
using WinDo.Utilities;
using WinDo.Utilities.PublicResource;
using WinDoControls;
using WinDoControls.Controls;
using WinDoControls.Controls.Menu;
using WinDoControls.Forms;

namespace WinDo.UI.Manage
{
    public partial class frmControlDemo : BaseForm
    {
        public frmControlDemo()
        {
            InitializeComponent();
            tabFormItem.AutoScroll = true;
            tabTransfer.AutoScroll = true;
            tabDGVEdit.AutoScroll = true;

            //窗体Tab列表
            this.winDoList_11.Items = new List<WinDoListItem>()
            {
                new WinDoListItem(){ Text="表单控件",RelationControl=this.tabFormItem },
                new WinDoListItem(){ Text="自定义表格与排序",RelationControl=this.tabDGV },
                new WinDoListItem(){ Text="动态表格编辑",RelationControl=this.tabDGVEdit},
                new WinDoListItem(){ Text="表格进度条",RelationControl=this.tabFormItem},
                new WinDoListItem(){ Text="穿梭",RelationControl= this.tabTransfer },
                new WinDoListItem(){ Text="树形控件",RelationControl=this.tabTree },
                new WinDoListItem(){ Text="右键菜单",RelationControl=this.tabRightMenu },
                new WinDoListItem(){ Text="树形控件拖放操作",RelationControl=this.tabFormItem },
                new WinDoListItem(){ Text="矩阵树图",RelationControl=this.tabFormItem },
                new WinDoListItem(){ Text="信息丰富的Tips",RelationControl=this.tabFormItem },
                new WinDoListItem(){ Text="拍照与图片裁剪",RelationControl=this.tabFormItem },
                new WinDoListItem(){ Text="画图工具",RelationControl=this.tabFormItem },
                new WinDoListItem(){ Text="日历视图",RelationControl=this.tabFormItem },
                new WinDoListItem(){ Text="表单控件",RelationControl=this.tabFormItem },
                new WinDoListItem(){ Text="表单控件",RelationControl=this.tabFormItem },
                new WinDoListItem(){ Text="表单控件",RelationControl=this.tabFormItem },
                new WinDoListItem(){ Text="表单控件",RelationControl=this.tabFormItem },
                new WinDoListItem(){ Text="表单控件",RelationControl=this.tabFormItem },
                new WinDoListItem(){ Text="表单控件",RelationControl=this.tabFormItem },
                new WinDoListItem(){ Text="表单控件",RelationControl=this.tabFormItem },
                new WinDoListItem(){ Text="表单控件",RelationControl=this.tabFormItem },
                new WinDoListItem(){ Text="表单控件",RelationControl=this.tabFormItem },
                new WinDoListItem(){ Text="表单控件",RelationControl=this.tabFormItem },
                new WinDoListItem(){ Text="表单控件",RelationControl=this.tabFormItem },
                new WinDoListItem(){ Text="表单控件Last",RelationControl=this.tabFormItem },
            };
            winDoList_11.ItemClick += WinDoList_11_ItemClick;
            winDoList_11.Height = winDoList_11.ItemHeight * winDoList_11.Items.Count;
            winDoList_11.CurrentItem = winDoList_11.Items.First();
            this.sundayRXScrollBar1.SmallChange = 10;
            this.sundayRXScrollBar1.ValueChanged += SundayRXScrollBar1_ValueChanged;
            winDoList_11.MouseWheel += winDoList_11_MouseWheel;


            //记录控件列表
            this.ucLabelLabel1.valueControl.Text = "红色必填标记标签";
            SetFormItemControls();

            //右键菜单
            tabRightMenu.MouseClick += TabPage5_MouseClick;

            SetTabDGV();

            //自定义树形控件
            SetTreeView();

            //表单控件
            ucLabelComboxGrid1.valueControl.BoxStyle = ComboBoxStyle.DropDownList;
            this.ucLabelComboxGrid1.valueControl.GridColumns = new List<DataGridViewColumnEntity>() {
                new DataGridViewColumnEntity() { DataField = "Value", HeadText = "用户", Width = ucLabelComboxGrid1.valueControl.Width, WidthType = SizeType.Absolute, TextAlign = ContentAlignment.MiddleLeft }
            };
            this.ucLabelComboxGrid1.valueControl.IsShowHead = false;
            this.ucLabelComboxGrid1.valueControl.TextField = "Value";
            var dls = PublicRes.GetUsersByTitleID(UserTitle.开发).Where(u => (u.RowStatus == 1 && u.IsActive.AsInt() == 1)).Select(d => new KeyValuePair<string, string>(d.UserID.ToString(), d.RealName)).OrderBy(l => l.Value).ToList();
            dls.Insert(0, FormHelper.NullItem);
            ucLabelComboxGrid1.valueControl.GridDataSource = dls;

            //下拉选择框
            ucLabelComboBox1.valueControl.BoxStyle = ComboBoxStyle.DropDownList;
            FormHelper.BindComboBoxByDic("角色", this.ucLabelComboBox1.valueControl, false);

            //下拉多选框            
            this.wdLabelCheckComboxGrid1.valueControl.GridColumns = new List<DataGridViewColumnEntity>() {
            new DataGridViewColumnEntity() { DataField = "Value", HeadText = "全选", Width = wdLabelCheckComboxGrid1.valueControl.Width, WidthType = SizeType.Absolute, TextAlign = ContentAlignment.MiddleLeft }};
            //this.combSuperiorPhysicist.valueControl.IsShowHead = false;
            this.wdLabelCheckComboxGrid1.valueControl.KeyField = "Key";
            this.wdLabelCheckComboxGrid1.valueControl.TextField = "Value";
            var pyls = PublicRes.GetUsersByTitleID(UserTitle.产品).Where(u => (u.RowStatus == 1 && u.IsActive.AsInt() == 1))
               .Select(d => new KeyValuePair<string, string>(d.UserID.ToString(), d.RealName))
               .OrderBy(l => l.Value).ToList();
            //pyls.Insert(0, FormHelper.NullItem);
            this.wdLabelCheckComboxGrid1.valueControl.GridDataSource = pyls.Cast<object>().ToList();

            Load += FrmControlDemo_Load;
        }

        private void FrmControlDemo_Load(object sender, EventArgs e)
        {
            float length = (float)(winDoList_11.Parent).Height / (float)winDoList_11.Height * (float)(winDoList_11.Parent).Height;
            sundayRXScrollBar1.SliderHeight = (int)length;
        }

        VerificationComponent verification = new VerificationComponent();
        FormItems<FormInputItem> formInputItems;
        private void SetFormItemControls()
        {
            formInputItems = new FormItems<FormInputItem>(verification);
            formInputItems.AddRange(new List<FormItem>{
            new FormItem(){ LabelText= ucLabelRadioButton1.LabelText, FieldName= "RadioField", Ctrl= ucLabelRadioButton1,Required=true},
            new FormItem(){ LabelText= ucLabelCheckBox1.LabelText, FieldName= "CheckBoxField", Ctrl= ucLabelCheckBox1,Required=true},
            new FormItem(){ LabelText= ucLabelSwitch1.LabelText, FieldName= "SwitchField", Ctrl= ucLabelSwitch1},
            new FormItem(){ LabelText= ucLabelTextBox1.LabelText, FieldName= "TextField", Ctrl= ucLabelTextBox1,Required=true},
            new FormItem(){ LabelText= ucLabelMultiLineTextBox1.LabelText, Ctrl= ucLabelMultiLineTextBox1},
            new FormItem(){ LabelText= ucLabelSearchBox1.LabelText,  Ctrl= ucLabelSearchBox1},

            new FormItem(){ LabelText= ucLabelTimePicker1.LabelText, Ctrl= ucLabelTimePicker1,Required=true},
            new FormItem(){ LabelText= ucLabelDateTimePicker1.LabelText,   FieldName= "DateField",Required=true, Ctrl= ucLabelDateTimePicker1},//Role_ID ID整型不能字符串直接转换
            new FormItem(){ LabelText= ucLabelComboBox1.LabelText, FieldName= "ComboField", Ctrl= ucLabelComboBox1,Required=true},
            new FormItem(){ LabelText= ucLabelComboxGrid1.LabelText, FieldName= "SearchComboField", Ctrl= ucLabelComboxGrid1,Required=true},
            new FormItem(){ LabelText= wdLabelCheckComboxGrid1.LabelText, Ctrl= wdLabelCheckComboxGrid1,Required=true},
            });
            //注册校验规则
            formInputItems.RegisterMRR();
        }

        /// <summary>
        /// 自定义表格
        /// </summary>
        private void SetTabDGV()
        {
            //使用默认样式
            DataGridViewHelper.SetDefaultStyle(dgvCustom);
            //创建列
            dgvCustom.AddColumn("选择", "Checked")
                .FixColumnWidth(40)
                //checkbox勾选列
                .SetCheckBoxColumn(
                (dgv, rowIndex, colIndex) =>
                {
                    if (rowIndex == -1)
                    {
                        var ls = dgv.DataSource as IList<MM>;
                        return ls.All(r => r.Checked);
                    }
                    return (dgv.Rows[rowIndex].DataBoundItem as MM).Checked;
                },
                (dgv, rowIndex, colIndex) =>
                {
                    if (rowIndex == -1)
                    {
                        var ls = dgv.DataSource as IList<MM>;
                        var all_checked = ls.All(r => r.Checked);

                        foreach (var r in ls)
                        {
                            r.Checked = !all_checked;
                        }
                        return;
                    }
                    var dr = dgv.Rows[rowIndex].DataBoundItem as MM;
                    dr.Checked = !dr.Checked;
                });
            dgvCustom.AddColumn("开关", "Switch")
                .FixColumnWidth(60)
                //开关列
                .SetSwitchColumn(
                (dgv, rowIdx, colIdx) =>
                {
                    if (rowIdx == -1)
                    {
                        var ls = dgv.DataSource as IList<MM>;
                        return ls.All(r => r.Switch);
                    }
                    return (dgv.Rows[rowIdx].DataBoundItem as MM).Switch;
                },
                (dgv, rowIdx, colIdx) =>
                {
                    if (rowIdx == -1)
                    {
                        var ls = dgv.DataSource as IList<MM>;
                        var all_On = ls.All(r => r.Switch);

                        foreach (var r in ls)
                        {
                            r.Switch = !all_On;
                        }
                        return;
                    }
                    var dr = dgv.Rows[rowIdx].DataBoundItem as MM;
                    dr.Switch = !dr.Switch;
                });
            dgvCustom.AddColumn("第一列", "One")
                //超出宽度，自动显示tips
                .ShowTipsOnOverLength();
            dgvCustom.AddColumn("第二列", "Two")
                //固定列宽
                .FixColumnWidth(100);
            dgvCustom.AddColumn("第三列", "Three")
                //超出宽度，自动显示tips
                .ShowTipsOnOverLength();
            dgvCustom.AddColumn("第四列", "Four");
            //所有列使用自定义排序
            foreach (DataGridViewTextBoxColumn col in dgvCustom.Columns)
            {
                if (col.DataPropertyName == "Checked")
                    continue;
                col.CustomSort(sort);
            }
            //绑定数据
            dgvCustom.DataSource = new List<MM> {
                new MM{ Checked=false, Switch=true, One="111111111111111111111111111111111111111111111111",Two="22222222222",Three="33333333333333",Four="111111"},
                new MM{ Checked=true, Switch=false,One="11111111",Two="22222222222",Three="3333333333333333333333333333333333333333",Four="222222222"},
                new MM{ Checked=false, Switch=false,One="11111111",Two="22222222222",Three="33333333333333",Four="333333333333"},
                new MM{ Checked=false, Switch=false,One="11111111",Two="22222222222",Three="33333333333333",Four="44444444444444444444"},
            };
        }
        void sort(DataGridView dgv, DataGridViewCellMouseEventArgs e, bool asc)
        {
            var ls = dgv.DataSource as IList<MM>;
            if (ls == null)
                return;
            string fieldName = dgv.Columns[e.ColumnIndex].DataPropertyName;
            dgv.DataSource = asc ? ls.OrderBy(l => l.GetPropertyValue(fieldName)).ToList()
                                : ls.OrderByDescending(l => l.GetPropertyValue(fieldName)).ToList();
        }
        /// <summary>
        /// 右键菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabPage5_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var ms = new List<WDNode> {
                    new WDNode(){ Key="1",Text="菜单1" },
                    new WDNode(){ Key="1-1",Text="菜单1-1" ,ParentKey="1"},
                    new WDNode(){ Key="1-2",Text="菜单1-2" ,ParentKey="1"},
                    new WDNode(){ Key="1-3",Text="菜单1-3" ,ParentKey="1"},
                    new WDNode(){ Key="1-4",Text="菜单1-4" ,ParentKey="1"},
                    new WDNode(){ Key="2",Text="菜单2" },
                    new WDNode(){ Key="3",Text="菜单3" },
                    new WDNode(){ Key="3-1",Text="菜单3-1" ,ParentKey="3"},
                    new WDNode(){ Key="3-2",Text="菜单3-2" ,ParentKey="3"},
                    new WDNode(){ Key="3-3",Text="菜单3-3" ,ParentKey="3"},
                    new WDNode(){ Key="3-3-1",Text="菜单3-3-3" ,ParentKey="3-3"},
                    new WDNode(){ Key="3-3-2",Text="菜单3-3-3" ,ParentKey="3-3"},
                    new WDNode(){ Key="3-3-3",Text="菜单3-3-3" ,ParentKey="3-3"},
                    new WDNode(){ Key="4",Text="菜单4" },
                    new WDNode(){ Key="5",Text="菜单5" },
                    new WDNode(){ Key="5-1",Text="菜单5-1" ,ParentKey="5"},
                    new WDNode(){ Key="5-2",Text="菜单5-2" ,ParentKey="5"},
                };
                //右键菜单
                var rm = new WDPopupMenu();
                rm.MenuItems = WDNode.GetTree(ms, null, (s, me) =>
                {
                    //菜单项点击处理
                    var m = s as WDMenuItem;
                    FormHelper.ShowTipsSuccess(m.Text);
                });
                rm.ItemPaddingH = 14;
                rm.EnableBorderDrawing = true;
                rm.EnableHoverBorderDrawing = true;
                rm.TextColor = WDColors.WhiteColor;
                rm.HoverTextColor = WDColors.geekblue6;
                rm.Location = this.tabRightMenu.PointToScreen(e.Location);
                rm.Show(this.tabRightMenu);
            }
        }


        private void SundayRXScrollBar2_ValueChanged(object sender, EventArgs e)
        {
            var v = ((float)sundayRXScrollBar2.Value / 100 * (float)(this.dgvTree.PreferredSize.Height - this.dgvTree.Height));
            var rowidx = (int)Math.Ceiling(v / dgvTree.RowTemplate.Height);
            var vrs = dgvTree.Rows.Cast<DataGridViewRow>().Where(r => r.Visible).ToList();
            if (vrs.Count == 0) return;
            if (rowidx < 0)
                rowidx = 0;
            if (vrs.Count < rowidx)
                dgvTree.FirstDisplayedScrollingRowIndex = vrs[vrs.Count - 1].Index;
            else
                dgvTree.FirstDisplayedScrollingRowIndex = vrs[rowidx].Index;
        }
        private void dgvTree_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!sundayRXScrollBar2.Enabled) return;
            if (e.Delta > 0)
            {
                this.sundayRXScrollBar2.Value -= this.sundayRXScrollBar2.SmallChange;
            }
            else
            {
                this.sundayRXScrollBar2.Value += this.sundayRXScrollBar2.SmallChange;
            }
        }
        void SetDgvTreeScrollBarSliderHeight()
        {
            if (dgvTree.PreferredSize.Height > dgvTree.Height)
            {
                var length = Math.Ceiling((double)dgvTree.Height / (double)dgvTree.PreferredSize.Height * (double)dgvTree.Height);
                sundayRXScrollBar2.SliderHeight = (int)length;
                this.sundayRXScrollBar2.Visible = true;
            }
            else
            {
                sundayRXScrollBar2.Value = 0;
                sundayRXScrollBar2.SliderHeight = 0;
                sundayRXScrollBar2.Visible = false;
            }
        }
        /// <summary>
        /// 自定义树形控件
        /// </summary>
        private void SetTreeView()
        {
            UC_DGV.SetTreeStyle(dgvTree, SetDgvTreeScrollBarSliderHeight, (type, data) =>
              {
                  //按钮点击事件处理
                  FormHelper.ShowTipsSuccess(type);
              });
            dgvTree.ScrollBars = ScrollBars.None;
            dgvTree.MouseWheel += this.dgvTree_MouseWheel;
            dgvTree.BackgroundColor = Color.White;
            dgvTree.BorderStyle = BorderStyle.None;
            var lss = new[]
            {
                 new {ParentID="1",ParentText="请选择1",ID= "1-1",IDText="请选择1-1" },
                 new {ParentID="1",ParentText="请选择1",ID= "1-2",IDText="请选择1-2" },
                 new {ParentID="1",ParentText="请选择1",ID= "1-3",IDText="请选择1-3" },
                 new {ParentID="1",ParentText="请选择1",ID= "1-4",IDText="请选择1-4" },

                 new {ParentID="2",ParentText="请选择2",ID= "2-1",IDText="请选择2-1" },
                 new {ParentID="2",ParentText="请选择2",ID= "2-2",IDText="请选择2-2" },
                 new {ParentID="2",ParentText="请选择2",ID= "2-3",IDText="请选择2-3" },
                 new {ParentID="2",ParentText="请选择2",ID= "2-4",IDText="请选择2-4" },
                 new {ParentID="2",ParentText="请选择2",ID= "2-5",IDText="请选择2-5" },

                 new {ParentID="3",ParentText="请选择3",ID= "3-1",IDText="请选择3-1" },
                 new {ParentID="3",ParentText="请选择3",ID= "3-2",IDText="请选择3-2" },
                 new {ParentID="3",ParentText="请选择3",ID= "3-3",IDText="请选择3-3" },
                 new {ParentID="3",ParentText="请选择3",ID= "3-4",IDText="请选择3-4" },
                 new {ParentID="3",ParentText="请选择3",ID= "3-5",IDText="请选择3-5" },
            };
            var parentls = lss.Select(g => new { RowType = 0, ParentID = g.ParentID, ID = g.ParentID, Text = g.ParentText }).Distinct()
                .Select(r => new DgvTreeRow { RowType = r.RowType, ParentID = r.ParentID, ID = r.ID, Text = r.Text })
                .ToList();
            foreach (var item in lss.GroupBy(l => l.ParentID))
            {
                var idx = parentls.FindLastIndex(p => p.ID == item.First().ParentID);
                var subs = item.Select(r => new DgvTreeRow { RowType = 1, ParentID = r.ParentID, ID = r.ID, Text = r.IDText }).ToList();
                parentls.InsertRange(idx + 1, subs);
            }

            this.dgvTree.DataSource = parentls;
            this.sundayRXScrollBar2.ValueChanged += SundayRXScrollBar2_ValueChanged;
            sundayRXScrollBar2.SmallChange = 20;
            SetDgvTreeScrollBarSliderHeight();
        }


        private void winDoList_11_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                this.sundayRXScrollBar1.Value -= this.sundayRXScrollBar1.SmallChange;
            }
            else
            {
                this.sundayRXScrollBar1.Value += this.sundayRXScrollBar1.SmallChange;
            }
        }

        private void SundayRXScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            this.winDoList_11.Top = -Convert.ToInt32((float)sundayRXScrollBar1.Value / 100 * (float)(this.winDoList_11.Height - (winDoList_11.Parent).Height));
        }
        private void WinDoList_11_ItemClick(WinDoControls.Controls.WinDoListItem item, MouseEventArgs e)
        {
            this.tablessControl1.SelectedTab = (item.RelationControl as TabPage);
            LoadForm();
        }
        /// <summary>
        /// 加载窗体
        /// </summary>
        private void LoadForm()
        {
            var tab = this.tablessControl1.SelectedTab;
            if (tab.Controls.Count > 0)
                return;
            if (tab == this.winDoList_11.Items.First(i => i.Text == "右键菜单").RelationControl)
                return;
            FrmBase frm = null;
            if (tab == this.winDoList_11.Items.First(i => i.Text == "穿梭").RelationControl)
            {
                //frm = new frmColumnSetting("500000");
            }
            else if (tab == this.winDoList_11.Items.First(i => i.Text == "动态表格编辑").RelationControl)
            {
                //frm = new frmMachineSetting(BLL.RecordDicCode.R20001);
            }
            if (frm == null)
            {
                FormHelper.ShowTipsError("未配置");
                return;
            }
            frm.IsShowShadowForm = false;
            frm.TopLevel = false;
            frm.Dock = DockStyle.Top;
            //frm.Size = tab.Size;
            tab.Controls.Add(frm);
            frm.Show();
        }

        private void WdBtnImg2WordsYSRed1_BtnClick(object sender, EventArgs e)
        {
            foreach (var item in this.tabFormItem.Controls.Cast<System.Windows.Forms.Control>())
            {
                item.SetPropertyValue("ReadOnly", true);
            }
        }

        private void WdBtnImg2Words1_BtnClick(object sender, EventArgs e)
        {

            foreach (var item in this.tabFormItem.Controls.Cast<System.Windows.Forms.Control>())
            {
                item.SetPropertyValue("ReadOnly", false);
            }
        }

        private void WdBtnImg2WordsYS1_BtnClick(object sender, EventArgs e)
        {
            var isOk = true;
            if (!verification.Verification())
            {
                isOk = false;
            }
            if (this.formInputItems.HasRequiredRadioAndCheckBoxErrMsg())
            {
                isOk = false;
            }
            if (!isOk)
                return;

            //取用户录入值
            var newP = formInputItems.GetNewTByControlValues();
            Console.WriteLine(JsonHelper.ObjectToJson(newP));

            //设置值到控件
            newP.TextField += "新文本";
            newP.ComboField = "5";
            newP.SearchComboField = "5,11,13";
            formInputItems.SetControlValuesByEntity(newP);
        }
    }
    public class FormInputItem
    {
        public string RadioField { get; set; }
        public string CheckBoxField { get; set; }
        public string SwitchField { get; set; }
        public string TextField { get; set; }
        public string TimeField { get; set; }
        public string DateField { get; set; }
        public string ComboField { get; set; }
        public string SearchComboField { get; set; }
    }
    public class MM
    {
        public bool Checked { get; set; }
        public bool Switch { get; set; }

        public string One { get; set; }
        public string Two { get; set; }
        public string Three { get; set; }
        public string Four { get; set; }
    }
}

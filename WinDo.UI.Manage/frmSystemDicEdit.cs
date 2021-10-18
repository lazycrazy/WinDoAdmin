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
using WinDo.Utilities.PublicResource;
using WinDo.Utilities.Mock;

namespace WinDo.UI.Manage
{
    public partial class frmSystemDicEdit : FrmTitleAnd4Words2Btn
    {
        public frmSystemDicEdit()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;
            Load += new EventHandler(frmSystemDicEdit_Load);
            btnOK.BtnClick += new EventHandler(btnOK_BtnClick);
            btnCancel.BtnClick += new EventHandler(btnCancel_BtnClick);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = WDColors.GrayBackColorF7;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersHeight = 40;
            dataGridView1.GridColor = ColorTranslator.FromHtml("#dddddd");//表格内边框
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("微软雅黑", 15, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            dataGridView1.EnableHeadersVisualStyles = false;
            //dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //dataGridView1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
            //设置标题字体



            //普通行
            dataGridView1.AutoGenerateColumns = false;//不允许自动生成列
            dataGridView1.RowTemplate.Height = 40;  //设置行高
            dataGridView1.RowHeadersVisible = false;

            ////隔行变色
            //dataGridView1.RowsDefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255); //Color.Bisque;
            //dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(242, 242, 242);

            //设置行改变颜色
            dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(47, 84, 235);
            //dataGridView1.DefaultcolorSet = Color.FromArgb(209, 244, 244);

            dataGridView1.RowsDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.RowsDefaultCellStyle.Font = new Font("微软雅黑", 10, FontStyle.Regular);




            dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            //禁止自动添加行
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.MultiSelect = false;




            //this.dataGridView1.ColumnHeadersDefaultCellStyle = headerStyle;
            this.dataGridView1.AutoGenerateColumns = false;//是否自动生成列
            this.dataGridView1.AllowUserToResizeRows = false;  //禁止调整行高
        }

        public string Title { set { this.ucLowPanelQuote1.Title = value; } }
        public string UserNote { set { this.textBox1.Text = value; } }
        public string Remark { set { this.ucTextBoxExRemark.InputText = value; } }
        public string cSystemDicCode { get; set; }
        public int SystemGroupDicID { get; set; }


        void btnCancel_BtnClick(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        void btnOK_BtnClick(object sender, EventArgs e)
        {
            try
            {
                //调用接口更新字典组和字典信息
                DialogResult = System.Windows.Forms.DialogResult.OK;

                ClientCache.Instance.InitSystemDicList(); //刷新字典缓存 qxh 20200311

            }
            catch (Exception ex)
            {
                DialogResult = System.Windows.Forms.DialogResult.No;
            }

        }


        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void frmSystemDicEdit_Load(object sender, EventArgs e)
        {
            //标题

           

            this.dataGridView1.DataSource = MockData.SystemDic.Where(d => d.DicGroupCode == cSystemDicCode).ToList();
            this.dataGridView1.ClearSelection();
        }


        /// <summary>
        /// 增加一个空行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucBtnImg0Words4_BtnClick(object sender, EventArgs e)
        {

            var ds = this.dataGridView1.DataSource as List<Model.SystemDic>;
            ds.Add(new Model.SystemDic()
            {
                SysType = cSystemDicCode,
                DicGroupCode = cSystemDicCode,
            });
            this.dataGridView1.DataSource = null;
            this.dataGridView1.DataSource = ds;
            this.dataGridView1.CurrentCell = this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells[0];
        }

        /// <summary>
        /// 删除选中行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucBtnImg0Words1_BtnClick(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count <= 0 || this.dataGridView1.CurrentRow == null)
            {
                FrmTips.ShowTipsError(this, "请选中一条记录");
                //MessageBox.Show("请选择一条记录");
                return;
            }
            else
            {
                var selectRow = this.dataGridView1.SelectedRows[0];
                if (DialogResult.Cancel == FrmShadowDialog.ShowAskDialog(this, "确定要删除选中行吗?", "确认删除"))
                    return;
                {
                    var ds = this.dataGridView1.DataSource as List<Model.SystemDic>;
                    ds.Remove(selectRow.DataBoundItem as Model.SystemDic);
                    this.dataGridView1.DataSource = null;
                    this.dataGridView1.DataSource = ds;
                }
            }
        }

        /// <summary>
        /// 窗体激活事件
        /// 解决dataGridView默认会有一个选中行的问题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSystemDicEdit_Activated(object sender, EventArgs e)
        {
            //this.dataGridView1.CurrentCell = null;
        }


        /// <summary>
        /// 上移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucBtnImg0Words3_BtnClick(object sender, EventArgs e)
        {
             

        }
        /// <summary>
        /// 下移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucBtnImg0Words2_BtnClick(object sender, EventArgs e)
        {
             
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinDo;

namespace WinDoControls.Controls
{
    /// <summary>
    /// Class UCComboxGridPanel.
    /// Implements the <see cref="System.Windows.Forms.UserControl" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.UserControl" />
    [ToolboxItem(false)]
    public partial class WDCheckComboxGridPanel : UserControl
    {
        /// <summary>
        /// 项点击事件
        /// </summary>
        [Description("项点击事件"), Category("自定义")]
        public event DataGridViewEventHandler ItemClick;

        /// <summary>
        /// The m data source
        /// </summary>
        private List<object> m_dataSource = null;
        /// <summary>
        /// 数据源
        /// </summary>
        /// <value>The data source.</value>
        [Description("数据源"), Category("自定义")]
        public List<object> DataSource
        {
            get { return m_dataSource; }
            set
            {
                m_dataSource = value;
                CheckedRows.Clear();
                this.dataGridView1.DataSource = m_dataSource;
            }
        }



        /// <summary>
        /// The string last search text
        /// </summary>
        private string strLastSearchText = string.Empty;
        /// <summary>
        /// The m page
        /// </summary>
        //UCPagerControl m_page = new UCPagerControl();

        /// <summary>
        /// Initializes a new instance of the <see cref="WDComboxGridPanel" /> class.
        /// </summary>
        public WDCheckComboxGridPanel()
        {
            InitializeComponent();

            DataGridViewHelper.SetDefaultStyle(DGV, true);
            var col = DGV.AddColumn("", "Checked").FixColumnWidth(40);
            DataGridViewHelper.SetCheckBoxColumn(col, (dgv, rowIndex, colIndex) =>
             {
                 if (rowIndex == -1)
                 {
                     var ls = dgv.DataSource as IEnumerable<dynamic>;
                     var all_checked = (ls == null || ls.Count() == 0) ? false : ls.Count() == CheckedRows.Count;
                     return all_checked;
                 }
                 return CheckedRows.Contains(dgv.Rows[rowIndex].DataBoundItem);
             },
            (dgv, rowIndex, colIndex) =>
            {
                if (rowIndex == -1)
                {
                    var ls = (dgv.DataSource as IEnumerable<dynamic>);
                    var all_checked = ls.Count() > 0 && ls.Count() == CheckedRows.Count;
                    CheckedRows.Clear();
                    if (all_checked)
                    {
                        return;
                    }
                    foreach (var item in ls)
                    {
                        CheckedRows.Add(item);
                    }
                    return;
                }
                var dr = dgv.Rows[rowIndex].DataBoundItem;
                if (CheckedRows.Contains(dr))
                {
                    CheckedRows.Remove(dr);
                }
                else
                {
                    CheckedRows.Add(dr);
                }
            });
            col = DGV.AddColumn("全选", "Value");
            col.ColumnAligment();
            col.DefaultCellStyle.Padding = new Padding(2, 0, 0, 0);
            DGV.Dock = DockStyle.Fill;
            DGV.ScrollBars = ScrollBars.Vertical;
            DGV.AllowUserToAddRows = false;
            DGV.BackgroundColor = Color.White;
            DGV.CellMouseClick += ucDataGridView1_ItemClick;
        }

        public List<object> CheckedRows = new List<object>();
        public DataGridView DGV => this.dataGridView1;


        /// <summary>
        /// Handles the ItemClick event of the ucDataGridView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewEventArgs" /> instance containing the event data.</param>
        void ucDataGridView1_ItemClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;
            var dgv = sender as DataGridView;
            if (ItemClick != null)
            {
                ItemClick(dgv.Rows[e.RowIndex].DataBoundItem, null);
            }
        }

    }
}

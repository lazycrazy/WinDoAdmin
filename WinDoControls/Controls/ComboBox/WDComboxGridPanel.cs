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
    public partial class WDComboxGridPanel : UserControl
    {
        /// <summary>
        /// 项点击事件
        /// </summary>
        [Description("项点击事件"), Category("自定义")]
        public event DataGridViewEventHandler ItemClick;

        public DataGridView DGV { get { return this.dataGridView1; } }
        private bool isShowHead = false;

        public bool IsShowHead
        {
            get { return isShowHead; }
            set
            {
                isShowHead = value;
                dataGridView1.ColumnHeadersVisible = isShowHead;
            }
        }


        /// <summary>
        /// The m data source
        /// </summary>
        private List<KeyValuePair<string, string>> m_dataSource = null;
        /// <summary>
        /// 数据源
        /// </summary>
        /// <value>The data source.</value>
        [Description("数据源"), Category("自定义")]
        public List<KeyValuePair<string, string>> DataSource
        {
            get { return m_dataSource; }
            set { m_dataSource = value; }
        }

        /// <summary>
        /// The string last search text
        /// </summary>
        private string strLastSearchText = string.Empty;
        /// <summary>
        /// The m page
        /// </summary>


        /// <summary>
        /// Initializes a new instance of the <see cref="WDComboxGridPanel" /> class.
        /// </summary>
        public WDComboxGridPanel()
        {
            InitializeComponent();
            DataGridViewHelper.SetDefaultStyle(DGV, true);
            var col = DataGridViewHelper.AddColumn(DGV, "", "Value");
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            col.DefaultCellStyle.Padding = new Padding(2, 0, 0, 0);
            DGV.Dock = DockStyle.Fill;
            DGV.ScrollBars = ScrollBars.Vertical;
            DGV.ColumnHeadersVisible = false;
            DGV.AllowUserToAddRows = false;
            DGV.BackgroundColor = Color.White;
            this.txtSearch.txtInput.TextChanged += txtInput_TextChanged;
            this.dataGridView1.CellMouseClick += ucDataGridView1_ItemClick;
        }
        public int SearchBoxHeight
        {
            get { return this.txtSearch.Height; }
            set
            {
                this.txtSearch.Height = value;
            }
        }
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

        /// <summary>
        /// Handles the TextChanged event of the txtInput control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        void txtInput_TextChanged(object sender, EventArgs e)
        {
            Search(txtSearch.InputText);
        }

        /// <summary>
        /// Handles the Load event of the UCComboxGridPanel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void UCComboxGridPanel_Load(object sender, EventArgs e)
        {
            ClearSearchInput();
        }



        public void ClearSearchInput()
        {
            txtSearch.InputText = "";
            Search("");
        }

        /// <summary>
        /// Searches the specified string text.
        /// </summary>
        /// <param name="strText">The string text.</param>
        private void Search(string strText)
        {
            if (!string.IsNullOrEmpty(strText))
            {
                strText = strText.ToLower().Trim();
                if (m_dataSource != null)
                    DGV.DataSource = m_dataSource.Where(r => r.Value.ToLower().Contains(strText)).ToList();
                else
                    DGV.DataSource = null;
            }
            else
            {
                if (m_dataSource != null)
                    DGV.DataSource = m_dataSource.Select(r => r).ToList();
                else
                    DGV.DataSource = null;
            }
        }
    }


    public delegate void DataGridViewEventHandler(object sender, EventArgs e);

}

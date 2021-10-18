using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinDoControls.Controls;
using WinDo;

namespace WinDoControls.Controls
{
    /// <summary>
    /// Class UCComboxGrid.
    /// Implements the <see cref="WinDoControls.Controls.WDCombox" />
    /// </summary>
    /// <seealso cref="WinDoControls.Controls.WDCombox" />
    public partial class WDComboxGrid : WDCombox
    {
        /// <summary>
        /// The int width
        /// </summary>
        protected int intWidth = 0;

        /// <summary>
        /// The m data source
        /// </summary>
        protected List<KeyValuePair<string, string>> m_dataSource = null;
        /// <summary>
        /// 表格数据源
        /// </summary>
        /// <value>The grid data source.</value>
        [Description("表格数据源"), Category("自定义")]
        public List<KeyValuePair<string, string>> GridDataSource
        {
            get { return m_dataSource; }
            set { m_dataSource = value; }
        }
        public List<DataGridViewColumnEntity> GridColumns { get; set; }
        /// <summary>
        /// The m text field
        /// </summary>
        private string m_textField;
        /// <summary>
        /// 显示值字段名称
        /// </summary>
        /// <value>The text field.</value>
        [Description("显示值字段名称"), Category("自定义")]
        public string TextField
        {
            get { return m_textField; }
            set
            {
                m_textField = value;
                SetText();
            }
        }
        /// <summary>
        /// 控件样式
        /// </summary>
        /// <value>The box style.</value>
        //[Obsolete("不再可用的属性")]
        //[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        //private new ComboBoxStyle BoxStyle
        //{
        //    get;
        //    set;
        //}
        /// <summary>
        /// The select source
        /// </summary>
        private object selectSource = null;
        /// <summary>
        /// 选中的数据源
        /// </summary>
        /// <value>The select source.</value>
        [Description("选中的数据源"), Category("自定义")]
        public object SelectSource
        {
            get { return selectSource; }
            set
            {
                selectSource = value;
                SetText();
                if (SelectedChangedEvent != null)
                {
                    SelectedChangedEvent(value, null);
                }
            }
        }

        public new string SelectedText
        {
            get
            {
                if (selectSource == null) return null;
                var kv = ((KeyValuePair<string, string>)selectSource);
                return string.IsNullOrWhiteSpace(kv.Key) ? null : kv.Value;
            }
        }

        public new string SelectedValue
        {
            get
            {
                if (selectSource == null) return null;
                var kv = ((KeyValuePair<string, string>)selectSource);
                return string.IsNullOrWhiteSpace(kv.Key) ? null : kv.Key;
            }
            set
            {
                if (GridDataSource == null || string.IsNullOrWhiteSpace(value)) { SelectSource = null; return; }
                var exists = GridDataSource.Exists(d => d.Key == value);
                if (exists)
                {
                    SelectSource = GridDataSource.FirstOrDefault(r => r.Key == value);
                }
                else
                {
                    SelectSource = null;
                }
            }
        }

        public override object GetTextByValue(object value)
        {
            if (value == null || m_dataSource == null || m_dataSource.Count <= 0) return value;

            if (m_dataSource.Exists(kv => !string.IsNullOrWhiteSpace(kv.Key) && kv.Key == value.ToString()))
                return m_dataSource.First(kv => kv.Key == value.ToString()).Value;
            return value;
        }

        public override object GetValueByText(object Text)
        {
            if (Text == null || m_dataSource == null || m_dataSource.Count <= 0) return Text;

            if (m_dataSource.Exists(kv => !string.IsNullOrWhiteSpace(kv.Value) && kv.Value == Text.ToString()))
                return m_dataSource.First(kv => kv.Value == Text.ToString()).Key;
            return Text;
        }

        /// <summary>
        /// 选中数据源改变事件
        /// </summary>
        [Description("选中数据源改变事件"), Category("自定义")]
        public new event EventHandler SelectedChangedEvent;
        /// <summary>
        /// Initializes a new instance of the <see cref="WDComboxGrid" /> class.
        /// </summary>
        public WDComboxGrid()
        {
            InitializeComponent();
            //base.BoxStyle = ComboBoxStyle.DropDownList;
            //txtInput.LostFocus += TxtInput_LostFocus;
        }

        private void TxtInput_LostFocus(object sender, EventArgs e)
        {
            if (_frmAnchor != null && !_frmAnchor.IsDisposed && _frmAnchor.Visible)
                _frmAnchor.Close();
            _frmAnchor = null;
        }

        public bool IsShowHead { get; set; }


        /// <summary>
        /// The m uc panel
        /// </summary>
        protected WDComboxGridPanel m_ucPanel = null;
        /// <summary>
        /// The FRM anchor
        /// </summary>
        protected Forms.FrmAnchor _frmAnchor;
        /// <summary>
        /// Handles the MouseDown event of the click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs" /> instance containing the event data.</param>
        protected override void click_MouseDown(object sender, MouseEventArgs e)
        {
            this.Focus();
            if (m_ucPanel == null)
            {
                var p = this.Parent.PointToScreen(this.Location);
                int intScreenHeight = Screen.FromControl(this).WorkingArea.Height;
                int intHeight = Math.Max(p.Y, intScreenHeight - p.Y - this.Height);
                intHeight -= 100;
                m_ucPanel = new WDComboxGridPanel();
                //m_ucPanel.IsShowHead = IsShowHead;
                m_ucPanel.ItemClick += m_ucPanel_ItemClick;
                m_ucPanel.Height = intHeight;
                m_ucPanel.Width = this.Width;
                if (m_dataSource != null && m_dataSource.Count > 0)
                {
                    int _intHeight = Math.Min(115 + Math.Min(m_dataSource.Count, 10) * m_ucPanel.DGV.RowTemplate.Height, m_ucPanel.Height);
                    if (_intHeight <= 0)
                        _intHeight = 115;
                    m_ucPanel.Height = _intHeight;
                }
            }
            if (_dropPanelWidth > 0)
            {
                m_ucPanel.Width = _dropPanelWidth;
            }
            if (_dropPanelHeight > 0)
            {
                m_ucPanel.Height = _dropPanelHeight;
            }
            m_ucPanel.DataSource = m_dataSource;

            if (_frmAnchor == null || _frmAnchor.IsDisposed)
            {
                _frmAnchor = new Forms.FrmAnchor(this, m_ucPanel, isNotFocus: false);
            }
            //Console.WriteLine(_frmAnchor.Visible);
            if (_frmAnchor.Visible)
            {
                _frmAnchor.Hide();
            }
            else
            {
                m_ucPanel.ClearSearchInput();
                _frmAnchor.Show(this.FindForm());
            }
            if (BoxStyle != ComboBoxStyle.DropDownList)
                txtInput.Focus();
        }

        /// <summary>
        /// Handles the ItemClick event of the m_ucPanel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewEventArgs" /> instance containing the event data.</param>
        protected void m_ucPanel_ItemClick(object sender, EventArgs e)
        {
            _frmAnchor.Hide();
            SelectSource = sender;
            m_ucPanel.ClearSearchInput();
        }

        /// <summary>
        /// Sets the text.
        /// </summary>
        private void SetText()
        {
            if (!string.IsNullOrEmpty(m_textField) && selectSource != null)
            {
                var pro = selectSource.GetType().GetProperty(m_textField);
                if (pro != null)
                {
                    TextValue = pro.GetValue(selectSource, null).ToStringExt();
                }
            }
            else
            {
                TextValue = "";
            }
        }
    }



    public class UCComboxGridMini : WDComboxGrid
    {
        public UCComboxGridMini()
        {
            this.SuspendLayout();

            txtInput.PromptText = "请选择";
            txtInput.Size = new Size(361, 19);
            panel1.Size = new Size(16, 26);
            panel1.Location = new Point(366, 3);
            txtInput.MouseHover += TxtInput_MouseHover;
            txtInput.MouseLeave += TxtInput_MouseLeave;
            this.ResumeLayout();
        }
        protected override void click_MouseDown(object sender, MouseEventArgs e)
        {
            if (readOnly)
                return;
            base.click_MouseDown(sender, e);
        }
        private bool readOnly = false;
        public bool ReadOnly
        {
            get { return readOnly; }
            set
            {
                readOnly = value;
                txtInput.ReadOnly = value;
            }
        }

        private void TxtInput_MouseLeave(object sender, EventArgs e)
        {
            CloseTips();
        }

        Forms.FrmAnchorTips _frmAnchorTips;
        void CloseTips()
        {
            if (_frmAnchorTips != null)
            {
                _frmAnchorTips.Close();
                _frmAnchorTips = null;
            }
        }
        private void TxtInput_MouseHover(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInput.Text))
            {
                CloseTips();
                return;
            }
            var tw = TextRenderer.MeasureText(txtInput.Text, txtInput.Font).Width;
            if (tw <= this.Width - 8)
            {
                CloseTips();
                return;
            }
            var tips = txtInput.Text;
            if (_frmAnchorTips == null)
                _frmAnchorTips = Forms.FrmAnchorTips.ShowTips(this, tips, Forms.AnchorTipsLocation.BOTTOM, WinDo.Utilities.PublicResource.WDColors.TaskListTip, autoCloseTime: 6000);
        }

        public string UPromptText
        {
            set { txtInput.PromptText = value; }
        }
    }
}

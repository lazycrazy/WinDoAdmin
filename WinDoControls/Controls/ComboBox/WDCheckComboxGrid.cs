using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinDoControls.Controls;
using WinDo.Utilities;
using WinDo;

namespace WinDoControls.Controls
{
    /// <summary>
    /// Class UCComboxGrid.
    /// Implements the <see cref="WinDoControls.Controls.WDCombox" />
    /// </summary>
    /// <seealso cref="WinDoControls.Controls.WDCombox" />
    public partial class WDCheckComboxGrid : WDCombox
    {

        /// <summary>
        /// The int width
        /// </summary>
        int intWidth = 0;
        private List<object> m_dataSource = null;
        /// <summary>
        /// 表格数据源
        /// </summary>
        /// <value>The grid data source.</value>
        [Description("表格数据源"), Category("自定义")]
        public List<object> GridDataSource
        {
            get { return m_dataSource; }
            set
            {
                m_dataSource = value;
                SelectKeyValues = null;
            }
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

        private string m_KeyField;
        /// <summary>
        /// 显示值字段名称
        /// </summary>
        /// <value>The text field.</value>
        [Description("主键值字段名称"), Category("自定义")]
        public string KeyField
        {
            get { return m_KeyField; }
            set
            {
                m_KeyField = value;
            }
        }

        /// <summary>
        /// 控件样式
        /// </summary>
        /// <value>The box style.</value>
        [Obsolete("不再可用的属性")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        private new ComboBoxStyle BoxStyle
        {
            get;
            set;
        }
        /// <summary>
        /// The select source
        /// </summary>
        private List<string> selectKeyValues = null;
        /// <summary>
        /// 选中的数据源
        /// </summary>
        /// <value>The select source.</value>
        [Description("选中的数据源"), Category("自定义")]
        public List<string> SelectKeyValues
        {
            get { return selectKeyValues; }
            set
            {
                selectKeyValues = value;
                SetText();
                if (SelectedChangedEvent != null)
                {
                    SelectedChangedEvent(value, null);
                }
            }
        }

        public override object GetTextByValue(object value)
        {
            if (value == null || m_dataSource == null || m_dataSource.Count <= 0) return value;
            var keys = value.ToString().Split(',').Select(k => k.Trim());
            return m_dataSource.Where(s => keys.Contains(s.GetPropertyValue(KeyField).AsString())).CommaSeparate(s => s.GetPropertyValue(TextField));
        }


        /// <summary>
        /// 选中数据源改变事件
        /// </summary>
        [Description("选中数据源改变事件"), Category("自定义")]
        public new event EventHandler SelectedChangedEvent;
        /// <summary>
        /// Initializes a new instance of the <see cref="WDComboxGrid" /> class.
        /// </summary>
        public WDCheckComboxGrid()
        {
            InitializeComponent();
            base.BoxStyle = ComboBoxStyle.DropDownList;
            //txtInput.LostFocus += TxtInput_LostFocus;
            this.MouseHover += TxtInput_MouseHover;
            this.MouseLeave += TxtInput_MouseLeave;
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
        private void TxtInput_LostFocus(object sender, EventArgs e)
        {
            if (_frmAnchor == null) return;
            _frmAnchor.Close();
            _frmAnchor = null;
        }

        /// <summary>
        /// The m uc panel
        /// </summary>
        WDCheckComboxGridPanel m_ucPanel = null;
        /// <summary>
        /// The FRM anchor
        /// </summary>
        Forms.FrmAnchor _frmAnchor;
        /// <summary>
        /// Handles the MouseDown event of the click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs" /> instance containing the event data.</param>
        protected override void click_MouseDown(object sender, MouseEventArgs e)
        {
            CloseTips();
            if (_frmAnchor == null || _frmAnchor.IsDisposed || !_frmAnchor.Visible)
            {
                var p = this.Parent.PointToScreen(this.Location);
                int intScreenHeight = Screen.FromControl(this).WorkingArea.Height;
                int intHeight = Math.Max(p.Y, intScreenHeight - p.Y - this.Height);
                intHeight -= 100;
                m_ucPanel = new WDCheckComboxGridPanel();
                //m_ucPanel.ItemClick += m_ucPanel_ItemClick;

                m_ucPanel.Height = Math.Min(300, intHeight);
                m_ucPanel.Width = this.Width;
                if (m_dataSource != null && m_dataSource.Count > 0)
                {
                    int _intHeight = Math.Min(110 + m_dataSource.Count * 36, m_ucPanel.Height);
                    if (_intHeight <= 0)
                        _intHeight = 100;
                    m_ucPanel.Height = _intHeight;
                }
                m_ucPanel.DataSource = m_dataSource;

                _frmAnchor = new Forms.FrmAnchor(this, m_ucPanel, isNotFocus: false);
                _frmAnchor.HideClose = true;
                _frmAnchor.FormClosed += _frmAnchor_FormClosed;
                _frmAnchor.Show(this.FindForm());

                if (selectKeyValues == null)
                {
                }
                else
                {
                    //设置勾选行
                    foreach (var item in m_dataSource)
                    {
                        var k = item as KeyValuePair<string, string>?;
                        if (selectKeyValues.Contains(k.Value.Key))
                            m_ucPanel.CheckedRows.Add(item);
                    }
                }
            }
            else
            {
                if (_frmAnchor != null && !_frmAnchor.IsDisposed)
                    _frmAnchor.Close();
            }
        }

        private void _frmAnchor_FormClosed(object sender, FormClosedEventArgs e)
        {
            SetCheckedValue();
        }

        void SetCheckedValue()
        {
            if (_frmAnchor != null && !_frmAnchor.IsDisposed)
            {
                if (m_ucPanel.CheckedRows.Count == 0)
                {
                    SelectKeyValues = null;
                    return;
                }
                //获取勾选了的行
                var checkedRows = m_ucPanel.CheckedRows.Cast<KeyValuePair<string, string>>().Select(kv => kv.Key).ToList();
                SelectKeyValues = checkedRows;
            }
        }


        /// <summary>
        /// Handles the ItemClick event of the m_ucPanel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewEventArgs" /> instance containing the event data.</param>
        void m_ucPanel_ItemClick(object sender, EventArgs e)
        {
            //_frmAnchor.Hide();
            //SelectSource = sender;
        }

        /// <summary>
        /// Sets the text.
        /// </summary>
        private void SetText()
        {
            if (GridDataSource == null || selectKeyValues == null)
            {
                TextValue = null;
                return;
            }
            var selectedRows = GridDataSource.Where(r => selectKeyValues.Contains(r.GetType().GetProperty(KeyField).GetValue(r, null).ToStringExt()));
            TextValue = string.Join(",", selectedRows.Select(r => r.GetType().GetProperty(TextField).GetValue(r, null).ToStringExt()));
        }
    }
}

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

namespace WinDo.UI.Manage
{
    public partial class frmConfigEdit : FrmTitleAnd4Words2Btn
    {
        public frmConfigEdit()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Normal;
            Load += new EventHandler(frmSystemDicEdit_Load);
            btnOK.BtnClick += new EventHandler(btnOK_BtnClick);
            btnCancel.BtnClick += new EventHandler(btnCancel_BtnClick);
            this.ucComboxValues.SelectedChangedEvent += ucComboxValues_SelectedChangedEvent;
            ProcessDoEnter = false;
        }
        private DataTable dtDataGriadView = new DataTable();
        //标题
        public string Title { set { this.ucLowPanelQuote1.Title = value; } }
        //文本值
        public string Value { set { this.ucTextBoxExValue.InputText = value; } }
        //配置类型
        public int KeyValueType { get; set; }
        //配置说明
        public string Note
        {
            set
            {
                SizeF sizeTexti = TextRenderer.MeasureText(value, WinDo.Utilities.PublicResource.WDFonts.TextFont);
                //最大是1151
                //最小是
                if ((int)sizeTexti.Width >= 800)
                {
                    int pianyi = ((int)sizeTexti.Width - 800) / 4;
                    //int pianyi = 80;
                    this.Height = 573 + pianyi;  //窗体
                    ucControlBase1.Height = 467 + pianyi;//最底层的controlBase
                    ucControlBase2.Height = 443 + pianyi;//白底
                    panel2.Height = 231 + pianyi;

                    ucLabelSysNoteValue.Height = 132 + pianyi;//容器
                    ucTextBoxExRemark.Top = 144 + pianyi;
                }
                this.ucLabelSysNoteValue.Text = value;
            }
        }
        //备注
        public string UserNote { set { this.ucTextBoxExRemark.InputText = value; } }
        public int cConfigID { get; set; }

        public List<KeyValuePair<string, string>> ComboxValues { get; set; }


        void btnCancel_BtnClick(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        void btnOK_BtnClick(object sender, EventArgs e)
        {
                if (this.ucTextBoxExValue.InputText == null || this.ucTextBoxExValue.InputText == "")
                {
                    FrmTips.ShowTipsError(this, "配置值不能为空");
                    return;
                }

                DialogResult = System.Windows.Forms.DialogResult.OK;
        }


        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void frmSystemDicEdit_Load(object sender, EventArgs e)
        {
            if (KeyValueType == 1)
            {
                this.ucTextBoxExValue.Visible = true;
            }
            else
            {
                this.ucComboxValues.Visible = true;
                ucComboxValues.BoxStyle = ComboBoxStyle.DropDownList;
                this.ucControlBase2.Height = 451;
                //ComboxValues.Insert(0, FormHelper.NullItem);
                ucComboxValues.Source = ComboxValues;
                int selectIndex = 0;
                if (this.ucTextBoxExValue.InputText != "")
                {
                    for (int i = 0; i < ComboxValues.Count; i++)
                    {
                        if (ComboxValues[i].Value == this.ucTextBoxExValue.InputText)
                        {
                            selectIndex = i;
                        }
                    }
                    ucComboxValues.SelectedIndex = selectIndex;
                }

            }
        }


        private void ucComboxValues_SelectedChangedEvent(object sender, EventArgs e)
        {
            this.ucTextBoxExValue.InputText = ucComboxValues.SelectedText;
        }
    }


}

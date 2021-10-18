using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinDo.Utilities;
using WinDoControls.Controls;
using WinDo.Utilities.PublicResource;

namespace WinDoControls.Controls
{
    public partial class WDProblem : Panel
    {
        public WDProblem()
        {
            InitializeComponent();
            this.Width = 700;
            this.Height = 50;
            //panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            var ctrA = new WDBtnImg0Words();
            ctrA.Location = new Point(653, 8);
            ctrA.IconName = "I_plus";
            ctrA.BtnClick += new EventHandler(OnAddBtnClick);
            ctrA.Visible = PublicRes.HasPrivilege(PrivilegesEnum.任务编辑);
            txtBox = new WDLabelSearchBox();
            txtBox.label.TextValue = "Problem";
            txtBox.Width = 569;
            txtBox.Height = 36;
            if (!PublicRes.HasPrivilege(PrivilegesEnum.任务编辑))
                txtBox.valueControl.SetSearchButtonCursor(Cursors.No);
            txtBox.Location = new Point(32, 6);
            txtBox.Tag = ctrA;
            txtBox.valueControl.BtnClick += new EventHandler(OnSelectProblemBtnClick);
            txtBox.valueControl.EnableInput = false;
            txtBox.IsRequired = false;

            var ctrD = new WDBtnImg0Words();
            ctrD.Location = new Point(613, 8);
            ctrD.IconName = "I_trash";
            ctrD.Tag = this;
            ctrD.BtnClick += new EventHandler(OnDeleteBtnClick);
            ctrD.Visible = PublicRes.HasPrivilege(PrivilegesEnum.任务编辑);

            this.Controls.Add(txtBox);
            this.Controls.Add(ctrD);
            this.Controls.Add(ctrA);
        }
        private TransparentPanel maskPanel = null;
        private bool _readOnly = false;
        public bool ReadOnly
        {
            get { return _readOnly; }
            set
            {
                _readOnly = value;
                this.TabStop = !_readOnly;

                if (!DesignMode)
                {
                    if (_readOnly && maskPanel == null)
                    {
                        maskPanel = new TransparentPanel();
                        maskPanel.TabStop = false;
                        maskPanel.Size = this.Size;
                        maskPanel.Cursor = Cursors.No;
                        //maskPanel.BackColor = Color.Transparent;
                        //maskPanel.Parent = this;
                        maskPanel.Visible = false;
                        maskPanel.Dock = DockStyle.None;
                        //maskPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                        this.Controls.Add(maskPanel);
                        this.SizeChanged += (sender, e) => { maskPanel.Size = this.Size; };

                        //maskPanel.Location = this.Location;
                    }
                    if (maskPanel != null)
                        if (_readOnly)
                        {
                            maskPanel.Visible = true;
                            maskPanel.BringToFront();
                        }
                        else
                        {
                            maskPanel.Visible = false;
                        }
                }
            }
        }
        WDLabelSearchBox txtBox;
        public object SelectedObj { get; set; }


        void OnSelectProblemBtnClick(object sender, EventArgs e)
        {
            if (!PublicRes.HasPrivilege(PrivilegesEnum.任务编辑)) return;
            if (SelectProblemBtnClick != null)
                SelectProblemBtnClick(this, e);
        }
        void OnDeleteBtnClick(object sender, EventArgs e)
        {
            if (DeleteBtnClick != null)
                DeleteBtnClick(this, e);
        }
        void OnAddBtnClick(object sender, EventArgs e)
        {
            if (AddBtnClick != null)
                AddBtnClick(this, e);
        }

        public string Text
        {
            get
            {
                return txtBox.valueControl.InputText;
            }
            set
            {
                txtBox.valueControl.InputText = value;
            }
        }


        public event EventHandler AddBtnClick;
        public event EventHandler DeleteBtnClick;
        public event EventHandler SelectProblemBtnClick;

    }
}

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
using WinDo.Utilities.PublicResource;
using WinDoControls;

using WinDo.UI.Utilities;
using WinDo.Utilities;
using System.Net;
using System.Management;
using System.Net.Sockets;
using WinDo.Model;
using System.IO;
using Newtonsoft.Json;

namespace WinDo.UI.Manage
{
    public partial class frmClientConfig : FrmTitleAnd2Btn
    {
        WorkStationInfo model;
        List<Config> list;
        public bool HadRegister = false;
        public frmClientConfig(bool IsLoad = false)
            : base("客户端配置")
        {
            InitializeComponent();
            Clear();
            HadRegister = HadRegisterVerify();
            verification.AnchorLocation = AnchorTipsLocation.BOTTOM;
            verification.TextAlignment = StringAlignment.Center;
            verification.Verificationed += new VerificationComponent.VerificationedHandle(verification_Verificationed);
            verification.SetVerificationModel(this.txtPhone.valueControl, WinDoControls.Controls.VerificationModel.Phone);
            verification.SetVerificationModel(this.txtExitTime.valueControl, WinDoControls.Controls.VerificationModel.PositiveIntegerNumber);
            verification.SetVerificationModel(this.txtName.valueControl, WinDoControls.Controls.VerificationModel.None);
            verification.SetVerificationRequired(this.txtName.valueControl, true, "必填项不能为空");
            verification.SetVerificationRequired(this.txtExitTime.valueControl, true, "必填项不能为空");
            //verification.SetVerificationModel(ucTextBoxClear1, WD_Controls.Controls.VerificationModel.Phone);
            //verification.SetVerificationRequired(ucTextBoxClear1, true, "请输入手机号");

            //ControlHelper.FreezeControl(this, true);

            txtName.valueControl.TextChanged += new EventHandler(valueControl_TextChanged);
            txtExitTime.valueControl.TextChanged += new EventHandler(valueControl_TextChanged2);
            txtName.valueControl.txtInput.MaxLength = 20;
            txtUserName.valueControl.txtInput.MaxLength = 20;
            txtPhone.valueControl.txtInput.MaxLength = 20;
            txtExitTime.valueControl.BackColor = Color.White;
            txtName.valueControl.BackColor = Color.White;
            //this.BackColor = DoBasisColors.MainColor1;
            //base.SetTitleBackColor(DoBasisColors.MainColor1);
            //base.SetTitleColor(DoBasisColors.MainColor);
            base.btnOK.IconName = "I_save";
            if (model != null && IsLoad)
            {
                StartPosition = FormStartPosition.Manual;
                this.Location = new Point(FormHelper.MainForm.Right - this.Width - 2, 74);
            }
            //ControlHelper.SetControlsDouble(this);

            lblHomePage.valueControl.Cursor = Cursors.Hand;
            lblHomePage.valueControl.ForeColor = WDColors.GridLinkColor;
            lblHomePage.valueControl.Click += ValueControl_Click;
            ControlHelper.SetControlsDouble(this);
            Load += new EventHandler(frmClientConfig_Load);
        }

        private void ValueControl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lblHomePage.valueControl.Text))
                return;
            var frm = new frmHomePageSetting();
            frm.ShowDialog(this);
            ClientCache.Instance.InitConfigList();
            SetHomePage();
        }

        void txtInput_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Tab)
            //    base.bt.Focus();
        }

        void valueControl_TextChanged(object sender, EventArgs e)
        {
            txtName.valueControl.IsErrorColor = string.IsNullOrWhiteSpace(txtName.valueControl.InputText);
        }
        void valueControl_TextChanged2(object sender, EventArgs e)
        {
            txtExitTime.valueControl.IsErrorColor = string.IsNullOrWhiteSpace(txtExitTime.valueControl.InputText);
        }
        public bool HadRegisterVerify()
        {
            //调用接口判断是否已注册
            //if (model == null)
            //{
            return false;
            //}
            //else
            //    return true;
        }
        public static bool Exists()
        {
            return WinDo.Utilities.MachineInfoHelper.GetMacAddress().Length > 0;
        }

        VerificationComponent verification = new VerificationComponent();

        void Clear()
        {
            lblIP.valueControl.Text = "";
            lblMAC.valueControl.Text = "";
            lblversion.valueControl.Text = "";
            lbltime.valueControl.Text = "";
            txtName.valueControl.InputText = "";
            txtExitTime.valueControl.InputText = "";
            txtPhone.valueControl.InputText = "";
            txtUserName.valueControl.InputText = "";
            lblHomePage.valueControl.Text = "";
        }
        void ShowControls()
        {
            lblIP.Visible = true;
            lblMAC.Visible = true;
            lblversion.Visible = true;
            lbltime.Visible = true;
            txtName.Visible = true;
            txtExitTime.Visible = true;
            txtPhone.Visible = true;
            txtUserName.Visible = true;
            lblHomePage.Visible = true;
        }
        public static string GetConfig(List<Config> list, string Ckey, string DefaultValue = default(string))
        {
            string strValue = DefaultValue;
            if (list.Exists(x => x.Ckey == Ckey))
            {
                strValue = list.Find(x => x.Ckey == Ckey).Value;
            }
            return strValue;
        }
        void frmClientConfig_Load(object sender, EventArgs e)
        {
            //ShowControls();
            //IP，MAC
            lblIP.valueControl.Text = GetLocalIP();
            string macAddress = WinDo.Utilities.MachineInfoHelper.GetMacAddress();
            lblMAC.valueControl.Text = macAddress;
            //model = new BLL.WorkStationInfo().GetModelList("MACAddress='" + macAddress + "'").FirstOrDefault();
            list = WinDo.Utilities.Mock.MockData.Configs.Where(c => c.Type == 2 && c.KeyOwner == macAddress && (c.Ckey == "C_SCAN_MODE" || c.Ckey == "C_SYSTEM_LOGTIMEOUT")).ToList();
            string C_SCAN_MODE = GetConfig(list, "C_SCAN_MODE", "0");//扫描枪模式
            string C_SYSTEM_LOGTIMEOUT = GetConfig(list, "C_SYSTEM_LOGTIMEOUT", "");//客户端自动退出时间
            //客户端自动退出时间，扫描枪模式
            txtExitTime.valueControl.InputText = C_SYSTEM_LOGTIMEOUT;
            rdoModel.SetSelectValue(C_SCAN_MODE);
            string StartPath = System.Windows.Forms.Application.StartupPath;
            dynamic Version = JsonConvert.DeserializeObject(GetVersion(StartPath));
            if (model != null)
            {
                this.lbltime.valueControl.Text = Version != null ? Convert.ToDateTime(Version.UpdateDate).ToString("yyyy-MM-dd HH:mm") : "";// Convert.ToDateTime(model.UserField2).ToString("yyyy-MM-dd HH:mm");
                txtName.valueControl.InputText = model.WorkStationName.ToString();
                txtUserName.valueControl.InputText = model.Users.ToString();
                txtPhone.valueControl.InputText = model.Phone.ToString();
                lblversion.valueControl.Text = Version != null ? Version.Version : "";
                if (WinDo.Utilities.PublicRes.HasPrivilege(PrivilegesEnum.客户端配置) == false)
                {
                    txtName.valueControl.Enabled = false;
                    txtExitTime.valueControl.Enabled = false;
                    rdoModel.Enabled = false;
                    txtExitTime.valueControl.BackColor = Color.Gray;
                    txtName.valueControl.BackColor = Color.Gray;
                }
                else
                {
                    txtName.valueControl.Enabled = true;
                    txtExitTime.valueControl.Enabled = true;
                    rdoModel.Enabled = true;
                }
                SetHomePage();
            }
            else
            {
                //新增客户端信息
                model = new WorkStationInfo();
                model.MACAddress = lblMAC.valueControl.Text.Trim();
                model.IPAddress = lblIP.valueControl.Text.Trim();
                model.UserField2 = Version != null ? Version.UpdateDate : "";
                model.UserField1 = Version != null ? Version.Version : "";
                HadRegister = true;
                lbltime.valueControl.Text = Version != null ? Version.UpdateDate : "";
                lblversion.valueControl.Text = Version != null ? Version.Version : "";

                txtName.valueControl.Enabled = true;
                txtExitTime.valueControl.Enabled = true;
                rdoModel.Enabled = true;
            }
        }

        private void SetHomePage()
        {
            var moduleCode = PublicRes.GetConfig(FormHelper.ClientHomePageKey, "");
            if (string.IsNullOrWhiteSpace(moduleCode))
                moduleCode = "无";
            else
            {
                var menu = PublicRes.lstModule.FirstOrDefault(m => m.IsMenu == 1 && m.ModuleCode == moduleCode);
                if (menu != null)
                    moduleCode = menu.ModuleName;
            }

            lblHomePage.valueControl.Text = moduleCode;
        }

        string GetVersion(string path)
        {
            System.IO.DirectoryInfo dic = new System.IO.DirectoryInfo(path);

            foreach (FileInfo info in dic.GetFiles())
            {
                if (info.Name.ToLower() == "version.json")
                {
                    return System.IO.File.ReadAllLines(info.FullName)[0];
                }
            }
            return "";
        }
        void verification_Verificationed(VerificationEventArgs e)
        {
            var ctrl = e.VerificationControl as WDTextBoxClear;
            if (ctrl != null)
                ctrl.IsErrorColor = !e.IsVerifySuccess;
        }




        protected override bool SaveData()
        {
            if (!verification.Verification()) return false;
            //调用接口保存客户端配置


            FrmTips.ShowTipsSuccess(FormHelper.MainForm, "已保存");

            //更新到数据库和缓存
            return true;
        }
        /// 获取客户端IP地址（无视代理）
        /// </summary>
        /// <returns>若失败则返回回送地址</returns>
        private string GetLocalIP()
        {
            string ip = "0.0.0.0";
            try
            {
                string hostname = Dns.GetHostName();
                IPAddress[] ipadrlist = Dns.GetHostAddresses(hostname);
                foreach (IPAddress ipadr in ipadrlist)
                {
                    if (ipadr.AddressFamily == AddressFamily.InterNetwork)
                    {//判断是否IPv4
                        ip = ipadr.ToString();
                        break;
                    }
                }
                return ip;
            }
            catch
            {
                return ip;
            }
        }
    }
}

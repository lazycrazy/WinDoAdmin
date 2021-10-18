using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Windows.Forms;
using MIP.UI.Utilities;
using MIP.UI.Utilities.Controls;
using MIP.Utilities;
using YKD_Controls.Controls;

namespace MIP.UI.Main
{
    public partial class frmClientLoginManage : BaseFormWithTitle
    {

        VerificationComponent verification = new VerificationComponent();
        public bool HadRegister = false;
        private string ip = string.Empty;
        private string mac = string.Empty;
        private Model.WorkStationInfo workStationInfo;
        public frmClientLoginManage()
        {
            InitializeComponent();
            BuildIPMac();//获取ip、mac
            HadRegister = HadRegisterVerify();
        }



        private void frmClientLoginManage_Load(object sender, EventArgs e)
        {
            //ucLabelIP.label.Controls[2].Font = new Font("微软雅黑", 10, FontStyle.Bold);
            //ucLabelMAC.label.Controls[2].Font = new Font("微软雅黑", 10, FontStyle.Bold);

            ucTxtDep.label.Controls[2].Font = new Font(ucTxtDep.Font, FontStyle.Bold);
            ucTxtWorkStationName.label.Controls[2].Font = new Font(ucTxtDep.Font, FontStyle.Bold);
            ucTxtWorkStationName.label.Controls[2].Font = new Font(ucTxtDep.Font, FontStyle.Bold);
            ucTxtWorkStationAddress.label.Controls[2].Font = new Font(ucTxtDep.Font, FontStyle.Bold);
            ucTxtTel.label.Controls[2].Font = new Font(ucTxtDep.Font, FontStyle.Bold);
            ucTxtUser.label.Controls[2].Font = new Font(ucTxtDep.Font, FontStyle.Bold);

  

            verification.Verificationed += new VerificationComponent.VerificationedHandle(verification_Verificationed);
            verification.SetVerificationModel(this.ucTxtDep.valueControl, YKD_Controls.Controls.VerificationModel.None);
            verification.SetVerificationRequired(this.ucTxtDep.valueControl, true, "必填项不能为空");

            verification.SetVerificationModel(this.ucTxtWorkStationName.valueControl, YKD_Controls.Controls.VerificationModel.None);
            verification.SetVerificationRequired(this.ucTxtWorkStationName.valueControl, true, "必填项不能为空");


            LoadIPCtrValue();// 加载ip ，mac 地址

        }
        void verification_Verificationed(VerificationEventArgs e)
        {
            var ctrl = e.VerificationControl as UCTextBoxClear;
            if (ctrl != null)
                ctrl.IsErrorColor = !e.IsVerifySuccess;
        }

        /// <summary>
        /// 返回按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucBtnBack_BtnClick(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_BtnClick(object sender, EventArgs e)
        {
            // 调用验证方法
            if (!verification.Verification())
            {
                return;
            }

            Model.WorkStationInfo model = new Model.WorkStationInfo();
            model.IPAddress = ucLabelIP.valueControl.Text;
            model.MACAddress = ucLabelMAC.valueControl.Text;
            model.Department = ucTxtDep.valueControl.InputText;
            model.WorkStationName = ucTxtWorkStationName.valueControl.InputText;
            model.Address = ucTxtWorkStationName.valueControl.InputText;
            model.Phone = ucTxtTel.valueControl.InputText;
            model.Users = ucTxtUser.valueControl.InputText;

            MIP.BLL.WorkStationInfo bll = new BLL.WorkStationInfo();
            int saveResult = bll.Add(model);
            if (saveResult>0)
            {
                FormHelper.ShowTipsSuccess("保存成功！");
                HadRegister = true;
                this.Close();
            }
            else
            {
                FormHelper.ShowTipsError("保存失败！");
            }
        }

        /// <summary>
        /// 加载设备ip mac 地址
        /// </summary>
        private void LoadIPCtrValue()
        {
            ucLabelIP.valueControl.Text = ip;
            ucLabelMAC.valueControl.Text = mac;

        }

        /// <summary>
        /// 获取客户端ip、mac 信息
        /// </summary>
        private void BuildIPMac()
        {
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection nics = mc.GetInstances();
            foreach (ManagementObject nic in nics)
            {
                if (Convert.ToBoolean(nic["ipEnabled"]) == true)
                {
                    mac = nic["MacAddress"].ToString();//Mac地址
                    ip = (nic["IPAddress"] as String[])[0];//IP地址

                }
            }
        }

        /// <summary>
        /// 判断客户端是否已经注册
        /// </summary>
        /// <returns></returns>
        public bool HadRegisterVerify()
        {
            MIP.BLL.WorkStationInfo bll = new BLL.WorkStationInfo();
            workStationInfo = bll.GetModelByIPMac(ip, mac);
            if (workStationInfo == null)
            {
                return false;
            }
            else
                return true;

        }
    }



}

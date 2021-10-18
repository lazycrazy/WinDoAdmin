using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinDoControls.Controls;
using WinDoControls.Forms;
using WinDo.UI.Utilities;
using WinDo.UI.Utilities.DialogForm;

using WinDo.Utilities;
using System.Threading.Tasks;
using System.Threading;
using WinDoControls;
using WinDo.Utilities.PublicResource;

namespace WinDo.UI.Call
{
    //[ToolboxItem(false)]
    public partial class frmQueueNumber : BaseForm
    {
        public frmQueueNumber()
        {

            InitializeComponent();
            ControlHelper.SetControlsDouble(this);

            //this.btnUserImgCur.IMG.IsRadius = false;
            //btnUserImgCur.IMG.ConerRadius = 1;
            //btnUserImgCur.IMG.IsShowRect = false;

            //this.btnUserImgPre.IMG.IsRadius = false;
            //btnUserImgPre.IMG.ConerRadius = 1;
            //btnUserImgPre.IMG.IsShowRect = false;

            Load += new EventHandler(UCQueueNumber_Load);
            Disposed += FrmQueueNumber_Disposed;


            //Paint += new PaintEventHandler(UCQueueNumber_Paint);
            combMachine.BoxStyle = ComboBoxStyle.DropDownList;
            combMachine.SelectedChangedEvent += new EventHandler(combMachine_SelectedChangedEvent);
            btnRefresh.BtnClick += BtnRefresh_BtnClick;
            //setgrids
            SetDataGrids();
            SetBtns();
            SetSearchBox();
            ClearPatient(true);
            lblTick.Text = PublicRes.GetConfig("C_QUEUE_NUMBER_TICK", "15");
            btnArrange.Visible = PublicRes.HasPrivilege(PrivilegesEnum.排班配置);
            C_QUEUE_NUMBER_BDSJFW = PublicRes.GetConfig("C_QUEUE_NUMBER_BDSJFW", "15").AsInt();
            SetTabs();
            SizeChanged += FrmQueueNumber_SizeChanged;

            btnDeltaCur.LblMouseHover += BtnDeltaCur_LblMouseHover;
            btnDeltaCur.LblMouseLevel += BtnDeltaCur_LblMouseLevel;
            btnDeltaCur.BtnClick += BtnDeltaCur_BtnClick;

            btnDeltaPre.LblMouseHover += BtnDeltaCur_LblMouseHover;
            btnDeltaPre.LblMouseLevel += BtnDeltaCur_LblMouseLevel;
            btnDeltaPre.BtnClick += BtnDeltaCur_BtnClick;

            btnOver.Visible = false;
            btnRecallCalled.Visible = false;
            btnCheckInCalled.Visible = false;

            btnT1.BtnClick += BtnT1_BtnClick;
            btnT2.BtnClick += BtnT2_BtnClick;
            btnT3.BtnClick += BtnT3_BtnClick;

            btnSMS1.BtnClick += BtnSMS1_BtnClick;
            btnSMS2.BtnClick += BtnSMS2_BtnClick;
            btnSMS3.BtnClick += BtnSMS3_BtnClick; ;

            BtnVSDesc = new Dictionary<WDBtnImg0Words, string>()
            {
                 {btnUndo,"撤回" },
                 {btnUp,"上移" },
                 {btnDown,"下移" },
                 {btnSMS1,"发消息" },
                 {btnT1,"特殊情况" },
                 {btnDelete,"移除到已呼叫" },
                 {btnSMS2,"发消息" },
                 {btnT2,"特殊情况" },
                 {btnSMS3,"发消息" },
                 {btnT3,"特殊情况" }
            };
            foreach (var btn in BtnVSDesc)
            {
                btn.Key.LblMouseHover += (s, e) =>
                {
                    Close_FrmAnchorTips();
                    _FrmAnchorTips = FrmAnchorTips.ShowTips(btn.Key, btn.Value, AnchorTipsLocation.BOTTOM, WDColors.TaskListTip, autoCloseTime: 6000);
                };
                btn.Key.LblMouseLevel += (s, e) =>
                {
                    Close_FrmAnchorTips();
                };
            }
        }

        private void FrmQueueNumber_Disposed(object sender, EventArgs e)
        {
            timer1.Dispose();
            timer1 = null;
        }

        FrmAnchorTips _FrmAnchorTips;
        void Close_FrmAnchorTips()
        {
            if (_FrmAnchorTips != null)
            {
                _FrmAnchorTips.Close();
                _FrmAnchorTips = null;
            }
        }

        private Dictionary<WDBtnImg0Words, string> BtnVSDesc;

        private void SetTabs()
        {
            btnScreen_1.Font = WDFonts.TextFontBold18;
            btnCalled_1.Font = WDFonts.TextFontBold18;
            btnArrive.Font = WDFonts.TextFontBold;
            btnScreen_1.ItemHeight = btnScreen_1.Height;
            btnCalled_1.ItemHeight = btnCalled_1.Height;
            btnArrive.ItemHeight = btnArrive.Height;
            btnScreen_1.BackColor = Color.White;
            btnCalled_1.BackColor = Color.White;
            btnScreen_1.Items = new List<WinDoListItem>() { new WinDoListItem() { Text = "大屏幕" } };
            btnScreen_1.CurrentItem = btnScreen_1.Items[0];
            btnCalled_1.Items = new List<WinDoListItem>() { new WinDoListItem() { Text = "已呼叫" } };
            btnCalled_1.CurrentItem = btnCalled_1.Items[0];
            btnArrive.Items = new List<WinDoListItem>() { new WinDoListItem() { Text = "已报到" },
            new WinDoListItem() { Text = "未报到" }};
            btnArrive.CurrentItem = btnArrive.Items[0];

            btnArrive.ItemClick += BtnArrive_ItemClick;
        }

        private void BtnArrive_ItemClick(WinDoListItem item, MouseEventArgs e)
        {
            SetArriveDgv();
        }

        private void BtnDeltaCur_BtnClick(object sender, EventArgs e)
        {
            var btn = (sender as System.Windows.Forms.Control);
            dynamic data = btn.Tag;
            if (data == null) return;
            string PatientID = data.PatientID;
            string PatientName = data.PatientName;
        }


        private void SendSMS(DataGridView dgv)
        {
            var rows = dgv.SelectedRows;
            if (rows == null || rows.Count == 0)
            {
                FormHelper.ShowTipsError("请选择一条记录");
                return;
            }
        }

        private void BtnSMS3_BtnClick(object sender, EventArgs e)
        {
            var dgv = dgvCalled;
            SendSMS(dgv);
        }

        private void BtnSMS2_BtnClick(object sender, EventArgs e)
        {
            var dgv = dgvArrive;
            SendSMS(dgv);
        }

        private void BtnSMS1_BtnClick(object sender, EventArgs e)
        {
            var dgv = dgvScreen;
            SendSMS(dgv);
        }

        private void BtnT1_BtnClick(object sender, EventArgs e)
        {
            var dgv = dgvScreen;
            EditSpecial(dgv);
        }

        private void EditSpecial(DataGridView dgv)
        {
            var rows = dgv.SelectedRows;
            if (rows == null || rows.Count == 0)
            {
                FormHelper.ShowTipsError("请选择一条记录");
                return;
            }

            var datarow = (rows[0].DataBoundItem as DataRowView).Row;
            var PatientID = datarow["PatientID"].AsString();
            var PatientName = datarow["PatientName"].AsString();
        }

        private void BtnT2_BtnClick(object sender, EventArgs e)
        {
            var dgv = dgvArrive;
            EditSpecial(dgv);
        }
        private void BtnT3_BtnClick(object sender, EventArgs e)
        {
            var dgv = dgvCalled;
            EditSpecial(dgv);
        }

        private void BtnDeltaCur_LblMouseLevel(object sender, EventArgs e)
        {
            if (frmAnchorTips != null)
            {
                frmAnchorTips.Close();
                frmAnchorTips = null;
            }
        }

        private void BtnDeltaCur_LblMouseHover(object sender, EventArgs e)
        {
            var btn = (sender as System.Windows.Forms.Control);
            dynamic data = btn.Tag;
            if (data == null) return;
            if (frmAnchorTips != null && frmAnchorTips.Owner != btn.Parent)
            {
                frmAnchorTips.Close();
                frmAnchorTips = null;
            }
            var patientid = data.PatientID.ToString();
        }

        private void FrmQueueNumber_SizeChanged(object sender, EventArgs e)
        {
            panel7.Height = panelLeft.Height / 3 * 2;
            panelRight.Width = Math.Floor((690.0 / 1920) * this.Width).AsInt();
        }

        /// <summary>
        /// 报到时间范围
        /// </summary>
        private int C_QUEUE_NUMBER_BDSJFW = 15;

        private void SetSearchBox()
        {
            txtScreen.SearchClick += TxtScreen_SearchClick;
            txtArrive.SearchClick += TxtScreen_SearchClick;
            txtCalled.SearchClick += TxtScreen_SearchClick;
            txtScreen.txtInput.KeyPress += (s, e) =>
            {
                if (e.KeyChar != (char)Keys.Enter) return;
                TxtScreen_SearchClick(s, e);
                //异步查询，如果，需要清空，需要查询结束后才能清空
                //if (PublicRes.GetConfig("C_SCAN_MODE", "0").AsBool())
                //{
                //    txtScreen.InputText = "";
                //}
            };
            txtArrive.txtInput.KeyPress += (s, e) =>
            {
                if (e.KeyChar != (char)Keys.Enter) return;
                TxtScreen_SearchClick(s, e);
                //异步查询，如果，需要清空，需要查询结束后才能清空
                //if (PublicRes.GetConfig("C_SCAN_MODE", "0").AsBool())
                //{
                //    txtArrive.InputText = "";
                //}
            };
            txtCalled.txtInput.KeyPress += (s, e) =>
            {
                if (e.KeyChar != (char)Keys.Enter) return;
                TxtScreen_SearchClick(s, e);
                //异步查询，如果，需要清空，需要查询结束后才能清空
                //if (PublicRes.GetConfig("C_SCAN_MODE", "0").AsBool())
                //{
                //    txtCalled.InputText = "";
                //}
            };
        }



        private void TxtScreen_SearchClick(object sender, EventArgs e)
        {
            OnBtnRefreshClick();
            return;
            var ctrl = (sender as System.Windows.Forms.Control).Parent;
            var txt = (ctrl as WDTextBoxEx).InputText;
            var dgv = ctrl == txtScreen ? dgvScreen : ctrl == txtArrive ? dgvArrive : dgvCalled;
            var dt = dgv.DataSource as DataTable;
            if (dt == null) return;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["PatientID"].ToString() == (txt) || dt.Rows[i]["PatientName"].ToString().Contains(txt))
                {
                    dgv.Rows[i].Selected = true;
                    dgv.FirstDisplayedScrollingRowIndex = i;
                    return;
                }
            }
        }

        private void SetBtns()
        {
            //通知设置
            btnNoticeSetting.BtnClick += BtnNoticeSetting_BtnClick;
            //手工报道
            btnHandleCheckIn.BtnClick += BtnHandleCheckIn_BtnClick;
            //排班设置
            btnArrange.BtnClick += BtnArrange_BtnClick;
            //交班记录
            btnShiftHandOver.BtnClick += BtnShiftHandOver_BtnClick;


            //呼叫
            btnCall.BtnClick += BtnCall_BtnClick;
            //撤回
            btnUndo.BtnClick += BtnUndo_BtnClick;
            //上移
            btnUp.BtnClick += BtnUp_BtnClick;
            //下移
            btnDown.BtnClick += BtnDown_BtnClick;
            //提取
            btnGet.BtnClick += BtnGet_BtnClick;
            //删除
            btnDelete.BtnClick += BtnDelete_BtnClick;
            //报到
            btnCheckIn.BtnClick += BtnCheckIn_BtnClick;
            //重呼
            btnRecall.BtnClick += BtnRecall_BtnClick;
            //下一位
            btnNext.BtnClick += BtnNext_BtnClick;
            //重呼2
            btnRecall2.BtnClick += BtnRecall2_BtnClick;


            //已呼叫队列
            btnRecallCalled.BtnClick += BtnRecallCalled_BtnClick;
            btnCheckInCalled.BtnClick += BtnCheckInCalled_BtnClick;
            btnOver.BtnClick += BtnOver_BtnClick;
            btnRecallCalled.Enabled = true;
        }

        private void BtnOver_BtnClick(object sender, EventArgs e)
        {
            SetDgvRowCallStatus(dgvCalled, WinDo.Utilities.PublicResource.ResourceEnum.CallStatus.已过号);

        }

        private void BtnCheckInCalled_BtnClick(object sender, EventArgs e)
        {
            SetDgvRowCallStatus(dgvCalled, WinDo.Utilities.PublicResource.ResourceEnum.CallStatus.已报到);

        }

        private void BtnRecallCalled_BtnClick(object sender, EventArgs e)
        {
            SetDgvRowCallStatus(dgvCalled, WinDo.Utilities.PublicResource.ResourceEnum.CallStatus.正在呼叫);

        }

        private void BtnShiftHandOver_BtnClick(object sender, EventArgs e)
        {
            OnBtnRefreshClick();
        }

        /// <summary>
        /// 排班配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnArrange_BtnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(combMachine.SelectedValue))
            {
                FormHelper.ShowTipsError("请选择设备");
                return;
            }
        }


        private void BtnHandleCheckIn_BtnClick(object sender, EventArgs e)
        {
            var frm = new FrmTextBoxWithOk();

            frm.InputTextBox.valueControl.txtInput.MaxLength = 50;
            frm.Size = new System.Drawing.Size(530, 290);

            frm.InputTextBox.Location = new Point(70, 110);
            frm.InputTextBox.Width = 390;
            frm.InputTextBox.LabelWidth = 120;
            frm.MustInput = true;
            frm.Title = "手工报到";
            frm.InputTextBox.label.TextValue = "请输入客户ID：";
            frm.verification.SetVerificationModel(frm.InputTextBox.valueControl, WinDoControls.Controls.VerificationModel.None);
            frm.BtnOK.BtnText = "确定";
            frm.BtnOK.IconName = "I_success";

            frm.BtnOK.BtnClick += (s, ee) =>
            {
                if (!frm.verification.Verification()) return;
            };
            frm.ShowDialog(this);
        }

        private void CheckIn(string Machine_ID, string patientID, int? Sch_ID = null, Action<bool> action = null, Form dialogForm = null)
        {
            var obj = JsonHelper.ObjectToJson(new { patientID, Machine_ID, Sch_ID, minute = C_QUEUE_NUMBER_BDSJFW });
            Task.Factory.StartNew(() =>
            {
                try
                {
                    var rsobj = RequestWebServiceHelper.CallWS("HandRegister", "json", obj, "报到", FormHelper.ShowTipsError);
                    if (rsobj != null)
                    {
                        if (rsobj.Status == 1)
                        {
                            if (rsobj.ChechInStatus == 0)
                            {
                                //FormHelper.ShowTipsSuccess("操作成功" + rsobj.Msg);
                                FormHelper.ShowTipsSuccess("" + rsobj.Msg);
                                if (action != null)
                                    action(true);
                                OnBtnRefreshClick();
                            }
                            else if (rsobj.ChechInStatus == 1)
                            {
                                //提示
                                ConfirmChecIn(action, rsobj, dialogForm);
                            }
                            else
                            {
                                FormHelper.ShowTipsError("" + rsobj.Msg);
                                if (action != null)
                                    action(false);
                            }
                        }
                        else
                        {
                            FormHelper.ShowTipsError("" + rsobj.Msg);
                            if (action != null)
                                action(false);
                        }
                    }
                }
                catch (Exception ex)
                {
                    FormHelper.ShowTipsError("操作失败！" + ex.Message);
                }
            });
        }

        private void ConfirmChecIn(Action<bool> action, dynamic rsobj, Form dialogForm)
        {
            if (dialogForm == null)
                dialogForm = this;
            var ls = (rsobj.Result.Data.handRegisterObj as IEnumerable<dynamic>);
            //if (ls.Count(l => l.Status == 1) == 1)
            if (ls.Count() == 1 && ls.First().Status == 1)
            {
                var l1 = ls.First(l => l.Status == 1);
                string msg = l1.Tips;
                string schid = l1.Sch_ID;
                this.SafeBeginInvoke((Action)(() =>
                {
                    if (DialogResult.Cancel == FrmShadowDialog.ShowAskDialog(dialogForm, msg, "确认提示"))
                        return;
                    Task.Factory.StartNew(() =>
                    {
                        var hrs = RequestWebServiceHelper.CallWS("ConfirmCheckIn", "Sch_ID", schid, "确认报到", FormHelper.ShowTipsError);
                        if (hrs != null)
                        {
                            if (hrs.ChechInStatus == 0)
                            {
                                //FormHelper.ShowTipsSuccess("操作成功" + hrs.Msg);
                                FormHelper.ShowTipsSuccess("" + hrs.Msg);
                                if (action != null)
                                    action(true);
                                OnBtnRefreshClick();
                            }
                            else
                            {
                                FormHelper.ShowTipsError("操作失败！" + hrs.Msg);
                                if (action != null)
                                    action(false);
                            }
                        }
                    });
                }));
            }
            else
            {
            }
        }

        private void FrmTips_BtnClick(object sender, EventArgs e)
        {
            dynamic sch = sender;
            string schid = sch.Sch_ID;
            Task.Factory.StartNew(() =>
            {
                var hrs = RequestWebServiceHelper.CallWS("ConfirmCheckIn", "Sch_ID", schid, "确认报到", FormHelper.ShowTipsError);
                if (hrs != null)
                {
                    if (hrs.Status == 1)
                    {
                        //FormHelper.ShowTipsSuccess("操作成功" + hrs.Msg);
                        FormHelper.ShowTipsSuccess("" + hrs.Msg);
                        this.SafeBeginInvoke((Action)(() =>
                        {
                            sch.Form.DialogResult = System.Windows.Forms.DialogResult.OK;
                            sch.Form.Close();
                        }));
                    }
                    else
                    {
                        FormHelper.ShowTipsError("操作失败！" + hrs.Msg);
                    }
                }
            });
        }

        private void SetCheckInTipInfo(List<dynamic> schs, DateTime start_time, DateTime end_time)
        {
            foreach (dynamic sch in schs)
            {
                DateTime app_dttm = sch.App_DtTm;
                sch.Time = app_dttm.ToString("HH:mm");
                sch.Tips = Enum.GetName(typeof(WinDo.Utilities.PublicResource.ResourceEnum.CallStatus), sch.CallStatus);
                if (sch.CallStatus == (int)WinDo.Utilities.PublicResource.ResourceEnum.CallStatus.未报到)
                {
                    if ((start_time <= app_dttm && app_dttm <= end_time))
                    {
                        continue;
                    }
                    else if (end_time < app_dttm)
                    {
                        var msg = string.Format("该客户预约时间为【{0}】，未到可报到时间，是否继续报到？", app_dttm.ToString("HH:mm"));
                        sch.Tips = msg;
                        continue;
                    }
                    else
                    {
                        var msg = string.Format("该客户预约时间为【{0}】，已迟到，是否继续报到？", app_dttm.ToString("HH:mm"));
                        sch.Tips = msg;
                        continue;
                    }
                }
                //---输入客户ID，若该客户在当天该设备有预约且未报到或已过号并且在可报到时间范围，则报到成功
                //-- - 成功提示： 报到成功！预约设备：【设备名称】；预约时间：【预约时间】
                if (sch.CallStatus == (int)WinDo.Utilities.PublicResource.ResourceEnum.CallStatus.已过号)
                {
                    if ((start_time <= app_dttm && app_dttm <= end_time))
                    {
                        continue;
                    }
                    else
                    {
                        var msg = string.Format("该客户预约时间为【{0}】，已过号，是否继续报到？", app_dttm.ToString("HH:mm"));
                        sch.Tips = msg;
                        continue;
                    }
                }
            }
        }

        private dynamic GetAllowCheckInSch(List<dynamic> schs, DateTime start_time, DateTime end_time)
        {
            dynamic rs = null;
            foreach (dynamic sch in schs)
            {
                DateTime app_dttm = sch.App_DtTm;
                if (sch.CallStatus == (int)WinDo.Utilities.PublicResource.ResourceEnum.CallStatus.未报到)
                {
                    if ((start_time <= app_dttm && app_dttm <= end_time))
                    {
                        rs = sch;
                        break;
                    }
                }
                //---输入客户ID，若该客户在当天该设备有预约且未报到或已过号并且在可报到时间范围，则报到成功
                //-- - 成功提示： 报到成功！预约设备：【设备名称】；预约时间：【预约时间】
                if (sch.CallStatus == (int)WinDo.Utilities.PublicResource.ResourceEnum.CallStatus.已过号)
                {
                    if ((start_time <= app_dttm && app_dttm <= end_time))
                    {
                        rs = sch;
                        break;
                    }
                }
            }

            return rs;
        }


        private void SetCheckIn(string mach_name, dynamic sch, DateTime app_dttm)
        {
            var tipInfo = string.Format("报到成功！预约设备：【{0}】；预约时间：【{1}】", mach_name, app_dttm.ToString("HH:mm"));
            SetCallStatus(sch.Sch_ID.ToString(), WinDo.Utilities.PublicResource.ResourceEnum.CallStatus.已报到, tipInfo);
            return;
            //BLL.Schedule.Instance.UpdateCallStatus(sch.CallStatus, new Model.Schedule() { Sch_Id = sch.Sch_Id, CallStatus = (int)CallStatus.已报到, Edit_User = PublicRes.userinfo.UserID.AsString(), OrderByNo = 0 });
            //OnBtnRefreshClick();
            //FormHelper.ShowTipsSuccess(string.Format("报到成功！预约设备：【{0}】；预约时间：【{1}】", mach_name, app_dttm.ToString("HH:mm")));
        }

        /// <summary>
        /// 打开通知设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNoticeSetting_BtnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(combMachine.SelectedValue))
            {
                FormHelper.ShowTipsError("请选择设备");
                return;
            }
        }

        private void BtnNext_BtnClick(object sender, EventArgs e)
        {
            var dgv = dgvScreen;
            dgv.ClearSelection();
            if (dgv.RowCount == 0)
            {
                return;
            }
            var rows = dgv.SelectedRows;
            if (rows == null || rows.Count == 0)
            {
                dgv.Rows[0].Selected = true;
            }
            BtnCall_BtnClick(sender, e);
        }

        private void BtnRecall2_BtnClick(object sender, EventArgs e)
        {
            Recall(sender);
        }
        void Recall(object sender)
        {
            dynamic sch = (sender as System.Windows.Forms.Control).Tag;
            if (sch == null) return;
            string schid = sch.Sch_Id.ToString();
            SetCallStatus(schid, WinDo.Utilities.PublicResource.ResourceEnum.CallStatus.正在呼叫, null);
            return;
            //BLL.Schedule.Instance.UpdateCallStatus(sch.CallStatus, new Model.Schedule() { Sch_Id = sch.Sch_Id, CallStatus = (int)CallStatus.已呼叫, Edit_User = PublicRes.userinfo.UserID.AsString() });
            ////呼叫
            //OnBtnRefreshClick(() =>
            //{
            //    this.SafeBeginInvoke((Action)(() =>
            //    {
            //        SetPatientInfos();
            //    }));
            //});
        }
        private void BtnRecall_BtnClick(object sender, EventArgs e)
        {
            Recall(sender);
        }

        private void BtnDown_BtnClick(object sender, EventArgs e)
        {
            var rows = dgvScreen.SelectedRows;
            if (rows == null || rows.Count == 0)
            {
                FormHelper.ShowTipsError("请选择一条记录");
                return;
            }
            if (rows[0].Index == dgvScreen.Rows.Count - 1)
            {
                FormHelper.ShowTipsError("最后一条，不能下移");
                return;
            }
            //var idx = rows[0].Index;
            var curRow = (rows[0].DataBoundItem as DataRowView).Row;
            //var nextRow = (dgvScreen.Rows[idx + 1].DataBoundItem as DataRowView).Row;
            //ChangeOrderByNo(curRow, nextRow);
            UpOrDown("down", curRow["Machine_ID"].AsString(), curRow["Sch_Id"].AsString());
        }

        void UpOrDown(string Type, string Machine_ID, string Sch_ID)
        {
            var obj = JsonHelper.ObjectToJson(new { Type, queue = new { Machine_ID, Sch_ID } });
            Task.Factory.StartNew(() =>
            {
                var rsobj = RequestWebServiceHelper.CallWS("AdjustQueueOrder", "inputStr", obj, Type == "up" ? "上移" : "下移", FormHelper.ShowTipsError);
                if (rsobj != null)
                {
                    if (rsobj.Status == 1)
                    {
                        //FormHelper.ShowTipsSuccess();
                    }
                    else
                    {
                        FormHelper.ShowTipsError("操作失败！" + rsobj.Msg);
                    }
                }
                OnBtnRefreshClick();
            });
        }


        void SetOverStatus(string Sch_ID, WinDo.Utilities.PublicResource.ResourceEnum.CallStatus callStatus, string successInfo = null)
        {
            if (successInfo == null)
                successInfo = "操作成功";
            var RealName = PublicRes.CurUser.RealName;
            var UserID = PublicRes.CurUser.UserID;
            var Si = new { Sch_ID, Status = ((int)callStatus).AsString() };
            var obj = JsonHelper.ObjectToJson(new { RealName, UserID, Si });
            Task.Factory.StartNew(() =>
            {
                var rsobj = RequestWebServiceHelper.CallWS("Call", "inputStr", obj, callStatus.ToString(), FormHelper.ShowTipsError);
                if (rsobj != null && (rsobj.Status == 3))
                {
                    this.SafeBeginInvoke((Action)(() =>
                    {
                        if (FrmShadowDialog.ShowAskDialog(FormHelper.MainForm, rsobj.Msg.ToString()) == DialogResult.Cancel)
                            return;
                        SetOverStatus(Sch_ID, WinDo.Utilities.PublicResource.ResourceEnum.CallStatus.强制过号, successInfo);
                    }));
                    return;
                }
                if (rsobj != null)
                {
                    if (rsobj.Status == 1)
                    {
                        FormHelper.ShowTipsSuccess(successInfo);
                    }
                    else
                    {
                        FormHelper.ShowTipsError("操作失败！" + rsobj.Msg);
                    }
                }
                OnBtnRefreshClick();
            });
        }

        void SetCallStatus(string Sch_ID, WinDo.Utilities.PublicResource.ResourceEnum.CallStatus callStatus, string successInfo = null)
        {
            if (successInfo == null)
                successInfo = "操作成功";
            var RealName = PublicRes.CurUser.RealName;
            var UserID = PublicRes.CurUser.UserID;
            var Si = new { Sch_ID, Status = ((int)callStatus).AsString() };
            var obj = JsonHelper.ObjectToJson(new { RealName, UserID, Si });
            Task.Factory.StartNew(() =>
            {
                bool cancle = false;
                try
                {
                    var iobj1 = JsonHelper.ObjectToJson(new { ActiveStationType = 1, Minutes = PublicRes.GetConfig("C_QUEUE_NUMBER_OFFLINE_TIME", "1").AsInt(1), MachineName = this.combMachine.SelectedText });
                    var rsobj1 = RequestWebServiceHelper.CallWS("ActiveStation", "inputStr", iobj1, "获取呼叫屏是否已经启动", null);
                    if (rsobj1 != null)
                        if (rsobj1.Status == 1 && string.IsNullOrWhiteSpace(rsobj1.Msg.ToString()))
                        {
                        }
                        else
                        {
                            this.Invoke((Action)(() =>
                            {
                                if (FrmShadowDialog.ShowAskDialog(FormHelper.MainForm, "排队叫号大屏已离线,是否继续操作?") == DialogResult.Cancel)
                                    cancle = true;
                            }));
                        }
                }
                catch (Exception ex)
                {

                }
                if (cancle) return;
                var rsobj = RequestWebServiceHelper.CallWS("Call", "inputStr", obj, callStatus.ToString(), FormHelper.ShowTipsError);
                if (rsobj != null)
                {
                    if (rsobj.Status == 1)
                    {
                        FormHelper.ShowTipsSuccess(successInfo);
                        OnBtnRefreshClick();
                    }
                    else
                    {
                        FormHelper.ShowTipsError("操作失败！" + rsobj.Msg);
                    }
                }
                else
                {
                    //FormHelper.ShowTipsError("操作失败！接口未返回数据");
                }
            });
        }

        private void ChangeOrderByNo(string Type, DataRow curRow, DataRow nextRow)
        {
            var curSch_Id = curRow["Sch_Id"].AsInt();
            var curOrderByNo = curRow["OrderByNo"].AsInt();
            var nextSch_Id = nextRow["Sch_Id"].AsInt();
            var nextOrderByNo = nextRow["OrderByNo"].AsInt();
            OnBtnRefreshClick();
        }

        private void BtnUp_BtnClick(object sender, EventArgs e)
        {
            var rows = dgvScreen.SelectedRows;
            if (rows == null || rows.Count == 0)
            {
                FormHelper.ShowTipsError("请选择一条记录");
                return;
            }
            if (rows[0].Index == 0)
            {
                FormHelper.ShowTipsError("第一条，不能上移");
                return;
            }
            //var idx = rows[0].Index;
            var curRow = (rows[0].DataBoundItem as DataRowView).Row;
            //var nextRow = (dgvScreen.Rows[idx - 1].DataBoundItem as DataRowView).Row;

            UpOrDown("up", curRow["Machine_ID"].AsString(), curRow["Sch_Id"].AsString());
            //ChangeOrderByNo(curRow, nextRow);
        }

        private void BtnUndo_BtnClick(object sender, EventArgs e)
        {
            SetDgvRowCallStatus(dgvScreen, WinDo.Utilities.PublicResource.ResourceEnum.CallStatus.已撤回);

            //var rows = dgvScreen.SelectedRows;
            //if (rows == null || rows.Count == 0) return;
            //var datarow = (rows[0].DataBoundItem as DataRowView).Row;
            //var status = CallStatus.已撤回;// datarow["CheckInTime"] == DBNull.Value ? CallStatus.未报到 : CallStatus.已报到;
            //SetDgvRowCallStatus(dgvScreen, status);
        }

        private void BtnCall_BtnClick(object sender, EventArgs e)
        {
            SetDgvRowCallStatus(dgvScreen, WinDo.Utilities.PublicResource.ResourceEnum.CallStatus.正在呼叫);
        }

        private void BtnCheckIn_BtnClick(object sender, EventArgs e)
        {
            if (dgvArrive.RowCount == 0)
            {
                FormHelper.ShowTipsError("请选择一条记录");
                return;
            }
            var rows = dgvArrive.SelectedRows;
            if (rows == null || rows.Count == 0)
            {
                FormHelper.ShowTipsError("请选择一条记录");
                return;
                dgvArrive.Rows[0].Selected = true;
                rows = dgvArrive.SelectedRows;
            }
            var datarow = (rows[0].DataBoundItem as DataRowView).Row;
            var Sch_Id = datarow["Sch_Id"].AsInt();
            var Machine_ID = datarow["Machine_ID"].AsString();
            var PatientID = datarow["PatientID"].AsString();
            CheckIn(Machine_ID, PatientID, Sch_Id);
        }

        private void BtnDelete_BtnClick(object sender, EventArgs e)
        {
            var dgv = dgvArrive;
            if (dgv.RowCount == 0)
            {
                FormHelper.ShowTipsError("请选择一条记录");
                return;
            }
            var rows = dgv.SelectedRows;
            if (rows == null || rows.Count == 0)
            {
                FormHelper.ShowTipsError("请选择一条记录");
                return;
            }
            var dr = FrmShadowDialog.ShowAskDialog(FormHelper.MainForm, "删除后客户需要重新报到，确认删除选中客户吗？", "请选择");
            if (DialogResult.Cancel == dr) return;
            SetDgvRowCallStatus(dgvArrive, WinDo.Utilities.PublicResource.ResourceEnum.CallStatus.已呼叫);

        }

        private void BtnGet_BtnClick(object sender, EventArgs e)
        {
            SetDgvRowCallStatus(dgvArrive, WinDo.Utilities.PublicResource.ResourceEnum.CallStatus.已候诊);
        }

        private void SetDgvRowCallStatus(DataGridView dgv, WinDo.Utilities.PublicResource.ResourceEnum.CallStatus status)
        {
            if (dgv.RowCount == 0)
            {
                FormHelper.ShowTipsError("请选择一条记录");
                return;
            }
            var rows = dgv.SelectedRows;
            if (rows == null || rows.Count == 0)
            {
                FormHelper.ShowTipsError("请选择一条记录");
                return;
                dgv.Rows[0].Selected = true;
                rows = dgv.SelectedRows;
            }
            var datarow = (rows[0].DataBoundItem as DataRowView).Row;
            var Sch_Id = datarow["Sch_Id"].AsString();
            SetCallStatus(Sch_Id, status, null);
            return;
            //var screenDt = (dgvScreen.DataSource as DataTable);
            //var waitNo = CallStatus.已候诊 == status ? (screenDt == null ? 1 : screenDt.AsEnumerable().Max(s => (s.Field<string>("OrderByNo"))).AsInt() + 1) : 0;
            //BLL.Schedule.Instance.UpdateCallStatus(datarow["CallStatus"].AsInt(), new Model.Schedule() { Sch_Id = datarow["Sch_Id"].AsInt(), CallStatus = (int)status, Edit_User = PublicRes.userinfo.UserID.AsString(), OrderByNo = waitNo });
            //OnBtnRefreshClick(action);
            //FormHelper.ShowTipsSuccess(status.ToString() + "成功");
        }

        DataGridViewTextBoxColumn AddCol(DataGridView dgv, string headText, string dataField)
        {
            var col = new DataGridViewTextBoxColumn();
            col.Name = "col_" + dataField;
            col.DataPropertyName = dataField;
            col.HeaderText = headText;
            col.SortMode = DataGridViewColumnSortMode.NotSortable;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (headText.Contains("预约备注"))
            {
                col.MinimumWidth = 200;
            }
            else if (headText.Contains("主管开发"))
            {
                col.MinimumWidth = 130;
            }
            else if (headText.Contains("过号"))
            {
                col.MinimumWidth = 50;
            }
            else if (headText.Contains("操作"))
            {
                col.MinimumWidth = 90;
            }
            else if (headText.Contains("时间"))
            {
                col.MinimumWidth = 70;
            }
            else if (headText.Contains("序号"))
            {
                col.MinimumWidth = 70;
                col.FillWeight = 12;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            else if (headText.Contains("ID") || headText.Contains("姓名"))
            {
                col.MinimumWidth = 96;
                col.DefaultCellStyle.ForeColor = WinDo.Utilities.PublicResource.WDColors.GridLinkColor;

            }
            //格式设置需要列数据类型为datetime
            //if (headText.Contains("时间"))
            //{
            //    col.DefaultCellStyle.Format = @"hh\:ss";
            //}
            //col.HeaderCell.Style.
            dgv.Columns.Add(col);
            if (headText.Contains("ID") || headText.Contains("姓名") || headText.Contains("预约备注"))
            {
                DataGridViewHelper.ShowTipsOnOverLength(col);
            }
            return col;
        }

        private void SetDataGrids()
        {
            SetDGV(dgvScreen);
            SetDGV(dgvArrive);
            SetDGV(dgvCalled);
            DataGridViewHelper.SetEmptyText(dgvScreen);
            DataGridViewHelper.SetEmptyText(dgvArrive);
            DataGridViewHelper.SetEmptyText(dgvCalled);
            AddCol(dgvScreen, "序号", "Index");
            AddCol(dgvScreen, "客户ID", "PatientID");

            var colException1 = dgvScreen.AddColumn("", "");

            AddCol(dgvScreen, "客户姓名", "PatientName").ColumnAligment(DataGridViewContentAlignment.MiddleLeft);
            AddCol(dgvScreen, "预约备注", "Schedule_Note").ShowTipsOnOverLength();
            AddCol(dgvScreen, "预约时间", "App_DtTm");
            AddCol(dgvScreen, "报到时间", "CheckInTime");
            AddCol(dgvScreen, "主管开发", "Attending");


            AddCol(dgvArrive, "预约时间", "App_DtTm");
            AddCol(dgvArrive, "报到时间", "CheckInTime");
            AddCol(dgvArrive, "客户ID", "PatientID");

            var colException2 = dgvArrive.AddColumn("", "");

            AddCol(dgvArrive, "客户姓名", "PatientName").ColumnAligment(DataGridViewContentAlignment.MiddleLeft);
            AddCol(dgvArrive, "预约备注", "Schedule_Note").ShowTipsOnOverLength();
            AddCol(dgvArrive, "主管开发", "Attending");

            AddCol(dgvCalled, "ID", "PatientID");

            var colException3 = dgvCalled.AddColumn("", "");

            AddCol(dgvCalled, "姓名", "PatientName").ColumnAligment(DataGridViewContentAlignment.MiddleLeft);
            AddCol(dgvCalled, "时间", "CallTime");
            AddCol(dgvCalled, "过号", "CallStatus");
            AddCol(dgvCalled, "操作", "");
        }
        #region 查看费用右键

        private void DataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                CloseRightMenuForm();
                return;
            }
            if (e.Button == MouseButtons.Left)
            {
                CloseRightMenuForm();
            }
            else if (e.Button == MouseButtons.Right)
            {
                var dgv = sender as DataGridView;
                var curRow = dgv.Rows[e.RowIndex];
                curRow.Selected = true;
                OpenRightMenuForm(dgv);
            }
        }

        private void OpenRightMenuForm(DataGridView dgv)
        {
            var keyPanel = new PT_RightKey() { Dock = DockStyle.Fill, Width = 120 };
            keyPanel.Items = GetRightKeyMenu(dgv);
            CloseRightMenuForm();
            if (keyPanel.Items == null || keyPanel.Items.Count == 0)
                return;
            RightMenuForm = new FrmPanel();
            RightMenuForm.Controls.Add(keyPanel);
            RightMenuForm.Height = keyPanel.GetHeight();
            RightMenuForm.Width = 120;
            RightMenuForm.Location = (MousePosition);
            var cs = Screen.FromPoint(MousePosition);
            if (RightMenuForm.Top + RightMenuForm.Height > cs.WorkingArea.Height)
            {
                RightMenuForm.Top = cs.WorkingArea.Height - RightMenuForm.Height - 10;
            }
            RightMenuForm.Show(this);
            RightMenuForm.Tag = dgv;
            keyPanel.ItemClick += KeyPanel_ItemClick;
        }
        List<WinDoListItem> GetRightKeyMenu(DataGridView dgv)
        {
            //if (ChargeInfo.ChargeTypeConfig == 0)
            //    return null;
            if (!PublicRes.HasPrivilege(PrivilegesEnum.排队叫号查看医嘱))
                return null;

            var selRows = dgv.SelectedRows;
            if (selRows.Count == 0)
                return null;

            dynamic dr = selRows[0].DataBoundItem;
            return new List<WinDoListItem>()
            {
              new WinDoListItem(){ Text = "查看医嘱"}
            };
        }

        private void KeyPanel_ItemClick(WinDoListItem item, MouseEventArgs e)
        {
            if (RightMenuForm.Tag == null)
                return;
            var dgv = RightMenuForm.Tag as DataGridView;
            if (dgv == null) return;
            //点击项事件处理            
            CloseRightMenuForm();
            var srs = dgv.SelectedRows;
            if (srs.Count == 0)
                return;
            if (item.Text == "查看医嘱")
            {
                var drv = srs[0].DataBoundItem as DataRowView;
                var Activity_ID = int.Parse(drv["Activity_ID"].ToString());
                var Pat_ID = int.Parse(drv["Pat_ID"].ToString());
                return;
            }
        }

        private FrmPanel RightMenuForm;
        private void CloseRightMenuForm()
        {
            if (RightMenuForm != null)
            {
                RightMenuForm.Close();
                RightMenuForm = null;
            }
        }

        #endregion
        private void SetDGV(DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;
            dgv.ScrollBars = ScrollBars.Vertical;

            dgv.DefaultCellStyle.Font = WinDo.Utilities.PublicResource.WDFonts.TextFont;
            dgv.ColumnHeadersDefaultCellStyle.Font = WinDo.Utilities.PublicResource.WDFonts.TextFontBold;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.RowsDefaultCellStyle.SelectionBackColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 40;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgv.AllowUserToOrderColumns = false;
            dgv.AllowUserToAddRows = false;
            dgv.MultiSelect = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgv.RowTemplate.Height = 40;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgv.AdvancedCellBorderStyle.All = DataGridViewAdvancedCellBorderStyle.None;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoGenerateColumns = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.AdvancedColumnHeadersBorderStyle.All = DataGridViewAdvancedCellBorderStyle.None;

            dgv.ColumnHeadersDefaultCellStyle.BackColor = WDColors.GrayBackColorF7;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = WDColors.GrayBackColorF7;


            dgv.ReadOnly = true;
            dgv.CellFormatting += Dgv_CellFormatting;
            dgv.DataBindingComplete += (s, e) => { var d = s as DataGridView; d.ClearSelection(); };
            dgv.CellPainting += Dgv_CellPainting;
            dgv.CellMouseEnter += (s, e) =>
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

                //colorTmp = dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor;
                dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = WinDo.Utilities.PublicResource.WDColors.SelectedBackColor;

            };

            dgv.CellMouseMove += Dgv_CellMouseMove;
            dgv.CellMouseLeave += Dgv_CellMouseLeave;
            dgv.CellMouseClick += Dgv_CellMouseClick;
            dgv.CellMouseClick += DataGridView1_CellMouseClick;
            dgv.Parent.MouseMove += Parent_MouseLeave;
            dgv.Parent.Parent.MouseMove += Parent_MouseLeave;
            panel5.MouseMove += Parent_MouseLeave;
            panel1.MouseMove += Parent_MouseLeave;
            panel3.MouseMove += Parent_MouseLeave;
            DataGridViewHelper.SetEmptyText(dgv, true);
        }

        private void Parent_MouseLeave(object sender, EventArgs e)
        {
            CloseRightMenuForm();
        }

        private void Dgv_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var d = sender as DataGridView;
            if (e.Button != MouseButtons.Left)
                return;
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var colHeadText = d.Columns[e.ColumnIndex].HeaderText;
            if (colHeadText.Contains("过号"))
            {
                var cell = d.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell != null && ((int)WinDo.Utilities.PublicResource.ResourceEnum.CallStatus.已过号).AsString() != cell.Value.ToString())
                {
                    var x = (cell.Size.Width - rect_width) / 2;
                    var y = (cell.Size.Height - rect_height) / 2;
                    var rect = new Rectangle(x, y, rect_width, rect_height);
                    if (rect.Contains(e.Location))
                    {
                        var datarow = (d.Rows[e.RowIndex].DataBoundItem as DataRowView).Row;
                        var msg = string.Format("确定将客户【{0}，{1}】设置为“过号”", datarow["PatientID"], datarow["PatientName"]);
                        if (datarow["CallStatus"].AsInt() == (int)WinDo.Utilities.PublicResource.ResourceEnum.CallStatus.已呼叫 && FrmShadowDialog.ShowAskDialog(FormHelper.MainForm, msg) == DialogResult.Cancel)
                            return;
                        SetOverStatus(datarow["Sch_Id"].AsString(), WinDo.Utilities.PublicResource.ResourceEnum.CallStatus.已过号);
                    }
                }
            }
            else if (colHeadText.Contains("操作"))
            {
                var cell = d.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell != null)
                {
                    var all_width = rect_width * 2 + space_width;
                    var x1 = (cell.Size.Width - all_width) / 2;
                    var y1 = (cell.Size.Height - rect_height) / 2;
                    var rect1 = new Rectangle(x1, y1, rect_width, rect_height);
                    var rect2 = new Rectangle(x1 + rect_width + space_width, y1, rect_width, rect_height);
                    var datarow = (d.Rows[e.RowIndex].DataBoundItem as DataRowView).Row;
                    if (rect1.Contains(e.Location))
                    {
                        SetRowCallStatus(datarow, WinDo.Utilities.PublicResource.ResourceEnum.CallStatus.正在呼叫);
                    }
                    else if (rect2.Contains(e.Location))
                    {
                        SetRowCallStatus(datarow, WinDo.Utilities.PublicResource.ResourceEnum.CallStatus.已报到);
                        //var Sch_Id = datarow["Sch_Id"].AsInt();
                        //var Machine_ID = datarow["Machine_ID"].AsString();
                        //var PatientID = datarow["PatientID"].AsString();
                        //CheckIn(Machine_ID, PatientID, Sch_Id);
                    }
                }
            }
            else if (colHeadText.Contains("ID") || colHeadText.Contains("姓名"))
            {
                var cell = d.Rows[e.RowIndex].Cells[e.ColumnIndex];
                var size = TextRenderer.MeasureText(cell.Value.AsString(""), d.RowsDefaultCellStyle.Font);
                var rect = new Rectangle((cell.Size.Width - size.Width) / 2, (cell.Size.Height - size.Height) / 2, size.Width, size.Height);
                if (colHeadText.Contains("姓名"))
                    rect.X = 0;
                if (!rect.Contains(e.Location)) return;
                DataRow row = (d.Rows[e.RowIndex].DataBoundItem as DataRowView).Row;
                //DataRow row1 = (dgv.DataSource as DataTable).Rows[e.RowIndex];
                if (row == null) return;

                //打开 客户 信息列表

            }
            else if (d.Columns[e.ColumnIndex].DataPropertyName == "SpecialInfoCount")
            {
                //var dgv = sender as DataGridView;
                //var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                //if (cell != null && cell.Value.AsInt() > 0)
                //{
                //    var rect_width = btnDeltaCur.Width;
                //    var rect_height = btnDeltaCur.Height;
                //    var x = (cell.Size.Width - rect_width) / 2;
                //    var y = (cell.Size.Height - rect_height) / 2;
                //    var rect = new Rectangle(x, y, rect_width, rect_height);
                //    if (rect.Contains(e.Location))
                //    {
                //        var dr = (dgv.Rows[e.RowIndex].DataBoundItem as DataRowView);
                //        if (dr == null) return;
                //        var patientid = dr["PatientID"].AsString();
                //        var patientName = dr["PatientName"].AsString();
                //        frmSpecialInfoItem.OpenPatientSpicialForm(patientid, patientName, OnBtnRefreshClick);
                //    }
                //}
            }
        }

        private void SetRowCallStatus(DataRow datarow, WinDo.Utilities.PublicResource.ResourceEnum.CallStatus status)
        {
            SetCallStatus(datarow["Sch_Id"].AsString(), status, null);
            return;
            //BLL.Schedule.Instance.UpdateCallStatus(datarow["CallStatus"].AsInt(), new Model.Schedule() { Sch_Id = datarow["Sch_Id"].AsInt(), CallStatus = (int)status, Edit_User = PublicRes.userinfo.UserID.AsString(), OrderByNo = 0 });
            //OnBtnRefreshClick(action);
            //FormHelper.ShowTipsSuccess(status.ToString() + "成功");
        }

        private void Dgv_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            Close_FrmAnchorTips();

            var dgv = sender as DataGridView;
            dgv.Cursor = Cursors.Default;
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = e.RowIndex % 2 == 0 ? Color.White : WDColors.GrayBackColorF7;
            if (frmAnchorTips != null)
            {
                frmAnchorTips.Close();
                frmAnchorTips = null;
            }
        }

        private void Dgv_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                CloseRightMenuForm();
                return;
            }
            var d = sender as DataGridView;
            var colHeadText = d.Columns[e.ColumnIndex].HeaderText;
            if (colHeadText.Contains("过号"))
            {
                var cell = d.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell != null && ((int)WinDo.Utilities.PublicResource.ResourceEnum.CallStatus.已过号).AsString() != cell.Value.ToString())
                {
                    var x = (cell.Size.Width - rect_width) / 2;
                    var y = (cell.Size.Height - rect_height) / 2;
                    var rect = new Rectangle(x, y, rect_width, rect_height);
                    if (rect.Contains(e.Location))
                    {
                        d.Cursor = Cursors.Hand;
                        var cellRect = d.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                        var dgvPoint1 = d.Parent.PointToScreen(d.Location);
                        cellRect.Offset(dgvPoint1);
                        rect.Offset(cellRect.Location);
                        if (_FrmAnchorTips != null && _FrmAnchorTips.RectControl == rect)
                            return;
                        Close_FrmAnchorTips();
                        _FrmAnchorTips = FrmAnchorTips.ShowTips(rect, "过号", AnchorTipsLocation.BOTTOM, WDColors.TaskListTip, autoCloseTime: 6000);
                    }
                    else
                    {
                        d.Cursor = Cursors.Default;
                        Close_FrmAnchorTips();
                    }
                }
            }
            else if (colHeadText.Contains("操作"))
            {
                var cell = d.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell != null)
                {
                    var all_width = rect_width * 2 + space_width;
                    var x1 = (cell.Size.Width - all_width) / 2;
                    var y1 = (cell.Size.Height - rect_height) / 2;
                    var rect1 = new Rectangle(x1, y1, rect_width, rect_height);
                    var rect2 = new Rectangle(x1 + rect_width + space_width, y1, rect_width, rect_height);
                    if (rect1.Contains(e.Location) || rect2.Contains(e.Location))
                    {
                        d.Cursor = Cursors.Hand;
                        var cellRect = d.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                        var dgvPoint1 = d.Parent.PointToScreen(d.Location);
                        cellRect.Offset(dgvPoint1);
                        if (rect1.Contains(e.Location))
                        {
                            rect1.Offset(cellRect.Location);
                            if (_FrmAnchorTips != null && _FrmAnchorTips.RectControl == rect1)
                                return;
                            Close_FrmAnchorTips();
                            _FrmAnchorTips = FrmAnchorTips.ShowTips(rect1, "呼叫", AnchorTipsLocation.BOTTOM, WDColors.TaskListTip, autoCloseTime: 6000);
                        }
                        else if (rect2.Contains(e.Location))
                        {
                            rect2.Offset(cellRect.Location);
                            if (_FrmAnchorTips != null && _FrmAnchorTips.RectControl == rect2)
                                return;
                            Close_FrmAnchorTips();
                            _FrmAnchorTips = FrmAnchorTips.ShowTips(rect2, "报到", AnchorTipsLocation.BOTTOM, WDColors.TaskListTip, autoCloseTime: 6000);
                        }
                    }
                    else
                    {
                        d.Cursor = Cursors.Default;
                        Close_FrmAnchorTips();
                    }
                }
            }
            else if (colHeadText.Contains("ID") || colHeadText.Contains("姓名"))
            {
                var cell = d.Rows[e.RowIndex].Cells[e.ColumnIndex];
                var size = TextRenderer.MeasureText(cell.Value.AsString(""), d.RowsDefaultCellStyle.Font);
                var rect = new Rectangle((cell.Size.Width - size.Width) / 2, (cell.Size.Height - size.Height) / 2, size.Width, size.Height);
                if (colHeadText.Contains("姓名"))
                    rect.X = 0;
                if (rect.Contains(e.Location))
                    d.Cursor = Cursors.Hand;
                else
                    d.Cursor = Cursors.Default;
            }
            else if (d.Columns[e.ColumnIndex].DataPropertyName == "SpecialInfoCount")
            {

                //var dgv = sender as DataGridView;
                //var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                //if (cell != null && cell.Value.AsInt() > 0)
                //{
                //    var rect_width = btnDeltaCur.Width;
                //    var rect_height = btnDeltaCur.Height;
                //    var x = (cell.Size.Width - rect_width) / 2;
                //    var y = (cell.Size.Height - rect_height) / 2;
                //    var rect = new Rectangle(x, y, rect_width, rect_height);
                //    if (rect.Contains(e.Location))
                //    {
                //        d.Cursor = Cursors.Hand;
                //        var cellRect = dgv.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                //        var dgvPoint1 = dgv.Parent.PointToScreen(dgv.Location);
                //        cellRect.Offset(dgvPoint1);
                //        if (frmAnchorTips != null && frmAnchorTips.RectControl != cellRect)
                //        {
                //            frmAnchorTips.Close();
                //            frmAnchorTips = null;
                //        }
                //        var patientid = (dgv.Rows[e.RowIndex].DataBoundItem as DataRowView)["PatientID"].AsString();
                //        if (frmAnchorTips == null)
                //            frmAnchorTips = FrmAnchorTips_TS.ShowWithIconTips(cellRect, patientid, AnchorTipsLocation.BOTTOM, YkdBasisColors.TaskListTip, autoCloseTime: 6000);
                //    }
                //    else
                //    {
                //        d.Cursor = Cursors.Default;
                //        if (frmAnchorTips != null)
                //        {
                //            frmAnchorTips.Close();
                //            frmAnchorTips = null;
                //        }
                //    }
                //}
            }
        }
        FrmAnchorTips frmAnchorTips;
        int rect_width = 32;
        int rect_height = 26;
        int space_width = 10;
        private void Dgv_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var dgv = sender as DataGridView;
            var headText = dgv.Columns[e.ColumnIndex].HeaderText;
            if (headText.Contains("过号"))
            {
                e.PaintBackground(e.CellBounds, true);

                //图片列
                if (((int)WinDo.Utilities.PublicResource.ResourceEnum.CallStatus.已过号).AsString() == e.Value.AsString())
                {
                    var format = new StringFormat();
                    format.LineAlignment = StringAlignment.Center;  // 更正： 垂直居中
                    format.Alignment = StringAlignment.Center;      // 水平居中
                    var brush = dgv.Rows[e.RowIndex].Selected ? Brushes.White : Brushes.Black;
                    e.Graphics.DrawString("已过号", e.CellStyle.Font, brush, e.CellBounds, format);
                }
                else
                {
                    var img = btnOver.Image;
                    ControlHelper.DrawRectBtn(e.Graphics, e.CellBounds, rect_width, rect_height, Color.White, WDColors.GrayRectColor, img);
                }

                e.Handled = true;
            }
            else if (dgv.Columns[e.ColumnIndex].DataPropertyName == "SpecialInfoCount")
            {
                //图片列
                //e.PaintBackground(e.CellBounds, true);
                //if (e.Value.AsInt() > 0)
                //{
                //    var img = DeltaImg;
                //    var p = new Point(e.CellBounds.X + (e.CellBounds.Width - img.Width) / 2, e.CellBounds.Y + (e.CellBounds.Height - img.Height) / 2);
                //    e.Graphics.DrawImage(img, p);
                //}
                //e.Handled = true;
            }
            else if (headText.Contains("操作"))
            {
                var bc = dgv.Rows[e.RowIndex].Selected ? e.CellStyle.SelectionBackColor : e.CellStyle.BackColor;
                using (var backColorBrush = new SolidBrush(bc))
                {
                    e.Graphics.FillRectangle(backColorBrush, e.CellBounds);

                    var img1 = btnRecallCalled.Image;
                    var img2 = btnCheckInCalled.Image;

                    var all_width = rect_width * 2 + space_width;
                    var x = e.CellBounds.X + (e.CellBounds.Width - all_width) / 2;
                    var y = e.CellBounds.Y + (e.CellBounds.Height - rect_height) / 2;
                    var container = new Rectangle(x, y, rect_width, rect_height);
                    ControlHelper.DrawRectBtn(e.Graphics, container, rect_width, rect_height, Color.White, WDColors.GrayRectColor, img1);
                    container.Offset(rect_width + space_width, 0);
                    ControlHelper.DrawRectBtn(e.Graphics, container, rect_width, rect_height, Color.White, WDColors.GrayRectColor, img2);

                    //var rect = new Rectangle(x, y, all_width, rect_height);
                    //var rect1 = new Rectangle(x, y, img1.Width, img1.Height);
                    //var rect2 = new Rectangle(x + img1.Width + 10, y, img1.Width, img1.Height);
                    //ControlHelper.FillRoundRectangle(e.Graphics, Brushes.White, rect1, 2);
                    //ControlHelper.FillRoundRectangle(e.Graphics, Brushes.White, rect2, 2);
                    //using (var pen = new Pen(YkdBasisColors.GrayRectColor))
                    //{
                    //    ControlHelper.DrawRoundRectangle(e.Graphics, Pens.Gray, rect1, 2);
                    //    ControlHelper.DrawRoundRectangle(e.Graphics, Pens.Gray, rect2, 2);
                    //}
                    //e.Graphics.DrawImage(img1, rect1);
                    //e.Graphics.DrawImage(img2, rect2);

                }
                e.Handled = true;
            }
        }
        Image DeltaImg = WDImages.GetBtnIconImage("I_exclamation_delta", color: WDColors.OrangeColor);

        private void Dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var dgv = sender as DataGridView;
            var headText = dgv.Columns[e.ColumnIndex].HeaderText;
            if (headText.Contains("时间"))
            {
                if (e.Value != null && e.Value != DBNull.Value && e.Value.AsString().Trim().Length > 0)
                    e.Value = e.Value.AsDateTime().ToString("HH:mm");
            }
            var dr = dgv.Rows[e.RowIndex].DataBoundItem as DataRowView;
            if (dr["HasCRB"].AsInt(0) > 0 && (dgv.Columns[e.ColumnIndex].DataPropertyName == "PatientID" || dgv.Columns[e.ColumnIndex].DataPropertyName == "PatientName"))
            {
                e.CellStyle.ForeColor = WDColors.CRBColor;
                e.CellStyle.Font = WDFonts.TextFontBold;
            }
        }

        private void BtnRefresh_BtnClick(object sender, EventArgs e)
        {
            //FrmAnchorTips_TS.ShowWithIconTips(btnRefresh, "34", AnchorTipsLocation.BOTTOM, YkdBasisColors.TaskListTip);
            OnBtnRefreshClick();
        }
        private void OnBtnRefreshClick()
        {
            this.SafeBeginInvoke((() =>
            {
                timer1.Stop();
                timer1.Enabled = false;
                try
                {
                    lblTick.Text = PublicRes.GetConfig("C_QUEUE_NUMBER_TICK", "15");
                    RefreshData();
                }
                catch (Exception ex)
                {
                    FormHelper.ShowTipsError("刷新失败," + ex.Message);
                }
                finally
                {
                    timer1.Enabled = true;
                    timer1.Start();
                }
            }));
        }


        public string SelectedMachine = "C_QUEUE_NUMBER_MACHINE";

        private void BindingData()
        {
        }


        /// <summary>
        /// 选择设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void combMachine_SelectedChangedEvent(object sender, EventArgs e)
        {
            var mID = combMachine.SelectedValue;
            if (!string.IsNullOrWhiteSpace(mID))
            {
            }
            OnBtnRefreshClick();
        }

        void Parent_SizeChanged(object sender, EventArgs e)
        {
            panelRight.Width = this.Width / 3;
        }

        /// <summary>
        /// 所有预约
        /// </summary>
        List<dynamic> Schedules;

        void RefreshData()
        {
            if (!this.Visible)
                return;
            if (string.IsNullOrWhiteSpace(combMachine.SelectedValue))
            {
                FormHelper.ShowTipsError("请选择设备");
                return;
            }
            Task.Factory.StartNew(() =>
            {

                var sch2 = Schedules.Where(s => s.CallStatus == (int)WinDo.Utilities.PublicResource.ResourceEnum.CallStatus.已候诊).OrderBy(s => s.WaitTime).ToList();
                var sch3 = Schedules.Where(s => s.CallStatus == (int)WinDo.Utilities.PublicResource.ResourceEnum.CallStatus.正在呼叫).ToList();
                var sch4 = Schedules.Where(s => s.CallStatus == (int)WinDo.Utilities.PublicResource.ResourceEnum.CallStatus.已呼叫).ToList();
                var sch5 = Schedules.Where(s => s.CallStatus == (int)WinDo.Utilities.PublicResource.ResourceEnum.CallStatus.已过号).ToList();
                this.SafeBeginInvoke((Action)(() =>
                {
                    var dgvScreenCurSchId = GetDGVSelectedRowSchID(dgvScreen);
                    var dgvArriveCurSchId = GetDGVSelectedRowSchID(dgvArrive);
                    var dgvCalledCurSchId = GetDGVSelectedRowSchID(dgvCalled);

                    if (ControlHelper.GetInputQueryText(txtScreen.InputText).Length > 0)
                        sch2 = sch2.Where(r => r.PatientID.ToString() == ControlHelper.GetInputQueryText(txtScreen.InputText) || r.PatientName.ToString() == ControlHelper.GetInputQueryText(txtScreen.InputText)).ToList();
                    //绑定datagrid
                    int idx = 1;
                    foreach (dynamic item in sch2)
                    {
                        var ss = (IDictionary<string, object>)item;
                        ss["Index"] = idx++;
                    }
                    dgvScreen.DataSource = sch2.ToDataTable();
                    btnScreen_1.Items[0].TipText = sch2.Count > 0 ? sch2.Count.AsString() : null;
                    btnScreen_1.Invalidate();
                    dgvScreen.AutoResizeColumns();
                    SetArriveDgv();


                    var calledls = sch3.Concat(sch4).Concat(sch5).OrderByDescending(s => s.CallTime).ToList();
                    if (ControlHelper.GetInputQueryText(txtCalled.InputText).Length > 0)
                        calledls = calledls.Where(r => r.PatientID.ToString() == ControlHelper.GetInputQueryText(txtCalled.InputText) || r.PatientName.ToString() == ControlHelper.GetInputQueryText(txtCalled.InputText)).ToList();
                    dgvCalled.DataSource = calledls.ToDataTable();
                    btnCalled_1.Items[0].TipText = calledls.Count > 0 ? calledls.Count.AsString() : null;
                    btnCalled_1.Invalidate();
                    dgvCalled.AutoResizeColumns();
                    SetPatientInfos();
                    //定位到原来各表格的行
                    SelectRowBySchId(dgvScreenCurSchId, dgvArriveCurSchId, dgvCalledCurSchId);
                }));
            });
        }

        private string GetDGVSelectedRowSchID(DataGridView dgv)
        {
            if (dgv.SelectedRows.Count > 0)
            {
                return (dgv.SelectedRows[0].DataBoundItem as DataRowView)["Sch_Id"].ToString();
            }
            return "";
        }
        private void SetDGVSelectedRowSchID(DataGridView dgv, string Sch_ID)
        {
            foreach (DataGridViewRow dgvr in dgv.Rows)
            {
                var dr = dgvr.DataBoundItem as DataRowView;
                if (dr["Sch_Id"].ToString() == Sch_ID)
                {
                    dgvr.Selected = true;
                    dgv.FirstDisplayedScrollingRowIndex = dgvr.Index;
                    break;
                }
            }
        }

        private void SelectRowBySchId(string dgvScreenCurSchId, string dgvArriveCurSchId, string dgvCalledCurSchId)
        {
            SetDGVSelectedRowSchID(dgvScreen, dgvScreenCurSchId);
            //SetDGVSelectedRowSchID(dgvArrive, dgvArriveCurSchId);
            //SetDGVSelectedRowSchID(dgvCalled, dgvCalledCurSchId);
        }

        void ClearPatient(bool resetPic)
        {
            panel8.BackColor = Color.White;
            panel9.BackColor = Color.White;
            lblLineCur.BackColor = WDColors.BtnSelectedLine;
            lblLinePre.BackColor = WDColors.BtnSelectedLine;
            lblCallCur.ForeColor = Color.Black;
            lblCallPre.ForeColor = Color.Black;
            lblUserNameCur.ForeColor = Color.Black;
            lblUserInfoCur.ForeColor = Color.Black;
            lblUserNamePre.ForeColor = Color.Black;
            lblUserInfoPre.ForeColor = Color.Black;
            btnDeltaCur.Visible = false;
            btnDeltaPre.Visible = false;
            btnErtongCur.Visible = false;
            btnErtongPre.Visible = false;
            btnNeedChargeCur.Visible = false;
            btnNeedChargePre.Visible = false;
            btnDeltaCur.Tag = null;
            btnDeltaPre.Tag = null;
            if (resetPic)
            {
                btnUserImgCur.ResetProfilePicture();
                btnUserImgCur.Tag = null;
                btnUserImgPre.ResetProfilePicture();
                btnUserImgPre.Tag = null;
            }

            lblUserNameCur.Text = "";
            lblUserInfoCur.Text = "";

            lblUserNamePre.Text = "";
            lblUserInfoPre.Text = "";
        }
        void SetPatientInfos()
        {
            ClearPatient(false);
            btnRecall.Tag = null;
            btnRecall2.Tag = null;
            btnNeedChargeCur.Tag = null;
            btnNeedChargePre.Tag = null;

            var calleds = Schedules.Where(s => s.CallStatus == (int)WinDo.Utilities.PublicResource.ResourceEnum.CallStatus.正在呼叫 || s.CallStatus == (int)WinDo.Utilities.PublicResource.ResourceEnum.CallStatus.已呼叫 || s.CallStatus == (int)WinDo.Utilities.PublicResource.ResourceEnum.CallStatus.已过号).OrderByDescending(s => s.CallTime).ToList();

            if (calleds.Count > 0)
            {
                var calling = calleds.First();
                SetPatient(calling.Pat_ID, lblUserNameCur, lblUserInfoCur, btnUserImgCur, btnErtongCur);
                btnRecall.Tag = calling;
                btnDeltaCur.Tag = calling;
                btnDeltaCur.Visible = calling.SpecialInfoCount > 0;
            }
            else
            {
                btnUserImgCur.ResetProfilePicture();
                btnUserImgCur.Tag = null;
            }
            if (calleds.Count > 1)
            {
                var preCalled = calleds.First(); // sch.Skip(1).Take(1).First();
                if (calleds.Count > 1)
                    preCalled = calleds.Skip(1).Take(1).First();
                SetPatient(preCalled.Pat_ID, lblUserNamePre, lblUserInfoPre, btnUserImgPre, btnErtongPre);
                btnRecall2.Tag = preCalled;
                btnDeltaPre.Tag = preCalled;
                btnDeltaPre.Visible = preCalled.SpecialInfoCount > 0;
            }
            else
            {
                btnUserImgPre.ResetProfilePicture();
                btnUserImgPre.Tag = null;
            }
        }

        private void SetPatient(int patid, System.Windows.Forms.Label username, System.Windows.Forms.Label userinfo, UCBigUserProfilePicture btnImg, WDBtnImg0Words btnErTong)
        {
        }

        private void SetArriveDgv()
        {
            if (Schedules == null) return;
            try
            {
                panel13.SuspendLayout();
                btnDelete.Visible = btnArrive.CurrentItem == btnArrive.Items[0];
                btnCheckIn.Visible = btnArrive.CurrentItem == btnArrive.Items[1];
                var sch0 = Schedules.Where(s => s.CallStatus == (int)WinDo.Utilities.PublicResource.ResourceEnum.CallStatus.未报到).ToList();
                var sch1 = Schedules.Where(s => s.CallStatus == (int)WinDo.Utilities.PublicResource.ResourceEnum.CallStatus.已报到).ToList();
                this.dgvArrive.Columns["col_CheckInTime"].Visible = btnArrive.CurrentItem == btnArrive.Items[0];
                var arrivels = PublicRes.GetConfig("C_QUEUE_NUMBER_ARR_SORT", "1") == "1" ? sch1.OrderBy(s => s.App_DtTm).ThenBy(s => s.CheckInTime).ToList() : sch1.OrderBy(s => s.CheckInTime).ToList();
                var not_arrivels = sch0.OrderBy(s => s.App_DtTm).ThenBy(s => s.Submission_DtTm).ToList();

                if (ControlHelper.GetInputQueryText(txtArrive.InputText).Length > 0)
                {
                    arrivels = arrivels.Where(r => r.PatientID.ToString() == ControlHelper.GetInputQueryText(txtArrive.InputText) || r.PatientName.ToString() == ControlHelper.GetInputQueryText(txtArrive.InputText)).ToList();
                    not_arrivels = not_arrivels.Where(r => r.PatientID.ToString() == ControlHelper.GetInputQueryText(txtArrive.InputText) || r.PatientName.ToString() == ControlHelper.GetInputQueryText(txtArrive.InputText)).ToList();
                }
                dgvArrive.DataSource = btnArrive.CurrentItem == btnArrive.Items[0] ? arrivels.ToDataTable() : not_arrivels.ToDataTable();
                dgvArrive.AutoResizeColumns();

                btnArrive.Items[0].TipText = arrivels.Count > 0 ? arrivels.Count.AsString() : null;
                btnArrive.Items[1].TipText = not_arrivels.Count > 0 ? not_arrivels.Count.AsString() : null;
                btnArrive.Invalidate();
            }
            finally
            {
                panel13.ResumeLayout(true);
            }
        }

        void UCQueueNumber_Load(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                BindingData();
                this.SafeBeginInvoke(() =>
                {
                    this.VisibleChanged += FrmQueueNumber_VisibleChanged;
                });
            });
            //this.MainForm.SizeChanged += new EventHandler(Parent_SizeChanged);
            //var lstCom = new List<KeyValuePair<string, string>>();
            //for (int i = 0; i < 5; i++)
            //{
            //    lstCom.Add(new KeyValuePair<string, string>(i.ToString(), "加速器一室" + i));
            //}
            //this.combMachine.Source = lstCom;
            //this.combMachine.SelectedIndex = 1;

            //SetBtnAndDataGrids();
            //SetIcon();
            //SetGrid();

        }

        private void FrmQueueNumber_VisibleChanged(object sender, EventArgs e)
        {
            this.timer1.Enabled = this.Visible;
            if (!this.Disposing && this.Visible && this.Parent != null)
                OnBtnRefreshClick();
        }

        private void SetBtnAndDataGrids()
        {
            //var btng = new BtnGroup(new Dictionary<UCTipBtnExt, Control> { { ucBtnArrived, ucDGVArrived }, { ucBtnNotArrived, ucDGVNotArrived }, { unBtnUnQueue, ucDGVUnQueue } });
            //btng.SetSeletedOne(ucBtnArrived);
            //ucBtnNotArrived.IsShowTips = true;
            //ucBtnNotArrived.TipsText = "55";
        }
        /// <summary>
        /// 闪烁设置宽度测试
        /// </summary>
        /// <param name="size"></param>
        //public void SetLayoutControlsSize(Size size)
        //{
        //    this.Width = size.Width;
        //    this.Height = size.Height;
        //    panelRight.Width = size.Width / 3;
        //    panelLeft.Width = size.Width - panelRight.Width;
        //}



        private void SetIcon()
        {




            //A_fa_user_circle
            btnUserImgCur.BackgroundImage = WDImages.GetBtnIconImage("A_fa_user_circle", 200, Color.FromArgb(240, 240, 240));
            btnUserImgCur.BackgroundImageLayout = ImageLayout.Zoom;
            btnUserImgPre.BackgroundImage = WDImages.GetBtnIconImage("A_fa_user_circle", 90, Color.FromArgb(240, 240, 240));
            btnUserImgPre.BackgroundImageLayout = ImageLayout.Zoom;






            //ucBtnImgDown.Image = ucBtnImgNotifySet.Image = ucBtnImgQueueSet.Image = ucBtnImgHand.Image = ucBtnImgChangeRecode.Image = ucBtnImgDown.Image;

            //var btns = new[]
            //{
            //    ucBtnImgCall,
            //    ucBtnImgRedo,
            //    ucBtnImgUp,
            //    ucBtnImgDown,
            //    ucBtnImgRecall,
            //    ucBtnImgNext,
            //    ucBtnImgRecall2,
            //    ucBtnImgGet,
            //    ucBtnImgArrive,
            //    ucBtnImgChange,
            //};
            //foreach (var item in btns)
            //{
            //    FormHelper.SetIconPadding(item);
            //}
            //tabCtrlTop.IsShowBadge = false;
            //tabCtrlTop.Padding = new Point(12, 0);
            //tabCtrlTop.BadgeText = new string[] { "88" };
            //tabControlExt3.IsShowBadge = tabCtrlBottom.IsShowBadge = tabCtrlTop.IsShowBadge;
            //tabControlExt3.Padding = tabCtrlBottom.Padding = tabCtrlTop.Padding;
        }

        private void ucBtnImgRedo_BtnClick(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var time = lblTick.Text.AsInt();
            if (time > 1)
            {
                lblTick.Text = (time - 1).AsString();
                return;
            }

            OnBtnRefreshClick();
        }

        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        var parms = base.CreateParams;
        //        parms.Style &= ~0x02000000;  // Turn off WS_CLIPCHILDREN
        //        return parms;
        //    }
        //}
        //protected override void WndProc(ref Message m)
        //{
        //    if (m.Msg == 0x0014) // 禁掉清除背景消息
        //        return;
        //    base.WndProc(ref m);
        //}


    }

    public class TestGridModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Sex { get; set; }
        public int Age { get; set; }
        public string Image1 { get; set; }
        public bool IsShowImage { get; set; }
        public DateTime Time { get; set; }
        public string Next { get; set; }
        public string Operation { get; set; }
        public Func<int, bool> ShowBtn { get; set; }
        public Func<object, Image> GetCellImage { get; set; }
    }
}

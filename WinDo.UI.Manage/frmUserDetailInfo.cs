using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinDo.UI.Utilities;

using WinDoControls.Controls;
using WinDo.Utilities;
using WinDo.UI.Utilities.DialogForm;
using WinDoControls.Forms;
using WinDo.Utilities.PublicResource;
using System.Threading.Tasks;
using WinDoControls;
using WinDo.Utilities.Mock;

namespace WinDo.UI.Manage
{
    public partial class frmUserDetailInfo : BaseForm
    {
        public frmUserDetailInfo()
        {
            InitializeComponent();

            InitFormControls();
            txtPhone.valueControl.txtInput.KeyPress += new KeyPressEventHandler(valueControl_KeyPress);
            Load += new EventHandler(frmUserDetailInfo_Load);
        }


        void valueControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((System.Windows.Forms.Control.ModifierKeys & Keys.Control) == Keys.Control) return;
            if (e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Delete)
                if ((e.KeyChar < (char)Keys.D0 || e.KeyChar > (char)Keys.D9))
                {
                    e.Handled = true;
                }
        }
        private Model.Users UpdateUser;
        void LoadUserInfo()
        {
            var user = MockData.Users.First(u => u.UserID == UserID);
            UpdateUser = user;

            formInputItems.SetControlValuesByEntity(user);
            txtStatus.valueControl.Text = user.IsActive == 1 ? "启用" : "停用";
            var titleName = MockData.SystemDic.Where(s => s.DicGroupCode == "职务")
                .FirstOrDefault(s => s.SysVal.AsInt() == user.Title_ID);
            txtTitle.valueControl.Text = "";
            if (titleName != null)
                txtTitle.valueControl.Text = titleName.SysDes;
            string changedField = "";
        }
        void frmUserDetailInfo_Load(object sender, EventArgs e)
        {
            LoadUserInfo();
        }
        public int UserID { get; set; }

        VerificationComponent verification = new VerificationComponent();
        FormItems<Model.Users> formInputItems;
        void InitFormControls()
        {
            formInputItems = new FormItems<Model.Users>(verification);
            formInputItems.AddRange(new List<FormItem>{
            new FormItem(){ LabelText= "用户名", FieldName= "LoginName", Ctrl= txtLoingName},
            new FormItem(){ LabelText= "姓名", FieldName= "RealName", Ctrl= txtRealName},
            new FormItem(){ LabelText= "性别", FieldName= "UserSex", Ctrl= txtSex},
            new FormItem(){ LabelText= "账户状态",  Ctrl= txtStatus},
            new FormItem(){ LabelText= "职务", Ctrl= txtTitle},
            new FormItem(){ LabelText= "角色",   FieldName= "RoleName",Required=true, Ctrl= combRole},//Role_ID ID整型不能字符串直接转换
            new FormItem(){ LabelText= "上级开发", FieldName= "SuperiorDoctorID", Ctrl= combSuperiorDoctor},
            new FormItem(){ LabelText= "开发组", FieldName= "DoctorGroupName", Ctrl= combDoctorGroup},
            new FormItem(){ LabelText= "上级产品", FieldName= "", Ctrl= combSuperiorPhysicist,},
            new FormItem(){ LabelText= "产品组", FieldName= "PhysicistGroupName", Ctrl= combPhysicistGroup},
            new FormItem(){ LabelText= "手机", FieldName= "Phone", Ctrl= txtPhone,Required=true, MaxLength=20,RegexStr=@"^(\+?86)?1\d{10}$",RegexMsg="请输入有效手机号"},

            new FormItem(){ LabelText= "座机", FieldName= "HomePhone", Ctrl= txtHomePhone,MaxLength=50,RegexStr=@"^[0-9 -]*$",RegexMsg="请输入有效座机号"},
            new FormItem(){ LabelText= "备注", FieldName= "Remark", Ctrl= txtRemark,MaxLength=100},
            });
            //注册校验规则
            formInputItems.RegisterMRR();

            SetCombCtrls();


            //txtAge.ReadOnly = true;
            //ResetDateTimeCtrls();
            //btnSave.BtnClick += new EventHandler(btnSave_BtnClick);
            //btnSaveAndOpenBill.BtnClick += new EventHandler(btnSaveAndOpenBill_BtnClick);
            //btnHistory.BtnClick += new EventHandler(btnHistory_BtnClick);
            //btnMakeCard.BtnClick += new EventHandler(btnMakeCard_BtnClick);
        }

        private void SetCombCtrls()
        {

            combSuperiorDoctor.valueControl.BoxStyle = ComboBoxStyle.DropDownList;//模糊查询
            combDoctorGroup.valueControl.BoxStyle = ComboBoxStyle.DropDownList;//模糊查询
            combSuperiorPhysicist.valueControl.BoxStyle = ComboBoxStyle.DropDownList;//模糊查询
            combPhysicistGroup.valueControl.BoxStyle = ComboBoxStyle.DropDownList;//模糊查询
            List<DataGridViewColumnEntity> lstCulumns = new List<DataGridViewColumnEntity>();
            var col = new DataGridViewColumnEntity() { DataField = "Value", HeadText = "开发", Width = combSuperiorDoctor.valueControl.Width, WidthType = SizeType.Absolute, TextAlign = ContentAlignment.MiddleLeft };
            lstCulumns.Add(col);
            //上级开发
            this.combSuperiorDoctor.valueControl.GridColumns = lstCulumns;
            this.combSuperiorDoctor.valueControl.IsShowHead = false;
            this.combSuperiorDoctor.valueControl.TextField = "Value";


            //开发组
            this.combDoctorGroup.valueControl.GridColumns = lstCulumns;
            this.combDoctorGroup.valueControl.IsShowHead = false;
            this.combDoctorGroup.valueControl.TextField = "Value";
            //上级产品
            this.combSuperiorPhysicist.valueControl.GridColumns = new List<DataGridViewColumnEntity>() {
            new DataGridViewColumnEntity() { DataField = "Value", HeadText = "全选", Width = combSuperiorPhysicist.valueControl.Width, WidthType = SizeType.Absolute, TextAlign = ContentAlignment.MiddleLeft }};
            //this.combSuperiorPhysicist.valueControl.IsShowHead = false;
            this.combSuperiorPhysicist.valueControl.KeyField = "Key";
            this.combSuperiorPhysicist.valueControl.TextField = "Value";
            //产品组
            this.combPhysicistGroup.valueControl.GridColumns = lstCulumns;
            this.combPhysicistGroup.valueControl.IsShowHead = false;
            this.combPhysicistGroup.valueControl.TextField = "Value";


            combRole.valueControl.BoxStyle = ComboBoxStyle.DropDownList;

            //上级开发
            var dls = MockData.Users.Where(u => u.Title_ID == UserTitle.开发.AsInt()).Where(u => (u.RowStatus == 1 && u.IsActive.AsInt() == 1)).Select(d => new KeyValuePair<string, string>(d.UserID.ToString(), d.RealName)).OrderBy(l => l.Value).ToList();
            dls.Insert(0, FormHelper.NullItem);
            combSuperiorDoctor.valueControl.GridDataSource = dls;


            //开发组
            var docGroups = MockData.SystemDic.Where(s => s.DicGroupCode == "开发组").Select(dg => new KeyValuePair<string, string>(dg.SysVal, dg.SysDes)).ToList();
            docGroups.Insert(0, FormHelper.NullItem);
            combDoctorGroup.valueControl.GridDataSource = docGroups;


            //上级产品 
            var pyls = PublicRes.GetUsersByTitleID(UserTitle.产品).Where(u => (u.RowStatus == 1 && u.IsActive.AsInt() == 1)).Select(d => new KeyValuePair<string, string>(d.UserID.ToString(), d.RealName)).OrderBy(l => l.Value).ToList();
            //pyls.Insert(0, FormHelper.NullItem);
            combSuperiorPhysicist.valueControl.GridDataSource = pyls.Cast<object>().ToList();


            //产品组
            var pyGroups = MockData.SystemDic.Where(s => s.DicGroupCode == "产品组").Select(w => new KeyValuePair<string, string>(w.SysVal.AsString(), w.SysDes)).ToList();
            pyGroups.Insert(0, FormHelper.NullItem);
            combPhysicistGroup.valueControl.GridDataSource = pyGroups;

            //角色
            FormHelper.BindComboBoxByDic("角色", combRole.valueControl, false);
        }
        void frmUserDetailInfo_SizeChanged(object sender, EventArgs e)
        {
            btnSave.Location = new Point((this.Width - btnSave.Width) / 2 - btnSave.Width, btnSave.Location.Y);
            btnReturn.Location = new Point((this.Width - btnSave.Width) / 2 + btnSave.Width, btnSave.Location.Y);
        }

        private void btnReturn_BtnClick(object sender, EventArgs e)
        {
            this.Close();
        }
        //List<Model.UsersEx> userexs = new List<Model.UsersEx>();

        private void btnSave_BtnClick(object sender, EventArgs e)
        {
            txtPhone.valueControl.InputText = txtPhone.valueControl.InputText.TrimEnd(';', '；').Replace("；", ";");
            if (!verification.Verification()) return;

            //更新用户信息
            var newP = formInputItems.GetNewTByControlValues();
            //特殊字段处理
            //上级开发
            newP.SuperiorDoctor = combSuperiorDoctor.valueControl.SelectedText;
            //上级产品
            //newP.SuperiorPhysicist = combSuperiorPhysicist.valueControl.SelectedText;

            //修改，对比差异，新增差异历史记录，生成差异字段更新sql
            var diffProps = FormHelper.GetDiffPropertyBy2O(UpdateUser, newP, formInputItems.Items.Where(i => !string.IsNullOrWhiteSpace(i.FieldName)).Select(i => i.FieldName)).ToList();


            //上级产品
            var svs = combSuperiorPhysicist.valueControl.SelectedValue;
            var sts = combSuperiorPhysicist.valueControl.SelectedText;

            if (diffProps.Count == 0)
            {
                //FrmShadowDialog.ShowWarningDialog(this, "数据未修改", "提示", false);
                this.Close();
                return;
            }
            if (diffProps.Contains("SuperiorDoctorID"))
                diffProps.Add("SuperiorDoctor");
            //if (diffProps.Contains("SuperiorPhysicistID"))
            //    diffProps.Add("SuperiorPhysicist");



            var usql = FormHelper.GetUpdateSqlByFields("Users", diffProps, "UserID");
            newP.UserID = UpdateUser.UserID;
            newP.EditUser = PublicRes.CurUser.UserID + PublicRes.CurUser.RealName;
            var sql = FormHelper.AddUpdateDtTmAndUser(usql);
            this.Close();
            var selecedSupPhysicistIds = this.combSuperiorPhysicist.GetSelectValue().AsString("");
                ////修改的当前登录用户，则刷新一下
                //if (PublicRes.CurUser.UserID == UpdateUser.UserID)
                //    ClientCache.Instance.SetLoginUserInfo(UpdateUser);
                //this.Close();
                //FrmTips.ShowTipsSuccess(this.MainForm, "更新成功");
        }
    }
}

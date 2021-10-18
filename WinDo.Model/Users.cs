using System;
namespace WinDo.Model
{
    [Serializable]
    public partial class Users
    {
        public Users()
        { }
        #region Model
        private int _userid;
        private string _loginname;
        private string _realname;
        private string _pwd;
        private int? _department_id;
        private int? _doctorgroup_id;
        private int? _physicistgroup_id;
        private int? _role_id;
        private int? _title_id;
        private int? _mosaiq_userid;
        private int? _professor;
        private string _id_code;
        private string _his_empl_code;
        private string _his_dept_code;
        private string _his_dept_name;
        private int? _cancelapp;
        private string _machineworkgroup;
        private string _phone;
        private string _usersex;
        private string _usersignpic;
        private string _wardcode;
        private string _wardsubcode;
        private string _watson_userid;
        private string _superiorphysicist;
        private string _superiordoctor;
        private int? _superiorphysicistid;
        private int? _superiordoctorid;
        private string _homephone;
        private string _employeetype;
        private string _remark;
        private int? _isactive;
        private string _employeetypecode;
        private string _doctorgroupname;
        private string _physicistgroupname;
        private string _rolename;
        private DateTime? _createdttm;
        private string _createuser;
        private DateTime? _editdttm;
        private string _edituser;
        private int? _rowstatus = 1;
        /// <summary>
        /// 
        /// </summary>
        public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LoginName
        {
            set { _loginname = value; }
            get { return _loginname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RealName
        {
            set { _realname = value; }
            get { return _realname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PWD
        {
            set { _pwd = value; }
            get { return _pwd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Department_ID
        {
            set { _department_id = value; }
            get { return _department_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? DoctorGroup_ID
        {
            set { _doctorgroup_id = value; }
            get { return _doctorgroup_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? PhysicistGroup_ID
        {
            set { _physicistgroup_id = value; }
            get { return _physicistgroup_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Role_ID
        {
            set { _role_id = value; }
            get { return _role_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Title_ID
        {
            set { _title_id = value; }
            get { return _title_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Professor
        {
            set { _professor = value; }
            get { return _professor; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ID_Code
        {
            set { _id_code = value; }
            get { return _id_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Phone
        {
            set { _phone = value; }
            get { return _phone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserSex
        {
            set { _usersex = value; }
            get { return _usersex; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserSignPic
        {
            set { _usersignpic = value; }
            get { return _usersignpic; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SuperiorPhysicist
        {
            set { _superiorphysicist = value; }
            get { return _superiorphysicist; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SuperiorDoctor
        {
            set { _superiordoctor = value; }
            get { return _superiordoctor; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SuperiorPhysicistID
        {
            set { _superiorphysicistid = value; }
            get { return _superiorphysicistid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SuperiorDoctorID
        {
            set { _superiordoctorid = value; }
            get { return _superiordoctorid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HomePhone
        {
            set { _homephone = value; }
            get { return _homephone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsActive
        {
            set { _isactive = value; }
            get { return _isactive; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DoctorGroupName
        {
            set { _doctorgroupname = value; }
            get { return _doctorgroupname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PhysicistGroupName
        {
            set { _physicistgroupname = value; }
            get { return _physicistgroupname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RoleName
        {
            set { _rolename = value; }
            get { return _rolename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateDtTm
        {
            set { _createdttm = value; }
            get { return _createdttm; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreateUser
        {
            set { _createuser = value; }
            get { return _createuser; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? EditDtTm
        {
            set { _editdttm = value; }
            get { return _editdttm; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EditUser
        {
            set { _edituser = value; }
            get { return _edituser; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? RowStatus
        {
            set { _rowstatus = value; }
            get { return _rowstatus; }
        }
        #endregion Model
        public string Token { get; set; }
    }
}


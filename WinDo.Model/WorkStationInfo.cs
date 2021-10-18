using System;
namespace WinDo.Model
{
	[Serializable]
	public partial class WorkStationInfo
	{
		public WorkStationInfo()
		{}
        #region Model
        private int _id;
        private string _ipaddress;
        private string _macaddress;
        private string _department;
        private string _workstationname;
        private string _address;
        private string _phone;
        private string _users;
        private string _remark;
        private string _userfield1;
        private string _userfield2;
        private string _userfield3;
        private string _userfield4;
        private string _userfield5;
        private DateTime? _create_dttm;
        private string _create_user;
        private DateTime? _edit_dttm;
        private string _edit_user;
        private int? _rowstatus = 1;
        private string _guid = Guid.NewGuid().ToString();
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IPAddress
        {
            set { _ipaddress = value; }
            get { return _ipaddress; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MACAddress
        {
            set { _macaddress = value; }
            get { return _macaddress; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Department
        {
            set { _department = value; }
            get { return _department; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WorkStationName
        {
            set { _workstationname = value; }
            get { return _workstationname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
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
        public string Users
        {
            set { _users = value; }
            get { return _users; }
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
        public string UserField1
        {
            set { _userfield1 = value; }
            get { return _userfield1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserField2
        {
            set { _userfield2 = value; }
            get { return _userfield2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserField3
        {
            set { _userfield3 = value; }
            get { return _userfield3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserField4
        {
            set { _userfield4 = value; }
            get { return _userfield4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserField5
        {
            set { _userfield5 = value; }
            get { return _userfield5; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? Create_DtTm
        {
            set { _create_dttm = value; }
            get { return _create_dttm; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Create_User
        {
            set { _create_user = value; }
            get { return _create_user; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? Edit_DtTm
        {
            set { _edit_dttm = value; }
            get { return _edit_dttm; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Edit_User
        {
            set { _edit_user = value; }
            get { return _edit_user; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? RowStatus
        {
            set { _rowstatus = value; }
            get { return _rowstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GUID
        {
            set { _guid = value; }
            get { return _guid; }
        }
        #endregion Model

	}
}


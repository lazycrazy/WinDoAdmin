using System;
namespace WinDo.Model
{
	[Serializable]
	public partial class Config
	{
		public Config()
		{}
        #region Model
        private int _id;
        private string _ckey;
        private string _value;
        private string _note;
        private string _cgroup;
        private string _cname;
        private DateTime? _create_dttm;
        private string _create_user;
        private int? _userid;
        private string _username;
        private int? _type;
        private DateTime? _edit_dttm;
        private string _edit_user;
        private int? _rowstatus = 1;
        private string _guid=Guid.NewGuid().ToString();
        private string _usernote;
        private string _keyowner;
        private int? _keyvaluetype;
        private string _keyvalues;
        private int? _configway = 1;
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
        public string Ckey
        {
            set { _ckey = value; }
            get { return _ckey; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Value
        {
            set { _value = value; }
            get { return _value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Note
        {
            set { _note = value; }
            get { return _note; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CGroup
        {
            set { _cgroup = value; }
            get { return _cgroup; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CName
        {
            set { _cname = value; }
            get { return _cname; }
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
        public int? UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 1:服务器  2：工作站 3:用户
        /// </summary>
        public int? Type
        {
            set { _type = value; }
            get { return _type; }
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
        /// <summary>
        /// 
        /// </summary>
        public string UserNote
        {
            set { _usernote = value; }
            get { return _usernote; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string KeyOwner
        {
            set { _keyowner = value; }
            get { return _keyowner; }
        }
        /// <summary>
        /// 1：文本，2下拉
        /// </summary>
        public int? KeyValueType
        {
            set { _keyvaluetype = value; }
            get { return _keyvaluetype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string KeyValues
        {
            set { _keyvalues = value; }
            get { return _keyvalues; }
        }
        /// <summary>
        /// 1、允许配置设置界面配置 2、其它设置界面配置
        /// </summary>
        public int? ConfigWay
        {
            set { _configway = value; }
            get { return _configway; }
        }
        #endregion Model

	}
   
}


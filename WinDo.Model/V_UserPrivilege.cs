using System;
namespace WinDo.Model
{
	[Serializable]
	public partial class V_UserPrivilege
	{
		public V_UserPrivilege()
		{}
        #region Model
        private string _loginname;
        private string _realname;
        private int _userid;
        private int _privilege_id;
        private string _privilege_code;
        private string _privilege_name;
        private string _module_code;
        private int? _isenabled;
        private int? _isactive;
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
        public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Privilege_ID
        {
            set { _privilege_id = value; }
            get { return _privilege_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Privilege_Code
        {
            set { _privilege_code = value; }
            get { return _privilege_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Privilege_Name
        {
            set { _privilege_name = value; }
            get { return _privilege_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Module_Code
        {
            set { _module_code = value; }
            get { return _module_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsEnabled
        {
            set { _isenabled = value; }
            get { return _isenabled; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsActive
        {
            set { _isactive = value; }
            get { return _isactive; }
        }
        #endregion Model

	}
}


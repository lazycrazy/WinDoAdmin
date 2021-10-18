using System;
namespace WinDo.Model
{
	[Serializable]
	public partial class SystemDic
	{
		public SystemDic()
		{}
        #region Model
        private int _id;
        private string _dicgroupcode;
        private string _systype;
        private string _sysdes;
        private string _sysval;
        private string _sysval2;
        private string _sysval3;
        private string _dicnote;
        private string _remark;
        private int? _userid;
        private int? _dicorder;
        private int? _rowstatus;
        private string _guid = Guid.NewGuid().ToString();
        private DateTime? _create_dttm;
        private string _create_user;
        private DateTime? _edit_dttm;
        private string _edit_user;
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
        public string DicGroupCode
        {
            set { _dicgroupcode = value; }
            get { return _dicgroupcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SysType
        {
            set { _systype = value; }
            get { return _systype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SysDes
        {
            set { _sysdes = value; }
            get { return _sysdes; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SysVal
        {
            set { _sysval = value; }
            get { return _sysval; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SysVal2
        {
            set { _sysval2 = value; }
            get { return _sysval2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SysVal3
        {
            set { _sysval3 = value; }
            get { return _sysval3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DicNote
        {
            set { _dicnote = value; }
            get { return _dicnote; }
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
        public int? UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? DicOrder
        {
            set { _dicorder = value; }
            get { return _dicorder; }
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
        #endregion Model

	}
}


using System;
namespace WinDo.Model
{
	[Serializable]
	public partial class SystemGroupDic
	{
		public SystemGroupDic()
		{}
 

        #region Model
        private int _systemgroupdicid;
		private int? _type;
		private string _dicgroupcode;
		private string _dicgroupname;
		private string _dicgroupnote;
		private string _module;
		private string _username;
		private string _usernote;
		private DateTime? _editdttm;
		private int? _dicnum;
		private string _remark;
		private int _rowstatus;
        private string _guid = Guid.NewGuid().ToString();
		private DateTime? _create_dttm;
		private string _create_user;
		private DateTime? _edit_dttm;
		private string _edit_user;
		/// <summary>
		/// 
		/// </summary>
		public int SystemGroupDicID
		{
			set{ _systemgroupdicid=value;}
			get{return _systemgroupdicid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DicGroupCode
		{
			set{ _dicgroupcode=value;}
			get{return _dicgroupcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DicGroupName
		{
			set{ _dicgroupname=value;}
			get{return _dicgroupname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DicGroupNote
		{
			set{ _dicgroupnote=value;}
			get{return _dicgroupnote;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Module
		{
			set{ _module=value;}
			get{return _module;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserNote
		{
			set{ _usernote=value;}
			get{return _usernote;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditDtTm
		{
			set{ _editdttm=value;}
			get{return _editdttm;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? DicNum
		{
			set{ _dicnum=value;}
			get{return _dicnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int RowStatus
		{
			set{ _rowstatus=value;}
			get{return _rowstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GUID
		{
			set{ _guid=value;}
			get{return _guid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Create_DtTm
		{
			set{ _create_dttm=value;}
			get{return _create_dttm;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Create_User
		{
			set{ _create_user=value;}
			get{return _create_user;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Edit_DtTm
		{
			set{ _edit_dttm=value;}
			get{return _edit_dttm;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Edit_User
		{
			set{ _edit_user=value;}
			get{return _edit_user;}
		}
		#endregion Model

	}
}


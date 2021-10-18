using System;
namespace WinDo.Model
{
	[Serializable]
	public partial class ModuleInfo
	{
		public ModuleInfo()
		{}
		#region Model
		private int _moduleid;
		private string _modulecode;
        private string _refmodulecode;
		private string _moduleparentcode;
		private string _moduleformcode;
		private string _modulename;
		private string _category;
		private int _moduletype;
		private string _imageindex;
		private string _formname;
		private string _assemblyname;
		private int _ismenu;
		private int? _sortcode;
		private int _isenabled=1;
		private string _description;
		private DateTime _createdttm;
		private string _createuser;
		private DateTime? _editdttm;
		private string _edituser;
		/// <summary>
		/// 
		/// </summary>
		public int ModuleID
		{
			set{ _moduleid=value;}
			get{return _moduleid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ModuleCode
		{
			set{ _modulecode=value;}
			get{return _modulecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ModuleParentCode
		{
			set{ _moduleparentcode=value;}
			get{return _moduleparentcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ModuleFormCode
		{
			set{ _moduleformcode=value;}
			get{return _moduleformcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ModuleName
		{
			set{ _modulename=value;}
			get{return _modulename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Category
		{
			set{ _category=value;}
			get{return _category;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ModuleType
		{
			set{ _moduletype=value;}
			get{return _moduletype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ImageIndex
		{
			set{ _imageindex=value;}
			get{return _imageindex;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FormName
		{
			set{ _formname=value;}
			get{return _formname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AssemblyName
		{
			set{ _assemblyname=value;}
			get{return _assemblyname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsMenu
		{
			set{ _ismenu=value;}
			get{return _ismenu;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? SortCode
		{
			set{ _sortcode=value;}
			get{return _sortcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsEnabled
		{
			set{ _isenabled=value;}
			get{return _isenabled;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}

        /// <summary>
        /// 
        /// </summary>
        public string RefModuleCode
        {
            set { _refmodulecode = value; }
            get { return _refmodulecode; }
        }
		/// <summary>
		/// 
		/// </summary>
		public DateTime CreateDtTm
		{
			set{ _createdttm=value;}
			get{return _createdttm;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CreateUser
		{
			set{ _createuser=value;}
			get{return _createuser;}
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
		public string EditUser
		{
			set{ _edituser=value;}
			get{return _edituser;}
		}
		#endregion Model

	}
}


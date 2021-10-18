using System;
namespace WinDo.Model
{
	[Serializable]
	public partial class UserModule
	{
		public UserModule()
		{}
		#region Model
		private int _userid;
		private int _moduleid;
		private DateTime? _createdttm;
		private string _createuser;
		private DateTime? _editdttm;
		private string _edituser;
		/// <summary>
		/// 
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
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
		public DateTime? CreateDtTm
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


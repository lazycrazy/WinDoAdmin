using System;
namespace WinDo.Model
{
	[Serializable]
	public partial class UserPrivileges
	{
		public UserPrivileges()
		{}
		#region Model
		private int _userid;
		private int _privilege_id;
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
		public int Privilege_ID
		{
			set{ _privilege_id=value;}
			get{return _privilege_id;}
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


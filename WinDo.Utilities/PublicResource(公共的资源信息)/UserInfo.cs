using System;

namespace WinDo.Utilities
{
	/// <summary>
	/// UserInfo
	/// 用户类	
	/// <author>
	///		<name></name>
	///		<date></date>
	/// </author> 
	/// </summary>
    [Serializable]
	public class UserInfo
	{
		public UserInfo()
		{
		  
		}   

	    /// <summary>
	    /// 用户主键
	    /// </summary>		
        public string UserID { get; set; }           

	    /// <summary>
	    /// 用户登录编码
	    /// </summary>		
        public string LoginName { get; set; }

	    /// <summary>
	    /// 用户姓名
	    /// </summary>		
	    public string RealName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string PWD { get; set; }

	    /// <summary>
	    /// HIS用户工号
	    /// </summary>		
        public string HisEmplCode { get; set; }

	    /// <summary>
	    /// 开发组名称
	    /// </summary>		
        public string DoctorGroupName { get; set; }

        /// <summary>
        /// 开发组ID
        /// </summary>		
        public string DoctorGroupID { get; set; }

        /// <summary>
        /// 当前医院UID
        /// </summary>		
        public string HospitalUID { get; set; }

	    /// <summary>
	    /// 当前科室代码
	    /// </summary>		
        public string DepartmentID { get; set; }

	    /// <summary>
	    /// 当前科室名称
	    /// </summary>		
        public string DepartmentName { get; set; }

	    /// <summary>
	    /// 默认角色
	    /// </summary>		
	    public string RoleId { get; set; }	 

	    /// <summary>
	    /// 默认角色名称
	    /// </summary>		
        public string RoleName { get; set; }

	    /// <summary>
	    /// 第二登录用户
	    /// </summary>		
        public bool RealName2 { get; set; }

	    /// <summary>
	    /// 第三登录用户
	    /// </summary>		
        public string RealName3 { get; set; }

             /// <summary>
        /// CancelApp
	    /// </summary>		
        public string CancelApp { get; set; }

         /// <summary>
        /// Watson_UserID
	    /// </summary>		
        public string WatsonUserID { get; set; }

	    /// <summary>
	    /// IP地址
	    /// </summary>		
	    public string IPAddress { get; set; }

	    /// <summary>
	    /// MAC地址
	    /// </summary>		
	    public string MACAddress { get; set; }


        /// <summary>
        /// 产品组ID
        /// </summary>
        public string PhysicistGroupID { get; set; }

        /// <summary>
        /// 产品组名称
        /// </summary>
        public string PhysicistGroupName { get; set; }

	  
	}
}
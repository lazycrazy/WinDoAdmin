using System;
namespace WinDo.Model
{
	[Serializable]
	public partial class Privileges
	{
		public Privileges()
		{}
        #region Model
        private int _privilege_id;
        private string _privilege_code;
        private string _privilege_name;
        private string _module_code;
        private string _privilege_note;
        private int? _isenabled = 1;
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
        public string Privilege_Note
        {
            set { _privilege_note = value; }
            get { return _privilege_note; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsEnabled
        {
            set { _isenabled = value; }
            get { return _isenabled; }
        }
        #endregion Model

	}
}


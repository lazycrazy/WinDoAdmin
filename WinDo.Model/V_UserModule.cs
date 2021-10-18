using System;
namespace WinDo.Model
{
    [Serializable]
    public partial class V_UserModule
    {
        public V_UserModule()
        { }
        #region Model
        private string _loginname;
        private string _realname;
        private int _userid;
        private int _moduleid;
        private int? _isactive;
        private int _isenabled;
        private string _modulecode;
        private string _moduleparentcode;
        private string _moduleformcode;
        private string _modulename;
        private string _category;
        private int _moduletype;
        private string _imageindex;
        private string _assemblyname;
        private int _ismenu;
        private string _formname;
        private string _description;
        private int? _sortcode;

        public string Command { get; set; } = "OpenForm";
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
        public int ModuleID
        {
            set { _moduleid = value; }
            get { return _moduleid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsActive
        {
            set { _isactive = value; }
            get { return _isactive; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IsEnabled
        {
            set { _isenabled = value; }
            get { return _isenabled; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ModuleCode
        {
            set { _modulecode = value; }
            get { return _modulecode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ModuleParentCode
        {
            set { _moduleparentcode = value; }
            get { return _moduleparentcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ModuleFormCode
        {
            set { _moduleformcode = value; }
            get { return _moduleformcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ModuleName
        {
            set { _modulename = value; }
            get { return _modulename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Category
        {
            set { _category = value; }
            get { return _category; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ModuleType
        {
            set { _moduletype = value; }
            get { return _moduletype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ImageIndex
        {
            set { _imageindex = value; }
            get { return _imageindex; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AssemblyName
        {
            set { _assemblyname = value; }
            get { return _assemblyname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IsMenu
        {
            set { _ismenu = value; }
            get { return _ismenu; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FormName
        {
            set { _formname = value; }
            get { return _formname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SortCode
        {
            set { _sortcode = value; }
            get { return _sortcode; }
        }
        #endregion Model

    }
}
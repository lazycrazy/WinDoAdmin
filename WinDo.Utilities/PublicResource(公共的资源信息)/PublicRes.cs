using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WinDo.Utilities
{
    public class PublicRes
    {
        /// <summary>
        /// 上次选择文件的目录
        /// </summary>
        private static string priviousOpenedDirectory;
        public static string PriviousOpenedDirectory
        {
            get
            {
                var mycomputer = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";
                if (string.IsNullOrWhiteSpace(priviousOpenedDirectory))
                    return mycomputer;
                if (Directory.Exists(priviousOpenedDirectory))
                    return priviousOpenedDirectory;
                return mycomputer;
            }
            set
            {
                priviousOpenedDirectory = value;
            }
        }

        /// <summary>
        /// 当前登录用户信息
        /// </summary>
        public static WinDo.Model.Users CurUser { get; set; }
        public static List<WinDo.Model.ModuleInfo> lstModule { get; set; }
        public static List<WinDo.Model.Privileges> lstPrivilege { get; set; }
        public static List<WinDo.Model.SystemDic> lstSystemDic { get; set; }

        public static List<WinDo.Model.SystemDic> lstSystemdicSysType { get; set; }

        public static List<WinDo.Model.SystemGroupDic> lstSystemDicGroup { get; set; }
        public static List<WinDo.Model.Config> lstConfig { get; set; }
        public static List<WinDo.Model.Config> lstConfigCGroup { get; set; }
        public static List<WinDo.Model.Users> lstUser { get; set; }

        /// <summary>
        /// 当前用户菜单权限
        /// </summary>
        public static List<WinDo.Model.V_UserModule> lstUserModule { get; set; }
        /// <summary>
        /// 当前用户功能权限
        /// </summary>
        public static List<WinDo.Model.V_UserPrivilege> lstUserPrivilege { get; set; }

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <param name="Ckey"></param>
        /// <param name="DefaultValue"></param>
        /// <returns></returns>
        public static string GetConfig(string Ckey, string DefaultValue = default(string))
        {
            string strValue = DefaultValue;
            if (lstConfig.Exists(x => x.Ckey == Ckey))
            {
                strValue = lstConfig.Find(x => x.Ckey == Ckey).Value;
            }
            return strValue;
        }

        /// <summary>
        /// 获取字典
        /// </summary>
        /// <param name="SysType"></param>
        /// <returns></returns>
        public static List<WinDo.Model.SystemDic> GetSystemDic(string SysType)
        {
            return lstSystemDic.Where(x => x.SysType.Trim().ToLower() == SysType.Trim().ToLower()).OrderBy(x => x.DicOrder).ToList();
        }

        /// <summary>
        /// 根据类型和值获取字典对象
        /// </summary>
        /// <param name="SysType"></param>
        /// <param name="sysDes"></param>
        /// <returns></returns>
        public static WinDo.Model.SystemDic GetDicByTypeAndVal(string SysType, string sysVal)
        {
            return PublicRes.GetSystemDic(SysType).FirstOrDefault(d => d.SysVal == sysVal);
        }

        /// <summary>
        /// 判断用户是否存在权限
        /// </summary>
        /// <param name="PrivilegeCode">权限Code</param>
        /// <returns>true:有权限 false:无权限</returns>
        public static bool HasPrivilege(string PrivilegeCode)
        {
            return lstUserPrivilege.Exists(x => x.Privilege_Code.Trim().ToLower() == PrivilegeCode.Trim().ToLower());
        }

        private static string AdministratorStr = "administrator";
        /// <summary>
        /// 判断是否 Administrator 账户
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public static bool IsAdministrator(string loginName = "")
        {
            if (string.IsNullOrWhiteSpace(loginName))
                return CurUser.LoginName.ToLower() == AdministratorStr;

            return loginName.ToLower() == AdministratorStr;
        }



        /// <summary>
        /// 判断用户是否存在菜单权限
        /// </summary>
        /// <param name="PrivilegeCode">权限名</param>
        /// <returns>true:有权限 false:无权限</returns>
        public static bool HasMenu(string code)
        {
            return lstUserModule.Exists(x => x.ModuleCode.Trim().ToLower() == code.Trim().ToLower());
        }

        /// <summary>
        /// 根据职务ID获取用户列表
        /// </summary>
        /// <param name="titleDesc"></param>
        /// <returns></returns>
        public static List<Model.Users> GetUsersByTitleID(string titleID)
        {
            return lstUser.Where(u => u.Title_ID.AsString() == titleID).OrderBy(u => u.RealName).ToList();
        }

    }
}

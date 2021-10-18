using WinDo.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using WinDo.Utilities.Mock;

namespace WinDo.Utilities
{
    /// <summary>
    /// ClientCache
    /// 客户端缓存功能   
    /// </summary>
    public class ClientCache
    {
        private static ClientCache instance = null;
        private static object locker = new Object();

        public static ClientCache Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                        {
                            instance = new ClientCache();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// 初始化资源
        /// </summary>
        public void InitResource()
        {
            InitUserList();
            InitSystemDicList();
            InitConfigList();
            InitPrivilegesList();
            InitModulesList();
        }

        public void RefreshMenuAndPrivileges()
        {
            InitPrivilegesList();
            InitModulesList();
            RefreshUserMenuAndPrivileges();
        }
        void RefreshUserMenuAndPrivileges()
        {
            WinDo.Utilities.PublicRes.lstUserModule = MockData.UserModules.Where(i => i.UserID == PublicRes.CurUser.UserID).ToList();
            WinDo.Utilities.PublicRes.lstUserPrivilege = MockData.UserPrivileges.Where(i => i.UserID == PublicRes.CurUser.UserID).ToList();
        }

        public void SetLoginUserInfo(Model.Users user)
        {
            WinDo.Utilities.PublicRes.CurUser = user;
            RefreshUserMenuAndPrivileges();
        }

        private void InitModulesList()
        {
            WinDo.Utilities.PublicRes.lstModule = MockData.Modules;
        }

        private void InitPrivilegesList()
        {
            WinDo.Utilities.PublicRes.lstPrivilege = MockData.Privileges;
        }

        public void InitUserList()
        {
            WinDo.Utilities.PublicRes.lstUser = MockData.Users;
        }

        public void InitSystemDicList()
        {
            WinDo.Utilities.PublicRes.lstSystemDic = MockData.SystemDic;
            WinDo.Utilities.PublicRes.lstSystemDicGroup = MockData.SystemDicGroup;

        }

        public void InitConfigList()
        {
            var mac = WinDo.Utilities.MachineInfoHelper.GetMacAddress();
            var configs = MockData.Configs.Where(c =>
            {
                if (c.Type == 1)
                    return true;

                if (c.Type == 2 && c.KeyOwner == mac)
                    return true;

                if (c.Type == 3 && c.KeyOwner == PublicRes.CurUser.UserID.ToString())
                    return true;
                return false;
            }).ToList();
            WinDo.Utilities.PublicRes.lstConfig = configs;
            WinDo.Utilities.PublicRes.lstConfigCGroup = configs.Where(c => c.ConfigWay == 1 && c.CGroup != null)
                .Select(c => c.CGroup).Distinct()
                .Select(c => new Model.Config() { CGroup = c }).ToList();
        }
    }
}

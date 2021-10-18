using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WinDo.Utilities.PublicLibrary;
using WinDo.Utilities.PublicResource;

namespace WinDo.Utilities.Mock
{
    public static class MockApi
    {
        public static Model.MsgInfo<Model.Users> Login(string userName, string pwd)
        {
            var user = MockData.Users.FirstOrDefault(u => u.LoginName == userName);
            if (user != null)
                user.Token = Guid.NewGuid().ToString();
            return new Model.MsgInfo<Model.Users>()
            {
                Code = (user == null ? ErrCode.用户不存在 : null),
                Msg = (user == null ? nameof(ErrCode.用户不存在) : null),
                Entity = user
            };
        }
        /// <summary>
        /// 调用API
        /// </summary>
        /// <returns></returns>
        public static object InvokeWebAPI()
        {
            var token = PublicRes.CurUser?.Token;
            if (string.IsNullOrWhiteSpace(token))
            {
                //退回到登录页面，重新登录
            }
            var rs = WebAPIHelper.PostWithToken("Api地址", "json请求数据", token);
            //可以对rs json反序列化为定义的类型
            return rs;
        }
    }

    /// <summary>
    /// Mock数据
    /// </summary>
    public static class MockData
    {
        /// <summary>
        /// 加载所有数据
        /// </summary>
        public static void LoadData()
        {
            foreach (var field in typeof(MockData).GetFields())
            {
                Load(field.Name);
            }
        }

        public static void Load(string fieldName)
        {
            var field = typeof(MockData).GetField(fieldName);
            var t = field.FieldType;
            var method = typeof(JsonHelper).GetMethod(nameof(JsonHelper.LoadJsonData));
            var generic = method.MakeGenericMethod(t);
            var data = generic.Invoke(null, new[] { fieldName });
            field.SetValue(field, data);
        }
        public static void Save(string propName)
        {
            var prop = typeof(MockData).GetField(propName);
            var data = prop.GetValue(prop);
            JsonHelper.SaveJsonData(propName, data);
            ClientCache.Instance.InitResource();
        }

        public static List<Model.Users> Users = new List<Model.Users>();
        public static List<Model.ModuleInfo> Modules = new List<Model.ModuleInfo>();
        public static List<Model.Privileges> Privileges = new List<Model.Privileges>();
        public static List<Model.V_UserModule> UserModules = new List<Model.V_UserModule>();
        public static List<Model.V_UserPrivilege> UserPrivileges = new List<Model.V_UserPrivilege>();
        public static List<Model.UserManageInfo> UserManageInfos = new List<Model.UserManageInfo>();

        public static List<Model.SystemDic> SystemDic = new List<Model.SystemDic>();
        public static List<Model.SystemGroupDic> SystemDicGroup = new List<Model.SystemGroupDic>();
        public static List<Model.Config> Configs = new List<Model.Config>();

    }
}

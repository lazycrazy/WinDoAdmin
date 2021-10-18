using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using WinDo.Utilities;
using WinDoControls.Controls;
using WinDoControls.Forms;

namespace WinDo.UI
{

    /// <summary>
    /// 窗体帮助类
    /// </summary>
    public static class FormHelper
    {

        public static void BindComboBoxByDic(string sysType, WDCombox comboBox, bool addEmpty = true)
        {
            var os = PublicRes.GetSystemDic(sysType).OrderBy(o => o.DicOrder).Select(o => new KeyValuePair<string, string>(o.SysVal, o.SysDes)).ToList();
            if (addEmpty)
            {
                os.Insert(0, NullItem);
            }
            comboBox.Source = os;
        }

        public static KeyValuePair<string, string> NullItem = new KeyValuePair<string, string>("", "请选择");

        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, IntPtr lParam);
        private const int WM_SETREDRAW = 0xB;


        public static void RefreshCtrl(System.Windows.Forms.Control ctrl, Action action)
        {
            SendMessage(ctrl.Handle, WM_SETREDRAW, 0, IntPtr.Zero);//禁止重绘
                                                                   // 重新布局
            action();
            SendMessage(ctrl.Handle, WM_SETREDRAW, 1, IntPtr.Zero);//取消禁止
            ctrl.Refresh();
        }

        /// <summary>
        /// 调试
        /// </summary>
        public static bool IsDebug
        {
            get
            {
                return File.Exists(Directory.GetCurrentDirectory() + "\\DEBUG");
            }
        }

        public static string CurrentVersion = "";

        public static Screen CurrentScreen
        {
            get
            {
                return Screen.FromControl(FormHelper.MainForm);
            }
        }

        public static bool Is1280
        {
            get
            {
                return CurrentScreen.WorkingArea.Width <= 1280;
            }
        }


        public static string UserHomePageKey = "C_HOMEPAGE_USER";
        public static string ClientHomePageKey = "C_HOMEPAGE_CLIENT";

        public static BaseForm MainForm;
        public static FlowLayoutPanel TabsPanel;


        #region 表单页面控件处理



        public static void SetControlsReadonly(List<System.Windows.Forms.Control> ctrls, bool readOnly = true)
        {
            foreach (var item in ctrls)
            {
                var rop = item.GetType().GetProperty("ReadOnly");
                if (rop == null) continue;
                rop.SetValue(item, readOnly, null);
            }
        }



        public static void SetCtrlRegxNone(VerificationComponent ver, System.Windows.Forms.Control ctrl)
        {
            ver.SetVerificationModel(ctrl, WinDoControls.Controls.VerificationModel.None);
        }

        public static void SetCtrlRegx(VerificationComponent ver, System.Windows.Forms.Control ctrl, string regex, string regexErrorMsg)
        {
            ver.SetVerificationModel(ctrl, WinDoControls.Controls.VerificationModel.Custom);
            //获取控件属性值再设置
            ver.SetVerificationCustomRegex(ctrl, regex);
            //获取控件属性值再设置
            ver.SetVerificationErrorMsg(ctrl, regexErrorMsg);
        }
        public static void SetCtrlRequired(VerificationComponent ver, System.Windows.Forms.Control ctrl)
        {
            //ver.SetVerificationModel(ctrl, ver.GetVerificationModel(ctrl));
            ver.SetVerificationRequired(ctrl, true);
        }

        public static string GetUpdateSqlByFields(string tablename, IEnumerable<string> props, string pk, Dictionary<string, string> propVsFieldName = null)
        {
            if (props.Count() == 0) return "";
            var us = props.Aggregate("", (p, c) => p + "," + (propVsFieldName != null && propVsFieldName.ContainsKey(c) ? propVsFieldName[c] : c) + "=@" + c).TrimStart(',');
            var transFields = "";
            if (propVsFieldName != null)
            {
                transFields = ", " + string.Join(",", propVsFieldName.Select(pf => pf.Value + " " + pf.Key));
            }
            return "update " + tablename + " set " + us + " where " + pk + "=@" + pk + " select t.* " + transFields + " from  " + tablename + " t where " + pk + "=@" + pk;
        }

        public static string GetInsertSqlByFields(string tablename, IEnumerable<string> fields, string pk, Dictionary<string, string> fieldNameVSProp = null)
        {
            if (fields.Count() == 0) return "";
            var props = fields;
            if (fieldNameVSProp != null)
                props = fields.Select(f => fieldNameVSProp.ContainsKey(f) ? fieldNameVSProp[f] : f);
            var transFields = "";
            if (fieldNameVSProp != null)
            {
                transFields = ", " + string.Join(",", fieldNameVSProp.Select(pf => pf.Key + " " + pf.Value));
            }
            return "insert into " + tablename + "( " + string.Join(",", fields) + ") values (@" + string.Join(",@", props) + " ) SELECT t.* " + transFields + " FROM " + tablename + " t WHERE " + pk + " = SCOPE_IDENTITY()";
        }
        public static string AddCreateDtTmAndUser(string isql, string CreateDtTm = "CreateDtTm", string CreateUser = "CreateUser", string EditDtTm = "EditDtTm", string EditUser = "EditUser")
        {
            var ifield = "," + CreateDtTm + "," + CreateUser + "," + EditDtTm + "," + EditUser;
            var ivalue = ",getdate(),@" + CreateUser + ",getdate(),@" + EditUser;
            var istr = isql.Insert(isql.IndexOf(") values"), ifield);
            return istr.Insert(istr.IndexOf(") SELECT"), ivalue);
        }
        public static string AddUpdateDtTmAndUser(string isql, string EditDtTm = "EditDtTm", string EditUser = "EditUser")
        {
            var iset = "," + EditDtTm + "=getdate()," + EditUser + "=@" + EditUser;
            var istr = isql.Insert(isql.IndexOf(" where"), iset);
            return istr;
        }

        public static List<string> GetDiffPropertyBy2O(object originalObject, object changedObject, IEnumerable<string> propstrs, Dictionary<string, string> overrideProps = null)
        {
            var diffProps = new List<string>();

            var props = originalObject.GetType().GetProperties().Where(p => ((overrideProps != null && overrideProps.ContainsValue(p.Name)) || propstrs.Contains(p.Name)));
            foreach (var property in props)
            {
                object originalValue = property.GetValue(originalObject, null);
                object newValue = property.GetValue(changedObject, null);
                if (!object.Equals(originalValue, newValue))
                {
                    diffProps.Add(property.Name);
                }
            }
            return diffProps;
        }

        public static object InvokeGetMethod(object obj, string methodName)
        {
            var mt = obj.GetType().GetMethod(methodName);//加载方法
            return mt.Invoke(obj, null);
        }
        public static object InvokeGetMethod(object obj, string methodName, object value)
        {
            var mt = obj.GetType().GetMethod(methodName);//加载方法
            return mt.Invoke(obj, new[] { value });
        }
        public static void InvokeSetMethod(object obj, string methodName, object value)
        {
            var mt = obj.GetType().GetMethod(methodName);//加载方法
            mt.Invoke(obj, new object[] { value });//需要数据类型判断处理???
        }


        #endregion

        #region Tips
        public static void ShowTipsSuccess(string msg = "操作成功")
        {
            if (FormHelper.MainForm == null) return;
            FormHelper.MainForm.BeginInvoke((Action)(() => WinDoControls.Forms.FrmTips.ShowTipsSuccess(FormHelper.MainForm, msg)));
        }
        public static void ShowTipsError(string msg)
        {
            if (FormHelper.MainForm == null) return;
            FormHelper.MainForm.BeginInvoke((Action)(() => WinDoControls.Forms.FrmTips.ShowTipsError(FormHelper.MainForm, msg)));
        }
        #endregion


        /// <summary>
        /// 切换输入法
        /// </summary>
        /// <param name="cultureType">语言项，如zh-CN，en-US</param>
        public static void SwitchToLanguageMode(string cultureType)
        {
            var installedInputLanguages = InputLanguage.InstalledInputLanguages;

            if (installedInputLanguages.Cast<InputLanguage>().Any(i => i.Culture.Name == cultureType))
            {
                InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(System.Globalization.CultureInfo.GetCultureInfo(cultureType));
                //CurrentLanguage = cultureType;

            }
        }

        public static string GetCultureType()
        {
            var currentInputLanguage = InputLanguage.CurrentInputLanguage;
            var cultureInfo = currentInputLanguage.Culture;
            //同 cultureInfo.IetfLanguageTag;
            return cultureInfo.Name;
        }

        #region 接口调用 

        public static Image GetImageFromUrl(string url)
        {
            try
            {
                var request = WebRequest.Create(url);
                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    return Bitmap.FromStream(stream);
                }
            }
            catch (Exception ex)
            {
                //FormHelper.MainForm.BeginInvoke((Action)(() =>
                //{
                //    FrmShadowDialog.ShowErrDialog(FormHelper.MainForm, "从url加载图片失败" + ex.Message, "错误", false);
                //}));
                //FormHelper.ShowTipsError("从url加载图片失败" + ex.Message);
                return null;
            }

        }

        #endregion



    }

}

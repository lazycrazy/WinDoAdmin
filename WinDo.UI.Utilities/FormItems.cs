using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinDo.Utilities;
using WinDo.Utilities.PublicResource;
using WinDoControls.Controls;
using WinDoControls.Forms;

namespace WinDo.UI
{

    
    /// <summary>
    /// 表单项集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FormItems<T> where T : class, new()
    {
        public FormItems(VerificationComponent verification)
        {
            _verification = verification;
            _verification.Verificationed += new VerificationComponent.VerificationedHandle(verification_Verificationed);
        }

        /// <summary>
        /// 每个校验项处理结果后续处理，用来设置错误显示红色框，和最大化中文长度校验
        /// </summary>
        /// <param name="e"></param>
        void verification_Verificationed(VerificationEventArgs e)
        {
            if (e.IsVerifySuccess)
            {
                var errmsg = GetCheckMaxLengthErrMsg(e.VerificationControl);
                if (!string.IsNullOrWhiteSpace(errmsg))
                {
                    e.IsVerifySuccess = false;
                    e.ErrorMsg = errmsg;
                }
            }
            Type ctrlType = e.VerificationControl.GetType();
            //设置不能为空
            var propertyInfo = ctrlType.GetProperty("IsErrorColor"); //获取指定名称的属性
            if (propertyInfo == null) return;
            var isErrorColor = (propertyInfo.GetValue(e.VerificationControl, null)); //获取属性值 
            if (isErrorColor != null)
                propertyInfo.SetValue(e.VerificationControl, !e.IsVerifySuccess, null);

        }

        VerificationComponent _verification;
        public List<FormItem> Items { get { return _items; } }
        private List<FormItem> _items = new List<FormItem>();
        private Dictionary<System.Windows.Forms.Control, TextBoxEx> NeedCheckLengthCtrls = new Dictionary<System.Windows.Forms.Control, TextBoxEx>();

        public FormItem Add(FormItem item)
        {
            _items.Add(item);
            return item;
        }

        public void AddRange(IEnumerable<FormItem> collection)
        {
            _items.AddRange(collection);
        }

        public void RegisterMRR()
        {
            NeedCheckLengthCtrls.Clear();
            _verification.Clear();
            RegisterMaxLength();
            RegisterRegex();
            RegisterRequired();
        }

        public void RegisterMaxLength()
        {
            //注册最大长度限制
            var maxLengthCtrls = _items.Where(c => c.MaxLength > 0);
            foreach (FormItem item in maxLengthCtrls)
            {
                Type ctrlType = item.Ctrl.GetType();

                System.Windows.Forms.Control valueControl = null;
                var valueControlProp = ctrlType.GetField("valueControl"); //获取指定名称的属性
                if (valueControlProp != null)
                {
                    valueControl = (System.Windows.Forms.Control)valueControlProp.GetValue(item.Ctrl); //获取属性值 
                }
                else
                {
                    var valueControlProp1 = ctrlType.GetProperty("valueControl");
                    if (valueControlProp1 != null)
                    {
                        valueControl = (System.Windows.Forms.Control)valueControlProp1.GetValue(item.Ctrl, null); //获取属性值 
                    }

                }


                if (valueControl != null)
                {
                    if (valueControl is WDTextBoxEx)
                    {
                        var ctrl = valueControl as WDTextBoxEx;
                        if (ctrl != null)
                        {
                            //ctrl.txtInput.MaxLength = item.MaxLength * 2;
                            if (!NeedCheckLengthCtrls.ContainsKey(ctrl))
                                NeedCheckLengthCtrls.Add(ctrl, ctrl.txtInput);
                            FormHelper.SetCtrlRegxNone(_verification, ctrl);
                        }
                    }
                    else if (valueControl is WDCombox)
                    {
                        var ctrl = valueControl as WDCombox;
                        if (ctrl != null && ctrl.BoxStyle != ComboBoxStyle.DropDownList)
                        {
                            //ctrl.txtInput.MaxLength = item.MaxLength * 2;
                            if (!NeedCheckLengthCtrls.ContainsKey(ctrl))
                                NeedCheckLengthCtrls.Add(ctrl, ctrl.txtInput);
                            FormHelper.SetCtrlRegxNone(_verification, ctrl);
                        }
                    }
                    else if (valueControl is WDSearchBox)
                    {
                        var ctrl = valueControl as WDSearchBox;
                        if (ctrl != null)
                        {
                            //ctrl.txtInput.MaxLength = item.MaxLength * 2;
                            if (!NeedCheckLengthCtrls.ContainsKey(ctrl))
                                NeedCheckLengthCtrls.Add(ctrl, ctrl.txtInput);
                            FormHelper.SetCtrlRegxNone(_verification, ctrl);
                        }

                    }
                }

            }
        }
        public void RegisterRegex()
        {
            //注册正则验证
            var regCtrls = _items.Where(c => !string.IsNullOrWhiteSpace(c.RegexStr));
            foreach (FormItem item in regCtrls)
            {
                Type ctrlType = item.Ctrl.GetType();

                System.Windows.Forms.Control valueControl = null;
                var valueControlProp = ctrlType.GetField("valueControl"); //获取指定名称的属性
                if (valueControlProp != null)
                {
                    valueControl = (System.Windows.Forms.Control)valueControlProp.GetValue(item.Ctrl); //获取属性值 
                }
                else
                {
                    var valueControlProp1 = ctrlType.GetProperty("valueControl");
                    if (valueControlProp1 != null)
                    {
                        valueControl = (System.Windows.Forms.Control)valueControlProp1.GetValue(item.Ctrl, null); //获取属性值 
                    }

                }


                if (valueControl != null)
                    FormHelper.SetCtrlRegx(_verification, valueControl, item.RegexStr, item.RegexMsg);

            }
        }
        public void RegisterRequired()
        {
            //注册非空验证
            var requiredCtrls = _items.Where(c => c.Required && !(c.Ctrl is WDLabelCheckBox || c.Ctrl is WDLabelRadioButton));
            foreach (FormItem item in requiredCtrls)
            {
                if (item.Required)
                {
                    Type ctrlType = item.Ctrl.GetType();
                    System.Windows.Forms.Control valueControl = null;
                    var valueControlProp = ctrlType.GetField("valueControl"); //获取指定名称的属性
                    if (valueControlProp != null)
                    {
                        valueControl = (System.Windows.Forms.Control)valueControlProp.GetValue(item.Ctrl); //获取属性值 
                    }
                    else
                    {
                        var valueControlProp1 = ctrlType.GetProperty("valueControl");
                        if (valueControlProp1 != null)
                        {
                            valueControl = (System.Windows.Forms.Control)valueControlProp1.GetValue(item.Ctrl, null); //获取属性值 
                        }

                    }


                    //if (valueControlProp != null)
                    //{

                    if (valueControl != null)
                    {
                        FormHelper.SetCtrlRequired(_verification, valueControl);//注册非空校验
                        //注册值改变事件，为空时红色边框提示
                        //var vcType = valueControl.GetType();
                        //var eventInfo = vcType.GetEvent((valueControl is UCCombox) ? "TextChangedEvent" : "TextChanged");
                        //var handler = new EventHandler(valueControl_TextChanged);
                        //eventInfo.AddEventHandler(valueControl, handler);
                    }
                    //}
                }
            }
        }

        /// <summary>
        /// 控件值改变，不能为空，红框提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void valueControl_TextChanged(object sender, EventArgs e)
        {
            var ctrl = sender;// (sender is UCCombox) ? sender : (sender as System.Windows.Forms.Control).Parent;
            Type ctrlType = ctrl.GetType();

            //设置不能为空
            var propertyInfo = ctrlType.GetProperty("IsErrorColor"); //获取指定名称的属性
            if (propertyInfo == null) return;
            var isErrorColor = (propertyInfo.GetValue(ctrl, null)); //获取属性值 
            if (isErrorColor != null)
            {
                if (ctrl is WDTextBoxClear || ctrl is WDSearchBox)
                {
                    var txtProperty = ctrlType.GetProperty("InputText"); //获取指定名称的属性
                    if (txtProperty != null)
                    {
                        var txt = (string)(txtProperty.GetValue(ctrl, null));
                        propertyInfo.SetValue(ctrl, string.IsNullOrWhiteSpace(txt), null);
                    }
                }
                else if (ctrl is WDComboxGrid)
                {
                    var ictrl = (ctrl as WDComboxGrid);
                    propertyInfo.SetValue(ctrl, string.IsNullOrWhiteSpace(ictrl.SelectedValue), null);
                }
                else if (ctrl is WDCheckComboxGrid)
                {
                    var ictrl = (ctrl as WDCheckComboxGrid);
                    if (ictrl.SelectKeyValues != null)
                    {
                        var selectedRows = ictrl.GridDataSource.Where(r => ictrl.SelectKeyValues.Contains(r.GetType().GetProperty(ictrl.KeyField).GetValue(r, null).ToString()));
                        propertyInfo.SetValue(ctrl, selectedRows.Count() == 0, null);
                    }
                    else
                        propertyInfo.SetValue(ctrl, true, null);
                }
                else if (ctrl is WDCombox)
                {
                    var ictrl = (ctrl as WDCombox);
                    propertyInfo.SetValue(ctrl, string.IsNullOrWhiteSpace(ictrl.SelectedValue), null);
                }
            }
        }


        public string GetCheckMaxLengthErrMsg(System.Windows.Forms.Control ctrl)
        {
            if (!NeedCheckLengthCtrls.ContainsKey(ctrl)) return "";
            var txtCtrl = NeedCheckLengthCtrls[ctrl];
            var curLength = StringHelper.NumChar(txtCtrl.Text.Trim());
            var len = _items.First(i => i.Ctrl == ctrl.Parent).MaxLength;
            if (curLength > len)
                return string.Format("最多可输入{0}个汉字", len / 2); //curLength + " 超出最大 " + txtCtrl.MaxLength + "长度"; //(1个中文2个字符)
            return "";
        }



        public void ClearFormErrors()
        {
            //UCControlBaseWithError 清除错误提示框
            foreach (var item in _items)
            {
                Type ctrlType = item.Ctrl.GetType();
                if (ctrlType == typeof(WDLabelRadioButton) || ctrlType == typeof(WDLabelCheckBox))
                {
                    var propertyInfo = ctrlType.GetProperty("IsErrorColor"); //获取指定名称的属性
                    if (propertyInfo == null)
                        continue;
                    propertyInfo.SetValue(item.Ctrl, false, null);
                    continue;
                }
                var valueControlProp = ctrlType.GetField("valueControl"); //获取指定名称的属性
                if (valueControlProp != null)
                {
                    var valueControl = (System.Windows.Forms.Control)valueControlProp.GetValue(item.Ctrl); //获取属性值 
                    if (valueControl != null)
                    {
                        Type vType = valueControl.GetType();
                        //设置不能为空
                        var propertyInfo = vType.GetProperty("IsErrorColor"); //获取指定名称的属性
                        if (propertyInfo == null) continue;
                        propertyInfo.SetValue(valueControl, false, null);
                    }
                }
            }
        }
        private Color errorTipsBackColor = WDColors.ErrorTipRedColor;
        private Color errorTipsForeColor = Color.White;

        /// <summary>
        /// 检查必录的单选和多选是否选择
        /// </summary>
        /// <param name="checkItems"></param>
        /// <returns></returns>
        public bool HasRequiredRadioAndCheckBoxErrMsg()
        {
            bool hasErr = false;
            foreach (var item in _items.Where(ii => ii.Required
                && (ii.IsMultiControl || (ii.Ctrl is WDLabelRadioButton) || (ii.Ctrl is WDLabelCheckBox))))
            {
                if (item.Ctrl is WDLabelRadioButton)
                {
                    var lc = item.Ctrl as WDLabelRadioButton;
                    if (lc.EditControls.All(r => !r.Checked))
                    {
                        lc.IsErrorColor = true;
                        var tips = WinDoControls.Forms.FrmAnchorTips.ShowTips(lc, "必填项不能为空", AnchorTipsLocation.RIGHT, background: errorTipsBackColor, foreColor: errorTipsForeColor, autoCloseTime: 3000, blnTopMost: false);
                        hasErr = true;
                    }
                    else
                        lc.IsErrorColor = false;
                }
                else if (item.Ctrl is WDLabelCheckBox)
                {
                    var lc = item.Ctrl as WDLabelCheckBox;
                    if (lc.EditControls.All(r => !r.Checked))
                    {
                        var tips = WinDoControls.Forms.FrmAnchorTips.ShowTips(lc, "必填项不能为空", AnchorTipsLocation.RIGHT, background: errorTipsBackColor, foreColor: errorTipsForeColor, autoCloseTime: 3000, blnTopMost: false);
                        lc.IsErrorColor = true;
                        hasErr = true;
                    }
                    else
                        lc.IsErrorColor = false;
                }


            }
            return hasErr;
        }

        /// <summary>
        /// 根据页面控件上的值，返回新的实体对象
        /// </summary>
        /// <returns></returns>
        public T GetNewTByControlValues(bool isTemplate = false)
        {
            var items = _items;
            if (isTemplate)
                items = _items.Where(i => i.TemplateField).ToList();
            var newP = new T();
            //字符串类型可以使用动态赋值，
            foreach (var item in items)
            {
                if (string.IsNullOrWhiteSpace(item.FieldName))
                    continue;
                var value = FormHelper.InvokeGetMethod(item.Ctrl, "GetSelectValue");
                var propertyStr = string.IsNullOrWhiteSpace(item.PropertyName) ? item.FieldName : item.PropertyName;
                newP.SetPropertyValue(propertyStr, value);
            }
            return newP;
        }

        public void SetControlValuesByEntity(T t, bool isTemplate = false)
        {
            var items = _items;
            if (isTemplate)
                items = _items.Where(i => i.TemplateField).ToList();
            //字符串类型可以使用动态赋值，
            foreach (var item in items)
            {
                if (string.IsNullOrWhiteSpace(item.FieldName))
                    continue;
                var propertyStr = string.IsNullOrWhiteSpace(item.PropertyName) ? item.FieldName : item.PropertyName;
                FormHelper.InvokeSetMethod(item.Ctrl, "SetSelectValue", t.GetPropertyValue(propertyStr));
            }
        }


    }

}

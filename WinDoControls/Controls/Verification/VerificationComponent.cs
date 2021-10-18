using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WinDoControls.Forms;

namespace WinDoControls.Controls
{
    /// <summary>
    /// Class VerificationComponent.
    /// Implements the <see cref="System.ComponentModel.Component" />
    /// Implements the <see cref="System.ComponentModel.IExtenderProvider" />
    /// </summary>
    /// <seealso cref="System.ComponentModel.Component" />
    /// <seealso cref="System.ComponentModel.IExtenderProvider" />
    [ProvideProperty("VerificationModel", typeof(Control))]
    [ProvideProperty("VerificationCustomRegex", typeof(Control))]
    [ProvideProperty("VerificationRequired", typeof(Control))]
    [ProvideProperty("VerificationErrorMsg", typeof(Control))]
    [DefaultEvent("Verificationed")]
    public class VerificationComponent : Component, IExtenderProvider
    {
        /// <summary>
        /// Delegate VerificationedHandle
        /// </summary>
        /// <param name="e">The <see cref="VerificationEventArgs"/> instance containing the event data.</param>
        public delegate void VerificationedHandle(VerificationEventArgs e);
        /// <summary>
        /// Occurs when [verificationed].
        /// </summary>
        [Browsable(true), Category("自定义属性"), Description("验证事件"), Localizable(true)]
        public event VerificationedHandle Verificationed;

        /// <summary>
        /// The m control cache
        /// </summary>
        Dictionary<Control, VerificationModel> m_controlCache = new Dictionary<Control, VerificationModel>();
        /// <summary>
        /// The m control regex cache
        /// </summary>
        Dictionary<Control, string> m_controlRegexCache = new Dictionary<Control, string>();
        /// <summary>
        /// The m control required cache
        /// </summary>
        Dictionary<Control, bool> m_controlRequiredCache = new Dictionary<Control, bool>();
        Dictionary<Control, string> m_controlRequiredMsgCache = new Dictionary<Control, string>();
        /// <summary>
        /// The m control MSG cache
        /// </summary>
        Dictionary<Control, string> m_controlMsgCache = new Dictionary<Control, string>();
        /// <summary>
        /// The m control tips
        /// </summary>
        Dictionary<Control, Forms.FrmAnchorTips> m_controlTips = new Dictionary<Control, Forms.FrmAnchorTips>();

        /// <summary>
        /// The error tips back color
        /// </summary>
        private Color errorTipsBackColor = Color.FromArgb(230, ColorTranslator.FromHtml("#ff0000"));//ColorTranslator.FromHtml("#9f0000");251, 4, 4

        /// <summary>
        /// Gets or sets the color of the error tips back.
        /// </summary>
        /// <value>The color of the error tips back.</value>
        [Browsable(true), Category("自定义属性"), Description("错误提示背景色"), Localizable(true)]
        public Color ErrorTipsBackColor
        {
            get { return errorTipsBackColor; }
            set { errorTipsBackColor = value; }
        }

        /// <summary>
        /// The error tips fore color
        /// </summary>
        private Color errorTipsForeColor = Color.White;

        /// <summary>
        /// Gets or sets the color of the error tips fore.
        /// </summary>
        /// <value>The color of the error tips fore.</value>
        [Browsable(true), Category("自定义属性"), Description("错误提示文字颜色"), Localizable(true)]
        public Color ErrorTipsForeColor
        {
            get { return errorTipsForeColor; }
            set { errorTipsForeColor = value; }
        }

        private int autoCloseErrorTipsTime = 3000;

        [Browsable(true), Category("自定义属性"), Description("自动关闭提示事件，当值为0时不自动关闭"), Localizable(true)]
        public int AutoCloseErrorTipsTime
        {
            get { return autoCloseErrorTipsTime; }
            set
            {
                if (value < 0)
                    return;
                autoCloseErrorTipsTime = value;
            }
        }

        #region 构造函数    English:Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="VerificationComponent"/> class.
        /// </summary>
        public VerificationComponent()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VerificationComponent"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public VerificationComponent(IContainer container)
            : this()
        {
            container.Add(this);
        }
        #endregion

        #region 指定此对象是否可以将其扩展程序属性提供给指定的对象。    English:Specifies whether this object can provide its extender properties to the specified object.
        /// <summary>
        /// 指定此对象是否可以将其扩展程序属性提供给指定的对象。
        /// </summary>
        /// <param name="extendee">要接收扩展程序属性的 <see cref="T:System.Object" />。</param>
        /// <returns>如果此对象可以扩展程序属性提供给指定对象，则为 true；否则为 false。</returns>
        public bool CanExtend(object extendee)
        {
            if (extendee is TextBoxBase || extendee is WDTextBoxEx || extendee is ComboBox || extendee is WDCombox)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region 验证规则    English:Validation rule
        /// <summary>
        /// Gets the verification model.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns>VerificationModel.</returns>
        [Browsable(true), Category("自定义属性"), Description("验证规则"), DisplayName("VerificationModel"), Localizable(true)]
        public VerificationModel GetVerificationModel(Control control)
        {
            if (m_controlCache.ContainsKey(control))
            {
                return m_controlCache[control];
            }
            else
                return VerificationModel.None;
        }

        /// <summary>
        /// Sets the verification model.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="vm">The vm.</param>
        public void SetVerificationModel(Control control, VerificationModel vm)
        {
            m_controlCache[control] = vm;
        }

        public void RemoveRegexVerificationModel(Control control)
        {
            if (m_controlRegexCache.ContainsKey(control))
                m_controlRegexCache.Remove(control);
        }


        #endregion

        #region 自定义正则    English:Custom Rules
        /// <summary>
        /// Gets the verification custom regex.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns>System.String.</returns>
        [Browsable(true), Category("自定义属性"), Description("自定义验证正则表达式"), DisplayName("VerificationCustomRegex"), Localizable(true)]
        public string GetVerificationCustomRegex(Control control)
        {
            if (m_controlRegexCache.ContainsKey(control))
            {
                return m_controlRegexCache[control];
            }
            else
                return "";
        }

        /// <summary>
        /// Sets the verification custom regex.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="strRegex">The string regex.</param>
        public void SetVerificationCustomRegex(Control control, string strRegex)
        {
            m_controlRegexCache[control] = strRegex;
        }
        #endregion

        #region 必填    English:Must fill
        /// <summary>
        /// Gets the verification required.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [Browsable(true), Category("自定义属性"), Description("是否必填项"), DisplayName("VerificationRequired"), Localizable(true)]
        public bool GetVerificationRequired(Control control)
        {
            if (m_controlRequiredCache.ContainsKey(control))
                return m_controlRequiredCache[control];
            return false;
        }

        /// <summary>
        /// Sets the verification required.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="blnRequired">if set to <c>true</c> [BLN required].</param>
        public void SetVerificationRequired(Control control, bool blnRequired, string errMsg = "")
        {
            if (!m_controlCache.ContainsKey(control))
                SetVerificationModel(control, VerificationModel.None);
            m_controlRequiredCache[control] = blnRequired;
            if (!string.IsNullOrWhiteSpace(errMsg))
            {
                m_controlRequiredMsgCache[control] = errMsg;
            }
        }
        #endregion

        #region 提示信息    English:Prompt information
        /// <summary>
        /// Gets the verification error MSG.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns>System.String.</returns>
        [Browsable(true), Category("自定义属性"), Description("验证错误提示信息，当为空时则使用默认提示信息"), DisplayName("VerificationErrorMsg"), Localizable(true)]
        public string GetVerificationErrorMsg(Control control)
        {
            if (m_controlMsgCache.ContainsKey(control))
                return m_controlMsgCache[control];
            return "";
        }

        /// <summary>
        /// Sets the verification error MSG.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="strErrorMsg">The string error MSG.</param>
        public void SetVerificationErrorMsg(Control control, string strErrorMsg)
        {
            m_controlMsgCache[control] = strErrorMsg;
        }
        #endregion


        #region 验证    English:Verification
        public bool Verification(Control c)
        {
            bool bln = true;
            if (m_controlCache.ContainsKey(c))
            {
                var vm = m_controlCache[c];
                string strRegex = "";
                string strErrMsg = "";
                #region 获取正则或默认错误提示    English:Get regular or error prompts
                if (vm == VerificationModel.Custom)
                {
                    //自定义正则
                    if (m_controlRegexCache.ContainsKey(c))
                    {
                        strRegex = m_controlRegexCache[c];
                        strErrMsg = "不正确的输入";
                    }
                }
                else
                {
                    //获取默认正则和错误提示
                    Type type = vm.GetType();   //获取类型  
                    MemberInfo[] memberInfos = type.GetMember(vm.ToString());
                    if (memberInfos.Length > 0)
                    {
                        var atts = memberInfos[0].GetCustomAttributes(typeof(VerificationAttribute), false);
                        if (atts.Length > 0)
                        {
                            var va = ((VerificationAttribute)atts[0]);
                            strErrMsg = va.ErrorMsg;
                            strRegex = va.Regex;
                        }
                    }
                }
                #endregion

                #region 取值    English:Value
                string strValue = "";
                if (c is TextBoxBase)
                {
                    strValue = (c as TextBoxBase).Text;
                }
                else if (c is WDTextBoxEx)
                {
                    strValue = (c as WDTextBoxEx).InputText.Trim();
                }
                else if (c is ComboBox)
                {
                    var cbo = (c as ComboBox);
                    if (cbo.DropDownStyle == ComboBoxStyle.DropDownList)
                    {
                        strValue = cbo.SelectedItem == null ? "" : cbo.SelectedValue.ToString();
                    }
                    else
                    {
                        strValue = cbo.Text;
                    }
                }
                else if (c is WDCombox)
                {
                    if (c is WDComboxGrid)
                    {
                        var ctrl = (c as WDComboxGrid);
                        strValue = ctrl.SelectedValue;
                    }
                    else if (c is WDCheckComboxGrid)
                    {
                        var ctrl = (c as WDCheckComboxGrid);
                        if (ctrl.SelectKeyValues != null)
                        {
                            var selectedRows = ctrl.GridDataSource.Where(r => ctrl.SelectKeyValues.Contains(r.GetType().GetProperty(ctrl.KeyField).GetValue(r, null).ToStringExt()));
                            strValue = string.Join(",", selectedRows.Select(r => r.GetType().GetProperty(ctrl.TextField).GetValue(r, null).ToStringExt()));
                        }
                    }
                    else
                    {
                        var ctrl = (c as WDCombox);
                        if (!string.IsNullOrWhiteSpace(ctrl.SelectedValue))
                            strValue = ctrl.SelectedValue.Trim();
                        else
                            strValue = ctrl.TextValue == "请选择" ? "" : ctrl.TextValue;
                    }
                }
                else
                {
                    var cType = c.GetType();
                    var propertyInfo = cType.GetProperty("DTP"); //获取指定名称的属性
                    if (propertyInfo != null)
                    {
                        var dtp = (NullableDateTimePicker)(propertyInfo.GetValue(c, null)); //获取属性值 
                        if (dtp.Value != null)
                            strValue = dtp.Text;
                    }

                    var inputTextProperty = cType.GetProperty("InputText"); //获取指定名称的属性
                    if (inputTextProperty != null)
                    {
                        strValue = (inputTextProperty.GetValue(c, null)).ToStringExt(); //获取属性值 
                    }

                }
                #endregion

                //自定义错误信息
                if (m_controlMsgCache.ContainsKey(c) && !string.IsNullOrEmpty(m_controlMsgCache[c]))
                    strErrMsg = m_controlMsgCache[c];
                var requireErrMsg = "必填项不能为空";
                if (m_controlRequiredMsgCache.ContainsKey(c) && !string.IsNullOrEmpty(m_controlRequiredMsgCache[c]))
                    requireErrMsg = m_controlRequiredMsgCache[c];


                //检查必填项
                if (m_controlRequiredCache.ContainsKey(c) && m_controlRequiredCache[c])
                {
                    if (string.IsNullOrEmpty(strValue))
                    {
                        VerControl(new VerificationEventArgs()
                        {
                            VerificationModel = vm,
                            Regex = strRegex,
                            ErrorMsg = requireErrMsg,
                            IsVerifySuccess = false,
                            Required = true,
                            VerificationControl = c
                        });
                        bln = false;
                        return false;
                    }
                }
                //验证正则
                if (!string.IsNullOrEmpty(strValue))
                {
                    if (!string.IsNullOrEmpty(strRegex))
                    {
                        if (!Regex.IsMatch(strValue, strRegex))
                        {
                            VerControl(new VerificationEventArgs()
                            {
                                VerificationModel = vm,
                                Regex = strRegex,
                                ErrorMsg = strErrMsg,
                                IsVerifySuccess = false,
                                Required = m_controlRequiredCache.ContainsKey(c) && m_controlRequiredCache[c],
                                VerificationControl = c
                            });
                            bln = false;
                            return false;
                        }
                    }
                }
                //没有问题出发一个成功信息
                var ve = new VerificationEventArgs()
                {
                    VerificationModel = vm,
                    Regex = strRegex,
                    ErrorMsg = strErrMsg,
                    IsVerifySuccess = true,
                    Required = m_controlRequiredCache.ContainsKey(c) && m_controlRequiredCache[c],
                    VerificationControl = c
                };
                VerControl(ve);
                bln = ve.IsVerifySuccess;
            }
            return bln;
        }
        #endregion
        #region 验证    English:Verification

        private List<Control> ErrCtrls = new List<Control>();
        public bool Verification()
        {
            bool bln = true;
            ErrCtrls.Clear();
            foreach (var item in m_controlCache)
            {
                Control c = item.Key;
                if (!Verification(c))
                {
                    bln = false;
                    ErrCtrls.Add(c);
                }
            }
            if (ErrCtrls.Count > 0)
            {
                ErrCtrls[0].Focus();
                var ctrl = ErrCtrls[0] as TextBoxBase;
                if (ctrl != null)
                    ctrl.SelectAll();
            }
            return bln;
        }


        #endregion



        #region 验证结果处理    English:Verification result processing
        public AnchorTipsLocation AnchorLocation = AnchorTipsLocation.RIGHT;
        public StringAlignment TextAlignment = StringAlignment.Near;
        public bool CheckControlIsVisible = false;

        private void VerControl(VerificationEventArgs e)
        {
            //如果成功则移除失败提示
            if (e.IsVerifySuccess)
            {
                if (m_controlTips.ContainsKey(e.VerificationControl))
                {
                    m_controlTips[e.VerificationControl].Close();
                    m_controlTips.Remove(e.VerificationControl);
                }
            }
            //触发事件
            if (Verificationed != null)
            {
                Verificationed(e);
                if (e.IsProcessed)//如果已处理，则不再向下执行
                {
                    return;
                }
            }
            //如果失败则显示提示
            if (!e.IsVerifySuccess)
            {
                if (m_controlTips.ContainsKey(e.VerificationControl))
                {
                    m_controlTips[e.VerificationControl].StrMsg = e.ErrorMsg;
                }
                else
                {
                    //判断控件是否屏幕可见
                    if (CheckControlIsVisible)
                    {
                        if (ControlHelper.ControlIsVisible(e.VerificationControl))
                        {
                            var tips = Forms.FrmAnchorTips.ShowTips(e.VerificationControl, e.ErrorMsg, AnchorLocation, background: errorTipsBackColor, foreColor: errorTipsForeColor, autoCloseTime: autoCloseErrorTipsTime, blnTopMost: false, alignment: TextAlignment);
                            tips.FormClosing += tips_FormClosing;
                            m_controlTips[e.VerificationControl] = tips;
                        }
                    }
                    else
                    {
                        var tips = Forms.FrmAnchorTips.ShowTips(e.VerificationControl, e.ErrorMsg, AnchorLocation, background: errorTipsBackColor, foreColor: errorTipsForeColor, autoCloseTime: autoCloseErrorTipsTime, blnTopMost: false, alignment: TextAlignment);
                        tips.FormClosing += tips_FormClosing;
                        m_controlTips[e.VerificationControl] = tips;
                    }
                }
            }
        }

        void tips_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var item in m_controlTips)
            {
                if (item.Value == sender)
                {
                    m_controlTips.Remove(item.Key);
                    break;
                }
            }
        }
        #endregion
        public void Clear()
        {
            CloseErrorTips();
            m_controlCache.Clear();
            m_controlRegexCache.Clear();
            m_controlRequiredCache.Clear();
            m_controlRequiredMsgCache.Clear();
            m_controlMsgCache.Clear();
            m_controlTips.Clear();
        }
        /// <summary>
        /// 关闭所有错误提示
        /// </summary>
        public void CloseErrorTips()
        {
            for (int i = 0; i < 1;)
            {
                try
                {
                    foreach (var item in m_controlTips)
                    {
                        if (item.Value != null && !item.Value.IsDisposed)
                        {
                            item.Value.Close();
                        }
                    }
                }
                catch
                {
                    continue;
                }
                i++;
            }

            m_controlTips.Clear();
        }
        /// <summary>
        /// 关闭指定验证控件的提示
        /// </summary>
        /// <param name="verificationControl">验证控件.</param>
        public void CloseErrorTips(Control verificationControl)
        {
            if (m_controlTips.ContainsKey(verificationControl))
            {
                if (m_controlTips[verificationControl] != null && !m_controlTips[verificationControl].IsDisposed)
                {
                    m_controlTips[verificationControl].Close();
                }
            }
        }
    }
}

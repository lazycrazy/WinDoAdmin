using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinDo.UI
{
    /// <summary>
    /// 表单项
    /// </summary>
    public class FormItem
    {
        public string LabelText { get; set; }
        private string _fieldName = "";
        public string FieldName { get { return _fieldName; } set { _fieldName = value; } }
        public string PropertyName { get; set; }
        public System.Windows.Forms.Control Ctrl { get; set; }
        public bool Required { get; set; }
        public int MaxLength { get; set; }
        public string RegexStr { get; set; }
        public string RegexMsg { get; set; }
        public bool IsMultiControl { get; set; }
        public bool TemplateField { get; set; }
    }
}

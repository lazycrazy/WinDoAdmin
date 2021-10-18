using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinDo.Model
{
    public class MsgInfo<T> where T : class
    {
        /// <summary>
        /// 有错误
        /// </summary>
        public bool HasError { get; private set; }
        /// <summary>
        /// 成功
        /// </summary>
        public bool Success { get { return !HasError; } }
        private string _code = "";
        /// <summary>
        /// 错误码
        /// </summary>
        public string Code
        {
            get
            {
                return _code;
            }
            set
            {
                _code = value;
                HasError = !string.IsNullOrWhiteSpace(_code);
            }
        }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 返回的实体对象
        /// </summary>
        public T Entity { get; set; }
    }
}

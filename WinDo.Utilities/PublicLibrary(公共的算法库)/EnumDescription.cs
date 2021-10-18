using System;

namespace WinDo.Utilities
{
    /// <summary>
    /// EnumDescription
    /// 枚举状态的说明。   
    /// <author>
    ///		<name></name>
    ///		<date></date>
    /// </author> 
    /// </summary>    
    public class EnumDescription : Attribute
    {
        private string _text;

        /// <summary>
        /// 
        /// </summary>
        public string Text
        {
            get
            {
                return _text;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public EnumDescription(string text)
        {
            _text = text;
        }
    }
}
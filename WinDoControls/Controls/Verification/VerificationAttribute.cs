using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinDoControls.Controls
{
    /// <summary>
    /// Class VerificationAttribute.
    /// Implements the <see cref="System.Attribute" />
    /// </summary>
    /// <seealso cref="System.Attribute" />
    public class VerificationAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VerificationAttribute"/> class.
        /// </summary>
        /// <param name="strRegex">The string regex.</param>
        /// <param name="strErrorMsg">The string error MSG.</param>
        public VerificationAttribute(string strRegex = "", string strErrorMsg = "")
        {
            Regex = strRegex;
            ErrorMsg = strErrorMsg;
        }
        /// <summary>
        /// Gets or sets the regex.
        /// </summary>
        /// <value>The regex.</value>
        public string Regex { get; set; }
        /// <summary>
        /// Gets or sets the error MSG.
        /// </summary>
        /// <value>The error MSG.</value>
        public string ErrorMsg { get; set; }

    }
}

using System.Windows.Forms;

namespace WinDo.Utilities
{
    /// <summary>
    /// 通用对话框显示类
    /// 
    public abstract class DialogHelper
    {
        /// <summary>
        /// 数据为空
        /// </summary>
        public static string DATA_ISNULL =
            "数据为空，请检查你的操作数据是否正确？";


        /// <summary>
        /// 显示错误信息
        /// </summary>
        /// <param name="strMsg">显示内容</param>
        public static void ShowErrorMsg(string strMsg)
        {
            MessageBox.Show(strMsg, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 显示成功信息
        /// </summary>
        /// <param name="strMsg">显示内容</param>
        public static void ShowSuccessMsg(string strMsg)
        {
            MessageBox.Show(strMsg, "成功提示", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        /// <summary>
        /// 显示警告信息
        /// </summary>
        /// <param name="strMsg">显示内容</param>
        public static void ShowWarningMsg(string strMsg)
        {
            MessageBox.Show(strMsg, "警告提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// 显示询问信息
        /// </summary>
        /// <param name="strMsg">显示内容</param>
        public static void ShowQuestionMsg(string strMsg)
        {
            MessageBox.Show(strMsg, "询问提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        /// <summary>
        /// 显示询问信息并返回DialogResult的结果
        /// </summary>
        /// <param name="strMsg">显示内容，如：你确定要删除吗？（是/否）</param>
        /// <returns>DialogResult</returns>
        public static DialogResult ShowDlgReturnResult(string strMsg)
        {
            if (MessageBox.Show(strMsg, "询问信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                return DialogResult.Yes;
            }
            else
            {
                return DialogResult.No;
            }
        }
    }
}

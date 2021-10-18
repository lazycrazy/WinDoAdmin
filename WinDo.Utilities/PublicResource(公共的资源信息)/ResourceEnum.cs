using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WinDo.Utilities.PublicResource
{
    public class ResourceEnum
    {
        #region "排队叫号"

        /// <summary>
        /// 呼叫状态 0:未报道 1:已报道 2:已侯诊 3正在呼叫 4已呼叫 5撤回  9已过号
        /// </summary>
        public enum CallStatus
        {
            //0未报到
            未报到 = 0,
            //1已报道
            已报到 = 1,
            //2已候诊
            已候诊 = 2,
            //3正在呼叫
            正在呼叫 = 3,
            //4已呼叫
            已呼叫 = 4,
            //5撤回
            已撤回 = 5,
            //9已过号
            已过号 = 9,
            //99强制过号
            强制过号 = 99

        }
        #endregion

        public enum Weekday
        {
            SUN, MON, TUS, WED, THU, FRI, SAT
        }

        public enum ScheduleListHandStatus
        {
            UNHAND,ALL
        }

        /// <summary>
        /// 预约列表状态
        /// </summary>
        public enum ScheduleListStatus
        {
            /// <summary>
            /// 1队列未处理
            /// </summary>
            [Description("队列未处理")]
            SCHLST_NOTPROCESS=1,
            /// <summary>
            /// 2队列处理成功
            /// </summary>
            [Description("队列处理成功")]
            SCHLST_PROCESS_SUCCESS = 2,
            /// <summary>
            /// 3取消预约
            /// </summary>
            [Description("取消预约")]
            CA_NOTPROCESS = 3,
            /// <summary>
            ///4 处理中
            /// </summary>
            [Description("处理中")]
            SCHLST_PROCESSING = 4,
            /// <summary>
            /// 5队列处理失败
            /// </summary>
            [Description("队列处理失败")]
            SCHLST_PROCESS_FAILED = 5          
          
        }
        
        /// <summary>
        /// 预约状态
        /// </summary>
        public enum ScheduleStatus
        {
            /// <summary>
            /// 0预约未处理
            /// </summary>
            [Description("预约未处理")]
            SCH_NOTPROCESS=0,
            /// <summary>
            /// 1预约处理中
            /// </summary>
            [Description("预约处理中")]
            SCH_PROCESSING=1,
            /// <summary>
            /// 2预约处理成功
            /// </summary>
            [Description("预约处理成功")]
            SCH_PROCESS_SUCCESS=2,
            /// <summary>
            /// 3预约处理失败
            /// </summary>
            [Description("预约处理失败")]
            SCH_PROCESS_FAILED = 3,
            /// <summary>
            /// 4取消未处理
            /// </summary>
            [Description("取消未处理")]
            CA_NOTPROCESS = 4,
            /// <summary>
            /// 5取消处理中
            /// </summary>
            [Description("取消处理中")]
            CA_PROCESSING = 5,
            /// <summary>
            /// 6取消处理成功
            /// </summary>
            [Description("取消处理成功")]
            CA_PROCESS_SUCCESS = 6,
            /// <summary>
            /// 7取消处理失败
            /// </summary>
            [Description("取消处理失败")]
            CA_PROCESS_FAILED = 7
        }

        /// <summary>
        /// 日志类型
        /// </summary>
        public enum LogType
        {
            手动,
            自动
        }

        /// <summary>
        /// 设置单据收费状态
        /// </summary>
        public enum SetChargeStatusEnum
        {
            /// <summary>
            /// 未收费
            /// </summary>
            NoCharge = 0,

            /// <summary>
            /// 已收费
            /// </summary>
            Pay = 1,
            /// <summary>
            /// 部分收费
            /// </summary>
            PartPay = 2,

            /// <summary>
            /// 退费成功
            /// </summary>
            Refund = 3,

            /// <summary>
            /// 已确费
            /// </summary>
            Confirm = 4,

            /// <summary>
            /// 确费失败
            /// </summary>
            ConfirmFailure = 5

        }
        /// <summary>
        /// 操作类型(1：新增 2:修改 3：取消)
        /// </summary>
        public enum OperationType
        {
            添加 = 1,
            修改 = 2,
            取消 = 3
        }

        /// <summary>
        /// 班次(1：上午 2:下午)
        /// </summary>
        public enum BrachyGroup
        {
            上午 = 1,
            下午 = 2
        }
         
    }

    public class ErrCode
    {
        public static readonly string 用户不存在 = "1000001";
    }

    /// <summary>
    /// 用户职务
    /// </summary>
    public class UserTitle
    {
        public static readonly string 开发 = "1";
        public static readonly string 设计 = "2";
        public static readonly string 产品 = "3";
        public static readonly string 测试 = "4";
    }

    #region 权限相关
    public static class PrivilegesEnum
    {

        #region 开发主页
        public static string 开发主页查看费用 = "DOCTOR_VIEW_CHARGE";
        public static string 开发主页忽略预警 = "DOCTOR_WARNING_I";
        public static string 开发主页发消息 = "DOCTOR_SENDMESSAGE";
        #endregion


        public static string 登记 = "PAT_REG001";
        public static string 手工登记 = "PAT_REG002";
        public static string 制卡 = "PAT_REG003";
        public static string 基本信息超级编辑 = "PAT_REG004";
        public static string 客户模板编辑 = "PAT_REG005";

        public static string 任务编辑 = "ORD_DIA";
        public static string 任务公共模板编辑 = "ORD_DIA_Temp";
        public static string 疗程编辑 = "ORD_COURSE";
        public static string 部位编辑 = "ORD_PART";



        public static string 非加预约超级权限 = "SCH_VIP001";
        public static string 加速器超级权限 = "SCH_VIP002";
        public static string 非加速器调整 = "SCH_ASS_ADJ_CT";

        public static string 非加速器修改 = "TASK_ADJ";


        public static string 加速器调整 = "SCH_ASS_ADJ_LINAC";
        public static string 预约调整发消息 = "SCH_ASS_SMS";
        public static string 任务列表发消息 = "TASK_SMS";
        public static string 预约队列完成 = "SCH_ASS_QUEUE_Done";

        public static string 排班配置 = "QUEUE_ARRANGE_SET";
        public static string 时段配置 = "QUEUE_TIMECON";
        public static string 交班本删除 = "QUEUE_BOOK_DEL";
        public static string 排队叫号查看医嘱 = "QUEUE_OPEN_CHARGE";


        public static string 影像报告编辑 = "IMAGE02";
        public static string 影像报告审核 = "IMAGE03";

        public static string 治疗室 = "SYS09";

        public static string 其他 = "SYS10";

        public static string 后装设备管理 = "SYS022";

        public static string PlanQA验证 = "QA_LIST02";

        public static string 客户端配置 = "SYS017";

        public static string 截图 = "TOOL01";
        public static string 预警处理_开发主页 = "EARLY_WARNING_PROCESS";
        public static string 预警处理_治疗师主页 = "EARLY_WARNING_PROCESS_ZL";

        public static string 模具处理 = "Model_Process";
        public static string 编辑公告 = "SYS023";

    }

    #endregion
}

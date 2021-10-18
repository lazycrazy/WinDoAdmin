using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using WinDo.Utilities.PublicResource;
using WinDoControls.IconSvg;

namespace WinDoControls
{

    public class WDImages
    {
        public static Image GetBtnIconImage(string iconName, int imageSize = 20, Color? color = null)
        {
            if (!color.HasValue)
                color = Color.Black;
            return WinDoControls.FontImages.GetImage((WinDoControls.FontIcons)Enum.Parse(typeof(WinDoControls.FontIcons), iconName), imageSize, color);
        }

        /// <summary>
        /// 需要缴费
        /// </summary>
        public static Image NeedCharge = GetBtnIconImage("I_cycle_money", 26, Color.Red);
        /// <summary>
        /// 需要缴费预警
        /// </summary>
        public static Image NeedChargeYellow = GetBtnIconImage("I_cycle_money", 26, WDColors.AskYellowColor);
        /// <summary>
        /// 特殊情况
        /// </summary>
        public static Image DeltaImg = GetBtnIconImage("I_exclamation_delta", 24, color: WDColors.OrangeColor);


        public static Image NewWarningImg = GetBtnIconImage("I_new_warning", imageSize: 28, color: ColorTranslator.FromHtml("#E80101"));
        public static Image SuccessImg = GetBtnIconImage("I_success_fill", imageSize: 20, color: Color.White);
        /// <summary>
        /// 完成
        /// </summary>
        public static Image FinishImg = GetBtnIconImage("I_wancheng", imageSize: 20, color: WDColors.StatusGreen);
        /// <summary>
        /// 加号
        /// </summary>
        public static Image PlusImg = GetBtnIconImage("I_plus", imageSize: 20, color: Color.Black);
        //减号
        public static Image MinusImg = GetBtnIconImage("I_minus", imageSize: 20, color: Color.Red);
        //笔
        public static Image PenImg = GetBtnIconImage("I_pen", imageSize: 16, color: Color.Black);

        public static Image ArrowDownWhiteImg = GetBtnIconImage("I_downarrow_clear", imageSize: 20, color: Color.White);
        public static Image ArrowRightWhiteImg = GetBtnIconImage("I_rightarrow_clear", imageSize: 20, color: Color.White);
        public static Image ArrowRightWhite = WinDoControls.FontImages.GetImage((WinDoControls.FontIcons)Enum.Parse(typeof(WinDoControls.FontIcons), "I_rightarrow_clear"), 14, Color.White);
        public static Image ArrowRightBlack = WinDoControls.FontImages.GetImage((WinDoControls.FontIcons)Enum.Parse(typeof(WinDoControls.FontIcons), "I_rightarrow_clear"), 14, Color.Black);

        /// <summary>
        /// 暂无数据
        /// </summary>
        public static Image EmptyImg = GetBtnIconImage("I_empty", imageSize: 40, color: WDColors.GrayTextColor9);

        public static Image ArrowDownImg = GetBtnIconImage("I_arrow_down_fill", imageSize: 14, color: WDColors.GrayRectColor);

        public static Image IgnoreImg = GetBtnIconImage("I_ignore", color: WDColors.BlackColor);
        public static Image UnIgnoreImg = GetBtnIconImage("I_ignore_cancel", color: WDColors.BlackColor);
        public static Image ProcessImg = GetBtnIconImage("I_process", color: WDColors.WhiteColor);



        //public static Image CheckedBox = Properties.Resources.CheckedBox;// SVGIcons.Icon(IconNames.check_square_fill, WinDoColors.MainGreenColor);
        //public static Image UnCheckedBox = Properties.Resources.UnCheckedBox;//SVGIcons.Icon(IconNames.square, WinDoColors.GrayRectColor);

        public static Size ToggleSize = new Size(32, 22);
        public static Image ToggleOn = SVGIcons.Icon(IconNames.toggle_on, WDColors.Green6, 32);
        //public static Image ToggleOff = SVGIcons.Icon(IconNames.toggle_off, WinDoColors.GrayRectColor, 32);

        public static Size RadioSize = new Size(20, 20);
        public static Image X = SVGIcons.Icon(IconNames.x, WDColors.WhiteColor, 32);
        public static Image X_Black = SVGIcons.Icon(IconNames.x, WDColors.BlackColor, 18);
        public static Image Logout = SVGIcons.Icon(IconNames.box_arrow_in_right, WDColors.WhiteColor, 20);
        public static Image LogoutRed = SVGIcons.Icon(IconNames.box_arrow_in_right, WDColors.red5, 20);
        public static Image Edit = SVGIcons.Icon(IconNames.pencil_square, WDColors.WhiteColor, 20);
        public static Image Mini = SVGIcons.Icon(IconNames.dash, WDColors.WhiteColor, 32);
        public static Image PersonFill = SVGIcons.Icon(IconNames.person_fill, WDColors.WhiteColor, 20);
        public static Image ArrowRight = SVGIcons.Icon(IconNames.play_fill, WDColors.WhiteColor, 20);

       
        //public static Image RadioOn = Properties.Resources.RadioOn;// SVGIcons.Icon(IconNames.record2, WinDoColors.MainGreenColor, 26);
        //public static Image RadioOff = Properties.Resources.RadioOff;// SVGIcons.Icon(IconNames.record, WinDoColors.GrayRectColor, 26);
    }
}

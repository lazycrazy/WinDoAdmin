using Svg;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using WinDo.Utilities;

namespace WinDoControls.IconSvg
{
    /// <summary>
    /// SVG图标
    /// </summary>
    public static class SVGIcons
    {
        static Dictionary<string, Bitmap> Icons = new Dictionary<string, Bitmap>();
        static string svgDirectory = System.IO.Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, $"bootstrap-icons-1.4.1\\");
        public static Bitmap Icon(IconNames name, Color? color = null, int? size = null)
        {
            var iconName = name.ToString().Replace("_", "-");
            var key = iconName;
            if (color.HasValue)
                key += "^" + color.Value.Name;
            if (size.HasValue)
                key += $"^{size.Value}X{size.Value}";
            if (Icons.ContainsKey(key))
                return Icons[key];

            if (SystemInfo.IsDesignMode)//判断是否为设计时
            {
                svgDirectory = @"C:\bootstrap-icons-1.4.1\";
            }
            var _document = SvgDocument.Open(svgDirectory + $"{iconName}.svg");
            if (color.HasValue)
                _document.Color = new SvgColourServer(color.Value);
            Icons[key] = size.HasValue ? _document.Draw(size.Value, size.Value) : _document.Draw();
            return Icons[key];
        }
    }

    /// <summary>
    /// SVG图标名称
    /// </summary>
    public enum IconNames
    {
        alarm,
        bag,
        check_square_fill,
        square,
        toggle_on,
        toggle_off,
        x,
        record,
        record2,
        box_arrow_in_right,
        pencil_square,
        dash,
        person_fill,
        play_fill
    }
}

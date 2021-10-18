using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinDoControls.Controls
{
    public class PanelNoScroll: System.Windows.Forms.Panel
    {
        protected override System.Drawing.Point ScrollToControl(System.Windows.Forms.Control activeControl)
        {
            //实现Panel的滚动条不随焦点变化而自动改变位置
            return DisplayRectangle.Location;
        }

    }
}

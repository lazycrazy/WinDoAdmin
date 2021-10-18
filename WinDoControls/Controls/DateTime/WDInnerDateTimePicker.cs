using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using WinDo.Utilities.PublicResource;
using WinDoControls.Controls;

namespace WinDoControls.Controls
{
    [ToolboxItem(false)]
    public class WDInnerDateTimePicker : Panel
    {
        public WDInnerDateTimePicker()
            : base()
        {
            dtp = new NullableDateTimePicker() { Nullable = true };

            dtp.Font = WDFonts.TextFont;
            dtp.Width = this.Width + 2;
            this.Height = dtp.Height - 2;
            dtp.Location = new System.Drawing.Point(-1, -1);
            dtp.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dtp.ValueChanged += new EventHandler(dtp_ValueChanged);
            dtp.Value = null;
            //this.MaximumSize = new System.Drawing.Size(0, this.Height);
            //this.MinimumSize = new System.Drawing.Size(0, this.Height);
            //dtp.Value = null;
            Controls.Add(dtp);
        }

        void dtp_ValueChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(sender, e);
        }

        public event EventHandler ValueChanged;

        public NullableDateTimePicker dtp;
    }
}

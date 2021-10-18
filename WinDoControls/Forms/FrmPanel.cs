using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinDoControls.Forms;
using WinDo.Utilities.PublicResource;

namespace WinDoControls.Forms
{
    /*
     * 自定义面板窗体
     //var keyPanel = new PT_RightKey() { Dock = DockStyle.Fill, Width = 120 };
            //keyPanel.Items = new List<AwesomeListItem>() {
            //new AwesomeListItem(){Text="自动勾画" },
            //new AwesomeListItem(){Text=PT_RightKey.SPLIT_LINE },
            //new AwesomeListItem(){Text="靶区勾画" },
            //new AwesomeListItem(){Text="特殊情况" },
            //new AwesomeListItem(){Text="发消息" },
            //new AwesomeListItem(){Text="隐藏" },
            //new AwesomeListItem(){Text="勾画开发" },
            //new AwesomeListItem(){Text="审核通过" },
            //new AwesomeListItem(){Text="审核驳回" },
            //};

            //if(RightMenuForm!=null){RightMenuForm.close(); RightMenuForm=null;}
            //RightMenuForm = new FrmPanel();
            //frm.Controls.Add(keyPanel);
            //frm.Height = keyPanel.GetHeight();
            //frm.Width = 120;
            //frm.Location = (MousePosition);
            //frm.Show(this);
            //frm.Location = (MousePosition);
            //keyPanel.ItemClick += (item, ee) =>
            //{
            //    //点击项事件

            //    RightMenuForm.Close();
            //};
         */
    public partial class FrmPanel : FrmBase
    {
        public FrmPanel()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
            WindowState = FormWindowState.Normal;
            IsShowShadowForm = true;
        }


    }
}
















using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinDoControls.Controls
{







    [ToolboxItem(false)]
    public class WDPageBase : UserControl, IPageControl
    {
        #region 构造



        private System.ComponentModel.IContainer components = null;





        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码





        private void InitializeComponent()
        {
            this.SuspendLayout();



            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Name = "UCPagerControlBase";
            this.Size = new System.Drawing.Size(304, 58);
            this.Load += new System.EventHandler(this.UCPagerControlBase_Load);
            this.ResumeLayout(false);

        }

        #endregion
        #endregion


        private List<object> dataSource;




        public virtual List<object> DataSource
        {
            get { return dataSource; }
            set
            {
                dataSource = value;
            }
        }



        public virtual event PageControlEventHandler ShowSourceChanged;


        /// <summary>
        /// 总行数
        /// </summary>
        private int _totalCount = 0;
        public virtual int TotalCount
        {
            get { return _totalCount; }
            set
            {
                _totalCount = value;              
            }
        }


        /// <summary>
        /// 总共页数
        /// </summary>
        public virtual int PageCount
        {
            get;
            set;
        }
        /// <summary>
        /// 当前页码
        /// </summary>
        private int m_pageIndex = 1;
        public virtual int PageIndex
        {
            get { return m_pageIndex; }
            set { m_pageIndex = value; }
        }

        private int m_pageSize = 20;
        [Description("每页显示数量"), Category("自定义")]
        public virtual int PageSize
        {
            get { return m_pageSize; }
            set { m_pageSize = value; }
        }
        private int startIndex = 0;
        [Description("开始的下标"), Category("自定义")]
        public virtual int StartIndex
        {
            get { return startIndex; }
            set
            {
                startIndex = value;
                if (startIndex <= 0)
                    startIndex = 0;
            }
        }




        public WDPageBase()
        {
            InitializeComponent();
        }






        private void UCPagerControlBase_Load(object sender, EventArgs e)
        {
            if (DataSource == null)
                ShowBtn(false, false);
            else
            {
                ShowBtn(false, DataSource.Count > PageSize);
            }
        }



        public virtual void FirstPage()
        {
            startIndex = 0;
            var s = GetCurrentSource();

            OnShowSourceChanged(s);
        }

        public void OnShowSourceChanged(object currentSource)
        {
            if (ShowSourceChanged != null)
            {
                ShowSourceChanged(currentSource);
            }
        }


        public virtual void PreviousPage()
        {
            if (startIndex == 0)
                return;
            startIndex -= m_pageSize;
            if (startIndex < 0)
                startIndex = 0;
            var s = GetCurrentSource();

            OnShowSourceChanged(s);
        }



        public virtual void NextPage()
        {

            if (startIndex + m_pageSize >= DataSource.Count)
            {
                return;
            }
            startIndex += m_pageSize;
            if (startIndex < 0)
                startIndex = 0;
            var s = GetCurrentSource();


            OnShowSourceChanged(s);
        }



        public virtual void EndPage()
        {
            if (DataSource == null)
            {
                OnShowSourceChanged(null);
                return;
            }
            startIndex = DataSource.Count - m_pageSize;
            if (startIndex < 0)
                startIndex = 0;
            var s = GetCurrentSource();

            OnShowSourceChanged(s);
        }



        public virtual void Reload()
        {
            var s = GetCurrentSource();
            OnShowSourceChanged(s);
        }




        public virtual List<object> GetCurrentSource()
        {
            if (DataSource == null || DataSource.Count <= 0)
                return null;
            int intShowCount = m_pageSize;
            if (intShowCount + startIndex > DataSource.Count)
                intShowCount = DataSource.Count - startIndex;
            object[] objs = new object[intShowCount];
            DataSource.CopyTo(startIndex, objs, 0, intShowCount);
            var lst = objs.ToList();

            bool blnLeft = false;
            bool blnRight = false;
            if (lst.Count > 0)
            {
                if (DataSource.IndexOf(lst[0]) > 0)
                {
                    blnLeft = true;
                }
                else
                {
                    blnLeft = false;
                }
                if (DataSource.IndexOf(lst[lst.Count - 1]) >= DataSource.Count - 1)
                {
                    blnRight = false;
                }
                else
                {
                    blnRight = true;
                }
            }
            ShowBtn(blnLeft, blnRight);
            return lst;
        }






        protected virtual void ShowBtn(bool blnLeftBtn, bool blnRightBtn)
        { }
    }
}

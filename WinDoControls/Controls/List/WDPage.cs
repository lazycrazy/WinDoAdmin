using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinDo.Utilities.PublicResource;

namespace WinDoControls.Controls
{
    /// <summary>
    /// 外接数据源的分页控件
    /// </summary>
    [ToolboxItem(true)]
    public partial class WDPage : WDPageBase
    {
        public WDPage()
        {
            InitializeComponent();
            ControlHelper.SetDouble(flowLayoutPanel1);
            btnFirst.BackgroundImage = WinDoControls.FontImages.GetImage((WinDoControls.FontIcons)Enum.Parse(typeof(WinDoControls.FontIcons), "I_leftarrow_clear2"), 14, SystemColors.ControlDarkDark);
            btnEnd.BackgroundImage = WinDoControls.FontImages.GetImage((WinDoControls.FontIcons)Enum.Parse(typeof(WinDoControls.FontIcons), "I_rightarrow_clear2"), 14, SystemColors.ControlDarkDark);
            btnPrevious.BackgroundImage = WinDoControls.FontImages.GetImage((WinDoControls.FontIcons)Enum.Parse(typeof(WinDoControls.FontIcons), "I_leftarrow_clear"), 14, SystemColors.ControlDarkDark);
            btnNext.BackgroundImage = WinDoControls.FontImages.GetImage((WinDoControls.FontIcons)Enum.Parse(typeof(WinDoControls.FontIcons), "I_rightarrow_clear"), 14, SystemColors.ControlDarkDark);
            btnFirst.BackgroundImageLayout = btnEnd.BackgroundImageLayout = btnPrevious.BackgroundImageLayout = btnNext.BackgroundImageLayout = ImageLayout.Center;
            txtPage.txtInput.TextAlign = HorizontalAlignment.Center;
            txtPage.txtInput.KeyDown += txtInput_KeyDown;
            combPageSize.Source = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("10", "10 条/页") ,
                new KeyValuePair<string, string>("15", "15 条/页") ,
                new KeyValuePair<string, string>("20", "20 条/页") ,
                new KeyValuePair<string, string>("25", "25 条/页") ,                
                //new KeyValuePair<string, string>("200", "200 条/页") ,                
                //new KeyValuePair<string, string>("500", "500 条/页") ,                
            };
            HandleCreated += UCDBPagerControl_HandleCreated;
        }

        private void UCDBPagerControl_HandleCreated(object sender, EventArgs e)
        {
            SetDefaultPageSize();
            combPageSize.SelectedChangedEvent += new EventHandler(combPageSize_SelectedChangedEvent);
        }

        private void SetDefaultPageSize()
        {
            var form = this.FindForm();
            if (form == null)
            {
                combPageSize.SelectedIndex = 2;
                return;
            }
            var pageSize = 20;
            var formName = form.Name;
            var pstr = System.Configuration.ConfigurationManager.AppSettings[PageSizeFlag];
            if (!string.IsNullOrWhiteSpace(pstr))
            {
                var fps = pstr.Split('|').FirstOrDefault(s => s.Split(':')[0] == formName);
                if (fps != null)
                {
                    int ps = 20;
                    if (int.TryParse(fps.Split(':')[1], out ps))
                    {
                        pageSize = ps;
                    }
                }
            }
            else
            {
                SetPageSizeConfig(20);
            }
            var idx = combPageSize.Source.FindIndex(s => s.Key == pageSize.ToString());
            if (idx >= 0)
                combPageSize.SelectedIndex = idx;
            else
                combPageSize.SelectedIndex = 2;
            base.PageSize = pageSize;
        }
        string PageSizeFlag = "PageSize";

        private void SetPageSizeConfig(int pageSize)
        {
            var form = this.FindForm();
            if (form == null) return;
            try
            {
                string formName = form.Name;
                var configFile = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[PageSizeFlag] == null)
                {
                    settings.Add(PageSizeFlag, formName + ":" + pageSize);
                }
                else
                {
                    var pstrs = settings[PageSizeFlag].Value.Split('|');
                    var fps = pstrs.FirstOrDefault(s => s.Split(':')[0] == formName);
                    if (fps != null)
                    {
                        settings[PageSizeFlag].Value = formName + ":" + pageSize + "|" + string.Join("|", pstrs.Where(s => s.Split(':')[0] != formName));
                    }
                    else
                    {
                        settings[PageSizeFlag].Value = formName + ":" + pageSize + "|" + settings[PageSizeFlag].Value;
                    }
                }
                configFile.Save(System.Configuration.ConfigurationSaveMode.Modified);
                //刷新，否则程序读取的还是之前的值（可能已装入内存）
                System.Configuration.ConfigurationManager.RefreshSection("appSettings");
            }
            catch (Exception)
            {
                var configFile = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                settings.Remove(PageSizeFlag);
                configFile.Save(System.Configuration.ConfigurationSaveMode.Modified);
                //刷新，否则程序读取的还是之前的值（可能已装入内存）
                System.Configuration.ConfigurationManager.RefreshSection("appSettings");
            }
        }

        void combPageSize_SelectedChangedEvent(object sender, EventArgs e)
        {
            //修改每页数量
            this.PageSize = int.Parse(combPageSize.SelectedValue.ToString());
        }






        void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnToPage_BtnClick(null, null);
                txtPage.InputText = "";
            }
        }






        /// <summary>
        /// 不再使用内部数据源
        /// </summary>
        /// <returns></returns>
        public override List<object> GetCurrentSource()
        {
            return null;
        }



        /// <summary>
        /// 当前页数据源，设置数据就行
        /// </summary>
        public override List<object> DataSource
        {
            get
            {
                return base.DataSource;
            }
            set
            {
                if (value == null)
                {
                    base.DataSource = new List<object>();
                }
                else
                {
                    base.DataSource = value;
                }
                OnShowSourceChanged(base.DataSource);
            }
        }

        /// <summary>
        /// 页改变事件，注册事件，获取数据源
        /// </summary>
        public event Action PageChanged;

        public void OnPageChanged()
        {
            if (PageChanged != null)
                PageChanged();
        }
        public void HiddenComb()
        {
            combPageSize.Visible = false;
        }

        /// <summary>
        /// 总计
        /// </summary>
        public override int TotalCount
        {
            get
            {
                return base.TotalCount;
            }
            set
            {
                base.TotalCount = value;


                //总共多少页数
                base.PageCount = base.TotalCount / base.PageSize + (base.TotalCount % base.PageSize > 0 ? 1 : 0);
                txtPage.MaxValue = base.PageCount;
                txtPage.MinValue = 1;
                if (txtPage.MaxValue < PageIndex || PageIndex < 1)
                    PageIndex = 1;
                ReloadPage();
            }
        }

        /// <summary>
        /// 每页多少
        /// </summary>
        public override int PageSize
        {
            get
            {
                return base.PageSize;
            }
            set
            {
                base.PageSize = value;
                SetPageSizeConfig(value);
                AddPageSizeItem(value);
                PageIndex = 1;
                StartIndex = (PageIndex - 1) * PageSize;
                OnPageChanged();
                base.PageCount = base.TotalCount / base.PageSize + (base.TotalCount % base.PageSize > 0 ? 1 : 0);
                ReloadPage();
            }
        }



        private void AddPageSizeItem(int value)
        {
            if (value <= 0 || combPageSize.SelectedValue == value.ToString()) return;
            if (combPageSize.Source.Exists(i => i.Key == value.ToString()))
            {
                combPageSize.SelectedValue = value.ToString();
                return;
            }
            var ls = combPageSize.Source;
            ls.Add(new KeyValuePair<string, string>(value.ToString(), value + " 条/页"));
            combPageSize.Source = ls.OrderBy(i => i.Key.ToInt()).ToList();
            combPageSize.SelectedValue = value.ToString();
        }

        public override void FirstPage()
        {
            if (PageIndex == 1)
                return;
            PageIndex = 1;
            StartIndex = (PageIndex - 1) * PageSize;
            OnPageChanged();
        }




        public override void PreviousPage()
        {
            if (PageIndex <= 1)
            {
                return;
            }
            PageIndex--;
            StartIndex = (PageIndex - 1) * PageSize;
            OnPageChanged();
        }




        public override void NextPage()
        {
            if (PageIndex >= PageCount)
            {
                return;
            }
            PageIndex++;
            StartIndex = (PageIndex - 1) * PageSize;
            OnPageChanged();
        }

        public override void EndPage()
        {
            if (PageIndex == PageCount)
                return;
            PageIndex = PageCount;
            StartIndex = (PageIndex - 1) * PageSize;
            OnPageChanged();
        }
        private bool _small = false;
        public void SetSmallStyle()
        {
            _small = true;
            lblTotalCount.Left = label1.Left;
            label1.Visible = false;
            txtPage.Visible = false;
            label2.Visible = false;
            btnToPage.Visible = false;
            combPageSize.Visible = false;
        }
        /// <summary>
        /// 刷新按钮组
        /// </summary>
        private void ReloadPage()
        {
            try
            {
                flowLayoutPanel1.SuspendLayout();
                ControlHelper.FreezeControl(this.flowLayoutPanel1, true);
                List<int> lst = new List<int>();

                if (PageCount <= 9)
                {
                    for (var i = 1; i <= PageCount; i++)
                    {
                        lst.Add(i);
                    }
                }
                else
                {
                    if (this.PageIndex <= 6)
                    {
                        for (var i = 1; i <= 7; i++)
                        {
                            lst.Add(i);
                        }
                        lst.Add(-1);
                        lst.Add(PageCount);
                    }
                    else if (this.PageIndex > PageCount - 6)
                    {
                        lst.Add(1);
                        lst.Add(-1);
                        for (var i = PageCount - 6; i <= PageCount; i++)
                        {
                            lst.Add(i);
                        }
                    }
                    else
                    {
                        lst.Add(1);
                        lst.Add(-1);
                        var begin = PageIndex - 2;
                        var end = PageIndex + 2;
                        if (end > PageCount)
                        {
                            end = PageCount;
                            begin = end - 4;
                            if (PageIndex - begin < 2)
                            {
                                begin = begin - 1;
                            }
                        }
                        else if (end + 1 == PageCount)
                        {
                            end = PageCount;
                        }
                        for (var i = begin; i <= end; i++)
                        {
                            lst.Add(i);
                        }
                        if (end != PageCount)
                        {
                            lst.Add(-1);
                            lst.Add(PageCount);
                        }
                    }
                }

                for (int i = 0; i < 9; i++)
                {
                    WDBtnExt c = (WDBtnExt)this.flowLayoutPanel1.Controls.Find("p" + (i + 1), false)[0];
                    if (i >= lst.Count)
                    {
                        c.Visible = false;
                    }
                    else
                    {

                        if (lst[i] == -1)
                        {
                            c.BtnText = "...";
                            c.Enabled = false;
                        }
                        else
                        {
                            c.BtnText = lst[i].ToString();
                            if (c.BtnText.Trim().Length > 3)
                                c.Width = TextRenderer.MeasureText(c.BtnText, c.BtnFont).Width;
                            c.Enabled = true;
                        }
                        c.Visible = true;
                        if (lst[i] == PageIndex)
                        {
                            //c.RectColor = Color.FromArgb(255, 77, 59);
                            c.FillColor = WDColors.StatusBlue;
                            c.BtnForeColor = WDColors.WhiteColor;
                        }
                        else
                        {
                            //c.RectColor = Color.FromArgb(223, 223, 223);
                            c.FillColor = WDColors.WhiteColor;
                            c.BtnForeColor = SystemColors.ControlText;
                        }
                    }
                }
                if (_small)
                {
                    p6.Visible = false;
                    p7.Visible = false;
                    p8.Visible = false;
                    p9.Visible = false;
                    if (PageCount <= 5)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            var idx = i + 1;
                            WDBtnExt c = (WDBtnExt)this.flowLayoutPanel1.Controls.Find("p" + idx, false)[0];
                            c.BtnText = idx.ToString();
                            c.Visible = idx <= PageCount;
                            SetColor(idx, c);
                        }
                    }
                    else
                    {
                        if (PageIndex <= 3)
                        {
                            for (var i = 1; i <= 3; i++)
                            {
                                WDBtnExt c = (WDBtnExt)this.flowLayoutPanel1.Controls.Find("p" + i, false)[0];
                                c.BtnText = i.ToString();
                            }
                            p4.BtnText = "...";
                            p5.BtnText = PageCount.ToString();
                        }
                        else if (PageIndex > PageCount - 3)
                        {
                            p1.BtnText = 1.ToString();
                            p2.BtnText = "...";
                            p3.BtnText = ((PageCount - 3) + 1).ToString();
                            p4.BtnText = ((PageCount - 3) + 2).ToString();
                            p5.BtnText = ((PageCount - 3) + 3).ToString();
                        }
                        else
                        {
                            p1.BtnText = 1.ToString();
                            p2.BtnText = "...";
                            p3.BtnText = PageIndex.ToString();
                            p4.BtnText = "...";
                            p5.BtnText = PageCount.ToString();
                        }

                        for (int i = 0; i < 5; i++)
                        {
                            var idx = i + 1;
                            WDBtnExt c = (WDBtnExt)this.flowLayoutPanel1.Controls.Find("p" + idx, false)[0];
                            c.Visible = true;
                            SetColor(idx, c);
                        }
                    }
                }
                ShowBtn(PageIndex > 1, PageIndex < PageCount);
            }
            finally
            {
                lblTotalCount.Text = "共 " + TotalCount + " 条";
                ControlHelper.FreezeControl(this.flowLayoutPanel1, false);
                flowLayoutPanel1.ResumeLayout(true);
            }
        }

        private void SetColor(int idx, WDBtnExt c)
        {
            c.BtnEnabled = c.BtnText != "...";
            c.Enabled = c.BtnText != "...";
            if (c.BtnText == PageIndex.ToString())
            {
                c.FillColor = WDColors.StatusBlue;
                c.BtnForeColor = WDColors.WhiteColor;
            }
            else
            {
                c.FillColor = WDColors.WhiteColor;
                c.BtnForeColor = SystemColors.ControlText;
            }
        }





        /// <summary>
        /// to page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void page_BtnClick(object sender, EventArgs e)
        {
            PageIndex = (sender as WDBtnExt).BtnText.ToInt();
            StartIndex = (PageIndex - 1) * PageSize;
            OnPageChanged();
        }






        protected override void ShowBtn(bool blnLeftBtn, bool blnRightBtn)
        {
            btnFirst.Enabled = btnPrevious.Enabled = blnLeftBtn;
            btnNext.Enabled = btnEnd.Enabled = blnRightBtn;
        }






        private void btnFirst_BtnClick(object sender, EventArgs e)
        {
            FirstPage();
        }






        private void btnPrevious_BtnClick(object sender, EventArgs e)
        {
            PreviousPage();
        }






        private void btnNext_BtnClick(object sender, EventArgs e)
        {
            NextPage();
        }






        private void btnEnd_BtnClick(object sender, EventArgs e)
        {
            EndPage();
        }





        /// <summary>
        /// goto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnToPage_BtnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPage.InputText))
            {
                PageIndex = txtPage.InputText.ToInt();
                StartIndex = (PageIndex - 1) * PageSize;
                OnPageChanged();
                txtPage.InputText = "";
            }
        }

    }
}

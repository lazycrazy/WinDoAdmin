using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YKD_Controls.Forms;
using MIP.Utilities.PublicResource;
using DevExpress.XtraPdfViewer;

namespace MIP.UI.Utilities.Pdf
{
    public partial class FrmWith2WordsOk : FrmBase
    {
        public FrmWith2WordsOk()
        {
            
        }

        string pdfSrc = string.Empty;
        public FrmWith2WordsOk(string fileSrc)
        {
            InitializeComponent();
            InitFormMove(this.lblTitle);
            btnClose.BackgroundImage = FormHelper.GetIconImage("E_icon_close", 38, YkdBasisColors.WhiteColor);
            btnClose.BackgroundImageLayout = ImageLayout.Center;
            //SizeChanged += new EventHandler(FrmQueryWithOk_SizeChanged);
            WindowState = FormWindowState.Normal;
            Load += new EventHandler(FrmWith2WordsOk_Load_1);
            btnClose.Click += new EventHandler(btnClose_Click);
            BorderStyleColor = YkdBasisColors.MainColor;
            pdfSrc = fileSrc;
        }

        void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        

        private void pdfViewer1_Load(object sender, EventArgs e)
        {
            this.pdfViewer1.DocumentFilePath = pdfSrc;
            this.label1.Text = this.pdfViewer1.CurrentPageNumber.ToString() + "/" + this.pdfViewer1.PageCount.ToString();
        }

        private void pdfViewer1_ScrollPositionChanged(object sender, PdfScrollPositionChangedEventArgs e)
        {
            this.label1.Text = this.pdfViewer1.CurrentPageNumber.ToString() + "/" + this.pdfViewer1.PageCount.ToString();
        }

        private void FrmWith2WordsOk_Load_1(object sender, EventArgs e)
        {
            if (!IsShowMaskDialog)
            {
                if (!DesignMode)
                {
                    var shadow = new Dropshadow(this)
                    {
                        ShadowV = 3,
                        ShadowBlur = 15,
                        ShadowSpread = -7,
                        ShadowColor = Color.DimGray

                    };
                    shadow.RefreshShadow();
                }
            }
        }

        private void panel5_Click(object sender, EventArgs e)
        {
            this.pdfViewer1.ZoomMode = PdfZoomMode.Custom;
            this.pdfViewer1.ZoomFactor += 20;
            this.pdfViewer1.Refresh();
        }

        private void PinterPal_Click(object sender, EventArgs e)
        {
            this.pdfViewer1.Print();
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            this.pdfViewer1.ZoomMode = PdfZoomMode.Custom;
            this.pdfViewer1.ZoomFactor -= 20;
            this.pdfViewer1.Refresh();
        }

        private void panel6_Click(object sender, EventArgs e)
        {
            this.pdfViewer1.ZoomMode = DevExpress.XtraPdfViewer.PdfZoomMode.FitToWidth;
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

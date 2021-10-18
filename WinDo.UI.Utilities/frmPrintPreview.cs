using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MIP.UI.Utilities.DialogForm;
using System.Reflection;
using Spire.Pdf;

namespace MIP.UI.Utilities
{
    public partial class frmPrintPreview : FrmWith2WordsOk
    {
        public frmPrintPreview(List<string> _lstPDF)
        {
            InitializeComponent();
            this.lstPDF = _lstPDF;
        }

        private FieldInfo m_Position;
        private MethodInfo m_SetPositionMethod;

        private List<string> lstPDF;

        private void frmPrintPreview_Load(object sender, EventArgs e)
        {
            Type type = typeof(System.Windows.Forms.PrintPreviewControl);

            m_Position = type.GetField("position", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.ExactBinding);
            m_SetPositionMethod = type.GetMethod("SetPositionNoInvalidate", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.ExactBinding);
            printPreviewControl1.MouseWheel += new MouseEventHandler(printPreviewControl1_MouseWheel);

            Preview();
        }

        PdfDocument doc;
        private void Preview()
        {
            doc = new PdfDocument();
            PdfDocument doc1 = new PdfDocument();


            if (lstPDF.Count >0)
            {
                doc.LoadFromFile(lstPDF[0]);

                for (int i = 1; i < lstPDF.Count; i++)
                {
                    doc1.LoadFromFile(lstPDF[i]);

                    doc.AppendPage(doc1);
                }

                printPreviewControl1.StartPage = 0;
                printPreviewControl1.Rows = doc.Pages.Count;

                doc.Preview(printPreviewControl1);

            }
            

            //doc.SaveToFile("D:\\test.pdf");

            
        }

        public void printPreviewControl1_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                if (!SystemInformation.MouseWheelPresent)
                {
                    //If have no wheel      
                    return;
                }
                int scrollAmount;
                float amount = Math.Abs(e.Delta) / SystemInformation.MouseWheelScrollDelta;
                amount *= SystemInformation.MouseWheelScrollLines;
                amount *= 20;//Row height      
                amount *= (float)printPreviewControl1.Zoom;//Zoom Rate      
                if (e.Delta < 0)
                {
                    scrollAmount = (int)amount;
                }
                else
                {
                    scrollAmount = -(int)amount;
                }
                Point curPos = (Point)(m_Position.GetValue(printPreviewControl1));
                m_SetPositionMethod.Invoke(printPreviewControl1, new object[] { new Point(curPos.X + 0, curPos.Y + scrollAmount) });
            }
            catch (Exception)
            {
                
                //throw;
            }
            
        }

        private void btnOk_BtnClick(object sender, EventArgs e)
        {
            //打印
            if (doc.Pages.Count >0)
            {
                doc.Print();
            }
        }
    }
}

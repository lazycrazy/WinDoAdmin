using WinDo.Utilities;
using WinDo.Utilities.PublicResource;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using WinDoControls;
using WinDoControls.Forms;

namespace WinDo
{
    public static class DataGridViewHelper
    {
        #region 单元格进度条式样

        #region Bar式样
        /// <summary>
        /// 列绑定进度条式样1
        /// DataGridViewHelper.SetColumnBarProcessBar(dataGridView1.Columns[3], (dgv, rowidx) =>
        //{  // 根据dgv和当前行号 rowidx，获取当前行进度条的数据，最大值，当前值，前景色，tips文本行，比如：
        //    var o = new Tuple<int, int, Color, List<string>>(100, 12, Color.Red, new List<string>() {
        //        "1111",
        //        "222",
        //        "1133311",
        //    });
        //    return o;
        //});
        /// </summary>
        /// <param name="column">绑定的列</param>
        /// <param name="getBarValues">获取进度条的数据，最大值，当前值，前景色，tips文本行</param>
        public static void SetColumnBarProcessBar(DataGridViewColumn column, Func<DataGridView, int, Tuple<int, int, Color, object>> getBarValues)
        {
            var dgv = column.DataGridView;

            dgv.CellPainting += (object sender, DataGridViewCellPaintingEventArgs e) =>
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                if (dgv.Columns[e.ColumnIndex] != column) return;
                e.PaintBackground(e.CellBounds, true);
                var values = getBarValues(dgv, e.RowIndex);
                DrawPercentProgress1(e.Graphics, e.CellBounds, values.Item1, values.Item2, values.Item3, "");
                e.Handled = true;
            };

            dgv.CellMouseMove += (object sender, DataGridViewCellMouseEventArgs e) =>
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                if (dgv.Columns[e.ColumnIndex] != column) return;
                var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                var barSize = new Size(120, 14);
                var x1 = (cell.Size.Width - barSize.Width) / 2;
                var y1 = (cell.Size.Height - barSize.Height) / 2;
                var rect1 = new Rectangle(x1, y1, barSize.Width, barSize.Height);
                if (rect1.Contains(e.Location))
                {
                    var values = getBarValues(dgv, e.RowIndex);
                    ShowBarTips(e, dgv, rect1, values.Item4);
                }
                else CloseBarTips();
            };
            dgv.CellMouseLeave += (object sender, DataGridViewCellEventArgs e) =>
            {
                CloseBarTips();
            };
        }
        public static void SetColumnBarProcessBarForDoctorDashBoard(DataGridViewColumn column, Func<DataGridView, int, Tuple<int, int, Color, object, string, string>> getBarValues)
        {
            if (getBarValues == null) return;
            var dgv = column.DataGridView;
            dgv.CellPainting += (object sender, DataGridViewCellPaintingEventArgs e) =>
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                if (dgv.Columns[e.ColumnIndex] != column) return;
                e.PaintBackground(e.CellBounds, true);
                var values = getBarValues(dgv, e.RowIndex);
                if (values == null)
                    return;
                if (values.Item5 == "2")
                {
                    DrawPercentProgress1(e.Graphics, e.CellBounds, values.Item1, values.Item2, values.Item3, "");
                }
                else if (values.Item5 == "3")
                {
                    var g = e.Graphics;
                    var rect = e.CellBounds;
                    g.SetGDIHigh();
                    var txt = values.Item6;
                    Color fontBackColor = Color.Black;
                    if (dgv.Rows[e.RowIndex].Selected)
                        fontBackColor = Color.White;
                    SolidBrush brush = new SolidBrush(fontBackColor);
                    Font font = WDFonts.TextFont;
                    var txtsize = TextRenderer.MeasureText(txt, WDFonts.TextFont);
                    var width = txtsize.Width + 4;
                    var tRect = rect;
                    tRect.Width = width;
                    tRect.Height = txtsize.Height + 2;
                    tRect.Offset(0, (rect.Height - tRect.Height) / 2);
                    var sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                    g.DrawString(txt, font, brush, tRect, sf);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                }
                e.Handled = true;
            };

            dgv.CellMouseMove += (object sender, DataGridViewCellMouseEventArgs e) =>
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                if (dgv.Columns[e.ColumnIndex] != column) return;
                var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                var barSize = new Size(120, 14);
                var x1 = (cell.Size.Width - barSize.Width) / 2;
                var y1 = (cell.Size.Height - barSize.Height) / 2;
                var rect1 = new Rectangle(x1, y1, barSize.Width, barSize.Height);
                if (rect1.Contains(e.Location))
                {
                    var values = getBarValues(dgv, e.RowIndex);
                    if (values.Item5 != "2")
                    {
                        CloseBarTips();
                        return;
                    }
                    ShowBarTips(e, dgv, rect1, values.Item4);
                }
                else CloseBarTips();
            };
            dgv.CellMouseLeave += (object sender, DataGridViewCellEventArgs e) =>
            {
                CloseBarTips();
            };
        }


        private static void ShowBarTips(DataGridViewCellMouseEventArgs e, DataGridView dgv, Rectangle rect, object tips)
        {
            var cellRect = dgv.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
            var dgvPoint1 = dgv.Parent.PointToScreen(dgv.Location);
            cellRect.Offset(dgvPoint1);
            rect.Offset(cellRect.Location);
            if (frmAnchorBarTips != null && frmAnchorBarTips.RectControl != rect)
            {
                frmAnchorBarTips.Close();
                frmAnchorBarTips = null;
            }
            if (tips == null) return;
            if (frmAnchorBarTips == null)
            {
                dynamic data = tips;
                var txtHeight = TextRenderer.MeasureText(" ", WDFonts.TextFont).Height;



                var height = 80;
                var width = 200;
                int addHeight = 0;
                if (data.ProNumber == 4)
                {
                    //治疗Tips
                    var TreatDetail = data.TreatDetail as IEnumerable<dynamic>;
                    if (TreatDetail != null)
                    {
                        if (TreatDetail.Count() > 1)
                            height = Math.Max(height, (TreatDetail.Count() * txtHeight) + 40);
                        else
                            height = Math.Max(height, (TreatDetail.Count() * txtHeight));
                        var maxRow = TreatDetail.Select(d => "Rx：" + d.Site_Name + "  ACT：" + d.TimesDose_Tx + "/" + d.Dose_Ttl + "cGy  Fx：" + d.FxCount + "/" + d.Fractions + "  最后一次治疗：" + d.LastExec_DtTm + "").Cast<string>().OrderByDescending(d => d.Length).First();
                        width = Math.Max(width, TextRenderer.MeasureText(maxRow, WDFonts.TextFont).Width + 60);
                    }
                }
                var sf = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
                //自定义打印行
                frmAnchorBarTips = FrmAnchorTips_CustomDraw.ShowTips(rect, AnchorTipsLocation.BOTTOM, WDColors.TaskListTip, autoCloseTime: 20000,
                    getSize: () =>
                    {
                        return new Size(width, height);
                    },
                    draw: (g) =>
                    {
                        if (data.ProNumber == 4)
                        {
                            var TreatDetail = data.TreatDetail as IEnumerable<dynamic>;
                            if (TreatDetail != null)
                            {
                                var trect = new Rectangle(20, 20, width - 20, txtHeight);
                                foreach (dynamic d in TreatDetail)
                                {
                                    var info = "Rx：" + d.Site_Name.ToString() + "  ACT：" + d.TimesDose_Tx.ToString() + "/" + d.Dose_Ttl.ToString() + "cGy  Fx：" + d.FxCount.ToString() + "/" + d.Fractions.ToString() + "  最后一次治疗：" + d.LastExec_DtTm.ToString() + "";
                                    var fcolor = "0" == d.IsExcess.ToString() ? Color.White : Color.Red;// 文字颜色区分条件
                                    using (var brush = new SolidBrush(fcolor))
                                        g.DrawString(info, WDFonts.TextFont, brush, trect, sf);
                                    trect.Offset(0, txtHeight);
                                }
                            }
                        }
                    });
                frmAnchorBarTips.ResetForm();
            }
        }



        /// <summary>
        /// 简单百分比进度条；调用方式
        /// DataGridView_CellPainting 事件中重绘单元格：
        /// DataGridViewHelper.DrawPercentProgress1(e.Graphics, e.CellBounds, 100, 30, Color.Red);
        /// </summary>
        /// <param name="g">e.Graphics</param>
        /// <param name="cellBounds">单元格e.CellBounds</param>
        /// <param name="maxValue">最大值</param>
        /// <param name="value">当前值</param>
        /// <param name="valueColor">当前值显示颜色</param>
        private static void DrawPercentProgress1(Graphics g, Rectangle cellBounds, int maxValue, int value, Color valueColor, string ExceTreat)
        {
            var percent = (double)value / maxValue;
            var barSize = new Size(120, 14);
            g.SetGDIHigh();
            var rect = cellBounds;
            rect.Offset(rect.GetCenterRangeLocation(barSize));
            rect.Size = barSize;
            using (var brush = new SolidBrush(ColorTranslator.FromHtml("#CFCFCF")))
                ControlHelper.FillRoundRectangle(g, brush, rect, barSize.Height / 2);

            var rectCover = rect;
            if (percent <= 0.1)
            {
                var width = percent * rect.Width;
                rectCover.Width = Math.Ceiling(width).ToInt();
                using (var path = rect.CreateRoundedRectanglePath(barSize.Height / 2))
                {
                    using (var rRegion = new Region(rectCover))
                    {
                        rRegion.Intersect(path);
                        var height = rRegion.GetBounds(g).Height.ToInt();
                        rectCover.Offset(0, (rectCover.Height - height) / 2);
                        using (var brush = new SolidBrush(valueColor))
                            g.FillEllipse(brush, rectCover.X, rectCover.Y, rectCover.Width, height);
                    }
                }
            }
            else if (percent > 0.1 && percent < 1)
            {
                var width = percent * rect.Width;
                rectCover.Width = Math.Ceiling(width).ToInt();
                using (var brush = new SolidBrush(valueColor))
                    ControlHelper.FillRoundRectangle(g, brush, rectCover, barSize.Height / 2);
            }
            else if (percent >= 1)
            {
                var width = rect.Width;
                rectCover.Width = width;
                using (var brush = new SolidBrush(valueColor))
                    ControlHelper.FillRoundRectangle(g, brush, rectCover, barSize.Height / 2);
            }
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            var sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            var sf2 = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
            var tRect = value == 0 ? rect : rectCover;
            if (maxValue == 0) return;
            var txt = value + "/" + maxValue;
            var tSize = TextRenderer.MeasureText(txt, WDFonts.TextFont12);
            tRect.Width = Math.Max(tRect.Width, tSize.Width);
            g.DrawString(txt, WDFonts.TextFont12, Brushes.Black, tRect, sf);
            //--------------------------------------------
            tRect.Offset(tRect.Width / 2 + 13, 0);
            tRect.Width += 20;
            if (valueColor == Color.FromArgb(255, 0, 190, 213))
                g.DrawString(ExceTreat, WDFonts.TextFont12, Brushes.Red, tRect, sf2);
            else
                g.DrawString(ExceTreat, WDFonts.TextFont12, Brushes.Black, tRect, sf2);
            //--------------------------------------------
        }

        private static FrmAnchorTips_CustomDraw frmAnchorBarTips;

        private static void CloseBarTips()
        {
            if (frmAnchorBarTips != null)
            {
                frmAnchorBarTips.Close();
                frmAnchorBarTips = null;
            }
        }
        #endregion

        #region 箭头进度条式样
        /// <summary>
        /// 箭头进度条式样
        /// DataGridViewHelper.SetColumnArrowProcessBar(dataGridView1.Columns[3], (dgv, rowidx) =>
        //    {  
        //        // 根据dgv和当前行号 rowidx，获取当前行，4个进度条的百分比值和4个对应的tips，比如：
        //        double[] values = new double[4] { 0.1, 0.2, 0.3, 1 };
        //        List<string>[] tips = Enumerable.Range(0, 4).Select(i => Enumerable.Repeat(string.Concat(Enumerable.Repeat(i, i)), 10).ToList()).ToArray();
        //        var o = new Tuple<double[], List<string>[]>(values, tips);
        //        return o;
        //    });
        /// </summary>
        /// <param name="column"></param>
        /// <param name="getBarValues"></param>
        public static void SetColumnArrowProcessBar(DataGridViewColumn column, Func<DataGridView, int, Tuple<double[], object[]>> getBarValues)
        {
            var dgv = column.DataGridView;
            dgv.CellPainting += (object sender, DataGridViewCellPaintingEventArgs e) =>
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                if (dgv.Columns[e.ColumnIndex] != column) return;
                e.PaintBackground(e.CellBounds, true);
                var values = getBarValues(dgv, e.RowIndex);
                DrawPercentProgress2(e.Graphics, e.CellBounds, values.Item1, (e.CellBounds.Width - 185) / 2, (e.CellBounds.Height - 14) / 2);
                e.Handled = true;
            };

            dgv.CellMouseMove += (object sender, DataGridViewCellMouseEventArgs e) =>
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                if (dgv.Columns[e.ColumnIndex] != column) return;
                var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                var barSize = new Size(185, 14);
                var rect_width = 43;
                var arrow_space_width = 3;
                var x1 = (cell.Size.Width - barSize.Width) / 2;
                var y1 = (cell.Size.Height - barSize.Height) / 2;
                var rect1 = new Rectangle(x1, y1, rect_width, barSize.Height);
                var rect2 = rect1;
                rect2.Offset(rect2.Width + arrow_space_width, 0);
                var rect3 = rect2;
                rect3.Offset(rect3.Width + arrow_space_width, 0);
                var rect4 = rect3;
                rect4.Offset(rect4.Width + arrow_space_width, 0);
                var values = getBarValues(dgv, e.RowIndex);
                var strs = values.Item2;
                if (rect1.Contains(e.Location))
                {
                    ShowArrowBarTips(e, dgv, rect1, strs[0]);
                }
                else if (rect2.Contains(e.Location))
                {
                    ShowArrowBarTips(e, dgv, rect2, strs[1]);
                }
                else if (rect3.Contains(e.Location))
                {
                    ShowArrowBarTips(e, dgv, rect3, strs[2]);
                }
                else if (rect4.Contains(e.Location))
                {
                    ShowArrowBarTips(e, dgv, rect4, strs[3]);
                }
                else CloseArrowBarTips();
            };
            dgv.CellMouseLeave += (object sender, DataGridViewCellEventArgs e) =>
            {
                CloseArrowBarTips();
            };

        }


        public static void SetColumnArrowProcessBarWithInfo(DataGridViewColumn column, Func<DataGridView, int, Tuple<double[], object[], string>> getBarValues)
        {
            var dgv = column.DataGridView;
            dgv.CellPainting += (object sender, DataGridViewCellPaintingEventArgs e) =>
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                if (dgv.Columns[e.ColumnIndex] != column) return;
                e.PaintBackground(e.CellBounds, true);
                using (var pen = new Pen(WDColors.GrayBackColor))
                    e.Graphics.DrawLine(pen, e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Right, e.CellBounds.Top);
                var values = getBarValues(dgv, e.RowIndex);
                if (values != null)
                {
                    var foreColor = Color.Black;
                    if (dgv.Rows[e.RowIndex].Selected)
                        foreColor = Color.White;
                    dynamic dr = dgv.Rows[e.RowIndex].DataBoundItem;
                    if (dr.HasCRB)
                        foreColor = WDColors.CRBColor;
                    using (var brush = new SolidBrush(foreColor))
                        e.Graphics.DrawString(values.Item3, WDFonts.TextFont, brush, e.CellBounds.X, e.CellBounds.Y + 20);
                    DrawPercentProgress2(e.Graphics, e.CellBounds, values.Item1, 0, 44);
                }
                e.Handled = true;
            };

            dgv.CellMouseMove += (object sender, DataGridViewCellMouseEventArgs e) =>
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                if (dgv.Columns[e.ColumnIndex] != column) return;
                var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                var barSize = new Size(185, 14);
                var rect_width = 43;
                var arrow_space_width = 3;
                var x1 = 0;
                var y1 = 44;
                var rect1 = new Rectangle(x1, y1, rect_width, barSize.Height);
                var rect2 = rect1;
                rect2.Offset(rect2.Width + arrow_space_width, 0);
                var rect3 = rect2;
                rect3.Offset(rect3.Width + arrow_space_width, 0);
                var rect4 = rect3;
                rect4.Offset(rect4.Width + arrow_space_width, 0);
                var values = getBarValues(dgv, e.RowIndex);
                if (values != null)
                {
                    var strs = values.Item2;
                    if (rect1.Contains(e.Location))
                    {
                        ShowArrowBarTips(e, dgv, rect1, strs[0]);
                    }
                    else if (rect2.Contains(e.Location))
                    {
                        ShowArrowBarTips(e, dgv, rect2, strs[1]);
                    }
                    else if (rect3.Contains(e.Location))
                    {
                        ShowArrowBarTips(e, dgv, rect3, strs[2]);
                    }
                    else if (rect4.Contains(e.Location))
                    {
                        ShowArrowBarTips(e, dgv, rect4, strs[3]);
                    }
                }
                else CloseArrowBarTips();
            };
            dgv.CellMouseLeave += (object sender, DataGridViewCellEventArgs e) =>
            {
                CloseArrowBarTips();
            };

        }


        private static FrmAnchorTips_CustomDraw frmAnchorArrowBarTips = null;


        private static void CloseArrowBarTips()
        {
            if (frmAnchorArrowBarTips != null)
            {
                frmAnchorArrowBarTips.Close();
                frmAnchorArrowBarTips = null;
            }
        }
        private static void ShowArrowBarTips(DataGridViewCellMouseEventArgs e, DataGridView dgv, Rectangle rect, object tip)
        {
            var cellRect = dgv.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
            var dgvPoint1 = dgv.Parent.PointToScreen(dgv.Location);
            cellRect.Offset(dgvPoint1);
            rect.Offset(cellRect.Location);
            if (frmAnchorArrowBarTips != null && frmAnchorArrowBarTips.RectControl != rect)
            {
                frmAnchorArrowBarTips.Close();
                frmAnchorArrowBarTips = null;
            }
            if (tip == null) return;
            if (string.IsNullOrWhiteSpace(tip.ToString())) return;
            if (frmAnchorArrowBarTips == null)
            {
                dynamic data = tip;
                var txtHeight = TextRenderer.MeasureText(" ", WDFonts.TextFont).Height;
                var height = 80;
                var width = 210;
                int addHeight = 0;
                if (data.ProNumber == 4)
                {
                    //治疗Tips
                    var TreatDetail = data.TreatDetail as IEnumerable<dynamic>;
                    if (TreatDetail != null)
                    {
                        height = Math.Max(height, TreatDetail.Count() * txtHeight);
                        var maxRow = TreatDetail.Select(d => "Rx：" + d.Site_Name + "  ACT：" + d.TimesDose_Tx + "/" + d.Dose_Ttl + "cGy  Fx：" + d.FxCount + "/" + d.Fractions + "  最后一次治疗：" + d.LastExec_DtTm + "").Cast<string>().OrderByDescending(d => d.Length).First();
                        width = Math.Max(width, TextRenderer.MeasureText(maxRow, WDFonts.TextFont).Width + 50);
                    }
                }
                else
                {
                    height = 140;
                    var ProgressDetail = data.ProgressDetail as IEnumerable<dynamic>;
                    if (ProgressDetail != null && ProgressDetail.Count() > 0)
                    {
                        height += ProgressDetail.Count() * txtHeight;
                        var maxRow = ProgressDetail.Select(d =>
                          d.Progress_Name.ToString() + "" + d.Progress_Status.ToString() + "" + d.Progress_DtTm.ToString() + ""
                        ).Cast<string>().OrderByDescending(d => d.Length).First();
                        width = Math.Max(width, TextRenderer.MeasureText(maxRow, WDFonts.TextFont).Width + 100);
                    }
                }
                var sf = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
                //自定义打印行
                frmAnchorArrowBarTips = FrmAnchorTips_CustomDraw.ShowTips(rect, AnchorTipsLocation.BOTTOM, WDColors.TaskListTip, autoCloseTime: 20000,
                    getSize: () =>
                    {
                        return new Size(width, height);
                    },
                    draw: (g) =>
                    {
                        if (data.ProNumber == 4)
                        {
                            var TreatDetail = data.TreatDetail as IEnumerable<dynamic>;
                            if (TreatDetail != null)
                            {
                                var trect = new Rectangle(20, 20, width - 20, txtHeight);
                                foreach (dynamic d in TreatDetail)
                                {
                                    var info = "Rx：" + d.Site_Name.ToString() + "  ACT：" + d.TimesDose_Tx.ToString() + "/" + d.Dose_Ttl.ToString() + "cGy  Fx：" + d.FxCount.ToString() + "/" + d.Fractions.ToString() + "  最后一次治疗：" + d.LastExec_DtTm.ToString() + "";
                                    var fcolor = "0" == d.IsExcess.ToString() ? Color.White : Color.Red;// 文字颜色区分条件
                                    using (var brush = new SolidBrush(fcolor))
                                        g.DrawString(info, WDFonts.TextFont, brush, trect, sf);
                                    trect.Offset(0, txtHeight);
                                }
                            }
                        }
                        else
                        {
                            var trect = new Rectangle(20, 20, width - 40, txtHeight);
                            g.DrawString("当前进度：", WDFonts.TextFontBold, Brushes.White, trect, sf);
                            trect.Offset(0, txtHeight);
                            g.DrawString(data.CurrentProgress.ToString() + "  " + data.CurrentPrgs_DtTm.ToString(), WDFonts.TextFont, Brushes.White, trect, sf);
                            trect.Offset(0, txtHeight);
                            trect.Offset(0, txtHeight);
                            g.DrawString("进度详情：", WDFonts.TextFontBold, Brushes.White, trect, sf);
                            trect.Offset(0, txtHeight);

                            var ProgressDetail = data.ProgressDetail as IEnumerable<dynamic>;
                            if (ProgressDetail != null && ProgressDetail.Count() > 0)
                            {
                                var maxRow = ProgressDetail.Select(d => d.Progress_Name.ToString() + "").Cast<string>().OrderByDescending(d => d.Length).First();
                                var fwidth = TextRenderer.MeasureText(maxRow, WDFonts.TextFont).Width;
                                foreach (dynamic d in ProgressDetail)
                                {
                                    var fcolor = "已完成" == d.Progress_Status.ToString() ? Color.White : WDColors.AskYellowColor;
                                    using (var brush = new SolidBrush(fcolor))
                                    {
                                        sf.Alignment = StringAlignment.Far;
                                        g.DrawString(d.Progress_DtTm.ToString(), WDFonts.TextFont, brush, trect, sf);
                                        sf.Alignment = StringAlignment.Near;
                                        g.DrawString(d.Progress_Name.ToString(), WDFonts.TextFont, brush, trect, sf);

                                        var crect = trect;
                                        crect.Offset(fwidth + 20, 0);
                                        g.DrawString(d.Progress_Status.ToString(), WDFonts.TextFont, brush, crect, sf);

                                        trect.Offset(0, txtHeight);
                                    }
                                }

                            }
                        }
                    });
                frmAnchorArrowBarTips.ResetForm();
            }
        }


        public static Color[] BarColors = new Color[] {
        ColorTranslator.FromHtml("#C6E5B2"),
        ColorTranslator.FromHtml("#BFDBFC"),
        ColorTranslator.FromHtml("#F5DAC0"),
        ColorTranslator.FromHtml("#E0C9EE"),
        };
        public static Color[] FinishBarColors = new Color[] {
        ColorTranslator.FromHtml("#149A18"),
        ColorTranslator.FromHtml("#105EDB"),
        ColorTranslator.FromHtml("#E36C0D"),
        ColorTranslator.FromHtml("#8E36BB"),
        };

        /// <summary>
        /// 4个色块的进度条
        /// </summary>
        /// <param name="g"></param>
        /// <param name="cellBounds">单元格e.CellBounds</param>
        /// <param name="percents">4项对应的完成百分比值,new[]{0.1,0.2,0.95,1}</param>
        private static void DrawPercentProgress2(Graphics g, Rectangle cellBounds, double[] percents, int x, int y)
        {
            g.SetGDIHigh();
            var barSize = new Size(185, 14);
            var rect_width = 43;
            var arrow_space_width = 3;
            var brect = cellBounds;
            brect.Offset(x, y);
            brect.Width = rect_width;
            brect.Height = barSize.Height;
            var arrow_width = 5;
            g.FillRectangle(Brushes.White, brect.X, brect.Y, barSize.Width - arrow_width, barSize.Height);
            for (int i = 0; i < BarColors.Length; i++)
            {
                var first = i == 0;
                var rect = brect;
                rect.Offset((rect.Width + arrow_space_width) * i, 0);
                List<PointF> points = new List<PointF>();
                points.Add(new PointF(rect.X, rect.Y));
                points.Add(new PointF(rect.Right - arrow_width, rect.Y));
                points.Add(new PointF(rect.Right, rect.Y + rect.Height / 2.0f));
                points.Add(new PointF(rect.Right - arrow_width, rect.Bottom));
                points.Add(new PointF(rect.X, rect.Bottom));
                points.Add(new PointF(rect.X + arrow_width, rect.Y + rect.Height / 2.0f));
                points.Add(new PointF(rect.X, rect.Y));
                Point[] pts = new Point[points.Count];
                for (int idx = 0; idx < points.Count; idx++)
                {
                    pts[idx] = new Point((int)points[idx].X, (int)points[idx].Y);
                }
                using (var brush = new SolidBrush(BarColors[i]))
                    g.FillPolygon(brush, pts);
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddPolygon(pts);
                    //覆盖已完成色块
                    //path.IsVisible

                    var coverRect = rect;
                    coverRect.Width = (percents[i] * (brect.Width + arrow_width)).ToInt();
                    using (var cRegion = new Region(coverRect))
                    {
                        cRegion.Intersect(path);
                        var coverSize = cRegion.GetBounds(g).Size;
                        //g.DrawRectangle(Pens.Aqua, coverRect);
                        using (var brush = new SolidBrush(FinishBarColors[i]))
                            g.FillRegion(brush, cRegion);
                    }
                }
            }
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
        }




        #endregion

        #endregion

        #region 设置CheckBox列

        public static void SetCheckBoxColumn(this DataGridViewColumn column,
            Func<DataGridView, int, int, bool> isChecked,
            Action<DataGridView, int, int> setChecked, bool bindMode = true, bool drawHead = true)
        {
            var dgv = column.DataGridView;
            var img_unchecked = WinDoControls.Properties.Resources.UnCheckedBox;
            var img_checked = WinDoControls.Properties.Resources.CheckedBox;
            dgv.CellPainting += (object sender, DataGridViewCellPaintingEventArgs e) =>
            {
                if (dgv.Columns[e.ColumnIndex] != column) return;
                var cellRect = e.CellBounds;
                if (e.RowIndex == -1)
                {
                    if (!drawHead) return;
                    e.PaintBackground(e.CellBounds, true);

                    var all_checked = isChecked(dgv, e.RowIndex, e.ColumnIndex);
                    var img_a = all_checked ? img_checked : img_unchecked;
                    var p_a = e.CellBounds.GetCenterRangeLocation(img_unchecked.Size);
                    cellRect.Offset(p_a);
                    e.Graphics.DrawImage(img_a, cellRect.Location);
                    e.Handled = true;
                    return;
                }
                if (e.RowIndex < 0) return;
                e.PaintBackground(e.CellBounds, true);

                var img = isChecked(dgv, e.RowIndex, e.ColumnIndex) ? img_checked : img_unchecked;
                var p = e.CellBounds.GetCenterRangeLocation(img_unchecked.Size);
                cellRect.Offset(p);
                e.Graphics.DrawImage(img, cellRect.Location);
                e.Handled = true;
            };
            dgv.CellMouseClick += (object sender, DataGridViewCellMouseEventArgs e) =>
            {
                if (dgv.Columns[e.ColumnIndex] != column) return;
                if (e.RowIndex < 0) return;
                var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (RectangleContains(cell.Size, img_unchecked.Size, e.Location))
                {
                    setChecked(dgv, e.RowIndex, e.ColumnIndex);
                    dgv.InvalidateCell(e.ColumnIndex, -1);
                }
            };
            dgv.ColumnHeaderMouseClick += (object sender, DataGridViewCellMouseEventArgs e) =>
            {
                if (dgv.Columns[e.ColumnIndex] != column) return;
                if (bindMode && dgv.DataSource == null) return;
                var cell = dgv.Columns[e.ColumnIndex].HeaderCell;
                if (RectangleContains(cell.Size, img_unchecked.Size, e.Location))
                {
                    setChecked(dgv, e.RowIndex, e.ColumnIndex);
                    dgv.InvalidateColumn(e.ColumnIndex);
                }
            };

            dgv.CellMouseMove += (object sender, DataGridViewCellMouseEventArgs e) =>
            {
                if (dgv.Columns[e.ColumnIndex] != column) return;
                var cell = e.RowIndex == -1 ? dgv.Columns[e.ColumnIndex].HeaderCell : dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (RectangleContains(cell.Size, img_unchecked.Size, e.Location))
                    dgv.Cursor = Cursors.Hand;
                else
                    dgv.Cursor = Cursors.Default;
            };

            dgv.CellMouseLeave += (object sender, DataGridViewCellEventArgs e) =>
            {
                if (dgv.Columns[e.ColumnIndex] != column) return;
                dgv.Cursor = Cursors.Default;
            };
        }

        #endregion

        #region 设置Switch，文本框，自绘边框列

        public static void SetSwitchColumn(this DataGridViewColumn column,
            Func<DataGridView, int, int, bool> isSwitchOn,
            Action<DataGridView, int, int> setSwitchOn, bool allowHeadSwitch = false)
        {
            var dgv = column.DataGridView;
            var img_switch_of = WinDoControls.Properties.Resources.toggle_off;
            var img_switch_on = WinDoControls.Properties.Resources.toggle_on;
            dgv.CellPainting += (object sender, DataGridViewCellPaintingEventArgs e) =>
            {
                if (e.ColumnIndex < 0) return;
                if (dgv.Columns[e.ColumnIndex] != column) return;
                var cellRect = e.CellBounds;
                if (allowHeadSwitch && e.RowIndex == -1)
                {
                    e.PaintBackground(e.CellBounds, true);

                    var all_checked = isSwitchOn(dgv, e.RowIndex, e.ColumnIndex);
                    var img_a = all_checked ? img_switch_on : img_switch_of;
                    var p_a = e.CellBounds.GetCenterRangeLocation(img_switch_of.Size);
                    cellRect.Offset(p_a);
                    e.Graphics.DrawImage(img_a, cellRect.Location);
                    e.Handled = true;
                    return;
                }
                if (e.RowIndex < 0) return;
                e.PaintBackground(e.CellBounds, true);

                var img = isSwitchOn(dgv, e.RowIndex, e.ColumnIndex) ? img_switch_on : img_switch_of;
                var p = e.CellBounds.GetCenterRangeLocation(img_switch_of.Size);
                cellRect.Offset(p);
                e.Graphics.DrawImage(img, cellRect.Location);
                e.Handled = true;
            };
            dgv.CellMouseClick += (object sender, DataGridViewCellMouseEventArgs e) =>
            {
                if (e.ColumnIndex < 0) return;
                if (dgv.Columns[e.ColumnIndex] != column) return;
                if (e.RowIndex < 0) return;
                var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (RectangleContains(cell.Size, WDImages.ToggleSize, e.Location))
                {
                    setSwitchOn(dgv, e.RowIndex, e.ColumnIndex);
                    dgv.InvalidateCell(e.ColumnIndex, -1);
                }
            };
            dgv.ColumnHeaderMouseClick += (object sender, DataGridViewCellMouseEventArgs e) =>
            {
                if (e.ColumnIndex < 0) return;
                if (dgv.Columns[e.ColumnIndex] != column) return;
                if (!allowHeadSwitch) return;
                var cell = dgv.Columns[e.ColumnIndex].HeaderCell;
                if (RectangleContains(cell.Size, WDImages.ToggleSize, e.Location))
                {
                    setSwitchOn(dgv, e.RowIndex, e.ColumnIndex);
                    dgv.InvalidateColumn(e.ColumnIndex);
                }
            };

            dgv.CellMouseMove += (object sender, DataGridViewCellMouseEventArgs e) =>
            {
                if (e.ColumnIndex < 0) return;
                if (dgv.Columns[e.ColumnIndex] != column) return;
                if (!allowHeadSwitch && e.RowIndex == -1) return;
                var cell = e.RowIndex == -1 ? dgv.Columns[e.ColumnIndex].HeaderCell : dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (RectangleContains(cell.Size, WDImages.ToggleSize, e.Location))
                    dgv.Cursor = Cursors.Hand;
                else
                    dgv.Cursor = Cursors.Default;
            };

            dgv.CellMouseLeave += (object sender, DataGridViewCellEventArgs e) =>
            {
                if (e.ColumnIndex < 0) return;
                if (dgv.Columns[e.ColumnIndex] != column) return;
                dgv.Cursor = Cursors.Default;
            };
        }
        public static DataGridViewColumn SetTexBoxColumnBorderForBranch(this DataGridViewColumn column)
        {
            var sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            var dgv = column.DataGridView;
            column.DefaultCellStyle.Padding = new Padding(3, 5, 3, 5);
            dgv.CellPainting += (object sender, DataGridViewCellPaintingEventArgs e) =>
            {
                if (e.RowIndex <= 0 || e.ColumnIndex < 0)
                {
                    if (e.RowIndex == 0)
                    {
                        dgv.Rows[e.RowIndex].ReadOnly = true;
                    }
                    return;
                }
                if (dgv.Columns[e.ColumnIndex] != column) return;
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                var brect = e.CellBounds;
                brect.Width -= 4;
                brect.Height -= 8;
                brect.Offset(2, 4);
                ControlHelper.FillRoundRectangle(e.Graphics, Brushes.White, brect, 2);
                using (var pen = new Pen(WDColors.GrayRectColor))
                    ControlHelper.DrawRoundRectangle(e.Graphics, pen, brect, 2);
                if (e.FormattedValue != null)
                {
                    e.Graphics.DrawString(e.FormattedValue.ToString(), WDFonts.TextFont, Brushes.Black, brect, sf);
                }
                e.Handled = true;
            };
            return column;
        }
        public static DataGridViewColumn SetTexBoxColumnBorder(this DataGridViewColumn column)
        {
            var sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            var dgv = column.DataGridView;
            column.DefaultCellStyle.Padding = new Padding(3, 5, 3, 5);
            dgv.CellPainting += (object sender, DataGridViewCellPaintingEventArgs e) =>
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                if (dgv.Columns[e.ColumnIndex] != column) return;
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                var brect = e.CellBounds;
                brect.Width -= 4;
                brect.Height -= 8;
                brect.Offset(2, 4);
                ControlHelper.FillRoundRectangle(e.Graphics, Brushes.White, brect, 2);
                using (var pen = new Pen(WDColors.GrayRectColor))
                    ControlHelper.DrawRoundRectangle(e.Graphics, pen, brect, 2);
                if (e.FormattedValue != null)
                {
                    e.Graphics.DrawString(e.FormattedValue.ToString(), WDFonts.TextFont, Brushes.Black, brect, sf);
                }
                e.Handled = true;
            };
            return column;
        }
        public static DataGridViewColumn SetColumnBorder(this DataGridViewColumn column, Action<DataGridViewCellPaintingEventArgs> painting)
        {
            var dgv = column.DataGridView;
            dgv.CellPainting += (object sender, DataGridViewCellPaintingEventArgs e) =>
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                if (dgv.Columns[e.ColumnIndex] != column) return;
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                painting(e);
                e.Handled = true;
            };
            return column;
        }
        #endregion

        #region "自绘列"


        public static DataGridViewColumn SetColumnCustomDraw(this DataGridViewColumn column, Action<DataGridView, DataGridViewCellPaintingEventArgs> customPaint, bool handled = true)
        {
            var dgv = column.DataGridView;
            dgv.CellPainting += (object sender, DataGridViewCellPaintingEventArgs e) =>
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                if (dgv.Columns[e.ColumnIndex] != column) return;
                customPaint(dgv, e);
                e.Handled = handled;
            };
            return column;
        }


        #endregion

        #region "列CellFormatting" 
        public static void SetCellValue(this DataGridView dgv, List<string> skipFields = null)
        {
            dgv.CellFormatting += (sender, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
                var dr = dgv.Rows[e.RowIndex].DataBoundItem as IDictionary<string, object>;
                if (dr == null) return;
                var fieldName = dgv.Columns[e.ColumnIndex].DataPropertyName;
                if (skipFields != null && skipFields.Contains(fieldName))
                    return;
                if (!string.IsNullOrEmpty(fieldName) && dr.ContainsKey(fieldName))
                    e.Value = dr[dgv.Columns[e.ColumnIndex].DataPropertyName];
            };
        }
        public static DataGridViewColumn CellFormatting(this DataGridViewColumn column, Action<DataGridView, DataGridViewCellFormattingEventArgs> formatting)
        {
            var dgv = column.DataGridView;
            dgv.CellFormatting += (sender, ee) =>
            {
                if (ee.RowIndex < 0) return;
                if (dgv.Columns[ee.ColumnIndex] != column) return;
                formatting(dgv, ee);
            };
            return column;
        }
        #endregion

        #region "单元格一些效果"
        /// <summary>
        /// tips显示超长文本
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public static DataGridViewColumn ShowTipsOnOverLength(this DataGridViewColumn col)
        {
            FrmAnchorTips _frmAnchorTips = null;
            var dgv = col.DataGridView;
            dgv.ShowCellToolTips = false;
            dgv.CellMouseMove += (s, e) =>
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell.OwningColumn != col) return;
                if (cell.Size.Width < cell.PreferredSize.Width)
                {
                    var cellRect = dgv.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    var dgvPoint1 = dgv.Parent.PointToScreen(dgv.Location);
                    cellRect.Offset(dgvPoint1);
                    if (_frmAnchorTips != null && _frmAnchorTips.RectControl != cellRect)
                    {
                        _frmAnchorTips.Close();
                        _frmAnchorTips = null;
                    }
                    var tips = string.Join<string>("\r\n", StringHelper.Split(cell.FormattedValue.AsString(""), 50));
                    if (_frmAnchorTips == null)
                        _frmAnchorTips = FrmAnchorTips.ShowTips(cellRect, tips, AnchorTipsLocation.BOTTOM, WDColors.TaskListTip, autoCloseTime: 6000);

                }
            };
            dgv.CellMouseLeave += (s, e) =>
            {
                if (_frmAnchorTips != null)
                {
                    _frmAnchorTips.Close();
                    _frmAnchorTips = null;
                }
            };
            return col;
        }


        public static Image[] SortImgs = new[] {
            WinDoControls.FontImages.GetImage((WinDoControls.FontIcons)Enum.Parse(typeof(WinDoControls.FontIcons), "A_fa_caret_up"), 14, ColorTranslator.FromHtml("#b2b2b2")),
            WinDoControls.FontImages.GetImage((WinDoControls.FontIcons)Enum.Parse(typeof(WinDoControls.FontIcons), "A_fa_caret_down"), 14, ColorTranslator.FromHtml("#b2b2b2")),
            WinDoControls.FontImages.GetImage((WinDoControls.FontIcons)Enum.Parse(typeof(WinDoControls.FontIcons), "A_fa_caret_up"), 14, Color.Black),
            WinDoControls.FontImages.GetImage((WinDoControls.FontIcons)Enum.Parse(typeof(WinDoControls.FontIcons), "A_fa_caret_down"), 14, Color.Black)
        };

        /// <summary>
        /// 清除排序
        /// </summary>
        /// <param name="dgv"></param>
        public static void ClearSorts(this DataGridView dgv)
        {
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.HeaderCell.SortGlyphDirection = SortOrder.None;
            }
        }
        /// <summary>
        /// 获取当前排序列信息
        /// </summary>
        /// <param name="dgv"></param>
        /// <returns></returns>
        public static Tuple<string, System.Windows.Forms.SortOrder, DataGridViewColumn> GetSortedColumnInfo(this DataGridView dgv)
        {
            var sortCol = dgv.Columns.Cast<DataGridViewColumn>().FirstOrDefault(c =>
                  c.HeaderCell.SortGlyphDirection == System.Windows.Forms.SortOrder.Descending
                  || c.HeaderCell.SortGlyphDirection == System.Windows.Forms.SortOrder.Ascending
             );
            if (sortCol == null)
                return null;
            var sortDataProperty = sortCol.DataPropertyName;
            System.Windows.Forms.SortOrder sortOrder = sortCol.HeaderCell.SortGlyphDirection;
            return new Tuple<string, SortOrder, DataGridViewColumn>(sortDataProperty, sortOrder, sortCol);
        }
        /// <summary>
        /// 恢复排序
        /// </summary>
        /// <param name="list"></param>
        /// <param name="sortInfo"></param>
        /// <returns></returns>
        public static List<dynamic> GetSortedList(this List<dynamic> list, Tuple<string, System.Windows.Forms.SortOrder, DataGridViewColumn> sortInfo)
        {
            if (sortInfo == null)
                return list;
            if (sortInfo.Item2 == SortOrder.Ascending)
                list = list.OrderBy(l => ((IDictionary<string, object>)l)[sortInfo.Item1]).ToList();
            else if (sortInfo.Item2 == SortOrder.Descending)
                list = list.OrderByDescending(l => ((IDictionary<string, object>)l)[sortInfo.Item1]).ToList();
            return list;
        }

        /// <summary>
        /// 自定义排序
        /// </summary>
        /// <param name="dgv"></param>
        /// <returns></returns>
        public static DataGridViewColumn CustomSort(this DataGridViewColumn col, Action<DataGridView, DataGridViewCellMouseEventArgs, bool> sortAction = null)
        {
            var dgv = col.DataGridView;
            if (sortAction == null)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            else
                col.SortMode = DataGridViewColumnSortMode.Programmatic;
            dgv.CellPainting += (s, e) =>
            {
                if (e.RowIndex != -1) return;
                if (dgv.Columns[e.ColumnIndex] != col) return;

                e.PaintBackground(e.CellBounds, true);
                var g = e.Graphics;
                var sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                if (col.HeaderCell.Style.Alignment == DataGridViewContentAlignment.MiddleLeft)
                    sf.Alignment = StringAlignment.Near;
                var font = (col.HeaderCell.Style.Font == null ? dgv.ColumnHeadersDefaultCellStyle.Font : col.HeaderCell.Style.Font);
                using (var brush = new SolidBrush(dgv.DefaultCellStyle.ForeColor)) //dgv.DefaultCellStyle.ForeColor
                    g.DrawString(col.HeaderText, font, brush, e.CellBounds, sf);
                if (dgv.Columns[e.ColumnIndex].SortMode == DataGridViewColumnSortMode.Programmatic
                    || dgv.Columns[e.ColumnIndex].SortMode == DataGridViewColumnSortMode.Automatic)
                {
                    var txtWidth = TextRenderer.MeasureText(col.HeaderText, font).Width;
                    var x = e.CellBounds.X + (e.CellBounds.Width - txtWidth) / 2 + txtWidth;
                    if (col.HeaderCell.Style.Alignment == DataGridViewContentAlignment.MiddleLeft)
                        x = e.CellBounds.X + txtWidth;
                    var yOffset = 4;
                    if (col.HeaderCell.SortGlyphDirection == SortOrder.None)
                    {
                        g.DrawImage(SortImgs[0], x, e.CellBounds.Height / 2 - SortImgs[0].Height + yOffset);
                        g.DrawImage(SortImgs[1], x, e.CellBounds.Height / 2 - yOffset);
                    }
                    else if (col.HeaderCell.SortGlyphDirection == SortOrder.Ascending)
                    {
                        g.DrawImage(SortImgs[2], x, e.CellBounds.Height / 2 - SortImgs[0].Height + yOffset);
                        g.DrawImage(SortImgs[1], x, e.CellBounds.Height / 2 - yOffset);
                    }
                    else if (col.HeaderCell.SortGlyphDirection == SortOrder.Descending)
                    {
                        g.DrawImage(SortImgs[0], x, e.CellBounds.Height / 2 - SortImgs[0].Height + yOffset);
                        g.DrawImage(SortImgs[3], x, e.CellBounds.Height / 2 - yOffset);
                    }
                }
                e.Handled = true;
            };
            dgv.ColumnHeaderMouseClick += (s, e) =>
            {
                var curCol = dgv.Columns[e.ColumnIndex];
                if (curCol != col || curCol.SortMode != DataGridViewColumnSortMode.Programmatic || sortAction == null) return;
                foreach (DataGridViewColumn dcol in dgv.Columns)
                {
                    if (curCol == dcol) continue;
                    dcol.HeaderCell.SortGlyphDirection = SortOrder.None;
                }
                switch (curCol.HeaderCell.SortGlyphDirection)
                {
                    case System.Windows.Forms.SortOrder.None:
                    case System.Windows.Forms.SortOrder.Ascending:
                        sortAction(dgv, e, false);
                        curCol.HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.Descending;
                        break;
                    case SortOrder.Descending:
                        sortAction(dgv, e, true);
                        curCol.HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.Ascending;
                        break;
                }
            };
            return col;
        }
        #endregion

        #region "操作列"

        public static int Space_Width = 10;
        public static List<KeyValuePair<Rectangle, string>> GetBtnRects(DataGridView dgv, DataGridViewCell cell, Tuple<string, Size>[] Btns, int offsetx = 0)
        {
            var all_width = Btns.Sum(b => b.Item2.Width) + (Btns.Length - 1) * 2;
            var rect_height = Btns[0].Item2.Height;
            var x1 = (cell.Size.Width - all_width) / 2;
            if (offsetx > 0)
                x1 = offsetx;
            var y1 = (cell.Size.Height - rect_height) / 2;
            var rs = new List<KeyValuePair<Rectangle, string>>();
            var ox = 0;
            foreach (var btn in Btns)
            {
                var rect = new Rectangle(x1 + ox, y1, btn.Item2.Width, rect_height);
                rs.Add(new KeyValuePair<Rectangle, string>(rect, btn.Item1));
                ox += btn.Item2.Width + Space_Width;
            }
            return rs;
        }
        /// <summary>
        /// 带间隔的按钮组矩形
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="cell"></param>
        /// <param name="Btns"></param>
        /// <param name="space"></param>
        /// <returns></returns>
        public static List<KeyValuePair<Rectangle, string>> GetBtnsRects(DataGridView dgv, DataGridViewCell cell, Tuple<string, Size>[] Btns, int space = 6)
        {
            var all_width = Btns.Sum(b => b.Item2.Width) + (Btns.Length - 1) * space;
            var rect_height = Btns[0].Item2.Height;
            var x1 = (cell.Size.Width - all_width) / 2;
            var y1 = (cell.Size.Height - rect_height) / 2;
            var rs = new List<KeyValuePair<Rectangle, string>>();
            var ox = 0;
            foreach (var btn in Btns)
            {
                var rect = new Rectangle(x1 + ox, y1, btn.Item2.Width, rect_height);
                rs.Add(new KeyValuePair<Rectangle, string>(rect, btn.Item1));
                ox += btn.Item2.Width + space;
            }
            return rs;
        }
        public static DataGridViewColumn SetOperationButtons(this DataGridViewColumn column, Action<DataGridView, DataGridViewCellMouseEventArgs> cellClick,
            Action<DataGridView, DataGridViewCellPaintingEventArgs> cellPainting,
            Action<DataGridView, DataGridViewCellMouseEventArgs> cellMouseMove)
        {
            var dgv = column.DataGridView;
            dgv.CellMouseLeave += (s, e) => { dgv.Cursor = Cursors.Default; };
            dgv.CellMouseClick += (object sender, DataGridViewCellMouseEventArgs e) =>
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                if (dgv.Columns[e.ColumnIndex] != column) return;
                var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell != null)
                {
                    cellClick(dgv, e);
                    //var all_width = Btns.Sum(b => b.Item2.Width) + space_width * 2;
                    //var rect_height = Btns[0].Item2.Height;
                    //var x1 = (cell.Size.Width - all_width) / 2;
                    //var y1 = (cell.Size.Height - rect_height) / 2;
                    //var rect1 = new Rectangle(x1, y1, Btns[0].Item2.Width, rect_height);
                    //var rect2 = new Rectangle(x1 + Btns[1].Item2.Width + space_width, y1, Btns[1].Item2.Width, rect_height);
                    //var rect3 = new Rectangle(x1 + Btns[0].Item2.Width + Btns[1].Item2.Width + +space_width * 2, y1, Btns[2].Item2.Width, rect_height);
                    //var dr = dgv.Rows[e.RowIndex].DataBoundItem;
                    //var hasBtn1 = (PublicRes.HasPrivilege(PrivilegesEnum.非加预约超级权限) || PublicRes.HasPrivilege(PrivilegesEnum.非加速器修改));
                    //if (hasBtn1 && rect1.Contains(e.Location))
                    //{
                    //    Operation(Btns[0].Item1, dr);
                    //}
                    //else if (rect2.Contains(e.Location))
                    //{
                    //    Operation(Btns[1].Item1, dr);
                    //}
                    //else if (rect3.Contains(e.Location))
                    //{
                    //    Operation(Btns[2].Item1, dr);
                    //}
                }
            };

            dgv.CellPainting += (object sender, DataGridViewCellPaintingEventArgs e) =>
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                if (dgv.Columns[e.ColumnIndex] != column) return;
                e.PaintBackground(e.CellBounds, true);
                e.Graphics.SetGDIHigh();
                cellPainting(dgv, e);
                e.Graphics.SmoothingMode = SmoothingMode.None;
                //var all_width = Btns.Sum(b => b.Item2.Width) + space_width * 2;
                //var rect_height = Btns[0].Item2.Height;
                //var x = e.CellBounds.X + (e.CellBounds.Width - all_width) / 2;
                //var y = e.CellBounds.Y + (e.CellBounds.Height - rect_height) / 2;
                //var container = new Rectangle(x, y, Btns[0].Item2.Width, rect_height);
                //if (PublicRes.HasPrivilege(PrivilegesEnum.非加预约超级权限) || PublicRes.HasPrivilege(PrivilegesEnum.非加速器修改))
                //    FormHelper.DrawRectBtn(e.Graphics, container, Btns[0].Item2.Width, rect_height, Color.White, YkdBasisColors.GrayRectColor, Btns[0].Item3);
                //container.Offset(Btns[0].Item2.Width + space_width, 0);
                //FormHelper.DrawRectBtn(e.Graphics, container, Btns[1].Item2.Width, rect_height, Color.White, YkdBasisColors.GrayRectColor, Btns[1].Item3);
                //container.Offset(Btns[1].Item2.Width + space_width, 0);
                //FormHelper.DrawRectBtn(e.Graphics, container, Btns[2].Item2.Width, rect_height, Color.White, YkdBasisColors.OrangeColorDelta, Btns[2].Item3);
                e.Handled = true;
            };

            dgv.CellMouseMove += (object sender, DataGridViewCellMouseEventArgs e) =>
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                if (dgv.Columns[e.ColumnIndex] != column) return;
                var cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell != null)
                {
                    cellMouseMove(dgv, e);
                    //var all_width = Btns.Sum(b => b.Item2.Width) + space_width * 2;
                    //var rect_height = Btns[0].Item2.Height;
                    //var x1 = (cell.Size.Width - all_width) / 2;
                    //var y1 = (cell.Size.Height - rect_height) / 2;
                    //var rect1 = new Rectangle(x1, y1, Btns[0].Item2.Width, rect_height);
                    //var rect2 = new Rectangle(x1 + Btns[1].Item2.Width + space_width, y1, Btns[1].Item2.Width, rect_height);
                    //var rect3 = new Rectangle(x1 + Btns[0].Item2.Width + Btns[1].Item2.Width + +space_width * 2, y1, Btns[2].Item2.Width, rect_height);
                    //var dr = dgv.Rows[e.RowIndex].DataBoundItem;
                    //var hasBtn1 = (PublicRes.HasPrivilege(PrivilegesEnum.非加预约超级权限) || PublicRes.HasPrivilege(PrivilegesEnum.非加速器修改));
                    //if ((hasBtn1 && rect1.Contains(e.Location)) || rect2.Contains(e.Location) || rect3.Contains(e.Location))
                    //{
                    //    this.Cursor = Cursors.Hand;
                    //}
                    //else
                    //    this.Cursor = Cursors.Default;
                    //if (hasBtn1 && rect1.Contains(e.Location))
                    //{
                    //    ShowBtnTips(e, dgv, rect1, Btns[0].Item1);
                    //}
                    //else if (rect2.Contains(e.Location))
                    //{
                    //    ShowBtnTips(e, dgv, rect2, Btns[1].Item1);
                    //}
                    //else if (rect3.Contains(e.Location))
                    //{
                    //    ShowBtnTips(e, dgv, rect3, Btns[2].Item1);
                    //}
                }
            };

            return column;
        }






        #endregion

        #region "表格单元格弹出控件"
        /*
        /// <summary>
        /// 弹出时间选择框
        /// </summary>
        /// <param name="e"></param>
        private void ShowTimeDropdown(DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex != 3)
            {
                return;
            }
            var cell = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            var rect = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            var dr = this.dataGridView1.Rows[e.RowIndex].DataBoundItem as TestClass;
            if (_frmAnchor != null)
            {
                var curCell = _frmAnchor.Tag == cell;
                var v = _frmAnchor.Visible;
                _frmAnchor.Close();
                _frmAnchor = null;
                if (curCell || !v)
                    return;
            }
            var frm = new Utilities.DialogForm.frmSelectTime1(0, 0);
            ControlHelper.SetControlsDouble(frm);
            frm.TopLevel = false;
            rect.Offset(0, rect.Height);
            _frmAnchor = new WD_Controls.Forms.FrmAnchor(dataGridView1, frm, rect.Location, isNotFocus: false) { CalcHeightByParent = false, AllowMouseOnParent = false };
            frm.FormClosed += (ss, ee) =>
            {
                //设置时间
                if (frm.Time.Length > 0)
                {
                    //var time = frm.Time.Split(':');
                    dr.DDD = frm.Time;
                }
                if (_frmAnchor == null) return;
                _frmAnchor.Close();
                _frmAnchor = null;
            };
            frm.Show();
            _frmAnchor.MinimumSize = new Size(1, 1);
            _frmAnchor.Size = frm.Size;
            _frmAnchor.Tag = cell;
            _frmAnchor.Show(this.FindForm());
        }

        /// <summary>
        /// 弹出日期选择框
        /// </summary>
        /// <param name="e"></param>
        private void ShowDateDropdown(DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex != 2)
            {
                return;
            }
            var cell = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            var rect = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            var dr = this.dataGridView1.Rows[e.RowIndex].DataBoundItem as TestClass;
            if (_frmAnchor != null)
            {
                var curCell = _frmAnchor.Tag == cell;
                var v = _frmAnchor.Visible;
                _frmAnchor.Close();
                _frmAnchor = null;
                if (curCell || !v)
                    return;
            }

            var datePicker = WD_Controls.Controls.DatePickerExt.CreateDefaultDatePickExt();
            datePicker.Value = DateTime.Now.Date;
            datePicker.BottomBarConfirmClick += (ss, ee) =>
            {
                //获取选择的值
                var date = datePicker.Value.Date;
                dr.CCC = date.ToShortDateString();

                if (_frmAnchor == null) return;
                _frmAnchor.Close();
                _frmAnchor = null;
            };
            datePicker.Show();
            rect.Offset(0, rect.Height);
            _frmAnchor = new FrmAnchor(this.dataGridView1, datePicker, rect.Location, isNotFocus: false) { CalcHeightByParent = false, AllowMouseOnParent = false };
            _frmAnchor.MinimumSize = new Size(1, 1);
            _frmAnchor.Size = datePicker.Size;
            _frmAnchor.Tag = cell;
            _frmAnchor.Show(this.FindForm());
        }

        /// <summary>
        /// 弹出下拉选择框
        /// </summary>
        /// <param name="e"></param>
        private void ShowCombDropdown(DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex != 1)
            {
                return;
            }
            var cell = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            var rect = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            var dr = this.dataGridView1.Rows[e.RowIndex].DataBoundItem as TestClass;
            if (_frmAnchor != null)
            {
                var curCell = _frmAnchor.Tag == cell;
                var v = _frmAnchor.Visible;
                _frmAnchor.Close();
                _frmAnchor = null;
                if (curCell || !v)
                    return;
            }
            var pnl = new UCCombPanel();
            pnl.DGV.Font = YkdTextFonts.TextFont;
            pnl.Size = new Size(cell.Size.Width, 200);
            pnl.DGV.RowTemplate.Height = 30;
            pnl.SelectSourceEvent += (s, ee) =>
            {
                dr.BBB = s.ToString();
                if (_frmAnchor == null)
                    return;
                _frmAnchor.Close();
                _frmAnchor = null;
            };
            var ls = Enumerable.Range(0, 15).Select(i => new KeyValuePair<string, string>("Key" + i, "Value" + i)).ToList();
            pnl.DGV.DataSource = ls;
            rect.Offset(0, rect.Height);
            _frmAnchor = new FrmAnchor(this.dataGridView1, pnl, rect.Location, isNotFocus: false) { CalcHeightByParent = false, AllowMouseOnParent = false };
            _frmAnchor.MinimumSize = new Size(1, 1);
            _frmAnchor.Size = pnl.Size;
            _frmAnchor.Tag = cell;
            _frmAnchor.Show(this.FindForm());
        }
        */


        #endregion

        public static bool RectangleContains(Size container, Size rect, Point location)
        {
            var r = new Rectangle((container.Width - rect.Width) / 2, (container.Height - rect.Height) / 2, rect.Width, rect.Height);
            return (r.Contains(location));
        }



        /// <summary>
        /// 计算两条直线的交点
        /// </summary>
        /// <param name="lineFirstStar">L1的点1坐标</param>
        /// <param name="lineFirstEnd">L1的点2坐标</param>
        /// <param name="lineSecondStar">L2的点1坐标</param>
        /// <param name="lineSecondEnd">L2的点2坐标</param>
        /// <returns></returns>
        public static PointF GetIntersection(PointF lineFirstStar, PointF lineFirstEnd, PointF lineSecondStar, PointF lineSecondEnd)
        {
            /*
             * L1，L2都存在斜率的情况：
             * 直线方程L1: ( y - y1 ) / ( y2 - y1 ) = ( x - x1 ) / ( x2 - x1 ) 
             * => y = [ ( y2 - y1 ) / ( x2 - x1 ) ]( x - x1 ) + y1
             * 令 a = ( y2 - y1 ) / ( x2 - x1 )
             * 有 y = a * x - a * x1 + y1   .........1
             * 直线方程L2: ( y - y3 ) / ( y4 - y3 ) = ( x - x3 ) / ( x4 - x3 )
             * 令 b = ( y4 - y3 ) / ( x4 - x3 )
             * 有 y = b * x - b * x3 + y3 ..........2
             * 
             * 如果 a = b，则两直线平等，否则， 联解方程 1,2，得:
             * x = ( a * x1 - b * x3 - y1 + y3 ) / ( a - b )
             * y = a * x - a * x1 + y1
             * 
             * L1存在斜率, L2平行Y轴的情况：
             * x = x3
             * y = a * x3 - a * x1 + y1
             * 
             * L1 平行Y轴，L2存在斜率的情况：
             * x = x1
             * y = b * x - b * x3 + y3
             * 
             * L1与L2都平行Y轴的情况：
             * 如果 x1 = x3，那么L1与L2重合，否则平等
             * 
            */
            float a = 0, b = 0;
            int state = 0;
            if (lineFirstStar.X != lineFirstEnd.X)
            {
                a = (lineFirstEnd.Y - lineFirstStar.Y) / (lineFirstEnd.X - lineFirstStar.X);
                state |= 1;
            }
            if (lineSecondStar.X != lineSecondEnd.X)
            {
                b = (lineSecondEnd.Y - lineSecondStar.Y) / (lineSecondEnd.X - lineSecondStar.X);
                state |= 2;
            }
            switch (state)
            {
                case 0: //L1与L2都平行Y轴
                    {
                        if (lineFirstStar.X == lineSecondStar.X)
                        {
                            //throw new Exception("两条直线互相重合，且平行于Y轴，无法计算交点。");
                            return new PointF(0, 0);
                        }
                        else
                        {
                            //throw new Exception("两条直线互相平行，且平行于Y轴，无法计算交点。");
                            return new PointF(0, 0);
                        }
                    }
                case 1: //L1存在斜率, L2平行Y轴
                    {
                        float x = lineSecondStar.X;
                        float y = (lineFirstStar.X - x) * (-a) + lineFirstStar.Y;
                        return new PointF(x, y);
                    }
                case 2: //L1 平行Y轴，L2存在斜率
                    {
                        float x = lineFirstStar.X;
                        //网上有相似代码的，这一处是错误的。你可以对比case 1 的逻辑 进行分析
                        //源code:lineSecondStar * x + lineSecondStar * lineSecondStar.X + p3.Y;
                        float y = (lineSecondStar.X - x) * (-b) + lineSecondStar.Y;
                        return new PointF(x, y);
                    }
                case 3: //L1，L2都存在斜率
                    {
                        if (a == b)
                        {
                            // throw new Exception("两条直线平行或重合，无法计算交点。");
                            return new PointF(0, 0);
                        }
                        float x = (a * lineFirstStar.X - b * lineSecondStar.X - lineFirstStar.Y + lineSecondStar.Y) / (a - b);
                        float y = a * x - a * lineFirstStar.X + lineFirstStar.Y;
                        return new PointF(x, y);
                    }
            }
            // throw new Exception("不可能发生的情况");
            return new PointF(0, 0);
        }

        /// <summary>
        /// 列对齐方式设置
        /// </summary>
        /// <param name="col"></param>
        /// <param name="alignment"></param>
        /// <returns></returns>
        public static DataGridViewTextBoxColumn ColumnAligment(this DataGridViewTextBoxColumn col, DataGridViewContentAlignment alignment = DataGridViewContentAlignment.MiddleLeft)
        {
            col.DefaultCellStyle.Alignment = alignment;
            col.HeaderCell.Style.Alignment = alignment;
            return col;
        }

        /// <summary>
        /// 固定列宽
        /// </summary>
        /// <param name="col"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static DataGridViewTextBoxColumn FixColumnWidth(this DataGridViewTextBoxColumn col, int width)
        {
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            col.Width = width;
            return col;
        }
        public static DataGridViewColumn FixColumnWidth(this DataGridViewColumn col, int width)
        {
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            col.Width = width;
            return col;
        }

        /// <summary>
        /// 新增列
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="headText"></param>
        /// <param name="dataField"></param>
        /// <returns></returns>
        public static DataGridViewTextBoxColumn AddColumn(this DataGridView dgv, string headText, string dataField)
        {
            var col = new DataGridViewTextBoxColumn();
            col.Name = "col_" + dataField;
            col.DataPropertyName = dataField;
            col.HeaderText = headText;
            col.SortMode = DataGridViewColumnSortMode.NotSortable;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns.Add(col);
            return col;
        }

        /// <summary>
        /// 设置水平表格线
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="gridColor"></param>
        public static void SetWhiteSingleHorizontal(DataGridView dgv, Color gridColor)
        {
            //显示白色水平网格线
            dgv.GridColor = gridColor;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.AdvancedCellBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.Single;
        }

        /// <summary>
        /// 默认表格式样
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="alternating"></param>
        /// <param name="useHoverBackColor"></param>
        public static void SetDefaultStyle(DataGridView dgv, bool alternating = false, bool useHoverBackColor = true, bool ShowEmpty = true)
        {
            ControlHelper.SetDouble(dgv);
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;

            //只显示垂直滚动条
            dgv.ScrollBars = ScrollBars.Vertical;
            //列头背景色，字体加粗
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = WDColors.GrayBackColorF7;
            dgv.ColumnHeadersDefaultCellStyle.Font = WinDo.Utilities.PublicResource.WDFonts.TextFontBold;
            if (alternating)
                dgv.AlternatingRowsDefaultCellStyle.BackColor = WDColors.GrayBackColorF7;

            dgv.DefaultCellStyle.Font = WinDo.Utilities.PublicResource.WDFonts.TextFont;
            dgv.DefaultCellStyle.ForeColor = Color.Black;
            //dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgv.RowsDefaultCellStyle.SelectionBackColor = WDColors.geekblue3;
            dgv.RowsDefaultCellStyle.SelectionForeColor = Color.White;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 40;

            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgv.RowTemplate.Height = 40;

            dgv.ShowCellToolTips = false;
            dgv.ReadOnly = true;
            dgv.AllowUserToOrderColumns = false;
            dgv.AllowUserToAddRows = false;
            dgv.MultiSelect = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;
            dgv.RowHeadersVisible = false;
            dgv.AutoGenerateColumns = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //网格线
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgv.AdvancedCellBorderStyle.All = DataGridViewAdvancedCellBorderStyle.None;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.AdvancedColumnHeadersBorderStyle.All = DataGridViewAdvancedCellBorderStyle.None;

            dgv.RowPrePaint += (s, e) => { e.PaintParts = e.PaintParts ^ DataGridViewPaintParts.Focus; };
            dgv.DataBindingComplete += (s, e) => { var d = s as DataGridView; d.ClearSelection(); };
            if (useHoverBackColor)
            {
                dgv.CellMouseEnter += (s, e) =>
                {
                    if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = WinDo.Utilities.PublicResource.WDColors.SelectedBackColor;
                };
                dgv.CellMouseLeave += (s, e) =>
                {
                    if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                    if (alternating)
                        dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = e.RowIndex % 2 == 0 ? dgv.RowsDefaultCellStyle.BackColor : WDColors.GrayBackColorF7;
                    else
                        dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = dgv.RowsDefaultCellStyle.BackColor;
                };
            }
            dgv.CellMouseLeave += (s, e) =>
            {
                dgv.Cursor = Cursors.Default;
            };
            if (ShowEmpty)
                SetEmptyText(dgv);
        }

        /// <summary>
        /// 显示无数据
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="ShowInNull"></param>
        public static void SetEmptyText(DataGridView dgv, bool ShowInNull = false, bool ShowIcon = false)
        {
            dgv.Paint += (s, e) =>
            {
                //var dgv = sender as DataGridView;
                if (dgv.DataSource == null && !ShowInNull) return;
                if (dgv.Rows.Count == 0) // <-- if there are no rows in the DataGridView when it paints, then it will create your message
                {
                    try
                    {
                        var g = e.Graphics;
                        if (ShowIcon)
                        {
                            var rectDgv = dgv.ClientRectangle;
                            rectDgv.Offset(0, 20);
                            var sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                            var imgp = new Point((rectDgv.Width - WDImages.EmptyImg.Width) / 2, (rectDgv.Height - WDImages.EmptyImg.Height) / 2);
                            imgp.Offset(-20, 20);
                            g.DrawImage(WDImages.EmptyImg, imgp);
                            rectDgv.Offset(30, 0);
                            using (var brush = new SolidBrush(WDColors.GrayTextColor9))
                                g.DrawString("暂无数据", WDFonts.TextFont, brush, rectDgv, sf);
                            return;
                        }
                        var rect = new Rectangle(new Point(0, dgv.ColumnHeadersHeight), new Size(dgv.Width, dgv.ColumnHeadersHeight));
                        //create a white rectangle so text will be easily readable
                        g.FillRectangle(Brushes.White, rect);
                        //write text on top of the white rectangle just created
                        //StringFormat sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                        var x = dgv.Width / 2 - 25; //(rect.Width - e.Graphics.MeasureString("无数据", YkdTextFonts.TextFont).Width) / 2;
                        g.DrawString("无数据", WDFonts.TextFont, Brushes.Black, new Point((int)x, dgv.ColumnHeadersHeight));
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                }
            };
        }


    }


    public class DataGridViewColumnEntity
    {




        public string HeadText { get; set; }
        public string TipText { get; set; }




        public int Width { get; set; }
        public int lblWidth { get; set; }
        public System.Windows.Forms.SizeType WidthType { get; set; }
        public string DataField { get; set; }
        public Func<object, string> Format { get; set; }
        private ContentAlignment _TextAlign = ContentAlignment.MiddleCenter;
        public ContentAlignment TextAlign { get { return _TextAlign; } set { _TextAlign = value; } }
        private bool _allowSort = false;
        public bool AllowSort { get { return _allowSort; } set { _allowSort = value; } }

        public Image HeadImage { get; set; }
        public bool IsImageColumn { get; set; }
        public bool IsShowHeadImage { get; set; }

        public string ControlType { get; set; }
    }


    public class DataGridViewEventArgs : EventArgs
    {




        public Control CellControl { get; set; }




        public int CellIndex { get; set; }




        public int RowIndex { get; set; }

    }

    #region 树形控件

    public class DgvTreeRow
    {
        public int RowType { get; set; }
        public string ParentID { get; set; }
        public string ID { get; set; }
        public string Text { get; set; }
        public object Data { get; set; }
    }
    public static class UC_DGV
    {

        static FrmAnchorTips _frmAnchorFullTips = null;
        public static void CloseFullTips()
        {
            if (_frmAnchorFullTips != null)
            {
                _frmAnchorFullTips.Close();
                _frmAnchorFullTips = null;
            }
        }
        public static void ShowFullTips(Rectangle ClientRectangle, DataGridView dgv, DataGridViewCellMouseEventArgs e, string tips)
        {
            if (string.IsNullOrWhiteSpace(tips))
                return;
            var cellRect = ((Rectangle)(ClientRectangle));
            var cRect = dgv.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
            cellRect.Offset(cRect.Location);
            var dgvPoint1 = dgv.Parent.PointToScreen(dgv.Location);
            cellRect.Offset(dgvPoint1);
            if (_frmAnchorFullTips != null && _frmAnchorFullTips.RectControl != cellRect)
            {
                CloseFullTips();
            }
            if (_frmAnchorFullTips == null)
                _frmAnchorFullTips = FrmAnchorTips.ShowTips(cellRect, tips, AnchorTipsLocation.BOTTOM, WDColors.TaskListTip, autoCloseTime: 6000);
        }

        /// <summary>
        /// 树形式样
        /// </summary>
        /// <param name="Operate"></param>
        public static void SetTreeStyle(DataGridView dgv, Action Expand, Action<string, object> Operate)
        {
            //自定义DataGridView实例
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            DataGridViewHelper.SetDefaultStyle(dgv);
            dgv.ColumnHeadersVisible = false;
            dgv.RowsDefaultCellStyle.SelectionBackColor = WDColors.TreeFocusRowL;
            dgv.RowsDefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.RowTemplate.Height = 40;
            dgv.DataSourceChanged += (s, e) =>
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    var dr = row.DataBoundItem as DgvTreeRow;
                    dynamic v = new System.Dynamic.ExpandoObject();
                    v.RowType = dr.RowType;
                    v.ParentID = dr.ParentID;
                    v.ID = dr.ID;
                    v.Text = dr.Text;
                    v.IsExpand = true;

                    var Btns = new[] {
                        new { Type="-",ClientRectangle=new Rectangle(35,10,20,20) },
                        new { Type="E",ClientRectangle=new Rectangle(65,10,20,20) },
                    };
                    if (dr.RowType == 0)
                    {
                        Btns = new[] {
                            new { Type="+",ClientRectangle=new Rectangle(5,10,20,20) },
                            new { Type="-",ClientRectangle=new Rectangle(35,10,20,20) },
                            new { Type="E",ClientRectangle=new Rectangle(65,10,20,20) },
                        };
                    }
                    v.Btns = Btns;
                    dr.Data = v;
                }
            };

            dgv.CellFormatting += (s, e) =>
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0)
                    return;

                dynamic dr = dgv.Rows[e.RowIndex].DataBoundItem;
                dynamic cv = dr.Data;
                if (cv == null)
                    return;
                if (e.ColumnIndex == 1)
                {
                    e.Value = dr.Text;
                }
            };

            var col = dgv.AddColumn("", "箭头列").FixColumnWidth(28);
            col.SetOperationButtons(
                (dgv1, e) =>
                {
                    dynamic dr = dgv.Rows[e.RowIndex].DataBoundItem;
                    dynamic cv = dr.Data;
                    if (cv == null)
                        return;
                    if (cv.RowType > 0)
                        return;
                    //展开或者收起行
                    bool IsExpand = !(cv.IsExpand);
                    cv.IsExpand = IsExpand;
                    var subRows = dgv1.Rows.Cast<DataGridViewRow>().Where(r =>
                    {
                        dynamic rcv = ((dynamic)(r.DataBoundItem)).Data;
                        return rcv.RowType > 0 && rcv.ParentID == cv.ID;
                    }).ToList();
                    subRows.ForEach(r => r.Visible = IsExpand);
                    Expand?.Invoke();
                },
                (dgv1, e) =>
                {
                    dynamic dr = dgv.Rows[e.RowIndex].DataBoundItem;
                    dynamic cv = dr.Data;
                    if (cv == null)
                        return;
                    if (cv.RowType > 0)
                        return;
                    bool IsExpand = (cv.IsExpand);
                    var img = IsExpand ? WDImages.ArrowDownWhiteImg : WDImages.ArrowRightWhiteImg;
                    ControlHelper.DrawRectBtn(e.Graphics, e.CellBounds, e.CellBounds.Width, e.CellBounds.Height - 12,
                        ColorTranslator.FromHtml("#B7E1EB"), ColorTranslator.FromHtml("#B7E1EB"),
                        img);
                },
                (dgv1, e) =>
                {
                    dynamic dr = dgv.Rows[e.RowIndex].DataBoundItem;
                    dynamic cv = dr.Data;
                    if (cv == null)
                        return;
                    if (cv.RowType > 0)
                    {
                        dgv.Cursor = Cursors.Default;
                        return;
                    }
                    dgv.Cursor = Cursors.Hand;
                });
            col = dgv.AddColumn("", "文本列").ColumnAligment(DataGridViewContentAlignment.MiddleLeft);
            col.SetColumnCustomDraw((s, e) =>
            {
                e.PaintBackground(e.CellBounds, true);
                dynamic dr = dgv.Rows[e.RowIndex].DataBoundItem;
                dynamic cv = dr.Data;
                if (cv == null)
                    return;
                e.Graphics.SetGDIHigh();
                if (cv.RowType == 0)
                {
                    using (var brush = new SolidBrush(WDColors.TreeFocusRowL))
                        e.Graphics.FillRectangle(brush, new Rectangle(e.CellBounds.X - 1, e.CellBounds.Y + 6, e.CellBounds.Width + 2, 28));
                }
                e.PaintContent(e.CellBounds);
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

            });
            col = dgv.AddColumn("", "操作列").FixColumnWidth(90);
            col.SetOperationButtons(
                (dgv1, e) =>
                {
                    dynamic dr = dgv.Rows[e.RowIndex].DataBoundItem;
                    dynamic cv = dr.Data;
                    if (cv == null)
                        return;
                    //按钮点击
                    var btn = (cv.Btns as IEnumerable<dynamic>).FirstOrDefault(b => ((Rectangle)(b.ClientRectangle)).Contains(e.Location));
                    if (btn == null)
                        return;
                    string btnType = btn.Type;
                    Operate(btnType, dr);
                },
                (dgv1, e) =>
                {
                    dynamic dr = dgv.Rows[e.RowIndex].DataBoundItem;
                    dynamic cv = dr.Data;
                    if (cv == null)
                        return;
                    if (cv.RowType == 0)
                    {
                        using (var brush = new SolidBrush(WDColors.TreeFocusRowL))
                            e.Graphics.FillRectangle(brush, new Rectangle(e.CellBounds.X - 1, e.CellBounds.Y + 6, e.CellBounds.Width + 2, 28));
                    }
                    var btns = (cv.Btns as IEnumerable<dynamic>);
                    foreach (dynamic btn in btns)
                    {
                        Image img = null;
                        if (btn.Type == "+")
                            img = WDImages.PlusImg;
                        else if (btn.Type == "-")
                            img = WDImages.MinusImg;
                        else if (btn.Type == "E")
                            img = WDImages.PenImg;
                        if (img == null)
                            continue;
                        Rectangle rect = btn.ClientRectangle;
                        rect.Offset(e.CellBounds.Location);
                        var rectColor = WDColors.GrayRectColor;
                        if (btn.Type == "-")
                            rectColor = Color.Red;
                        ControlHelper.DrawRectBtn(e.Graphics, rect, rect.Width, rect.Height,
                            Color.White, rectColor,
                            img);
                    }
                },
                (dgv1, e) =>
                {
                    dynamic dr = dgv.Rows[e.RowIndex].DataBoundItem;
                    dynamic cv = dr.Data;
                    if (cv == null)
                        return;
                    var btn = (cv.Btns as IEnumerable<dynamic>).FirstOrDefault(b => ((Rectangle)(b.ClientRectangle)).Contains(e.Location));
                    if (btn == null)
                    {
                        dgv.Cursor = Cursors.Default;
                        return;
                    }
                    dgv.Cursor = Cursors.Hand;
                });
        }

    }

    #endregion
}

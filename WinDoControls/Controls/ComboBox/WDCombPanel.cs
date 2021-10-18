using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinDo;

namespace WinDoControls.Controls
{
    public partial class WDCombPanel : UserControl
    {
        public WDCombPanel()
        {
            InitializeComponent();
            SetDGVStyle();
            var col = DataGridViewHelper.AddColumn(DGV, "", "Value");
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            col.DefaultCellStyle.Padding = new Padding(2, 0, 0, 0);
            DataGridViewHelper.ShowTipsOnOverLength(col);
            //DGV.Dock = DockStyle.Fill;
            DGV.ScrollBars = ScrollBars.Vertical;
            DGV.ColumnHeadersVisible = false;
            DGV.AllowUserToAddRows = false;
            DGV.CellMouseClick += DGV_CellMouseClick;
        }

        protected virtual void SetDGVStyle()
        {
            DataGridViewHelper.SetDefaultStyle(DGV);
        }

        public event EventHandler SelectSourceEvent;
        public void SetSelect(string key)
        {
            if (DGV.DataSource == null) return;
            foreach (DataGridViewRow row in DGV.Rows)
            {
                var dr = (KeyValuePair<string, string>)row.DataBoundItem;
                if (dr.Key == key)
                    row.Selected = true;
            }
        }

        public KeyValuePair<string, string>? GetSelected()
        {
            if (DGV.DataSource == null || DGV.SelectedRows.Count == 0) return null;
            return (KeyValuePair<string, string>)DGV.SelectedRows[0].DataBoundItem;
        }

        public bool SelectRowMode = false;

        private void DGV_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (SelectRowMode)
            {
                if (DGV.DataSource == null || DGV.SelectedRows.Count == 0)
                    return;

                if (SelectSourceEvent != null)
                    SelectSourceEvent(DGV.SelectedRows[0].DataBoundItem, e);
                return;
            }
            var sr = GetSelected();
            OnClick(sr, e);
        }

        protected void OnClick(KeyValuePair<string, string>? sr, DataGridViewCellMouseEventArgs e)
        {
            if (SelectSourceEvent != null)
                SelectSourceEvent(sr.HasValue ? sr.Value.Key : "", e);
        }
    }


    public class UCCombPanel2 : WDCombPanel
    {

        public bool HoverAutoClick = true;
        public void SetHovePopuSub(bool hoverAutoClick)
        {
            HoverAutoClick = hoverAutoClick;
            DGV.CellMouseMove += DGV_CellMouseMove;
        }
        private void DGV_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var fr = DGV.Rows[e.RowIndex];
            if (fr.Selected)
                return;
            fr.Selected = true;
            if (!HoverAutoClick)
                return;
            var sr = GetSelected();
            OnClick(sr, e);
        }

        public Func<object, bool> HasSubData;
        private void DGV_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;
            //using(var brush = new SolidBrush(e.CellBounds.bac))
            //ControlHelper.FillRoundRectangle(e.Graphics,)
            //e.Handled = true;
            e.PaintBackground(e.ClipBounds, true);
            e.PaintContent(e.ClipBounds);
            if (HasSubData != null && HasSubData(DGV.Rows[e.RowIndex].DataBoundItem))
            {
                var arrow = DGV.Rows[e.RowIndex].Selected ? WDImages.ArrowRightWhite : WDImages.ArrowRightBlack;
                e.Graphics.DrawImage(arrow, e.CellBounds.Right - 20, e.CellBounds.Y + 4);
            }
            e.Handled = true;
        }
        protected override void SetDGVStyle()
        {
            DataGridViewHelper.SetDefaultStyle(DGV, false, false);
            DGV.CellMouseEnter += (s, e) =>
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                DGV.Rows[e.RowIndex].DefaultCellStyle.BackColor = WinDo.Utilities.PublicResource.WDColors.geekblue6;
            };
            DGV.CellMouseLeave += (s, e) =>
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                DGV.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
            };
            DGV.CellPainting += DGV_CellPainting;
        }
    }
}

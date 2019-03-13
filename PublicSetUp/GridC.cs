using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using MDIDemo.PublicClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MDIDemo.PublicSetUp
{
    /// <summary>
    /// 设置GridControl
    /// </summary>
    public class GridC
    {
        public GridC()
        {
            this.pri_IniInfo();
        }

        private void pri_IniInfo()
        {
        }

        private void pri_SetBandedGridViewStyle(BandedGridView bgv)
        {
            Class_PublicMethod class_PublicMethod = new Class_PublicMethod();
            Font font = new Font("Tahoma", class_PublicMethod.GetGridFontSize());
            bgv.OptionsView.ShowColumnHeaders = true;
            bgv.OptionsView.ColumnAutoWidth = false;
            bgv.OptionsView.ShowAutoFilterRow = true;
            //bgv.Appearance.OddRow.BackColor = Color.Beige;
            bgv.OptionsView.EnableAppearanceOddRow = true;
            bgv.OptionsCustomization.AllowGroup = false;
            bgv.OptionsView.ShowFooter = false;
            //bgv.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 11F);
            bgv.Appearance.Row.Font = font;
        }

        private void pri_SetBandGridColumnStyle(BandedGridView bgv1, BandedGridColumn bgc, DataRow p_datarow)
        {
            bgc.Visible = true;
            bgc.RowCount = 1;
            bgc.OptionsFilter.AllowFilter = false;
            bgc.Visible = true;
            bgc.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            bgc.OptionsColumn.ReadOnly = true;
            if (p_datarow["IsGroup"].ToString().ToLower() == "true")
            {
                this.pri_setSumColumn(bgc);
            }
            this.pri_SetOrderNo(bgv1, bgc, p_datarow["OrderType"].ToString());
        }

        private void pri_SetFieldType(GridColumn p_GridColumn, string p_str_type)
        {
            switch (p_str_type.ToLower())
            {
                case "integer":
                    p_GridColumn.DisplayFormat.FormatType = FormatType.Numeric;
                    p_GridColumn.SummaryItem.DisplayFormat = "小计：{0:c} ";
                    p_GridColumn.SummaryItem.SummaryType = SummaryItemType.Sum;
                    break;

                case "smallint":
                    p_GridColumn.DisplayFormat.FormatType = FormatType.Numeric;
                    p_GridColumn.SummaryItem.DisplayFormat = "小计：{0:c} ";
                    p_GridColumn.SummaryItem.SummaryType = SummaryItemType.Sum;
                    break;

                case "float":
                    p_GridColumn.DisplayFormat.FormatType = FormatType.Numeric;
                    p_GridColumn.SummaryItem.DisplayFormat = "小计：{0:c} ";
                    p_GridColumn.SummaryItem.SummaryType = SummaryItemType.Sum;
                    break;

                case "decimal":
                    p_GridColumn.DisplayFormat.FormatType = FormatType.Numeric;
                    p_GridColumn.SummaryItem.DisplayFormat = "小计：{0:c} ";
                    p_GridColumn.SummaryItem.SummaryType = SummaryItemType.Sum;
                    break;

                case "money":
                    p_GridColumn.DisplayFormat.FormatType = FormatType.Numeric;
                    p_GridColumn.SummaryItem.DisplayFormat = "小计：{0:c} ";
                    p_GridColumn.SummaryItem.SummaryType = SummaryItemType.Sum;
                    break;

                case "datetime":
                    p_GridColumn.DisplayFormat.FormatType = FormatType.DateTime;
                    break;

                default:
                    p_GridColumn.DisplayFormat.FormatType = FormatType.None;
                    break;
            }
        }

        private void pri_SetGridBandStyle(GridBand gb)
        {
            if (gb != null)
            {
                gb.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            }
        }

        public void SetGridBar(GridControl gridControl)
        {
            pri_SetGridBar(gridControl);
        }
        private void pri_SetGridBar(GridControl p_GridControl)
        {
            p_GridControl.UseEmbeddedNavigator = true;
            p_GridControl.EmbeddedNavigator.TextStringFormat = "第{0}条,共{1}条";
            p_GridControl.EmbeddedNavigator.Buttons.Edit.Visible = false;
            if (p_GridControl.EmbeddedNavigator.Buttons.Edit.Visible)
            {
                p_GridControl.EmbeddedNavigator.Buttons.Edit.Hint = "开始修改";
            }
            p_GridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            if (p_GridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible)
            {
                p_GridControl.EmbeddedNavigator.Buttons.CancelEdit.Hint = "撤消修改";
            }
            p_GridControl.EmbeddedNavigator.Buttons.Append.Visible = false;
            if (p_GridControl.EmbeddedNavigator.Buttons.Append.Visible)
            {
                p_GridControl.EmbeddedNavigator.Buttons.Append.Hint = "开始添加";
            }
            p_GridControl.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            if (p_GridControl.EmbeddedNavigator.Buttons.EndEdit.Visible)
            {
                p_GridControl.EmbeddedNavigator.Buttons.EndEdit.Hint = "提交修改";
            }
            p_GridControl.EmbeddedNavigator.Buttons.Remove.Visible = false;
            if (p_GridControl.EmbeddedNavigator.Buttons.Remove.Visible)
            {
                p_GridControl.EmbeddedNavigator.Buttons.Remove.Hint = "开始删除";
            }
            p_GridControl.EmbeddedNavigator.Buttons.First.Visible = true;
            if (p_GridControl.EmbeddedNavigator.Buttons.First.Visible)
            {
                p_GridControl.EmbeddedNavigator.Buttons.First.Hint = "首条";
            }
            p_GridControl.EmbeddedNavigator.Buttons.Next.Visible = true;
            if (p_GridControl.EmbeddedNavigator.Buttons.Next.Visible)
            {
                p_GridControl.EmbeddedNavigator.Buttons.Next.Hint = "下一条";
            }
            p_GridControl.EmbeddedNavigator.Buttons.Last.Visible = true;
            if (p_GridControl.EmbeddedNavigator.Buttons.Last.Visible)
            {
                p_GridControl.EmbeddedNavigator.Buttons.Last.Hint = "末条";
            }
            p_GridControl.EmbeddedNavigator.Buttons.NextPage.Visible = true;
            if (p_GridControl.EmbeddedNavigator.Buttons.NextPage.Visible)
            {
                p_GridControl.EmbeddedNavigator.Buttons.NextPage.Hint = "下一页";
            }
            p_GridControl.EmbeddedNavigator.Buttons.PrevPage.Visible = true;
            if (p_GridControl.EmbeddedNavigator.Buttons.PrevPage.Visible)
            {
                p_GridControl.EmbeddedNavigator.Buttons.PrevPage.Hint = "上一页";
            }
            p_GridControl.EmbeddedNavigator.Buttons.Prev.Visible = true;
            if (p_GridControl.EmbeddedNavigator.Buttons.Prev.Visible)
            {
                p_GridControl.EmbeddedNavigator.Buttons.Prev.Hint = "上一条";
            }
        }

        private void pri_SetGridC(bool p_bool_IsNavigator, GridControl p_grid, string p_str_GridName, GridView p_view, bool p_bool_IsAddColu, Form p_form, bool p_bool_IsComp)
        {
            p_grid.DataSource = null;
            p_grid.RepositoryItems.Clear();
            p_view.Columns.Clear();
            if (p_bool_IsNavigator)
            {
                p_grid.UseEmbeddedNavigator = true;
                p_grid.EmbeddedNavigator.TextStringFormat = "第{0}条,共{1}条";
                p_grid.EmbeddedNavigator.Buttons.Edit.Visible = false;
                if (p_grid.EmbeddedNavigator.Buttons.Edit.Visible)
                {
                    p_grid.EmbeddedNavigator.Buttons.Edit.Hint = "开始修改";
                }
                p_grid.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
                if (p_grid.EmbeddedNavigator.Buttons.CancelEdit.Visible)
                {
                    p_grid.EmbeddedNavigator.Buttons.CancelEdit.Hint = "撤消修改";
                }
                p_grid.EmbeddedNavigator.Buttons.Append.Visible = false;
                if (p_grid.EmbeddedNavigator.Buttons.Append.Visible)
                {
                    p_grid.EmbeddedNavigator.Buttons.Append.Hint = "开始添加";
                }
                p_grid.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
                if (p_grid.EmbeddedNavigator.Buttons.EndEdit.Visible)
                {
                    p_grid.EmbeddedNavigator.Buttons.EndEdit.Hint = "提交修改";
                }
                p_grid.EmbeddedNavigator.Buttons.Remove.Visible = false;
                if (p_grid.EmbeddedNavigator.Buttons.Remove.Visible)
                {
                    p_grid.EmbeddedNavigator.Buttons.Remove.Hint = "开始删除";
                }
                p_grid.EmbeddedNavigator.Buttons.First.Visible = true;
                if (p_grid.EmbeddedNavigator.Buttons.First.Visible)
                {
                    p_grid.EmbeddedNavigator.Buttons.First.Hint = "首条";
                }
                p_grid.EmbeddedNavigator.Buttons.Next.Visible = true;
                if (p_grid.EmbeddedNavigator.Buttons.Next.Visible)
                {
                    p_grid.EmbeddedNavigator.Buttons.Next.Hint = "下一条";
                }
                p_grid.EmbeddedNavigator.Buttons.Last.Visible = true;
                if (p_grid.EmbeddedNavigator.Buttons.Last.Visible)
                {
                    p_grid.EmbeddedNavigator.Buttons.Last.Hint = "末条";
                }
                p_grid.EmbeddedNavigator.Buttons.NextPage.Visible = true;
                if (p_grid.EmbeddedNavigator.Buttons.NextPage.Visible)
                {
                    p_grid.EmbeddedNavigator.Buttons.NextPage.Hint = "下一页";
                }
                p_grid.EmbeddedNavigator.Buttons.PrevPage.Visible = true;
                if (p_grid.EmbeddedNavigator.Buttons.PrevPage.Visible)
                {
                    p_grid.EmbeddedNavigator.Buttons.PrevPage.Hint = "上一页";
                }
                p_grid.EmbeddedNavigator.Buttons.Prev.Visible = true;
                if (p_grid.EmbeddedNavigator.Buttons.Prev.Visible)
                {
                    p_grid.EmbeddedNavigator.Buttons.Prev.Hint = "上一条";
                }
            }
        }

        private void pri_SetGridColumn(GridView p_GridView, string[] p_str_Columns)
        {
            for (int i = 0; i < p_str_Columns.Length; i++)
            {
                GridColumn column = new GridColumn();
                string[] strArray = p_str_Columns[i].Split(new char[] { '|' });
                column.Name = strArray[0].ToString();
                column.Caption = strArray[0].ToString();
                column.FieldName = strArray[1].ToString();
                column.Visible = true;
                column.Width = 80;
                column.VisibleIndex = i;
                column.OptionsColumn.ReadOnly = true;
                this.pri_SetFieldType(column, strArray[2].ToString());
                column.OptionsFilter.AllowFilter = false;
                p_GridView.Columns.Add(column);
            }
        }

        private void pri_SetGridColumn(DataTable p_data_subwhere, GridControl p_control, GridView p_GridView)
        {
            //if ((p_data_subwhere != null) && (p_data_subwhere.Rows.Count > 0))
            //{
            //    for (int i = 0; i < p_data_subwhere.Rows.Count; i++)
            //    {
            //        GridColumn column = new GridColumn
            //        {
            //            Name = p_data_subwhere.Rows[i]["FieldName"].ToString(),
            //            Caption = p_data_subwhere.Rows[i]["FieldCaption"].ToString(),
            //            FieldName = p_data_subwhere.Rows[i]["FieldName"].ToString(),
            //            Width = 80,
            //            OptionsColumn = { ReadOnly = true },
            //            VisibleIndex = int.Parse(p_data_subwhere.Rows[i]["OrderNo"].ToString()),
            //            OptionsFilter = { AllowFilter = false }
            //        };
            //        if (p_data_subwhere.Rows[i]["IsGroup"].ToString().ToLower() == "true")
            //        {
            //            this.pri_setSumColumn(column);
            //        }
            //        this.pri_SetOrderNo(p_GridView, column, p_data_subwhere.Rows[i]["OrderNo"].ToString());
            //        p_GridView.Columns.Add(column);
            //        column.Visible = bool.Parse(p_data_subwhere.Rows[i]["IsVisble"].ToString());
            //    }
            //    p_control.MainView = p_GridView;
            //    p_control.ViewCollection.AddRange(new BaseView[] { p_GridView });
            //}
        }

        private void pri_SetGridColumn(DataTable p_data_subwhere, GridControl p_control, string p_str_GroupPanelText)
        {
            //BandedGridView bgv = new BandedGridView
            //{
            //    GridControl = p_control,
            //    GroupPanelText = p_str_GroupPanelText,
            //    Name = "bgv1"
            //};
            //this.pri_SetBandedGridViewStyle(bgv);
            ////this.pri_CreateChildBand(p_data_subwhere, bgv, null);
            //p_control.MainView = bgv;
            //p_control.ViewCollection.AddRange(new BaseView[] { bgv });
        }

        private void pri_SetGridView(GridView p_GridView, string p_str_GroupPanelText)
        {
            p_GridView.GroupPanelText = p_str_GroupPanelText;
            p_GridView.OptionsView.ShowAutoFilterRow = true;
            p_GridView.OptionsCustomization.AllowGroup = true;
            p_GridView.OptionsView.ShowFooter = true;
            p_GridView.Appearance.OddRow.BackColor = Color.Beige;
            p_GridView.OptionsView.EnableAppearanceOddRow = true;
            p_GridView.OptionsView.ColumnAutoWidth = false;
            p_GridView.OptionsCustomization.AllowGroup = true;
            p_GridView.OptionsBehavior.AutoExpandAllGroups = true;
        }

        private void pri_SetOrderNo(GridView p_GridView, GridColumn p_GridColumn, string orderType)
        {
            switch (orderType)
            {
                case "asc":
                    p_GridView.SortInfo.Add(new GridColumnSortInfo(p_GridColumn, ColumnSortOrder.Ascending));
                    break;

                case "desc":
                    p_GridView.SortInfo.Add(new GridColumnSortInfo(p_GridColumn, ColumnSortOrder.Descending));
                    break;
            }
        }

        private void pri_setSumColumn(GridColumn p_GridColumn)
        {
            p_GridColumn.SummaryItem.DisplayFormat = "{0:c} ";
            p_GridColumn.SummaryItem.SummaryType = SummaryItemType.Sum;
        }

        public DataTable pub_GetGridDataReadFormXml(string XmlFilename, string tablename)
        {
            DataSet set = new DataSet();
            set.ReadXml(XmlFilename, XmlReadMode.ReadSchema);
            if (set.Tables.IndexOf(tablename) > -1)
            {
                return set.Tables[tablename];
            }
            return null;
        }

        public void pub_SetBandedGridViewStyle(BandedGridView bgv)
        {
            this.pri_SetBandedGridViewStyle(bgv);
        }

        public void pub_SetGridC(GridControl p_GridControl, GridView p_GridView, string p_str_GroupPanelText)
        {
            p_GridControl.RepositoryItems.Clear();
            p_GridView.Columns.Clear();
            this.pri_SetGridBar(p_GridControl);
            this.pri_SetGridView(p_GridView, p_str_GroupPanelText);
        }

        public void pub_SetGridC(GridControl p_GridControl, GridView p_GridView, string p_str_GroupPanelText, DataTable p_data_ColumnStyle)
        {
            p_GridControl.RepositoryItems.Clear();
            p_GridView.Columns.Clear();
            this.pri_SetGridBar(p_GridControl);
            this.pri_SetGridView(p_GridView, p_str_GroupPanelText);
            DataRow[] rowArray = p_data_ColumnStyle.Select("isnull(PareID,'')<>''");
            if ((rowArray != null) && (rowArray.Length > 0))
            {
                this.pri_SetGridColumn(p_data_ColumnStyle, p_GridControl, p_str_GroupPanelText);
            }
            else
            {
                this.pri_SetGridColumn(p_data_ColumnStyle, p_GridControl, p_GridView);
            }
        }

        public void pub_SetGridC(GridControl p_GridControl, GridView p_GridView, string[] p_str_Columns, string p_str_GroupPanelText)
        {
            p_GridControl.RepositoryItems.Clear();
            p_GridView.Columns.Clear();
            this.pri_SetGridBar(p_GridControl);
            this.pri_SetGridView(p_GridView, p_str_GroupPanelText);
            this.pri_SetGridColumn(p_GridView, p_str_Columns);
        }

        public void pub_SetGridC(bool p_bool_IsNavigator, GridControl p_grid, string p_str_GridName, GridView p_view, bool p_bool_IsAddColu)
        {
            this.pri_SetGridC(p_bool_IsNavigator, p_grid, p_str_GridName, p_view, p_bool_IsAddColu, null, false);
        }

        public void pub_SetGridC(bool p_bool_IsNavigator, GridControl p_grid, string p_str_GridName, GridView p_view, bool p_bool_IsAddColu, Form p_form, bool p_bool_IsComp)
        {
            this.pri_SetGridC(p_bool_IsNavigator, p_grid, p_str_GridName, p_view, p_bool_IsAddColu, p_form, p_bool_IsComp);
        }

        public void pub_SetGridC(bool p_bool_IsNavigator, GridControl p_grid, string p_str_GridName, GridView p_view, bool p_bool_IsAddColu, Form p_form, bool p_bool_IsComp, ImageList p_ImageList)
        {
            this.pri_SetGridC(p_bool_IsNavigator, p_grid, p_str_GridName, p_view, p_bool_IsAddColu, p_form, p_bool_IsComp);
        }

        public void SetGrid(bool p_bool_IsNavigator, GridControl p_grid, GridView p_view)
        {
            p_grid.DataSource = null;
            p_grid.RepositoryItems.Clear();
            if (p_bool_IsNavigator)
            {
                p_grid.UseEmbeddedNavigator = true;
                p_grid.EmbeddedNavigator.TextStringFormat = "第{0}条,共{1}条";
                p_grid.EmbeddedNavigator.Buttons.Edit.Visible = false;
                if (p_grid.EmbeddedNavigator.Buttons.Edit.Visible)
                {
                    p_grid.EmbeddedNavigator.Buttons.Edit.Hint = "开始修改";
                }
                p_grid.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
                if (p_grid.EmbeddedNavigator.Buttons.CancelEdit.Visible)
                {
                    p_grid.EmbeddedNavigator.Buttons.CancelEdit.Hint = "撤消修改";
                }
                p_grid.EmbeddedNavigator.Buttons.Append.Visible = false;
                if (p_grid.EmbeddedNavigator.Buttons.Append.Visible)
                {
                    p_grid.EmbeddedNavigator.Buttons.Append.Hint = "开始添加";
                }
                p_grid.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
                if (p_grid.EmbeddedNavigator.Buttons.EndEdit.Visible)
                {
                    p_grid.EmbeddedNavigator.Buttons.EndEdit.Hint = "提交修改";
                }
                p_grid.EmbeddedNavigator.Buttons.Remove.Visible = false;
                if (p_grid.EmbeddedNavigator.Buttons.Remove.Visible)
                {
                    p_grid.EmbeddedNavigator.Buttons.Remove.Hint = "开始删除";
                }
                p_grid.EmbeddedNavigator.Buttons.First.Visible = true;
                if (p_grid.EmbeddedNavigator.Buttons.First.Visible)
                {
                    p_grid.EmbeddedNavigator.Buttons.First.Hint = "首条";
                }
                p_grid.EmbeddedNavigator.Buttons.Next.Visible = true;
                if (p_grid.EmbeddedNavigator.Buttons.Next.Visible)
                {
                    p_grid.EmbeddedNavigator.Buttons.Next.Hint = "下一条";
                }
                p_grid.EmbeddedNavigator.Buttons.Last.Visible = true;
                if (p_grid.EmbeddedNavigator.Buttons.Last.Visible)
                {
                    p_grid.EmbeddedNavigator.Buttons.Last.Hint = "末条";
                }
                p_grid.EmbeddedNavigator.Buttons.NextPage.Visible = true;
                if (p_grid.EmbeddedNavigator.Buttons.NextPage.Visible)
                {
                    p_grid.EmbeddedNavigator.Buttons.NextPage.Hint = "下一页";
                }
                p_grid.EmbeddedNavigator.Buttons.PrevPage.Visible = true;
                if (p_grid.EmbeddedNavigator.Buttons.PrevPage.Visible)
                {
                    p_grid.EmbeddedNavigator.Buttons.PrevPage.Hint = "上一页";
                }
                p_grid.EmbeddedNavigator.Buttons.Prev.Visible = true;
                if (p_grid.EmbeddedNavigator.Buttons.Prev.Visible)
                {
                    p_grid.EmbeddedNavigator.Buttons.Prev.Hint = "上一条";
                }
            }
            p_view.OptionsView.ShowGroupPanel = false;
            p_view.GroupSummary.Clear();
            p_view.OptionsBehavior.Editable = true;
            p_view.OptionsCustomization.AllowSort = false;
            p_view.OptionsView.ShowAutoFilterRow = true;
            p_view.Appearance.OddRow.BackColor = Color.DarkGray;
            p_view.OptionsView.EnableAppearanceOddRow = true;
            p_view.OptionsView.ShowFooter = false;
            p_view.OptionsSelection.MultiSelect = false;
            p_view.OptionsView.ColumnAutoWidth = false;
            p_view.OptionsCustomization.AllowGroup = false;
            p_view.OptionsBehavior.AutoExpandAllGroups = false;
            p_grid.RepositoryItems.Clear();
            foreach (GridColumn column in p_view.Columns)
            {
                column.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                column.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                column.OptionsColumn.AllowMove = true;
            }
        }
    }

    public class GridViewC
    {
        public void SetGridView(GridView gridView)
        {
            gridView.OptionsCustomization.AllowColumnMoving = false;
            gridView.OptionsCustomization.AllowSort = false;
            gridView.OptionsMenu.EnableColumnMenu = false;
            gridView.OptionsMenu.EnableFooterMenu = false;
            gridView.OptionsMenu.EnableGroupPanelMenu = false;
            gridView.OptionsMenu.ShowAutoFilterRowItem = false;
            gridView.OptionsMenu.ShowGroupSortSummaryItems = false;
            gridView.OptionsMenu.ShowSplitItem = false;
            gridView.OptionsView.ColumnAutoWidth = false;
            gridView.OptionsView.ShowGroupPanel = false;

            foreach (GridColumn col in gridView.Columns)
            {
                col.AppearanceHeader.Options.UseTextOptions = true;
                col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                col.OptionsColumn.AllowEdit = false;
                col.OptionsColumn.FixedWidth = true;
                col.Visible = true;
                col.Width = 150;
            }
        }

    }

}

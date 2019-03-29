using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml.Serialization;
using static MDIDemo.PublicClass.Class_DataBaseContent;

namespace MDIDemo.PublicClass
{
    public class Class_ToExcel
    {

        /// <summary>
        /// 导出数据库说明书
        /// </summary>
        /// <param name="class_DataBaseContent"></param>
        /// <returns></returns>
        public string GetDataBaseContent(Class_DataBaseContent class_DataBaseContent, string AllPathFileName)
        {
            #region 参数
            int TitleRow = 1;
            int TitleLength = 7;
            int TitleScriptRow = TitleRow + 2;
            #endregion

            if ((class_DataBaseContent != null) && (class_DataBaseContent.class_SheetContents.Count > 0))
            {
                using (ExcelPackage package = new ExcelPackage())
                {
                    int DataBeginRow = TitleScriptRow + 1;
                    int DataEndRow = TitleScriptRow + 1;
                    int DataBeginCol = 2;
                    int DataEndCol = 2;
                    //int TableIndex = 0;

                    OfficeOpenXml.Table.TableStyles NowTableStyle = OfficeOpenXml.Table.TableStyles.Medium2;
                    foreach (Class_SheetContent class_SheetContent in class_DataBaseContent.class_SheetContents)
                    {
                        DataEndRow = DataBeginRow + class_SheetContent.dataTable.Rows.Count;
                        DataEndCol = DataBeginCol + class_SheetContent.dataTable.Columns.Count - 1;
                        ExcelWorksheet NowSheet = package.Workbook.Worksheets.Add(class_SheetContent.SheetName);

                        if (class_SheetContent.dataTable.Rows.Count > 0)
                        {
                            #region 设置字体和对齐
                            NowSheet.Cells.Style.Font.Name = "微软雅黑";
                            NowSheet.Cells.Style.Font.Size = 12;
                            NowSheet.Cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            NowSheet.Cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                            #endregion

                            #region 标题
                            if (class_SheetContent.SheetTitle.Length > 0)
                            {
                                NowSheet.Cells[TitleRow, DataBeginCol].Value = class_SheetContent.SheetTitle;
                                NowSheet.Cells[TitleRow, DataBeginCol].Style.Font.Bold = true;
                                NowSheet.Cells[TitleRow, DataBeginCol].Style.Font.Size = 24;
                                NowSheet.Cells[TitleRow, DataBeginCol, TitleRow, DataBeginCol + TitleLength].Merge = true;
                                NowSheet.Cells[TitleRow, DataBeginCol, TitleRow, DataBeginCol + TitleLength].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                NowSheet.Cells[TitleRow, DataBeginCol, TitleRow, DataBeginCol + TitleLength].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(91, 155, 213));
                                NowSheet.Cells[TitleScriptRow, (DataEndCol > 9 ? 9 : DataEndCol) - 2, TitleScriptRow, DataEndCol > 9 ? 9 : DataEndCol].Merge = true;
                            }
                            #endregion

                            #region 描述栏
                            if (class_SheetContent.SheetTitle.Length > 0)
                            {
                                NowSheet.Cells[TitleScriptRow, (DataEndCol > 12 ? 12 : DataEndCol) - 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                                NowSheet.Cells[TitleScriptRow, (DataEndCol > 12 ? 12 : DataEndCol) - 2].Style.Font.Bold = true;
                                NowSheet.Cells[TitleScriptRow, (DataEndCol > 12 ? 12 : DataEndCol) - 2].Style.Font.Size = 13;
                                NowSheet.Cells[TitleScriptRow, (DataEndCol > 12 ? 12 : DataEndCol) - 2].Value = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            }
                            #endregion

                            #region 写入数据
                            NowSheet.Cells[DataBeginRow, DataBeginCol].LoadFromDataTable(class_SheetContent.dataTable, true, NowTableStyle);
                            #endregion

                            #region 写入中文表头
                            if (class_SheetContent.FieldTitleList.Count > 0)
                            {
                                for (int Index = 0; Index < class_SheetContent.FieldTitleList.Count; Index++)
                                {
                                    NowSheet.Cells[DataBeginRow, DataBeginCol + Index].Value = class_SheetContent.FieldTitleList[Index].ToString();
                                    NowSheet.Cells[DataBeginRow, DataBeginCol + Index].Style.Font.Bold = true;
                                }
                            }
                            #endregion

                            #region 数字列左靠齐
                            for (int Index = 0; Index < class_SheetContent.dataTable.Columns.Count; Index++)
                            {
                                if (Index > 0)
                                {
                                    string ColType = class_SheetContent.dataTable.Columns[Index].DataType.ToString();
                                    bool IsRight = false;
                                    //bool IsNumber = false;
                                    bool IsPercent = false;
                                    #region 类型
                                    switch (ColType)
                                    {
                                        case "System.Decimal":
                                            IsRight = true;
                                            //IsNumber = true;
                                            if ((class_SheetContent.dataTable.Columns[Index].ColumnName.IndexOf("比例") > -1) || (class_SheetContent.dataTable.Columns[Index].ColumnName.IndexOf("Scale") > -1))
                                            {
                                                IsPercent = true;
                                            }
                                            if (IsRight)
                                            {
                                                if (!IsPercent)
                                                    NowSheet.Cells[DataBeginRow + 1, DataBeginCol + Index, DataEndRow, DataBeginCol + Index].Style.Numberformat.Format = "#,##0.00";
                                                else
                                                    NowSheet.Cells[DataBeginRow + 1, DataBeginCol + Index, DataEndRow, DataBeginCol + Index].Style.Numberformat.Format = "0.00%";
                                            }
                                            break;
                                        case "System.Int32":
                                            IsRight = true;
                                            break;
                                        case "System.String":
                                        case "System.Object":
                                            if (class_SheetContent.LeftFieldNameList.IndexOf(class_SheetContent.dataTable.Columns[Index].ColumnName) > -1)
                                            {
                                                NowSheet.Cells[DataBeginRow + 1, DataBeginCol + Index, DataEndRow, DataBeginCol + Index].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                                            }
                                            break;
                                        case "System.Float":
                                            IsRight = true;
                                            //IsNumber = true;
                                            if ((class_SheetContent.dataTable.Columns[Index].ColumnName.IndexOf("比例") > -1) || (class_SheetContent.dataTable.Columns[Index].ColumnName.IndexOf("Scale") > -1))
                                            {
                                                IsPercent = true;
                                            }
                                            if (IsRight)
                                            {
                                                if (!IsPercent)
                                                    NowSheet.Cells[DataBeginRow + 1, DataBeginCol + Index, DataEndRow, DataBeginCol + Index].Style.Numberformat.Format = "#,##0.00";
                                                else
                                                    NowSheet.Cells[DataBeginRow + 1, DataBeginCol + Index, DataEndRow, DataBeginCol + Index].Style.Numberformat.Format = "0.00%";
                                            }
                                            break;
                                        default:
                                            IsRight = false;
                                            break;
                                    }
                                    if (IsRight)
                                    {
                                        NowSheet.Cells[DataBeginRow + 1, DataBeginCol + Index, DataEndRow, DataBeginCol + Index].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                                    }
                                    //if (IsTotal || IsAverage)
                                    //{
                                    //    if (IsTotal)
                                    //    {
                                    //        if (SumFieldNameList.IndexOf("," + NowTable.Columns[Index].Caption + ",") > -1)
                                    //        {
                                    //            NowSheet.Cells[DataEndRow + 1, DataBeginCol + Index].Formula = string.Format(@"SUM({0})"
                                    //                , new ExcelAddress(DataBeginRow + 1, DataBeginCol + Index, DataEndRow, DataBeginCol + Index).Address);
                                    //            if (IsNumber)
                                    //                NowSheet.Cells[DataEndRow + 1, DataBeginCol + Index].Style.Numberformat.Format = "#,##0.00";
                                    //        }
                                    //    }
                                    //    if (IsAverage)
                                    //    {
                                    //        if (AveFieldNameList.IndexOf(NowTable.Columns[Index].Caption) > -1)
                                    //        {
                                    //            NowSheet.Cells[DataEndRow + 1, DataBeginCol + Index].Formula = string.Format(@"AVERAGE({0})"
                                    //                , new ExcelAddress(DataBeginRow + 1, DataBeginCol + Index, DataEndRow, DataBeginCol + Index).Address);
                                    //            if (IsNumber)
                                    //                NowSheet.Cells[DataEndRow + 1, DataBeginCol + Index].Style.Numberformat.Format = "#,##0.00";
                                    //        }
                                    //    }
                                    //    NowSheet.Cells[DataEndRow + 1, DataBeginCol + Index].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                    //    NowSheet.Cells[DataEndRow + 1, DataBeginCol + Index].Style.Fill.BackgroundColor.SetColor(Color.Blue);
                                    //    NowSheet.Cells[DataEndRow + 1, DataBeginCol + Index].Style.Font.Color.SetColor(Color.Yellow);
                                    //    NowSheet.Cells[DataEndRow + 1, DataBeginCol + Index].Style.Font.Bold = true;
                                    //}
                                    #endregion

                                    //#region 备注
                                    //string RemarkCol = NowTable.Columns[Index].ColumnName;
                                    //if ((RemarkCol == "Remark") || (RemarkCol == "remark") || (RemarkCol == "备注"))
                                    //{
                                    //    NowSheet.Cells[DataBeginRow + 1, DataBeginCol + Index, DataEndRow, DataBeginCol + Index].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                                    //    NowSheet.Cells[DataBeginRow + 1, DataBeginCol + Index, DataEndRow, DataBeginCol + Index].Style.WrapText = true;//自动换行
                                    //}
                                    //#endregion
                                }
                                NowSheet.Column(DataBeginCol + Index).AutoFit();
                            }
                            #endregion

                            #region 边框
                            NowSheet.Cells[DataBeginRow, DataBeginCol, DataEndRow, DataEndCol].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            NowSheet.Cells[DataBeginRow, DataBeginCol, DataEndRow, DataEndCol].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            NowSheet.Cells[DataBeginRow, DataBeginCol, DataEndRow, DataEndCol].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            NowSheet.Cells[DataBeginRow, DataBeginCol, DataEndRow, DataEndCol].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            NowSheet.Cells[DataBeginRow, DataBeginCol, DataEndRow, DataEndCol].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thick);
                            #endregion

                            #region 表注释
                            NowSheet.Cells[DataEndRow + 1, DataBeginCol].Value = string.Format("说明：{0}", class_SheetContent.TableContent);
                            NowSheet.Cells[DataEndRow + 1, DataBeginCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                            NowSheet.Cells[DataEndRow + 1, DataBeginCol].Style.Font.Color.SetColor(Color.Blue);
                            NowSheet.Cells[DataEndRow + 1, DataBeginCol].Style.Font.Bold = true;
                            #endregion
                        }

                    }
                    #region 保存EXCEL
                    FileInfo file = new FileInfo(AllPathFileName);
                    package.SaveAs(file);
                    #endregion
                }
                return AllPathFileName;
            }
            else
                return null;
        }

        /// <summary>
        /// 导出EXCEL
        /// </summary>
        /// <param name="SheetNameArray">Sheet名</param>
        /// <param name="SheetTitleArray">Sheet内部标题</param>
        /// <param name="IsTotal">是否有合计</param>
        /// <param name="ExcelFileName">EXCEL文件名</param>
        /// <param name="FieldTitleArray">中文表头</param>
        /// <param name="SumFieldNameList">合计字段名（英文）</param>
        /// <param name="IsAverage">取平均数</param>
        /// <param name="AveFieldNameList">平均字段名</param>
        /// <param name="IsHeader">是否显示表标题</param>
        /// <param name="LeftFieldName">左空字段</param>
        /// <param name="ExcelDataSet">数据集</param>
        /// <param name="IsZip"></param>
        private void _GiveYouExcel(ArrayList SheetNameArray, ArrayList SheetTitleArray
            , bool IsTotal, string ExcelFileName, ArrayList FieldTitleArray, string SumFieldNameList
            , bool IsAverage, ArrayList AveFieldNameList, bool IsHeader, string LeftFieldName
            , DataSet ExcelDataSet
            , bool IsZip = false)
        {
            #region 参数
            int TitleRow = 1;
            int TitleLength = 10;
            int TitleScriptRow = TitleRow + 2;
            if (SheetTitleArray.Count == 0)
            {
                TitleScriptRow = 0;
            }
            #endregion

            #region 导出
            //MyClass = new Frame_Class();
            //ExcelDataSet = MyClass.GetChangeExcel(SqlArray, SheetNameArray);
            if ((ExcelDataSet != null) && (ExcelDataSet.Tables.Count > 0))
            {
                using (ExcelPackage package = new ExcelPackage())
                {
                    int DataBeginRow = TitleScriptRow + 1;
                    int DataEndRow = TitleScriptRow + 1;
                    int DataBeginCol = 2;
                    int DataEndCol = 2;
                    if (SheetTitleArray.Count == 0)
                    {
                        DataBeginCol = 1;
                        DataEndCol = 1;
                    }
                    int TableIndex = 0;
                    OfficeOpenXml.Table.TableStyles NowTableStyle = OfficeOpenXml.Table.TableStyles.Medium2;
                    foreach (DataTable NowTable in ExcelDataSet.Tables)
                    {
                        DataEndRow = DataBeginRow + NowTable.Rows.Count;
                        DataEndCol = DataBeginCol + NowTable.Columns.Count - 1;
                        ExcelWorksheet NowSheet = package.Workbook.Worksheets.Add(SheetNameArray[TableIndex].ToString());
                        if (NowTable.Rows.Count > 0)
                        {
                            #region 设置字体和对齐
                            NowSheet.Cells.Style.Font.Name = "微软雅黑";
                            NowSheet.Cells.Style.Font.Size = 12;
                            NowSheet.Cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            NowSheet.Cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                            #endregion

                            #region 标题
                            if (SheetTitleArray.Count > 0)
                            {
                                NowSheet.Cells[TitleRow, DataBeginCol].Value = SheetTitleArray[TableIndex].ToString();
                                NowSheet.Cells[TitleRow, DataBeginCol].Style.Font.Bold = true;
                                NowSheet.Cells[TitleRow, DataBeginCol].Style.Font.Size = 24;
                                NowSheet.Cells[TitleRow, DataBeginCol, TitleRow, DataBeginCol + TitleLength].Merge = true;
                                NowSheet.Cells[TitleRow, DataBeginCol, TitleRow, DataBeginCol + TitleLength].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                NowSheet.Cells[TitleRow, DataBeginCol, TitleRow, DataBeginCol + TitleLength].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(91, 155, 213));
                                NowSheet.Cells[TitleScriptRow, (DataEndCol > 12 ? 12 : DataEndCol) - 2, TitleScriptRow, DataEndCol > 12 ? 12 : DataEndCol].Merge = true;
                            }
                            #endregion

                            #region 描述栏
                            if (SheetTitleArray.Count > 0)
                            {
                                NowSheet.Cells[TitleScriptRow, (DataEndCol > 12 ? 12 : DataEndCol) - 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                                NowSheet.Cells[TitleScriptRow, (DataEndCol > 12 ? 12 : DataEndCol) - 2].Style.Font.Bold = true;
                                NowSheet.Cells[TitleScriptRow, (DataEndCol > 12 ? 12 : DataEndCol) - 2].Style.Font.Size = 13;
                                NowSheet.Cells[TitleScriptRow, (DataEndCol > 12 ? 12 : DataEndCol) - 2].Value = string.Format("导出时间：{0}", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            }
                            #endregion

                            #region 写入数据
                            NowSheet.Cells[DataBeginRow, DataBeginCol].LoadFromDataTable(NowTable, IsHeader, NowTableStyle);
                            #endregion

                            #region 写入中文表头
                            if ((FieldTitleArray.Count > 0) && IsHeader)
                            {
                                string[] ChinaTitle = FieldTitleArray[TableIndex].ToString().Split(',');
                                for (int Index = 0; Index < ChinaTitle.Length; Index++)
                                {
                                    NowSheet.Cells[DataBeginRow, DataBeginCol + Index].Value = ChinaTitle[Index].ToString();
                                    NowSheet.Cells[DataBeginRow, DataBeginCol + Index].Style.Font.Bold = true;
                                }
                            }
                            #endregion

                            #region 数字列左靠齐
                            for (int Index = 0; Index < NowTable.Columns.Count; Index++)
                            {
                                if (Index > 0)
                                {
                                    string ColType = NowTable.Columns[Index].DataType.ToString();
                                    bool IsRight = false;
                                    bool IsNumber = false;
                                    bool IsPercent = false;
                                    #region 类型
                                    switch (ColType)
                                    {
                                        case "System.Decimal":
                                            IsRight = true;
                                            IsNumber = true;
                                            if ((NowTable.Columns[Index].ColumnName.IndexOf("比例") > -1) || (NowTable.Columns[Index].ColumnName.IndexOf("Scale") > -1))
                                            {
                                                IsPercent = true;
                                            }
                                            if (IsRight)
                                            {
                                                if (!IsPercent)
                                                    NowSheet.Cells[DataBeginRow + 1, DataBeginCol + Index, DataEndRow, DataBeginCol + Index].Style.Numberformat.Format = "#,##0.00";
                                                else
                                                    NowSheet.Cells[DataBeginRow + 1, DataBeginCol + Index, DataEndRow, DataBeginCol + Index].Style.Numberformat.Format = "0.00%";
                                            }
                                            break;
                                        case "System.Int32":
                                            IsRight = true;
                                            break;
                                        case "System.String":
                                            if (LeftFieldName != null)
                                            {
                                                if (LeftFieldName.IndexOf("," + NowTable.Columns[Index].ColumnName + ",") > -1)
                                                {
                                                    NowSheet.Cells[DataBeginRow + 1, DataBeginCol + Index, DataEndRow, DataBeginCol + Index].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                                                }
                                            }
                                            break;
                                        case "System.Float":
                                            IsRight = true;
                                            IsNumber = true;
                                            if ((NowTable.Columns[Index].ColumnName.IndexOf("比例") > -1) || (NowTable.Columns[Index].ColumnName.IndexOf("Scale") > -1))
                                            {
                                                IsPercent = true;
                                            }
                                            if (IsRight)
                                            {
                                                if (!IsPercent)
                                                    NowSheet.Cells[DataBeginRow + 1, DataBeginCol + Index, DataEndRow, DataBeginCol + Index].Style.Numberformat.Format = "#,##0.00";
                                                else
                                                    NowSheet.Cells[DataBeginRow + 1, DataBeginCol + Index, DataEndRow, DataBeginCol + Index].Style.Numberformat.Format = "0.00%";
                                            }
                                            break;
                                        default:
                                            IsRight = false;
                                            break;
                                    }
                                    if (IsRight)
                                    {
                                        NowSheet.Cells[DataBeginRow + 1, DataBeginCol + Index, DataEndRow, DataBeginCol + Index].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                                    }
                                    if (IsTotal || IsAverage)
                                    {
                                        if (IsTotal)
                                        {
                                            if (SumFieldNameList.IndexOf("," + NowTable.Columns[Index].Caption + ",") > -1)
                                            {
                                                NowSheet.Cells[DataEndRow + 1, DataBeginCol + Index].Formula = string.Format(@"SUM({0})"
                                                    , new ExcelAddress(DataBeginRow + 1, DataBeginCol + Index, DataEndRow, DataBeginCol + Index).Address);
                                                if (IsNumber)
                                                    NowSheet.Cells[DataEndRow + 1, DataBeginCol + Index].Style.Numberformat.Format = "#,##0.00";
                                            }
                                        }
                                        if (IsAverage)
                                        {
                                            if (AveFieldNameList.IndexOf(NowTable.Columns[Index].Caption) > -1)
                                            {
                                                NowSheet.Cells[DataEndRow + 1, DataBeginCol + Index].Formula = string.Format(@"AVERAGE({0})"
                                                    , new ExcelAddress(DataBeginRow + 1, DataBeginCol + Index, DataEndRow, DataBeginCol + Index).Address);
                                                if (IsNumber)
                                                    NowSheet.Cells[DataEndRow + 1, DataBeginCol + Index].Style.Numberformat.Format = "#,##0.00";
                                            }
                                        }
                                        NowSheet.Cells[DataEndRow + 1, DataBeginCol + Index].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                        NowSheet.Cells[DataEndRow + 1, DataBeginCol + Index].Style.Fill.BackgroundColor.SetColor(Color.Blue);
                                        NowSheet.Cells[DataEndRow + 1, DataBeginCol + Index].Style.Font.Color.SetColor(Color.Yellow);
                                        NowSheet.Cells[DataEndRow + 1, DataBeginCol + Index].Style.Font.Bold = true;
                                    }
                                    #endregion

                                    //#region 备注
                                    //string RemarkCol = NowTable.Columns[Index].ColumnName;
                                    //if ((RemarkCol == "Remark") || (RemarkCol == "remark") || (RemarkCol == "备注"))
                                    //{
                                    //    NowSheet.Cells[DataBeginRow + 1, DataBeginCol + Index, DataEndRow, DataBeginCol + Index].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                                    //    NowSheet.Cells[DataBeginRow + 1, DataBeginCol + Index, DataEndRow, DataBeginCol + Index].Style.WrapText = true;//自动换行
                                    //}
                                    //#endregion
                                }
                                NowSheet.Column(DataBeginCol + Index).AutoFit();
                            }
                            #endregion

                            #region 合计栏
                            if (IsTotal || IsAverage)
                            {
                                NowSheet.Cells[DataEndRow + 1, DataBeginCol].Value = "合计";
                                NowSheet.Cells[DataEndRow + 1, DataBeginCol].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                NowSheet.Cells[DataEndRow + 1, DataBeginCol].Style.Fill.BackgroundColor.SetColor(Color.Blue);
                                NowSheet.Cells[DataEndRow + 1, DataBeginCol].Style.Font.Color.SetColor(Color.Yellow);
                                NowSheet.Cells[DataEndRow + 1, DataBeginCol].Style.Font.Bold = true;
                                NowSheet.Cells[DataEndRow + 1, DataBeginCol + 1, DataEndRow + 1, DataEndCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                                NowSheet.Cells[DataEndRow + 1, DataBeginCol + 1, DataEndRow + 1, DataEndCol].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                            }
                            #endregion

                            #region 边框
                            NowSheet.Cells[DataBeginRow, DataBeginCol, IsTotal ? DataEndRow + 1 : DataEndRow, DataEndCol].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            NowSheet.Cells[DataBeginRow, DataBeginCol, IsTotal ? DataEndRow + 1 : DataEndRow, DataEndCol].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            NowSheet.Cells[DataBeginRow, DataBeginCol, IsTotal ? DataEndRow + 1 : DataEndRow, DataEndCol].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            NowSheet.Cells[DataBeginRow, DataBeginCol, IsTotal ? DataEndRow + 1 : DataEndRow, DataEndCol].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            NowSheet.Cells[DataBeginRow, DataBeginCol, IsTotal ? DataEndRow + 1 : DataEndRow, DataEndCol].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thick);
                            #endregion
                        }
                        else
                        {
                            #region 无数据
                            NowSheet.Cells[DataBeginCol, DataEndCol].Value = "无数据";
                            NowSheet.Cells[DataBeginCol, DataEndCol].Style.Font.Bold = true;
                            NowSheet.Cells[DataBeginCol, DataEndCol].Style.Font.Size = 24;
                            NowSheet.Cells[DataBeginCol, DataEndCol].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            NowSheet.Cells[DataBeginCol, DataEndCol].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(91, 155, 213));
                            #endregion
                        }
                        TableIndex++;
                    }

                    #region 返回EXCEL数据流

                    if (IsZip)
                    {
                        string ExcelFileNameNew = "";
                        if (ExcelFileName.IndexOf(".zip") < 0)
                            ExcelFileNameNew = string.Format("{0}.zip", ExcelFileName);
                        //Response.ContentType = "application/x-zip-compressed ";
                        //Response.AddHeader("content-disposition", string.Format("attachment;  filename={0}", ExcelFileNameNew));

                        //if (ExcelFileName.IndexOf(".xlsx") < 0)
                        //    ExcelFileNameNew = string.Format("{0}.xlsx", ExcelFileName);
                        //Response.BinaryWrite(new Public_Code.YWFrame_Class().ZipCompress(package.GetAsByteArray(), ExcelFileNameNew));
                    }
                    else
                    {
                        if (ExcelFileName.IndexOf(".xlsx") < 0)
                            ExcelFileName = string.Format("{0}.xlsx", ExcelFileName);
                        //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        //Response.AddHeader("content-disposition", string.Format("attachment;  filename={0}", ExcelFileName));
                        //Response.BinaryWrite(package.GetAsByteArray());
                    }
                    #endregion

                    #region 保存EXCEL
                    //FileInfo file = new FileInfo(Server.MapPath(FilePath) + ExcelFileName);
                    //if (!Directory.Exists(Server.MapPath(FilePath)))//判断文件夹是否存在 
                    //{
                    //    Directory.CreateDirectory(Server.MapPath(FilePath));//不存在则创建文件夹 
                    //}
                    //package.SaveAs(file);
                    #endregion
                }
            }
            ExcelDataSet.Dispose();
            #endregion
        }

    }
    /// <summary>
    /// 工具类 2019-3-26
    /// </summary>
    public class Class_Tool
    {
        /// <summary>
        /// 根据英文字段，得到对应的中文字段
        /// </summary>
        /// <param name="FieldName">字段名</param>
        /// <returns></returns>
        public static string GetChinaField(string FieldName)
        {
            if (IsEnglishField(FieldName))
                return FieldName.Replace("English", "");
            else
                return FieldName;
        }
        /// <summary>
        /// 是否为英文字段
        /// </summary>
        /// <param name="FieldName">字段名</param>
        /// <returns></returns>
        public static bool IsEnglishField(string FieldName)
        {
            if ((FieldName != null) && (FieldName.Length > 0))
            {
                int Index = FieldName.IndexOf("English");
                if (Index > -1)
                    return Index == FieldName.Length - 7 ? true : false;
                else
                    return false;
            }
            else
                return false;
        }
        /// <summary>
        /// 得到封装类Java类型
        /// </summary>
        /// <param name="MultJavaType"></param>
        /// <returns></returns>
        public static string GetClosedJavaType(string MultJavaType)
        {
            string[] vs = MultJavaType.Split('.');
            if (MultJavaType != null)
            {
                if (vs.Length > 0)
                    return class_JavaAndClosedClasses.Find(a => a.ClosedType.Equals(MultJavaType)).SimplClosedType;
                else
                    return null;
            }
            else
                return null;
        }
        /// <summary>
        /// 得到简单Java类型
        /// </summary>
        /// <param name="MultJavaType"></param>
        /// <returns></returns>
        public static string GetSimplificationJavaType(string MultJavaType)
        {
            if (MultJavaType != null)
            {
                string[] vs = MultJavaType.Split('.');
                if (vs.Length > 0)
                {
                    return class_JavaAndClosedClasses.Find(a => a.ClosedType.Equals(MultJavaType)).JavaType;
                }
                else
                    return null;
            }
            else
                return null;
        }
        /// <summary>
        /// 得到原文
        /// </summary>
        /// <param name="Content"></param>
        /// <returns></returns>
        public static string UnEscapeCharacter(string Content)
        {
            if (Content != null)
                return System.Text.RegularExpressions.Regex.Unescape(Content);
            else
                return null;
        }
        /// <summary>
        /// 加入转义符
        /// </summary>
        /// <param name="Content"></param>
        /// <returns></returns>
        public static string EscapeCharacter(string Content)
        {
            if (Content != null)
                return System.Text.RegularExpressions.Regex.Escape(Content);
            else
                return null;
        }
        public string GetSetSpaceCount(int Number)
        {
            if (Number > 0)
            {
                int Index = 0;
                string Result = null;
                while (Index < Number)
                {
                    Result += "    ";
                    Index++;
                }
                return Result;
            }
            else
                return "";
        }
        public Class_Tool()
        {
            #region Java基本类型与Jdbc类型
            class_JavaAndJdbcs = new List<Class_JavaAndJdbc>();
            Class_JavaAndJdbc class_JavaAndJdbc = new Class_JavaAndJdbc();
            class_JavaAndJdbc.JdbcType = "VARCHAR";
            class_JavaAndJdbc.JavaType = "java.lang.String";
            class_JavaAndJdbcs.Add(class_JavaAndJdbc);

            class_JavaAndJdbc = new Class_JavaAndJdbc();
            class_JavaAndJdbc.JdbcType = "CHAR";
            class_JavaAndJdbc.JavaType = "java.lang.String";
            class_JavaAndJdbcs.Add(class_JavaAndJdbc);

            class_JavaAndJdbc = new Class_JavaAndJdbc();
            class_JavaAndJdbc.JdbcType = "LONGVARCHAR";
            class_JavaAndJdbc.JavaType = "java.lang.String";
            class_JavaAndJdbcs.Add(class_JavaAndJdbc);

            class_JavaAndJdbc = new Class_JavaAndJdbc();
            class_JavaAndJdbc.JdbcType = "NUMERIC";
            class_JavaAndJdbc.JavaType = "java.math.BigDecimal";
            class_JavaAndJdbcs.Add(class_JavaAndJdbc);

            class_JavaAndJdbc = new Class_JavaAndJdbc();
            class_JavaAndJdbc.JdbcType = "DECIMAL";
            class_JavaAndJdbc.JavaType = "java.math.BigDecimal";
            class_JavaAndJdbcs.Add(class_JavaAndJdbc);

            class_JavaAndJdbc = new Class_JavaAndJdbc();
            class_JavaAndJdbc.JdbcType = "BIT";
            class_JavaAndJdbc.JavaType = "java.lang.Boolean";
            class_JavaAndJdbcs.Add(class_JavaAndJdbc);

            class_JavaAndJdbc = new Class_JavaAndJdbc();
            class_JavaAndJdbc.JdbcType = "BOOLEAN";
            class_JavaAndJdbc.JavaType = "java.lang.Boolean";
            class_JavaAndJdbcs.Add(class_JavaAndJdbc);

            class_JavaAndJdbc = new Class_JavaAndJdbc();
            class_JavaAndJdbc.JdbcType = "TINYINT";
            class_JavaAndJdbc.JavaType = "java.lang.byte";
            class_JavaAndJdbcs.Add(class_JavaAndJdbc);

            class_JavaAndJdbc = new Class_JavaAndJdbc();
            class_JavaAndJdbc.JdbcType = "SMALLINT";
            class_JavaAndJdbc.JavaType = "java.lang.short";
            class_JavaAndJdbcs.Add(class_JavaAndJdbc);

            class_JavaAndJdbc = new Class_JavaAndJdbc();
            class_JavaAndJdbc.JdbcType = "INTEGER";
            class_JavaAndJdbc.JavaType = "java.lang.Integer";
            class_JavaAndJdbcs.Add(class_JavaAndJdbc);

            class_JavaAndJdbc = new Class_JavaAndJdbc();
            class_JavaAndJdbc.JdbcType = "BIGINT";
            class_JavaAndJdbc.JavaType = "java.lang.Long";
            class_JavaAndJdbcs.Add(class_JavaAndJdbc);

            class_JavaAndJdbc = new Class_JavaAndJdbc();
            class_JavaAndJdbc.JdbcType = "REAL";
            class_JavaAndJdbc.JavaType = "java.lang.Float";
            class_JavaAndJdbcs.Add(class_JavaAndJdbc);

            class_JavaAndJdbc = new Class_JavaAndJdbc();
            class_JavaAndJdbc.JdbcType = "FLOAT";
            class_JavaAndJdbc.JavaType = "java.lang.Double";
            class_JavaAndJdbcs.Add(class_JavaAndJdbc);

            class_JavaAndJdbc = new Class_JavaAndJdbc();
            class_JavaAndJdbc.JdbcType = "DOUBLE";
            class_JavaAndJdbc.JavaType = "java.lang.Double";
            class_JavaAndJdbcs.Add(class_JavaAndJdbc);

            class_JavaAndJdbc = new Class_JavaAndJdbc();
            class_JavaAndJdbc.JdbcType = "BINARY";
            class_JavaAndJdbc.JavaType = "java.lang.Byte[]";
            class_JavaAndJdbcs.Add(class_JavaAndJdbc);

            class_JavaAndJdbc = new Class_JavaAndJdbc();
            class_JavaAndJdbc.JdbcType = "VARBINARY";
            class_JavaAndJdbc.JavaType = "java.lang.Byte[]";
            class_JavaAndJdbcs.Add(class_JavaAndJdbc);

            class_JavaAndJdbc = new Class_JavaAndJdbc();
            class_JavaAndJdbc.JdbcType = "LONGVARBINARY";
            class_JavaAndJdbc.JavaType = "java.lang.byte[]";
            class_JavaAndJdbcs.Add(class_JavaAndJdbc);

            class_JavaAndJdbc = new Class_JavaAndJdbc();
            class_JavaAndJdbc.JdbcType = "DATE";
            class_JavaAndJdbc.JavaType = "java.sql.Date";
            class_JavaAndJdbcs.Add(class_JavaAndJdbc);

            class_JavaAndJdbc = new Class_JavaAndJdbc();
            class_JavaAndJdbc.JdbcType = "TIME";
            class_JavaAndJdbc.JavaType = "java.sql.Time";
            class_JavaAndJdbcs.Add(class_JavaAndJdbc);

            class_JavaAndJdbc = new Class_JavaAndJdbc();
            class_JavaAndJdbc.JdbcType = "TIMESTAMP";
            class_JavaAndJdbc.JavaType = "java.sql.Timestamp";
            class_JavaAndJdbcs.Add(class_JavaAndJdbc);

            class_JavaAndJdbc = new Class_JavaAndJdbc();
            class_JavaAndJdbc.JdbcType = "CLOB";
            class_JavaAndJdbc.JavaType = "java.lang.Clob";
            class_JavaAndJdbcs.Add(class_JavaAndJdbc);

            class_JavaAndJdbc = new Class_JavaAndJdbc();
            class_JavaAndJdbc.JdbcType = "BLOB";
            class_JavaAndJdbc.JavaType = "java.lang.Blob";
            class_JavaAndJdbcs.Add(class_JavaAndJdbc);

            class_JavaAndJdbc = new Class_JavaAndJdbc();
            class_JavaAndJdbc.JdbcType = "ARRAY";
            class_JavaAndJdbc.JavaType = "java.lang.Array";
            class_JavaAndJdbcs.Add(class_JavaAndJdbc);
            #endregion

            #region Java基本类型与封装类
            class_JavaAndClosedClasses = new List<Class_JavaAndClosedClass>();
            Class_JavaAndClosedClass class_JavaAndClosedClass = new Class_JavaAndClosedClass();
            class_JavaAndClosedClass.JavaType = "byte";
            class_JavaAndClosedClass.ClosedType = "java.lang.Byte";
            class_JavaAndClosedClass.SimplClosedType = "Byte";
            class_JavaAndClosedClasses.Add(class_JavaAndClosedClass);
            class_JavaAndClosedClass = new Class_JavaAndClosedClass();
            class_JavaAndClosedClass.JavaType = "short";
            class_JavaAndClosedClass.ClosedType = "java.lang.Short";
            class_JavaAndClosedClass.SimplClosedType = "Short";
            class_JavaAndClosedClasses.Add(class_JavaAndClosedClass);
            class_JavaAndClosedClass = new Class_JavaAndClosedClass();
            class_JavaAndClosedClass.JavaType = "int";
            class_JavaAndClosedClass.ClosedType = "java.lang.Integer";
            class_JavaAndClosedClass.SimplClosedType = "Integer";
            class_JavaAndClosedClasses.Add(class_JavaAndClosedClass);
            class_JavaAndClosedClass = new Class_JavaAndClosedClass();
            class_JavaAndClosedClass.JavaType = "long";
            class_JavaAndClosedClass.ClosedType = "java.lang.Long";
            class_JavaAndClosedClass.SimplClosedType = "Long";
            class_JavaAndClosedClasses.Add(class_JavaAndClosedClass);
            class_JavaAndClosedClass = new Class_JavaAndClosedClass();
            class_JavaAndClosedClass.JavaType = "float";
            class_JavaAndClosedClass.ClosedType = "java.lang.Float";
            class_JavaAndClosedClass.SimplClosedType = "Float";
            class_JavaAndClosedClasses.Add(class_JavaAndClosedClass);
            class_JavaAndClosedClass = new Class_JavaAndClosedClass();
            class_JavaAndClosedClass.JavaType = "double";
            class_JavaAndClosedClass.ClosedType = "java.lang.Double";
            class_JavaAndClosedClass.SimplClosedType = "double";
            class_JavaAndClosedClasses.Add(class_JavaAndClosedClass);
            class_JavaAndClosedClass = new Class_JavaAndClosedClass();
            class_JavaAndClosedClass.JavaType = "char";
            class_JavaAndClosedClass.ClosedType = "java.lang.Character";
            class_JavaAndClosedClass.SimplClosedType = "Char";
            class_JavaAndClosedClasses.Add(class_JavaAndClosedClass);
            class_JavaAndClosedClass = new Class_JavaAndClosedClass();
            class_JavaAndClosedClass.JavaType = "boolean";
            class_JavaAndClosedClass.ClosedType = "java.lang.Boolean";
            class_JavaAndClosedClass.SimplClosedType = "Boolean";
            class_JavaAndClosedClasses.Add(class_JavaAndClosedClass);
            class_JavaAndClosedClass = new Class_JavaAndClosedClass();
            class_JavaAndClosedClass.JavaType = "String";
            class_JavaAndClosedClass.ClosedType = "java.lang.String";
            class_JavaAndClosedClass.SimplClosedType = "String";
            class_JavaAndClosedClasses.Add(class_JavaAndClosedClass);
            class_JavaAndClosedClass = new Class_JavaAndClosedClass();
            class_JavaAndClosedClass.JavaType = "Date";
            class_JavaAndClosedClass.ClosedType = "java.sql.Timestamp";
            class_JavaAndClosedClass.SimplClosedType = "Date";
            class_JavaAndClosedClasses.Add(class_JavaAndClosedClass);
            class_JavaAndClosedClass = new Class_JavaAndClosedClass();
            class_JavaAndClosedClass.JavaType = "Date";
            class_JavaAndClosedClass.ClosedType = "java.sql.Date";
            class_JavaAndClosedClass.SimplClosedType = "Date";
            class_JavaAndClosedClasses.Add(class_JavaAndClosedClass);
            class_JavaAndClosedClass = new Class_JavaAndClosedClass();
            class_JavaAndClosedClass.JavaType = "BigDecimal";
            class_JavaAndClosedClass.ClosedType = "java.math.BigDecimal";
            class_JavaAndClosedClass.SimplClosedType = "BigDecimal";
            class_JavaAndClosedClasses.Add(class_JavaAndClosedClass);
            #endregion
        }
        private static List<Class_JavaAndJdbc> class_JavaAndJdbcs;
        private static List<Class_JavaAndClosedClass> class_JavaAndClosedClasses;
        /// <summary>
        /// 根据封装类得到Java类
        /// </summary>
        /// <param name="ClosedType">封装类名称</param>
        /// <returns></returns>
        public static string GetJavaTypeByClosedType(string ClosedType)
        {
            if ((class_JavaAndClosedClasses == null) || (class_JavaAndClosedClasses.Count == 0))
                return ClosedType;
            int Index = class_JavaAndClosedClasses.FindIndex(a => a.JavaType.Equals(ClosedType));
            if (Index > -1)
                return string.Format("{0}", class_JavaAndClosedClasses[Index].JavaType);
            else
                return ClosedType;
        }
        /// <summary>
        /// 通过JavaType类型，得到JDBC类型
        /// </summary>
        /// <param name="JavaType"></param>
        /// <returns></returns>
        public static string GetJdbcType(string JavaType)
        {
            if ((class_JavaAndJdbcs == null) || (class_JavaAndJdbcs.Count == 0))
                return null;
            int Index = class_JavaAndJdbcs.FindIndex(a => a.JavaType.Equals(JavaType));
            if (Index > -1)
                return string.Format("{0}", class_JavaAndJdbcs[Index].JdbcType);
            else
                return null;
        }
        /// <summary>
        /// 通过JDBC类型，得到JavaType类型
        /// </summary>
        /// <param name="JdbcType"></param>
        /// <returns></returns>
        public static string GetJavaType(string JdbcType)
        {
            if ((class_JavaAndJdbcs == null) || (class_JavaAndJdbcs.Count == 0))
                return null;
            int Index = class_JavaAndJdbcs.FindIndex(a => a.JdbcType.Equals(JdbcType));
            if (Index > -1)
                return class_JavaAndJdbcs[Index].JavaType;
            else
                return null;
        }

        public static List<string> FindFile(string MyAllPath)
        {
            List<string> vs = new List<string>();
            DirectoryInfo Dir = new DirectoryInfo(MyAllPath);
            DirectoryInfo[] DirSub = Dir.GetDirectories();
            if (DirSub.Length <= 0)
            {
                foreach (FileInfo f in Dir.GetFiles("*.xml", SearchOption.TopDirectoryOnly)) //查找文件
                {
                    vs.Add(Dir + @"\" + f.ToString());
                }
            }
            int Counter = 1;
            foreach (DirectoryInfo d in DirSub)//查找子目录 
            {
                FindFile(Dir + @"\" + d.ToString());
                vs.Add(Dir + @"\" + d.ToString());
                if (Counter == 1)
                {
                    foreach (FileInfo f in Dir.GetFiles("*.xml", SearchOption.TopDirectoryOnly)) //查找文件
                    {
                        vs.Add(Dir + @"\" + f.ToString());
                    }
                    Counter++;
                }
            }
            return vs;
        }
        public static string getKeyId(string Sign)
        {
            return string.Format("{0}{1}{2}", Sign, System.DateTime.Now.ToString("yyyyMMddHHmmss"), getRandomInt().ToString());
        }
        private static int _getRandomInt(int Max, int Min)
        {
            if (Min > Max)
            {
                Max = 999;
                Min = 100;
            }
            Random ran = new Random();
            return ran.Next(Min, Max);
        }
        public static int getRandomInt()
        {
            return _getRandomInt(100, 300);
        }
        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="Content"></param>
        /// <returns></returns>
        public static string GetFirstCodeUpper(string Content)
        {
            if ((Content == null) || Content.Length == 0)
                return null;
            return Content.Substring(0, 1).ToUpper() + Content.Substring(1);
        }
        /// <summary>
        /// 转成驼峰命名
        /// </summary>
        /// <param name="Content"></param>
        /// <returns></returns>
        public static string GetFirstCodeLow(string Content)
        {
            if ((Content == null) || Content.Length == 0)
                return null;
            string[] vs = Content.Split('_');
            if (vs.Length > 1)
            {
                Content = null;
                foreach (string row in vs)
                {
                    Content += row.Substring(0, 1).ToUpper() + row.Substring(1);
                }
            }
            return Content.Substring(0, 1).ToLower() + Content.Substring(1);
        }
    }
    /// <summary>
    /// 序列化到JSON类
    /// </summary>
    public class JsonTools
    {
        /// <summary>
        /// 将对象保存到JSON文件
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="fileName"></param>
        /// <param name="obj"></param>
        /// <returns>保存好的JSON全路径</returns>
        public static string ObjectToJsonFile(string directory, string fileName, object obj)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(directory);
            if (!directoryInfo.Exists)
                directoryInfo.Create();
            fileName = string.Format("{0}\\{1}.json", directory, fileName);
            try
            {
                if (!File.Exists(fileName))  // 判断是否已有相同文件 
                {
                    FileStream fs1 = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
                    fs1.Close();
                }
                File.WriteAllText(fileName, ObjectToJson(obj));
                return fileName;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        /// <summary>
        /// 从一个对象信息生成Json串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectToJson(object obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream stream = new MemoryStream();
            serializer.WriteObject(stream, obj);
            byte[] dataBytes = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(dataBytes, 0, (int)stream.Length);
            return Encoding.UTF8.GetString(dataBytes);
        }
        /// <summary>
        /// 从一个Json串生成对象信息
        /// </summary>
        /// <param name="jsonString"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object JsonToObject(string jsonString, object obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream mStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            return serializer.ReadObject(mStream);
        }
    }
    /// <summary>
    /// 序列化XML基础类
    /// </summary>
    public class XmlUtil
    {
        /// <summary>
        /// 序列化到XML
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="directory"></param>
        /// <param name="fileName"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool ObjectSerialXml<T>(string directory, string fileName, T t)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(directory);
            if (!directoryInfo.Exists)
                directoryInfo.Create();

            try
            {
                using (FileStream fs = new FileStream(string.Format("{0}\\{1}.xml", directory, fileName), FileMode.Create))
                {

                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(fs, t);

                    fs.Dispose();

                    return true;
                }

            }
            catch (Exception e)
            {

                throw e;
            }
        }

        /// <summary>
        /// 从xml序列中反序列化
        /// </summary>
        /// <param name="XmlFile"></param>
        /// <returns></returns>
        public T XmlSerialObject<T>(string fileFullName) where T : class
        {

            if (!System.IO.File.Exists(fileFullName))
                throw new Exception("文件不存在");

            T t = null;
            try
            {
                //Xml格式反序列化
                using (Stream stream = new FileStream(fileFullName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    XmlSerializer formatter = new XmlSerializer(typeof(T));
                    t = (T)formatter.Deserialize(stream);
                    stream.Dispose();
                }

                return t;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

    }

}

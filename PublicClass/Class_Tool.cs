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
        //public string GetDataBaseContent(DataSet ExcelDataSet)
        //{
        //    string AllPathFieldName = null;

        //    #region 外来参数
        //    string TitleFileName = string.Format(@"数据库说明书{0}", System.DateTime.Now.ToString("yyyy年MM月dd日"));
        //    #endregion

        //    #region 参数
        //    string ExcelFileName = string.Format(@"数据库说明书{0}.xlsx", System.DateTime.Now.ToString("yyyy年MM月dd日"));
        //    #endregion

        //    ArrayList FieldTitleArray = new ArrayList();
        //    ArrayList SheetNameArray = new ArrayList();
        //    ArrayList SheetTitleArray = new ArrayList();
        //    ArrayList AveFieldNameArray = new ArrayList();

        //    SheetTitleArray.Add(string.Format(@"{0}财务费用信息明细", System.DateTime.Now.ToString("yyyy年MM月dd日")));
        //    SheetNameArray.Add("财务费用信息明细");

        //    _GiveYouExcel(SheetNameArray, SheetTitleArray, false, ExcelFileName, FieldTitleArray, null, false, null, true, null, ExcelDataSet);
        //    return AllPathFieldName;
        //}
        ///// <summary>
        ///// 财务费用信息ToExcel
        ///// </summary>
        //public void AcountCashTempListToExcel()
        //{
        //    DataSet ExcelDataSet = new DataSet();
        //    //DateTime VouDate = Convert.ToDateTime(VouDateStr);
        //    //string VouDateStr = this._GetRequest("VouDate");
        //    //string OperName = _ReadCookid("PersName");
        //    //string CorpName = this._GetRequest("CorpName");
        //    //string DepartName = this._GetRequest("DepartName");
        //    //string InvoiceNumber = this._GetRequest("InvoiceNumber");
        //    DateTime VouDate = System.DateTime.Now;
        //    string VouDateStr = null;
        //    string OperName = null;
        //    string CorpName = null;
        //    string DepartName = null;
        //    string InvoiceNumber = null;
        //    #region 查询条件
        //    StringBuilder WhereKey = new StringBuilder();
        //    WhereKey.AppendFormat(@" Where CONVERT(varchar(7),AC.[WorkTime],120) = CONVERT(varchar(7),'{0}',120)", VouDateStr);
        //    if (CorpName.Length > 0)
        //        WhereKey.AppendFormat(@" And isnull(co.CorpName,AC.CorpName) like '%{0}%'", CorpName);
        //    if (OperName.Length > 0)
        //        WhereKey.AppendFormat(@" And AC.OperName = '{0}'", OperName);
        //    if (DepartName.Length > 0)
        //        WhereKey.AppendFormat(@" And Dep.DepartName like '{0}%'", DepartName);
        //    if (InvoiceNumber.Length > 0)
        //        WhereKey.AppendFormat(@" And AC.InvoiceNumber like '{0}%'", InvoiceNumber);
        //    #endregion

        //    #region 外来参数
        //    string TitleFileName = string.Format(@"{0}财务费用信息", VouDate.ToString("yyyy年MM月"));
        //    #endregion

        //    #region 参数
        //    bool IsTotal = true;
        //    bool IsAverage = false;
        //    string ExcelFileName = string.Format(@"{0}财务费用信息.xlsx", VouDate.ToString("yyyy年MM月"));
        //    #endregion

        //    #region Sql语句集
        //    ArrayList SqlArray = new ArrayList();
        //    ArrayList FieldTitleArray = new ArrayList();
        //    ArrayList SheetNameArray = new ArrayList();
        //    ArrayList SheetTitleArray = new ArrayList();
        //    ArrayList AveFieldNameArray = new ArrayList();

        //    SqlArray.Add(string.Format(@"
        //    Select isnull(co.CorpName,AC.CorpName) as [企业名称]
        //            ,Dep.[DepartName] as [所属部门]
        //            ,AC.[OperName] as [核算员]
        //            ,AC.InvoiceNumber as [发票号]  
        //            ,AC.InvoiceType as [发票类型] 
        //            ,Case When Convert(varchar(10),AC.[BillingTime],120) = '1900-01-01' then '' else Convert(varchar(10),AC.[BillingTime],120) end as [开票日期]
        //            ,Case When Convert(varchar(10),AC.[ArrivalTime],120) = '1900-01-01' then '' else Convert(varchar(10),AC.[ArrivalTime],120) end as [到款日期]
        //            ,AC.[NoTax] as [无税金额]
        //            ,AC.[TaxSix] as [6%税金额]
        //            ,AC.[TaxFive] as [5%税金额]
        //            ,AC.[TaxEleven] as [11%税金额]
        //            ,AC.[TaxSeventeen] as [17%税金额]
        //            ,AC.[NoTax] + AC.[TaxSix] + AC.[TaxFive] + AC.[TaxSeventeen]+AC.TaxEleven as [税合计]
        //            ,AC.[NoTax] + AC.[TaxSix] + AC.[TaxFive] + AC.[TaxSeventeen]+AC.TaxEleven - AC.[ReceiveAccount] as [应收金额]
        //            ,AC.[ReceiveAccount] as [实收金额]
        //            ,AC.[YSalary] - AC.[SSalary] as [工资]
        //            ,AC.[YTax] - AC.[STax] as [个税]
        //            ,AC.[YSoc] - AC.[SSoc] as [社保]
        //            ,AC.[YGjj] - AC.[SGjj] as [公积金]
        //            ,AC.[YAdminCash] - AC.[SAdminCash] as [行政费用]
        //            ,AC.[YOtherCash] - AC.[SOtherCash] as [其它费用]
        //            ,AC.[YSalary] + AC.[YTax] + AC.[YSoc] + AC.[YGjj] + AC.[YAdminCash] + AC.[YOtherCash] +AC.[YTaxCash]+AC.[YManageCash] - AC.[SSalary] - AC.[STax] - AC.[SSoc] - AC.[SGjj] - AC.[SAdminCash] - AC.[SOtherCash] as [差异小计]
        //            ,AC.[YSalary] as [应收工资]
        //            ,AC.[YTax] as [应收个税]
        //            ,AC.[YSoc] as [应收社保]
        //            ,AC.[YGjj] as [应收公积金]
        //            ,AC.[YAdminCash] as [应收行政费用]
        //            ,AC.[YOtherCash] as [应收其它费用]
        //            ,AC.[YTaxCash]  as [税金]
        //            ,AC.[YManageCash] as [管理费]
        //            ,AC.[YSalary] + AC.[YTax] + AC.[YSoc] + AC.[YGjj] + AC.[YAdminCash] + AC.[YOtherCash]+AC.[YTaxCash]+AC.[YManageCash] as [应收小计]
        //            ,AC.[SSalary] as [实付工资]
        //            ,AC.[STax] as [实付个税]
        //            ,AC.[SSoc] as [实付社保]
        //            ,AC.[SGjj] as [实付公积金]
        //            ,AC.[SAdminCash] as [实付行政费用]
        //            ,AC.[SOtherCash] as [实付其它费用]
        //            ,AC.[SSalary] + AC.[STax] + AC.[SSoc] + AC.[SGjj] + AC.[SAdminCash] + AC.[SOtherCash]  as [实付小计]                   
        //            ,Convert(varchar(19),AC.[WorkTime],120) as [数据生成时间]                                      
        //            ,AC.[Remark] as [备注]
        //    From Vou_AcountCashTemp As AC Left join Inf_Department as Dep on Dep.ID = AC.DepartID 
        //    left join Inf_CorpFinance as co on co.ID = AC.CorpID  {0}
        //    Order by AC.[WorkTime] Desc
        //    ", WhereKey.ToString()));
        //    SheetTitleArray.Add(string.Format(@"{0}财务费用信息明细", VouDate.ToString("yyyy年MM月")));
        //    SheetNameArray.Add("财务费用信息明细");


        //    string SumFieldName = @"
        //    ,无税金额,6%税金额,5%税金额,11%税金额,17%税金额,税合计,应收金额,实收金额,工资,个税,社保,公积金,行政费用,其它费用,差异小计,应收工资,应收个税,应收社保,应收公积金,应收行政费用,应收其它费用,税金,管理费,应收小计,实付工资,实付个税,实付社保,实付公积金,实付行政费用,实付其它费用,实付小计,";
        //    string LeftFieldName = ",备注,";
        //    #endregion

        //    _GiveYouExcel(SheetNameArray, SheetTitleArray, IsTotal, ExcelFileName, FieldTitleArray, IsTotal ? SumFieldName : null, IsAverage, IsAverage ? AveFieldNameArray : null, true, LeftFieldName, ExcelDataSet);
        //}

        private DataSet GetChangeExcel(ArrayList SqlArray, ArrayList TableNameArray)
        {
            DataSet ReturnSet = new DataSet();
            //JsDB = new myDb();
            //JsDB.Open();
            //for (int Index = 0; Index < SqlArray.Count; Index++)
            //{
            //    DataTable ChangeTable = new DataTable();
            //    ChangeTable = JsDB.GetDataSet(SqlArray[Index].ToString()).Tables[0].Copy();
            //    ChangeTable.TableName = TableNameArray[Index].ToString();
            //    ReturnSet.Tables.Add(ChangeTable);
            //}
            //JsDB.Close(); JsDB.Dispose();
            return ReturnSet;
        }

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
                                NowSheet.Cells[TitleScriptRow, (DataEndCol > 12 ? 12 : DataEndCol) - 2].Value = string.Format("导出时间：{0}", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
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
    public class Class_Tool
    {
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
            class_JavaAndJdbc = new Class_JavaAndJdbc();
            class_JavaAndJdbc.JdbcType = "DATE";
            class_JavaAndJdbc.JavaType = "java.sql.Date";
            class_JavaAndJdbcs.Add(class_JavaAndJdbc);
            class_JavaAndJdbc = new Class_JavaAndJdbc();
            class_JavaAndJdbc.JdbcType = "TIME";
            class_JavaAndJdbc.JavaType = "java.sql.Time";
            class_JavaAndJdbcs.Add(class_JavaAndJdbc);
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
            class_JavaAndClosedClasses.Add(class_JavaAndClosedClass);
            class_JavaAndClosedClass = new Class_JavaAndClosedClass();
            class_JavaAndClosedClass.JavaType = "short";
            class_JavaAndClosedClass.ClosedType = "java.lang.Short";
            class_JavaAndClosedClasses.Add(class_JavaAndClosedClass);
            class_JavaAndClosedClass = new Class_JavaAndClosedClass();
            class_JavaAndClosedClass.JavaType = "int";
            class_JavaAndClosedClass.ClosedType = "java.lang.Integer";
            class_JavaAndClosedClasses.Add(class_JavaAndClosedClass);
            class_JavaAndClosedClass = new Class_JavaAndClosedClass();
            class_JavaAndClosedClass.JavaType = "long";
            class_JavaAndClosedClass.ClosedType = "java.lang.Long";
            class_JavaAndClosedClasses.Add(class_JavaAndClosedClass);
            class_JavaAndClosedClass = new Class_JavaAndClosedClass();
            class_JavaAndClosedClass.JavaType = "float";
            class_JavaAndClosedClass.ClosedType = "java.lang.Float";
            class_JavaAndClosedClasses.Add(class_JavaAndClosedClass);
            class_JavaAndClosedClass = new Class_JavaAndClosedClass();
            class_JavaAndClosedClass.JavaType = "double";
            class_JavaAndClosedClass.ClosedType = "java.lang.Double";
            class_JavaAndClosedClasses.Add(class_JavaAndClosedClass);
            class_JavaAndClosedClass = new Class_JavaAndClosedClass();
            class_JavaAndClosedClass.JavaType = "char";
            class_JavaAndClosedClass.ClosedType = "java.lang.Character";
            class_JavaAndClosedClasses.Add(class_JavaAndClosedClass);
            class_JavaAndClosedClass = new Class_JavaAndClosedClass();
            class_JavaAndClosedClass.JavaType = "boolean";
            class_JavaAndClosedClass.ClosedType = "java.lang.Boolean";
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

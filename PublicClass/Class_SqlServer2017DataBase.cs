using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static MDIDemo.PublicClass.Class_SelectAllModel;

namespace MDIDemo.PublicClass
{
    public class Class_SqlServer2017DataBase : Class_InterFaceDataBase
    {
        public Class_SqlServer2017DataBase()
        {
            IniFieldTypeChange();
        }
        public Class_SqlServer2017DataBase(string ip, string dataBaseName, string userName, string passWord)
        {
            if (ip != null)
            {
                this.Ip = ip;
                this.DataBaseName = dataBaseName;
                this.UserName = userName;
                this.PassWord = passWord;
            }
            else
            {
                Class_PublicMethod class_PublicMethod = new Class_PublicMethod();
                Class_DataBaseConDefault class_DataBaseConDefault = new Class_DataBaseConDefault();
                class_DataBaseConDefault = class_PublicMethod.FromXmlToSelectObject<Class_DataBaseConDefault>("DataBaseDefaultValues");

                if (class_DataBaseConDefault == null)
                {
                    this.Ip = "221.234.36.229";
                    this.DataBaseName = "QD_MANAGER";
                    this.UserName = "sa";
                    this.PassWord = "qdsasasa";
                }
                else
                {
                    if (class_DataBaseConDefault.databaseType == "SqlServer 2017")
                    {
                        this.Ip = class_DataBaseConDefault.dataSourceUrl;
                        this.DataBaseName = class_DataBaseConDefault.dataBaseName;
                        this.UserName = class_DataBaseConDefault.dataSourceUserName;
                        this.PassWord = class_DataBaseConDefault.dataSourcePassWord;
                    }
                }
            }
            IniFieldTypeChange();
            ConnectString = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}"
                , this.Ip
                , this.DataBaseName
                , this.UserName
                , this.PassWord);
            mydb = new Mydb(ConnectString);
        }
        private Mydb mydb;
        private string OperateType;
        private string ConnectString;
        public string Ip { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string DataBaseName { get; set; }
        private object class_AllModel;
        private List<Class_SqlServerAndJava> class_SqlServerAndJavas;

        private void IniFieldTypeChange()
        {
            class_SqlServerAndJavas = new List<Class_SqlServerAndJava>();
            Class_SqlServerAndJava class_SqlServerAndJava = new Class_SqlServerAndJava();
            #region
            class_SqlServerAndJava.DataBaseFieldType = "bigint";
            class_SqlServerAndJava.JavaType = "java.lang.long";
            class_SqlServerAndJavas.Add(class_SqlServerAndJava);
            class_SqlServerAndJava = new Class_SqlServerAndJava();
            class_SqlServerAndJava.DataBaseFieldType = "timestamp";
            class_SqlServerAndJava.JavaType = "java.lang.byte[]";
            class_SqlServerAndJavas.Add(class_SqlServerAndJava);
            class_SqlServerAndJava = new Class_SqlServerAndJava();
            class_SqlServerAndJava.DataBaseFieldType = "binary";
            class_SqlServerAndJava.JavaType = "java.lang.byte[]";
            class_SqlServerAndJavas.Add(class_SqlServerAndJava);
            class_SqlServerAndJava = new Class_SqlServerAndJava();
            class_SqlServerAndJava.DataBaseFieldType = "bit";
            class_SqlServerAndJava.JavaType = "java.lang.boolean";
            class_SqlServerAndJavas.Add(class_SqlServerAndJava);
            class_SqlServerAndJava = new Class_SqlServerAndJava();
            class_SqlServerAndJava.DataBaseFieldType = "char";
            class_SqlServerAndJava.JavaType = "java.lang.String";
            class_SqlServerAndJavas.Add(class_SqlServerAndJava);
            class_SqlServerAndJava = new Class_SqlServerAndJava();
            class_SqlServerAndJava.DataBaseFieldType = "decimal";
            class_SqlServerAndJava.JavaType = "java.math.BigDecimal";
            class_SqlServerAndJavas.Add(class_SqlServerAndJava);
            class_SqlServerAndJava = new Class_SqlServerAndJava();
            class_SqlServerAndJava.DataBaseFieldType = "money";
            class_SqlServerAndJava.JavaType = "java.math.BigDecimal";
            class_SqlServerAndJavas.Add(class_SqlServerAndJava);
            class_SqlServerAndJava = new Class_SqlServerAndJava();
            class_SqlServerAndJava.DataBaseFieldType = "smallmoney";
            class_SqlServerAndJava.JavaType = "java.math.BigDecimal";
            class_SqlServerAndJavas.Add(class_SqlServerAndJava);
            class_SqlServerAndJava = new Class_SqlServerAndJava();
            class_SqlServerAndJava.DataBaseFieldType = "float";
            class_SqlServerAndJava.JavaType = "java.lang.double";
            class_SqlServerAndJavas.Add(class_SqlServerAndJava);
            class_SqlServerAndJava = new Class_SqlServerAndJava();
            class_SqlServerAndJava.DataBaseFieldType = "int";
            class_SqlServerAndJava.JavaType = "java.lang.int";
            class_SqlServerAndJavas.Add(class_SqlServerAndJava);
            class_SqlServerAndJava = new Class_SqlServerAndJava();
            class_SqlServerAndJava.DataBaseFieldType = "image";
            class_SqlServerAndJava.JavaType = "java.lang.byte[]";
            class_SqlServerAndJavas.Add(class_SqlServerAndJava);
            class_SqlServerAndJava = new Class_SqlServerAndJava();
            class_SqlServerAndJava.DataBaseFieldType = "varbinary";
            class_SqlServerAndJava.JavaType = "java.lang.byte[]";
            class_SqlServerAndJavas.Add(class_SqlServerAndJava);
            class_SqlServerAndJava = new Class_SqlServerAndJava();
            class_SqlServerAndJava.DataBaseFieldType = "varchar";
            class_SqlServerAndJava.JavaType = "java.lang.String";
            class_SqlServerAndJavas.Add(class_SqlServerAndJava);
            class_SqlServerAndJava = new Class_SqlServerAndJava();
            class_SqlServerAndJava.DataBaseFieldType = "text";
            class_SqlServerAndJava.JavaType = "java.lang.String";
            class_SqlServerAndJavas.Add(class_SqlServerAndJava);
            class_SqlServerAndJava = new Class_SqlServerAndJava();
            class_SqlServerAndJava.DataBaseFieldType = "nchar";
            class_SqlServerAndJava.JavaType = "java.lang.String";
            class_SqlServerAndJavas.Add(class_SqlServerAndJava);
            class_SqlServerAndJava = new Class_SqlServerAndJava();
            class_SqlServerAndJava.DataBaseFieldType = "nvarchar";
            class_SqlServerAndJava.JavaType = "java.lang.String";
            class_SqlServerAndJavas.Add(class_SqlServerAndJava);
            class_SqlServerAndJava = new Class_SqlServerAndJava();
            class_SqlServerAndJava.DataBaseFieldType = "nvarchar";
            class_SqlServerAndJava.JavaType = "java.lang.String";
            class_SqlServerAndJavas.Add(class_SqlServerAndJava);
            class_SqlServerAndJava = new Class_SqlServerAndJava();
            class_SqlServerAndJava.DataBaseFieldType = "ntext";
            class_SqlServerAndJava.JavaType = "java.lang.String";
            class_SqlServerAndJavas.Add(class_SqlServerAndJava);
            class_SqlServerAndJava = new Class_SqlServerAndJava();
            class_SqlServerAndJava.DataBaseFieldType = "numeric";
            class_SqlServerAndJava.JavaType = "java.math.BigDecimal";
            class_SqlServerAndJavas.Add(class_SqlServerAndJava);
            class_SqlServerAndJava = new Class_SqlServerAndJava();
            class_SqlServerAndJava.DataBaseFieldType = "real";
            class_SqlServerAndJava.JavaType = "java.lang.float";
            class_SqlServerAndJavas.Add(class_SqlServerAndJava);
            class_SqlServerAndJava = new Class_SqlServerAndJava();
            class_SqlServerAndJava.DataBaseFieldType = "smallint";
            class_SqlServerAndJava.JavaType = "java.lang.short";
            class_SqlServerAndJavas.Add(class_SqlServerAndJava);
            class_SqlServerAndJava = new Class_SqlServerAndJava();
            class_SqlServerAndJava.DataBaseFieldType = "datetime";
            class_SqlServerAndJava.JavaType = "java.sql.Timestamp";
            class_SqlServerAndJavas.Add(class_SqlServerAndJava);
            class_SqlServerAndJava = new Class_SqlServerAndJava();
            class_SqlServerAndJava.DataBaseFieldType = "smalldatetime";
            class_SqlServerAndJava.JavaType = "java.sql.Timestamp";
            class_SqlServerAndJavas.Add(class_SqlServerAndJava);
            class_SqlServerAndJava = new Class_SqlServerAndJava();
            class_SqlServerAndJava.DataBaseFieldType = "varbinary";
            class_SqlServerAndJava.JavaType = "java.lang.byte[]";
            class_SqlServerAndJavas.Add(class_SqlServerAndJava);
            class_SqlServerAndJava = new Class_SqlServerAndJava();
            class_SqlServerAndJava.DataBaseFieldType = "udt";
            class_SqlServerAndJava.JavaType = "java.lang.byte[]";
            class_SqlServerAndJavas.Add(class_SqlServerAndJava);
            class_SqlServerAndJava = new Class_SqlServerAndJava();
            class_SqlServerAndJava.DataBaseFieldType = "varchar";
            class_SqlServerAndJava.JavaType = "java.lang.String";
            class_SqlServerAndJavas.Add(class_SqlServerAndJava);
            class_SqlServerAndJava = new Class_SqlServerAndJava();
            class_SqlServerAndJava.DataBaseFieldType = "tinyint";
            class_SqlServerAndJava.JavaType = "java.lang.short";
            class_SqlServerAndJavas.Add(class_SqlServerAndJava);
            class_SqlServerAndJava = new Class_SqlServerAndJava();
            class_SqlServerAndJava.DataBaseFieldType = "uniqueidentifier";
            class_SqlServerAndJava.JavaType = "java.lang.String";
            class_SqlServerAndJavas.Add(class_SqlServerAndJava);
            class_SqlServerAndJava = new Class_SqlServerAndJava();
            class_SqlServerAndJava.DataBaseFieldType = "time";
            class_SqlServerAndJava.JavaType = "java.sql.Time (1)";
            class_SqlServerAndJavas.Add(class_SqlServerAndJava);
            class_SqlServerAndJava = new Class_SqlServerAndJava();
            class_SqlServerAndJava.DataBaseFieldType = "date";
            class_SqlServerAndJava.JavaType = "java.sql.Date";
            class_SqlServerAndJavas.Add(class_SqlServerAndJava);
            #endregion
        }

        private DataTable _GetTableStruct(string TableName)
        {
            string Sql = string.Format(@"select FieldName = a.name 
            ,FieldRemark = isnull(g.[value], '')
            ,FieldType = b.name
            ,FieldLength = columnproperty(a.id, a.name, 'PRECISION')
            ,FieldDefaultValue = isnull(e.text, '') 
            ,FieldIsNull = case when a.isnullable = 1 then Convert(bit,1) else Convert(bit,0) end
            ,FieldIsKey = case when exists ( select  1
            from sysobjects
	            where   xtype = 'PK' and name in (select    name
	            from sysindexes
	            where indid in (select    indid
	            from sysindexkeys
	            where id = a.id and colid = a.colid)) 
            ) then Convert(bit,1) else Convert(bit,0) end
            ,FieldIsAutoAdd = case when columnproperty(a.id, a.name, 'IsIdentity') = 1 then Convert(bit,1) else Convert(bit,0) end
            from syscolumns a
            left join systypes b on a.xusertype = b.xusertype
            inner join sysobjects d on a.id = d.id and d.xtype in('U','V') and d.name <> 'dtproperties'
            left join syscomments e on a.cdefault = e.id
            left join sys.extended_properties g on a.id = g.major_id and a.colid = g.minor_id
            left join sys.extended_properties f on d.id = f.major_id and f.minor_id = 0
            where d.name='{0}'
            order by a.id,a.colorder", TableName);
            return mydb.GetDataTable(Sql);
        }
        private DataTable _GetMainTableStruct(string TableName, int PageSelectIndex)
        {
            DataTable dataTable = new DataTable();
            try
            {
                if (TableName != null)
                {
                    dataTable = _GetTableStruct(TableName);

                    Class_SelectAllModel mySelect = new Class_SelectAllModel();
                    switch (OperateType)
                    {
                        case "select":
                            mySelect = this.class_AllModel as Class_SelectAllModel;
                            #region 加入自定义Select列
                            DataColumn SelectSelect = new DataColumn("SelectSelect", typeof(bool));
                            DataColumn ParaName = new DataColumn("ParaName", typeof(string));
                            DataColumn MaxLegth = new DataColumn("MaxLegth", typeof(Int32));
                            DataColumn CaseWhen = new DataColumn("CaseWhen", typeof(string));
                            DataColumn ReturnType = new DataColumn("ReturnType", typeof(string));
                            DataColumn TrimSign = new DataColumn("TrimSign", typeof(bool));
                            DataColumn FunctionName = new DataColumn("FunctionName", typeof(string));

                            DataColumn WhereSelect = new DataColumn("WhereSelect", typeof(bool));
                            DataColumn WhereType = new DataColumn("WhereType", typeof(string));
                            DataColumn LogType = new DataColumn("LogType", typeof(string));
                            DataColumn WhereValue = new DataColumn("WhereValue", typeof(string));
                            DataColumn WhereTrim = new DataColumn("WhereTrim", typeof(bool));
                            DataColumn WhereIsNull = new DataColumn("WhereIsNull", typeof(bool));

                            DataColumn OrderSelect = new DataColumn("OrderSelect", typeof(bool));
                            DataColumn SortType = new DataColumn("SortType", typeof(string));
                            DataColumn SortNo = new DataColumn("SortNo", typeof(Int32));

                            DataColumn GroupSelect = new DataColumn("GroupSelect", typeof(bool));

                            DataColumn HavingSelect = new DataColumn("HavingSelect", typeof(bool));
                            DataColumn HavingFunction = new DataColumn("HavingFunction", typeof(string));
                            DataColumn HavingCondition = new DataColumn("HavingCondition", typeof(string));
                            DataColumn HavingValue = new DataColumn("HavingValue", typeof(string));

                            SelectSelect.DefaultValue = false;
                            TrimSign.DefaultValue = true;
                            WhereSelect.DefaultValue = false;
                            WhereTrim.DefaultValue = true;
                            WhereIsNull.DefaultValue = true;
                            OrderSelect.DefaultValue = false;
                            GroupSelect.DefaultValue = false;
                            HavingSelect.DefaultValue = false;

                            dataTable.Columns.Add(SelectSelect);
                            dataTable.Columns.Add(ParaName);
                            dataTable.Columns.Add(MaxLegth);
                            dataTable.Columns.Add(CaseWhen);
                            dataTable.Columns.Add(ReturnType);
                            dataTable.Columns.Add(TrimSign);
                            dataTable.Columns.Add(FunctionName);

                            dataTable.Columns.Add(WhereSelect);
                            dataTable.Columns.Add(WhereType);
                            dataTable.Columns.Add(LogType);
                            dataTable.Columns.Add(WhereValue);
                            dataTable.Columns.Add(WhereTrim);
                            dataTable.Columns.Add(WhereIsNull);

                            dataTable.Columns.Add(OrderSelect);
                            dataTable.Columns.Add(SortType);
                            dataTable.Columns.Add(SortNo);

                            dataTable.Columns.Add(GroupSelect);

                            dataTable.Columns.Add(HavingSelect);
                            dataTable.Columns.Add(HavingFunction);
                            dataTable.Columns.Add(HavingCondition);
                            dataTable.Columns.Add(HavingValue);
                            #endregion
                            break;
                        case "insert":
                            //mySelect = this.class_AllModel as Class_SelectAllModel;
                            break;
                        case "update":
                            //mySelect = this.class_AllModel as Class_SelectAllModel;
                            break;
                        case "delete":
                            //mySelect = this.class_AllModel as Class_SelectAllModel;
                            break;
                        default:
                            break;
                    }
                    int Counter = 1;
                    Class_Sub class_Mains = new Class_Sub();
                    switch (PageSelectIndex)
                    {
                        case 0:
                            class_Mains = mySelect.class_Main as Class_Sub;
                            break;
                        case 1:
                            class_Mains = mySelect.class_Subs;
                            break;
                        case 2:
                            class_Mains = mySelect.class_SubSubs;
                            break;
                        default:
                            break;
                    }
                    foreach (DataRow row in dataTable.Rows)
                    {
                        string myFieldName = row["FieldName"].ToString();
                        bool IsDefault = true;
                        row.BeginEdit();
                        if (class_AllModel != null)
                        {
                            switch (OperateType)
                            {
                                case "select":
                                    {
                                        if (mySelect.class_Main.class_Fields.Count > 0)
                                        {
                                            int FindIndex = class_Mains.class_Fields.FindIndex(a => a.FieldName.Equals(myFieldName));
                                            if (FindIndex > -1)
                                            {
                                                row["SelectSelect"] = class_Mains.class_Fields[FindIndex].SelectSelect;
                                                row["ParaName"] = class_Mains.class_Fields[FindIndex].ParaName == null ? row["FieldName"] : class_Mains.class_Fields[FindIndex].ParaName;
                                                row["MaxLegth"] = class_Mains.class_Fields[FindIndex].MaxLegth;
                                                row["CaseWhen"] = class_Mains.class_Fields[FindIndex].CaseWhen;
                                                row["ReturnType"] = class_Mains.class_Fields[FindIndex].ReturnType;
                                                row["TrimSign"] = class_Mains.class_Fields[FindIndex].TrimSign;
                                                row["FunctionName"] = class_Mains.class_Fields[FindIndex].FunctionName;
                                                row["WhereSelect"] = class_Mains.class_Fields[FindIndex].WhereSelect;
                                                row["WhereType"] = class_Mains.class_Fields[FindIndex].WhereType;
                                                row["LogType"] = class_Mains.class_Fields[FindIndex].LogType;
                                                row["WhereValue"] = class_Mains.class_Fields[FindIndex].WhereValue;
                                                row["WhereTrim"] = class_Mains.class_Fields[FindIndex].WhereTrim;
                                                row["WhereIsNull"] = class_Mains.class_Fields[FindIndex].WhereIsNull;
                                                row["OrderSelect"] = class_Mains.class_Fields[FindIndex].OrderSelect;
                                                row["SortType"] = class_Mains.class_Fields[FindIndex].SortType;
                                                row["SortNo"] = class_Mains.class_Fields[FindIndex].SortNo;
                                                row["GroupSelect"] = class_Mains.class_Fields[FindIndex].GroupSelect;
                                                row["HavingSelect"] = class_Mains.class_Fields[FindIndex].HavingSelect;
                                                row["HavingFunction"] = class_Mains.class_Fields[FindIndex].HavingFunction;
                                                row["HavingCondition"] = class_Mains.class_Fields[FindIndex].HavingCondition;
                                                row["HavingValue"] = class_Mains.class_Fields[FindIndex].HavingValue;
                                                IsDefault = false;
                                            }
                                        }
                                        if (IsDefault)
                                        {
                                            row["ParaName"] = Class_Tool.GetFirstCodeLow(row["FieldName"].ToString());
                                            row["MaxLegth"] = row["FieldLength"];
                                            row["ReturnType"] = row["FieldType"];
                                            row["SortNo"] = Counter++;
                                        }
                                    }
                                    break;
                                case "insert":
                                    break;
                                case "update":
                                    break;
                                case "delete":
                                    break;
                                default:
                                    break;
                            }
                        }
                        row.EndEdit();
                    }
                    if (dataTable.GetChanges() != null)
                        dataTable.AcceptChanges();
                    return dataTable;
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                dataTable.Dispose();
            }
        }
        /// <summary>
        /// 得到指定表结构
        /// </summary>
        /// <param name="TableName">指定表名</param>
        /// <returns></returns>
        public DataTable GetMainTableStruct(string TableName, int PageSelectIndex)
        {
            return _GetMainTableStruct(TableName, PageSelectIndex);
        }
        /// <summary>
        /// 得到指定数据库所有用户表
        /// </summary>
        /// <returns></returns>
        public List<Class_TableInfo> GetUseTableList()
        {
            List<Class_TableInfo> vs = new List<Class_TableInfo>();
            string Str = string.Format(@"select d.[name] as TableName
                ,isnull(f.value, '') as TableComment
                from sysobjects as d
                left join sys.extended_properties f on d.id = f.major_id and f.minor_id = 0
                where d.xtype in('u','V')
                order by d.xtype,d.[name]");
            DataTable UseTable = new DataTable("UseTable");
            UseTable = mydb.GetDataTable(Str);
            foreach (DataRow row in UseTable.Rows)
            {
                Class_TableInfo class_TableInfo = new Class_TableInfo();
                class_TableInfo.TableName = row["TableName"].ToString();
                class_TableInfo.TableComment = row["TableComment"].ToString();
                vs.Add(class_TableInfo);
            }
            UseTable.Dispose();
            return vs;
        }

        public string GetJavaType(string dataBaseFieldType)
        {
            if ((class_SqlServerAndJavas == null) || (class_SqlServerAndJavas.Count == 0))
                return null;
            int Index = class_SqlServerAndJavas.FindIndex(a => a.DataBaseFieldType.ToUpper().Equals(dataBaseFieldType.ToUpper()));
            if (Index > -1)
                return string.Format("{0}", class_SqlServerAndJavas[Index].JavaType);
            else
                return null;
        }
        /// <summary>
        /// 得到所有数据类型
        /// </summary>
        /// <returns></returns>
        public List<string> GetDataType()
        {
            List<string> vs = new List<string>();
            class_SqlServerAndJavas.ForEach(a => vs.Add(a.DataBaseFieldType));
            //vs.Add("varchar");
            //vs.Add("bit");
            //vs.Add("int");
            //vs.Add("decimal");
            //vs.Add("date");
            //vs.Add("datetime");
            return vs;
        }
        /// <summary>
        /// 得到数据类型对应的函数
        /// </summary>
        /// <param name="FieldType"></param>
        /// <returns></returns>
        public List<string> GetFunctionList(string FieldType)
        {
            List<string> vs = new List<string>();
            switch (FieldType)
            {
                case "int":
                    vs.Add(FieldType);
                    vs.Add("varchar");
                    break;
                case "date":
                    vs.Add(FieldType);
                    vs.Add("varchar");
                    break;
                case "datetime":
                    vs.Add(FieldType);
                    vs.Add("varchar");
                    break;
                case "varchar":
                    vs.Add(FieldType);
                    vs.Add("int");
                    break;
                default:
                    vs.Add("varchar");
                    vs.Add("bit");
                    vs.Add("int");
                    vs.Add("decimal");
                    vs.Add("date");
                    vs.Add("datetime");
                    break;
            }
            return vs;
        }

        public List<string> GetHavingFuctionList(string FieldType)
        {
            List<string> vs = new List<string>();
            switch (FieldType)
            {
                case "int":
                    vs.Add(FieldType);
                    vs.Add("varchar");
                    break;
                case "date":
                    vs.Add(FieldType);
                    vs.Add("varchar");
                    break;
                case "datetime":
                    vs.Add(FieldType);
                    vs.Add("varchar");
                    break;
                case "varchar":
                    vs.Add(FieldType);
                    vs.Add("int");
                    break;
                default:
                    vs.Add("COUNT");
                    vs.Add("COUNT DISTINCT");
                    vs.Add("DISINCT");
                    vs.Add("MAX");
                    vs.Add("MIN");
                    vs.Add("AVG");
                    break;
            }
            return vs;
        }
        /// <summary>
        /// 查询条件中，此字段是否加入单引号
        /// </summary>
        /// <param name="FieldType"></param>
        /// <returns></returns>
        public bool IsAddPoint(string FieldType)
        {
            bool isAdd;
            switch (FieldType)
            {
                case "datetime":
                case "date":
                case "bit":
                case "char":
                case "varchar":
                    isAdd = true;
                    break;
                default:
                    isAdd = false;
                    break;
            }
            return isAdd;
        }

        public void SetClass_AllModel<T>(T class_AllModel)
        {
            if (class_AllModel != null)
            {
                Type type = class_AllModel.GetType();
                switch (type.Name)
                {
                    case "Class_SelectAllModel":
                        OperateType = "select";
                        break;
                    case "Class_delectAllModel":
                        OperateType = "delect";
                        break;
                    case "Class_updateAllModel":
                        OperateType = "update";
                        break;
                    case "Class_insertAllModel":
                        OperateType = "insert";
                        break;
                    default:
                        break;
                }
                this.class_AllModel = class_AllModel;
            }
        }
    }
}

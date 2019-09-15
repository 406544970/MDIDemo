using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MDIDemo.PublicClass.Class_DataBaseContent;
using static MDIDemo.PublicClass.Class_SelectAllModel;

namespace MDIDemo.PublicClass
{
    public class Class_MySqlDataBase : IClass_InterFaceDataBase
    {
        public string Ip { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string DataBaseName { get; set; }
        public int Port { get; set; }
        private MySqlDb mySqlDb;
        private string ConnectString;
        private string OperateType;
        private object class_AllModel;
        private List<Class_MySqlFieldAndJava> class_MySqlFieldAndJavas;
        private List<string> FunctionList;
        ~Class_MySqlDataBase()
        {
            FunctionList.Clear();
        }
        public Class_MySqlDataBase()
        {
            IniFieldTypeChange();
        }
        public Class_MySqlDataBase(string ip, string dataBaseName, string userName, string passWord, int port)
        {
            try
            {
                if (ip != null)
                {
                    this.Ip = ip;
                    this.DataBaseName = dataBaseName;
                    this.UserName = userName;
                    this.PassWord = passWord;
                    this.Port = port;
                }
                else
                {
                    Class_PublicMethod class_PublicMethod = new Class_PublicMethod();
                    Class_DataBaseConDefault class_DataBaseConDefault = new Class_DataBaseConDefault();
                    class_DataBaseConDefault = class_PublicMethod.FromXmlToSelectObject<Class_DataBaseConDefault>("DataBaseDefaultValues");

                    if (class_DataBaseConDefault == null)
                    {
                        this.Ip = "101.201.101.138";
                        this.DataBaseName = "test01";
                        this.UserName = "king";
                        this.PassWord = "123456";
                        this.Port = 10001;
                    }
                    else
                    {
                        if (class_DataBaseConDefault.databaseType == "MySql")
                        {
                            this.Ip = class_DataBaseConDefault.dataSourceUrl;
                            this.DataBaseName = class_DataBaseConDefault.dataBaseName;
                            this.UserName = class_DataBaseConDefault.dataSourceUserName;
                            this.PassWord = class_DataBaseConDefault.dataSourcePassWord;
                            this.Port = class_DataBaseConDefault.Port;
                        }
                    }
                }
                IniFieldTypeChange();
                ConnectString = string.Format("server={0};port={4};user={2};password={3}; database={1};"
                    , this.Ip
                    , this.DataBaseName
                    , this.UserName
                    , this.PassWord
                    , this.Port.ToString());
                mySqlDb = new MySqlDb(ConnectString);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<string> GetDataType()
        {
            List<string> vs = new List<string>();
            class_MySqlFieldAndJavas.ForEach(a => vs.Add(a.DataBaseFieldType.ToLower()));
            //vs.Add("varchar");
            //vs.Add("bit");
            //vs.Add("int");
            //vs.Add("decimal");
            //vs.Add("date");
            //vs.Add("datetime");
            return vs;
        }

        private void IniFieldTypeChange()
        {
            class_MySqlFieldAndJavas = new List<Class_MySqlFieldAndJava>();
            Class_MySqlFieldAndJava class_MySqlFieldAndJava = new Class_MySqlFieldAndJava();
            #region
            class_MySqlFieldAndJava.DataBaseFieldType = "VARCHAR";
            class_MySqlFieldAndJava.JavaType = "java.lang.String";
            class_MySqlFieldAndJavas.Add(class_MySqlFieldAndJava);
            class_MySqlFieldAndJava = new Class_MySqlFieldAndJava();
            class_MySqlFieldAndJava.DataBaseFieldType = "CHAR";
            class_MySqlFieldAndJava.JavaType = "java.lang.String";
            class_MySqlFieldAndJavas.Add(class_MySqlFieldAndJava);
            class_MySqlFieldAndJava = new Class_MySqlFieldAndJava();
            class_MySqlFieldAndJava.DataBaseFieldType = "BLOB";
            class_MySqlFieldAndJava.JavaType = "java.lang.byte[]";
            class_MySqlFieldAndJavas.Add(class_MySqlFieldAndJava);
            class_MySqlFieldAndJava = new Class_MySqlFieldAndJava();
            class_MySqlFieldAndJava.DataBaseFieldType = "TEXT";
            class_MySqlFieldAndJava.JavaType = "java.lang.String";
            class_MySqlFieldAndJavas.Add(class_MySqlFieldAndJava);
            class_MySqlFieldAndJava = new Class_MySqlFieldAndJava();
            class_MySqlFieldAndJava.DataBaseFieldType = "INTEGER";
            class_MySqlFieldAndJava.JavaType = "java.lang.Long";
            class_MySqlFieldAndJavas.Add(class_MySqlFieldAndJava);
            class_MySqlFieldAndJava = new Class_MySqlFieldAndJava();
            class_MySqlFieldAndJava.DataBaseFieldType = "TEXT";
            class_MySqlFieldAndJava.JavaType = "java.lang.String";
            class_MySqlFieldAndJavas.Add(class_MySqlFieldAndJava);
            class_MySqlFieldAndJava = new Class_MySqlFieldAndJava();
            class_MySqlFieldAndJava.DataBaseFieldType = "INT";
            class_MySqlFieldAndJava.JavaType = "java.lang.Integer";
            class_MySqlFieldAndJavas.Add(class_MySqlFieldAndJava);
            class_MySqlFieldAndJava = new Class_MySqlFieldAndJava();
            class_MySqlFieldAndJava.DataBaseFieldType = "TINYINT";
            class_MySqlFieldAndJava.JavaType = "java.lang.Integer";
            class_MySqlFieldAndJavas.Add(class_MySqlFieldAndJava);
            class_MySqlFieldAndJava = new Class_MySqlFieldAndJava();
            class_MySqlFieldAndJava.DataBaseFieldType = "SMALLINT";
            class_MySqlFieldAndJava.JavaType = "java.lang.Integer";
            class_MySqlFieldAndJavas.Add(class_MySqlFieldAndJava);
            class_MySqlFieldAndJava = new Class_MySqlFieldAndJava();
            class_MySqlFieldAndJava.DataBaseFieldType = "MEDIUMINT";
            class_MySqlFieldAndJava.JavaType = "java.lang.Integer";
            class_MySqlFieldAndJavas.Add(class_MySqlFieldAndJava);
            class_MySqlFieldAndJava = new Class_MySqlFieldAndJava();
            class_MySqlFieldAndJava.DataBaseFieldType = "BIT";
            class_MySqlFieldAndJava.JavaType = "java.lang.Boolean";
            class_MySqlFieldAndJavas.Add(class_MySqlFieldAndJava);
            class_MySqlFieldAndJava = new Class_MySqlFieldAndJava();
            class_MySqlFieldAndJava.DataBaseFieldType = "BIGINT";
            //class_MySqlFieldAndJava.JavaType = "java.math.BigInteger";
            class_MySqlFieldAndJava.JavaType = "java.lang.Integer";
            class_MySqlFieldAndJavas.Add(class_MySqlFieldAndJava);
            class_MySqlFieldAndJava = new Class_MySqlFieldAndJava();
            class_MySqlFieldAndJava.DataBaseFieldType = "FLOAT";
            class_MySqlFieldAndJava.JavaType = "java.lang.Float";
            class_MySqlFieldAndJavas.Add(class_MySqlFieldAndJava);
            class_MySqlFieldAndJava = new Class_MySqlFieldAndJava();
            class_MySqlFieldAndJava.DataBaseFieldType = "DOUBLE";
            class_MySqlFieldAndJava.JavaType = "java.lang.Double";
            class_MySqlFieldAndJavas.Add(class_MySqlFieldAndJava);
            class_MySqlFieldAndJava = new Class_MySqlFieldAndJava();
            class_MySqlFieldAndJava.DataBaseFieldType = "DECIMAL";
            class_MySqlFieldAndJava.JavaType = "java.math.BigDecimal";
            class_MySqlFieldAndJavas.Add(class_MySqlFieldAndJava);
            class_MySqlFieldAndJava = new Class_MySqlFieldAndJava();
            class_MySqlFieldAndJava.DataBaseFieldType = "ID";
            class_MySqlFieldAndJava.JavaType = "java.lang.Long";
            class_MySqlFieldAndJavas.Add(class_MySqlFieldAndJava);
            class_MySqlFieldAndJava = new Class_MySqlFieldAndJava();
            class_MySqlFieldAndJava.DataBaseFieldType = "DATE";
            class_MySqlFieldAndJava.JavaType = "java.sql.Date";
            class_MySqlFieldAndJavas.Add(class_MySqlFieldAndJava);
            class_MySqlFieldAndJava = new Class_MySqlFieldAndJava();
            class_MySqlFieldAndJava.DataBaseFieldType = "TIME";
            class_MySqlFieldAndJava.JavaType = "java.sql.Time";
            class_MySqlFieldAndJavas.Add(class_MySqlFieldAndJava);
            class_MySqlFieldAndJava = new Class_MySqlFieldAndJava();
            class_MySqlFieldAndJava.DataBaseFieldType = "DATETIME";
            class_MySqlFieldAndJava.JavaType = "java.sql.Timestamp";
            class_MySqlFieldAndJavas.Add(class_MySqlFieldAndJava);
            class_MySqlFieldAndJava = new Class_MySqlFieldAndJava();
            class_MySqlFieldAndJava.DataBaseFieldType = "TIMESTAMP";
            class_MySqlFieldAndJava.JavaType = "java.sql.Timestamp";
            class_MySqlFieldAndJavas.Add(class_MySqlFieldAndJava);
            class_MySqlFieldAndJava = new Class_MySqlFieldAndJava();
            class_MySqlFieldAndJava.DataBaseFieldType = "YEAR";
            class_MySqlFieldAndJava.JavaType = "java.sql.Date";
            class_MySqlFieldAndJavas.Add(class_MySqlFieldAndJava);
            #endregion

            FunctionList = new List<string>();
            FunctionList.Add("COUNT(?)");
            FunctionList.Add("COUNT(DISTINCT ?)");
            FunctionList.Add("SUM(?)");
            FunctionList.Add("MAX(?)");
            FunctionList.Add("MIN(?)");
            FunctionList.Add("AVG(?)");
            FunctionList.Add("DATE_FORMAT(?,'%Y-%m-%d')");
            FunctionList.Add("DATE_FORMAT(?,'%Y-%m')");
            FunctionList.Add("DATE_FORMAT(?,'%Y-%m-%d %H:%i:%S')");

        }
        public string GetDataTypeByFunction(string FunctionName, string MySqlDataType)
        {
            if (FunctionName == null || FunctionName.Length == 0)
                return MySqlDataType;

            string ResultValue = null;
            switch (FunctionName)
            {
                case "COUNT(?)":
                case "COUNT(DISTINCT ?)":
                    ResultValue = "int";
                    break;
                case "SUM(?)":
                case "MAX(?)":
                case "MIN(?)":
                case "AVG(?)":
                    ResultValue = MySqlDataType;
                    break;
                case "DATE_FORMAT(?,'%Y-%m')":
                case "DATE_FORMAT(?,'%Y-%m-%d')":
                case "DATE_FORMAT(?,'%Y-%m-%d %H:%i:%S')":
                    ResultValue = "varchar";
                    break;
                default:
                    ResultValue = "varchar";
                    break;
            }
            return ResultValue;
        }
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
                    vs = FunctionList;
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
                    vs.Add("COUNT(?)");
                    vs.Add("COUNT(DISTINCT ?)");
                    vs.Add("SUM(?)");
                    vs.Add("MAX(?)");
                    vs.Add("MIN(?)");
                    vs.Add("AVG(?)");
                    break;
            }
            return vs;
        }

        public DataTable GetMainTableStruct<T>(string TableName, int PageSelectIndex, bool SelectSelectDefault)
        {
            return _GetMainTableStruct<T>(TableName, PageSelectIndex, SelectSelectDefault);
        }
        private DataTable _GetTableStruct(string TableName)
        {
            DataTable dataTable = new DataTable();
            DataColumn FieldName = new DataColumn("FieldName", typeof(string));
            DataColumn FieldRemark = new DataColumn("FieldRemark", typeof(string));
            DataColumn FieldType = new DataColumn("FieldType", typeof(string));
            DataColumn FieldLength = new DataColumn("FieldLength", typeof(long));
            DataColumn FieldDefaultValue = new DataColumn("FieldDefaultValue", typeof(string));
            DataColumn FieldIsNull = new DataColumn("FieldIsNull", typeof(bool));
            DataColumn FieldIsKey = new DataColumn("FieldIsKey", typeof(bool));
            DataColumn FieldIsAutoAdd = new DataColumn("FieldIsAutoAdd", typeof(bool));
            dataTable.Columns.Add(FieldName);
            dataTable.Columns.Add(FieldRemark);
            dataTable.Columns.Add(FieldType);
            dataTable.Columns.Add(FieldLength);
            dataTable.Columns.Add(FieldDefaultValue);
            dataTable.Columns.Add(FieldIsNull);
            dataTable.Columns.Add(FieldIsKey);
            dataTable.Columns.Add(FieldIsAutoAdd);

            string Sql = string.Format(@"SELECT 
cList.COLUMN_NAME AS FieldName,
TRIM(cList.COLUMN_COMMENT) AS FieldRemark,
cList.DATA_TYPE AS FieldType,
CASE WHEN cList.CHARACTER_MAXIMUM_LENGTH IS NOT NULL THEN cList.CHARACTER_MAXIMUM_LENGTH ELSE cList.NUMERIC_PRECISION END AS FieldLength,
cList.COLUMN_DEFAULT AS FieldDefaultValue,
CASE cList.IS_NULLABLE WHEN 'YES' THEN 1 ELSE 0 END AS FieldIsNull,
CASE WHEN pKey.COLUMN_NAME IS NULL THEN 0 ELSE 1 END  AS FieldIsKey,
CASE WHEN cList.EXTRA = 'auto_increment' THEN 1 ELSE 0 END AS FieldIsAutoAdd
FROM information_schema.COLUMNS AS cList
LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS pKey ON cList.TABLE_SCHEMA = pKey.TABLE_SCHEMA
AND cList.TABLE_NAME = pKey.TABLE_NAME 
AND cList.COLUMN_NAME = pKey.COLUMN_NAME 
AND pKey.CONSTRAINT_NAME = 'PRIMARY'
WHERE cList.TABLE_NAME = '{0}'
AND cList.TABLE_SCHEMA = '{1}'
ORDER BY cList.ORDINAL_POSITION", TableName, this.DataBaseName);
            DataTable iniDataTable = new DataTable();
            iniDataTable = mySqlDb.GetDataTable(Sql);
            foreach (DataRow dataRow in iniDataTable.Rows)
            {
                DataRow newRow = dataTable.NewRow();
                foreach (DataColumn dataColumn in iniDataTable.Columns)
                {
                    switch (dataColumn.Caption)
                    {
                        case "FieldIsNull":
                        case "FieldIsKey":
                        case "FieldIsAutoAdd":
                            newRow[dataColumn.Caption] = Convert.ToBoolean(dataRow[dataColumn.Caption]) ? true : false;
                            break;
                        default:
                            newRow[dataColumn.Caption] = dataRow[dataColumn.Caption];
                            break;
                    }
                }
                dataTable.Rows.Add(newRow);
            }
            dataTable.AcceptChanges();
            iniDataTable.Dispose();
            return dataTable;
        }
        private DataTable _GetMainTableStruct<T>(string TableName, int PageSelectIndex, bool SelectSelectDefault)
        {
            DataTable dataTable = new DataTable();
            try
            {
                if (TableName != null)
                {
                    Class_CaseWhen class_CaseWhen = new Class_CaseWhen();
                    List<string> vs = new List<string>();
                    vs = class_CaseWhen.GetCaseWhenNameList();
                    dataTable = _GetTableStruct(TableName);
                    T mySelect = (T)this.class_AllModel;
                    switch (OperateType)
                    {
                        case "select":
                            {
                                #region 加入自定义Select列
                                DataColumn SelectSelect = new DataColumn("SelectSelect", typeof(bool));
                                DataColumn ParaName = new DataColumn("ParaName", typeof(string));
                                DataColumn MaxLegth = new DataColumn("MaxLegth", typeof(Int32));
                                DataColumn CaseWhen = new DataColumn("CaseWhen", typeof(string));
                                DataColumn ReturnType = new DataColumn("ReturnType", typeof(string));
                                DataColumn TrimSign = new DataColumn("TrimSign", typeof(bool));
                                DataColumn FunctionName = new DataColumn("FunctionName", typeof(string));
                                DataColumn TotalFunctionName = new DataColumn("TotalFunctionName", typeof(string));

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

                                DataColumn DisplaySort = new DataColumn("DisplaySort", typeof(Int32));
                                DataColumn Title = new DataColumn("Title", typeof(string));
                                DataColumn Width = new DataColumn("Width", typeof(Int32));
                                DataColumn Type = new DataColumn("Type", typeof(string));
                                DataColumn LayChecked = new DataColumn("LayChecked", typeof(bool));
                                DataColumn Fixed = new DataColumn("Fixed", typeof(string));
                                DataColumn Hide = new DataColumn("IsDisplay", typeof(bool));
                                DataColumn Sort = new DataColumn("Sort", typeof(bool));
                                DataColumn UnResize = new DataColumn("UnResize", typeof(bool));
                                DataColumn Style = new DataColumn("Style", typeof(string));
                                DataColumn Align = new DataColumn("Align", typeof(string));
                                DataColumn ToolBar = new DataColumn("ToolBar", typeof(string));


                                SelectSelect.DefaultValue = SelectSelectDefault;
                                TrimSign.DefaultValue = true;
                                WhereSelect.DefaultValue = false;
                                WhereTrim.DefaultValue = true;
                                WhereIsNull.DefaultValue = true;
                                OrderSelect.DefaultValue = false;
                                GroupSelect.DefaultValue = false;
                                HavingSelect.DefaultValue = false;

                                LayChecked.DefaultValue = false;
                                Hide.DefaultValue = false;
                                UnResize.DefaultValue = false;
                                Sort.DefaultValue = false;

                                dataTable.Columns.Add(SelectSelect);
                                dataTable.Columns.Add(ParaName);
                                dataTable.Columns.Add(MaxLegth);
                                dataTable.Columns.Add(CaseWhen);
                                dataTable.Columns.Add(ReturnType);
                                dataTable.Columns.Add(TrimSign);
                                dataTable.Columns.Add(FunctionName);
                                dataTable.Columns.Add(TotalFunctionName);

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

                                dataTable.Columns.Add(DisplaySort);
                                dataTable.Columns.Add(Title);
                                dataTable.Columns.Add(Width);
                                dataTable.Columns.Add(Type);
                                dataTable.Columns.Add(LayChecked);
                                dataTable.Columns.Add(Fixed);
                                dataTable.Columns.Add(Hide);
                                dataTable.Columns.Add(Sort);
                                dataTable.Columns.Add(UnResize);
                                dataTable.Columns.Add(Style);
                                dataTable.Columns.Add(Align);
                                dataTable.Columns.Add(ToolBar);
                                #endregion
                            }
                            break;
                        case "insert":
                            {
                                #region 加入自定义Insert列
                                DataColumn InsertSelect = new DataColumn("InsertSelect", typeof(bool));//Insert/Values选择
                                DataColumn ParaName = new DataColumn("ParaName", typeof(string));
                                DataColumn TrimSign = new DataColumn("TrimSign", typeof(bool));//是否去空格

                                DataColumn WhereSelect = new DataColumn("WhereSelect", typeof(bool));//重复判断选择
                                DataColumn WhereType = new DataColumn("WhereType", typeof(string));//And Or
                                DataColumn LogType = new DataColumn("LogType", typeof(string));// = >< like，固定值
                                DataColumn WhereValue = new DataColumn("WhereValue", typeof(string));
                                DataColumn WhereIsNull = new DataColumn("WhereIsNull", typeof(bool));//Where条件是否可为空

                                DataColumn FrontSelect = new DataColumn("FrontSelect", typeof(bool));//是否页面显示
                                DataColumn LabelCaption = new DataColumn("LabelCaption", typeof(string));//标签内容
                                DataColumn IsMust = new DataColumn("IsMust", typeof(bool));//是否为必填项
                                DataColumn CompomentType = new DataColumn("CompomentType", typeof(string));//控件类型
                                DataColumn Hint = new DataColumn("Hint", typeof(string));//提示
                                DataColumn DefaultValue = new DataColumn("DefaultValue", typeof(string));//默认值
                                DataColumn SortNo = new DataColumn("SortNo", typeof(Int32));//出现顺序
                                DataColumn ValueId = new DataColumn("ValueId", typeof(string));//值ID
                                DataColumn ReadOnly = new DataColumn("ReadOnly", typeof(bool));//是否只读
                                DataColumn CheckType = new DataColumn("CheckType", typeof(string));//校验类型
                                DataColumn ClassTitle = new DataColumn("ClassTitle", typeof(string));//分类标题

                                InsertSelect.DefaultValue = true;
                                TrimSign.DefaultValue = true;
                                WhereSelect.DefaultValue = false;
                                WhereIsNull.DefaultValue = true;
                                ReadOnly.DefaultValue = false;

                                FrontSelect.DefaultValue = true;
                                IsMust.DefaultValue = true;

                                dataTable.Columns.Add(InsertSelect);
                                dataTable.Columns.Add(ParaName);
                                dataTable.Columns.Add(TrimSign);

                                dataTable.Columns.Add(WhereSelect);
                                dataTable.Columns.Add(WhereType);
                                dataTable.Columns.Add(LogType);
                                dataTable.Columns.Add(WhereValue);
                                dataTable.Columns.Add(WhereIsNull);

                                dataTable.Columns.Add(FrontSelect);
                                dataTable.Columns.Add(IsMust);
                                dataTable.Columns.Add(LabelCaption);
                                dataTable.Columns.Add(CompomentType);
                                dataTable.Columns.Add(Hint);
                                dataTable.Columns.Add(DefaultValue);
                                dataTable.Columns.Add(SortNo);
                                dataTable.Columns.Add(ValueId);
                                dataTable.Columns.Add(ReadOnly);
                                dataTable.Columns.Add(CheckType);
                                dataTable.Columns.Add(ClassTitle);
                                #endregion
                            }
                            break;
                        case "update":
                            {
                                #region 加入自定义Update列
                                DataColumn UpdateSelect = new DataColumn("UpdateSelect", typeof(bool));//SET选择
                                DataColumn ParaName = new DataColumn("ParaName", typeof(string));
                                DataColumn TrimSign = new DataColumn("TrimSign", typeof(bool));//是否去空格

                                DataColumn WhereSelect = new DataColumn("WhereSelect", typeof(bool));//重复判断选择
                                DataColumn WhereType = new DataColumn("WhereType", typeof(string));//And Or
                                DataColumn LogType = new DataColumn("LogType", typeof(string));// = >< like，固定值
                                DataColumn WhereValue = new DataColumn("WhereValue", typeof(string));
                                DataColumn WhereTrim = new DataColumn("WhereTrim", typeof(bool));//去空格
                                DataColumn WhereIsNull = new DataColumn("WhereIsNull", typeof(bool));//Where条件是否可为空
                                
                                DataColumn FrontSelect = new DataColumn("FrontSelect", typeof(bool));//是否页面显示
                                DataColumn LabelCaption = new DataColumn("LabelCaption", typeof(string));//标签内容
                                DataColumn IsMust = new DataColumn("IsMust", typeof(bool));//是否为必填项
                                DataColumn CompomentType = new DataColumn("CompomentType", typeof(string));//控件类型
                                DataColumn Hint = new DataColumn("Hint", typeof(string));//提示
                                DataColumn DefaultValue = new DataColumn("DefaultValue", typeof(string));//默认值
                                DataColumn SortNo = new DataColumn("SortNo", typeof(Int32));//出现顺序
                                DataColumn ValueId = new DataColumn("ValueId", typeof(string));//值ID
                                DataColumn ReadOnly = new DataColumn("ReadOnly", typeof(bool));//是否只读
                                DataColumn CheckType = new DataColumn("CheckType", typeof(string));//校验类型
                                DataColumn ClassTitle = new DataColumn("ClassTitle", typeof(string));//分类标题

                                UpdateSelect.DefaultValue = true;
                                TrimSign.DefaultValue = true;
                                WhereSelect.DefaultValue = false;
                                WhereTrim.DefaultValue = false;
                                WhereIsNull.DefaultValue = true;
                                ReadOnly.DefaultValue = false;

                                FrontSelect.DefaultValue = true;
                                IsMust.DefaultValue = true;

                                dataTable.Columns.Add(UpdateSelect);
                                dataTable.Columns.Add(ParaName);
                                dataTable.Columns.Add(TrimSign);

                                dataTable.Columns.Add(WhereSelect);
                                dataTable.Columns.Add(WhereType);
                                dataTable.Columns.Add(LogType);
                                dataTable.Columns.Add(WhereValue);
                                dataTable.Columns.Add(WhereTrim);
                                dataTable.Columns.Add(WhereIsNull);

                                dataTable.Columns.Add(FrontSelect);
                                dataTable.Columns.Add(IsMust);
                                dataTable.Columns.Add(LabelCaption);
                                dataTable.Columns.Add(CompomentType);
                                dataTable.Columns.Add(Hint);
                                dataTable.Columns.Add(DefaultValue);
                                dataTable.Columns.Add(SortNo);
                                dataTable.Columns.Add(ValueId);
                                dataTable.Columns.Add(ReadOnly);
                                dataTable.Columns.Add(CheckType);
                                dataTable.Columns.Add(ClassTitle);
                                #endregion
                            }
                            break;
                        case "delete":
                            {
                                #region 加入自定义Delete列
                                DataColumn ParaName = new DataColumn("ParaName", typeof(string));

                                DataColumn WhereSelect = new DataColumn("WhereSelect", typeof(bool));//重复判断选择
                                DataColumn WhereType = new DataColumn("WhereType", typeof(string));//And Or
                                DataColumn LogType = new DataColumn("LogType", typeof(string));// = >< like，固定值
                                DataColumn WhereValue = new DataColumn("WhereValue", typeof(string));
                                DataColumn WhereTrim = new DataColumn("WhereTrim", typeof(bool));//去空格
                                DataColumn WhereIsNull = new DataColumn("WhereIsNull", typeof(bool));//Where条件是否可为空

                                DataColumn FrontSelect = new DataColumn("FrontSelect", typeof(bool));//是否页面显示
                                DataColumn LabelCaption = new DataColumn("LabelCaption", typeof(string));//标签内容
                                DataColumn IsMust = new DataColumn("IsMust", typeof(bool));//是否为必填项
                                DataColumn CompomentType = new DataColumn("CompomentType", typeof(string));//控件类型
                                DataColumn Hint = new DataColumn("Hint", typeof(string));//提示
                                DataColumn DefaultValue = new DataColumn("DefaultValue", typeof(string));//默认值
                                DataColumn SortNo = new DataColumn("SortNo", typeof(Int32));//出现顺序
                                DataColumn ValueId = new DataColumn("ValueId", typeof(string));//值ID
                                DataColumn ReadOnly = new DataColumn("ReadOnly", typeof(bool));//是否只读
                                DataColumn CheckType = new DataColumn("CheckType", typeof(string));//校验类型
                                DataColumn ClassTitle = new DataColumn("ClassTitle", typeof(string));//分类标题

                                WhereSelect.DefaultValue = false;
                                WhereTrim.DefaultValue = false;
                                WhereIsNull.DefaultValue = true;
                                ReadOnly.DefaultValue = false;

                                FrontSelect.DefaultValue = true;
                                IsMust.DefaultValue = true;

                                dataTable.Columns.Add(ParaName);

                                dataTable.Columns.Add(WhereSelect);
                                dataTable.Columns.Add(WhereType);
                                dataTable.Columns.Add(LogType);
                                dataTable.Columns.Add(WhereValue);
                                dataTable.Columns.Add(WhereTrim);
                                dataTable.Columns.Add(WhereIsNull);

                                dataTable.Columns.Add(FrontSelect);
                                dataTable.Columns.Add(IsMust);
                                dataTable.Columns.Add(LabelCaption);
                                dataTable.Columns.Add(CompomentType);
                                dataTable.Columns.Add(Hint);
                                dataTable.Columns.Add(DefaultValue);
                                dataTable.Columns.Add(SortNo);
                                dataTable.Columns.Add(ValueId);
                                dataTable.Columns.Add(ReadOnly);
                                dataTable.Columns.Add(CheckType);
                                dataTable.Columns.Add(ClassTitle);
                                #endregion
                            }
                            break;
                        default:
                            break;
                    }
                    int Counter = 1;

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
                                        Class_SelectAllModel.Class_Sub class_CurrentPage = new Class_SelectAllModel.Class_Sub();
                                        if ((mySelect as Class_SelectAllModel).class_SubList.Count > PageSelectIndex)
                                            class_CurrentPage = (mySelect as Class_SelectAllModel).class_SubList[PageSelectIndex];
                                        if ((mySelect as Class_SelectAllModel).class_SubList.Count > PageSelectIndex && (mySelect as Class_SelectAllModel).class_SubList[PageSelectIndex].class_Fields.Count > 0)
                                        {
                                            int FindIndex = class_CurrentPage.class_Fields.FindIndex(a => a.FieldName.Equals(myFieldName));
                                            if (FindIndex > -1)
                                            {
                                                row["SelectSelect"] = class_CurrentPage.class_Fields[FindIndex].SelectSelect;
                                                row["ParaName"] = class_CurrentPage.class_Fields[FindIndex].ParaName == null ? Class_Tool.GetFirstCodeLow(row["FieldName"].ToString()) : class_CurrentPage.class_Fields[FindIndex].ParaName;
                                                row["MaxLegth"] = class_CurrentPage.class_Fields[FindIndex].MaxLegth;
                                                row["CaseWhen"] = class_CurrentPage.class_Fields[FindIndex].CaseWhen;
                                                row["ReturnType"] = class_CurrentPage.class_Fields[FindIndex].ReturnType;
                                                row["TrimSign"] = class_CurrentPage.class_Fields[FindIndex].TrimSign;
                                                row["FunctionName"] = class_CurrentPage.class_Fields[FindIndex].FunctionName;
                                                row["TotalFunctionName"] = class_CurrentPage.class_Fields[FindIndex].TotalFunctionName;
                                                row["WhereSelect"] = class_CurrentPage.class_Fields[FindIndex].WhereSelect;
                                                row["WhereType"] = class_CurrentPage.class_Fields[FindIndex].WhereType;
                                                row["LogType"] = class_CurrentPage.class_Fields[FindIndex].LogType;
                                                row["WhereValue"] = class_CurrentPage.class_Fields[FindIndex].WhereValue;
                                                row["WhereTrim"] = class_CurrentPage.class_Fields[FindIndex].WhereTrim;
                                                row["WhereIsNull"] = class_CurrentPage.class_Fields[FindIndex].WhereIsNull;
                                                row["OrderSelect"] = class_CurrentPage.class_Fields[FindIndex].OrderSelect;
                                                row["SortType"] = class_CurrentPage.class_Fields[FindIndex].SortType;
                                                row["SortNo"] = class_CurrentPage.class_Fields[FindIndex].SortNo;
                                                row["GroupSelect"] = class_CurrentPage.class_Fields[FindIndex].GroupSelect;
                                                row["HavingSelect"] = class_CurrentPage.class_Fields[FindIndex].HavingSelect;
                                                row["HavingFunction"] = class_CurrentPage.class_Fields[FindIndex].HavingFunction;
                                                row["HavingCondition"] = class_CurrentPage.class_Fields[FindIndex].HavingCondition;
                                                row["HavingValue"] = class_CurrentPage.class_Fields[FindIndex].HavingValue;

                                                row["Title"] = class_CurrentPage.class_Fields[FindIndex].Title;
                                                row["Width"] = class_CurrentPage.class_Fields[FindIndex].Width;
                                                row["Type"] = class_CurrentPage.class_Fields[FindIndex].Type;
                                                row["LayChecked"] = class_CurrentPage.class_Fields[FindIndex].LayChecked;
                                                row["Fixed"] = class_CurrentPage.class_Fields[FindIndex].Fixed;
                                                row["IsDisplay"] = class_CurrentPage.class_Fields[FindIndex].IsDisplay;
                                                row["Sort"] = class_CurrentPage.class_Fields[FindIndex].Sort;
                                                row["UnResize"] = class_CurrentPage.class_Fields[FindIndex].UnResize;
                                                row["Style"] = class_CurrentPage.class_Fields[FindIndex].Style;
                                                row["Align"] = class_CurrentPage.class_Fields[FindIndex].Align;
                                                row["ToolBar"] = class_CurrentPage.class_Fields[FindIndex].ToolBar;
                                                IsDefault = false;
                                            }
                                        }
                                        if (IsDefault)
                                        {

                                            row["SortType"] = "升序";
                                            row["LogType"] = "=";
                                            row["WhereType"] = "AND";
                                            row["WhereValue"] = "参数";
                                            row["ParaName"] = Class_Tool.GetFirstCodeLow(row["FieldName"].ToString());
                                            row["MaxLegth"] = row["FieldLength"];
                                            row["ReturnType"] = row["FieldType"];
                                            row["SortNo"] = Counter++;
                                            if (vs != null && vs.Count > 0)
                                            {
                                                if (vs.IndexOf(row["FieldName"].ToString()) > -1)
                                                    row["CaseWhen"] = row["FieldName"].ToString();
                                            }
                                            switch (row["FieldName"].ToString())
                                            {
                                                case "worktime":
                                                    row["FunctionName"] = "DATE_FORMAT(?,'%Y-%m-%d')";
                                                    break;
                                                case "stopSign":
                                                    row["WhereValue"] = "0";
                                                    row["WhereSelect"] = Convert.ToBoolean(true);
                                                    row["SelectSelect"] = Convert.ToBoolean(false);
                                                    row["WhereIsNull"] = Convert.ToBoolean(false);
                                                    break;
                                                case "sortNo":
                                                    row["SelectSelect"] = Convert.ToBoolean(false);
                                                    row["OrderSelect"] = Convert.ToBoolean(true);
                                                    break;
                                                default:
                                                    break;
                                            }
                                            if (row["FieldType"].Equals("varchar"))
                                                row["WhereTrim"] = Convert.ToBoolean(true);
                                            else
                                                row["WhereTrim"] = Convert.ToBoolean(false);

                                            row["IsDisplay"] = row["SelectSelect"];
                                            row["Title"] = Class_Tool.DeleteKuoHaoOther(row["FieldRemark"].ToString());
                                            row["Align"] = GetAlign(row["FieldType"].ToString());
                                            if (row["FieldName"].ToString().IndexOf("remark") > -1)
                                                row["Align"] = "left";
                                            row["DisplaySort"] = row["SortNo"];
                                            row["Type"] = "normal";
                                            row["Width"] = 80;
                                            row["Fixed"] = "none";
                                        }
                                    }
                                    break;
                                case "insert":
                                    {
                                        Class_InsertAllModel.Class_Sub class_CurrentPage = new Class_InsertAllModel.Class_Sub();
                                        if ((mySelect as Class_InsertAllModel).class_SubList.Count > PageSelectIndex)
                                            class_CurrentPage = (mySelect as Class_InsertAllModel).class_SubList[PageSelectIndex];
                                        if ((mySelect as Class_InsertAllModel).class_SubList.Count > PageSelectIndex && (mySelect as Class_InsertAllModel).class_SubList[PageSelectIndex].class_Fields.Count > 0)
                                        {
                                            int FindIndex = class_CurrentPage.class_Fields.FindIndex(a => a.FieldName.Equals(myFieldName));
                                            if (FindIndex > -1)
                                            {
                                                row["InsertSelect"] = class_CurrentPage.class_Fields[FindIndex].InsertSelect;
                                                row["ParaName"] = class_CurrentPage.class_Fields[FindIndex].ParaName == null ? row["FieldName"] : class_CurrentPage.class_Fields[FindIndex].ParaName;
                                                row["TrimSign"] = class_CurrentPage.class_Fields[FindIndex].TrimSign;
                                                row["WhereSelect"] = class_CurrentPage.class_Fields[FindIndex].WhereSelect;
                                                row["WhereType"] = class_CurrentPage.class_Fields[FindIndex].WhereType;
                                                row["LogType"] = class_CurrentPage.class_Fields[FindIndex].LogType;
                                                row["WhereValue"] = class_CurrentPage.class_Fields[FindIndex].WhereValue;
                                                row["WhereIsNull"] = class_CurrentPage.class_Fields[FindIndex].WhereIsNull;

                                                row["FrontSelect"] = class_CurrentPage.class_Fields[FindIndex].FrontSelect;
                                                row["IsMust"] = class_CurrentPage.class_Fields[FindIndex].IsMust;
                                                row["LabelCaption"] = class_CurrentPage.class_Fields[FindIndex].LabelCaption;
                                                row["CompomentType"] = class_CurrentPage.class_Fields[FindIndex].CompomentType;
                                                row["Hint"] = class_CurrentPage.class_Fields[FindIndex].Hint;
                                                row["DefaultValue"] = class_CurrentPage.class_Fields[FindIndex].DefaultValue;
                                                row["SortNo"] = class_CurrentPage.class_Fields[FindIndex].SortNo;
                                                row["ValueId"] = class_CurrentPage.class_Fields[FindIndex].ValueId;
                                                row["ReadOnly"] = class_CurrentPage.class_Fields[FindIndex].ReadOnly;
                                                row["CheckType"] = class_CurrentPage.class_Fields[FindIndex].CheckType;
                                                IsDefault = false;
                                            }
                                        }
                                        if (IsDefault)
                                        {
                                            row["LogType"] = "=";
                                            row["ParaName"] = Class_Tool.GetFirstCodeLow(row["FieldName"].ToString());
                                            row["WhereType"] = "AND";
                                            row["WhereValue"] = "参数";
                                            row["SortNo"] = Counter++;
                                            switch (row["FieldName"].ToString())
                                            {
                                                case "worktime":
                                                    break;
                                                case "stopSign":
                                                    break;
                                                case "sortNo":
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                    }
                                    break;
                                case "update":
                                    {
                                        Class_UpdateAllModel.Class_Sub class_CurrentPage = new Class_UpdateAllModel.Class_Sub();
                                        if ((mySelect as Class_UpdateAllModel).class_SubList.Count > PageSelectIndex)
                                            class_CurrentPage = (mySelect as Class_UpdateAllModel).class_SubList[PageSelectIndex];
                                        if ((mySelect as Class_UpdateAllModel).class_SubList.Count > PageSelectIndex && (mySelect as Class_UpdateAllModel).class_SubList[PageSelectIndex].class_Fields.Count > 0)
                                        {
                                            int FindIndex = class_CurrentPage.class_Fields.FindIndex(a => a.FieldName.Equals(myFieldName));
                                            if (FindIndex > -1)
                                            {
                                                row["UpdateSelect"] = class_CurrentPage.class_Fields[FindIndex].UpdateSelect;
                                                row["ParaName"] = class_CurrentPage.class_Fields[FindIndex].ParaName == null ? row["FieldName"] : class_CurrentPage.class_Fields[FindIndex].ParaName;
                                                row["TrimSign"] = class_CurrentPage.class_Fields[FindIndex].TrimSign;
                                                row["WhereSelect"] = class_CurrentPage.class_Fields[FindIndex].WhereSelect;
                                                row["WhereType"] = class_CurrentPage.class_Fields[FindIndex].WhereType;
                                                row["LogType"] = class_CurrentPage.class_Fields[FindIndex].LogType;
                                                row["WhereValue"] = class_CurrentPage.class_Fields[FindIndex].WhereValue;
                                                row["WhereTrim"] = class_CurrentPage.class_Fields[FindIndex].WhereTrim;
                                                row["WhereIsNull"] = class_CurrentPage.class_Fields[FindIndex].WhereIsNull;

                                                row["FrontSelect"] = class_CurrentPage.class_Fields[FindIndex].FrontSelect;
                                                row["IsMust"] = class_CurrentPage.class_Fields[FindIndex].IsMust;
                                                row["LabelCaption"] = class_CurrentPage.class_Fields[FindIndex].LabelCaption;
                                                row["CompomentType"] = class_CurrentPage.class_Fields[FindIndex].CompomentType;
                                                row["Hint"] = class_CurrentPage.class_Fields[FindIndex].Hint;
                                                row["DefaultValue"] = class_CurrentPage.class_Fields[FindIndex].DefaultValue;
                                                row["SortNo"] = class_CurrentPage.class_Fields[FindIndex].SortNo;
                                                row["ValueId"] = class_CurrentPage.class_Fields[FindIndex].ValueId;
                                                row["ReadOnly"] = class_CurrentPage.class_Fields[FindIndex].ReadOnly;
                                                row["CheckType"] = class_CurrentPage.class_Fields[FindIndex].CheckType;
                                                IsDefault = false;
                                            }
                                        }
                                        if (IsDefault)
                                        {
                                            row["LogType"] = "=";
                                            row["ParaName"] = Class_Tool.GetFirstCodeLow(row["FieldName"].ToString());
                                            row["WhereSelect"] = true;
                                            row["WhereType"] = "AND";
                                            row["WhereValue"] = "参数";
                                            row["WhereTrim"] = true;
                                            row["SortNo"] = Counter++;
                                            switch (row["FieldName"].ToString())
                                            {
                                                case "worktime":
                                                    break;
                                                case "stopSign":
                                                    break;
                                                case "sortNo":
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                    }
                                    break;
                                case "delete":
                                    {
                                        Class_DeleteAllModel.Class_Sub class_CurrentPage = new Class_DeleteAllModel.Class_Sub();
                                        if ((mySelect as Class_DeleteAllModel).class_SubList.Count > PageSelectIndex)
                                            class_CurrentPage = (mySelect as Class_DeleteAllModel).class_SubList[PageSelectIndex];
                                        if ((mySelect as Class_DeleteAllModel).class_SubList.Count > PageSelectIndex && (mySelect as Class_DeleteAllModel).class_SubList[PageSelectIndex].class_Fields.Count > 0)
                                        {
                                            int FindIndex = class_CurrentPage.class_Fields.FindIndex(a => a.FieldName.Equals(myFieldName));
                                            if (FindIndex > -1)
                                            {
                                                row["ParaName"] = class_CurrentPage.class_Fields[FindIndex].ParaName == null ? row["FieldName"] : class_CurrentPage.class_Fields[FindIndex].ParaName;
                                                row["WhereSelect"] = class_CurrentPage.class_Fields[FindIndex].WhereSelect;
                                                row["WhereType"] = class_CurrentPage.class_Fields[FindIndex].WhereType;
                                                row["LogType"] = class_CurrentPage.class_Fields[FindIndex].LogType;
                                                row["WhereValue"] = class_CurrentPage.class_Fields[FindIndex].WhereValue;
                                                row["WhereTrim"] = class_CurrentPage.class_Fields[FindIndex].WhereTrim;
                                                row["WhereIsNull"] = class_CurrentPage.class_Fields[FindIndex].WhereIsNull;

                                                row["FrontSelect"] = class_CurrentPage.class_Fields[FindIndex].FrontSelect;
                                                row["IsMust"] = class_CurrentPage.class_Fields[FindIndex].IsMust;
                                                row["LabelCaption"] = class_CurrentPage.class_Fields[FindIndex].LabelCaption;
                                                row["CompomentType"] = class_CurrentPage.class_Fields[FindIndex].CompomentType;
                                                row["Hint"] = class_CurrentPage.class_Fields[FindIndex].Hint;
                                                row["DefaultValue"] = class_CurrentPage.class_Fields[FindIndex].DefaultValue;
                                                row["SortNo"] = class_CurrentPage.class_Fields[FindIndex].SortNo;
                                                row["ValueId"] = class_CurrentPage.class_Fields[FindIndex].ValueId;
                                                row["ReadOnly"] = class_CurrentPage.class_Fields[FindIndex].ReadOnly;
                                                row["CheckType"] = class_CurrentPage.class_Fields[FindIndex].CheckType;
                                                IsDefault = false;
                                            }
                                        }
                                        if (IsDefault)
                                        {
                                            row["LogType"] = "=";
                                            row["ParaName"] = Class_Tool.GetFirstCodeLow(row["FieldName"].ToString());
                                            row["WhereSelect"] = true;
                                            row["WhereType"] = "AND";
                                            row["WhereValue"] = "参数";
                                            row["WhereTrim"] = true;
                                            row["SortNo"] = Counter++;
                                            switch (row["FieldName"].ToString())
                                            {
                                                case "worktime":
                                                    break;
                                                case "stopSign":
                                                    break;
                                                case "sortNo":
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        row.EndEdit();
                    }
                    vs.Clear();
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

        public List<Class_TableInfo> GetUseTableList(List<string> myTableNameList)
        {
            List<Class_TableInfo> vs = new List<Class_TableInfo>();
            string Str;
            if (myTableNameList == null)
                Str = string.Format(@"SELECT TABLE_NAME AS TableName
                ,TABLE_COMMENT as TableComment
                FROM information_schema.TABLES
                WHERE TABLE_SCHEMA = '{0}'
                ORDER BY CREATE_TIME", this.DataBaseName);
            else
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (string row in myTableNameList)
                {
                    stringBuilder.AppendFormat(",'{0}'", row);
                }
                Str = string.Format(@"SELECT TABLE_NAME AS TableName
                ,TABLE_COMMENT as TableComment
                FROM information_schema.TABLES
                WHERE TABLE_SCHEMA = '{0}'
                AND TABLE_NAME in ({1})
                ORDER BY CREATE_TIME", this.DataBaseName, stringBuilder.ToString().Substring(1));
                stringBuilder.Clear();
            }
            DataTable UseTable = new DataTable("UseTable");
            UseTable = mySqlDb.GetDataTable(Str);
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
                    case "Class_DeleteAllModel":
                        OperateType = "delete";
                        break;
                    case "Class_UpdateAllModel":
                        OperateType = "update";
                        break;
                    case "Class_InsertAllModel":
                        OperateType = "insert";
                        break;
                    default:
                        break;
                }
                this.class_AllModel = class_AllModel;
            }
        }

        public string GetJavaType(string dataBaseFieldType)
        {
            if ((class_MySqlFieldAndJavas == null) || (class_MySqlFieldAndJavas.Count == 0) || (dataBaseFieldType == null))
                return null;
            int Index = class_MySqlFieldAndJavas.FindIndex(a => a.DataBaseFieldType.ToUpper().Equals(dataBaseFieldType.ToUpper()));
            if (Index > -1)
                return string.Format("{0}", class_MySqlFieldAndJavas[Index].JavaType);
            else
                return null;
        }
        /// <summary>
        /// 导出数据库说明书
        /// </summary>
        /// <param name="DataBaseFileName"></param>
        /// <returns></returns>
        public string GetDataBaseContent()
        {
            try
            {
                string DataBaseFileName = string.Format("{0}数据库说明书{1}", this.DataBaseName, System.DateTime.Now.ToString("yyyyMMdd"));
                SaveFileDialog SaveFileDialog = new SaveFileDialog();
                SaveFileDialog.FileName = DataBaseFileName;
                SaveFileDialog.Filter = "EXCEL|*.xlsx";
                SaveFileDialog.Title = "生成数据库说明书";
                if (SaveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Class_ToExcel class_ToExcel = new Class_ToExcel();
                    Class_DataBaseContent class_DataBaseContent = new Class_DataBaseContent();
                    class_DataBaseContent.DataBaseFileName = DataBaseFileName;
                    //1：得到所有用户列表信息，包括：表名、表注释；
                    List<Class_TableInfo> class_TableInfos = new List<Class_TableInfo>();
                    class_TableInfos = GetUseTableList(null);
                    if ((class_TableInfos != null) && (class_TableInfos.Count > 0))
                    {
                        #region 得到所有表信息
                        DataTable dataTable = new DataTable("TableInfo");
                        DataColumn TableIndex = new DataColumn("TableIndex", typeof(int));
                        DataColumn TableName = new DataColumn("TableName", typeof(string));
                        DataColumn TableComment = new DataColumn("TableComment", typeof(string));
                        dataTable.Columns.Add(TableIndex);
                        dataTable.Columns.Add(TableName);
                        dataTable.Columns.Add(TableComment);

                        int TableCount = 1;
                        foreach (Class_TableInfo class_TableInfo in class_TableInfos)
                        {
                            DataRow newRow = dataTable.NewRow();
                            newRow["TableIndex"] = TableCount++;
                            newRow["TableName"] = class_TableInfo.TableName;
                            newRow["TableComment"] = class_TableInfo.TableComment;
                            //TableCount++;
                            dataTable.Rows.Add(newRow);
                        }
                        Class_SheetContent class_SheetContent = new Class_SheetContent();
                        class_SheetContent.dataTable = dataTable;
                        class_SheetContent.FieldTitleList.Add("序号");
                        class_SheetContent.FieldTitleList.Add("表名");
                        class_SheetContent.FieldTitleList.Add("注释");
                        class_SheetContent.LeftFieldNameList.Add("TableComment");
                        class_SheetContent.SheetName = "用户表";
                        class_SheetContent.SheetTitle = "用户表";
                        class_SheetContent.TableContent = string.Format("{0}库的所有用户表", this.DataBaseName);
                        class_DataBaseContent.class_SheetContents.Add(class_SheetContent);
                        #endregion

                        //2：根据表名，得到表字段信息；
                        TableCount = 1;
                        foreach (Class_TableInfo class_TableInfo in class_TableInfos)
                        {
                            dataTable = new DataTable(class_TableInfo.TableName + TableCount.ToString());
                            dataTable = _GetTableStruct(class_TableInfo.TableName);
                            Class_SheetContent class_SheetContentRow = new Class_SheetContent();
                            class_SheetContentRow.dataTable = dataTable;
                            class_SheetContentRow.FieldTitleList.Add("字段名");
                            class_SheetContentRow.FieldTitleList.Add("注释");
                            class_SheetContentRow.FieldTitleList.Add("字段类型");
                            class_SheetContentRow.FieldTitleList.Add("字段长度");
                            class_SheetContentRow.FieldTitleList.Add("默认值");
                            class_SheetContentRow.FieldTitleList.Add("是否可为空");
                            class_SheetContentRow.FieldTitleList.Add("主键");
                            class_SheetContentRow.FieldTitleList.Add("自增");
                            class_SheetContentRow.LeftFieldNameList.Add("FieldRemark");
                            class_SheetContentRow.SheetName = TableCount.ToString();
                            class_SheetContentRow.SheetTitle = class_TableInfo.TableName;
                            class_SheetContentRow.TableContent = class_TableInfo.TableComment;
                            class_DataBaseContent.class_SheetContents.Add(class_SheetContentRow);
                            TableCount++;
                        }
                        dataTable.Dispose();
                        DataBaseFileName = class_ToExcel.GetDataBaseContent(class_DataBaseContent, SaveFileDialog.FileName);
                    }
                    else
                        DataBaseFileName = null;
                }
                else
                {
                    DataBaseFileName = null;
                }
                SaveFileDialog.Dispose();
                return DataBaseFileName;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 返回Like查询条件字符串
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="Type">-1：左LIKE，0：全LIKE，1：右LIKE</param>
        /// <returns></returns>
        public string GetLikeString(string FieldName, int Type)
        {
            String ResultValue = null;
            if (FieldName != null)
            {
                switch (Type)
                {
                    case -1:
                        ResultValue = String.Format("CONCAT(\'%\',{0})", FieldName);
                        break;
                    case 0:
                        ResultValue = String.Format("CONCAT(\'%\',{0},\'%\')", FieldName);
                        break;
                    case 1:
                        ResultValue = String.Format("CONCAT({0},\'%\')", FieldName);
                        break;
                    default:
                        ResultValue = String.Format("CONCAT(\'%\',{0})", FieldName);
                        break;
                }
            }
            return ResultValue;
        }
        /// <summary>
        /// 是否为聚合函数
        /// </summary>
        /// <param name="FunctionName"></param>
        /// <returns></returns>
        public bool IsPolymerization(string FunctionName)
        {
            bool IsFinder = false;
            if (FunctionName != null && FunctionName.Length > 0)
            {
                if (FunctionName.IndexOf("COUNT") > -1)
                    IsFinder = true;
                if (FunctionName.IndexOf("SUM") > -1)
                    IsFinder = true;
                if (FunctionName.IndexOf("AVG") > -1)
                    IsFinder = true;
                if (FunctionName.IndexOf("MAX") > -1)
                    IsFinder = true;
                if (FunctionName.IndexOf("MIN") > -1)
                    IsFinder = true;
            }
            return IsFinder;
        }
        /// <summary>
        /// 字段类型合法性验证
        /// </summary>
        /// <param name="FieldType"></param>
        /// <param name="FunctionName"></param>
        /// <returns></returns>
        public bool FieldTypeAndFunction(string FieldType, string FunctionName)
        {
            bool IsFinder = true;
            if (FunctionName == null || FunctionName.Length == 0)
                return IsFinder;

            switch (FieldType)
            {
                case "bigint":
                case "integer":
                case "smallint":
                case "int":
                case "tinyint":
                case "float":
                case "decimal":
                    if (FunctionName.IndexOf("DATE_FORMAT") > -1)
                        IsFinder = false;
                    break;
                case "char":
                case "text":
                case "varchar":
                    if (FunctionName.IndexOf("DATE_FORMAT") > -1)
                        IsFinder = false;
                    if (FunctionName.IndexOf("SUM") > -1)
                        IsFinder = false;
                    if (FunctionName.IndexOf("AVG") > -1)
                        IsFinder = false;
                    if (FunctionName.IndexOf("MAX") > -1)
                        IsFinder = false;
                    if (FunctionName.IndexOf("MIN") > -1)
                        IsFinder = false;
                    break;
                case "datetime":
                case "date":
                case "time":
                    break;
                case "bit":
                    if (FunctionName.IndexOf("DATE_FORMAT") > -1)
                        IsFinder = false;
                    break;
                default:
                    break;
            }
            return IsFinder;
        }

        public List<string> GetTotalFunctionList(string FieldType)
        {
            List<string> vs = new List<string>();
            switch (FieldType)
            {
                case "int":
                case "tinyint":
                case "decimal":
                    vs.Add("SUM(?)");
                    vs.Add("MAX(?)");
                    vs.Add("MIN(?)");
                    vs.Add("AVG(?)");
                    break;
                case "date":
                case "datetime":
                    vs.Add("MAX(?)");
                    vs.Add("MIN(?)");
                    break;
                default:
                    vs.Add("SUM(?)");
                    vs.Add("MAX(?)");
                    vs.Add("MIN(?)");
                    vs.Add("AVG(?)");
                    break;
            }
            return vs;
        }

        public string GetAlign(string FieldType)
        {
            string ResultValue = "center";
            switch (FieldType)
            {
                case "int":
                case "tinyint":
                case "decimal":
                case "float":
                    ResultValue = "right";
                    break;
                default:
                    ResultValue = "center";
                    break;
            }
            return ResultValue;
        }
    }
}

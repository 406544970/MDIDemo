using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MDIDemo.PublicClass.Class_SelectAllModel;

namespace MDIDemo.PublicClass
{
    public class Class_MySqlDataBase : Class_InterFaceDataBase
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
        public Class_MySqlDataBase()
        {
            IniFieldTypeChange();
        }
        public Class_MySqlDataBase(string ip, string dataBaseName, string userName, string passWord, int port)
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
                    vs.Add("COUNT(?)");
                    vs.Add("COUNT(DISTINCT ?)");
                    vs.Add("SUM(?)");
                    vs.Add("MAX(?)");
                    vs.Add("MIN(?)");
                    vs.Add("AVG(?)"); 
                    vs.Add("DATE_FORMAT(?,'%Y-%m-%d')");
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

        public DataTable GetMainTableStruct(string TableName, int PageSelectIndex)
        {
            return _GetMainTableStruct(TableName, PageSelectIndex);
        }
        private DataTable _GetTableStruct(string TableName)
        {
            DataTable dataTable = new DataTable();
            DataColumn FieldName = new DataColumn("FieldName", typeof(string));
            DataColumn FieldRemark = new DataColumn("FieldRemark", typeof(string));
            DataColumn FieldType = new DataColumn("FieldType", typeof(string));
            DataColumn FieldLength = new DataColumn("FieldLength", typeof(Int32));
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
cList.COLUMN_COMMENT AS FieldRemark,
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
        private DataTable _GetMainTableStruct(string TableName, int PageSelectIndex)
        {
            DataTable dataTable = new DataTable();
            //占用字节数 = a.length 
            //小数位数 = isnull(columnproperty(a.id, a.name, 'Scale'), 0) 
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
                        else
                        {
                            row["ParaName"] = Class_Tool.GetFirstCodeLow(row["FieldName"].ToString());
                            row["MaxLegth"] = row["FieldLength"];
                            row["ReturnType"] = row["FieldType"];
                            row["SortNo"] = Counter++;
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

        public List<string> GetUseTableList()
        {
            List<string> vs = new List<string>();
            string Str = string.Format(@"SELECT TABLE_NAME AS TableName
                FROM information_schema.TABLES
                WHERE TABLE_SCHEMA = '{0}'
                ORDER BY CREATE_TIME", this.DataBaseName);
            DataTable UseTable = new DataTable("UseTable");
            UseTable = mySqlDb.GetDataTable(Str);
            foreach (DataRow row in UseTable.Rows)
            {
                vs.Add(row["TableName"].ToString());
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

        public string GetJavaType(string dataBaseFieldType)
        {
            if ((class_MySqlFieldAndJavas == null) || (class_MySqlFieldAndJavas.Count == 0))
                return null;
            int Index = class_MySqlFieldAndJavas.FindIndex(a => a.DataBaseFieldType.ToUpper().Equals(dataBaseFieldType.ToUpper()));
            if (Index > -1)
                return string.Format("{0}", class_MySqlFieldAndJavas[Index].JavaType);
            else
                return null;
        }
    }
}

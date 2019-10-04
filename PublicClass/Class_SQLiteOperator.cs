using MDIDemo.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDIDemo.PublicClass
{
    public class Class_SQLiteOperator
    {
        public Class_SQLiteOperator()
        {
            mySqlite3 = new MySqlite3();
        }
        private MySqlite3 mySqlite3;
        private bool _DeleteByPageKey(string XmlFileKey)
        {
            string Sql = string.Format(@"DELETE
                FROM vou_pageInfomation
                WHERE pageKey = '{0}'
                And pushSign = 1"
                , XmlFileKey);
            return mySqlite3.ExecuteSql(Sql) == 1 ? true : false;
        }
        private bool _UpdateIntoPageKey(Class_PageInfomationMode class_PageInfomationMode)
        {
            string Sql = string.Format(@"UPDATE vou_pageInfomation
                SET projectId = '{1}',pageType = '{2}',
                pageVersion = case pageVersion when 100000 then 0 else pageVersion + 1 end
                ,lastUpdateTime = datetime('now','localtime')
                ,createOperatorId = '{3}'
                ,doOperatorId = '{4}'
                ,finishCount = case finishCount when 100000 then 0 else finishCount + 1 end,
                methodRemark = {5}
                ,readOnly = {6}
                ,frontOperatorId = '{7}'
                ,createOperator = '{8}'
                ,doOperator = '{9}'
                ,frontOperator = '{10}'
                WHERE pageKey = '{0}'"
                , class_PageInfomationMode.pageKey
                , class_PageInfomationMode.projectId
                , class_PageInfomationMode.pageType
                , class_PageInfomationMode.createOperatorId
                , class_PageInfomationMode.doOperatorId
                , (class_PageInfomationMode.methodRemark == null ? "null" : "'" + class_PageInfomationMode.methodRemark + "'")
                , (class_PageInfomationMode.readOnly ? 1 : 0)
                , class_PageInfomationMode.frontOperatorId
                , class_PageInfomationMode.createOperator
                , class_PageInfomationMode.doOperator
                , class_PageInfomationMode.frontOperator
                );
            return mySqlite3.ExecuteSql(Sql) == 1 ? true : false;
        }
        private string _GetInsertSql(string pageKey, string projectId, string pageType, int pageVersion
            , DateTime createTime, DateTime lastUpdateTime
            , string createOperatorId, string doOperatorId, int finishCount
            , string methodRemark, bool readOnly
            , string frontOperatorId, string createOperator, string doOperator, string frontOperator)
        {
            return string.Format(@"INSERT INTO vou_pageInfomation(pageKey, projectId, 
pageType, pageVersion, createTime, lastUpdateTime,
createOperatorId, doOperatorId, finishCount, methodRemark, readOnly, frontOperatorId
, createOperator, doOperator, frontOperator, pushSign)
VAlUES('{0}', '{1}', '{2}',{3},'{4}','{5}','{6}','{7}',{8},{9},{10},'{11}','{12}','{13}','{14}',0)"
                , pageKey
                , projectId, pageType, pageVersion
                , createTime.ToString("yyyy-MM-dd HH:mm:ss")
                , lastUpdateTime.ToString("yyyy-MM-dd HH:mm:ss")
                , createOperatorId
                , doOperatorId, finishCount
                , (methodRemark == null ? "null" : "'" + methodRemark + "'")
                , (readOnly ? 1 : 0)
                , frontOperatorId, createOperator, doOperator, frontOperator);
        }
        private bool _InsertIntoPageKey(Class_PageInfomationMode class_PageInfomationMode)
        {
            string Sql = string.Format(@"INSERT INTO vou_pageInfomation(pageKey, projectId, 
pageType, pageVersion, createTime, lastUpdateTime, 
createOperatorId, doOperatorId, finishCount, methodRemark,readOnly,frontOperatorId
,createOperator, doOperator,frontOperator,pushSign)
VAlUES('{0}','{1}','{2}',{3},'{4}','{5}','{6}','{7}',{8},{9},{10},'{11}','{12}','{13}','{14}',0)"
, class_PageInfomationMode.pageKey, class_PageInfomationMode.projectId
, class_PageInfomationMode.pageType, class_PageInfomationMode.pageVersion
, class_PageInfomationMode.createTime.ToString("yyyy-MM-dd HH:mm:ss")
, class_PageInfomationMode.lastUpdateTime.ToString("yyyy-MM-dd HH:mm:ss")
, class_PageInfomationMode.createOperatorId, class_PageInfomationMode.doOperatorId
, class_PageInfomationMode.finishCount, (class_PageInfomationMode.methodRemark == null ? "null" : "'" + class_PageInfomationMode.methodRemark + "'")
, (class_PageInfomationMode.readOnly ? 1 : 0)
, class_PageInfomationMode.frontOperatorId
, class_PageInfomationMode.createOperator
, class_PageInfomationMode.doOperator
, class_PageInfomationMode.frontOperator
);
            return mySqlite3.ExecuteSql(Sql) == 1 ? true : false;
        }
        private int _GetPageKeyCount(string PageKey)
        {
            string Sql = string.Format(@"SELECT COUNT(pageKey) as counter
                FROM vou_pageInfomation
                WHERE pageKey = '{0}'", PageKey);
            object ReturnValue = mySqlite3.GetSingle(Sql);
            if (ReturnValue != null)
                return Convert.ToInt32(ReturnValue);
            else
                return -1;
        }
        public List<string> GetComponentList()
        {
            string Sql = string.Format(@"SELECT id as ComponentName
                FROM cp_ComponentFrontPage
                ORDER BY sortNo");
            return mySqlite3.ExecuteReadList(Sql);
        }
        public List<string> GetLocalPageList()
        {
            string Sql = string.Format(@"SELECT pageKey,pageVersion,pageType
                FROM vou_pageInfomation
                WHERE pushSign = 1
                ORDER BY createTime");
            return mySqlite3.ExecuteReadList(Sql);
        }
        public Class_LogRemamber GetLogRemember()
        {
            Class_LogRemamber class_LogRemamber = new Class_LogRemamber();
            List<string> vs = new List<string>();
            vs = mySqlite3.ExecuteReadList("SELECT * FROM inf_log");
            if (vs != null)
            {
                foreach (string row in vs)
                {
                    string[] rowList = row.Split(';');
                    class_LogRemamber.UseName = rowList[0];
                    class_LogRemamber.PassWord = rowList[1];
                    class_LogRemamber.RememberSign = Convert.ToInt32(rowList[2]) == 1 ? true : false;
                    class_LogRemamber.AutoLog = Convert.ToInt32(rowList[3]) == 1 ? true : false;
                }
                return class_LogRemamber;
            }
            else
                return null;

        }
        public bool UpdateLogInfo(string UseName, string PassWord, bool RememberSign, bool AutoLog)
        {
            bool ResultValue = false;
            string Sql = null;
            if (PassWord == null)
                Sql = string.Format(@"UPDATE inf_log
                set passWord = null
                ,rememberSign = {1}
                ,autoLog= {2}
                WHERE useName = '{0}'"
                    , UseName
                    , RememberSign ? 1 : 0
                    , AutoLog ? 1 : 0);
            else
                Sql = string.Format(@"UPDATE inf_log
                set passWord = '{1}'
                ,rememberSign = {2}
                ,autoLog= {3}
                WHERE useName = '{0}'"
                    , UseName
                    , PassWord
                    , RememberSign ? 1 : 0
                    , AutoLog ? 1 : 0);
            if (mySqlite3.ExecuteSql(Sql) > 0)
                ResultValue = true;
            else
            {
                if (PassWord == null)
                    Sql = string.Format(@"INSERT INTO inf_log
                        VALUES('{0}',null,{1},{2})"
                    , UseName
                    , RememberSign ? 1 : 0
                    , AutoLog ? 1 : 0);
                else
                    Sql = string.Format(@"INSERT INTO inf_log
                        VALUES('{0}','{1}',{2},{3})"
                    , UseName
                    , PassWord
                    , RememberSign ? 1 : 0
                    , AutoLog ? 1 : 0);
                if (mySqlite3.ExecuteSql(Sql) > 0)
                    ResultValue = true;
            }
            Sql = string.Format(@"DELETE FROM inf_log
                WHERE UseName != '{0}'", UseName);
            mySqlite3.ExecuteSql(Sql);
            return ResultValue;
        }
        /// <summary>
        /// 得到表别名
        /// </summary>
        /// <param name="Ip"></param>
        /// <param name="DataBaseName"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public string GetTableAlias(string Ip, string DataBaseName, string TableName)
        {
            string Sql = string.Format(@"SELECT tableAlias
                From sys_dataBaseAlias
                WHERE ip = '{0}'
                AND dataBaseName = '{1}'
                AND tableName = '{2}'
                ", Ip, DataBaseName, TableName);
            object ReturnValue = mySqlite3.GetSingle(Sql);
            if (ReturnValue != null)
                return ReturnValue.ToString();
            else
                return "";
        }
        public bool DeleteByPageKey(string XmlFileKey)
        {
            return _DeleteByPageKey(XmlFileKey);
        }
        public List<Class_WindowType> GetWindowTypes()
        {
            List<Class_WindowType> class_WindowTypes = new List<Class_WindowType>();
            List<string> vs = new List<string>();
            vs = mySqlite3.ExecuteReadList("Select XmlFileName,WindowType,ActiveSign From vou_CurrentOpenWin order by SortNo");
            if (vs != null)
            {
                foreach (string row in vs)
                {
                    string[] rowList = row.Split(';');
                    Class_WindowType class_WindowType = new Class_WindowType()
                    {
                        XmlFileName = rowList[0],
                        WindowType = rowList[1],
                        ActiveSign = Convert.ToInt32(rowList[2]) == 1 ? true : false
                    };
                    class_WindowTypes.Add(class_WindowType);
                }
            }
            return class_WindowTypes;
        }
        /// <summary>
        /// 保存当前打开窗口状态
        /// </summary>
        /// <param name="class_WindowTypes"></param>
        /// <returns></returns>
        public int SaveCurrentOpenWin(List<Class_WindowType> class_WindowTypes)
        {
            int ResultCounter = 0;
            int Counter = 1;
            //1：删除数据库vou_CurrentOpenWin
            mySqlite3.ExecuteSql("delete from vou_CurrentOpenWin");
            if ((class_WindowTypes != null) && (class_WindowTypes.Count > 0))
            {
                //2：insert into
                foreach (Class_WindowType class_WindowType in class_WindowTypes)
                {
                    if (class_WindowType.XmlFileName != null && class_WindowType.XmlFileName.Length > 0)
                    {
                        string Sql = string.Format(@"Insert into vou_CurrentOpenWin(XmlFileName,WindowType,ActiveSign,SortNo)
                        values('{0}','{1}',{2},{3})"
                            , class_WindowType.XmlFileName
                            , class_WindowType.WindowType
                            , class_WindowType.ActiveSign ? 1 : 0
                            , Counter++);
                        ResultCounter += mySqlite3.ExecuteSql(Sql);
                    }
                }
            }
            return ResultCounter;
        }
        public bool InsertIntoPageKey(Class_PageInfomationMode class_PageInfomationMode)
        {
            if (_GetPageKeyCount(class_PageInfomationMode.pageKey) == 0)
                return _InsertIntoPageKey(class_PageInfomationMode);
            else
                return _UpdateIntoPageKey(class_PageInfomationMode);
        }
        /// <summary>
        /// 测试SQLite是否工作正常，用于热身工作池
        /// </summary>
        /// <returns></returns>
        public bool IsOk()
        {
            string Sql = @"SELECT COUNT(*) as counter
                FROM Sys_Version";
            object ReturnValue = mySqlite3.GetSingle(Sql);
            if (ReturnValue != null)
                return true;
            else
                return false;
        }

        public DataSet GetAllWindowInfomation()
        {
            int Index = 0;
            string Sql = @"SELECT pageKey,lastUpdateTime
                ,createOperator as createOperatorId
                ,doOperator as doOperatorId
                ,readOnly,methodRemark
                ,frontOperator as frontOperatorId
                FROM vou_pageInfomation
                WHERE pageType = '{0}'
                ORDER BY createTime DESC";
            DataSet dataSet = new DataSet();
            DataTable BeginTable = new DataTable("SelectTable");
            DataColumn pageKey = new DataColumn("pageKey", typeof(string));
            DataColumn lastUpdateTime = new DataColumn("lastUpdateTime", typeof(DateTime));
            DataColumn createOperatorId = new DataColumn("createOperator", typeof(string));
            DataColumn doOperatorId = new DataColumn("doOperator", typeof(string));
            DataColumn readOnly = new DataColumn("readOnly", typeof(bool));
            DataColumn methodRemark = new DataColumn("methodRemark", typeof(string));
            DataColumn frontOperatorId = new DataColumn("frontOperator", typeof(string));
            BeginTable.Columns.Add(pageKey);
            BeginTable.Columns.Add(lastUpdateTime);
            BeginTable.Columns.Add(createOperatorId);
            BeginTable.Columns.Add(doOperatorId);
            BeginTable.Columns.Add(readOnly);
            BeginTable.Columns.Add(methodRemark);
            BeginTable.Columns.Add(frontOperatorId);

            while (Index < 4)
            {
                List<string> vs = new List<string>();
                string NewSql;
                switch (Index)
                {
                    case 0:
                        NewSql = string.Format(Sql, "select");
                        break;
                    case 1:
                        NewSql = string.Format(Sql, "insert");
                        break;
                    case 2:
                        NewSql = string.Format(Sql, "update");
                        break;
                    case 3:
                        NewSql = string.Format(Sql, "delete");
                        break;
                    default:
                        NewSql = string.Format(Sql, "select");
                        break;
                }
                vs = mySqlite3.ExecuteReadList(NewSql);
                BeginTable.Clear();
                if ((vs != null) && (vs.Count > 0))
                {
                    foreach (string row in vs)
                    {
                        DataRow NewRow = BeginTable.NewRow();
                        string[] RowList = row.Split(';');
                        NewRow["pageKey"] = RowList[0];
                        NewRow["lastUpdateTime"] = Convert.ToDateTime(RowList[1]);
                        NewRow["createOperator"] = RowList[2];
                        NewRow["doOperator"] = RowList[3];
                        NewRow["readOnly"] = (RowList[4] == "1" ? true : false);
                        NewRow["methodRemark"] = RowList[5];
                        NewRow["frontOperator"] = RowList[6];
                        BeginTable.Rows.Add(NewRow);
                    }
                    BeginTable.AcceptChanges();
                }
                DataTable dataTable = new DataTable();
                dataTable = BeginTable.Copy();
                dataTable.TableName = string.Format("Table{0}", ++Index);
                dataSet.Tables.Add(dataTable);
            }
            return dataSet;
        }

    }
}

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

        private bool _UpdateIntoPageKey(Class_PageInfomationMode class_PageInfomationMode)
        {
            string Sql = string.Format(@"UPDATE vou_pageInfomation
                SET projectId = '{1}',pageType = '{2}',
                pageVersion = pageVersion + 1,lastUpdateTime = '{3}',
                createOperatorId = '{4}',doOperatorId = '{5}',finishCount = {6},
                methodRemark = {7},readOnly = {8}
                WHERE pageKey = '{0}'"
                , class_PageInfomationMode.pageKey, class_PageInfomationMode.projectId
                , class_PageInfomationMode.pageType
                , class_PageInfomationMode.lastUpdateTime.ToString("yyyy-MM-dd HH:mm:ss")
                , class_PageInfomationMode.createOperatorId, class_PageInfomationMode.doOperatorId
                , class_PageInfomationMode.finishCount
                , (class_PageInfomationMode.methodRemark == null ? "null" : "'" + class_PageInfomationMode.methodRemark + "'")
                , (class_PageInfomationMode.readOnly ? 1 : 0));
            return mySqlite3.ExecuteSql(Sql) == 1 ? true : false;
        }
        private bool _InsertIntoPageKey(Class_PageInfomationMode class_PageInfomationMode)
        {
            string Sql = string.Format(@"INSERT INTO vou_pageInfomation(pageKey, projectId, 
pageType, pageVersion, createTime, lastUpdateTime, 
createOperatorId, doOperatorId, finishCount, methodRemark,readOnly)
VAlUES('{0}','{1}','{2}',{3},'{4}','{5}','{6}','{7}',{8},{9},{10})"
, class_PageInfomationMode.pageKey, class_PageInfomationMode.projectId
, class_PageInfomationMode.pageType, class_PageInfomationMode.pageVersion
, class_PageInfomationMode.createTime.ToString("yyyy-MM-dd HH:mm:ss")
, class_PageInfomationMode.lastUpdateTime.ToString("yyyy-MM-dd HH:mm:ss")
, class_PageInfomationMode.createOperatorId, class_PageInfomationMode.doOperatorId
, class_PageInfomationMode.finishCount, (class_PageInfomationMode.methodRemark == null ? "null" : "'" + class_PageInfomationMode.methodRemark + "'")
, (class_PageInfomationMode.readOnly ? 1 : 0));
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
        public List<Class_WindowType> GetWindowTypes()
        {
            List<Class_WindowType> class_WindowTypes = new List<Class_WindowType>();
            List<string> vs = new List<string>();
            vs = mySqlite3.ExecuteReadList("Select XmlFileName,WindowType,ActiveSign From vou_CurrentOpenWin order by SortNo");
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
                    string Sql = string.Format(@"Insert into vou_CurrentOpenWin(XmlFileName,WindowType,ActiveSign,SortNo)
                        values('{0}','{1}',{2},{3})"
                        , class_WindowType.XmlFileName
                        , class_WindowType.WindowType
                        , class_WindowType.ActiveSign ? 1 : 0
                        , Counter++);
                    ResultCounter += mySqlite3.ExecuteSql(Sql);
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
            string Sql = @"SELECT pageKey,lastUpdateTime,createOperatorId,doOperatorId,readOnly,methodRemark
                FROM vou_pageInfomation
                WHERE projectId = 'projectId'
                AND pageType = '{0}'
                ORDER BY createTime DESC";
            DataSet dataSet = new DataSet();
            DataTable BeginTable = new DataTable("SelectTable");
            DataColumn pageKey = new DataColumn("pageKey", typeof(string));
            DataColumn lastUpdateTime = new DataColumn("lastUpdateTime", typeof(DateTime));
            DataColumn createOperatorId = new DataColumn("createOperatorId", typeof(string));
            DataColumn doOperatorId = new DataColumn("doOperatorId", typeof(string));
            DataColumn readOnly = new DataColumn("readOnly", typeof(bool));
            DataColumn methodRemark = new DataColumn("methodRemark", typeof(string));
            BeginTable.Columns.Add(pageKey);
            BeginTable.Columns.Add(lastUpdateTime);
            BeginTable.Columns.Add(createOperatorId);
            BeginTable.Columns.Add(doOperatorId);
            BeginTable.Columns.Add(readOnly);
            BeginTable.Columns.Add(methodRemark);

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
                        NewRow["createOperatorId"] = RowList[2];
                        NewRow["doOperatorId"] = RowList[3];
                        NewRow["readOnly"] = (RowList[4] == "1" ? true : false);
                        NewRow["methodRemark"] = RowList[5];
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

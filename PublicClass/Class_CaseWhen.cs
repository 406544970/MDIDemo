using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDIDemo.PublicClass
{
    public class Class_CaseWhen
    {
        public Class_CaseWhen()
        {
            mySqlite3 = new MySqlite3();
        }
        private MySqlite3 mySqlite3;
        /// <summary>
        /// 得到所有CASE列表（主表）
        /// </summary>
        /// <returns></returns>
        public DataTable GetCaseMainList()
        {
            string Sql = @"SELECT *
                FROM vou_caseWhenMain
                ORDER BY id";
            List<string> vs = new List<string>();
            vs = mySqlite3.ExecuteReadList(Sql);
            if (vs.Count > 0)
            {
                DataTable dataTable = new DataTable();
                DataColumn id = new DataColumn("id", typeof(string));
                DataColumn whenTypeSign = new DataColumn("whenTypeSign", typeof(bool));
                DataColumn thenTypeSign = new DataColumn("thenTypeSign", typeof(bool));
                DataColumn logicSymbol = new DataColumn("logicSymbol", typeof(string));
                dataTable.Columns.Add(id);
                dataTable.Columns.Add(whenTypeSign);
                dataTable.Columns.Add(thenTypeSign);
                dataTable.Columns.Add(logicSymbol);
                foreach (string row in vs)
                {
                    string[] rowArray = row.Split(';');
                    DataRow newRow = dataTable.NewRow();
                    newRow["id"] = rowArray[0];
                    newRow["whenTypeSign"] = rowArray[1].Equals("1") ? true : false;
                    newRow["thenTypeSign"] = rowArray[2].Equals("1") ? true : false;
                    newRow["logicSymbol"] = rowArray[3];
                    dataTable.Rows.Add(newRow);
                }
                dataTable.AcceptChanges();
                vs.Clear();
                return dataTable;
            }
            else
                return null;
        }
        public string GetCaseWhenContent(string CaseWhenId, string FieldName, string Space)
        {
            string Sql = string.Format(@"SELECT main.whenTypeSign
,main.thenTypeSign
,main.logicSymbol
,sub.whenContent
,sub.thenContent
FROM vou_caseWhenMain as main
INNER JOIN vou_caseWhenSub as sub on main.id = sub.mainId
WHERE sub.stopSign = '0'
AND main.id = '{0}'
ORDER BY sub.sortNo
", CaseWhenId);
            List<string> vs = new List<string>();
            vs = mySqlite3.ExecuteReadList(Sql);
            if ((vs != null) && (vs.Count > 0))
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("CASE {0}\r\n", FieldName);
                bool whenTypeSign = false;
                bool thenTypeSign = false;
                for (int index = 0; index < vs.Count; index++)
                {
                    string WhenOrElse = "ELSE";
                    string logicSymbol;
                    string whenContent;
                    string thenContent;
                    string[] rowList = vs[index].Split(';');
                    if (index == 0)
                    {
                        whenTypeSign = rowList[0].Equals("1") ? true : false;
                        thenTypeSign = rowList[1].Equals("1") ? true : false;
                        logicSymbol = rowList[2];
                    }
                    whenContent = rowList[3];
                    thenContent = rowList[4];
                    if (index < vs.Count - 1)
                    {
                        WhenOrElse = "WHEN";
                    }
                    if (whenTypeSign)
                        stringBuilder.AppendFormat("{0}{2} '{1}'", Space, whenContent, WhenOrElse);
                    else
                        stringBuilder.AppendFormat("{0}{2} {1}", Space, whenContent, WhenOrElse);
                    if (thenTypeSign)
                        stringBuilder.AppendFormat(" THEN '{0}'\r\n", thenContent);
                    else
                        stringBuilder.AppendFormat(" THEN {0}\r\n", thenContent);
                }
                stringBuilder.AppendFormat("{0}END", Space);
                vs.Clear();
                if (stringBuilder.Length > 0)
                    return stringBuilder.ToString();
                else
                    return null;
            }
            else
            {
                return FieldName;
            }
        }
    }
}

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MDIDemo.PublicClass
{
    public class Mydb
    {
        #region the private object

        private static string strDBName = "";        //数据库名
        private static string strServerName = "";    //服务器名
        private static string strUserName = "";      //数据库用户名
        private static string strPWD = "";           //访问密码

        /// <summary>
        /// the Transcation is Open
        /// </summary>
        private bool m_TransOpen;

        /// <summary>
        /// 获取事务开启状态
        /// </summary>
        public bool a_TransOpen
        {
            get { return m_TransOpen; }
        }

        /// <summary>
        /// 获取事务开启状态
        /// </summary>
        public string a_str_Connection
        {
            get { return m_str_Connection; }
        }

        /// <summary>
        /// the SqlConeection string
        /// </summary>
        private string m_str_Connection = "";

        /// <summary>
        /// the SqlConnection object
        /// </summary>
        private SqlConnection m_SqlConnection;

        /// <summary>
        /// the sqlcommand object 
        /// </summary>
        private SqlCommand m_SqlCommand;

        /// <summary>
        /// the SqlConnecton Timeout Time (second)
        /// </summary>
        public static int m_int_TimeOut = 1000;

        /// <summary>
        /// the SqlTranscation object
        /// </summary>
        private SqlTransaction m_SqlTransaction;

        /// <summary>
        /// the object of SqlDataAdapter
        /// </summary>
        private SqlDataAdapter m_SqlDataAdapter;

        /// <summary>
        /// the object of sqlDataReader
        /// </summary>
        private SqlDataReader m_SqlDataReader;

        /// <summary>
        /// 自带数据集
        /// </summary>
        private DataSet m_DataSet_Server;

        /// <summary>
        /// the DataTable object
        /// </summary>
        private DataTable m_DataTable_Server;
        #endregion

        #region Constract function

        /// <summary>
        /// the class of Database Contract function
        /// </summary>
        /// <param name="str_SqlConnection">ths SqlConnection string</param>
        public Mydb(string str_SqlConnection)
        {
            InitMydb(str_SqlConnection);
        }

        /// <summary>
        /// the class of Database Contract function
        /// </summary>
        public Mydb()
        {
            InitMydb("");
        }

        /// <summary>
        /// the function of instaniation the mybd class
        /// </summary>
        private void InitMydb(string str_SqlConnection)
        {
            //check the SqlConnection string is get the ini or other,"" is get ini 
            if (str_SqlConnection == "")
            {
                //get ServerName,DatabaseName,UserName,PWD from ini File

                //set the Sql connection and Instantiation the Database Class
                pri_GetSqlConString();
            }
            else
                m_str_Connection = str_SqlConnection;

            //set the sqlconnection 
            pri_SetSqlConnection();
        }

        #endregion

        #region private function about the ADO Object

        /// <summary>
        /// set the object of SqlConnetion
        /// </summary>
        private void pri_SetSqlConnection()
        {
            m_SqlConnection = new SqlConnection(m_str_Connection);
        }

        /// <summary>
        /// get the SqlConnection string [m_str_connection]
        /// </summary>
        private void pri_GetSqlConString()
        {
            if (strServerName == null)
                strServerName = "";
            if (strDBName == null)
                strDBName = "";
            if (strUserName == null)
                strUserName = "";
            if (strPWD == null)
                strPWD = "";
            m_str_Connection = "Data Source=" + strServerName + ";Initial Catalog=" + strDBName + ";Persist Security Info=True;User ID=" + strUserName + ";Password=" + strPWD;
        }


        /// <summary>
        /// open the sqlconnection 
        /// </summary>
        private void pri_OpenSqlConnection()
        {
            if (m_SqlConnection.State.ToString() != "Open")
                m_SqlConnection.Open();
        }

        /// <summary>
        /// close the sqlconnection
        /// </summary>
        private void pri_CloseSqlConnection()
        {
            if (m_SqlConnection.State.ToString() == "Open" && m_TransOpen == false)
                m_SqlConnection.Close();
        }

        /// <summary>
        /// get the sqlCommand object [m_sqlCommand]
        /// </summary>
        private void pri_GetSqlCommand()
        {
            m_SqlCommand = new SqlCommand();
            m_SqlCommand.Connection = m_SqlConnection;
        }

        /// <summary>
        /// get the sqlCommand object [m_sqlCommand]
        /// </summary>
        /// <param name="strCommandText">CommandText</param>
        private void pri_GetSqlCommand(string strCommandText)
        {
            m_SqlCommand = new SqlCommand(strCommandText, m_SqlConnection);
        }

        /// <summary>
        /// clear the SqlConnection Pool and Dispose the M_SqlConnection
        /// </summary>
        /// <returns>true or false</returns>
        public bool Pub_ClearPool()
        {
            bool isReturn = false;
            try
            {
                if (m_SqlConnection != null)
                {
                    SqlConnection.ClearPool(m_SqlConnection);
                    m_SqlConnection.Dispose();
                }
                isReturn = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isReturn;
        }

        #endregion

        #region public function about Exec SqlText,Procedurce,View and get the datatable or other values
        /// <summary>
        /// 判断是否存在事务锁
        /// </summary>
        /// <returns></returns>
        public bool Is_Lock()
        {
            return Convert.ToBoolean(this.GetDataTable("execute Is_Lock").Rows[0][0]);
        }

        /// <summary>
        /// exec the Procedure ,has the Parameter 
        /// </summary>
        /// <param name="strProcName">Proc Name</param>
        /// <param name="parameter">string list of parameter name</param>
        /// <param name="paraValue">string list of the parmater value</param>
        /// <param name="rowsCount">rows of affect</param>
        /// <returns>true or false</returns>
        public bool pub_ExecProcByParameter(string strProcName, string[] parameter, string[] paraValue, out int rowsCount)
        {
            bool IsReturn = false;
            //get the Sqlcommand ,the command has the sqlconnection
            pri_GetSqlCommand();
            m_SqlCommand.CommandText = strProcName;
            SqlParameter m_SqlParameter;
            for (int n = 0; n < parameter.Length; n++)
            {
                m_SqlParameter = new SqlParameter("@" + "" + parameter[n] + "", SqlDbType.Variant);
                m_SqlParameter.Value = paraValue[n];
                m_SqlCommand.Parameters.Add(m_SqlParameter);
            }
            try
            {
                pri_OpenSqlConnection();
                //judge the Tran is open or not ,and get the tran to ADO object
                if (m_TransOpen)
                    m_SqlCommand.Transaction = m_SqlTransaction;
                rowsCount = m_SqlCommand.ExecuteNonQuery();
                IsReturn = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (!m_TransOpen) //if the trans is donot close the connection can not close and dispose
                {
                    pri_CloseSqlConnection();
                    //m_SqlConnection.Dispose();
                }
                m_SqlCommand.Dispose();
            }
            return IsReturn;
        }

        /// <summary>
        /// exec the Procedure ,has the Parameter 
        /// </summary>
        /// <param name="strProcName">Proc Name</param>
        /// <param name="parameter">string list of parameter name</param>
        /// <param name="paraValue">string list of the parmater value</param>
        /// <param name="rowsCount">rows of affect</param>
        /// <returns>影响行数</returns>
        public int pub_ExecProcByParameterInt(string strProcName, string[] parameter, string[] paraValue, out int rowsCount)
        {
            int IsReturn = 0;
            //get the Sqlcommand ,the command has the sqlconnection
            pri_GetSqlCommand();
            m_SqlCommand.CommandText = strProcName;
            SqlParameter m_SqlParameter;
            for (int n = 0; n < parameter.Length; n++)
            {
                m_SqlParameter = new SqlParameter("@" + "" + parameter[n] + "", SqlDbType.Variant);
                m_SqlParameter.Value = paraValue[n];
                m_SqlCommand.Parameters.Add(m_SqlParameter);
            }
            try
            {
                pri_OpenSqlConnection();
                //judge the Tran is open or not ,and get the tran to ADO object
                if (m_TransOpen)
                    m_SqlCommand.Transaction = m_SqlTransaction;
                rowsCount = m_SqlCommand.ExecuteNonQuery();
                IsReturn = rowsCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (!m_TransOpen) //if the trans is donot close the connection can not close and dispose
                {
                    pri_CloseSqlConnection();
                    //m_SqlConnection.Dispose();
                }
                m_SqlCommand.Dispose();
            }
            return IsReturn;
        }

        /// <summary>
        /// get the Datatable from the sqlstring
        /// </summary>
        /// <param name="SelfString">sqlstring</param>
        /// <returns></returns>
        private DataTable pri_GetDataTableOperator(string SelfString)
        {
            //new the object of dataset
            m_DataSet_Server = new DataSet();
            try
            {
                //open the sqlconnection
                pri_OpenSqlConnection();
                //new the sqldataadapter
                m_SqlDataAdapter = new SqlDataAdapter(SelfString, this.m_SqlConnection);
                //judge the Tran is open or not ,and get the tran to ADO object
                if (m_TransOpen)
                    m_SqlDataAdapter.SelectCommand.Transaction = m_SqlTransaction;
                //fill the dataset 
                m_SqlDataAdapter.SelectCommand.CommandTimeout = m_int_TimeOut;
                m_SqlDataAdapter.Fill(m_DataSet_Server);
                //set the int_timeout
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (!m_TransOpen) //if the trans is donot close the connection can not close and dispose
                {
                    pri_CloseSqlConnection();
                    //m_SqlConnection.Dispose();
                }
                m_SqlDataAdapter.Dispose();
            }
            return m_DataSet_Server.Tables[0];
        }

        /// <summary>
        /// 根据表名或SQL语句得到数据集合
        /// </summary>
        /// <param name="strTabNameOrSqlText">the Table Name or SqlText</param>
        /// <param name="isTabName">is the TablName?</param>
        /// <returns>datatable</returns>
        public DataTable pub_GetDataTable(string strTabNameOrSqlText, bool isTabName)
        {
            if (isTabName)
                strTabNameOrSqlText = "select * from " + strTabNameOrSqlText;
            return pri_GetDataTableOperator(strTabNameOrSqlText);
        }

        /// <summary>
        /// 根据SQL语句得到数据集合
        /// </summary>
        /// <param name="strSqlText">指定字符串</param>
        /// <returns>datatable</returns>
        public DataTable GetDataTable(string strSqlText)
        {
            return pri_GetDataTableOperator(strSqlText);
        }

        /// <summary>
        /// 通过对象SqlBulkCop 来实现高效的Excel数据导入到数据表中
        /// xpf 20101112
        /// </summary>
        /// <param name="p_str_Sql">执行导入地SQL语句</param>
        /// <param name="p_str_ExcelPath">Excel文件的全路径</param>
        /// <param name="p_str_TableName">要导入的数据表名称</param>
        /// <returns>导入成功或失败</returns>
        public bool Pub_ExcuSqlBulkCopy(string p_str_Sql, string p_str_ExcelPath, string p_str_TableName)
        {
            //反馈值
            bool IsReturn = false;
            //连接Excel的SqlConnection
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + p_str_ExcelPath + ";" + "Extended Properties=Excel 8.0;";
            OleDbConnection strSqlCon = new OleDbConnection(strConn);
            strSqlCon.Open();

            //Sqlcommand 从Excel的Sheet中得到数据到DataTable
            OleDbCommand SrcCom = new OleDbCommand();
            SrcCom.CommandText = p_str_Sql;
            SrcCom.Connection = strSqlCon;
            DataTable dt = new DataTable();
            OleDbDataAdapter SrcAdapter = new OleDbDataAdapter();
            SrcAdapter.SelectCommand = SrcCom;
            SrcAdapter.Fill(dt);

            //如果目标表不存在则创建
            string strSql = string.Format("if object_id(&apos;{0}&apos;) is null create table {0}(", p_str_TableName);
            foreach (DataColumn c in dt.Columns)
            {
                strSql += string.Format("[{0}] varchar(255),", c.ColumnName);
            }
            char[] aa = { ',' };
            strSql = strSql.TrimEnd(aa) + ")";
            pub_ExecBySqlText(strSql);

            //申明SqlBullCopy 对象
            SqlBulkCopy DesBulkOp = new SqlBulkCopy(m_SqlConnection.ConnectionString, SqlBulkCopyOptions.UseInternalTransaction);
            DesBulkOp.BulkCopyTimeout = 500000000;
            DesBulkOp.NotifyAfter = dt.Rows.Count;
            try
            {
                //往数据表中写入数据
                DesBulkOp.DestinationTableName = p_str_TableName;
                DesBulkOp.WriteToServer(dt);
                IsReturn = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strSqlCon.Close();
            }
            return IsReturn;
        }

        /// <summary>
        /// 通过对象SqlBulkCop 来实现高效的Excel数据导入到数据表中
        /// xpf 20101112
        /// </summary>
        /// <param name="dt">数据集合</param>
        /// <param name="p_str_TableName">要导入的数据表名称</param>
        /// <returns>导入成功或失败</returns>
        public bool Pub_ExcuSqlBulkCopy(DataTable dt, string p_str_TableName)
        {
            //反馈值
            bool IsReturn = false;

            //如果目标表不存在则创建
            string strSql = string.Format("if object_id('{0}') is null create table {0}(", p_str_TableName);
            foreach (DataColumn c in dt.Columns)
            {
                if (c.DataType.ToString().ToLower() == "system.double")
                {
                    if ((c.ColumnName == "缴费年月") || (c.ColumnName == "审核年月"))
                        strSql += string.Format("[{0}] varchar(255),", c.ColumnName);
                    else
                        strSql += string.Format("[{0}] decimal(18,2),", c.ColumnName);
                }
                else
                    strSql += string.Format("[{0}] varchar(255),", c.ColumnName);
            }
            char[] aa = { ',' };
            strSql = strSql.TrimEnd(aa) + ")";
            pub_ExecBySqlText(strSql);

            SqlBulkCopy DesBulkOp;
            //申明SqlBullCopy 对象UseInternalTransaction
            if (m_TransOpen == true)
                DesBulkOp = new SqlBulkCopy(m_SqlConnection, SqlBulkCopyOptions.Default, m_SqlTransaction);
            else
                DesBulkOp = new SqlBulkCopy(m_SqlConnection.ConnectionString, SqlBulkCopyOptions.UseInternalTransaction);

            DesBulkOp.BulkCopyTimeout = 500000000;
            DesBulkOp.NotifyAfter = dt.Rows.Count;
            try
            {
                //往数据表中写入数据
                DesBulkOp.DestinationTableName = p_str_TableName;
                DesBulkOp.WriteToServer(dt);
                IsReturn = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DesBulkOp.Close();
                DesBulkOp = null;
                //strSqlCon.Close();
            }
            return IsReturn;
        }

        /// <summary>
        /// 用存储过程返回记录集
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="cmdParms">存储过程所使用的参数</param>
        /// <returns>存储过程执行后的结果（执行成功或者执行失败）</returns>
        public DataTable pub_GetDataTableByProc(string procName, SqlParameter[] sqlPara)
        {
            try
            {
                pri_GetSqlCommand();
                for (int i = 0; i < sqlPara.Length; i++)
                {
                    m_SqlCommand.Parameters.Add(sqlPara[i]);
                }
                m_SqlCommand.CommandText = procName;
                m_SqlCommand.CommandType = CommandType.StoredProcedure;
                m_SqlCommand.CommandTimeout = m_int_TimeOut;
                pri_OpenSqlConnection();
                //judge the Tran is open or not ,and get the tran to ADO object
                if (m_TransOpen)
                    m_SqlCommand.Transaction = m_SqlTransaction;
                m_SqlDataAdapter = new SqlDataAdapter(m_SqlCommand);

                m_DataSet_Server = new DataSet();
                m_SqlDataAdapter.Fill(m_DataSet_Server);
                m_DataTable_Server = m_DataSet_Server.Tables[0].Copy();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (!m_TransOpen) //if the trans is donot close the connection can not close and dispose
                {
                    pri_CloseSqlConnection();
                    //m_SqlConnection.Dispose();
                }
                m_SqlDataAdapter.Dispose();
                m_SqlCommand.Dispose();
            }
            return m_DataTable_Server;
        }

        /// <summary>
        /// get the row count by select SqlText
        /// </summary>
        /// <param name="str_SqlText">SqlText</param>
        /// <returns>数据行数</returns>
        public int pub_GetRowCountBySqlText(string str_SqlText)
        {
            //new the object of dataset
            int intCount = 0;
            m_DataSet_Server = new DataSet();
            try
            {
                //open the sqlconnection
                pri_OpenSqlConnection();
                //new the sqldataadapter
                m_SqlDataAdapter = new SqlDataAdapter(str_SqlText, this.m_SqlConnection);
                //judge the Tran is open or not ,and get the tran to ADO object
                if (m_TransOpen)
                    m_SqlDataAdapter.SelectCommand.Transaction = m_SqlTransaction;
                //fill the dataset 
                m_SqlDataAdapter.Fill(m_DataSet_Server);
                if (m_DataSet_Server != null && m_DataSet_Server.Tables[0].Rows.Count > 0)
                    intCount = int.Parse(m_DataSet_Server.Tables[0].Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (!m_TransOpen) //if the trans is donot close the connection can not close and dispose
                {
                    pri_CloseSqlConnection();
                    //m_SqlConnection.Dispose();
                }
                m_SqlDataAdapter.Dispose();
            }
            return intCount;
        }

        /// <summary>
        /// exec Procedure by the SqlParameter
        /// </summary>
        /// <param name="procName">procedure name</param>
        /// <param name="cmdParms">the sql parameter</param>
        /// <returns>is true of not</returns>
        public bool pub_ExecByParameter(string procName, SqlParameter[] sqlPara)
        {
            bool IsReturn = false;
            try
            {
                pri_GetSqlCommand();
                for (int i = 0; i < sqlPara.Length; i++)
                {
                    m_SqlCommand.Parameters.Add(sqlPara[i]);
                }
                m_SqlCommand.CommandText = procName;
                m_SqlCommand.CommandType = CommandType.StoredProcedure;
                pri_OpenSqlConnection();
                //judge the Tran is open or not ,and get the tran to ADO object
                if (m_TransOpen)
                    m_SqlCommand.Transaction = m_SqlTransaction;
                m_SqlCommand.ExecuteNonQuery();
                IsReturn = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (!m_TransOpen) //if the trans is donot close the connection can not close and dispose
                {
                    pri_CloseSqlConnection();
                    //m_SqlConnection.Dispose();
                }
                m_SqlCommand.Dispose();
            }
            return IsReturn;
        }

        /// <summary>
        /// exec Procedure by the SqlParameter
        /// </summary>
        /// <param name="procName">procedure name</param>
        /// <param name="cmdParms">the sql parameter</param>
        /// <returns>影响行数</returns>
        public int pub_ExecByParameterInt(string procName, SqlParameter[] sqlPara)
        {
            int IsReturn = 0;
            try
            {
                pri_GetSqlCommand();
                for (int i = 0; i < sqlPara.Length; i++)
                {
                    m_SqlCommand.Parameters.Add(sqlPara[i]);
                }
                m_SqlCommand.CommandText = procName;
                m_SqlCommand.CommandType = CommandType.StoredProcedure;
                pri_OpenSqlConnection();
                //judge the Tran is open or not ,and get the tran to ADO object
                if (m_TransOpen)
                    m_SqlCommand.Transaction = m_SqlTransaction;
                IsReturn = m_SqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (!m_TransOpen) //if the trans is donot close the connection can not close and dispose
                {
                    pri_CloseSqlConnection();
                    //m_SqlConnection.Dispose();
                }
                m_SqlCommand.Dispose();
            }
            return IsReturn;
        }

        /// <summary>
        /// exec Procedure by the SqlParameter and out the Output parameter
        /// </summary>
        /// <param name="procName">procedure name</param>
        /// <param name="cmdParms">the sql parameter</param>
        /// <param name="strOutput">the output parameter values</param>
        /// <returns>is true of not</returns>
        public bool pub_ExecByParameter(string procName, SqlParameter[] sqlPara, ref string[] strOutput)
        {
            bool IsReturn = false;
            try
            {
                pri_GetSqlCommand();
                for (int i = 0; i < sqlPara.Length; i++)
                {
                    m_SqlCommand.Parameters.Add(sqlPara[i]);
                }
                m_SqlCommand.CommandText = procName;
                m_SqlCommand.CommandType = CommandType.StoredProcedure;
                pri_OpenSqlConnection();
                //judge the Tran is open or not ,and get the tran to ADO object
                if (m_TransOpen)
                    m_SqlCommand.Transaction = m_SqlTransaction;
                m_SqlCommand.ExecuteNonQuery();
                int j = 0;
                for (int i = 0; i < m_SqlCommand.Parameters.Count; i++)
                {
                    if (m_SqlCommand.Parameters[i].Direction == ParameterDirection.Output)
                    {
                        strOutput[j] = m_SqlCommand.Parameters[i].SqlValue.ToString();
                        j++;
                    }
                }
                IsReturn = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (!m_TransOpen) //if the trans is donot close the connection can not close and dispose
                {
                    pri_CloseSqlConnection();
                    //m_SqlConnection.Dispose();
                }
                m_SqlCommand.Dispose();
            }
            return IsReturn;
        }

        /// <summary>
        /// exec Procedure by the SqlParameter and out the Output parameter
        /// </summary>
        /// <param name="procName">procedure name</param>
        /// <param name="cmdParms">the sql parameter</param>
        /// <param name="strOutput">the output parameter values</param>
        /// <returns>is true of not</returns>
        public int pub_ExecByParameterInt(string procName, SqlParameter[] sqlPara, ref string[] strOutput)
        {
            int IsReturn = 0;
            try
            {
                pri_GetSqlCommand();
                for (int i = 0; i < sqlPara.Length; i++)
                {
                    m_SqlCommand.Parameters.Add(sqlPara[i]);
                }
                m_SqlCommand.CommandText = procName;
                m_SqlCommand.CommandType = CommandType.StoredProcedure;
                pri_OpenSqlConnection();
                //judge the Tran is open or not ,and get the tran to ADO object
                if (m_TransOpen)
                    m_SqlCommand.Transaction = m_SqlTransaction;
                IsReturn = m_SqlCommand.ExecuteNonQuery();
                int j = 0;
                for (int i = 0; i < m_SqlCommand.Parameters.Count; i++)
                {
                    if (m_SqlCommand.Parameters[i].Direction == ParameterDirection.Output)
                    {
                        strOutput[j] = m_SqlCommand.Parameters[i].SqlValue.ToString();
                        j++;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (!m_TransOpen) //if the trans is donot close the connection can not close and dispose
                {
                    pri_CloseSqlConnection();
                    //m_SqlConnection.Dispose();
                }
                m_SqlCommand.Dispose();
            }
            return IsReturn;
        }

        /// <summary>
        /// exec Procedure by the SqlText
        /// </summary>
        /// <param name="procName">SqlText</param>
        /// <param name="cmdParms">the sql parameter</param>
        /// <returns>is true of not</returns>
        public bool pub_ExecBySqlText(string strSqlText)
        {
            bool IsReturn = false;
            try
            {
                pri_GetSqlCommand(strSqlText);
                pri_OpenSqlConnection();
                //judge the Tran is open or not ,and get the tran to ADO object
                if (m_TransOpen)
                    m_SqlCommand.Transaction = m_SqlTransaction;
                m_SqlCommand.CommandTimeout = 6000000;
                m_SqlCommand.ExecuteNonQuery();
                IsReturn = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (!m_TransOpen) //if the trans is donot close the connection can not close and dispose
                {
                    pri_CloseSqlConnection();
                    //m_SqlConnection.Dispose();
                }
                m_SqlCommand.Dispose();
            }
            return IsReturn;
        }

        /// <summary>
        /// exec Procedure by the SqlText
        /// </summary>
        /// <param name="procName">SqlText</param>
        /// <param name="cmdParms">the sql parameter</param>
        /// <returns>is true of not</returns>
        public void pub_ExecBySqlTextVoid(string strSqlText)
        {
            try
            {
                pri_GetSqlCommand(strSqlText);
                pri_OpenSqlConnection();
                //judge the Tran is open or not ,and get the tran to ADO object
                if (m_TransOpen)
                    m_SqlCommand.Transaction = m_SqlTransaction;
                m_SqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (!m_TransOpen) //if the trans is donot close the connection can not close and dispose
                {
                    pri_CloseSqlConnection();
                    //m_SqlConnection.Dispose();
                }
                m_SqlCommand.Dispose();
            }
        }
        /// </summary>
        /// exec Procedure by the SqlText
        /// </summary>
        /// <param name="procName">SqlText</param>
        /// <param name="cmdParms">the sql parameter</param>
        /// <returns>is true of not</returns>
        public int pub_ExecBySqlTextInt(string strSqlText)
        {
            int IsReturn = 0;
            try
            {
                pri_GetSqlCommand(strSqlText);
                pri_OpenSqlConnection();
                //judge the Tran is open or not ,and get the tran to ADO object
                if (m_TransOpen)
                    m_SqlCommand.Transaction = m_SqlTransaction;
                m_SqlCommand.CommandTimeout = m_int_TimeOut;
                IsReturn = m_SqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (!m_TransOpen) //if the trans is donot close the connection can not close and dispose
                {
                    pri_CloseSqlConnection();
                    //m_SqlConnection.Dispose();
                }
                m_SqlCommand.Dispose();
            }
            return IsReturn;
        }

        /// <summary>
        /// 根据标识得到主键ＩＤ(正式启用)
        /// </summary>
        /// <param name="tempid">标识</param>
        /// <returns>标识+年+月+日+流水号</returns>
        public string pub_GetCounterId(string p_str_id)
        {
            string strProcid = "";
            try
            {
                //get the parameter of sqlcommand
                SqlParameter Parm1 = new SqlParameter("@findcounterid", SqlDbType.VarChar, 50);
                Parm1.Value = p_str_id;
                Parm1.Direction = ParameterDirection.InputOutput;
                pri_GetSqlCommand("Sel_Sys_Counter_LP");
                m_SqlCommand.Parameters.Add(Parm1);
                m_SqlCommand.CommandType = CommandType.StoredProcedure;

                //open the sqlconnection
                pri_OpenSqlConnection();
                //judge the Tran is open or not ,and get the tran to ADO object
                if (m_TransOpen)
                    m_SqlCommand.Transaction = m_SqlTransaction;
                //exec the sqlcommand
                m_SqlCommand.ExecuteNonQuery();
                //get the output parameter string of the findcounterid
                strProcid = m_SqlCommand.Parameters["@findcounterid"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //dispose the sqlconnection and sqlcommand
                if (!m_TransOpen) //if the trans is donot close the connection can not close and dispose
                {
                    pri_CloseSqlConnection();
                    //m_SqlConnection.Dispose();
                }
                m_SqlCommand.Dispose();
            }
            //return the value
            return strProcid;
        }

        /// <summary>
        /// get database datetime 
        /// </summary>
        /// <returns>the database time</returns>
        public DateTime pub_GetDataDateTime()
        {
            //create the sqlcommand
            pri_GetSqlCommand("select getdate()");
            pri_OpenSqlConnection(); //open the sqlconnecton
            //judge the Tran is open or not ,and get the tran to ADO object
            if (m_TransOpen)
                m_SqlCommand.Transaction = m_SqlTransaction;
            DateTime selfDateTime = new DateTime();
            try
            {
                //instantiation the sqldatareader and exec
                m_SqlDataReader = m_SqlCommand.ExecuteReader();
                m_SqlDataReader.Read();
                //get the database datetime
                selfDateTime = Convert.ToDateTime(m_SqlDataReader[0].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //dispose the connection and reader
                if (!m_TransOpen) //if the trans is donot close the connection can not close and dispose
                {
                    pri_CloseSqlConnection();
                    //m_SqlConnection.Dispose();
                }
                m_SqlDataReader.Close();
                m_SqlDataReader.Dispose();
            }
            return selfDateTime;
        }

        /// <summary>
        /// 批量更新一个datatable 数据到对应的数据表
        /// </summary>
        /// <param name="getchangetable">要更新的记录集</param>
        /// <param name="selftablename">表名</param>
        /// <returns>true:更新成功，false:更新失败</returns>
        public bool pub_UpdateAloneTable(DataTable getchangetable, string selftablename)
        {
            bool IsReturn = false;
            //open the sqlconnection
            pri_OpenSqlConnection();
            //judge the Tran is open or not ,and get the tran to ADO object
            if (m_TransOpen)
            {
                m_SqlDataAdapter.SelectCommand.Transaction = m_SqlTransaction;
                m_SqlDataAdapter.UpdateCommand.Transaction = m_SqlTransaction;
                m_SqlDataAdapter.InsertCommand.Transaction = m_SqlTransaction;
                m_SqlDataAdapter.DeleteCommand.Transaction = m_SqlTransaction;
            }
            //new the sqldataadapter
            m_SqlDataAdapter = new SqlDataAdapter("select * from " + selftablename, m_SqlConnection);
            //new the sqlcommandbuilder
            SqlCommandBuilder m_SqlCommandBuilder = new SqlCommandBuilder(m_SqlDataAdapter);
            try
            {
                if (!getchangetable.HasErrors)
                {
                    //update the changetable records
                    m_SqlDataAdapter.Update(getchangetable);
                    IsReturn = true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (!m_TransOpen) //if the trans is donot close the connection can not close and dispose
                {
                    pri_CloseSqlConnection();
                    //m_SqlConnection.Dispose();
                }
                m_SqlDataAdapter.Dispose();
                m_SqlCommandBuilder.Dispose();
            }
            return IsReturn;
        }
        #endregion

        #region get the Excel data by the oledb and SqlBulkCopy

        /// <summary>
        /// 通过对象SqlBulkCop 来实现高效的Excel数据导入到数据表中
        /// xpf 20101112
        /// </summary>
        /// <param name="p_str_Sql">执行导入地SQL语句</param>
        /// <param name="p_str_ExcelPath">Excel文件的全路径</param>
        /// <param name="p_str_TableName">要导入的数据表名称</param>
        /// <returns>导入成功或失败</returns>
        public bool Pub_GetExcelDataByOledb(string p_str_Sql, string p_str_ExcelPath, string p_str_TableName)
        {
            //反馈值
            bool IsReturn = false;
            //连接Excel的SqlConnection
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + p_str_ExcelPath + ";" + "Extended Properties=Excel 8.0;";
            OleDbConnection strSqlCon = new OleDbConnection(strConn);
            strSqlCon.Open();

            //Sqlcommand 从Excel的Sheet中得到数据到DataTable
            OleDbCommand SrcCom = new OleDbCommand();
            SrcCom.CommandText = p_str_Sql;
            SrcCom.Connection = strSqlCon;
            DataTable dt = new DataTable();
            OleDbDataAdapter SrcAdapter = new OleDbDataAdapter();
            SrcAdapter.SelectCommand = SrcCom;
            SrcAdapter.Fill(dt);

            //申明SqlBullCopy 对象
            SqlBulkCopy DesBulkOp = new SqlBulkCopy(m_SqlConnection.ConnectionString, SqlBulkCopyOptions.UseInternalTransaction);
            DesBulkOp.BulkCopyTimeout = 500000000;
            DesBulkOp.NotifyAfter = dt.Rows.Count;
            try
            {
                //往数据表中写入数据
                DesBulkOp.DestinationTableName = p_str_TableName;
                DesBulkOp.WriteToServer(dt);
                IsReturn = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strSqlCon.Close();
                SrcCom.Dispose();
                SrcAdapter.Dispose();
                strSqlCon.Dispose();
            }
            return IsReturn;
        }

        /// <summary>
        /// 通过对象SqlBulkCop 来实现高效的Excel数据导入到数据表中
        /// xpf 20101112
        /// </summary>
        /// <param name="dt">数据集合</param>
        /// <param name="p_str_TableName">要导入的数据表名称</param>
        /// <returns>导入成功或失败</returns>
        public bool Pub_GetExcelDataByOledb(DataTable dt, string p_str_TableName)
        {
            //反馈值
            bool IsReturn = false;

            //申明SqlBullCopy 对象
            SqlBulkCopy DesBulkOp = new SqlBulkCopy(m_SqlConnection.ConnectionString, SqlBulkCopyOptions.UseInternalTransaction);
            DesBulkOp.BulkCopyTimeout = 500000000;
            DesBulkOp.NotifyAfter = dt.Rows.Count;
            try
            {
                //往数据表中写入数据
                DesBulkOp.DestinationTableName = p_str_TableName;
                DesBulkOp.WriteToServer(dt);
                IsReturn = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //strSqlCon.Close();
            }
            return IsReturn;
        }

        #endregion

        #region begin,commit,rollback the Transcation

        /// <summary>
        /// 开启事务
        /// </summary>
        public bool pub_Ope_BeginTranscation()
        {
            return pri_BeginTransaction(null);
        }

        /// <summary>
        /// 开启指定名的事务
        /// </summary>
        /// <param name="p_str_TranscationName">事务名</param>
        public bool pub_Ope_BeginTranscation(string p_str_TranscationName)
        {
            return pri_BeginTransaction(p_str_TranscationName);
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public bool pub_Ope_Rollback()
        {
            return pri_Ope_Rollback(null);
        }

        /// <summary>
        /// 回滚指定名的事务
        /// </summary>
        /// <param name="p_str_TranscationName">事务名</param>
        public bool pub_Ope_Rollback(string p_str_TranscationName)
        {
            return pri_Ope_Rollback(p_str_TranscationName);
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public bool pub_Ope_CommitTranscation()
        {
            bool returnvalue = false;
            try
            {
                m_SqlTransaction.Commit();
                pri_CloseSqlConnection();
                m_SqlTransaction.Dispose();
                //m_SqlConnection.Dispose();
                m_TransOpen = false;
                returnvalue = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return returnvalue;
        }

        ///<summary>
        /// 开启事务内部函数
        /// </summary>
        /// <param name="p_str_TransactionName">事务名</param>
        private bool pri_BeginTransaction(string p_str_TransactionName)
        {
            bool IsReturn = false;
            try
            {
                if (m_SqlConnection.State == ConnectionState.Broken)
                {
                    m_SqlConnection.Close();
                }
                pri_OpenSqlConnection();
                if (p_str_TransactionName == null)
                    m_SqlTransaction = m_SqlConnection.BeginTransaction();
                else
                    m_SqlTransaction = m_SqlConnection.BeginTransaction(p_str_TransactionName);

                m_TransOpen = true;//开启事务
                IsReturn = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return IsReturn;
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        /// <param name="p_str_TranscationName">指定事务名</param>
        private bool pri_Ope_Rollback(string p_str_TranscationName)
        {
            bool IsReturn = false;
            try
            {
                if (m_TransOpen)
                {
                    if (p_str_TranscationName != null && p_str_TranscationName != "")
                        m_SqlTransaction.Rollback(p_str_TranscationName);
                    else
                        m_SqlTransaction.Rollback();
                }
                m_TransOpen = false;
                pri_CloseSqlConnection();
                m_SqlTransaction.Dispose();
                //m_SqlConnection.Dispose();
                IsReturn = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IsReturn;
        }

        #endregion

    }

    public class MySqlDb
    {
        private MySqlConnection mySqlConnection;
        private string ConnectString;
        public MySqlDb(string ConnectString)
        {
            try
            {
                this.ConnectString = ConnectString;
                mySqlConnection = new MySqlConnection(this.ConnectString);
                mySqlConnection.Open();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DataTable GetDataTable(string Sql)
        {
            return _GetDataTable(Sql);
        }

        private DataTable _GetDataTable(string Sql)
        {
            DataTable dataTable = new DataTable();
            using (MySqlCommand mySqlCommand = new MySqlCommand(Sql, mySqlConnection))
            {
                MySqlDataReader reader = mySqlCommand.ExecuteReader();//执行ExecuteReader()返回一个MySqlDataReader对象
                dataTable.Load(reader);
                reader.Close();
            };
            return dataTable;
        }

    }
}

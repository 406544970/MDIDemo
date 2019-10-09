using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MDIDemo.PublicClass
{
    public class MySqlite3
    {
        #region 构造函数
        public MySqlite3()
        {
            _IniConnectStr(Application.StartupPath, "myBatisSQLite.db");
        }
        public MySqlite3(string Path, string dbFileName)
        {
            _IniConnectStr(Path, dbFileName);
        }
        public MySqlite3(string Path)
        {
            _IniConnectStr(Path, "myBatisSQLite.db");
        }
        #endregion

        #region 内部
        /// <summary>
        /// 连接字符串
        /// </summary>
        private string _ConnectStr;
        private void _IniConnectStr(string Path, string dbFileName)
        {
            _ConnectStr = string.Format(@"Data Source ={0}\\{1}; Version = 3;
                        Pooling = True; Max Pool Size = 100;"
                        , Path, dbFileName);
            //"Data Source=Z:\SQLite\DPTestSystem.db;Version=3;Pooling=True;Max Pool Size=100;" />
        }
        #endregion

        #region 公共方法
        public string GetConnectStr()
        {
            return _ConnectStr;
        }
        /// <summary>  
        /// 执行SQL语句，返回影响的记录数  
        /// </summary>  
        /// <param name="SQLString">SQL语句</param>  
        /// <returns>影响的记录数</returns>  
        public int ExecuteSql(string SQLString)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_ConnectStr))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Data.SQLite.SQLiteException E)
                    {
                        connection.Close();
                        throw new Exception(E.Message);
                    }
                }
            }
        }
        /// <summary>  
        /// 执行多条SQL语句，实现数据库事务。  
        /// </summary>  
        /// <param name="SQLStringList">多条SQL语句</param>       
        public void ExecuteSqlTran(ArrayList SQLStringList)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_ConnectStr))
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = conn;
                SQLiteTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n].ToString();
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
                catch (System.Data.SQLite.SQLiteException E)
                {
                    tx.Rollback();
                    throw new Exception(E.Message);
                }
            }
        }
        /// <summary>  
        /// 执行一条计算查询结果语句，返回查询结果（object）。  
        /// </summary>  
        /// <param name="SQLString">计算查询结果语句</param>  
        /// <returns>查询结果（object）</returns>  
        public object GetSingle(string SQLString)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_ConnectStr))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SQLite.SQLiteException e)
                    {
                        connection.Close();
                        throw new Exception(e.Message);
                    }
                }
            }
        }
        ///// <summary>  
        ///// 执行一条计算查询结果语句，返回查询结果（object）。  
        ///// </summary>  
        ///// <param name="SQLString">计算查询结果语句</param>  
        ///// <returns>查询结果（object）</returns>  
        //public object GetSingle(string SQLString, params SQLiteParameter[] cmdParms)
        //{
        //    using (SQLiteConnection connection = new SQLiteConnection(_ConnectStr))
        //    {
        //        using (SQLiteCommand cmd = new SQLiteCommand())
        //        {
        //            try
        //            {
        //                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
        //                object obj = cmd.ExecuteScalar();
        //                cmd.Parameters.Clear();
        //                if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
        //                {
        //                    return null;
        //                }
        //                else
        //                {
        //                    return obj;
        //                }
        //            }
        //            catch (System.Data.SQLite.SQLiteException e)
        //            {
        //                throw new Exception(e.Message);
        //            }
        //        }
        //    }
        //}

        public DataTable ExecuteReaderField(string strSQL)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_ConnectStr))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(strSQL, connection))
                {
                    try
                    {
                        connection.Open();
                        SQLiteDataReader myReader = cmd.ExecuteReader();
                        return myReader.GetSchemaTable();
                    }
                    catch (System.Data.SQLite.SQLiteException e)
                    {
                        throw new Exception(e.Message);
                    }
                }
            }
        }
        /// <summary>  
        /// 执行查询语句，返回SQLiteDataReader  
        /// </summary>  
        /// <param name="strSQL">查询语句</param>  
        /// <returns>SQLiteDataReader</returns>  
        public SQLiteDataReader ExecuteReader(string strSQL)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_ConnectStr))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(strSQL, connection))
                {
                    try
                    {
                        connection.Open();
                        SQLiteDataReader myReader = cmd.ExecuteReader();
                        return myReader;
                    }
                    catch (System.Data.SQLite.SQLiteException e)
                    {
                        throw new Exception(e.Message);
                    }
                }
            }
        }
        public List<string> ExecuteReadList(string Sql)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_ConnectStr))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(Sql, connection))
                {
                    try
                    {
                        connection.Open();
                        SQLiteDataReader myReader = cmd.ExecuteReader();
                        if (myReader.HasRows)
                        {
                            List<string> vs = new List<string>();
                            StringBuilder stringBuilder = new StringBuilder();
                            int Counter = 0;
                            while (myReader.Read())
                            {
                                stringBuilder.Clear();
                                for (int i = 0; i < myReader.FieldCount; i++)
                                {
                                    if (i > 0)
                                    {
                                        stringBuilder.AppendFormat(";{0}", myReader[i].ToString());
                                    }
                                    else
                                    {
                                        stringBuilder.Append(myReader[i].ToString());
                                    }
                                }
                                Counter++;
                                vs.Add(stringBuilder.ToString());
                            }
                            stringBuilder = null;
                            return vs;
                        }
                        else
                            return null;
                    }
                    catch (System.Data.SQLite.SQLiteException e)
                    {
                        throw new Exception(e.Message);
                    }
                }
            }
        }
        ///// <summary>  
        ///// 执行查询语句，返回SQLiteDataReader  
        ///// </summary>  
        ///// <param name="strSQL">查询语句</param>  
        ///// <returns>SQLiteDataReader</returns>  
        //public SQLiteDataReader ExecuteReader(string SQLString, params SQLiteParameter[] cmdParms)
        //{
        //    SQLiteConnection connection = new SQLiteConnection(_ConnectStr);
        //    SQLiteCommand cmd = new SQLiteCommand();
        //    try
        //    {
        //        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
        //        SQLiteDataReader myReader = cmd.ExecuteReader();
        //        cmd.Parameters.Clear();
        //        return myReader;
        //    }
        //    catch (System.Data.SQLite.SQLiteException e)
        //    {
        //        throw new Exception(e.Message);
        //    }

        //}
        /// <summary>  
        /// 执行查询语句，返回DataSet  
        /// </summary>  
        /// <param name="SQLString">查询语句</param>  
        /// <returns>DataSet</returns>  
        public DataSet Query(string SQLString)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_ConnectStr))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SQLiteDataAdapter command = new SQLiteDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                }
                catch (System.Data.SQLite.SQLiteException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }
        ///// <summary>  
        ///// 执行查询语句，返回DataSet  
        ///// </summary>  
        ///// <param name = "SQLString" > 查询语句 </ param >
        ///// < returns > DataSet </ returns >
        //public DataSet Query(string SQLString, params SQLiteParameter[] cmdParms)
        //{
        //    using (SQLiteConnection connection = new SQLiteConnection(_ConnectStr))
        //    {
        //        SQLiteCommand cmd = new SQLiteCommand();
        //        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
        //        using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
        //        {
        //            DataSet ds = new DataSet();
        //            try
        //            {
        //                da.Fill(ds, "ds");
        //                cmd.Parameters.Clear();
        //            }
        //            catch (System.Data.SQLite.SQLiteException ex)
        //            {
        //                throw new Exception(ex.Message);
        //            }
        //            return ds;
        //        }
        //    }
        //}
        /// <summary>  
        /// 执行多条SQL语句，实现数据库事务。  
        /// </summary>  
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SQLiteParameter[]）</param>  
        public void ExecuteSqlTran(Hashtable SQLStringList)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_ConnectStr))
            {
                conn.Open();
                using (SQLiteTransaction trans = conn.BeginTransaction())
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    try
                    {
                        //循环  
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            SQLiteParameter[] cmdParms = (SQLiteParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                            trans.Commit();
                        }
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }
        /// <summary>
        /// 带参数的命令
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        private void PrepareCommand(SQLiteCommand cmd, SQLiteConnection conn, SQLiteTransaction trans, string cmdText, SQLiteParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;  
            if (cmdParms != null)
            {
                foreach (SQLiteParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
        #endregion
    }
}

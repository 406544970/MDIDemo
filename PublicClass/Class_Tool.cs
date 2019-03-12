using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml.Serialization;

namespace MDIDemo.PublicClass
{
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
            class_JavaAndJdbc.JavaType = "java.lang.int";
            class_JavaAndJdbcs.Add(class_JavaAndJdbc);
            class_JavaAndJdbc = new Class_JavaAndJdbc();
            class_JavaAndJdbc.JdbcType = "BIGINT";
            class_JavaAndJdbc.JavaType = "java.lang.long";
            class_JavaAndJdbcs.Add(class_JavaAndJdbc);
            class_JavaAndJdbc = new Class_JavaAndJdbc();
            class_JavaAndJdbc.JdbcType = "REAL";
            class_JavaAndJdbc.JavaType = "java.lang.float";
            class_JavaAndJdbcs.Add(class_JavaAndJdbc);
            class_JavaAndJdbc = new Class_JavaAndJdbc();
            class_JavaAndJdbc.JdbcType = "FLOAT";
            class_JavaAndJdbc.JavaType = "java.lang.double";
            class_JavaAndJdbcs.Add(class_JavaAndJdbc);
            class_JavaAndJdbc = new Class_JavaAndJdbc();
            class_JavaAndJdbc.JdbcType = "DOUBLE";
            class_JavaAndJdbc.JavaType = "java.lang.double";
            class_JavaAndJdbcs.Add(class_JavaAndJdbc);
            class_JavaAndJdbc = new Class_JavaAndJdbc();
            class_JavaAndJdbc.JdbcType = "BINARY";
            class_JavaAndJdbc.JavaType = "java.lang.byte[]";
            class_JavaAndJdbcs.Add(class_JavaAndJdbc);
            class_JavaAndJdbc = new Class_JavaAndJdbc();
            class_JavaAndJdbc.JdbcType = "VARBINARY";
            class_JavaAndJdbc.JavaType = "java.lang.byte[]";
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
        }
        private static List<Class_JavaAndJdbc> class_JavaAndJdbcs;
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

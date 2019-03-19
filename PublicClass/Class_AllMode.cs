using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDIDemo.PublicClass
{
    public class Class_EnglishField
    {
        public string FieldChinaName;
        public string FieldEnglishName;
    }
    public class Class_WhereField
    {
        public string FieldName;
        public string FieldRemark;
        public string FieldType;
        public string FieldDefaultValue;
    }
    /// <summary>
    /// 数据库说明书类
    /// </summary>
    public class Class_DataBaseContent
    {
        public Class_DataBaseContent()
        {
            class_SheetContents = new List<Class_SheetContent>();
        }
        ~Class_DataBaseContent()
        {
            class_SheetContents.Clear();
        }
        /// <summary>
        /// 单Sheet类
        /// </summary>
        public partial class Class_SheetContent
        {
            public Class_SheetContent()
            {
                dataTable = new DataTable();
                FieldTitleList = new List<string>();
                LeftFieldNameList = new List<string>();
            }
            ~Class_SheetContent()
            {
                dataTable.Dispose();
                FieldTitleList.Clear();
                LeftFieldNameList.Clear();
            }
            /// <summary>
            /// Sheet名称
            /// </summary>
            public string SheetName;
            /// <summary>
            /// Sheet标题
            /// </summary>
            public string SheetTitle;
            /// <summary>
            /// 内容
            /// </summary>
            public string TableContent;
            /// <summary>
            /// 实际数据
            /// </summary>
            public DataTable dataTable;
            /// <summary>
            /// 列标题名称
            /// </summary>
            public List<string> FieldTitleList;
            /// <summary>
            /// 需要居左的String字段名
            /// </summary>
            public List<string> LeftFieldNameList;
        }
        /// <summary>
        /// 数据库说明书文件名称
        /// </summary>
        public string DataBaseFileName;
        /// <summary>
        /// Sheet集列表
        /// </summary>
        public List<Class_SheetContent> class_SheetContents;
    }
    public class Class_TableInfo
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName;
        /// <summary>
        /// 表注释
        /// </summary>
        public string TableComment;
    }
    public class Class_ReadWriteSetUpXml
    {
        public Class_ReadWriteSetUpXml()
        {
            GridFontSize = 11;
            OpenWelcome = true;
        }
        public float GridFontSize { get; set; }
        public bool OpenWelcome { get; set; }
    }
    public class Class_WindowType
    {
        public Class_WindowType()
        {
            ActiveSign = false;
        }
        public string XmlFileName { get; set; }
        public string WindowType { get; set; }
        public bool ActiveSign { get; set; }
    }
    public class Class_OrderBy
    {
        public string FieldName { get; set; }
        public string SortType { get; set; }
        public int SortNo { get; set; }
    }
    public class Class_SqlServerAndJava: Class_MySqlFieldAndJava
    {

    }
    public class Class_MySqlFieldAndJava
    {
        public string DataBaseFieldType;
        public string JavaType;
    }
    /// <summary>
    /// java基本类型与封闭类
    /// </summary>
    public class Class_JavaAndClosedClass
    {
        /// <summary>
        /// Java基本类型
        /// </summary>
        public string JavaType;
        /// <summary>
        /// 封装类全名
        /// </summary>
        public string ClosedType;
        /// <summary>
        /// 封装类简名
        /// </summary>
        public string SimplClosedType;
    }
    /// <summary>
    /// java类型与jdbc类型
    /// </summary>
    public class Class_JavaAndJdbc
    {
        public string JavaType;
        public string JdbcType;
    }
    public class Class_PageInfomationMode
    {
        /// <summary>
        /// 页面信息Mode
        /// </summary>
        public Class_PageInfomationMode()
        {
            lastUpdateTime = System.DateTime.Now;
            finishCount = 0;
            readOnly = true;
        }
        public string pageKey;
        public string projectId;
        public string pageType;
        public int pageVersion;
        public DateTime createTime;
        public DateTime lastUpdateTime;
        public string createOperatorId;
        public string doOperatorId;
        public int finishCount;
        public string methodRemark;
        public bool readOnly;

    }
}

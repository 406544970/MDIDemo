﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDIDemo.PublicClass
{
    public class class_ReadWriteSetUpXml
    {
        public class_ReadWriteSetUpXml()
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
        public string JavaType;
        public string ClosedType;
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

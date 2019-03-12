using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDIDemo.PublicClass
{
    public class Class_TableStructModel
    {

        public Class_TableStructModel()
        {
            this.FieldIsKey = false;
            this.FieldIsNull = true;
            this.FieldIsAutoAdd = false;
            this.DataBaseType = 0;
        }
        /// <summary>
        /// 0:mysql,1:sql server 2017,2:oracle 11g
        /// </summary>
        public int DataBaseType
        {
            get;set;
        }
        public string FieldName
        {
            get;set;
        }
        public string FieldRemark
        {
            get;set;
        }
        public string FieldType
        {
            get; set;
        }
        public int FieldLength
        {
            get; set;
        }
        public string FieldDefaultValue
        {
            get; set;
        }
        public bool FieldIsNull
        {
            get; set;
        }
        public bool FieldIsKey
        {
            get; set;
        }
        public bool FieldIsAutoAdd
        {
            get; set;
        }
    }
}

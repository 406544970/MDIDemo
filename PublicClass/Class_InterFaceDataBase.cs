using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MDIDemo.PublicClass
{
    public interface Class_InterFaceDataBase
    {
        //string OperateType { get; set; }
        string Ip { get; set; }
        string UserName { get; set; }
        string PassWord { get; set; }
        string DataBaseName { get; set; }
        /// <summary>
        /// 根据数据库类型，得到java类型
        /// </summary>
        /// <param name="dataBaseFieldType"></param>
        /// <returns></returns>
        string GetJavaType(string dataBaseFieldType);
        /// <summary>
        /// 得到用户列表
        /// </summary>
        /// <returns></returns>
        List<Class_TableInfo> GetUseTableList(List<string> TableNameList);
        /// <summary>
        /// 得到指定表结构
        /// </summary>
        /// <param name="TableName">指定表名</param>
        /// <returns></returns>
        DataTable GetMainTableStruct(string TableName,int PageSelectIndex);
        List<string> GetDataType();
        List<string> GetFunctionList(string FieldType);
        List<string> GetHavingFuctionList(string FieldType);
        /// <summary>
        /// 根据字段类型，得到是否加点
        /// </summary>
        /// <param name="FieldType">字段类型</param>
        /// <returns></returns>
        bool IsAddPoint(string FieldType);
        void SetClass_AllModel<T>(T class_AllModel);

        /// <summary>
        /// 导出数据库说明书
        /// </summary>
        /// <param name="class_DataBaseContent"></param>
        /// <returns></returns>
        string GetDataBaseContent();

    }
}

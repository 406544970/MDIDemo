using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MDIDemo.PublicClass
{
    public class Class_PublicMethod
    {
        private XmlUtil xmlUtil;
        private Class_SQLiteOperator class_SQLiteOperator;
        public Class_PublicMethod()
        {
            xmlUtil = new XmlUtil();
            class_SQLiteOperator = new Class_SQLiteOperator();
        }

        private bool SaveToXml<T>(string xmlPath, string fileName, T t)
        {
            string PathXmlSolutionName = string.Format("{0}\\{1}", Application.StartupPath, xmlPath);
            Class_PageInfomationMode class_PageInfomationMode = new Class_PageInfomationMode();
            Type type = t.GetType();
            switch (type.Name)
            {
                case "Class_SelectAllModel":
                    {
                        Class_SelectAllModel class_SelectAllModel = new Class_SelectAllModel();
                        class_SelectAllModel = t as Class_SelectAllModel;
                        class_PageInfomationMode.pageKey = class_SelectAllModel.class_Create.MethodId;
                        class_PageInfomationMode.projectId = "projectId";
                        class_PageInfomationMode.pageType = "select";
                        class_PageInfomationMode.pageVersion = 0;
                        class_PageInfomationMode.createTime = System.DateTime.Now;
                        class_PageInfomationMode.lastUpdateTime = class_PageInfomationMode.createTime;
                        class_PageInfomationMode.createOperatorId = class_SelectAllModel.class_Create.CreateMan;
                        class_PageInfomationMode.doOperatorId = class_SelectAllModel.class_Create.CreateDo;
                        class_PageInfomationMode.finishCount = 0;
                        class_PageInfomationMode.methodRemark = class_SelectAllModel.class_Create.MethodRemark;
                        class_PageInfomationMode.readOnly = class_SelectAllModel.class_Create.ReadOnly;
                    }
                    break;
                default:
                    {
                        Class_SelectAllModel class_SelectAllModel = new Class_SelectAllModel();
                        class_SelectAllModel = t as Class_SelectAllModel;
                        class_PageInfomationMode.pageKey = class_SelectAllModel.class_Create.MethodId;
                        class_PageInfomationMode.projectId = "projectId";
                        class_PageInfomationMode.pageType = "select";
                        class_PageInfomationMode.pageVersion = 0;
                        class_PageInfomationMode.createTime = System.DateTime.Now;
                        class_PageInfomationMode.lastUpdateTime = class_PageInfomationMode.createTime;
                        class_PageInfomationMode.createOperatorId = class_SelectAllModel.class_Create.CreateMan;
                        class_PageInfomationMode.doOperatorId = class_SelectAllModel.class_Create.CreateDo;
                        class_PageInfomationMode.finishCount = 0;
                        class_PageInfomationMode.methodRemark = class_SelectAllModel.class_Create.MethodRemark;
                        class_PageInfomationMode.readOnly = class_SelectAllModel.class_Create.ReadOnly;
                    }
                    break;
            }
            if (class_SQLiteOperator.InsertIntoPageKey(class_PageInfomationMode))
                return xmlUtil.ObjectSerialXml<T>(PathXmlSolutionName, fileName, t);
            else
                return false;
        }

        private T _FromXmlToObject<T>(string fileFolderName, string fileFullName) where T : class
        {
            fileFullName = string.Format(@"{0}\\{1}\\{2}.xml", Application.StartupPath, fileFolderName, fileFullName);
            if (File.Exists(fileFullName))
                return xmlUtil.XmlSerialObject<T>(fileFullName);
            else
                return null;
        }
        public T FromXmlToDefaultValueObject<T>(string fileFullName) where T : class
        {
            return _FromXmlToObject<T>("DataBaseDefault", fileFullName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileFullName"></param>
        /// <returns></returns>
        public T FromXmlToSelectObject<T>(string fileFullName) where T : class
        {
            return _FromXmlToObject<T>("select", fileFullName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileFullName"></param>
        /// <returns></returns>
        public T FromXmlToDeleteObject<T>(string fileFullName) where T : class
        {
            return _FromXmlToObject<T>("delete", fileFullName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileFullName"></param>
        /// <returns></returns>
        public T FromXmlToUpdateObject<T>(string fileFullName) where T : class
        {
            return _FromXmlToObject<T>("update", fileFullName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileFullName"></param>
        /// <returns></returns>
        public T FromXmlInsertObject<T>(string fileFullName) where T : class
        {
            return _FromXmlToObject<T>("insert", fileFullName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool DataBaseDefaultValueToXml<T>(string fileName, T t)
        {
            return SaveToXml<T>("DataBaseDefault", fileName, t);
        }
        /// <summary>
        /// 保存Select
        /// </summary>
        /// <typeparam name="T">类名</typeparam>
        /// <param name="fileName">文件名，不用加扩展名</param>
        /// <param name="t">对象</param>
        /// <returns></returns>
        public bool SelectToXml<T>(string fileName, T t)
        {
            return SaveToXml<T>("select", fileName, t);
        }
        /// <summary>
        /// 保存Insert
        /// </summary>
        /// <typeparam name="T">类名</typeparam>
        /// <param name="fileName">文件名，不用加扩展名</param>
        /// <param name="t">对象</param>
        /// <returns></returns>
        public bool InsertToXml<T>(string fileName, T t)
        {
            return SaveToXml<T>("insert", fileName, t);
        }
        /// <summary>
        /// 保存Update
        /// </summary>
        /// <typeparam name="T">类名</typeparam>
        /// <param name="fileName">文件名，不用加扩展名</param>
        /// <param name="t">对象</param>
        /// <returns></returns>
        public bool UpdateToXml<T>(string fileName, T t)
        {
            return SaveToXml<T>("update", fileName, t);
        }
        /// <summary>
        /// 保存Delete
        /// </summary>
        /// <typeparam name="T">类名</typeparam>
        /// <param name="fileName">文件名，不用加扩展名</param>
        /// <param name="t">对象</param>
        /// <returns></returns>
        public bool DeleteToXml<T>(string fileName, T t)
        {
            return SaveToXml<T>("delete", fileName, t);
        }

    }
}

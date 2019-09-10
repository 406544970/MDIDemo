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
            bool SaveOk = false;
            string PathXmlSolutionName = string.Format("{0}\\{1}", Application.StartupPath, xmlPath);
            if (!System.IO.Directory.Exists(PathXmlSolutionName))
                System.IO.Directory.CreateDirectory(PathXmlSolutionName); 
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
                        class_PageInfomationMode.frontOperatorId = class_SelectAllModel.class_Create.CreateFrontDo;
                        class_PageInfomationMode.finishCount = 0;
                        class_PageInfomationMode.readOnly = class_SelectAllModel.class_Create.ReadOnly;
                        SaveOk = class_SQLiteOperator.InsertIntoPageKey(class_PageInfomationMode);
                    }
                    break;
                case "Class_InsertAllModel":
                    {
                        Class_InsertAllModel class_InsertAllModel = new Class_InsertAllModel();
                        class_InsertAllModel = t as Class_InsertAllModel;
                        class_PageInfomationMode.pageKey = class_InsertAllModel.class_Create.MethodId;
                        class_PageInfomationMode.projectId = "projectId";
                        class_PageInfomationMode.pageType = "insert";
                        class_PageInfomationMode.pageVersion = 0;
                        class_PageInfomationMode.createTime = System.DateTime.Now;
                        class_PageInfomationMode.lastUpdateTime = class_PageInfomationMode.createTime;
                        class_PageInfomationMode.createOperatorId = class_InsertAllModel.class_Create.CreateMan;
                        class_PageInfomationMode.doOperatorId = class_InsertAllModel.class_Create.CreateDo;
                        class_PageInfomationMode.frontOperatorId = class_InsertAllModel.class_Create.CreateFrontDo;
                        class_PageInfomationMode.finishCount = 0;
                        class_PageInfomationMode.readOnly = class_InsertAllModel.class_Create.ReadOnly;
                        SaveOk = class_SQLiteOperator.InsertIntoPageKey(class_PageInfomationMode);
                    }
                    break;
                case "Class_DataBaseConDefault":
                    SaveOk = true;
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
                        class_PageInfomationMode.frontOperatorId = class_SelectAllModel.class_Create.CreateFrontDo;
                        class_PageInfomationMode.finishCount = 0;
                        class_PageInfomationMode.readOnly = class_SelectAllModel.class_Create.ReadOnly;
                        SaveOk = class_SQLiteOperator.InsertIntoPageKey(class_PageInfomationMode);
                    }
                    break;
            }
            if (SaveOk)
                return xmlUtil.ObjectSerialXml<T>(PathXmlSolutionName, fileName, t);
            else
                return false;
        }
        public bool DeleteXml(string xmlFileName, string classType)
        {
            if (true)
            {
                if (File.Exists(string.Format(@"{0}\\{1}\\{2}.xml", Application.StartupPath, classType, xmlFileName)))
                {
                    File.Delete(string.Format(@"{0}\\{1}\\{2}.xml", Application.StartupPath, classType, xmlFileName));
                }
                return class_SQLiteOperator.DeleteByPageKey(xmlFileName);
            }
        }
        public string CopyToNewXml(string xmlFileName, string classType)
        {
            string returnKey;
            switch (classType)
            {
                case "select":
                    {
                        returnKey = Class_Tool.getKeyId("SE");
                        Class_SelectAllModel class_SelectAllModel = new Class_SelectAllModel();
                        class_SelectAllModel = this.FromXmlToSelectObject<Class_SelectAllModel>(xmlFileName);
                        class_SelectAllModel.class_Create.MethodId = returnKey;
                        class_SelectAllModel.class_Create.DateTime = System.DateTime.Now;
                        if ((true) && !this.SelectToXml(class_SelectAllModel.class_Create.MethodId, class_SelectAllModel))
                        {
                            returnKey = null;
                        }
                    }
                    break;
                case "insert":
                    returnKey = Class_Tool.getKeyId("IN");
                    break;
                case "update":
                    returnKey = Class_Tool.getKeyId("UP");
                    break;
                case "delete":
                    returnKey = Class_Tool.getKeyId("DE");
                    break;
                default:
                    {
                        returnKey = Class_Tool.getKeyId("SE");
                        Class_SelectAllModel class_SelectAllModel = new Class_SelectAllModel();
                        class_SelectAllModel = this.FromXmlToSelectObject<Class_SelectAllModel>(xmlFileName);
                        class_SelectAllModel.class_Create.MethodId = returnKey;
                        class_SelectAllModel.class_Create.DateTime = System.DateTime.Now;
                        if ((true) && !this.SelectToXml(class_SelectAllModel.class_Create.MethodId, class_SelectAllModel))
                        {
                            returnKey = null;
                        }
                    }
                    break;
            }
            return returnKey;
        }
        /// <summary>
        /// 是否启动首页
        /// </summary>
        /// <returns></returns>
        public bool GetOpenWelcome()
        {
            Class_ReadWriteSetUpXml class_ReadWriteSetUpXml = new Class_ReadWriteSetUpXml();
            class_ReadWriteSetUpXml = _GetSetUpXml<Class_ReadWriteSetUpXml>();
            if (class_ReadWriteSetUpXml != null)
                return class_ReadWriteSetUpXml.OpenWelcome;
            else
                return true;
        }
        /// <summary>
        /// 设置是否启动打开首页
        /// </summary>
        /// <param name="OpenWelcome"></param>
        /// <returns></returns>
        public bool SetOpenWelcome(bool OpenWelcome)
        {
            Class_ReadWriteSetUpXml class_ReadWriteSetUpXml = new Class_ReadWriteSetUpXml();
            class_ReadWriteSetUpXml = _GetSetUpXml<Class_ReadWriteSetUpXml>();
            class_ReadWriteSetUpXml.OpenWelcome = OpenWelcome;
            return xmlUtil.ObjectSerialXml(Application.StartupPath, "SetUp", class_ReadWriteSetUpXml);
        }
        /// <summary>
        /// 设置GridView字体大小
        /// </summary>
        /// <param name="FontSize"></param>
        /// <returns></returns>
        public bool SetGridFontSize(float FontSize)
        {
            Class_ReadWriteSetUpXml class_ReadWriteSetUpXml = new Class_ReadWriteSetUpXml();
            //class_ReadWriteSetUpXml = _GetSetUpXml<class_ReadWriteSetUpXml>();
            if (_GetSetUpXml<Class_ReadWriteSetUpXml>() != null)
                class_ReadWriteSetUpXml.GridFontSize = FontSize;
            else
                class_ReadWriteSetUpXml.GridFontSize = 11;
            return xmlUtil.ObjectSerialXml(Application.StartupPath, "SetUp", class_ReadWriteSetUpXml);
        }
        /// <summary>
        /// 得到GridView字体大小尺寸
        /// </summary>
        /// <returns></returns>
        public float GetGridFontSize()
        {
            Class_ReadWriteSetUpXml class_ReadWriteSetUpXml = new Class_ReadWriteSetUpXml();
            class_ReadWriteSetUpXml = _GetSetUpXml<Class_ReadWriteSetUpXml>();
            if (class_ReadWriteSetUpXml != null)
                return class_ReadWriteSetUpXml.GridFontSize;
            else
                return 11;
        }
        /// <summary>
        /// 得到XML内容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private T _GetSetUpXml<T>() where T : class
        {
            string SetUpXmlFieldName = string.Format("{0}\\{1}.xml", Application.StartupPath, "SetUp");
            if (File.Exists(SetUpXmlFieldName))
                return xmlUtil.XmlSerialObject<T>(SetUpXmlFieldName);
            else
                return null;
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

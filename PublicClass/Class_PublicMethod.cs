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
                        class_PageInfomationMode.projectId = class_SelectAllModel.class_Create.SystemName;
                        class_PageInfomationMode.pageType = "select";
                        class_PageInfomationMode.pageVersion = 0;
                        class_PageInfomationMode.createTime = System.DateTime.Now;
                        class_PageInfomationMode.lastUpdateTime = class_PageInfomationMode.createTime;
                        class_PageInfomationMode.createOperatorId = class_SelectAllModel.class_Create.CreateManId;
                        class_PageInfomationMode.doOperatorId = class_SelectAllModel.class_Create.CreateDoId;
                        class_PageInfomationMode.frontOperatorId = class_SelectAllModel.class_Create.CreateFrontDoId;
                        class_PageInfomationMode.createOperator = class_SelectAllModel.class_Create.CreateMan;
                        class_PageInfomationMode.doOperator = class_SelectAllModel.class_Create.CreateDo;
                        class_PageInfomationMode.frontOperator = class_SelectAllModel.class_Create.CreateFrontDo;
                        class_PageInfomationMode.finishCount = 0;
                        class_PageInfomationMode.readOnly = class_SelectAllModel.class_Create.ReadOnly;
                        if (class_SelectAllModel.class_SubList.Count > 0)
                            class_PageInfomationMode.methodRemark = class_SelectAllModel.class_SubList[0].MethodContent;
                        SaveOk = class_SQLiteOperator.InsertIntoPageKey(class_PageInfomationMode);
                    }
                    break;
                case "Class_InsertAllModel":
                    {
                        Class_InsertAllModel class_InsertAllModel = new Class_InsertAllModel();
                        class_InsertAllModel = t as Class_InsertAllModel;
                        class_PageInfomationMode.pageKey = class_InsertAllModel.class_Create.MethodId;
                        class_PageInfomationMode.projectId = class_InsertAllModel.class_Create.SystemName;
                        class_PageInfomationMode.pageType = "insert";
                        class_PageInfomationMode.pageVersion = 0;
                        class_PageInfomationMode.createTime = System.DateTime.Now;
                        class_PageInfomationMode.lastUpdateTime = class_PageInfomationMode.createTime;
                        class_PageInfomationMode.createOperatorId = class_InsertAllModel.class_Create.CreateManId;
                        class_PageInfomationMode.doOperatorId = class_InsertAllModel.class_Create.CreateDoId;
                        class_PageInfomationMode.frontOperatorId = class_InsertAllModel.class_Create.CreateFrontDoId;
                        class_PageInfomationMode.createOperator = class_InsertAllModel.class_Create.CreateMan;
                        class_PageInfomationMode.doOperator = class_InsertAllModel.class_Create.CreateDo;
                        class_PageInfomationMode.frontOperator = class_InsertAllModel.class_Create.CreateFrontDo;
                        class_PageInfomationMode.finishCount = 0;
                        class_PageInfomationMode.readOnly = class_InsertAllModel.class_Create.ReadOnly;
                        if (class_InsertAllModel.class_SubList.Count > 0)
                            class_PageInfomationMode.methodRemark = class_InsertAllModel.class_SubList[0].MethodContent;
                        SaveOk = class_SQLiteOperator.InsertIntoPageKey(class_PageInfomationMode);
                    }
                    break;
                case "Class_UpdateAllModel":
                    {
                        Class_UpdateAllModel class_UpdateAllModel = new Class_UpdateAllModel();
                        class_UpdateAllModel = t as Class_UpdateAllModel;
                        class_PageInfomationMode.pageKey = class_UpdateAllModel.class_Create.MethodId;
                        class_PageInfomationMode.projectId = class_UpdateAllModel.class_Create.SystemName;
                        class_PageInfomationMode.pageType = "update";
                        class_PageInfomationMode.pageVersion = 0;
                        class_PageInfomationMode.createTime = System.DateTime.Now;
                        class_PageInfomationMode.lastUpdateTime = class_PageInfomationMode.createTime;
                        class_PageInfomationMode.createOperatorId = class_UpdateAllModel.class_Create.CreateManId;
                        class_PageInfomationMode.doOperatorId = class_UpdateAllModel.class_Create.CreateDoId;
                        class_PageInfomationMode.frontOperatorId = class_UpdateAllModel.class_Create.CreateFrontDoId;
                        class_PageInfomationMode.createOperator = class_UpdateAllModel.class_Create.CreateMan;
                        class_PageInfomationMode.doOperator = class_UpdateAllModel.class_Create.CreateDo;
                        class_PageInfomationMode.frontOperator = class_UpdateAllModel.class_Create.CreateFrontDo;
                        class_PageInfomationMode.finishCount = 0;
                        class_PageInfomationMode.readOnly = class_UpdateAllModel.class_Create.ReadOnly;
                        if (class_UpdateAllModel.class_SubList.Count > 0)
                            class_PageInfomationMode.methodRemark = class_UpdateAllModel.class_SubList[0].MethodContent;
                        SaveOk = class_SQLiteOperator.InsertIntoPageKey(class_PageInfomationMode);
                    }
                    break;
                case "Class_DeleteAllModel":
                    {
                        Class_DeleteAllModel class_DeleteAllModel = new Class_DeleteAllModel();
                        class_DeleteAllModel = t as Class_DeleteAllModel;
                        class_PageInfomationMode.pageKey = class_DeleteAllModel.class_Create.MethodId;
                        class_PageInfomationMode.projectId = class_DeleteAllModel.class_Create.SystemName;
                        class_PageInfomationMode.pageType = "delete";
                        class_PageInfomationMode.pageVersion = 0;
                        class_PageInfomationMode.createTime = System.DateTime.Now;
                        class_PageInfomationMode.lastUpdateTime = class_PageInfomationMode.createTime;
                        class_PageInfomationMode.createOperatorId = class_DeleteAllModel.class_Create.CreateManId;
                        class_PageInfomationMode.doOperatorId = class_DeleteAllModel.class_Create.CreateDoId;
                        class_PageInfomationMode.frontOperatorId = class_DeleteAllModel.class_Create.CreateFrontDoId;
                        class_PageInfomationMode.createOperator = class_DeleteAllModel.class_Create.CreateMan;
                        class_PageInfomationMode.doOperator = class_DeleteAllModel.class_Create.CreateDo;
                        class_PageInfomationMode.frontOperator = class_DeleteAllModel.class_Create.CreateFrontDo;
                        class_PageInfomationMode.finishCount = 0;
                        class_PageInfomationMode.readOnly = class_DeleteAllModel.class_Create.ReadOnly;
                        if (class_DeleteAllModel.class_SubList.Count > 0)
                            class_PageInfomationMode.methodRemark = class_DeleteAllModel.class_SubList[0].MethodContent;
                        SaveOk = class_SQLiteOperator.InsertIntoPageKey(class_PageInfomationMode);
                    }
                    break;
                case "Class_DataBaseConDefault":
                case "Class_SystemDefault":
                    SaveOk = true;
                    break;
                default:
                    SaveOk = true;
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
                            returnKey = null;
                    }
                    break;
                case "insert":
                    returnKey = Class_Tool.getKeyId("IN");
                    Class_InsertAllModel class_InsertAllModel = new Class_InsertAllModel();
                    class_InsertAllModel = this.FromXmlToInsertObject<Class_InsertAllModel>(xmlFileName);
                    class_InsertAllModel.class_Create.MethodId = returnKey;
                    class_InsertAllModel.class_Create.DateTime = System.DateTime.Now;
                    if ((true) && !this.InsertToXml(class_InsertAllModel.class_Create.MethodId, class_InsertAllModel))
                        returnKey = null;
                    break;
                case "update":
                    returnKey = Class_Tool.getKeyId("UP");
                    Class_UpdateAllModel class_UpdateAllModel = new Class_UpdateAllModel();
                    class_UpdateAllModel = this.FromXmlToUpdateObject<Class_UpdateAllModel>(xmlFileName);
                    class_UpdateAllModel.class_Create.MethodId = returnKey;
                    class_UpdateAllModel.class_Create.DateTime = System.DateTime.Now;
                    if ((true) && !this.UpdateToXml(class_UpdateAllModel.class_Create.MethodId, class_UpdateAllModel))
                        returnKey = null;
                    break;
                case "delete":
                    returnKey = Class_Tool.getKeyId("DE");
                    Class_DeleteAllModel class_DeleteAllModel = new Class_DeleteAllModel();
                    class_DeleteAllModel = this.FromXmlToDeleteObject<Class_DeleteAllModel>(xmlFileName);
                    class_DeleteAllModel.class_Create.MethodId = returnKey;
                    class_DeleteAllModel.class_Create.DateTime = System.DateTime.Now;
                    if ((true) && !this.DeleteToXml(class_DeleteAllModel.class_Create.MethodId, class_DeleteAllModel))
                        returnKey = null;
                    break;
                default:
                    {
                        returnKey = Class_Tool.getKeyId("SE");
                        Class_SelectAllModel class_SelectAllModel = new Class_SelectAllModel();
                        class_SelectAllModel = this.FromXmlToSelectObject<Class_SelectAllModel>(xmlFileName);
                        class_SelectAllModel.class_Create.MethodId = returnKey;
                        class_SelectAllModel.class_Create.DateTime = System.DateTime.Now;
                        if ((true) && !this.SelectToXml(class_SelectAllModel.class_Create.MethodId, class_SelectAllModel))
                            returnKey = null;
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
        public T FromXmlToInsertObject<T>(string fileFullName) where T : class
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
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool SystemDefaultValueToXml<T>(string fileName, T t)
        {
            return SaveToXml<T>("SystemDefault", fileName, t);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileFullName"></param>
        /// <returns></returns>
        public T FromXmlToSystemDefaultObject<T>(string fileFullName) where T : class
        {
            return _FromXmlToObject<T>("SystemDefault", fileFullName);
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

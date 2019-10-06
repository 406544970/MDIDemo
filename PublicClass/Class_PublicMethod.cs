﻿using DevExpress.XtraEditors;
using MDIDemo.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MDIDemo.PublicClass.PageVersionListInParam;

namespace MDIDemo.PublicClass
{
    public class Class_PublicMethod
    {
        private XmlUtil xmlUtil;
        private Class_SQLiteOperator class_SQLiteOperator;
        private Class_Remote class_Remote;
        public Class_PublicMethod()
        {
            xmlUtil = new XmlUtil();
            class_SQLiteOperator = new Class_SQLiteOperator();
            class_Remote = new Class_Remote();
        }
        private List<Class_ParaArray> GetVersionPara(PageModel pageModel)
        {
            List<Class_ParaArray> class_ParaArrays = new List<Class_ParaArray>();

            #region
            Class_ParaArray class_ParaArray_pageKey = new Class_ParaArray();
            class_ParaArray_pageKey.ParaName = "pageKey";
            class_ParaArray_pageKey.ParaValue = pageModel.pageKey;
            Class_ParaArray class_ParaArray_projectId = new Class_ParaArray();
            class_ParaArray_projectId.ParaName = "projectId";
            class_ParaArray_projectId.ParaValue = pageModel.projectId;
            Class_ParaArray class_ParaArray_pageType = new Class_ParaArray();
            class_ParaArray_pageType.ParaName = "pageType";
            class_ParaArray_pageType.ParaValue = pageModel.pageType;
            Class_ParaArray class_ParaArray_pageVersion = new Class_ParaArray();
            class_ParaArray_pageVersion.ParaName = "pageVersion";
            class_ParaArray_pageVersion.ParaValue = pageModel.pageVersion;
            Class_ParaArray class_ParaArray_createTime = new Class_ParaArray();
            class_ParaArray_createTime.ParaName = "createTime";
            class_ParaArray_createTime.ParaValue = pageModel.createTime;
            Class_ParaArray class_ParaArray_lastUpdateTime = new Class_ParaArray();
            class_ParaArray_lastUpdateTime.ParaName = "lastUpdateTime";
            class_ParaArray_lastUpdateTime.ParaValue = pageModel.lastUpdateTime;
            Class_ParaArray class_ParaArray_createOperator = new Class_ParaArray();
            class_ParaArray_createOperator.ParaName = "createOperator";
            class_ParaArray_createOperator.ParaValue = pageModel.createOperator;
            Class_ParaArray class_ParaArray_createOperatorId = new Class_ParaArray();
            class_ParaArray_createOperatorId.ParaName = "createOperatorId";
            class_ParaArray_createOperatorId.ParaValue = pageModel.createOperatorId;
            Class_ParaArray class_ParaArray_doOperator = new Class_ParaArray();
            class_ParaArray_doOperator.ParaName = "doOperator";
            class_ParaArray_doOperator.ParaValue = pageModel.doOperator;
            Class_ParaArray class_ParaArray_doOperatorId = new Class_ParaArray();
            class_ParaArray_doOperatorId.ParaName = "doOperatorId";
            class_ParaArray_doOperatorId.ParaValue = pageModel.doOperatorId;
            Class_ParaArray class_ParaArray_frontOperator = new Class_ParaArray();
            class_ParaArray_frontOperator.ParaName = "frontOperator";
            class_ParaArray_frontOperator.ParaValue = pageModel.frontOperator;
            Class_ParaArray class_ParaArray_frontOperatorId = new Class_ParaArray();
            class_ParaArray_frontOperatorId.ParaName = "frontOperatorId";
            class_ParaArray_frontOperatorId.ParaValue = pageModel.frontOperatorId;
            Class_ParaArray class_ParaArray_finishCount = new Class_ParaArray();
            class_ParaArray_finishCount.ParaName = "finishCount";
            class_ParaArray_finishCount.ParaValue = pageModel.finishCount;
            Class_ParaArray class_ParaArray_readOnly = new Class_ParaArray();
            class_ParaArray_readOnly.ParaName = "readOnly";
            class_ParaArray_readOnly.ParaValue = pageModel.readOnly;
            Class_ParaArray class_ParaArray_methodRemark = new Class_ParaArray();
            class_ParaArray_methodRemark.ParaName = "methodRemark";
            class_ParaArray_methodRemark.ParaValue = pageModel.methodRemark;
            #endregion

            #region
            class_ParaArrays.Add(class_ParaArray_pageKey);
            class_ParaArrays.Add(class_ParaArray_projectId);
            class_ParaArrays.Add(class_ParaArray_pageType);
            class_ParaArrays.Add(class_ParaArray_pageVersion);
            class_ParaArrays.Add(class_ParaArray_createTime);
            class_ParaArrays.Add(class_ParaArray_lastUpdateTime);
            class_ParaArrays.Add(class_ParaArray_createOperator);
            class_ParaArrays.Add(class_ParaArray_createOperatorId);
            class_ParaArrays.Add(class_ParaArray_doOperator);
            class_ParaArrays.Add(class_ParaArray_doOperatorId);
            class_ParaArrays.Add(class_ParaArray_frontOperator);
            class_ParaArrays.Add(class_ParaArray_frontOperatorId);
            class_ParaArrays.Add(class_ParaArray_finishCount);
            class_ParaArrays.Add(class_ParaArray_readOnly);
            class_ParaArrays.Add(class_ParaArray_methodRemark);
            #endregion

            return class_ParaArrays;
        }
        /// <summary>
        /// PUSH到远程
        /// </summary>
        /// <param name="PageKey"></param>
        /// <returns></returns>
        public bool PushToRemote(string PageKey)
        {
            bool ResultValue = false;
            if (PageKey == null)
                return ResultValue;

            PageModel pageModel = class_SQLiteOperator.GetPageByPageKey(PageKey);
            if (pageModel != null)
            {
                string AllPathFileName = string.Format("{0}\\{1}\\{2}.xml", Application.StartupPath, pageModel.pageType, pageModel.pageKey);
                if (File.Exists(AllPathFileName))
                {
                    if (class_SQLiteOperator.UpdatePushSign(pageModel.pageKey))
                    {
                        ResultVO<bool> resultVO = new ResultVO<bool>();
                        switch (pageModel.pageType)
                        {
                            case "select":
                                resultVO = class_Remote.UploadFileSelect<bool>(AllPathFileName, pageModel.pageKey + ".xml");
                                break;
                            case "insert":
                                resultVO = class_Remote.UploadFileInsert<bool>(AllPathFileName, pageModel.pageKey + ".xml");
                                break;
                            case "update":
                                resultVO = class_Remote.UploadFileUpdate<bool>(AllPathFileName, pageModel.pageKey + ".xml");
                                break;
                            case "delete":
                                resultVO = class_Remote.UploadFileDelete<bool>(AllPathFileName, pageModel.pageKey + ".xml");
                                break;
                            default:
                                resultVO = class_Remote.UploadFileSelect<bool>(AllPathFileName, pageModel.pageKey + ".xml");
                                break;
                        }
                        if (resultVO.code == 0)
                        {
                            List<Class_ParaArray> class_ParaArrays = new List<Class_ParaArray>();
                            class_ParaArrays = GetVersionPara(pageModel);
                            ResultValue = class_Remote.InsertPage(class_ParaArrays) > 0 ? true : false;
                            class_ParaArrays.Clear();
                        }
                        else
                        {
                            ResultValue = ResultValue && false;
                        }
                    }
                }
            }
            return ResultValue;
        }
        public bool GetVersionUpdateInfo(ProgressBarControl progressBarControl)
        {
            bool ResultValue = true;
            List<PageVersionInParam> pageKey = new List<PageVersionInParam>();
            #region 从SQLITE读取数据
            List<string> vs = new List<string>();
            vs = class_SQLiteOperator.GetLocalPageList();
            if (vs != null)
            {
                foreach (string item in vs)
                {
                    string[] row = item.Split(';');
                    PageVersionInParam pageVersionInParam = new PageVersionInParam()
                    {
                        pageKey = row[0],
                        pageVersion = Convert.ToInt32(row[1]),
                        pageType = row[2]
                    };
                    pageKey.Add(pageVersionInParam);
                }
                vs.Clear();
            }

            #endregion
            PageVersionListInParam pageVersionListInParam = new PageVersionListInParam();
            pageVersionListInParam.pageKey = pageKey;
            Class_Remote class_Remote = new Class_Remote();
            ResultVO<List<PageModel>> resultVO = new ResultVO<List<PageModel>>();
            resultVO = class_Remote.SelectVersionList<List<PageModel>>(pageVersionListInParam);
            if (resultVO.code == 0)
            {
                if (resultVO.count > 0)
                {
                    if (progressBarControl != null)
                    {
                        progressBarControl.Properties.Maximum = Convert.ToInt32(resultVO.count);
                        progressBarControl.Properties.Maximum = 0;
                        progressBarControl.Properties.Step = 1;
                        progressBarControl.Position = 0;
                    }
                    List<PageModel> pageModels = new List<PageModel>();
                    pageModels = resultVO.data;
                    int index = 1;
                    int changeCount = 0;
                    foreach (PageModel pageModel in pageModels)
                    {
                        string FileName = string.Format("{0}\\{1}\\{2}.xml"
                            , Application.StartupPath
                            , pageModel.pageType
                            , pageModel.pageKey);
                        switch (pageModel.operateType)
                        {
                            case -1:
                                {
                                    if (class_SQLiteOperator.DeleteByPageKey(pageModel.pageKey))
                                    {
                                        if (File.Exists(FileName))
                                            File.Delete(FileName);
                                        changeCount++;
                                    }
                                    //ResultVO<int> resultVODel = new ResultVO<int>();
                                    //resultVODel = class_Remote.DeletePage<int>(pageModel.pageKey);
                                    //if (resultVODel.code == 0)
                                    //    changeCount += resultVODel.data;
                                    //要删除SQLITE数据与文件
                                }
                                break;
                            case 0:
                                {
                                    //更新文件
                                    //更新SQLITE
                                    changeCount += class_SQLiteOperator.DownLoadUpate(pageModel);
                                }
                                break;
                            case 1:
                                {
                                    //更新文件
                                    //更新SQLITE
                                    changeCount += class_SQLiteOperator.DownLoadInsert(pageModel);
                                }
                                break;
                            default:
                                break;
                        }
                        if (progressBarControl != null)
                        {
                            progressBarControl.Position = index++;
                            Thread.Sleep(0);
                            Application.DoEvents();
                        }
                    }

                    ResultValue = ResultValue && true;
                }
            }
            return ResultValue;
        }

        private bool SaveToXml<T>(string xmlPath, string fileName, T t, bool PageVersionSign)
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
                        SaveOk = class_SQLiteOperator.InsertIntoPageKey(class_PageInfomationMode, PageVersionSign);
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
                        SaveOk = class_SQLiteOperator.InsertIntoPageKey(class_PageInfomationMode, PageVersionSign);
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
                        SaveOk = class_SQLiteOperator.InsertIntoPageKey(class_PageInfomationMode, PageVersionSign);
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
                        SaveOk = class_SQLiteOperator.InsertIntoPageKey(class_PageInfomationMode, PageVersionSign);
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
                        if ((true) && !this.SelectToXml(class_SelectAllModel.class_Create.MethodId, class_SelectAllModel, false))
                            returnKey = null;
                    }
                    break;
                case "insert":
                    returnKey = Class_Tool.getKeyId("IN");
                    Class_InsertAllModel class_InsertAllModel = new Class_InsertAllModel();
                    class_InsertAllModel = this.FromXmlToInsertObject<Class_InsertAllModel>(xmlFileName);
                    class_InsertAllModel.class_Create.MethodId = returnKey;
                    class_InsertAllModel.class_Create.DateTime = System.DateTime.Now;
                    if ((true) && !this.InsertToXml(class_InsertAllModel.class_Create.MethodId, class_InsertAllModel, false))
                        returnKey = null;
                    break;
                case "update":
                    returnKey = Class_Tool.getKeyId("UP");
                    Class_UpdateAllModel class_UpdateAllModel = new Class_UpdateAllModel();
                    class_UpdateAllModel = this.FromXmlToUpdateObject<Class_UpdateAllModel>(xmlFileName);
                    class_UpdateAllModel.class_Create.MethodId = returnKey;
                    class_UpdateAllModel.class_Create.DateTime = System.DateTime.Now;
                    if ((true) && !this.UpdateToXml(class_UpdateAllModel.class_Create.MethodId, class_UpdateAllModel, false))
                        returnKey = null;
                    break;
                case "delete":
                    returnKey = Class_Tool.getKeyId("DE");
                    Class_DeleteAllModel class_DeleteAllModel = new Class_DeleteAllModel();
                    class_DeleteAllModel = this.FromXmlToDeleteObject<Class_DeleteAllModel>(xmlFileName);
                    class_DeleteAllModel.class_Create.MethodId = returnKey;
                    class_DeleteAllModel.class_Create.DateTime = System.DateTime.Now;
                    if ((true) && !this.DeleteToXml(class_DeleteAllModel.class_Create.MethodId, class_DeleteAllModel, false))
                        returnKey = null;
                    break;
                default:
                    {
                        returnKey = Class_Tool.getKeyId("SE");
                        Class_SelectAllModel class_SelectAllModel = new Class_SelectAllModel();
                        class_SelectAllModel = this.FromXmlToSelectObject<Class_SelectAllModel>(xmlFileName);
                        class_SelectAllModel.class_Create.MethodId = returnKey;
                        class_SelectAllModel.class_Create.DateTime = System.DateTime.Now;
                        if ((true) && !this.SelectToXml(class_SelectAllModel.class_Create.MethodId, class_SelectAllModel, false))
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
            return SaveToXml<T>("DataBaseDefault", fileName, t, false);
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
            return SaveToXml<T>("SystemDefault", fileName, t,false);
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
        public bool SelectToXml<T>(string fileName, T t, bool PageVersionSign)
        {
            return SaveToXml<T>("select", fileName, t, PageVersionSign);
        }
        /// <summary>
        /// 保存Insert
        /// </summary>
        /// <typeparam name="T">类名</typeparam>
        /// <param name="fileName">文件名，不用加扩展名</param>
        /// <param name="t">对象</param>
        /// <returns></returns>
        public bool InsertToXml<T>(string fileName, T t, bool PageVersionSign)
        {
            return SaveToXml<T>("insert", fileName, t, PageVersionSign);
        }
        /// <summary>
        /// 保存Update
        /// </summary>
        /// <typeparam name="T">类名</typeparam>
        /// <param name="fileName">文件名，不用加扩展名</param>
        /// <param name="t">对象</param>
        /// <returns></returns>
        public bool UpdateToXml<T>(string fileName, T t, bool PageVersionSign)
        {
            return SaveToXml<T>("update", fileName, t, PageVersionSign);
        }
        /// <summary>
        /// 保存Delete
        /// </summary>
        /// <typeparam name="T">类名</typeparam>
        /// <param name="fileName">文件名，不用加扩展名</param>
        /// <param name="t">对象</param>
        /// <returns></returns>
        public bool DeleteToXml<T>(string fileName, T t, bool PageVersionSign)
        {
            return SaveToXml<T>("delete", fileName, t, PageVersionSign);
        }


    }
}

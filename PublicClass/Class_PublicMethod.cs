using DevExpress.XtraEditors;
using MDIDemo.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MDIDemo.PublicClass.Class_SelectAllModel;
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
            string RemoteAddress = null;
            int RemotePort = 0;
            string MyBaseUrl = null;
            Class_AllParamSetUp class_AllParamSetUp = new Class_AllParamSetUp();
            class_AllParamSetUp = _FromXmlToObject<Class_AllParamSetUp>("AllParamSetUp", "Class_AllParamSetUp");
            if (class_AllParamSetUp != null)
            {
                RemoteAddress = class_AllParamSetUp.RemoteAddress;
                RemotePort = class_AllParamSetUp.RemotePort;
                MyBaseUrl = RemoteAddress;
                if (RemotePort > 0)
                {
                    MyBaseUrl += ":" + RemotePort.ToString();
                }
                class_Remote = new Class_Remote(MyBaseUrl, true);
            }
            else
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
        public void DownLoadFile()
        {
            class_Remote.DownLoadFile("SE20191005142535878812C8537FD3C", "select");
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
                                    if (class_Remote.DeletePage(pageModel.pageKey, pageModel.pageType) > 0)
                                    {
                                        if (class_SQLiteOperator.DeleteByPageKey(pageModel.pageKey))
                                        {
                                            if (File.Exists(FileName))
                                                File.Delete(FileName);
                                            changeCount++;
                                        }
                                    }
                                }
                                break;
                            case 0:
                                {
                                    //更新文件
                                    byte[] FileByte = class_Remote.DownLoadFile(pageModel.pageKey, pageModel.pageType);
                                    if (FileByte.Length > 0)
                                    {
                                        File.WriteAllBytes(FileName, FileByte);
                                        //更新SQLITE
                                        changeCount += class_SQLiteOperator.DownLoadUpate(pageModel);
                                    }
                                }
                                break;
                            case 1:
                                {
                                    //更新文件
                                    byte[] FileByte = class_Remote.DownLoadFile(pageModel.pageKey, pageModel.pageType);
                                    if (FileByte.Length > 0)
                                    {
                                        File.WriteAllBytes(FileName, FileByte);
                                        //更新SQLITE
                                        changeCount += class_SQLiteOperator.DownLoadInsert(pageModel);
                                    }
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
                case "Class_AllParamSetUp":
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
            class_Remote.DeletePage(xmlFileName, classType);
            if (File.Exists(string.Format(@"{0}\\{1}\\{2}.xml", Application.StartupPath, classType, xmlFileName)))
                File.Delete(string.Format(@"{0}\\{1}\\{2}.xml", Application.StartupPath, classType, xmlFileName));
            return class_SQLiteOperator.DeleteByPageKey(xmlFileName);
        }

        public string CopyToNewXml(string xmlFileName, string NewPageType, string OldPageType)
        {
            string returnKey;
            switch (NewPageType)
            {
                case "select":
                    {
                        returnKey = Class_Tool.getKeyId("SE");
                        Class_SelectAllModel class_SelectAllModel = new Class_SelectAllModel();
                        #region COPY
                        if (NewPageType == OldPageType)
                            class_SelectAllModel = this.FromXmlToSelectObject<Class_SelectAllModel>(xmlFileName);
                        else
                        {
                            switch (OldPageType)
                            {
                                case "select":
                                    Class_SelectAllModel class_SelectAllModelOld = new Class_SelectAllModel();
                                    class_SelectAllModelOld = this.FromXmlToSelectObject<Class_SelectAllModel>(xmlFileName);
                                    if (class_SelectAllModelOld.class_Create != null)
                                    {
                                        class_SelectAllModel.class_Create.CreateDo = class_SelectAllModelOld.class_Create.CreateDo;
                                        class_SelectAllModel.class_Create.CreateDoId = class_SelectAllModelOld.class_Create.CreateDoId;
                                        class_SelectAllModel.class_Create.CreateFrontDo = class_SelectAllModelOld.class_Create.CreateFrontDo;
                                        class_SelectAllModel.class_Create.CreateFrontDoId = class_SelectAllModelOld.class_Create.CreateFrontDoId;
                                        class_SelectAllModel.class_Create.CreateMan = class_SelectAllModelOld.class_Create.CreateMan;
                                        class_SelectAllModel.class_Create.CreateManId = class_SelectAllModelOld.class_Create.CreateManId;
                                        class_SelectAllModel.class_Create.EnglishSign = class_SelectAllModelOld.class_Create.EnglishSign;
                                        class_SelectAllModel.class_Create.HttpRequestType = class_SelectAllModelOld.class_Create.HttpRequestType;
                                        class_SelectAllModel.class_Create.MethodSite = class_SelectAllModelOld.class_Create.MethodSite;
                                        class_SelectAllModel.class_Create.MicroServiceName = class_SelectAllModelOld.class_Create.MicroServiceName;
                                        class_SelectAllModel.class_Create.Port = class_SelectAllModelOld.class_Create.Port;
                                        class_SelectAllModel.class_Create.ReadOnly = class_SelectAllModelOld.class_Create.ReadOnly;
                                        class_SelectAllModel.class_Create.SwaggerSign = class_SelectAllModelOld.class_Create.SwaggerSign;
                                        class_SelectAllModel.class_Create.SystemName = class_SelectAllModelOld.class_Create.SystemName;
                                    }
                                    if (class_SelectAllModelOld.class_SubList != null && class_SelectAllModelOld.class_SubList.Count > 0)
                                    {
                                        Class_SelectAllModel.Class_Sub class_Sub;
                                        if (class_SelectAllModel.class_SubList.Count == 0)
                                        {
                                            class_Sub = new Class_SelectAllModel.Class_Sub();
                                            class_SelectAllModel.class_SubList.Add(class_Sub);
                                        }
                                        else
                                            class_Sub = class_SelectAllModel.class_SubList[0];
                                        class_Sub.NameSpace = class_SelectAllModelOld.class_SubList[0].NameSpace;
                                        class_Sub.ControlSwaggerValue = class_SelectAllModelOld.class_SubList[0].ControlSwaggerValue;
                                        class_Sub.ControlSwaggerDescription = class_SelectAllModelOld.class_SubList[0].ControlSwaggerDescription;
                                        class_Sub.MethodContent = class_SelectAllModelOld.class_SubList[0].MethodContent;
                                        class_Sub.ServiceInterFaceReturnRemark = class_SelectAllModelOld.class_SubList[0].ServiceInterFaceReturnRemark;
                                        class_Sub.TableName = class_SelectAllModelOld.class_SubList[0].TableName;
                                        class_Sub.AliasName = class_SelectAllModelOld.class_SubList[0].AliasName;
                                    }
                                    class_SelectAllModel.class_SelectDataBase.dataBaseName = class_SelectAllModelOld.class_SelectDataBase.dataBaseName;
                                    class_SelectAllModel.class_SelectDataBase.databaseType = class_SelectAllModelOld.class_SelectDataBase.databaseType;
                                    class_SelectAllModel.class_SelectDataBase.dataSourcePassWord = class_SelectAllModelOld.class_SelectDataBase.dataSourcePassWord;
                                    class_SelectAllModel.class_SelectDataBase.dataSourceUrl = class_SelectAllModelOld.class_SelectDataBase.dataSourceUrl;
                                    class_SelectAllModel.class_SelectDataBase.dataSourceUserName = class_SelectAllModelOld.class_SelectDataBase.dataSourceUserName;
                                    class_SelectAllModel.class_SelectDataBase.Port = class_SelectAllModelOld.class_SelectDataBase.Port;
                                    class_SelectAllModel.AllPackerName = class_SelectAllModelOld.AllPackerName;
                                    break;
                                case "insert":
                                    Class_InsertAllModel class_InsertAllModelOld = new Class_InsertAllModel();
                                    class_InsertAllModelOld = this.FromXmlToInsertObject<Class_InsertAllModel>(xmlFileName);
                                    if (class_InsertAllModelOld.class_Create != null)
                                    {
                                        class_SelectAllModel.class_Create.CreateDo = class_InsertAllModelOld.class_Create.CreateDo;
                                        class_SelectAllModel.class_Create.CreateDoId = class_InsertAllModelOld.class_Create.CreateDoId;
                                        class_SelectAllModel.class_Create.CreateFrontDo = class_InsertAllModelOld.class_Create.CreateFrontDo;
                                        class_SelectAllModel.class_Create.CreateFrontDoId = class_InsertAllModelOld.class_Create.CreateFrontDoId;
                                        class_SelectAllModel.class_Create.CreateMan = class_InsertAllModelOld.class_Create.CreateMan;
                                        class_SelectAllModel.class_Create.CreateManId = class_InsertAllModelOld.class_Create.CreateManId;
                                        class_SelectAllModel.class_Create.EnglishSign = class_InsertAllModelOld.class_Create.EnglishSign;
                                        class_SelectAllModel.class_Create.HttpRequestType = class_InsertAllModelOld.class_Create.HttpRequestType;
                                        class_SelectAllModel.class_Create.MethodSite = class_InsertAllModelOld.class_Create.MethodSite;
                                        class_SelectAllModel.class_Create.MicroServiceName = class_InsertAllModelOld.class_Create.MicroServiceName;
                                        class_SelectAllModel.class_Create.Port = class_InsertAllModelOld.class_Create.Port;
                                        class_SelectAllModel.class_Create.ReadOnly = class_InsertAllModelOld.class_Create.ReadOnly;
                                        class_SelectAllModel.class_Create.SwaggerSign = class_InsertAllModelOld.class_Create.SwaggerSign;
                                        class_SelectAllModel.class_Create.SystemName = class_InsertAllModelOld.class_Create.SystemName;
                                    }
                                    if (class_InsertAllModelOld.class_SubList != null && class_InsertAllModelOld.class_SubList.Count > 0)
                                    {
                                        Class_SelectAllModel.Class_Sub class_Sub;
                                        if (class_SelectAllModel.class_SubList.Count == 0)
                                        {
                                            class_Sub = new Class_SelectAllModel.Class_Sub();
                                            class_SelectAllModel.class_SubList.Add(class_Sub);
                                        }
                                        else
                                            class_Sub = class_SelectAllModel.class_SubList[0];
                                        class_Sub.NameSpace = class_InsertAllModelOld.class_SubList[0].NameSpace;
                                        class_Sub.ControlSwaggerValue = class_InsertAllModelOld.class_SubList[0].ControlSwaggerValue;
                                        class_Sub.ControlSwaggerDescription = class_InsertAllModelOld.class_SubList[0].ControlSwaggerDescription;
                                        class_Sub.MethodContent = class_InsertAllModelOld.class_SubList[0].MethodContent;
                                        class_Sub.ServiceInterFaceReturnRemark = class_InsertAllModelOld.class_SubList[0].ServiceInterFaceReturnRemark;
                                        class_Sub.TableName = class_InsertAllModelOld.class_SubList[0].TableName;
                                        class_Sub.AliasName = class_InsertAllModelOld.class_SubList[0].AliasName;
                                    }
                                    class_SelectAllModel.class_SelectDataBase.dataBaseName = class_InsertAllModelOld.class_SelectDataBase.dataBaseName;
                                    class_SelectAllModel.class_SelectDataBase.databaseType = class_InsertAllModelOld.class_SelectDataBase.databaseType;
                                    class_SelectAllModel.class_SelectDataBase.dataSourcePassWord = class_InsertAllModelOld.class_SelectDataBase.dataSourcePassWord;
                                    class_SelectAllModel.class_SelectDataBase.dataSourceUrl = class_InsertAllModelOld.class_SelectDataBase.dataSourceUrl;
                                    class_SelectAllModel.class_SelectDataBase.dataSourceUserName = class_InsertAllModelOld.class_SelectDataBase.dataSourceUserName;
                                    class_SelectAllModel.class_SelectDataBase.Port = class_InsertAllModelOld.class_SelectDataBase.Port;
                                    class_SelectAllModel.AllPackerName = class_InsertAllModelOld.AllPackerName;
                                    break;
                                case "update":
                                    Class_UpdateAllModel class_UpdateAllModelOld = new Class_UpdateAllModel();
                                    class_UpdateAllModelOld = this.FromXmlToUpdateObject<Class_UpdateAllModel>(xmlFileName);
                                    if (class_UpdateAllModelOld.class_Create != null)
                                    {
                                        class_SelectAllModel.class_Create.CreateDo = class_UpdateAllModelOld.class_Create.CreateDo;
                                        class_SelectAllModel.class_Create.CreateDoId = class_UpdateAllModelOld.class_Create.CreateDoId;
                                        class_SelectAllModel.class_Create.CreateFrontDo = class_UpdateAllModelOld.class_Create.CreateFrontDo;
                                        class_SelectAllModel.class_Create.CreateFrontDoId = class_UpdateAllModelOld.class_Create.CreateFrontDoId;
                                        class_SelectAllModel.class_Create.CreateMan = class_UpdateAllModelOld.class_Create.CreateMan;
                                        class_SelectAllModel.class_Create.CreateManId = class_UpdateAllModelOld.class_Create.CreateManId;
                                        class_SelectAllModel.class_Create.EnglishSign = class_UpdateAllModelOld.class_Create.EnglishSign;
                                        class_SelectAllModel.class_Create.HttpRequestType = class_UpdateAllModelOld.class_Create.HttpRequestType;
                                        class_SelectAllModel.class_Create.MethodSite = class_UpdateAllModelOld.class_Create.MethodSite;
                                        class_SelectAllModel.class_Create.MicroServiceName = class_UpdateAllModelOld.class_Create.MicroServiceName;
                                        class_SelectAllModel.class_Create.Port = class_UpdateAllModelOld.class_Create.Port;
                                        class_SelectAllModel.class_Create.ReadOnly = class_UpdateAllModelOld.class_Create.ReadOnly;
                                        class_SelectAllModel.class_Create.SwaggerSign = class_UpdateAllModelOld.class_Create.SwaggerSign;
                                        class_SelectAllModel.class_Create.SystemName = class_UpdateAllModelOld.class_Create.SystemName;
                                    }
                                    if (class_UpdateAllModelOld.class_SubList != null && class_UpdateAllModelOld.class_SubList.Count > 0)
                                    {
                                        Class_SelectAllModel.Class_Sub class_Sub;
                                        if (class_SelectAllModel.class_SubList.Count == 0)
                                        {
                                            class_Sub = new Class_SelectAllModel.Class_Sub();
                                            class_SelectAllModel.class_SubList.Add(class_Sub);
                                        }
                                        else
                                            class_Sub = class_SelectAllModel.class_SubList[0];
                                        class_Sub.NameSpace = class_UpdateAllModelOld.class_SubList[0].NameSpace;
                                        class_Sub.ControlSwaggerValue = class_UpdateAllModelOld.class_SubList[0].ControlSwaggerValue;
                                        class_Sub.ControlSwaggerDescription = class_UpdateAllModelOld.class_SubList[0].ControlSwaggerDescription;
                                        class_Sub.MethodContent = class_UpdateAllModelOld.class_SubList[0].MethodContent;
                                        class_Sub.ServiceInterFaceReturnRemark = class_UpdateAllModelOld.class_SubList[0].ServiceInterFaceReturnRemark;
                                        class_Sub.TableName = class_UpdateAllModelOld.class_SubList[0].TableName;
                                        class_Sub.AliasName = class_UpdateAllModelOld.class_SubList[0].AliasName;
                                    }
                                    class_SelectAllModel.class_SelectDataBase.dataBaseName = class_UpdateAllModelOld.class_SelectDataBase.dataBaseName;
                                    class_SelectAllModel.class_SelectDataBase.databaseType = class_UpdateAllModelOld.class_SelectDataBase.databaseType;
                                    class_SelectAllModel.class_SelectDataBase.dataSourcePassWord = class_UpdateAllModelOld.class_SelectDataBase.dataSourcePassWord;
                                    class_SelectAllModel.class_SelectDataBase.dataSourceUrl = class_UpdateAllModelOld.class_SelectDataBase.dataSourceUrl;
                                    class_SelectAllModel.class_SelectDataBase.dataSourceUserName = class_UpdateAllModelOld.class_SelectDataBase.dataSourceUserName;
                                    class_SelectAllModel.class_SelectDataBase.Port = class_UpdateAllModelOld.class_SelectDataBase.Port;
                                    class_SelectAllModel.AllPackerName = class_UpdateAllModelOld.AllPackerName;
                                    break;
                                case "delete":
                                    Class_DeleteAllModel class_DeleteAllModelOld = new Class_DeleteAllModel();
                                    class_DeleteAllModelOld = this.FromXmlToDeleteObject<Class_DeleteAllModel>(xmlFileName);
                                    if (class_DeleteAllModelOld.class_Create != null)
                                    {
                                        class_SelectAllModel.class_Create.CreateDo = class_DeleteAllModelOld.class_Create.CreateDo;
                                        class_SelectAllModel.class_Create.CreateDoId = class_DeleteAllModelOld.class_Create.CreateDoId;
                                        class_SelectAllModel.class_Create.CreateFrontDo = class_DeleteAllModelOld.class_Create.CreateFrontDo;
                                        class_SelectAllModel.class_Create.CreateFrontDoId = class_DeleteAllModelOld.class_Create.CreateFrontDoId;
                                        class_SelectAllModel.class_Create.CreateMan = class_DeleteAllModelOld.class_Create.CreateMan;
                                        class_SelectAllModel.class_Create.CreateManId = class_DeleteAllModelOld.class_Create.CreateManId;
                                        class_SelectAllModel.class_Create.EnglishSign = class_DeleteAllModelOld.class_Create.EnglishSign;
                                        class_SelectAllModel.class_Create.HttpRequestType = class_DeleteAllModelOld.class_Create.HttpRequestType;
                                        class_SelectAllModel.class_Create.MethodSite = class_DeleteAllModelOld.class_Create.MethodSite;
                                        class_SelectAllModel.class_Create.MicroServiceName = class_DeleteAllModelOld.class_Create.MicroServiceName;
                                        class_SelectAllModel.class_Create.Port = class_DeleteAllModelOld.class_Create.Port;
                                        class_SelectAllModel.class_Create.ReadOnly = class_DeleteAllModelOld.class_Create.ReadOnly;
                                        class_SelectAllModel.class_Create.SwaggerSign = class_DeleteAllModelOld.class_Create.SwaggerSign;
                                        class_SelectAllModel.class_Create.SystemName = class_DeleteAllModelOld.class_Create.SystemName;
                                    }
                                    if (class_DeleteAllModelOld.class_SubList != null && class_DeleteAllModelOld.class_SubList.Count > 0)
                                    {
                                        Class_SelectAllModel.Class_Sub class_Sub;
                                        if (class_SelectAllModel.class_SubList.Count == 0)
                                        {
                                            class_Sub = new Class_SelectAllModel.Class_Sub();
                                            class_SelectAllModel.class_SubList.Add(class_Sub);
                                        }
                                        else
                                            class_Sub = class_SelectAllModel.class_SubList[0];
                                        class_Sub.NameSpace = class_DeleteAllModelOld.class_SubList[0].NameSpace;
                                        class_Sub.ControlSwaggerValue = class_DeleteAllModelOld.class_SubList[0].ControlSwaggerValue;
                                        class_Sub.ControlSwaggerDescription = class_DeleteAllModelOld.class_SubList[0].ControlSwaggerDescription;
                                        class_Sub.MethodContent = class_DeleteAllModelOld.class_SubList[0].MethodContent;
                                        class_Sub.ServiceInterFaceReturnRemark = class_DeleteAllModelOld.class_SubList[0].ServiceInterFaceReturnRemark;
                                        class_Sub.TableName = class_DeleteAllModelOld.class_SubList[0].TableName;
                                        class_Sub.AliasName = class_DeleteAllModelOld.class_SubList[0].AliasName;
                                    }
                                    class_SelectAllModel.class_SelectDataBase.dataBaseName = class_DeleteAllModelOld.class_SelectDataBase.dataBaseName;
                                    class_SelectAllModel.class_SelectDataBase.databaseType = class_DeleteAllModelOld.class_SelectDataBase.databaseType;
                                    class_SelectAllModel.class_SelectDataBase.dataSourcePassWord = class_DeleteAllModelOld.class_SelectDataBase.dataSourcePassWord;
                                    class_SelectAllModel.class_SelectDataBase.dataSourceUrl = class_DeleteAllModelOld.class_SelectDataBase.dataSourceUrl;
                                    class_SelectAllModel.class_SelectDataBase.dataSourceUserName = class_DeleteAllModelOld.class_SelectDataBase.dataSourceUserName;
                                    class_SelectAllModel.class_SelectDataBase.Port = class_DeleteAllModelOld.class_SelectDataBase.Port;
                                    class_SelectAllModel.AllPackerName = class_DeleteAllModelOld.AllPackerName;
                                    break;
                                default:
                                    break;
                            }
                        }
                        #endregion
                        class_SelectAllModel.class_Create.MethodId = returnKey;
                        class_SelectAllModel.class_Create.DateTime = System.DateTime.Now;
                        if (!this.SelectToXml(class_SelectAllModel.class_Create.MethodId, class_SelectAllModel, false))
                            returnKey = null;
                    }
                    break;
                case "insert":
                    {
                        returnKey = Class_Tool.getKeyId("IN");
                        Class_InsertAllModel class_InsertAllModel = new Class_InsertAllModel();
                        #region COPY
                        if (NewPageType == OldPageType)
                            class_InsertAllModel = this.FromXmlToInsertObject<Class_InsertAllModel>(xmlFileName);
                        else
                        {
                            switch (OldPageType)
                            {
                                case "select":
                                    Class_SelectAllModel class_SelectAllModelOld = new Class_SelectAllModel();
                                    class_SelectAllModelOld = this.FromXmlToSelectObject<Class_SelectAllModel>(xmlFileName);
                                    if (class_SelectAllModelOld.class_Create != null)
                                    {
                                        class_InsertAllModel.class_Create.CreateDo = class_SelectAllModelOld.class_Create.CreateDo;
                                        class_InsertAllModel.class_Create.CreateDoId = class_SelectAllModelOld.class_Create.CreateDoId;
                                        class_InsertAllModel.class_Create.CreateFrontDo = class_SelectAllModelOld.class_Create.CreateFrontDo;
                                        class_InsertAllModel.class_Create.CreateFrontDoId = class_SelectAllModelOld.class_Create.CreateFrontDoId;
                                        class_InsertAllModel.class_Create.CreateMan = class_SelectAllModelOld.class_Create.CreateMan;
                                        class_InsertAllModel.class_Create.CreateManId = class_SelectAllModelOld.class_Create.CreateManId;
                                        class_InsertAllModel.class_Create.EnglishSign = class_SelectAllModelOld.class_Create.EnglishSign;
                                        class_InsertAllModel.class_Create.HttpRequestType = class_SelectAllModelOld.class_Create.HttpRequestType;
                                        class_InsertAllModel.class_Create.MethodSite = class_SelectAllModelOld.class_Create.MethodSite;
                                        class_InsertAllModel.class_Create.MicroServiceName = class_SelectAllModelOld.class_Create.MicroServiceName;
                                        class_InsertAllModel.class_Create.Port = class_SelectAllModelOld.class_Create.Port;
                                        class_InsertAllModel.class_Create.ReadOnly = class_SelectAllModelOld.class_Create.ReadOnly;
                                        class_InsertAllModel.class_Create.SwaggerSign = class_SelectAllModelOld.class_Create.SwaggerSign;
                                        class_InsertAllModel.class_Create.SystemName = class_SelectAllModelOld.class_Create.SystemName;
                                    }
                                    if (class_SelectAllModelOld.class_SubList != null && class_SelectAllModelOld.class_SubList.Count > 0)
                                    {
                                        Class_InsertAllModel.Class_Sub class_Sub;
                                        if (class_InsertAllModel.class_SubList.Count == 0)
                                        {
                                            class_Sub = new Class_InsertAllModel.Class_Sub();
                                            class_InsertAllModel.class_SubList.Add(class_Sub);
                                        }
                                        else
                                            class_Sub = class_InsertAllModel.class_SubList[0];
                                        class_Sub.NameSpace = class_SelectAllModelOld.class_SubList[0].NameSpace;
                                        class_Sub.ControlSwaggerValue = class_SelectAllModelOld.class_SubList[0].ControlSwaggerValue;
                                        class_Sub.ControlSwaggerDescription = class_SelectAllModelOld.class_SubList[0].ControlSwaggerDescription;
                                        class_Sub.MethodContent = class_SelectAllModelOld.class_SubList[0].MethodContent;
                                        class_Sub.ServiceInterFaceReturnRemark = class_SelectAllModelOld.class_SubList[0].ServiceInterFaceReturnRemark;
                                        class_Sub.TableName = class_SelectAllModelOld.class_SubList[0].TableName;
                                        class_Sub.AliasName = class_SelectAllModelOld.class_SubList[0].AliasName;
                                    }
                                    class_InsertAllModel.class_SelectDataBase.dataBaseName = class_SelectAllModelOld.class_SelectDataBase.dataBaseName;
                                    class_InsertAllModel.class_SelectDataBase.databaseType = class_SelectAllModelOld.class_SelectDataBase.databaseType;
                                    class_InsertAllModel.class_SelectDataBase.dataSourcePassWord = class_SelectAllModelOld.class_SelectDataBase.dataSourcePassWord;
                                    class_InsertAllModel.class_SelectDataBase.dataSourceUrl = class_SelectAllModelOld.class_SelectDataBase.dataSourceUrl;
                                    class_InsertAllModel.class_SelectDataBase.dataSourceUserName = class_SelectAllModelOld.class_SelectDataBase.dataSourceUserName;
                                    class_InsertAllModel.class_SelectDataBase.Port = class_SelectAllModelOld.class_SelectDataBase.Port;
                                    class_InsertAllModel.AllPackerName = class_SelectAllModelOld.AllPackerName;
                                    break;
                                case "update":
                                    Class_UpdateAllModel class_UpdateAllModelOld = new Class_UpdateAllModel();
                                    class_UpdateAllModelOld = this.FromXmlToUpdateObject<Class_UpdateAllModel>(xmlFileName);
                                    if (class_UpdateAllModelOld.class_Create != null)
                                    {
                                        class_InsertAllModel.class_Create.CreateDo = class_UpdateAllModelOld.class_Create.CreateDo;
                                        class_InsertAllModel.class_Create.CreateDoId = class_UpdateAllModelOld.class_Create.CreateDoId;
                                        class_InsertAllModel.class_Create.CreateFrontDo = class_UpdateAllModelOld.class_Create.CreateFrontDo;
                                        class_InsertAllModel.class_Create.CreateFrontDoId = class_UpdateAllModelOld.class_Create.CreateFrontDoId;
                                        class_InsertAllModel.class_Create.CreateMan = class_UpdateAllModelOld.class_Create.CreateMan;
                                        class_InsertAllModel.class_Create.CreateManId = class_UpdateAllModelOld.class_Create.CreateManId;
                                        class_InsertAllModel.class_Create.EnglishSign = class_UpdateAllModelOld.class_Create.EnglishSign;
                                        class_InsertAllModel.class_Create.HttpRequestType = class_UpdateAllModelOld.class_Create.HttpRequestType;
                                        class_InsertAllModel.class_Create.MethodSite = class_UpdateAllModelOld.class_Create.MethodSite;
                                        class_InsertAllModel.class_Create.MicroServiceName = class_UpdateAllModelOld.class_Create.MicroServiceName;
                                        class_InsertAllModel.class_Create.Port = class_UpdateAllModelOld.class_Create.Port;
                                        class_InsertAllModel.class_Create.ReadOnly = class_UpdateAllModelOld.class_Create.ReadOnly;
                                        class_InsertAllModel.class_Create.SwaggerSign = class_UpdateAllModelOld.class_Create.SwaggerSign;
                                        class_InsertAllModel.class_Create.SystemName = class_UpdateAllModelOld.class_Create.SystemName;
                                    }
                                    if (class_UpdateAllModelOld.class_SubList != null && class_UpdateAllModelOld.class_SubList.Count > 0)
                                    {
                                        Class_InsertAllModel.Class_Sub class_Sub;
                                        if (class_InsertAllModel.class_SubList.Count == 0)
                                        {
                                            class_Sub = new Class_InsertAllModel.Class_Sub();
                                            class_InsertAllModel.class_SubList.Add(class_Sub);
                                        }
                                        else
                                            class_Sub = class_InsertAllModel.class_SubList[0];
                                        class_Sub.NameSpace = class_UpdateAllModelOld.class_SubList[0].NameSpace;
                                        class_Sub.ControlSwaggerValue = class_UpdateAllModelOld.class_SubList[0].ControlSwaggerValue;
                                        class_Sub.ControlSwaggerDescription = class_UpdateAllModelOld.class_SubList[0].ControlSwaggerDescription;
                                        class_Sub.MethodContent = class_UpdateAllModelOld.class_SubList[0].MethodContent;
                                        class_Sub.ServiceInterFaceReturnRemark = class_UpdateAllModelOld.class_SubList[0].ServiceInterFaceReturnRemark;
                                        class_Sub.TableName = class_UpdateAllModelOld.class_SubList[0].TableName;
                                        class_Sub.AliasName = class_UpdateAllModelOld.class_SubList[0].AliasName;
                                    }
                                    class_InsertAllModel.class_SelectDataBase.dataBaseName = class_UpdateAllModelOld.class_SelectDataBase.dataBaseName;
                                    class_InsertAllModel.class_SelectDataBase.databaseType = class_UpdateAllModelOld.class_SelectDataBase.databaseType;
                                    class_InsertAllModel.class_SelectDataBase.dataSourcePassWord = class_UpdateAllModelOld.class_SelectDataBase.dataSourcePassWord;
                                    class_InsertAllModel.class_SelectDataBase.dataSourceUrl = class_UpdateAllModelOld.class_SelectDataBase.dataSourceUrl;
                                    class_InsertAllModel.class_SelectDataBase.dataSourceUserName = class_UpdateAllModelOld.class_SelectDataBase.dataSourceUserName;
                                    class_InsertAllModel.class_SelectDataBase.Port = class_UpdateAllModelOld.class_SelectDataBase.Port;
                                    class_InsertAllModel.AllPackerName = class_UpdateAllModelOld.AllPackerName;
                                    break;
                                case "delete":
                                    Class_DeleteAllModel class_DeleteAllModelOld = new Class_DeleteAllModel();
                                    class_DeleteAllModelOld = this.FromXmlToDeleteObject<Class_DeleteAllModel>(xmlFileName);
                                    if (class_DeleteAllModelOld.class_Create != null)
                                    {
                                        class_InsertAllModel.class_Create.CreateDo = class_DeleteAllModelOld.class_Create.CreateDo;
                                        class_InsertAllModel.class_Create.CreateDoId = class_DeleteAllModelOld.class_Create.CreateDoId;
                                        class_InsertAllModel.class_Create.CreateFrontDo = class_DeleteAllModelOld.class_Create.CreateFrontDo;
                                        class_InsertAllModel.class_Create.CreateFrontDoId = class_DeleteAllModelOld.class_Create.CreateFrontDoId;
                                        class_InsertAllModel.class_Create.CreateMan = class_DeleteAllModelOld.class_Create.CreateMan;
                                        class_InsertAllModel.class_Create.CreateManId = class_DeleteAllModelOld.class_Create.CreateManId;
                                        class_InsertAllModel.class_Create.EnglishSign = class_DeleteAllModelOld.class_Create.EnglishSign;
                                        class_InsertAllModel.class_Create.HttpRequestType = class_DeleteAllModelOld.class_Create.HttpRequestType;
                                        class_InsertAllModel.class_Create.MethodSite = class_DeleteAllModelOld.class_Create.MethodSite;
                                        class_InsertAllModel.class_Create.MicroServiceName = class_DeleteAllModelOld.class_Create.MicroServiceName;
                                        class_InsertAllModel.class_Create.Port = class_DeleteAllModelOld.class_Create.Port;
                                        class_InsertAllModel.class_Create.ReadOnly = class_DeleteAllModelOld.class_Create.ReadOnly;
                                        class_InsertAllModel.class_Create.SwaggerSign = class_DeleteAllModelOld.class_Create.SwaggerSign;
                                        class_InsertAllModel.class_Create.SystemName = class_DeleteAllModelOld.class_Create.SystemName;
                                    }
                                    if (class_DeleteAllModelOld.class_SubList != null && class_DeleteAllModelOld.class_SubList.Count > 0)
                                    {
                                        Class_InsertAllModel.Class_Sub class_Sub;
                                        if (class_InsertAllModel.class_SubList.Count == 0)
                                        {
                                            class_Sub = new Class_InsertAllModel.Class_Sub();
                                            class_InsertAllModel.class_SubList.Add(class_Sub);
                                        }
                                        else
                                            class_Sub = class_InsertAllModel.class_SubList[0];
                                        class_Sub.NameSpace = class_DeleteAllModelOld.class_SubList[0].NameSpace;
                                        class_Sub.ControlSwaggerValue = class_DeleteAllModelOld.class_SubList[0].ControlSwaggerValue;
                                        class_Sub.ControlSwaggerDescription = class_DeleteAllModelOld.class_SubList[0].ControlSwaggerDescription;
                                        class_Sub.MethodContent = class_DeleteAllModelOld.class_SubList[0].MethodContent;
                                        class_Sub.ServiceInterFaceReturnRemark = class_DeleteAllModelOld.class_SubList[0].ServiceInterFaceReturnRemark;
                                        class_Sub.TableName = class_DeleteAllModelOld.class_SubList[0].TableName;
                                        class_Sub.AliasName = class_DeleteAllModelOld.class_SubList[0].AliasName;
                                    }
                                    class_InsertAllModel.class_SelectDataBase.dataBaseName = class_DeleteAllModelOld.class_SelectDataBase.dataBaseName;
                                    class_InsertAllModel.class_SelectDataBase.databaseType = class_DeleteAllModelOld.class_SelectDataBase.databaseType;
                                    class_InsertAllModel.class_SelectDataBase.dataSourcePassWord = class_DeleteAllModelOld.class_SelectDataBase.dataSourcePassWord;
                                    class_InsertAllModel.class_SelectDataBase.dataSourceUrl = class_DeleteAllModelOld.class_SelectDataBase.dataSourceUrl;
                                    class_InsertAllModel.class_SelectDataBase.dataSourceUserName = class_DeleteAllModelOld.class_SelectDataBase.dataSourceUserName;
                                    class_InsertAllModel.class_SelectDataBase.Port = class_DeleteAllModelOld.class_SelectDataBase.Port;
                                    class_InsertAllModel.AllPackerName = class_DeleteAllModelOld.AllPackerName;
                                    break;
                                default:
                                    break;
                            }
                        }
                        #endregion
                        class_InsertAllModel.class_Create.MethodId = returnKey;
                        class_InsertAllModel.class_Create.DateTime = System.DateTime.Now;
                        if (!this.InsertToXml(class_InsertAllModel.class_Create.MethodId, class_InsertAllModel, false))
                            returnKey = null;
                    }
                    break;
                case "update":
                    {
                        returnKey = Class_Tool.getKeyId("UP");
                        Class_UpdateAllModel class_UpdateAllModel = new Class_UpdateAllModel();
                        #region COPY
                        if (NewPageType == OldPageType)
                            class_UpdateAllModel = this.FromXmlToUpdateObject<Class_UpdateAllModel>(xmlFileName);
                        else
                        {
                            switch (OldPageType)
                            {
                                case "select":
                                    Class_SelectAllModel class_SelectAllModelOld = new Class_SelectAllModel();
                                    class_SelectAllModelOld = this.FromXmlToSelectObject<Class_SelectAllModel>(xmlFileName);
                                    if (class_SelectAllModelOld.class_Create != null)
                                    {
                                        class_UpdateAllModel.class_Create.CreateDo = class_SelectAllModelOld.class_Create.CreateDo;
                                        class_UpdateAllModel.class_Create.CreateDoId = class_SelectAllModelOld.class_Create.CreateDoId;
                                        class_UpdateAllModel.class_Create.CreateFrontDo = class_SelectAllModelOld.class_Create.CreateFrontDo;
                                        class_UpdateAllModel.class_Create.CreateFrontDoId = class_SelectAllModelOld.class_Create.CreateFrontDoId;
                                        class_UpdateAllModel.class_Create.CreateMan = class_SelectAllModelOld.class_Create.CreateMan;
                                        class_UpdateAllModel.class_Create.CreateManId = class_SelectAllModelOld.class_Create.CreateManId;
                                        class_UpdateAllModel.class_Create.EnglishSign = class_SelectAllModelOld.class_Create.EnglishSign;
                                        class_UpdateAllModel.class_Create.HttpRequestType = class_SelectAllModelOld.class_Create.HttpRequestType;
                                        class_UpdateAllModel.class_Create.MethodSite = class_SelectAllModelOld.class_Create.MethodSite;
                                        class_UpdateAllModel.class_Create.MicroServiceName = class_SelectAllModelOld.class_Create.MicroServiceName;
                                        class_UpdateAllModel.class_Create.Port = class_SelectAllModelOld.class_Create.Port;
                                        class_UpdateAllModel.class_Create.ReadOnly = class_SelectAllModelOld.class_Create.ReadOnly;
                                        class_UpdateAllModel.class_Create.SwaggerSign = class_SelectAllModelOld.class_Create.SwaggerSign;
                                        class_UpdateAllModel.class_Create.SystemName = class_SelectAllModelOld.class_Create.SystemName;
                                    }
                                    if (class_SelectAllModelOld.class_SubList != null && class_SelectAllModelOld.class_SubList.Count > 0)
                                    {
                                        Class_UpdateAllModel.Class_Sub class_Sub;
                                        if (class_UpdateAllModel.class_SubList.Count == 0)
                                        {
                                            class_Sub = new Class_UpdateAllModel.Class_Sub();
                                            class_UpdateAllModel.class_SubList.Add(class_Sub);
                                        }
                                        else
                                            class_Sub = class_UpdateAllModel.class_SubList[0];
                                        class_Sub.NameSpace = class_SelectAllModelOld.class_SubList[0].NameSpace;
                                        class_Sub.ControlSwaggerValue = class_SelectAllModelOld.class_SubList[0].ControlSwaggerValue;
                                        class_Sub.ControlSwaggerDescription = class_SelectAllModelOld.class_SubList[0].ControlSwaggerDescription;
                                        class_Sub.MethodContent = class_SelectAllModelOld.class_SubList[0].MethodContent;
                                        class_Sub.ServiceInterFaceReturnRemark = class_SelectAllModelOld.class_SubList[0].ServiceInterFaceReturnRemark;
                                        class_Sub.TableName = class_SelectAllModelOld.class_SubList[0].TableName;
                                        class_Sub.AliasName = class_SelectAllModelOld.class_SubList[0].AliasName;
                                    }
                                    class_UpdateAllModel.class_SelectDataBase.dataBaseName = class_SelectAllModelOld.class_SelectDataBase.dataBaseName;
                                    class_UpdateAllModel.class_SelectDataBase.databaseType = class_SelectAllModelOld.class_SelectDataBase.databaseType;
                                    class_UpdateAllModel.class_SelectDataBase.dataSourcePassWord = class_SelectAllModelOld.class_SelectDataBase.dataSourcePassWord;
                                    class_UpdateAllModel.class_SelectDataBase.dataSourceUrl = class_SelectAllModelOld.class_SelectDataBase.dataSourceUrl;
                                    class_UpdateAllModel.class_SelectDataBase.dataSourceUserName = class_SelectAllModelOld.class_SelectDataBase.dataSourceUserName;
                                    class_UpdateAllModel.class_SelectDataBase.Port = class_SelectAllModelOld.class_SelectDataBase.Port;
                                    class_UpdateAllModel.AllPackerName = class_SelectAllModelOld.AllPackerName;
                                    break;
                                case "insert":
                                    Class_InsertAllModel class_InsertAllModelOld = new Class_InsertAllModel();
                                    class_InsertAllModelOld = this.FromXmlToInsertObject<Class_InsertAllModel>(xmlFileName);
                                    if (class_InsertAllModelOld.class_Create != null)
                                    {
                                        class_UpdateAllModel.class_Create.CreateDo = class_InsertAllModelOld.class_Create.CreateDo;
                                        class_UpdateAllModel.class_Create.CreateDoId = class_InsertAllModelOld.class_Create.CreateDoId;
                                        class_UpdateAllModel.class_Create.CreateFrontDo = class_InsertAllModelOld.class_Create.CreateFrontDo;
                                        class_UpdateAllModel.class_Create.CreateFrontDoId = class_InsertAllModelOld.class_Create.CreateFrontDoId;
                                        class_UpdateAllModel.class_Create.CreateMan = class_InsertAllModelOld.class_Create.CreateMan;
                                        class_UpdateAllModel.class_Create.CreateManId = class_InsertAllModelOld.class_Create.CreateManId;
                                        class_UpdateAllModel.class_Create.EnglishSign = class_InsertAllModelOld.class_Create.EnglishSign;
                                        class_UpdateAllModel.class_Create.HttpRequestType = class_InsertAllModelOld.class_Create.HttpRequestType;
                                        class_UpdateAllModel.class_Create.MethodSite = class_InsertAllModelOld.class_Create.MethodSite;
                                        class_UpdateAllModel.class_Create.MicroServiceName = class_InsertAllModelOld.class_Create.MicroServiceName;
                                        class_UpdateAllModel.class_Create.Port = class_InsertAllModelOld.class_Create.Port;
                                        class_UpdateAllModel.class_Create.ReadOnly = class_InsertAllModelOld.class_Create.ReadOnly;
                                        class_UpdateAllModel.class_Create.SwaggerSign = class_InsertAllModelOld.class_Create.SwaggerSign;
                                        class_UpdateAllModel.class_Create.SystemName = class_InsertAllModelOld.class_Create.SystemName;
                                    }
                                    if (class_InsertAllModelOld.class_SubList != null && class_InsertAllModelOld.class_SubList.Count > 0)
                                    {
                                        Class_UpdateAllModel.Class_Sub class_Sub;
                                        if (class_UpdateAllModel.class_SubList.Count == 0)
                                        {
                                            class_Sub = new Class_UpdateAllModel.Class_Sub();
                                            class_UpdateAllModel.class_SubList.Add(class_Sub);
                                        }
                                        else
                                            class_Sub = class_UpdateAllModel.class_SubList[0];
                                        class_Sub.NameSpace = class_InsertAllModelOld.class_SubList[0].NameSpace;
                                        class_Sub.ControlSwaggerValue = class_InsertAllModelOld.class_SubList[0].ControlSwaggerValue;
                                        class_Sub.ControlSwaggerDescription = class_InsertAllModelOld.class_SubList[0].ControlSwaggerDescription;
                                        class_Sub.MethodContent = class_InsertAllModelOld.class_SubList[0].MethodContent;
                                        class_Sub.ServiceInterFaceReturnRemark = class_InsertAllModelOld.class_SubList[0].ServiceInterFaceReturnRemark;
                                        class_Sub.TableName = class_InsertAllModelOld.class_SubList[0].TableName;
                                        class_Sub.AliasName = class_InsertAllModelOld.class_SubList[0].AliasName;
                                    }
                                    class_UpdateAllModel.class_SelectDataBase.dataBaseName = class_InsertAllModelOld.class_SelectDataBase.dataBaseName;
                                    class_UpdateAllModel.class_SelectDataBase.databaseType = class_InsertAllModelOld.class_SelectDataBase.databaseType;
                                    class_UpdateAllModel.class_SelectDataBase.dataSourcePassWord = class_InsertAllModelOld.class_SelectDataBase.dataSourcePassWord;
                                    class_UpdateAllModel.class_SelectDataBase.dataSourceUrl = class_InsertAllModelOld.class_SelectDataBase.dataSourceUrl;
                                    class_UpdateAllModel.class_SelectDataBase.dataSourceUserName = class_InsertAllModelOld.class_SelectDataBase.dataSourceUserName;
                                    class_UpdateAllModel.class_SelectDataBase.Port = class_InsertAllModelOld.class_SelectDataBase.Port;
                                    class_UpdateAllModel.AllPackerName = class_InsertAllModelOld.AllPackerName;
                                    break;
                                case "update":
                                    Class_UpdateAllModel class_UpdateAllModelOld = new Class_UpdateAllModel();
                                    class_UpdateAllModelOld = this.FromXmlToUpdateObject<Class_UpdateAllModel>(xmlFileName);
                                    if (class_UpdateAllModelOld.class_Create != null)
                                    {
                                        class_UpdateAllModel.class_Create.CreateDo = class_UpdateAllModelOld.class_Create.CreateDo;
                                        class_UpdateAllModel.class_Create.CreateDoId = class_UpdateAllModelOld.class_Create.CreateDoId;
                                        class_UpdateAllModel.class_Create.CreateFrontDo = class_UpdateAllModelOld.class_Create.CreateFrontDo;
                                        class_UpdateAllModel.class_Create.CreateFrontDoId = class_UpdateAllModelOld.class_Create.CreateFrontDoId;
                                        class_UpdateAllModel.class_Create.CreateMan = class_UpdateAllModelOld.class_Create.CreateMan;
                                        class_UpdateAllModel.class_Create.CreateManId = class_UpdateAllModelOld.class_Create.CreateManId;
                                        class_UpdateAllModel.class_Create.EnglishSign = class_UpdateAllModelOld.class_Create.EnglishSign;
                                        class_UpdateAllModel.class_Create.HttpRequestType = class_UpdateAllModelOld.class_Create.HttpRequestType;
                                        class_UpdateAllModel.class_Create.MethodSite = class_UpdateAllModelOld.class_Create.MethodSite;
                                        class_UpdateAllModel.class_Create.MicroServiceName = class_UpdateAllModelOld.class_Create.MicroServiceName;
                                        class_UpdateAllModel.class_Create.Port = class_UpdateAllModelOld.class_Create.Port;
                                        class_UpdateAllModel.class_Create.ReadOnly = class_UpdateAllModelOld.class_Create.ReadOnly;
                                        class_UpdateAllModel.class_Create.SwaggerSign = class_UpdateAllModelOld.class_Create.SwaggerSign;
                                        class_UpdateAllModel.class_Create.SystemName = class_UpdateAllModelOld.class_Create.SystemName;
                                    }
                                    if (class_UpdateAllModelOld.class_SubList != null && class_UpdateAllModelOld.class_SubList.Count > 0)
                                    {
                                        Class_UpdateAllModel.Class_Sub class_Sub;
                                        if (class_UpdateAllModel.class_SubList.Count == 0)
                                        {
                                            class_Sub = new Class_UpdateAllModel.Class_Sub();
                                            class_UpdateAllModel.class_SubList.Add(class_Sub);
                                        }
                                        else
                                            class_Sub = class_UpdateAllModel.class_SubList[0];
                                        class_Sub.NameSpace = class_UpdateAllModelOld.class_SubList[0].NameSpace;
                                        class_Sub.ControlSwaggerValue = class_UpdateAllModelOld.class_SubList[0].ControlSwaggerValue;
                                        class_Sub.ControlSwaggerDescription = class_UpdateAllModelOld.class_SubList[0].ControlSwaggerDescription;
                                        class_Sub.MethodContent = class_UpdateAllModelOld.class_SubList[0].MethodContent;
                                        class_Sub.ServiceInterFaceReturnRemark = class_UpdateAllModelOld.class_SubList[0].ServiceInterFaceReturnRemark;
                                        class_Sub.TableName = class_UpdateAllModelOld.class_SubList[0].TableName;
                                        class_Sub.AliasName = class_UpdateAllModelOld.class_SubList[0].AliasName;
                                    }
                                    class_UpdateAllModel.class_SelectDataBase.dataBaseName = class_UpdateAllModelOld.class_SelectDataBase.dataBaseName;
                                    class_UpdateAllModel.class_SelectDataBase.databaseType = class_UpdateAllModelOld.class_SelectDataBase.databaseType;
                                    class_UpdateAllModel.class_SelectDataBase.dataSourcePassWord = class_UpdateAllModelOld.class_SelectDataBase.dataSourcePassWord;
                                    class_UpdateAllModel.class_SelectDataBase.dataSourceUrl = class_UpdateAllModelOld.class_SelectDataBase.dataSourceUrl;
                                    class_UpdateAllModel.class_SelectDataBase.dataSourceUserName = class_UpdateAllModelOld.class_SelectDataBase.dataSourceUserName;
                                    class_UpdateAllModel.class_SelectDataBase.Port = class_UpdateAllModelOld.class_SelectDataBase.Port;
                                    class_UpdateAllModel.AllPackerName = class_UpdateAllModelOld.AllPackerName;
                                    break;
                                case "delete":
                                    Class_DeleteAllModel class_DeleteAllModelOld = new Class_DeleteAllModel();
                                    class_DeleteAllModelOld = this.FromXmlToDeleteObject<Class_DeleteAllModel>(xmlFileName);
                                    if (class_DeleteAllModelOld.class_Create != null)
                                    {
                                        class_UpdateAllModel.class_Create.CreateDo = class_DeleteAllModelOld.class_Create.CreateDo;
                                        class_UpdateAllModel.class_Create.CreateDoId = class_DeleteAllModelOld.class_Create.CreateDoId;
                                        class_UpdateAllModel.class_Create.CreateFrontDo = class_DeleteAllModelOld.class_Create.CreateFrontDo;
                                        class_UpdateAllModel.class_Create.CreateFrontDoId = class_DeleteAllModelOld.class_Create.CreateFrontDoId;
                                        class_UpdateAllModel.class_Create.CreateMan = class_DeleteAllModelOld.class_Create.CreateMan;
                                        class_UpdateAllModel.class_Create.CreateManId = class_DeleteAllModelOld.class_Create.CreateManId;
                                        class_UpdateAllModel.class_Create.EnglishSign = class_DeleteAllModelOld.class_Create.EnglishSign;
                                        class_UpdateAllModel.class_Create.HttpRequestType = class_DeleteAllModelOld.class_Create.HttpRequestType;
                                        class_UpdateAllModel.class_Create.MethodSite = class_DeleteAllModelOld.class_Create.MethodSite;
                                        class_UpdateAllModel.class_Create.MicroServiceName = class_DeleteAllModelOld.class_Create.MicroServiceName;
                                        class_UpdateAllModel.class_Create.Port = class_DeleteAllModelOld.class_Create.Port;
                                        class_UpdateAllModel.class_Create.ReadOnly = class_DeleteAllModelOld.class_Create.ReadOnly;
                                        class_UpdateAllModel.class_Create.SwaggerSign = class_DeleteAllModelOld.class_Create.SwaggerSign;
                                        class_UpdateAllModel.class_Create.SystemName = class_DeleteAllModelOld.class_Create.SystemName;
                                    }
                                    if (class_DeleteAllModelOld.class_SubList != null && class_DeleteAllModelOld.class_SubList.Count > 0)
                                    {
                                        Class_UpdateAllModel.Class_Sub class_Sub;
                                        if (class_UpdateAllModel.class_SubList.Count == 0)
                                        {
                                            class_Sub = new Class_UpdateAllModel.Class_Sub();
                                            class_UpdateAllModel.class_SubList.Add(class_Sub);
                                        }
                                        else
                                            class_Sub = class_UpdateAllModel.class_SubList[0];
                                        class_Sub.NameSpace = class_DeleteAllModelOld.class_SubList[0].NameSpace;
                                        class_Sub.ControlSwaggerValue = class_DeleteAllModelOld.class_SubList[0].ControlSwaggerValue;
                                        class_Sub.ControlSwaggerDescription = class_DeleteAllModelOld.class_SubList[0].ControlSwaggerDescription;
                                        class_Sub.MethodContent = class_DeleteAllModelOld.class_SubList[0].MethodContent;
                                        class_Sub.ServiceInterFaceReturnRemark = class_DeleteAllModelOld.class_SubList[0].ServiceInterFaceReturnRemark;
                                        class_Sub.TableName = class_DeleteAllModelOld.class_SubList[0].TableName;
                                        class_Sub.AliasName = class_DeleteAllModelOld.class_SubList[0].AliasName;
                                    }
                                    class_UpdateAllModel.class_SelectDataBase.dataBaseName = class_DeleteAllModelOld.class_SelectDataBase.dataBaseName;
                                    class_UpdateAllModel.class_SelectDataBase.databaseType = class_DeleteAllModelOld.class_SelectDataBase.databaseType;
                                    class_UpdateAllModel.class_SelectDataBase.dataSourcePassWord = class_DeleteAllModelOld.class_SelectDataBase.dataSourcePassWord;
                                    class_UpdateAllModel.class_SelectDataBase.dataSourceUrl = class_DeleteAllModelOld.class_SelectDataBase.dataSourceUrl;
                                    class_UpdateAllModel.class_SelectDataBase.dataSourceUserName = class_DeleteAllModelOld.class_SelectDataBase.dataSourceUserName;
                                    class_UpdateAllModel.class_SelectDataBase.Port = class_DeleteAllModelOld.class_SelectDataBase.Port;
                                    class_UpdateAllModel.AllPackerName = class_DeleteAllModelOld.AllPackerName;
                                    break;
                                default:
                                    break;
                            }
                        }
                        #endregion
                        class_UpdateAllModel.class_Create.MethodId = returnKey;
                        class_UpdateAllModel.class_Create.DateTime = System.DateTime.Now;
                        if (!this.UpdateToXml(class_UpdateAllModel.class_Create.MethodId, class_UpdateAllModel, false))
                            returnKey = null;
                    }
                    break;
                case "delete":
                    {
                        returnKey = Class_Tool.getKeyId("DE");
                        Class_DeleteAllModel class_DeleteAllModel = new Class_DeleteAllModel();
                        #region COPY
                        if (NewPageType == OldPageType)
                            class_DeleteAllModel = this.FromXmlToDeleteObject<Class_DeleteAllModel>(xmlFileName);
                        else
                        {
                            switch (OldPageType)
                            {
                                case "select":
                                    Class_SelectAllModel class_SelectAllModelOld = new Class_SelectAllModel();
                                    class_SelectAllModelOld = this.FromXmlToSelectObject<Class_SelectAllModel>(xmlFileName);
                                    if (class_SelectAllModelOld.class_Create != null)
                                    {
                                        class_DeleteAllModel.class_Create.CreateDo = class_SelectAllModelOld.class_Create.CreateDo;
                                        class_DeleteAllModel.class_Create.CreateDoId = class_SelectAllModelOld.class_Create.CreateDoId;
                                        class_DeleteAllModel.class_Create.CreateFrontDo = class_SelectAllModelOld.class_Create.CreateFrontDo;
                                        class_DeleteAllModel.class_Create.CreateFrontDoId = class_SelectAllModelOld.class_Create.CreateFrontDoId;
                                        class_DeleteAllModel.class_Create.CreateMan = class_SelectAllModelOld.class_Create.CreateMan;
                                        class_DeleteAllModel.class_Create.CreateManId = class_SelectAllModelOld.class_Create.CreateManId;
                                        class_DeleteAllModel.class_Create.EnglishSign = class_SelectAllModelOld.class_Create.EnglishSign;
                                        class_DeleteAllModel.class_Create.HttpRequestType = class_SelectAllModelOld.class_Create.HttpRequestType;
                                        class_DeleteAllModel.class_Create.MethodSite = class_SelectAllModelOld.class_Create.MethodSite;
                                        class_DeleteAllModel.class_Create.MicroServiceName = class_SelectAllModelOld.class_Create.MicroServiceName;
                                        class_DeleteAllModel.class_Create.Port = class_SelectAllModelOld.class_Create.Port;
                                        class_DeleteAllModel.class_Create.ReadOnly = class_SelectAllModelOld.class_Create.ReadOnly;
                                        class_DeleteAllModel.class_Create.SwaggerSign = class_SelectAllModelOld.class_Create.SwaggerSign;
                                        class_DeleteAllModel.class_Create.SystemName = class_SelectAllModelOld.class_Create.SystemName;
                                    }
                                    if (class_SelectAllModelOld.class_SubList != null && class_SelectAllModelOld.class_SubList.Count > 0)
                                    {
                                        Class_DeleteAllModel.Class_Sub class_Sub;
                                        if (class_DeleteAllModel.class_SubList.Count == 0)
                                        {
                                            class_Sub = new Class_DeleteAllModel.Class_Sub();
                                            class_DeleteAllModel.class_SubList.Add(class_Sub);
                                        }
                                        else
                                            class_Sub = class_DeleteAllModel.class_SubList[0];
                                        class_Sub.NameSpace = class_SelectAllModelOld.class_SubList[0].NameSpace;
                                        class_Sub.ControlSwaggerValue = class_SelectAllModelOld.class_SubList[0].ControlSwaggerValue;
                                        class_Sub.ControlSwaggerDescription = class_SelectAllModelOld.class_SubList[0].ControlSwaggerDescription;
                                        class_Sub.MethodContent = class_SelectAllModelOld.class_SubList[0].MethodContent;
                                        class_Sub.ServiceInterFaceReturnRemark = class_SelectAllModelOld.class_SubList[0].ServiceInterFaceReturnRemark;
                                        class_Sub.TableName = class_SelectAllModelOld.class_SubList[0].TableName;
                                        class_Sub.AliasName = class_SelectAllModelOld.class_SubList[0].AliasName;
                                    }
                                    class_DeleteAllModel.class_SelectDataBase.dataBaseName = class_SelectAllModelOld.class_SelectDataBase.dataBaseName;
                                    class_DeleteAllModel.class_SelectDataBase.databaseType = class_SelectAllModelOld.class_SelectDataBase.databaseType;
                                    class_DeleteAllModel.class_SelectDataBase.dataSourcePassWord = class_SelectAllModelOld.class_SelectDataBase.dataSourcePassWord;
                                    class_DeleteAllModel.class_SelectDataBase.dataSourceUrl = class_SelectAllModelOld.class_SelectDataBase.dataSourceUrl;
                                    class_DeleteAllModel.class_SelectDataBase.dataSourceUserName = class_SelectAllModelOld.class_SelectDataBase.dataSourceUserName;
                                    class_DeleteAllModel.class_SelectDataBase.Port = class_SelectAllModelOld.class_SelectDataBase.Port;
                                    class_DeleteAllModel.AllPackerName = class_SelectAllModelOld.AllPackerName;
                                    break;
                                case "insert":
                                    Class_InsertAllModel class_InsertAllModelOld = new Class_InsertAllModel();
                                    class_InsertAllModelOld = this.FromXmlToInsertObject<Class_InsertAllModel>(xmlFileName);
                                    if (class_InsertAllModelOld.class_Create != null)
                                    {
                                        class_DeleteAllModel.class_Create.CreateDo = class_InsertAllModelOld.class_Create.CreateDo;
                                        class_DeleteAllModel.class_Create.CreateDoId = class_InsertAllModelOld.class_Create.CreateDoId;
                                        class_DeleteAllModel.class_Create.CreateFrontDo = class_InsertAllModelOld.class_Create.CreateFrontDo;
                                        class_DeleteAllModel.class_Create.CreateFrontDoId = class_InsertAllModelOld.class_Create.CreateFrontDoId;
                                        class_DeleteAllModel.class_Create.CreateMan = class_InsertAllModelOld.class_Create.CreateMan;
                                        class_DeleteAllModel.class_Create.CreateManId = class_InsertAllModelOld.class_Create.CreateManId;
                                        class_DeleteAllModel.class_Create.EnglishSign = class_InsertAllModelOld.class_Create.EnglishSign;
                                        class_DeleteAllModel.class_Create.HttpRequestType = class_InsertAllModelOld.class_Create.HttpRequestType;
                                        class_DeleteAllModel.class_Create.MethodSite = class_InsertAllModelOld.class_Create.MethodSite;
                                        class_DeleteAllModel.class_Create.MicroServiceName = class_InsertAllModelOld.class_Create.MicroServiceName;
                                        class_DeleteAllModel.class_Create.Port = class_InsertAllModelOld.class_Create.Port;
                                        class_DeleteAllModel.class_Create.ReadOnly = class_InsertAllModelOld.class_Create.ReadOnly;
                                        class_DeleteAllModel.class_Create.SwaggerSign = class_InsertAllModelOld.class_Create.SwaggerSign;
                                        class_DeleteAllModel.class_Create.SystemName = class_InsertAllModelOld.class_Create.SystemName;
                                    }
                                    if (class_InsertAllModelOld.class_SubList != null && class_InsertAllModelOld.class_SubList.Count > 0)
                                    {
                                        Class_DeleteAllModel.Class_Sub class_Sub;
                                        if (class_DeleteAllModel.class_SubList.Count == 0)
                                        {
                                            class_Sub = new Class_DeleteAllModel.Class_Sub();
                                            class_DeleteAllModel.class_SubList.Add(class_Sub);
                                        }
                                        else
                                            class_Sub = class_DeleteAllModel.class_SubList[0];
                                        class_Sub.NameSpace = class_InsertAllModelOld.class_SubList[0].NameSpace;
                                        class_Sub.ControlSwaggerValue = class_InsertAllModelOld.class_SubList[0].ControlSwaggerValue;
                                        class_Sub.ControlSwaggerDescription = class_InsertAllModelOld.class_SubList[0].ControlSwaggerDescription;
                                        class_Sub.MethodContent = class_InsertAllModelOld.class_SubList[0].MethodContent;
                                        class_Sub.ServiceInterFaceReturnRemark = class_InsertAllModelOld.class_SubList[0].ServiceInterFaceReturnRemark;
                                        class_Sub.TableName = class_InsertAllModelOld.class_SubList[0].TableName;
                                        class_Sub.AliasName = class_InsertAllModelOld.class_SubList[0].AliasName;
                                    }
                                    class_DeleteAllModel.class_SelectDataBase.dataBaseName = class_InsertAllModelOld.class_SelectDataBase.dataBaseName;
                                    class_DeleteAllModel.class_SelectDataBase.databaseType = class_InsertAllModelOld.class_SelectDataBase.databaseType;
                                    class_DeleteAllModel.class_SelectDataBase.dataSourcePassWord = class_InsertAllModelOld.class_SelectDataBase.dataSourcePassWord;
                                    class_DeleteAllModel.class_SelectDataBase.dataSourceUrl = class_InsertAllModelOld.class_SelectDataBase.dataSourceUrl;
                                    class_DeleteAllModel.class_SelectDataBase.dataSourceUserName = class_InsertAllModelOld.class_SelectDataBase.dataSourceUserName;
                                    class_DeleteAllModel.class_SelectDataBase.Port = class_InsertAllModelOld.class_SelectDataBase.Port;
                                    class_DeleteAllModel.AllPackerName = class_InsertAllModelOld.AllPackerName;
                                    break;
                                case "update":
                                    Class_UpdateAllModel class_UpdateAllModelOld = new Class_UpdateAllModel();
                                    class_UpdateAllModelOld = this.FromXmlToUpdateObject<Class_UpdateAllModel>(xmlFileName);
                                    if (class_UpdateAllModelOld.class_Create != null)
                                    {
                                        class_DeleteAllModel.class_Create.CreateDo = class_UpdateAllModelOld.class_Create.CreateDo;
                                        class_DeleteAllModel.class_Create.CreateDoId = class_UpdateAllModelOld.class_Create.CreateDoId;
                                        class_DeleteAllModel.class_Create.CreateFrontDo = class_UpdateAllModelOld.class_Create.CreateFrontDo;
                                        class_DeleteAllModel.class_Create.CreateFrontDoId = class_UpdateAllModelOld.class_Create.CreateFrontDoId;
                                        class_DeleteAllModel.class_Create.CreateMan = class_UpdateAllModelOld.class_Create.CreateMan;
                                        class_DeleteAllModel.class_Create.CreateManId = class_UpdateAllModelOld.class_Create.CreateManId;
                                        class_DeleteAllModel.class_Create.EnglishSign = class_UpdateAllModelOld.class_Create.EnglishSign;
                                        class_DeleteAllModel.class_Create.HttpRequestType = class_UpdateAllModelOld.class_Create.HttpRequestType;
                                        class_DeleteAllModel.class_Create.MethodSite = class_UpdateAllModelOld.class_Create.MethodSite;
                                        class_DeleteAllModel.class_Create.MicroServiceName = class_UpdateAllModelOld.class_Create.MicroServiceName;
                                        class_DeleteAllModel.class_Create.Port = class_UpdateAllModelOld.class_Create.Port;
                                        class_DeleteAllModel.class_Create.ReadOnly = class_UpdateAllModelOld.class_Create.ReadOnly;
                                        class_DeleteAllModel.class_Create.SwaggerSign = class_UpdateAllModelOld.class_Create.SwaggerSign;
                                        class_DeleteAllModel.class_Create.SystemName = class_UpdateAllModelOld.class_Create.SystemName;
                                    }
                                    if (class_UpdateAllModelOld.class_SubList != null && class_UpdateAllModelOld.class_SubList.Count > 0)
                                    {
                                        Class_DeleteAllModel.Class_Sub class_Sub;
                                        if (class_DeleteAllModel.class_SubList.Count == 0)
                                        {
                                            class_Sub = new Class_DeleteAllModel.Class_Sub();
                                            class_DeleteAllModel.class_SubList.Add(class_Sub);
                                        }
                                        else
                                            class_Sub = class_DeleteAllModel.class_SubList[0];
                                        class_Sub.NameSpace = class_UpdateAllModelOld.class_SubList[0].NameSpace;
                                        class_Sub.ControlSwaggerValue = class_UpdateAllModelOld.class_SubList[0].ControlSwaggerValue;
                                        class_Sub.ControlSwaggerDescription = class_UpdateAllModelOld.class_SubList[0].ControlSwaggerDescription;
                                        class_Sub.MethodContent = class_UpdateAllModelOld.class_SubList[0].MethodContent;
                                        class_Sub.ServiceInterFaceReturnRemark = class_UpdateAllModelOld.class_SubList[0].ServiceInterFaceReturnRemark;
                                        class_Sub.TableName = class_UpdateAllModelOld.class_SubList[0].TableName;
                                        class_Sub.AliasName = class_UpdateAllModelOld.class_SubList[0].AliasName;
                                    }
                                    class_DeleteAllModel.class_SelectDataBase.dataBaseName = class_UpdateAllModelOld.class_SelectDataBase.dataBaseName;
                                    class_DeleteAllModel.class_SelectDataBase.databaseType = class_UpdateAllModelOld.class_SelectDataBase.databaseType;
                                    class_DeleteAllModel.class_SelectDataBase.dataSourcePassWord = class_UpdateAllModelOld.class_SelectDataBase.dataSourcePassWord;
                                    class_DeleteAllModel.class_SelectDataBase.dataSourceUrl = class_UpdateAllModelOld.class_SelectDataBase.dataSourceUrl;
                                    class_DeleteAllModel.class_SelectDataBase.dataSourceUserName = class_UpdateAllModelOld.class_SelectDataBase.dataSourceUserName;
                                    class_DeleteAllModel.class_SelectDataBase.Port = class_UpdateAllModelOld.class_SelectDataBase.Port;
                                    class_DeleteAllModel.AllPackerName = class_UpdateAllModelOld.AllPackerName;
                                    break;
                                case "delete":
                                    Class_DeleteAllModel class_DeleteAllModelOld = new Class_DeleteAllModel();
                                    class_DeleteAllModelOld = this.FromXmlToDeleteObject<Class_DeleteAllModel>(xmlFileName);
                                    if (class_DeleteAllModelOld.class_Create != null)
                                    {
                                        class_DeleteAllModel.class_Create.CreateDo = class_DeleteAllModelOld.class_Create.CreateDo;
                                        class_DeleteAllModel.class_Create.CreateDoId = class_DeleteAllModelOld.class_Create.CreateDoId;
                                        class_DeleteAllModel.class_Create.CreateFrontDo = class_DeleteAllModelOld.class_Create.CreateFrontDo;
                                        class_DeleteAllModel.class_Create.CreateFrontDoId = class_DeleteAllModelOld.class_Create.CreateFrontDoId;
                                        class_DeleteAllModel.class_Create.CreateMan = class_DeleteAllModelOld.class_Create.CreateMan;
                                        class_DeleteAllModel.class_Create.CreateManId = class_DeleteAllModelOld.class_Create.CreateManId;
                                        class_DeleteAllModel.class_Create.EnglishSign = class_DeleteAllModelOld.class_Create.EnglishSign;
                                        class_DeleteAllModel.class_Create.HttpRequestType = class_DeleteAllModelOld.class_Create.HttpRequestType;
                                        class_DeleteAllModel.class_Create.MethodSite = class_DeleteAllModelOld.class_Create.MethodSite;
                                        class_DeleteAllModel.class_Create.MicroServiceName = class_DeleteAllModelOld.class_Create.MicroServiceName;
                                        class_DeleteAllModel.class_Create.Port = class_DeleteAllModelOld.class_Create.Port;
                                        class_DeleteAllModel.class_Create.ReadOnly = class_DeleteAllModelOld.class_Create.ReadOnly;
                                        class_DeleteAllModel.class_Create.SwaggerSign = class_DeleteAllModelOld.class_Create.SwaggerSign;
                                        class_DeleteAllModel.class_Create.SystemName = class_DeleteAllModelOld.class_Create.SystemName;
                                    }
                                    if (class_DeleteAllModelOld.class_SubList != null && class_DeleteAllModelOld.class_SubList.Count > 0)
                                    {
                                        Class_DeleteAllModel.Class_Sub class_Sub;
                                        if (class_DeleteAllModel.class_SubList.Count == 0)
                                        {
                                            class_Sub = new Class_DeleteAllModel.Class_Sub();
                                            class_DeleteAllModel.class_SubList.Add(class_Sub);
                                        }
                                        else
                                            class_Sub = class_DeleteAllModel.class_SubList[0];
                                        class_Sub.NameSpace = class_DeleteAllModelOld.class_SubList[0].NameSpace;
                                        class_Sub.ControlSwaggerValue = class_DeleteAllModelOld.class_SubList[0].ControlSwaggerValue;
                                        class_Sub.ControlSwaggerDescription = class_DeleteAllModelOld.class_SubList[0].ControlSwaggerDescription;
                                        class_Sub.MethodContent = class_DeleteAllModelOld.class_SubList[0].MethodContent;
                                        class_Sub.ServiceInterFaceReturnRemark = class_DeleteAllModelOld.class_SubList[0].ServiceInterFaceReturnRemark;
                                        class_Sub.TableName = class_DeleteAllModelOld.class_SubList[0].TableName;
                                        class_Sub.AliasName = class_DeleteAllModelOld.class_SubList[0].AliasName;
                                    }
                                    class_DeleteAllModel.class_SelectDataBase.dataBaseName = class_DeleteAllModelOld.class_SelectDataBase.dataBaseName;
                                    class_DeleteAllModel.class_SelectDataBase.databaseType = class_DeleteAllModelOld.class_SelectDataBase.databaseType;
                                    class_DeleteAllModel.class_SelectDataBase.dataSourcePassWord = class_DeleteAllModelOld.class_SelectDataBase.dataSourcePassWord;
                                    class_DeleteAllModel.class_SelectDataBase.dataSourceUrl = class_DeleteAllModelOld.class_SelectDataBase.dataSourceUrl;
                                    class_DeleteAllModel.class_SelectDataBase.dataSourceUserName = class_DeleteAllModelOld.class_SelectDataBase.dataSourceUserName;
                                    class_DeleteAllModel.class_SelectDataBase.Port = class_DeleteAllModelOld.class_SelectDataBase.Port;
                                    class_DeleteAllModel.AllPackerName = class_DeleteAllModelOld.AllPackerName;
                                    break;
                                default:
                                    break;
                            }
                        }
                        #endregion
                        class_DeleteAllModel.class_Create.MethodId = returnKey;
                        class_DeleteAllModel.class_Create.DateTime = System.DateTime.Now;
                        if (!this.DeleteToXml(class_DeleteAllModel.class_Create.MethodId, class_DeleteAllModel, false))
                            returnKey = null;
                    }
                    break;
                default:
                    {
                        returnKey = Class_Tool.getKeyId("SE");
                        Class_SelectAllModel class_SelectAllModel = new Class_SelectAllModel();
                        #region COPY
                        if (NewPageType == OldPageType)
                            class_SelectAllModel = this.FromXmlToSelectObject<Class_SelectAllModel>(xmlFileName);
                        else
                        {
                            switch (OldPageType)
                            {
                                case "select":
                                    Class_SelectAllModel class_SelectAllModelOld = new Class_SelectAllModel();
                                    class_SelectAllModelOld = this.FromXmlToSelectObject<Class_SelectAllModel>(xmlFileName);
                                    if (class_SelectAllModelOld.class_Create != null)
                                    {
                                        class_SelectAllModel.class_Create.CreateDo = class_SelectAllModelOld.class_Create.CreateDo;
                                        class_SelectAllModel.class_Create.CreateDoId = class_SelectAllModelOld.class_Create.CreateDoId;
                                        class_SelectAllModel.class_Create.CreateFrontDo = class_SelectAllModelOld.class_Create.CreateFrontDo;
                                        class_SelectAllModel.class_Create.CreateFrontDoId = class_SelectAllModelOld.class_Create.CreateFrontDoId;
                                        class_SelectAllModel.class_Create.CreateMan = class_SelectAllModelOld.class_Create.CreateMan;
                                        class_SelectAllModel.class_Create.CreateManId = class_SelectAllModelOld.class_Create.CreateManId;
                                        class_SelectAllModel.class_Create.EnglishSign = class_SelectAllModelOld.class_Create.EnglishSign;
                                        class_SelectAllModel.class_Create.HttpRequestType = class_SelectAllModelOld.class_Create.HttpRequestType;
                                        class_SelectAllModel.class_Create.MethodSite = class_SelectAllModelOld.class_Create.MethodSite;
                                        class_SelectAllModel.class_Create.MicroServiceName = class_SelectAllModelOld.class_Create.MicroServiceName;
                                        class_SelectAllModel.class_Create.Port = class_SelectAllModelOld.class_Create.Port;
                                        class_SelectAllModel.class_Create.ReadOnly = class_SelectAllModelOld.class_Create.ReadOnly;
                                        class_SelectAllModel.class_Create.SwaggerSign = class_SelectAllModelOld.class_Create.SwaggerSign;
                                        class_SelectAllModel.class_Create.SystemName = class_SelectAllModelOld.class_Create.SystemName;
                                    }
                                    if (class_SelectAllModelOld.class_SubList != null && class_SelectAllModelOld.class_SubList.Count > 0)
                                    {
                                        Class_SelectAllModel.Class_Sub class_Sub;
                                        if (class_SelectAllModel.class_SubList.Count == 0)
                                        {
                                            class_Sub = new Class_SelectAllModel.Class_Sub();
                                            class_SelectAllModel.class_SubList.Add(class_Sub);
                                        }
                                        else
                                            class_Sub = class_SelectAllModel.class_SubList[0];
                                        class_Sub.NameSpace = class_SelectAllModelOld.class_SubList[0].NameSpace;
                                        class_Sub.ControlSwaggerValue = class_SelectAllModelOld.class_SubList[0].ControlSwaggerValue;
                                        class_Sub.ControlSwaggerDescription = class_SelectAllModelOld.class_SubList[0].ControlSwaggerDescription;
                                        class_Sub.MethodContent = class_SelectAllModelOld.class_SubList[0].MethodContent;
                                        class_Sub.ServiceInterFaceReturnRemark = class_SelectAllModelOld.class_SubList[0].ServiceInterFaceReturnRemark;
                                        class_Sub.TableName = class_SelectAllModelOld.class_SubList[0].TableName;
                                        class_Sub.AliasName = class_SelectAllModelOld.class_SubList[0].AliasName;
                                    }
                                    class_SelectAllModel.class_SelectDataBase.dataBaseName = class_SelectAllModelOld.class_SelectDataBase.dataBaseName;
                                    class_SelectAllModel.class_SelectDataBase.databaseType = class_SelectAllModelOld.class_SelectDataBase.databaseType;
                                    class_SelectAllModel.class_SelectDataBase.dataSourcePassWord = class_SelectAllModelOld.class_SelectDataBase.dataSourcePassWord;
                                    class_SelectAllModel.class_SelectDataBase.dataSourceUrl = class_SelectAllModelOld.class_SelectDataBase.dataSourceUrl;
                                    class_SelectAllModel.class_SelectDataBase.dataSourceUserName = class_SelectAllModelOld.class_SelectDataBase.dataSourceUserName;
                                    class_SelectAllModel.class_SelectDataBase.Port = class_SelectAllModelOld.class_SelectDataBase.Port;
                                    class_SelectAllModel.AllPackerName = class_SelectAllModelOld.AllPackerName;
                                    break;
                                case "insert":
                                    Class_InsertAllModel class_InsertAllModelOld = new Class_InsertAllModel();
                                    class_InsertAllModelOld = this.FromXmlToInsertObject<Class_InsertAllModel>(xmlFileName);
                                    if (class_InsertAllModelOld.class_Create != null)
                                    {
                                        class_SelectAllModel.class_Create.CreateDo = class_InsertAllModelOld.class_Create.CreateDo;
                                        class_SelectAllModel.class_Create.CreateDoId = class_InsertAllModelOld.class_Create.CreateDoId;
                                        class_SelectAllModel.class_Create.CreateFrontDo = class_InsertAllModelOld.class_Create.CreateFrontDo;
                                        class_SelectAllModel.class_Create.CreateFrontDoId = class_InsertAllModelOld.class_Create.CreateFrontDoId;
                                        class_SelectAllModel.class_Create.CreateMan = class_InsertAllModelOld.class_Create.CreateMan;
                                        class_SelectAllModel.class_Create.CreateManId = class_InsertAllModelOld.class_Create.CreateManId;
                                        class_SelectAllModel.class_Create.EnglishSign = class_InsertAllModelOld.class_Create.EnglishSign;
                                        class_SelectAllModel.class_Create.HttpRequestType = class_InsertAllModelOld.class_Create.HttpRequestType;
                                        class_SelectAllModel.class_Create.MethodSite = class_InsertAllModelOld.class_Create.MethodSite;
                                        class_SelectAllModel.class_Create.MicroServiceName = class_InsertAllModelOld.class_Create.MicroServiceName;
                                        class_SelectAllModel.class_Create.Port = class_InsertAllModelOld.class_Create.Port;
                                        class_SelectAllModel.class_Create.ReadOnly = class_InsertAllModelOld.class_Create.ReadOnly;
                                        class_SelectAllModel.class_Create.SwaggerSign = class_InsertAllModelOld.class_Create.SwaggerSign;
                                        class_SelectAllModel.class_Create.SystemName = class_InsertAllModelOld.class_Create.SystemName;
                                    }
                                    if (class_InsertAllModelOld.class_SubList != null && class_InsertAllModelOld.class_SubList.Count > 0)
                                    {
                                        Class_SelectAllModel.Class_Sub class_Sub;
                                        if (class_SelectAllModel.class_SubList.Count == 0)
                                        {
                                            class_Sub = new Class_SelectAllModel.Class_Sub();
                                            class_SelectAllModel.class_SubList.Add(class_Sub);
                                        }
                                        else
                                            class_Sub = class_SelectAllModel.class_SubList[0];
                                        class_Sub.NameSpace = class_InsertAllModelOld.class_SubList[0].NameSpace;
                                        class_Sub.ControlSwaggerValue = class_InsertAllModelOld.class_SubList[0].ControlSwaggerValue;
                                        class_Sub.ControlSwaggerDescription = class_InsertAllModelOld.class_SubList[0].ControlSwaggerDescription;
                                        class_Sub.MethodContent = class_InsertAllModelOld.class_SubList[0].MethodContent;
                                        class_Sub.ServiceInterFaceReturnRemark = class_InsertAllModelOld.class_SubList[0].ServiceInterFaceReturnRemark;
                                        class_Sub.TableName = class_InsertAllModelOld.class_SubList[0].TableName;
                                        class_Sub.AliasName = class_InsertAllModelOld.class_SubList[0].AliasName;
                                    }
                                    class_SelectAllModel.class_SelectDataBase.dataBaseName = class_InsertAllModelOld.class_SelectDataBase.dataBaseName;
                                    class_SelectAllModel.class_SelectDataBase.databaseType = class_InsertAllModelOld.class_SelectDataBase.databaseType;
                                    class_SelectAllModel.class_SelectDataBase.dataSourcePassWord = class_InsertAllModelOld.class_SelectDataBase.dataSourcePassWord;
                                    class_SelectAllModel.class_SelectDataBase.dataSourceUrl = class_InsertAllModelOld.class_SelectDataBase.dataSourceUrl;
                                    class_SelectAllModel.class_SelectDataBase.dataSourceUserName = class_InsertAllModelOld.class_SelectDataBase.dataSourceUserName;
                                    class_SelectAllModel.class_SelectDataBase.Port = class_InsertAllModelOld.class_SelectDataBase.Port;
                                    class_SelectAllModel.AllPackerName = class_InsertAllModelOld.AllPackerName;
                                    break;
                                case "update":
                                    Class_UpdateAllModel class_UpdateAllModelOld = new Class_UpdateAllModel();
                                    class_UpdateAllModelOld = this.FromXmlToUpdateObject<Class_UpdateAllModel>(xmlFileName);
                                    if (class_UpdateAllModelOld.class_Create != null)
                                    {
                                        class_SelectAllModel.class_Create.CreateDo = class_UpdateAllModelOld.class_Create.CreateDo;
                                        class_SelectAllModel.class_Create.CreateDoId = class_UpdateAllModelOld.class_Create.CreateDoId;
                                        class_SelectAllModel.class_Create.CreateFrontDo = class_UpdateAllModelOld.class_Create.CreateFrontDo;
                                        class_SelectAllModel.class_Create.CreateFrontDoId = class_UpdateAllModelOld.class_Create.CreateFrontDoId;
                                        class_SelectAllModel.class_Create.CreateMan = class_UpdateAllModelOld.class_Create.CreateMan;
                                        class_SelectAllModel.class_Create.CreateManId = class_UpdateAllModelOld.class_Create.CreateManId;
                                        class_SelectAllModel.class_Create.EnglishSign = class_UpdateAllModelOld.class_Create.EnglishSign;
                                        class_SelectAllModel.class_Create.HttpRequestType = class_UpdateAllModelOld.class_Create.HttpRequestType;
                                        class_SelectAllModel.class_Create.MethodSite = class_UpdateAllModelOld.class_Create.MethodSite;
                                        class_SelectAllModel.class_Create.MicroServiceName = class_UpdateAllModelOld.class_Create.MicroServiceName;
                                        class_SelectAllModel.class_Create.Port = class_UpdateAllModelOld.class_Create.Port;
                                        class_SelectAllModel.class_Create.ReadOnly = class_UpdateAllModelOld.class_Create.ReadOnly;
                                        class_SelectAllModel.class_Create.SwaggerSign = class_UpdateAllModelOld.class_Create.SwaggerSign;
                                        class_SelectAllModel.class_Create.SystemName = class_UpdateAllModelOld.class_Create.SystemName;
                                    }
                                    if (class_UpdateAllModelOld.class_SubList != null && class_UpdateAllModelOld.class_SubList.Count > 0)
                                    {
                                        Class_SelectAllModel.Class_Sub class_Sub;
                                        if (class_SelectAllModel.class_SubList.Count == 0)
                                        {
                                            class_Sub = new Class_SelectAllModel.Class_Sub();
                                            class_SelectAllModel.class_SubList.Add(class_Sub);
                                        }
                                        else
                                            class_Sub = class_SelectAllModel.class_SubList[0];
                                        class_Sub.NameSpace = class_UpdateAllModelOld.class_SubList[0].NameSpace;
                                        class_Sub.ControlSwaggerValue = class_UpdateAllModelOld.class_SubList[0].ControlSwaggerValue;
                                        class_Sub.ControlSwaggerDescription = class_UpdateAllModelOld.class_SubList[0].ControlSwaggerDescription;
                                        class_Sub.MethodContent = class_UpdateAllModelOld.class_SubList[0].MethodContent;
                                        class_Sub.ServiceInterFaceReturnRemark = class_UpdateAllModelOld.class_SubList[0].ServiceInterFaceReturnRemark;
                                        class_Sub.TableName = class_UpdateAllModelOld.class_SubList[0].TableName;
                                        class_Sub.AliasName = class_UpdateAllModelOld.class_SubList[0].AliasName;
                                    }
                                    class_SelectAllModel.class_SelectDataBase.dataBaseName = class_UpdateAllModelOld.class_SelectDataBase.dataBaseName;
                                    class_SelectAllModel.class_SelectDataBase.databaseType = class_UpdateAllModelOld.class_SelectDataBase.databaseType;
                                    class_SelectAllModel.class_SelectDataBase.dataSourcePassWord = class_UpdateAllModelOld.class_SelectDataBase.dataSourcePassWord;
                                    class_SelectAllModel.class_SelectDataBase.dataSourceUrl = class_UpdateAllModelOld.class_SelectDataBase.dataSourceUrl;
                                    class_SelectAllModel.class_SelectDataBase.dataSourceUserName = class_UpdateAllModelOld.class_SelectDataBase.dataSourceUserName;
                                    class_SelectAllModel.class_SelectDataBase.Port = class_UpdateAllModelOld.class_SelectDataBase.Port;
                                    class_SelectAllModel.AllPackerName = class_UpdateAllModelOld.AllPackerName;
                                    break;
                                case "delete":
                                    Class_DeleteAllModel class_DeleteAllModelOld = new Class_DeleteAllModel();
                                    class_DeleteAllModelOld = this.FromXmlToDeleteObject<Class_DeleteAllModel>(xmlFileName);
                                    if (class_DeleteAllModelOld.class_Create != null)
                                    {
                                        class_SelectAllModel.class_Create.CreateDo = class_DeleteAllModelOld.class_Create.CreateDo;
                                        class_SelectAllModel.class_Create.CreateDoId = class_DeleteAllModelOld.class_Create.CreateDoId;
                                        class_SelectAllModel.class_Create.CreateFrontDo = class_DeleteAllModelOld.class_Create.CreateFrontDo;
                                        class_SelectAllModel.class_Create.CreateFrontDoId = class_DeleteAllModelOld.class_Create.CreateFrontDoId;
                                        class_SelectAllModel.class_Create.CreateMan = class_DeleteAllModelOld.class_Create.CreateMan;
                                        class_SelectAllModel.class_Create.CreateManId = class_DeleteAllModelOld.class_Create.CreateManId;
                                        class_SelectAllModel.class_Create.EnglishSign = class_DeleteAllModelOld.class_Create.EnglishSign;
                                        class_SelectAllModel.class_Create.HttpRequestType = class_DeleteAllModelOld.class_Create.HttpRequestType;
                                        class_SelectAllModel.class_Create.MethodSite = class_DeleteAllModelOld.class_Create.MethodSite;
                                        class_SelectAllModel.class_Create.MicroServiceName = class_DeleteAllModelOld.class_Create.MicroServiceName;
                                        class_SelectAllModel.class_Create.Port = class_DeleteAllModelOld.class_Create.Port;
                                        class_SelectAllModel.class_Create.ReadOnly = class_DeleteAllModelOld.class_Create.ReadOnly;
                                        class_SelectAllModel.class_Create.SwaggerSign = class_DeleteAllModelOld.class_Create.SwaggerSign;
                                        class_SelectAllModel.class_Create.SystemName = class_DeleteAllModelOld.class_Create.SystemName;
                                    }
                                    if (class_DeleteAllModelOld.class_SubList != null && class_DeleteAllModelOld.class_SubList.Count > 0)
                                    {
                                        Class_SelectAllModel.Class_Sub class_Sub;
                                        if (class_SelectAllModel.class_SubList.Count == 0)
                                        {
                                            class_Sub = new Class_SelectAllModel.Class_Sub();
                                            class_SelectAllModel.class_SubList.Add(class_Sub);
                                        }
                                        else
                                            class_Sub = class_SelectAllModel.class_SubList[0];
                                        class_Sub.NameSpace = class_DeleteAllModelOld.class_SubList[0].NameSpace;
                                        class_Sub.ControlSwaggerValue = class_DeleteAllModelOld.class_SubList[0].ControlSwaggerValue;
                                        class_Sub.ControlSwaggerDescription = class_DeleteAllModelOld.class_SubList[0].ControlSwaggerDescription;
                                        class_Sub.MethodContent = class_DeleteAllModelOld.class_SubList[0].MethodContent;
                                        class_Sub.ServiceInterFaceReturnRemark = class_DeleteAllModelOld.class_SubList[0].ServiceInterFaceReturnRemark;
                                        class_Sub.TableName = class_DeleteAllModelOld.class_SubList[0].TableName;
                                        class_Sub.AliasName = class_DeleteAllModelOld.class_SubList[0].AliasName;
                                    }
                                    class_SelectAllModel.class_SelectDataBase.dataBaseName = class_DeleteAllModelOld.class_SelectDataBase.dataBaseName;
                                    class_SelectAllModel.class_SelectDataBase.databaseType = class_DeleteAllModelOld.class_SelectDataBase.databaseType;
                                    class_SelectAllModel.class_SelectDataBase.dataSourcePassWord = class_DeleteAllModelOld.class_SelectDataBase.dataSourcePassWord;
                                    class_SelectAllModel.class_SelectDataBase.dataSourceUrl = class_DeleteAllModelOld.class_SelectDataBase.dataSourceUrl;
                                    class_SelectAllModel.class_SelectDataBase.dataSourceUserName = class_DeleteAllModelOld.class_SelectDataBase.dataSourceUserName;
                                    class_SelectAllModel.class_SelectDataBase.Port = class_DeleteAllModelOld.class_SelectDataBase.Port;
                                    class_SelectAllModel.AllPackerName = class_DeleteAllModelOld.AllPackerName;
                                    break;
                                default:
                                    break;
                            }
                        }
                        #endregion
                        class_SelectAllModel.class_Create.MethodId = returnKey;
                        class_SelectAllModel.class_Create.DateTime = System.DateTime.Now;
                        if (!this.SelectToXml(class_SelectAllModel.class_Create.MethodId, class_SelectAllModel, false))
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
        public T FromXmlToAllParamSetUpObject<T>(string fileFullName) where T : class
        {
            return _FromXmlToObject<T>("AllParamSetUp", fileFullName);
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
        public bool DataBaseAllParamSetUpValueToXml<T>(string fileName, T t)
        {
            return SaveToXml<T>("AllParamSetUp", fileName, t, false);
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
            return SaveToXml<T>("SystemDefault", fileName, t, false);
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

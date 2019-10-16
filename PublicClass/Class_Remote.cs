using MDIDemo.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MDIDemo.PublicClass
{
    public class Class_Remote
    {
        public Class_Remote(string BaseUrl, bool HttpSign, string MicroServePath, bool CSign)
        {
            Ini(BaseUrl, HttpSign, MicroServePath, CSign);
        }
        public Class_Remote(string MicroServePath, bool CSign)
        {
            DefaultIni(MicroServePath, CSign);
        }
        public Class_Remote(string MicroServePath)
        {
            DefaultIni(MicroServePath, true);
        }
        private void DefaultIni(string MicroServePath, bool CSign)
        {
            string FileName = string.Format(@"{0}\{1}\{2}.xml"
                , Application.StartupPath
                , "AllParamSetUp"
                , "Class_AllParamSetUp");
            if (File.Exists(FileName))
            {
                string RemoteAddress = null;
                int RemotePort = 0;
                string MyBaseUrl = null;
                Class_AllParamSetUp class_AllParamSetUp = new Class_AllParamSetUp();
                XmlUtil xmlUtil = new XmlUtil();
                class_AllParamSetUp = xmlUtil.XmlSerialObject<Class_AllParamSetUp>(FileName);
                if (class_AllParamSetUp != null)
                {
                    RemoteAddress = class_AllParamSetUp.RemoteAddress;
                    RemotePort = class_AllParamSetUp.RemotePort;
                    MyBaseUrl = RemoteAddress;
                    if (RemotePort > 0)
                    {
                        MyBaseUrl += ":" + RemotePort.ToString();
                    }
                    Ini(MyBaseUrl, class_AllParamSetUp.HttpSign, MicroServePath, CSign);//这里写入默认值
                }
            }
            else
                Ini("www.lh.com:2000", true, MicroServePath, CSign);//这里写入默认值
        }
        private Class_RestClient class_RestClient;

        private void Ini(string BaseUrl, bool HttpSign, string MicroServePath, bool CSign)
        {
            class_RestClient = new Class_RestClient(BaseUrl, HttpSign, MicroServePath, CSign);
        }
        public bool UploadFileByHttp(string FileName)
        {
            try
            {
                return class_RestClient.UploadFileByHttp("useAuthorityPageFeign/saveCapture", FileName);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int UpdatePassWord(string PassWord)
        {
            List<Class_ParaArray> class_ParaArrays = new List<Class_ParaArray>();
            Class_ParaArray class_ParaArrayFileName = new Class_ParaArray();
            class_ParaArrayFileName.ParaName = "passWord";
            class_ParaArrayFileName.ParaValue = PassWord;
            class_ParaArrays.Add(class_ParaArrayFileName);
            string ResultValue = class_RestClient.Post("useAuthorityPageFeign/updatePassWord", class_ParaArrays, null, true);
            return Convert.ToInt32(ResultValue);
        }
        public ResultVO<T> UpLoadFileBinary<T>(string AllPathFileName, string FolderName, string FileName)
        {
            return PrivateUploadFile<T>(AllPathFileName, FileName, null, FolderName);
        }
        public ResultVO<T> UploadFileSelect<T>(string AllPathFileName, string FileName)
        {
            return PrivateUploadFile<T>(AllPathFileName, FileName, "Select", null);
        }
        public ResultVO<T> UploadFileInsert<T>(string AllPathFileName, string FileName)
        {
            return PrivateUploadFile<T>(AllPathFileName, FileName, "Insert", null);
        }
        public ResultVO<T> UploadFileUpdate<T>(string AllPathFileName, string FileName)
        {
            return PrivateUploadFile<T>(AllPathFileName, FileName, "Update", null);
        }
        public ResultVO<T> UploadFileDelete<T>(string AllPathFileName, string FileName)
        {
            return PrivateUploadFile<T>(AllPathFileName, FileName, "Delete", null);
        }
        private ResultVO<T> PrivateUploadFile<T>(string AllPathFileName, string FileName, string Operate, string FolderName)
        {
            List<Class_ParaArray> class_ParaArrays = new List<Class_ParaArray>();
            Class_ParaArray class_ParaArrayFileName = new Class_ParaArray();
            class_ParaArrayFileName.ParaName = "fileName";
            class_ParaArrayFileName.ParaValue = FileName;
            class_ParaArrays.Add(class_ParaArrayFileName);
            if (FolderName != null)
            {
                Class_ParaArray class_ParaArrayFolderName = new Class_ParaArray();
                class_ParaArrayFolderName.ParaName = "dictionary";
                class_ParaArrayFolderName.ParaValue = FolderName;
                class_ParaArrays.Add(class_ParaArrayFolderName);
            }

            byte[] byteArray = FileBinaryConvertHelper.File2Bytes(AllPathFileName);
            ResultVO<T> resultVO = new ResultVO<T>();
            string Url = "useAuthorityPageFeign/uploadFile";
            if (FolderName == null)
                Url = string.Format("useAuthorityPageFeign/uploadFile{0}", Operate);
            string ResultValue = class_RestClient.PostBinary(Url
                , class_ParaArrays
                , byteArray, true);
            return JsonTools.JsonToObject(ResultValue, resultVO) as ResultVO<T>;
        }
        public byte[] DownLoadFile(string PageKey, string pageType)
        {
            List<Class_ParaArray> class_ParaArrays = new List<Class_ParaArray>();
            Class_ParaArray class_ParaArray = new Class_ParaArray();
            class_ParaArray.ParaName = "dictionary";
            class_ParaArray.ParaValue = pageType;
            class_ParaArrays.Add(class_ParaArray);

            Class_ParaArray class_ParaArrayPageType = new Class_ParaArray();
            class_ParaArrayPageType.ParaName = "fileName";
            class_ParaArrayPageType.ParaValue = PageKey;
            class_ParaArrays.Add(class_ParaArrayPageType);

            string ResultValue = class_RestClient.Post("useAuthorityPageFeign/downLoadFile", class_ParaArrays);
            return System.Text.Encoding.UTF8.GetBytes(ResultValue);
        }
        public int DeletePage(string PageKey, string pageType)
        {
            List<Class_ParaArray> class_ParaArrays = new List<Class_ParaArray>();
            Class_ParaArray class_ParaArray = new Class_ParaArray();
            class_ParaArray.ParaName = "pageKey";
            class_ParaArray.ParaValue = PageKey;
            class_ParaArrays.Add(class_ParaArray);
            Class_ParaArray class_ParaArrayPageType = new Class_ParaArray();
            class_ParaArrayPageType.ParaName = "pageType";
            class_ParaArrayPageType.ParaValue = pageType;
            class_ParaArrays.Add(class_ParaArrayPageType);
            string ResultValue = class_RestClient.Post("useAuthorityPageFeign/deletePageAndXml", class_ParaArrays);
            return Convert.ToInt32(ResultValue);

        }
        public List<string> SelectUseCreateNickNameList()
        {
            return _SelectUseNickNameList("selectUseCreateNickNameList");
        }
        public List<string> SelectUseDoNickNameList()
        {
            return _SelectUseNickNameList("selectUseDoNickNameList");
        }
        public List<string> SelectUseFrontNickNameList()
        {
            return _SelectUseNickNameList("selectUseFrontNickNameList");
        }
        private List<string> _SelectUseNickNameList(string MethodName)
        {
            MethodName = string.Format("useAuthorityPageFeign/{0}", MethodName);
            return _SelectDictionaryListString(MethodName, null);

        }
        public int InsertPage(List<Class_ParaArray> class_ParaArrays)
        {
            string ResultValue = class_RestClient.Post("useAuthorityPageFeign/insertPage", class_ParaArrays);
            return Convert.ToInt32(ResultValue);
        }
        /// <summary>
        /// 下载指定公司所有用户
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Company">公司全称</param>
        /// <param name="Page">当前页数</param>
        /// <param name="Limit">每页条数</param>
        /// <returns></returns>
        public ResultVOPage<T> DownAllUseByCompany<T>(string Company)
        {
            ResultVOPage<T> resultVO = new ResultVOPage<T>();
            List<Class_ParaArray> class_ParaArrays = new List<Class_ParaArray>();
            Class_ParaArray class_ParaArray = new Class_ParaArray();
            class_ParaArray.ParaName = "companyName";
            class_ParaArray.ParaValue = Company;
            class_ParaArrays.Add(class_ParaArray);
            string ResultValue = class_RestClient.Post("useAuthorityPageFeign/downAllUseByCompany", class_ParaArrays, null, true);
            return JsonTools.JsonToObject(ResultValue, resultVO, true) as ResultVOPage<T>;
        }
        public ResultVO<T> SelectVersionList<T>(PageVersionListInParam pageVersionListInParam)
        {
            ResultVO<T> resultVO = new ResultVO<T>();
            string ResultValue = class_RestClient.Post("useAuthorityPageFeign/selectVersionList"
                , null
                , JsonTools.ObjectToJson(pageVersionListInParam)
                , true);
            return JsonTools.JsonToObject(ResultValue, resultVO, true) as ResultVO<T>;
        }
        /// <summary>
        /// 登录远程方法
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="class_ParaArrays">参数列表</param>
        /// <returns></returns>
        public ResultVO<T> UseLogCS<T>(List<Class_ParaArray> class_ParaArrays) where T : class
        {
            ResultVO<T> resultVO = new ResultVO<T>();
            string ResultValue = class_RestClient.Post("logFeign/useLogCS", class_ParaArrays);
            return JsonTools.JsonToObject(ResultValue, resultVO) as ResultVO<T>;
        }
        public List<string> selectSystemListString()
        {
            List<Class_ParaArray> class_ParaArrays = new List<Class_ParaArray>();
            Class_ParaArray class_ParaArray = new Class_ParaArray();
            class_ParaArray.ParaName = "signName";
            class_ParaArray.ParaValue = "STS";
            class_ParaArrays.Add(class_ParaArray);
            return _SelectDictionaryListString("useAuthorityPageFeign/selectDictionaryListString", class_ParaArrays);
        }
        private List<string> _SelectDictionaryListString(string MethodName, List<Class_ParaArray> class_ParaArrays)
        {
            List<string> vs = new List<string>();
            string ResultValue = class_RestClient.Post(MethodName, class_ParaArrays);
            return JsonTools.JsonToObject(ResultValue, vs) as List<string>;
        }

        //public string SelectUseId(string NickName)
        //{
        //    List<Class_ParaArray> class_ParaArrays = new List<Class_ParaArray>();
        //    Class_ParaArray class_ParaArray = new Class_ParaArray();
        //    class_ParaArray.ParaName = "nickName";
        //    class_ParaArray.ParaValue = NickName;
        //    class_ParaArrays.Add(class_ParaArray);
        //    string ResultValue = class_RestClient.Post("useAuthorityPageFeign/selectUseId", class_ParaArrays);
        //    return ResultValue;
        //}
    }
}

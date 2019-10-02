using MDIDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDIDemo.PublicClass
{
    public class Class_Remote
    {
        public Class_Remote(string BaseUrl, bool HttpSign)
        {
            Ini(BaseUrl, HttpSign);
        }
        public Class_Remote()
        {
            Ini("localhost:2519", true);//这里写入默认值
        }

        private string BaseUrl;
        private Class_RestClient class_RestClient;

        private void Ini(string BaseUrl, bool HttpSign)
        {
            this.BaseUrl = BaseUrl.Trim();
            class_RestClient = new Class_RestClient(this.BaseUrl, HttpSign);
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
            MethodName = string.Format("myBatisUseController/{0}", MethodName);
            return _SelectDictionaryListString(MethodName, null);

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
            string ResultValue = class_RestClient.Post("myBatisUseController/useLogCS", class_ParaArrays);
            return JsonTools.JsonToObject(ResultValue, resultVO) as ResultVO<T>;
        }
        public List<string> selectSystemListString()
        {
            List<Class_ParaArray> class_ParaArrays = new List<Class_ParaArray>();
            Class_ParaArray class_ParaArray = new Class_ParaArray();
            class_ParaArray.ParaName = "signName";
            class_ParaArray.ParaValue = "STS";
            class_ParaArrays.Add(class_ParaArray);
            return _SelectDictionaryListString("dictionaryController/selectDictionaryListString", class_ParaArrays);
        }
        private List<string> _SelectDictionaryListString(string MethodName, List<Class_ParaArray> class_ParaArrays)
        {
            List<string> vs = new List<string>();
            string ResultValue = class_RestClient.Post(MethodName, class_ParaArrays);
            return JsonTools.JsonToObject(ResultValue, vs) as List<string>;
        }

        public string SelectUseId(string NickName)
        {
            List<Class_ParaArray> class_ParaArrays = new List<Class_ParaArray>();
            Class_ParaArray class_ParaArray = new Class_ParaArray();
            class_ParaArray.ParaName = "nickName";
            class_ParaArray.ParaValue = NickName;
            class_ParaArrays.Add(class_ParaArray);
            string ResultValue = class_RestClient.Post("myBatisUseController/selectUseId", class_ParaArrays);
            return ResultValue;

        }
    }
}

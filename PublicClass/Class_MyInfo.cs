using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDIDemo.PublicClass
{
    public class Class_MyInfo
    {
        public static string UseId = "useId";
        public static string UseName = "useName";
        public static string UseType = "useType";
        public static string TokenName = "accessToken";
        public static string ClientType = "clientType";

        public static string UseIdValue;
        public static string UseNameValue;
        public static string UseTypeValue;
        public static string ClientTypeValue = "CS";
        public static string TokenNameValue;
        /// <summary>
        /// token有效时间
        /// </summary>
        public static DateTime TokenEffectiveDateTime;

        public List<Class_ParaArray> GetAddStrongPara(List<Class_ParaArray> OldParaArrays)
        {
            List<Class_ParaArray> NewParaArrays = new List<Class_ParaArray>();
            if (OldParaArrays != null)
                NewParaArrays = OldParaArrays;
            if (UseIdValue != null)
            {
                Class_ParaArray UseIdParaArray = new Class_ParaArray();
                UseIdParaArray.ParaName = UseId;
                UseIdParaArray.ParaValue = UseIdValue;
                NewParaArrays.Add(UseIdParaArray);
            }
            if (UseNameValue != null)
            {
                Class_ParaArray UseNameParaArray = new Class_ParaArray();
                UseNameParaArray.ParaName = UseName;
                UseNameParaArray.ParaValue = UseNameValue;
                NewParaArrays.Add(UseNameParaArray);
            }
            if (UseTypeValue != null)
            {
                Class_ParaArray UseTypeParaArray = new Class_ParaArray();
                UseTypeParaArray.ParaName = UseType;
                UseTypeParaArray.ParaValue = UseTypeValue;
                NewParaArrays.Add(UseTypeParaArray);
            }
            if (TokenNameValue != null && TokenEffectiveDateTime != null)
            {
                if (TokenEffectiveDateTime >= DateTime.Now)
                {
                    Class_ParaArray TokenNameParaArray = new Class_ParaArray();
                    TokenNameParaArray.ParaName = TokenName;
                    TokenNameParaArray.ParaValue = TokenNameValue;
                    NewParaArrays.Add(TokenNameParaArray);
                }
            }
            return NewParaArrays;
        }
    }
}

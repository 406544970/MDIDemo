using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDIDemo.PublicClass
{
    public interface IClass_CreateFrontPage
    {
        string GetUsedMethod();
        List<string> GetComponentType();
    }
    public interface IClass_InterFaceCreateCode
    {
        #region
        bool IsCheckOk(ref List<string> outMessage);
        void AddAllOutFieldName();
        string _GetTypeContent(string FieldType);
        #endregion

        #region 粘子层
        string GetMap(int Index);
        string GetSql(int Index);
        string GetServiceInterFace(int Index);
        string GetServiceImpl(int Index);
        string GetModel(int Index);
        string GetDTO(int Index);
        string GetDAO(int Index);
        string GetControl(int Index);
        string GetInPutParam(int Index);
        string GetTestSql(int Index);
        string GetFrontPage();
        #endregion

        #region Feign
        string GetFeignControl(int Index);
        string GetFeignInterFace(int Index);
        string GetFeignInterFaceHystric(int Index);
        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDIDemo.PublicClass
{
    public interface IClass_InterFaceCreateCode
    {
        bool IsCheckOk();

        #region 
        string GetMap(int Index);
        string GetMapLable(int Index);
        //string GetWhereLable(int Index);
        string GetServiceInterFace(int Index);
        string GetServiceImpl(int Index);
        string GetModel(int Index);
        string GetDTO(int Index);
        string GetDAO(int Index);
        string GetControl(int Index);
        string GetFeignControl(int Index);
        string GetTestUnit(int Index);
        #endregion

    }
}

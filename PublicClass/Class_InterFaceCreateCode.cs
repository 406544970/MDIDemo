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

        #region New
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

        #region 主表
        string GetMainMap();
        string GetMainMapLable();
        //string GetMainWhereLable();
        string GetMainServiceInterFace();
        string GetMainServiceImpl();
        string GetMainModel();
        string GetMainDTO();
        string GetMainDAO();
        string GetMainControl();
        string GetMainFeignControl();
        string GetMainTestUnit();
        #endregion

        #region 从表一
        string GetSubOneMap();
        string GetSubOneMapLable();
        string GetSubOneServiceInterFace();
        string GetSubOneServiceImpl();
        string GetSubOneModel();
        string GetSubOneDTO();
        string GetSubOneDAO();
        string GetSubOneControl();
        string GetSubOneTestUnit();
        #endregion

        #region 从表二
        string GetSubTwoMap();
        string GetSubTwoMapLable();
        string GetSubTwoServiceInterFace();
        string GetSubTwoServiceImpl();
        string GetSubTwoModel();
        string GetSubTwoDTO();
        string GetSubTwoDAO();
        string GetSubTwoControl();
        string GetSubTwoTestUnit();
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDIDemo.PublicClass
{
    public interface Class_InterFaceCreateCode
    {
        bool IsCheckOk();
        #region 主表
        string GetMainMap();
        string GetMainMapLable();
        string GetMainWhereLable();
        string GetMainServiceInterFace();
        string GetMainServiceImpl();
        string GetMainModel();
        string GetMainDTO();
        string GetMainDAO();
        string GetMainControl();
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

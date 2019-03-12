using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDIDemo.PublicClass
{
    /// <summary>
    /// 生成Select相关代码
    /// </summary>
    public class Class_CreateSelectCode : Class_InterFaceCreateCode
    {
        public Class_CreateSelectCode(string xmlFileName)
        {
            if (xmlFileName != null)
            {
                Class_PublicMethod class_PublicMethod = new Class_PublicMethod();
                class_SelectAllModel = new Class_SelectAllModel();
                class_SelectAllModel = class_PublicMethod.FromXmlToSelectObject<Class_SelectAllModel>(xmlFileName);
            }
        }
        private Class_SelectAllModel class_SelectAllModel;
        public string GetMainControl()
        {
            throw new NotImplementedException();
        }

        public string GetMainDAO()
        {
            throw new NotImplementedException();
        }

        public string GetMainDTO()
        {
            throw new NotImplementedException();
        }

        public string GetMainMap()
        {
            Class_Tool class_Tool = new Class_Tool();
            StringBuilder stringBuilder = new StringBuilder();
            Class_InterFaceDataBase class_InterFaceDataBase;
            if (class_SelectAllModel.class_Main.IsAddXmlHead)
            {
                stringBuilder.Append("<?xml version=\"1.0\" encoding=\"UTF - 8\" ?>\r\n");
                stringBuilder.Append("< !DOCTYPE mapper PUBLIC \"-//mybatis.org//DTD Mapper 3.0//EN\" \"http://mybatis.org/dtd/mybatis-3-mapper.dtd\" >\r\n");
            }
            stringBuilder.AppendFormat("<mapper namespace=\"{0}.{1}Mapper\">\r\n"
                , class_SelectAllModel.AllPackerName
                , class_SelectAllModel.class_Main.NameSpace);
            if (class_SelectAllModel.class_Main.ResultType == 0)
            {
                stringBuilder.AppendFormat("{0}<resultMap id=\"{1}\" type=\"{3}.{2}\">\r\n"
                    , class_Tool.AddOneSpace()
                    , class_SelectAllModel.class_Main.ResultMapId
                    , class_SelectAllModel.class_Main.ResultMapType
                , class_SelectAllModel.AllPackerName);
                class_Tool.AddOne();
                switch (class_SelectAllModel.class_SelectDataBase.databaseType)
                {
                    case "MySql":
                        class_InterFaceDataBase = new Class_MySqlDataBase(class_SelectAllModel.class_SelectDataBase.dataSourceUrl, class_SelectAllModel.class_SelectDataBase.dataBaseName, class_SelectAllModel.class_SelectDataBase.dataSourceUserName, class_SelectAllModel.class_SelectDataBase.dataSourcePassWord, class_SelectAllModel.class_SelectDataBase.Port);
                        break;
                    case "SqlServer 2017":
                        class_InterFaceDataBase = new Class_SqlServer2017DataBase(class_SelectAllModel.class_SelectDataBase.dataSourceUrl, class_SelectAllModel.class_SelectDataBase.dataBaseName, class_SelectAllModel.class_SelectDataBase.dataSourceUserName, class_SelectAllModel.class_SelectDataBase.dataSourcePassWord);
                        break;
                    default:
                        class_InterFaceDataBase = new Class_MySqlDataBase(class_SelectAllModel.class_SelectDataBase.dataSourceUrl, class_SelectAllModel.class_SelectDataBase.dataBaseName, class_SelectAllModel.class_SelectDataBase.dataSourceUserName, class_SelectAllModel.class_SelectDataBase.dataSourcePassWord, class_SelectAllModel.class_SelectDataBase.Port);
                        break;
                }
                foreach (Class_SelectAllModel.Class_Field class_Field in class_SelectAllModel.class_Main.class_Fields)
                {
                    if (class_Field.FieldIsKey)
                    {
                        stringBuilder.AppendFormat("{0}<id column=\"{1}\" property=\"{2}\" jdbcType=\"{3}\"/><!--{4}-->\r\n"
                            , class_Tool.GetSpace()
                            , class_Field.FieldName
                            , class_Field.ParaName
                            , Class_Tool.GetJdbcType(class_InterFaceDataBase.GetJavaType(class_Field.FieldType))
                            , class_Field.FieldRemark);
                    }
                    else
                    {
                        stringBuilder.AppendFormat("{0}<result column=\"{1}\" property=\"{2}\" jdbcType=\"{3}\"/><!--{4}-->\r\n"
                            , class_Tool.GetSpace()
                            , class_Field.FieldName
                            , class_Field.ParaName
                            , Class_Tool.GetJdbcType(class_InterFaceDataBase.GetJavaType(class_Field.FieldType))
                            , class_Field.FieldRemark);
                    }
                }
                stringBuilder.AppendFormat("{0}</resultMap>\r\n", class_Tool.LessOneSpace());
            }
            stringBuilder.Append("</mapper>\r\n");
            if (stringBuilder.Length > 0)
                return stringBuilder.ToString();
            else
                return null;
        }

        public string GetMainMapLable()
        {
            Class_Tool class_ToolSelect = new Class_Tool();
            StringBuilder stringBuilder = new StringBuilder();
            Class_InterFaceDataBase class_InterFaceDataBase;
            stringBuilder.AppendFormat("{1}<!--注释：{0}-->\r\n", class_SelectAllModel.class_Main.MethodContent, class_ToolSelect.AddOneSpace());
            stringBuilder.AppendFormat("{0}<select id=\"{1}\" "
                , class_ToolSelect.GetSpace()
                , class_SelectAllModel.class_Main.MethodId);
            stringBuilder.AppendFormat(" parameterType=\"{0}.{1}\">\r\n"
                , class_SelectAllModel.AllPackerName
                , "User");
            switch (class_SelectAllModel.class_SelectDataBase.databaseType)
            {
                case "MySql":
                    class_InterFaceDataBase = new Class_MySqlDataBase(class_SelectAllModel.class_SelectDataBase.dataSourceUrl, class_SelectAllModel.class_SelectDataBase.dataBaseName, class_SelectAllModel.class_SelectDataBase.dataSourceUserName, class_SelectAllModel.class_SelectDataBase.dataSourcePassWord, class_SelectAllModel.class_SelectDataBase.Port);
                    break;
                case "SqlServer 2017":
                    class_InterFaceDataBase = new Class_SqlServer2017DataBase(class_SelectAllModel.class_SelectDataBase.dataSourceUrl, class_SelectAllModel.class_SelectDataBase.dataBaseName, class_SelectAllModel.class_SelectDataBase.dataSourceUserName, class_SelectAllModel.class_SelectDataBase.dataSourcePassWord);
                    break;
                default:
                    class_InterFaceDataBase = new Class_MySqlDataBase(class_SelectAllModel.class_SelectDataBase.dataSourceUrl, class_SelectAllModel.class_SelectDataBase.dataBaseName, class_SelectAllModel.class_SelectDataBase.dataSourceUserName, class_SelectAllModel.class_SelectDataBase.dataSourcePassWord, class_SelectAllModel.class_SelectDataBase.Port);
                    break;
            }
            stringBuilder.AppendFormat("{0}SELECT\r\n", class_ToolSelect.AddOneSpace());
            int Counter = 0;
            foreach (Class_SelectAllModel.Class_Field class_Field in class_SelectAllModel.class_Main.class_Fields)
            {
                #region Select
                if (class_Field.SelectSelect)
                {
                    string FieldName = class_Field.FieldName;
                    if ((class_Field.CaseWhen != null) && (class_Field.CaseWhen.Length > 0))
                    {
                        Class_CaseWhen class_CaseWhen = new Class_CaseWhen();
                        FieldName = class_CaseWhen.GetCaseWhenContent(class_Field.CaseWhen, FieldName, class_ToolSelect.GetSpace());
                        if ((class_Field.FunctionName == null) || (class_Field.FunctionName.Length == 0))
                        {
                            FieldName = string.Format(FieldName + " AS {0}", class_Field.ParaName);
                        }
                    }
                    if ((class_Field.FunctionName != null) && (class_Field.FunctionName.Length > 0))
                    {
                        FieldName = string.Format(class_Field.FunctionName.Replace("?", "{0}") + " AS {1}", FieldName, class_Field.ParaName);
                    }
                    if (Counter++ > 0)
                        stringBuilder.AppendFormat("{1},{0}<!--{2}-->\r\n", FieldName, class_ToolSelect.GetSpace(), class_Field.FieldRemark);
                    else
                        stringBuilder.AppendFormat("{1}{0}<!--{2}-->\r\n", FieldName, class_ToolSelect.GetSpace(), class_Field.FieldRemark);
                }
                #endregion
            }
            stringBuilder.AppendFormat("{0}FROM {1}\r\n", class_ToolSelect.GetSpace(), class_SelectAllModel.class_Main.TableName);
            stringBuilder.Append(GetMainWhereLable());
            stringBuilder.AppendFormat("{0}</select>\r\n", class_ToolSelect.LessOneSpace());
            if (stringBuilder.Length > 0)
                return stringBuilder.ToString();
            else
                return null;
        }

        public string GetMainWhereLable()
        {
            bool HavaChoose = false;
            Class_Tool class_ToolWhere = new Class_Tool();
            Class_Tool class_ToolChoose = new Class_Tool();
            StringBuilder stringBuilderWhereAnd = new StringBuilder();
            StringBuilder stringBuilderWhereOr = new StringBuilder();
            StringBuilder stringBuilderGroup = new StringBuilder();
            StringBuilder stringBuilderHaving = new StringBuilder();
            StringBuilder stringBuilderOrder = new StringBuilder();
            Class_InterFaceDataBase class_InterFaceDataBase;
            switch (class_SelectAllModel.class_SelectDataBase.databaseType)
            {
                case "MySql":
                    class_InterFaceDataBase = new Class_MySqlDataBase(class_SelectAllModel.class_SelectDataBase.dataSourceUrl, class_SelectAllModel.class_SelectDataBase.dataBaseName, class_SelectAllModel.class_SelectDataBase.dataSourceUserName, class_SelectAllModel.class_SelectDataBase.dataSourcePassWord, class_SelectAllModel.class_SelectDataBase.Port);
                    break;
                case "SqlServer 2017":
                    class_InterFaceDataBase = new Class_SqlServer2017DataBase(class_SelectAllModel.class_SelectDataBase.dataSourceUrl, class_SelectAllModel.class_SelectDataBase.dataBaseName, class_SelectAllModel.class_SelectDataBase.dataSourceUserName, class_SelectAllModel.class_SelectDataBase.dataSourcePassWord);
                    break;
                default:
                    class_InterFaceDataBase = new Class_MySqlDataBase(class_SelectAllModel.class_SelectDataBase.dataSourceUrl, class_SelectAllModel.class_SelectDataBase.dataBaseName, class_SelectAllModel.class_SelectDataBase.dataSourceUserName, class_SelectAllModel.class_SelectDataBase.dataSourcePassWord, class_SelectAllModel.class_SelectDataBase.Port);
                    break;
            }
            int WhereCounter = 0;
            class_ToolWhere.SetSpaceCount(2);
            stringBuilderWhereAnd.AppendFormat("{0}<where>\r\n", class_ToolWhere.GetSpace());
            foreach (Class_SelectAllModel.Class_Field class_Field in class_SelectAllModel.class_Main.class_Fields)
            {
                #region Where
                if (class_Field.WhereSelect)
                {
                    if (WhereCounter++ == 0)
                    {
                        class_ToolWhere.SetSpaceCount(3);
                    }
                    string FieldName = class_Field.FieldName;
                    string IfLabel = null;
                    string NowWhere = null;
                    if (class_Field.WhereType == "AND")
                    {
                        if (class_Field.WhereIsNull)
                        {
                            IfLabel = string.Format("{1}<if test=\"{0} != null\">\r\n", class_Field.ParaName, class_ToolWhere.GetSpace());
                        }
                    }
                    if (class_Field.WhereType == "OR")
                    {
                        if (!HavaChoose)
                        {
                            class_ToolChoose.SetSpaceCount(class_ToolWhere.GetSpaceCount() + 1);
                        }
                        HavaChoose = true;
                        IfLabel = string.Format("{1}<when test=\"{0} != null\">\r\n", class_Field.ParaName, class_ToolChoose.GetSpace());
                    }
                    NowWhere = string.Format("{0}{2} {1} "
                        , (class_Field.WhereType == "AND" ? class_ToolWhere.AddOneSpace() : class_ToolChoose.AddOneSpace())
                        , FieldName
                        , class_Field.WhereType);
                    int LikeType = class_Field.LogType.IndexOf("Like") > -1 ? 1 : -100;
                    if (class_Field.LogType.Equals("左Like"))
                        LikeType = -1;
                    if (class_Field.LogType.Equals("右Like"))
                        LikeType = 1;
                    if (class_Field.LogType.Equals("全Like"))
                        LikeType = 0;
                    NowWhere += string.Format("{0} ", class_Field.LogType.IndexOf("Like") > -1 ? "like" : class_Field.LogType);
                    if (class_Field.WhereValue == "固定值")
                    {
                        NowWhere = NowWhere + string.Format("'{0}'", class_Field.WhereValue);
                    }
                    else
                    {
                        if (LikeType < -99)
                            NowWhere = NowWhere + "#{" + string.Format("{0},jdbcType={1}"
                            , class_Field.ParaName
                            , Class_Tool.GetJdbcType(class_InterFaceDataBase.GetJavaType(class_Field.FieldType))) + "}";
                        else
                        {
                            switch (LikeType)
                            {
                                case -1:
                                    NowWhere = NowWhere + "%#{" + string.Format("{0},jdbcType={1}"
                                    , class_Field.ParaName
                                    , Class_Tool.GetJdbcType(class_InterFaceDataBase.GetJavaType(class_Field.FieldType))) + "}";
                                    break;
                                case 0:
                                    NowWhere = NowWhere + "%#{" + string.Format("{0},jdbcType={1}"
                                    , class_Field.ParaName
                                    , Class_Tool.GetJdbcType(class_InterFaceDataBase.GetJavaType(class_Field.FieldType))) + "}%";
                                    break;
                                case 1:
                                    NowWhere = NowWhere + "#{" + string.Format("{0},jdbcType={1}"
                                    , class_Field.ParaName
                                    , Class_Tool.GetJdbcType(class_InterFaceDataBase.GetJavaType(class_Field.FieldType))) + "}%";
                                    break;
                                default:
                                    NowWhere = NowWhere + "%#{" + string.Format("{0},jdbcType={1}"
                                    , class_Field.ParaName
                                    , Class_Tool.GetJdbcType(class_InterFaceDataBase.GetJavaType(class_Field.FieldType))) + "}";
                                    break;
                            }
                        }
                    }
                    if ((class_Field.LogType.IndexOf("<") > -1) || (class_Field.LogType.IndexOf(">") > -1) || (class_Field.LogType.IndexOf("&") > -1))
                        NowWhere = string.Format("{0}<!CDATA[{1}]]>\r\n", class_ToolWhere.GetSpace(), NowWhere.Trim());
                    else
                        NowWhere += "\r\n";
                    if (class_Field.WhereType == "AND")
                    {
                        if (class_Field.WhereIsNull)
                        {
                            NowWhere += string.Format("{0}</if>\r\n", class_ToolWhere.LessOneSpace());
                        }
                    }
                    if (class_Field.WhereType == "OR")
                    {
                        //<when test="title != null">
                        NowWhere += string.Format("{0}</when>\r\n", class_ToolChoose.LessOneSpace());
                    }
                    if (class_Field.WhereType == "AND")
                        stringBuilderWhereAnd.Append(IfLabel + NowWhere);
                    if (class_Field.WhereType == "OR")
                        stringBuilderWhereOr.Append(IfLabel + NowWhere);
                }
                #endregion
            }
            if (stringBuilderWhereOr.Length > 0)
            {
                stringBuilderWhereAnd.AppendFormat("{0}<choose>\r\n", class_ToolChoose.LessOneSpace());
                stringBuilderWhereAnd.Append(stringBuilderWhereOr.ToString());
                stringBuilderWhereAnd.AppendFormat("{0}</choose>\r\n", class_ToolChoose.GetSpace());
            }
            stringBuilderWhereAnd.AppendFormat("{0}</where>\r\n", class_ToolWhere.LessOneSpace());
            if (stringBuilderGroup.Length > 0)
            {

            }
            if (stringBuilderHaving.Length > 0)
            {

            }
            if (stringBuilderOrder.Length > 0)
            {
            }
            if (stringBuilderWhereAnd.Length > 0)
                return stringBuilderWhereAnd.ToString();
            else
                return null;
        }
        public string GetMainModel()
        {
            throw new NotImplementedException();
        }

        public string GetMainServiceImpl()
        {
            throw new NotImplementedException();
        }

        public string GetMainServiceInterFace()
        {
            throw new NotImplementedException();
        }

        public string GetMainTestUnit()
        {
            throw new NotImplementedException();
        }

        public string GetSubOneControl()
        {
            throw new NotImplementedException();
        }

        public string GetSubOneDAO()
        {
            throw new NotImplementedException();
        }

        public string GetSubOneDTO()
        {
            throw new NotImplementedException();
        }

        public string GetSubOneMap()
        {
            throw new NotImplementedException();
        }

        public string GetSubOneMapLable()
        {
            throw new NotImplementedException();
        }

        public string GetSubOneModel()
        {
            throw new NotImplementedException();
        }

        public string GetSubOneServiceImpl()
        {
            throw new NotImplementedException();
        }

        public string GetSubOneServiceInterFace()
        {
            throw new NotImplementedException();
        }

        public string GetSubOneTestUnit()
        {
            throw new NotImplementedException();
        }

        public string GetSubTwoControl()
        {
            throw new NotImplementedException();
        }

        public string GetSubTwoDAO()
        {
            throw new NotImplementedException();
        }

        public string GetSubTwoDTO()
        {
            throw new NotImplementedException();
        }

        public string GetSubTwoMap()
        {
            throw new NotImplementedException();
        }

        public string GetSubTwoMapLable()
        {
            throw new NotImplementedException();
        }

        public string GetSubTwoModel()
        {
            throw new NotImplementedException();
        }

        public string GetSubTwoServiceImpl()
        {
            throw new NotImplementedException();
        }

        public string GetSubTwoServiceInterFace()
        {
            throw new NotImplementedException();
        }

        public string GetSubTwoTestUnit()
        {
            throw new NotImplementedException();
        }

        public bool IsCheckOk()
        {
            return true;
        }

    }
}

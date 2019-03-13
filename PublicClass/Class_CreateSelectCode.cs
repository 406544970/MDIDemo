using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDIDemo.PublicClass
{
    /// <summary>
    /// 生成Select相关代码 2019-03-12
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
            if (class_SelectAllModel.class_Main.ResultType == 0)
            {
                Class_Tool class_ToolSpace = new Class_Tool();
                StringBuilder stringBuilder = new StringBuilder();
                Class_InterFaceDataBase class_InterFaceDataBase;
                if (class_SelectAllModel.class_Main.IsAddXmlHead)
                {
                    stringBuilder.Append("<?xml version=\"1.0\" encoding=\"UTF - 8\" ?>\r\n");
                    stringBuilder.Append("< !DOCTYPE mapper PUBLIC \"-//mybatis.org//DTD Mapper 3.0//EN\" \"http://mybatis.org/dtd/mybatis-3-mapper.dtd\" >\r\n");
                }
                stringBuilder.AppendFormat("<mapper namespace=\"{0}.dao.{1}Mapper\">\r\n"
                    , class_SelectAllModel.AllPackerName
                    , class_SelectAllModel.class_Main.NameSpace);
                stringBuilder.AppendFormat("{0}<resultMap id=\"{1}Map\" type=\"{3}.model.{2}\">\r\n"
                    , class_ToolSpace.GetSetSpaceCount(1)
                    , class_SelectAllModel.class_Main.ResultMapId
                    , class_SelectAllModel.class_Main.ResultMapType
                    , class_SelectAllModel.AllPackerName);
                switch (class_SelectAllModel.class_SelectDataBase.databaseType)
                {
                    case "MySql":
                        class_InterFaceDataBase = new Class_MySqlDataBase();
                        break;
                    case "SqlServer 2017":
                        class_InterFaceDataBase = new Class_SqlServer2017DataBase();
                        break;
                    default:
                        class_InterFaceDataBase = new Class_MySqlDataBase();
                        break;
                }
                foreach (Class_SelectAllModel.Class_Field class_Field in class_SelectAllModel.class_Main.class_Fields)
                {
                    if (class_Field.SelectSelect)
                    {
                        if (class_Field.FieldIsKey)
                        {
                            stringBuilder.AppendFormat("{0}<id column=\"{1}\" property=\"{2}\" jdbcType=\"{3}\"/><!--{4}-->\r\n"
                                , class_ToolSpace.GetSetSpaceCount(2)
                                , class_Field.FieldName
                                , class_Field.ParaName
                                , Class_Tool.GetJdbcType(class_InterFaceDataBase.GetJavaType(class_Field.ReturnType))
                                , class_Field.FieldRemark);
                        }
                        else
                        {
                            stringBuilder.AppendFormat("{0}<result column=\"{1}\" property=\"{2}\" jdbcType=\"{3}\"/><!--{4}-->\r\n"
                                , class_ToolSpace.GetSetSpaceCount(2)
                                , class_Field.FieldName
                                , class_Field.ParaName
                                , Class_Tool.GetJdbcType(class_InterFaceDataBase.GetJavaType(class_Field.ReturnType))
                                , class_Field.FieldRemark);
                        }
                    }
                }
                stringBuilder.AppendFormat("{0}</resultMap>\r\n", class_ToolSpace.GetSetSpaceCount(1));
                stringBuilder.Append("</mapper>\r\n");
                if (stringBuilder.Length > 0)
                    return stringBuilder.ToString();
                else
                    return null;
            }
            else
                return null;
        }
        private string GetSelectType(Class_SelectAllModel.Class_Main class_Main)
        {
            string ResultValue = null;
            int Index = 0;
            int Counter = 0;
            while (Index < class_Main.class_Fields.Count)
            {
                if (class_Main.class_Fields[Index].SelectSelect)
                {
                    if (Counter == 0)
                        ResultValue = class_Main.class_Fields[Index].ReturnType;
                    Counter++;
                }
                if (Counter > 1)
                    break;
                Index++;
            }
            if (Counter > 1)
                ResultValue = "mult";
            return ResultValue;
        }
        private string GetParameterType(Class_SelectAllModel.Class_Main class_Main)
        {
            string ResultValue = null;
            int Index = 0;
            int Counter = 0;
            while (Index < class_Main.class_Fields.Count)
            {
                if (class_Main.class_Fields[Index].WhereSelect)
                {
                    if (Counter == 0)
                        ResultValue = class_Main.class_Fields[Index].FieldType;
                    Counter++;
                }
                if (Counter > 1)
                    break;
                Index++;
            }
            if (Counter > 1)
                ResultValue = "mult";
            return ResultValue;
        }
        public string GetMainMapLable()
        {
            Class_Tool class_ToolSpace = new Class_Tool();
            StringBuilder stringBuilder = new StringBuilder();
            Class_InterFaceDataBase class_InterFaceDataBase;

            switch (class_SelectAllModel.class_SelectDataBase.databaseType)
            {
                case "MySql":
                    class_InterFaceDataBase = new Class_MySqlDataBase();
                    break;
                case "SqlServer 2017":
                    class_InterFaceDataBase = new Class_SqlServer2017DataBase();
                    break;
                default:
                    class_InterFaceDataBase = new Class_MySqlDataBase();
                    break;
            }
            stringBuilder.AppendFormat("{1}<!--注释：{0}-->\r\n", class_SelectAllModel.class_Main.MethodContent, class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0}<select id=\"{1}\" "
                , class_ToolSpace.GetSetSpaceCount(1)
                , class_SelectAllModel.class_Main.MethodId);
            if (class_SelectAllModel.class_Main.ResultType == 0)
            {
                stringBuilder.AppendFormat("resultMap=\"{0}Map\""
                    , class_SelectAllModel.class_Main.ResultMapId
                    , class_SelectAllModel.AllPackerName
                    , class_SelectAllModel.class_Main.ResultMapType);
            }
            else
            {
                string ResultType = GetSelectType(class_SelectAllModel.class_Main);
                if (ResultType == "mult")
                    stringBuilder.AppendFormat("resultType=\"{0}.model.{1}\""
                    , class_SelectAllModel.AllPackerName
                    , class_SelectAllModel.class_Main.ResultMapType);
                else
                    stringBuilder.AppendFormat("resultType=\"{0}\""
                    , class_InterFaceDataBase.GetJavaType(ResultType));
            }
            string FieldType = GetParameterType(class_SelectAllModel.class_Main);
            if (FieldType == "mult")
                stringBuilder.AppendFormat(" parameterType=\"{0}.{1}\">\r\n"
                , class_SelectAllModel.AllPackerName
                , class_SelectAllModel.class_Main.ResultMapType);
            else
                stringBuilder.AppendFormat(" parameterType=\"{0}\">\r\n"
                , class_InterFaceDataBase.GetJavaType(FieldType));

            stringBuilder.AppendFormat("{0}SELECT\r\n", class_ToolSpace.GetSetSpaceCount(2));
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
                        FieldName = class_CaseWhen.GetCaseWhenContent(class_Field.CaseWhen, FieldName, class_ToolSpace.GetSetSpaceCount(3));
                    }
                    if ((class_Field.FunctionName != null) && (class_Field.FunctionName.Length > 0))
                    {
                        FieldName = string.Format(class_Field.FunctionName.Replace("?", "{0}") + " AS {1}", FieldName, class_Field.ParaName);
                    }
                    if (!FieldName.Equals(class_Field.ParaName))
                    {
                        FieldName = string.Format(FieldName + " AS {0}", class_Field.ParaName);
                    }
                    if (Counter++ > 0)
                        stringBuilder.AppendFormat("{1},{0}<!--{2}-->\r\n", FieldName, class_ToolSpace.GetSetSpaceCount(3), class_Field.FieldRemark);
                    else
                        stringBuilder.AppendFormat("{1}{0}<!--{2}-->\r\n", FieldName, class_ToolSpace.GetSetSpaceCount(3), class_Field.FieldRemark);
                }
                #endregion
            }
            stringBuilder.AppendFormat("{0}FROM {1}\r\n", class_ToolSpace.GetSetSpaceCount(2), class_SelectAllModel.class_Main.TableName);
            stringBuilder.Append(GetMainWhereLable());
            stringBuilder.AppendFormat("{0}</select>\r\n", class_ToolSpace.GetSetSpaceCount(1));
            if (stringBuilder.Length > 0)
                return stringBuilder.ToString();
            else
                return null;
        }

        public string GetMainWhereLable()
        {
            bool HaveGroup = false;
            bool HaveHaving = false;
            List<Class_OrderBy> class_OrderBies = new List<Class_OrderBy>();
            Class_Tool class_ToolSpace = new Class_Tool();
            StringBuilder stringBuilderWhereAnd = new StringBuilder();
            StringBuilder stringBuilderWhereOr = new StringBuilder();
            StringBuilder stringBuilderGroup = new StringBuilder();
            StringBuilder stringBuilderHaving = new StringBuilder();
            StringBuilder stringBuilderOrder = new StringBuilder();
            Class_InterFaceDataBase class_InterFaceDataBase;
            switch (class_SelectAllModel.class_SelectDataBase.databaseType)
            {
                case "MySql":
                    class_InterFaceDataBase = new Class_MySqlDataBase();
                    break;
                case "SqlServer 2017":
                    class_InterFaceDataBase = new Class_SqlServer2017DataBase();
                    break;
                default:
                    class_InterFaceDataBase = new Class_MySqlDataBase();
                    break;
            }
            foreach (Class_SelectAllModel.Class_Field class_Field in class_SelectAllModel.class_Main.class_Fields)
            {
                #region Where
                if (class_Field.WhereSelect)
                {
                    string FieldName = class_Field.FieldName;
                    string IfLabel = null;
                    string NowWhere = null;
                    if (class_Field.WhereType == "AND")
                    {
                        if (class_Field.WhereIsNull)
                        {
                            IfLabel = string.Format("{1}<if test=\"{0} != null\">\r\n"
                                , class_Field.ParaName, class_ToolSpace.GetSetSpaceCount(3));
                        }
                    }
                    if (class_Field.WhereType == "OR")
                    {
                        IfLabel = string.Format("{1}<when test=\"{0} != null\">\r\n"
                            , class_Field.ParaName, class_ToolSpace.GetSetSpaceCount(4));
                    }
                    NowWhere = string.Format("{0} AND {1} "
                        , class_ToolSpace.GetSetSpaceCount(class_Field.WhereType == "AND" ? 4 : 5)
                        , FieldName
                        , class_Field.WhereType);
                    if (class_Field.LogType.IndexOf("IN") > -1)
                    {
                        //                      <foreach item = "item" index = "index" collection = "list" open = "(" separator = "," close = ")" >
                        //          #{item}
                        //</foreach>
                        NowWhere += string.Format("{2}\r\n{0}<foreach item = \"item\" index = \"index\" collection = \"{1}\" open = \"(\" separator = \", \" close = \")\" >\r\n"
                            , class_ToolSpace.GetSetSpaceCount(class_Field.WhereType == "AND" ? 4 : 5)
                            , FieldName
                            , class_Field.LogType);
                        NowWhere += class_ToolSpace.GetSetSpaceCount(class_Field.WhereType == "AND" ? 5 : 6) + "#{item}\r\n";
                        NowWhere += string.Format("{0}</foreach>\r\n", class_ToolSpace.GetSetSpaceCount(class_Field.WhereType == "AND" ? 4 : 5));
                        if (class_Field.WhereIsNull)
                        {
                            NowWhere += string.Format("{0}</if>\r\n", class_ToolSpace.GetSetSpaceCount(3));
                        }
                    }
                    else
                    {
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
                            NowWhere = string.Format("{0}<![CDATA[{1}]]>\r\n", class_ToolSpace.GetSetSpaceCount(5), NowWhere.Trim());
                        else
                            NowWhere += "\r\n";
                        if (class_Field.WhereType == "AND")
                        {
                            if (class_Field.WhereIsNull)
                            {
                                NowWhere += string.Format("{0}</if>\r\n", class_ToolSpace.GetSetSpaceCount(3));
                            }
                        }
                        if (class_Field.WhereType == "OR")
                        {
                            NowWhere += string.Format("{0}</when>\r\n", class_ToolSpace.GetSetSpaceCount(4));
                        }
                    }
                    if (class_Field.WhereType == "AND")
                        stringBuilderWhereAnd.Append(IfLabel + NowWhere);
                    if (class_Field.WhereType == "OR")
                        stringBuilderWhereOr.Append(IfLabel + NowWhere);
                }
                #endregion
                #region Group
                if (class_Field.GroupSelect)
                {
                    if (!HaveGroup)
                    {
                        HaveGroup = true;
                    }
                    else
                    {
                        stringBuilderGroup.Append(",");
                    }
                    stringBuilderGroup.AppendFormat("{0}", class_Field.FieldName);
                }
                #endregion
                #region Having
                if (class_Field.HavingSelect)
                {
                    if (!HaveHaving)
                    {
                        HaveHaving = true;
                    }
                    else
                    {
                        stringBuilderHaving.Append(",");
                    }
                    stringBuilderHaving.AppendFormat("{0} {1} {2}"
                        , class_Field.HavingFunction.Replace("?", class_Field.FieldName)
                        , class_Field.HavingCondition
                        , class_Field.HavingValue);
                }
                #endregion
                #region Order
                if (class_Field.OrderSelect)
                {
                    Class_OrderBy class_OrderBy = new Class_OrderBy();
                    class_OrderBy.FieldName = class_Field.FieldName;
                    class_OrderBy.SortNo = class_Field.SortNo;
                    class_OrderBy.SortType = class_Field.SortType == "升序" ? "" : " DESC";
                    class_OrderBies.Add(class_OrderBy);
                }
                #endregion
            }
            if (stringBuilderWhereOr.Length > 0)
            {
                stringBuilderWhereAnd.AppendFormat("{0}<choose>\r\n", class_ToolSpace.GetSetSpaceCount(3));
                stringBuilderWhereAnd.Append(stringBuilderWhereOr.ToString());
                stringBuilderWhereAnd.AppendFormat("{0}</choose>\r\n", class_ToolSpace.GetSetSpaceCount(3));
            }
            if (stringBuilderWhereAnd.Length > 0)
            {
                stringBuilderWhereAnd.Insert(0, string.Format("{0}<where>\r\n", class_ToolSpace.GetSetSpaceCount(2)));
                stringBuilderWhereAnd.AppendFormat("{0}</where>\r\n", class_ToolSpace.GetSetSpaceCount(2));
            }
            if (stringBuilderGroup.Length > 0)
            {
                stringBuilderWhereAnd.AppendFormat("{0}GROUP BY ", class_ToolSpace.GetSetSpaceCount(2));
                stringBuilderWhereAnd.Append(stringBuilderGroup.ToString() + "\r\n");
            }
            if (stringBuilderHaving.Length > 0)
            {
                stringBuilderWhereAnd.AppendFormat("{0}HAVING ", class_ToolSpace.GetSetSpaceCount(2));
                if ((stringBuilderHaving.ToString().IndexOf("<") > -1) || (stringBuilderHaving.ToString().IndexOf(">") > -1) || (stringBuilderHaving.ToString().IndexOf("&") > -1))
                    stringBuilderWhereAnd.AppendFormat("<![CDATA[{0}]]>\r\n", stringBuilderHaving.ToString());
                else
                    stringBuilderWhereAnd.Append(stringBuilderHaving.ToString() + "\r\n");
            }
            class_OrderBies = class_OrderBies.OrderBy(a => a.SortNo).ToList();
            foreach (Class_OrderBy row in class_OrderBies)
            {
                stringBuilderOrder.AppendFormat(",{0}{1}", row.FieldName, row.SortType);
            }
            if (class_OrderBies.Count > 0)
            {
                stringBuilderWhereAnd.AppendFormat("{0}ORDER BY ", class_ToolSpace.GetSetSpaceCount(2));
                stringBuilderWhereAnd.Append(stringBuilderOrder.ToString().Substring(1) + "\r\n");
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

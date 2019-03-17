﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MDIDemo.PublicClass.Class_SelectAllModel;

namespace MDIDemo.PublicClass
{
    /// <summary>
    /// 生成Select相关代码 2019-03-12
    /// </summary>
    public class Class_CreateSelectCode : IClass_InterFaceCreateCode
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
        private string _GetMainControl(Class_Main class_Main)
        {
            List<Class_WhereField> class_WhereFields = new List<Class_WhereField>();
            Class_Tool class_ToolSpace = new Class_Tool();
            StringBuilder stringBuilder = new StringBuilder();
            IClass_InterFaceDataBase class_InterFaceDataBase;
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
            stringBuilder.Append("/**\r\n");
            stringBuilder.AppendFormat(" * @author {0}\r\n", Class_UseInfo.UserName);
            stringBuilder.AppendFormat(" * @create {0}\r\n", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            stringBuilder.Append(" */\r\n");
            stringBuilder.Append("@RestController\r\n");
            stringBuilder.AppendFormat("@RequestMapping(\"/{0}\")\r\n", class_SelectAllModel.class_Create.MicroServiceName);
            stringBuilder.Append(string.Format("public class {0}Controller ", class_Main.NameSpace) + "{\r\n");

            stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(1) + "@Value(\"${server.port}\"\r\n");
            stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(1) + "String myPort;\r\n\r\n");

            stringBuilder.AppendFormat("{0}@Autowired\r\n", class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0}{1}Service {2}Service;\r\n"
                , class_ToolSpace.GetSetSpaceCount(1)
                , class_Main.NameSpace
                , Class_Tool.GetFirstCodeLow(class_Main.NameSpace));

            stringBuilder.Append("\r\n");
            stringBuilder.AppendFormat("{0}/**\r\n", class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0} * {1}\r\n", class_ToolSpace.GetSetSpaceCount(1)
                , class_Main.MethodContent);
            class_WhereFields = _GetParameterType(class_Main);
            if (class_WhereFields != null)
            {
                foreach (Class_WhereField row in class_WhereFields)
                {
                    if (class_WhereFields.Count > 0)
                        stringBuilder.AppendFormat("{0} * @param {1} {2}\r\n"
                    , class_ToolSpace.GetSetSpaceCount(1)
                    , Class_Tool.GetFirstCodeLow(row.FieldName)
                    , row.FieldRemark);
                }
            }
            stringBuilder.AppendFormat("{0} * @return {1}\r\n", class_ToolSpace.GetSetSpaceCount(1)
                , class_Main.ServiceInterFaceReturnRemark);
            stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));

            stringBuilder.AppendFormat("{0}@{1}Mapping(\"/{2}\")\r\n"
                , class_ToolSpace.GetSetSpaceCount(1)
                , class_SelectAllModel.class_Create.HttpRequestType
                , class_Main.MethodId);
            stringBuilder.AppendFormat("{0}public ", class_ToolSpace.GetSetSpaceCount(1));

            if (class_Main.ServiceInterFaceReturnCount == 0)
                stringBuilder.AppendFormat("{0}", _GetServiceReturnType(class_Main, false));
            else
                stringBuilder.AppendFormat("List<{0}>", _GetServiceReturnType(class_Main, false));
            stringBuilder.AppendFormat(" {0}", class_Main.MethodId);
            stringBuilder.Append("(");
            int Index = 0;
            foreach (Class_WhereField row in class_WhereFields)
            {
                if (Index++ > 0)
                    stringBuilder.AppendFormat("\r\n{1}, @RequestParam(value = \"{0}\""
                    , row.FieldName
                    , class_ToolSpace.GetSetSpaceCount(3));
                else
                    stringBuilder.AppendFormat("@RequestParam(value = \"{0}\""
                    , row.FieldName);
                if ((row.FieldDefaultValue != null) && (row.FieldDefaultValue.Length > 0))
                    stringBuilder.AppendFormat(", defaultValue = \"{0}\"", row.FieldDefaultValue);
                stringBuilder.AppendFormat(") {0} {1}"
                    , Class_Tool.GetSimplificationJavaType(class_InterFaceDataBase.GetJavaType(row.FieldType))
                    , row.FieldName);
            }
            stringBuilder.Append(") {\r\n");
            if (class_WhereFields != null)
            {
                if (class_WhereFields.Count > 1)
                {
                    stringBuilder.AppendFormat("{0}{1} {2} = new {1}();\r\n"
                    , class_ToolSpace.GetSetSpaceCount(2)
                    , class_Main.NameSpace
                    , Class_Tool.GetFirstCodeLow(class_Main.NameSpace));
                    foreach (Class_WhereField row in class_WhereFields)
                    {
                        stringBuilder.AppendFormat("{0}{1}.set{2}({3});\r\n"
                        , class_ToolSpace.GetSetSpaceCount(2)
                        , Class_Tool.GetFirstCodeLow(class_Main.NameSpace)
                        , Class_Tool.GetFirstCodeUpper(row.FieldName)
                        , row.FieldName);
                    }
                }
            }
            stringBuilder.Append("\r\n//      请在这里写逻辑代码\r\n");
            stringBuilder.AppendFormat("\r\n{0}return {1}Service."
                , class_ToolSpace.GetSetSpaceCount(2)
                , Class_Tool.GetFirstCodeLow(class_Main.NameSpace));

            if (class_WhereFields != null)
            {
                if (class_WhereFields.Count > 1)
                    stringBuilder.AppendFormat("{0}({1});\r\n"
                        , class_Main.MethodId
                        , Class_Tool.GetFirstCodeLow(class_Main.NameSpace));
                else if (class_WhereFields.Count == 1)
                {
                    stringBuilder.AppendFormat("{0}({1});\r\n"
                        , class_Main.MethodId
                        , class_WhereFields[0].FieldName);
                }
                else
                    stringBuilder.AppendFormat("{0}();\r\n"
                        , class_Main.MethodId);
            }


            stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(1) + "}\r\n");

            stringBuilder.Append("\r\n");
            stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(1) + "@PostMapping(value = \"/myPort\")\r\n");
            stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(1) + "public String myPort(){\r\n");
            stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(2) + "return \"myPort: \" + this.myPort;\r\n");
            stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(1) + "}\r\n");
            stringBuilder.Append("\r\n");
            stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(1) + "@GetMapping(value = \"/downLine\")\r\n");
            stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(1) + "public void downLine(){\r\n");
            stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(2) + "getInstance().shutdownComponent();\r\n");
            stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(1) + "}\r\n");

            stringBuilder.Append("}\r\n");

            return stringBuilder.ToString();

        }
        private string _GetSelectType(Class_Main class_Main)
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
        private List<Class_WhereField> _GetParameterType(Class_Main class_Main)
        {
            List<Class_WhereField> class_WhereFields = new List<Class_WhereField>();
            foreach (Class_Field row in class_Main.class_Fields)
            {
                if (row.WhereSelect)
                {
                    Class_WhereField class_WhereField = new Class_WhereField()
                    {
                        FieldName = row.ParaName,
                        FieldRemark = row.FieldRemark,
                        FieldType = row.FieldType,
                        FieldDefaultValue = row.FieldDefaultValue
                    };
                    class_WhereFields.Add(class_WhereField);
                }
            }
            return class_WhereFields;
        }
        private string _GetServiceReturnType(Class_Main class_Main, bool HavePackageName = true)
        {
            StringBuilder stringBuilder = new StringBuilder();
            IClass_InterFaceDataBase class_InterFaceDataBase;
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
            string ResultType = _GetSelectType(class_Main);
            if (ResultType == "mult")
            {
                if (HavePackageName)
                    stringBuilder.AppendFormat("{0}.model.{1}"
                    , class_SelectAllModel.AllPackerName
                    , class_SelectAllModel.class_Main.ResultMapType);
                else
                    stringBuilder.Append(class_SelectAllModel.class_Main.ResultMapType);
            }
            else
                stringBuilder.AppendFormat("{0}"
                , Class_Tool.GetSimplificationJavaType(class_InterFaceDataBase.GetJavaType(ResultType)));
            return stringBuilder.ToString();
        }
        private string _GetMainWhereLable()
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
            IClass_InterFaceDataBase class_InterFaceDataBase;
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
                        if (class_Field.WhereValue == "参数")
                        {
                            if ((LikeType < -99) && (class_Field.LogType.IndexOf("NULL") == -1))
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
                                        if ((LikeType < -99) && (class_Field.LogType.IndexOf("NULL") == -1))
                                            NowWhere = NowWhere + "%#{" + string.Format("{0},jdbcType={1}"
                                        , class_Field.ParaName
                                        , Class_Tool.GetJdbcType(class_InterFaceDataBase.GetJavaType(class_Field.FieldType))) + "}";
                                        break;
                                }
                            }
                        }
                        else
                        {
                            if (class_InterFaceDataBase.IsAddPoint(class_Field.FieldType))
                                NowWhere = NowWhere + string.Format("'{0}'", class_Field.WhereValue);
                            else
                                NowWhere = NowWhere + string.Format("{0}", class_Field.WhereValue);
                        }
                        if ((class_Field.LogType.IndexOf("<") > -1) || (class_Field.LogType.IndexOf(">") > -1) || (class_Field.LogType.IndexOf("&") > -1))
                            NowWhere = string.Format("{0}<![CDATA[{1}]]>\r\n", class_ToolSpace.GetSetSpaceCount(4), NowWhere.Trim());
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
        private string _GetMainServiceImpl(Class_Main class_Main)
        {
            List<Class_WhereField> class_WhereFields = new List<Class_WhereField>();
            Class_Tool class_ToolSpace = new Class_Tool();
            StringBuilder stringBuilder = new StringBuilder();
            IClass_InterFaceDataBase class_InterFaceDataBase;
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
            stringBuilder.Append("/**\r\n");
            stringBuilder.AppendFormat(" * @author {0}\r\n", Class_UseInfo.UserName);
            stringBuilder.AppendFormat(" * @create {0}\r\n", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            stringBuilder.Append(" */\r\n");
            stringBuilder.Append("@SuppressWarnings(\"SpringJavaInjectionPointsAutowiringInspection\")\r\n@Service\r\n");
            stringBuilder.AppendFormat("public class {0}ServiceImpl implements {0}Service", class_Main.NameSpace);
            stringBuilder.Append(" {\r\n");

            stringBuilder.AppendFormat("{0}@Autowired\r\n", class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0}{1}Mapper {2}Dao;\r\n", class_ToolSpace.GetSetSpaceCount(1)
            , class_Main.NameSpace
            , Class_Tool.GetFirstCodeLow(class_Main.NameSpace));
            stringBuilder.AppendFormat("{0}/**\r\n", class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0} * {1}\r\n", class_ToolSpace.GetSetSpaceCount(1)
                , class_Main.MethodContent);
            class_WhereFields = _GetParameterType(class_Main);
            if (class_WhereFields != null)
            {
                if (class_WhereFields.Count > 1)
                {
                    stringBuilder.AppendFormat("{0} * @param {1} {2}\r\n"
                    , class_ToolSpace.GetSetSpaceCount(1)
                    , Class_Tool.GetFirstCodeLow(class_Main.NameSpace)
                    , class_Main.MethodContent);
                }
                else
                {
                    if (class_WhereFields.Count > 0)
                        stringBuilder.AppendFormat("{0} * @param {1} {2}\r\n"
                    , class_ToolSpace.GetSetSpaceCount(1)
                    , Class_Tool.GetFirstCodeLow(class_WhereFields[0].FieldName)
                    , class_WhereFields[0].FieldRemark);
                }
            }
            stringBuilder.AppendFormat("{0} * @return {1}\r\n", class_ToolSpace.GetSetSpaceCount(1)
                , class_Main.ServiceInterFaceReturnRemark);
            stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0}@Override\r\n{0}public ", class_ToolSpace.GetSetSpaceCount(1));
            if (class_Main.ServiceInterFaceReturnCount == 0)
                stringBuilder.AppendFormat("{0}", _GetServiceReturnType(class_Main, false));
            else
                stringBuilder.AppendFormat("List<{0}>", _GetServiceReturnType(class_Main, false));
            if (class_WhereFields != null)
            {
                if (class_WhereFields.Count > 1)
                    stringBuilder.AppendFormat(" {0}({1} {2})"
                        , class_Main.MethodId
                        , class_Main.NameSpace
                        , Class_Tool.GetFirstCodeLow(class_Main.NameSpace));
                else if (class_WhereFields.Count == 1)
                {
                    stringBuilder.AppendFormat(" {0}({1} {2})"
                        , class_Main.MethodId
                        , Class_Tool.GetClosedJavaType(class_InterFaceDataBase.GetJavaType(class_WhereFields[0].FieldType))
                        , class_WhereFields[0].FieldName);
                }
                else
                    stringBuilder.AppendFormat(" {0}()"
                        , class_Main.MethodId);
                stringBuilder.Append(" {\r\n");
            }
            stringBuilder.AppendFormat("{0}return {1}Dao."
    , class_ToolSpace.GetSetSpaceCount(2)
    , Class_Tool.GetFirstCodeLow(class_Main.NameSpace));

            if (class_WhereFields != null)
            {
                if (class_WhereFields.Count > 1)
                    stringBuilder.AppendFormat("{0}({1});\r\n"
                        , class_Main.MethodId
                        , Class_Tool.GetFirstCodeLow(class_Main.NameSpace));
                else if (class_WhereFields.Count == 1)
                {
                    stringBuilder.AppendFormat("{0}({1});\r\n"
                        , class_Main.MethodId
                        , class_WhereFields[0].FieldName);
                }
                else
                    stringBuilder.AppendFormat("{0}();\r\n"
                        , class_Main.MethodId);
            }

            stringBuilder.AppendFormat("{0}", class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.Append("}\r\n");


            stringBuilder.Append("}\r\n");
            return stringBuilder.ToString();
        }
        private string _GetMainDAO(Class_Main class_Main)
        {
            List<Class_WhereField> class_WhereFields = new List<Class_WhereField>();
            Class_Tool class_ToolSpace = new Class_Tool();
            StringBuilder stringBuilder = new StringBuilder();
            IClass_InterFaceDataBase class_InterFaceDataBase;
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
            stringBuilder.Append("/**\r\n");
            stringBuilder.AppendFormat(" * @author {0}\r\n", Class_UseInfo.UserName);
            stringBuilder.AppendFormat(" * @create {0}\r\n", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            stringBuilder.Append(" */\r\n");
            stringBuilder.Append("@Mapper\r\n");
            stringBuilder.Append(string.Format("public interface {0}Mapper ", class_Main.NameSpace) + "{\r\n");

            stringBuilder.AppendFormat("{0}/**\r\n", class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0} * {1}\r\n", class_ToolSpace.GetSetSpaceCount(1)
                , class_Main.MethodContent);
            class_WhereFields = _GetParameterType(class_Main);
            if (class_WhereFields != null)
            {
                if (class_WhereFields.Count > 1)
                {
                    stringBuilder.AppendFormat("{0} * @param {1} {2}\r\n"
                    , class_ToolSpace.GetSetSpaceCount(1)
                    , Class_Tool.GetFirstCodeLow(class_Main.NameSpace)
                    , class_Main.MethodContent);
                }
                else
                {
                    if (class_WhereFields.Count > 0)
                        stringBuilder.AppendFormat("{0} * @param {1} {2}\r\n"
                    , class_ToolSpace.GetSetSpaceCount(1)
                    , Class_Tool.GetFirstCodeLow(class_WhereFields[0].FieldName)
                    , class_WhereFields[0].FieldRemark);
                }
            }
            stringBuilder.AppendFormat("{0} * @return {1}\r\n", class_ToolSpace.GetSetSpaceCount(1)
                , class_Main.ServiceInterFaceReturnRemark);
            stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));

            if (class_Main.ServiceInterFaceReturnCount == 0)
                stringBuilder.AppendFormat("{0}{1}", class_ToolSpace.GetSetSpaceCount(1)
                    , _GetServiceReturnType(class_Main, false));
            else
                stringBuilder.AppendFormat("{0}List<{1}>", class_ToolSpace.GetSetSpaceCount(1)
                    , _GetServiceReturnType(class_Main, false));
            if (class_WhereFields != null)
            {
                if (class_WhereFields.Count > 1)
                    stringBuilder.AppendFormat(" {0}({1} {2});\r\n"
                        , class_Main.MethodId
                        , class_Main.NameSpace
                        , Class_Tool.GetFirstCodeLow(class_Main.NameSpace));
                else if (class_WhereFields.Count == 1)
                {
                    stringBuilder.AppendFormat(" {0}({1} {2});\r\n"
                        , class_Main.MethodId
                        , Class_Tool.GetClosedJavaType(class_InterFaceDataBase.GetJavaType(class_WhereFields[0].FieldType))
                        , class_WhereFields[0].FieldName);
                }
                else
                    stringBuilder.AppendFormat(" {0}();\r\n"
                        , class_Main.MethodId);
            }
            stringBuilder.Append("}\r\n");

            return stringBuilder.ToString();
        }

        public string GetMainControl()
        {
            return _GetMainControl(class_SelectAllModel.class_Main);
        }
        public string GetMainDAO()
        {
            return _GetMainDAO(class_SelectAllModel.class_Main);
        }

        /// <summary>
        /// 得到Map
        /// </summary>
        /// <returns></returns>
        public string GetMainMap()
        {
            if (class_SelectAllModel.class_Main.ResultType == 0)
            {
                Class_Tool class_ToolSpace = new Class_Tool();
                StringBuilder stringBuilder = new StringBuilder();
                IClass_InterFaceDataBase class_InterFaceDataBase;
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
                foreach (Class_Field class_Field in class_SelectAllModel.class_Main.class_Fields)
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
        /// <summary>
        /// 得到MapXml
        /// </summary>
        /// <returns></returns>
        public string GetMainMapLable()
        {
            List<Class_WhereField> class_WhereFields = new List<Class_WhereField>();
            Class_Tool class_ToolSpace = new Class_Tool();
            StringBuilder stringBuilder = new StringBuilder();
            IClass_InterFaceDataBase class_InterFaceDataBase;

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
                string ResultType = _GetSelectType(class_SelectAllModel.class_Main);
                if (ResultType == "mult")
                    stringBuilder.AppendFormat("resultType=\"{0}.model.{1}\""
                    , class_SelectAllModel.AllPackerName
                    , class_SelectAllModel.class_Main.ResultMapType);
                else
                    stringBuilder.AppendFormat("resultType=\"{0}\""
                    , class_InterFaceDataBase.GetJavaType(ResultType));
            }
            class_WhereFields = _GetParameterType(class_SelectAllModel.class_Main);
            if (class_WhereFields != null)
            {
                if (class_WhereFields.Count > 1)
                    stringBuilder.AppendFormat(" parameterType=\"{0}.{1}\">\r\n"
                    , class_SelectAllModel.AllPackerName
                    , class_SelectAllModel.class_Main.ResultMapType);
                else if (class_WhereFields.Count == 1)
                    stringBuilder.AppendFormat(" parameterType=\"{0}\">\r\n"
                    , class_InterFaceDataBase.GetJavaType(class_WhereFields[0].FieldType));
                else
                    stringBuilder.Append(" >\r\n");
            }
            else
                stringBuilder.Append(" >\r\n");

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
            stringBuilder.Append(_GetMainWhereLable());
            stringBuilder.AppendFormat("{0}</select>\r\n", class_ToolSpace.GetSetSpaceCount(1));
            if (stringBuilder.Length > 0)
                return stringBuilder.ToString();
            else
                return null;
        }

        public string GetMainModel()
        {
            Class_Tool class_ToolSpace = new Class_Tool();
            StringBuilder stringBuilder = new StringBuilder();
            IClass_InterFaceDataBase class_InterFaceDataBase;
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
            stringBuilder.Append("/**\r\n");
            stringBuilder.AppendFormat(" * @author {0}\r\n", Class_UseInfo.UserName);
            stringBuilder.AppendFormat(" * @create {0}\r\n", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            stringBuilder.Append(" */\r\n");
            stringBuilder.AppendFormat("public class {0} implements Serializable ", class_SelectAllModel.class_Main.NameSpace);
            stringBuilder.Append(" {\r\n");
            //加入字段
            foreach (Class_Field row in class_SelectAllModel.class_Main.class_Fields)
            {
                if (row.SelectSelect)
                {
                    stringBuilder.AppendFormat("{0}/**\r\n", class_ToolSpace.GetSetSpaceCount(1));
                    stringBuilder.AppendFormat("{0} * {1}\r\n", class_ToolSpace.GetSetSpaceCount(1), row.FieldRemark);
                    stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));
                    stringBuilder.AppendFormat("{0}private {1} {2};\r\n"
                        , class_ToolSpace.GetSetSpaceCount(1)
                        , Class_Tool.GetClosedJavaType(class_InterFaceDataBase.GetJavaType(row.ReturnType))
                        , row.ParaName);
                }
            }

            stringBuilder.Append("}\r\n");

            return stringBuilder.ToString();
        }

        public string GetMainServiceImpl()
        {
            return _GetMainServiceImpl(class_SelectAllModel.class_Main);
        }
        public string _GetMainServiceInterFace(Class_Main class_Main)
        {
            List<Class_WhereField> class_WhereFields = new List<Class_WhereField>();
            Class_Tool class_ToolSpace = new Class_Tool();
            StringBuilder stringBuilder = new StringBuilder();
            IClass_InterFaceDataBase class_InterFaceDataBase;
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
            stringBuilder.Append("/**\r\n");
            stringBuilder.AppendFormat(" * @author {0}\r\n", Class_UseInfo.UserName);
            stringBuilder.AppendFormat(" * @create {0}\r\n", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            stringBuilder.Append(" */\r\n");
            stringBuilder.Append(string.Format("public interface {0}Service ", class_Main.NameSpace) + "{\r\n");

            stringBuilder.AppendFormat("{0}/**\r\n", class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0} * {1}\r\n", class_ToolSpace.GetSetSpaceCount(1)
                , class_Main.MethodContent);
            class_WhereFields = _GetParameterType(class_Main);
            if (class_WhereFields != null)
            {
                if (class_WhereFields.Count > 1)
                {
                    stringBuilder.AppendFormat("{0} * @param {1} {2}\r\n"
                    , class_ToolSpace.GetSetSpaceCount(1)
                    , Class_Tool.GetFirstCodeLow(class_Main.NameSpace)
                    , class_Main.MethodContent);
                }
                else
                {
                    if (class_WhereFields.Count > 0)
                        stringBuilder.AppendFormat("{0} * @param {1} {2}\r\n"
                    , class_ToolSpace.GetSetSpaceCount(1)
                    , Class_Tool.GetFirstCodeLow(class_WhereFields[0].FieldName)
                    , class_WhereFields[0].FieldRemark);
                }
            }
            stringBuilder.AppendFormat("{0} * @return {1}\r\n", class_ToolSpace.GetSetSpaceCount(1)
                , class_Main.ServiceInterFaceReturnRemark);
            stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));

            if (class_Main.ServiceInterFaceReturnCount == 0)
                stringBuilder.AppendFormat("{0}{1}", class_ToolSpace.GetSetSpaceCount(1)
                    , _GetServiceReturnType(class_Main, false));
            else
                stringBuilder.AppendFormat("{0}List<{1}>", class_ToolSpace.GetSetSpaceCount(1)
                    , _GetServiceReturnType(class_Main, false));
            if (class_WhereFields != null)
            {
                if (class_WhereFields.Count > 1)
                    stringBuilder.AppendFormat(" {0}({1} {2});\r\n"
                        , class_Main.MethodId
                        , class_Main.NameSpace
                        , Class_Tool.GetFirstCodeLow(class_Main.NameSpace));
                else if (class_WhereFields.Count == 1)
                {
                    stringBuilder.AppendFormat(" {0}({1} {2});\r\n"
                        , class_Main.MethodId
                        , Class_Tool.GetClosedJavaType(class_InterFaceDataBase.GetJavaType(class_WhereFields[0].FieldType))
                        , class_WhereFields[0].FieldName);
                }
                else
                    stringBuilder.AppendFormat(" {0}();\r\n"
                        , class_Main.MethodId);
            }
            stringBuilder.Append("}\r\n");
            return stringBuilder.ToString();
        }
        public string GetMainServiceInterFace()
        {
            return _GetMainServiceInterFace(class_SelectAllModel.class_Main);
        }

        public bool IsCheckOk()
        {
            return true;
        }
        public string GetMainDTO()
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


    }
}

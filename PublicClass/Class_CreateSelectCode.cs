using System;
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
        private static string InPutParamer = "InPutPara";
        #region 私有
        private int AddLinkFieldInfo()
        {
            return class_SelectAllModel.AddLinkFieldInfo();
        }
        private void ChangeCheck(int CurTableNo)
        {
            List<Class_LinkFieldInfo> class_LinkFieldInfoChecks = new List<Class_LinkFieldInfo>();
            class_LinkFieldInfoChecks = class_SelectAllModel.GetClass_LinkFieldInfos().FindAll(a => a.TableNo.Equals(CurTableNo));
            if (class_LinkFieldInfoChecks != null && class_LinkFieldInfoChecks.Count > 0)
            {
                foreach (Class_LinkFieldInfo item in class_LinkFieldInfoChecks)
                {
                    item.CheckOk = true;
                    ChangeCheck(item.CurTableNo);
                }
            }
            class_LinkFieldInfoChecks.Clear();
        }
        private bool CheckClassLinkField(ref List<string> outMessage)
        {
            bool ReturnValue = true;
            if (class_SelectAllModel.GetLinkFieldInfosCount() > 0)
            {
                ChangeCheck(-1);
                foreach (Class_LinkFieldInfo item in class_SelectAllModel.GetClass_LinkFieldInfos())
                {
                    ReturnValue = ReturnValue && item.CheckOk;
                }
            }
            return ReturnValue;
        }
        private string _GetAuthor()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat(" * @author ：{0}", class_SelectAllModel.class_Create.CreateMan);
            if (class_SelectAllModel.class_Create.CreateDo != null && class_SelectAllModel.class_Create.CreateDo.Length > 0)
            {
                stringBuilder.AppendFormat("，后端工程师：{0}", class_SelectAllModel.class_Create.CreateDo);
            }
            if (class_SelectAllModel.class_Create.CreateFrontDo != null && class_SelectAllModel.class_Create.CreateFrontDo.Length > 0)
            {
                stringBuilder.AppendFormat("，前端工程师：{0}", class_SelectAllModel.class_Create.CreateFrontDo);
            }
            stringBuilder.AppendFormat("\r\n");
            return stringBuilder.ToString();
        }
        private List<Class_EnglishField> _GetEnglishFieldList(Class_Sub class_Main)
        {
            List<Class_EnglishField> class_EnglishFields = new List<Class_EnglishField>();
            if (class_SelectAllModel.class_Create.EnglishSign)
            {
                foreach (Class_Field row in class_Main.class_Fields)
                {
                    if (row.SelectSelect)
                    {
                        if (Class_Tool.IsEnglishField(row.ParaName))
                        {
                            Class_EnglishField class_EnglishField = new Class_EnglishField();
                            class_EnglishField.FieldEnglishName = row.ParaName;
                            class_EnglishField.FieldChinaName = Class_Tool.GetChinaField(row.ParaName);
                            class_EnglishFields.Add(class_EnglishField);
                        }
                    }
                }
            }
            return class_EnglishFields;
        }
        private Class_SelectAllModel class_SelectAllModel;
        private string _GetSelectType(Class_Sub class_Main)
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
        private List<Class_WhereField> _GetParameterType()
        {
            List<Class_WhereField> class_WhereFields = new List<Class_WhereField>();
            int Counter = 0;
            foreach (Class_Sub item in class_SelectAllModel.class_SubList)
            {
                if (item != null)
                {
                    foreach (Class_Field class_Field in item.class_Fields)
                    {
                        if (class_Field.WhereSelect && class_Field.WhereValue == "参数" && class_Field.LogType.IndexOf("NULL") < 0)
                        {
                            string InParaFieldName = null;
                            bool IsSame = class_SelectAllModel.GetHaveSameFieldName(class_Field.ParaName, Counter);
                            if (IsSame)
                                InParaFieldName = class_Field.MultFieldName;
                            else
                                InParaFieldName = class_Field.ParaName;
                            Class_WhereField class_WhereField = new Class_WhereField()
                            {
                                FieldName = class_Field.FieldName,
                                ParaName = class_Field.ParaName,
                                FieldRemark = class_Field.FieldRemark,
                                LogType = class_Field.ReturnType,
                                FieldDefaultValue = class_Field.FieldDefaultValue,
                                FieldLogType = class_Field.LogType,
                                TableNo = Counter,
                                IsSame = IsSame,
                                OutFieldName = InParaFieldName,
                                TableName = item.TableName
                            };
                            class_WhereFields.Add(class_WhereField);
                        }
                    }
                }
                Counter++;
            }
            return class_WhereFields;
        }
        private string _GetServiceReturnType(Class_Sub class_Main, bool HavePackageName = true)
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
                    , class_Main.ResultMapType);
                else
                    stringBuilder.Append(class_Main.ResultMapType);
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
            int CurPageIndex = 0;
            foreach (Class_Sub item in class_SelectAllModel.class_SubList)
            {
                string AliasName = item.AliasName;
                foreach (Class_Field class_Field in item.class_Fields)
                {
                    string FieldName = class_Field.FieldName;
                    string InParaFieldName = null;
                    if (class_SelectAllModel.IsMultTable)
                    {
                        FieldName = AliasName + "." + FieldName;
                        if (class_SelectAllModel.GetHaveSameFieldName(class_Field.ParaName, CurPageIndex))
                            InParaFieldName = class_Field.MultFieldName;
                        else
                            InParaFieldName = class_Field.ParaName;
                    }
                    else
                        InParaFieldName = class_Field.ParaName;
                    #region Where
                    if (class_Field.WhereSelect)
                    {
                        string IfLabel = null;
                        string NowWhere = null;
                        if (class_Field.WhereType == "AND")
                        {
                            if (class_Field.WhereIsNull)
                            {
                                IfLabel = string.Format("{1}<if test=\"{0} != null\">\r\n"
                                    , InParaFieldName, class_ToolSpace.GetSetSpaceCount(3));
                            }
                        }
                        if (class_Field.WhereType == "OR")
                        {
                            IfLabel = string.Format("{1}<when test=\"{0} != null\">\r\n"
                                , InParaFieldName, class_ToolSpace.GetSetSpaceCount(4));
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
                                String XmlFieldString = "#{" + string.Format("{0},jdbcType={1}"
                                    , InParaFieldName
                                    , Class_Tool.GetJdbcType(class_InterFaceDataBase.GetJavaType(class_Field.ReturnType))) + "}";
                                if ((LikeType < -99) && (class_Field.LogType.IndexOf("NULL") == -1))
                                    NowWhere = NowWhere + XmlFieldString;
                                else
                                    NowWhere = NowWhere + class_InterFaceDataBase.GetLikeString(XmlFieldString, LikeType);
                            }
                            else
                            {
                                if (class_InterFaceDataBase.IsAddPoint(class_Field.ReturnType))
                                    NowWhere = NowWhere + string.Format("'{0}'", class_Field.WhereValue);
                                else
                                    NowWhere = NowWhere + string.Format("{0}", class_Field.WhereValue);
                            }
                            if ((class_Field.LogType.IndexOf("<") > -1) || (class_Field.LogType.IndexOf("&") > -1))
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
                        stringBuilderGroup.AppendFormat("{0}", FieldName);
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
                            , class_Field.HavingFunction.Replace("?", FieldName)
                            , class_Field.HavingCondition
                            , class_Field.HavingValue);
                    }
                    #endregion
                    #region Order
                    if (class_Field.OrderSelect)
                    {
                        Class_OrderBy class_OrderBy = new Class_OrderBy();
                        class_OrderBy.FieldName = FieldName;
                        class_OrderBy.SortNo = class_Field.SortNo;
                        class_OrderBy.SortType = class_Field.SortType == "升序" ? "" : " DESC";
                        class_OrderBies.Add(class_OrderBy);
                    }
                    #endregion
                }
                CurPageIndex++;
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
        private string _GetMainServiceImpl(int PageIndex)
        {
            Class_Sub class_Sub = class_SelectAllModel.class_SubList[PageIndex];
            if (class_Sub == null)
                return null;
            string MapAfterString = "Mapper";
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
            stringBuilder.AppendFormat(_GetAuthor());
            stringBuilder.AppendFormat(" * @create {0}\r\n", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            stringBuilder.Append(" * @function\r\n * @editLog\r\n");
            stringBuilder.Append(" */\r\n");
            stringBuilder.Append("@SuppressWarnings(\"SpringJavaInjectionPointsAutowiringInspection\")\r\n@Service\r\n");
            stringBuilder.AppendFormat("public class {0}ServiceImpl implements {0}Service", class_Sub.NameSpace);
            stringBuilder.Append(" {\r\n");

            stringBuilder.AppendFormat("{0}@Autowired\r\n", class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0}{1}{3} {2}{3};\r\n", class_ToolSpace.GetSetSpaceCount(1)
            , class_Sub.NameSpace
            , Class_Tool.GetFirstCodeLow(class_Sub.NameSpace)
            , MapAfterString);
            stringBuilder.AppendFormat("{0}/**\r\n", class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0} * {1}\r\n{0} *\r\n", class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.MethodContent);

            List<Class_WhereField> class_WhereFields = new List<Class_WhereField>();
            class_WhereFields = _GetParameterType();
            if (class_WhereFields != null && class_WhereFields.Count > 0)
            {
                if (class_WhereFields.Count > 1)
                {
                    if (class_SelectAllModel.IsMultTable)
                        stringBuilder.AppendFormat("{0} * @param {1}{3} {2}\r\n"
                        , class_ToolSpace.GetSetSpaceCount(1)
                        , Class_Tool.GetFirstCodeLow(class_Sub.NameSpace)
                        , class_Sub.MethodContent
                        , InPutParamer);
                    else
                        stringBuilder.AppendFormat("{0} * @param {1} {2}\r\n"
                        , class_ToolSpace.GetSetSpaceCount(1)
                        , Class_Tool.GetFirstCodeLow(class_Sub.NameSpace)
                        , class_Sub.MethodContent);
                }
                else
                {
                    stringBuilder.AppendFormat("{0} * @param {1} {2}\r\n"
                    , class_ToolSpace.GetSetSpaceCount(1)
                    , Class_Tool.GetFirstCodeLow(class_WhereFields[0].OutFieldName)
                    , class_WhereFields[0].FieldRemark);
                }

            }

            #region
            //if (class_WhereFields != null)
            //{
            //    if (class_WhereFields.Count > 1)
            //    {
            //        stringBuilder.AppendFormat("{0} * @param {1} {2}\r\n"
            //        , class_ToolSpace.GetSetSpaceCount(1)
            //        , Class_Tool.GetFirstCodeLow(class_Sub.NameSpace)
            //        , class_Sub.MethodContent);
            //    }
            //    else
            //    {
            //        if (class_WhereFields.Count > 0)
            //            stringBuilder.AppendFormat("{0} * @param {1} {2}\r\n"
            //        , class_ToolSpace.GetSetSpaceCount(1)
            //        , Class_Tool.GetFirstCodeLow(class_WhereFields[0].FieldName)
            //        , class_WhereFields[0].FieldRemark);
            //    }
            //}
            #endregion
            stringBuilder.AppendFormat("{0} * @return {1}\r\n", class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.ServiceInterFaceReturnRemark);
            stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0}@Override\r\n{0}public ", class_ToolSpace.GetSetSpaceCount(1));
            if (class_Sub.ServiceInterFaceReturnCount == 0)
                stringBuilder.AppendFormat("{0}", _GetServiceReturnType(class_Sub, false));
            else
                stringBuilder.AppendFormat("List<{0}>", _GetServiceReturnType(class_Sub, false));
            if (class_WhereFields != null)
            {
                if (class_WhereFields.Count > 1)
                    stringBuilder.AppendFormat(" {0}({1} {2})"
                        , class_Sub.MethodId
                        , class_Sub.NameSpace
                        , Class_Tool.GetFirstCodeLow(class_Sub.NameSpace));
                else if (class_WhereFields.Count == 1)
                {
                    stringBuilder.AppendFormat(" {0}({1} {2})"
                        , class_Sub.MethodId
                        , Class_Tool.GetClosedJavaType(class_InterFaceDataBase.GetJavaType(class_WhereFields[0].LogType))
                        , class_WhereFields[0].OutFieldName);
                }
                else
                    stringBuilder.AppendFormat(" {0}()"
                        , class_Sub.MethodId);
                stringBuilder.Append(" {\r\n");
            }
            stringBuilder.AppendFormat("{0}return {1}{2}."
    , class_ToolSpace.GetSetSpaceCount(2)
    , Class_Tool.GetFirstCodeLow(class_Sub.NameSpace)
    , MapAfterString);

            if (class_WhereFields != null)
            {
                if (class_WhereFields.Count > 1)
                    stringBuilder.AppendFormat("{0}({1});\r\n"
                        , class_Sub.MethodId
                        , Class_Tool.GetFirstCodeLow(class_Sub.NameSpace));
                else if (class_WhereFields.Count == 1)
                {
                    stringBuilder.AppendFormat("{0}({1});\r\n"
                        , class_Sub.MethodId
                        , class_WhereFields[0].FieldName);
                }
                else
                    stringBuilder.AppendFormat("{0}();\r\n"
                        , class_Sub.MethodId);
            }

            stringBuilder.AppendFormat("{0}", class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.Append("}\r\n");


            stringBuilder.Append("}\r\n");
            return stringBuilder.ToString();
        }
        private string _GetAllWhereCount()
        {
            string WhereCount = null;
            int Counter = 0;
            if (class_SelectAllModel.class_SubList != null)
            {
                foreach (Class_Sub class_Sub in class_SelectAllModel.class_SubList)
                {
                    foreach (Class_Field class_Field in class_Sub.class_Fields)
                    {
                        if (class_Field.WhereSelect && class_Field.WhereValue == "参数" && class_Field.LogType.IndexOf("NULL") < 0)
                        {
                            WhereCount = class_Field.ReturnType;
                            Counter++;
                        }
                    }
                }
            }
            if (Counter > 1)
                WhereCount = "mult";
            return WhereCount;
        }
        private string _GetMainDAO(Class_Sub class_Main)
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
            stringBuilder.AppendFormat(_GetAuthor());
            stringBuilder.AppendFormat(" * @create {0}\r\n", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            stringBuilder.Append(" * @function\r\n * @editLog\r\n");
            stringBuilder.Append(" */\r\n");
            stringBuilder.Append("@Mapper\r\n");
            stringBuilder.Append(string.Format("public interface {0}Mapper ", class_Main.NameSpace) + "{\r\n");

            stringBuilder.AppendFormat("{0}/**\r\n", class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0} * {1}\r\n{0} *\r\n", class_ToolSpace.GetSetSpaceCount(1)
                , class_Main.MethodContent);
            class_WhereFields = _GetParameterType();
            if (class_WhereFields != null)
            {
                //_GetAllWhereCount()
                if (class_WhereFields.Count > 1)
                {
                    if (class_SelectAllModel.IsMultTable)
                        stringBuilder.AppendFormat("{0} * @param {1}{3} {2}\r\n"
                        , class_ToolSpace.GetSetSpaceCount(1)
                        , Class_Tool.GetFirstCodeLow(class_Main.NameSpace)
                        , class_Main.MethodContent
                        , InPutParamer);
                    else
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
                {
                    if (class_SelectAllModel.IsMultTable)
                        stringBuilder.AppendFormat(" {0}({1}{3} {2}{3});\r\n"
                            , class_Main.MethodId
                            , class_Main.NameSpace
                            , Class_Tool.GetFirstCodeLow(class_Main.NameSpace)
                            , InPutParamer);
                    else
                        stringBuilder.AppendFormat(" {0}({1} {2});\r\n"
                        , class_Main.MethodId
                        , class_Main.NameSpace
                        , Class_Tool.GetFirstCodeLow(class_Main.NameSpace));
                }
                else if (class_WhereFields.Count == 1)
                {
                    stringBuilder.AppendFormat(" {0}(@Param(\"{2}\") {1} {2});\r\n"
                        , class_Main.MethodId
                        , Class_Tool.GetClosedJavaType(class_InterFaceDataBase.GetJavaType(class_WhereFields[0].LogType))
                        , class_WhereFields[0].FieldName);
                }
                else
                    stringBuilder.AppendFormat(" {0}();\r\n"
                        , class_Main.MethodId);
            }
            stringBuilder.Append("}\r\n");

            return stringBuilder.ToString();
        }
        private string _GetMap(int PageIndex)
        {
            if (class_SelectAllModel.class_SubList.Count < PageIndex)
                return null;
            Class_Sub class_Main = class_SelectAllModel.class_SubList[PageIndex];
            if (class_Main == null)
                return null;
            if (class_Main.ResultType == 0)
            {
                Class_Tool class_ToolSpace = new Class_Tool();
                StringBuilder stringBuilder = new StringBuilder();
                IClass_InterFaceDataBase class_InterFaceDataBase;
                if (!class_Main.IsAddXmlHead)
                {
                    stringBuilder.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>\r\n");
                    stringBuilder.Append("<!DOCTYPE mapper PUBLIC \"-//mybatis.org//DTD Mapper 3.0//EN\" \"http://mybatis.org/dtd/mybatis-3-mapper.dtd\" >\r\n");
                    stringBuilder.AppendFormat("<mapper namespace=\"{0}.dao.{1}Mapper\">\r\n"
                        , class_SelectAllModel.AllPackerName
                        , class_Main.NameSpace);
                }
                stringBuilder.AppendFormat("{0}<resultMap id=\"{1}Map\" type=\"{2}."
                , class_ToolSpace.GetSetSpaceCount(1)
                , class_Main.ResultMapId
                , class_SelectAllModel.AllPackerName);
                if (class_SelectAllModel.IsMultTable)
                    stringBuilder.AppendFormat("dto.{0}\">\r\n"
                    , class_Main.DtoClassName);
                else
                    stringBuilder.AppendFormat("model.{0}\">\r\n"
                    , class_Main.ResultMapType);
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

                stringBuilder.Append(_GetMyAllMap(PageIndex, class_SelectAllModel.IsMultTable, 0, 2));

                stringBuilder.AppendFormat("{0}</resultMap>\r\n", class_ToolSpace.GetSetSpaceCount(1));
                if (!class_Main.IsAddXmlHead)
                    stringBuilder.Append("</mapper>\r\n");
                if (stringBuilder.Length > 0)
                    return stringBuilder.ToString();
                else
                    return null;
            }
            else
                return null;
        }
        private string _GetMainMapLable(int PageIndex)
        {
            if (class_SelectAllModel.class_SubList.Count < PageIndex)
                return null;
            Class_Sub class_Main = class_SelectAllModel.class_SubList[PageIndex];
            if (class_Main == null)
                return null;
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

            #region
            stringBuilder.AppendFormat("{1}<!-- 注释：{0} -->\r\n", class_Main.MethodContent, class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0}<select id=\"{1}\" "
                , class_ToolSpace.GetSetSpaceCount(1)
                , class_Main.MethodId);
            if (class_Main.ResultType == 0)
            {
                stringBuilder.AppendFormat("resultMap=\"{0}Map\""
                    , class_Main.ResultMapId);
            }
            else
            {
                string ResultType = _GetSelectType(class_Main);
                if (ResultType == "mult")
                {
                    if (class_WhereFields.Count > 1)
                        stringBuilder.AppendFormat("resultType=\"{0}.model.{1}\""
                        , class_SelectAllModel.AllPackerName
                        , class_Main.ResultMapType);
                    else
                        stringBuilder.AppendFormat("resultType=\"{0}\""
                        , class_InterFaceDataBase.GetJavaType(ResultType));
                }
                else
                    stringBuilder.AppendFormat("resultType=\"{0}\""
                    , class_InterFaceDataBase.GetJavaType(ResultType));
            }
            class_WhereFields = _GetParameterType();
            if (class_WhereFields != null)
            {
                if (class_WhereFields.Count > 1)
                {
                    stringBuilder.AppendFormat(" parameterType=\"{0}.model.{1}{2}\">\r\n"
                    , class_SelectAllModel.AllPackerName
                    , class_Main.ResultMapType
                    , InPutParamer);
                }
                else if (class_WhereFields.Count == 1)
                    stringBuilder.AppendFormat(" parameterType=\"{0}\">\r\n"
                    , class_InterFaceDataBase.GetJavaType(class_WhereFields[0].LogType));
                else
                    stringBuilder.Append(" >\r\n");
            }
            else
                stringBuilder.Append(" >\r\n");
            #endregion

            stringBuilder.AppendFormat("{0}SELECT\r\n", class_ToolSpace.GetSetSpaceCount(2));
            int Counter = 0;
            int CurPageIndex = 0;
            foreach (Class_Sub item in class_SelectAllModel.class_SubList)
            {
                string AliasName = item.AliasName;
                foreach (Class_Field class_Field in item.class_Fields)
                {
                    #region Select
                    if (class_Field.SelectSelect)
                    {
                        string FieldName = class_Field.FieldName;
                        string MyFieldName = null;
                        if (class_SelectAllModel.GetHaveSameFieldName(class_Field.ParaName, CurPageIndex))
                        {
                            MyFieldName = class_Field.MultFieldName;
                        }
                        if (class_SelectAllModel.IsMultTable)
                        {
                            FieldName = AliasName + "." + FieldName;
                        }

                        if ((class_Field.CaseWhen != null) && (class_Field.CaseWhen.Length > 0))
                        {
                            Class_CaseWhen class_CaseWhen = new Class_CaseWhen();
                            FieldName = class_CaseWhen.GetCaseWhenContent(class_Field.CaseWhen, FieldName, class_ToolSpace.GetSetSpaceCount(3));
                            if (MyFieldName != null)
                                FieldName = FieldName + " AS " + MyFieldName;
                        }
                        if ((class_Field.FunctionName != null) && (class_Field.FunctionName.Length > 0))
                        {
                            if (MyFieldName != null)
                                FieldName = string.Format(class_Field.FunctionName.Replace("?", "{0}") + " AS {1}", FieldName, MyFieldName);
                            else
                                FieldName = string.Format(class_Field.FunctionName.Replace("?", "{0}") + " AS {1}", FieldName, class_Field.ParaName);
                        }
                        if (MyFieldName != null)
                        {
                            FieldName = string.Format(FieldName + " AS {0}", MyFieldName);
                        }
                        else
                        {
                            if (!class_Field.FieldName.Equals(class_Field.ParaName))
                            {
                                if (class_Main.ResultType > 0)
                                    FieldName = string.Format(class_Field.FieldName + " AS {0}", class_Field.ParaName);
                            }
                        }
                        if (Counter++ > 0)
                            stringBuilder.AppendFormat("{1},{0}\r\n", FieldName, class_ToolSpace.GetSetSpaceCount(3));
                        else
                            stringBuilder.AppendFormat("{1}{0}\r\n", FieldName, class_ToolSpace.GetSetSpaceCount(3));
                    }
                    #endregion
                }
                CurPageIndex++;
            }

            #region FROM
            Counter = 0;
            foreach (Class_Sub item in class_SelectAllModel.class_SubList)
            {
                string AliasName = item.AliasName;
                if (Counter > 0)
                {
                    stringBuilder.AppendFormat("{0}", class_ToolSpace.GetSetSpaceCount(2));
                    if (item.InnerType == 0)
                        stringBuilder.AppendFormat("LEFT JOIN ");
                    else
                        stringBuilder.AppendFormat("INNER JOIN ");
                }
                else
                {
                    stringBuilder.AppendFormat("{0}FROM ", class_ToolSpace.GetSetSpaceCount(2));
                }

                if (class_SelectAllModel.IsMultTable)
                {
                    stringBuilder.AppendFormat("{0} AS {1} "
                        , item.TableName
                        , AliasName);
                    if (Counter > 0)
                        stringBuilder.AppendFormat("ON {0} = {1}\r\n"
                            , class_SelectAllModel.class_SubList[item.TableNo].AliasName + "." + item.OutFieldName
                            , AliasName + "." + item.MainTableFieldName);
                    else
                        stringBuilder.Append("\r\n");
                }
                else
                    stringBuilder.AppendFormat(" {1}\r\n", class_ToolSpace.GetSetSpaceCount(2), item.TableName);
                Counter++;
            }
            #endregion

            #region WHERE
            stringBuilder.Append(_GetMainWhereLable());
            #endregion

            stringBuilder.AppendFormat("{0}</select>\r\n", class_ToolSpace.GetSetSpaceCount(1));
            if (stringBuilder.Length > 0)
                return stringBuilder.ToString();
            else
                return null;
        }
        private string _GetMainModel(int PageIndex)
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
            stringBuilder.AppendFormat(_GetAuthor());
            stringBuilder.AppendFormat(" * @create {0}\r\n", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            stringBuilder.Append(" * @function\r\n * @editLog\r\n");
            stringBuilder.Append(" */\r\n");
            if (class_SelectAllModel.IsMultTable)
                stringBuilder.AppendFormat("public class {0}{1} "
                    , class_SelectAllModel.class_SubList[PageIndex].NameSpace
                    , InPutParamer);
            else
                stringBuilder.AppendFormat("public class {0} ", class_SelectAllModel.class_SubList[PageIndex].NameSpace);
            stringBuilder.Append(" {\r\n");

            //加入字段
            if (!class_SelectAllModel.IsMultTable)
            {
                foreach (Class_Field row in class_SelectAllModel.class_SubList[PageIndex].class_Fields)
                {
                    if (row.SelectSelect)
                    {
                        stringBuilder.AppendFormat("{0}/**\r\n", class_ToolSpace.GetSetSpaceCount(1));
                        stringBuilder.AppendFormat("{0} * {1}\r\n", class_ToolSpace.GetSetSpaceCount(1), row.FieldRemark);
                        stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));
                        if (class_SelectAllModel.class_Create.EnglishSign && Class_Tool.IsEnglishField(row.ParaName))
                            stringBuilder.AppendFormat("{0}private transient {1} {2};\r\n"
                                , class_ToolSpace.GetSetSpaceCount(1)
                                , Class_Tool.GetClosedJavaType(class_InterFaceDataBase.GetJavaType(row.ReturnType))
                                , row.ParaName);
                        else
                            stringBuilder.AppendFormat("{0}private {1} {2};\r\n"
                                , class_ToolSpace.GetSetSpaceCount(1)
                                , Class_Tool.GetClosedJavaType(class_InterFaceDataBase.GetJavaType(row.ReturnType))
                                , row.ParaName);

                    }
                }
            }
            else
            {
                List<Class_WhereField> class_WhereFields = new List<Class_WhereField>();
                class_WhereFields = _GetParameterType();
                if (class_WhereFields != null)
                {
                    foreach (Class_WhereField class_Field in class_WhereFields)
                    {
                        string JaveType = Class_Tool.GetClosedJavaType(class_InterFaceDataBase.GetJavaType(class_Field.LogType));
                        if (class_Field.LogType.IndexOf("IN") > -1)
                        {
                            JaveType = string.Format("List<{0}>", JaveType);
                        }
                        stringBuilder.AppendFormat("{0}/**\r\n", class_ToolSpace.GetSetSpaceCount(1));
                        if (class_Field.IsSame)
                            stringBuilder.AppendFormat("{0} * 表{1},原字段名{2},现字段名{3}:{4}\r\n"
                                , class_ToolSpace.GetSetSpaceCount(1)
                                , class_Field.TableName
                                , class_Field.FieldName
                                , class_Field.OutFieldName
                                , class_Field.FieldRemark);
                        else
                            stringBuilder.AppendFormat("{0} * 表{1},字段名{2}:{3}\r\n"
                                , class_ToolSpace.GetSetSpaceCount(1)
                                , class_Field.TableName
                                , class_Field.OutFieldName
                                , class_Field.FieldRemark);
                        stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));
                        if (class_SelectAllModel.class_Create.EnglishSign && Class_Tool.IsEnglishField(class_Field.ParaName))
                            stringBuilder.AppendFormat("{0}private transient {1} {2};\r\n"
                                , class_ToolSpace.GetSetSpaceCount(1)
                                , JaveType
                                , class_Field.OutFieldName);
                        else
                            stringBuilder.AppendFormat("{0}private {1} {2};\r\n"
                                , class_ToolSpace.GetSetSpaceCount(1)
                                , JaveType
                                , class_Field.OutFieldName);
                    }
                }
                class_WhereFields.Clear();

                #region
                //int CurPageIndex = 0;
                //foreach (Class_Sub item in class_SelectAllModel.class_SubList)
                //{
                //    string AliasName = item.AliasName;
                //    foreach (Class_Field class_Field in item.class_Fields)
                //    {
                //        #region Where
                //        if (class_Field.WhereSelect && class_Field.WhereValue == "参数" && class_Field.LogType.IndexOf("NULL") < 0)
                //        {
                //            string InParaFieldName = null;
                //            bool IsSame = class_SelectAllModel.GetHaveSameFieldName(class_Field.ParaName, CurPageIndex);
                //            if (IsSame)
                //                InParaFieldName = class_Field.MultFieldName;
                //            else
                //                InParaFieldName = class_Field.ParaName;
                //            string JaveType = Class_Tool.GetClosedJavaType(class_InterFaceDataBase.GetJavaType(class_Field.ReturnType));
                //            if (class_Field.LogType.IndexOf("IN") > -1)
                //            {
                //                JaveType = string.Format("List<{0}>", JaveType);
                //            }
                //            stringBuilder.AppendFormat("{0}/**\r\n", class_ToolSpace.GetSetSpaceCount(1));
                //            if (IsSame)
                //                stringBuilder.AppendFormat("{0} * 表{1},原字段名{2},现字段名{3}:{4}\r\n"
                //                    , class_ToolSpace.GetSetSpaceCount(1)
                //                    , item.TableName
                //                    , class_Field.FieldName, InParaFieldName
                //                    , class_Field.FieldRemark);
                //            else
                //                stringBuilder.AppendFormat("{0} * 表{1},字段名{2}:{3}\r\n"
                //                    , class_ToolSpace.GetSetSpaceCount(1)
                //                    , item.TableName
                //                    , InParaFieldName
                //                    , class_Field.FieldRemark);
                //            stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));
                //            if (class_SelectAllModel.class_Create.EnglishSign && Class_Tool.IsEnglishField(class_Field.ParaName))
                //                stringBuilder.AppendFormat("{0}private transient {1} {2};\r\n"
                //                    , class_ToolSpace.GetSetSpaceCount(1)
                //                    , JaveType
                //                    , InParaFieldName);
                //            else
                //                stringBuilder.AppendFormat("{0}private {1} {2};\r\n"
                //                    , class_ToolSpace.GetSetSpaceCount(1)
                //                    , JaveType
                //                    , InParaFieldName);

                //        }
                //        #endregion
                //    }
                //    CurPageIndex++;
                //}
                #endregion
            }

            stringBuilder.Append("}\r\n");

            return stringBuilder.ToString();
        }
        private string _GetMainServiceInterFace(Class_Sub class_Main)
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
            stringBuilder.AppendFormat(_GetAuthor());
            stringBuilder.AppendFormat(" * @create {0}\r\n", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            stringBuilder.Append(" * @function\r\n * @editLog\r\n");
            stringBuilder.Append(" */\r\n");
            stringBuilder.Append(string.Format("public interface {0}Service ", class_Main.NameSpace) + "{\r\n");

            stringBuilder.AppendFormat("{0}/**\r\n", class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0} * {1}\r\n{0} *\r\n", class_ToolSpace.GetSetSpaceCount(1)
                , class_Main.MethodContent);
            class_WhereFields = _GetParameterType();
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
                        , Class_Tool.GetClosedJavaType(class_InterFaceDataBase.GetJavaType(class_WhereFields[0].LogType))
                        , class_WhereFields[0].FieldName);
                }
                else
                    stringBuilder.AppendFormat(" {0}();\r\n"
                        , class_Main.MethodId);
            }
            stringBuilder.Append("}\r\n");
            return stringBuilder.ToString();
        }
        private string _GetMainFeignControl(Class_Sub class_Main)
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
            stringBuilder.AppendFormat(_GetAuthor());
            stringBuilder.AppendFormat(" * @create {0}\r\n", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            stringBuilder.Append(" * @function\r\n * @editLog\r\n");
            stringBuilder.Append(" */\r\n");
            stringBuilder.Append("@RestController\r\n");
            stringBuilder.AppendFormat("@RequestMapping(\"/{0}\")\r\n", class_SelectAllModel.class_Create.MicroServiceName);
            if (class_SelectAllModel.class_Create.SwaggerSign)
                stringBuilder.AppendFormat("@Api(value = \"{0}\", description = \"{1}\")\r\n"
                    , class_Main.ControlSwaggerValue
                    , class_Main.ControlSwaggerDescription);
            stringBuilder.Append(string.Format("public class {0}Controller ", class_Main.NameSpace) + "{\r\n");

            stringBuilder.AppendFormat("{0}@Autowired\r\n", class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0}{1}Service {2}Service;\r\n"
                , class_ToolSpace.GetSetSpaceCount(1)
                , class_Main.NameSpace
                , Class_Tool.GetFirstCodeLow(class_Main.NameSpace));

            stringBuilder.Append("\r\n");
            stringBuilder.AppendFormat("{0}/**\r\n", class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0} * {1}\r\n{0} *\r\n", class_ToolSpace.GetSetSpaceCount(1)
                , class_Main.MethodContent);
            class_WhereFields = _GetParameterType();
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
            #region Swagger
            if (class_SelectAllModel.class_Create.SwaggerSign)
            {
                stringBuilder.AppendFormat("{0}@ApiOperation(value = \"{1}\", notes = \"{2}\")\r\n"
                , class_ToolSpace.GetSetSpaceCount(1)
                , class_Main.MethodContent
                , class_Main.ServiceInterFaceReturnRemark);
            }
            #endregion
            stringBuilder.AppendFormat("{0}@{1}Mapping(\"/{2}Dto\")\r\n"
            , class_ToolSpace.GetSetSpaceCount(1)
            , class_SelectAllModel.class_Create.HttpRequestType
            , class_Main.MethodId);
            stringBuilder.AppendFormat("{0}public ", class_ToolSpace.GetSetSpaceCount(1));

            if (class_Main.ServiceInterFaceReturnCount == 0)
                stringBuilder.AppendFormat("{0}", class_Main.DtoClassName);
            else
                stringBuilder.AppendFormat("List<{0}>", class_Main.DtoClassName);
            stringBuilder.AppendFormat(" {0}Dto", class_Main.MethodId);
            stringBuilder.Append("(@RequestBody CommonQuery commonQuery) {\r\n");
            //1、载入所有关联字段信息
            //2、载入所有从表类名信息

            stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(1) + "}\r\n");
            stringBuilder.Append("\r\n");

            stringBuilder.Append("}\r\n");
            return stringBuilder.ToString();
        }
        private string _GetMainDTO(int PageIndex)
        {
            if (class_SelectAllModel.class_SubList == null)
                return null;

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
            stringBuilder.AppendFormat(_GetAuthor());
            stringBuilder.AppendFormat(" * @create {0}\r\n", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            stringBuilder.Append(" * @function\r\n * @editLog\r\n");
            stringBuilder.Append(" */\r\n");
            stringBuilder.AppendFormat("public final class {0}"
                , class_SelectAllModel.class_SubList[PageIndex].DtoClassName);
            if (!class_SelectAllModel.IsMultTable)
            {
                if (class_SelectAllModel.class_SubList[PageIndex].ExtendsSign)
                {
                    stringBuilder.AppendFormat(" extends {0}", class_SelectAllModel.class_SubList[PageIndex].NameSpace);
                }
            }
            stringBuilder.Append(" {\r\n");

            stringBuilder.Append(_GetMyDto(PageIndex, class_SelectAllModel.IsMultTable, 0));

            List<Class_LinkFieldInfo> class_LinkFieldInfos = class_SelectAllModel.GetClass_LinkFieldInfos()
                .FindAll(a => a.TableNo.Equals(PageIndex));
            if (class_LinkFieldInfos != null)
            {
                foreach (Class_LinkFieldInfo item in class_LinkFieldInfos)
                {
                    stringBuilder.Append(_GetMyDto(item.CurTableNo, class_SelectAllModel.IsMultTable, item.JoinType));
                }
            }
            stringBuilder.Append("}\r\n");
            class_LinkFieldInfos.Clear();
            return stringBuilder.ToString();
        }
        private string _GetMyAllMap(int PageIndex, bool IsMultTable, int JoinType, int SpaceCounter)
        {
            Class_Tool class_ToolSpace = new Class_Tool();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(_GetMyMap(PageIndex, class_SelectAllModel.IsMultTable, JoinType, SpaceCounter));

            List<Class_LinkFieldInfo> class_LinkFieldInfos = class_SelectAllModel.GetClass_LinkFieldInfos()
                .FindAll(a => a.TableNo.Equals(PageIndex));
            if (class_LinkFieldInfos != null)
            {
                foreach (Class_LinkFieldInfo item in class_LinkFieldInfos)
                {
                    stringBuilder.Append(_GetMyAllMap(item.CurTableNo, class_SelectAllModel.IsMultTable, item.JoinType, (JoinType == 0 ? SpaceCounter : SpaceCounter + 1)));
                }
            }
            if (JoinType == 1)
                stringBuilder.AppendFormat("{0}</association>\r\n"
                , class_ToolSpace.GetSetSpaceCount(SpaceCounter));
            if (JoinType == 2)
                stringBuilder.AppendFormat("{0}</collection>\r\n"
                , class_ToolSpace.GetSetSpaceCount(SpaceCounter));
            return stringBuilder.ToString();
        }
        private string _GetMyMap(int PageIndex, bool IsMultTable, int JoinType, int SpaceCounter)
        {
            if (class_SelectAllModel.class_SubList == null)
                return null;
            if (PageIndex > class_SelectAllModel.class_SubList.Count)
                return null;
            Class_Sub class_Sub = class_SelectAllModel.class_SubList[PageIndex];
            if (class_Sub == null)
                return null;

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
            switch (JoinType)
            {
                case 0:
                    {
                        foreach (Class_Field class_Field in class_Sub.class_Fields)
                        {
                            if (class_Field.SelectSelect)
                            {
                                string MyFieldName = class_Field.ParaName;
                                if (IsMultTable && class_SelectAllModel.GetHaveSameFieldName(class_Field.ParaName, PageIndex))
                                    MyFieldName = class_Field.MultFieldName;
                                if (class_Field.FieldIsKey)
                                {
                                    if (PageIndex > 0)
                                    {
                                        stringBuilder.AppendFormat("{0}<result column=\"{1}\" property=\"{2}\" jdbcType=\"{3}\"/><!-- {4} -->\r\n"
                                            , class_ToolSpace.GetSetSpaceCount(SpaceCounter)
                                            , MyFieldName
                                            , MyFieldName
                                            , Class_Tool.GetJdbcType(class_InterFaceDataBase.GetJavaType(class_Field.ReturnType))
                                            , class_Field.FieldRemark);
                                    }
                                    else
                                        stringBuilder.AppendFormat("{0}<id column=\"{1}\" property=\"{2}\" jdbcType=\"{3}\"/><!-- {4} -->\r\n"
                                            , class_ToolSpace.GetSetSpaceCount(SpaceCounter)
                                            , MyFieldName
                                            , MyFieldName
                                            , Class_Tool.GetJdbcType(class_InterFaceDataBase.GetJavaType(class_Field.ReturnType))
                                            , class_Field.FieldRemark);
                                }
                                else
                                {
                                    stringBuilder.AppendFormat("{0}<result column=\"{1}\" property=\"{2}\" jdbcType=\"{3}\"/><!-- {4} -->\r\n"
                                        , class_ToolSpace.GetSetSpaceCount(SpaceCounter)
                                        , MyFieldName
                                        , MyFieldName
                                        , Class_Tool.GetJdbcType(class_InterFaceDataBase.GetJavaType(class_Field.ReturnType))
                                        , class_Field.FieldRemark);
                                }
                            }
                        }
                    }
                    break;
                case 1:
                    {
                        stringBuilder.AppendFormat("{0}<association property=\"{1}\" column=\"{2}\" javaType=\"{3}.dto.{4}\">\r\n"
                            , class_ToolSpace.GetSetSpaceCount(SpaceCounter)
                            , Class_Tool.GetFirstCodeLow(class_Sub.DtoClassName)
                            , "idtest"
                            , class_SelectAllModel.AllPackerName
                            , Class_Tool.GetFirstCodeUpper(class_Sub.DtoClassName));
                        foreach (Class_Field class_Field in class_Sub.class_Fields)
                        {
                            if (class_Field.SelectSelect)
                            {
                                string MyFieldName = class_Field.ParaName;
                                if (IsMultTable && class_SelectAllModel.GetHaveSameFieldName(class_Field.ParaName, PageIndex))
                                    MyFieldName = class_Field.MultFieldName;
                                if (class_Field.FieldIsKey)
                                {
                                    if (PageIndex > 0)
                                    {
                                        stringBuilder.AppendFormat("{0}<result column=\"{1}\" property=\"{2}\" jdbcType=\"{3}\"/><!-- {4} -->\r\n"
                                            , class_ToolSpace.GetSetSpaceCount(SpaceCounter + 1)
                                            , MyFieldName
                                            , MyFieldName
                                            , Class_Tool.GetJdbcType(class_InterFaceDataBase.GetJavaType(class_Field.ReturnType))
                                            , class_Field.FieldRemark);
                                    }
                                    else
                                        stringBuilder.AppendFormat("{0}<id column=\"{1}\" property=\"{2}\" jdbcType=\"{3}\"/><!-- {4} -->\r\n"
                                            , class_ToolSpace.GetSetSpaceCount(SpaceCounter + 1)
                                            , MyFieldName
                                            , MyFieldName
                                            , Class_Tool.GetJdbcType(class_InterFaceDataBase.GetJavaType(class_Field.ReturnType))
                                            , class_Field.FieldRemark);
                                }
                                else
                                {
                                    stringBuilder.AppendFormat("{0}<result column=\"{1}\" property=\"{2}\" jdbcType=\"{3}\"/><!-- {4} -->\r\n"
                                        , class_ToolSpace.GetSetSpaceCount(SpaceCounter + 1)
                                        , MyFieldName
                                        , MyFieldName
                                        , Class_Tool.GetJdbcType(class_InterFaceDataBase.GetJavaType(class_Field.ReturnType))
                                        , class_Field.FieldRemark);
                                }
                            }
                        }
                    }
                    break;
                default:
                    {
                        stringBuilder.AppendFormat("{0}<collection property = \"{1}s\" ofType = \"{2}.dto.{3}\" >\r\n"
                            , class_ToolSpace.GetSetSpaceCount(SpaceCounter)
                            , Class_Tool.GetFirstCodeLow(class_Sub.DtoClassName)
                            , class_SelectAllModel.AllPackerName
                            , Class_Tool.GetFirstCodeUpper(class_Sub.DtoClassName));
                        foreach (Class_Field class_Field in class_Sub.class_Fields)
                        {
                            if (class_Field.SelectSelect)
                            {
                                string MyFieldName = class_Field.ParaName;
                                if (IsMultTable && class_SelectAllModel.GetHaveSameFieldName(class_Field.ParaName, PageIndex))
                                    MyFieldName = class_Field.MultFieldName;
                                if (class_Field.FieldIsKey)
                                {
                                    if (PageIndex > 0)
                                    {
                                        stringBuilder.AppendFormat("{0}<result column=\"{1}\" property=\"{2}\" jdbcType=\"{3}\"/><!-- {4} -->\r\n"
                                            , class_ToolSpace.GetSetSpaceCount(SpaceCounter + 1)
                                            , MyFieldName
                                            , MyFieldName
                                            , Class_Tool.GetJdbcType(class_InterFaceDataBase.GetJavaType(class_Field.ReturnType))
                                            , class_Field.FieldRemark);
                                    }
                                    else
                                        stringBuilder.AppendFormat("{0}<id column=\"{1}\" property=\"{2}\" jdbcType=\"{3}\"/><!-- {4} -->\r\n"
                                            , class_ToolSpace.GetSetSpaceCount(SpaceCounter + 1)
                                            , MyFieldName
                                            , MyFieldName
                                            , Class_Tool.GetJdbcType(class_InterFaceDataBase.GetJavaType(class_Field.ReturnType))
                                            , class_Field.FieldRemark);
                                }
                                else
                                {
                                    stringBuilder.AppendFormat("{0}<result column=\"{1}\" property=\"{2}\" jdbcType=\"{3}\"/><!-- {4} -->\r\n"
                                        , class_ToolSpace.GetSetSpaceCount(SpaceCounter + 1)
                                        , MyFieldName
                                        , MyFieldName
                                        , Class_Tool.GetJdbcType(class_InterFaceDataBase.GetJavaType(class_Field.ReturnType))
                                        , class_Field.FieldRemark);
                                }
                            }
                        }

                    }
                    break;
            }
            return stringBuilder.ToString();
        }
        private string _GetMyDto(int PageIndex, bool IsMultTable, int JoinType)
        {
            if (class_SelectAllModel.class_SubList == null)
                return null;
            if (PageIndex > class_SelectAllModel.class_SubList.Count)
                return null;
            Class_Sub class_Sub = class_SelectAllModel.class_SubList[PageIndex];
            if (class_Sub == null)
                return null;

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
            stringBuilder.AppendFormat("{0}/**\r\n", class_ToolSpace.GetSetSpaceCount(1));
            switch (JoinType)
            {
                case 0:
                    {
                        foreach (Class_Field class_Field in class_Sub.class_Fields)
                        {
                            if (class_Field.SelectSelect)
                            {
                                string MyFieldName = class_Field.ParaName;
                                if (IsMultTable && class_SelectAllModel.GetHaveSameFieldName(class_Field.ParaName, PageIndex))
                                    MyFieldName = class_Field.MultFieldName;

                                stringBuilder.AppendFormat("{0} * {1}\r\n", class_ToolSpace.GetSetSpaceCount(1), class_Field.FieldRemark);
                                stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));
                                if (class_SelectAllModel.class_Create.EnglishSign && Class_Tool.IsEnglishField(class_Field.ParaName))
                                    stringBuilder.AppendFormat("{0}private transient {1} {2};\r\n"
                                        , class_ToolSpace.GetSetSpaceCount(1)
                                        , Class_Tool.GetClosedJavaType(class_InterFaceDataBase.GetJavaType(class_Field.ReturnType))
                                        , MyFieldName);
                                else
                                    stringBuilder.AppendFormat("{0}private {1} {2};\r\n"
                                        , class_ToolSpace.GetSetSpaceCount(1)
                                        , Class_Tool.GetClosedJavaType(class_InterFaceDataBase.GetJavaType(class_Field.ReturnType))
                                        , MyFieldName);
                            }
                        }
                    }
                    break;
                case 1:
                    {
                        stringBuilder.AppendFormat("{0} * {1}\r\n", class_ToolSpace.GetSetSpaceCount(1), "关联外表");
                        stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));
                        stringBuilder.AppendFormat("{0}private {1} {2};\r\n"
                            , class_ToolSpace.GetSetSpaceCount(1)
                            , class_SelectAllModel.class_SubList[PageIndex].DtoClassName
                            , Class_Tool.GetFirstCodeLow(class_SelectAllModel.class_SubList[PageIndex].DtoClassName));
                    }
                    break;
                default:
                    {
                        stringBuilder.AppendFormat("{0} * {1}\r\n", class_ToolSpace.GetSetSpaceCount(1), "关联外表");
                        stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));
                        stringBuilder.AppendFormat("{0}private List<{1}> {2}s;\r\n"
                            , class_ToolSpace.GetSetSpaceCount(1)
                            , class_SelectAllModel.class_SubList[PageIndex].DtoClassName
                            , Class_Tool.GetFirstCodeLow(class_SelectAllModel.class_SubList[PageIndex].DtoClassName));
                    }
                    break;
            }
            return stringBuilder.ToString();
        }
        private string _GetMainControl(Class_Sub class_Main)
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
            if (!class_Main.ControlMainCode)
            {
                stringBuilder.Append("/**\r\n");
                stringBuilder.AppendFormat(_GetAuthor());
                stringBuilder.AppendFormat(" * @create {0}\r\n", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                stringBuilder.Append(" * @function\r\n * @editLog\r\n");
                stringBuilder.Append(" */\r\n");
                stringBuilder.Append("@RestController\r\n");
                stringBuilder.AppendFormat("@RequestMapping(\"/{0}\")\r\n", class_SelectAllModel.class_Create.MicroServiceName);
                if (class_SelectAllModel.class_Create.SwaggerSign)
                    stringBuilder.AppendFormat("@Api(value = \"{0}\", description = \"{1}\")\r\n"
                        , class_Main.ControlSwaggerValue
                        , class_Main.ControlSwaggerDescription);
                stringBuilder.Append(string.Format("public class {0}Controller ", class_Main.NameSpace) + "{\r\n");

                //stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(1) + "@Value(\"${server.port}\")\r\n");
                //stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(1) + "String myPort;\r\n\r\n");

                stringBuilder.AppendFormat("{0}@Autowired\r\n", class_ToolSpace.GetSetSpaceCount(1));
                stringBuilder.AppendFormat("{0}{1}Service {2}Service;\r\n"
                    , class_ToolSpace.GetSetSpaceCount(1)
                    , class_Main.NameSpace
                    , Class_Tool.GetFirstCodeLow(class_Main.NameSpace));

                stringBuilder.Append("\r\n");
            }
            stringBuilder.AppendFormat("{0}/**\r\n", class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0} * {1}\r\n{0} *\r\n", class_ToolSpace.GetSetSpaceCount(1)
                , class_Main.MethodContent);
            class_WhereFields = _GetParameterType();
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
            if (class_SelectAllModel.class_Create.EnglishSign)
            {
                stringBuilder.AppendFormat("{0} * @param {1} {2}\r\n"
                    , class_ToolSpace.GetSetSpaceCount(1)
                , "englishSign"
                , "是否是英文版");
            }
            stringBuilder.AppendFormat("{0} * @return {1}\r\n", class_ToolSpace.GetSetSpaceCount(1)
            , class_Main.ServiceInterFaceReturnRemark);
            stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));
            #region Swagger
            if (class_SelectAllModel.class_Create.SwaggerSign)
            {
                stringBuilder.AppendFormat("{0}@ApiOperation(value = \"{1}\", notes = \"{2}\")\r\n"
                , class_ToolSpace.GetSetSpaceCount(1)
                , class_Main.MethodContent
                , class_Main.ServiceInterFaceReturnRemark);
                stringBuilder.AppendFormat("{0}@ApiImplicitParams(", class_ToolSpace.GetSetSpaceCount(1));
                stringBuilder.Append("{\r\n");
                int index = 0;
                foreach (Class_WhereField row in class_WhereFields)
                {
                    stringBuilder.AppendFormat("{0}@ApiImplicitParam(name = \"{1}\", value = \"{2}\", required = true, dataType = \"{3}\")"
                    , class_ToolSpace.GetSetSpaceCount(3)
                    , Class_Tool.GetFirstCodeLow(row.FieldName)
                    , row.FieldRemark
                    , Class_Tool.GetSimplificationJavaType(class_InterFaceDataBase.GetJavaType(row.LogType)));
                    if (index < class_WhereFields.Count - 1)
                        stringBuilder.Append(",");
                    if (index == class_WhereFields.Count - 1 && class_SelectAllModel.class_Create.EnglishSign)
                        stringBuilder.Append(",");
                    stringBuilder.Append("\r\n");
                    index++;
                }
                if (class_SelectAllModel.class_Create.EnglishSign)
                    stringBuilder.AppendFormat("{0}@ApiImplicitParam(name = \"englishSign\", value = \"是否生成英文\", required = true, dataType = \"Boolean\")\r\n"
                    , class_ToolSpace.GetSetSpaceCount(3));

                stringBuilder.AppendFormat("{0}", class_ToolSpace.GetSetSpaceCount(1));
                stringBuilder.Append("})\r\n");
            }
            #endregion
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
                stringBuilder.Append(")");
                if (row.FieldLogType.IndexOf("IN") > -1)
                {
                    stringBuilder.AppendFormat(" List<{0}>"
                    , Class_Tool.GetSimplificationJavaType(class_InterFaceDataBase.GetJavaType(row.LogType)));
                }
                else
                    stringBuilder.AppendFormat(" {0}"
                    , Class_Tool.GetSimplificationJavaType(class_InterFaceDataBase.GetJavaType(row.LogType)));
                stringBuilder.AppendFormat(" {0}", row.FieldName);
            }
            if (class_SelectAllModel.class_Create.EnglishSign)
            {
                if (Index > 0)
                {
                    stringBuilder.AppendFormat("\r\n{1}, @RequestParam(value = \"{0}\", defaultValue = \"false\") Boolean {0}"
                    , "englishSign"
                    , class_ToolSpace.GetSetSpaceCount(3));
                }
                else
                {
                    stringBuilder.AppendFormat("\r\n{1} @RequestParam(value = \"{0}\", defaultValue = \"false\") Boolean {0}"
                    , "englishSign"
                    , class_ToolSpace.GetSetSpaceCount(3));
                }
            }
            stringBuilder.Append(") {\r\n");
            if (class_WhereFields != null)
            {
                if (class_WhereFields.Count > 1)
                {
                    stringBuilder.AppendFormat("{0}{1} {2}Para = new {1}();\r\n"
                    , class_ToolSpace.GetSetSpaceCount(2)
                    , class_Main.NameSpace
                    , Class_Tool.GetFirstCodeLow(class_Main.NameSpace));
                    foreach (Class_WhereField row in class_WhereFields)
                    {
                        stringBuilder.AppendFormat("{0}{1}Para.set{2}({3});\r\n"
                        , class_ToolSpace.GetSetSpaceCount(2)
                        , Class_Tool.GetFirstCodeLow(class_Main.NameSpace)
                        , Class_Tool.GetFirstCodeUpper(row.FieldName)
                        , row.FieldName);
                    }
                }
            }
            stringBuilder.Append("\r\n//      请在这里写逻辑代码\r\n\r\n");

            if ((class_SelectAllModel.class_Create.EnglishSign) && (_GetEnglishFieldList(class_Main).Count > 0))
            {
                if (class_Main.ServiceInterFaceReturnCount == 0)
                {
                    stringBuilder.AppendFormat("{0}{1}"
                        , class_ToolSpace.GetSetSpaceCount(2)
                        , _GetServiceReturnType(class_Main, false));
                    stringBuilder.AppendFormat(" {1} = new {0}();\r\n"
                        , _GetServiceReturnType(class_Main, false)
                        , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Main, false))
                        , class_ToolSpace.GetSetSpaceCount(2));
                    stringBuilder.AppendFormat("{0}{1} = {2}Service."
                            , class_ToolSpace.GetSetSpaceCount(2)
                        , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Main, false))
                        , Class_Tool.GetFirstCodeLow(class_Main.NameSpace));
                }
                else
                {
                    stringBuilder.AppendFormat("{0}List<{1}>"
                        , class_ToolSpace.GetSetSpaceCount(2)
                        , _GetServiceReturnType(class_Main, false));
                    stringBuilder.AppendFormat(" {1}s = new {0}();\r\n"
                        , _GetServiceReturnType(class_Main, false)
                        , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Main, false))
                        , class_ToolSpace.GetSetSpaceCount(2));
                    stringBuilder.AppendFormat("{0}{1}s = {2}Service."
                            , class_ToolSpace.GetSetSpaceCount(2)
                        , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Main, false))
                        , Class_Tool.GetFirstCodeLow(class_Main.NameSpace));
                }

                if (class_WhereFields != null)
                {
                    if (class_WhereFields.Count > 1)
                        stringBuilder.AppendFormat("{0}({1}Para);\r\n"
                            , class_Main.MethodId
                            , Class_Tool.GetFirstCodeLow(class_Main.NameSpace));
                    else if (class_WhereFields.Count == 1)
                    {
                        stringBuilder.AppendFormat("{0}({1}Para);\r\n"
                            , class_Main.MethodId
                            , class_WhereFields[0].FieldName);
                    }
                    else
                        stringBuilder.AppendFormat("{0}();\r\n"
                            , class_Main.MethodId);
                }
                stringBuilder.AppendFormat("{0}if (englishSign) ", class_ToolSpace.GetSetSpaceCount(2));
                stringBuilder.Append("{\r\n");
                if (class_Main.ServiceInterFaceReturnCount == 0)
                {
                    List<Class_EnglishField> class_EnglishFields = new List<Class_EnglishField>();
                    class_EnglishFields = _GetEnglishFieldList(class_Main);
                    if (class_EnglishFields.Count > 0)
                    {
                        foreach (Class_EnglishField row in class_EnglishFields)
                        {
                            stringBuilder.AppendFormat("{0}{3}.set{1}({3}.get{2}());\r\n"
                                , class_ToolSpace.GetSetSpaceCount(3)
                                , Class_Tool.GetFirstCodeUpper(row.FieldChinaName)
                                , Class_Tool.GetFirstCodeUpper(row.FieldEnglishName)
                                , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Main, false)));
                        }
                    }
                }
                else
                {
                    stringBuilder.AppendFormat("{0}{1}.forEach(item ->"
                        , class_ToolSpace.GetSetSpaceCount(3)
                        , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Main, false)) + "s");
                    List<Class_EnglishField> class_EnglishFields = new List<Class_EnglishField>();
                    class_EnglishFields = _GetEnglishFieldList(class_Main);
                    if (class_EnglishFields.Count > 0)
                    {
                        stringBuilder.Append(" {\r\n");
                        foreach (Class_EnglishField row in class_EnglishFields)
                        {
                            //item.setName(item.getName());
                            stringBuilder.AppendFormat("{0}item.set{1}(item.get{2}());\r\n"
                                , class_ToolSpace.GetSetSpaceCount(4)
                                , Class_Tool.GetFirstCodeUpper(row.FieldChinaName)
                                , Class_Tool.GetFirstCodeUpper(row.FieldEnglishName));
                        }
                        stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(3) + "});\r\n");
                    }
                }
                stringBuilder.AppendFormat("{0}", class_ToolSpace.GetSetSpaceCount(2));
                stringBuilder.Append("}\r\n");
                stringBuilder.AppendFormat("{0}return "
                    , class_ToolSpace.GetSetSpaceCount(2));
                if (class_Main.ServiceInterFaceReturnCount == 0)
                {
                    stringBuilder.AppendFormat(" {0};\r\n"
                    , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Main, false)));
                }
                else
                {
                    stringBuilder.AppendFormat(" {0}s;\r\n"
                    , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Main, false)));

                }
            }
            else
            {
                stringBuilder.AppendFormat("{0}return {1}Service."
                    , class_ToolSpace.GetSetSpaceCount(2)
                    , Class_Tool.GetFirstCodeLow(class_Main.NameSpace));
                if (class_WhereFields != null)
                {
                    if (class_WhereFields.Count > 1)
                        stringBuilder.AppendFormat("{0}({1}Para);\r\n"
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
            }

            stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(1) + "}\r\n");
            if (!class_Main.ControlMainCode)
                stringBuilder.Append("\r\n");
            //if (class_SelectAllModel.class_Create.SwaggerSign)
            //    stringBuilder.AppendFormat("{0}@ApiOperation(value = \"{1}\", notes = \"{2}\")\r\n"
            //    , class_ToolSpace.GetSetSpaceCount(1)
            //    , "查看该站点端口"
            //    , "以便观察负载均衡");
            //stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(1) + "@PostMapping(value = \"/myPort\")\r\n");
            //stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(1) + "public String myPort(){\r\n");
            //stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(2) + "return \"myPort: \" + this.myPort;\r\n");
            //stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(1) + "}\r\n");
            //stringBuilder.Append("\r\n");
            //if (class_SelectAllModel.class_Create.SwaggerSign)
            //    stringBuilder.AppendFormat("{0}@ApiOperation(value = \"{1}\", notes = \"{2}\")\r\n"
            //    , class_ToolSpace.GetSetSpaceCount(1)
            //    , "手动下线该站点功能"
            //    , "通过此方法，主动向注册中心提出下线");
            //stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(1) + "@GetMapping(value = \"/downLine\")\r\n");
            //stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(1) + "public void downLine(){\r\n");
            //stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(2) + "getInstance().shutdownComponent();\r\n");
            //stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(1) + "}\r\n");
            //stringBuilder.Append("}\r\n");
            return stringBuilder.ToString();
        }
        #endregion

        #region 公共方法
        public bool IsCheckOk(ref List<string> outMessage)
        {
            bool OkSign = true;

            #region 认证关联性
            if (OkSign && GetLinkFieldInfosCount() > 0)
            {
                OkSign = CheckClassLinkField(ref outMessage);
            }
            #endregion

            return OkSign;
        }
        public Class_CreateSelectCode(string xmlFileName)
        {
            if (xmlFileName != null)
            {
                Class_PublicMethod class_PublicMethod = new Class_PublicMethod();
                class_SelectAllModel = new Class_SelectAllModel();
                class_SelectAllModel = class_PublicMethod.FromXmlToSelectObject<Class_SelectAllModel>(xmlFileName);
            }
        }
        public string GetMap(int Index)
        {
            if (class_SelectAllModel.class_SubList.Count > Index)
                return _GetMap(Index);
            else
                return null;

        }
        public string GetMapLable(int Index)
        {
            return _GetMainMapLable(Index);
        }
        public string GetServiceInterFace(int Index)
        {
            return _GetMainServiceInterFace(class_SelectAllModel.class_SubList[Index]);
        }
        public string GetServiceImpl(int Index)
        {
            return _GetMainServiceImpl(Index);
        }
        public string GetModel(int Index)
        {
            return _GetMainModel(Index);
        }
        public string GetDTO(int Index)
        {
            return _GetMainDTO(Index);
        }
        public string GetDAO(int Index)
        {
            return _GetMainDAO(class_SelectAllModel.class_SubList[Index]);
        }
        public string GetControl(int Index)
        {
            return _GetMainControl(class_SelectAllModel.class_SubList[Index]);
        }
        public string GetFeignControl(int Index)
        {
            return _GetMainFeignControl(class_SelectAllModel.class_SubList[Index]);
        }
        public string GetTestUnit(int Index)
        {
            throw new NotImplementedException();
        }

        public void IniClass_OutFields()
        {
            AddLinkFieldInfo();
        }

        public int GetLinkFieldInfosCount()
        {
            return class_SelectAllModel.GetLinkFieldInfosCount();
        }

        public void AddAllOutFieldName()
        {
            class_SelectAllModel.AddAllOutFieldName();
        }
        #endregion
    }
}

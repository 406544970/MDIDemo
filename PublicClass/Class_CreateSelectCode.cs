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
    public class Class_CreateSelectCode : IClass_InterFaceCreateCode, IClass_CreateFrontPage
    {
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
        private string _GetSelectType(IClass_InterFaceDataBase class_InterFaceDataBase)
        {
            string ResultValue = null;
            int Counter = 0;
            if (class_SelectAllModel.class_SubList != null)
            {
                foreach (Class_Sub class_Sub in class_SelectAllModel.class_SubList)
                {
                    int Index = 0;
                    while (Index < class_Sub.class_Fields.Count)
                    {
                        if (class_Sub.class_Fields[Index].SelectSelect)
                        {
                            if (Counter == 0)
                                ResultValue = class_InterFaceDataBase.GetDataTypeByFunction(class_Sub.class_Fields[Index].FunctionName, class_Sub.class_Fields[Index].ReturnType);
                            Counter++;
                        }
                        if (Counter > 1)
                            break;
                        Index++;
                    }
                }
                if (Counter > 1)
                    ResultValue = "mult";
            }
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
                                WhereIsNull = class_Field.WhereIsNull,
                                TableName = item.TableName,
                                FieldType = class_Field.FieldType,
                                WhereTrim = class_Field.WhereTrim
                            };
                            class_WhereFields.Add(class_WhereField);
                        }
                    }
                }
                Counter++;
            }

            #region
            #endregion

            return class_WhereFields;
        }
        private string _GetServiceReturnType(Class_Sub class_Main, bool HavePackageName = true, bool SimpleSign = true)
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
            string ResultType = _GetSelectType(class_InterFaceDataBase);
            if (ResultType == "mult")
            {
                if (HavePackageName)
                    stringBuilder.AppendFormat("{0}.model.{1}"
                    , class_SelectAllModel.AllPackerName
                    , class_Main.ModelClassName);
                else
                    stringBuilder.Append(class_Main.ModelClassName);
            }
            else
            {
                if (SimpleSign)
                    stringBuilder.AppendFormat("{0}"
                    , Class_Tool.GetSimplificationJavaType(
                        class_InterFaceDataBase.GetJavaType(ResultType)));
                else
                    stringBuilder.AppendFormat("{0}"
                    , Class_Tool.GetJavaTypeByClosedType(
                        class_InterFaceDataBase.GetJavaType(ResultType)));

            }
            return stringBuilder.ToString();
        }
        private string _GetTestWhereSql()
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
            int WhereCounter = 0;
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
                        string NowWhere = " ";
                        if (WhereCounter++ > 0)
                        {
                            NowWhere += "\r\n" + class_ToolSpace.GetSetSpaceCount(2) + class_Field.WhereType;
                        }
                        NowWhere += " " + FieldName + " ";
                        if (class_Field.LogType.IndexOf("IN") > -1)
                        {
                            NowWhere += string.Format(" {0} (\'\',\'\')", class_Field.LogType);
                        }
                        else if (class_Field.LogType.IndexOf("NULL") > -1)
                        {
                            NowWhere += string.Format(" {0}", class_Field.LogType);
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
                                if (LikeType != -100)
                                {
                                    NowWhere = NowWhere + class_InterFaceDataBase.GetLikeString("\"\"", LikeType);
                                }
                                else
                                {
                                    if (class_InterFaceDataBase.IsAddPoint(class_Field.ReturnType, class_Field.WhereValue))
                                        NowWhere = NowWhere + "'{0}'";
                                    else
                                        NowWhere = NowWhere + "0";
                                }
                            }
                            else
                            {
                                if (class_InterFaceDataBase.IsAddPoint(class_Field.ReturnType, class_Field.WhereValue))
                                    NowWhere = NowWhere + string.Format("'{0}'", class_Field.WhereValue);
                                else
                                    NowWhere = NowWhere + string.Format("{0}", class_Field.WhereValue);
                            }
                        }
                        if (NowWhere != null)
                        {
                            if (stringBuilderWhereAnd.Length == 0)
                                stringBuilderWhereAnd.AppendFormat("{0}WHERE", class_ToolSpace.GetSetSpaceCount(2));
                            stringBuilderWhereAnd.Append(" " + NowWhere);
                        }
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
                        if (class_Field.SelectSelect)
                            class_OrderBy.FunctionName = class_Field.FunctionName;
                        class_OrderBies.Add(class_OrderBy);
                    }
                    #endregion
                }
                CurPageIndex++;
            }
            if (stringBuilderWhereAnd.Length > 0)
                stringBuilderWhereAnd.Append("\r\n");
            if (stringBuilderWhereOr.Length > 0)
            {
                stringBuilderWhereAnd.Append(stringBuilderWhereOr.ToString());
            }
            if (stringBuilderGroup.Length > 0)
            {
                stringBuilderWhereAnd.AppendFormat("{0}GROUP BY ", class_ToolSpace.GetSetSpaceCount(2));
                stringBuilderWhereAnd.Append(stringBuilderGroup.ToString() + "\r\n");
            }
            if (stringBuilderHaving.Length > 0)
            {
                stringBuilderWhereAnd.AppendFormat("{0}HAVING ", class_ToolSpace.GetSetSpaceCount(2));
                stringBuilderWhereAnd.Append(stringBuilderHaving.ToString() + "\r\n");
            }
            class_OrderBies = class_OrderBies.OrderBy(a => a.SortNo).ToList();
            foreach (Class_OrderBy row in class_OrderBies)
            {
                if (row.FunctionName != null && row.FunctionName.Length > 0)
                {
                    if (class_InterFaceDataBase.IsPolymerization(row.FunctionName))
                        stringBuilderOrder.AppendFormat(",{0}{1}"
                            , string.Format(row.FunctionName.Replace("?", row.FieldName))
                            , row.SortType);
                }
                else
                    stringBuilderOrder.AppendFormat(",{0}{1}", row.FieldName, row.SortType);
            }
            if (class_OrderBies.Count > 0)
            {
                stringBuilderWhereAnd.AppendFormat("{0}ORDER BY ", class_ToolSpace.GetSetSpaceCount(2));
                stringBuilderWhereAnd.Append(stringBuilderOrder.ToString().Substring(1));
            }
            if (stringBuilderWhereAnd.Length > 0)
                return stringBuilderWhereAnd.ToString();
            else
                return null;
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
                        if (class_Field.WhereType == "AND" || class_Field.WhereType == "OR")
                        {
                            if (class_Field.WhereIsNull && class_Field.WhereValue.Equals("参数") && class_Field.LogType.IndexOf("NULL") == -1)
                            {
                                IfLabel = string.Format("{1}<if test=\"{0} != null\">\r\n"
                                , InParaFieldName, class_ToolSpace.GetSetSpaceCount(3));
                            }
                        }
                        NowWhere = string.Format("{0} {2} {1} "
                            , class_ToolSpace.GetSetSpaceCount((class_Field.WhereType == "AND" || class_Field.WhereType == "OR") ? 4 : 5)
                            , FieldName
                            , class_Field.WhereType);
                        if (class_Field.LogType.IndexOf("IN") > -1)
                        {
                            NowWhere += string.Format("{2}\r\n{0}<foreach item = \"item\" index = \"index\" collection = \"{1}\" open = \"(\" separator = \", \" close = \")\" >\r\n"
                                , class_ToolSpace.GetSetSpaceCount((class_Field.WhereType == "AND" || class_Field.WhereType == "OR") ? 4 : 5)
                                , InParaFieldName
                                , class_Field.LogType);
                            NowWhere += class_ToolSpace.GetSetSpaceCount((class_Field.WhereType == "AND" || class_Field.WhereType == "OR") ? 5 : 6) + "#{item}\r\n";
                            NowWhere += string.Format("{0}</foreach>\r\n", class_ToolSpace.GetSetSpaceCount((class_Field.WhereType == "AND" || class_Field.WhereType == "OR") ? 4 : 5));
                            if (class_Field.WhereIsNull)
                            {
                                NowWhere += string.Format("{0}</if>\r\n", class_ToolSpace.GetSetSpaceCount(3));
                            }
                        }
                        else if (class_Field.LogType.IndexOf("NULL") > -1)
                        {
                            NowWhere += class_Field.LogType + "\r\n";
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
                                string XmlFieldString = "#{" + string.Format("{0},jdbcType={1}"
                                    , InParaFieldName
                                    , Class_Tool.GetJdbcType(class_InterFaceDataBase.GetJavaType(class_Field.ReturnType))) + "}";
                                if ((LikeType < -99) && (class_Field.LogType.IndexOf("NULL") == -1))
                                    NowWhere = NowWhere + XmlFieldString;
                                else
                                    NowWhere = NowWhere + class_InterFaceDataBase.GetLikeString(XmlFieldString, LikeType);
                            }
                            else
                            {
                                if (class_InterFaceDataBase.IsAddPoint(class_Field.ReturnType, class_Field.WhereValue))
                                    NowWhere = NowWhere + string.Format("'{0}'", class_Field.WhereValue);
                                else
                                    NowWhere = NowWhere + string.Format("{0}", class_Field.WhereValue);
                            }
                            if ((class_Field.LogType.IndexOf("<") > -1) || (class_Field.LogType.IndexOf("&") > -1))
                                NowWhere = string.Format("{0}<![CDATA[{1}]]>\r\n", class_ToolSpace.GetSetSpaceCount(4), NowWhere.Trim());
                            else
                                NowWhere += "\r\n";
                            if (class_Field.WhereType == "AND" || class_Field.WhereType == "OR")
                            {
                                if (class_Field.WhereIsNull && class_Field.WhereValue.Equals("参数") && class_Field.LogType.IndexOf("IN") == -1)
                                {
                                    NowWhere += string.Format("{0}</if>\r\n", class_ToolSpace.GetSetSpaceCount(3));
                                }
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
                        if (class_Field.SelectSelect)
                            class_OrderBy.FunctionName = class_Field.FunctionName;
                        class_OrderBies.Add(class_OrderBy);
                    }
                    #endregion
                }
                CurPageIndex++;
            }
            if (stringBuilderWhereOr.Length > 0)
            {
                stringBuilderWhereAnd.Append(stringBuilderWhereOr.ToString());
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
                if (row.FunctionName != null && row.FunctionName.Length > 0)
                {
                    if (class_InterFaceDataBase.IsPolymerization(row.FunctionName))
                        stringBuilderOrder.AppendFormat(",{0}{1}"
                            , string.Format(row.FunctionName.Replace("?", row.FieldName))
                            , row.SortType);
                }
                else
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
        private string _GetDAO(int PageIndex)
        {
            if (class_SelectAllModel.class_SubList == null || class_SelectAllModel.class_SubList.Count < PageIndex)
                return null;
            Class_Sub class_Sub = class_SelectAllModel.class_SubList[PageIndex];
            if (class_Sub == null)
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
            if (!class_Sub.CreateMainCode)
            {
                stringBuilder.Append("/**\r\n");
                stringBuilder.AppendFormat(_GetAuthor());
                stringBuilder.AppendFormat(" * @create {0}\r\n", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                stringBuilder.Append(" * @function\r\n * @editLog\r\n");
                stringBuilder.Append(" */\r\n");
                stringBuilder.Append("@Mapper\r\n");
                stringBuilder.Append(string.Format("public interface {0}", class_Sub.DaoClassName));
                stringBuilder.Append(" {\r\n");
            }

            stringBuilder.AppendFormat("{0}/**\r\n", class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0} * {1}\r\n{0} *\r\n", class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.MethodContent);
            class_WhereFields = _GetParameterType();
            if (class_WhereFields != null)
            {
                if (class_WhereFields.Count > 1)
                {
                    stringBuilder.AppendFormat("{0} * @param {1} {2}\r\n"
                    , class_ToolSpace.GetSetSpaceCount(1)
                    , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName)
                    , "输入参数");
                }
                else
                {
                    if (class_WhereFields.Count > 0)
                        stringBuilder.AppendFormat("{0} * @param {1} {2}\r\n"
                    , class_ToolSpace.GetSetSpaceCount(1)
                    , Class_Tool.GetFirstCodeLow(class_WhereFields[0].OutFieldName)
                    , class_WhereFields[0].FieldRemark);
                }
            }
            stringBuilder.AppendFormat("{0} * @return {1}\r\n", class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.ServiceInterFaceReturnRemark);
            stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));

            if (class_Sub.ServiceInterFaceReturnCount == 0)
            {
                if (class_SelectAllModel.IsMultTable)
                    stringBuilder.AppendFormat("{0}{1}", class_ToolSpace.GetSetSpaceCount(1)
                        , class_Sub.DtoClassName);
                else
                    stringBuilder.AppendFormat("{0}{1}", class_ToolSpace.GetSetSpaceCount(1)
                    , _GetServiceReturnType(class_Sub, false));

            }
            else
            {
                if (class_SelectAllModel.IsMultTable)
                    stringBuilder.AppendFormat("{0}List<{1}>", class_ToolSpace.GetSetSpaceCount(1)
                    , class_Sub.DtoClassName);
                else
                    stringBuilder.AppendFormat("{0}List<{1}>", class_ToolSpace.GetSetSpaceCount(1)
                    , _GetServiceReturnType(class_Sub, false));
            }
            if (class_WhereFields != null)
            {
                if (class_WhereFields.Count > 1)
                {
                    stringBuilder.AppendFormat(" {0}({1} {2});\r\n"
                        , class_Sub.MethodId
                        , class_Sub.ParamClassName
                        , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName));
                }
                else if (class_WhereFields.Count == 1)
                {
                    if (class_WhereFields[0].FieldLogType.IndexOf("IN") > -1)
                        stringBuilder.AppendFormat(" {0}(@Param(\"{2}\") List<{1}> {2});\r\n"
                        , class_Sub.MethodId
                        , Class_Tool.GetClosedJavaType(class_InterFaceDataBase.GetJavaType(class_WhereFields[0].LogType))
                        , class_WhereFields[0].OutFieldName);
                    else
                        stringBuilder.AppendFormat(" {0}(@Param(\"{2}\") {1} {2});\r\n"
                        , class_Sub.MethodId
                        , Class_Tool.GetClosedJavaType(class_InterFaceDataBase.GetJavaType(class_WhereFields[0].LogType))
                        , class_WhereFields[0].OutFieldName);
                }
                else
                    stringBuilder.AppendFormat(" {0}();\r\n"
                        , class_Sub.MethodId);

                #region 加入汇总代码
                if (class_Sub.ServiceInterFaceReturnCount > 0 && class_SelectAllModel.ReturnStructure
                && (class_SelectAllModel.ReturnStructureType == 2
                || class_SelectAllModel.ReturnStructureType == 3))
                {
                    stringBuilder.AppendFormat("\r\n{0}/**\r\n", class_ToolSpace.GetSetSpaceCount(1));
                    stringBuilder.AppendFormat("{0} * {1}汇总功能\r\n{0} *\r\n", class_ToolSpace.GetSetSpaceCount(1)
                        , class_Sub.MethodContent);
                    stringBuilder.AppendFormat("{0} * @param oldSql 原始SQL语句\r\n"
                            , class_ToolSpace.GetSetSpaceCount(1));
                    stringBuilder.AppendFormat("{0} * @return {1}\r\n", class_ToolSpace.GetSetSpaceCount(1)
                        , class_Sub.ServiceInterFaceReturnRemark);
                    stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));
                    stringBuilder.AppendFormat("{0}LinkedHashMap {1}Total(@Param(\"oldSql\") String oldSql);\r\n"
                        , class_ToolSpace.GetSetSpaceCount(1)
                        , class_Sub.MethodId);
                }
                #endregion
            }
            if (!class_Sub.CreateMainCode)
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
                if (!class_Main.CreateMainCode)
                {
                    stringBuilder.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>\r\n");
                    stringBuilder.Append("<!DOCTYPE mapper PUBLIC \"-//mybatis.org//DTD Mapper 3.0//EN\" \"http://mybatis.org/dtd/mybatis-3-mapper.dtd\" >\r\n");
                    stringBuilder.AppendFormat("<mapper namespace=\"{0}.dao.{1}\">\r\n"
                        , class_SelectAllModel.AllPackerName
                        , class_Main.DaoClassName);
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
                    , class_Main.ModelClassName);
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
                if (!class_Main.CreateMainCode)
                    stringBuilder.Append("</mapper>\r\n");
                if (stringBuilder.Length > 0)
                    return stringBuilder.ToString();
                else
                    return null;
            }
            else
                return null;
        }
        public string GetTestSql(int PageIndex)
        {
            return _GetTestSql(PageIndex, true);
        }
        private string _GetTestSql(int PageIndex, bool AddTotal)
        {
            if (class_SelectAllModel.class_SubList.Count < PageIndex)
                return null;
            Class_Sub class_Sub = class_SelectAllModel.class_SubList[PageIndex];
            if (class_Sub == null)
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

            #region Select
            stringBuilder.AppendFormat("{0}SELECT\r\n", class_ToolSpace.GetSetSpaceCount(2));
            int Counter = 0;
            int CurPageIndex = 0;
            foreach (Class_Sub item in class_SelectAllModel.class_SubList)
            {
                string AliasName = item.AliasName;
                foreach (Class_Field class_Field in item.class_Fields)
                {
                    if (class_Field.SelectSelect)
                    {
                        string FieldName = class_Field.FieldName;
                        string MyFieldName = null;
                        if (class_SelectAllModel.GetHaveSameFieldName(class_Field.ParaName, CurPageIndex))
                        {
                            MyFieldName = class_Field.MultFieldName;
                        }
                        else
                        {
                            MyFieldName = class_Field.ParaName;
                        }
                        if (class_SelectAllModel.IsMultTable)
                        {
                            FieldName = AliasName + "." + FieldName;
                        }

                        if ((class_Field.CaseWhen != null) && (class_Field.CaseWhen.Length > 0))
                        {
                            Class_CaseWhen class_CaseWhen = new Class_CaseWhen();
                            FieldName = class_CaseWhen.GetCaseWhenContent(class_Field.CaseWhen, FieldName, class_ToolSpace.GetSetSpaceCount(3));
                            //if (MyFieldName != null)
                            //    FieldName = FieldName + " AS " + MyFieldName;
                        }
                        if ((class_Field.FunctionName != null) && (class_Field.FunctionName.Length > 0))
                        {
                            if (MyFieldName != null)
                                FieldName = string.Format(class_Field.FunctionName.Replace("?", "{0}"), FieldName);
                            else
                                FieldName = string.Format(class_Field.FunctionName.Replace("?", "{0}"), FieldName);
                        }
                        if (!FieldName.Equals(MyFieldName))
                        {
                            FieldName = string.Format(FieldName + " AS {0}", MyFieldName);
                        }
                        if (Counter++ > 0)
                            stringBuilder.AppendFormat("{1},{0}\r\n", FieldName, class_ToolSpace.GetSetSpaceCount(3));
                        else
                            stringBuilder.AppendFormat("{1}{0}\r\n", FieldName, class_ToolSpace.GetSetSpaceCount(3));
                    }
                }
                CurPageIndex++;
            }
            #endregion

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
            string TestSql = _GetTestWhereSql();
            if (TestSql != null)
                stringBuilder.Append(TestSql);
            #endregion

            #region 加入汇总
            if (AddTotal)
            {
                if (class_Sub.ServiceInterFaceReturnCount > 0
                    && class_SelectAllModel.ReturnStructure
                    && (class_SelectAllModel.ReturnStructureType == 2
                    || class_SelectAllModel.ReturnStructureType == 3))
                {
                    stringBuilder.AppendFormat("\r\n\r\n{0}SELECT\r\n", class_ToolSpace.GetSetSpaceCount(2));
                    Counter = 0;
                    CurPageIndex = 0;
                    foreach (Class_Sub item in class_SelectAllModel.class_SubList)
                    {
                        string AliasName = item.AliasName;
                        foreach (Class_Field class_Field in item.class_Fields)
                        {
                            if (class_Field.SelectSelect && !(class_Field.CaseWhen != null && class_Field.CaseWhen.Length > 0) && !(class_Field.FunctionName != null && class_Field.FunctionName.Length > 0) && class_Field.TotalFunctionName != null && class_Field.TotalFunctionName.Length > 0 && (class_Field.FieldType.Equals("int") || class_Field.FieldType.Equals("tinyint") || class_Field.FieldType.Equals("decimal") || class_Field.FieldType.Equals("date") || class_Field.FieldType.Equals("datetime")))
                            {
                                string MyFieldName = null;
                                if (class_SelectAllModel.GetHaveSameFieldName(class_Field.ParaName, CurPageIndex))
                                {
                                    MyFieldName = class_Field.MultFieldName;
                                }
                                else
                                {
                                    MyFieldName = class_Field.ParaName;
                                }
                                MyFieldName = string.Format(class_Field.TotalFunctionName.Replace("?", MyFieldName) + " AS {0}", MyFieldName);

                                stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(3));
                                if (Counter++ > 0)
                                    stringBuilder.Append(",");
                                stringBuilder.AppendFormat("{0}\r\n", MyFieldName);
                            }
                        }
                        CurPageIndex++;
                    }
                    stringBuilder.AppendFormat("{0}FROM (\r\n", class_ToolSpace.GetSetSpaceCount(2));
                    stringBuilder.Append(_GetTestSql(PageIndex, false).Replace("        ", "            "));
                    stringBuilder.AppendFormat("\r\n{0}) as TotalTable\r\n", class_ToolSpace.GetSetSpaceCount(2));
                }
            }
            #endregion
            if (stringBuilder.Length > 0)
                return stringBuilder.ToString();
            else
                return null;
        }
        private string _GetUsedMethod()
        {
            Class_Tool class_ToolSpace = new Class_Tool();
            StringBuilder stringBuilder = new StringBuilder();
            #region
            stringBuilder.AppendFormat("{0}/**\r\n", class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0} * 加入汇总数据位置\r\n", class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0} *\r\n", class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0} * @param next                迭代器\r\n", class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0} * @param totalValueClassList 目标list\r\n", class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0} * @param i                   位置\r\n", class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0}private void addTotalSite(Map.Entry next, List<TotalValueClass> totalValueClassList, byte i) {{\r\n", class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0}TotalValueClass totalValueClass = new TotalValueClass();\r\n", class_ToolSpace.GetSetSpaceCount(2));
            stringBuilder.AppendFormat("{0}totalValueClass.setValue(next.getValue() == null ? 0 : next.getValue().toString().replace(\", \", \"\"));\r\n", class_ToolSpace.GetSetSpaceCount(2));
            stringBuilder.AppendFormat("{0}totalValueClass.setSite(i);\r\n", class_ToolSpace.GetSetSpaceCount(2));
            stringBuilder.AppendFormat("{0}totalValueClassList.add(totalValueClass);\r\n", class_ToolSpace.GetSetSpaceCount(2));
            stringBuilder.AppendFormat("{0}}}\r\n\r\n", class_ToolSpace.GetSetSpaceCount(1));
            #endregion
            return stringBuilder.ToString();
        }
        private string _GetWhereString()
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
            int CurPageIndex = 0;
            int FieldCounter = 0;
            foreach (Class_Sub item in class_SelectAllModel.class_SubList)
            {
                foreach (Class_Field class_Field in item.class_Fields)
                {
                    if (class_Field.WhereSelect)
                    {
                        //stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(3));
                        if (FieldCounter > 0)
                            stringBuilder.Append(",");

                        string MyFieldName = null;
                        if (class_SelectAllModel.GetHaveSameFieldName(class_Field.ParaName, CurPageIndex))
                            MyFieldName = class_Field.MultFieldName;
                        else
                            MyFieldName = class_Field.ParaName;

                        stringBuilder.AppendFormat("{0}:\'{0}\'", MyFieldName);


                        FieldCounter++;
                    }
                }
                CurPageIndex++;
            }
            if (FieldCounter > 0)
            {
                return string.Format("{0}let myWhere = {{\r\n{1}{2}\r\n{0}}};\r\n"
                    , class_ToolSpace.GetSetSpaceCount(2)
                    , class_ToolSpace.GetSetSpaceCount(3)
                    , stringBuilder.ToString());
            }
            else
                return null;
        }
        private string _GetFrontPage()
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
            int CurPageIndex = 0;
            int FieldCounter = 0;
            foreach (Class_Sub item in class_SelectAllModel.class_SubList)
            {
                foreach (Class_Field class_Field in item.class_Fields)
                {
                    if (class_Field.SelectSelect)
                    {
                        stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(3));
                        if (FieldCounter > 0)
                            stringBuilder.Append(",");
                        stringBuilder.Append("{");

                        string MyFieldName = null;
                        if (class_SelectAllModel.GetHaveSameFieldName(class_Field.ParaName, CurPageIndex))
                            MyFieldName = class_Field.MultFieldName;
                        else
                            MyFieldName = class_Field.ParaName;

                        stringBuilder.AppendFormat("field:\'{0}\'", MyFieldName);
                        stringBuilder.AppendFormat(",title:\'{0}\'", class_Field.Title);
                        stringBuilder.AppendFormat(",width:{0}", class_Field.Width);
                        if (!class_Field.Type.Equals("normal") && class_Field.Type.Length > 0)
                            stringBuilder.AppendFormat(",type:\'{0}\'", class_Field.Type);
                        if (class_Field.LayChecked)
                            stringBuilder.Append(",LAY_CHECKED:true");
                        if (!class_Field.Fixed.Equals("none") && class_Field.Fixed.Length > 0)
                            stringBuilder.AppendFormat(",fixed:\'{0}\'", class_Field.Fixed);
                        if (!class_Field.IsDisplay)
                            stringBuilder.Append(",hide:true");
                        if (class_Field.Sort)
                            stringBuilder.Append(",sort:true");
                        if (class_Field.UnResize)
                            stringBuilder.Append(",unresize:true");

                        if (class_Field.Style.Length > 0)
                            stringBuilder.AppendFormat(",style:\'{0}\'", class_Field.Style);
                        stringBuilder.AppendFormat(",align:\'{0}\'", class_Field.Align.Length == 0 ? "center" : class_Field.Align);
                        if (class_Field.ToolBar.Length > 0)
                            stringBuilder.AppendFormat(",toolBar:\'{0}\'", class_Field.ToolBar);

                        stringBuilder.Append("}\r\n");
                        FieldCounter++;
                    }
                }
                CurPageIndex++;
            }
            if (FieldCounter > 0)
            {
                return string.Format("{0}let myColos = [[\r\n{1}{0}]];\r\n"
                    , class_ToolSpace.GetSetSpaceCount(2)
                    , stringBuilder.ToString());
            }
            else
                return null;
        }
        private string _GetSql(int PageIndex)
        {
            if (class_SelectAllModel.class_SubList.Count < PageIndex)
                return null;
            Class_Sub class_Sub = class_SelectAllModel.class_SubList[PageIndex];
            if (class_Sub == null)
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
            if (class_Sub.ResultType > 0 && !class_Sub.CreateMainCode)
            {
                stringBuilder.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>\r\n");
                stringBuilder.Append("<!DOCTYPE mapper PUBLIC \"-//mybatis.org//DTD Mapper 3.0//EN\" \"http://mybatis.org/dtd/mybatis-3-mapper.dtd\" >\r\n");
                stringBuilder.AppendFormat("<mapper namespace=\"{0}.dao.{1}\">\r\n"
                    , class_SelectAllModel.AllPackerName
                    , class_Sub.DaoClassName);
            }
            #region SelectId
            stringBuilder.AppendFormat("{1}<!-- 注释：{0} -->\r\n", class_Sub.MethodContent, class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0}<select id=\"{1}\" "
                , class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.MethodId);
            #endregion

            #region resultMap resultType
            if (class_Sub.ResultType == 0)
            {
                stringBuilder.AppendFormat("resultMap=\"{0}Map\""
                    , class_Sub.ResultMapId);
            }
            else
            {
                if (class_SelectAllModel.IsMultTable)
                    stringBuilder.AppendFormat("resultType=\"{0}.dto.{1}\""
                    , class_SelectAllModel.AllPackerName
                        , class_Sub.DtoClassName);
                else
                    stringBuilder.AppendFormat("resultType=\"{0}\""
                        , _GetServiceReturnType(class_Sub, true, false));
            }
            #endregion

            #region parameterType
            class_WhereFields = _GetParameterType();
            if (class_WhereFields != null)
            {
                if (class_WhereFields.Count > 1)
                {
                    stringBuilder.AppendFormat(" parameterType=\"{0}.model.InPutParam.{1}\">\r\n"
                    , class_SelectAllModel.AllPackerName
                    , class_Sub.ParamClassName);
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

            #region Select
            stringBuilder.AppendFormat("{0}SELECT\r\n", class_ToolSpace.GetSetSpaceCount(2));
            int Counter = 0;
            int CurPageIndex = 0;
            foreach (Class_Sub item in class_SelectAllModel.class_SubList)
            {
                string AliasName = item.AliasName;
                foreach (Class_Field class_Field in item.class_Fields)
                {
                    if (class_Field.SelectSelect)
                    {
                        string FieldName = class_Field.FieldName;
                        string MyFieldName = null;
                        if (class_SelectAllModel.GetHaveSameFieldName(class_Field.ParaName, CurPageIndex))
                        {
                            MyFieldName = class_Field.MultFieldName;
                        }
                        else
                        {
                            MyFieldName = class_Field.ParaName;
                        }
                        if (class_SelectAllModel.IsMultTable)
                        {
                            FieldName = AliasName + "." + FieldName;
                        }

                        if ((class_Field.CaseWhen != null) && (class_Field.CaseWhen.Length > 0))
                        {
                            Class_CaseWhen class_CaseWhen = new Class_CaseWhen();
                            FieldName = class_CaseWhen.GetCaseWhenContent(class_Field.CaseWhen, FieldName, class_ToolSpace.GetSetSpaceCount(3));
                            //if (MyFieldName != null)
                            //    FieldName = FieldName + " AS " + MyFieldName;
                        }
                        if ((class_Field.FunctionName != null) && (class_Field.FunctionName.Length > 0))
                        {
                            if (MyFieldName != null)
                                FieldName = string.Format(class_Field.FunctionName.Replace("?", "{0}"), FieldName);
                            else
                                FieldName = string.Format(class_Field.FunctionName.Replace("?", "{0}"), FieldName);
                        }
                        if (!FieldName.Equals(MyFieldName))
                        {
                            FieldName = string.Format(FieldName + " AS {0}", MyFieldName);
                        }
                        if (Counter++ > 0)
                            stringBuilder.AppendFormat("{1},{0}\r\n", FieldName, class_ToolSpace.GetSetSpaceCount(3));
                        else
                            stringBuilder.AppendFormat("{1}{0}\r\n", FieldName, class_ToolSpace.GetSetSpaceCount(3));
                    }
                }
                CurPageIndex++;
            }
            #endregion

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

            #region 加入汇总代码
            if (class_Sub.ServiceInterFaceReturnCount > 0 && class_SelectAllModel.ReturnStructure
                && (class_SelectAllModel.ReturnStructureType == 2
                || class_SelectAllModel.ReturnStructureType == 3))
            {
                stringBuilder.AppendFormat("\r\n{1}<!-- 注释：{0}汇总 -->\r\n", class_Sub.MethodContent, class_ToolSpace.GetSetSpaceCount(1));
                stringBuilder.AppendFormat("{0}<select id=\"{1}Total\" parameterType=\"java.lang.String\" resultType=\"java.util.LinkedHashMap\">\r\n"
                    , class_ToolSpace.GetSetSpaceCount(1)
                    , class_Sub.MethodId);
                stringBuilder.AppendFormat("{0}SELECT\r\n", class_ToolSpace.GetSetSpaceCount(2));

                Counter = 0;
                CurPageIndex = 0;
                foreach (Class_Sub item in class_SelectAllModel.class_SubList)
                {
                    string AliasName = item.AliasName;
                    foreach (Class_Field class_Field in item.class_Fields)
                    {
                        if (class_Field.SelectSelect
                            && !(class_Field.CaseWhen != null && class_Field.CaseWhen.Length > 0)
                            && !(class_Field.FunctionName != null && class_Field.FunctionName.Length > 0)
                            && class_Field.TotalFunctionName != null
                            && class_Field.TotalFunctionName.Length > 0
                            && (class_Field.FieldType.Equals("int") || class_Field.FieldType.Equals("tinyint")
                            || class_Field.FieldType.Equals("decimal") || class_Field.FieldType.Equals("date")
                            || class_Field.FieldType.Equals("datetime")))
                        {
                            string MyFieldName = null;
                            if (class_SelectAllModel.GetHaveSameFieldName(class_Field.ParaName, CurPageIndex))
                            {
                                MyFieldName = class_Field.MultFieldName;
                            }
                            else
                            {
                                MyFieldName = class_Field.ParaName;
                            }
                            MyFieldName = string.Format(class_Field.TotalFunctionName.Replace("?", MyFieldName) + " AS {0}", MyFieldName);

                            stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(3));
                            if (Counter++ > 0)
                                stringBuilder.Append(",");
                            stringBuilder.AppendFormat("{0}\r\n", MyFieldName);
                        }
                    }
                    CurPageIndex++;
                }

                stringBuilder.AppendFormat("{0}FROM (\r\n", class_ToolSpace.GetSetSpaceCount(2));
                stringBuilder.AppendFormat("{0}  ${{oldSql}}\r\n", class_ToolSpace.GetSetSpaceCount(2));
                stringBuilder.AppendFormat("{0}) as TotalTable\r\n", class_ToolSpace.GetSetSpaceCount(2));

                stringBuilder.AppendFormat("{0}</select>\r\n", class_ToolSpace.GetSetSpaceCount(1));
            }
            #endregion

            if (class_Sub.ResultType > 0 && !class_Sub.CreateMainCode)
            {
                stringBuilder.Append("</mapper>\r\n");
            }
            if (stringBuilder.Length > 0)
                return stringBuilder.ToString();
            else
                return null;
        }
        private int _GetWhereFieldCount()
        {
            int Counter = 0;
            if (class_SelectAllModel.class_SubList == null)
                return Counter;
            foreach (Class_Sub class_Sub in class_SelectAllModel.class_SubList)
            {
                Counter += class_Sub != null ? class_Sub.class_Fields.Count : 0;
            }
            return Counter;
        }
        private string _GetModel(int PageIndex)
        {
            if (class_SelectAllModel.IsMultTable)
                return null;
            if (class_SelectAllModel.class_SubList == null || class_SelectAllModel.class_SubList.Count < PageIndex)
                return null;

            int Counter = _GetWhereFieldCount();
            if (Counter == 0)
                return null;
            Counter = 0;
            foreach (Class_Field row in class_SelectAllModel.class_SubList[PageIndex].class_Fields)
                if (row.SelectSelect)
                    Counter++;
            if (Counter < 2 && class_SelectAllModel.class_SubList[PageIndex].ResultType == 1)
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
            stringBuilder.AppendFormat("public class {0}", class_SelectAllModel.class_SubList[PageIndex].ModelClassName);
            stringBuilder.Append(" {\r\n");

            //加入字段
            int FieldCount = 0;
            foreach (Class_Field class_Field in class_SelectAllModel.class_SubList[PageIndex].class_Fields)
            {
                if (class_Field.SelectSelect)
                {
                    string ReturnType = class_Field.ReturnType;
                    if ((class_Field.CaseWhen != null) && (class_Field.CaseWhen.Length > 0))
                        ReturnType = "varchar";
                    ReturnType = Class_Tool.GetClosedJavaType(class_InterFaceDataBase.GetJavaType(class_InterFaceDataBase.GetDataTypeByFunction(class_Field.FunctionName, ReturnType)));
                    stringBuilder.AppendFormat("{0}/**\r\n", class_ToolSpace.GetSetSpaceCount(1));
                    stringBuilder.AppendFormat("{0} * {1}\r\n", class_ToolSpace.GetSetSpaceCount(1), class_Field.FieldRemark);
                    stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));
                    stringBuilder.AppendFormat("{0}private", class_ToolSpace.GetSetSpaceCount(1));
                    if (class_SelectAllModel.class_Create.EnglishSign && Class_Tool.IsEnglishField(class_Field.ParaName))
                        stringBuilder.Append(" transient");
                    stringBuilder.AppendFormat(" {0} {1};\r\n"
                        , ReturnType
                        , class_Field.ParaName);
                    FieldCount++;
                }
            }
            if (FieldCount > 0)
            {
                //stringBuilder.Append("\r\n");
                foreach (Class_Field class_Field in class_SelectAllModel.class_SubList[PageIndex].class_Fields)
                {
                    if (class_Field.SelectSelect)
                    {
                        string ReturnType = class_Field.ReturnType;
                        if ((class_Field.CaseWhen != null) && (class_Field.CaseWhen.Length > 0))
                            ReturnType = "varchar";
                        ReturnType = Class_Tool.GetClosedJavaType(class_InterFaceDataBase.GetJavaType(class_InterFaceDataBase.GetDataTypeByFunction(class_Field.FunctionName, ReturnType)));

                        #region Get
                        stringBuilder.AppendFormat("\r\n{0}public {2} get{1}()"
                            , class_ToolSpace.GetSetSpaceCount(1)
                            , Class_Tool.GetFirstCodeUpper(class_Field.ParaName)
                            , ReturnType);
                        stringBuilder.Append("{\r\n");
                        stringBuilder.AppendFormat("{0}return {1};\r\n"
                            , class_ToolSpace.GetSetSpaceCount(2)
                            , Class_Tool.GetFirstCodeLow(class_Field.ParaName));
                        stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(1));
                        stringBuilder.Append("}\r\n\r\n");
                        #endregion

                        #region Set
                        stringBuilder.AppendFormat("{0}public void set{1}({3} {2})"
                            , class_ToolSpace.GetSetSpaceCount(1)
                            , Class_Tool.GetFirstCodeUpper(class_Field.ParaName)
                            , Class_Tool.GetFirstCodeLow(class_Field.ParaName)
                            , ReturnType);
                        stringBuilder.Append("{\r\n");
                        stringBuilder.AppendFormat("{0}this.{1} = {1};\r\n"
                            , class_ToolSpace.GetSetSpaceCount(2)
                            , Class_Tool.GetFirstCodeLow(class_Field.ParaName));
                        stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(1));
                        stringBuilder.Append("}\r\n");
                        #endregion
                    }
                }
            }
            stringBuilder.Append("}\r\n");

            return stringBuilder.ToString();
        }
        private string _GetServiceImpl(int PageIndex)
        {
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


            if (!class_Sub.CreateMainCode)
            {
                stringBuilder.Append("/**\r\n");
                stringBuilder.AppendFormat(_GetAuthor());
                stringBuilder.AppendFormat(" * @create {0}\r\n", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                stringBuilder.Append(" * @function\r\n * @editLog\r\n");
                stringBuilder.Append(" */\r\n");
                stringBuilder.Append("@SuppressWarnings(\"SpringJavaInjectionPointsAutowiringInspection\")\r\n@Service\r\n");
                stringBuilder.AppendFormat("public class {1} implements {0}"
                    , class_Sub.ServiceInterFaceName
                    , class_Sub.ServiceClassName);
                stringBuilder.Append(" {\r\n");
                stringBuilder.AppendFormat("{0}@Autowired\r\n", class_ToolSpace.GetSetSpaceCount(1));
                stringBuilder.AppendFormat("{0}{1} {2};\r\n", class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.DaoClassName
                , Class_Tool.GetFirstCodeLow(class_Sub.DaoClassName));
            }
            if (class_Sub.ServiceInterFaceReturnCount > 0
                && class_SelectAllModel.ReturnStructure
                && (class_SelectAllModel.ReturnStructureType == 3
                || class_SelectAllModel.ReturnStructureType == 2))
            {
                stringBuilder.AppendFormat("{0}@Autowired\r\n", class_ToolSpace.GetSetSpaceCount(1));
                stringBuilder.AppendFormat("{0}SqlSessionFactory sqlSessionFactory;\r\n", class_ToolSpace.GetSetSpaceCount(1));
                stringBuilder.Append("\r\n");
                stringBuilder.AppendFormat("{0}private MybatisSqlHelper mybatisSqlHelper;\r\n", class_ToolSpace.GetSetSpaceCount(1));
                stringBuilder.Append("\r\n");
            }
            stringBuilder.AppendFormat("{0}/**\r\n", class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0} * {1}\r\n{0} *\r\n", class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.MethodContent);

            List<Class_WhereField> class_WhereFields = new List<Class_WhereField>();
            class_WhereFields = _GetParameterType();
            if (class_WhereFields != null && class_WhereFields.Count > 0)
            {
                if (class_WhereFields.Count > 1)
                {
                    stringBuilder.AppendFormat("{0} * @param {1} {2}\r\n"
                    , class_ToolSpace.GetSetSpaceCount(1)
                    , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName)
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

            stringBuilder.AppendFormat("{0} * @return {1}\r\n", class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.ServiceInterFaceReturnRemark);
            stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0}@Override\r\n{0}public ", class_ToolSpace.GetSetSpaceCount(1));
            if (class_Sub.ServiceInterFaceReturnCount == 0)
            {
                if (class_SelectAllModel.IsMultTable)
                    stringBuilder.AppendFormat("{0}", class_Sub.DtoClassName);
                else
                    stringBuilder.AppendFormat("{0}", _GetServiceReturnType(class_Sub, false));
            }
            else
            {
                if (class_SelectAllModel.IsMultTable)
                    stringBuilder.AppendFormat("List<{0}>", class_Sub.DtoClassName);
                else
                    stringBuilder.AppendFormat("List<{0}>", _GetServiceReturnType(class_Sub, false));
            }

            if (class_WhereFields != null && class_WhereFields.Count > 0)
            {
                if (class_WhereFields.Count > 1)
                    stringBuilder.AppendFormat(" {0}({1} {2})"
                        , class_Sub.MethodId
                        , class_Sub.ParamClassName
                        , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName));
                else
                {
                    if (class_WhereFields[0].FieldLogType.IndexOf("IN") > -1)
                        stringBuilder.AppendFormat(" {0}(List<{1}> {2})"
                        , class_Sub.MethodId
                        , Class_Tool.GetClosedJavaType(class_InterFaceDataBase.GetJavaType(class_WhereFields[0].LogType))
                        , class_WhereFields[0].OutFieldName);
                    else
                        stringBuilder.AppendFormat(" {0}({1} {2})"
                        , class_Sub.MethodId
                        , Class_Tool.GetClosedJavaType(class_InterFaceDataBase.GetJavaType(class_WhereFields[0].LogType))
                        , class_WhereFields[0].OutFieldName);
                }
            }
            else
                stringBuilder.AppendFormat(" {0}()"
                    , class_Sub.MethodId);

            stringBuilder.Append(" {\r\n");
            stringBuilder.AppendFormat("{0}return {1}."
            , class_ToolSpace.GetSetSpaceCount(2)
            , Class_Tool.GetFirstCodeLow(class_Sub.DaoClassName));

            if (class_WhereFields != null && class_WhereFields.Count > 0)
            {
                if (class_WhereFields.Count > 1)
                {
                    stringBuilder.AppendFormat("{0}({1});\r\n"
                        , class_Sub.MethodId
                        , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName));
                }
                else
                {
                    stringBuilder.AppendFormat("{0}({1});\r\n"
                        , class_Sub.MethodId
                        , class_WhereFields[0].OutFieldName);
                }
            }
            else
                stringBuilder.AppendFormat("{0}();\r\n"
                    , class_Sub.MethodId);

            stringBuilder.AppendFormat("{0}", class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.Append("}\r\n");

            #region 加入汇总代码
            if (class_Sub.ServiceInterFaceReturnCount > 0
                && class_SelectAllModel.ReturnStructure
                && (class_SelectAllModel.ReturnStructureType == 2
                || class_SelectAllModel.ReturnStructureType == 3))
            {
                stringBuilder.AppendFormat("\r\n{0}/**\r\n", class_ToolSpace.GetSetSpaceCount(1));
                stringBuilder.AppendFormat("{0} * {1}汇总功能\r\n{0} *\r\n", class_ToolSpace.GetSetSpaceCount(1)
                    , class_Sub.MethodContent);
                class_WhereFields = _GetParameterType();
                if (class_WhereFields != null && class_WhereFields.Count > 0)
                {
                    if (class_WhereFields.Count > 1)
                    {
                        stringBuilder.AppendFormat("{0} * @param {1} {2}\r\n"
                        , class_ToolSpace.GetSetSpaceCount(1)
                        , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName)
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
                stringBuilder.AppendFormat("{0} * @return {1}\r\n", class_ToolSpace.GetSetSpaceCount(1)
                    , class_Sub.ServiceInterFaceReturnRemark);
                stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));
                stringBuilder.AppendFormat("{0}@Override\r\n", class_ToolSpace.GetSetSpaceCount(1));
                stringBuilder.AppendFormat("{0}public LinkedHashMap"
                    , class_ToolSpace.GetSetSpaceCount(1));
                if (class_WhereFields != null && class_WhereFields.Count > 0)
                {
                    if (class_WhereFields.Count > 1)
                    {
                        stringBuilder.AppendFormat(" {0}Total({1} {2})"
                            , class_Sub.MethodId
                            , class_Sub.ParamClassName
                            , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName));
                    }
                    else
                    {
                        if (class_WhereFields[0].FieldLogType.IndexOf("IN") > -1)
                            stringBuilder.AppendFormat(" {0}Total(List<{1}> {2})"
                            , class_Sub.MethodId
                            , Class_Tool.GetClosedJavaType(class_InterFaceDataBase.GetJavaType(class_WhereFields[0].LogType))
                            , class_WhereFields[0].OutFieldName);
                        else
                            stringBuilder.AppendFormat(" {0}Total({1} {2})"
                            , class_Sub.MethodId
                            , Class_Tool.GetClosedJavaType(class_InterFaceDataBase.GetJavaType(class_WhereFields[0].LogType))
                            , class_WhereFields[0].OutFieldName);
                    }
                }
                else
                    stringBuilder.AppendFormat(" {0}Total()"
                        , class_Sub.MethodId);
                stringBuilder.Append(" {\r\n");
                stringBuilder.AppendFormat("{0}mybatisSqlHelper = new MybatisSqlHelper(this.sqlSessionFactory);\r\n", class_ToolSpace.GetSetSpaceCount(2));
                stringBuilder.AppendFormat("{0}String mapperSql;\r\n", class_ToolSpace.GetSetSpaceCount(2));

                if (class_WhereFields != null && class_WhereFields.Count > 0)
                {
                    if (class_WhereFields.Count > 1)
                    {
                        stringBuilder.AppendFormat("{0}mapperSql = mybatisSqlHelper.getNamespaceSql(\"{1}.dao.{2}.{3}\", {4});\r\n"
                            , class_ToolSpace.GetSetSpaceCount(2)
                            , class_SelectAllModel.AllPackerName
                            , class_Sub.DaoClassName
                            , class_Sub.MethodId
                            , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName));
                    }
                    else
                    {
                        if (class_WhereFields[0].FieldLogType.IndexOf("IN") > -1)
                        {
                            stringBuilder.AppendFormat("{0}mapperSql = mybatisSqlHelper.getNamespaceSql(\"{1}.dao.{2}.{3}\", {4});\r\n"
                                , class_ToolSpace.GetSetSpaceCount(2)
                                , class_SelectAllModel.AllPackerName
                                , class_Sub.DaoClassName
                                , class_Sub.MethodId
                                , class_WhereFields[0].OutFieldName);
                        }
                        else
                        {
                            stringBuilder.AppendFormat("{0}if ({1} != null)"
                                , class_ToolSpace.GetSetSpaceCount(2)
                                , class_WhereFields[0].OutFieldName);
                            stringBuilder.Append(" {\r\n");
                            stringBuilder.AppendFormat("{0}Map map = new HashMap();\r\n", class_ToolSpace.GetSetSpaceCount(3));
                            stringBuilder.AppendFormat("{0}map.put(\"{1}\",{1});\r\n"
                                , class_ToolSpace.GetSetSpaceCount(3)
                                , class_WhereFields[0].OutFieldName);
                            stringBuilder.AppendFormat("{0}mapperSql = mybatisSqlHelper.getNamespaceSql(\"{1}.dao.{2}.{3}\", map);\r\n"
                                , class_ToolSpace.GetSetSpaceCount(3)
                                , class_SelectAllModel.AllPackerName
                                , class_Sub.DaoClassName
                                , class_Sub.MethodId);
                            stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(2) + "}\r\n");
                            stringBuilder.AppendFormat("{0}else\r\n", class_ToolSpace.GetSetSpaceCount(2));
                            stringBuilder.AppendFormat("{0}mapperSql = mybatisSqlHelper.getNamespaceSql(\"{1}.dao.{2}.{3}\", null);\r\n"
                                , class_ToolSpace.GetSetSpaceCount(3)
                                , class_SelectAllModel.AllPackerName
                                , class_Sub.DaoClassName
                                , class_Sub.MethodId);
                        }
                    }
                }
                else
                    stringBuilder.AppendFormat("{0}mapperSql = mybatisSqlHelper.getNamespaceSql(\"{1}.dao.{2}.{3}\", null);\r\n"
                        , class_ToolSpace.GetSetSpaceCount(2)
                        , class_SelectAllModel.AllPackerName
                        , class_Sub.DaoClassName
                        , class_Sub.MethodId);


                stringBuilder.AppendFormat("{0}return {1}.{2}Total(mapperSql);\r\n"
                    , class_ToolSpace.GetSetSpaceCount(2)
                    , Class_Tool.GetFirstCodeLow(class_Sub.DaoClassName)
                    , class_Sub.MethodId);
                stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(1) + "}\r\n");
            }
            #endregion

            if (!class_Sub.CreateMainCode)
                stringBuilder.Append("}\r\n");
            return stringBuilder.ToString();
        }
        private string _GetServiceInterFace(int PageIndex)
        {
            Class_Sub class_Sub = class_SelectAllModel.class_SubList[PageIndex];
            if (class_Sub == null)
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
            if (!class_Sub.CreateMainCode)
            {
                stringBuilder.Append("/**\r\n");
                stringBuilder.AppendFormat(_GetAuthor());
                stringBuilder.AppendFormat(" * @create {0}\r\n", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                stringBuilder.Append(" * @function\r\n * @editLog\r\n");
                stringBuilder.Append(" */\r\n");
                stringBuilder.Append(string.Format("public interface {0} ", class_Sub.ServiceInterFaceName) + "{\r\n");
            }

            stringBuilder.AppendFormat("{0}/**\r\n", class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0} * {1}\r\n{0} *\r\n", class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.MethodContent);
            class_WhereFields = _GetParameterType();
            if (class_WhereFields != null && class_WhereFields.Count > 0)
            {
                if (class_WhereFields.Count > 1)
                {
                    stringBuilder.AppendFormat("{0} * @param {1} {2}\r\n"
                    , class_ToolSpace.GetSetSpaceCount(1)
                    , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName)
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
            stringBuilder.AppendFormat("{0} * @return {1}\r\n", class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.ServiceInterFaceReturnRemark);
            stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));

            if (class_Sub.ServiceInterFaceReturnCount == 0)
            {
                if (class_SelectAllModel.IsMultTable)
                    stringBuilder.AppendFormat("{0} {1}", class_ToolSpace.GetSetSpaceCount(1)
                    , class_Sub.DtoClassName);
                else
                    stringBuilder.AppendFormat("{0} {1}", class_ToolSpace.GetSetSpaceCount(1)
                    , _GetServiceReturnType(class_Sub, false));
            }
            else
            {
                if (class_SelectAllModel.IsMultTable)
                    stringBuilder.AppendFormat("{0}List<{1}>", class_ToolSpace.GetSetSpaceCount(1)
                    , class_Sub.DtoClassName);
                else
                    stringBuilder.AppendFormat("{0}List<{1}>", class_ToolSpace.GetSetSpaceCount(1)
                    , _GetServiceReturnType(class_Sub, false));
            }
            if (class_WhereFields != null && class_WhereFields.Count > 0)
            {
                if (class_WhereFields.Count > 1)
                {
                    stringBuilder.AppendFormat(" {0}({1} {2});\r\n"
                        , class_Sub.MethodId
                        , class_Sub.ParamClassName
                        , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName));
                }
                else
                {
                    if (class_WhereFields[0].FieldLogType.IndexOf("IN") > -1)
                        stringBuilder.AppendFormat(" {0}(List<{1}> {2});\r\n"
                        , class_Sub.MethodId
                        , Class_Tool.GetClosedJavaType(class_InterFaceDataBase.GetJavaType(class_WhereFields[0].LogType))
                        , class_WhereFields[0].OutFieldName);
                    else
                        stringBuilder.AppendFormat(" {0}({1} {2});\r\n"
                        , class_Sub.MethodId
                        , Class_Tool.GetClosedJavaType(class_InterFaceDataBase.GetJavaType(class_WhereFields[0].LogType))
                        , class_WhereFields[0].OutFieldName);
                }
            }
            else
                stringBuilder.AppendFormat(" {0}();\r\n"
                    , class_Sub.MethodId);

            #region 加入汇总代码
            if (class_Sub.ServiceInterFaceReturnCount > 0
                && class_SelectAllModel.ReturnStructure
                && (class_SelectAllModel.ReturnStructureType == 2
                || class_SelectAllModel.ReturnStructureType == 3))
            {
                stringBuilder.AppendFormat("\r\n{0}/**\r\n", class_ToolSpace.GetSetSpaceCount(1));
                stringBuilder.AppendFormat("{0} * {1}汇总功能\r\n{0} *\r\n", class_ToolSpace.GetSetSpaceCount(1)
                    , class_Sub.MethodContent);
                stringBuilder.AppendFormat("{0} * @return {1}\r\n", class_ToolSpace.GetSetSpaceCount(1)
                    , class_Sub.ServiceInterFaceReturnRemark);
                stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));
                stringBuilder.AppendFormat("{0}LinkedHashMap"
                    , class_ToolSpace.GetSetSpaceCount(1));
                if (class_WhereFields != null && class_WhereFields.Count > 0)
                {
                    if (class_WhereFields.Count > 1)
                    {
                        stringBuilder.AppendFormat(" {0}Total({1} {2})"
                            , class_Sub.MethodId
                            , class_Sub.ParamClassName
                            , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName));
                    }
                    else
                    {
                        if (class_WhereFields[0].FieldLogType.IndexOf("IN") > -1)
                            stringBuilder.AppendFormat(" {0}Total(List<{1}> {2})"
                            , class_Sub.MethodId
                            , Class_Tool.GetClosedJavaType(class_InterFaceDataBase.GetJavaType(class_WhereFields[0].LogType))
                            , class_WhereFields[0].OutFieldName);
                        else
                            stringBuilder.AppendFormat(" {0}Total({1} {2})"
                            , class_Sub.MethodId
                            , Class_Tool.GetClosedJavaType(class_InterFaceDataBase.GetJavaType(class_WhereFields[0].LogType))
                            , class_WhereFields[0].OutFieldName);
                    }
                    stringBuilder.Append(";\r\n");
                }
                else
                    stringBuilder.AppendFormat(" {0}Total();\r\n"
                        , class_Sub.MethodId);
            }
            #endregion
            if (!class_Sub.CreateMainCode)
                stringBuilder.Append("}\r\n");
            return stringBuilder.ToString();
        }
        private string _GetInPutParam(int PageIndex)
        {
            List<Class_WhereField> class_WhereFields = new List<Class_WhereField>();
            class_WhereFields = _GetParameterType();
            if (class_WhereFields == null || class_WhereFields.Count < 2)
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
            stringBuilder.AppendFormat("public class {0}"
                , class_SelectAllModel.class_SubList[PageIndex].ParamClassName);
            stringBuilder.Append(" {\r\n");

            //加入字段
            //if (class_SelectAllModel.IsMultTable)
            //{
            if (class_WhereFields != null)
            {
                foreach (Class_WhereField class_Field in class_WhereFields)
                {
                    string JaveType = Class_Tool.GetClosedJavaType(class_InterFaceDataBase.GetJavaType(class_Field.LogType));
                    if (class_Field.FieldLogType.IndexOf("IN") > -1)
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

            #region GET SET
            if (class_WhereFields != null)
            {
                foreach (Class_WhereField class_Field in class_WhereFields)
                {
                    string JaveType = Class_Tool.GetClosedJavaType(class_InterFaceDataBase.GetJavaType(class_Field.LogType));
                    if (class_Field.FieldLogType.IndexOf("IN") > -1)
                    {
                        JaveType = string.Format("List<{0}>", JaveType);
                    }
                    stringBuilder.AppendFormat("\r\n{0}public {2} get{1}()"
                        , class_ToolSpace.GetSetSpaceCount(1)
                        , Class_Tool.GetFirstCodeUpper(class_Field.OutFieldName)
                        , JaveType);
                    stringBuilder.Append("{\r\n");
                    stringBuilder.AppendFormat("{0}return {1};\r\n"
                        , class_ToolSpace.GetSetSpaceCount(2)
                        , Class_Tool.GetFirstCodeLow(class_Field.OutFieldName));
                    stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(1));
                    stringBuilder.Append("}\r\n\r\n");

                    stringBuilder.AppendFormat("{0}public void set{1}({3} {2})"
                        , class_ToolSpace.GetSetSpaceCount(1)
                        , Class_Tool.GetFirstCodeUpper(class_Field.OutFieldName)
                        , Class_Tool.GetFirstCodeLow(class_Field.OutFieldName)
                        , JaveType);
                    stringBuilder.Append("{\r\n");
                    stringBuilder.AppendFormat("{0}this.{1} = {1};\r\n"
                        , class_ToolSpace.GetSetSpaceCount(2)
                        , Class_Tool.GetFirstCodeLow(class_Field.OutFieldName));
                    stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(1));
                    stringBuilder.Append("}\r\n");
                }
            }
            #endregion

            stringBuilder.Append("}\r\n");
            class_WhereFields.Clear();

            return stringBuilder.ToString();
        }
        private string _GetDTO(int PageIndex)
        {
            if (class_SelectAllModel.class_SubList == null)
                return null;
            if (!class_SelectAllModel.IsMultTable)
                return null;
            if (PageIndex > 0 && class_SelectAllModel.class_SubList[PageIndex].JoinType == 0)
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
            stringBuilder.AppendFormat("public final class {0} {{\r\n"
                , class_SelectAllModel.class_SubList[PageIndex].DtoClassName);

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
                                string ReturnType = class_Field.ReturnType;
                                if ((class_Field.CaseWhen != null) && (class_Field.CaseWhen.Length > 0))
                                    ReturnType = "varchar";
                                ReturnType = Class_Tool.GetJdbcType(class_InterFaceDataBase.GetJavaType(class_InterFaceDataBase.GetDataTypeByFunction(class_Field.FunctionName, ReturnType)));
                                if (class_Field.FieldIsKey)
                                {
                                    if (PageIndex > 0)
                                    {
                                        stringBuilder.AppendFormat("{0}<result column=\"{1}\" property=\"{2}\" jdbcType=\"{3}\"/><!-- {4} -->\r\n"
                                            , class_ToolSpace.GetSetSpaceCount(SpaceCounter)
                                            , MyFieldName
                                            , MyFieldName
                                            , ReturnType
                                            , class_Field.FieldRemark);
                                    }
                                    else
                                        stringBuilder.AppendFormat("{0}<id column=\"{1}\" property=\"{2}\" jdbcType=\"{3}\"/><!-- {4} -->\r\n"
                                            , class_ToolSpace.GetSetSpaceCount(SpaceCounter)
                                            , MyFieldName
                                            , MyFieldName
                                            , ReturnType
                                            , class_Field.FieldRemark);
                                }
                                else
                                {
                                    stringBuilder.AppendFormat("{0}<result column=\"{1}\" property=\"{2}\" jdbcType=\"{3}\"/><!-- {4} -->\r\n"
                                        , class_ToolSpace.GetSetSpaceCount(SpaceCounter)
                                        , MyFieldName
                                        , MyFieldName
                                        , ReturnType
                                        , class_Field.FieldRemark);
                                }
                            }
                        }
                    }
                    break;
                case 1:
                    {
                        stringBuilder.AppendFormat("{0}<association property=\"{1}\" javaType=\"{2}.dto.{3}\">\r\n"
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
                                string ReturnType = class_Field.ReturnType;
                                if ((class_Field.CaseWhen != null) && (class_Field.CaseWhen.Length > 0))
                                    ReturnType = "varchar";
                                ReturnType = Class_Tool.GetJdbcType(class_InterFaceDataBase.GetJavaType(class_InterFaceDataBase.GetDataTypeByFunction(class_Field.FunctionName, ReturnType)));
                                if (class_Field.FieldIsKey)
                                {
                                    if (PageIndex > 0)
                                    {
                                        stringBuilder.AppendFormat("{0}<result column=\"{1}\" property=\"{2}\" jdbcType=\"{3}\"/><!-- {4} -->\r\n"
                                            , class_ToolSpace.GetSetSpaceCount(SpaceCounter + 1)
                                            , MyFieldName
                                            , MyFieldName
                                            , ReturnType
                                            , class_Field.FieldRemark);
                                    }
                                    else
                                        stringBuilder.AppendFormat("{0}<id column=\"{1}\" property=\"{2}\" jdbcType=\"{3}\"/><!-- {4} -->\r\n"
                                            , class_ToolSpace.GetSetSpaceCount(SpaceCounter + 1)
                                            , MyFieldName
                                            , MyFieldName
                                            , ReturnType
                                            , class_Field.FieldRemark);
                                }
                                else
                                {
                                    stringBuilder.AppendFormat("{0}<result column=\"{1}\" property=\"{2}\" jdbcType=\"{3}\"/><!-- {4} -->\r\n"
                                        , class_ToolSpace.GetSetSpaceCount(SpaceCounter + 1)
                                        , MyFieldName
                                        , MyFieldName
                                        , ReturnType
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
                                string ReturnType = class_Field.ReturnType;
                                if ((class_Field.CaseWhen != null) && (class_Field.CaseWhen.Length > 0))
                                    ReturnType = "varchar";
                                ReturnType = Class_Tool.GetJdbcType(class_InterFaceDataBase.GetJavaType(class_InterFaceDataBase.GetDataTypeByFunction(class_Field.FunctionName, ReturnType)));
                                if (class_Field.FieldIsKey)
                                {
                                    if (PageIndex > 0)
                                    {
                                        stringBuilder.AppendFormat("{0}<result column=\"{1}\" property=\"{2}\" jdbcType=\"{3}\"/><!-- {4} -->\r\n"
                                            , class_ToolSpace.GetSetSpaceCount(SpaceCounter + 1)
                                            , MyFieldName
                                            , MyFieldName
                                            , ReturnType
                                            , class_Field.FieldRemark);
                                    }
                                    else
                                        stringBuilder.AppendFormat("{0}<id column=\"{1}\" property=\"{2}\" jdbcType=\"{3}\"/><!-- {4} -->\r\n"
                                            , class_ToolSpace.GetSetSpaceCount(SpaceCounter + 1)
                                            , MyFieldName
                                            , MyFieldName
                                            , ReturnType
                                            , class_Field.FieldRemark);
                                }
                                else
                                {
                                    stringBuilder.AppendFormat("{0}<result column=\"{1}\" property=\"{2}\" jdbcType=\"{3}\"/><!-- {4} -->\r\n"
                                        , class_ToolSpace.GetSetSpaceCount(SpaceCounter + 1)
                                        , MyFieldName
                                        , MyFieldName
                                        , ReturnType
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
            switch (JoinType)
            {
                case 0:
                    {
                        foreach (Class_Field class_Field in class_Sub.class_Fields)
                        {
                            if (class_Field.SelectSelect)
                            {
                                string ReturnType = class_Field.ReturnType;
                                if ((class_Field.CaseWhen != null) && (class_Field.CaseWhen.Length > 0))
                                    ReturnType = "varchar";
                                ReturnType = Class_Tool.GetClosedJavaType(class_InterFaceDataBase.GetJavaType(class_InterFaceDataBase.GetDataTypeByFunction(class_Field.FunctionName, ReturnType)));
                                string MyFieldName = class_Field.ParaName;
                                if (IsMultTable && class_SelectAllModel.GetHaveSameFieldName(class_Field.ParaName, PageIndex))
                                    MyFieldName = class_Field.MultFieldName;

                                stringBuilder.AppendFormat("{0}/**\r\n", class_ToolSpace.GetSetSpaceCount(1));
                                stringBuilder.AppendFormat("{0} * {1}\r\n", class_ToolSpace.GetSetSpaceCount(1), class_Field.FieldRemark);
                                stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));
                                if (class_SelectAllModel.class_Create.EnglishSign && Class_Tool.IsEnglishField(class_Field.ParaName))
                                    stringBuilder.AppendFormat("{0}private transient {1} {2};\r\n"
                                        , class_ToolSpace.GetSetSpaceCount(1)
                                        , ReturnType
                                        , MyFieldName);
                                else
                                    stringBuilder.AppendFormat("{0}private {1} {2};\r\n"
                                        , class_ToolSpace.GetSetSpaceCount(1)
                                        , ReturnType
                                        , MyFieldName);
                            }
                        }
                        foreach (Class_Field class_Field in class_Sub.class_Fields)
                        {
                            if (class_Field.SelectSelect)
                            {
                                string ReturnType = class_Field.ReturnType;
                                if ((class_Field.CaseWhen != null) && (class_Field.CaseWhen.Length > 0))
                                    ReturnType = "varchar";
                                ReturnType = Class_Tool.GetClosedJavaType(class_InterFaceDataBase.GetJavaType(class_InterFaceDataBase.GetDataTypeByFunction(class_Field.FunctionName, ReturnType)));
                                string MyFieldName = class_Field.ParaName;
                                if (IsMultTable && class_SelectAllModel.GetHaveSameFieldName(class_Field.ParaName, PageIndex))
                                    MyFieldName = class_Field.MultFieldName;

                                #region Get
                                stringBuilder.AppendFormat("\r\n{0}public {2} get{1}()"
                                    , class_ToolSpace.GetSetSpaceCount(1)
                                    , Class_Tool.GetFirstCodeUpper(MyFieldName)
                                    , ReturnType);
                                stringBuilder.Append("{\r\n");
                                stringBuilder.AppendFormat("{0}return {1};\r\n"
                                    , class_ToolSpace.GetSetSpaceCount(2)
                                    , Class_Tool.GetFirstCodeLow(MyFieldName));
                                stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(1));
                                stringBuilder.Append("}\r\n\r\n");
                                #endregion

                                #region Set
                                stringBuilder.AppendFormat("{0}public void set{1}({3} {2})"
                                    , class_ToolSpace.GetSetSpaceCount(1)
                                    , Class_Tool.GetFirstCodeUpper(MyFieldName)
                                    , Class_Tool.GetFirstCodeLow(MyFieldName)
                                    , ReturnType);
                                stringBuilder.Append("{\r\n");
                                stringBuilder.AppendFormat("{0}this.{1} = {1};\r\n"
                                    , class_ToolSpace.GetSetSpaceCount(2)
                                    , Class_Tool.GetFirstCodeLow(MyFieldName));
                                stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(1));
                                stringBuilder.Append("}\r\n");
                                #endregion
                            }
                        }
                    }
                    break;
                case 1:
                    {
                        stringBuilder.AppendFormat("{0}/**\r\n", class_ToolSpace.GetSetSpaceCount(1));
                        stringBuilder.AppendFormat("{0} * 关联外表:{1}，一对一；\r\n"
                            , class_ToolSpace.GetSetSpaceCount(1)
                            , class_SelectAllModel.class_SubList[PageIndex].TableName);
                        stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));
                        stringBuilder.AppendFormat("{0}private {1} {2};\r\n"
                            , class_ToolSpace.GetSetSpaceCount(1)
                            , class_SelectAllModel.class_SubList[PageIndex].DtoClassName
                            , Class_Tool.GetFirstCodeLow(class_SelectAllModel.class_SubList[PageIndex].DtoClassName));

                        #region Get
                        stringBuilder.AppendFormat("\r\n{0}public {2} get{1}()"
                            , class_ToolSpace.GetSetSpaceCount(1)
                            , class_SelectAllModel.class_SubList[PageIndex].DtoClassName
                            , Class_Tool.GetFirstCodeUpper(class_SelectAllModel.class_SubList[PageIndex].DtoClassName));
                        stringBuilder.Append("{\r\n");
                        stringBuilder.AppendFormat("{0}return {1};\r\n"
                            , class_ToolSpace.GetSetSpaceCount(2)
                            , Class_Tool.GetFirstCodeLow(class_SelectAllModel.class_SubList[PageIndex].DtoClassName));
                        stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(1));
                        stringBuilder.Append("}\r\n\r\n");
                        #endregion

                        #region Set
                        stringBuilder.AppendFormat("{0}public void set{1}({3} {2})"
                            , class_ToolSpace.GetSetSpaceCount(1)
                            , Class_Tool.GetFirstCodeUpper(class_SelectAllModel.class_SubList[PageIndex].DtoClassName)
                            , Class_Tool.GetFirstCodeLow(class_SelectAllModel.class_SubList[PageIndex].DtoClassName)
                            , class_SelectAllModel.class_SubList[PageIndex].DtoClassName);
                        stringBuilder.Append("{\r\n");
                        stringBuilder.AppendFormat("{0}this.{1} = {1};\r\n"
                            , class_ToolSpace.GetSetSpaceCount(2)
                            , Class_Tool.GetFirstCodeLow(class_SelectAllModel.class_SubList[PageIndex].DtoClassName));
                        stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(1));
                        stringBuilder.Append("}\r\n");
                        #endregion
                    }
                    break;
                default:
                    {
                        stringBuilder.AppendFormat("{0}/**\r\n", class_ToolSpace.GetSetSpaceCount(1));
                        stringBuilder.AppendFormat("{0} * 关联外表:{1}，一对多；\r\n"
                            , class_ToolSpace.GetSetSpaceCount(1)
                            , class_SelectAllModel.class_SubList[PageIndex].TableName);
                        stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));
                        stringBuilder.AppendFormat("{0}private List<{1}> {2}s;\r\n"
                            , class_ToolSpace.GetSetSpaceCount(1)
                            , class_SelectAllModel.class_SubList[PageIndex].DtoClassName
                            , Class_Tool.GetFirstCodeLow(class_SelectAllModel.class_SubList[PageIndex].DtoClassName));
                        #region Get
                        stringBuilder.AppendFormat("\r\n{0}public List<{2}> get{1}()"
                            , class_ToolSpace.GetSetSpaceCount(1)
                            , class_SelectAllModel.class_SubList[PageIndex].DtoClassName
                            , Class_Tool.GetFirstCodeUpper(class_SelectAllModel.class_SubList[PageIndex].DtoClassName));
                        stringBuilder.Append("{\r\n");
                        stringBuilder.AppendFormat("{0}return {1}s;\r\n"
                            , class_ToolSpace.GetSetSpaceCount(2)
                            , Class_Tool.GetFirstCodeLow(class_SelectAllModel.class_SubList[PageIndex].DtoClassName));
                        stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(1));
                        stringBuilder.Append("}\r\n\r\n");
                        #endregion

                        #region Set
                        stringBuilder.AppendFormat("{0}public void set{1}(List<{3}> {2}s)"
                            , class_ToolSpace.GetSetSpaceCount(1)
                            , Class_Tool.GetFirstCodeUpper(class_SelectAllModel.class_SubList[PageIndex].DtoClassName)
                            , Class_Tool.GetFirstCodeLow(class_SelectAllModel.class_SubList[PageIndex].DtoClassName)
                            , class_SelectAllModel.class_SubList[PageIndex].DtoClassName);
                        stringBuilder.Append("{\r\n");
                        stringBuilder.AppendFormat("{0}this.{1}s = {1}s;\r\n"
                            , class_ToolSpace.GetSetSpaceCount(2)
                            , Class_Tool.GetFirstCodeLow(class_SelectAllModel.class_SubList[PageIndex].DtoClassName));
                        stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(1));
                        stringBuilder.Append("}\r\n");
                        #endregion
                    }
                    break;
            }
            return stringBuilder.ToString();
        }
        private string _GetControl(int PageIndex)
        {
            if (class_SelectAllModel.class_SubList == null)
                return null;
            if (class_SelectAllModel.class_SubList[PageIndex] == null)
                return null;
            Class_Sub class_Sub = class_SelectAllModel.class_SubList[PageIndex];
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

            bool MyPage = true;
            MyPage = MyPage && class_SelectAllModel.PageSign;
            MyPage = MyPage && (class_Sub.ServiceInterFaceReturnCount == 0 ? false : true);
            if (MyPage && class_SelectAllModel.ReturnStructure)
                MyPage = MyPage && (class_SelectAllModel.ReturnStructureType == 1 || class_SelectAllModel.ReturnStructureType == 2) ? true : false;

            #region 注释
            if (!class_Sub.CreateMainCode)
            {
                stringBuilder.Append("/**\r\n");
                stringBuilder.Append(_GetAuthor());
                stringBuilder.AppendFormat(" * @create {0}\r\n", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                stringBuilder.Append(" * @function\r\n * @editLog\r\n");
                stringBuilder.Append(" */\r\n");
                stringBuilder.Append("@RestController\r\n");
                if (class_Sub.ControlRequestMapping != null)
                    stringBuilder.AppendFormat("@RequestMapping(\"/{0}\")\r\n", Class_Tool.GetFirstCodeLow(class_Sub.ControlRequestMapping));

                if (class_SelectAllModel.class_Create.SwaggerSign)
                    stringBuilder.AppendFormat("@Api(value = \"{0}\", description = \"{1}\")\r\n"
                        , class_Sub.ControlSwaggerValue
                        , class_Sub.ControlSwaggerDescription);
                stringBuilder.AppendFormat("public class {0}", class_Sub.ControlRequestMapping);
                stringBuilder.Append(" {\r\n");
                stringBuilder.AppendFormat("{0}@Autowired\r\n", class_ToolSpace.GetSetSpaceCount(1));
                stringBuilder.AppendFormat("{0}{1} {2};\r\n"
                    , class_ToolSpace.GetSetSpaceCount(1)
                    , class_Sub.ServiceInterFaceName
                    , Class_Tool.GetFirstCodeLow(class_Sub.ServiceInterFaceName));

                stringBuilder.Append("\r\n");
            }
            stringBuilder.AppendFormat("{0}/**\r\n", class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0} * {1}，方法ID：{2}\r\n{0} *\r\n"
                , class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.MethodContent
                , class_SelectAllModel.class_Create.MethodId);
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
                    if (class_Field.IsSame)
                        stringBuilder.AppendFormat("{0} * @param {3} 表{1},原字段名{2},现字段名{3}:{4}\r\n"
                            , class_ToolSpace.GetSetSpaceCount(1)
                            , class_Field.TableName
                            , class_Field.FieldName
                            , class_Field.OutFieldName
                            , class_Field.FieldRemark);
                    else
                        stringBuilder.AppendFormat("{0} * @param {2} 表{1},字段名{2}:{3}\r\n"
                            , class_ToolSpace.GetSetSpaceCount(1)
                            , class_Field.TableName
                            , class_Field.OutFieldName
                            , class_Field.FieldRemark);
                }
            }
            if (MyPage)
            {
                stringBuilder.AppendFormat("{0} * @param {1} {2}\r\n"
                , class_ToolSpace.GetSetSpaceCount(1)
                , "page"
                , "当前页数");
                stringBuilder.AppendFormat("{0} * @param {1} {2}\r\n"
                , class_ToolSpace.GetSetSpaceCount(1)
                , "limit"
                , "每页条数");
            }
            if (class_SelectAllModel.class_Create.EnglishSign)
            {
                stringBuilder.AppendFormat("{0} * @param {1} {2}\r\n"
                    , class_ToolSpace.GetSetSpaceCount(1)
                , "englishSign"
                , "是否是英文版");
            }
            stringBuilder.AppendFormat("{0} * @return {1}\r\n", class_ToolSpace.GetSetSpaceCount(1)
            , class_Sub.ServiceInterFaceReturnRemark);
            stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));
            #endregion

            #region Swagger
            if (class_SelectAllModel.class_Create.SwaggerSign)
            {
                stringBuilder.AppendFormat("{0}@ApiOperation(value = \"{1}\", notes = \"{2}\")\r\n"
                , class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.MethodContent
                , class_Sub.ServiceInterFaceReturnRemark);
                if (class_WhereFields != null && class_WhereFields.Count > 0)
                {
                    stringBuilder.AppendFormat("{0}@ApiImplicitParams(", class_ToolSpace.GetSetSpaceCount(1));
                    stringBuilder.Append("{\r\n");
                    int index = 0;
                    foreach (Class_WhereField row in class_WhereFields)
                    {
                        stringBuilder.AppendFormat("{0}@ApiImplicitParam(name = \"{1}\", value = \"{2}\", dataType = \"{3}\""
                        , class_ToolSpace.GetSetSpaceCount(3)
                        , Class_Tool.GetFirstCodeLow(row.OutFieldName)
                        , row.FieldRemark
                        , Class_Tool.GetSimplificationJavaType(class_InterFaceDataBase.GetJavaType(row.LogType)));
                        if (!row.WhereIsNull)
                            stringBuilder.Append(", required = true");
                        if (row.FieldLogType.IndexOf("IN") > -1)
                            stringBuilder.Append(", paramType = \"query\"");
                        stringBuilder.Append(")");

                        if (index < class_WhereFields.Count - 1)
                            stringBuilder.Append(",");
                        if (index == class_WhereFields.Count - 1 && (class_SelectAllModel.class_Create.EnglishSign || MyPage))
                            stringBuilder.Append(",");
                        stringBuilder.Append("\r\n");
                        index++;
                    }
                    if (MyPage)
                    {
                        stringBuilder.AppendFormat("{0}@ApiImplicitParam(name = \"{1}\", value = \"{2}\", dataType = \"{3}\"),"
                        , class_ToolSpace.GetSetSpaceCount(3)
                        , "page"
                        , "当前页数"
                        , "int");
                        stringBuilder.Append("\r\n");
                        stringBuilder.AppendFormat("{0}@ApiImplicitParam(name = \"{1}\", value = \"{2}\", dataType = \"{3}\")"
                        , class_ToolSpace.GetSetSpaceCount(3)
                        , "limit"
                        , "每页条数"
                        , "int");
                        stringBuilder.Append("\r\n");
                    }

                    if (class_SelectAllModel.class_Create.EnglishSign)
                        stringBuilder.AppendFormat("{0}@ApiImplicitParam(name = \"englishSign\", value = \"是否生成英文\", required = true, dataType = \"Boolean\")\r\n"
                        , class_ToolSpace.GetSetSpaceCount(3));

                    stringBuilder.AppendFormat("{0}", class_ToolSpace.GetSetSpaceCount(1));
                    stringBuilder.Append("})\r\n");
                }
                else
                {
                    if (MyPage)
                    {
                        stringBuilder.AppendFormat("{0}@ApiImplicitParams(", class_ToolSpace.GetSetSpaceCount(1));
                        stringBuilder.Append("{\r\n");
                        stringBuilder.AppendFormat("{0}@ApiImplicitParam(name = \"{1}\", value = \"{2}\", dataType = \"{3}\"),"
                        , class_ToolSpace.GetSetSpaceCount(3)
                        , "page"
                        , "当前页数"
                        , "int");
                        stringBuilder.Append("\r\n");
                        stringBuilder.AppendFormat("{0}@ApiImplicitParam(name = \"{1}\", value = \"{2}\", dataType = \"{3}\")"
                        , class_ToolSpace.GetSetSpaceCount(3)
                        , "limit"
                        , "每页条数"
                        , "int");
                        stringBuilder.Append("\r\n");
                        stringBuilder.AppendFormat("{0}", class_ToolSpace.GetSetSpaceCount(1));
                        stringBuilder.Append("})\r\n");
                    }
                }
            }
            #endregion

            stringBuilder.AppendFormat("{0}@{1}Mapping(\"/{2}\")\r\n"
            , class_ToolSpace.GetSetSpaceCount(1)
            , class_SelectAllModel.class_Create.HttpRequestType
            , class_Sub.MethodId);
            stringBuilder.AppendFormat("{0}public ", class_ToolSpace.GetSetSpaceCount(1));

            #region 返回值
            if (class_SelectAllModel.ReturnStructure)
            {
                int StructureType = class_SelectAllModel.ReturnStructureType;
                if (class_Sub.ServiceInterFaceReturnCount == 0)
                    StructureType = 0;
                switch (StructureType)
                {
                    case 0:
                        stringBuilder.Append("ResultVO");
                        break;
                    case 1:
                        stringBuilder.Append("ResultVOPage");
                        break;
                    case 2:
                        stringBuilder.Append("ResultVOPageTotal");
                        break;
                    case 3:
                        stringBuilder.Append("ResultVOTotal");
                        break;
                    default:
                        stringBuilder.Append("ResultVOPage");
                        break;
                }
            }
            else
            {
                if (class_Sub.ServiceInterFaceReturnCount == 0)
                {
                    if (class_SelectAllModel.IsMultTable)
                        stringBuilder.AppendFormat("{0}", class_Sub.DtoClassName);
                    else
                        stringBuilder.AppendFormat("{0}", _GetServiceReturnType(class_Sub, false));

                }
                else
                {
                    if (MyPage)
                    {
                        stringBuilder.Append("PageInfo");
                    }
                    else
                    {
                        if (class_SelectAllModel.IsMultTable)
                            stringBuilder.AppendFormat("List<{0}>", class_Sub.DtoClassName);
                        else
                            stringBuilder.AppendFormat("List<{0}>", _GetServiceReturnType(class_Sub, false));
                    }
                }
            }
            #endregion

            stringBuilder.AppendFormat(" {0}", class_Sub.MethodId);
            stringBuilder.Append("(");
            int Index = 0;
            foreach (Class_WhereField row in class_WhereFields)
            {
                if (Index++ > 0)
                    stringBuilder.AppendFormat("\r\n{1}, @RequestParam(value = \"{0}\""
                    , row.OutFieldName
                    , class_ToolSpace.GetSetSpaceCount(3));
                else
                    stringBuilder.AppendFormat("@RequestParam(value = \"{0}\""
                    , row.OutFieldName);
                if (row.WhereIsNull)
                {
                    stringBuilder.Append(", required = false");
                }
                if (!row.FieldDefaultValue.Equals("CURRENT_TIMESTAMP") && (row.FieldDefaultValue != null) && (row.FieldDefaultValue.Length > 0))
                    stringBuilder.AppendFormat(", defaultValue = \"{0}\"", _GetFieldDefaultValue(row.FieldDefaultValue));
                stringBuilder.Append(")");
                if (row.FieldType.IndexOf("date") > -1 && row.LogType.IndexOf("IN") < 0)
                {
                    if (row.FieldType.Equals("date"))
                        stringBuilder.Append(" @DateTimeFormat(pattern = \"yyyy-MM-dd\")");
                    if (row.FieldType.Equals("datetime"))
                        stringBuilder.Append(" @DateTimeFormat(pattern = \"yyyy-MM-dd HH:mm:ss\")");
                }
                if (row.FieldLogType.IndexOf("IN") > -1)
                {
                    stringBuilder.AppendFormat(" List<{0}>"
                    , Class_Tool.GetSimplificationJavaType(class_InterFaceDataBase.GetJavaType(row.LogType)));
                }
                else
                    stringBuilder.AppendFormat(" {0}"
                    , Class_Tool.GetSimplificationJavaType(class_InterFaceDataBase.GetJavaType(row.LogType)));
                stringBuilder.AppendFormat(" {0}", row.OutFieldName);
            }
            if (MyPage)
            {
                if (Index > 0)
                {
                    stringBuilder.AppendFormat("\r\n{1}, @RequestParam(value = \"{0}\", defaultValue = \"1\") int {0}"
                    , "page"
                    , class_ToolSpace.GetSetSpaceCount(3));
                    stringBuilder.AppendFormat("\r\n{1}, @RequestParam(value = \"{0}\", defaultValue = \"10\") int {0}"
                    , "limit"
                    , class_ToolSpace.GetSetSpaceCount(3));
                }
                else
                {
                    stringBuilder.AppendFormat("\r\n{1} @RequestParam(value = \"{0}\", defaultValue = \"1\") int {0},"
                    , "page"
                    , class_ToolSpace.GetSetSpaceCount(3));
                    stringBuilder.AppendFormat("\r\n{1} @RequestParam(value = \"{0}\", defaultValue = \"10\") int {0}"
                    , "limit"
                    , class_ToolSpace.GetSetSpaceCount(3));
                }
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

            #region 去空格
            foreach (Class_WhereField row in class_WhereFields)
            {
                if ((row.LogType.Equals("varchar") || row.LogType.Equals("char")) && row.WhereTrim)
                {
                    if (row.FieldLogType.IndexOf("=") > -1 || row.FieldLogType.IndexOf("Like") > -1)
                    {
                        stringBuilder.AppendFormat("{0}{1} = {1} == null ? {1} : {1}.trim();\r\n"
                            , class_ToolSpace.GetSetSpaceCount(2), row.OutFieldName);
                    }
                }
            }
            stringBuilder.Append("\r\n");
            #endregion
            if (class_WhereFields != null)
            {
                if (class_WhereFields.Count > 1)
                {
                    stringBuilder.AppendFormat("{0}{1} {2} = new {1}();\r\n"
                    , class_ToolSpace.GetSetSpaceCount(2)
                    , class_Sub.ParamClassName
                    , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName));
                    foreach (Class_WhereField row in class_WhereFields)
                    {
                        stringBuilder.AppendFormat("{0}{1}.set{2}({3});\r\n"
                        , class_ToolSpace.GetSetSpaceCount(2)
                        , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName)
                        , Class_Tool.GetFirstCodeUpper(row.OutFieldName)
                        , row.OutFieldName);
                    }
                }
            }

            if (class_SelectAllModel.ReturnStructure)
            {
                if (class_Sub.ServiceInterFaceReturnCount == 0)
                {
                    stringBuilder.AppendFormat("\r\n{0}return ResultStruct.success({1}."
                    , class_ToolSpace.GetSetSpaceCount(2)
                    , Class_Tool.GetFirstCodeLow(class_Sub.ServiceInterFaceName));
                    if (class_WhereFields != null)
                    {
                        if (class_WhereFields.Count > 1)
                            stringBuilder.AppendFormat("{0}({1})"
                                , class_Sub.MethodId
                                , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName));
                        else if (class_WhereFields.Count == 1)
                        {
                            stringBuilder.AppendFormat("{0}({1})"
                                , class_Sub.MethodId
                                , class_WhereFields[0].OutFieldName);
                        }
                        else
                            stringBuilder.AppendFormat("{0}()"
                                , class_Sub.MethodId);
                    }
                    stringBuilder.Append(");\r\n");
                }
                else
                {
                    if (MyPage)
                    {
                        stringBuilder.AppendFormat("\r\n{0}PageHelper.startPage(page, limit);\r\n"
                            , class_ToolSpace.GetSetSpaceCount(2));
                        if ((class_SelectAllModel.class_Create.EnglishSign) && (_GetEnglishFieldList(class_Sub).Count > 0))
                        {
                            if (class_Sub.ServiceInterFaceReturnCount == 0)
                            {
                                stringBuilder.AppendFormat("{0}{1}"
                                    , class_ToolSpace.GetSetSpaceCount(2)
                                    , _GetServiceReturnType(class_Sub, false));
                                stringBuilder.AppendFormat(" {1} = new {0}();\r\n"
                                    , _GetServiceReturnType(class_Sub, false)
                                    , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false))
                                    , class_ToolSpace.GetSetSpaceCount(2));
                                stringBuilder.AppendFormat("{0}{1} = {2}."
                                        , class_ToolSpace.GetSetSpaceCount(2)
                                    , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false))
                                    , Class_Tool.GetFirstCodeLow(class_Sub.ServiceInterFaceName));
                            }
                            else
                            {
                                stringBuilder.AppendFormat("{0}List<{1}>"
                                    , class_ToolSpace.GetSetSpaceCount(2)
                                    , _GetServiceReturnType(class_Sub, false));
                                stringBuilder.AppendFormat(" {1}s = new {0}();\r\n"
                                    , _GetServiceReturnType(class_Sub, false)
                                    , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false))
                                    , class_ToolSpace.GetSetSpaceCount(2));
                                stringBuilder.AppendFormat("{0}{1}s = {2}."
                                        , class_ToolSpace.GetSetSpaceCount(2)
                                    , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false))
                                    , Class_Tool.GetFirstCodeLow(class_Sub.ServiceInterFaceName));
                            }

                            if (class_WhereFields != null)
                            {
                                if (class_WhereFields.Count > 1)
                                    stringBuilder.AppendFormat("{0}({1});\r\n"
                                        , class_Sub.MethodId
                                        , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName));
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
                            stringBuilder.AppendFormat("{0}if (englishSign) ", class_ToolSpace.GetSetSpaceCount(2));
                            stringBuilder.Append("{\r\n");
                            if (class_Sub.ServiceInterFaceReturnCount == 0)
                            {
                                List<Class_EnglishField> class_EnglishFields = new List<Class_EnglishField>();
                                class_EnglishFields = _GetEnglishFieldList(class_Sub);
                                if (class_EnglishFields.Count > 0)
                                {
                                    foreach (Class_EnglishField row in class_EnglishFields)
                                    {
                                        stringBuilder.AppendFormat("{0}{3}.set{1}({3}.get{2}());\r\n"
                                            , class_ToolSpace.GetSetSpaceCount(3)
                                            , Class_Tool.GetFirstCodeUpper(row.FieldChinaName)
                                            , Class_Tool.GetFirstCodeUpper(row.FieldEnglishName)
                                            , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false)));
                                    }
                                }
                            }
                            else
                            {
                                stringBuilder.AppendFormat("{0}{1}.forEach(item ->"
                                    , class_ToolSpace.GetSetSpaceCount(3)
                                    , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false)) + "s");
                                List<Class_EnglishField> class_EnglishFields = new List<Class_EnglishField>();
                                class_EnglishFields = _GetEnglishFieldList(class_Sub);
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
                            if (class_Sub.ServiceInterFaceReturnCount == 0)
                            {
                                stringBuilder.AppendFormat(" {0};\r\n"
                                , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false)));
                            }
                            else
                            {
                                stringBuilder.AppendFormat(" {0}s;\r\n"
                                , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false)));

                            }
                        }
                        else
                        {
                            stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(2));
                            if (class_SelectAllModel.IsMultTable)
                                stringBuilder.AppendFormat("List<{0}> {1}s = "
                                    , class_Sub.DtoClassName
                                    , Class_Tool.GetFirstCodeLow(class_Sub.DtoClassName));
                            else
                                stringBuilder.AppendFormat("List<{0}> {1}s = "
                                    , _GetServiceReturnType(class_Sub, false)
                                    , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false)));

                            stringBuilder.AppendFormat("{0}."
                                , Class_Tool.GetFirstCodeLow(class_Sub.ServiceInterFaceName));

                            if (class_WhereFields != null)
                            {
                                if (class_WhereFields.Count > 1)
                                    stringBuilder.AppendFormat("{0}({1});\r\n"
                                        , class_Sub.MethodId
                                        , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName));
                                else if (class_WhereFields.Count == 1)
                                {
                                    stringBuilder.AppendFormat("{0}({1});\r\n"
                                        , class_Sub.MethodId
                                        , class_WhereFields[0].OutFieldName);
                                }
                                else
                                    stringBuilder.AppendFormat("{0}();\r\n"
                                        , class_Sub.MethodId);
                            }
                            if (class_SelectAllModel.IsMultTable)
                            {
                                stringBuilder.AppendFormat("{0}PageInfo pageInfo = new PageInfo<>({1}s, limit);\r\n"
                                    , class_ToolSpace.GetSetSpaceCount(2)
                                    , Class_Tool.GetFirstCodeLow(class_Sub.DtoClassName));
                            }
                            else
                            {
                                stringBuilder.AppendFormat("{0}PageInfo pageInfo = new PageInfo<>({1}s, limit);\r\n"
                                    , class_ToolSpace.GetSetSpaceCount(2)
                                    , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false)));
                            }
                            if (class_SelectAllModel.ReturnStructure)
                            {
                                string OutObjectName = null;
                                if (class_SelectAllModel.IsMultTable)
                                    OutObjectName = String.Format("{0}s", Class_Tool.GetFirstCodeLow(class_Sub.DtoClassName));
                                else
                                    OutObjectName = string.Format("{0}s", _GetServiceReturnType(class_Sub, false));

                                int StructureType = class_SelectAllModel.ReturnStructureType;

                                #region 加入汇总代码
                                if (StructureType == 2 || StructureType == 3)
                                {
                                    stringBuilder.AppendFormat("\r\n{3}LinkedHashMap {0}Total = {1}.{2}Total"
                                        , OutObjectName
                                        , Class_Tool.GetFirstCodeLow(class_Sub.ServiceInterFaceName)
                                        , class_Sub.MethodId
                                        , class_ToolSpace.GetSetSpaceCount(2));
                                    if (class_WhereFields != null)
                                    {
                                        if (class_WhereFields.Count > 1)
                                            stringBuilder.AppendFormat("({0});\r\n"
                                                , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName));
                                        else if (class_WhereFields.Count == 1)
                                        {
                                            stringBuilder.AppendFormat("({0});\r\n"
                                                , class_WhereFields[0].OutFieldName);
                                        }
                                        else
                                            stringBuilder.Append("();\r\n");
                                    }
                                    stringBuilder.AppendFormat("{0}List<TotalValueClass> totalValueClassList = new ArrayList<>();\r\n"
                                        , class_ToolSpace.GetSetSpaceCount(2));
                                    stringBuilder.AppendFormat("{0}if ({1}Total != null)", class_ToolSpace.GetSetSpaceCount(2)
                                        , OutObjectName);
                                    stringBuilder.Append(" {\r\n");
                                    stringBuilder.AppendFormat("{0}Iterator iterator = {1}Total.entrySet().iterator();\r\n"
                                        , class_ToolSpace.GetSetSpaceCount(3)
                                        , OutObjectName);
                                    stringBuilder.AppendFormat("{0}while (iterator.hasNext()) "
                                        , class_ToolSpace.GetSetSpaceCount(3));
                                    stringBuilder.Append(" {\r\n");
                                    stringBuilder.AppendFormat("{0}Map.Entry next = (Map.Entry) iterator.next();\r\n"
                                        , class_ToolSpace.GetSetSpaceCount(4));
                                    stringBuilder.AppendFormat("{0}byte i;\r\n"
                                        , class_ToolSpace.GetSetSpaceCount(4));
                                    stringBuilder.AppendFormat("{0}switch (next.getKey().toString()) {{\r\n"
                                        , class_ToolSpace.GetSetSpaceCount(4));

                                    int TotalCounter = 0;
                                    int CurPageIndex = 0;
                                    foreach (Class_Sub item in class_SelectAllModel.class_SubList)
                                    {
                                        string AliasName = item.AliasName;
                                        foreach (Class_Field class_Field in item.class_Fields)
                                        {
                                            if (class_Field.SelectSelect && !(class_Field.CaseWhen != null && class_Field.CaseWhen.Length > 0) && !(class_Field.FunctionName != null && class_Field.FunctionName.Length > 0) && class_Field.TotalFunctionName != null && class_Field.TotalFunctionName.Length > 0 && (class_Field.FieldType.Equals("int") || class_Field.FieldType.Equals("tinyint") || class_Field.FieldType.Equals("decimal") || class_Field.FieldType.Equals("date") || class_Field.FieldType.Equals("datetime")))
                                            {
                                                string MyFieldName = null;
                                                if (class_SelectAllModel.GetHaveSameFieldName(class_Field.ParaName, CurPageIndex))
                                                    MyFieldName = class_Field.MultFieldName;
                                                else
                                                    MyFieldName = class_Field.ParaName;
                                                stringBuilder.AppendFormat("{0}case \"{1}\":\r\n"
                                                    , class_ToolSpace.GetSetSpaceCount(5), MyFieldName);
                                                stringBuilder.AppendFormat("{0}i = {1};\r\n"
                                                    , class_ToolSpace.GetSetSpaceCount(6), TotalCounter);

                                                stringBuilder.AppendFormat("{0}break;\r\n", class_ToolSpace.GetSetSpaceCount(6));
                                                TotalCounter++;
                                            }
                                        }
                                        CurPageIndex++;
                                    }
                                    stringBuilder.AppendFormat("{0}default:\r\n", class_ToolSpace.GetSetSpaceCount(5));
                                    stringBuilder.AppendFormat("{0}i = 0;\r\n", class_ToolSpace.GetSetSpaceCount(6));
                                    stringBuilder.AppendFormat("{0}break;\r\n", class_ToolSpace.GetSetSpaceCount(6));
                                    stringBuilder.AppendFormat("{0}}}\r\n"
                                        , class_ToolSpace.GetSetSpaceCount(4));
                                    stringBuilder.AppendFormat("{0}if (i > 0)\r\n"
                                        , class_ToolSpace.GetSetSpaceCount(4));
                                    stringBuilder.AppendFormat("{0}addTotalSite(next, totalValueClassList, i);\r\n"
                                        , class_ToolSpace.GetSetSpaceCount(5));
                                    stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(3) + "}\r\n");
                                    stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(2) + "}\r\n\r\n");
                                }
                                #endregion

                                stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(2) + "return ResultStruct.success");

                                switch (StructureType)
                                {
                                    case 0:
                                        stringBuilder.AppendFormat("({0}", Class_Tool.GetFirstCodeLow(OutObjectName));
                                        break;
                                    case 1:
                                        stringBuilder.AppendFormat("Page({0}, pageInfo.getPageNum(), pageInfo.getPageSize(), pageInfo.getTotal()", Class_Tool.GetFirstCodeLow(OutObjectName));
                                        break;
                                    case 2:
                                        stringBuilder.AppendFormat("PageTotal({0}, pageInfo.getPageNum(), pageInfo.getPageSize(), pageInfo.getTotal(), totalValueClassList", Class_Tool.GetFirstCodeLow(OutObjectName));
                                        break;
                                    case 3:
                                        stringBuilder.AppendFormat("Total({0}, totalValueClassList", Class_Tool.GetFirstCodeLow(OutObjectName));
                                        break;
                                    default:
                                        stringBuilder.AppendFormat("Page({0}, pageInfo.getPageNum(), pageInfo.getPageSize(), pageInfo.getTotal()", Class_Tool.GetFirstCodeLow(OutObjectName));
                                        break;
                                }
                                stringBuilder.Append(");\r\n");
                            }
                            else
                            {
                                stringBuilder.Append("pageInfo;\r\n");
                            }
                        }
                    }
                    else
                    {
                        if ((class_SelectAllModel.class_Create.EnglishSign) && (_GetEnglishFieldList(class_Sub).Count > 0))
                        {
                            if (class_Sub.ServiceInterFaceReturnCount == 0)
                            {
                                stringBuilder.AppendFormat("{0}{1}"
                                    , class_ToolSpace.GetSetSpaceCount(2)
                                    , _GetServiceReturnType(class_Sub, false));
                                stringBuilder.AppendFormat(" {1} = new {0}();\r\n"
                                    , _GetServiceReturnType(class_Sub, false)
                                    , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false))
                                    , class_ToolSpace.GetSetSpaceCount(2));
                                stringBuilder.AppendFormat("{0}{1} = {2}."
                                        , class_ToolSpace.GetSetSpaceCount(2)
                                    , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false))
                                    , Class_Tool.GetFirstCodeLow(class_Sub.ServiceInterFaceName));
                            }
                            else
                            {
                                stringBuilder.AppendFormat("{0}List<{1}>"
                                    , class_ToolSpace.GetSetSpaceCount(2)
                                    , _GetServiceReturnType(class_Sub, false));
                                stringBuilder.AppendFormat(" {1}s = new {0}();\r\n"
                                    , _GetServiceReturnType(class_Sub, false)
                                    , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false))
                                    , class_ToolSpace.GetSetSpaceCount(2));
                                stringBuilder.AppendFormat("{0}{1}s = {2}."
                                        , class_ToolSpace.GetSetSpaceCount(2)
                                    , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false))
                                    , Class_Tool.GetFirstCodeLow(class_Sub.ServiceInterFaceName));
                            }

                            if (class_WhereFields != null)
                            {
                                if (class_WhereFields.Count > 1)
                                    stringBuilder.AppendFormat("{0}({1});\r\n"
                                        , class_Sub.MethodId
                                        , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName));
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
                            stringBuilder.AppendFormat("{0}if (englishSign) ", class_ToolSpace.GetSetSpaceCount(2));
                            stringBuilder.Append("{\r\n");
                            if (class_Sub.ServiceInterFaceReturnCount == 0)
                            {
                                List<Class_EnglishField> class_EnglishFields = new List<Class_EnglishField>();
                                class_EnglishFields = _GetEnglishFieldList(class_Sub);
                                if (class_EnglishFields.Count > 0)
                                {
                                    foreach (Class_EnglishField row in class_EnglishFields)
                                    {
                                        stringBuilder.AppendFormat("{0}{3}.set{1}({3}.get{2}());\r\n"
                                            , class_ToolSpace.GetSetSpaceCount(3)
                                            , Class_Tool.GetFirstCodeUpper(row.FieldChinaName)
                                            , Class_Tool.GetFirstCodeUpper(row.FieldEnglishName)
                                            , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false)));
                                    }
                                }
                            }
                            else
                            {
                                stringBuilder.AppendFormat("{0}{1}.forEach(item ->"
                                    , class_ToolSpace.GetSetSpaceCount(3)
                                    , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false)) + "s");
                                List<Class_EnglishField> class_EnglishFields = new List<Class_EnglishField>();
                                class_EnglishFields = _GetEnglishFieldList(class_Sub);
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
                            if (class_Sub.ServiceInterFaceReturnCount == 0)
                            {
                                stringBuilder.AppendFormat(" {0};\r\n"
                                , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false)));
                            }
                            else
                            {
                                stringBuilder.AppendFormat(" {0}s;\r\n"
                                , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false)));

                            }
                        }
                        else
                        {
                            if (class_SelectAllModel.ReturnStructure)
                            {
                                stringBuilder.AppendFormat("\r\n{0}return ResultStruct.success({1}."
                                    , class_ToolSpace.GetSetSpaceCount(2)
                                    , Class_Tool.GetFirstCodeLow(class_Sub.ServiceInterFaceName));
                                if (class_WhereFields != null)
                                {
                                    if (class_WhereFields.Count > 1)
                                        stringBuilder.AppendFormat("{0}({1})"
                                            , class_Sub.MethodId
                                            , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName));
                                    else if (class_WhereFields.Count == 1)
                                    {
                                        stringBuilder.AppendFormat("{0}({1})"
                                            , class_Sub.MethodId
                                            , class_WhereFields[0].OutFieldName);
                                    }
                                    else
                                        stringBuilder.AppendFormat("{0}()"
                                            , class_Sub.MethodId);
                                }
                                stringBuilder.Append(");\r\n");
                            }
                            else
                            {
                                stringBuilder.AppendFormat("\r\n{0}return {1}."
                                    , class_ToolSpace.GetSetSpaceCount(2)
                                    , Class_Tool.GetFirstCodeLow(class_Sub.ServiceInterFaceName));
                                if (class_WhereFields != null)
                                {
                                    if (class_WhereFields.Count > 1)
                                        stringBuilder.AppendFormat("{0}({1});\r\n"
                                            , class_Sub.MethodId
                                            , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName));
                                    else if (class_WhereFields.Count == 1)
                                    {
                                        stringBuilder.AppendFormat("{0}({1});\r\n"
                                            , class_Sub.MethodId
                                            , class_WhereFields[0].OutFieldName);
                                    }
                                    else
                                        stringBuilder.AppendFormat("{0}();\r\n"
                                            , class_Sub.MethodId);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (class_Sub.ServiceInterFaceReturnCount == 0)
                {
                    if ((class_SelectAllModel.class_Create.EnglishSign) && (_GetEnglishFieldList(class_Sub).Count > 0))
                    {
                        stringBuilder.AppendFormat("{0}{1}"
                            , class_ToolSpace.GetSetSpaceCount(2)
                            , _GetServiceReturnType(class_Sub, false));
                        stringBuilder.AppendFormat(" {1} = new {0}();\r\n"
                            , _GetServiceReturnType(class_Sub, false)
                            , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false))
                            , class_ToolSpace.GetSetSpaceCount(2));
                        stringBuilder.AppendFormat("{0}{1} = {2}."
                                , class_ToolSpace.GetSetSpaceCount(2)
                            , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false))
                            , Class_Tool.GetFirstCodeLow(class_Sub.ServiceInterFaceName));
                        if (class_WhereFields != null)
                        {
                            if (class_WhereFields.Count > 1)
                                stringBuilder.AppendFormat("{0}({1});\r\n"
                                    , class_Sub.MethodId
                                    , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName));
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
                        stringBuilder.AppendFormat("{0}if (englishSign) ", class_ToolSpace.GetSetSpaceCount(2));
                        stringBuilder.Append("{\r\n");
                        if (class_Sub.ServiceInterFaceReturnCount == 0)
                        {
                            List<Class_EnglishField> class_EnglishFields = new List<Class_EnglishField>();
                            class_EnglishFields = _GetEnglishFieldList(class_Sub);
                            if (class_EnglishFields.Count > 0)
                            {
                                foreach (Class_EnglishField row in class_EnglishFields)
                                {
                                    stringBuilder.AppendFormat("{0}{3}.set{1}({3}.get{2}());\r\n"
                                        , class_ToolSpace.GetSetSpaceCount(3)
                                        , Class_Tool.GetFirstCodeUpper(row.FieldChinaName)
                                        , Class_Tool.GetFirstCodeUpper(row.FieldEnglishName)
                                        , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false)));
                                }
                            }
                        }
                        else
                        {
                            stringBuilder.AppendFormat("{0}{1}.forEach(item ->"
                                , class_ToolSpace.GetSetSpaceCount(3)
                                , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false)) + "s");
                            List<Class_EnglishField> class_EnglishFields = new List<Class_EnglishField>();
                            class_EnglishFields = _GetEnglishFieldList(class_Sub);
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
                        if (class_Sub.ServiceInterFaceReturnCount == 0)
                        {
                            stringBuilder.AppendFormat(" {0};\r\n"
                            , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false)));
                        }
                        else
                        {
                            stringBuilder.AppendFormat(" {0}s;\r\n"
                            , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false)));

                        }
                    }
                    else
                    {
                        stringBuilder.AppendFormat("\r\n{0}return {1}."
                            , class_ToolSpace.GetSetSpaceCount(2)
                            , Class_Tool.GetFirstCodeLow(class_Sub.ServiceInterFaceName));
                        if (class_WhereFields != null)
                        {
                            if (class_WhereFields.Count > 1)
                                stringBuilder.AppendFormat("{0}({1});\r\n"
                                    , class_Sub.MethodId
                                    , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName));
                            else if (class_WhereFields.Count == 1)
                            {
                                stringBuilder.AppendFormat("{0}({1});\r\n"
                                    , class_Sub.MethodId
                                    , class_WhereFields[0].OutFieldName);
                            }
                            else
                                stringBuilder.AppendFormat("{0}();\r\n"
                                    , class_Sub.MethodId);
                        }
                    }
                }
                else
                {
                    if (MyPage)
                    {
                        stringBuilder.AppendFormat("\r\n{0}PageHelper.startPage(page, limit);\r\n"
                            , class_ToolSpace.GetSetSpaceCount(2));
                        if ((class_SelectAllModel.class_Create.EnglishSign) && (_GetEnglishFieldList(class_Sub).Count > 0))
                        {
                            if (class_Sub.ServiceInterFaceReturnCount == 0)
                            {
                                stringBuilder.AppendFormat("{0}{1}"
                                    , class_ToolSpace.GetSetSpaceCount(2)
                                    , _GetServiceReturnType(class_Sub, false));
                                stringBuilder.AppendFormat(" {1} = new {0}();\r\n"
                                    , _GetServiceReturnType(class_Sub, false)
                                    , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false))
                                    , class_ToolSpace.GetSetSpaceCount(2));
                                stringBuilder.AppendFormat("{0}{1} = {2}."
                                        , class_ToolSpace.GetSetSpaceCount(2)
                                    , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false))
                                    , Class_Tool.GetFirstCodeLow(class_Sub.ServiceInterFaceName));
                            }
                            else
                            {
                                stringBuilder.AppendFormat("{0}List<{1}>"
                                    , class_ToolSpace.GetSetSpaceCount(2)
                                    , _GetServiceReturnType(class_Sub, false));
                                stringBuilder.AppendFormat(" {1}s = new {0}();\r\n"
                                    , _GetServiceReturnType(class_Sub, false)
                                    , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false))
                                    , class_ToolSpace.GetSetSpaceCount(2));
                                stringBuilder.AppendFormat("{0}{1}s = {2}."
                                        , class_ToolSpace.GetSetSpaceCount(2)
                                    , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false))
                                    , Class_Tool.GetFirstCodeLow(class_Sub.ServiceInterFaceName));
                            }

                            if (class_WhereFields != null)
                            {
                                if (class_WhereFields.Count > 1)
                                    stringBuilder.AppendFormat("{0}({1});\r\n"
                                        , class_Sub.MethodId
                                        , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName));
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
                            stringBuilder.AppendFormat("{0}if (englishSign) ", class_ToolSpace.GetSetSpaceCount(2));
                            stringBuilder.Append("{\r\n");
                            if (class_Sub.ServiceInterFaceReturnCount == 0)
                            {
                                List<Class_EnglishField> class_EnglishFields = new List<Class_EnglishField>();
                                class_EnglishFields = _GetEnglishFieldList(class_Sub);
                                if (class_EnglishFields.Count > 0)
                                {
                                    foreach (Class_EnglishField row in class_EnglishFields)
                                    {
                                        stringBuilder.AppendFormat("{0}{3}.set{1}({3}.get{2}());\r\n"
                                            , class_ToolSpace.GetSetSpaceCount(3)
                                            , Class_Tool.GetFirstCodeUpper(row.FieldChinaName)
                                            , Class_Tool.GetFirstCodeUpper(row.FieldEnglishName)
                                            , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false)));
                                    }
                                }
                            }
                            else
                            {
                                stringBuilder.AppendFormat("{0}{1}.forEach(item ->"
                                    , class_ToolSpace.GetSetSpaceCount(3)
                                    , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false)) + "s");
                                List<Class_EnglishField> class_EnglishFields = new List<Class_EnglishField>();
                                class_EnglishFields = _GetEnglishFieldList(class_Sub);
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
                            if (class_Sub.ServiceInterFaceReturnCount == 0)
                            {
                                stringBuilder.AppendFormat(" {0};\r\n"
                                , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false)));
                            }
                            else
                            {
                                stringBuilder.AppendFormat(" {0}s;\r\n"
                                , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false)));

                            }
                        }
                        else
                        {
                            stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(2));
                            if (class_SelectAllModel.IsMultTable)
                                stringBuilder.AppendFormat("List<{0}> {1}s = "
                                    , class_Sub.DtoClassName
                                    , Class_Tool.GetFirstCodeLow(class_Sub.DtoClassName));
                            else
                                stringBuilder.AppendFormat("List<{0}> {1}s = "
                                    , _GetServiceReturnType(class_Sub, false)
                                    , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false)));

                            stringBuilder.AppendFormat("{0}."
                                , Class_Tool.GetFirstCodeLow(class_Sub.ServiceInterFaceName));

                            if (class_WhereFields != null)
                            {
                                if (class_WhereFields.Count > 1)
                                    stringBuilder.AppendFormat("{0}({1});\r\n"
                                        , class_Sub.MethodId
                                        , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName));
                                else if (class_WhereFields.Count == 1)
                                {
                                    stringBuilder.AppendFormat("{0}({1});\r\n"
                                        , class_Sub.MethodId
                                        , class_WhereFields[0].OutFieldName);
                                }
                                else
                                    stringBuilder.AppendFormat("{0}();\r\n"
                                        , class_Sub.MethodId);
                            }
                            if (class_SelectAllModel.IsMultTable)
                            {
                                stringBuilder.AppendFormat("{0}PageInfo pageInfo = new PageInfo<>({1}s, limit);\r\n"
                                    , class_ToolSpace.GetSetSpaceCount(2)
                                    , Class_Tool.GetFirstCodeLow(class_Sub.DtoClassName));
                            }
                            else
                            {
                                stringBuilder.AppendFormat("{0}PageInfo pageInfo = new PageInfo<>({1}s, limit);\r\n"
                                    , class_ToolSpace.GetSetSpaceCount(2)
                                    , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false)));
                            }
                            stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(2) + "return ");
                            if (class_SelectAllModel.ReturnStructure)
                            {
                                stringBuilder.Append("ResultStruct.success(pageInfo);\r\n");
                            }
                            else
                            {
                                stringBuilder.Append("pageInfo;\r\n");
                            }
                        }
                    }
                    else
                    {
                        if ((class_SelectAllModel.class_Create.EnglishSign) && (_GetEnglishFieldList(class_Sub).Count > 0))
                        {
                            if (class_Sub.ServiceInterFaceReturnCount == 0)
                            {
                                stringBuilder.AppendFormat("{0}{1}"
                                    , class_ToolSpace.GetSetSpaceCount(2)
                                    , _GetServiceReturnType(class_Sub, false));
                                stringBuilder.AppendFormat(" {1} = new {0}();\r\n"
                                    , _GetServiceReturnType(class_Sub, false)
                                    , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false))
                                    , class_ToolSpace.GetSetSpaceCount(2));
                                stringBuilder.AppendFormat("{0}{1} = {2}."
                                        , class_ToolSpace.GetSetSpaceCount(2)
                                    , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false))
                                    , Class_Tool.GetFirstCodeLow(class_Sub.ServiceInterFaceName));
                            }
                            else
                            {
                                stringBuilder.AppendFormat("{0}List<{1}>"
                                    , class_ToolSpace.GetSetSpaceCount(2)
                                    , _GetServiceReturnType(class_Sub, false));
                                stringBuilder.AppendFormat(" {1}s = new {0}();\r\n"
                                    , _GetServiceReturnType(class_Sub, false)
                                    , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false))
                                    , class_ToolSpace.GetSetSpaceCount(2));
                                stringBuilder.AppendFormat("{0}{1}s = {2}."
                                        , class_ToolSpace.GetSetSpaceCount(2)
                                    , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false))
                                    , Class_Tool.GetFirstCodeLow(class_Sub.ServiceInterFaceName));
                            }

                            if (class_WhereFields != null)
                            {
                                if (class_WhereFields.Count > 1)
                                    stringBuilder.AppendFormat("{0}({1});\r\n"
                                        , class_Sub.MethodId
                                        , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName));
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
                            stringBuilder.AppendFormat("{0}if (englishSign) ", class_ToolSpace.GetSetSpaceCount(2));
                            stringBuilder.Append("{\r\n");
                            if (class_Sub.ServiceInterFaceReturnCount == 0)
                            {
                                List<Class_EnglishField> class_EnglishFields = new List<Class_EnglishField>();
                                class_EnglishFields = _GetEnglishFieldList(class_Sub);
                                if (class_EnglishFields.Count > 0)
                                {
                                    foreach (Class_EnglishField row in class_EnglishFields)
                                    {
                                        stringBuilder.AppendFormat("{0}{3}.set{1}({3}.get{2}());\r\n"
                                            , class_ToolSpace.GetSetSpaceCount(3)
                                            , Class_Tool.GetFirstCodeUpper(row.FieldChinaName)
                                            , Class_Tool.GetFirstCodeUpper(row.FieldEnglishName)
                                            , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false)));
                                    }
                                }
                            }
                            else
                            {
                                stringBuilder.AppendFormat("{0}{1}.forEach(item ->"
                                    , class_ToolSpace.GetSetSpaceCount(3)
                                    , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false)) + "s");
                                List<Class_EnglishField> class_EnglishFields = new List<Class_EnglishField>();
                                class_EnglishFields = _GetEnglishFieldList(class_Sub);
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
                            if (class_Sub.ServiceInterFaceReturnCount == 0)
                            {
                                stringBuilder.AppendFormat(" {0};\r\n"
                                , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false)));
                            }
                            else
                            {
                                stringBuilder.AppendFormat(" {0}s;\r\n"
                                , Class_Tool.GetFirstCodeLow(_GetServiceReturnType(class_Sub, false)));

                            }
                        }
                        else
                        {
                            if (class_SelectAllModel.ReturnStructure)
                            {
                                stringBuilder.AppendFormat("\r\n{0}return ResultStruct.success({1}."
                                    , class_ToolSpace.GetSetSpaceCount(2)
                                    , Class_Tool.GetFirstCodeLow(class_Sub.ServiceInterFaceName));
                                if (class_WhereFields != null)
                                {
                                    if (class_WhereFields.Count > 1)
                                        stringBuilder.AppendFormat("{0}({1});"
                                            , class_Sub.MethodId
                                            , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName));
                                    else if (class_WhereFields.Count == 1)
                                    {
                                        stringBuilder.AppendFormat("{0}({1});"
                                            , class_Sub.MethodId
                                            , class_WhereFields[0].OutFieldName);
                                    }
                                    else
                                        stringBuilder.AppendFormat("{0}();"
                                            , class_Sub.MethodId);
                                }
                                stringBuilder.Append(")\r\n");
                            }
                            else
                            {
                                stringBuilder.AppendFormat("\r\n{0}return {1}."
                                    , class_ToolSpace.GetSetSpaceCount(2)
                                    , Class_Tool.GetFirstCodeLow(class_Sub.ServiceInterFaceName));
                                if (class_WhereFields != null)
                                {
                                    if (class_WhereFields.Count > 1)
                                        stringBuilder.AppendFormat("{0}({1});\r\n"
                                            , class_Sub.MethodId
                                            , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName));
                                    else if (class_WhereFields.Count == 1)
                                    {
                                        stringBuilder.AppendFormat("{0}({1});\r\n"
                                            , class_Sub.MethodId
                                            , class_WhereFields[0].OutFieldName);
                                    }
                                    else
                                        stringBuilder.AppendFormat("{0}();\r\n"
                                            , class_Sub.MethodId);
                                }
                            }
                        }
                    }
                }
            }
            stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(1) + "}\r\n");
            if (!class_Sub.CreateMainCode)
                stringBuilder.Append("}");
            stringBuilder.Append("\r\n");

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
            class_SelectAllModel = new Class_SelectAllModel();
            if (xmlFileName != null)
            {
                Class_PublicMethod class_PublicMethod = new Class_PublicMethod();
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
        public string GetSql(int Index)
        {
            return _GetSql(Index);
        }
        public string GetServiceInterFace(int Index)
        {
            return _GetServiceInterFace(Index);
        }
        public string GetServiceImpl(int Index)
        {
            return _GetServiceImpl(Index);
        }
        public string GetModel(int Index)
        {
            return _GetModel(Index);
        }
        public string GetDTO(int Index)
        {
            return _GetDTO(Index);
        }
        public string GetDAO(int Index)
        {
            return _GetDAO(Index);
        }
        public string GetControl(int Index)
        {
            return _GetControl(Index);
        }
        public string GetInPutParam(int Index)
        {
            return _GetInPutParam(Index);
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

        public string GetFrontPage()
        {
            return _GetFrontPage() + _GetWhereString();
        }

        public string GetUsedMethod()
        {
            return _GetUsedMethod();
        }

        public List<string> GetComponentType()
        {
            throw new NotImplementedException();
        }

        public string _GetTypeContent(string FieldType)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Feign
        public string GetFeignControl(int PageIndex)
        {
            return _GetFeignControl(PageIndex);
        }
        private string _GetFeignControl(int PageIndex)
        {
            if (class_SelectAllModel.class_SubList == null)
                return null;
            if (class_SelectAllModel.class_SubList[PageIndex] == null)
                return null;
            Class_Sub class_Sub = class_SelectAllModel.class_SubList[PageIndex];
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
            bool MyPage = true;
            MyPage = MyPage && class_SelectAllModel.PageSign;
            MyPage = MyPage && (class_Sub.ServiceInterFaceReturnCount == 0 ? false : true);
            if (MyPage && class_SelectAllModel.ReturnStructure)
                MyPage = MyPage && (class_SelectAllModel.ReturnStructureType == 1 || class_SelectAllModel.ReturnStructureType == 2) ? true : false;

            #region 注释
            if (!class_Sub.CreateMainCode)
            {
                stringBuilder.Append("/**\r\n");
                stringBuilder.Append(_GetAuthor());
                stringBuilder.AppendFormat(" * @create {0}\r\n", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                stringBuilder.Append(" * @function\r\n * @editLog\r\n");
                stringBuilder.Append(" */\r\n");
                stringBuilder.Append("@RestController\r\n");
                if (class_Sub.ControlRequestMapping != null)
                    stringBuilder.AppendFormat("@RequestMapping(\"/{0}\")\r\n"
                        , Class_Tool.GetFirstCodeLow(class_Sub.FeignControlClassName));

                if (class_SelectAllModel.class_Create.SwaggerSign)
                    stringBuilder.AppendFormat("@Api(value = \"{0}\", description = \"{1}\")\r\n"
                        , class_Sub.ControlSwaggerValue
                        , class_Sub.ControlSwaggerDescription);
                stringBuilder.AppendFormat("public class {0}", class_Sub.FeignControlClassName);
                stringBuilder.Append(" {\r\n");
                stringBuilder.AppendFormat("{0}@Autowired\r\n", class_ToolSpace.GetSetSpaceCount(1));
                stringBuilder.AppendFormat("{0}{1} {2};\r\n"
                    , class_ToolSpace.GetSetSpaceCount(1)
                    , class_Sub.FeignInterFaceClassName
                    , Class_Tool.GetFirstCodeLow(class_Sub.FeignInterFaceClassName));
            }

            stringBuilder.AppendFormat("{0}/**\r\n", class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0} * {1}，方法ID：{2}\r\n{0} *\r\n"
                , class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.MethodContent
                , class_SelectAllModel.class_Create.MethodId);
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
                    if (class_Field.IsSame)
                        stringBuilder.AppendFormat("{0} * @param {3} 表{1},原字段名{2},现字段名{3}:{4}\r\n"
                            , class_ToolSpace.GetSetSpaceCount(1)
                            , class_Field.TableName
                            , class_Field.FieldName
                            , class_Field.OutFieldName
                            , class_Field.FieldRemark);
                    else
                        stringBuilder.AppendFormat("{0} * @param {2} 表{1},字段名{2}:{3}\r\n"
                            , class_ToolSpace.GetSetSpaceCount(1)
                            , class_Field.TableName
                            , class_Field.OutFieldName
                            , class_Field.FieldRemark);
                }
            }
            if (MyPage)
            {
                stringBuilder.AppendFormat("{0} * @param {1} {2}\r\n"
                , class_ToolSpace.GetSetSpaceCount(1)
                , "page"
                , "当前页数");
                stringBuilder.AppendFormat("{0} * @param {1} {2}\r\n"
                , class_ToolSpace.GetSetSpaceCount(1)
                , "limit"
                , "每页条数");
            }
            if (class_SelectAllModel.class_Create.EnglishSign)
            {
                stringBuilder.AppendFormat("{0} * @param {1} {2}\r\n"
                    , class_ToolSpace.GetSetSpaceCount(1)
                , "englishSign"
                , "是否是英文版");
            }
            stringBuilder.AppendFormat("{0} * @return {1}\r\n", class_ToolSpace.GetSetSpaceCount(1)
            , class_Sub.ServiceInterFaceReturnRemark);
            stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));
            #endregion

            #region Swagger
            if (class_SelectAllModel.class_Create.SwaggerSign)
            {
                stringBuilder.AppendFormat("{0}@ApiOperation(value = \"{1}\", notes = \"{2}\")\r\n"
                , class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.MethodContent
                , class_Sub.ServiceInterFaceReturnRemark);
                if (class_WhereFields != null && class_WhereFields.Count > 0)
                {
                    stringBuilder.AppendFormat("{0}@ApiImplicitParams(", class_ToolSpace.GetSetSpaceCount(1));
                    stringBuilder.Append("{\r\n");
                    int index = 0;
                    foreach (Class_WhereField row in class_WhereFields)
                    {
                        stringBuilder.AppendFormat("{0}@ApiImplicitParam(name = \"{1}\", value = \"{2}\", dataType = \"{3}\""
                        , class_ToolSpace.GetSetSpaceCount(3)
                        , Class_Tool.GetFirstCodeLow(row.OutFieldName)
                        , row.FieldRemark
                        , Class_Tool.GetSimplificationJavaType(class_InterFaceDataBase.GetJavaType(row.LogType)));
                        if (!row.WhereIsNull)
                            stringBuilder.Append(", required = true");
                        if (row.FieldLogType.IndexOf("IN") > -1)
                            stringBuilder.Append(", paramType = \"query\"");
                        stringBuilder.Append(")");

                        if (index < class_WhereFields.Count - 1)
                            stringBuilder.Append(",");
                        if (index == class_WhereFields.Count - 1 && (class_SelectAllModel.class_Create.EnglishSign || MyPage))
                            stringBuilder.Append(",");
                        stringBuilder.Append("\r\n");
                        index++;
                    }
                    if (MyPage)
                    {
                        stringBuilder.AppendFormat("{0}@ApiImplicitParam(name = \"{1}\", value = \"{2}\", dataType = \"{3}\"),"
                        , class_ToolSpace.GetSetSpaceCount(3)
                        , "page"
                        , "当前页数"
                        , "int");
                        stringBuilder.Append("\r\n");
                        stringBuilder.AppendFormat("{0}@ApiImplicitParam(name = \"{1}\", value = \"{2}\", dataType = \"{3}\")"
                        , class_ToolSpace.GetSetSpaceCount(3)
                        , "limit"
                        , "每页条数"
                        , "int");
                        stringBuilder.Append("\r\n");
                    }

                    if (class_SelectAllModel.class_Create.EnglishSign)
                        stringBuilder.AppendFormat("{0}@ApiImplicitParam(name = \"englishSign\", value = \"是否生成英文\", required = true, dataType = \"Boolean\")\r\n"
                        , class_ToolSpace.GetSetSpaceCount(3));

                    stringBuilder.AppendFormat("{0}", class_ToolSpace.GetSetSpaceCount(1));
                    stringBuilder.Append("})\r\n");
                }
                else
                {
                    if (MyPage)
                    {
                        stringBuilder.AppendFormat("{0}@ApiImplicitParams(", class_ToolSpace.GetSetSpaceCount(1));
                        stringBuilder.Append("{\r\n");
                        stringBuilder.AppendFormat("{0}@ApiImplicitParam(name = \"{1}\", value = \"{2}\", dataType = \"{3}\"),"
                        , class_ToolSpace.GetSetSpaceCount(3)
                        , "page"
                        , "当前页数"
                        , "int");
                        stringBuilder.Append("\r\n");
                        stringBuilder.AppendFormat("{0}@ApiImplicitParam(name = \"{1}\", value = \"{2}\", dataType = \"{3}\")"
                        , class_ToolSpace.GetSetSpaceCount(3)
                        , "limit"
                        , "每页条数"
                        , "int");
                        stringBuilder.Append("\r\n");
                        stringBuilder.AppendFormat("{0}", class_ToolSpace.GetSetSpaceCount(1));
                        stringBuilder.Append("})\r\n");
                    }
                }
            }
            #endregion

            stringBuilder.AppendFormat("{0}@{1}Mapping(\"/{2}\")\r\n"
            , class_ToolSpace.GetSetSpaceCount(1)
            , class_SelectAllModel.class_Create.HttpRequestType
            , class_Sub.MethodId);
            stringBuilder.AppendFormat("{0}public ", class_ToolSpace.GetSetSpaceCount(1));

            #region 返回值
            if (class_SelectAllModel.ReturnStructure)
            {
                int StructureType = class_SelectAllModel.ReturnStructureType;
                if (class_Sub.ServiceInterFaceReturnCount == 0)
                    StructureType = 0;
                switch (StructureType)
                {
                    case 0:
                        stringBuilder.Append("ResultVO");
                        break;
                    case 1:
                        stringBuilder.Append("ResultVOPage");
                        break;
                    case 2:
                        stringBuilder.Append("ResultVOPageTotal");
                        break;
                    case 3:
                        stringBuilder.Append("ResultVOTotal");
                        break;
                    default:
                        stringBuilder.Append("ResultVOPage");
                        break;
                }
            }
            else
            {
                if (class_Sub.ServiceInterFaceReturnCount == 0)
                {
                    if (class_SelectAllModel.IsMultTable)
                        stringBuilder.AppendFormat("{0}", class_Sub.DtoClassName);
                    else
                        stringBuilder.AppendFormat("{0}", _GetServiceReturnType(class_Sub, false));

                }
                else
                {
                    if (MyPage)
                    {
                        stringBuilder.Append("PageInfo");
                    }
                    else
                    {
                        if (class_SelectAllModel.IsMultTable)
                            stringBuilder.AppendFormat("List<{0}>", class_Sub.DtoClassName);
                        else
                            stringBuilder.AppendFormat("List<{0}>", _GetServiceReturnType(class_Sub, false));
                    }
                }
            }
            #endregion

            stringBuilder.AppendFormat(" {0}", class_Sub.MethodId);
            stringBuilder.Append("(");
            int Index = 0;
            foreach (Class_WhereField row in class_WhereFields)
            {
                if (Index++ > 0)
                    stringBuilder.AppendFormat("\r\n{1}, @RequestParam(value = \"{0}\""
                    , row.OutFieldName
                    , class_ToolSpace.GetSetSpaceCount(3));
                else
                    stringBuilder.AppendFormat("@RequestParam(value = \"{0}\""
                    , row.OutFieldName);
                if (row.WhereIsNull)
                {
                    stringBuilder.Append(", required = false");
                }
                if (!row.FieldDefaultValue.Equals("CURRENT_TIMESTAMP") && (row.FieldDefaultValue != null) && (row.FieldDefaultValue.Length > 0))
                    stringBuilder.AppendFormat(", defaultValue = \"{0}\"", _GetFieldDefaultValue(row.FieldDefaultValue));
                stringBuilder.Append(")");
                if (row.FieldType.IndexOf("date") > -1 && row.LogType.IndexOf("IN") < 0)
                {
                    if (row.FieldType.Equals("date"))
                        stringBuilder.Append(" @DateTimeFormat(pattern = \"yyyy-MM-dd\")");
                    if (row.FieldType.Equals("datetime"))
                        stringBuilder.Append(" @DateTimeFormat(pattern = \"yyyy-MM-dd HH:mm:ss\")");
                }
                if (row.FieldLogType.IndexOf("IN") > -1)
                {
                    stringBuilder.AppendFormat(" List<{0}>"
                    , Class_Tool.GetSimplificationJavaType(class_InterFaceDataBase.GetJavaType(row.LogType)));
                }
                else
                    stringBuilder.AppendFormat(" {0}"
                    , Class_Tool.GetSimplificationJavaType(class_InterFaceDataBase.GetJavaType(row.LogType)));
                stringBuilder.AppendFormat(" {0}", row.OutFieldName);
            }
            if (MyPage)
            {
                if (Index > 0)
                {
                    stringBuilder.AppendFormat("\r\n{1}, @RequestParam(value = \"{0}\", defaultValue = \"1\") int {0}"
                    , "page"
                    , class_ToolSpace.GetSetSpaceCount(3));
                    stringBuilder.AppendFormat("\r\n{1}, @RequestParam(value = \"{0}\", defaultValue = \"10\") int {0}"
                    , "limit"
                    , class_ToolSpace.GetSetSpaceCount(3));
                }
                else
                {
                    stringBuilder.AppendFormat("\r\n{1} @RequestParam(value = \"{0}\", defaultValue = \"1\") int {0},"
                    , "page"
                    , class_ToolSpace.GetSetSpaceCount(3));
                    stringBuilder.AppendFormat("\r\n{1} @RequestParam(value = \"{0}\", defaultValue = \"10\") int {0}"
                    , "limit"
                    , class_ToolSpace.GetSetSpaceCount(3));
                }
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

            #region 返回值
            stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(2));
            if (class_SelectAllModel.ReturnStructure)
            {
                int StructureType = class_SelectAllModel.ReturnStructureType;
                if (class_Sub.ServiceInterFaceReturnCount == 0)
                    StructureType = 0;
                switch (StructureType)
                {
                    case 0:
                        stringBuilder.Append("ResultVO");
                        break;
                    case 1:
                        stringBuilder.Append("ResultVOPage");
                        break;
                    case 2:
                        stringBuilder.Append("ResultVOPageTotal");
                        break;
                    case 3:
                        stringBuilder.Append("ResultVOTotal");
                        break;
                    default:
                        stringBuilder.Append("ResultVOPage");
                        break;
                }
            }
            else
            {
                if (class_Sub.ServiceInterFaceReturnCount == 0)
                {
                    if (class_SelectAllModel.IsMultTable)
                        stringBuilder.AppendFormat("{0}", class_Sub.DtoClassName);
                    else
                        stringBuilder.AppendFormat("{0}", _GetServiceReturnType(class_Sub, false));

                }
                else
                {
                    if (MyPage)
                    {
                        stringBuilder.Append("PageInfo");
                    }
                    else
                    {
                        if (class_SelectAllModel.IsMultTable)
                            stringBuilder.AppendFormat("List<{0}>", class_Sub.DtoClassName);
                        else
                            stringBuilder.AppendFormat("List<{0}>", _GetServiceReturnType(class_Sub, false));
                    }
                }
            }
            #endregion

            stringBuilder.AppendFormat(" resultValue = {0}."
                , Class_Tool.GetFirstCodeLow(class_Sub.FeignInterFaceClassName));
            stringBuilder.AppendFormat(" {0}", class_Sub.MethodId);
            stringBuilder.Append("(");
            Index = 0;
            foreach (Class_WhereField row in class_WhereFields)
            {
                if (Index++ > 0)
                    stringBuilder.Append(" ,");
                stringBuilder.AppendFormat("{0}", row.OutFieldName);
            }
            if (MyPage)
            {
                if (Index > 0)
                {
                    stringBuilder.AppendFormat("\r\n{1}, int {0}"
                    , "page"
                    , class_ToolSpace.GetSetSpaceCount(3));
                    stringBuilder.AppendFormat("\r\n{1}, int {0}"
                    , "limit"
                    , class_ToolSpace.GetSetSpaceCount(3));
                }
                else
                {
                    stringBuilder.AppendFormat("\r\n{1} int {0},"
                    , "page"
                    , class_ToolSpace.GetSetSpaceCount(3));
                    stringBuilder.AppendFormat("\r\n{1} int {0}"
                    , "limit"
                    , class_ToolSpace.GetSetSpaceCount(3));
                }
            }
            if (class_SelectAllModel.class_Create.EnglishSign)
            {
                if (Index > 0)
                {
                    stringBuilder.AppendFormat("\r\n{1}, Boolean {0}"
                    , "englishSign"
                    , class_ToolSpace.GetSetSpaceCount(3));
                }
                else
                {
                    stringBuilder.AppendFormat("\r\n{1} Boolean {0}"
                    , "englishSign"
                    , class_ToolSpace.GetSetSpaceCount(3));
                }
            }
            stringBuilder.Append(");\r\n");
            stringBuilder.AppendFormat("\r\n{0}return resultValue;\r\n"
                , class_ToolSpace.GetSetSpaceCount(2));
            stringBuilder.AppendFormat("{0}}}\r\n"
                , class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.Append("\r\n");
            if (!class_Sub.CreateMainCode)
                stringBuilder.Append("}\r\n");
            return stringBuilder.ToString();
        }

        public string GetFeignInterFace(int PageIndex)
        {
            return _GetFeignInterFace(PageIndex);
        }

        private string _GetFeignInterFace(int PageIndex)
        {
            if (class_SelectAllModel.class_SubList == null)
                return null;
            if (class_SelectAllModel.class_SubList[PageIndex] == null)
                return null;
            Class_Sub class_Sub = class_SelectAllModel.class_SubList[PageIndex];
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
            bool MyPage = true;
            MyPage = MyPage && class_SelectAllModel.PageSign;
            MyPage = MyPage && (class_Sub.ServiceInterFaceReturnCount == 0 ? false : true);
            if (MyPage && class_SelectAllModel.ReturnStructure)
                MyPage = MyPage && (class_SelectAllModel.ReturnStructureType == 1 || class_SelectAllModel.ReturnStructureType == 2) ? true : false;

            #region 注释
            if (!class_Sub.CreateMainCode)
            {
                stringBuilder.Append("/**\r\n");
                stringBuilder.Append(_GetAuthor());
                stringBuilder.AppendFormat(" * @create {0}\r\n", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                stringBuilder.Append(" * @function\r\n * @editLog\r\n");
                stringBuilder.Append(" */\r\n");
                stringBuilder.Append("@Repository\r\n");
                stringBuilder.Append("@");
                stringBuilder.AppendFormat("FeignClient(value = \"{0}/{1}\"" +
                    ", fallback = {2}.class)\r\n"
                    , class_SelectAllModel.class_Create.MicroServiceName
                    , class_Sub.ControlRequestMapping
                    , class_Sub.FeignInterFaceHystricClassName);

                stringBuilder.AppendFormat("public interface {0} {{\r\n", class_Sub.FeignInterFaceClassName);
            }
            #endregion

            stringBuilder.AppendFormat("{0}@{1}Mapping(\"/{2}\")\r\n"
                , class_ToolSpace.GetSetSpaceCount(1)
                , class_SelectAllModel.class_Create.HttpRequestType
                , class_Sub.MethodId);

            #region 返回值
            stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(1));
            if (class_SelectAllModel.ReturnStructure)
            {
                int StructureType = class_SelectAllModel.ReturnStructureType;
                if (class_Sub.ServiceInterFaceReturnCount == 0)
                    StructureType = 0;
                switch (StructureType)
                {
                    case 0:
                        stringBuilder.Append("ResultVO");
                        break;
                    case 1:
                        stringBuilder.Append("ResultVOPage");
                        break;
                    case 2:
                        stringBuilder.Append("ResultVOPageTotal");
                        break;
                    case 3:
                        stringBuilder.Append("ResultVOTotal");
                        break;
                    default:
                        stringBuilder.Append("ResultVOPage");
                        break;
                }
            }
            else
            {
                if (class_Sub.ServiceInterFaceReturnCount == 0)
                {
                    if (class_SelectAllModel.IsMultTable)
                        stringBuilder.AppendFormat("{0}", class_Sub.DtoClassName);
                    else
                        stringBuilder.AppendFormat("{0}", _GetServiceReturnType(class_Sub, false));

                }
                else
                {
                    if (MyPage)
                    {
                        stringBuilder.Append("PageInfo");
                    }
                    else
                    {
                        if (class_SelectAllModel.IsMultTable)
                            stringBuilder.AppendFormat("List<{0}>", class_Sub.DtoClassName);
                        else
                            stringBuilder.AppendFormat("List<{0}>", _GetServiceReturnType(class_Sub, false));
                    }
                }
            }
            #endregion
            stringBuilder.AppendFormat(" {0}", class_Sub.MethodId);
            stringBuilder.Append("(");
            class_WhereFields = _GetParameterType();
            int Index = 0;
            foreach (Class_WhereField row in class_WhereFields)
            {
                if (Index++ > 0)
                    stringBuilder.AppendFormat("\r\n{1}, @RequestParam(value = \"{0}\""
                    , row.OutFieldName
                    , class_ToolSpace.GetSetSpaceCount(3));
                else
                    stringBuilder.AppendFormat("@RequestParam(value = \"{0}\""
                    , row.OutFieldName);
                if (row.WhereIsNull)
                {
                    stringBuilder.Append(", required = false");
                }
                if (!row.FieldDefaultValue.Equals("CURRENT_TIMESTAMP") && (row.FieldDefaultValue != null) && (row.FieldDefaultValue.Length > 0))
                    stringBuilder.AppendFormat(", defaultValue = \"{0}\"", _GetFieldDefaultValue(row.FieldDefaultValue));
                stringBuilder.Append(")");
                if (row.FieldType.IndexOf("date") > -1 && row.LogType.IndexOf("IN") < 0)
                {
                    if (row.FieldType.Equals("date"))
                        stringBuilder.Append(" @DateTimeFormat(pattern = \"yyyy-MM-dd\")");
                    if (row.FieldType.Equals("datetime"))
                        stringBuilder.Append(" @DateTimeFormat(pattern = \"yyyy-MM-dd HH:mm:ss\")");
                }
                if (row.FieldLogType.IndexOf("IN") > -1)
                {
                    stringBuilder.AppendFormat(" List<{0}>"
                    , Class_Tool.GetSimplificationJavaType(class_InterFaceDataBase.GetJavaType(row.LogType)));
                }
                else
                    stringBuilder.AppendFormat(" {0}"
                    , Class_Tool.GetSimplificationJavaType(class_InterFaceDataBase.GetJavaType(row.LogType)));
                stringBuilder.AppendFormat(" {0}", row.OutFieldName);
            }
            if (MyPage)
            {
                if (Index > 0)
                {
                    stringBuilder.AppendFormat("\r\n{1}, @RequestParam(value = \"{0}\", defaultValue = \"1\") int {0}"
                    , "page"
                    , class_ToolSpace.GetSetSpaceCount(3));
                    stringBuilder.AppendFormat("\r\n{1}, @RequestParam(value = \"{0}\", defaultValue = \"10\") int {0}"
                    , "limit"
                    , class_ToolSpace.GetSetSpaceCount(3));
                }
                else
                {
                    stringBuilder.AppendFormat("\r\n{1} @RequestParam(value = \"{0}\", defaultValue = \"1\") int {0},"
                    , "page"
                    , class_ToolSpace.GetSetSpaceCount(3));
                    stringBuilder.AppendFormat("\r\n{1} @RequestParam(value = \"{0}\", defaultValue = \"10\") int {0}"
                    , "limit"
                    , class_ToolSpace.GetSetSpaceCount(3));
                }
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
            stringBuilder.Append(");\r\n");


            if (!class_Sub.CreateMainCode)
                stringBuilder.Append("}\r\n");
            return stringBuilder.ToString();
        }

        public string GetFeignInterFaceHystric(int PageIndex)
        {
            return _GetFeignInterFaceHystric(PageIndex);
        }

        private string _GetFeignInterFaceHystric(int PageIndex)
        {
            if (class_SelectAllModel.class_SubList == null)
                return null;
            if (class_SelectAllModel.class_SubList[PageIndex] == null)
                return null;
            Class_Sub class_Sub = class_SelectAllModel.class_SubList[PageIndex];
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

            bool MyPage = true;
            MyPage = MyPage && class_SelectAllModel.PageSign;
            MyPage = MyPage && (class_Sub.ServiceInterFaceReturnCount == 0 ? false : true);
            if (MyPage && class_SelectAllModel.ReturnStructure)
                MyPage = MyPage && (class_SelectAllModel.ReturnStructureType == 1 || class_SelectAllModel.ReturnStructureType == 2) ? true : false;

            #region 注释
            if (!class_Sub.CreateMainCode)
            {
                stringBuilder.Append("/**\r\n");
                stringBuilder.Append(_GetAuthor());
                stringBuilder.AppendFormat(" * @create {0}\r\n", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                stringBuilder.Append(" * @function\r\n * @editLog\r\n");
                stringBuilder.Append(" */\r\n");
                stringBuilder.Append("@Service\r\n");
                stringBuilder.AppendFormat("public class {0} implements {1} {{\r\n"
                    , class_Sub.FeignInterFaceHystricClassName
                    , class_Sub.FeignInterFaceClassName);

                stringBuilder.AppendFormat("{0}private final String hystricMessage = \"亲，服务器正忙，请稍后再戳。\";\r\n\r\n"
                    , class_ToolSpace.GetSetSpaceCount(1));
            }
            #endregion
            stringBuilder.AppendFormat("{0}@Override\r\n"
                , class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0}public ", class_ToolSpace.GetSetSpaceCount(1));

            #region 返回值
            if (class_SelectAllModel.ReturnStructure)
            {
                int StructureType = class_SelectAllModel.ReturnStructureType;
                if (class_Sub.ServiceInterFaceReturnCount == 0)
                    StructureType = 0;
                switch (StructureType)
                {
                    case 0:
                        stringBuilder.Append("ResultVO");
                        break;
                    case 1:
                        stringBuilder.Append("ResultVOPage");
                        break;
                    case 2:
                        stringBuilder.Append("ResultVOPageTotal");
                        break;
                    case 3:
                        stringBuilder.Append("ResultVOTotal");
                        break;
                    default:
                        stringBuilder.Append("ResultVOPage");
                        break;
                }
            }
            else
            {
                if (class_Sub.ServiceInterFaceReturnCount == 0)
                {
                    if (class_SelectAllModel.IsMultTable)
                        stringBuilder.AppendFormat("{0}", class_Sub.DtoClassName);
                    else
                        stringBuilder.AppendFormat("{0}", _GetServiceReturnType(class_Sub, false));

                }
                else
                {
                    if (MyPage)
                    {
                        stringBuilder.Append("PageInfo");
                    }
                    else
                    {
                        if (class_SelectAllModel.IsMultTable)
                            stringBuilder.AppendFormat("List<{0}>", class_Sub.DtoClassName);
                        else
                            stringBuilder.AppendFormat("List<{0}>", _GetServiceReturnType(class_Sub, false));
                    }
                }
            }
            #endregion

            stringBuilder.AppendFormat(" {0}", class_Sub.MethodId);
            stringBuilder.Append("(");
            class_WhereFields = _GetParameterType();
            int Index = 0;
            foreach (Class_WhereField row in class_WhereFields)
            {
                if (Index++ > 0)
                    stringBuilder.AppendFormat("\r\n{1}, @RequestParam(value = \"{0}\""
                    , row.OutFieldName
                    , class_ToolSpace.GetSetSpaceCount(3));
                else
                    stringBuilder.AppendFormat("@RequestParam(value = \"{0}\""
                    , row.OutFieldName);
                if (row.WhereIsNull)
                {
                    stringBuilder.Append(", required = false");
                }
                if (!row.FieldDefaultValue.Equals("CURRENT_TIMESTAMP") && (row.FieldDefaultValue != null) && (row.FieldDefaultValue.Length > 0))
                    stringBuilder.AppendFormat(", defaultValue = \"{0}\"", _GetFieldDefaultValue(row.FieldDefaultValue));
                stringBuilder.Append(")");
                if (row.FieldType.IndexOf("date") > -1 && row.LogType.IndexOf("IN") < 0)
                {
                    if (row.FieldType.Equals("date"))
                        stringBuilder.Append(" @DateTimeFormat(pattern = \"yyyy-MM-dd\")");
                    if (row.FieldType.Equals("datetime"))
                        stringBuilder.Append(" @DateTimeFormat(pattern = \"yyyy-MM-dd HH:mm:ss\")");
                }
                if (row.FieldLogType.IndexOf("IN") > -1)
                {
                    stringBuilder.AppendFormat(" List<{0}>"
                    , Class_Tool.GetSimplificationJavaType(class_InterFaceDataBase.GetJavaType(row.LogType)));
                }
                else
                    stringBuilder.AppendFormat(" {0}"
                    , Class_Tool.GetSimplificationJavaType(class_InterFaceDataBase.GetJavaType(row.LogType)));
                stringBuilder.AppendFormat(" {0}", row.OutFieldName);
            }
            if (MyPage)
            {
                if (Index > 0)
                {
                    stringBuilder.AppendFormat("\r\n{1}, @RequestParam(value = \"{0}\", defaultValue = \"1\") int {0}"
                    , "page"
                    , class_ToolSpace.GetSetSpaceCount(3));
                    stringBuilder.AppendFormat("\r\n{1}, @RequestParam(value = \"{0}\", defaultValue = \"10\") int {0}"
                    , "limit"
                    , class_ToolSpace.GetSetSpaceCount(3));
                }
                else
                {
                    stringBuilder.AppendFormat("\r\n{1} @RequestParam(value = \"{0}\", defaultValue = \"1\") int {0},"
                    , "page"
                    , class_ToolSpace.GetSetSpaceCount(3));
                    stringBuilder.AppendFormat("\r\n{1} @RequestParam(value = \"{0}\", defaultValue = \"10\") int {0}"
                    , "limit"
                    , class_ToolSpace.GetSetSpaceCount(3));
                }
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

            stringBuilder.AppendFormat("{0}return ResultStruct.error(hystricMessage, ResultVO.class, null);\r\n"
                , class_ToolSpace.GetSetSpaceCount(2));
            stringBuilder.AppendFormat("{0}}}\r\n"
                , class_ToolSpace.GetSetSpaceCount(1));
            if (!class_Sub.CreateMainCode)
                stringBuilder.Append("}\r\n");
            return stringBuilder.ToString();

        }

        public string _GetFieldDefaultValue(string DefaultValue)
        {
            string ResultValue = DefaultValue;
            if (DefaultValue == "b\'0\'")
                ResultValue = "0";
            if (DefaultValue == "b\'1\'")
                ResultValue = "1";
            return ResultValue;
        }
        #endregion
    }
}
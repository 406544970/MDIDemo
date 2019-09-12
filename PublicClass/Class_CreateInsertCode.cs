using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MDIDemo.PublicClass.Class_InsertAllModel;

namespace MDIDemo.PublicClass
{
    public class Class_CreateInsertCode : IClass_InterFaceCreateCode, IClass_CreateFrontPage
    {
        public Class_CreateInsertCode()
        {
            InitClass(null);
        }
        public Class_CreateInsertCode(string xmlFileName)
        {
            InitClass(xmlFileName);
        }
        private void InitClass(string xmlFileName)
        {
            class_InsertAllModel = new Class_InsertAllModel();
            if (xmlFileName != null)
            {
                Class_PublicMethod class_PublicMethod = new Class_PublicMethod();
                class_InsertAllModel = class_PublicMethod.FromXmlToInsertObject<Class_InsertAllModel>(xmlFileName);
            }
            class_SQLiteOperator = new Class_SQLiteOperator();
        }
        private Class_SQLiteOperator class_SQLiteOperator;
        private Class_InsertAllModel class_InsertAllModel;
        public void AddAllOutFieldName()
        {
            throw new NotImplementedException();
        }

        public string GetControl(int Index)
        {
            return "没做";
        }

        public string GetDAO(int Index)
        {
            return "没做";
        }

        public string GetDTO(int Index)
        {
            return "没做";
        }

        public string GetFrontPage()
        {
            return "没做";
        }

        public string GetInPutParam(int Index)
        {
            return "没做";
        }

        public string GetMap(int Index)
        {
            return null;
        }

        public string GetModel(int Index)
        {
            return "没做";
        }

        public string GetServiceImpl(int Index)
        {
            return "没做";
        }

        public string GetServiceInterFace(int Index)
        {
            return "没做";
        }

        public string GetSql(int Index)
        {
            return _GetSql(Index);
        }
        private string _GetSql(int PageIndex)
        {
            if (class_InsertAllModel.class_SubList.Count < PageIndex)
                return null;
            Class_Sub class_Sub = class_InsertAllModel.class_SubList[PageIndex];
            if (class_Sub == null)
                return null;
            List<Class_WhereField> class_WhereFields = new List<Class_WhereField>();
            Class_Tool class_ToolSpace = new Class_Tool();
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder stringBuilderField = new StringBuilder();
            StringBuilder stringBuilderValue = new StringBuilder();
            IClass_InterFaceDataBase class_InterFaceDataBase;

            switch (class_InsertAllModel.class_SelectDataBase.databaseType)
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
            if (!class_Sub.IsAddXmlHead)
            {
                stringBuilder.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>\r\n");
                stringBuilder.Append("<!DOCTYPE mapper PUBLIC \"-//mybatis.org//DTD Mapper 3.0//EN\" \"http://mybatis.org/dtd/mybatis-3-mapper.dtd\" >\r\n");
                stringBuilder.AppendFormat("<mapper namespace=\"{0}.dao.{1}\">\r\n"
                    , class_InsertAllModel.AllPackerName
                    , class_Sub.DaoClassName);
            }
            #region SelectId
            stringBuilder.AppendFormat("{1}<!-- 注释：{0} -->\r\n", class_Sub.MethodContent, class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0}<insert id=\"{1}\" parameterType=\"{2}.model.InPutParam.{3}\">\r\n"
                , class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.MethodId
                , class_InsertAllModel.AllPackerName
                , class_InsertAllModel.class_SubList[PageIndex].ModelClassName);
            #endregion

            #region Insert Values
            stringBuilder.AppendFormat("{0}INSERT INTO {1} (\r\n"
                , class_ToolSpace.GetSetSpaceCount(2)
                , class_Sub.TableName);
            int Counter = 0;
            foreach (Class_Field class_Field in class_Sub.class_Fields)
            {
                bool IsAddField = false;
                if (!class_Field.FieldIsKey)
                {
                    IsAddField = true;
                }
                else
                    IsAddField = !class_Field.FieldIsAutoAdd;

                if (IsAddField && !class_Field.FieldIsAutoAdd && class_Field.FieldIsNull)
                {
                    stringBuilderField.AppendFormat("{0}<if test=\"{1} != null\">\r\n"
                        , class_ToolSpace.GetSetSpaceCount(3)
                        , class_Field.ParaName);
                    stringBuilderValue.AppendFormat("{0}<if test=\"{1} != null\">\r\n"
                        , class_ToolSpace.GetSetSpaceCount(3)
                        , class_Field.ParaName);
                }
                if (IsAddField)
                {
                    stringBuilderField.Append(class_ToolSpace.GetSetSpaceCount(4));
                    stringBuilderValue.Append(class_ToolSpace.GetSetSpaceCount(4));
                    if (Counter > 0)
                    {
                        stringBuilderField.Append(",");
                        stringBuilderValue.Append(",");
                    }
                    stringBuilderField.AppendFormat("{0}\r\n"
                        , class_Field.FieldName);
                    stringBuilderValue.AppendFormat("#{{{0},jdbcType={1}}}\r\n"
                        , class_Field.ParaName
                        , Class_Tool.GetJdbcType(class_InterFaceDataBase.GetJavaType(class_Field.FieldType)));
                    Counter++;
                }
                if (IsAddField && !class_Field.FieldIsAutoAdd && class_Field.FieldIsNull)
                {
                    stringBuilderField.AppendFormat("{0}</if>\r\n"
                        , class_ToolSpace.GetSetSpaceCount(3));
                    stringBuilderValue.AppendFormat("{0}</if>\r\n"
                        , class_ToolSpace.GetSetSpaceCount(3));
                }
            }
            #endregion

            #region FROM
            //Counter = 0;
            //foreach (Class_Sub item in class_InsertAllModel.class_SubList)
            //{
            //    string AliasName = item.AliasName;
            //    if (Counter > 0)
            //    {
            //        stringBuilder.AppendFormat("{0}", class_ToolSpace.GetSetSpaceCount(2));
            //        if (item.InnerType == 0)
            //            stringBuilder.AppendFormat("LEFT JOIN ");
            //        else
            //            stringBuilder.AppendFormat("INNER JOIN ");
            //    }
            //    else
            //    {
            //        stringBuilder.AppendFormat("{0}FROM ", class_ToolSpace.GetSetSpaceCount(2));
            //    }

            //    if (class_InsertAllModel.IsMultTable)
            //    {
            //        stringBuilder.AppendFormat("{0} AS {1} "
            //            , item.TableName
            //            , AliasName);
            //        if (Counter > 0)
            //            stringBuilder.AppendFormat("ON {0} = {1}\r\n"
            //                , class_InsertAllModel.class_SubList[item.TableNo].AliasName + "." + item.OutFieldName
            //                , AliasName + "." + item.MainTableFieldName);
            //        else
            //            stringBuilder.Append("\r\n");
            //    }
            //    else
            //        stringBuilder.AppendFormat(" {1}\r\n", class_ToolSpace.GetSetSpaceCount(2), item.TableName);
            //    Counter++;
            //}
            #endregion

            #region WHERE
            //stringBuilder.Append(_GetMainWhereLable());
            #endregion

            stringBuilder.Append(stringBuilderField.ToString().Substring(1));
            stringBuilder.AppendFormat("{0})\r\n", class_ToolSpace.GetSetSpaceCount(2));
            stringBuilder.AppendFormat("{0}VALUES (\r\n", class_ToolSpace.GetSetSpaceCount(2));
            stringBuilder.Append(stringBuilderValue.ToString().Substring(1));
            stringBuilder.AppendFormat("{0})\r\n", class_ToolSpace.GetSetSpaceCount(2));
            stringBuilder.AppendFormat("{0}</insert>\r\n", class_ToolSpace.GetSetSpaceCount(1));

            //if (class_Sub.ResultType > 0 && !class_Sub.IsAddXmlHead)
            //{
            //    stringBuilder.Append("</mapper>\r\n");
            //}
            if (stringBuilder.Length > 0)
                return stringBuilder.ToString();
            else
                return null;
        }

        public string GetTestSql(int Index)
        {
            return "没做";
        }

        public string GetUsedMethod()
        {
            return "没做";
        }

        public bool IsCheckOk(ref List<string> outMessage)
        {
            return true;
        }

        public List<string> GetComponentType()
        {
            return class_SQLiteOperator.GetComponentList();
        }
    }
}

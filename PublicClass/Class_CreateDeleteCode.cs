﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MDIDemo.PublicClass.Class_DeleteAllModel;

namespace MDIDemo.PublicClass
{
    public class Class_CreateDeleteCode : IClass_InterFaceCreateCode, IClass_CreateFrontPage
    {
        public Class_CreateDeleteCode(IClass_InterFaceDataBase class_InterFaceDataBase)
        {
            InitClass(class_InterFaceDataBase, null);
        }
        public Class_CreateDeleteCode(IClass_InterFaceDataBase class_InterFaceDataBase,string xmlFileName)
        {
            InitClass(class_InterFaceDataBase,xmlFileName);
        }
        private IClass_InterFaceDataBase class_InterFaceDataBase;
        private Class_SQLiteOperator class_SQLiteOperator;
        private Class_DeleteAllModel class_DeleteAllModel;
        #region 私有
        private void InitClass(IClass_InterFaceDataBase class_InterFaceDataBase, string xmlFileName)
        {
            this.class_InterFaceDataBase = class_InterFaceDataBase;
            class_DeleteAllModel = new Class_DeleteAllModel();
            if (xmlFileName != null)
            {
                Class_PublicMethod class_PublicMethod = new Class_PublicMethod();
                class_DeleteAllModel = class_PublicMethod.FromXmlToDeleteObject<Class_DeleteAllModel>(xmlFileName);
            }
            class_SQLiteOperator = new Class_SQLiteOperator();
        }
        private string _GetControl(int PageIndex)
        {
            string KeyType = null;
            if (class_DeleteAllModel.class_SubList == null)
                return null;
            if (class_DeleteAllModel.class_SubList[PageIndex] == null)
                return null;
            int ParamCounter = 0;
            Class_Sub class_Sub = class_DeleteAllModel.class_SubList[PageIndex];
            Class_Tool class_ToolSpace = new Class_Tool();
            StringBuilder stringBuilder = new StringBuilder();

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

                if (class_DeleteAllModel.class_Create.SwaggerSign)
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
                , class_DeleteAllModel.class_Create.MethodId);
            foreach (Class_Field class_Field in class_Sub.class_Fields)
            {
                ParamCounter += class_Field.WhereSelect ? 1 : 0;
                if (class_Field.WhereSelect)
                    stringBuilder.AppendFormat("{0} * @param {1} {2}\r\n"
                        , class_ToolSpace.GetSetSpaceCount(1)
                        , class_Field.ParaName
                        , class_Field.FieldRemark);
                if (class_Field.FieldIsKey && KeyType == null)
                {
                    KeyType = Class_Tool.GetSimplificationJavaType(class_InterFaceDataBase.GetJavaType(class_Field.FieldType));
                }
            }

            stringBuilder.AppendFormat("{0} * @return {1}\r\n", class_ToolSpace.GetSetSpaceCount(1)
            , class_Sub.ServiceInterFaceReturnRemark);
            stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));
            #endregion

            #region Swagger
            if (class_DeleteAllModel.class_Create.SwaggerSign)
            {
                stringBuilder.AppendFormat("{0}@ApiOperation(value = \"{1}\", notes = \"{2}\")\r\n"
                , class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.MethodContent
                , class_Sub.ServiceInterFaceReturnRemark);
                if (class_Sub.class_Fields != null && class_Sub.class_Fields.Count > 0)
                {
                    stringBuilder.AppendFormat("{0}@ApiImplicitParams({{\r\n", class_ToolSpace.GetSetSpaceCount(1));
                    int index = 0;
                    foreach (Class_Field class_Field in class_Sub.class_Fields)
                    {
                        if (class_Field.WhereSelect)
                        {
                            stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(3));
                            if (index > 0)
                                stringBuilder.Append(", ");
                            stringBuilder.AppendFormat("@ApiImplicitParam(name = \"{0}\", value = \"{1}\", dataType = \"{2}\""
                                , Class_Tool.GetFirstCodeLow(class_Field.FieldName)
                                , class_Field.FieldRemark
                                , Class_Tool.GetSimplificationJavaType(class_InterFaceDataBase.GetJavaType(class_Field.FieldType)));
                            if (!class_Field.WhereIsNull)
                                stringBuilder.Append(", required = true");
                            if (class_Field.LogType.IndexOf("IN") > -1)
                                stringBuilder.Append(", paramType = \"query\"");
                            stringBuilder.Append(")\r\n");
                            index++;
                        }
                    }
                    stringBuilder.AppendFormat("{0}}})\r\n", class_ToolSpace.GetSetSpaceCount(1));
                }
            }
            #endregion

            stringBuilder.AppendFormat("{0}@{1}Mapping(\"/{2}\")\r\n"
            , class_ToolSpace.GetSetSpaceCount(1)
            , class_DeleteAllModel.class_Create.HttpRequestType
            , class_Sub.MethodId);
            stringBuilder.AppendFormat("{0}public ", class_ToolSpace.GetSetSpaceCount(1));

            #region
            if (class_DeleteAllModel.ReturnStructure)
            {
                stringBuilder.Append("ResultVO");
            }
            else
            {
                switch (class_Sub.ServiceInterFaceReturnCount)
                {
                    case 0:
                        stringBuilder.Append("int");
                        break;
                    case 1:
                        stringBuilder.Append(KeyType);
                        break;
                    case 2:
                        stringBuilder.AppendFormat("{0}", class_Sub.ParamClassName);
                        break;
                    default:
                        break;
                }
            }
            #endregion

            stringBuilder.AppendFormat(" {0}", class_Sub.MethodId);
            stringBuilder.Append("(");
            int Index = 0;
            foreach (Class_Field class_Field in class_Sub.class_Fields)
            {
                if (class_Field.WhereSelect)
                {
                    if (Index++ > 0)
                        stringBuilder.AppendFormat("\r\n{1}, @RequestParam(value = \"{0}\""
                        , class_Field.ParaName
                        , class_ToolSpace.GetSetSpaceCount(3));
                    else
                        stringBuilder.AppendFormat("@RequestParam(value = \"{0}\""
                        , class_Field.ParaName);
                    if (class_Field.WhereIsNull)
                        stringBuilder.Append(", required = false");
                    if (class_Field.FieldType.IndexOf("date") < 0 && (class_Field.FieldDefaultValue != null) && (class_Field.FieldDefaultValue.Length > 0) && class_Field.LogType.IndexOf("IN") < 0)
                        stringBuilder.AppendFormat(", defaultValue = \"{0}\"", _GetFieldDefaultValue(class_Field.FieldDefaultValue));
                    stringBuilder.Append(")");
                    if (class_Field.FieldType.IndexOf("date") > -1 && class_Field.LogType.IndexOf("IN") < 0)
                    {
                        if (class_Field.FieldType.Equals("date"))
                            stringBuilder.Append(" @DateTimeFormat(pattern = \"yyyy-MM-dd\")");
                        if (class_Field.FieldType.Equals("datetime"))
                            stringBuilder.Append(" @DateTimeFormat(pattern = \"yyyy-MM-dd HH:mm:ss\")");
                    }
                    if (class_Field.LogType.IndexOf("IN") > -1)
                        stringBuilder.AppendFormat(" List<{0}>"
                        , Class_Tool.GetSimplificationJavaType(class_InterFaceDataBase.GetJavaType(class_Field.FieldType)));
                    else
                        stringBuilder.AppendFormat(" {0}"
                        , Class_Tool.GetSimplificationJavaType(class_InterFaceDataBase.GetJavaType(class_Field.FieldType)));
                    stringBuilder.AppendFormat(" {0}", class_Field.ParaName);
                }
            }
            stringBuilder.Append(") {\r\n");

            #region 去空格
            foreach (Class_Field class_Field in class_Sub.class_Fields)
            {
                if (class_Field.LogType.IndexOf("IN") < 0 && class_Field.WhereSelect && (class_Field.FieldType.Equals("varchar") || class_Field.FieldType.Equals("char")) && class_Field.WhereTrim)
                    stringBuilder.AppendFormat("{0}{1} = {1} == null ? {1} : {1}.trim();\r\n"
                        , class_ToolSpace.GetSetSpaceCount(2), class_Field.ParaName);
            }
            stringBuilder.Append("\r\n");
            #endregion

            #region 构建输入参数对象
            if (ParamCounter > 0)
                stringBuilder.AppendFormat("{0}{1} {2} = new {1}();\r\n"
                , class_ToolSpace.GetSetSpaceCount(2)
                , class_Sub.ParamClassName
                , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName));
            foreach (Class_Field class_Field in class_Sub.class_Fields)
            {
                if (class_Field.WhereSelect)
                {
                    stringBuilder.AppendFormat("{0}{1}.set{2}({3});\r\n"
                    , class_ToolSpace.GetSetSpaceCount(2)
                    , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName)
                    , Class_Tool.GetFirstCodeUpper(class_Field.ParaName)
                    , class_Field.ParaName);
                }
            }
            #endregion

            #region 返回
            if (ParamCounter > 0)
                stringBuilder.AppendFormat("{0}{1} {2} = {3}.{4}({5});\r\n"
                , class_ToolSpace.GetSetSpaceCount(2)
                , "int"
                , "updateCount"
                , Class_Tool.GetFirstCodeLow(class_Sub.ServiceInterFaceName)
                , class_Sub.MethodId
                , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName));
            else
                stringBuilder.AppendFormat("{0}{1} {2} = {3}.{4}();\r\n"
                , class_ToolSpace.GetSetSpaceCount(2)
                , "int"
                , "updateCount"
                , Class_Tool.GetFirstCodeLow(class_Sub.ServiceInterFaceName)
                , class_Sub.MethodId);

            if (class_DeleteAllModel.ReturnStructure)
            {
                stringBuilder.AppendFormat("{0}if (updateCount > 0)\r\n"
                    , class_ToolSpace.GetSetSpaceCount(2));
                stringBuilder.AppendFormat("{0}return ResultStruct.success("
                        , class_ToolSpace.GetSetSpaceCount(3));
                switch (class_Sub.ServiceInterFaceReturnCount)
                {
                    case 0:
                        stringBuilder.Append("updateCount");
                        break;
                    case 1:
                        stringBuilder.AppendFormat("{0}.getId()"
                            , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName));
                        break;
                    case 2:
                        stringBuilder.Append(Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName));
                        break;
                    default:
                        break;
                }
                stringBuilder.AppendFormat(");\r\n{0}else\r\n"
                , class_ToolSpace.GetSetSpaceCount(2));
                stringBuilder.AppendFormat("{0}return ResultStruct.error(\"删除失败\", ResultVO.class, "
                        , class_ToolSpace.GetSetSpaceCount(3));
                switch (class_Sub.ServiceInterFaceReturnCount)
                {
                    case 0:
                        stringBuilder.Append("int.class");
                        break;
                    case 1:
                    case 2:
                        stringBuilder.Append("null");
                        break;
                    default:
                        break;
                }

                stringBuilder.Append(");\r\n");
            }
            else
            {
                if (class_Sub.ServiceInterFaceReturnCount > 0)
                    stringBuilder.AppendFormat("{0}if (updateCount > 0)\r\n"
                        , class_ToolSpace.GetSetSpaceCount(2));
                switch (class_Sub.ServiceInterFaceReturnCount)
                {
                    case 0:
                        stringBuilder.AppendFormat("\r\n{0}return updateCount;\r\n"
                        , class_ToolSpace.GetSetSpaceCount(2));
                        break;
                    case 1:
                        stringBuilder.AppendFormat("{0}return {1}.getId();\r\n"
                            , class_ToolSpace.GetSetSpaceCount(3)
                            , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName));

                        stringBuilder.AppendFormat("{0}else\r\n"
                            , class_ToolSpace.GetSetSpaceCount(2));
                        stringBuilder.AppendFormat("{0}return {1};\r\n"
                                , class_ToolSpace.GetSetSpaceCount(3)
                                , _GetTypeContent(KeyType));
                        break;
                    case 2:
                        stringBuilder.AppendFormat("{0}return {1};\r\n"
                        , class_ToolSpace.GetSetSpaceCount(3)
                        , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName));
                        stringBuilder.AppendFormat("{0}else\r\n"
                            , class_ToolSpace.GetSetSpaceCount(2));
                        stringBuilder.AppendFormat("{0}return null;\r\n"
                                , class_ToolSpace.GetSetSpaceCount(3));
                        break;
                    default:
                        break;
                }
            }
            #endregion

            stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(1) + "}\r\n");
            if (!class_Sub.CreateMainCode)
                stringBuilder.Append("}\r\n");

            return stringBuilder.ToString();
        }
        #endregion

        public void AddAllOutFieldName()
        {
            throw new NotImplementedException();
        }

        public string GetControl(int Index)
        {
            return _GetControl(Index);
        }
        public string _GetTypeContent(string FieldType)
        {
            string ResultContent;
            switch (FieldType)
            {
                case "int":
                case "float":
                case "double":
                case "byte":
                case "short":
                case "long":
                    ResultContent = "-1000";
                    break;
                case "bool":
                    ResultContent = "false";
                    break;
                default:
                    ResultContent = "null";
                    break;
            }
            return ResultContent;
        }
        public string GetDAO(int Index)
        {
            return _GetDAO(Index);
        }
        private string _GetDAO(int Index)
        {
            if (class_DeleteAllModel.class_SubList == null || class_DeleteAllModel.class_SubList.Count < Index)
                return null;
            Class_Sub class_Sub = class_DeleteAllModel.class_SubList[Index];
            if (class_Sub == null)
                return null;
            Class_Tool class_ToolSpace = new Class_Tool();
            StringBuilder stringBuilder = new StringBuilder();
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
            int ParamCounter = 0;
            foreach (Class_Field field in class_Sub.class_Fields)
                ParamCounter += field.WhereSelect ? 1 : 0;
            if (ParamCounter > 0)
                stringBuilder.AppendFormat("{0} * @param {3} {1}.model.InPutParam.{2}\r\n"
                    , class_ToolSpace.GetSetSpaceCount(1)
                    , class_DeleteAllModel.AllPackerName
                    , class_DeleteAllModel.class_SubList[Index].ParamClassName
                    , Class_Tool.GetFirstCodeLow(class_DeleteAllModel.class_SubList[Index].ParamClassName));

            stringBuilder.AppendFormat("{0} * @return {1}\r\n", class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.ServiceInterFaceReturnRemark);
            stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));
            if (ParamCounter > 0)
                stringBuilder.AppendFormat("{0}int {1}({2} {3});\r\n"
                , class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.MethodId
                , class_DeleteAllModel.class_SubList[Index].ParamClassName
                , Class_Tool.GetFirstCodeLow(class_DeleteAllModel.class_SubList[Index].ParamClassName));
            else
                stringBuilder.AppendFormat("{0}int {1}();\r\n"
                , class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.MethodId);


            if (!class_Sub.CreateMainCode)
                stringBuilder.Append("}\r\n");

            return stringBuilder.ToString();
        }

        public string GetDTO(int Index)
        {
            return null;
        }

        public string GetFrontPage()
        {
            return "没做";
        }

        public string GetInPutParam(int Index)
        {
            return _GetInPutParam(Index);
        }
        private string _GetInPutParam(int PageIndex)
        {
            if (class_DeleteAllModel.class_SubList == null || class_DeleteAllModel.class_SubList.Count < PageIndex)
                return null;

            Class_Tool class_ToolSpace = new Class_Tool();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("/**\r\n");
            stringBuilder.AppendFormat(_GetAuthor());
            stringBuilder.AppendFormat(" * @create {0}\r\n", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            stringBuilder.Append(" * @function\r\n * @editLog\r\n");
            stringBuilder.Append(" */\r\n");
            stringBuilder.AppendFormat("public class {0}"
                , class_DeleteAllModel.class_SubList[PageIndex].ParamClassName);
            stringBuilder.Append(" {\r\n");

            //加入字段
            int FieldCount = 0;
            foreach (Class_Field class_Field in class_DeleteAllModel.class_SubList[PageIndex].class_Fields)
            {
                string ReturnType = class_Field.FieldType;
                ReturnType = Class_Tool.GetClosedJavaType(class_InterFaceDataBase.GetJavaType(ReturnType));
                stringBuilder.AppendFormat("{0}/**\r\n", class_ToolSpace.GetSetSpaceCount(1));
                stringBuilder.AppendFormat("{0} * {1}\r\n", class_ToolSpace.GetSetSpaceCount(1), class_Field.FieldRemark);
                stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));
                stringBuilder.AppendFormat("{0}private", class_ToolSpace.GetSetSpaceCount(1));
                if (class_DeleteAllModel.class_Create.EnglishSign && Class_Tool.IsEnglishField(class_Field.ParaName))
                    stringBuilder.Append(" transient");
                if (class_Field.LogType.IndexOf("IN") > -1)
                    stringBuilder.AppendFormat(" List<{0}> {1};\r\n"
                    , ReturnType
                    , class_Field.ParaName);
                else
                    stringBuilder.AppendFormat(" {0} {1};\r\n"
                    , ReturnType
                    , class_Field.ParaName);
                FieldCount++;
            }
            if (FieldCount > 0)
            {
                foreach (Class_Field class_Field in class_DeleteAllModel.class_SubList[PageIndex].class_Fields)
                {
                    string ReturnType = class_Field.FieldType;
                    ReturnType = Class_Tool.GetClosedJavaType(class_InterFaceDataBase.GetJavaType(ReturnType));

                    #region Get
                    if (class_Field.LogType.IndexOf("IN") > -1)
                        stringBuilder.AppendFormat("\r\n{0}public List<{2}> get{1}(){{\r\n"
                        , class_ToolSpace.GetSetSpaceCount(1)
                        , Class_Tool.GetFirstCodeUpper(class_Field.ParaName)
                        , ReturnType);
                    else
                        stringBuilder.AppendFormat("\r\n{0}public {2} get{1}(){{\r\n"
                        , class_ToolSpace.GetSetSpaceCount(1)
                        , Class_Tool.GetFirstCodeUpper(class_Field.ParaName)
                        , ReturnType);
                    stringBuilder.AppendFormat("{0}return {1};\r\n"
                        , class_ToolSpace.GetSetSpaceCount(2)
                        , Class_Tool.GetFirstCodeLow(class_Field.ParaName));
                    stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(1));
                    stringBuilder.Append("}\r\n");
                    #endregion

                    #region Set
                    if (class_Field.LogType.IndexOf("IN") > -1)
                        stringBuilder.AppendFormat("{0}public void set{1}(List<{3}> {2}){{\r\n"
                        , class_ToolSpace.GetSetSpaceCount(1)
                        , Class_Tool.GetFirstCodeUpper(class_Field.ParaName)
                        , Class_Tool.GetFirstCodeLow(class_Field.ParaName)
                        , ReturnType);
                    else
                        stringBuilder.AppendFormat("{0}public void set{1}({3} {2}){{\r\n"
                        , class_ToolSpace.GetSetSpaceCount(1)
                        , Class_Tool.GetFirstCodeUpper(class_Field.ParaName)
                        , Class_Tool.GetFirstCodeLow(class_Field.ParaName)
                        , ReturnType);
                    stringBuilder.AppendFormat("{0}this.{1} = {1};\r\n"
                        , class_ToolSpace.GetSetSpaceCount(2)
                        , Class_Tool.GetFirstCodeLow(class_Field.ParaName));
                    stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(1));
                    stringBuilder.Append("}\r\n");
                    #endregion
                }
            }
            stringBuilder.Append("}\r\n");

            return stringBuilder.ToString();
        }

        private string _GetAuthor()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat(" * @author ：{0}", class_DeleteAllModel.class_Create.CreateMan);
            if (class_DeleteAllModel.class_Create.CreateDo != null && class_DeleteAllModel.class_Create.CreateDo.Length > 0)
            {
                stringBuilder.AppendFormat("，后端工程师：{0}", class_DeleteAllModel.class_Create.CreateDo);
            }
            if (class_DeleteAllModel.class_Create.CreateFrontDo != null && class_DeleteAllModel.class_Create.CreateFrontDo.Length > 0)
            {
                stringBuilder.AppendFormat("，前端工程师：{0}", class_DeleteAllModel.class_Create.CreateFrontDo);
            }
            stringBuilder.AppendFormat("\r\n");
            return stringBuilder.ToString();
        }
        public string GetMap(int Index)
        {
            return null;
        }

        public string GetModel(int Index)
        {
            return null;
        }

        public string GetServiceImpl(int Index)
        {
            return _GetServiceImpl(Index);
        }
        private string _GetServiceImpl(int Index)
        {
            Class_Sub class_Sub = class_DeleteAllModel.class_SubList[Index];
            if (class_Sub == null)
                return null;
            Class_Tool class_ToolSpace = new Class_Tool();
            StringBuilder stringBuilder = new StringBuilder();
            if (!class_Sub.CreateMainCode)
            {
                stringBuilder.Append("/**\r\n");
                stringBuilder.AppendFormat(_GetAuthor());
                stringBuilder.AppendFormat(" * @create {0}\r\n", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                stringBuilder.Append(" * @function\r\n * @editLog\r\n");
                stringBuilder.Append(" */\r\n");
                stringBuilder.Append("@SuppressWarnings(\"SpringJavaInjectionPointsAutowiringInspection\")\r\n@Service\r\n");
                stringBuilder.AppendFormat("public class {1} implements {0} {{\r\n"
                    , class_Sub.ServiceInterFaceName
                    , class_Sub.ServiceClassName);
                stringBuilder.AppendFormat("{0}@Autowired\r\n", class_ToolSpace.GetSetSpaceCount(1));
                stringBuilder.AppendFormat("{0}{1} {2};\r\n", class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.DaoClassName
                , Class_Tool.GetFirstCodeLow(class_Sub.DaoClassName));
            }
            stringBuilder.AppendFormat("\r\n{0}/**\r\n", class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0} * {1}\r\n{0} *\r\n", class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.MethodContent);
            int ParamCounter = 0;
            foreach (Class_Field field in class_Sub.class_Fields)
                ParamCounter += field.WhereSelect ? 1 : 0;
            if (ParamCounter > 0)
                stringBuilder.AppendFormat("{0} * @param {3} {1}.model.InPutParam.{2}\r\n"
                , class_ToolSpace.GetSetSpaceCount(1)
                , class_DeleteAllModel.AllPackerName
                , class_DeleteAllModel.class_SubList[Index].ParamClassName
                , Class_Tool.GetFirstCodeLow(class_DeleteAllModel.class_SubList[Index].ParamClassName));
            stringBuilder.AppendFormat("{0} * @return {1}\r\n", class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.ServiceInterFaceReturnRemark);
            stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));

            if (ParamCounter > 0)
                stringBuilder.AppendFormat("{0}@Override\r\n{0}public int {1} ({2} {3}) {{\r\n"
                , class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.MethodId
                , class_Sub.ParamClassName
                , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName));
            else
                stringBuilder.AppendFormat("{0}@Override\r\n{0}public int {1} () {{\r\n"
                , class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.MethodId);

            if (ParamCounter > 0)
                stringBuilder.AppendFormat("{0}return {1}.{2}({3});\r\n"
            , class_ToolSpace.GetSetSpaceCount(2)
            , Class_Tool.GetFirstCodeLow(class_Sub.DaoClassName)
            , class_Sub.MethodId
            , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName));
            else
                stringBuilder.AppendFormat("{0}return {1}.{2}();\r\n"
            , class_ToolSpace.GetSetSpaceCount(2)
            , Class_Tool.GetFirstCodeLow(class_Sub.DaoClassName)
            , class_Sub.MethodId);

            stringBuilder.AppendFormat("{0}}}\r\n", class_ToolSpace.GetSetSpaceCount(1));

            if (!class_Sub.CreateMainCode)
                stringBuilder.Append("}\r\n");

            return stringBuilder.ToString();
        }

        public string GetServiceInterFace(int Index)
        {
            return _GetServiceInterFace(Index);
        }
        private string _GetServiceInterFace(int Index)
        {
            Class_Sub class_Sub = class_DeleteAllModel.class_SubList[Index];
            if (class_Sub == null)
                return null;
            Class_Tool class_ToolSpace = new Class_Tool();
            StringBuilder stringBuilder = new StringBuilder();
            if (!class_Sub.CreateMainCode)
            {
                stringBuilder.Append("/**\r\n");
                stringBuilder.AppendFormat(_GetAuthor());
                stringBuilder.AppendFormat(" * @create {0}\r\n", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                stringBuilder.Append(" * @function\r\n * @editLog\r\n");
                stringBuilder.Append(" */\r\n");
                stringBuilder.Append(string.Format("public interface {0} {{\r\n"
                    , class_Sub.ServiceInterFaceName));
            }

            stringBuilder.AppendFormat("{0}/**\r\n", class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0} * {1}\r\n{0} *\r\n", class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.MethodContent);
            int ParamCounter = 0;
            foreach (Class_Field field in class_Sub.class_Fields)
                ParamCounter += field.WhereSelect ? 1 : 0;
            if (ParamCounter > 0)
                stringBuilder.AppendFormat("{0} * @param {3} {1}.model.InPutParam.{2}\r\n"
                , class_ToolSpace.GetSetSpaceCount(1)
                , class_DeleteAllModel.AllPackerName
                , class_DeleteAllModel.class_SubList[Index].ParamClassName
                , Class_Tool.GetFirstCodeLow(class_DeleteAllModel.class_SubList[Index].ParamClassName));
            stringBuilder.AppendFormat("{0} * @return {1}\r\n", class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.ServiceInterFaceReturnRemark);
            stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));

            if (ParamCounter > 0)
                stringBuilder.AppendFormat("{0}int {1}({2} {3});\r\n"
                , class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.MethodId
                , class_DeleteAllModel.class_SubList[Index].ParamClassName
                , Class_Tool.GetFirstCodeLow(class_DeleteAllModel.class_SubList[Index].ParamClassName));
            else
                stringBuilder.AppendFormat("{0}int {1}();\r\n"
                , class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.MethodId);

            if (!class_Sub.CreateMainCode)
                stringBuilder.Append("}\r\n");
            return stringBuilder.ToString();
        }

        public string GetSql(int Index)
        {
            return _GetSql(Index);
        }
        private string _GetSql(int Index)
        {
            if (class_DeleteAllModel.class_SubList.Count < Index)
                return null;
            Class_Sub class_Sub = class_DeleteAllModel.class_SubList[Index];
            if (class_Sub == null)
                return null;
            Class_Tool class_ToolSpace = new Class_Tool();
            StringBuilder stringBuilder = new StringBuilder();
            int ParamCounter = 0;
            foreach (Class_Field field in class_Sub.class_Fields)
                ParamCounter += field.WhereSelect ? 1 : 0;

            if (!class_Sub.CreateMainCode)
            {
                stringBuilder.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>\r\n");
                stringBuilder.Append("<!DOCTYPE mapper PUBLIC \"-//mybatis.org//DTD Mapper 3.0//EN\" \"http://mybatis.org/dtd/mybatis-3-mapper.dtd\" >\r\n");
                stringBuilder.AppendFormat("<mapper namespace=\"{0}.dao.{1}\">\r\n"
                    , class_DeleteAllModel.AllPackerName
                    , class_Sub.DaoClassName);
            }
            #region UpdateId
            stringBuilder.AppendFormat("{1}<!-- 注释：{0} -->\r\n", class_Sub.MethodContent, class_ToolSpace.GetSetSpaceCount(1));
            if (ParamCounter > 0)
                stringBuilder.AppendFormat("{0}<delete id=\"{1}\" parameterType=\"{2}.model.InPutParam.{3}\">\r\n"
                , class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.MethodId
                , class_DeleteAllModel.AllPackerName
                , class_DeleteAllModel.class_SubList[Index].ParamClassName);
            else
                stringBuilder.AppendFormat("{0}<delete id=\"{1}\">\r\n"
                    , class_ToolSpace.GetSetSpaceCount(1)
                    , class_Sub.MethodId);
            #endregion

            #region DELETE
            stringBuilder.AppendFormat("{0}DELETE FROM {1}\r\n"
                , class_ToolSpace.GetSetSpaceCount(2)
                , class_Sub.TableName);
            #endregion

            stringBuilder.Append(_GetMainWhereLable(Index));
            stringBuilder.AppendFormat("{0}</delete>\r\n", class_ToolSpace.GetSetSpaceCount(1));

            if (!class_Sub.CreateMainCode)
                stringBuilder.Append("</mapper>\r\n");

            if (stringBuilder.Length > 0)
                return stringBuilder.ToString();
            else
                return null;
        }
        private string _GetMainWhereLable(int Index)
        {
            if (class_DeleteAllModel == null)
                return null;
            Class_Sub item = class_DeleteAllModel.class_SubList[Index];
            Class_Tool class_ToolSpace = new Class_Tool();
            StringBuilder stringBuilderWhere = new StringBuilder();
            foreach (Class_Field class_Field in item.class_Fields)
            {
                string FieldName = class_Field.FieldName;
                string InParaFieldName = class_Field.ParaName;

                #region Where
                string NowWhere = null;
                bool IsAddIf = class_Field.LogType.IndexOf("NULL") > -1 ? false : true;
                IsAddIf = IsAddIf && class_Field.WhereValue.Equals("参数") ? true : false;

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
                    NowWhere += string.Format("{0}</if>\r\n", class_ToolSpace.GetSetSpaceCount(3));
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
                            , Class_Tool.GetJdbcType(class_InterFaceDataBase.GetJavaType(class_Field.FieldType))) + "}";
                        if ((LikeType < -99) && (class_Field.LogType.IndexOf("NULL") == -1))
                            NowWhere = NowWhere + XmlFieldString;
                        else
                            NowWhere = NowWhere + class_InterFaceDataBase.GetLikeString(XmlFieldString, LikeType);
                    }
                    else
                    {
                        if (class_InterFaceDataBase.IsAddPoint(class_Field.FieldType, class_Field.WhereValue))
                            NowWhere = NowWhere + string.Format("'{0}'\r\n", class_Field.WhereValue);
                        else
                            NowWhere = NowWhere + string.Format("{0}\r\n", class_Field.WhereValue);
                    }
                }
                if ((class_Field.LogType.IndexOf("<") > -1) || (class_Field.LogType.IndexOf("&") > -1))
                    NowWhere = string.Format("{0}<![CDATA[{1}]]>\r\n", class_ToolSpace.GetSetSpaceCount(4), NowWhere.Trim());
                else
                    NowWhere += "\r\n";
                if (IsAddIf)
                    stringBuilderWhere.AppendFormat("{1}<if test=\"{0} != null\">\r\n"
                        , InParaFieldName, class_ToolSpace.GetSetSpaceCount(3));
                stringBuilderWhere.Append(NowWhere);
                if (IsAddIf)
                    stringBuilderWhere.AppendFormat("{0}</if>\r\n", class_ToolSpace.GetSetSpaceCount(3));
                #endregion
            }
            if (stringBuilderWhere.Length > 0)
            {
                stringBuilderWhere.Insert(0, string.Format("{0}<where>\r\n", class_ToolSpace.GetSetSpaceCount(2)));
                stringBuilderWhere.AppendFormat("{0}</where>\r\n", class_ToolSpace.GetSetSpaceCount(2));
            }
            if (stringBuilderWhere.Length > 0)
                return stringBuilderWhere.ToString();
            else
                return null;
        }

        public string GetTestSql(int Index)
        {
            return null;
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
        #region Feign
        public string GetFeignControl(int PageIndex)
        {
            return _GetFeignControl(PageIndex);
        }
        private string _GetFeignControl(int PageIndex)
        {
            string KeyType = null;
            if (class_DeleteAllModel.class_SubList == null)
                return null;
            if (class_DeleteAllModel.class_SubList[PageIndex] == null)
                return null;
            int RepetitionCounter = 0;
            Class_Sub class_Sub = class_DeleteAllModel.class_SubList[PageIndex];
            Class_Tool class_ToolSpace = new Class_Tool();
            StringBuilder stringBuilder = new StringBuilder();
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

                if (class_DeleteAllModel.class_Create.SwaggerSign)
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

                stringBuilder.Append("\r\n");
            }
            stringBuilder.AppendFormat("{0}/**\r\n", class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0} * {1}，方法ID：{2}\r\n{0} *\r\n"
                , class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.MethodContent
                , class_DeleteAllModel.class_Create.MethodId);
            foreach (Class_Field class_Field in class_Sub.class_Fields)
            {
                RepetitionCounter += class_Field.WhereSelect ? 1 : 0;
                if (class_Field.WhereSelect)
                    stringBuilder.AppendFormat("{0} * @param {1} {2}\r\n"
                        , class_ToolSpace.GetSetSpaceCount(1)
                        , class_Field.ParaName
                        , class_Field.FieldRemark);
                if (class_Field.FieldIsKey && KeyType == null)
                {
                    KeyType = Class_Tool.GetSimplificationJavaType(class_InterFaceDataBase.GetJavaType(class_Field.FieldType));
                }
            }

            stringBuilder.AppendFormat("{0} * @return {1}\r\n", class_ToolSpace.GetSetSpaceCount(1)
            , class_Sub.ServiceInterFaceReturnRemark);
            stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));
            #endregion

            #region Swagger
            if (class_DeleteAllModel.class_Create.SwaggerSign)
            {
                stringBuilder.AppendFormat("{0}@ApiOperation(value = \"{1}\", notes = \"{2}\")\r\n"
                , class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.MethodContent
                , class_Sub.ServiceInterFaceReturnRemark);
                if (class_Sub.class_Fields != null && class_Sub.class_Fields.Count > 0)
                {
                    stringBuilder.AppendFormat("{0}@ApiImplicitParams({{\r\n", class_ToolSpace.GetSetSpaceCount(1));
                    int index = 0;
                    foreach (Class_Field class_Field in class_Sub.class_Fields)
                    {
                        if (class_Field.WhereSelect)
                        {
                            stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(3));
                            if (index > 0)
                                stringBuilder.Append(", ");
                            stringBuilder.AppendFormat("@ApiImplicitParam(name = \"{0}\", value = \"{1}\", dataType = \"{2}\""
                                , Class_Tool.GetFirstCodeLow(class_Field.FieldName)
                                , class_Field.FieldRemark
                                , Class_Tool.GetSimplificationJavaType(class_InterFaceDataBase.GetJavaType(class_Field.FieldType)));
                            if (!class_Field.WhereIsNull)
                                stringBuilder.Append(", required = true");
                            if (class_Field.LogType.IndexOf("IN") > -1)
                                stringBuilder.Append(", paramType = \"query\"");
                            stringBuilder.Append(")\r\n");
                            index++;
                        }
                    }
                    stringBuilder.AppendFormat("{0}}})\r\n", class_ToolSpace.GetSetSpaceCount(1));
                }
            }
            #endregion

            stringBuilder.AppendFormat("{0}@{1}Mapping(\"/{2}\")\r\n"
            , class_ToolSpace.GetSetSpaceCount(1)
            , class_DeleteAllModel.class_Create.HttpRequestType
            , class_Sub.MethodId);
            stringBuilder.AppendFormat("{0}public ", class_ToolSpace.GetSetSpaceCount(1));

            #region
            if (class_DeleteAllModel.ReturnStructure)
            {
                stringBuilder.Append("ResultVO");
            }
            else
            {
                switch (class_Sub.ServiceInterFaceReturnCount)
                {
                    case 0:
                        stringBuilder.Append("int");
                        break;
                    case 1:
                        stringBuilder.Append(KeyType);
                        break;
                    case 2:
                        stringBuilder.AppendFormat("{0}", class_Sub.ParamClassName);
                        break;
                    default:
                        break;
                }
            }
            #endregion

            stringBuilder.AppendFormat(" {0}", class_Sub.MethodId);
            stringBuilder.Append("(");
            int Index = 0;
            foreach (Class_Field class_Field in class_Sub.class_Fields)
            {
                if (class_Field.WhereSelect)
                {
                    if (Index++ > 0)
                        stringBuilder.AppendFormat("\r\n{1}, @RequestParam(value = \"{0}\""
                        , class_Field.ParaName
                        , class_ToolSpace.GetSetSpaceCount(3));
                    else
                        stringBuilder.AppendFormat("@RequestParam(value = \"{0}\""
                        , class_Field.ParaName);
                    if (class_Field.WhereIsNull)
                        stringBuilder.Append(", required = false");
                    if (class_Field.FieldType.IndexOf("date") < 0 && (class_Field.FieldDefaultValue != null) && (class_Field.FieldDefaultValue.Length > 0) && class_Field.LogType.IndexOf("IN") < 0)
                        stringBuilder.AppendFormat(", defaultValue = \"{0}\"", _GetFieldDefaultValue(class_Field.FieldDefaultValue));
                    stringBuilder.Append(")");
                    if (class_Field.FieldType.IndexOf("date") > -1 && class_Field.LogType.IndexOf("IN") < 0)
                    {
                        if (class_Field.FieldType.Equals("date"))
                            stringBuilder.Append(" @DateTimeFormat(pattern = \"yyyy-MM-dd\")");
                        if (class_Field.FieldType.Equals("datetime"))
                            stringBuilder.Append(" @DateTimeFormat(pattern = \"yyyy-MM-dd HH:mm:ss\")");
                    }
                    if (class_Field.LogType.IndexOf("IN") > -1)
                        stringBuilder.AppendFormat(" List<{0}>"
                        , Class_Tool.GetSimplificationJavaType(class_InterFaceDataBase.GetJavaType(class_Field.FieldType)));
                    else
                        stringBuilder.AppendFormat(" {0}"
                        , Class_Tool.GetSimplificationJavaType(class_InterFaceDataBase.GetJavaType(class_Field.FieldType)));
                    stringBuilder.AppendFormat(" {0}", class_Field.ParaName);
                }
            }
            stringBuilder.Append(") {\r\n");

            #region 返回
            stringBuilder.AppendFormat("{0} return {1}.{2}("
                , class_ToolSpace.GetSetSpaceCount(2)
                , Class_Tool.GetFirstCodeLow(class_Sub.FeignInterFaceClassName)
                , class_Sub.MethodId);
            Index = 0;
            foreach (Class_Field class_Field in class_Sub.class_Fields)
            {
                if (class_Field.WhereSelect)
                {
                    if (Index++ > 0)
                        stringBuilder.Append(", ");
                    stringBuilder.Append(class_Field.ParaName);
                }
            }
            stringBuilder.Append(");\r\n");
            #endregion

            stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(1) + "}\r\n");
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
            if (class_DeleteAllModel.class_SubList == null)
                return null;
            if (class_DeleteAllModel.class_SubList[PageIndex] == null)
                return null;
            Class_Sub class_Sub = class_DeleteAllModel.class_SubList[PageIndex];
            List<Class_WhereField> class_WhereFields = new List<Class_WhereField>();
            Class_Tool class_ToolSpace = new Class_Tool();
            StringBuilder stringBuilder = new StringBuilder();
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
                    , class_DeleteAllModel.class_Create.MicroServiceName
                    , class_Sub.ControlRequestMapping
                    , class_Sub.FeignInterFaceHystricClassName);

                stringBuilder.AppendFormat("public interface {0} {{\r\n", class_Sub.FeignInterFaceClassName);
            }
            #endregion

            stringBuilder.AppendFormat("{0}@{1}Mapping(\"/{2}\")\r\n"
                , class_ToolSpace.GetSetSpaceCount(1)
                , class_DeleteAllModel.class_Create.HttpRequestType
                , class_Sub.MethodId);
            stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(1));
            #region 返回值
            string KeyType = null;
            foreach (Class_Field class_Field in class_Sub.class_Fields)
            {
                if (class_Field.FieldIsKey && KeyType == null)
                {
                    KeyType = Class_Tool.GetSimplificationJavaType(class_InterFaceDataBase.GetJavaType(class_Field.FieldType));
                }
            }

            if (class_DeleteAllModel.ReturnStructure)
            {
                stringBuilder.Append("ResultVO");
            }
            else
            {
                switch (class_Sub.ServiceInterFaceReturnCount)
                {
                    case 0:
                        stringBuilder.Append("int");
                        break;
                    case 1:
                        stringBuilder.Append(KeyType);
                        break;
                    case 2:
                        stringBuilder.AppendFormat("{0}", class_Sub.ParamClassName);
                        break;
                    default:
                        break;
                }
            }
            #endregion
            stringBuilder.AppendFormat(" {0}", class_Sub.MethodId);
            stringBuilder.Append("(");
            int Index = 0;
            foreach (Class_Field class_Field in class_Sub.class_Fields)
            {
                if (class_Field.WhereSelect)
                {
                    if (Index++ > 0)
                        stringBuilder.AppendFormat("\r\n{1}, @RequestParam(value = \"{0}\""
                        , class_Field.ParaName
                        , class_ToolSpace.GetSetSpaceCount(3));
                    else
                        stringBuilder.AppendFormat("@RequestParam(value = \"{0}\""
                        , class_Field.ParaName);
                    if (class_Field.WhereIsNull)
                        stringBuilder.Append(", required = false");
                    if (class_Field.FieldType.IndexOf("date") < 0 && (class_Field.FieldDefaultValue != null) && (class_Field.FieldDefaultValue.Length > 0) && class_Field.LogType.IndexOf("IN") < 0)
                        stringBuilder.AppendFormat(", defaultValue = \"{0}\"", _GetFieldDefaultValue(class_Field.FieldDefaultValue));
                    stringBuilder.Append(")");
                    if (class_Field.FieldType.IndexOf("date") > -1 && class_Field.LogType.IndexOf("IN") < 0)
                    {
                        if (class_Field.FieldType.Equals("date"))
                            stringBuilder.Append(" @DateTimeFormat(pattern = \"yyyy-MM-dd\")");
                        if (class_Field.FieldType.Equals("datetime"))
                            stringBuilder.Append(" @DateTimeFormat(pattern = \"yyyy-MM-dd HH:mm:ss\")");
                    }
                    if (class_Field.LogType.IndexOf("IN") > -1)
                        stringBuilder.AppendFormat(" List<{0}>"
                        , Class_Tool.GetSimplificationJavaType(class_InterFaceDataBase.GetJavaType(class_Field.FieldType)));
                    else
                        stringBuilder.AppendFormat(" {0}"
                        , Class_Tool.GetSimplificationJavaType(class_InterFaceDataBase.GetJavaType(class_Field.FieldType)));
                    stringBuilder.AppendFormat(" {0}", class_Field.ParaName);
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
            if (class_DeleteAllModel.class_SubList == null)
                return null;
            if (class_DeleteAllModel.class_SubList[PageIndex] == null)
                return null;
            Class_Sub class_Sub = class_DeleteAllModel.class_SubList[PageIndex];
            List<Class_WhereField> class_WhereFields = new List<Class_WhereField>();
            Class_Tool class_ToolSpace = new Class_Tool();
            StringBuilder stringBuilder = new StringBuilder();
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

            string KeyType = null;
            foreach (Class_Field class_Field in class_Sub.class_Fields)
            {
                if (class_Field.FieldIsKey && KeyType == null)
                {
                    KeyType = Class_Tool.GetSimplificationJavaType(class_InterFaceDataBase.GetJavaType(class_Field.FieldType));
                }
            }
            stringBuilder.AppendFormat("{0}@Override\r\n"
                , class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0}public ", class_ToolSpace.GetSetSpaceCount(1));

            if (class_DeleteAllModel.ReturnStructure)
            {
                stringBuilder.Append("ResultVO");
            }
            else
            {
                switch (class_Sub.ServiceInterFaceReturnCount)
                {
                    case 0:
                        stringBuilder.Append("int");
                        break;
                    case 1:
                        stringBuilder.Append(KeyType);
                        break;
                    case 2:
                        stringBuilder.AppendFormat("{0}", class_Sub.ParamClassName);
                        break;
                    default:
                        break;
                }
            }

            stringBuilder.AppendFormat(" {0}", class_Sub.MethodId);
            stringBuilder.Append("(");
            int Index = 0;
            foreach (Class_Field class_Field in class_Sub.class_Fields)
            {
                if (class_Field.WhereSelect)
                {
                    if (Index++ > 0)
                        stringBuilder.AppendFormat("\r\n{1}, @RequestParam(value = \"{0}\""
                        , class_Field.ParaName
                        , class_ToolSpace.GetSetSpaceCount(3));
                    else
                        stringBuilder.AppendFormat("@RequestParam(value = \"{0}\""
                        , class_Field.ParaName);
                    if (class_Field.WhereIsNull)
                        stringBuilder.Append(", required = false");
                    if (class_Field.FieldType.IndexOf("date") < 0 && (class_Field.FieldDefaultValue != null) && (class_Field.FieldDefaultValue.Length > 0) && class_Field.LogType.IndexOf("IN") < 0)
                        stringBuilder.AppendFormat(", defaultValue = \"{0}\"", _GetFieldDefaultValue(class_Field.FieldDefaultValue));
                    stringBuilder.Append(")");
                    if (class_Field.FieldType.IndexOf("date") > -1 && class_Field.LogType.IndexOf("IN") < 0)
                    {
                        if (class_Field.FieldType.Equals("date"))
                            stringBuilder.Append(" @DateTimeFormat(pattern = \"yyyy-MM-dd\")");
                        if (class_Field.FieldType.Equals("datetime"))
                            stringBuilder.Append(" @DateTimeFormat(pattern = \"yyyy-MM-dd HH:mm:ss\")");
                    }
                    if (class_Field.LogType.IndexOf("IN") > -1)
                        stringBuilder.AppendFormat(" List<{0}>"
                        , Class_Tool.GetSimplificationJavaType(class_InterFaceDataBase.GetJavaType(class_Field.FieldType)));
                    else
                        stringBuilder.AppendFormat(" {0}"
                        , Class_Tool.GetSimplificationJavaType(class_InterFaceDataBase.GetJavaType(class_Field.FieldType)));
                    stringBuilder.AppendFormat(" {0}", class_Field.ParaName);
                }
            }
            stringBuilder.Append(") {\r\n");
            stringBuilder.AppendFormat("{0}return "
                , class_ToolSpace.GetSetSpaceCount(2));
            if (class_DeleteAllModel.ReturnStructure)
            {
                stringBuilder.Append("ResultStruct.error(hystricMessage, ResultVO.class, ");
                switch (class_Sub.ServiceInterFaceReturnCount)
                {
                    case 0:
                        stringBuilder.Append("int.class");
                        break;
                    case 1:
                    case 2:
                        stringBuilder.Append("null");
                        break;
                    default:
                        stringBuilder.Append("null");
                        break;
                }
                stringBuilder.Append(");\r\n");
            }
            else
            {
                switch (class_Sub.ServiceInterFaceReturnCount)
                {
                    case 0:
                        stringBuilder.Append("0");
                        break;
                    case 1:
                        stringBuilder.Append("null");
                        break;
                    case 2:
                        stringBuilder.Append("null");
                        break;
                    default:
                        stringBuilder.Append("null");
                        break;
                }
                stringBuilder.Append(";\r\n");
            }

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

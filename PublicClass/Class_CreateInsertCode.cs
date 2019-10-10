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
            return _GetControl(Index);
        }
        private string _GetControl(int PageIndex)
        {
            string KeyType = null;
            if (class_InsertAllModel.class_SubList == null)
                return null;
            if (class_InsertAllModel.class_SubList[PageIndex] == null)
                return null;
            int RepetitionCounter = 0;
            Class_Sub class_Sub = class_InsertAllModel.class_SubList[PageIndex];
            Class_Tool class_ToolSpace = new Class_Tool();
            StringBuilder stringBuilder = new StringBuilder();
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

                if (class_InsertAllModel.class_Create.SwaggerSign)
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
                , class_InsertAllModel.class_Create.MethodId);
            foreach (Class_Field class_Field in class_Sub.class_Fields)
            {
                RepetitionCounter += class_Field.WhereSelect ? 1 : 0;
                if (!class_Field.FieldIsKey && (class_Field.InsertSelect || class_Field.WhereSelect))
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
            if (class_InsertAllModel.class_Create.SwaggerSign)
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
                        if (!class_Field.FieldIsKey && (class_Field.InsertSelect || class_Field.WhereSelect))
                        {
                            stringBuilder.Append(class_ToolSpace.GetSetSpaceCount(3));
                            if (index > 0)
                                stringBuilder.Append(",");
                            stringBuilder.AppendFormat(" @ApiImplicitParam(name = \"{0}\", value = \"{1}\", dataType = \"{2}\""
                            , Class_Tool.GetFirstCodeLow(class_Field.ParaName)
                            , class_Field.FieldRemark
                            , Class_Tool.GetSimplificationJavaType(class_InterFaceDataBase.GetJavaType(class_Field.FieldType)));
                            if (!class_Field.FieldIsNull)
                                stringBuilder.Append(", required = true");
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
            , class_InsertAllModel.class_Create.HttpRequestType
            , class_Sub.MethodId);
            stringBuilder.AppendFormat("{0}public ", class_ToolSpace.GetSetSpaceCount(1));

            #region
            if (class_InsertAllModel.ReturnStructure)
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
                if (!class_Field.FieldIsKey && (class_Field.InsertSelect || class_Field.WhereSelect))
                {
                    if (Index++ > 0)
                        stringBuilder.AppendFormat("\r\n{1}, @RequestParam(value = \"{0}\""
                        , class_Field.ParaName
                        , class_ToolSpace.GetSetSpaceCount(3));
                    else
                        stringBuilder.AppendFormat("@RequestParam(value = \"{0}\""
                        , class_Field.ParaName);
                    if (class_Field.FieldIsNull)
                        stringBuilder.Append(", required = false");
                    if ((class_Field.FieldDefaultValue != null) && (class_Field.FieldDefaultValue.Length > 0))
                        stringBuilder.AppendFormat(", defaultValue = \"{0}\"", _GetFieldDefaultValue(class_Field.FieldDefaultValue));
                    stringBuilder.Append(")");

                    stringBuilder.AppendFormat(" {0} {1}"
                        , Class_Tool.GetSimplificationJavaType(class_InterFaceDataBase.GetJavaType(class_Field.FieldType))
                        , class_Field.ParaName);
                }
            }

            stringBuilder.Append(") {\r\n");

            #region 去空格
            foreach (Class_Field class_Field in class_Sub.class_Fields)
            {
                if ((class_Field.InsertSelect || class_Field.WhereSelect) && !class_Field.FieldIsKey && (class_Field.FieldType.Equals("varchar") || class_Field.FieldType.Equals("char")) && class_Field.TrimSign)
                {
                    stringBuilder.AppendFormat("{0}{1} = {1} == null ? {1} : {1}.trim();\r\n"
                        , class_ToolSpace.GetSetSpaceCount(2), class_Field.ParaName);
                }
            }
            stringBuilder.Append("\r\n");
            #endregion

            #region 构建输入参数对象
            stringBuilder.AppendFormat("{0}{1} {2} = new {1}();\r\n"
            , class_ToolSpace.GetSetSpaceCount(2)
            , class_Sub.ParamClassName
            , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName));
            foreach (Class_Field class_Field in class_Sub.class_Fields)
            {
                if (class_Field.FieldIsKey)
                {
                    if (!class_Field.FieldIsAutoAdd && (class_Field.InsertSelect || class_Field.WhereSelect))
                    {
                        stringBuilder.AppendFormat("{0}{1} mainKey;//这里引用架包中的生成主键方法\r\n"
                            , class_ToolSpace.GetSetSpaceCount(2)
                            , KeyType);
                        stringBuilder.AppendFormat("{0}{1}.set{2}({3});\r\n"
                        , class_ToolSpace.GetSetSpaceCount(2)
                        , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName)
                        , Class_Tool.GetFirstCodeUpper(class_Field.ParaName)
                        , "mainKey");
                    }
                }
                else
                {
                    if (class_Field.InsertSelect || class_Field.WhereSelect)
                    {
                        stringBuilder.AppendFormat("{0}{1}.set{2}({3});\r\n"
                        , class_ToolSpace.GetSetSpaceCount(2)
                        , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName)
                        , Class_Tool.GetFirstCodeUpper(class_Field.ParaName)
                        , class_Field.ParaName);
                    }
                }
            }
            #endregion

            #region 重复
            if (RepetitionCounter > 0)
            {
                stringBuilder.AppendFormat("{0}{1} {2} = {3}.{4}BeforeCheck({5});\r\n"
                    , class_ToolSpace.GetSetSpaceCount(2)
                    , "int"
                    , "repetitionCount"
                    , Class_Tool.GetFirstCodeLow(class_Sub.ServiceInterFaceName)
                    , class_Sub.MethodId
                    , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName));
                stringBuilder.AppendFormat("{0}if (repetitionCount > 0)\r\n"
                    , class_ToolSpace.GetSetSpaceCount(2));
                if (class_InsertAllModel.ReturnStructure)
                {
                    switch (class_Sub.ServiceInterFaceReturnCount)
                    {
                        case 0:
                            stringBuilder.AppendFormat("{0}return ResultStruct.error(\"增加失败，有\" + repetitionCount + \"条数据已重复！\", ResultVO.class, int.class);\r\n"
                                , class_ToolSpace.GetSetSpaceCount(3));
                            break;
                        case 1:
                        case 2:
                            stringBuilder.AppendFormat("{0}return ResultStruct.error(\"增加失败，有\" + repetitionCount + \"条数据已重复！\", ResultVO.class, null);\r\n"
                                , class_ToolSpace.GetSetSpaceCount(3));
                            break;
                        default:
                            stringBuilder.AppendFormat("{0}return ResultStruct.error(\"增加失败，有\" + repetitionCount + \"条数据已重复！\", ResultVO.class, null);\r\n"
                                , class_ToolSpace.GetSetSpaceCount(3));
                            break;
                    }

                }
                else
                {
                    switch (class_Sub.ServiceInterFaceReturnCount)
                    {
                        case 0:
                            stringBuilder.AppendFormat("{0}return {1};\r\n"
                                , class_ToolSpace.GetSetSpaceCount(3)
                                , _GetTypeContent("int"));
                            break;
                        case 1:
                            stringBuilder.AppendFormat("{0}return {1};\r\n"
                                , class_ToolSpace.GetSetSpaceCount(3)
                                , _GetTypeContent(KeyType));
                            break;
                        case 2:
                            stringBuilder.AppendFormat("{0}return null;\r\n"
                                , class_ToolSpace.GetSetSpaceCount(3));
                            break;
                        default:
                            stringBuilder.AppendFormat("{0}return {1};\r\n"
                                , class_ToolSpace.GetSetSpaceCount(3)
                                , _GetTypeContent(KeyType));
                            break;
                    }
                }
            }
            #endregion

            #region 返回
            stringBuilder.AppendFormat("{0}{1} {2} = {3}.{4}({5});\r\n"
                , class_ToolSpace.GetSetSpaceCount(2)
                , "int"
                , "resultCount"
                , Class_Tool.GetFirstCodeLow(class_Sub.ServiceInterFaceName)
                , class_Sub.MethodId
                , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName));
            if (class_InsertAllModel.ReturnStructure)
            {
                stringBuilder.AppendFormat("{0}if (resultCount > 0)\r\n"
                    , class_ToolSpace.GetSetSpaceCount(2));
                stringBuilder.AppendFormat("{0}return ResultStruct.success("
                        , class_ToolSpace.GetSetSpaceCount(3));
                switch (class_Sub.ServiceInterFaceReturnCount)
                {
                    case 0:
                        stringBuilder.Append("resultCount");
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
                switch (class_Sub.ServiceInterFaceReturnCount)
                {
                    case 0:
                        stringBuilder.AppendFormat("{0}return ResultStruct.error(\"增加失败\", ResultVO.class, int.class);\r\n"
                                , class_ToolSpace.GetSetSpaceCount(3));
                        break;
                    case 1:
                    case 2:
                        stringBuilder.AppendFormat("{0}return ResultStruct.error(\"增加失败\", ResultVO.class, null);\r\n"
                                , class_ToolSpace.GetSetSpaceCount(3));
                        break;
                    default:
                        stringBuilder.AppendFormat("{0}return ResultStruct.error(\"增加失败\", ResultVO.class, null);\r\n"
                                , class_ToolSpace.GetSetSpaceCount(3));
                        break;
                }
            }
            else
            {
                if (class_Sub.ServiceInterFaceReturnCount > 0)
                    stringBuilder.AppendFormat("{0}if (resultCount > 0)\r\n"
                        , class_ToolSpace.GetSetSpaceCount(2));
                switch (class_Sub.ServiceInterFaceReturnCount)
                {
                    case 0:
                        stringBuilder.AppendFormat("\r\n{0}return resultCount;\r\n"
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
            if (class_InsertAllModel.class_SubList == null || class_InsertAllModel.class_SubList.Count < Index)
                return null;
            Class_Sub class_Sub = class_InsertAllModel.class_SubList[Index];
            if (class_Sub == null)
                return null;
            Class_Tool class_ToolSpace = new Class_Tool();
            StringBuilder stringBuilder = new StringBuilder();
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
            stringBuilder.AppendFormat("{0} * @param {3} {1}.model.InPutParam.{2}\r\n"
                , class_ToolSpace.GetSetSpaceCount(1)
                , class_InsertAllModel.AllPackerName
                , class_InsertAllModel.class_SubList[Index].ParamClassName
                , Class_Tool.GetFirstCodeLow(class_InsertAllModel.class_SubList[Index].ParamClassName));

            stringBuilder.AppendFormat("{0} * @return {1}\r\n", class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.ServiceInterFaceReturnRemark);
            stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));

            stringBuilder.AppendFormat("{0}int {1}({2} {3});\r\n"
                , class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.MethodId
                , class_InsertAllModel.class_SubList[Index].ParamClassName
                , Class_Tool.GetFirstCodeLow(class_InsertAllModel.class_SubList[Index].ParamClassName));

            #region 重复
            int RepetitionCounter = 0;
            foreach (Class_Field class_Field in class_Sub.class_Fields)
                RepetitionCounter += class_Field.WhereSelect ? 1 : 0;
            if (RepetitionCounter > 0)
            {
                stringBuilder.AppendFormat("\r\n{0}/**\r\n", class_ToolSpace.GetSetSpaceCount(1));
                stringBuilder.AppendFormat("{0} * {1}\r\n{0} *\r\n", class_ToolSpace.GetSetSpaceCount(1)
                    , class_Sub.MethodContent);
                stringBuilder.AppendFormat("{0} * @param {3} {1}.model.InPutParam.{2}\r\n"
                    , class_ToolSpace.GetSetSpaceCount(1)
                    , class_InsertAllModel.AllPackerName
                    , class_InsertAllModel.class_SubList[Index].ParamClassName
                    , Class_Tool.GetFirstCodeLow(class_InsertAllModel.class_SubList[Index].ParamClassName));
                stringBuilder.AppendFormat("{0} * @return {1}\r\n", class_ToolSpace.GetSetSpaceCount(1)
                    , class_Sub.ServiceInterFaceReturnRemark);
                stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));

                stringBuilder.AppendFormat("{0}int {1}BeforeCheck({2} {3});\r\n"
                    , class_ToolSpace.GetSetSpaceCount(1)
                    , class_Sub.MethodId
                    , class_InsertAllModel.class_SubList[Index].ParamClassName
                    , Class_Tool.GetFirstCodeLow(class_InsertAllModel.class_SubList[Index].ParamClassName));
            }
            #endregion

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
            if (class_InsertAllModel.class_SubList == null || class_InsertAllModel.class_SubList.Count < PageIndex)
                return null;

            Class_Tool class_ToolSpace = new Class_Tool();
            StringBuilder stringBuilder = new StringBuilder();
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
            stringBuilder.Append("/**\r\n");
            stringBuilder.AppendFormat(_GetAuthor());
            stringBuilder.AppendFormat(" * @create {0}\r\n", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            stringBuilder.Append(" * @function\r\n * @editLog\r\n");
            stringBuilder.Append(" */\r\n");
            stringBuilder.AppendFormat("public class {0}"
                , class_InsertAllModel.class_SubList[PageIndex].ParamClassName);
            stringBuilder.Append(" {\r\n");

            //加入字段
            int FieldCount = 0;
            foreach (Class_Field class_Field in class_InsertAllModel.class_SubList[PageIndex].class_Fields)
            {
                string ReturnType = class_Field.FieldType;
                ReturnType = Class_Tool.GetClosedJavaType(class_InterFaceDataBase.GetJavaType(ReturnType));
                stringBuilder.AppendFormat("{0}/**\r\n", class_ToolSpace.GetSetSpaceCount(1));
                stringBuilder.AppendFormat("{0} * {1}\r\n", class_ToolSpace.GetSetSpaceCount(1), class_Field.FieldRemark);
                stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));
                stringBuilder.AppendFormat("{0}private", class_ToolSpace.GetSetSpaceCount(1));
                if (class_InsertAllModel.class_Create.EnglishSign && Class_Tool.IsEnglishField(class_Field.ParaName))
                    stringBuilder.Append(" transient");
                stringBuilder.AppendFormat(" {0} {1};\r\n"
                    , ReturnType
                    , class_Field.ParaName);
                FieldCount++;
            }
            if (FieldCount > 0)
            {
                foreach (Class_Field class_Field in class_InsertAllModel.class_SubList[PageIndex].class_Fields)
                {
                    string ReturnType = class_Field.FieldType;
                    ReturnType = Class_Tool.GetClosedJavaType(class_InterFaceDataBase.GetJavaType(ReturnType));

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
            stringBuilder.Append("}\r\n");

            return stringBuilder.ToString();
        }

        private string _GetAuthor()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat(" * @author ：{0}", class_InsertAllModel.class_Create.CreateMan);
            if (class_InsertAllModel.class_Create.CreateDo != null && class_InsertAllModel.class_Create.CreateDo.Length > 0)
            {
                stringBuilder.AppendFormat("，后端工程师：{0}", class_InsertAllModel.class_Create.CreateDo);
            }
            if (class_InsertAllModel.class_Create.CreateFrontDo != null && class_InsertAllModel.class_Create.CreateFrontDo.Length > 0)
            {
                stringBuilder.AppendFormat("，前端工程师：{0}", class_InsertAllModel.class_Create.CreateFrontDo);
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
            Class_Sub class_Sub = class_InsertAllModel.class_SubList[Index];
            if (class_Sub == null)
                return null;
            Class_Tool class_ToolSpace = new Class_Tool();
            StringBuilder stringBuilder = new StringBuilder();
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
            stringBuilder.AppendFormat("{0} * @param {3} {1}.model.InPutParam.{2}\r\n"
                , class_ToolSpace.GetSetSpaceCount(1)
                , class_InsertAllModel.AllPackerName
                , class_InsertAllModel.class_SubList[Index].ParamClassName
                , Class_Tool.GetFirstCodeLow(class_InsertAllModel.class_SubList[Index].ParamClassName));
            stringBuilder.AppendFormat("{0} * @return {1}\r\n", class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.ServiceInterFaceReturnRemark);
            stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));

            stringBuilder.AppendFormat("{0}@Override\r\n{0}public int {1} ({2} {3}) {{\r\n"
                , class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.MethodId
                , class_Sub.ParamClassName
                , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName));

            stringBuilder.AppendFormat("{0}return {1}.{2}({3});\r\n"
            , class_ToolSpace.GetSetSpaceCount(2)
            , Class_Tool.GetFirstCodeLow(class_Sub.DaoClassName)
            , class_Sub.MethodId
            , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName));

            stringBuilder.AppendFormat("{0}}}\r\n", class_ToolSpace.GetSetSpaceCount(1));

            #region 重复
            int RepetitionCounter = 0;
            foreach (Class_Field class_Field in class_Sub.class_Fields)
                RepetitionCounter += class_Field.WhereSelect ? 1 : 0;
            if (RepetitionCounter > 0)
            {
                stringBuilder.AppendFormat("\r\n{0}/**\r\n", class_ToolSpace.GetSetSpaceCount(1));
                stringBuilder.AppendFormat("{0} * {1}\r\n{0} *\r\n", class_ToolSpace.GetSetSpaceCount(1)
                    , class_Sub.MethodContent);
                stringBuilder.AppendFormat("{0} * @param {3} {1}.model.InPutParam.{2}\r\n"
                    , class_ToolSpace.GetSetSpaceCount(1)
                    , class_InsertAllModel.AllPackerName
                    , class_InsertAllModel.class_SubList[Index].ParamClassName
                    , Class_Tool.GetFirstCodeLow(class_InsertAllModel.class_SubList[Index].ParamClassName));
                stringBuilder.AppendFormat("{0} * @return {1}\r\n", class_ToolSpace.GetSetSpaceCount(1)
                    , class_Sub.ServiceInterFaceReturnRemark);
                stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));
                stringBuilder.AppendFormat("{0}@Override\r\n{0}public int {1}BeforeCheck({2} {3}) {{\r\n"
                    , class_ToolSpace.GetSetSpaceCount(1)
                    , class_Sub.MethodId
                    , class_Sub.ParamClassName
                    , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName));
                stringBuilder.AppendFormat("{0}return {1}.{2}BeforeCheck({3});\r\n"
                    , class_ToolSpace.GetSetSpaceCount(2)
                    , Class_Tool.GetFirstCodeLow(class_Sub.DaoClassName)
                    , class_Sub.MethodId
                    , Class_Tool.GetFirstCodeLow(class_Sub.ParamClassName));
                stringBuilder.AppendFormat("{0}}}\r\n", class_ToolSpace.GetSetSpaceCount(1));
            }
            #endregion

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
            Class_Sub class_Sub = class_InsertAllModel.class_SubList[Index];
            if (class_Sub == null)
                return null;
            Class_Tool class_ToolSpace = new Class_Tool();
            StringBuilder stringBuilder = new StringBuilder();
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
            stringBuilder.AppendFormat("{0} * @param {3} {1}.model.InPutParam.{2}\r\n"
                , class_ToolSpace.GetSetSpaceCount(1)
                , class_InsertAllModel.AllPackerName
                , class_InsertAllModel.class_SubList[Index].ParamClassName
                , Class_Tool.GetFirstCodeLow(class_InsertAllModel.class_SubList[Index].ParamClassName));
            stringBuilder.AppendFormat("{0} * @return {1}\r\n", class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.ServiceInterFaceReturnRemark);
            stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));

            stringBuilder.AppendFormat("{0}int {1}({2} {3});\r\n"
                , class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.MethodId
                , class_InsertAllModel.class_SubList[Index].ParamClassName
                , Class_Tool.GetFirstCodeLow(class_InsertAllModel.class_SubList[Index].ParamClassName));

            #region 重复
            int RepetitionCounter = 0;
            foreach (Class_Field class_Field in class_Sub.class_Fields)
                RepetitionCounter += class_Field.WhereSelect ? 1 : 0;
            if (RepetitionCounter > 0)
            {
                stringBuilder.AppendFormat("\r\n{0}/**\r\n", class_ToolSpace.GetSetSpaceCount(1));
                stringBuilder.AppendFormat("{0} * {1}\r\n{0} *\r\n", class_ToolSpace.GetSetSpaceCount(1)
                    , class_Sub.MethodContent);
                stringBuilder.AppendFormat("{0} * @param {3} {1}.model.InPutParam.{2}\r\n"
                    , class_ToolSpace.GetSetSpaceCount(1)
                    , class_InsertAllModel.AllPackerName
                    , class_InsertAllModel.class_SubList[Index].ParamClassName
                    , Class_Tool.GetFirstCodeLow(class_InsertAllModel.class_SubList[Index].ParamClassName));
                stringBuilder.AppendFormat("{0} * @return {1}\r\n", class_ToolSpace.GetSetSpaceCount(1)
                    , class_Sub.ServiceInterFaceReturnRemark);
                stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));

                stringBuilder.AppendFormat("{0}int {1}BeforeCheck({2} {3});\r\n"
                    , class_ToolSpace.GetSetSpaceCount(1)
                    , class_Sub.MethodId
                    , class_InsertAllModel.class_SubList[Index].ParamClassName
                    , Class_Tool.GetFirstCodeLow(class_InsertAllModel.class_SubList[Index].ParamClassName));
            }
            #endregion

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
            if (class_InsertAllModel.class_SubList.Count < Index)
                return null;
            Class_Sub class_Sub = class_InsertAllModel.class_SubList[Index];
            if (class_Sub == null)
                return null;
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
            if (!class_Sub.CreateMainCode)
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
                , class_InsertAllModel.class_SubList[Index].ParamClassName);
            #endregion

            #region Insert Values
            stringBuilder.AppendFormat("{0}INSERT INTO {1} (\r\n"
                , class_ToolSpace.GetSetSpaceCount(2)
                , class_Sub.TableName);
            int RepetitionCounter = 0;
            int Counter = 0;
            foreach (Class_Field class_Field in class_Sub.class_Fields)
            {
                RepetitionCounter += class_Field.WhereSelect ? 1 : 0;
                if (class_Field.InsertSelect)
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
            }
            #endregion

            stringBuilder.Append(stringBuilderField.ToString().Substring(1));
            stringBuilder.AppendFormat("{0})\r\n", class_ToolSpace.GetSetSpaceCount(2));
            stringBuilder.AppendFormat("{0}VALUES (\r\n", class_ToolSpace.GetSetSpaceCount(2));
            stringBuilder.Append(stringBuilderValue.ToString().Substring(1));
            stringBuilder.AppendFormat("{0})\r\n", class_ToolSpace.GetSetSpaceCount(2));
            stringBuilder.AppendFormat("{0}</insert>\r\n", class_ToolSpace.GetSetSpaceCount(1));

            #region 生成重复查询
            if (RepetitionCounter > 0)
            {
                stringBuilder.Append("\r\n");
                stringBuilder.AppendFormat("{1}<!-- 注释：重复检查功能,{0} -->\r\n", class_Sub.MethodContent, class_ToolSpace.GetSetSpaceCount(1));
                stringBuilder.AppendFormat("{0}<select id=\"{1}BeforeCheck\" resultType=\"java.lang.Integer\" parameterType=\"{2}.model.InPutParam.{3}\">\r\n"
                    , class_ToolSpace.GetSetSpaceCount(1)
                    , class_Sub.MethodId
                    , class_InsertAllModel.AllPackerName
                    , class_InsertAllModel.class_SubList[Index].ParamClassName);
                stringBuilder.AppendFormat("{0}SELECT COUNT(*) AS COUNTER\r\n{0}FROM {1}\r\n"
                    , class_ToolSpace.GetSetSpaceCount(2), class_Sub.TableName);
                stringBuilder.AppendFormat("{0}<where>\r\n"
                    , class_ToolSpace.GetSetSpaceCount(2));
                StringBuilder WhereBuilder = new StringBuilder();
                foreach (Class_Field class_Field in class_Sub.class_Fields)
                {
                    if (class_Field.WhereSelect)
                    {
                        string MyWhereSql = null;
                        if (class_Field.WhereIsNull)
                            WhereBuilder.AppendFormat("{0}<if test=\"{1} != null\">\r\n"
                                , class_ToolSpace.GetSetSpaceCount(3)
                                , class_Field.ParaName);
                        MyWhereSql = string.Format("{0} {2} {1} "
                            , class_ToolSpace.GetSetSpaceCount((class_Field.WhereType == "AND" || class_Field.WhereType == "OR") ? 4 : 5)
                            , class_Field.FieldName, class_Field.WhereType);
                        if (class_Field.LogType.IndexOf("NULL") > -1)
                        {
                            MyWhereSql += string.Format("{0}\r\n", class_Field.LogType);
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
                            MyWhereSql += string.Format("{0} ", class_Field.LogType.IndexOf("Like") > -1 ? "like" : class_Field.LogType);
                            if (class_Field.WhereValue == "参数")
                            {
                                string XmlFieldString = string.Format("#{{{0},jdbcType={1}}}"
                                    , class_Field.ParaName
                                    , Class_Tool.GetJdbcType(class_InterFaceDataBase.GetJavaType(class_Field.FieldType)));
                                if ((LikeType < -99) && (class_Field.LogType.IndexOf("NULL") == -1))
                                    MyWhereSql += XmlFieldString;
                                else
                                    MyWhereSql += string.Format(class_InterFaceDataBase.GetLikeString(XmlFieldString, LikeType));
                            }
                            else
                            {
                                if (class_InterFaceDataBase.IsAddPoint(class_Field.FieldType))
                                    MyWhereSql += string.Format("'{0}'", class_Field.WhereValue);
                                else
                                    MyWhereSql += string.Format("{0}", class_Field.WhereValue);
                            }
                            if ((class_Field.LogType.IndexOf("<") > -1) || (class_Field.LogType.IndexOf("&") > -1))
                                WhereBuilder.AppendFormat("{0}<![CDATA[{1}]]>", class_ToolSpace.GetSetSpaceCount(4), MyWhereSql.TrimStart());
                            else
                                WhereBuilder.Append(MyWhereSql);
                            if (class_Field.WhereIsNull)
                                WhereBuilder.AppendFormat("\r\n{0}</if>"
                                    , class_ToolSpace.GetSetSpaceCount(3));
                            WhereBuilder.Append("\r\n");
                        }
                    }
                }
                stringBuilder.Append(WhereBuilder.ToString());
                stringBuilder.AppendFormat("{0}</where>\r\n"
                    , class_ToolSpace.GetSetSpaceCount(2));
                stringBuilder.AppendFormat("{0}</select>\r\n"
                    , class_ToolSpace.GetSetSpaceCount(1));
            }
            #endregion

            if (!class_Sub.CreateMainCode)
                stringBuilder.Append("</mapper>\r\n");

            if (stringBuilder.Length > 0)
                return stringBuilder.ToString();
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

        public string GetFeignControl(int Index)
        {
            throw new NotImplementedException();
        }

        public string GetFeignInterFace(int Index)
        {
            throw new NotImplementedException();
        }

        public string GetFeignInterFaceHystric(int Index)
        {
            throw new NotImplementedException();
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
    }
}

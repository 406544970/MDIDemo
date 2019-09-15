using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MDIDemo.PublicClass.Class_UpdateAllModel;

namespace MDIDemo.PublicClass
{
    public class Class_CreateUpdateCode : IClass_InterFaceCreateCode, IClass_CreateFrontPage
    {
        public Class_CreateUpdateCode()
        {
            InitClass(null);
        }
        public Class_CreateUpdateCode(string xmlFileName)
        {
            InitClass(xmlFileName);
        }
        private void InitClass(string xmlFileName)
        {
            class_UpdateAllModel = new Class_UpdateAllModel();
            if (xmlFileName != null)
            {
                Class_PublicMethod class_PublicMethod = new Class_PublicMethod();
                class_UpdateAllModel = class_PublicMethod.FromXmlToUpdateObject<Class_UpdateAllModel>(xmlFileName);
            }
            class_SQLiteOperator = new Class_SQLiteOperator();
        }
        private Class_SQLiteOperator class_SQLiteOperator;
        private Class_UpdateAllModel class_UpdateAllModel;
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
            if (class_UpdateAllModel.class_SubList == null)
                return null;
            if (class_UpdateAllModel.class_SubList[PageIndex] == null)
                return null;
            int RepetitionCounter = 0;
            Class_Sub class_Sub = class_UpdateAllModel.class_SubList[PageIndex];
            Class_Tool class_ToolSpace = new Class_Tool();
            StringBuilder stringBuilder = new StringBuilder();
            IClass_InterFaceDataBase class_InterFaceDataBase;
            switch (class_UpdateAllModel.class_SelectDataBase.databaseType)
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
            if (!class_Sub.ControlMainCode)
            {
                stringBuilder.Append("/**\r\n");
                stringBuilder.Append(_GetAuthor());
                stringBuilder.AppendFormat(" * @create {0}\r\n", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                stringBuilder.Append(" * @function\r\n * @editLog\r\n");
                stringBuilder.Append(" */\r\n");
                stringBuilder.Append("@RestController\r\n");
                if (class_Sub.ControlRequestMapping != null)
                    stringBuilder.AppendFormat("@RequestMapping(\"/{0}\")\r\n", Class_Tool.GetFirstCodeLow(class_Sub.ControlRequestMapping));

                if (class_UpdateAllModel.class_Create.SwaggerSign)
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
                , class_UpdateAllModel.class_Create.MethodId);
            foreach (Class_Field class_Field in class_Sub.class_Fields)
            {
                RepetitionCounter += class_Field.WhereSelect ? 1 : 0;
                if (!class_Field.FieldIsKey && (class_Field.UpdateSelect || class_Field.WhereSelect))
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
            if (class_UpdateAllModel.class_Create.SwaggerSign)
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
                        if (!class_Field.FieldIsKey && (class_Field.UpdateSelect || class_Field.WhereSelect))
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
            , class_UpdateAllModel.class_Create.HttpRequestType
            , class_Sub.MethodId);
            stringBuilder.AppendFormat("{0}public ", class_ToolSpace.GetSetSpaceCount(1));

            #region
            if (class_UpdateAllModel.ReturnStructure)
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
                if (!class_Field.FieldIsKey && (class_Field.UpdateSelect || class_Field.WhereSelect))
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
                        stringBuilder.AppendFormat(", defaultValue = \"{0}\"", class_Field.FieldDefaultValue);
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
                if ((class_Field.UpdateSelect || class_Field.WhereSelect) && !class_Field.FieldIsKey && (class_Field.FieldType.Equals("varchar") || class_Field.FieldType.Equals("char")) && class_Field.TrimSign)
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
                    if (!class_Field.FieldIsAutoAdd)
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
                    if (class_Field.UpdateSelect || class_Field.WhereSelect)
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
                if (class_UpdateAllModel.ReturnStructure)
                {
                    stringBuilder.AppendFormat("{0}return ResultStruct.error(\"增加失败，有\" + repetitionCount + \"条数据已重复！\", ResultVO.Class);\r\n"
                        , class_ToolSpace.GetSetSpaceCount(3));
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
            if (class_UpdateAllModel.ReturnStructure)
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
                stringBuilder.AppendFormat("{0}return ResultStruct.error(\"增加失败\",ResultVO.class);\r\n"
                        , class_ToolSpace.GetSetSpaceCount(3));
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
            if (!class_Sub.ControlMainCode)
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
            if (class_UpdateAllModel.class_SubList == null || class_UpdateAllModel.class_SubList.Count < Index)
                return null;
            Class_Sub class_Sub = class_UpdateAllModel.class_SubList[Index];
            if (class_Sub == null)
                return null;
            Class_Tool class_ToolSpace = new Class_Tool();
            StringBuilder stringBuilder = new StringBuilder();
            IClass_InterFaceDataBase class_InterFaceDataBase;
            switch (class_UpdateAllModel.class_SelectDataBase.databaseType)
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
            stringBuilder.Append(string.Format("public interface {0}", class_Sub.DaoClassName));
            stringBuilder.Append(" {\r\n");

            stringBuilder.AppendFormat("{0}/**\r\n", class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0} * {1}\r\n{0} *\r\n", class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.MethodContent);
            stringBuilder.AppendFormat("{0} * @param {3} {1}.model.InPutParam.{2}\r\n"
                , class_ToolSpace.GetSetSpaceCount(1)
                , class_UpdateAllModel.AllPackerName
                , class_UpdateAllModel.class_SubList[Index].ParamClassName
                , Class_Tool.GetFirstCodeLow(class_UpdateAllModel.class_SubList[Index].ParamClassName));

            stringBuilder.AppendFormat("{0} * @return {1}\r\n", class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.ServiceInterFaceReturnRemark);
            stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));

            stringBuilder.AppendFormat("{0}int {1}({2} {3});\r\n"
                , class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.MethodId
                , class_UpdateAllModel.class_SubList[Index].ParamClassName
                , Class_Tool.GetFirstCodeLow(class_UpdateAllModel.class_SubList[Index].ParamClassName));

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
                    , class_UpdateAllModel.AllPackerName
                    , class_UpdateAllModel.class_SubList[Index].ParamClassName
                    , Class_Tool.GetFirstCodeLow(class_UpdateAllModel.class_SubList[Index].ParamClassName));
                stringBuilder.AppendFormat("{0} * @return {1}\r\n", class_ToolSpace.GetSetSpaceCount(1)
                    , class_Sub.ServiceInterFaceReturnRemark);
                stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));

                stringBuilder.AppendFormat("{0}int {1}BeforeCheck({2} {3});\r\n"
                    , class_ToolSpace.GetSetSpaceCount(1)
                    , class_Sub.MethodId
                    , class_UpdateAllModel.class_SubList[Index].ParamClassName
                    , Class_Tool.GetFirstCodeLow(class_UpdateAllModel.class_SubList[Index].ParamClassName));
            }
            #endregion


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
            if (class_UpdateAllModel.class_SubList == null || class_UpdateAllModel.class_SubList.Count < PageIndex)
                return null;

            Class_Tool class_ToolSpace = new Class_Tool();
            StringBuilder stringBuilder = new StringBuilder();
            IClass_InterFaceDataBase class_InterFaceDataBase;
            switch (class_UpdateAllModel.class_SelectDataBase.databaseType)
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
                , class_UpdateAllModel.class_SubList[PageIndex].ParamClassName);
            stringBuilder.Append(" {\r\n");

            //加入字段
            int FieldCount = 0;
            foreach (Class_Field class_Field in class_UpdateAllModel.class_SubList[PageIndex].class_Fields)
            {
                string ReturnType = class_Field.FieldType;
                ReturnType = Class_Tool.GetClosedJavaType(class_InterFaceDataBase.GetJavaType(ReturnType));
                stringBuilder.AppendFormat("{0}/**\r\n", class_ToolSpace.GetSetSpaceCount(1));
                stringBuilder.AppendFormat("{0} * {1}\r\n", class_ToolSpace.GetSetSpaceCount(1), class_Field.FieldRemark);
                stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));
                stringBuilder.AppendFormat("{0}private", class_ToolSpace.GetSetSpaceCount(1));
                if (class_UpdateAllModel.class_Create.EnglishSign && Class_Tool.IsEnglishField(class_Field.ParaName))
                    stringBuilder.Append(" transient");
                stringBuilder.AppendFormat(" {0} {1};\r\n"
                    , ReturnType
                    , class_Field.ParaName);
                FieldCount++;
            }
            if (FieldCount > 0)
            {
                foreach (Class_Field class_Field in class_UpdateAllModel.class_SubList[PageIndex].class_Fields)
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
            stringBuilder.AppendFormat(" * @author ：{0}", class_UpdateAllModel.class_Create.CreateMan);
            if (class_UpdateAllModel.class_Create.CreateDo != null && class_UpdateAllModel.class_Create.CreateDo.Length > 0)
            {
                stringBuilder.AppendFormat("，后端工程师：{0}", class_UpdateAllModel.class_Create.CreateDo);
            }
            if (class_UpdateAllModel.class_Create.CreateFrontDo != null && class_UpdateAllModel.class_Create.CreateFrontDo.Length > 0)
            {
                stringBuilder.AppendFormat("，前端工程师：{0}", class_UpdateAllModel.class_Create.CreateFrontDo);
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
            Class_Sub class_Sub = class_UpdateAllModel.class_SubList[Index];
            if (class_Sub == null)
                return null;
            Class_Tool class_ToolSpace = new Class_Tool();
            StringBuilder stringBuilder = new StringBuilder();
            IClass_InterFaceDataBase class_InterFaceDataBase;
            switch (class_UpdateAllModel.class_SelectDataBase.databaseType)
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
            stringBuilder.AppendFormat("public class {1} implements {0} {{\r\n"
                , class_Sub.ServiceInterFaceName
                , class_Sub.ServiceClassName);

            stringBuilder.AppendFormat("{0}@Autowired\r\n", class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0}{1} {2};\r\n", class_ToolSpace.GetSetSpaceCount(1)
            , class_Sub.DaoClassName
            , Class_Tool.GetFirstCodeLow(class_Sub.DaoClassName));
            stringBuilder.AppendFormat("\r\n{0}/**\r\n", class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0} * {1}\r\n{0} *\r\n", class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.MethodContent);
            stringBuilder.AppendFormat("{0} * @param {3} {1}.model.InPutParam.{2}\r\n"
                , class_ToolSpace.GetSetSpaceCount(1)
                , class_UpdateAllModel.AllPackerName
                , class_UpdateAllModel.class_SubList[Index].ParamClassName
                , Class_Tool.GetFirstCodeLow(class_UpdateAllModel.class_SubList[Index].ParamClassName));
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
                    , class_UpdateAllModel.AllPackerName
                    , class_UpdateAllModel.class_SubList[Index].ParamClassName
                    , Class_Tool.GetFirstCodeLow(class_UpdateAllModel.class_SubList[Index].ParamClassName));
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
            stringBuilder.Append("}\r\n");

            return stringBuilder.ToString();
        }

        public string GetServiceInterFace(int Index)
        {
            return _GetServiceInterFace(Index);
        }
        private string _GetServiceInterFace(int Index)
        {
            Class_Sub class_Sub = class_UpdateAllModel.class_SubList[Index];
            if (class_Sub == null)
                return null;
            Class_Tool class_ToolSpace = new Class_Tool();
            StringBuilder stringBuilder = new StringBuilder();
            IClass_InterFaceDataBase class_InterFaceDataBase;
            switch (class_UpdateAllModel.class_SelectDataBase.databaseType)
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
            stringBuilder.Append(string.Format("public interface {0} {{\r\n"
                , class_Sub.ServiceInterFaceName));

            stringBuilder.AppendFormat("{0}/**\r\n", class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0} * {1}\r\n{0} *\r\n", class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.MethodContent);
            stringBuilder.AppendFormat("{0} * @param {3} {1}.model.InPutParam.{2}\r\n"
                , class_ToolSpace.GetSetSpaceCount(1)
                , class_UpdateAllModel.AllPackerName
                , class_UpdateAllModel.class_SubList[Index].ParamClassName
                , Class_Tool.GetFirstCodeLow(class_UpdateAllModel.class_SubList[Index].ParamClassName));
            stringBuilder.AppendFormat("{0} * @return {1}\r\n", class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.ServiceInterFaceReturnRemark);
            stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));

            stringBuilder.AppendFormat("{0}int {1}({2} {3});\r\n"
                , class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.MethodId
                , class_UpdateAllModel.class_SubList[Index].ParamClassName
                , Class_Tool.GetFirstCodeLow(class_UpdateAllModel.class_SubList[Index].ParamClassName));

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
                    , class_UpdateAllModel.AllPackerName
                    , class_UpdateAllModel.class_SubList[Index].ParamClassName
                    , Class_Tool.GetFirstCodeLow(class_UpdateAllModel.class_SubList[Index].ParamClassName));
                stringBuilder.AppendFormat("{0} * @return {1}\r\n", class_ToolSpace.GetSetSpaceCount(1)
                    , class_Sub.ServiceInterFaceReturnRemark);
                stringBuilder.AppendFormat("{0} */\r\n", class_ToolSpace.GetSetSpaceCount(1));

                stringBuilder.AppendFormat("{0}int {1}BeforeCheck({2} {3});\r\n"
                    , class_ToolSpace.GetSetSpaceCount(1)
                    , class_Sub.MethodId
                    , class_UpdateAllModel.class_SubList[Index].ParamClassName
                    , Class_Tool.GetFirstCodeLow(class_UpdateAllModel.class_SubList[Index].ParamClassName));
            }
            #endregion

            stringBuilder.Append("}\r\n");
            return stringBuilder.ToString();
        }

        public string GetSql(int Index)
        {
            return _GetSql(Index);
        }
        private string _GetSql(int Index)
        {
            if (class_UpdateAllModel.class_SubList.Count < Index)
                return null;
            Class_Sub class_Sub = class_UpdateAllModel.class_SubList[Index];
            if (class_Sub == null)
                return null;
            Class_Tool class_ToolSpace = new Class_Tool();
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder stringBuilderSet = new StringBuilder();
            StringBuilder stringBuilderWhere = new StringBuilder();
            IClass_InterFaceDataBase class_InterFaceDataBase;

            switch (class_UpdateAllModel.class_SelectDataBase.databaseType)
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
                    , class_UpdateAllModel.AllPackerName
                    , class_Sub.DaoClassName);
            }
            #region UpdateId
            stringBuilder.AppendFormat("{1}<!-- 注释：{0} -->\r\n", class_Sub.MethodContent, class_ToolSpace.GetSetSpaceCount(1));
            stringBuilder.AppendFormat("{0}<update id=\"{1}\" parameterType=\"{2}.model.InPutParam.{3}\">\r\n"
                , class_ToolSpace.GetSetSpaceCount(1)
                , class_Sub.MethodId
                , class_UpdateAllModel.AllPackerName
                , class_UpdateAllModel.class_SubList[Index].ParamClassName);
            #endregion

            #region Insert Values
            stringBuilder.AppendFormat("{0}UPDATE {1}\r\n"
                , class_ToolSpace.GetSetSpaceCount(2)
                , class_Sub.TableName);
            int RepetitionCounter = 0;
            int Counter = 0;
            foreach (Class_Field class_Field in class_Sub.class_Fields)
            {
                bool IsAddField = false;
                RepetitionCounter += class_Field.WhereSelect ? 1 : 0;
                if (!class_Field.FieldIsKey)
                {
                    IsAddField = true;
                }
                else
                    IsAddField = !class_Field.FieldIsAutoAdd;

                stringBuilderSet.AppendFormat("{0}<if test=\"{1} != null\">\r\n"
                    , class_ToolSpace.GetSetSpaceCount(3)
                    , class_Field.ParaName);
                stringBuilderWhere.AppendFormat("{0}<if test=\"{1}Where != null\">\r\n"
                    , class_ToolSpace.GetSetSpaceCount(3)
                    , class_Field.ParaName);
                if (IsAddField)
                {
                    stringBuilderSet.Append(class_ToolSpace.GetSetSpaceCount(4));
                    stringBuilderWhere.Append(class_ToolSpace.GetSetSpaceCount(4));
                    if (Counter > 0)
                    {
                        stringBuilderSet.Append(",");
                        stringBuilderWhere.Append(",");
                    }
                    stringBuilderSet.AppendFormat("{0} = #{{{1},jdbcType={2}}}\r\n"
                        , class_Field.FieldName
                        , class_Field.ParaName
                        , Class_Tool.GetJdbcType(class_InterFaceDataBase.GetJavaType(class_Field.FieldType)));

                    stringBuilderWhere.AppendFormat("这是有问题{0} = #{{{1}Where,jdbcType={2}}}\r\n"
                        , class_Field.FieldName
                        , class_Field.ParaName
                        , Class_Tool.GetJdbcType(class_InterFaceDataBase.GetJavaType(class_Field.FieldType)));
                    Counter++;
                }
                stringBuilderSet.AppendFormat("{0}</if>\r\n"
                    , class_ToolSpace.GetSetSpaceCount(3));
                stringBuilderWhere.AppendFormat("{0}</if>\r\n"
                    , class_ToolSpace.GetSetSpaceCount(3));
            }
            #endregion

            stringBuilder.AppendFormat("{0}<set>\r\n"
                , class_ToolSpace.GetSetSpaceCount(2));
            stringBuilder.Append(stringBuilderSet.ToString().Substring(1));
            stringBuilder.AppendFormat("{0}</set>\r\n"
                , class_ToolSpace.GetSetSpaceCount(2));
            stringBuilder.AppendFormat("{0}<where>\r\n", class_ToolSpace.GetSetSpaceCount(2));
            stringBuilder.Append(stringBuilderWhere.ToString().Substring(1));
            stringBuilder.AppendFormat("{0}</where>\r\n", class_ToolSpace.GetSetSpaceCount(2));
            stringBuilder.AppendFormat("{0}</update>\r\n", class_ToolSpace.GetSetSpaceCount(1));

            if (!class_Sub.IsAddXmlHead)
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
    }
}

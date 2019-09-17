using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MDIDemo.PublicClass
{
    public class Class_SelectAllModel
    {
        #region 构造、析构函数
        public Class_SelectAllModel()
        {
            class_MyBatisMap = new Class_MyBatisMap();
            class_SelectDataBase = new Class_SelectDataBase();
            class_Create = new Class_Create();
            class_SubList = new List<Class_Sub>();
            class_WindowLastState = new Class_WindowLastState();
            class_FrontPage = new Class_FrontPage();
            LastSelectTableName = null;
            classType = "select";
            FrontType = 0;
            PageSign = true;
            ReturnStructure = false;
            ReadWriteSeparation = true;
            IsMultTable = false;
        }
        ~Class_SelectAllModel()
        {
            if (class_LinkFieldInfos != null)
                class_LinkFieldInfos.Clear();
            if (class_SubList != null)
                class_SubList.Clear();
        }
        #endregion

        #region 属性
        //[NonSerialized]
        public List<Class_OutField> class_OutFields;
        //[NonSerialized]
        public List<Class_LinkFieldInfo> class_LinkFieldInfos;
        public string AllPackerName { get; set; }
        public Class_WindowLastState class_WindowLastState { get; set; }
        public Class_MyBatisMap class_MyBatisMap { get; set; }
        /// <summary>
        /// 生成代码类型
        /// </summary>
        public Class_SelectDataBase class_SelectDataBase { get; set; }
        public Class_Create class_Create { get; set; }
        public List<Class_Sub> class_SubList { get; set; }
        public string LastSelectTableName { get; set; }
        public string classType { get; set; }
        public string FrontPage { get; set; }
        public int FrontType { get; set; }
        public string UsedMethod { get; set; }
        public bool IsMultTable { get; set; }
        /// <summary>
        /// 是否分页
        /// </summary>
        public bool PageSign { get; set; }
        /// <summary>
        /// 是否返回固定结构体
        /// </summary>
        public bool ReturnStructure { get; set; }
        /// <summary>
        /// 结构体类型
        /// 0:ReturnVO,1:ReturePage,2:ReturePageTotal,3:RetureTotal
        /// </summary>
        public int ReturnStructureType { get; set; }
        /// <summary>
        /// 是否读写分离
        /// </summary>
        public bool ReadWriteSeparation { get; set; }

        public Class_FrontPage class_FrontPage;
        #endregion

        #region 子类

        #region 前端页面
        public partial class Class_FrontPage
        {
            public Class_FrontPage()
            {
                SelectType = 0;
            }
            /// <summary>
            /// 动态表格名称
            /// </summary>
            public string GridName { get; set; }
            /// <summary>
            /// 工具栏类型
            /// </summary>
            public string ToolType { get; set; }
            /// <summary>
            /// 列工具栏类型
            /// </summary>
            public string ColumnToolType { get; set; }
            /// <summary>
            /// 动态表格上的选择类型：0:无选、1:单选、2:多选
            /// </summary>
            public int SelectType { get; set; }
        }
        #endregion

        #region 界面最后状态
        /// <summary>
        /// 界面最后状态
        /// </summary>
        public partial class Class_WindowLastState
        {
            public Class_WindowLastState()
            {
                xtraTabControl1 = 0;
                xtraTabControl2 = 0;
                xtraTabControl3 = 0;
                xtraTabControl4 = 0;
                xtraTabControl5 = 0;
                xtraTabControl6 = 0;
                xtraTabControl7 = 0;
                xtraTabControl8 = 0;
                xtraTabControl9 = 0;
                xtraTabControl10 = 0;
                xtraTabControl11 = 0;
                xtraTabControl12 = 0;
                xtraTabControl13 = 0;
                xtraTabControl14 = 0;
                xtraTabControl15 = 0;
            }
            public int xtraTabControl1 { get; set; }
            public int xtraTabControl2 { get; set; }
            public int xtraTabControl3 { get; set; }
            public int xtraTabControl4 { get; set; }
            public int xtraTabControl5 { get; set; }
            public int xtraTabControl8 { get; set; }
            public int xtraTabControl6 { get; set; }
            public int xtraTabControl7 { get; set; }
            public int xtraTabControl9 { get; set; }
            public int xtraTabControl10 { get; set; }
            public int xtraTabControl11 { get; set; }
            public int xtraTabControl12 { get; set; }
            public int xtraTabControl13 { get; set; }
            public int xtraTabControl14 { get; set; }
            public int xtraTabControl15 { get; set; }
        }
        #endregion

        #region mybatisMap文件配置
        public partial class Class_MyBatisMap
        {
            public Class_MyBatisMap()
            {
                MybatisVersion = "3.4.0";
                SetIni();
            }

            #region 属性
            [Browsable(true)]
            [Category("Map文件配置")]
            [DisplayName("mybatis版本号")]
            [Description("当前使用的mybatis版本号:3.4.0")]
            [ReadOnly(true)]
            public string MybatisVersion
            {
                get;
            }
            [Browsable(true)]
            [Category("Map文件配置")]
            [DisplayName("配置参数方式")]
            [Description("SpringCloud：SpringCloud方式；Original：原生方式；")]
            [ReadOnly(false)]
            [TypeConverter(typeof(MybatisXmlCreateType))] //使用自定义的属性下拉框
            [DefaultValue("SpringCloud")]
            public string ParaCreateType
            {
                get; set;
            }
            [Browsable(true)]
            [Category("Map文件配置")]
            [DisplayName("cacheEnabled")]
            [Description("全局地开启或关闭配置文件中的所有映射器已经配置的任何缓存。")]
            [ReadOnly(false)]
            [DefaultValue(true)]
            public bool CacheEnabled
            {
                get; set;
            }
            [Browsable(true)]
            [Category("Map文件配置")]
            [DisplayName("lazyLoadingEnabled")]
            [Description("延迟加载的全局开关。当开启时，所有关联对象都会延迟加载。 特定关联关系中可通过设置fetchType属性来覆盖该项的开关状态。")]
            [ReadOnly(false)]
            [DefaultValue(false)]
            public bool LazyLoadingEnabled
            {
                get; set;
            }
            [Browsable(true)]
            [Category("Map文件配置")]
            [DisplayName("aggressiveLazyLoading")]
            [Description("当开启时，任何方法的调用都会加载该对象的所有属性。否则，每个属性会按需加载（默认值为false，≤3.4.1).")]
            [ReadOnly(false)]
            [DefaultValue(false)]
            public bool AggressiveLazyLoading
            {
                get; set;
            }
            [Browsable(true)]
            [Category("Map文件配置")]
            [DisplayName("multipleResultSetsEnabled")]
            [Description("是否允许单一语句返回多结果集（需要兼容驱动）。")]
            [ReadOnly(false)]
            [DefaultValue(true)]
            public bool MultipleResultSetsEnabled
            {
                get; set;
            }
            [Browsable(true)]
            [Category("Map文件配置")]
            [DisplayName("useColumnLabel")]
            [Description("使用列标签代替列名。不同的驱动在这方面会有不同的表现， 具体可参考相关驱动文档或通过测试这两种不同的模式来观察所用驱动的结果。")]
            [ReadOnly(false)]
            [DefaultValue(true)]
            public bool UseColumnLabel
            {
                get; set;
            }
            [Browsable(true)]
            [Category("Map文件配置")]
            [DisplayName("useGeneratedKeys")]
            [Description("允许JDBC支持自动生成主键，需要驱动兼容。 如果设置为 true 则这个设置强制使用自动生成主键，尽管一些驱动不能兼容但仍可正常工作（比如 Derby）。")]
            [ReadOnly(false)]
            [DefaultValue(false)]
            public bool UseGeneratedKeys
            {
                get; set;
            }
            [Browsable(true)]
            [Category("Map文件配置")]
            [DisplayName("autoMappingBehavior")]
            [Description("指定 MyBatis 应如何自动映射列到字段或属性。 NONE 表示取消自动映射；PARTIAL 只会自动映射没有定义嵌套结果集映射的结果集。 FULL 会自动映射任意复杂的结果集（无论是否嵌套）。")]
            [ReadOnly(false)]
            [DefaultValue("PARTIA")]
            [TypeConverter(typeof(AutoMappingBehaviorType))] //使用自定义的属性下拉框
            public string AutoMappingBehavior
            {
                get; set;
            }
            [Browsable(true)]
            [Category("Map文件配置")]
            [DisplayName("autoMappingUnknownColumnBehavior")]
            [Description(@"指定发现自动映射目标未知列（或者未知属性类型）的行为。NONE: 不做任何反应、WARNING: 输出提醒日志、FAILING: 映射失败，抛出异常")]
            [ReadOnly(false)]
            [DefaultValue("NONE")]
            [TypeConverter(typeof(AutoMappingUnknownColumnBehaviorType))] //使用自定义的属性下拉框
            public string AutoMappingUnknownColumnBehavior
            {
                get; set;
            }

            [Browsable(true)]
            [Category("Map文件配置")]
            [DisplayName("defaultExecutorType")]
            [Description("配置默认的执行器。SIMPLE 就是普通的执行器；REUSE 执行器会重用预处理语句（prepared statements）； BATCH 执行器将重用语句并执行批量更新。")]
            [ReadOnly(false)]
            [DefaultValue("SIMPLE")]
            [TypeConverter(typeof(DefaultExecutorType))] //使用自定义的属性下拉框
            public string DefaultExecutorType
            {
                get; set;
            }
            [Browsable(true)]
            [Category("Map文件配置")]
            [DisplayName("defaultStatementTimeout")]
            [Description("设置超时时间，它决定驱动等待数据库响应的秒数。")]
            [ReadOnly(false)]
            [Editor(typeof(PropertyGridRichText), typeof(System.Drawing.Design.UITypeEditor))]
            public string DefaultStatementTimeout
            {
                get; set;
            }
            [Browsable(true)]
            [Category("Map文件配置")]
            [DisplayName("defaultFetchSize")]
            [Description("为驱动的结果集获取数量（fetchSize）设置一个提示值。此参数只可以在查询设置中被覆盖。")]
            [ReadOnly(false)]
            [Editor(typeof(PropertyGridRichText), typeof(System.Drawing.Design.UITypeEditor))]
            public string DefaultFetchSize
            {
                get; set;
            }
            [Browsable(true)]
            [Category("Map文件配置")]
            [DisplayName("safeRowBoundsEnabled")]
            [Description("允许在嵌套语句中使用分页（RowBounds）。如果允许使用则设置为false。")]
            [ReadOnly(false)]
            [DefaultValue(false)]
            public bool SafeRowBoundsEnabled
            {
                get; set;
            }
            [Browsable(true)]
            [Category("Map文件配置")]
            [DisplayName("safeResultHandlerEnabled")]
            [Description("允许在嵌套语句中使用分页（ResultHandler）。如果允许使用则设置为false。")]
            [ReadOnly(false)]
            [DefaultValue(true)]
            public bool SafeResultHandlerEnabled
            {
                get; set;
            }
            [Browsable(true)]
            [Category("Map文件配置")]
            [DisplayName("mapUnderscoreToCamelCase")]
            [Description("是否开启自动驼峰命名规则（camel case）映射，即从经典数据库列名 A_COLUMN 到经典 Java 属性名 aColumn 的类似映射。")]
            [ReadOnly(false)]
            [DefaultValue(false)]
            public bool MapUnderscoreToCamelCase
            {
                get; set;
            }
            [Browsable(true)]
            [Category("Map文件配置")]
            [DisplayName("localCacheScope")]
            [Description("MyBatis 利用本地缓存机制（Local Cache）防止循环引用（circular references）和加速重复嵌套查询。 默认值为 SESSION，这种情况下会缓存一个会话中执行的所有查询。 若设置值为 STATEMENT，本地会话仅用在语句执行上，对相同 SqlSession 的不同调用将不会共享数据。")]
            [ReadOnly(false)]
            [DefaultValue("SESSION")]
            [TypeConverter(typeof(LocalCacheScopeType))] //使用自定义的属性下拉框
            public string LocalCacheScope
            {
                get; set;
            }
            [Browsable(true)]
            [Category("Map文件配置")]
            [DisplayName("jdbcTypeForNull")]
            [Description("当没有为参数提供特定的 JDBC 类型时，为空值指定 JDBC 类型。 某些驱动需要指定列的 JDBC 类型，多数情况直接用一般类型即可，比如 NULL、VARCHAR 或 OTHER。")]
            [ReadOnly(false)]
            [DefaultValue("OTHER")]
            [TypeConverter(typeof(JdbcTypeForNullType))] //使用自定义的属性下拉框
            public string JdbcTypeForNull
            {
                get; set;
            }
            [Browsable(true)]
            [Category("Map文件配置")]
            [DisplayName("lazyLoadTriggerMethods")]
            [Description("指定哪个对象的方法触发一次延迟加载。用逗号分隔的方法列表:equals,clone,hashCode,toString")]
            [ReadOnly(false)]
            [Editor(typeof(PropertyGridRichText), typeof(System.Drawing.Design.UITypeEditor))]
            public string LazyLoadTriggerMethods
            {
                get; set;
            }
            [Browsable(true)]
            [Category("Map文件配置")]
            [DisplayName("defaultScriptingLanguage")]
            [Description("指定动态 SQL 生成的默认语言。如：org.apache.ibatis.scripting.xmltags.XMLLanguageDriver")]
            [ReadOnly(false)]
            [Editor(typeof(PropertyGridRichText), typeof(System.Drawing.Design.UITypeEditor))]
            public string DefaultScriptingLanguage
            {
                get; set;
            }
            [Browsable(true)]
            [Category("Map文件配置")]
            [DisplayName("defaultEnumTypeHandler")]
            [Description("指定 Enum 使用的默认 TypeHandler 。 (从3.4.5开始)，如：org.apache.ibatis.type.EnumTypeHandler")]
            [ReadOnly(false)]
            [Editor(typeof(PropertyGridRichText), typeof(System.Drawing.Design.UITypeEditor))]
            public string DefaultEnumTypeHandler
            {
                get; set;
            }
            [Browsable(true)]
            [Category("Map文件配置")]
            [DisplayName("callSettersOnNulls")]
            [Description("指定当结果集中值为 null 的时候是否调用映射对象的 setter（map 对象时为 put）方法，这对于有 Map.keySet() 依赖或 null 值初始化的时候是有用的。注意基本类型（int、boolean等）是不能设置成 null 的。")]
            [ReadOnly(false)]
            [DefaultValue(false)]
            public bool CallSettersOnNulls
            {
                get; set;
            }
            [Browsable(true)]
            [Category("Map文件配置")]
            [DisplayName("returnInstanceForEmptyRow")]
            [Description("当返回行的所有列都是空时，MyBatis默认返回null。 当开启这个设置时，MyBatis会返回一个空实例。 ")]
            [ReadOnly(false)]
            [DefaultValue(false)]
            public bool ReturnInstanceForEmptyRow
            {
                get; set;
            }
            [Browsable(true)]
            [Category("Map文件配置")]
            [DisplayName("logPrefix")]
            [Description("指定 MyBatis 增加到日志名称的前缀。")]
            [ReadOnly(false)]
            [Editor(typeof(PropertyGridRichText), typeof(System.Drawing.Design.UITypeEditor))]
            public string LogPrefix
            {
                get; set;
            }
            [Browsable(true)]
            [Category("Map文件配置")]
            [DisplayName("logImpl")]
            [Description("指定 MyBatis 所用日志的具体实现，未指定时将自动查找。")]
            [ReadOnly(false)]
            [TypeConverter(typeof(JLogImplType))] //使用自定义的属性下拉框
            [Editor(typeof(PropertyGridRichText), typeof(System.Drawing.Design.UITypeEditor))]
            public string LogImpl
            {
                get; set;
            }
            [Browsable(true)]
            [Category("Map文件配置")]
            [DisplayName("proxyFactory")]
            [Description("指定 Mybatis 创建具有延迟加载能力的对象所用到的代理工具。")]
            [ReadOnly(false)]
            [DefaultValue("JAVASSIST")]
            [TypeConverter(typeof(ProxyFactoryType))] //使用自定义的属性下拉框
            public string ProxyFactory
            {
                get; set;
            }
            [Browsable(true)]
            [Category("Map文件配置")]
            [DisplayName("vfsImpl")]
            [Description("指定VFS的实现")]
            [ReadOnly(false)]
            [Editor(typeof(PropertyGridRichText), typeof(System.Drawing.Design.UITypeEditor))]
            public string VfsImpl
            {
                get; set;
            }
            [Browsable(true)]
            [Category("Map文件配置")]
            [DisplayName("useActualParamName")]
            [Description("允许使用方法签名中的名称作为语句参数名称。 为了使用该特性，你的工程必须采用Java 8编译，并且加上-parameters选项。（从3.4.1开始）")]
            [ReadOnly(false)]
            [DefaultValue(true)]
            public bool UseActualParamName
            {
                get; set;
            }
            [Browsable(true)]
            [Category("Map文件配置")]
            [DisplayName("configurationFactory")]
            [Description("指定一个提供Configuration实例的类。 这个被返回的Configuration实例用来加载被反序列化对象的懒加载属性值。 这个类必须包含一个签名方法static Configuration getConfiguration(). (从 3.2.3 版本开始)")]
            [ReadOnly(false)]
            [Editor(typeof(PropertyGridRichText), typeof(System.Drawing.Design.UITypeEditor))]
            public string ConfigurationFactory
            {
                get; set;
            }
            #endregion

            #region 公共方法
            /// <summary>
            /// 恢复官方默认值
            /// </summary>
            public void SetIni()
            {
                ParaCreateType = "SpringCloud";
                CacheEnabled = true;
                LazyLoadingEnabled = false;
                AggressiveLazyLoading = false;
                MultipleResultSetsEnabled = true;
                UseColumnLabel = true;
                AutoMappingBehavior = "PARTIA";
                AutoMappingUnknownColumnBehavior = "NONE";
                DefaultExecutorType = "SIMPLE";
                SafeRowBoundsEnabled = false;
                SafeResultHandlerEnabled = true;
                MapUnderscoreToCamelCase = false;
                LocalCacheScope = "SESSION";
                JdbcTypeForNull = "OTHER";
                CallSettersOnNulls = false;
                ReturnInstanceForEmptyRow = false;
                ProxyFactory = "JAVASSIST";
                UseActualParamName = true;
            }
            #endregion
        }
        #endregion

        #region 生成配置
        public partial class Class_Create
        {
            public Class_Create()
            {
                EnglishSign = false;
                MethodSite = "粘子层";
                HttpRequestType = "Post";
                ReadOnly = false;
                SwaggerSign = true;
                Port = "8080";
            }
            [Browsable(true)]
            [Category("生成配置")]
            [DisplayName("方法标识")]
            [Description("方法唯一标识")]
            [ReadOnly(true)]
            public string MethodId
            {
                get; set;
            }
            [Browsable(true)]
            [Category("生成配置")]
            [DisplayName("HTTP请求方式")]
            [Description(@"1、【OPTIONS】：返回服务器针对特定资源所支持的HTTP请求方法，也可以利用向web服务器发送‘*’的请求来测试服务器的功能性
2、【HEAD】：向服务器索与GET请求相一致的响应，只不过响应体将不会被返回。这一方法可以再不必传输整个响应内容的情况下，就可以获取包含在响应小消息头中的元信息。
3、【GET】：向特定的资源发出请求。它本质就是发送一个请求来取得服务器上的某一资源。资源通过一组HTTP头和呈现数据（如HTML文本，或者图片或者视频等）返回给客户端。GET请求中，永远不会包含呈现数据。
4、【POST】：向指定资源提交数据进行处理请求（例如提交表单或者上传文件）。数据被包含在请求体中。POST请求可能会导致新的资源的建立和 / 或已有资源的修改。 Loadrunner中对应POST请求函数：web_submit_data, web_submit_form
5、【PUT】：向指定资源位置上传其最新内容
6、【DELETE】：请求服务器删除Request - URL所标识的资源
7、【TRACE】：回显服务器收到的请求，主要用于测试或诊断
8、【CONNECT】：HTTP / 1.1协议中预留给能够将连接改为管道方式的代理服务器")]
            [ReadOnly(false)]
            [TypeConverter(typeof(HttpRequestTypeItem))] //使用自定义的属性下拉框
            [DefaultValue("Post")]
            public string HttpRequestType
            {
                get; set;
            }
            [Browsable(true)]
            [Category("生成配置")]
            [DisplayName("方法位置")]
            [Description("针对分布式系统中，代码存放的位置：聚合层、粘子层")]
            [ReadOnly(false)]
            [TypeConverter(typeof(MothedTypeItem))] //使用自定义的属性下拉框
            [DefaultValue("粘子层")]
            public string MethodSite
            {
                get; set;
            }
            [Browsable(true)]
            [Category("生成配置")]
            [DisplayName("系统名称")]
            [Description("不同系统之间，微服务名、方法名等，可允许不同")]
            [ReadOnly(false)]
            [TypeConverter(typeof(ProjectNameTypeItem))] //使用自定义的属性下拉框
            public string SystemName
            {
                get; set;
            }
            [Browsable(true)]
            [Category("生成配置")]
            [DisplayName("启动端口")]
            [Description("该实例启动的默认端口")]
            [ReadOnly(false)]
            [DefaultValue(8080)]
            public string Port
            {
                get; set;
            }
            [Browsable(true)]
            [Category("生成配置")]
            [DisplayName("微服务名")]
            [Description("微服务名,将放入Control层")]
            [ReadOnly(false)]
            [Editor(typeof(PropertyGridRichText), typeof(System.Drawing.Design.UITypeEditor))]
            public string MicroServiceName
            {
                get; set;
            }
            [Browsable(true)]
            [Category("生成配置")]
            [DisplayName("生成Swagger2文档")]
            [Description("true:生成注释文档，false:不生成注释文档；")]
            [ReadOnly(false)]
            [DefaultValue(true)]
            public bool SwaggerSign
            {
                get; set;
            }
            [Browsable(true)]
            [Category("生成配置")]
            [DisplayName("生成英文版")]
            [Description("true:生成中英文，false:仅生成中文；")]
            [ReadOnly(false)]
            [DefaultValue(false)]
            public bool EnglishSign
            {
                get; set;
            }
            [Browsable(true)]
            [Category("生成配置")]
            [DisplayName("创建者姓名")]
            [Description("设计者姓名")]
            [ReadOnly(false)]
            [Editor(typeof(PropertyGridRichText), typeof(System.Drawing.Design.UITypeEditor))]
            public string CreateMan
            {
                get; set;
            }
            [Browsable(true)]
            [Category("生成配置")]
            [DisplayName("后端工程师姓名")]
            [Description("后端工程师姓名")]
            [ReadOnly(false)]
            [Editor(typeof(PropertyGridRichText), typeof(System.Drawing.Design.UITypeEditor))]
            public string CreateDo
            {
                get; set;
            }
            [Browsable(true)]
            [Category("生成配置")]
            [DisplayName("前端工程师姓名")]
            [Description("前端工程师姓名")]
            [ReadOnly(false)]
            [Editor(typeof(PropertyGridRichText), typeof(System.Drawing.Design.UITypeEditor))]
            public string CreateFrontDo
            {
                get; set;
            }
            [Browsable(true)]
            [Category("生成配置")]
            [DisplayName("最后更新时间")]
            [Description("最后创建方法的时间")]
            [ReadOnly(true)]
            public DateTime DateTime
            {
                get; set;
            }
            [Browsable(true)]
            [Category("生成配置")]
            [DisplayName("是否只读")]
            [Description("是：执行者只能查看，不能重新生成，否则，可以重新生成；")]
            [ReadOnly(false)]
            [DefaultValue(false)]
            public bool ReadOnly { get; set; }

        }
        #endregion

        #region DTO字段类
        public partial class Class_OutField
        {
            public int PageIndex { get; set; }
            public string TableSimplificationName { get; set; }
            public string FieldName { get; set; }
            public string OutFieldName { get; set; }
            public string FieldRemark { get; set; }
            public string FieldType { get; set; }
            public string FunctionName { get; set; }
        }
        #endregion

        #region 外键关联类
        public partial class Class_LinkFieldInfo
        {
            public Class_LinkFieldInfo()
            {
                LinkType = 1;
                CountToCount = 0;
                CheckOk = false;
            }
            public bool CheckOk { get; set; }
            /// <summary>
            /// 当前表序号
            /// </summary>
            public int CurTableNo { get; set; }
            /// <summary>
            /// 关联表序号
            /// </summary>
            public int TableNo { get; set; }
            /// <summary>
            /// 外键字段
            /// </summary>
            public string MainFieldName { get; set; }
            /// <summary>
            /// 当前字段
            /// </summary>
            public string OutFieldName { get; set; }
            /// <summary>
            /// 连接类型，0:Left Join，1:Inner Join
            /// </summary>
            public int LinkType { get; set; }
            /// <summary>
            /// 0：线性;1:一对一；2：一对多；
            /// </summary>
            public int CountToCount { get; set; }
            /// <summary>
            /// DTO类名
            /// </summary>
            public string DtoClassName { get; set; }
            /// <summary>
            /// Model类名
            /// </summary>
            public string ResultMapType { get; set; }
            /// <summary>
            /// 0：join、1：assosication；2：collection
            /// </summary>
            public int JoinType { get; set; }
            /// <summary>
            /// 0:左连接、1:内连接
            /// </summary>
            public int InnerType { get; set; }
        }
        #endregion

        #region 数据库链接属性
        public partial class Class_SelectDataBase
        {
            public void GetDataBaseDefault()
            {
                Class_PublicMethod class_PublicMethod = new Class_PublicMethod();
                Class_DataBaseConDefault class_DataBaseConDefault = new Class_DataBaseConDefault();
                class_DataBaseConDefault = class_PublicMethod.FromXmlToDefaultValueObject<Class_DataBaseConDefault>("DataBaseDefaultValues");

                if (class_DataBaseConDefault == null)
                {
                    databaseType = "MySql";
                    dataBaseName = "test01";
                    dataSourceUserName = "king";
                    dataSourcePassWord = "123456";
                    dataSourceUrl = "101.201.101.138";
                    Port = 10001;
                }
                else
                {
                    databaseType = class_DataBaseConDefault.databaseType;
                    dataBaseName = class_DataBaseConDefault.dataBaseName;
                    dataSourceUserName = class_DataBaseConDefault.dataSourceUserName;
                    dataSourcePassWord = class_DataBaseConDefault.dataSourcePassWord;
                    dataSourceUrl = class_DataBaseConDefault.dataSourceUrl;
                    Port = class_DataBaseConDefault.Port;
                }
            }
            public Class_SelectDataBase()
            {
                GetDataBaseDefault();
            }

            [Browsable(true)]
            [Category("数据库配置")]
            [DisplayName("数据库类型名")]
            [Description("将支持Mysql、SqlServer 2017\r\n右键菜单可设置为默认值")]
            [ReadOnly(false)]
            [TypeConverter(typeof(DataBaseTypeItem))] //使用自定义的属性下拉框MothedTypeItem
                                                      //[DefaultValue("SqlServer 2017")]
            public string databaseType
            {
                get; set;
            }

            [Browsable(true)]
            [Category("数据库配置")]
            [DisplayName("数据库名称")]
            [Description("指定数据库名\r\n右键菜单可设置为默认值")]
            [ReadOnly(false)]
            [Editor(typeof(PropertyGridRichText), typeof(System.Drawing.Design.UITypeEditor))]
            public string dataBaseName
            {
                get; set;
            }

            [Browsable(true)]
            [Category("数据库配置")]
            [DisplayName("数据库链接地址")]
            [Description("链接地址\r\n右键菜单可设置为默认值")]
            [ReadOnly(false)]
            [Editor(typeof(PropertyGridRichText), typeof(System.Drawing.Design.UITypeEditor))]
            public string dataSourceUrl
            {
                get; set;
            }

            [Browsable(true)]
            [Category("数据库配置")]
            [DisplayName("数据库链接端口号")]
            [Description("此属性，MySql、Oracle数据库有效\r\n右键菜单可设置为默认值")]
            [ReadOnly(false)]
            //[DefaultValue(3306)]
            public int Port
            {
                get; set;
            }

            [Browsable(true)]
            [Category("数据库配置")]
            [DisplayName("登录用户")]
            [Description("用于数据库登录的用户名称\r\n右键菜单可设置为默认值")]
            [ReadOnly(false)]
            [Editor(typeof(PropertyGridRichText), typeof(System.Drawing.Design.UITypeEditor))]
            public string dataSourceUserName
            {
                get; set;
            }

            [Browsable(true)]
            [Category("数据库配置")]
            [DisplayName("登录密码")]
            [Description("用于数据库登录的密码\r\n右键菜单可设置为默认值")]
            [ReadOnly(false)]
            [Editor(typeof(PropertyGridRichText), typeof(System.Drawing.Design.UITypeEditor))]
            public string dataSourcePassWord
            {
                get; set;
            }
        }
        #endregion

        #region 字段类
        public partial class Class_Field
        {
            public Class_Field()
            {
                SelectSelect = false;
                TrimSign = false;
                WhereSelect = false;
                WhereTrim = false;
                WhereIsNull = false;
                OrderSelect = false;
                GroupSelect = false;

                Width = 100;
                Type = "normal";
                LayChecked = false;
                IsDisplay = true;
                UnResize = false;
                Sort = false;
                DisplaySort = 0;
            }
            #region 字段名称
            public string FieldName { get; set; }
            public string FieldRemark { get; set; }
            public string FieldType { get; set; }
            public int FieldLength { get; set; }
            public string FieldDefaultValue { get; set; }
            public bool FieldIsNull { get; set; }
            public bool FieldIsKey { get; set; }
            public bool FieldIsAutoAdd { get; set; }
            public string MultFieldName { get; set; }
            #endregion

            #region Select
            public bool SelectSelect { get; set; }
            public string ParaName { get; set; }
            public int MaxLegth { get; set; }
            public string CaseWhen { get; set; }
            public string ReturnType { get; set; }
            public bool TrimSign { get; set; }
            public string FunctionName { get; set; }
            public string TotalFunctionName { get; set; }
            #endregion

            #region Where
            public bool WhereSelect { get; set; }
            public string WhereType { get; set; }
            public string LogType { get; set; }
            public string WhereValue { get; set; }
            public bool WhereTrim { get; set; }
            public bool WhereIsNull { get; set; }
            #endregion

            #region Order
            public bool OrderSelect { get; set; }
            public string SortType { get; set; }
            public int SortNo { get; set; }
            #endregion

            #region Group
            public bool GroupSelect { get; set; }
            #endregion

            #region Having
            public bool HavingSelect { get; set; }
            public string HavingFunction { get; set; }
            public string HavingCondition { get; set; }
            public string HavingValue { get; set; }
            #endregion

            #region 数据表格
            public int DisplaySort { get; set; }
            public string Title { get; set; }
            public int Width { get; set; }
            public string Type { get; set; }
            public bool LayChecked { get; set; }
            public string Fixed { get; set; }
            public bool IsDisplay { get; set; }
            public bool Sort { get; set; }
            public bool UnResize { get; set; }
            public string Style { get; set; }
            public string Align { get; set; }
            public string ToolBar { get; set; }
            #endregion
        }
        #endregion

        #region 主表字段类
        public partial class Class_Main
        {
            public Class_Main()
            {
                class_Fields = new List<Class_Field>();
                ResultType = 1;
                PanelHeight = 197;
            }
            ~Class_Main()
            {
                class_Fields.Clear();
            }
            public int PanelHeight;
            public string TableName { get; set; }
            /// <summary>
            /// 主键名称
            /// </summary>
            public string MainFieldName { get; set; }
            public bool AddPoint { get; set; }
            public List<Class_Field> class_Fields { get; set; }
            /// <summary>
            /// 表别名
            /// </summary>
            public string AliasName { get; set; }
            /// <summary>
            /// Map命名空间
            /// </summary>
            public string NameSpace { get; set; }
            /// <summary>
            /// 方法说明
            /// </summary>
            public string MethodContent { get; set; }
            /// <summary>
            /// 方法ID
            /// </summary>
            public string MethodId { get; set; }
            /// <summary>
            /// 0：resultMap，1：resultType
            /// </summary>
            public int ResultType { get; set; }
            /// <summary>
            /// XML中的Map内容
            /// </summary>
            public string MapContent { get; set; }
            /// <summary>
            /// XML中的Select内容
            /// </summary>
            public string SelectContent { get; set; }
            public string ServiceInterFaceContent { get; set; }
            /// <summary>
            /// Service接口层方法返回说明
            /// </summary>
            public string ServiceInterFaceReturnRemark { get; set; }
            /// <summary>
            /// Service 返回单条，还是多条
            /// </summary>
            public int ServiceInterFaceReturnCount { get; set; }
            public string ServiceImplContent { get; set; }
            public string ModelContent { get; set; }
            public string DTOContent { get; set; }
            public string DAOContent { get; set; }
            /// <summary>
            /// Controller内容
            /// </summary>
            public string ControlContent { get; set; }
            /// <summary>
            /// InPutParam
            /// </summary>
            public string InPutParamContent { get; set; }
            public string ResultMapId { get; set; }
            public string ControlSwaggerValue { get; set; }
            public string ControlSwaggerDescription { get; set; }
            /// <summary>
            /// 0：join、1：assosication；2：collection
            /// </summary>
            public int JoinType { get; set; }
            /// <summary>
            /// 0:左连接、1:内连接
            /// </summary>
            public int InnerType { get; set; }
            /// <summary>
            /// 测试SQL
            /// </summary>
            public string TestSql { get; set; }

            public string ParamClassName { get; set; }
            public string ModelClassName { get; set; }
            public string DtoClassName { get; set; }
            public string DaoClassName { get; set; }
            public string XmlFileName { get; set; }
            public string ControlRequestMapping { get; set; }
            public string ServiceInterFaceName { get; set; }
            public string ServiceClassName { get; set; }
        }
        #endregion

        #region 表字段类
        public partial class Class_Sub : Class_Main
        {
            public Class_Sub()
            {
                LinkType = 1;
                CountToCount = 0;
                TableNo = -1;
                CreateMainCode = true;
            }
            /// <summary>
            /// 本表的主键
            /// </summary>
            public string OutFieldName { get; set; }
            /// <summary>
            /// 主表的外键
            /// </summary>
            public string MainTableFieldName { get; set; }

            /// <summary>
            /// 连接类型，0:Left Join，1:Inner Join
            /// </summary>
            public int LinkType { get; set; }
            /// <summary>
            /// 0：线性;1:一对一；2：一对多；
            /// </summary>
            public int CountToCount { get; set; }
            /// <summary>
            /// 关联表的序号，主表：0，从表依次变大，-1：没有关联，默认值为：-1
            /// </summary>
            public int TableNo { get; set; }
            /// <summary>
            /// 仅生成主体代码Control
            /// </summary>
            public bool CreateMainCode { get; set; }

        }
        #endregion

        #endregion

        #region 字段列表操作
        public List<Class_OutField> GetClass_OutFields()
        {
            return this.class_OutFields;
        }
        public Class_OutField GetOneClass_OutField(int index)
        {
            if (this.class_OutFields == null || this.class_OutFields.Count == 0)
                return null;
            return this.class_OutFields[index];
        }
        public void IniClass_OutFields()
        {
            if (this.class_OutFields == null)
                class_OutFields = new List<Class_OutField>();
            else
                class_OutFields.Clear();
        }
        public void AddClass_OutField(Class_OutField class_OutField)
        {
            if (class_OutFields == null)
                class_OutFields = new List<Class_OutField>();
            class_OutFields.Add(class_OutField);
        }
        public int GetIndexFromClass_OutField(int CurTableNo, string FieldName)
        {
            int index = -1;
            if (class_OutFields != null && class_OutFields.Count > 0)
                index = class_OutFields.FindIndex(a => !a.PageIndex.Equals(CurTableNo) && a.FieldName.Equals(FieldName));
            return index;
        }
        public void GetUpdateOutFieldName()
        {
            if (this.class_OutFields != null && this.class_OutFields.Count > 0)
            {
                foreach (Class_OutField item in this.class_OutFields)
                {
                    item.OutFieldName = GetDTOFieldName(item.PageIndex, item.FieldName);
                }
            }
        }
        private string GetDTOFieldName(int CurTableNo, string FieldName)
        {
            string OutFieldName = null;
            int index = GetIndexFromClass_OutField(CurTableNo, FieldName);
            if (index > -1)
            {
                Class_OutField class_OutField = GetOneClass_OutField(index);
                OutFieldName = class_OutField.FieldName + class_OutField.TableSimplificationName;
            }
            else
                OutFieldName = FieldName;
            return OutFieldName;
        }
        #endregion

        #region 外键列表操作
        public void UpdateMultFieldName()
        {
            if (this.class_SubList != null)
            {
                foreach (Class_Sub item in class_SubList)
                {
                    if (item.class_Fields != null)
                    {
                        foreach (Class_Field class_Field in item.class_Fields)
                        {
                            if (IsMultTable)
                                class_Field.MultFieldName = string.Format("{0}{1}"
                                    , Class_Tool.GetFirstCodeLow(item.AliasName)
                                    , Class_Tool.GetFirstCodeUpper(class_Field.ParaName));
                            else
                                class_Field.MultFieldName = class_Field.ParaName;
                        }
                    }
                }
            }
        }
        public bool GetHaveSameFieldName(string FieldName, int PageIndex)
        {
            bool finder = false;
            for (int i = 0; i < this.class_SubList.Count; i++)
            {
                if (i != PageIndex && !finder)
                {
                    Class_Sub class_Sub = class_SubList[i];
                    if (class_Sub != null)
                    {
                        for (int j = 0; j < class_Sub.class_Fields.Count; j++)
                        {
                            if (class_Sub.class_Fields[j].SelectSelect)
                            {
                                if (class_Sub.class_Fields[j].ParaName.Equals(FieldName))
                                {
                                    finder = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return finder;
        }
        public List<Class_LinkFieldInfo> GetClass_LinkFieldInfos()
        {
            return this.class_LinkFieldInfos;
        }
        public Class_LinkFieldInfo GetOneClass_LinkFieldInfo(int index)
        {
            if (this.class_LinkFieldInfos == null || this.class_LinkFieldInfos.Count == 0)
                return null;
            return this.class_LinkFieldInfos[index];
        }
        public int GetRowNumberLinkFieldInfo(string MainFieldName, string OutFieldName)
        {
            int finder = -1;
            if (this.class_LinkFieldInfos != null && this.class_LinkFieldInfos.Count > 0)
            {
                for (int i = 0; i < this.class_LinkFieldInfos.Count; i++)
                {
                    Class_LinkFieldInfo class_LinkFieldInfo = this.class_LinkFieldInfos[i];
                    if (class_LinkFieldInfo.MainFieldName.Equals(MainFieldName) && class_LinkFieldInfo.OutFieldName.Equals(OutFieldName))
                        finder = i;
                }
            }
            return finder;
        }
        public void IniLinkFieldInfos()
        {
            if (class_LinkFieldInfos == null)
                class_LinkFieldInfos = new List<Class_LinkFieldInfo>();
            else
                class_LinkFieldInfos.Clear();
        }
        public void AddLinkFieldInfosCount(Class_LinkFieldInfo class_LinkFieldInfo)
        {
            if (class_LinkFieldInfos == null)
                class_LinkFieldInfos = new List<Class_LinkFieldInfo>();
            class_LinkFieldInfos.Add(class_LinkFieldInfo);
        }
        public int GetLinkFieldInfosCount()
        {
            if (class_LinkFieldInfos == null)
                return 0;
            else
                return class_LinkFieldInfos.Count;
        }
        public void UpdateIsMultTableSign()
        {
            if (class_LinkFieldInfos == null)
                IsMultTable = false;
            else
                IsMultTable = class_LinkFieldInfos.Count > 1 ? true : false;
        }
        public void AddAllOutFieldName()
        {
            if (GetLinkFieldInfosCount() > 1)
            {
                IniClass_OutFields();
                if (class_SubList != null)
                {
                    foreach (Class_Sub item in class_SubList)
                    {
                        int index = 0;
                        if (item.class_Fields.Count > 0)
                        {
                            foreach (Class_Field class_Field in item.class_Fields)
                            {
                                if (class_Field.SelectSelect)
                                {
                                    Class_OutField class_OutField = new Class_OutField();
                                    class_OutField.PageIndex = index;
                                    class_OutField.FieldName = class_Field.FieldName;
                                    class_OutField.TableSimplificationName = item.DtoClassName;
                                    class_OutField.FieldRemark = class_Field.FieldRemark;
                                    class_OutField.FieldType = class_Field.FieldType;
                                    class_OutField.FunctionName = class_Field.FunctionName;
                                    AddClass_OutField(class_OutField);
                                }
                            }
                        }
                        index++;
                    }
                    GetUpdateOutFieldName();
                }
            }
        }
        public int AddLinkFieldInfo()
        {
            IniLinkFieldInfos();
            for (int i = 0; i < class_SubList.Count; i++)
            {
                Class_Sub class_Sub = class_SubList[i];
                Class_LinkFieldInfo class_LinkFieldInfo = new Class_LinkFieldInfo();
                class_LinkFieldInfo.CurTableNo = i;
                class_LinkFieldInfo.MainFieldName = class_Sub.MainTableFieldName;
                class_LinkFieldInfo.OutFieldName = class_Sub.OutFieldName;
                class_LinkFieldInfo.LinkType = class_Sub.LinkType;
                class_LinkFieldInfo.CountToCount = class_Sub.CountToCount;
                class_LinkFieldInfo.TableNo = class_Sub.TableNo;
                class_LinkFieldInfo.InnerType = class_Sub.InnerType;
                class_LinkFieldInfo.JoinType = class_Sub.JoinType;
                class_LinkFieldInfo.DtoClassName = class_Sub.DtoClassName;
                class_LinkFieldInfo.ResultMapType = class_Sub.ModelClassName;
                AddLinkFieldInfosCount(class_LinkFieldInfo);
            }
            return GetLinkFieldInfosCount();
        }

        #endregion
    }
}

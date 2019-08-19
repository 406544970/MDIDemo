using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace MDIDemo.PublicClass
{
    /// <summary>
    /// 多行编辑框
    /// </summary>
    public class PropertyGridRichText : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            try
            {
                IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if (edSvc != null)
                {
                    RichTextBox box = new RichTextBox();
                    if (value is string)
                    {
                        box.Text = value as string;
                    }
                    edSvc.DropDownControl(box);
                    return box.Text;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return value;
        }
    }
    /// <summary>
    /// 复选框
    /// </summary>
    public class CheckboxPro : System.Drawing.Design.UITypeEditor
    {
        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override void PaintValue(System.Drawing.Design.PaintValueEventArgs e)
        {
            ControlPaint.DrawCheckBox(e.Graphics, e.Bounds, ButtonState.All);
        }
    }
    /// <summary>
    /// 单选框：父类
    /// </summary>
    public abstract class ComboxItem : StringConverter
    {
        protected string[] myList;
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(myList); //编辑下拉框中的items
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
    }
    /// <summary>
    /// 单选框：数据库类型
    /// </summary>
    public sealed class DataBaseTypeItem : ComboxItem
    {
        public DataBaseTypeItem()
        {
            base.myList = new string[3];
            myList[0] = "MySql";
            myList[1] = "SqlServer 2017";
            myList[2] = "Oracle 11g";
        }
    }
    /// <summary>
    /// 单选框：数据库类型
    /// </summary>
    public sealed class MothedTypeItem : ComboxItem
    {
        public MothedTypeItem()
        {
            base.myList = new string[2];
            myList[0] = "聚合层";
            myList[1] = "粘子层";
        }
    }
    /// <summary>
    /// 单选框：数据库类型
    /// </summary>
    public sealed class HttpRequestTypeItem : ComboxItem
    {
        public HttpRequestTypeItem()
        {
            base.myList = new string[4];
            myList[0] = "Post";
            myList[1] = "Get";
            myList[2] = "Delete";
            myList[3] = "Put";
        }
    }
    public sealed class ProjectNameTypeItem : ComboxItem
    {
        public ProjectNameTypeItem()
        {
            base.myList = new string[2];
            myList[0] = "系统架构项目";
            myList[1] = "万联易达";
        }
    }
    public sealed class MybatisXmlCreateType : ComboxItem
    {
        public MybatisXmlCreateType()
        {
            base.myList = new string[2];
            myList[0] = "SpringCloud";
            myList[1] = "Original";
        }
    }
    public sealed class AutoMappingBehaviorType : ComboxItem
    {
        public AutoMappingBehaviorType()
        {
            base.myList = new string[3];
            myList[0] = "NONE";
            myList[1] = "PARTIAL";
            myList[2] = "FULL";
        }
    }
    public sealed class AutoMappingUnknownColumnBehaviorType : ComboxItem
    {
        public AutoMappingUnknownColumnBehaviorType()
        {
            base.myList = new string[3];
            myList[0] = "NONE";
            myList[1] = "WARNING";
            myList[2] = "FAILING";
        }
    }
    public sealed class DefaultExecutorType : ComboxItem
    {
        public DefaultExecutorType()
        {
            base.myList = new string[3];
            myList[0] = "SIMPLE";
            myList[1] = "REUSE";
            myList[2] = "BATCH";
        }
    }
    public sealed class LocalCacheScopeType : ComboxItem
    {
        public LocalCacheScopeType()
        {
            base.myList = new string[2];
            myList[0] = "SESSION";
            myList[1] = "STATEMENT";
        }
    }
    public sealed class JdbcTypeForNullType : ComboxItem
    {
        public JdbcTypeForNullType()
        {
            base.myList = new string[3];
            myList[0] = "NULL";
            myList[1] = "VARCHAR";
            myList[2] = "OTHER";
        }
    }
    public sealed class JLogImplType : ComboxItem
    {
        public JLogImplType()
        {
            base.myList = new string[3];
            myList[0] = "NULL";
            myList[1] = "VARCHAR";
            myList[2] = "OTHER";
        }
    }
    public sealed class ProxyFactoryType : ComboxItem
    {
        public ProxyFactoryType()
        {
            base.myList = new string[7];
            myList[0] = "SLF4J";
            myList[1] = "LOG4J";
            myList[2] = "LOG4J2";
            myList[3] = "JDK_LOGGING";
            myList[4] = "COMMONS_LOGGING";
            myList[5] = "STDOUT_LOGGING";
            myList[6] = "NO_LOGGIN";
        }
    }
}

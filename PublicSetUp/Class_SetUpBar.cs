using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDIDemo.PublicSetUp
{
    /// <summary>
    /// 设置工具栏Bar
    /// </summary>
    public class Class_SetUpBar
    {
        /// <summary>
        /// 设置Bar
        /// </summary>
        /// <param name="Bar"></param>
        /// <param name="BarText"></param>
        public void setBar(Bar Bar,string BarText)
        {
            Bar.OptionsBar.AllowQuickCustomization = false;
            Bar.Text = BarText;
        }
    }

}

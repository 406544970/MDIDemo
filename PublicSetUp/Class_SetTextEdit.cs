using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDIDemo.PublicSetUp
{
    public class Class_SetTextEdit
    {
        /// <summary>
        /// 设置DevTestEdit,非只读设置
        /// </summary>
        /// <param name="textEdit">控件对象</param>
        /// <param name="color">指定颜色</param>
        public void SetTextEdit(TextEdit textEdit, Color color)
        {
            SetTextEdit(textEdit, false, color);
        }
        /// <summary>
        /// 设置DevTestEdit,只读，且为红色
        /// </summary>
        /// <param name="textEdit">控件对象</param>
        public void SetTextEdit(TextEdit textEdit)
        {
            SetTextEdit(textEdit, true,Color.Tomato);
        }
        /// <summary>
        /// 设置DevTestEdit
        /// </summary>
        /// <param name="textEdit">控件对象</param>
        /// <param name="ReadOnly">是否只读</param>
        /// <param name="color">指定颜色</param>
        public void SetTextEdit(TextEdit textEdit,bool ReadOnly,Color color)
        {
            textEdit.Properties.ReadOnly = ReadOnly;
            textEdit.ForeColor = color;
            textEdit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }
    }
}

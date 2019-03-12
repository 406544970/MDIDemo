using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDIDemo.PublicSetUp
{
    public class Class_SetLabelControl
    {
        public void SetLabel(LabelControl labelControl)
        {
            labelControl.Text = "";
            labelControl.ForeColor = Color.Red;
            labelControl.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }
    }
}

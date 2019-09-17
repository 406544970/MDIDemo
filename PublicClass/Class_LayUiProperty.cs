using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDIDemo.PublicClass
{
    public class Class_LayUiProperty : IClass_InterFaceUiProperty
    {
        public List<string> GetColumnToolType()
        {
            List<string> vs = new List<string>();
            vs.Add("ColumnTool1");
            vs.Add("ColumnTool2");
            vs.Add("ColumnTool3");
            vs.Add("ColumnTool4");
            return vs;
        }

        public string GetVueLower(string Content)
        {
            return Class_Tool.GetVueString(Content);
        }

        public List<string> GetToolType()
        {
            List<string> vs = new List<string>();
            vs.Add("Tool1");
            vs.Add("Tool2");
            vs.Add("Tool3");
            vs.Add("Tool4");
            return vs;
        }

        public string GetVueUpper(string Content)
        {
            string[] vs = Content.Split('-');
            if (vs.Length > 1)
            {
                Content = null;
                foreach (string row in vs)
                    Content += row.Substring(0, 1).ToUpper() + row.Substring(1);
            }
            return Content.Substring(0, 1).ToUpper() + Content.Substring(1);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDIDemo.PublicClass
{
    public interface IClass_InterFaceUiProperty
    {
        List<string> GetToolType();
        List<string> GetColumnToolType();
        string GetVueLower(string Content);
        string GetVueUpper(string Content);
    }
}

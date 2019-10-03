using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDIDemo.PublicClass
{
    public class Class_SystemDefault
    {
        public int SelectOpenWindowIndex { get; set; }
    }

    public class PageModel
    {
        public string pageKey { get; set; }
        public string projectId { get; set; }
        public string pageType { get; set; }
        public int pageVersion { get; set; }
        public DateTime createTime { get; set; }
        public DateTime lastUpdateTime { get; set; }
        public string createOperator { get; set; }
        public string createOperatorId { get; set; }
        public string doOperator { get; set; }
        public string doOperatorId { get; set; }
        public string frontOperator { get; set; }
        public string frontOperatorId { get; set; }
        public int finishCount { get; set; }
        public bool readOnly { get; set; }
        public string methodRemark { get; set; }
        public int operateType { get; set; }
    }

    public sealed class PageVersionListInParam
    {
        public List<PageVersionInParam> pageKey;
        public partial class PageVersionInParam
        {
            public string pageKey { get; set; }
            public int pageVersion { get; set; }
        }
    }
}

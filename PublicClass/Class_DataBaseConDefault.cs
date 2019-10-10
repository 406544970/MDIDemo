using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDIDemo.PublicClass
{
    public abstract class Class_DataBaseDefault
    {
        public string databaseType { get; set; }
        public string dataBaseName { get; set; }
        public string dataSourceUrl { get; set; }
        public string dataSourceUserName { get; set; }
        public string dataSourcePassWord { get; set; }
    }

    public sealed class Class_DataBaseConDefault: Class_DataBaseDefault
    {
        public int Port { get; set; }
    }
    public class Class_AllParamSetUp
    {
        public Class_AllParamSetUp()
        {
            this.RemotePort = 0;
            HttpSign = true;
        }
        public string AllPackageName;
        public int RemotePort;
        public string RemoteAddress;
        public bool HttpSign;
    }
}

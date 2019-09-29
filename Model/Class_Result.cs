using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDIDemo.Model
{
    public class ResultVO<T>
    {
        public int code { get; set; }
        public string msg { get; set; }
        public long count { get; set; }
        public T data { get; set; }

    }
}

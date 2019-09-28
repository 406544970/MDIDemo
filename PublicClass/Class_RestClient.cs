using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MDIDemo.PublicClass
{
    public class Class_ParaArray
    {
        public string ParaName { get; set; }
        public object ParaValue { get; set; }
    }

    public class Class_RestClient
    {
        private string BaseUrl;
        public Class_RestClient(string BaseUrl, bool HttpSign)
        {
            IniClass(BaseUrl, HttpSign);
        }
        public Class_RestClient(string BaseUrl)
        {
            IniClass(BaseUrl, true);
        }

        private void IniClass(string BaseUrl, bool HttpSign)
        {
            if (HttpSign)
                this.BaseUrl = string.Format("http://{0}", BaseUrl);
            else
                this.BaseUrl = string.Format("https://{0}", BaseUrl);
        }
        private string _GetParaUrl(List<Class_ParaArray> class_ParaArrays)
        {
            if (class_ParaArrays == null)
                return null;
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Class_ParaArray class_ParaArray in class_ParaArrays)
                stringBuilder.AppendFormat("&{0}={1}", class_ParaArray.ParaName, class_ParaArray.ParaValue.ToString());
            if (stringBuilder.Length > 0)
                return stringBuilder.ToString().Substring(1);
            else
                return null;
        }
        #region Get请求
        public string Get(string Url, List<Class_ParaArray> class_ParaArrays)
        {
            string ParaList = _GetParaUrl(class_ParaArrays);
            //先根据用户请求的uri构造请求地址
            string serviceUrl = string.Format("{0}/{1}", this.BaseUrl, Url);
            if (ParaList != null)
                serviceUrl += ParaList;
            //创建Web访问对  象
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
            //通过Web访问对象获取响应内容
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            //通过响应内容流创建StreamReader对象，因为StreamReader更高级更快
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            //string returnXml = HttpUtility.UrlDecode(reader.ReadToEnd());//如果有编码问题就用这个方法
            string returnXml = reader.ReadToEnd();//利用StreamReader就可以从响应内容从头读到尾
            reader.Close();
            myResponse.Close();
            return returnXml;
        }
        #endregion

        #region Post请求
        public string Post(string Url, List<Class_ParaArray> class_ParaArrays)
        {
            HttpWebRequest myRequest = null;
            StreamReader reader = null;
            HttpWebResponse myResponse = null;
            try
            {
                string Data = _GetParaUrl(class_ParaArrays);
                //先根据用户请求的uri构造请求地址
                string serviceUrl = string.Format("{0}/{1}", this.BaseUrl, Url);
                //创建Web访问对象
                myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
                //把用户传过来的数据转成“UTF-8”的字节流
                byte[] buf = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(Data);

                myRequest.Method = "POST";
                myRequest.ContentLength = buf.Length;
                myRequest.ContentType = "application/json";
                myRequest.MaximumAutomaticRedirections = 1;
                myRequest.AllowAutoRedirect = true;
                //发送请求
                if (buf.Length > 0)
                {
                    Stream stream = myRequest.GetRequestStream();
                    stream.Write(buf, 0, buf.Length);
                    stream.Close();
                }

                //获取接口返回值
                //通过Web访问对象获取响应内容
                myResponse = (HttpWebResponse)myRequest.GetResponse();
                //通过响应内容流创建StreamReader对象，因为StreamReader更高级更快
                reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                //string returnXml = HttpUtility.UrlDecode(reader.ReadToEnd());//如果有编码问题就用这个方法
                string returnXml = reader.ReadToEnd();//利用StreamReader就可以从响应内容从头读到尾
                return returnXml;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                reader.Close();
                myResponse.Close();
            }
        }
        #endregion

        #region Put请求
        public string Put(string data, string uri)
        {
            //先根据用户请求的uri构造请求地址
            string serviceUrl = string.Format("{0}/{1}", this.BaseUrl, uri);
            //创建Web访问对象
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
            //把用户传过来的数据转成“UTF-8”的字节流
            byte[] buf = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(data);

            myRequest.Method = "PUT";
            myRequest.ContentLength = buf.Length;
            myRequest.ContentType = "application/json";
            myRequest.MaximumAutomaticRedirections = 1;
            myRequest.AllowAutoRedirect = true;
            //发送请求
            Stream stream = myRequest.GetRequestStream();
            stream.Write(buf, 0, buf.Length);
            stream.Close();

            //获取接口返回值
            //通过Web访问对象获取响应内容
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            //通过响应内容流创建StreamReader对象，因为StreamReader更高级更快
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            //string returnXml = HttpUtility.UrlDecode(reader.ReadToEnd());//如果有编码问题就用这个方法
            string returnXml = reader.ReadToEnd();//利用StreamReader就可以从响应内容从头读到尾
            reader.Close();
            myResponse.Close();
            return returnXml;
        }
        #endregion

        #region Delete请求
        public string Delete(string data, string uri)
        {
            //先根据用户请求的uri构造请求地址
            string serviceUrl = string.Format("{0}/{1}", this.BaseUrl, uri);
            //创建Web访问对象
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
            //把用户传过来的数据转成“UTF-8”的字节流
            byte[] buf = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(data);

            myRequest.Method = "DELETE";
            myRequest.ContentLength = buf.Length;
            myRequest.ContentType = "application/json";
            myRequest.MaximumAutomaticRedirections = 1;
            myRequest.AllowAutoRedirect = true;
            //发送请求
            Stream stream = myRequest.GetRequestStream();
            stream.Write(buf, 0, buf.Length);
            stream.Close();

            //获取接口返回值
            //通过Web访问对象获取响应内容
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            //通过响应内容流创建StreamReader对象，因为StreamReader更高级更快
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            //string returnXml = HttpUtility.UrlDecode(reader.ReadToEnd());//如果有编码问题就用这个方法
            string returnXml = reader.ReadToEnd();//利用StreamReader就可以从响应内容从头读到尾
            reader.Close();
            myResponse.Close();
            return returnXml;
        }
        #endregion
    }
}

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
        private string _GetParaUrl(List<Class_ParaArray> class_ParaArrays, bool AddDefault)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (class_ParaArrays != null)
                foreach (Class_ParaArray class_ParaArray in class_ParaArrays)
                    stringBuilder.AppendFormat("&{0}={1}", class_ParaArray.ParaName, class_ParaArray.ParaValue.ToString());
            if (AddDefault)
            {
                stringBuilder.AppendFormat("&{0}={1}", Class_MyInfo.UseId, Class_MyInfo.UseIdValue);
                stringBuilder.AppendFormat("&{0}={1}", Class_MyInfo.UseName, Class_MyInfo.UseNameValue);
                stringBuilder.AppendFormat("&{0}={1}", Class_MyInfo.UseType, Class_MyInfo.UseTypeValue);
                if (Class_MyInfo.TokenEffectiveDateTime > DateTime.Now)
                    stringBuilder.AppendFormat("&{0}={1}", Class_MyInfo.TokenName, Class_MyInfo.TokenNameValue);
            }
            if (stringBuilder.Length > 0)
                return "?" + stringBuilder.ToString().Substring(1);
            else
                return null;
        }
        #region Get请求
        public string Get(string Url, List<Class_ParaArray> class_ParaArrays)
        {
            string ParaList = _GetParaUrl(class_ParaArrays, false);
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
        public bool UploadFileByHttp(string url, string filePath)
        {
            // 时间戳，用做boundary
            url = string.Format("{0}/{1}", this.BaseUrl, url);
            string timeStamp = DateTime.Now.Ticks.ToString("x");

            //根据uri创建HttpWebRequest对象
            HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(new Uri(url));
            httpReq.Method = "POST";
            httpReq.AllowWriteStreamBuffering = false; //对发送的数据不使用缓存
            httpReq.Timeout = 300000;  //设置获得响应的超时时间（300秒）
            httpReq.ContentType = "multipart/form-data; boundary=" + timeStamp;

            //文件
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);

            //头信息
            string boundary = "--" + timeStamp;
            string dataFormat = boundary + "\r\nContent-Disposition: form-data; name=\"{0}\";filename=\"{1}\"\r\nContent-Type:application/octet-stream\r\n\r\n";
            string header = string.Format(dataFormat, "file", Path.GetFileName(filePath));
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(header);

            //结束边界
            byte[] boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + timeStamp + "--\r\n");

            long length = fileStream.Length + postHeaderBytes.Length + boundaryBytes.Length;

            httpReq.ContentLength = length;//请求内容长度

            try
            {
                //每次上传4k
                int bufferLength = 4096;
                byte[] buffer = new byte[bufferLength];

                //已上传的字节数
                long offset = 0;
                int size = binaryReader.Read(buffer, 0, bufferLength);
                Stream postStream = httpReq.GetRequestStream();

                //发送请求头部消息
                postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);

                while (size > 0)
                {
                    postStream.Write(buffer, 0, size);
                    offset += size;
                    size = binaryReader.Read(buffer, 0, bufferLength);
                }

                //添加尾部边界
                postStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                postStream.Close();

                //获取服务器端的响应
                using (HttpWebResponse response = (HttpWebResponse)httpReq.GetResponse())
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                    string returnValue = readStream.ReadToEnd();
                    response.Close();
                    readStream.Close();
                }
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                fileStream.Close();
                binaryReader.Close();
            }
        }
        public string Post(string Url)
        {
            return Post(Url, null, null, false);
        }
        public string Post(string Url, List<Class_ParaArray> class_ParaArrays)
        {
            return Post(Url, class_ParaArrays, null, false);
        }
        public string PostBinary(string Url, List<Class_ParaArray> class_ParaArrays, byte[] data, bool AddDefault)
        {
            HttpWebRequest myRequest = null;
            StreamReader reader = null;
            HttpWebResponse myResponse = null;
            byte[] buf = null;
            Stream stream = null;
            try
            {
                if (data != null)
                    buf = data;
                string Data = _GetParaUrl(class_ParaArrays, AddDefault);
                //先根据用户请求的uri构造请求地址
                string serviceUrl = string.Format("{0}/{1}", this.BaseUrl, Url);
                if (Data != null)
                    serviceUrl += Data;
                //创建Web访问对象
                myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
                //把用户传过来的数据转成“UTF-8”的字节流
                myRequest.Method = "POST";
                myRequest.ContentType = "application/json";
                myRequest.MaximumAutomaticRedirections = 1;
                myRequest.AllowAutoRedirect = true;

                //发送请求
                if (data != null)
                {
                    stream = myRequest.GetRequestStream();
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
        public string Post(string Url, List<Class_ParaArray> class_ParaArrays, string data, bool AddDefault)
        {
            HttpWebRequest myRequest = null;
            StreamReader reader = null;
            HttpWebResponse myResponse = null;
            byte[] buf = null;
            Stream stream = null;
            try
            {
                if (data != null)
                    buf = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(data);
                string Data = _GetParaUrl(class_ParaArrays, AddDefault);
                //先根据用户请求的uri构造请求地址
                string serviceUrl = string.Format("{0}/{1}", this.BaseUrl, Url);
                if (Data != null)
                    serviceUrl += Data;
                //创建Web访问对象
                myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
                //把用户传过来的数据转成“UTF-8”的字节流
                myRequest.Method = "POST";
                myRequest.ContentType = "application/json";
                myRequest.MaximumAutomaticRedirections = 1;
                myRequest.AllowAutoRedirect = true;

                //发送请求
                if (data != null)
                {
                    stream = myRequest.GetRequestStream();
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

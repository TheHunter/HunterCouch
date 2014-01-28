using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using HunterCouch.Exceptions;
using Newtonsoft.Json.Linq;

namespace HunterCouch.Net.Impl
{
    public class CouchWebHttpRequest
        : IWebHttpRequest
    {
        private readonly HttpWebRequest customRequest;


        public CouchWebHttpRequest(string uri)
            :this(uri, 1000)
        {
            
        }


        public CouchWebHttpRequest(string uri, string etag)
            : this(uri, 1000)
        {
            
        }


        public CouchWebHttpRequest(string uri, int timeout)
        {
            try
            {
                customRequest = (HttpWebRequest)WebRequest.Create(uri);
                customRequest.Headers.Clear();
            }
            catch (Exception ex)
            {
                throw new HttpRequestException("Error on creating the given uri, value: " + uri, null, ex);
            }

            customRequest.Headers.Add("Accept-Charset", "utf-8");       //ok
            customRequest.Headers.Add("Accept-Language", "en-us");      //ok
            customRequest.Accept = "application/json";                  //ok
            customRequest.Referer = uri;                                //ok
            customRequest.ContentType = "application/json";             //ok
            customRequest.KeepAlive = true;                             //ok
            customRequest.Timeout = timeout;                            //ok
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="authCookie"></param>
        public CouchWebHttpRequest(string uri, Cookie authCookie)
            : this(uri, 1000, authCookie)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="timeout"></param>
        /// <param name="authCookie"></param>
        public CouchWebHttpRequest(string uri, int timeout, Cookie authCookie)
            : this(uri, timeout)
        {
            if (authCookie != null)
                customRequest.Headers.Add("Cookie", "AuthSession=" + authCookie.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="credential"></param>
        public CouchWebHttpRequest(string uri, IUserCredential credential)
            : this(uri, 1000, credential)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="timeout"></param>
        /// <param name="credential"></param>
        public CouchWebHttpRequest(string uri, int timeout, IUserCredential credential)
            : this(uri, timeout)
        {
            if (credential != null)
                customRequest.Headers.Add("Authorization", credential.Encode());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="etag"></param>
        /// <returns></returns>
        public IWebHttpRequest SetEtag(string etag)
        {
            if (!string.IsNullOrEmpty(etag))
                customRequest.Headers.Add("If-None-Match", etag);       //ok

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public IWebHttpRequest MethodAs(DocumentMethod method)
        {
            string val = CouchConstant.GetDocMethod(method);
            if (val == null)
                throw new CouchException("The given method is not managed, value: " + method);
            
            this.customRequest.Method = val;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public IWebHttpRequest WriteBody(Stream data)
        {
            using (var body = this.customRequest.GetRequestStream())
            {
                byte[] buffer = new byte[4096];
                int bytesRead = 0;

                while (0 != (bytesRead = data.Read(buffer, 0, buffer.Length)))
                {
                    body.Write(buffer, 0, bytesRead);
                }
            }
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public IWebHttpRequest WriteBody(string data)
        {
            using (var body = this.customRequest.GetRequestStream())
            {
                var encodedData = Encoding.UTF8.GetBytes(data);
                body.Write(encodedData, 0, encodedData.Length);
            }
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public IWebHttpRequest WriteBody(byte[] data)
        {
            using (var body = this.customRequest.GetRequestStream())
            {
                body.Write(data, 0, data.Length);
            }
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public IWebHttpRequest WriteBody(JObject data)
        {
            return this.WriteBody(data.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public IWebHttpRequest ContentTypeAs(string contentType)
        {
            this.customRequest.ContentType = contentType;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public IWebHttpRequest ContentTypeAs(ContentType contentType)
        {
            var conType = CouchConstant.GetContentType(contentType);
            if (conType == null)
                throw new CouchException("The given content type is not managed, value: " + contentType);

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeoutMs"></param>
        /// <returns></returns>
        public IWebHttpRequest SetTimeout(int timeoutMs)
        {
            this.customRequest.Timeout = timeoutMs;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        public IWebHttpRequest SetHeader(string header)
        {
            this.customRequest.Headers.Add(header);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="header"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IWebHttpRequest SetHeader(string header, string value)
        {
            if (this.customRequest.Headers.AllKeys.Contains(header, StringComparer.InvariantCulture))
                this.customRequest.Headers.Set(header, value);
            else
                this.customRequest.Headers.Add(header, value);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        public string GetHeader(string header)
        {
            return this.customRequest.Headers.Get(header);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IWebHttpResponse GetResponse()
        {
            IWebHttpResponse response;
            WebResponse resp;
            try
            {
                resp = this.customRequest.GetResponse();
                response = new CouchWebHttpResponse(resp as HttpWebResponse);
            }
            catch (WebException ex)
            {
                resp = ex.Response;
                if (resp == null)
                    throw new HttpResponseException("Request failed to receive a response", null, ex);

                response = new CouchWebHttpResponse(resp as HttpWebResponse);
            }

            if (!response.RequestAuthenticated)
                throw new HttpResponseException("Invalid username or password, verify user credential", resp);

            if (!response.RequestAuthorized)
                throw new HttpResponseException("The request is not authorized to access database", resp);

            return response;
        }

    }
}

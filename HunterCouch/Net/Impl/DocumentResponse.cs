using System;
using System.IO;
using System.Net;
using HunterCouch.Exceptions;
using Newtonsoft.Json.Linq;

namespace HunterCouch.Net.Impl
{
    /// <summary>
    /// 
    /// </summary>
    public class DocumentResponse
        : IJDocumentResponse, IDisposable
    {
        private readonly string etag;
        private readonly string response;
        private readonly JObject jDocument;
        private readonly HttpStatusCode statusCode;
        private readonly HttpWebResponse httpResponse;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpResponse"></param>
        public DocumentResponse(HttpWebResponse httpResponse)
        {
            if (httpResponse == null)
                throw new HttpResponseException("The current httpResponse cannot be null.", null);

            this.httpResponse = httpResponse;

            try
            {
                Stream stream = httpResponse.GetResponseStream();
                if (stream != null)
                {
                    using (var streamReader = new StreamReader(stream))
                    {
                        var result = streamReader.ReadToEnd();
                        this.response = string.IsNullOrWhiteSpace(result) ? null : result;
                        this.jDocument = JObject.Parse(result);
                    }
                }
                this.statusCode = httpResponse.StatusCode;
                this.etag = httpResponse.Headers["ETag"];
            }
            catch (Exception ex)
            {
                throw new HttpResponseException("Error on initializing the IDocumentResponse instance, see inner exception for details.", this.httpResponse, ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Etag
        {
            get { return etag; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Response
        {
            get { return response; }
        }

        /// <summary>
        /// 
        /// </summary>
        public JObject JDocument
        {
            get { return jDocument; }
        }

        /// <summary>
        /// 
        /// </summary>
        public HttpStatusCode StatusCode
        {
            get { return statusCode; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected HttpWebResponse HttpResponse
        {
            get { return this.httpResponse; }
        }

        /// <summary>
        /// 
        /// </summary>
        void IDisposable.Dispose()
        {
            using (this.HttpResponse)
            {
                //
            }
        }
    }
}

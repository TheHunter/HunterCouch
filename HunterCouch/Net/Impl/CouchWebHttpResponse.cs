using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Web;

namespace HunterCouch.Net.Impl
{
    /// <summary>
    /// 
    /// </summary>
    public class CouchWebHttpResponse
        : DocumentResponse, IWebHttpResponse
    {
        private readonly bool isAuthorized;
        private readonly bool isAuthenticated;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpResponse"></param>
        public CouchWebHttpResponse(HttpWebResponse httpResponse)
            : base(httpResponse)
        {
            IEnumerable<string> split = httpResponse
                .ResponseUri
                .Query
                .Split('&')
                .Select(HttpUtility.UrlDecode)
                ;

            this.isAuthorized = split.Any(n => !n.Contains("reason=Name or password is incorrect"));
            this.isAuthenticated = split.Any(n => !n.Contains("reason=You are not authorized to access this db."));
        }

        /// <summary>
        /// 
        /// </summary>
        public string Server
        {
            get { return this.HttpResponse.Server; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        public string GetHeader(string header)
        {
            return this.HttpResponse.Headers.Get(header);
        }

        /// <summary>
        /// 
        /// </summary>
        public string StatusDescription
        {
            get { return this.HttpResponse.StatusDescription; }
        }


        public bool RequestAuthorized
        {
            get { return this.isAuthorized; }
        }


        public bool RequestAuthenticated
        {
            get { return this.isAuthenticated; }
        }
    }
}

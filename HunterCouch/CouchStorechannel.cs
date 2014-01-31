using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using HunterCouch.Exceptions;
using HunterCouch.Net;
using HunterCouch.Net.Impl;

namespace HunterCouch
{
    /// <summary>
    /// 
    /// </summary>
    public class CouchStoreChannel
    {
        private readonly string uriBase;
        private readonly IUserCredential userCredential;
        private readonly AuthenticationLevel authLevel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uriBase"></param>
        /// <param name="userCredential"></param>
        /// <param name="authLevel"></param>
        internal protected CouchStoreChannel(string uriBase, IUserCredential userCredential, AuthenticationLevel authLevel)
        {
            if (string.IsNullOrWhiteSpace(uriBase))
                throw new CouchParameterException("UriBase on IJSessionConfig cannot be empty or null.", "uriBase");

            this.uriBase = uriBase;
            this.userCredential = userCredential;
            this.authLevel = authLevel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal protected Cookie GetCookie()
        {
            if (this.userCredential == null)
                return null;

            UriBuilder uri = new UriBuilder(this.uriBase) { Path = "_session" };

            IWebHttpResponse response = new CouchWebHttpRequest(uri.ToString(), 10000)
                .SetHeader("Authorization", this.userCredential.Encode())
                .MethodAs(DocumentMethod.Post)
                .ContentTypeAs(ContentType.Form)
                .WriteBody("name=" + this.userCredential.Username + "&password=" + this.userCredential.Password)
                .GetResponse()
                ;

            if (response != null)
            {
                string cookieVal = response.GetHeader("Set-Cookie");
                if (cookieVal != null)
                {
                    var parts = cookieVal.Split(';')[0].Split('=');
                    var authCookie = new Cookie(parts[0], parts[1]) { Domain = response.Server };
                    return authCookie;
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        internal protected IWebHttpRequest BuildRequest(params string[] uri)
        {
            string url = new UriBuilder(this.uriBase)
            {
                Path = uri != null ? string.Join("/", uri.Where(n => !string.IsNullOrWhiteSpace(n))) : string.Empty
            }.ToString();

            switch (this.authLevel)
            {
                case AuthenticationLevel.Basic:
                    {
                        return new CouchWebHttpRequest(url, this.userCredential);
                    }
                default:
                    {
                        return new CouchWebHttpRequest(url, this.GetCookie());
                    }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string UriBase { get { return this.uriBase; } }

        /// <summary>
        /// 
        /// </summary>
        public IUserCredential UserCredential { get { return this.userCredential; } }

        /// <summary>
        /// 
        /// </summary>
        public AuthenticationLevel AuthLevel { get { return this.authLevel; } }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace HunterCouch.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpResponseException
        : CouchException
    {
        private readonly WebResponse response;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="response"></param>
        public HttpResponseException(string message, WebResponse response) : base(message)
        {
            this.response = response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="response"></param>
        /// <param name="innerException"></param>
        public HttpResponseException(string message, WebResponse response, Exception innerException)
            : base(message, innerException)
        {
            this.response = response;
        }

        /// <summary>
        /// 
        /// </summary>
        public WebResponse Response { get { return this.response; } }
    }
}

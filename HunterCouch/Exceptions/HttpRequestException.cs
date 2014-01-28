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
    public class HttpRequestException
        : CouchException
    {
        private readonly WebRequest request;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="request"></param>
        public HttpRequestException(string message, WebRequest request) : base(message)
        {
            this.request = request;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="request"></param>
        /// <param name="innerException"></param>
        public HttpRequestException(string message, WebRequest request, Exception innerException) : base(message, innerException)
        {
            this.request = request;
        }

        /// <summary>
        /// 
        /// </summary>
        public WebRequest Request { get { return this.request; } }

    }
}

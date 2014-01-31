using System;
using System.Net;
using Newtonsoft.Json.Linq;

namespace HunterCouch.Net
{
    /// <summary>
    /// 
    /// </summary>
    public interface IJDocumentResponse
    {
        /// <summary>
        /// 
        /// </summary>
        string Etag { get; }

        /// <summary>
        /// 
        /// </summary>
        string Response { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <returns></returns>
        TResponse ResponseAs<TResponse>();

        /// <summary>
        /// 
        /// </summary>
        HttpStatusCode StatusCode { get; }
    }
}

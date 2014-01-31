using System;

namespace HunterCouch.Net
{
    /// <summary>
    /// 
    /// </summary>
    public interface IWebHttpResponse
        : IJDocumentResponse
    {
        /// <summary>
        /// 
        /// </summary>
        string Server { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        string GetHeader(string header);

        /// <summary>
        /// 
        /// </summary>
        string StatusDescription { get; }

        /// <summary>
        /// 
        /// </summary>
        bool RequestAuthorized { get; }

        /// <summary>
        /// 
        /// </summary>
        bool RequestAuthenticated { get; }
    }
}

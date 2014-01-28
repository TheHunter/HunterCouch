using System;

namespace HunterCouch.Net
{
    public interface IWebHttpResponse
        : IJDocumentResponse
    {

        string Server { get; }

        
        string GetHeader(string header);


        string StatusDescription { get; }

        
        bool RequestAuthorized { get; }


        bool RequestAuthenticated { get; }
    }
}

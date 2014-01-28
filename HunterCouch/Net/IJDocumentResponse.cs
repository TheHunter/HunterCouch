using System;
using System.Net;
using Newtonsoft.Json.Linq;

namespace HunterCouch.Net
{
    public interface IJDocumentResponse
    {
        
        string Etag { get; }

        
        string Response { get; }

        
        JObject JDocument { get; }


        HttpStatusCode StatusCode { get; }
    }
}

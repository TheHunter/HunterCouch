using System.IO;
using Newtonsoft.Json.Linq;

namespace HunterCouch.Net
{

    public interface IWebHttpRequest
    {

        IWebHttpRequest SetEtag(string etag);


        IWebHttpRequest MethodAs(DocumentMethod method);


        IWebHttpRequest WriteBody(Stream data);


        IWebHttpRequest WriteBody(string data);


        IWebHttpRequest WriteBody(byte[] data);

        
        IWebHttpRequest WriteBody(JObject data);

        
        IWebHttpRequest ContentTypeAs(string contentType);

        
        IWebHttpRequest ContentTypeAs(ContentType contentType);


        IWebHttpRequest SetTimeout(int timeoutMs);


        IWebHttpRequest SetHeader(string header);


        IWebHttpRequest SetHeader(string header, string value);


        string GetHeader(string header);


        IWebHttpResponse GetResponse();
    }
}

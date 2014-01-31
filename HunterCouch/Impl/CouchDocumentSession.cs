using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using HunterCouch.Net;
using HunterCouch.Net.Impl;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HunterCouch.Impl
{
    /// <summary>
    /// 
    /// </summary>
    public class CouchDocumentSession
        : CouchStoreChannel, IJDocumentSession
    {
        private readonly string databaseName;
        private readonly JsonSerializerSettings settings;
        private readonly DateTime createdAt;


        //public CouchDocumentSession(string databaseName, IJSessionConfig sessionConfig)
        //{
        //    this.uriBase = sessionConfig.UriBase;
        //    this.databaseName = databaseName;
        //    this.userCredential = sessionConfig.UserCredential;
        //    this.authLevel = sessionConfig.AuthLevel;
        //    this.settings = sessionConfig.SerializerSettings;
        //    this.createdAt = DateTime.Now;
        //}

        public CouchDocumentSession(string uriBase, string databaseName, IUserCredential userCredential, AuthenticationLevel authLevel, JsonSerializerSettings serializerSettings)
            : base(uriBase, userCredential, authLevel)
        {
            this.databaseName = databaseName;
            this.settings = serializerSettings;
            this.createdAt = DateTime.Now;
        }


        //protected Cookie GetCookie()
        //{
        //    if (this.UserCredential == null)
        //        return null;

        //    UriBuilder uri = new UriBuilder(this.uriBase) { Path = "_session" };

        //    IWebHttpResponse response = new CouchWebHttpRequest(uri.ToString(), 10000)
        //        .SetHeader("Authorization", this.UserCredential.Encode())
        //        .MethodAs(DocumentMethod.Post)
        //        .ContentTypeAs(ContentType.Form)
        //        .WriteBody("name=" + this.UserCredential.Username + "&password=" + this.UserCredential.Password)
        //        .GetResponse()
        //        ;

        //    if (response != null)
        //    {
        //        string cookieVal = response.GetHeader("Set-Cookie");
        //        if (cookieVal != null)
        //        {
        //            var parts = cookieVal.Split(';')[0].Split('=');
        //            var authCookie = new Cookie(parts[0], parts[1]) { Domain = response.Server };
        //            return authCookie;
        //        }
        //    }
        //    return null;
        //}

        //protected IWebHttpRequest BuildRequest(params string[] uri)
        //{
        //    string url = new UriBuilder(this.uriBase)
        //    {
        //        Path = string.Join("/", uri.Where(n => !string.IsNullOrWhiteSpace(n)))
        //    }.ToString();

        //    switch (this.AuthLevel)
        //    {
        //        case AuthenticationLevel.Basic:
        //            {
        //                return new CouchWebHttpRequest(url, this.UserCredential);
        //            }
        //        default:
        //            {
        //                return new CouchWebHttpRequest(url, this.GetCookie());
        //            }
        //    }
        //}

        public IJDocumentResponse Load<TDocument>(string id) where TDocument : class
        {
            throw new NotImplementedException();
        }

        public IJDocumentResponse Load<TDocument>(ValueType id) where TDocument : class
        {
            throw new NotImplementedException();
        }

        public IJDocumentResponse Store(string id, string jDocument)
        {
            IWebHttpRequest request = this.BuildRequest(this.databaseName, id);
            return request.MethodAs(DocumentMethod.Put)
                   .ContentTypeAs(ContentType.Form)
                   .WriteBody(jDocument)
                   .GetResponse();
        }

        public IJDocumentResponse Store(ValueType id, string jDocument)
        {
            return this.Store(id.ToString(), jDocument);
        }

        public TDocument Store<TDocument>(string id, TDocument document) where TDocument : class
        {
            string jDocument = JsonConvert.SerializeObject(document, Formatting.Indented, this.settings);
            var response = this.Store(id, jDocument);
            return JsonConvert.DeserializeObject<TDocument>(response.Response, this.settings);        // document must be deserialized from the current response...
        }

        public TDocument Store<TDocument>(ValueType id, TDocument document) where TDocument : class
        {
            return this.Store(id.ToString(), document);
        }

        public TDocument Store<TDocument>(TDocument document) where TDocument : class
        {
            string id = string.Empty;  // the id property must be founded by a helper method, using the given document to update | save.
            return this.Store(id, document);
        }

        public void Delete<TDocument>(string id) where TDocument : class
        {
            throw new NotImplementedException();
        }

        public void Delete<TDocument>(ValueType id) where TDocument : class
        {
            throw new NotImplementedException();
        }

        public void PersistChanges()
        {
            throw new NotImplementedException();
        }

        public void ClearChanges()
        {
            throw new NotImplementedException();
        }


        public string DatabaseName { get { return this.databaseName; } }
        
    }
}

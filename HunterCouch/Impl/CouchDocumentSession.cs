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
    public class CouchDocumentSession
        : IJDocumentSession
    {
        private readonly string uriBase;
        private readonly string databaseName;
        private readonly IUserCredential userCredential;
        private readonly AuthenticationLevel level;
        private readonly JsonSerializerSettings settings;
        private readonly IJSessionConfig sessionConfig;


        public CouchDocumentSession(string databaseName, IJSessionConfig sessionConfig)
        {
            this.uriBase = sessionConfig.UriBase;
            this.databaseName = databaseName;
            this.userCredential = sessionConfig.UserCredential;
            this.level = sessionConfig.AuthLevel;
            this.settings = sessionConfig.SerializerSettings;
            this.sessionConfig = sessionConfig;
        }

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
            IWebHttpRequest request = sessionConfig.BuildRequest(this.databaseName, id);
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
    }
}

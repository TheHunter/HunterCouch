using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using HunterCouch.Net;
using HunterCouch.Net.Impl;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HunterCouch.Impl
{
    public class CouchSessionFactory
        : IJSessionFactory, IJSessionConfig
    {
        private readonly string uriBase;
        private readonly IUserCredential userCredential;
        private readonly AuthenticationLevel authLevel;
        private readonly JsonSerializerSettings serializerSettings;


        public CouchSessionFactory(string uriBase, IUserCredential userCredential, AuthenticationLevel authLevel)
        {
            this.uriBase = uriBase;
            this.userCredential = userCredential;
            this.authLevel = authLevel;

            serializerSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    TypeNameHandling = TypeNameHandling.None,
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
                };
            
            //
            ////if (serializerInfo.EnablePolymorphicMembers)
            ////{
            ////    serializer.Binder = new OperationTypeBinder(this.ConfigRegister);
            ////    serializer.TypeNameHandling = TypeNameHandling.Objects;
            ////}
        }


        public IJDocumentSession OpenSession(string databaseName)
        {
            return new CouchDocumentSession(databaseName, this);
        }


        public IJDocumentSession OpenSession(string databaseName, IUserCredential credential)
        {
            return new CouchDocumentSession(databaseName, this);
        }


        public IJDocumentSession OpenSession(string databaseName, IUserCredential credential, AuthenticationLevel level)
        {
            return new CouchDocumentSession(databaseName, this);
        }

        public IJDocumentResponse CreateDataBase(string databaseName)
        {
            //return this.BuildRequest(null, databaseName)
            //           .MethodAs(DocumentMethod.Put)
            //           .GetResponse();
            
            throw new NotImplementedException();
        }

        public IJDocumentResponse EcoCouch()
        {
            //return this.BuildRequest(null)
            //           .MethodAs(DocumentMethod.Get)
            //           .GetResponse();

            throw new NotImplementedException();
        }
        
        public string UriBase { get { return this.uriBase; } }


        public IUserCredential UserCredential { get { return this.userCredential; } }


        public AuthenticationLevel AuthLevel { get { return this.authLevel; } }


        public Func<PropertyInfo, bool> IdentityResolver { get; set; }


        public JsonSerializerSettings SerializerSettings { get { return this.serializerSettings; } }


        protected Cookie GetCookie()
        {
            if (this.userCredential == null)
                return null;

            UriBuilder uri = new UriBuilder(this.uriBase) {Path = "_session"};

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


        public IWebHttpRequest BuildRequest(params string[] uri)
        {
            string url = new UriBuilder(this.uriBase)
            {
                Path = string.Join("/", uri.Where(n => !string.IsNullOrWhiteSpace(n)))
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

    }
}

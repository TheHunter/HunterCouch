using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using HunterCouch.Exceptions;
using HunterCouch.Net;
using HunterCouch.Net.Impl;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HunterCouch.Impl
{
    public class CouchSessionFactory
        : CouchStoreChannel, IJSessionFactory, IAdvancedStoreManager, IDisposable
    {
        private readonly JsonSerializerSettings serializerSettings;
        private readonly HashSet<IJDocumentSession> sessionUsers; 


        public CouchSessionFactory(string uriBase, IUserCredential userCredential, AuthenticationLevel authLevel)
            : base(uriBase, userCredential, authLevel)
        {
            if (string.IsNullOrWhiteSpace(uriBase))
                throw new CouchParameterException("UriBase on IJSessionConfig cannot be empty or null.", "uriBase");

            if (userCredential == null)
                throw new CouchParameterException("userCredential on IJSessionConfig cannot be null.", "userCredential");

            try
            {
                this.EchoCouch();
            }
            catch (Exception ex)
            {
                throw new CouchException(ex.Message, ex);
            }

            serializerSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    TypeNameHandling = TypeNameHandling.None,
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
                };

            this.sessionUsers = new HashSet<IJDocumentSession>();
        }


        public IJDocumentSession OpenSession(string databaseName)
        {
            return this.GetSession(databaseName, this.UserCredential, this.AuthLevel);
        }


        public IJDocumentSession OpenSession(string databaseName, IUserCredential credential)
        {
            return this.GetSession(databaseName, credential, this.AuthLevel);
        }


        public IJDocumentSession OpenSession(string databaseName, IUserCredential credential, AuthenticationLevel level)
        {
            return this.GetSession(databaseName, credential, level);
        }


        private IJDocumentSession GetSession(string databaseName, IUserCredential credential, AuthenticationLevel level)
        {
            IJDocumentSession session = this.sessionUsers.FirstOrDefault(
                advSession =>
                advSession.AuthLevel == level && advSession.DatabaseName.Equals(databaseName) &&
                advSession.UserCredential.Equals(credential));

            if (session == null)
            {
                session = new CouchDocumentSession(this.UriBase, databaseName, credential, level,
                                                   this.SerializerSettings);
                this.sessionUsers.Add(session);
            }

            return session;
        }


        public Func<PropertyInfo, bool> IdentityResolver { get; set; }


        public JsonSerializerSettings SerializerSettings { get { return this.serializerSettings; } }


        void IDisposable.Dispose()
        {
            this.sessionUsers.Clear();
        }

        public IJDocumentResponse EchoCouch()
        {
            return this.BuildRequest(null)
                       .MethodAs(DocumentMethod.Get)
                       .GetResponse();
        }

        public IJDocumentResponse CreateDataBase(string dbName)
        {
            return this.BuildRequest(dbName)
                       .MethodAs(DocumentMethod.Put)
                       .GetResponse();
        }

        public IJDocumentResponse DeleteDataBase(string dbName)
        {
            return this.BuildRequest(dbName)
                       .MethodAs(DocumentMethod.Delete)
                       .GetResponse();
        }

        public IJDocumentResponse ExistsDataBase(string dbName)
        {
            return this.BuildRequest(dbName)
                       .MethodAs(DocumentMethod.Get)
                       .GetResponse();
        }

        public IJDocumentResponse CreateAdminUser(IUserCredential credential)
        {
            throw new NotImplementedException();
        }

        public IJDocumentResponse DeleteAdminUser(IUserCredential credential)
        {
            throw new NotImplementedException();
        }

        public IJDocumentResponse GenerateUUID(int count)
        {
            if (count < 0)
                throw new CouchParameterException("The count of UUID to generate cannot be less or equals than zero", "count");

            if (count > 50)
                count = 50;

            return this.BuildRequest("_uuids", "?Count=" + count)
                       .MethodAs(DocumentMethod.Get)
                       .ContentTypeAs(ContentType.Json)
                       .GetResponse();
        }
    }
}

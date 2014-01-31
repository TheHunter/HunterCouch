using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using HunterCouch.Exceptions;
using HunterCouch.Impl;
using HunterCouch.Net;
using HunterCouch.Test.Pocos;
using NUnit.Framework;

namespace HunterCouch.Test
{
    [TestFixture]
    public class Tester
    {
        [Test]
        public void EchoDatabaseTest()
        {
            const string uriBase = "http://localhost:5984/";
            IUserCredential crd = new UserCredential("Professor", "Farnsworth");

            var sessionFactory = new CouchSessionFactory(uriBase, crd, AuthenticationLevel.Cookie);
            var response = sessionFactory.EchoCouch();

            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);

        }

        [Test]
        [ExpectedException(typeof(CouchException))]
        public void FailedEchoDatabaseTest()
        {
            const string uriBase = "http://localhost:5900/";
            IUserCredential crd = new UserCredential("Professor", "Farnsworth");

            new CouchSessionFactory(uriBase, crd, AuthenticationLevel.Cookie);
        }

        [Test]
        public void CreateDatabaseTest()
        {
            const string uriBase = "http://localhost:5984/";
            IUserCredential crd = new UserCredential("Professor", "Farnsworth");
            
            var sessionFactory = new CouchSessionFactory(uriBase, crd, AuthenticationLevel.Cookie);
            
            var response1 = sessionFactory.CreateDataBase("ciccio");
            Assert.IsTrue(response1.StatusCode == HttpStatusCode.Created);

            var response2 = sessionFactory.DeleteDataBase("ciccio");
            Assert.IsTrue(response2.StatusCode == HttpStatusCode.OK);
        }

        [Test]
        public void FailedCreateDatabaseTest()
        {
            const string uriBase = "http://localhost:5984/";
            IUserCredential crd = new UserCredential("Professor", "Farnsworth");

            var sessionFactory = new CouchSessionFactory(uriBase, crd, AuthenticationLevel.Cookie);

            var response = sessionFactory.CreateDataBase("_ciccio");
            var rr = response.ResponseAs<HttpErrorResponse>();

            Assert.IsNotNull(rr);
            Assert.IsTrue(rr.Reason.StartsWith("Name:"));
            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);

        }

        [Test]
        public void FailedDeleteDatabaseTest()
        {
            const string uriBase = "http://localhost:5984/";
            IUserCredential crd = new UserCredential("Professor", "Farnsworth");

            var sessionFactory = new CouchSessionFactory(uriBase, crd, AuthenticationLevel.Cookie);
            var response = sessionFactory.DeleteDataBase("nodb");

            var rr = response.ResponseAs<HttpErrorResponse>();

            Assert.IsNotNull(rr);
            Assert.AreEqual(rr.Reason, "missing");
            Assert.IsNotNull(response.StatusCode == HttpStatusCode.NotFound);

        }


        [Test]
        public void ExistsDatabaseTest()
        {
            const string uriBase = "http://localhost:5984/";
            IUserCredential crd = new UserCredential("Professor", "Farnsworth");

            var sessionFactory = new CouchSessionFactory(uriBase, crd, AuthenticationLevel.Cookie);

            var response1 = sessionFactory.ExistsDataBase("db");
            var rr = response1.ResponseAs<HttpErrorResponse>();

            Assert.IsNotNull(rr);
            Assert.IsTrue(rr.Reason.StartsWith("no_db_file"));
            Assert.IsTrue(response1.StatusCode == HttpStatusCode.NotFound);


            var response2 = sessionFactory.ExistsDataBase("_users");
            Assert.IsTrue(response2.StatusCode == HttpStatusCode.OK);
        }
    }
}

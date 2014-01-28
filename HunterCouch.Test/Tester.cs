using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public void Test1()
        {
            //string uriBase = "http://127.0.0.1:5984";
            const string uriBase = "http://localhost:5984/";
            IUserCredential crd = new UserCredential("Professor", "Farnsworth");
            
            var sessionFactory = new CouchSessionFactory(uriBase, crd, AuthenticationLevel.Cookie);
            var response = sessionFactory.CreateDataBase("ciccio");

            //var sessionFactory = new CoachSessionFactory(uriBase, null, AuthenticationLevel.Basic);
            //var response = sessionFactory.EcoCouch();

            Assert.IsNotNull(response);

        }

        public void TestCreateDb()
        {
            string databaseName = "love-seat-test-base";
            string uriBase = "http://localhost:5984/";

            string username = "Professor";
            string password = "Farnsworth";
            AuthenticationLevel level = AuthenticationLevel.Cookie;



        }


        [Test]
        public void Test2()
        {
            string uriBase = "http://localhost:5984/";
            IUserCredential crd = new UserCredential("Professor", "Farnsworth");

            var sessionFactory = new CouchSessionFactory(uriBase, crd, AuthenticationLevel.Basic);
            var session = sessionFactory.OpenSession("salesarea");

            Person p = new Person{ Name = "name", Surname = "surname"};
            var res = session.Store("1", p);

            Assert.IsNotNull(res);
        }

    }
}

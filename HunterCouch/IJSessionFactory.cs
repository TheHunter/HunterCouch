using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using HunterCouch.Net;
using Newtonsoft.Json;

namespace HunterCouch
{
    /// <summary>
    /// 
    /// </summary>
    public interface IJSessionFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userCredential"></param>
        /// <param name="databaseName"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        IJDocumentSession OpenSession(string databaseName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseName"></param>
        /// <param name="credential"></param>
        /// <returns></returns>
        IJDocumentSession OpenSession(string databaseName, IUserCredential credential);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseName"></param>
        /// <param name="credential"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        IJDocumentSession OpenSession(string databaseName, IUserCredential credential, AuthenticationLevel level);

    }
}

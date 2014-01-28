using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using HunterCouch.Net;
using Newtonsoft.Json;

namespace HunterCouch
{
    /// <summary>
    /// 
    /// </summary>
    public interface IJSessionConfig
    {
        string UriBase { get; }
        /// <summary>
        /// 
        /// </summary>
        IUserCredential UserCredential { get; }

        /// <summary>
        /// 
        /// </summary>
        AuthenticationLevel AuthLevel { get; }

        /// <summary>
        /// 
        /// </summary>
        Func<PropertyInfo, bool> IdentityResolver { get; }

        /// <summary>
        /// 
        /// </summary>
        JsonSerializerSettings SerializerSettings { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        IWebHttpRequest BuildRequest(params string[] uri);
    }
}

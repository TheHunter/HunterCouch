using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HunterCouch.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class CouchException
        : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public CouchException(string message)
            : base(message)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public CouchException(string message, Exception innerException)
            : base(message, innerException)
        {
            
        }
    }
}

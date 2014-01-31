using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HunterCouch.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class CouchParameterException
        : CouchException
    {

        private readonly string parameterNane;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="parameterNane"></param>
        public CouchParameterException(string message, string parameterNane)
            : base(message)
        {
            this.parameterNane = parameterNane;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="parameterNane"></param>
        /// <param name="innerException"></param>
        public CouchParameterException(string message, string parameterNane, Exception innerException)
            : base(message, innerException)
        {
            this.parameterNane = parameterNane;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ParameterNane
        {
            get { return this.parameterNane; }
        }
    }
}

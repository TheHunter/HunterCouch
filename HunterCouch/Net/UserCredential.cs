using System;
using System.Text;

namespace HunterCouch.Net
{
    /// <summary>
    /// 
    /// </summary>
    public class UserCredential
        : IUserCredential
    {
        private readonly string username;
        private readonly string password;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public UserCredential(string username, string password)
        {
            username = username != null ? username.Trim() : null;
            password = password != null ? password.Trim() : null;

            if (string.IsNullOrEmpty(username))
                throw new ArgumentException("The username cannot be empty or null.", "username");

            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("The username cannot be empty or null.", "username");

            this.username = username;
            this.password = password;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Username { get { return this.username; } }

        /// <summary>
        /// 
        /// </summary>
        public string Password { get { return this.password; } }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Encode()
        {
            return this.Encode("Basic");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authValue"></param>
        /// <returns></returns>
        public string Encode(string authValue)
        {
            const string strPath = "{0} {1}";
            string strKey = string.Format("{0}:{1}", username, password);

            authValue = authValue != null ? authValue.Trim() : null;

            if (string.IsNullOrEmpty(authValue))
                throw new ArgumentException("The username cannot be empty or null.", "authValue");

            return string.Format(strPath, authValue, Convert.ToBase64String(Encoding.ASCII.GetBytes(strKey)));
        }


        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is UserCredential)
                return this.GetHashCode() == obj.GetHashCode();

            return false;
        }


        public override int GetHashCode()
        {
            return 7 * (this.Username.GetHashCode() - this.Password.GetHashCode());
        }


        public override string ToString()
        {
            return string.Format("Username: {0}, Password: {1}", this.Username, this.Password);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IUserCredential
    {
        /// <summary>
        /// 
        /// </summary>
        string Username { get; }

        /// <summary>
        /// 
        /// </summary>
        string Password { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string Encode();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authValue"></param>
        /// <returns></returns>
        string Encode(string authValue);
    }
}

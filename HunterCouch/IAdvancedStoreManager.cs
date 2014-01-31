using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HunterCouch.Net;

namespace HunterCouch
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAdvancedStoreManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IJDocumentResponse EchoCouch();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        IJDocumentResponse CreateDataBase(string dbName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        IJDocumentResponse DeleteDataBase(string dbName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        IJDocumentResponse ExistsDataBase(string dbName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="credential"></param>
        /// <returns></returns>
        IJDocumentResponse CreateAdminUser(IUserCredential credential);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="credential"></param>
        /// <returns></returns>
        IJDocumentResponse DeleteAdminUser(IUserCredential credential);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        IJDocumentResponse GenerateUUID(int count);

    }
}

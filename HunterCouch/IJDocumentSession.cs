using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HunterCouch.Net;

namespace HunterCouch
{
    public interface IJDocumentSession
    {
        /// <summary>
        /// 
        /// </summary>
        string DatabaseName { get; }

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
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        IJDocumentResponse Load<TDocument>(string id)
            where TDocument : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        IJDocumentResponse Load<TDocument>(ValueType id)
            where TDocument : class;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jDocument"></param>
        /// <returns></returns>
        IJDocumentResponse Store(string id, string jDocument);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jDocument"></param>
        /// <returns></returns>
        IJDocumentResponse Store(ValueType id, string jDocument);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="id"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        TDocument Store<TDocument>(string id, TDocument document)
            where TDocument : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="id"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        TDocument Store<TDocument>(ValueType id, TDocument document)
            where TDocument : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="document"></param>
        /// <returns></returns>
        TDocument Store<TDocument>(TDocument document)
            where TDocument : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="id"></param>
        void Delete<TDocument>(string id)
            where TDocument : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="id"></param>
        void Delete<TDocument>(ValueType id)
            where TDocument : class;

        /// <summary>
        /// 
        /// </summary>
        void PersistChanges();

        /// <summary>
        /// 
        /// </summary>
        void ClearChanges();

        // mettere in batch le istanze da inviare via HTTP
        // ad ex. quando viene utilizzata TransactionScope.... 
    }
}

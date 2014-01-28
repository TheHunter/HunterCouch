using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HunterCouch.Net;

namespace HunterCouch
{
    public interface IJDocumentSession
    {

        IJDocumentResponse Load<TDocument>(string id)
            where TDocument : class;


        IJDocumentResponse Load<TDocument>(ValueType id)
            where TDocument : class;


        IJDocumentResponse Store(string id, string jDocument);


        IJDocumentResponse Store(ValueType id, string jDocument);


        TDocument Store<TDocument>(string id, TDocument document)
            where TDocument : class;


        TDocument Store<TDocument>(ValueType id, TDocument document)
            where TDocument : class;


        TDocument Store<TDocument>(TDocument document)
            where TDocument : class;


        void Delete<TDocument>(string id)
            where TDocument : class;


        void Delete<TDocument>(ValueType id)
            where TDocument : class;


        void PersistChanges();


        void ClearChanges();

        // mettere in batch le istanze da inviare via HTTP
        // ad ex. quando viene utilizzata TransactionScope.... 
    }
}

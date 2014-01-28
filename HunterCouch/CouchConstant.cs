using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HunterCouch.Net;

namespace HunterCouch
{
    internal static class CouchConstant
    {
        private static readonly Dictionary<DocumentMethod, string> DocMethods;
        private static readonly Dictionary<ContentType, string> ContentTypes;


        static CouchConstant()
        {
            DocMethods = new Dictionary<DocumentMethod, string>
                {
                    {DocumentMethod.Copy, "COPY"},
                    {DocumentMethod.Delete, "DELETE"},
                    {DocumentMethod.Get, "GET"},
                    {DocumentMethod.Head, "HEAD"},
                    {DocumentMethod.Post, "POST"},
                    {DocumentMethod.Put, "PUT"}
                };


            ContentTypes = new Dictionary<ContentType, string>
                {
                    {ContentType.Form, "application/x-www-form-urlencoded"},
                    {ContentType.Json, "application/json"}
                };
        }


        public static string GetDocMethod(DocumentMethod method)
        {
            if (DocMethods.ContainsKey(method))
                return DocMethods[method];

            return null;
        }

        public static string GetContentType(ContentType contentType)
        {
            if (ContentTypes.ContainsKey(contentType))
                return ContentTypes[contentType];

            return null;
        }
    }
}

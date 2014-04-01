using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace HunterCouch.Linq.Impl
{
    public class CouchQueryProvider
        : ICouchQueryProvider
    {

        IQueryable IQueryProvider.CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
        }

        IQueryable<TElement> IQueryProvider.CreateQuery<TElement>(Expression expression)
        {
            throw new NotImplementedException();
        }

        object IQueryProvider.Execute(Expression expression)
        {
            throw new NotImplementedException();
        }

        TResult IQueryProvider.Execute<TResult>(Expression expression)
        {
            throw new NotImplementedException();
        }
    }
}

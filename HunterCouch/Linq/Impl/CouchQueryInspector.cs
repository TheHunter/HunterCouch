using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace HunterCouch.Linq.Impl
{
    public class CouchQueryInspector<T>
        : ICouchQueryInspector<T>
    {
        private Expression expression;
        private Type elementType;
        private IQueryProvider provider;

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        Expression IQueryable.Expression
        {
            get { return expression; }
        }

        Type IQueryable.ElementType
        {
            get { return elementType; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return provider; }
        }
    }
}

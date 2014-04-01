using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HunterCouch.Linq
{
    public interface ICouchQueryInspector<out T>
        : IOrderedQueryable<T>
    {

    }
}

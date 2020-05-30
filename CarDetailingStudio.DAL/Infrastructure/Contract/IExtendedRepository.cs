using System;
using System.Collections.Generic;

namespace CarDetailingStudio.DAL.Infrastructure.Contract
{
    public interface IExtendedRepository<T> where T : class
    {
        IEnumerable<T> GetWhere(Func<T, bool> predicate);
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.DAL.Infrastructure.Contract
{
    public interface IMultipleInclude<T> where T : class
    {
        //IEnumerable<T> MultipleInclude(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> MultipleInclude(Expression<Func<T, bool>> filter, List<string> includeProperties);
    }
}


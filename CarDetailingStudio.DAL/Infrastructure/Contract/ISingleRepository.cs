﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.DAL.Infrastructure.Contract
{
    public interface ISingleRepository : IExtendedRepository<OrderServicesCarWash>
    {
        IEnumerable<OrderServicesCarWash> QueryObjectGraph(Expression<Func<OrderServicesCarWash, bool>> filter);
    }
}
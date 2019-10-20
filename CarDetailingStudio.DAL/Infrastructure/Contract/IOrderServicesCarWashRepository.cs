using CarDetailingStudio.DAL.Infrastructure.Contract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CarDetailingStudio.DAL.Infrastructure.Contract
{
    public interface IOrderServicesCarWashRepository : IGetRepository<OrderServicesCarWash>, ISingleRepository<OrderServicesCarWash>
    {
       
    }
}
using CarDetailingStudio.DAL.Infrastructure.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.DAL.Infrastructure
{
    public class OrderServicesCarWashRepository : IOrderServicesCarWashRepository
    {
        internal carWashEntities _context;

        public OrderServicesCarWashRepository(carWashEntities entities)
        {
            _context = entities;
        }

        public IEnumerable<OrderServicesCarWash> Get()
        {
            throw new NotImplementedException();
        }

        public OrderServicesCarWash GetById(int? id)
        {
            var result = _context.OrderServicesCarWash.Include("ClientsOfCarWash")
                                                       .Include("StatusOrder1")
                                                       .Include("ServisesCarWashOrder")
                                                       .Include("ClientsOfCarWash.car_model")
                                                       .Include("ClientsOfCarWash.car_mark")
                                                       .Include("ClientsOfCarWash.CarBody")
                                                       .Include("OrderCarWashWorkers.CarWashWorkers")
                                                       .FirstOrDefault(x => x.Id == id);
            return result;
        }

        public IEnumerable<OrderServicesCarWash> GetWhere(Func<OrderServicesCarWash, bool> predicate)
        {
            var result = _context.OrderServicesCarWash.Include("ClientsOfCarWash")
                                                       .Include("StatusOrder1")
                                                       .Include("ServisesCarWashOrder")
                                                       .Include("ClientsOfCarWash.car_model")
                                                       .Include("ClientsOfCarWash.car_mark")
                                                       .Include("ClientsOfCarWash.CarBody")
                                                       .Where(predicate);
            return result;
        }

        public IEnumerable<OrderServicesCarWash> QueryObjectGraph(Expression<Func<OrderServicesCarWash, bool>> filter)
        {
            var result = _context.OrderServicesCarWash.Include("OrderCarWashWorkers")
                                                      .Include("OrderCarWashWorkers.CarWashWorkers")
                                                      .Include("OrderCarWashWorkers.CarWashWorkers.JobTitleTable")
                                                       .Where(filter);
            return result;
        }



    }
}

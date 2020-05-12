using CarDetailingStudio.DAL.Infrastructure.Contract;
using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
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
            var result = _context.OrderServicesCarWash
                                                      .AsNoTracking()
                                                      .Include("ClientsOfCarWash")
                                                      .Include("StatusOrder1")
                                                      .Include("PaymentState1")
                                                      .Include("ServisesCarWashOrder")
                                                      .Include("ClientsOfCarWash.CarBody")
                                                      .Include("ClientsOfCarWash.ClientInfo")
                                                      .Include("ClientsOfCarWash.car_mark")
                                                      .Include("ClientsOfCarWash.car_model")
                                                      .Include("ClientsOfCarWash.CarBody")
                                                      .Include("ClientsOfCarWash.ClientsGroups");
            return result;
        }

        public OrderServicesCarWash GetById(int? id)
        {
            var result = _context.OrderServicesCarWash
                                                       .AsNoTracking()
                                                       .Include("ClientsOfCarWash")
                                                       .Include("ClientsOfCarWash.ClientInfo")
                                                       .Include("StatusOrder1")
                                                       .Include("PaymentState1")
                                                       .Include("ServisesCarWashOrder")
                                                       .Include("OrderCarWashWorkers.CarWashWorkers")
                                                       .Include("ClientsOfCarWash.ClientInfo")
                                                       .Include("ClientsOfCarWash.car_mark")
                                                       .Include("ClientsOfCarWash.car_model")
                                                       .Include("ClientsOfCarWash.CarBody")
                                                       .Include("ClientsOfCarWash.ClientsGroups")
                                                       .FirstOrDefault(x => x.Id == id);
            return result;
        }

        public IEnumerable<OrderServicesCarWash> GetWhere(Func<OrderServicesCarWash, bool> predicate)
        {
            var result = _context.OrderServicesCarWash
                                                      .AsNoTracking()
                                                      .Include("ClientsOfCarWash")
                                                      .Include("StatusOrder1")
                                                      .Include("PaymentState1")
                                                      .Include("ServisesCarWashOrder")
                                                      .Include("ClientsOfCarWash.ClientInfo")
                                                      .Include("ClientsOfCarWash.car_mark")
                                                      .Include("ClientsOfCarWash.car_model")
                                                      .Include("ClientsOfCarWash.CarBody")
                                                      .Include("ClientsOfCarWash.ClientsGroups")
                                                      .Where(predicate).AsQueryable();
            return result;
        }

        public IEnumerable<OrderServicesCarWash> GetWhereDate(Func<OrderServicesCarWash, bool> predicate)
        {
            var result = _context.OrderServicesCarWash.Include("ClientsOfCarWash")
                                                      .Include("StatusOrder1")
                                                      .Include("PaymentState1")
                                                      .Where(predicate).AsQueryable();
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

        public IEnumerable<OrderServicesCarWash> Test()
        {
            var result = _context.OrderServicesCarWash.Include("OrderCarWashWorkers")
                                                     .Include("OrderCarWashWorkers.CarWashWorkers")
                                                     .Include("OrderCarWashWorkers.CarWashWorkers.JobTitleTable")
                                                     .Where(x => SqlMethods.Like(x.ClientsOfCarWash.NumberCar, x.ClientsOfCarWash.car_mark.name)).OrderBy(p => p.OrderDate);
            return result;
        }
    }
}

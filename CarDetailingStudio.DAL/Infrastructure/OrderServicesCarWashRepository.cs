using CarDetailingStudio.DAL.Infrastructure.Contract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<IEnumerable<OrderServicesCarWash>> Get()
        {
            var result = await _context.OrderServicesCarWash
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
                                                      .Include("ClientsOfCarWash.ClientsGroups")
                                                      .ToListAsync();
            return result;
        }

        public async Task<OrderServicesCarWash> GetById(int? id)
        {
            var result = await _context.OrderServicesCarWash
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
                                                       .FirstAsync(x => x.Id == id);
            return result;
        }

        public async Task<IEnumerable<OrderServicesCarWash>> GetWhere(Expression<Func<OrderServicesCarWash, bool>> predicate)
        {
            var result = await _context.OrderServicesCarWash
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
                                                      .Where(predicate).AsQueryable().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<OrderServicesCarWash>> WhereMonthlyReport(Expression<Func<OrderServicesCarWash, bool>> predicate)
        {
            var result = await _context.OrderServicesCarWash
                                                      .AsNoTracking()
                                                      .Include("ClientsOfCarWash")
                                                      .Include("ServisesCarWashOrder")
                                                      .Include("ServisesCarWashOrder.Detailings")
                                                      .Include("OrderCarWashWorkers")
                                                      .Include("ClientsOfCarWash.ClientInfo")
                                                      .Include("OrderCarWashWorkers.CarWashWorkers")
                                                      .Where(predicate).AsQueryable().ToListAsync();
            return result;
        }


        public async Task<IEnumerable<OrderServicesCarWash>> GetWhereDate(Expression<Func<OrderServicesCarWash, bool>> predicate)
        {
            var result = await _context.OrderServicesCarWash.Include("ClientsOfCarWash")
                                                      .Include("StatusOrder1")
                                                      .Include("PaymentState1")
                                                      .Where(predicate).AsQueryable().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<OrderServicesCarWash>> QueryObjectGraph(Expression<Func<OrderServicesCarWash, bool>> filter)
        {
            var result = await _context.OrderServicesCarWash.AsNoTracking()
                                                      .Include("OrderCarWashWorkers")
                                                      .Include("OrderCarWashWorkers.CarWashWorkers")
                                                      .Include("OrderCarWashWorkers.CarWashWorkers.JobTitleTable")
                                                      .Where(filter).AsQueryable().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<OrderServicesCarWash>> Test()
        {
            var result = await _context.OrderServicesCarWash.AsNoTracking()
                                                         .Include("OrderCarWashWorkers")
                                                         .Include("OrderCarWashWorkers.CarWashWorkers")
                                                         .Include("OrderCarWashWorkers.CarWashWorkers.JobTitleTable")
                                                         .Where(x => SqlMethods.Like(x.ClientsOfCarWash.NumberCar, x.ClientsOfCarWash.car_mark.name))
                                                         .OrderBy(p => p.OrderDate).AsQueryable().ToListAsync();
            return result;
        }
    }
}

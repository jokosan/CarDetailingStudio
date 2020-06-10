using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CarDetailingStudio.DAL.Infrastructure.Contract
{
    public class ServisesCarWashOrderRepository : IServisesCarWashOrderRepository
    {
        internal carWashEntities _context;

        public ServisesCarWashOrderRepository(carWashEntities entities)
        {
            _context = entities;
        }

        public async Task<IEnumerable<ServisesCarWashOrder>> Get()
        {
            var GetAllResult = await _context.ServisesCarWashOrder.AsNoTracking()
                                                            .Include("Detailings")
                                                            .Include("Detailings.GroupWashServices")
                                                            .Include("OrderServicesCarWash")
                                                            .Include("OrderServicesCarWash.ClientsOfCarWash")
                                                            .ToListAsync();
            return GetAllResult;
        }

        public async Task<ServisesCarWashOrder> GetById(int? id)
        {
            var GetIdResult = await _context.ServisesCarWashOrder.AsNoTracking()
                                                           .Include("Detailings")
                                                           .Include("Detailings.GroupWashServices")
                                                           .Include("OrderServicesCarWash")
                                                           .Include("OrderServicesCarWash.ClientsOfCarWash")
                                                           .FirstAsync(x => x.Id == id);
            return GetIdResult;
        }

        public async Task<IEnumerable<ServisesCarWashOrder>> GetWhere(Expression<Func<ServisesCarWashOrder, bool>> predicate)
        {
            var GetWhereResult = await _context.ServisesCarWashOrder.AsNoTracking()
                                                               .Include("Detailings")
                                                               .Include("Detailings.GroupWashServices")
                                                               .Include("OrderServicesCarWash")
                                                               //.Include("OrderServicesCarWash.ClientsOfCarWash")
                                                               .Where(predicate).AsQueryable().ToListAsync();
      
            return GetWhereResult;
        }

    }
}

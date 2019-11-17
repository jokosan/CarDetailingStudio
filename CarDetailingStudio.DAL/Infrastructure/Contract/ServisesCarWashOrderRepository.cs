using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public IEnumerable<ServisesCarWashOrder> Get()
        {
            var GetAllResult = _context.ServisesCarWashOrder.Include("Detailings")
                                                            .Include("Detailings.GroupWashServices")
                                                            .Include("OrderServicesCarWash")
                                                            .Include("OrderServicesCarWash.ClientsOfCarWash");

            return GetAllResult;
        }

        public ServisesCarWashOrder GetById(int? id)
        {
            var GetIdResult = _context.ServisesCarWashOrder.Include("Detailings")
                                                           .Include("Detailings.GroupWashServices")
                                                           .Include("OrderServicesCarWash")
                                                           .Include("OrderServicesCarWash.ClientsOfCarWash")
                                                           .FirstOrDefault(x => x.Id == id);
            return GetIdResult;
        }

        public IEnumerable<ServisesCarWashOrder> GetWhere(Func<ServisesCarWashOrder, bool> predicate)
        {
            var GetWhereResult = _context.ServisesCarWashOrder.Include("Detailings")
                                                               .Include("Detailings.GroupWashServices")
                                                               .Include("OrderServicesCarWash")
                                                               .Include("OrderServicesCarWash.ClientsOfCarWash")
                                                               .Where(predicate);
            return GetWhereResult;
        }
    }
}

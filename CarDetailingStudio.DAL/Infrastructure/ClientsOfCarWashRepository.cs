using CarDetailingStudio.DAL.Infrastructure.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.DAL.Infrastructure
{
    public class ClientsOfCarWashRepository : IGetRepository<ClientsOfCarWash>
    {
        internal carWashEntities _context;

        public ClientsOfCarWashRepository(carWashEntities entities)
        {
            _context = entities;
        }

        public IEnumerable<ClientsOfCarWash> Get()
        {
            var GetResultAll = _context.ClientsOfCarWash.Include("CarBody")
                                                        .Include("ClientsGroups")
                                                        .Include("car_model")
                                                        .Include("car_mark");
            return GetResultAll;
        }

        public ClientsOfCarWash GetById(int? id)
        {
            var GetId = _context.ClientsOfCarWash.Include("CarBody")
                                                 .Include("ClientsGroups")
                                                 .Include("car_model")
                                                 .Include("car_mark").FirstOrDefault(x => x.ib == id);
            return GetId;

        }
    }
}

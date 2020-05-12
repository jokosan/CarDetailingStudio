using CarDetailingStudio.DAL.Infrastructure.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.DAL.Infrastructure
{
    public class ClientsOfCarWashRepository : IGetRepository<ClientsOfCarWash>, IExtendedRepository<ClientsOfCarWash>
    {
        internal carWashEntities _context;

        public ClientsOfCarWashRepository(carWashEntities entities)
        {
            _context = entities;
        }

        public IEnumerable<ClientsOfCarWash> Get()
        {
            var GetResultAll = _context.ClientsOfCarWash.Include("ClientInfo")
                                                        .Include("ClientsGroups")
                                                        .Include("car_mark")
                                                        .Include("car_model")
                                                        .Include("CarBody");
            return GetResultAll;
        }

        public ClientsOfCarWash GetById(int? id)
        {
            var GetId = _context.ClientsOfCarWash.Include("ClientInfo")
                                                 .Include("ClientsGroups")
                                                 .Include("car_mark")
                                                 .Include("car_model")
                                                 .Include("CarBody").FirstOrDefault(x => x.id == id);
            return GetId;
        }

        public IEnumerable<ClientsOfCarWash> GetId(int? id)
        {
            var GetId = _context.ClientsOfCarWash.Include("ClientInfo")
                                                     .Include("ClientsGroups")
                                                     .Include("car_mark")
                                                     .Include("car_model")
                                                     .Include("CarBody").Where(x => x.IdInfoClient == id);
            return GetId;
        }

        public IEnumerable<ClientsOfCarWash> GetWhere(Func<ClientsOfCarWash, bool> predicate)
        {
            var GetId = _context.ClientsOfCarWash.Include("ClientInfo")
                                                .Include("ClientsGroups")
                                                .Include("car_mark")
                                                .Include("car_model")
                                                .Include("CarBody").Where(predicate);
            return GetId;
        }
    }
}

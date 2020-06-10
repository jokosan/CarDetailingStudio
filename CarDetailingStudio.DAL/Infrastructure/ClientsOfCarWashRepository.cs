using CarDetailingStudio.DAL.Infrastructure.Contract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<IEnumerable<ClientsOfCarWash>> Get()
        {
            var GetResultAll = await _context.ClientsOfCarWash.AsNoTracking()
                                                        .Include("ClientInfo")
                                                        .Include("ClientsGroups")
                                                        .Include("car_mark")
                                                        .Include("car_model")
                                                        .Include("CarBody")
                                                        .ToListAsync();
            return GetResultAll;
        }

        public async Task<ClientsOfCarWash> GetById(int? id)
        {
            var GetId = await _context.ClientsOfCarWash.AsNoTracking()
                                                 .Include("ClientInfo")
                                                 .Include("ClientsGroups")
                                                 .Include("car_mark")
                                                 .Include("car_model")
                                                 .Include("CarBody").FirstAsync(x => x.id == id);
            return GetId;
        }

        public async Task<IEnumerable<ClientsOfCarWash>> GetId(int? id)
        {
            var GetId = await _context.ClientsOfCarWash.Include("ClientInfo")
                                                     .Include("ClientsGroups")
                                                     .Include("car_mark")
                                                     .Include("car_model")
                                                     .Include("CarBody")
                                                     .Where(x => x.IdInfoClient == id)
                                                     .ToListAsync();
            return GetId;
        }

        public async Task<IEnumerable<ClientsOfCarWash>> GetWhere(Expression<Func<ClientsOfCarWash, bool>> predicate)
        {
            var GetId = await _context.ClientsOfCarWash.AsNoTracking()
                                                .Include("ClientInfo")
                                                .Include("ClientsGroups")
                                                .Include("car_mark")
                                                .Include("car_model")
                                                .Include("CarBody")
                                                .Where(predicate).AsQueryable().ToListAsync();
            return GetId;
        }
    }
}

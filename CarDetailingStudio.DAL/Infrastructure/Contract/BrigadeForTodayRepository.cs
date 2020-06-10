using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CarDetailingStudio.DAL.Infrastructure.Contract
{
    public class BrigadeForTodayRepository : IGetRepository<brigadeForToday>, IExtendedRepository<brigadeForToday>
    {
        internal carWashEntities _context;

        public BrigadeForTodayRepository(carWashEntities entities)
        {
            _context = entities;
        }

        public Task<IEnumerable<brigadeForToday>> Get()
        {
            throw new NotImplementedException();
        }

        public async Task<brigadeForToday> GetById(int? id)
        {
            var GetIdResult = await _context.brigadeForToday.AsNoTracking()
                                                      .Include("CarWashWorkers")
                                                      .Include("CarWashWorkers.JobTitleTable")
                                                      .FirstAsync(x => x.id == id);

            return GetIdResult;
        }

        public async Task<IEnumerable<brigadeForToday>> GetWhere(Expression<Func<brigadeForToday, bool>> predicate)
        {
            var GetWhereResult = await _context.brigadeForToday.AsNoTracking()
                                                         .Include("CarWashWorkers")
                                                         .Include("CarWashWorkers.JobTitleTable")
                                                         .Where(predicate).AsQueryable().ToListAsync();

            return GetWhereResult;
        }
    }
}

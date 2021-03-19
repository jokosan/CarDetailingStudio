using CarDetailingStudio.DAL.Infrastructure.Contract;
using System;
using System.Collections.Generic;
using System.Data.Entity;

using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CarDetailingStudio.DAL.Infrastructure
{
    public class CarWashWorkersRepository : IGetRepository<CarWashWorkers>
    {
        internal carWashEntities _context;

        public CarWashWorkersRepository(carWashEntities entities)
        {
            _context = entities;
        }

        public async Task<IEnumerable<CarWashWorkers>> Get()
        {
            var GetResultAll = await _context.CarWashWorkers.AsNoTracking()
                                                            .Include("JobTitleTable")
                                                            .Include("brigadeForToday")
                                                            .ToListAsync();
            return GetResultAll;
        }

        public async Task<CarWashWorkers> GetById(int? id)
        {
            return await _context.CarWashWorkers.AsNoTracking()
                                                .Include("JobTitleTable")
                                                .Include("brigadeForToday")
                                                .FirstAsync(x => x.id == id);
        }

       
        public async Task<IEnumerable<CarWashWorkers>> GetWhere(Expression<Func<CarWashWorkers, bool>> predicate)
        {
            var GetWhereResult = await _context.CarWashWorkers.AsNoTracking()
                                                              .Include("JobTitleTable")
                                                              .Include("brigadeForToday")
                                                              .Where(predicate).AsQueryable().ToListAsync();
            return GetWhereResult;
        }

    }


}

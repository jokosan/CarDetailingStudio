using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.DAL.Infrastructure.Contract
{
    public class BrigadeForTodayRepository : IExtendedRepository<brigadeForToday>, IGetRepository<brigadeForToday>
    {
        internal carWashEntities _context;

        public BrigadeForTodayRepository(carWashEntities entities)
        {
            _context = entities;
        }

        public IEnumerable<brigadeForToday> Get()
        {
            throw new NotImplementedException();
        }

        public brigadeForToday GetById(int? id)
        {
            var GetIdResult = _context.brigadeForToday.Include("CarWashWorkers")
                                                      .Include("CarWashWorkers.JobTitleTable")
                                                      .FirstOrDefault(x => x.id == id);

            return GetIdResult;
        }

        public IEnumerable<brigadeForToday> GetWhere(Func<brigadeForToday, bool> predicate)
        {
            var GetWhereResult = _context.brigadeForToday.Include("CarWashWorkers")
                                                         .Include("CarWashWorkers.JobTitleTable")
                                                         .Where(predicate);

            return GetWhereResult;
        }
    }
}

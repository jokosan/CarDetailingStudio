using CarDetailingStudio.DAL.Infrastructure.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.DAL.Infrastructure
{
    public class CarWashWorkersRepository : IGetRepository<CarWashWorkers>, IExtendedRepository<CarWashWorkers>
    {
        internal carWashEntities _context;

        public CarWashWorkersRepository(carWashEntities entities)
        {
            _context = entities;
        }

        public IEnumerable<CarWashWorkers> Get()
        {
            var GetResultAll = _context.CarWashWorkers.Include("JobTitleTable")
                                                      .Include("Wage")                                                      
                                                      .Include("brigadeForToday");
            return GetResultAll;
        }

        public CarWashWorkers GetById(int? id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CarWashWorkers> GetWhere(Func<CarWashWorkers, bool> predicate)
        {
            var GetWhereResult = _context.CarWashWorkers.Include("JobTitleTable")
                                                        .Include("Wage")                                                        
                                                        .Include("brigadeForToday")
                                                        .Where(predicate);
            return GetWhereResult;
        }
    }
}

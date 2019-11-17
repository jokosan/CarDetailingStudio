using CarDetailingStudio.DAL.Infrastructure.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
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

        public IEnumerable<CarWashWorkers> Get()
        {
            var GetResultAll = _context.CarWashWorkers.Include("JobTitleTable")                                                  
                                                      .Include("brigadeForToday");
            return GetResultAll;
        }

        public CarWashWorkers GetById(int? id)
        {
            return _context.CarWashWorkers.Include("JobTitleTable")
                                          .Include("brigadeForToday").FirstOrDefault(x => x.id == id);
        }

        public IEnumerable<CarWashWorkers> GetWhere(Func<CarWashWorkers, bool> predicate)
        {
            var GetWhereResult = _context.CarWashWorkers.Include("JobTitleTable")                                                        
                                                        .Include("brigadeForToday")
                                                        .Where(predicate);
            return GetWhereResult;
        }

    }

   
}

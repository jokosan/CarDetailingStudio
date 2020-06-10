using CarDetailingStudio.DAL.Infrastructure.Contract;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CarDetailingStudio.DAL.Infrastructure
{
    public class OrderInfoViewRepository : IGetRepository<ItogOrderView>
    {
        internal carWashEntities _context;

        public OrderInfoViewRepository(carWashEntities entities)
        {
            _context = entities;
        }

        public async Task<IEnumerable<ItogOrderView>> Get()
        {
            return await _context.ItogOrderView.ToListAsync();
        }

        public async Task<ItogOrderView> GetById(int? id)
        {
            return await _context.ItogOrderView.FindAsync(id);
        }
    }
}

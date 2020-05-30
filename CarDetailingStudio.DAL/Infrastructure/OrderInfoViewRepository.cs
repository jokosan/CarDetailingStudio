using CarDetailingStudio.DAL.Infrastructure.Contract;
using System.Collections.Generic;
using System.Linq;

namespace CarDetailingStudio.DAL.Infrastructure
{
    public class OrderInfoViewRepository : IGetRepository<ItogOrderView>
    {
        internal carWashEntities _context;

        public OrderInfoViewRepository(carWashEntities entities)
        {
            _context = entities;
        }

        public IEnumerable<ItogOrderView> Get()
        {
            return _context.ItogOrderView;
        }

        public ItogOrderView GetById(int? id)
        {
            return _context.ItogOrderView.FirstOrDefault(x => x.id == id);
        }
    }
}

using CarDetailingStudio.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using CarDetailingStudio.BLL.Utilities.Map;

namespace CarDetailingStudio.BLL.Services
{
    public class OrderServicesCarWashServices : IServices<OrderServicesCarWashBll>
    {
        private IUnitOfWork _ubitOfWork;
        private AutomapperConfig _automapper;

        public OrderServicesCarWashServices()
        {
            _ubitOfWork = new UnitOfWork();
            _automapper = new AutomapperConfig();
        }
        public IEnumerable<OrderServicesCarWashBll> GetAll()
        {
            return Mapper.Map<IEnumerable<OrderServicesCarWashBll>>(_ubitOfWork.OrderServicesCarWashUnitOfWork.Get());
        }
    }
}

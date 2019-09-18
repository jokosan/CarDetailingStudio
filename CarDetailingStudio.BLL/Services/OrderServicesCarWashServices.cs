using CarDetailingStudio.BLL.Services.IServices;
using CarDetailingStudio.BLL.Services.UnitOfWorks;
using CarDetailingStudio.DataBase.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services
{
    public class OrderServicesCarWashServices : IServices<OrderServicesCarWash>
    {
        private UnitOfWork _unitOfWorks;

        public OrderServicesCarWashServices()
        {
            _unitOfWorks = new UnitOfWork();
        }

        public void Create(OrderServicesCarWash item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _unitOfWorks.Dispose();
        }

        public IEnumerable<OrderServicesCarWash> GetAll()
        {
            return _unitOfWorks.OrderServicesCarWashUW.GetAll();
        }

        public OrderServicesCarWash GetId(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(OrderServicesCarWash item)
        {
            throw new NotImplementedException();
        }
    }
}

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
    public class ServisesCarWashOrderServices : IServices<ServisesCarWashOrder>
    {
        private UnitOfWork _unitOfWork;

        public ServisesCarWashOrderServices()
        {
            _unitOfWork = new UnitOfWork();
        }
        public void Create(ServisesCarWashOrder item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ServisesCarWashOrder> GetAll()
        {
            throw new NotImplementedException();
        }

        public ServisesCarWashOrder GetId(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(ServisesCarWashOrder item)
        {
            throw new NotImplementedException();
        }
    }
}

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
    public class ClientsOfCarWashServices : IServices<ClientsOfCarWash>
    {
        UnitOfWork _unitOfWork;

        private ClientsOfCarWashServices()
        {
            _unitOfWork = new UnitOfWork();
        }

        public void Create(ClientsOfCarWash item)
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

        public IEnumerable<ClientsOfCarWash> GetAll()
        {
            return _unitOfWork.ClientsOfCarWashUW.GetAll();
        }

        public ClientsOfCarWash GetId(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(ClientsOfCarWash item)
        {
            throw new NotImplementedException();
        }
    }
}

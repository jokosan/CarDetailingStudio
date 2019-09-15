using CarDetailingStudio.DAL.Repositories.Interface;
using CarDetailingStudio.DataBase.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.DAL.Repositories
{
    public class CarWashWorkersRepositories : IRepositories<CarWashWorkers>
    {
        private carWashEntities db;
        public CarWashWorkersRepositories(carWashEntities dbModel)
        {
            db = dbModel;
        }

        public void Create(CarWashWorkers item)
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

        public IEnumerable<CarWashWorkers> GetAll()
        {
            throw new NotImplementedException();
        }

        public CarWashWorkers GetId(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(CarWashWorkers item)
        {
            throw new NotImplementedException();
        }
    }
}

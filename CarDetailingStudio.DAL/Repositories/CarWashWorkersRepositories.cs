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
           
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        #region DisposeSave
        private bool disposed = false;

        public virtual void Dispose(bool disposed)
        {
            if (!this.disposed)
            {
                if (disposed)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        #endregion

        public IEnumerable<CarWashWorkers> GetAll()
        {
            return db.CarWashWorkers;
        }

        public CarWashWorkers GetId(int id)
        {
            throw new NotImplementedException();
        }      

        public void Update(CarWashWorkers item)
        {
            throw new NotImplementedException();
        }
    }
}

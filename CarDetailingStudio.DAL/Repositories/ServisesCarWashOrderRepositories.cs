using CarDetailingStudio.DAL.Repositories.Interface;
using CarDetailingStudio.DataBase.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.DAL.Repositories
{
    public class ServisesCarWashOrderRepositories : IRepositories<ServisesCarWashOrder>
    {
        private carWashEntities db;

        public ServisesCarWashOrderRepositories(carWashEntities dbModel)
        {
            db = dbModel;
        }

        public void Create(ServisesCarWashOrder item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ServisesCarWashOrder> GetAll()
        {
            return db.ServisesCarWashOrder;
        }

        public ServisesCarWashOrder GetId(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(ServisesCarWashOrder item)
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
    }
}

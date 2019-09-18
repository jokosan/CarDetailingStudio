using CarDetailingStudio.DAL.Repositories.Interface;
using CarDetailingStudio.DataBase.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.DAL.Repositories
{
    public class ClientsOfCarWashRepositories : IRepositories<ClientsOfCarWash>
    {
        private carWashEntities db;

        public ClientsOfCarWashRepositories(carWashEntities dbModel)
        {
            db = dbModel;
        }

        public void Create(ClientsOfCarWash item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ClientsOfCarWash> GetAll()
        {
            return db.ClientsOfCarWash;
        }

        public ClientsOfCarWash GetId(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(ClientsOfCarWash item)
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

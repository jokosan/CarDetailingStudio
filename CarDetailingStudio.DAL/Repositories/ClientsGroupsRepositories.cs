using CarDetailingStudio.DAL.Repositories.Interface;
using CarDetailingStudio.DataBase.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.DAL.Repositories
{
    public class ClientsGroupsRepositories : IRepositories<ClientsGroups>
    {
        private carWashEntities db;

        public ClientsGroupsRepositories(carWashEntities dbModel)
        {
            db = dbModel;
        }


        public void Create(ClientsGroups item)
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

        public IEnumerable<ClientsGroups> GetAll()
        {
            throw new NotImplementedException();
        }

        public ClientsGroups GetId(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(ClientsGroups item)
        {
            throw new NotImplementedException();
        }
    }
}

using CarDetailingStudio.BLL.Services.IServices;
using CarDetailingStudio.DAL.Repositories;
using CarDetailingStudio.DAL.Repositories.Interface;
using CarDetailingStudio.DataBase.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.UnitOfWorks
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private carWashEntities _dbCarWashEntities = new carWashEntities();
              
        private IRepositories<ClientsOfCarWash> _ClientsOfCarWash;
        private IRepositories<ClientsGroups> _ClientsGroups;
        private IRepositories<brigadeForToday> _BrigadeForToday;
        private IRepositories<CarWashWorkers> _CarWashWorkers;        


        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbCarWashEntities.Dispose();
                }

                this.disposed = true;
            }
        }

        public void Save()
        {
            _dbCarWashEntities.SaveChanges();
        }

        public IRepositories<ClientsOfCarWash> ClientsOfCarWashUW
        {
            get
            {
                if (_ClientsOfCarWash == null)
                {
                    _ClientsOfCarWash = new ClientsOfCarWashRepositories(_dbCarWashEntities);
                }

                return _ClientsOfCarWash;
            }
        }

        public IRepositories<ClientsGroups> ClientsGroupsUW
        {
            get
            {
                if (_ClientsGroups == null)
                {
                    _ClientsGroups = new ClientsGroupsRepositories(_dbCarWashEntities);
                }
                return _ClientsGroups;
            }
        }

        public IRepositories<brigadeForToday> BrigadeForTodayUW
        {
            get
            {
                if (_BrigadeForToday == null)
                {
                    _BrigadeForToday = new BrigadeForTodayRepositories(_dbCarWashEntities);
                }

                return _BrigadeForToday;
            }
        }


        public IRepositories<CarWashWorkers> CarWashWorkersUW
        {
            get
            {
                if (_CarWashWorkers == null)
                {
                    _CarWashWorkers = new CarWashWorkersRepositories(_dbCarWashEntities);
                }

                return _CarWashWorkers;
            }
        }
    }
}

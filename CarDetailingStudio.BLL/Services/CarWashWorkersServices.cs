﻿using CarDetailingStudio.BLL.Services.IServices;
using CarDetailingStudio.DataBase.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDetailingStudio.BLL.Services.UnitOfWorks;

namespace CarDetailingStudio.BLL.Services
{
    public class CarWashWorkersServices : IServices<CarWashWorkers>
    {
        private UnitOfWork _unitOfWork;

        public CarWashWorkersServices()
        {
            _unitOfWork = new UnitOfWork();
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

        public void Update(CarWashWorkers item)
        {
            throw new NotImplementedException();
        }
    }
}

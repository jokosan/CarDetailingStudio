﻿using CarDetailingStudio.BLL.Services.IServices;
using CarDetailingStudio.DAL.Repositories.Interface;
using CarDetailingStudio.DataBase.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDetailingStudio.BLL.Services.UnitOfWorks;

namespace CarDetailingStudio.BLL.Services
{
    public class BrigadeForTodayServices : IServices<brigadeForToday>
    {
        private UnitOfWork _unitOfWorks;

        public BrigadeForTodayServices()
        {
            _unitOfWorks = new UnitOfWork();
        }

        public void Create(brigadeForToday item)
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

        public IEnumerable<brigadeForToday> GetAll()
        {
            throw new NotImplementedException();
        }

        public brigadeForToday GetId(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(brigadeForToday item)
        {
            throw new NotImplementedException();
        }
    }
}
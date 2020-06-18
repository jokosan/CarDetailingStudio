using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.TireStorageServices.TireStorageContract;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.TireStorageServices
{
    public class TireStorageServices : ITireStorage
    {
        private IUnitOfWork _unitOfWork;

        public TireStorageServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TireStorageBll>> GetTableAll()
        {
            return Mapper.Map<IEnumerable<TireStorageBll>>(await _unitOfWork.tireStorageUnitOfWork.Get());
        }

        public async Task<TireStorageBll> SelectId(int? elementId)
        {
            throw new NotImplementedException();
        }

        public async Task Insert(TireStorageBll element)
        {
            TireStorage tireStorage = Mapper.Map<TireStorageBll, TireStorage>(element);

            _unitOfWork.tireStorageUnitOfWork.Insert(tireStorage);
            await _unitOfWork.Save();
        }

        public async Task Update(TireStorageBll elementToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}

using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract.GenericContract;
using CarDetailingStudio.BLL.Services.TireStorageServices.TireStorageContract;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.TireStorageServices
{
    public class TireStorageSerServices : ITireStorageServices
    {
        private IUnitOfWork _unitOfWork;

        public TireStorageSerServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TireStorageServicesBll>> GetTableAll()
        {
            return Mapper.Map<IEnumerable<TireStorageServicesBll>>(await _unitOfWork.tireStorageServicesUnitOfWork.Get());
        }

        public async Task Insert(TireStorageServicesBll element)
        {
            throw new NotImplementedException();
        }

        public async Task<TireStorageServicesBll> SelectId(int? elementId)
        {
            return Mapper.Map<TireStorageServicesBll>(await _unitOfWork.tireStorageServicesUnitOfWork.GetById(elementId));
        }

        public async Task Update(TireStorageServicesBll elementToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}

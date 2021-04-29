using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.TireStorageServices.TireStorageContract;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<TireStorageBll> GetOrderId(int idOrder)
        {
            var result = Mapper.Map<IEnumerable<TireStorageBll>>(await _unitOfWork.tireStorageUnitOfWork.QueryObjectGraph(x => x.IdOrderServicesCarWash == idOrder, "storageFee"));
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<TireStorageBll>> GetTableAll() =>
            Mapper.Map<IEnumerable<TireStorageBll>>(await _unitOfWork.tireStorageUnitOfWork.Get());
        
        public async Task<TireStorageBll> SelectId(int? elementId)
        {
            throw new NotImplementedException();
        }

        public async Task Insert(TireStorageBll element)
        {
            _unitOfWork.tireStorageUnitOfWork.Insert(TransformAnEntity(element));
            await _unitOfWork.Save();
        }

        public async Task Update(TireStorageBll elementToUpdate)
        {
            _unitOfWork.tireStorageUnitOfWork.Update(TransformAnEntity(elementToUpdate));
            await _unitOfWork.Save();
        }

        private TireStorage TransformAnEntity(TireStorageBll entity) =>
            Mapper.Map<TireStorageBll, TireStorage>(entity);

    }
}

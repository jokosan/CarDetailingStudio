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
    public class StorageFeeServices : IStorageFee
    {
        private IUnitOfWork _unitOfWork;

        public StorageFeeServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<StorageFeeBll>> GetTableAll()
        {
            return Mapper.Map<IEnumerable<StorageFeeBll>>(await _unitOfWork.storageFeeUnitOfWork.Get());
        }

        public async Task Insert(StorageFeeBll element)
        {
            storageFee storage = Mapper.Map<StorageFeeBll, storageFee>(element);
            _unitOfWork.storageFeeUnitOfWork.Insert(storage);
            await _unitOfWork.Save();
        }

        public async Task<int> InsertVoidInt(StorageFeeBll element)
        {
            storageFee storage = Mapper.Map<StorageFeeBll, storageFee>(element);
            _unitOfWork.storageFeeUnitOfWork.Insert(storage);
            await _unitOfWork.Save();

            return storage.idStorageFee;
        }

        public async Task<StorageFeeBll> SelectId(int? elementId)
        {
            return Mapper.Map<StorageFeeBll>(await _unitOfWork.storageFeeUnitOfWork.GetById(elementId));
        }

        public async Task Update(StorageFeeBll elementToUpdate)
        {
            storageFee storage = Mapper.Map<StorageFeeBll, storageFee>(elementToUpdate);
            _unitOfWork.storageFeeUnitOfWork.Update(storage);
            await _unitOfWork.Save();
        }
    }
}

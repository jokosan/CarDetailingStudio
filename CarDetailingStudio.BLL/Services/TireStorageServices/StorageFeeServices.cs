using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.TireStorageServices.TireStorageContract;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;

namespace CarDetailingStudio.BLL.Services.TireStorageServices
{
    public class StorageFeeServices : IStorageFee
    {
        private IUnitOfWork _unitOfWork;

        public StorageFeeServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<StorageFeeBll> GetTableAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(StorageFeeBll element)
        {
            throw new NotImplementedException();
        }

        public int InsertVoidInt(StorageFeeBll element)
        {
            storageFee storage = Mapper.Map<StorageFeeBll, storageFee>(element);
            _unitOfWork.storageFeeUnitOfWork.Insert(storage);
            _unitOfWork.Save();

            return storage.idStorageFee;
        }

        public StorageFeeBll SelectId(int? elementId)
        {
            throw new NotImplementedException();
        }

        public void Update(StorageFeeBll elementToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}

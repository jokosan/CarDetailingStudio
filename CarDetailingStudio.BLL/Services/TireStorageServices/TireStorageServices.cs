using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.TireStorageServices.TireStorageContract;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;

namespace CarDetailingStudio.BLL.Services.TireStorageServices
{
    public class TireStorageServices : ITireStorage
    {
        private IUnitOfWork _unitOfWork;

        public TireStorageServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<TireStorageBll> GetTableAll()
        {
            throw new NotImplementedException();
        }
       
        public TireStorageBll SelectId(int? elementId)
        {
            throw new NotImplementedException();
        }

        public void Insert(TireStorageBll element)
        {
            TireStorage tireStorage = Mapper.Map<TireStorageBll, TireStorage>(element);
         
            _unitOfWork.tireStorageUnitOfWork.Insert(tireStorage);
            _unitOfWork.Save();
        }

        public void Update(TireStorageBll elementToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}

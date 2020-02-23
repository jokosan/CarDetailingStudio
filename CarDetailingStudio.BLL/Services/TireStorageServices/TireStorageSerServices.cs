using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.TireStorageServices.TireStorageContract;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;

namespace CarDetailingStudio.BLL.Services.TireStorageServices
{
    public class TireStorageSerServices : ITireStorageServices
    {
        private IUnitOfWork _unitOfWork;

        public TireStorageSerServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<TireStorageServicesBll> GetTableAll()
        {
            return Mapper.Map<IEnumerable<TireStorageServicesBll>>(_unitOfWork.tireStorageServicesUnitOfWork.Get());
        }

        public void Insert(TireStorageServicesBll element)
        {
            throw new NotImplementedException();
        }

        public TireStorageServicesBll SelectId(int? elementId)
        {
            throw new NotImplementedException();
        }

        public void Update(TireStorageServicesBll elementToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}

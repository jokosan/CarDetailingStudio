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
using DevExpress.DirectX.Common.DirectWrite;

namespace CarDetailingStudio.BLL.Services.TireStorageServices
{
    public class AdditionalTireStorageServices : IAdditionalTireStorageServices
    {
        private IUnitOfWork _unitOfWork;

        public AdditionalTireStorageServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<AdditionalTireStorageServicesBll>> GetOrderId(int id)
        {
            return Mapper.Map<IEnumerable<AdditionalTireStorageServicesBll>>(await _unitOfWork.AdditionalTireStorageServicesUnitOfWork.QueryObjectGraph(x => x.orderServicesCarWashId == id, "TireStorageServices"));
        }
   
        public async Task Update(AdditionalTireStorageServicesBll elementToUpdate)
        {
            _unitOfWork.AdditionalTireStorageServicesUnitOfWork.Update(TransformAnEntity(elementToUpdate));
            await _unitOfWork.Save();
        }
        public async Task Insert(AdditionalTireStorageServicesBll element)
        {
            _unitOfWork.AdditionalTireStorageServicesUnitOfWork.Insert(TransformAnEntity(element));
            await _unitOfWork.Save();
        }

        private additionalTireStorageServices TransformAnEntity(AdditionalTireStorageServicesBll entity) => Mapper.Map<AdditionalTireStorageServicesBll, additionalTireStorageServices>(entity);
    }
}

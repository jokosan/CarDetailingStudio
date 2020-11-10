using CarDetailingStudio.BLL.Services.TireFitting.TireFittingContract;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.DAL;

namespace CarDetailingStudio.BLL.Services.TireFitting
{
    public class PriceTireFittingAdditionalServices : IPriceTireFittingAdditionalServices
    {
        private IUnitOfWork _unitOfWork;

        public PriceTireFittingAdditionalServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PriceListTireFittingAdditionalServicesBll>> GetTableAll()
        {
            return Mapper.Map<IEnumerable<PriceListTireFittingAdditionalServicesBll>>(await _unitOfWork.PriceListTireFittingAdditionalServicesUnitOfWork.Get());
        }

        public async Task<PriceListTireFittingAdditionalServicesBll> SelectId(int? elementId)
        {
            return Mapper.Map<PriceListTireFittingAdditionalServicesBll>(await _unitOfWork.PriceListTireFittingAdditionalServicesUnitOfWork.GetById(elementId));
        }

        public async Task<IEnumerable<PriceListTireFittingAdditionalServicesBll>> PriceListTireFittingsContains(List<int> id)
        {
            return Mapper.Map<IEnumerable<PriceListTireFittingAdditionalServicesBll>>(await _unitOfWork.PriceListTireFittingAdditionalServicesUnitOfWork.QueryObjectGraph(x => id.Contains(x.idPriceListTireFittingAdditionalServices)));
        }

        public Task Insert(PriceListTireFittingAdditionalServicesBll element)
        {
            throw new NotImplementedException();
        }

        public async Task Update(PriceListTireFittingAdditionalServicesBll elementToUpdate)
        {
            _unitOfWork.PriceListTireFittingAdditionalServicesUnitOfWork.Update(EntityTransformation(elementToUpdate));
            await _unitOfWork.Save();
        }

        private PriceListTireFittingAdditionalServices EntityTransformation(PriceListTireFittingAdditionalServicesBll entities) => Mapper.Map<PriceListTireFittingAdditionalServicesBll, PriceListTireFittingAdditionalServices>(entities);
    }
}

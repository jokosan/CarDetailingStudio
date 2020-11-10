using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.TireFitting.TireFittingContract;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace CarDetailingStudio.BLL.Services.TireFitting
{
    public class PriceListTireFittingServices : IPriceListTireFitting
    {
        private IUnitOfWork _unitOfWork;

        public PriceListTireFittingServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PriceListTireFittingBll>> GetTableAll()
        {
            return Mapper.Map<IEnumerable<PriceListTireFittingBll>>(await _unitOfWork.PriceListTireFittingUnitOfWork.GetInclude("TireRadius", "TypeOfCars"));
        }

        public async Task<PriceListTireFittingBll> SelectId(int? elementId)
        {
            return Mapper.Map<PriceListTireFittingBll>(await _unitOfWork.PriceListTireFittingUnitOfWork.GetById(elementId));
        }

        public async Task<IEnumerable<PriceListTireFittingBll>> SelectValueFromThePriceList(List<int> id)
        {
            return Mapper.Map<IEnumerable<PriceListTireFittingBll>>(await _unitOfWork.PriceListTireFittingUnitOfWork.QueryObjectGraph(x => id.Contains(x.idPriceListTireFitting), "TireRadius"));
        }

        public async Task<IEnumerable<PriceListTireFittingBll>> SelectRadius(List<int> idRadius, int typyCar)
        {
            return Mapper.Map<IEnumerable<PriceListTireFittingBll>>(await _unitOfWork.PriceListTireFittingUnitOfWork.QueryObjectGraph(x => idRadius.Contains(x.TireRadiusId.Value) && x.TypeOfCarsId == typyCar, "TireRadius"));
        }

    }
}

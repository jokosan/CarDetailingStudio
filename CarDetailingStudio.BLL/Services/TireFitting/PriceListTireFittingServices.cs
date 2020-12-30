using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.TireFitting.TireFittingContract;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.DAL;

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

        public async Task<IEnumerable<PriceListTireFittingBll>> SelectId(int? id, int? typeCar)
        {
            return Mapper.Map<IEnumerable<PriceListTireFittingBll>>(await _unitOfWork.PriceListTireFittingUnitOfWork.QueryObjectGraph(x => x.TireRadiusId == id && x.TypeOfCarsId == typeCar, "TireRadius"));
        }

        public async Task<IEnumerable<PriceListTireFittingBll>> SelectValueFromThePriceList(List<int> id)
        {
            return Mapper.Map<IEnumerable<PriceListTireFittingBll>>(await _unitOfWork.PriceListTireFittingUnitOfWork.QueryObjectGraph(x => id.Contains(x.idPriceListTireFitting), "TireRadius"));
        }

        public async Task<IEnumerable<PriceListTireFittingBll>> SelectRadius(int idRadius)
        {
            var radius = await _unitOfWork.PriceListTireFittingUnitOfWork.GetById(idRadius);
            return Mapper.Map<IEnumerable<PriceListTireFittingBll>>(await _unitOfWork.PriceListTireFittingUnitOfWork.QueryObjectGraph(x => x.TireRadiusId == radius.TireRadiusId && x.TypeOfCarsId == radius.TypeOfCarsId, "TireRadius"));
        }

        private PriceListTireFitting TransformEntity(PriceListTireFittingBll entity) => Mapper.Map<PriceListTireFittingBll, PriceListTireFitting>(entity);

        public async Task Insert(PriceListTireFittingBll element)
        {
            _unitOfWork.PriceListTireFittingUnitOfWork.Insert(TransformEntity(element));
            await _unitOfWork.Save();
        }

        public async Task Update(PriceListTireFittingBll elementToUpdate)
        {
            _unitOfWork.PriceListTireFittingUnitOfWork.Update(TransformEntity(elementToUpdate));
            await _unitOfWork.Save();
        }

        
    }
}

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
using System.Runtime.InteropServices;

namespace CarDetailingStudio.BLL.Services.TireFitting
{
    public class TireService : ITireService
    {
        private IUnitOfWork _unitOfWork;

        public TireService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TireServiceBll>> GetTableAll()
        {
            return Mapper.Map<IEnumerable<TireServiceBll>>(await _unitOfWork.TireServiceUnitOfWork.GetInclude("PriceListTireFittingAdditionalServices"));
        }

        public async Task Insert(TireServiceBll element)
        {
            tireService tireServices = Mapper.Map<TireServiceBll, tireService>(element);
            _unitOfWork.TireServiceUnitOfWork.Insert(tireServices);
            await _unitOfWork.Save();
        }

        public async Task<TireServiceBll> SelectId(int? elementId)
        {

            throw new NotImplementedException();
        }

        public async Task Update(TireServiceBll elementToUpdate)
        {
            tireService tireServices = Mapper.Map<TireServiceBll, tireService>(elementToUpdate);
            _unitOfWork.TireServiceUnitOfWork.Update(tireServices);
            await _unitOfWork.Save();
        }

        public async Task<IEnumerable<TireServiceBll>> SelectTireServices(int OrderId)
        {
            return Mapper.Map<IEnumerable<TireServiceBll>>(await _unitOfWork.TireServiceUnitOfWork.QueryObjectGraph(x => x.orderServicesCarWashId == OrderId, "PriceListTireFittingAdditionalServices"));
        }
    }
}

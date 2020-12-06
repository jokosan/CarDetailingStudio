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
    public class TireChangeService : ITireChangeService
    {
        private IUnitOfWork _unitOfWork;

        public TireChangeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TireChangeServiceBll>> GetTableAll()
        {
            return Mapper.Map<IEnumerable<TireChangeServiceBll>>(await _unitOfWork.TireChangeServiceUnitOfWork.GetInclude("PriceListTireFitting", "PriceListTireFitting.TireRadius"));
        }

        public async Task Insert(TireChangeServiceBll element)
        {
            tireChangeService tireChange = Mapper.Map<TireChangeServiceBll, tireChangeService>(element);
            _unitOfWork.TireChangeServiceUnitOfWork.Insert(tireChange);
            await _unitOfWork.Save();
        }

        public async Task<TireChangeServiceBll> SelectId(int? elementId) => Mapper.Map<TireChangeServiceBll>(await _unitOfWork.TireChangeServiceUnitOfWork.GetById(elementId));
        

        public async Task Update(TireChangeServiceBll elementToUpdate)
        {
            tireChangeService tireChange = Mapper.Map<TireChangeServiceBll, tireChangeService>(elementToUpdate);
            _unitOfWork.TireChangeServiceUnitOfWork.Update(tireChange);
            await _unitOfWork.Save();
        }

        public async Task<IEnumerable<TireChangeServiceBll>> SelectTireService(int OrderId)
        {
            return Mapper.Map<IEnumerable<TireChangeServiceBll>>(await _unitOfWork.TireChangeServiceUnitOfWork.QueryObjectGraph(x => x.idOrder == OrderId, "PriceListTireFitting", "PriceListTireFitting.TireRadius"));
        }

    }
}

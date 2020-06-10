using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services
{
    public class StatusOrderServices : IStatusOrder
    {
        private IUnitOfWork _unitOfWork;

        public StatusOrderServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<StatusOrderBll>> GetTableAll()
        {
            return Mapper.Map<IEnumerable<StatusOrderBll>>(await _unitOfWork.StatusOrderUnitOfWork.Get());
        }

        public async Task<StatusOrderBll> SelectId(int? elementId)
        {
            return Mapper.Map<StatusOrderBll>(await _unitOfWork.StatusOrderUnitOfWork.GetById(elementId));
        }
    }
}

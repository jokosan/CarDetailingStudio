using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;

namespace CarDetailingStudio.BLL.Services
{
    public class StatusOrderServices : IStatusOrder
    {
        private IUnitOfWork _unitOfWork;

        public StatusOrderServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<StatusOrderBll> GetTableAll()
        {
            return Mapper.Map<IEnumerable<StatusOrderBll>>(_unitOfWork.StatusOrderUnitOfWork.Get());
        }

        public StatusOrderBll SelectId(int? elementId)
        {
            throw new NotImplementedException();
        }
    }
}

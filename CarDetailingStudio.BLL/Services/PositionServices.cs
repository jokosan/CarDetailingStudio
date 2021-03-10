using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace CarDetailingStudio.BLL.Services
{
    public class PositionServices : IPosition
    {
        private readonly IUnitOfWork _unitOfWork;

        public PositionServices(
            IUnitOfWork unitoGWork)
        {
            _unitOfWork = unitoGWork;
        }

        public async Task<IEnumerable<PositionBll>> GetTableAll() =>
            Mapper.Map<IEnumerable<PositionBll>>(await _unitOfWork.PositionUnitOfWork.Get());

        public async Task<PositionBll> SelectId(int? elementId) =>
            Mapper.Map<PositionBll>(await _unitOfWork.PositionUnitOfWork.GetById(elementId));
    }
}

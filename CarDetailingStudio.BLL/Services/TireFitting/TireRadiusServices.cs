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
    public class TireRadiusServices : ITireRadius
    {
        private IUnitOfWork _unitOfWork;

        public TireRadiusServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TireRadiusBll>> GetTableAll()
        {
            return Mapper.Map<IEnumerable<TireRadiusBll>>(await _unitOfWork.TireRadiusUnitOfWork.Get()); 
        }

        public async Task<TireRadiusBll> SelectId(int? elementId)
        {
            return Mapper.Map<TireRadiusBll>(await _unitOfWork.TireRadiusUnitOfWork.GetById(elementId));
        }

        public async Task<IEnumerable<TireRadiusBll>> SeletTireRadius(List<int> idRadius)
        {
            var resultRadius = await GetTableAll();
            return resultRadius.Where(x => idRadius.Contains(x.idTireRadius));
        }
    }
}

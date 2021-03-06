﻿using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Utilities.Map;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services
{
    public class ServisesCarWashOrderServices : IServisesCarWashOrderServices
    {
        private IUnitOfWork _unitOfWork;
        private AutomapperConfig _automapper;

        public ServisesCarWashOrderServices(IUnitOfWork unitOfWork, AutomapperConfig automapper)
        {
            _unitOfWork = unitOfWork;
            _automapper = automapper;
        }

        public async Task<IEnumerable<ServisesCarWashOrderBll>> GetAllId(int? id) => 
            Mapper.Map<IEnumerable<ServisesCarWashOrderBll>>(await _unitOfWork.ServisesUnitOfWork.GetWhere(x => x.IdOrderServicesCarWash == id));       
      
        public async Task ServicesDelete(int id, string NameClass)
        {
            if (NameClass == "OrderController")
            {
                _unitOfWork.ServisesCarWashOrderUnitOfWork.Delete(id);
                await _unitOfWork.Save();
            }
            else
            {
                var resultId = Mapper.Map<IEnumerable<ServisesCarWashOrderBll>>(await _unitOfWork.ServisesUnitOfWork.GetWhere(x => x.IdOrderServicesCarWash == id));

                foreach (var x in resultId)
                {
                    _unitOfWork.ServisesCarWashOrderUnitOfWork.Delete(x.Id);
                    await _unitOfWork.Save();
                }
            }
        }
    }
}

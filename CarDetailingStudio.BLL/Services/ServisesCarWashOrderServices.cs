using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Utilities.Map;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.DAL;

namespace CarDetailingStudio.BLL.Services
{
    public class ServisesCarWashOrderServices : IServices<ServisesCarWashOrderBll>
    {
        private IUnitOfWork _unitOfWork;
        private AutomapperConfig _automapper;

        public ServisesCarWashOrderServices(UnitOfWork unitOfWork, AutomapperConfig automapper)
        {
            _unitOfWork = unitOfWork;
            _automapper = automapper;
        }

        public IEnumerable<ServisesCarWashOrderBll> GetAll()
        {
            return Mapper.Map<IEnumerable<ServisesCarWashOrderBll>>(_unitOfWork.ServisesUnitOfWork.Get());
        }

        public IEnumerable<ServisesCarWashOrderBll> GetAllId(int? id)
        {
            var servises = Mapper.Map<IEnumerable<ServisesCarWashOrderBll>>(_unitOfWork.ServisesUnitOfWork.GetWhere(x => x.IdOrderServicesCarWash == id));

            return servises;
        }

        public void ServisesInsert(List<ServisesCarWashOrderBll> idServeces, int idOrder, int idClient)
        {
            List<ServisesCarWashOrderBll> AddOrder = new List<ServisesCarWashOrderBll>();

            foreach (var item in idServeces)
            {
                AddOrder.Add(new ServisesCarWashOrderBll
                {
                    IdClientsOfCarWash = idClient,
                    IdOrderServicesCarWash = idOrder,
                    IdWashServices = item.IdWashServices,
                    Price = item.Price                    
                });
            }
        }

        public void ServicesDelete(int id)
        {
            _unitOfWork.ServisesCarWashOrderUnitOfWork.Delete(id);
            _unitOfWork.Save();
        }
    }
}

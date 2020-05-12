using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Utilities.Map;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.DAL;
using CarDetailingStudio.BLL.Services.Contract;

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

        public void ServicesDelete(int id, string NameClass)
        {
            if (NameClass == "OrderController")
            {
                _unitOfWork.ServisesCarWashOrderUnitOfWork.Delete(id);
                _unitOfWork.Save();
            }
            else
            {
                var resultId = Mapper.Map<IEnumerable<ServisesCarWashOrderBll>>(_unitOfWork.ServisesUnitOfWork.GetWhere(x => x.IdOrderServicesCarWash == id));

                foreach (var x in resultId)
                {
                    _unitOfWork.ServisesCarWashOrderUnitOfWork.Delete(x.Id);
                    _unitOfWork.Save();
                }
            }
        }
    }
}

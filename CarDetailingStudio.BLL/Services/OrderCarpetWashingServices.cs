using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services
{
    public class OrderCarpetWashingServices : IOrderCarpetWashingServices
    {
        private IUnitOfWork _unitOfWork;

        public OrderCarpetWashingServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<IEnumerable<OrderCarpetWashingBll>> GetTableAll()
        {
            return Mapper.Map<IEnumerable<OrderCarpetWashingBll>>(await _unitOfWork.OrderCarpetWashingUnitOfWork.Get());
        }

        public async Task<IEnumerable<OrderCarpetWashingBll>> GetIncludeWhere()
        {
            // return Mapper.Map<IEnumerable<OrderCarpetWashingBll>>(_unitOfWork.OrderCarpetWashingUnitOfWork.QueryObjectGraph(x => x.OrderServicesCarWash.typeOfOrder == 3, "OrderServicesCarWash"));
            List<string> input = new List<string>() { "OrderServicesCarWash", "PaymentState1", "StatusOrder1" };
            return Mapper.Map<IEnumerable<OrderCarpetWashingBll>>(await _unitOfWork.OrderCarpetWashingUnitOfWork.QueryObjectGraph(x => x.OrderServicesCarWash.typeOfOrder == 3, input));
            //return Mapper.Map<IEnumerable<OrderCarpetWashingBll>>(_unitOfWork.OrderCarpetWashingUnitOfWork.Item(x => x.OrderServicesCarWash.typeOfOrder == 3,
            //                                                                                                    y => y.OrderServicesCarWash,
            //                                                                                                    y => y.OrderServicesCarWash.PaymentState1,
            //                                                                                                    y => y.OrderServicesCarWash.PaymentState1
            //                                                                                                    ));
        }

        public async Task<OrderCarpetWashingBll> SelectId(int? elementId)
        {
            return Mapper.Map<OrderCarpetWashingBll>(await _unitOfWork.OrderCarpetWashingUnitOfWork.GetById(elementId));
        }

        public async Task Insert(OrderCarpetWashingBll element)
        {
            orderCarpetWashing ordersCarpetWashing = Mapper.Map<OrderCarpetWashingBll, orderCarpetWashing>(element);

            _unitOfWork.OrderCarpetWashingUnitOfWork.Insert(ordersCarpetWashing);
            await _unitOfWork.Save();
        }

        public async Task Update(OrderCarpetWashingBll elementToUpdate)
        {
            orderCarpetWashing ordersCarpetWashing = Mapper.Map<OrderCarpetWashingBll, orderCarpetWashing>(elementToUpdate);

            _unitOfWork.OrderCarpetWashingUnitOfWork.Update(ordersCarpetWashing);
            await _unitOfWork.Save();
        }
    }
}

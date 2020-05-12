﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;

namespace CarDetailingStudio.BLL.Services
{
    public class OrderCarpetWashingServices : IOrderCarpetWashingServices
    {
        private IUnitOfWork _unitOfWork;

        public OrderCarpetWashingServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IEnumerable<OrderCarpetWashingBll> GetTableAll()
        {
            return Mapper.Map<IEnumerable<OrderCarpetWashingBll>>(_unitOfWork.OrderCarpetWashingUnitOfWork.Get());
        }

        public IEnumerable<OrderCarpetWashingBll> GetIncludeWhere()
        {
            // return Mapper.Map<IEnumerable<OrderCarpetWashingBll>>(_unitOfWork.OrderCarpetWashingUnitOfWork.QueryObjectGraph(x => x.OrderServicesCarWash.typeOfOrder == 3, "OrderServicesCarWash"));
            List<string> input = new List<string>() { "OrderServicesCarWash", "PaymentState1", "StatusOrder1" };
            return Mapper.Map<IEnumerable<OrderCarpetWashingBll>>(_unitOfWork.OrderCarpetWashingUnitOfWork.QueryObjectGraph(x => x.OrderServicesCarWash.typeOfOrder == 3, input));
            //return Mapper.Map<IEnumerable<OrderCarpetWashingBll>>(_unitOfWork.OrderCarpetWashingUnitOfWork.Item(x => x.OrderServicesCarWash.typeOfOrder == 3,
            //                                                                                                    y => y.OrderServicesCarWash,
            //                                                                                                    y => y.OrderServicesCarWash.PaymentState1,
            //                                                                                                    y => y.OrderServicesCarWash.PaymentState1
            //                                                                                                    ));
        }

        public OrderCarpetWashingBll SelectId(int? elementId)
        {
            return Mapper.Map<OrderCarpetWashingBll>(_unitOfWork.OrderCarpetWashingUnitOfWork.GetById(elementId));
        }

        public void Insert(OrderCarpetWashingBll element)
        {
            orderCarpetWashing ordersCarpetWashing = Mapper.Map<OrderCarpetWashingBll, orderCarpetWashing>(element);

            _unitOfWork.OrderCarpetWashingUnitOfWork.Insert(ordersCarpetWashing);
            _unitOfWork.Save();
        }

        public void Update(OrderCarpetWashingBll elementToUpdate)
        {
            orderCarpetWashing ordersCarpetWashing = Mapper.Map<OrderCarpetWashingBll, orderCarpetWashing>(elementToUpdate);

            _unitOfWork.OrderCarpetWashingUnitOfWork.Update(ordersCarpetWashing);
            _unitOfWork.Save();
        }
    }
}
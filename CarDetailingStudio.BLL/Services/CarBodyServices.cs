﻿using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Utilities.Map;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System.Collections.Generic;

namespace CarDetailingStudio.BLL.Services
{
    public class CarBodyServices : ICarBodyServices
    {
        private IUnitOfWork _unitOfWork;
        private AutomapperConfig _automapperConfig;

        public CarBodyServices(IUnitOfWork unitOfWork, AutomapperConfig automapper)
        {
            _unitOfWork = unitOfWork;
            _automapperConfig = automapper;
        }

        public IEnumerable<CarBodyBll> GetTableAll()
        {
            return Mapper.Map<IEnumerable<CarBodyBll>>(_unitOfWork.CarBodyUnitOfWork.Get());
        }

        public CarBodyBll SelectId(int? elementId)
        {
            return Mapper.Map<CarBodyBll>(_unitOfWork.CarBodyUnitOfWork.GetById(elementId));
        }
    }
}

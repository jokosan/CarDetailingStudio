﻿using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Utilities.Map;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System.Collections.Generic;

namespace CarDetailingStudio.BLL.Services
{
    public class InfoBrigadeForTodayServices
    {
        private IUnitOfWork _unitOfWork;
        private AutomapperConfig _automapper;

        public InfoBrigadeForTodayServices(IUnitOfWork unitOfWork, AutomapperConfig automapper)
        {
            _unitOfWork = unitOfWork;
            _automapper = automapper;
        }

        public IEnumerable<InfoBrigadeForTodayBll> infoBrigadeForTodays()
        {
            // return Mapper.Map<IEnumerable<InfoBrigadeForTodayBll>>(_unitOfWork.infoBrigadeUnitOfWork.GetWhere(x => x.Date? > 7));
            return null;
        }
    }
}

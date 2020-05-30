using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarDetailingStudio.BLL.Services.Filters
{
    public class TeamMonitoringFilters
    {
        private IUnitOfWork _unitOfWork;

        public TeamMonitoringFilters()
        {
            _unitOfWork = new UnitOfWork();
        }

        public int Monitoring()
        {
            var TeamMonitoring = Mapper.Map<IEnumerable<BrigadeForTodayBll>>(_unitOfWork.BrigadeUnitOfWork
                .GetWhere(x => (x.Date?.ToString("dd.MM.yyyy") == DateTime.Now.ToString("dd.MM.yyyy") && (x.EarlyTermination == true))));

            return TeamMonitoring.Where(b => b.EarlyTermination == true).Count();
        }
    }
}

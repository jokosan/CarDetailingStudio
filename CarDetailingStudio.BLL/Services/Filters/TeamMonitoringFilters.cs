using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Filters
{
    public class TeamMonitoringFilters
    {
        private IUnitOfWork _unitOfWork;

        public TeamMonitoringFilters()
        {
            _unitOfWork = new UnitOfWork();
        }

        public async Task<int> Monitoring()
        {
            var brigadeResult = Mapper.Map<IEnumerable<BrigadeForTodayBll>>(await _unitOfWork.BrigadeUnitOfWork.GetWhere(x => x.EarlyTermination == true));
            var TeamMonitoring = brigadeResult.Where(x => x.Date?.ToString("dd.MM.yyyy") == DateTime.Now.ToString("dd.MM.yyyy"));

            return TeamMonitoring.Where(b => b.EarlyTermination == true).Count();
        }
    }
}

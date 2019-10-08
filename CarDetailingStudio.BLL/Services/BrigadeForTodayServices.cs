using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Utilities.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;

namespace CarDetailingStudio.BLL.Services
{
    public class BrigadeForTodayServices : IServices<BrigadeForTodayBll>
    {
        private IUnitOfWork _unitOfWork;
        private AutomapperConfig _automapper;

        public BrigadeForTodayServices()
        {
            _unitOfWork = new UnitOfWork();
            _automapper = new AutomapperConfig();
        }

        public IEnumerable<BrigadeForTodayBll> GetAll()
        {
            return Mapper.Map<IEnumerable<BrigadeForTodayBll>>(_unitOfWork.BrigadeForTodayUnitOfWork.Get()
                .Where(x => x.Date?.ToString("dd.MM.yyyy") == DateTime.Now.ToString("dd.MM.yyyy")));
        }

        public IEnumerable<BrigadeForTodayBll> Info(int? id)
        {            
           return  Mapper.Map<IEnumerable<BrigadeForTodayBll>>(_unitOfWork.BrigadeForTodayUnitOfWork.Get()
                 .Where(x => x.IdCarWashWorkers == id)).OrderByDescending(x => x.id);
        }
    }
}

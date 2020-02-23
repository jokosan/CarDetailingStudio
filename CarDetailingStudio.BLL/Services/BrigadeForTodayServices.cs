using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Utilities.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using CarDetailingStudio.DAL;
using CarDetailingStudio.BLL.Services.Contract;

namespace CarDetailingStudio.BLL.Services
{
    public class BrigadeForTodayServices : IBrigadeForTodayServices
    {
        private IUnitOfWork _unitOfWork;

        public BrigadeForTodayServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<BrigadeForTodayBll> GetDateTimeNow()
        {
                return Mapper.Map<IEnumerable<BrigadeForTodayBll>>(_unitOfWork.BrigadeUnitOfWork
                  .GetWhere(x => (x.Date?.ToString("dd.MM.yyyy") == DateTime.Now.ToString("dd.MM.yyyy")
                        && (x.EarlyTermination == true))));
        }

        public IEnumerable<BrigadeForTodayBll> Info(int? id)
        {
            return Mapper.Map<IEnumerable<BrigadeForTodayBll>>(_unitOfWork.BrigadeUnitOfWork
                .GetWhere(x => x.IdCarWashWorkers == id)).OrderByDescending(x => x.id);
        }
        
        public void RemoveFromBrigade(int id)
        {
            BrigadeForTodayBll brigadeForTodayBll = GetId(id);

            brigadeForToday removeFromBrigade = Mapper.Map<BrigadeForTodayBll, brigadeForToday>(brigadeForTodayBll);

            removeFromBrigade.EndTime = DateTime.Now;
            removeFromBrigade.EarlyTermination = false;

            _unitOfWork.BrigadeForTodayUnitOfWork.Update(removeFromBrigade);
            _unitOfWork.Save();
        }

        public BrigadeForTodayBll GetId(int id)
        {
            return Mapper.Map<BrigadeForTodayBll>(_unitOfWork.BrigadeUnitOfWork.GetById(id));
        }
    }
}

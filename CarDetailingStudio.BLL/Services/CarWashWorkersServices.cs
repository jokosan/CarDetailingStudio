using CarDetailingStudio.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Web.Mvc;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Utilities.Map;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using CarDetailingStudio.DAL;

namespace CarDetailingStudio.BLL.Services
{
    public class CarWashWorkersServices : IServices<CarWashWorkersBll>
    {
        private IUnitOfWork _unitOfWork;
        private BrigadeForTodayBll _brigade;
        private AutomapperConfig _automapper;

        public CarWashWorkersServices()
        {
            _unitOfWork = new UnitOfWork();
            _brigade = new BrigadeForTodayBll();
            _automapper = new AutomapperConfig();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public IEnumerable<CarWashWorkersBll> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CarWashWorkersBll> GetChooseEmployees()
        {
            return Mapper.Map<IEnumerable<CarWashWorkersBll>>(_unitOfWork.CarWashWorkersUnitOfWork.Get().Where(x => (x.status == "true")
                           && (x.JobTitleTable.Position == "Детейлер")
                           || (x.JobTitleTable.Position == "Мойщик")
                           || (x.JobTitleTable.Position == "Старший мойщик")));
        }

        public void AddToCurrentShift(FormCollection collection)
        {
            string idString = collection[0];
            string[] idList = idString.Split(',');


            foreach (var item in idList)
            {
                _brigade.Date = DateTime.Now;
                _brigade.IdCarWashWorkers = Int32.Parse(item);
                                
                brigadeForToday brigade = Mapper.Map<BrigadeForTodayBll, brigadeForToday>(_brigade);
                     
                _unitOfWork.BrigadeForTodayUnitOfWork.Insert(brigade);
                _unitOfWork.Save();
            }
        }

        public bool HomeEntryCondition()
        {
            string date = DateTime.Now.ToString("dd.MM.yyyy");
            var thisDay = _unitOfWork.BrigadeForTodayUnitOfWork.Get().Any(x => x.Date?.ToString("dd.MM.yyyy") == DateTime.Now.ToString("dd.MM.yyyy"));
         
            return thisDay;
        }        
    }
}

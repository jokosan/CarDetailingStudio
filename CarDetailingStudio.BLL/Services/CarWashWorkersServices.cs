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
    public class CarWashWorkersServices : ICarWashWorkersServices
    {
        private IUnitOfWork _unitOfWork;
        private BrigadeForTodayBll _brigade;

        public CarWashWorkersServices()
        {
            _unitOfWork = new UnitOfWork();
            _brigade = new BrigadeForTodayBll();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public IEnumerable<CarWashWorkersBll> GetTable()
        {
            return Mapper.Map<IEnumerable<CarWashWorkersBll>>(_unitOfWork.CarWashWorkersUnitOfWork.Get());
        }

        public IEnumerable<CarWashWorkersBll> GetStaffAll()
        {
            return Mapper.Map<IEnumerable<CarWashWorkersBll>>(_unitOfWork.WorkersUnitOfWork.Get());
        }

        public IEnumerable<CarWashWorkersBll> GetChooseEmployees()
        {
            return Mapper.Map<IEnumerable<CarWashWorkersBll>>(_unitOfWork.WorkersUnitOfWork
                .GetWhere(x => (x.status == "true")));
        }

        public CarWashWorkersBll CarWashWorkersId(int? id)
        {
            return Mapper.Map<CarWashWorkersBll>(_unitOfWork.WorkersUnitOfWork.GetById(id));
        }

        public void AddToCurrentShift(int? adminCarWosh, int? adminDetailing, List<int> chkRow)
        {
            if (adminCarWosh != null && adminDetailing != null)
            {
                AdninRegistr(adminCarWosh, 1);
                AdninRegistr(adminDetailing, 2);
            }
                        
            foreach (var item in chkRow)
            {
                AdninRegistr(item, 3);
            }
        }

        private void AdninRegistr(int? admin, int status)
        {
            _brigade.Date = DateTime.Now;
            _brigade.IdCarWashWorkers = admin;
            _brigade.EarlyTermination = true;
            _brigade.StatusId = status;

            brigadeForToday brigade = Mapper.Map<BrigadeForTodayBll, brigadeForToday>(_brigade);

            _unitOfWork.BrigadeForTodayUnitOfWork.Insert(brigade);
            _unitOfWork.Save();
        }

        public bool HomeEntryCondition()
        {
            string date = DateTime.Now.ToString("dd.MM.yyyy");
            var thisDay = _unitOfWork.BrigadeForTodayUnitOfWork.Get().Any(x => x.Date?.ToString("dd.MM.yyyy") == DateTime.Now.ToString("dd.MM.yyyy"));

            return thisDay;
        }

        public void InsertEmployee(CarWashWorkersBll carWashWorkersBll)
        {
            CarWashWorkers carWashWorkers = Mapper.Map<CarWashWorkersBll, CarWashWorkers>(carWashWorkersBll);
            _unitOfWork.CarWashWorkersUnitOfWork.Insert(carWashWorkers);
            _unitOfWork.Save();
        }

        public void UpdateEmploee(CarWashWorkersBll carWashWorkersId, string action)
        {
            CarWashWorkers carWashWorkers = Mapper.Map<CarWashWorkersBll, CarWashWorkers>(carWashWorkersId);

            if (action == "Delete")// перед увольнением предусмотреть  
            {
                carWashWorkers.status = "false";
                carWashWorkers.DataDismissal = DateTime.Now.ToString("dd.MM.yyyy");   /// Внесни изменения в БД!! изменить  тип домена  DateTime
            }

            _unitOfWork.CarWashWorkersUnitOfWork.Update(carWashWorkers);
            _unitOfWork.Save();
        }
    }
}

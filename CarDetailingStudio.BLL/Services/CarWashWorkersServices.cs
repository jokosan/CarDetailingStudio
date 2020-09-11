using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using DevExpress.Data.Mask;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<CarWashWorkersBll>> GetTable()
        {
            return Mapper.Map<IEnumerable<CarWashWorkersBll>>(await _unitOfWork.CarWashWorkersUnitOfWork.Get());
        }

        public async Task<IEnumerable<CarWashWorkersBll>> GetStaffAll()
        {
            return Mapper.Map<IEnumerable<CarWashWorkersBll>>(await _unitOfWork.WorkersUnitOfWork.Get());
        }

        public async Task<IEnumerable<CarWashWorkersBll>> GetChooseEmployees()
        {
            return Mapper.Map<IEnumerable<CarWashWorkersBll>>(await _unitOfWork.WorkersUnitOfWork
                .GetWhere(x => (x.status == "true")));
        }

        public async Task<CarWashWorkersBll> CarWashWorkersId(int? id)
        {
            return Mapper.Map<CarWashWorkersBll>(await _unitOfWork.WorkersUnitOfWork.GetById(id));
        }

        public async Task AddToCurrentShift(int? adminCarWosh, int? adminDetailing, List<int> chkRow)
        {
            DateTime date = DateTime.Now;
            var resultDay = await _unitOfWork.BrigadeForTodayUnitOfWork.GetWhere(x => (DbFunctions.TruncateTime(x.Date.Value) == date.Date) && (x.EarlyTermination == true));

            if (adminCarWosh != null && adminDetailing != null)
            {
                if (resultDay.Count() == 0)
                {
                    await AdninRegistr(adminCarWosh, 1);
                    await AdninRegistr(adminDetailing, 2);
                }
                else if (resultDay.Any(x => (x.StatusId == 1) && (x.IdCarWashWorkers == adminCarWosh) == false))
                {
                    await AdninRegistr(adminCarWosh, 1);
                }
                else if (resultDay.Any(x => (x.StatusId == 2) && (x.IdCarWashWorkers == adminDetailing) == false))
                {
                    await AdninRegistr(adminDetailing, 2);
                }
            }

            foreach (var item in chkRow)
            {
                if (resultDay.Count() == 0)
                {
                    await AdninRegistr(item, 3);
                }
                else
                {
                    if (resultDay.Any(x => (x.StatusId == 3) && (x.IdCarWashWorkers == item)) == false)
                    {
                        await AdninRegistr(item, 3);
                    }
                }
            }
        }

        private async Task AdninRegistr(int? admin, int status)
        {
            _brigade.Date = DateTime.Now;
            _brigade.IdCarWashWorkers = admin;
            _brigade.EarlyTermination = true;
            _brigade.StatusId = status;

            brigadeForToday brigade = Mapper.Map<BrigadeForTodayBll, brigadeForToday>(_brigade);

            _unitOfWork.BrigadeForTodayUnitOfWork.Insert(brigade);
            await _unitOfWork.Save();
        }

        public async Task<bool> HomeEntryCondition()
        {
            string date = DateTime.Now.ToString("dd.MM.yyyy");
            var thisDay = await _unitOfWork.BrigadeForTodayUnitOfWork.Get();
            var result = thisDay.Any(x => x.Date?.ToString("dd.MM.yyyy") == DateTime.Now.ToString("dd.MM.yyyy"));

            return result;
        }

        public async Task InsertEmployee(CarWashWorkersBll carWashWorkersBll)
        {
            CarWashWorkers carWashWorkers = Mapper.Map<CarWashWorkersBll, CarWashWorkers>(carWashWorkersBll);
            _unitOfWork.CarWashWorkersUnitOfWork.Insert(carWashWorkers);
            await _unitOfWork.Save();
        }

        public async Task UpdateEmploee(CarWashWorkersBll carWashWorkersId, string action)
        {
            CarWashWorkers carWashWorkers = Mapper.Map<CarWashWorkersBll, CarWashWorkers>(carWashWorkersId);

            if (action == "Delete")// перед увольнением предусмотреть  
            {
                carWashWorkers.status = "false";
                carWashWorkers.DataDismissal = DateTime.Now.ToString("dd.MM.yyyy");   /// Внесни изменения в БД!! изменить  тип домена  DateTime
            }

            _unitOfWork.CarWashWorkersUnitOfWork.Update(carWashWorkers);
            await _unitOfWork.Save();
        }

        public async Task<IEnumerable<CarWashWorkersBll>> EmployeeName()
        {
            var employee = Mapper.Map<IEnumerable<CarWashWorkersBll>>(await GetTable());

            var resultEmployee = from e1 in employee
                                 select new CarWashWorkersBll
                                 {
                                     id = e1.id,
                                     Name = $"{e1.Surname} {e1.Name} {e1.Patronymic}"
                                 };

            return resultEmployee.AsEnumerable();
        }
    }
}

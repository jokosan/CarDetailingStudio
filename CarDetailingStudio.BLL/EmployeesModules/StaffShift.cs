using CarDetailingStudio.BLL.EmployeesModules.Contract;
using CarDetailingStudio.BLL.EmployeesModules.Model;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.EmployeesModules
{
    public class StaffShift : IStaffShift
    {
        private readonly ICarWashWorkersServices _carWashWorkers;
        private readonly IBrigadeForTodayServices _brigadeForToday;
        private readonly IPremiumAndRateServices _premiumAndRate;

        public StaffShift(
            ICarWashWorkersServices carWashWorkers,
            IBrigadeForTodayServices brigadeForToday,
            IPremiumAndRateServices premiumAndRate)
        {
            _carWashWorkers = carWashWorkers;
            _brigadeForToday = brigadeForToday;
            _premiumAndRate = premiumAndRate;
        }

        private ChangeOfDay changeOfDay = new ChangeOfDay();

        public async Task<ChangeOfDay> NewShifts()
        {
            changeOfDay.DetailingAdministrator = new List<BLL.Model.CarWashWorkersBll>();
            changeOfDay.WashingAdministrator = new List<BLL.Model.CarWashWorkersBll>();
            changeOfDay.ShiftStaff = new List<BLL.Model.CarWashWorkersBll>();

            var AllEmployees = await _premiumAndRate.AllCurrentEmployees();

            foreach (var item in AllEmployees.Where(a => a.positionId == 1 && a.positionsStatus == true))
            {
                if (changeOfDay.DetailingAdministrator.Any(x => x.id == item.carWashWorkersId) == false)
                    changeOfDay.DetailingAdministrator.Add(СreateListCarWashWorkers(item));
            }

            foreach (var item in AllEmployees.Where(a => a.positionId == 2 && a.positionsStatus == true))
            {
                if (changeOfDay.WashingAdministrator.Any(x => x.id == item.carWashWorkersId) == false)
                    changeOfDay.WashingAdministrator.Add(СreateListCarWashWorkers(item));
            }

            foreach (var item in AllEmployees.Where(a => a.Position.positionsOfAdministrators == 2 && a.positionsStatus == true))
            {
                if (changeOfDay.ShiftStaff.Any(x => x.id == item.carWashWorkersId) == false)
                    changeOfDay.ShiftStaff.Add(СreateListCarWashWorkers(item));
            }

            return changeOfDay;
        }

        public async Task<ChangeOfDay> NewShifts(DateTime dateTime)
        {
            changeOfDay.DetailingAdministrator = new List<BLL.Model.CarWashWorkersBll>();
            changeOfDay.WashingAdministrator = new List<BLL.Model.CarWashWorkersBll>();
            changeOfDay.ShiftStaff = new List<BLL.Model.CarWashWorkersBll>();

            var currentShift = await _brigadeForToday.Reports(dateTime);
            var currentShiftTrue = currentShift.Where(x => x.EarlyTermination == true);
            var AllEmployees = await _premiumAndRate.AllCurrentEmployees();

            if (currentShiftTrue.Any(a => a.StatusId == 2) == false)
            {
                foreach (var item in AllEmployees.Where(a => a.positionId == 1 && a.positionsStatus == true))
                {
                    if (changeOfDay.DetailingAdministrator.Any(x => x.id == item.carWashWorkersId) == false)
                        changeOfDay.DetailingAdministrator.Add(СreateListCarWashWorkers(item));
                }
            }

            if (currentShiftTrue.Any(a => a.StatusId == 1) == false)
            {
                foreach (var item in AllEmployees.Where(a => a.positionId == 2 && a.positionsStatus == true))
                {
                    if (changeOfDay.WashingAdministrator.Any(x => x.id == item.carWashWorkersId) == false)
                        changeOfDay.WashingAdministrator.Add(СreateListCarWashWorkers(item));
                }
            }

            foreach (var item in AllEmployees.Where(a => a.Position.positionsOfAdministrators == 2 && a.positionsStatus == true))
            {
                if (currentShiftTrue.Any(x => x.IdCarWashWorkers == item.carWashWorkersId) == false)
                {
                    if (changeOfDay.ShiftStaff.Any(x => x.id == item.carWashWorkersId) == false)
                        changeOfDay.ShiftStaff.Add(СreateListCarWashWorkers(item));
                }
            }

            return changeOfDay;
        }

        public async Task<IEnumerable<CarWashWorkersBll>> EmployeeToPerformServices(int idServises, DateTime? date = null)
        {
            List<CarWashWorkersBll> carWashWorker = new List<CarWashWorkersBll>();
            List<PremiumAndRateBll> premiumAndRates = new List<PremiumAndRateBll>();

            var AllEmployees = await _premiumAndRate.AllCurrentEmployees();
            var brigade = await _brigadeForToday.GetDateTimeNow();

            switch (idServises)
            {
                case 1:
                    premiumAndRates = AllEmployees.Where(x => x.positionId == 4).ToList();
                    break;
                case 2:
                    premiumAndRates = AllEmployees.Where(x => x.positionId == 5).ToList();
                    break;
                case 3:
                    premiumAndRates = AllEmployees.Where(x => x.positionId == 6).ToList();
                    break;
                case 4:
                    premiumAndRates = AllEmployees.Where(x => x.positionId == 8).ToList();
                    break;
                case 5:
                    premiumAndRates = AllEmployees.Where(x => x.positionId == 10).ToList();
                    break;
            }

            foreach (var item in premiumAndRates)
            {
                if (brigade.Any(x => x.IdCarWashWorkers == item.carWashWorkersId) && item.positionsStatus == true)
                    carWashWorker.Add(СreateListCarWashWorkers(item));
            }

            return carWashWorker.AsEnumerable();
        }

        private CarWashWorkersBll СreateListCarWashWorkers(PremiumAndRateBll premiumAndRate)
        {
            CarWashWorkersBll carWash = new CarWashWorkersBll();

            carWash.id = premiumAndRate.carWashWorkersId;
            carWash.Name = premiumAndRate.CarWashWorkers.Name;
            carWash.Surname = premiumAndRate.CarWashWorkers.Surname;
            carWash.Patronymic = premiumAndRate.CarWashWorkers.Patronymic;
            carWash.MobilePhone = premiumAndRate.CarWashWorkers.MobilePhone;

            return carWash;
        }
    }
}

using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.Modules.EmployeeRate;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Modules.EmployeeRate
{
    public class EmployeeRateModules : IEmployeeRateModules
    {       
        private readonly IUnitOfWork _unitOfWork;
  
        public EmployeeRateModules(
            IUnitOfWork unitOfWork)
        {           
            _unitOfWork = unitOfWork;
        }

        public async Task ClosingShift(IEnumerable<BrigadeForTodayBll> brigadeForTodays)
        {
            var groupBrigadeForTodays = brigadeForTodays.GroupBy(x => x.IdCarWashWorkers)
                                                        .Select(y => new BrigadeForTodayBll
                                                        {
                                                            id = y.First().id,
                                                            Date = y.First().Date,
                                                            EndTime = y.First().EndTime,
                                                            EarlyTermination = y.First().EarlyTermination,
                                                            IdCarWashWorkers = y.First().IdCarWashWorkers,
                                                            StatusId = Convert.ToInt32(y.First().CarWashWorkers.rate)
                                                        }); ;

            foreach (var item in groupBrigadeForTodays)
            {
                if (item.StatusId != null)
                {
                    if (item.StatusId != 0)
                    {
                        await EndDayRateCalculation(item);
                    }                 
                }               
            }
        }

        public async Task EndDayRateCalculation(BrigadeForTodayBll brigadeForToday)
        {
            var employeeRate = new DAL.EmployeeRate();

            int dateStart = brigadeForToday.Date.Value.Hour;
            int workingHours = brigadeForToday.EndTime.Value.Hour - dateStart;
            double hourlyRate = (double)brigadeForToday.StatusId.Value / 12;

            employeeRate.brigadeForTodayId = brigadeForToday.id;
            employeeRate.numberHoursWorked = workingHours;
            employeeRate.hourlyRate = hourlyRate;
            employeeRate.wage = OpeningHours(workingHours, hourlyRate);
          
            _unitOfWork.EmployeeRateUnitOfWork.Insert(employeeRate);
            await _unitOfWork.Save();
        }

        private double OpeningHours(int WorkingHours, double rate)
        {
            if (WorkingHours >= 12)
            {
                return (double)12 * rate;
            }
            else
            {
                return (double)WorkingHours * rate;
            }
        }
    }
}

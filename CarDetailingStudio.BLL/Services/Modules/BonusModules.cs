using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Model.ModelViewBll;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Modules
{
    public class BonusModules : IBonusModules
    {
        private IBonusToSalary _bonus;
        private IOrderCarWashWorkersServices _orderCarWashWorkers;

        public BonusModules(IBonusToSalary bonus, IOrderCarWashWorkersServices orderCarWashWorkersServices)
        {
            _bonus = bonus;
            _orderCarWashWorkers = orderCarWashWorkersServices;
        }

        public async Task PremiumAccrual(int carWashWorkers, double payroll)
        {
            BonusToSalaryBll bonusToSalaryResult = new BonusToSalaryBll();

            double bonusStart = 1500;
            double bonusRresult = Math.Floor(payroll / bonusStart);

            if (bonusRresult > 0)
            {
                bonusToSalaryResult.amount = bonusRresult * 50;
                bonusToSalaryResult.carWashWorkersId = carWashWorkers;
                bonusToSalaryResult.date = DateTime.Now;
                bonusToSalaryResult.note = $"Премия за кассу { payroll }";

                await _bonus.Insert(bonusToSalaryResult);
            }
        }
    }
}

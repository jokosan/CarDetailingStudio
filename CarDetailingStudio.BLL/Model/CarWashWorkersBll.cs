using System;
using System.Collections.Generic;

namespace CarDetailingStudio.BLL.Model
{
    public class CarWashWorkersBll
    {
        public CarWashWorkersBll()
        {
            this.brigadeForToday = new HashSet<BrigadeForTodayBll>();
            this.Credit = new HashSet<CreditBll>();
            this.OrderCarWashWorkers = new HashSet<OrderCarWashWorkersBll>();
            this.salaryBalance = new HashSet<SalaryBalanceBll>();
            this.salaryExpenses = new HashSet<SalaryExpensesBll>();
        }

        public int id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string MobilePhone { get; set; }
        public string Experience { get; set; }
        public Nullable<int> AdministratorsInterestRate { get; set; }
        public Nullable<int> InterestRate { get; set; }
        public Nullable<double> rate { get; set; }
        public string DataRegistration { get; set; }
        public string DataDismissal { get; set; }
        public string status { get; set; }
        public string Photo { get; set; }
        public Nullable<int> IdPosition { get; set; }

        public virtual ICollection<BonusToSalaryBll> bonusToSalary { get; set; }
        public virtual ICollection<BrigadeForTodayBll> brigadeForToday { get; set; }
        public virtual JobTitleTableBll JobTitleTable { get; set; }
        public virtual ICollection<CreditBll> Credit { get; set; }
        public virtual ICollection<OrderCarWashWorkersBll> OrderCarWashWorkers { get; set; }
        public virtual ICollection<SalaryExpensesBll> salaryExpenses { get; set; }
        public virtual ICollection<SalaryBalanceBll> salaryBalance { get; set; }
    }
}

using CarDetailingStudio.DAL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.DAL.Utilities.UnitOfWorks
{
    public partial class UnitOfWork 
    {
        private DbRepository<expenseCategory> expenseCategoryUW;
        private DbRepository<salaryExpenses> salaryExpensesUW;
        private DbRepository<utilityCosts> utilityCostsUW;
        private DbRepository<otherExpenses> otherExpensesUW;
        private DbRepository<costsCarWashAndDeteyling> costsCarWashAndDeteylingUW;
        private DbRepository<consumablesTireFitting> consumablesTireFittingUW;


        public DbRepository<expenseCategory> expenseCategoryUnitOfWork
        {
            get => expenseCategoryUW ?? (expenseCategoryUW = new DbRepository<expenseCategory>(_entities));
            set => expenseCategoryUW = value;
        }

        public DbRepository<salaryExpenses> salaryExpensesUnitOfWork 
        {
            get => salaryExpensesUW ?? (salaryExpensesUW = new DbRepository<salaryExpenses>(_entities));
            set => salaryExpensesUW = value;
        }

        public DbRepository<utilityCosts> utilityCostsUnitOfWork 
        {
            get => utilityCostsUW ?? (utilityCostsUW = new DbRepository<utilityCosts>(_entities));
            set => utilityCostsUW = value;
        }

        public DbRepository<otherExpenses> otherExpensesUnitOfWork 
        {
            get => otherExpensesUW ?? (otherExpensesUW = new DbRepository<otherExpenses>(_entities));
            set => otherExpensesUW = value;
        }
        
        public DbRepository<costsCarWashAndDeteyling> costsCarWashAndDeteylingUnitOfWork
        {
            get => costsCarWashAndDeteylingUW ?? (costsCarWashAndDeteylingUW = new DbRepository<costsCarWashAndDeteyling>(_entities));
            set => costsCarWashAndDeteylingUW = value;
        }

        public DbRepository<consumablesTireFitting> consumablesTireFittingUnitOfWork 
        {
            get => consumablesTireFittingUW ?? (consumablesTireFittingUW = new DbRepository<consumablesTireFitting>(_entities));
            set => consumablesTireFittingUW = value;
        }
    }
}

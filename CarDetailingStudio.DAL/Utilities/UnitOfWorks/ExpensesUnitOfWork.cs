using CarDetailingStudio.DAL.Infrastructure;

namespace CarDetailingStudio.DAL.Utilities.UnitOfWorks
{
    public partial class UnitOfWork
    {
        private DbRepository<expenseCategory> expenseCategoryUW;
        private DbRepository<salaryExpenses> salaryExpensesUW;
        private DbRepository<utilityCosts> utilityCostsUW;
        private DbRepository<Expenses> ExpensesUW;


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

        public DbRepository<Expenses> ExpensesUnitOfWork
        {
            get => ExpensesUW ?? (ExpensesUW = new DbRepository<Expenses>(_entities));
            set => ExpensesUW = value;
        }

     
    }
}

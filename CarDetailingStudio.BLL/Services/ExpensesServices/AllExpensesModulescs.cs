using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.ExpensesServices.ExpensesContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.ExpensesServices
{
    public class AllExpensesModulescs : IAllExpenses
    {
        private IUtilityCosts _utilityCosts;
        private IExpenses _expenses;
       
        public AllExpensesModulescs(
            IUtilityCosts utilityCosts,
            IExpenses expenses)      
        {
            _utilityCosts = utilityCosts;
            _expenses = expenses;
        }

        public async Task SaveExpenses(AllExpensesBll allExpenses)
        {           
            if (allExpenses.expenseCategoryId != 5)
            {
                await ExpensesInsert(allExpenses);
            }
            else if (allExpenses.expenseCategoryId == 5)
            {
                int idExpense = await ExpensesInsert(allExpenses);

                UtilityCostsBll utilityCosts = new UtilityCostsBll();

                utilityCosts.indicationCounter = allExpenses.indicationCounter;
                utilityCosts.utilityCostsCategoryId = allExpenses.utilityCostsCategoryId;
                utilityCosts.expenseId = idExpense;

                await _utilityCosts.Insert(utilityCosts);
            }
        }

        public async Task<int> ExpensesInsert(AllExpensesBll allExpenses)
        {
            ExpensesBll expenses = new ExpensesBll();

            expenses.dateExpenses = allExpenses.dateExpenses;
            expenses.expenseCategoryId = allExpenses.expenseCategoryId.Value;
            expenses.Amount = allExpenses.amount;
            expenses.note = allExpenses.nameExpenses;
            expenses.paymentType = allExpenses.paymentType;
            expenses.idCostCategories = allExpenses.utilityCostsCategoryId;

            int idExpenses = await _expenses.InsertId(expenses);

            return idExpenses;
        }
    }
}

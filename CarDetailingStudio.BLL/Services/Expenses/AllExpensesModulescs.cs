using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Expenses.ExpensesContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Expenses
{
    public class AllExpensesModulescs : IAllExpenses
    {
        private IUtilityCosts _utilityCosts;                             // Коммунальные услуги
        private IOtherExpenses _otherExpenses;                           // Прочие расходы
        private ICostsCarWashAndDeteyling _costsCarWashAndDeteyling;     // Расходы мойка и детейлин   

        public AllExpensesModulescs(IUtilityCosts utilityCosts, IOtherExpenses otherExpenses, ICostsCarWashAndDeteyling costsCarWashAndDeteyling)
        {
            _utilityCosts = utilityCosts;
            _otherExpenses = otherExpenses;
            _costsCarWashAndDeteyling = costsCarWashAndDeteyling;
        }

        public async Task SaveExpenses(AllExpensesBll allExpenses)
        {
            if (allExpenses.expenseCategoryId == 2 || allExpenses.expenseCategoryId == 3)
            {
                CostsCarWashAndDeteylingBll costsCarWash = new CostsCarWashAndDeteylingBll();
                
                costsCarWash.nameExpenses = allExpenses.nameExpenses;
                costsCarWash.expenseCategoryId = allExpenses.expenseCategoryId;
                costsCarWash.dateExpenses = allExpenses.dateExpenses;
                costsCarWash.amount = allExpenses.amount;
                costsCarWash.typeServicesId = allExpenses.typeServicesId;

                await _costsCarWashAndDeteyling.Insert(costsCarWash);

            }
            else if (allExpenses.expenseCategoryId == 5)
            {
                UtilityCostsBll utilityCosts = new UtilityCostsBll();

                utilityCosts.indicationCounter = allExpenses.indicationCounter;
                utilityCosts.amount = allExpenses.amount;
                utilityCosts.dateExpenses = allExpenses.dateExpenses;
                utilityCosts.expenseCategoryId = allExpenses.expenseCategoryId;
                utilityCosts.utilityCostsCategoryId = allExpenses.utilityCostsCategoryId;

                await _utilityCosts.Insert(utilityCosts);

            }
            else if (allExpenses.expenseCategoryId == 6)
            {
                OtherExpensesBll otherExpenses = new OtherExpensesBll();

                otherExpenses.nameExpenses = allExpenses.nameExpenses;
                otherExpenses.amount = allExpenses.amount;
                otherExpenses.dateExpenses = allExpenses.dateExpenses;
                otherExpenses.expenseCategoryId = allExpenses.expenseCategoryId;

                await _otherExpenses.Insert(otherExpenses);
            }
        }
    }
}

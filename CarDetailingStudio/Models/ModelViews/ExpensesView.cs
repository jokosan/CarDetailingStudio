using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class ExpensesView
    {
        public ExpensesView()
        {
            this.salaryExpenses = new HashSet<SalaryExpensesView>();
            this.utilityCosts = new HashSet<UtilityCostsView>();
            this.procurement = new HashSet<ProcurementView>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idExpenses { get; set; }
        public int expenseCategoryId { get; set; }
        [Display(Name = "Дата")]
        public Nullable<System.DateTime> dateExpenses { get; set; }
        [Display(Name = "Сумма расходов")]
        public Nullable<double> Amount { get; set; }
        public Nullable<int> paymentType { get; set; }
        [Display (Name ="Примечания")]
        public string note { get; set; }
        public Nullable<int> idCostCategories { get; set; }

        public virtual ExpenseCategoryView expenseCategory { get; set; }
        public virtual ICollection<SalaryExpensesView> salaryExpenses { get; set; }
        public virtual ICollection<UtilityCostsView> utilityCosts { get; set; }
        public virtual CostCategoriesView costCategories { get; set; }
        public virtual ICollection<ProcurementView> procurement { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class ExpenseCategoryView
    {     
        public ExpenseCategoryView()
        {
            this.consumablesTireFitting = new HashSet<ConsumablesTireFittingView>();
            this.costsCarWashAndDeteyling = new HashSet<CostsCarWashAndDeteylingView>();
            this.otherExpenses = new HashSet<OtherExpensesView>();
            this.salaryExpenses = new HashSet<SalaryExpensesView>();
            this.utilityCosts = new HashSet<UtilityCostsView>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idExpenseCategory { get; set; }
        public string name { get; set; }

       
        public virtual ICollection<ConsumablesTireFittingView> consumablesTireFitting { get; set; }
        public virtual ICollection<CostsCarWashAndDeteylingView> costsCarWashAndDeteyling { get; set; }
        public virtual ICollection<OtherExpensesView> otherExpenses { get; set; }
        public virtual ICollection<SalaryExpensesView> salaryExpenses { get; set; }
        public virtual ICollection<UtilityCostsView> utilityCosts { get; set; }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarDetailingStudio.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class salaryExpenses
    {
        public int idSalaryExpenses { get; set; }
        public Nullable<int> idCarWashWorkers { get; set; }
        public Nullable<double> amount { get; set; }
        public Nullable<System.DateTime> dateExpenses { get; set; }
        public Nullable<int> expenseCategoryId { get; set; }
    
        public virtual CarWashWorkers CarWashWorkers { get; set; }
        public virtual expenseCategory expenseCategory { get; set; }
    }
}
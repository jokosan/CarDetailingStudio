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
    
    public partial class costCategories
    {
        public int idCostCategories { get; set; }
        public Nullable<int> typeOfExpenses { get; set; }
        public string Name { get; set; }
    
        public virtual expenseCategory expenseCategory { get; set; }
    }
}

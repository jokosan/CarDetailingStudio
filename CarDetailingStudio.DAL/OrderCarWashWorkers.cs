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
    
    public partial class OrderCarWashWorkers
    {
        public int Id { get; set; }
        public int IdOrder { get; set; }
        public int IdCarWashWorkers { get; set; }
        public Nullable<bool> CalculationStatus { get; set; }
        public Nullable<double> Payroll { get; set; }
        public Nullable<System.DateTime> salaryDate { get; set; }
        public Nullable<bool> closedDayStatus { get; set; }
        public Nullable<int> typeServicesId { get; set; }
    
        public virtual CarWashWorkers CarWashWorkers { get; set; }
        public virtual OrderServicesCarWash OrderServicesCarWash { get; set; }
    }
}

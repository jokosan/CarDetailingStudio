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
    
    public partial class brigadeForToday
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public brigadeForToday()
        {
            this.EmployeeRate = new HashSet<EmployeeRate>();
        }
    
        public int id { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public Nullable<bool> EarlyTermination { get; set; }
        public Nullable<int> IdCarWashWorkers { get; set; }
        public Nullable<int> StatusId { get; set; }
    
        public virtual CarWashWorkers CarWashWorkers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeRate> EmployeeRate { get; set; }
    }
}

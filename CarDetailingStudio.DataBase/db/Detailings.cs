//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarDetailingStudio.DataBase.db
{
    using System;
    using System.Collections.Generic;
    
    public partial class Detailings
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Detailings()
        {
            this.ServisesCarWashOrder = new HashSet<ServisesCarWashOrder>();
        }
    
        public int Id { get; set; }
        public string services_list { get; set; }
        public string validity { get; set; }
        public string note { get; set; }
        public Nullable<double> S { get; set; }
        public Nullable<double> M { get; set; }
        public Nullable<double> L { get; set; }
        public Nullable<double> XL { get; set; }
        public string group { get; set; }
        public string status { get; set; }
        public string currency { get; set; }
        public Nullable<bool> mark { get; set; }
        public Nullable<int> IdGroupWashServices { get; set; }
    
        public virtual GroupWashServices GroupWashServices { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServisesCarWashOrder> ServisesCarWashOrder { get; set; }
    }
}

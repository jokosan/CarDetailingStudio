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
    
    public partial class WashServices
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WashServices()
        {
            this.ServisesCarWashOrder = new HashSet<ServisesCarWashOrder>();
        }
    
        public int id { get; set; }
        public string Services_list { get; set; }
        public Nullable<double> S { get; set; }
        public Nullable<double> M { get; set; }
        public Nullable<double> L { get; set; }
        public Nullable<double> XL { get; set; }
        public int IdGroupWashServices { get; set; }
        public Nullable<bool> mark { get; set; }
    
        public virtual GroupWashServices GroupWashServices { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServisesCarWashOrder> ServisesCarWashOrder { get; set; }
    }
}

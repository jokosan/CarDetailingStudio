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
    
    public partial class ServisesCarWashOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ServisesCarWashOrder()
        {
            this.OrderServicesCarWash = new HashSet<OrderServicesCarWash>();
        }
    
        public int Id { get; set; }
        public int IdClientsOfCarWash { get; set; }
        public int IdOrderServicesCarWash { get; set; }
        public Nullable<int> idCarWashWorkers { get; set; }
        public Nullable<int> IdWashServices { get; set; }
        public Nullable<double> Price { get; set; }
    
        public virtual Detailings Detailings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderServicesCarWash> OrderServicesCarWash { get; set; }
        public virtual WashServices WashServices { get; set; }
    }
}

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
    
    public partial class ClientsOfCarWash
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClientsOfCarWash()
        {
            this.OrderServicesCarWash = new HashSet<OrderServicesCarWash>();
        }
    
        public int id { get; set; }
        public Nullable<int> IdClientsGroups { get; set; }
        public Nullable<int> IdMark { get; set; }
        public Nullable<int> IdModel { get; set; }
        public Nullable<int> IdBody { get; set; }
        public Nullable<int> IdInfoClient { get; set; }
        public string NumberCar { get; set; }
        public Nullable<int> discont { get; set; }
        public Nullable<bool> arxiv { get; set; }
    
        public virtual car_mark car_mark { get; set; }
        public virtual car_model car_model { get; set; }
        public virtual CarBody CarBody { get; set; }
        public virtual ClientInfo ClientInfo { get; set; }
        public virtual ClientsGroups ClientsGroups { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderServicesCarWash> OrderServicesCarWash { get; set; }
    }
}

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
    
    public partial class ClientsOfCarWash
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClientsOfCarWash()
        {
            this.OrderServicesCarWash = new HashSet<OrderServicesCarWash>();
        }
    
        public int ib { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string PatronymicName { get; set; }
        public string phone { get; set; }
        public Nullable<System.DateTime> DateRegistration { get; set; }
        public string Email { get; set; }
        public string discont { get; set; }
        public string Recommendation { get; set; }
        public string NumderCar { get; set; }
        public Nullable<int> IdClientsGroups { get; set; }
        public Nullable<int> Idmark { get; set; }
        public Nullable<int> Idmodel { get; set; }
        public Nullable<int> IdBody { get; set; }
        public string note { get; set; }
        public string barcode { get; set; }
    
        public virtual car_mark car_mark { get; set; }
        public virtual car_model car_model { get; set; }
        public virtual CarBody CarBody { get; set; }
        public virtual ClientsGroups ClientsGroups { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderServicesCarWash> OrderServicesCarWash { get; set; }
    }
}

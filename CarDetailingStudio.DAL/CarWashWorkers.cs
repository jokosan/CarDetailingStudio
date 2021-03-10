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
    
    public partial class CarWashWorkers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CarWashWorkers()
        {
            this.bonusToSalary = new HashSet<bonusToSalary>();
            this.brigadeForToday = new HashSet<brigadeForToday>();
            this.Credit = new HashSet<Credit>();
            this.OrderCarWashWorkers = new HashSet<OrderCarWashWorkers>();
            this.premiumAndRate = new HashSet<premiumAndRate>();
            this.salaryArchive = new HashSet<salaryArchive>();
            this.salaryBalance = new HashSet<salaryBalance>();
            this.salaryExpenses = new HashSet<salaryExpenses>();
        }
    
        public int id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string MobilePhone { get; set; }
        public string Experience { get; set; }
        public Nullable<int> AdministratorsInterestRate { get; set; }
        public Nullable<int> InterestRate { get; set; }
        public Nullable<double> rate { get; set; }
        public string DataRegistration { get; set; }
        public string DataDismissal { get; set; }
        public string status { get; set; }
        public string Photo { get; set; }
        public Nullable<int> IdPosition { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<bonusToSalary> bonusToSalary { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<brigadeForToday> brigadeForToday { get; set; }
        public virtual JobTitleTable JobTitleTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Credit> Credit { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderCarWashWorkers> OrderCarWashWorkers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<premiumAndRate> premiumAndRate { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<salaryArchive> salaryArchive { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<salaryBalance> salaryBalance { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<salaryExpenses> salaryExpenses { get; set; }
    }
}

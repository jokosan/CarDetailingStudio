﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class carWashEntities : DbContext
    {
        public carWashEntities()
            : base("name=carWashEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<brigadeForToday> brigadeForToday { get; set; }
        public virtual DbSet<car_generation> car_generation { get; set; }
        public virtual DbSet<car_mark> car_mark { get; set; }
        public virtual DbSet<car_model> car_model { get; set; }
        public virtual DbSet<car_modification> car_modification { get; set; }
        public virtual DbSet<car_serie> car_serie { get; set; }
        public virtual DbSet<CarBody> CarBody { get; set; }
        public virtual DbSet<CarWashWorkers> CarWashWorkers { get; set; }
        public virtual DbSet<ClientsGroups> ClientsGroups { get; set; }
        public virtual DbSet<ClientsOfCarWash> ClientsOfCarWash { get; set; }
        public virtual DbSet<Detailings> Detailings { get; set; }
        public virtual DbSet<GroupWashServices> GroupWashServices { get; set; }
        public virtual DbSet<JobTitleTable> JobTitleTable { get; set; }
        public virtual DbSet<OrderServicesCarWash> OrderServicesCarWash { get; set; }
        public virtual DbSet<PaymentState> PaymentState { get; set; }
        public virtual DbSet<ServisesCarWashOrder> ServisesCarWashOrder { get; set; }
        public virtual DbSet<StatusOrder> StatusOrder { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<logo_mark_car> logo_mark_car { get; set; }
    }
}

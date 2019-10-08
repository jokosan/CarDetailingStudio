using CarDetailingStudio.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models
{
    public class CarDetailingStudioContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public CarDetailingStudioContext() : base("name=CarDetailingStudioContext")
        {
        }

        //public System.Data.Entity.DbSet<OrderServicesCarWashView> OrderServicesCarWashViews { get; set; }
        public DbSet<BrigadeForTodayView> brigadeForToday { get; set; }
        public DbSet<CarGenerationView> car_generation { get; set; }
        public DbSet<CarMarkView> car_mark { get; set; }
        public DbSet<CarModelView> car_model { get; set; }
        public DbSet<CarModificationView> car_modification { get; set; }
        public DbSet<CarSerieView> car_serie { get; set; }
        public DbSet<CarBodyView> CarBody { get; set; }
        public DbSet<CarWashWorkersView> CarWashWorkers { get; set; }
        public DbSet<ClientsGroupsView> ClientsGroups { get; set; }
        public DbSet<ClientsOfCarWashView> ClientsOfCarWash { get; set; }
        public DbSet<DetailingsView> Detailings { get; set; }
        public DbSet<GroupWashServicesView> GroupWashServices { get; set; }
        public DbSet<JobTitleTableView> JobTitleTable { get; set; }
        public DbSet<OrderServicesCarWashView> OrderServicesCarWash { get; set; }
        public DbSet<ServisesCarWashOrderView> ServisesCarWashOrder { get; set; }

        public System.Data.Entity.DbSet<CarDetailingStudio.Models.CloseOrderModel> CloseOrderModels { get; set; }
    }
}

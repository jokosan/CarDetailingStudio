﻿using CarDetailingStudio.Models.ModelViews;
using Microsoft.Owin.Security;
using System.Data.Entity;

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

        public System.Data.Entity.DbSet<CarDetailingStudio.Models.ModelViews.OrderInfoViewModel> OrderInfoViewModels { get; set; }

        public System.Data.Entity.DbSet<CarDetailingStudio.Models.ModelViews.OrderCarWashWorkersView> OrderCarWashWorkersViews { get; set; }

        public System.Data.Entity.DbSet<CarDetailingStudio.Models.ClientView> ClientViews { get; set; }

        public System.Data.Entity.DbSet<CarDetailingStudio.Models.ModelViews.ClientInfoView> ClientCarViews { get; set; }

        public System.Data.Entity.DbSet<CarDetailingStudio.Models.ModelViews.CreditView> CreditViews { get; set; }

        public System.Data.Entity.DbSet<CarDetailingStudio.Models.DayResultModelView> DayResultModelViews { get; set; }

        public System.Data.Entity.DbSet<CarDetailingStudio.Models.ModelViews.SalaryExpensesView> SalaryExpensesViews { get; set; }

        public System.Data.Entity.DbSet<CarDetailingStudio.Models.ModelViews.UtilityCostsView> UtilityCostsViews { get; set; }

        public System.Data.Entity.DbSet<CarDetailingStudio.Models.WagesForDaysWorkedView> WagesForDaysWorkedViews { get; set; }

        public System.Data.Entity.DbSet<CarDetailingStudio.Models.ModelViews.TireStorageView> TireStorageViews { get; set; }

        public System.Data.Entity.DbSet<CarDetailingStudio.Models.OrderTireStorageModelView> OrderTireStorageModelViews { get; set; }

        public System.Data.Entity.DbSet<CarDetailingStudio.Models.ModelViews.OrderCarpetWashingView> OrderCarpetWashingViews { get; set; }

        public System.Data.Entity.DbSet<CarDetailingStudio.Models.ModelViews.BonusToSalaryView> BonusToSalaryViews { get; set; }

        public System.Data.Entity.DbSet<CarDetailingStudio.Models.ClientJoinCarpetWashingModelView> ClientJoinCarpetWashingModelViews { get; set; }

        public System.Data.Entity.DbSet<CarDetailingStudio.Models.AllExpenses> AllExpenses { get; set; }

        public System.Data.Entity.DbSet<CarDetailingStudio.Models.ModelViews.CashierView> CashierViews { get; set; }

        public System.Data.Entity.DbSet<CarDetailingStudio.Models.ModelViews.CostCategoriesView> CostCategoriesViews { get; set; }

        public System.Data.Entity.DbSet<CarDetailingStudio.Models.ModelViews.TireServiceView> TireServiceViews { get; set; }

        public System.Data.Entity.DbSet<CarDetailingStudio.Models.ModelViews.TireChangeServiceView> TireChangeServiceViews { get; set; }

        public System.Data.Entity.DbSet<CarDetailingStudio.Models.ModelViews.PriceListTireFittingAdditionalServicesView> PriceListTireFittingAdditionalServicesViews { get; set; }

        public System.Data.Entity.DbSet<CarDetailingStudio.Models.ModelViews.TireStorageServicesView> TireStorageServicesViews { get; set; }

        public System.Data.Entity.DbSet<CarDetailingStudio.Models.ModelViews.PriceListTireFittingView> PriceListTireFittingViews { get; set; }

        public System.Data.Entity.DbSet<CarDetailingStudio.Models.ModelViews.ExpensesView> ExpensesViews { get; set; }

        public System.Data.Entity.DbSet<CarDetailingStudio.Models.ModelViews.ListOfGoodsView> ListOfGoodsViews { get; set; }

        public System.Data.Entity.DbSet<CarDetailingStudio.Models.ModelViews.GoodsSoldView> GoodsSoldViews { get; set; }

        public System.Data.Entity.DbSet<CarDetailingStudio.Models.ModelViews.AdditionalIncomeView> AdditionalIncomeViews { get; set; }

        public System.Data.Entity.DbSet<CarDetailingStudio.Models.ModelViews.SalaryArchiveView> SalaryArchiveViews { get; set; }

        public System.Data.Entity.DbSet<CarDetailingStudio.Models.ModelViews.EmployeeRateView> EmployeeRateViews { get; set; }

        public System.Data.Entity.DbSet<CarDetailingStudio.Models.ModelViews.PremiumAndRateView> PremiumAndRateViews { get; set; }

        public System.Data.Entity.DbSet<CarDetailingStudio.Models.ModelViews.ExpenseCategoryView> ExpenseCategoryViews { get; set; }

        public System.Data.Entity.DbSet<CarDetailingStudio.Models.ModelViews.UtilityCostsCategoryView> UtilityCostsCategoryViews { get; set; }
    }
}

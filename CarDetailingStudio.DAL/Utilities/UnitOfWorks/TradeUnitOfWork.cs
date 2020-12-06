using CarDetailingStudio.DAL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.DAL.Utilities.UnitOfWorks
{
    public partial class UnitOfWork
    {
        private DbRepository<ProductCategories> ProductCategoriesUW { get; set; }
        private DbRepository<listOfGoods> ListOfGoodsUW { get; set; }
        private DbRepository<goodsSold> GoodsSoldUW { get; set; }
        private DbRepository<procurement> ProcurementUW { get; set; }

        public DbRepository<ProductCategories> ProductCategoriesUnitOfWork
        {
            get => ProductCategoriesUW ?? (ProductCategoriesUW = new DbRepository<ProductCategories>(_entities));
            set => ProductCategoriesUW = value;
        }

        public DbRepository<listOfGoods> ListOfGoodsUnitOfWork
        {
            get => ListOfGoodsUW ?? (ListOfGoodsUW = new DbRepository<listOfGoods>(_entities));
            set => ListOfGoodsUW = value;
        }

        public DbRepository<goodsSold> GoodsSoldUnitOfWork
        {
            get => GoodsSoldUW ?? (GoodsSoldUW = new DbRepository<goodsSold>(_entities));
            set => GoodsSoldUW = value;
        }

        public DbRepository<procurement> ProcurementUnitOfWork
        {
            get => ProcurementUW ?? (ProcurementUW = new DbRepository<procurement>(_entities));
            set => ProcurementUW = value;
        }
    }
}

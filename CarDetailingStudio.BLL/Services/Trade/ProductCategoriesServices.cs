using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Trade.TradeContract;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;

namespace CarDetailingStudio.BLL.Services.Trade
{
    public class ProductCategoriesServices : IProductCategories
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductCategoriesServices(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProductCategoriesBll>> GetTableAll() => Mapper.Map<IEnumerable<ProductCategoriesBll>>(await _unitOfWork.ProductCategoriesUnitOfWork.Get());

        public async Task<ProductCategoriesBll> SelectId(int? elementId) => Mapper.Map<ProductCategoriesBll>(await _unitOfWork.ProductCategoriesUnitOfWork.GetById(elementId));

        public Task Insert(ProductCategoriesBll element)
        {
            throw new NotImplementedException();
        }

        public Task Update(ProductCategoriesBll elementToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}

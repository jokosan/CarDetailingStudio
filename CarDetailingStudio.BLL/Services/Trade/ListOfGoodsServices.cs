using CarDetailingStudio.BLL.Services.Trade.TradeContract;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.DAL;

namespace CarDetailingStudio.BLL.Services.Trade
{
    public class ListOfGoodsServices : IListOfGoods
    {
        private readonly IUnitOfWork _unitOfWork;

        public ListOfGoodsServices(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ListOfGoodsBll>> GetTableAll() => Mapper.Map<IEnumerable<ListOfGoodsBll>>(await _unitOfWork.ListOfGoodsUnitOfWork.GetInclude("ProductCategories"));
        public async Task<IEnumerable<ListOfGoodsBll>> GetTableAll(int idCategories) => Mapper.Map<IEnumerable<ListOfGoodsBll>>(await _unitOfWork
                                                       .ListOfGoodsUnitOfWork
                                                       .QueryObjectGraph(x => x.productCategoriesId == idCategories
                                                       , "ProductCategories"));

        public async Task<ListOfGoodsBll> SelectId(int? elementId) => Mapper.Map<ListOfGoodsBll>(await _unitOfWork.ListOfGoodsUnitOfWork.GetById(elementId));

        public async Task Insert(ListOfGoodsBll element)
        {
            _unitOfWork.ListOfGoodsUnitOfWork.Insert(TransformEntity(element));
            await _unitOfWork.Save();
        }
        
        public async Task Update(ListOfGoodsBll elementToUpdate)
        {
            _unitOfWork.ListOfGoodsUnitOfWork.Update(TransformEntity(elementToUpdate));
            await _unitOfWork.Save();
        }

        private listOfGoods TransformEntity(ListOfGoodsBll entity) => Mapper.Map<ListOfGoodsBll, listOfGoods>(entity);
    }
}

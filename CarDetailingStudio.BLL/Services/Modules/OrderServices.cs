using CarDetailingStudio.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using System.Linq.Expressions;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;

namespace CarDetailingStudio.BLL.Services.Modules
{
    public class OrderServices
    {
        private IUnitOfWork _unitOfWork;

        public OrderServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public static int? idClient { get; set; }
        public static string body { get; set; }

        public static List<int> IdOrders = new List<int>();
        public static List<DetailingsBll> OrderList = new List<DetailingsBll>();

        private IEnumerable<DetailingsBll> Atest { get; set; }

        public void IdOrderServices(FormCollection collection)
        {
            int x = collection.Count;
            IdOrders.Clear();

            string IdNewOrder = collection[0];

            string[] Id = IdNewOrder.Split(',');

            foreach (var item in Id)
            {
                IdOrders.Add(Convert.ToInt32(item));
            }
        }

        public void OrderPreview()
        {
            var AllDetailings = Mapper.Map<IEnumerable<DetailingsBll>>(_unitOfWork.DetailingsUnitOfWork.Get());

            Atest = AllDetailings.Where(a => IdOrders.Contains(a.Id));

            foreach (var i in Atest)
            {
                OrderList.Add(new DetailingsBll
                {
                    Id = i.Id,
                    services_list = i.services_list,
                    S = i.S,
                    M = i.M,
                    L = i.M,
                    XL = i.XL
                });
            }
        }
    }
}

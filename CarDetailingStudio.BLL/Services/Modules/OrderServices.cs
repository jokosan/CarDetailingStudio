using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CarDetailingStudio.BLL.Services.Modules
{
    public class OrderServices : IOrderServices
    {
        private IUnitOfWork _unitOfWork;

        public OrderServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public static int? idClient { get; set; }
        public static string body { get; set; }
        public static double Price { get; set; }

        public static List<int> IdOrders = new List<int>();
        public static List<DetailingsBll> OrderList = new List<DetailingsBll>();

        private IEnumerable<DetailingsBll> Atest { get; set; }

        public void ClearListOrder()
        {
            OrderList.Clear();
        }

        public void IdOrderServices(FormCollection collection)
        {
            IdOrders.Clear();

            string IdNewOrder = collection[0];

            string[] Id = IdNewOrder.Split(',');

            foreach (var item in Id)
            {
                IdOrders.Add(Convert.ToInt32(item));
            }
        }

        public void IdOrderServices(List<int> idServises)
        {
            IdOrders.Clear();

            foreach (var item in idServises)
            {
                IdOrders.Add(item);
            }
        }


        public async Task OrderPreview()
        {
            var AllDetailings = Mapper.Map<IEnumerable<DetailingsBll>>(await _unitOfWork.DetailingsUnitOfWork.Get());

            Atest = AllDetailings.Where(a => IdOrders.Contains(a.Id));

            foreach (var i in Atest)
            {
                OrderList.Add(new DetailingsBll
                {
                    Id = i.Id,
                    services_list = i.services_list,
                    S = i.S,
                    M = i.M,
                    L = i.L,
                    XL = i.XL
                });
            }
        }

        public double? OrderPrice()
        {
            var result = OrderList;

            switch (body)
            {
                case "S":
                    return result.Sum(x => x.S);

                case "M":
                    return result.Sum(x => x.M);

                case "L":
                    return result.Sum(x => x.L);

                case "XL":
                    return result.Sum(x => x.XL);
            }

            return null;
        }

        public double? Discont(int? discont, double? sum)
        {
            double? result = sum / 100 * discont;
            return sum - result;
        }
    }
}


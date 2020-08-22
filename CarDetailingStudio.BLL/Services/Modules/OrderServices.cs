using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Checkout;
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
        private int TwoCount { get; set; }

        public static List<int> IdOrders = new List<int>();
        public static Dictionary<int, int> CountServic = new Dictionary<int, int>();
        public static List<DetailingsBll> OrderList = new List<DetailingsBll>();

        private IEnumerable<DetailingsBll> Atest { get; set; }

        public void ClearListOrder()
        {
            OrderList.Clear();
        }

        //public void IdOrderServices(FormCollection collection)
        //{
        //    IdOrders.Clear();

        //    string IdNewOrder = collection[0];

        //    string[] Id = IdNewOrder.Split(',');

        //    foreach (var item in Id)
        //    {
        //        IdOrders.Add(Convert.ToInt32(item));
        //    }
        //}

        public void IdOrderServices(List<int> idServises, List<int> listKey, List<int> listValues)
        {
            IdOrders.Clear();
            CountServic.Clear();

            if (listKey != null && listValues != null)
            {
                CountServic = listKey.Zip(listValues, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);
            }

            foreach (var item in idServises)
            {
                IdOrders.Add(item);
            }
        }


        public async Task OrderPreview()
        {
            var AllDetailings = Mapper.Map<IEnumerable<DetailingsBll>>(await _unitOfWork.DetailingsUnitOfWork.Get());

            Atest = AllDetailings.Where(a => IdOrders.Contains(a.Id));
            var whereResultServicecs = CountServic.Where(x => IdOrders.Contains(x.Key));

            foreach (var i in Atest)
            {
                OrderList.Add(new DetailingsBll
                {
                    Id = i.Id,
                    services_list = i.services_list,
                    forUnit = i.forUnit,
                    S = i.S,
                    M = i.M,
                    L = i.L,
                    XL = i.XL
                });
            }

            foreach (var item in whereResultServicecs)
            {
                var orderListSingl = OrderList.Find(x => x.Id == item.Key);
                OrderList.Find(f => f.Id == item.Key).S = orderListSingl.S * item.Value;
                OrderList.Find(f => f.Id == item.Key).M = orderListSingl.M * item.Value;
                OrderList.Find(f => f.Id == item.Key).L = orderListSingl.L * item.Value;
                OrderList.Find(f => f.Id == item.Key).XL = orderListSingl.XL * item.Value;
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


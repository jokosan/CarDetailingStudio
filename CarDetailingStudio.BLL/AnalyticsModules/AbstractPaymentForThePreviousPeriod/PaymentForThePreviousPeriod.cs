using CarDetailingStudio.BLL.AnalyticsModules.Models;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.AnalyticsModules.AbstractPaymentForThePreviousPeriod
{
    class PaymentForThePreviousPeriod : IPaymentForThePreviousPeriod
    {
        private readonly IOrderServicesCarWashServices _orderServicesCarWash;

        public PaymentForThePreviousPeriod(
         IOrderServicesCarWashServices orderServicesCarWash)
        {
            _orderServicesCarWash = orderServicesCarWash;
        }

        public async Task<IEnumerable<OrderServicesCarWashBll>> PaymentForThePreviousPeriodPerDay(DateTime date) => await _orderServicesCarWash.ReportsClosingData(date);

        public async Task<IEnumerable<OrderServicesCarWashBll>> PaymentForThePreviousPeriodPerDay(DateTime date, int paymentState)
        {
            var resultORder = await _orderServicesCarWash.ReportsClosingData(date);
            return resultORder.Where(x => x.PaymentState == paymentState);
        }

        public InformationPreviousPeriod AnalyticsFormationPaymentForThePreviousPeriod(IEnumerable<OrderServicesCarWashBll> orderCarWashes)
        {
            InformationPreviousPeriod model = new InformationPreviousPeriod();

            var orderServicesCarWashesWashing = orderCarWashes.Where(x => x.typeOfOrder == 2);
            var result = OrdersForThePreviousPeriodCaunt(orderServicesCarWashesWashing);

            model.Washing = new OrdersForThePreviousPeriod();
            model.Washing.OrderSum = result.Item2;
            model.Washing.OrderCount = result.Item1;

            var orderServicesCarWashesDetailing = orderCarWashes.Where(x => x.typeOfOrder == 1);
            result = OrdersForThePreviousPeriodCaunt(orderServicesCarWashesDetailing);

            model.Detailing = new OrdersForThePreviousPeriod();
            model.Detailing.OrderSum = result.Item2;
            model.Detailing.OrderCount = result.Item1;

            var orderServicesCarWashesCarpetWashing = orderCarWashes.Where(x => x.typeOfOrder == 3);
            result = OrdersForThePreviousPeriodCaunt(orderServicesCarWashesCarpetWashing);

            model.CarpetWashing = new OrdersForThePreviousPeriod();
            model.CarpetWashing.OrderSum = result.Item2;
            model.CarpetWashing.OrderCount = result.Item1; 

            var orderServicesCarWashesTireFitting = orderCarWashes.Where(x => x.typeOfOrder == 4);
            result = OrdersForThePreviousPeriodCaunt(orderServicesCarWashesTireFitting);

            model.TireFitting = new OrdersForThePreviousPeriod();
            model.TireFitting.OrderSum = result.Item2;
            model.TireFitting.OrderCount = result.Item1;

            var orderServicesCarWashesTireStorage = orderCarWashes.Where(x => x.typeOfOrder == 5);
            result = OrdersForThePreviousPeriodCaunt(orderServicesCarWashesTireStorage);

            model.TireStorage = new OrdersForThePreviousPeriod();
            model.TireStorage.OrderSum = result.Item2;
            model.TireStorage.OrderCount = result.Item1;

            return model;
        }

        private (double, double) OrdersForThePreviousPeriodCaunt(IEnumerable<OrderServicesCarWashBll> orderCarWashes)
        {
            double OrderCount = 0;
            double OrderSum = 0;

            foreach (var item in orderCarWashes)
            {
                if (item.OrderDate.Value.Date != item.ClosingData.Value.Date)
                {
                    OrderCount += 1;
                    OrderSum += item.DiscountPrice.Value;
                }
            }

            return (OrderCount, OrderSum);
        }

        public IEnumerable<OrderServicesCarWashBll> InfoOrdersForThePreviousPeriod(IEnumerable<OrderServicesCarWashBll> orderCarWashes)
        {
            List<OrderServicesCarWashBll> orderServices = new List<OrderServicesCarWashBll>();

            foreach (var item in orderCarWashes)
            {
                if (item.OrderDate.Value.Date != item.ClosingData.Value.Date)
                {
                    orderServices.Add(item);
                }
            }

            return orderServices.AsEnumerable();
        }
    }
}

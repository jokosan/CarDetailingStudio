using CarDetailingStudio.BLL.AnalyticsModules.Models.CompletedOrders;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.AnalyticsModules.AbstractCompletedOrders
{
    class CompletedOrders : ICompletedOrders
    {
        private readonly IOrderServicesCarWashServices _orderServicesCarWash;


        public CompletedOrders(IOrderServicesCarWashServices orderServicesCarWash)
        {
            _orderServicesCarWash = orderServicesCarWash;
        }

        public async Task<IEnumerable<OrderServicesCarWashBll>> CompletedOrdersPerDay(DateTime date) => await _orderServicesCarWash.Reports(date);

        public async Task<IEnumerable<OrderServicesCarWashBll>> CompletedOrdersForTheSelectedPeriod(DateTime date, DateTime final) => await _orderServicesCarWash.Reports(date, final);

        public async Task<IEnumerable<OrderServicesCarWashBll>> CompletedOrdersForTheSelectedPeriod(DateTime date, DateTime final, int paymentState)
        {
            var completedOrdersReports = await _orderServicesCarWash.Reports(date, final);
            return completedOrdersReports.Where(x => x.PaymentState == paymentState);
        }

        public async Task<IEnumerable<OrderServicesCarWashBll>> CompletedOrdersPerDay(DateTime date, int paymentState)
        {
            var completedOrdersReports = await _orderServicesCarWash.Reports(date);
            return completedOrdersReports.Where(x => x.PaymentState == paymentState);
        }

        public  CompletedOrdersModels AnalyticsFormationCompletedOrders(IEnumerable<OrderServicesCarWashBll> orderServicesCarWashes)
        {
            CompletedOrdersModels completedOrdersModels = new CompletedOrdersModels();          

            var orderServicesCarWashesWashing = orderServicesCarWashes.Where(x => x.typeOfOrder == 2);

            completedOrdersModels.Washing = new InformationCompletedOrders();
            completedOrdersModels.Washing.Cashier = orderServicesCarWashesWashing.Where(x => x.StatusOrder == 2).Sum(x => x.DiscountPrice).Value;
            completedOrdersModels.Washing.Quantities = orderServicesCarWashesWashing.Where(x => x.StatusOrder == 2).Count();
            completedOrdersModels.Washing.OrdersAwaitingPaymentCashier = orderServicesCarWashesWashing.Where(x => x.StatusOrder == 4).Sum(s => s.DiscountPrice).Value;
            completedOrdersModels.Washing.OrdersAwaitingPaymentQuantites = orderServicesCarWashesWashing.Where(x => x.StatusOrder == 4).Count();
            completedOrdersModels.Washing.SumQuantitiesAndOrdersAwaitingPaymentQuantites = Convert.ToInt32(completedOrdersModels.Washing.Quantities) + completedOrdersModels.Washing.OrdersAwaitingPaymentQuantites;
            completedOrdersModels.Washing.SumCashierAndOrdersAwaitingPaymentCashier = completedOrdersModels.Washing.Cashier + completedOrdersModels.Washing.OrdersAwaitingPaymentCashier;

            var orderServicesCarWashesDetailing = orderServicesCarWashes.Where(x => x.typeOfOrder == 1);

            completedOrdersModels.Detailing = new InformationCompletedOrders();
            completedOrdersModels.Detailing.Cashier = orderServicesCarWashesDetailing.Where(x => x.StatusOrder == 2).Sum(x => x.DiscountPrice).Value;
            completedOrdersModels.Detailing.Quantities = orderServicesCarWashesDetailing.Where(x => x.StatusOrder == 2).Count();
            completedOrdersModels.Detailing.OrdersAwaitingPaymentCashier = orderServicesCarWashesDetailing.Where(x => x.StatusOrder == 4).Sum(s => s.DiscountPrice).Value;
            completedOrdersModels.Detailing.OrdersAwaitingPaymentQuantites = orderServicesCarWashesDetailing.Where(x => x.StatusOrder == 4).Count();
            completedOrdersModels.Detailing.SumQuantitiesAndOrdersAwaitingPaymentQuantites = Convert.ToInt32(completedOrdersModels.Detailing.Quantities) + completedOrdersModels.Detailing.OrdersAwaitingPaymentQuantites;
            completedOrdersModels.Detailing.SumCashierAndOrdersAwaitingPaymentCashier = completedOrdersModels.Detailing.Cashier + completedOrdersModels.Detailing.OrdersAwaitingPaymentCashier;

            var orderServicesCarWashesCarpetWashing = orderServicesCarWashes.Where(x => x.typeOfOrder == 3);

            completedOrdersModels.CarpetWashing = new InformationCompletedOrders();
            completedOrdersModels.CarpetWashing.Cashier = orderServicesCarWashesCarpetWashing.Where(x => x.StatusOrder == 2).Sum(x => x.DiscountPrice).Value;
            completedOrdersModels.CarpetWashing.Quantities = orderServicesCarWashesCarpetWashing.Where(x => x.StatusOrder == 2).Count();
            completedOrdersModels.CarpetWashing.OrdersAwaitingPaymentCashier = orderServicesCarWashesCarpetWashing.Where(x => x.StatusOrder == 4).Sum(s => s.DiscountPrice).Value;
            completedOrdersModels.CarpetWashing.OrdersAwaitingPaymentQuantites = orderServicesCarWashesCarpetWashing.Where(x => x.StatusOrder == 4).Count();
            completedOrdersModels.CarpetWashing.SumQuantitiesAndOrdersAwaitingPaymentQuantites = Convert.ToInt32(completedOrdersModels.CarpetWashing.Quantities) + completedOrdersModels.CarpetWashing.OrdersAwaitingPaymentQuantites;
            completedOrdersModels.CarpetWashing.SumCashierAndOrdersAwaitingPaymentCashier = completedOrdersModels.CarpetWashing.Cashier + completedOrdersModels.CarpetWashing.OrdersAwaitingPaymentCashier;

            var orderServicesCarWashesTireFitting = orderServicesCarWashes.Where(x => x.typeOfOrder == 4);

            completedOrdersModels.TireFitting = new InformationCompletedOrders();
            completedOrdersModels.TireFitting.Cashier = orderServicesCarWashesTireFitting.Where(x => x.StatusOrder == 2).Sum(x => x.DiscountPrice).Value;
            completedOrdersModels.TireFitting.Quantities = orderServicesCarWashesTireFitting.Where(x => x.StatusOrder == 2).Count();
            completedOrdersModels.TireFitting.OrdersAwaitingPaymentCashier = orderServicesCarWashesTireFitting.Where(x => x.StatusOrder == 4).Sum(s => s.DiscountPrice).Value;
            completedOrdersModels.TireFitting.OrdersAwaitingPaymentQuantites = orderServicesCarWashesTireFitting.Where(x => x.StatusOrder == 4).Count();
            completedOrdersModels.TireFitting.SumQuantitiesAndOrdersAwaitingPaymentQuantites = Convert.ToInt32(completedOrdersModels.TireFitting.Quantities) + completedOrdersModels.TireFitting.OrdersAwaitingPaymentQuantites;
            completedOrdersModels.TireFitting.SumCashierAndOrdersAwaitingPaymentCashier = completedOrdersModels.TireFitting.Cashier + completedOrdersModels.TireFitting.OrdersAwaitingPaymentCashier;

            var orderServicesCarWashesTireStorage = orderServicesCarWashes.Where(x => x.typeOfOrder == 5);

            completedOrdersModels.TireStorage = new InformationCompletedOrders();
            completedOrdersModels.TireStorage.Cashier = orderServicesCarWashesTireStorage.Where(x => x.StatusOrder == 2).Sum(x => x.DiscountPrice).Value;
            completedOrdersModels.TireStorage.Quantities = orderServicesCarWashesTireStorage.Count();
            completedOrdersModels.TireStorage.OrdersAwaitingPaymentCashier = orderServicesCarWashesTireStorage.Where(x => x.StatusOrder == 4).Sum(s => s.DiscountPrice).Value;
            completedOrdersModels.TireStorage.OrdersAwaitingPaymentQuantites = orderServicesCarWashesTireStorage.Where(x => x.StatusOrder == 4).Count();
            completedOrdersModels.TireStorage.SumQuantitiesAndOrdersAwaitingPaymentQuantites = Convert.ToInt32(completedOrdersModels.TireStorage.Quantities) + completedOrdersModels.TireStorage.OrdersAwaitingPaymentQuantites;
            completedOrdersModels.TireStorage.SumCashierAndOrdersAwaitingPaymentCashier = completedOrdersModels.TireStorage.Cashier + completedOrdersModels.TireStorage.OrdersAwaitingPaymentCashier;

            return completedOrdersModels;
        }
    }
}

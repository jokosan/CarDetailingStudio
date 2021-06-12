using CarDetailingStudio.BLL.AnalyticsModules.Models;
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
        private readonly ITypeOfOrderServices _typeOfOrderServices;

        public CompletedOrders(
            IOrderServicesCarWashServices orderServicesCarWash,
            ITypeOfOrderServices typeOfOrderServices)
        {
            _orderServicesCarWash = orderServicesCarWash;
            _typeOfOrderServices = typeOfOrderServices;
        }

        public async Task<IEnumerable<OrderServicesCarWashBll>> CompletedOrdersPerDay(DateTime date)
            => await _orderServicesCarWash.Reports(date);

        public async Task<IEnumerable<OrderServicesCarWashBll>> CompletedOrdersForTheSelectedPeriod(DateTime date, DateTime final)
            => await _orderServicesCarWash.Reports(date, final);

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

        public CompletedOrdersModels AnalyticsFormationCompletedOrders(IEnumerable<OrderServicesCarWashBll> orderServicesCarWashes)
        {
            CompletedOrdersModels completedOrdersModels = new CompletedOrdersModels();

            var orderServicesCarWashesWashing = orderServicesCarWashes.Where(x => x.typeOfOrder == 2);

            completedOrdersModels.Washing = new InformationCompletedOrders();
            completedOrdersModels.Washing.Cashier = Math.Round(orderServicesCarWashesWashing.Where(x => x.StatusOrder == 2).Sum(x => x.DiscountPrice).Value, 1);
            completedOrdersModels.Washing.Quantities = orderServicesCarWashesWashing.Where(x => x.StatusOrder == 2).Count();
            completedOrdersModels.Washing.OrdersAwaitingPaymentCashier = Math.Round(orderServicesCarWashesWashing.Where(x => x.StatusOrder == 4).Sum(s => s.DiscountPrice).Value, 1);
            completedOrdersModels.Washing.OrdersAwaitingPaymentQuantites = orderServicesCarWashesWashing.Where(x => x.StatusOrder == 4).Count();
            completedOrdersModels.Washing.SumQuantitiesAndOrdersAwaitingPaymentQuantites = Convert.ToInt32(completedOrdersModels.Washing.Quantities) + completedOrdersModels.Washing.OrdersAwaitingPaymentQuantites;
            completedOrdersModels.Washing.SumCashierAndOrdersAwaitingPaymentCashier = Math.Round(completedOrdersModels.Washing.Cashier + completedOrdersModels.Washing.OrdersAwaitingPaymentCashier, 1);

            var orderServicesCarWashesDetailing = orderServicesCarWashes.Where(x => x.typeOfOrder == 1);

            completedOrdersModels.Detailing = new InformationCompletedOrders();
            completedOrdersModels.Detailing.Cashier = Math.Round(orderServicesCarWashesDetailing.Where(x => x.StatusOrder == 2).Sum(x => x.DiscountPrice).Value, 1);
            completedOrdersModels.Detailing.Quantities = orderServicesCarWashesDetailing.Where(x => x.StatusOrder == 2).Count();
            completedOrdersModels.Detailing.OrdersAwaitingPaymentCashier = Math.Round(orderServicesCarWashesDetailing.Where(x => x.StatusOrder == 4).Sum(s => s.DiscountPrice).Value, 1);
            completedOrdersModels.Detailing.OrdersAwaitingPaymentQuantites = orderServicesCarWashesDetailing.Where(x => x.StatusOrder == 4).Count();
            completedOrdersModels.Detailing.SumQuantitiesAndOrdersAwaitingPaymentQuantites = Convert.ToInt32(completedOrdersModels.Detailing.Quantities) + completedOrdersModels.Detailing.OrdersAwaitingPaymentQuantites;
            completedOrdersModels.Detailing.SumCashierAndOrdersAwaitingPaymentCashier = Math.Round(completedOrdersModels.Detailing.Cashier + completedOrdersModels.Detailing.OrdersAwaitingPaymentCashier, 1);

            var orderServicesCarWashesCarpetWashing = orderServicesCarWashes.Where(x => x.typeOfOrder == 3);

            completedOrdersModels.CarpetWashing = new InformationCompletedOrders();
            completedOrdersModels.CarpetWashing.Cashier = Math.Round(orderServicesCarWashesCarpetWashing.Where(x => x.StatusOrder == 2).Sum(x => x.DiscountPrice).Value, 1);
            completedOrdersModels.CarpetWashing.Quantities = orderServicesCarWashesCarpetWashing.Where(x => x.StatusOrder == 2).Count();
            completedOrdersModels.CarpetWashing.OrdersAwaitingPaymentCashier = Math.Round(orderServicesCarWashesCarpetWashing.Where(x => x.StatusOrder == 4).Sum(s => s.DiscountPrice).Value, 1);
            completedOrdersModels.CarpetWashing.OrdersAwaitingPaymentQuantites = orderServicesCarWashesCarpetWashing.Where(x => x.StatusOrder == 4).Count();
            completedOrdersModels.CarpetWashing.SumQuantitiesAndOrdersAwaitingPaymentQuantites = Convert.ToInt32(completedOrdersModels.CarpetWashing.Quantities) + completedOrdersModels.CarpetWashing.OrdersAwaitingPaymentQuantites;
            completedOrdersModels.CarpetWashing.SumCashierAndOrdersAwaitingPaymentCashier = Math.Round(completedOrdersModels.CarpetWashing.Cashier + completedOrdersModels.CarpetWashing.OrdersAwaitingPaymentCashier, 1);

            var orderServicesCarWashesTireFitting = orderServicesCarWashes.Where(x => x.typeOfOrder == 4);

            completedOrdersModels.TireFitting = new InformationCompletedOrders();
            completedOrdersModels.TireFitting.Cashier = Math.Round(orderServicesCarWashesTireFitting.Where(x => x.StatusOrder == 2).Sum(x => x.DiscountPrice).Value);
            completedOrdersModels.TireFitting.Quantities = orderServicesCarWashesTireFitting.Where(x => x.StatusOrder == 2).Count();
            completedOrdersModels.TireFitting.OrdersAwaitingPaymentCashier = Math.Round(orderServicesCarWashesTireFitting.Where(x => x.StatusOrder == 4).Sum(s => s.DiscountPrice).Value, 1);
            completedOrdersModels.TireFitting.OrdersAwaitingPaymentQuantites = orderServicesCarWashesTireFitting.Where(x => x.StatusOrder == 4).Count();
            completedOrdersModels.TireFitting.SumQuantitiesAndOrdersAwaitingPaymentQuantites = Convert.ToInt32(completedOrdersModels.TireFitting.Quantities) + completedOrdersModels.TireFitting.OrdersAwaitingPaymentQuantites;
            completedOrdersModels.TireFitting.SumCashierAndOrdersAwaitingPaymentCashier = Math.Round(completedOrdersModels.TireFitting.Cashier + completedOrdersModels.TireFitting.OrdersAwaitingPaymentCashier, 1);

            var orderServicesCarWashesTireStorage = orderServicesCarWashes.Where(x => x.typeOfOrder == 5);

            completedOrdersModels.TireStorage = new InformationCompletedOrders();
            completedOrdersModels.TireStorage.Cashier = Math.Round(orderServicesCarWashesTireStorage.Where(x => x.StatusOrder == 2).Sum(x => x.DiscountPrice).Value, 1);
            completedOrdersModels.TireStorage.Quantities = orderServicesCarWashesTireStorage.Count();
            completedOrdersModels.TireStorage.OrdersAwaitingPaymentCashier = Math.Round(orderServicesCarWashesTireStorage.Where(x => x.StatusOrder == 4).Sum(s => s.DiscountPrice).Value, 1);
            completedOrdersModels.TireStorage.OrdersAwaitingPaymentQuantites = orderServicesCarWashesTireStorage.Where(x => x.StatusOrder == 4).Count();
            completedOrdersModels.TireStorage.SumQuantitiesAndOrdersAwaitingPaymentQuantites = Convert.ToInt32(completedOrdersModels.TireStorage.Quantities) + completedOrdersModels.TireStorage.OrdersAwaitingPaymentQuantites;
            completedOrdersModels.TireStorage.SumCashierAndOrdersAwaitingPaymentCashier = Math.Round(completedOrdersModels.TireStorage.Cashier + completedOrdersModels.TireStorage.OrdersAwaitingPaymentCashier, 1);

            return completedOrdersModels;
        }

        public async Task<List<IncomeModel>> ServiceIncome(IEnumerable<OrderServicesCarWashBll> orderServicesCarWashes)
        {
            var typeServices = await _typeOfOrderServices.GetTableAll();
            var financServises = new List<IncomeModel>();

            foreach (var itemServ in typeServices)
            {
                if (orderServicesCarWashes.Any(x => x.typeOfOrder == itemServ.IdTypeOfOrder))
                {
                    var orderServices = orderServicesCarWashes.Where(x => x.typeOfOrder == itemServ.IdTypeOfOrder);
                    financServises.Add(new IncomeModel
                    {
                        IdIncome = itemServ.IdTypeOfOrder,
                        ServicesOfInvome = itemServ.nameOrder,
                        CountServices = orderServices.Count(),
                        SumOfIncome = Math.Round(orderServices.Where(x => x.StatusOrder == 2).Sum(x => x.DiscountPrice).Value, 1),
                        SumNoCash = Math.Round(orderServices.Where(x => x.StatusOrder == 2 && x.PaymentState == (int)PaymentMethod.nonСash).Sum(s => s.DiscountPrice).Value, 1),
                        SumCash = Math.Round(orderServices.Where(x => x.StatusOrder == 2 && x.PaymentState == (int)PaymentMethod.cash).Sum(s => s.DiscountPrice).Value, 1),
                        AwaitingPayment = Math.Round(orderServices.Where(x => x.StatusOrder == 4).Sum(s => s.DiscountPrice).Value, 1),
                        SumTotalIncome = Math.Round(orderServices.Where(x => x.StatusOrder == 2).Sum(x => x.DiscountPrice).Value + orderServices.Where(x => x.StatusOrder == 4).Sum(s => s.DiscountPrice).Value, 1)
                    });
                }
            }

            return financServises;
        }

        public async Task<List<IncomeModel>> ServiceIncome(DateTime start)
        {
            var result = await _orderServicesCarWash.ReportsClosingData(start);
            return await ServiceIncome(PaidDebtsForServices(result.ToList(), start).AsEnumerable());
        }

        public async Task<List<IncomeModel>> ServiceIncome(DateTime start, DateTime final)
        {
            var result = await _orderServicesCarWash.ReportsClosingData(start, final);
            return await ServiceIncome(PaidDebtsForServices(result.ToList(), start).AsEnumerable());
        }

        public async Task<IEnumerable<OrderServicesCarWashBll>> PaidServiceDebtInformation(DateTime start)
            => PaidDebtsForServices(await _orderServicesCarWash.ReportsClosingData(start), start).AsEnumerable();  

        public async Task<IEnumerable<OrderServicesCarWashBll>> PaidServiceDebtInformation(DateTime start, DateTime final)        
            =>  PaidDebtsForServices(await _orderServicesCarWash.ReportsClosingData(start, final), start).AsEnumerable();             

        public List<OrderServicesCarWashBll> PaidDebtsForServices(IEnumerable<OrderServicesCarWashBll> orderServices, DateTime date )
        {
            List<OrderServicesCarWashBll> OrderList = new List<OrderServicesCarWashBll>();
            OrderList = orderServices.ToList();

            foreach (var order in orderServices)
            {
                if (date.Date <= order.OrderDate.Value.Date)
                {
                    OrderList.Remove(order);
                }
            }

            return OrderList;
        }


    }
}

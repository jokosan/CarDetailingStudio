using CarDetailingStudio.BLL.Services.TireFitting.Module.Contract;
using CarDetailingStudio.BLL.Services.TireFitting.TireFittingContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.DAL;
using CarDetailingStudio.BLL.Services.Contract;

namespace CarDetailingStudio.BLL.Services.TireFitting.Module
{
    public class CreateOrderModule : ICreateOrderModule
    {
        private IPriceListTireFitting _priceListTireFitting;
        private IPriceTireFittingAdditionalServices _priceTireFittingAdditionalServices;
        private IOrderServicesCarWashServices _orderServices;
        private ITireService _tireService;
        private ITireChangeService _tireChangeService;

        public CreateOrderModule(
            IPriceTireFittingAdditionalServices priceTireFittingAdditionalServices,
            IPriceListTireFitting priceListTireFitting,
            IOrderServicesCarWashServices orderServices,
            ITireService tireService,
            ITireChangeService tireChangeService)
        {
            _priceTireFittingAdditionalServices = priceTireFittingAdditionalServices;
            _priceListTireFitting = priceListTireFitting;
            _orderServices = orderServices;
            _tireService = tireService;
            _tireChangeService = tireChangeService;
        }

        public Dictionary<int, int> SaveOrder(List<int> AdditionalServices, List<int> AdditionalServicesQuantity) =>
            AdditionalServices.Zip(AdditionalServicesQuantity, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);

        public async Task<List<PriceListTireFittingAdditionalServicesBll>> SelectPriceServise(Dictionary<int, int> keyValues, List<int> AdditionalServices)
        {
            var priceListTires = await _priceTireFittingAdditionalServices.PriceListTireFittingsContains(AdditionalServices);

            var priceResult = priceListTires.Where(a => AdditionalServices.Contains(a.idPriceListTireFittingAdditionalServices));
            var finalResult = keyValues.Where(x => AdditionalServices.Contains(x.Key));

            foreach (var item in finalResult)
            {
                var test = priceListTires.ToList().Find(x => x.idPriceListTireFittingAdditionalServices == item.Key);

                priceListTires.ToList().Find(f => f.idPriceListTireFittingAdditionalServices == item.Key).TheCost = test.TheCost * item.Value;
            }

            return priceListTires.ToList();
        }

        public async Task<int> SaveOrderTireFitting(double Total, double TotalDiscontClient, int idPaymentState, int idStatusOrder, int? Client, int typeOfOrder)
        {
            OrderServicesCarWashBll orderservices = new OrderServicesCarWashBll();

            orderservices.IdClientsOfCarWash = Client != null ? Client : null;
            orderservices.StatusOrder = idStatusOrder;
            orderservices.PaymentState = idPaymentState;
            orderservices.OrderDate = DateTime.Now;
            orderservices.TotalCostOfAllServices = Total;
            orderservices.DiscountPrice = TotalDiscontClient;
            orderservices.typeOfOrder = typeOfOrder;

            if (idPaymentState != 3 && idStatusOrder != 1)
            {
                orderservices.ClosingData = DateTime.Now;
            }

            return await _orderServices.CreateOrder(orderservices);
        }

        public async Task SeveTireService(int? ClientId, int orderServicesCarWashId, List<PriceListTireFittingAdditionalServicesBll> priceListTireFittings)
        {
            TireServiceBll tireService = new TireServiceBll();

            foreach (var item in priceListTireFittings)
            {
                tireService.clientsOfCarWashId = ClientId != null ? ClientId.Value : 0;
                tireService.orderServicesCarWashId = orderServicesCarWashId;
                tireService.priceListTireFittingAdditionalServicesId = item.idPriceListTireFittingAdditionalServices;
                tireService.priceTireFitting = item.TheCost;

                await _tireService.Insert(tireService);
            }
        }

        public async Task SaveTireChangeService(int orderServicesCarWashId, int numberOfTires, List<PriceListTireFittingBll> priceListTireFittings)
        {
            TireChangeServiceBll tireChangeServiceBll = new TireChangeServiceBll();

            foreach (var item in priceListTireFittings)
            {
                tireChangeServiceBll.idOrder = orderServicesCarWashId;
                tireChangeServiceBll.priceListTireFittingId = item.idPriceListTireFitting;
                tireChangeServiceBll.numberOfTires = numberOfTires;
                tireChangeServiceBll.tireRadius = (int)item.TireRadiusId;
                tireChangeServiceBll.price = item.TheCost;

                await _tireChangeService.Insert(tireChangeServiceBll);
            }
        }
    }
}

using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Model.ModelViewBll;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.Modules.TireStorage.Contract;
using CarDetailingStudio.BLL.Services.TireStorageServices.TireStorageContract;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Modules.TireStorage
{
    public class ReviwOrderModules : IReviwOrderModules
    {
        private ITireStorageServices _tireStorageServices;
        private IDetailingsServises _detailings;

        private ReviwOrderModelBll reviwOrder = new ReviwOrderModelBll();

        int radius;

        public ReviwOrderModules(
            ITireStorageServices tireStorageServices,
            IDetailingsServises detailings)
        {
            _tireStorageServices = tireStorageServices;
            _detailings = detailings;
        }

        public async Task<ReviwOrderModelBll> ReviwOrder(OrderTireStorageModelBll tireStorage)
        {
            var servicesTireStorage = await _tireStorageServices.GetTableAll();
            var detailing = await _detailings.GetPriceGroupWashServices(20);
            reviwOrder.tireStorageServices = new List<TireStorageServicesBll>();

            // наличие дисков
            if (tireStorage.discAvailability != null)
            {
                var resultPriceDisk = servicesTireStorage.Single(x => x.Id == 16);
                AddTireStoregeService(resultPriceDisk);
                reviwOrder.priceDisk = (double)tireStorage.quantity * resultPriceDisk.Price.Value;
            }

            // время хранения
            if (tireStorage.storageTime == 6)
            {
                // количество шин для хранения
                if (tireStorage.quantity == 4)
                {
                    TireStorageServicesBll priceResult = PriceRadius(tireStorage.radius, 4, servicesTireStorage, 6);
                    AddTireStoregeService(priceResult);
                    radius = priceResult.radius.Value;
                    reviwOrder.priceOfTire = priceResult.Price.Value;
                }
                else if (tireStorage.quantity < 4)
                {
                    TireStorageServicesBll priceResult = PriceRadius(tireStorage.radius, 1, servicesTireStorage);
                    AddTireStoregeService(priceResult);
                    radius = priceResult.radius.Value;
                    reviwOrder.priceOfTire = priceResult.Price.Value * (double)tireStorage.quantity;
                }
            }
            else
            {
                // количество шин для хранения если время хранение меньше 6 месяцев ()

                TireStorageServicesBll priceResult = PriceRadius(tireStorage.radius, 1, servicesTireStorage);
                AddTireStoregeService(priceResult);
                radius = priceResult.radius.Value;
                reviwOrder.priceOfTire = priceResult.Price.Value * (double)tireStorage.quantity;
            }

            // Мойка колес (шт)
            if (tireStorage.wheelWash != null)
            {
                if (tireStorage.wheelWash == 4)
                {
                    var wheelWashPrice = servicesTireStorage.Single(x => x.Id == 9);
                    AddTireStoregeService(wheelWashPrice);
                    reviwOrder.priceWheelWash = wheelWashPrice.Price.Value;
                    reviwOrder.IdWheelWash = wheelWashPrice.Id;
                }
                else if (tireStorage.wheelWash < 4)
                {
                    var wheelWashPrice = servicesTireStorage.Single(x => x.Id == 9);
                    AddTireStoregeService(wheelWashPrice);
                    reviwOrder.priceWheelWash = wheelWashPrice.Price.Value * (double)tireStorage.wheelWash;
                    reviwOrder.IdWheelWash = wheelWashPrice.Id;
                }
            }

            // Количество пакетов
            if (tireStorage.tireStorageBags != null)
            {
                var packets = servicesTireStorage.Single(x => x.Id == 7);
                AddTireStoregeService(packets);
                reviwOrder.priceNumberOfPackets = (double)tireStorage.tireStorageBags * packets.Price.Value;
            }

            // Селикон
            if (tireStorage.silicone != null)
            {
                if (tireStorage.silicone == 4)
                {
                    var siliconePrice = servicesTireStorage.Single(x => (x.radius == radius) && (x.amount == 4) && (x.ServicesName.Contains("обработать селиконом")));
                    AddTireStoregeService(siliconePrice);
                    reviwOrder.priceSilicone = siliconePrice.Price.Value;
                    reviwOrder.IdpriceSilicone = siliconePrice.Id;
                }
                else if (tireStorage.silicone < 4)
                {
                    var siliconePrice = servicesTireStorage.Single(x => (x.radius == radius) && (x.amount == 1) && (x.ServicesName.Contains("обработать селиконом")));
                    AddTireStoregeService(siliconePrice);
                    reviwOrder.priceSilicone = siliconePrice.Price.Value * (double)tireStorage.silicone;
                    reviwOrder.IdpriceSilicone = siliconePrice.Id;
                }
            }

            return reviwOrder;
        }

        private TireStorageServicesBll PriceRadius(int? radius, int countTire, IEnumerable<TireStorageServicesBll> tires, int timeStorage = 1)
        {
            if (radius <= 15)
            {
                return tires.Single(x => (x.radius == 15) && (x.amount == countTire) && (x.storageTime == timeStorage));
            }
            else if (radius >= 16 && radius <= 19)
            {
                return tires.Single(x => (x.radius == 16) && (x.amount == countTire) && (x.storageTime == timeStorage));
            }
            else
            {
                return tires.Single(x => (x.radius == 20) && (x.amount == countTire) && (x.storageTime == timeStorage));
            }
        }

        private void AddTireStoregeService(TireStorageServicesBll tireStorageServicesBll)
        {
            reviwOrder.tireStorageServices.Add(tireStorageServicesBll);
        }
    }
}

using CarDetailingStudio.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.TireFitting.Module.Contract
{
    public interface ICreateOrderModule
    {
        Dictionary<int, int> SaveOrder(List<int> AdditionalServices, List<int> AdditionalServicesQuantity);
        Task<List<PriceListTireFittingAdditionalServicesBll>> SelectPriceServise(Dictionary<int, int> keyValues, List<int> AdditionalServices);
        Task<int> SaveOrderTireFitting(double Total, double TotalDiscontClient, int idPaymentState, int idStatusOrder, int? Client, int typeOfOrder);
        Task SeveTireService(int? ClientId, int orderServicesCarWashId, List<PriceListTireFittingAdditionalServicesBll> priceListTireFittings);
        Task SaveTireChangeService(int orderServicesCarWashId, int numberOfTires, List<PriceListTireFittingBll> priceListTireFittings);
    }
}

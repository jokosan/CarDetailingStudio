using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Model.ModelViewBll;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Checkout.CheckoutContract
{
    public interface IOrder
    {
         Task<int> Chekout(OrderTireStorageModelBll orderTireStorage, double? sum, int? idPaymentState);
         Task<int> OrderForCarpetCleaning(OrderCarpetWashingBll orderCarpetWashing, int? idPaymentState, int prise, int clientId);
    }
}

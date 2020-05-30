using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Model.ModelViewBll;

namespace CarDetailingStudio.BLL.Services.Checkout.CheckoutContract
{
    public interface IOrder
    {
        int Chekout(OrderTireStorageModelBll orderTireStorage, double? sum, int? idPaymentState);
        int OrderForCarpetCleaning(OrderCarpetWashingBll orderCarpetWashing, int? idPaymentState, int prise);
    }
}

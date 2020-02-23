using CarDetailingStudio.BLL.Model.ModelViewBll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Checkout.CheckoutContract
{
    public interface IOrder
    {
        int Chekout(OrderTireStorageModelBll orderTireStorage, double? sum, int? idPaymentState);
    }
}

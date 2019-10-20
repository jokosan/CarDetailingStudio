using System.Web.Mvc;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface IOrderServices
    {
        void ClearListOrder();
        double? Discont(int? discont, double? sum);
        void IdOrderServices(FormCollection collection);
        void OrderPreview();
        double? OrderPrice();
    }
}
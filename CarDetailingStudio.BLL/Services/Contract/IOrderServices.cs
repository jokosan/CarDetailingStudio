using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface IOrderServices
    {
        void ClearListOrder();
        double? Discont(int? discont, double? sum);
        //void IdOrderServices(FormCollection collection);
        void IdOrderServices(List<int> idServises);
        Task OrderPreview();
        double? OrderPrice();
    }
}
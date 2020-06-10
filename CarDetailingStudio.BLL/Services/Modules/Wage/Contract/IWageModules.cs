using CarDetailingStudio.BLL.Model.ModelViewBll;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Modules.Wage.Contract
{
    public interface IWageModules
    {
        Task CloseOrder(int idPaymentState, int idOrderint, int idStatusOrder);
        Task Payroll(int idOrder, List<string> idBrigade);
        Task Payroll(int idOrder, int idBrigade, int idAdmin, ReviwOrderModelBll reviwOrder);
        Task AdminWageTireStorage(int idAdmin, int idOrder, int quantityTire);
    }
}
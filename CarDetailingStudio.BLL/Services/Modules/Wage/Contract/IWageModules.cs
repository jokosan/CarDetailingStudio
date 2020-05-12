using CarDetailingStudio.BLL.Model.ModelViewBll;
using System.Collections.Generic;

namespace CarDetailingStudio.BLL.Services.Modules.Wage.Contract
{
    public interface IWageModules
    {
        void CloseOrder(int idPaymentState, int idOrderint, int idStatusOrder);
        void Payroll(int idOrder, List<string> idBrigade);
        void Payroll(int idOrder, int idBrigade, int idAdmin, ReviwOrderModelBll reviwOrder);
        void AdminWageTireStorage(int idAdmin, int idOrder, int quantityTire);
    }
}
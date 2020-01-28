using System;
using System.Collections.Generic;
using CarDetailingStudio.BLL.Model;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface IOrderServicesCarWashServices
    {        
        void CloseOrder(int idPaymentState, int idOrder, List<string> idBrigade);
        IEnumerable<OrderServicesCarWashBll> GetAll(int statusOrder,string searchTable = null);
        OrderServicesCarWashBll GetId(int? id);
        void InsertOrders(List<double> carBody, List<int> id, List<int> sum);
        IEnumerable<OrderServicesCarWashBll> OrderReport(DateTime start, DateTime final);
        double PriceServices(List<double> carBody, List<int> idList, List<int> sum, int id);
        void RecountOrder(int idOrder);
        void DeleteOrder(int idOrder);
    }
}
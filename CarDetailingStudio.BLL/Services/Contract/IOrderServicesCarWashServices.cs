using System;
using System.Collections.Generic;
using CarDetailingStudio.BLL.Model;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface IOrderServicesCarWashServices
    {
        IEnumerable<OrderServicesCarWashBll> GetOrderAllTireStorage();
        IEnumerable<OrderServicesCarWashBll> GetAll(int statusOrder, int typeOfOrder);
        IEnumerable<OrderServicesCarWashBll> GetAll(int statusOrder);
        OrderServicesCarWashBll GetId(int? id);
        void InsertOrders(List<double> carBody, List<int> id, List<int> sum, double total);
        void InsertOrders(List<double> carBody, List<int> id, List<int> sum, double total, int? idOrder);
        IEnumerable<OrderServicesCarWashBll> OrderReport(DateTime start, DateTime final);
        double PriceServices(List<double> carBody, List<int> idList, List<int> sum, int id);
        void RecountOrder(int idOrder, int? ClientDiscont = null);
        void DeleteOrder(int idOrder);
        void StatusOrder(int? idOrder, int status);
        void SaveOrder(OrderServicesCarWashBll orderSave);
        int CreateOrder(OrderServicesCarWashBll orderSave);
        IEnumerable<OrderServicesCarWashBll> GetDataClosing();
        IEnumerable<OrderServicesCarWashBll> AllOrderOneEmployee(List<int> idOrder);
    }
}
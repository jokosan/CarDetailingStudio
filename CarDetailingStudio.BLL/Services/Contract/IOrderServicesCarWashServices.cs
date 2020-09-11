using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract.GenericContract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface IOrderServicesCarWashServices : IReports<OrderServicesCarWashBll>
    {
        Task<IEnumerable<OrderServicesCarWashBll>> GetOrderAllTireStorage();
        Task<IEnumerable<OrderServicesCarWashBll>> GetAll(int statusOrder, int typeOfOrder);
        Task<IEnumerable<OrderServicesCarWashBll>> GetAll(int statusOrder);
        Task<OrderServicesCarWashBll> GetId(int? id);
        Task InsertOrders(int service, List<double> carBody, List<int> id, List<int> sum, double total);
        Task InsertOrders(List<double> carBody, List<int> id, List<int> sum, double total, int? idOrder);
        Task<IEnumerable<OrderServicesCarWashBll>> OrderReport(DateTime start, DateTime final);
        double PriceServices(List<double> carBody, List<int> idList, List<int> sum, int id);
        Task RecountOrder(int idOrder, int? ClientDiscont = null);
        Task DeleteOrder(int idOrder);
        Task StatusOrder(int? idOrder, int status);
        Task SaveOrder(OrderServicesCarWashBll orderSave);
        Task<int> CreateOrder(OrderServicesCarWashBll orderSave);
        Task<IEnumerable<OrderServicesCarWashBll>> GetDataClosing();
        Task<IEnumerable<OrderServicesCarWashBll>> AllOrderOneEmployee(List<int> idOrder);
    }
}
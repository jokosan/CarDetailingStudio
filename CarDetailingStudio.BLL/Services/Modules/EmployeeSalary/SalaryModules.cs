using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Modules.ModulesModel;
using CarDetailingStudio.BLL.Utilities.Map;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.BLL.Services.Contract;

namespace CarDetailingStudio.BLL.Services.Modules.EmployeeSalary
{
    public class SalaryModules : ISalaryModules
    {
        private IUnitOfWork _unitOfWork;
        private AutomapperConfig _automapper;
        private IOrderCarWashWorkersServices _orderCarWash;

        private List<OrderServicesCarWashBll> orders = new List<OrderServicesCarWashBll>();

        public SalaryModules(IUnitOfWork unitOfWork, AutomapperConfig automapper,
            IOrderCarWashWorkersServices orderCarWash)
        {
            _unitOfWork = unitOfWork;
            _automapper = automapper;
            _orderCarWash = orderCarWash;   
        }

        public List<OrderServicesCarWashBll> TotalSalaryForToday(int IdCarWashWorkers, int InterestRate)
        {
            var CompletedWork = _orderCarWash.SampleForPayroll(IdCarWashWorkers);

            List<int> idOrder = new List<int>();

            foreach (var x in CompletedWork)
                idOrder.Add(x.IdOrder.Value);

            var allOrder = Mapper.Map<IEnumerable<OrderServicesCarWashBll>>(_unitOfWork.orderUnitiOfWork.QueryObjectGraph(x => idOrder.Contains(x.Id)));
                       
            foreach (var x in allOrder)
            {                
                orders.Add(new OrderServicesCarWashBll
                {
                    Id = x.Id,
                    IdClientsOfCarWash = x.IdClientsOfCarWash,
                    OrderDate = x.OrderDate,
                    ClosingData = x.ClosingData,
                    DiscountPrice = x.DiscountPrice,
                    TotalCostOfAllServices = PercentageOfOrder(InterestRate, x.DiscountPrice)
                });
            }
           
            return orders;
        }

        public double? PercentageOfOrder(int? discont, double? sum) => sum / 100 * discont;
    }
}

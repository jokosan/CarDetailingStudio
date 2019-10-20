using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Utilities.Map;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.BLL.Services.Modules.EmployeeSalary;

namespace CarDetailingStudio.BLL.Services.Modules
{
    public class OrderInfoViewServices
    {
        private IUnitOfWork _unitOfWork;
        private AutomapperConfig _automapper;
        private SalaryModules _salaryModules;

        public OrderInfoViewServices(UnitOfWork unitOfWork, AutomapperConfig maper, SalaryModules salaryModules)
        {
            _unitOfWork = unitOfWork;
            _automapper = maper;
            _salaryModules = salaryModules;
        }
      

        public IEnumerable<OrderInfoViewBll> GetOrderInfo()
        {
            var GetAll = Mapper.Map<IEnumerable<OrderInfoViewBll>>(_unitOfWork.OrderInfoUnitOfWork.Get());

            List<OrderInfoViewBll> PercentageOfTheAmount = new List<OrderInfoViewBll>();

            foreach (var x in GetAll)
            {
                PercentageOfTheAmount.Add(new OrderInfoViewBll 
                {
                    id = x.id,
                    countOrder = x.countOrder,
                    Name = x.Name,
                    Surname = x.Surname,
                    Patronymic = x.Patronymic,
                    MobilePhone = x.MobilePhone,
                    InterestRate = x.InterestRate,
                    CalculationStatus = x.CalculationStatus,
                    Expr1 = _salaryModules.PercentageOfOrder(x.InterestRate, x.Expr1)
                });;
            }

            return PercentageOfTheAmount.AsEnumerable();
        }
    }
}

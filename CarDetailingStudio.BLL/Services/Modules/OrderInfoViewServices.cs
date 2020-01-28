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
using CarDetailingStudio.DAL;
using CarDetailingStudio.BLL.Services.Contract;

namespace CarDetailingStudio.BLL.Services.Modules
{
    public class OrderInfoViewServices : IOrderInfoViewServices
    {
        private IUnitOfWork _unitOfWork;
        private AutomapperConfig _automapper;
        private ISalaryModules _salaryModules;
        private ICostServices _costServices;

        public OrderInfoViewServices(IUnitOfWork unitOfWork, AutomapperConfig maper,
                                     ISalaryModules salaryModules, ICostServices costServices)
        {
            _unitOfWork = unitOfWork;
            _automapper = maper;
            _salaryModules = salaryModules;
            _costServices = costServices;
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
                }); ;
            }

            return PercentageOfTheAmount.AsEnumerable();
        }

        public void PayAllEmployees(IEnumerable<OrderInfoViewBll> Salary) // IEnumerable<OrderInfoViewBll> Salary - del
        {
            var OrderCarWashWorkersWhere = Mapper.Map<IEnumerable<OrderCarWashWorkersBll>>(_unitOfWork.OrderCarWasWorkersUnitOFWork.GetWhere(x => x.CalculationStatus == false));

            OrderCarWashWorkersBll CloseSalaryAll = new OrderCarWashWorkersBll();
            CostsBll costs = new CostsBll();

            foreach (var item in OrderCarWashWorkersWhere)
            {
                CloseSalaryAll.Id = item.Id;
                CloseSalaryAll.IdOrder = item.IdOrder;
                CloseSalaryAll.IdCarWashWorkers = item.IdCarWashWorkers;
                CloseSalaryAll.CalculationStatus = true;

                OrderCarWashWorkers orderCarWash = Mapper.Map<OrderCarWashWorkersBll, OrderCarWashWorkers>(CloseSalaryAll);

                _unitOfWork.OrderCarWasWorkersUnitOFWork.Update(orderCarWash);
                _unitOfWork.Save();
            }

            //_costServices.AddCost(Salary.Sum(x => x.Expr1));

       
            //costs.Type = 1;
            //costs.Name = "Выплата заработной платы для всех сотрудников";
            //costs.expenses = Salary.Sum(x => x.Expr1);
            //costs.Date = DateTime.Now;

            //Costs teamSalary = Mapper.Map<CostsBll, Costs>(costs);

            //_unitOfWork.CostsUnitOfWork.Insert(teamSalary);
            //_unitOfWork.Save();

            #region заполнение таблицы WageBll 
            //List<WageBll> Wage = new List<WageBll>();

            //foreach (var item in Salary)
            //{
            //    Wage.Add(new WageBll
            //    {
            //        IdCarWashWorkers = item.id,
            //        CostsId = teamSalary.Id
            //    });
            //}

            //IEnumerable<Wage> wages = Mapper.Map<IEnumerable<WageBll>, IEnumerable<Wage>>(Wage);
            //_unitOfWork.WageUnitOfWork.Insert(wages.ToList());
            //_unitOfWork.Save();
            #endregion
        }
    }
}
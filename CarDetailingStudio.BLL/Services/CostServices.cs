using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Utilities.Map;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;

namespace CarDetailingStudio.BLL.Services
{
    public class CostServices : ICostServices
    {
        private IUnitOfWork _unitOfWork;
        private AutomapperConfig _automapper;

        public CostServices(IUnitOfWork unitOfWork, AutomapperConfig automapper)
        {
            _unitOfWork = unitOfWork;
            _automapper = automapper;
        }

        public IEnumerable<CostsBll> GetCost()
        {
            return Mapper.Map<IEnumerable<CostsBll>>(_unitOfWork.CostsUnitOfWork.Get());
        }

        public void AddCost(double? Sum, string NameCarWashWorkers = null)
        {
            CostsBll costs = new CostsBll();

            costs.Type = 1;
            costs.expenses = Sum;
            costs.Date = DateTime.Now;

            if (String.IsNullOrEmpty(NameCarWashWorkers))
            {
                
                costs.Name = "Выплата заработной платы для всех сотрудников";
            }
            else
            {
                costs.Name = $"Выплата заработной платы для {NameCarWashWorkers} ";
            }
         
            Costs teamSalary = Mapper.Map<CostsBll, Costs>(costs);

            _unitOfWork.CostsUnitOfWork.Insert(teamSalary);
            _unitOfWork.Save();

        }
    }
}

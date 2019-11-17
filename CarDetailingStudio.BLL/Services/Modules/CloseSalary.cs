using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Utilities.Map;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.BLL.Model;

namespace CarDetailingStudio.BLL.Services.Modules
{
    public class CloseSalary
    {
        private IUnitOfWork _unitOfWork;
        private AutomapperConfig _automapper;

        public CloseSalary(IUnitOfWork unitOfWork, AutomapperConfig automapper)
        {
            _unitOfWork = unitOfWork;
            _automapper = automapper;          
        }

        public void Close(IEnumerable<OrderInfoViewBll> salary)
        {
            
        }


    }
}

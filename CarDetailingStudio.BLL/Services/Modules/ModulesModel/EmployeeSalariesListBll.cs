using CarDetailingStudio.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Modules.ModulesModel
{
    public class EmployeeSalariesListBll
    {
        public List<OrderCarWashWorkersBll> admin { get; set; }
        public List<OrderCarWashWorkersBll> staffCarWashWorker { get; set; }
        public List<OrderCarWashWorkersBll> adminCarpetWashing { get; set; }
        public List<OrderCarWashWorkersBll> staffCarWashWorkerCarpetWashing { get; set; }
        public List<OrderCarWashWorkersBll> administratorDetailings { get; set; }
        public List<OrderCarWashWorkersBll> staffDetailing { get; set; }
    }
}

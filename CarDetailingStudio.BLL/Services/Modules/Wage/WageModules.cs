using CarDetailingStudio.BLL.EmployeesModules.Contract;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Model.ModelViewBll;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.Modules.Wage.Contract;
using CarDetailingStudio.BLL.Utilities.Map;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Modules.Wage
{
    public class WageModules : IWageModules
    {
        private IUnitOfWork _unitOfWork;
        private IOrderServices _orderServices;
        private IOrderServicesCarWashServices _orderServicesCarWash;
        private IOrderCarWashWorkersServices _orderCarWashWorkers;
        private ICarWashWorkersServices _carWashWorkers;
        private IServisesCarWashOrderServices _servisesCarWash;
        private IBrigadeForTodayServices _brigadeForToday;
        private AutomapperConfig _automapper;
        private readonly IPremiumAndRateServices _premiumAndRate;

        public WageModules(
            IUnitOfWork unitOfWork,
            AutomapperConfig automapperConfig,
            IOrderServices orderServices,
            IOrderServicesCarWashServices orderServicesCarWash,
            IOrderCarWashWorkersServices orderCarWashWorkers,
            ICarWashWorkersServices carWashWorkers,
            IServisesCarWashOrderServices servisesCarWash,
            IBrigadeForTodayServices brigadeForToday,
            IPremiumAndRateServices premiumAndRate)
        {
            _unitOfWork = unitOfWork;
            _orderServices = orderServices;
            _automapper = automapperConfig;
            _orderServicesCarWash = orderServicesCarWash;
            _orderCarWashWorkers = orderCarWashWorkers;
            _carWashWorkers = carWashWorkers;
            _servisesCarWash = servisesCarWash;
            _brigadeForToday = brigadeForToday;
            _premiumAndRate = premiumAndRate;
        }

        // db Table [OrderServicesCarWash]
        // Закрыть заказ
        public async Task CloseOrder(int idPaymentState, int idOrder, int idStatusOrder)
        
        {
            var Order = await _orderServicesCarWash.GetId(idOrder);

            Order.PaymentState = idPaymentState;

            if (idStatusOrder != 4)
            {
                Order.ClosingData = DateTime.Now;
            }
          

            Order.StatusOrder = idStatusOrder;

            if (Order.ClientsOfCarWash.discont > 0)
            {
                double x = Convert.ToDouble(_orderServices.Discont(Order.ClientsOfCarWash.discont, Order.TotalCostOfAllServices));
                Order.DiscountPrice = Math.Ceiling(x);
            }
            else
            {
                Order.DiscountPrice = Order.TotalCostOfAllServices;
            }

            await _orderServicesCarWash.SaveOrder(Order);
        }

        // Начисление заработной платы
        public async Task Payroll(int idOrder, List<string> idBrigade, int serveses)
        {
            int numberOfEmployees = idBrigade.Count;

            var amountOfCurrentOrder = await _orderServicesCarWash.GetId(idOrder);

            OrderCarWashWorkersBll orderCarWashWorkers = new OrderCarWashWorkersBll();

            double discountPercentage = Convert.ToDouble(amountOfCurrentOrder.TotalCostOfAllServices) - Convert.ToDouble(amountOfCurrentOrder.DiscountPrice);
            double discontResult = ((double)discountPercentage / (double)amountOfCurrentOrder.TotalCostOfAllServices) * 100;

            foreach (var item in idBrigade)
            {
                orderCarWashWorkers.IdCarWashWorkers = ConvertStringToInt(item);
                orderCarWashWorkers.IdOrder = idOrder;
                orderCarWashWorkers.CalculationStatus = false;
                orderCarWashWorkers.closedDayStatus = false;
                orderCarWashWorkers.typeServicesId = StatusTypeServises(serveses, 2); // изменить  

                if (discontResult >= 50)
                {
                    orderCarWashWorkers.Payroll = await PercentOfTheOrder(ConvertStringToInt(item), numberOfEmployees, amountOfCurrentOrder.TotalCostOfAllServices, false, orderCarWashWorkers.typeServicesId.Value);
                }
                else
                {
                    orderCarWashWorkers.Payroll = await PercentOfTheOrder(ConvertStringToInt(item), numberOfEmployees, amountOfCurrentOrder.DiscountPrice, false, orderCarWashWorkers.typeServicesId.Value);
                }

                await _orderCarWashWorkers.Insert(orderCarWashWorkers);
            }

            if (discontResult >= 50)
            {
                await AdminWage(amountOfCurrentOrder, amountOfCurrentOrder.TotalCostOfAllServices, serveses);
            }
            else
            {
                await AdminWage(amountOfCurrentOrder, amountOfCurrentOrder.DiscountPrice, serveses);
            }               
        }

        private int StatusTypeServises(int servises, int brigade)
        {
            if (brigade == (int)PositionsOfAdministrators.Administrator && servises == (int)TypeService.Detailing)
                return (int)ServiceCategories.AdmminDetailing;

            if (brigade == (int)PositionsOfAdministrators.Employees && servises == (int)TypeService.Detailing)
                return (int)ServiceCategories.EmployeesDetailing;

            if (brigade == (int)PositionsOfAdministrators.Administrator && servises == (int)TypeService.CarWash)
                return (int)ServiceCategories.AdminCarWash;

            if (brigade == (int)PositionsOfAdministrators.Employees && servises == (int)TypeService.CarWash)
                return (int)ServiceCategories.EmployeesCarWash;

            if (brigade == (int)PositionsOfAdministrators.Administrator && servises == (int)TypeService.CarpetCleaning)
                return (int)ServiceCategories.AdminCarpetCleaning;

            if (brigade == (int)PositionsOfAdministrators.Employees && servises == (int)TypeService.CarpetCleaning)
                return (int)ServiceCategories.EmployeesCarpetCleaning;

            if (brigade == (int)PositionsOfAdministrators.Administrator && servises == (int)TypeService.TireStorage)
                return (int)ServiceCategories.AdmminTireStorage;

            if (brigade == (int)PositionsOfAdministrators.Employees && servises == (int)TypeService.TireStorage)
                return (int)ServiceCategories.EmployeesTireStorage;

            if (brigade == (int)PositionsOfAdministrators.Administrator && servises == (int)TypeService.TireFitting)
                return (int)ServiceCategories.AdmminTireFitting;

            if (brigade == (int)PositionsOfAdministrators.Employees && servises == (int)TypeService.TireFitting)
                return (int)ServiceCategories.EmployeesTireFitting;

            return 0;
        }

        // Начисление ЗП за хранение шин 
        public async Task Payroll(int idOrder, int idBrigade, int idAdmin, ReviwOrderModelBll reviwOrder)
        {
            var amountOfCurrentOrder = reviwOrder.priceSilicone + reviwOrder.priceWheelWash;

            OrderCarWashWorkersBll orderCarWashWorkers = new OrderCarWashWorkersBll();

            orderCarWashWorkers.IdCarWashWorkers = idBrigade;
            orderCarWashWorkers.IdOrder = idOrder;
            orderCarWashWorkers.CalculationStatus = false;
            orderCarWashWorkers.typeServicesId = StatusTypeServises(8, 2); // изменить  
            orderCarWashWorkers.Payroll = await PercentOfTheOrder(idBrigade, 1, amountOfCurrentOrder, false, orderCarWashWorkers.typeServicesId.Value);
            orderCarWashWorkers.closedDayStatus = false;

            await _orderCarWashWorkers.Insert(orderCarWashWorkers);

            orderCarWashWorkers.IdCarWashWorkers = idAdmin;
            orderCarWashWorkers.typeServicesId = StatusTypeServises(7, 1); // изменить  
            orderCarWashWorkers.Payroll = await PercentOfTheOrder(idAdmin, 1, amountOfCurrentOrder, true, orderCarWashWorkers.typeServicesId.Value);
            
            await _orderCarWashWorkers.Insert(orderCarWashWorkers);
        }

        private int ConvertStringToInt(string x) => Convert.ToInt32(x);

        // Процент от заказа
        private async Task<double?> PercentOfTheOrder(int employeeId, int count, double? orderPrice, bool status, int positio)
        {
            var selectPercentageOfEmployeeOrder = await _premiumAndRate.SelectPosition(employeeId);
            var result = selectPercentageOfEmployeeOrder.First(x => x.positionId == positio);

            double employeePercentage;

            if (status)
            {
                employeePercentage = ((double)result.percentageRatePerOrder) / 100;
            }
            else
            {
                employeePercentage = ((double)result.percentageRatePerOrder) / 100;
            }

            return employeePercentage * orderPrice / count;
        }

        // Заработная плата администратора
        private async Task AdminWage(OrderServicesCarWashBll orderServicesCarWash, double? orderPrice, int serveses)
        {
            var servicesOrder = await _servisesCarWash.GetAllId(orderServicesCarWash.Id);
            bool serviceСategory = servicesOrder.Any(x => x.Detailings.IdTypeService == 1);

            if (serviceСategory)
            {
                await ChoiceAdministrator(2, orderServicesCarWash.Id, orderPrice, serveses);
            }
            else
            {
                await ChoiceAdministrator(1, orderServicesCarWash.Id, orderPrice, serveses);
            }
        }

        // Начисление заработной платы за оформление заказа (хранения шин)
        public async Task AdminWageTireStorage(int idOrder, int quantityTire)
        {
            var category = await _brigadeForToday.GetDateTimeNow();
            var adminSelectionByCategory = category.Single(x => x.StatusId == 1);

            OrderCarWashWorkersBll orderCarWashWorkers = new OrderCarWashWorkersBll();

            orderCarWashWorkers.IdOrder = idOrder;
            orderCarWashWorkers.IdCarWashWorkers = adminSelectionByCategory.IdCarWashWorkers.Value;
            orderCarWashWorkers.CalculationStatus = false;
            orderCarWashWorkers.Payroll = 5 * quantityTire;
            orderCarWashWorkers.closedDayStatus = false;
            orderCarWashWorkers.typeServicesId = 7;

            await _orderCarWashWorkers.Insert(orderCarWashWorkers);
        }

        // Выбор Администратора
        private async Task ChoiceAdministrator(int adminСategory, int idOrder, double? orderPrice, int serveses)
        {
            var category = await _brigadeForToday.GetDateTimeNow();
            var adminSelectionByCategory = category.Single(x => x.StatusId == adminСategory);

            OrderCarWashWorkersBll orderCarWashWorkers = new OrderCarWashWorkersBll();

            orderCarWashWorkers.IdOrder = idOrder;
            orderCarWashWorkers.IdCarWashWorkers = adminSelectionByCategory.CarWashWorkers.id;
            orderCarWashWorkers.CalculationStatus = false;
            orderCarWashWorkers.typeServicesId = StatusTypeServises(serveses, 1); // изменить  
            orderCarWashWorkers.Payroll = await PercentOfTheOrder(adminSelectionByCategory.CarWashWorkers.id, 1, orderPrice, true, orderCarWashWorkers.typeServicesId.Value);
            orderCarWashWorkers.closedDayStatus = false;
           
            await _orderCarWashWorkers.Insert(orderCarWashWorkers);
        }
    }
}

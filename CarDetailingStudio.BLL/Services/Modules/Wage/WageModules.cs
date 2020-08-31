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

        public WageModules(IUnitOfWork unitOfWork, AutomapperConfig automapperConfig, IOrderServices orderServices,
                            IOrderServicesCarWashServices orderServicesCarWash, IOrderCarWashWorkersServices orderCarWashWorkers,
                            ICarWashWorkersServices carWashWorkers, IServisesCarWashOrderServices servisesCarWash, IBrigadeForTodayServices brigadeForToday)
        {
            _unitOfWork = unitOfWork;
            _orderServices = orderServices;
            _automapper = automapperConfig;
            _orderServicesCarWash = orderServicesCarWash;
            _orderCarWashWorkers = orderCarWashWorkers;
            _carWashWorkers = carWashWorkers;
            _servisesCarWash = servisesCarWash;
            _brigadeForToday = brigadeForToday;
        }

        // db Table [OrderServicesCarWash]
        // Закрыть заказ
        public async Task CloseOrder(int idPaymentState, int idOrder, int idStatusOrder)
        {
            var Order = await _orderServicesCarWash.GetId(idOrder);

            Order.PaymentState = idPaymentState;
            Order.ClosingData = DateTime.Now;

            
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
        public async Task Payroll(int idOrder, List<string> idBrigade)
        {
            int numberOfEmployees = idBrigade.Count;
            var amountOfCurrentOrder = await _orderServicesCarWash.GetId(idOrder);

            OrderCarWashWorkersBll orderCarWashWorkers = new OrderCarWashWorkersBll();

            foreach (var item in idBrigade)
            {
                orderCarWashWorkers.IdCarWashWorkers = ConvertStringToInt(item);
                orderCarWashWorkers.IdOrder = idOrder;
                orderCarWashWorkers.CalculationStatus = false;
                orderCarWashWorkers.Payroll = await PercentOfTheOrder(ConvertStringToInt(item), numberOfEmployees, amountOfCurrentOrder.DiscountPrice, false);
                orderCarWashWorkers.closedDayStatus = false;

                await _orderCarWashWorkers.SaveOrderCarWashWorkers(orderCarWashWorkers);
            }

            await AdminWage(amountOfCurrentOrder, amountOfCurrentOrder.DiscountPrice);
        }

        // Начисление ЗП за хранение шин 
        public async Task Payroll(int idOrder, int idBrigade, int idAdmin, ReviwOrderModelBll reviwOrder)
        {
            var amountOfCurrentOrder = reviwOrder.priceSilicone + reviwOrder.priceWheelWash;

            OrderCarWashWorkersBll orderCarWashWorkers = new OrderCarWashWorkersBll();

            orderCarWashWorkers.IdCarWashWorkers = idBrigade;
            orderCarWashWorkers.IdOrder = idOrder;
            orderCarWashWorkers.CalculationStatus = false;
            orderCarWashWorkers.Payroll = await PercentOfTheOrder(idBrigade, 1, amountOfCurrentOrder, false);
            orderCarWashWorkers.closedDayStatus = false;

            await _orderCarWashWorkers.SaveOrderCarWashWorkers(orderCarWashWorkers);

            orderCarWashWorkers.IdCarWashWorkers = idAdmin;
            orderCarWashWorkers.Payroll = await PercentOfTheOrder(idAdmin, 1, amountOfCurrentOrder, true);

            await _orderCarWashWorkers.SaveOrderCarWashWorkers(orderCarWashWorkers);

        }

        private int ConvertStringToInt(string x) => Convert.ToInt32(x);

        // Процент от заказа
        private async Task<double?> PercentOfTheOrder(int employeeId, int count, double? orderPrice, bool status)
        {
            var selectedEmployee = await _carWashWorkers.CarWashWorkersId(employeeId);
            double employeePercentage;

            //if (status)
            //{
            //   JobTitle = Regex.IsMatch(selectedEmployee.JobTitleTable.Position, "\\bАдминистратор\\b");
            //}            

            if (status)
            {
                employeePercentage = ((double)selectedEmployee.AdministratorsInterestRate) / 100;
            }
            else
            {
                employeePercentage = ((double)selectedEmployee.InterestRate) / 100;
            }

            return employeePercentage * orderPrice / count;
        
        }

        // Заработная плата администратора
        private async Task AdminWage(OrderServicesCarWashBll orderServicesCarWash, double? orderPrice)
        {
            var servicesOrder = await _servisesCarWash.GetAllId(orderServicesCarWash.Id);
            bool serviceСategory = servicesOrder.Any(x => x.Detailings.IdTypeService == 1);

            if (serviceСategory)
            {
                await ChoiceAdministrator(2, orderServicesCarWash.Id, orderPrice);
            }
            else
            {
               await ChoiceAdministrator(1, orderServicesCarWash.Id, orderPrice);
            }
        }

        // Начисление заработной платы за оформление заказа (хранения шин)
        public async Task AdminWageTireStorage(int idAdmin, int idOrder, int quantityTire)
        {
            OrderCarWashWorkersBll orderCarWashWorkers = new OrderCarWashWorkersBll();

            orderCarWashWorkers.IdOrder = idOrder;
            orderCarWashWorkers.IdCarWashWorkers = idAdmin;
            orderCarWashWorkers.CalculationStatus = false;
            orderCarWashWorkers.Payroll = 5 * quantityTire;
            orderCarWashWorkers.closedDayStatus = false;

            await _orderCarWashWorkers.SaveOrderCarWashWorkers(orderCarWashWorkers);

        }

        // Выбор Администратора
        private async Task ChoiceAdministrator(int adminСategory, int idOrder, double? orderPrice)
        {
            var category = await _brigadeForToday.GetDateTimeNow();
            var adminSelectionByCategory = category.Single(x => x.StatusId == adminСategory);

            if (adminSelectionByCategory.StatusId == 1 || adminSelectionByCategory.StatusId == 2)
            {
                OrderCarWashWorkersBll orderCarWashWorkers = new OrderCarWashWorkersBll();

                orderCarWashWorkers.IdOrder = idOrder;
                orderCarWashWorkers.IdCarWashWorkers = adminSelectionByCategory.CarWashWorkers.id;
                orderCarWashWorkers.CalculationStatus = false;
                orderCarWashWorkers.Payroll = await PercentOfTheOrder(adminSelectionByCategory.CarWashWorkers.id, 1, orderPrice, true);
                orderCarWashWorkers.closedDayStatus = false;

                await _orderCarWashWorkers.SaveOrderCarWashWorkers(orderCarWashWorkers);
            }
        }
    }
}

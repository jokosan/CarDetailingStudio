using CarDetailingStudio.BLL.AnalyticsModules;
using CarDetailingStudio.BLL.AnalyticsModules.AbstractCompletedOrders;
using CarDetailingStudio.BLL.AnalyticsModules.AbstractExpenses;
using CarDetailingStudio.BLL.AnalyticsModules.AbstractFactory;
using CarDetailingStudio.BLL.AnalyticsModules.AbstractPaymentForThePreviousPeriod;
using CarDetailingStudio.BLL.AnalyticsModules.AbstractSalaryExpenses;
using CarDetailingStudio.BLL.AnalyticsModules.AbstractSaleOfGoods;
using CarDetailingStudio.BLL.AnalyticsModules.AbstractWagesForCompletedOrders;
using CarDetailingStudio.BLL.AnalyticsModules.AdditionalIncome;
using CarDetailingStudio.BLL.Model;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Utilities.Ninject
{
    public class NinjectAnalytics
    {
        public static void Initialize(IKernel kernel)
        {
            kernel.Bind<IAbstractFactory>().To<AnalyticsFactory>();

            kernel.Bind<ICompletedOrders>().To<CompletedOrders>();          
            kernel.Bind<IAbstractExpenses>().To<Expenses>();
            kernel.Bind<IWagesForCompletedOrders>().To<WagesForCompletedOrders>();
            kernel.Bind<ISaleOfGoods>().To<SaleOfGoods>();
            kernel.Bind<IAdditionalIncome>().To<AdditionalIncome>();
            kernel.Bind<IPaymentForThePreviousPeriod>().To<PaymentForThePreviousPeriod>();
            kernel.Bind<IAbstractSalaryExpenses>().To<SalaryExpenses>();


        }
    }
}

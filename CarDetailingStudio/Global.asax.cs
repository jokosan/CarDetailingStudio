using AutoMapper;
using CarDetailingStudio.Utilities;
using CarDetailingStudio.Utilities.Map;
using CarDetailingStudio.Utilities.Quartz;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CarDetailingStudio
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());

            // ������ ���������� ������
            SetingShiftClose.Start();
        }
    }
}

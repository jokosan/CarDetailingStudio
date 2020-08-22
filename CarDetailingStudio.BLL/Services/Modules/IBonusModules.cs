using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Modules
{
    public interface IBonusModules
    {
        Task PremiumAccrual(int carWashWorkers, double payroll);
    }
}
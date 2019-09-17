using CarDetailingStudio.BLL.Services.IServices;
using CarDetailingStudio.BLL.Services.UnitOfWorks;
using CarDetailingStudio.DataBase.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services
{
    public class CarWashWorkersServices : IServices<CarWashWorkers>
    {
        private UnitOfWork _unitOfWorks;

        public CarWashWorkersServices()
        {
            _unitOfWorks = new UnitOfWork();
        }

        public void Create(CarWashWorkers item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _unitOfWorks.Dispose();
        }

        #region GET
        public IEnumerable<CarWashWorkers> GetAll()
        {
            return _unitOfWorks.CarWashWorkersUW.GetAll();
        }

        public IEnumerable<CarWashWorkers> GetChooseEmployees()
        {
            return _unitOfWorks.CarWashWorkersUW.GetAll().Where(x => (x.status == "true") 
                            && (x.JobTitleTable.Position == "Детейлер") 
                            || (x.JobTitleTable.Position == "Мойщик") 
                            || (x.JobTitleTable.Position == "Старший мойщик"));    
        }

        public CarWashWorkers GetId(int id)
        {
            throw new NotImplementedException();
        }
        #endregion


        public void Update(CarWashWorkers item)
        {
            throw new NotImplementedException();
        }
    }
}

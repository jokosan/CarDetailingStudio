using CarDetailingStudio.BLL.Services.UnitOfWorks;
using CarDetailingStudio.DataBase.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CarDetailingStudio.BLL.Services.ServiceLogic
{
    public class FormationOfTheCurrentShift : IDisposable
    {
        private UnitOfWork _unitOfWorks;

        public FormationOfTheCurrentShift()
        {
            _unitOfWorks = new UnitOfWork();
        }

      
        public void AddToCurrentShift(FormCollection collection)
        {
            List<brigadeForToday> brigadeForTodays = new List<brigadeForToday>();


            string idString = collection[0];
            string[] idList = idString.Split(',');

            brigadeForToday AddCreateTable = new brigadeForToday();

            foreach (var item in idList)
            {
                AddCreateTable.Date = DateTime.Now;
                AddCreateTable.IdCarWashWorkers = Int32.Parse(item);

                _unitOfWorks.BrigadeForTodayUW.Create(AddCreateTable);
                _unitOfWorks.Save();
            }

          
        }

        public void Dispose()
        {
            _unitOfWorks.Dispose();
        }
    }
}

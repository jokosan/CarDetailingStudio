using CarDetailingStudio.BLL.Services.IServices;
using CarDetailingStudio.BLL.Services.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Logic
{
    // Условие входа
    public class EntryCondition
    {
        private UnitOfWork _unitOfWorks;

        public EntryCondition()
        {
            _unitOfWorks = new UnitOfWork();
        }

        public bool HomeEntryCondition()
        {
            string date = DateTime.Now.ToString("dd.MM.yyyy");
            var thisDay = _unitOfWorks.BrigadeForTodayUW.GetAll().FirstOrDefault(x => x.Date?.ToString("dd.MM.yyyy") == date);

            if (thisDay.Date?.ToString("dd.MM.yyyy") == date)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

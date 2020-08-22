using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services
{
    public class BrigadeForTodayServices : IBrigadeForTodayServices
    {
        private IUnitOfWork _unitOfWork;

        public BrigadeForTodayServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<BrigadeForTodayBll>> GetDateTimeNow()
        {
            var brigadeResult = Mapper.Map<IEnumerable<BrigadeForTodayBll>>(await _unitOfWork.BrigadeUnitOfWork.GetWhere(x => x.EarlyTermination == true));

            return brigadeResult.Where(x => x.Date?.ToString("dd.MM.yyyy") == DateTime.Now.ToString("dd.MM.yyyy"));
        }

        
        public async Task RemoveFromBrigade(int id)
        {
            BrigadeForTodayBll brigadeForTodayBll = await GetId(id);

            brigadeForToday removeFromBrigade = Mapper.Map<BrigadeForTodayBll, brigadeForToday>(brigadeForTodayBll);

            removeFromBrigade.EndTime = DateTime.Now;
            removeFromBrigade.EarlyTermination = false;

            _unitOfWork.BrigadeForTodayUnitOfWork.Update(removeFromBrigade);
            await _unitOfWork.Save();
        }

        public async Task<BrigadeForTodayBll> GetId(int id)
        {
            return Mapper.Map<BrigadeForTodayBll>(await _unitOfWork.BrigadeUnitOfWork.GetById(id));
        }
    }
}

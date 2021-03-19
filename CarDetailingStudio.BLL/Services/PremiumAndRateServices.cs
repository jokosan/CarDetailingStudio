using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using System.Threading.Tasks;
using CarDetailingStudio.DAL;

namespace CarDetailingStudio.BLL.Services
{
    public class PremiumAndRateServices : IPremiumAndRateServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPosition _position;

        public PremiumAndRateServices(
            IUnitOfWork unitOfWork,
            IPosition position)
        {
            _unitOfWork = unitOfWork;
            _position = position;
        }

        public async Task CreatePremiumAndRateServices(int position, int carWashWorkersId)
        {
            var positionResult = await _position.GetTableAll();

            PremiumAndRateBll premiumAndRates = new PremiumAndRateBll();

            foreach (var item in positionResult)
            {
                premiumAndRates.carWashWorkersId = carWashWorkersId;
                //premiumAndRates.percentageStatusForOrder = true;
                premiumAndRates.percentageRatePerOrder = PercentageRatePerOrder(item.positionsOfAdministrators.Value, item.idPosition);
                premiumAndRates.positionsStatus = PositionsStatus(position, item.idPosition);
                premiumAndRates.positionId = item.idPosition;

                await Insert(premiumAndRates);
            }
        }

        private double PercentageRatePerOrder(int positionsOfAdministrators, int positionAll)
        {
            if (positionAll == 1)
            {
                return 5;
            }
            else if (positionsOfAdministrators == 1)
            {
                return 10;
            }
            else
            {
                return 30;
            }
        }

        private bool PositionsStatus(int chosenPosition, int possiblePosition)
        {
            if (chosenPosition == possiblePosition)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<PremiumAndRateBll>> AllCurrentEmployees() =>
            Mapper.Map<IEnumerable<PremiumAndRateBll>>(await _unitOfWork.PremiumAndRateServicesUnitOFWork.QueryObjectGraph(x =>
                x.CarWashWorkers.status == true, "CarWashWorkers", "Position"));

        public async Task<IEnumerable<PremiumAndRateBll>> SelectPosition(int carWashWorkersId) =>
            Mapper.Map<IEnumerable<PremiumAndRateBll>>(await _unitOfWork.PremiumAndRateServicesUnitOFWork.QueryObjectGraph(x =>
                x.carWashWorkersId == carWashWorkersId, "Position"));

        public async Task<PremiumAndRateBll> SelectId(int? elementId) =>
            Mapper.Map<PremiumAndRateBll>(await _unitOfWork.PremiumAndRateServicesUnitOFWork.GetById(elementId));

        public async Task Insert(PremiumAndRateBll element)
        {
            _unitOfWork.PremiumAndRateServicesUnitOFWork.Insert(TransformAnEntity(element));
            await _unitOfWork.Save();
        }

        public async Task Update(PremiumAndRateBll elementToUpdate)
        {
            _unitOfWork.PremiumAndRateServicesUnitOFWork.Update(TransformAnEntity(elementToUpdate));
            await _unitOfWork.Save();
        }

        private premiumAndRate TransformAnEntity(PremiumAndRateBll entity) => Mapper.Map<PremiumAndRateBll, premiumAndRate>(entity);

        public Task<IEnumerable<PremiumAndRateBll>> GetTableAll()
        {
            throw new NotImplementedException();
        }
    }
}

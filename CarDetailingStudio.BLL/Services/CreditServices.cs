using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Utilities.Map;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services
{
    public class CreditServices : ICreditServices
    {
        private IUnitOfWork _unitOfWork;
        private AutomapperConfig _automapper;

        public CreditServices(IUnitOfWork unitOfWork, AutomapperConfig automapperConfig)
        {
            _unitOfWork = unitOfWork;
            _automapper = automapperConfig;
        }

        public async Task<IEnumerable<CreditBll>> GetAll()
        {
            return Mapper.Map<IEnumerable<CreditBll>>(await _unitOfWork.CreditUnitOgWork.GetInclude("CarWashWorkers"));
        }

        public async Task Create(CreditBll credit)
        {
            Credit creditCreate = Mapper.Map<CreditBll, Credit>(credit);
            _unitOfWork.CreditUnitOgWork.Insert(creditCreate);
            await _unitOfWork.Save();
        }

        public async Task<IEnumerable<CreditBll>> GetCreditWhere()
        {
            return Mapper.Map<IEnumerable<CreditBll>>(await _unitOfWork.CreditUnitOgWork.Get());
        }

        public async Task<CreditBll> IdCredit(int id)
        {
            return Mapper.Map<CreditBll>(await _unitOfWork.CreditUnitOgWork.GetById(id));
        }
    }
}

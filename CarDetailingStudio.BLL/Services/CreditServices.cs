using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Utilities.Map;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;

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

        public IEnumerable<CreditBll> GetAll()
        {
            return Mapper.Map<IEnumerable<CreditBll>>(_unitOfWork.CreditUnitOgWork.GetInclude("CarWashWorkers"));
        }

        public void Create(CreditBll credit)
        {
            Credit creditCreate = Mapper.Map<CreditBll, Credit>(credit);
            _unitOfWork.CreditUnitOgWork.Insert(creditCreate);
            _unitOfWork.Save();
        }

        public IEnumerable<CreditBll> GetCreditWhere()
        {
            throw new NotImplementedException();
        }

        public CreditBll IdCredit(int id)
        {
            throw new NotImplementedException();
        }
    }
}

using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Trade.TradeContract;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace CarDetailingStudio.BLL.Services.Trade
{
    public class ProcurementServices : IProcurement
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProcurementServices(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<ProcurementBll>> GetTableAll()
        {
            throw new NotImplementedException();
        }

        public Task Insert(ProcurementBll element)
        {
            throw new NotImplementedException();
        }

        public Task<ProcurementBll> SelectId(int? elementId)
        {
            throw new NotImplementedException();
        }

        public Task Update(ProcurementBll elementToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}

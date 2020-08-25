using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.BLL.Model;
using System.Data.Entity;
using DevExpress.Internal;

namespace CarDetailingStudio.BLL.Services.Filters
{
    public class InitialAmountFiltersBll
    {
        private IUnitOfWork _unitOfWork;

        public InitialAmountFiltersBll()
        {
            _unitOfWork = new UnitOfWork();
        }

        public async Task<bool> Cashier()
        {
            DateTime date = DateTime.Now;
            var cashiesInfo = Mapper.Map<IEnumerable<CashierBll>>(await _unitOfWork.CashierUtilOfWork.GetWhere(x => DbFunctions.TruncateTime(x.date) == date.Date));

            if (cashiesInfo.Count() != 0 && cashiesInfo != null)
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

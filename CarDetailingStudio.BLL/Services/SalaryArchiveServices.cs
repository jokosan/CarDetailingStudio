using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace CarDetailingStudio.BLL.Services
{
    public class SalaryArchiveServices : ISalaryArchive
    {
        private readonly IUnitOfWork _unitOfWork;

        public SalaryArchiveServices(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<SalaryArchiveBll>> MonthlySalary(int idCarWashWorkers, int month, int year) =>
            Mapper.Map<IEnumerable<SalaryArchiveBll>>(await _unitOfWork.salaryArchiveUnitOfWork.QueryObjectGraph(x =>
                x.carWashWorkersId == idCarWashWorkers
                && x.date.Value.Month == month
                && x.date.Value.Year == year));

        public async Task<IEnumerable<SalaryArchiveBll>> MonthlySalary(int month, int year) =>
            Mapper.Map<IEnumerable<SalaryArchiveBll>>(await _unitOfWork.salaryArchiveUnitOfWork.QueryObjectGraph(x =>
                x.date.Value.Year == year
                && x.date.Value.Month == month, "CarWashWorkers"));

        public async Task<SalaryArchiveBll> LastMonth(int idCarWash)
        {
            var result = await SelectCarWashWorkers(idCarWash);
            return result.LastOrDefault();
        }

        public async Task<IEnumerable<SalaryArchiveBll>> GetTableAll() =>
            Mapper.Map<IEnumerable<SalaryArchiveBll>>(await _unitOfWork.salaryArchiveUnitOfWork.GetInclude("CarWashWorkers"));

        public async Task<SalaryArchiveBll> SelectId(int? elementId) =>
            Mapper.Map<SalaryArchiveBll>(await _unitOfWork.salaryArchiveUnitOfWork.GetById(elementId));

        public async Task<IEnumerable<SalaryArchiveBll>> SelectCarWashWorkers(int idCarWashWorkers)
        {
            return Mapper.Map<IEnumerable<SalaryArchiveBll>>(await _unitOfWork.salaryArchiveUnitOfWork.QueryObjectGraph(x => x.carWashWorkersId == idCarWashWorkers && x.salaryStatus == false));
        }

        public async Task Update(SalaryArchiveBll elementToUpdate)
        {
            _unitOfWork.salaryArchiveUnitOfWork.Update(TransformAnEntity(elementToUpdate));
            await _unitOfWork.Save();
        }
        public async Task Insert(SalaryArchiveBll element)
        {
            _unitOfWork.salaryArchiveUnitOfWork.Insert(TransformAnEntity(element));
            await _unitOfWork.Save();
        }

        public salaryArchive TransformAnEntity(SalaryArchiveBll entity) =>
            Mapper.Map<SalaryArchiveBll, salaryArchive>(entity);

    }
}

using CarDetailingStudio.DAL.Repositories;
using CarDetailingStudio.DataBase.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.UnitOfWorks
{
    public interface IUnitOfWork
    {
        void Dispose();
        void Dispose(bool disposing);
        void Save();
    }
}

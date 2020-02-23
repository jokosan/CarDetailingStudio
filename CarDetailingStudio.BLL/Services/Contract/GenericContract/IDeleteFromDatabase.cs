using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Contract.GenericContract
{
    interface IDeleteFromDatabase<T> where T : class
    {
        void Delete(T elementToDelete);
    }
}

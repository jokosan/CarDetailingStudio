using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface IServices<T> where T : class
    {
        IEnumerable<T> GetAll();
        //T GetId(int? id);
    }
}

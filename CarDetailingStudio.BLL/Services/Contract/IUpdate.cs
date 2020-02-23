using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface IUpdate<T> where T : class
    {
        void UpdateState(T entry);
    }
}

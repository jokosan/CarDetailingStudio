using CarDetailingStudio.BLL.Model.ModelViewBll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Modules.TireStorage.Contract
{
    public interface IReviwOrderModules
    {
        ReviwOrderModelBll ReviwOrder(OrderTireStorageModelBll tireStorage);
    }
}

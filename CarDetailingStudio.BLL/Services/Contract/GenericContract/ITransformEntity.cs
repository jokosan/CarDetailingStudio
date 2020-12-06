using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Contract.GenericContract
{
    public interface ITransformEntity<T> where T : class
    {
        T TransformAnEntity(T entity);
    }
}

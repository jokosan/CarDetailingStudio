﻿using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract.GenericContract;

namespace CarDetailingStudio.BLL.Services.TireStorageServices.TireStorageContract
{
    public interface ITireStorageServices : IGetFromDatabase<TireStorageServicesBll>, IDatabaseOperations<TireStorageServicesBll>
    {
    }
}

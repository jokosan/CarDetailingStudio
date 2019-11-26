﻿using System.Collections.Generic;
using CarDetailingStudio.BLL.Model;

namespace CarDetailingStudio.BLL.Services.Contract
{
    public interface IServisesCarWashOrderServices
    {
        IEnumerable<ServisesCarWashOrderBll> GetAll();
        IEnumerable<ServisesCarWashOrderBll> GetAllId(int? id);
        void ServicesDelete(int id, string NameClass);
        void ServisesInsert(List<ServisesCarWashOrderBll> idServeces, int idOrder, int idClient);
        
    }
}
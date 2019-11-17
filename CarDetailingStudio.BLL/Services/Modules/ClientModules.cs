using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Utilities.Map;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarDetailingStudio.DAL;

namespace CarDetailingStudio.BLL.Services.Modules
{
    public class ClientModules : IClientModules
    {
        private IUnitOfWork _unitOfWork;
        private AutomapperConfig _automapperConfig;

        public ClientModules(IUnitOfWork unitOfWork, AutomapperConfig automapperConfig)
        {
            _unitOfWork = unitOfWork;
            _automapperConfig = automapperConfig;
        }

        public void Distribute(ClientViewsBll client)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ClientViewsBll> JoinTable()
        {
            throw new NotImplementedException();
        }
    }
}
using AutoMapper;
using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Utilities.Map;
using CarDetailingStudio.DAL;
using CarDetailingStudio.DAL.Utilities.UnitOfWorks;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.Modules
{
    public class ClientModules : IClientModules
    {
        private IUnitOfWork _unitOfWork;
        private IClientsOfCarWashServices _clientsOfCarWash;
        private IClientInfoServices _clientInfo;
        private AutomapperConfig _automapperConfig;

        public ClientModules(IUnitOfWork unitOfWork, AutomapperConfig automapperConfig, IClientsOfCarWashServices clientsOfCarWashServices, IClientInfoServices clientInfo)
        {
            _unitOfWork = unitOfWork;
            _automapperConfig = automapperConfig;
            _clientsOfCarWash = clientsOfCarWashServices;
            _clientInfo = clientInfo;
        }

        public async Task<int> Distribute(ClientViewsBll client)

        {
            ClientsOfCarWashBll clientsOfCarWash = new ClientsOfCarWashBll();
            ClientInfoBll clientInfoBll = new ClientInfoBll();

            clientInfoBll.Name = client.Name;
            clientInfoBll.Surname = client.Surname;
            clientInfoBll.PatronymicName = client.PatronymicName;
            clientInfoBll.Phone = client.phone;
            clientInfoBll.DateRegistration = client.DateRegistration;
            clientInfoBll.Email = client.Email;
            clientInfoBll.barcode = client.barcode;
            clientInfoBll.note = client.note;

            ClientInfo clientInfo = Mapper.Map<ClientInfoBll, ClientInfo>(clientInfoBll);
            _unitOfWork.ClientInfoUnitOfWork.Insert(clientInfo);
            await _unitOfWork.Save();

            clientsOfCarWash.IdBody = client.IdBody;
            clientsOfCarWash.IdClientsGroups = client.IdClientsGroups;
            clientsOfCarWash.IdInfoClient = clientInfo.Id;
            clientsOfCarWash.IdMark = client.Idmark;
            clientsOfCarWash.IdModel = client.Idmodel;
            clientsOfCarWash.IdBody = client.IdBody;
            clientsOfCarWash.NumberCar = client.NumberCar;
            clientsOfCarWash.discont = client.discont;
            clientsOfCarWash.arxiv = true;

            ClientsOfCarWash clientsOfCar = Mapper.Map<ClientsOfCarWashBll, ClientsOfCarWash>(clientsOfCarWash);
            _unitOfWork.ClientsOfCarWashUnitOfWork.Insert(clientsOfCar);
            await _unitOfWork.Save();

            return clientsOfCar.id;
        }

    }
}
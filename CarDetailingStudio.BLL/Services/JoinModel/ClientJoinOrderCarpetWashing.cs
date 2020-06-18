using CarDetailingStudio.BLL.Model;
using CarDetailingStudio.BLL.Services.Contract;
using CarDetailingStudio.BLL.Services.JoinModel.Contract;
using CarDetailingStudio.BLL.Services.JoinModel.Model;
using DevExpress.Xpo.DB.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Services.JoinModel
{
    public class ClientJoinOrderCarpetWashing : IClientJoinOrderCarpetWashing
    {
        private IOrderCarpetWashingServices _orderCarpet;
        private IClientInfoServices _clientInfo;

        public ClientJoinOrderCarpetWashing(IOrderCarpetWashingServices orderCarpetWashing, IClientInfoServices clientInfoServices)
        {
            _orderCarpet = orderCarpetWashing;
            _clientInfo = clientInfoServices;
        }

        public async Task<IEnumerable<ClientJoinCarpetWashingModel>> JoinTableClientToCarpetWashing()
        {
            var client = await _clientInfo.ClientInfoAll();
            var orderCarpet = await _orderCarpet.GetTableAllInclude();

            var resultJoin = (from t1 in orderCarpet
                             join t2 in client on t1.clientId equals t2.Id
                             where t1.orderClosingDate == null
                             select new ClientJoinCarpetWashingModel()
                             {
                                 idOrderCarpetWashing = t1.idOrderCarpetWashing,
                                 clientId = t1.clientId,
                                 orderServicesCarWashId = t1.orderServicesCarWashId,
                                 Name = t2.Name,
                                 Surname = t2.Surname,
                                 PatronymicName = t2.PatronymicName,
                                 Phone = t2.Phone,
                                 DateRegistration = t2.DateRegistration,
                                 area = t1.area,
                                 orderDate = t1.orderDate,
                                 orderCompletionDate = t1.orderCompletionDate,
                                 orderClosingDate = t1.orderClosingDate,
                                 DiscountPrice = t1.OrderServicesCarWash.DiscountPrice
                                 
                             });

            return resultJoin.AsEnumerable();

        }

    }
}

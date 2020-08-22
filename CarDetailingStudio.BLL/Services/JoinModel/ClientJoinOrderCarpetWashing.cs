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

        public async Task<IEnumerable<ClientJoinCarpetWashingModel>> ActiveOrdersWashingCarpets()
        {
            var client = await _clientInfo.ClientInfoAll();
            var orderCarpet = await _orderCarpet.GetTableAllInclude();

            return JoinTableClientToCarpetWashing(client, orderCarpet);
        }

        public async Task<IEnumerable<ClientJoinCarpetWashingModel>> ActiveOrdersCarpets()
        {
            var client = await _clientInfo.ClientInfoAll();
            var orderCarpet = await _orderCarpet.GetTableAllIncludeArxiv();

            return JoinTableClientToCarpetWashing(client, orderCarpet);
        }

        private IEnumerable<ClientJoinCarpetWashingModel> JoinTableClientToCarpetWashing(IEnumerable<ClientInfoBll> client, IEnumerable<OrderCarpetWashingBll> orderCarpet)
        {                
            var resultJoin = (from t1 in orderCarpet
                              join t2 in client on t1.clientId equals t2.Id 
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
                                  DiscountPrice = t1.OrderServicesCarWash.DiscountPrice,
                                  statusOrder = t1.OrderServicesCarWash.StatusOrder1.StatusOrder1
                              });

            return resultJoin.AsEnumerable();
        }

    }
}

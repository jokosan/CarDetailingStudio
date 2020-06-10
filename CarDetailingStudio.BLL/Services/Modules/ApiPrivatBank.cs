using CarDetailingStudio.BLL.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Xml;

namespace CarDetailingStudio.BLL.Services.Modules
{
    public class ApiPrivatBank : IApiPrivatBank
    {
        XmlDocument xml = new XmlDocument();
        ExchangeRatesServices exchangeRatesServices = new ExchangeRatesServices();

        internal static List<ExchangeRatesBll> exchangeRatesModel = new List<ExchangeRatesBll>();

        public async Task ApiPrivat()
        {
            bool check = await exchangeRatesServices.CheckForUpdate();

            try
            {
                if (check != true)
                {
                    xml.Load(@"https://api.privatbank.ua/p24api/pubinfo?exchange&coursid=5");

                    XmlNodeList xmlNode = xml.GetElementsByTagName("exchangerate");

                    if (xmlNode != null)
                    {
                        foreach (XmlNode xn in xmlNode)
                        {
                            string xmlCcy = xn.Attributes["ccy"].Value;
                            string xmlBasa_ccy = xn.Attributes["base_ccy"].Value;
                            float xmlBuy = float.Parse(xn.Attributes["buy"].Value, CultureInfo.InvariantCulture.NumberFormat);
                            float xmlSale = float.Parse(xn.Attributes["sale"].Value, CultureInfo.InvariantCulture.NumberFormat);

                            exchangeRatesModel.Add(new ExchangeRatesBll()
                            {
                                ccy = xmlCcy,
                                base_ccy = xmlBasa_ccy,
                                buy = xmlBuy,
                                sale = xmlSale,
                                Date = DateTime.Now

                            });
                        }

                        if (check == false)
                        {
                           await exchangeRatesServices.UpdateTable();
                        }

                    }
                    else
                    {
                       await exchangeRatesServices.UpdateListExchangeRates();
                    }
                }
            }
            catch (Exception ex)
            {
                string log = ex.Message;
               await exchangeRatesServices.UpdateListExchangeRates();
            }
        }
    }
}

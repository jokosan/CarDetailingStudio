using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CarDetailingStudio.BLL.Services.Modules
{
    public class ApiPrivatBank
    {
        XmlDocument xml = new XmlDocument();

        public void ApiPrivat()
        {
            try
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

                        ExchangeRates.ExchangeRatesList.Add(new ExchangeRatesModel()
                        {
                            ccy = xmlCcy,
                            base_ccy = xmlBasa_ccy,
                            buy = xmlBuy,
                            sale = xmlSale
                        });
                    }
                }
            }
            catch(Exception ex)
            {
                string log = ex.Message;
            }      

        }
    }
}

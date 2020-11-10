using System.Collections.Generic;

namespace CarDetailingStudio.BLL.Model.ModelViewBll
{
    public class ReviwOrderModelBll
    {
        public double priceDisk { get; set; } // цена за хранения c дисками
        public double priceNumberOfPackets { get; set; } // цена за количество пакетов 
      
        public double priceOfTire { get; set; } // цена за хранение шин
        public double priceSilicone { get; set; } // цена за селекон
        public int IdpriceSilicone { get; set; } // id услуги  за мойку шин
        public double priceWheelWash { get; set; } // цена за мойку шин
        public int IdWheelWash { get; set; } // id услуги  за мойку шин
        public List<TireStorageServicesBll> tireStorageServices { get; set; }
    }
}

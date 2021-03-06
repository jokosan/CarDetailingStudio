﻿using CarDetailingStudio.BLL.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarDetailingStudio.Models
{
    public class ReviwOrderModelView
    {
        [Display(Name = "Сумма за хранение шин с дисками")]
        public double priceDisk { get; set; }

        [Display(Name = "Цена за количество пакетов для шин")]
        public double priceNumberOfPackets { get; set; }

        [Display(Name = "Стоимость за хранение шин")]
        public double priceOfTire { get; set; }

        [Display(Name = "Стоимость за обработку селиконом")]
        public double priceSilicone { get; set; }

        [Display(Name = "Стоимость за мойку шин")]
        public double priceWheelWash { get; set; }
        public int IdWheelWash { get; set; }  // id услуги  за мойку шин
        public int IdpriceSilicone { get; set; } // id услуги  за мойку шин

        public List<TireStorageServicesBll> tireStorageServices { get; set; }
    }
}
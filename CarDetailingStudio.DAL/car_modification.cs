//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarDetailingStudio.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class car_modification
    {
        public int id { get; set; }
        public int id_car_modification { get; set; }
        public Nullable<int> id_car_serie { get; set; }
        public Nullable<int> id_car_model { get; set; }
        public string name { get; set; }
        public Nullable<double> start_production_year { get; set; }
        public string end_production_year { get; set; }
        public Nullable<double> date_create { get; set; }
        public Nullable<double> date_update { get; set; }
        public Nullable<int> id_car_type { get; set; }
    
        public virtual car_serie car_serie { get; set; }
    }
}

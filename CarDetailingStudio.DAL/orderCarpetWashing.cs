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
    
    public partial class orderCarpetWashing
    {
        public int idOrderCarpetWashing { get; set; }
        public int orderServicesCarWashId { get; set; }
        public string Customer { get; set; }
        public string telephone { get; set; }
        public Nullable<double> area { get; set; }
        public Nullable<System.DateTime> orderDate { get; set; }
        public Nullable<System.DateTime> orderClosingDate { get; set; }
        public Nullable<System.DateTime> orderCompletionDate { get; set; }
    
        public virtual OrderServicesCarWash OrderServicesCarWash { get; set; }
    }
}

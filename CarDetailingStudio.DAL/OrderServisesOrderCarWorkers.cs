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
    
    public partial class OrderServisesOrderCarWorkers
    {
        public int Id { get; set; }
        public Nullable<int> IdClientsOfCarWash { get; set; }
        public Nullable<int> StatusOrder { get; set; }
        public Nullable<int> PaymentState { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public Nullable<System.DateTime> ClosingData { get; set; }
        public Nullable<double> DiscountPrice { get; set; }
        public Nullable<int> typeOfOrder { get; set; }
        public int Expr1 { get; set; }
        public int IdOrder { get; set; }
        public int IdCarWashWorkers { get; set; }
        public Nullable<double> Payroll { get; set; }
        public Nullable<int> typeServicesId { get; set; }
    }
}

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
    
    public partial class ItogOrderView
    {
        public int id { get; set; }
        public Nullable<int> countOrder { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string MobilePhone { get; set; }
        public Nullable<int> InterestRate { get; set; }
        public Nullable<bool> CalculationStatus { get; set; }
        public Nullable<double> Expr1 { get; set; }
    }
}

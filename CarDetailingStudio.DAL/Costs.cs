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
    
    public partial class Costs
    {
        public int Id { get; set; }
        public Nullable<int> Type { get; set; }
        public string Name { get; set; }
        public Nullable<double> expenses { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    
        public virtual TypeOfCosts TypeOfCosts { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDetailingStudio.Models.ModelViews
{
    public class OrderInfoViewModel
    {


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
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
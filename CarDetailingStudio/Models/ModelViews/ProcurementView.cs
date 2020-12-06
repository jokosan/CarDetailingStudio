using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class ProcurementView
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idProcurement { get; set; }
        public Nullable<int> ListOfGoodsId { get; set; }
        public Nullable<int> expensesId { get; set; }
        public Nullable<int> amount { get; set; }
        public Nullable<int> remainder { get; set; }

        public virtual ExpensesView Expenses { get; set; }
        public virtual ListOfGoodsView listOfGoods { get; set; }
    }
}
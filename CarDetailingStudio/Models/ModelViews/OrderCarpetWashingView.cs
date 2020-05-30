using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDetailingStudio.Models.ModelViews
{
    public class OrderCarpetWashingView
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idOrderCarpetWashing { get; set; }
        public int orderServicesCarWashId { get; set; }

        [Display(Name = "Заказчик*")]
        [Required]
        public string Customer { get; set; }

        [Display(Name = "Телефон")]
        public string telephone { get; set; }

        [Display(Name = "Площадь м2 ковра*")]
        [Required]
        public Nullable<double> area { get; set; }

        [Display(Name = "Дата оформления заказа*")]
        [Required]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> orderDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата закрытия заказа")]
        public Nullable<System.DateTime> orderClosingDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата выполнения заказа")]
        public Nullable<System.DateTime> orderCompletionDate { get; set; }

        public virtual OrderServicesCarWashView OrderServicesCarWash { get; set; }
    }
}
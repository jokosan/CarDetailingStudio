using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDetailingStudio.Models
{
    public class OrderTireStorageModelView
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public Nullable<int> ClientId { get; set; }
        public Nullable<int> carWashWorkersId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата оформления заказа")]
        public Nullable<System.DateTime> dateOfAdoption { get; set; }

        [Required(ErrorMessage = "Обязательное поле для заполнения")]
        [Display(Name = "Количество шин")]
        public Nullable<int> quantity { get; set; }

        [Required(ErrorMessage = "Обязательное поле для заполнения")]
        [Display(Name = "Радиус")]
        public Nullable<int> radius { get; set; }

        [Display(Name = "Фирма")]
        public string firm { get; set; }

        [Display(Name = "Наличие дисков")]
        public Nullable<int> discAvailability { get; set; }

        public Nullable<int> storageFeeId { get; set; }

        [Display(Name = "Количество пакетов (шт)")]
        [Range(0, 10000, ErrorMessage = "Допустимое значение от 0 до 100")]
        public Nullable<int> tireStorageBags { get; set; }

        [Display(Name = "Мойка колес (шт)")]
        public Nullable<int> wheelWash { get; set; }

        public Nullable<int> IdOrderServicesCarWash { get; set; }

        [Display(Name = "Селикон (шт)")]
        public Nullable<int> silicone { get; set; }

        [Required]
        [Display(Name = "Срок хранения")]
        public Nullable<int> storageTime { get; set; }
        public Nullable<int> stockNumber { get; set; }
    }
}
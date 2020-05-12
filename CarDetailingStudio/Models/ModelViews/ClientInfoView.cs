using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Models.ModelViews
{
    public class ClientInfoView
    {
       
        public ClientInfoView()
        {
            this.ClientsOfCarWash = new HashSet<ClientsOfCarWashView>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Display(Name = "Фамилия")]
        public string Surname { get; set; }
        
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Отчество")]
        public string PatronymicName { get; set; }

        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Display(Name = "Дата регистрации")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateRegistration { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        public string barcode { get; set; }

        [Display(Name = "Примечание")]
        public string note { get; set; }

        public virtual ICollection<ClientsOfCarWashView> ClientsOfCarWash { get; set; }
    }
}
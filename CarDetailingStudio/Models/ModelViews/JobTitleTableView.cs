using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDetailingStudio.Models.ModelViews
{
    public class JobTitleTableView
    {
        public JobTitleTableView()
        {
            this.CarWashWorkers = new HashSet<CarWashWorkersView>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Position { get; set; }

        public virtual ICollection<CarWashWorkersView> CarWashWorkers { get; set; }
    }
}
using System;

namespace CarDetailingStudio.Models.ModelViews
{
    public class CreditView
    {
        public int Id { get; set; }
        public Nullable<int> CarWashWorkersId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<double> Sum { get; set; }
        public Nullable<bool> RepaidDebt { get; set; }

        public virtual CarWashWorkersView CarWashWorkers { get; set; }
    }
}
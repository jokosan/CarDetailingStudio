using System.Collections.Generic;

namespace CarDetailingStudio.BLL.Model
{
    public class CarBodyBll
    {
        public CarBodyBll()
        {
            this.ClientsOfCarWash = new HashSet<ClientsOfCarWashBll>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ClientsOfCarWashBll> ClientsOfCarWash { get; set; }
    }
}

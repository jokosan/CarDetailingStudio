﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingStudio.BLL.Model
{
    public class ClientsGroupsBll
    {
        public ClientsGroupsBll()
        {
            this.ClientsOfCarWash = new HashSet<ClientsOfCarWashBll>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ClientsOfCarWashBll> ClientsOfCarWash { get; set; }
    }
}
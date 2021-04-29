using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace CarDetailingStudio.Context
{
    public partial class IdentityMotorClean : DbContext
    {
        public IdentityMotorClean()
            : base("name=IdentityMotorClean")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}

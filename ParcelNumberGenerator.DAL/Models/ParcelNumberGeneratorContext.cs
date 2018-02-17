using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelNumberGenerator.DAL.Models
{
    public class ParcelNumberGeneratorContext : DbContext
    {
        public ParcelNumberGeneratorContext() : base("DefaultConnection")
        {

        }
        public DbSet<User> User { get; set; }
    }
}

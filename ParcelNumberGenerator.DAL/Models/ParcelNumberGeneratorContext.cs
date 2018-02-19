using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelNumberGenerator.DAL.Models
{
    public class ParcelNumberGeneratorContext : DbContext
    {
        public ParcelNumberGeneratorContext() : base("ParcelNumberGenerator")
        {

        }
        public static ParcelNumberGeneratorContext Create() => new ParcelNumberGeneratorContext();

        public DbSet<UsedNumber> Numbers { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            
            //przykład zaciągnięty z innego projektu
            //modelBuilder.Entity<Ogloszenie>().HasRequired(x => x.Uzytkownik).WithMany(x => x.Ogloszenia).HasForeignKey(x => x.UzytkownikId).WillCascadeOnDelete(true);
        }
    }
}

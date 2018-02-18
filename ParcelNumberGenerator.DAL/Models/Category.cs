using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelNumberGenerator.DAL.Models
{
    public class Category
    {
        public Category()
        {

        }
        [Display(Name = "Id:")]
        public int ID { get; set; }
        [Display(Name = "Tresc ogloszenia:")]
        [MaxLength(500)]
        public string Tresc { get; set; }

    }
}

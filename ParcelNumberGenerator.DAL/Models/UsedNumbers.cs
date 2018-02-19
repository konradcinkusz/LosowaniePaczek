using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelNumberGenerator.DAL.Models
{
    public class UsedNumber
    {
        [Key]
        public int Id { get; set; }
        public int Number { get; set; }
    }
}

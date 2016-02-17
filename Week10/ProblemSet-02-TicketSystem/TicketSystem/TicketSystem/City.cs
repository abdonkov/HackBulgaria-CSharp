using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem
{
    public class City
    {
        [Key(), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CityID { get; set; }
        [Required]
        public string Name { get; set; }
    }
}

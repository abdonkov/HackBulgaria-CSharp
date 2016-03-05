using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem
{
    public class Train
    {
        [Key(), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrainID { get; set; }
        [Required]
        public int NumberOfSeats { get; set; }
        public virtual ICollection<Seat> Seats { get; set; }
        [Required]
        public string BriefDescription { get; set; }
    }
}

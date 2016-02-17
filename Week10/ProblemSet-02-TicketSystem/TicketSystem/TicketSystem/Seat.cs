using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem
{
    public class Seat
    {
        [Key, Column(Order = 0)]
        public int SeatNumber { get; set; }
        [Required]
        public bool Taken { get; set; }
        [Required]
        public SeatClass Class { get; set; }

        [Key, Column(Order = 1)]
        public int TrainID { get; set; }
        [ForeignKey("TrainID")]
        public virtual Train Train { get; set; }

        [Key, Column(Order = 2)]
        public int ScheduleID { get; set; }
        [ForeignKey("ScheduleID")]
        public virtual Schedule Schedule { get; set; }

    }

    public enum SeatClass { FirstClass = 1, SecondClass} 
}

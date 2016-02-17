using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem
{
    public class Ticket
    {
        [Key(), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TicketID { get; set; }
        [Required]
        public DateTime TripDateAndTime { get; set; }
        [Required]
        public decimal OriginalPrice { get; set; }
        [Required]
        public decimal PriceSold { get; set; }

        [Required]
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        [Required]
        [ForeignKey("Seat"), Column(Order = 0)]
        public int SeatNumber { get; set; }
        [Required]
        [ForeignKey("Seat"), Column(Order = 1)]
        public int TrainID { get; set; }
        [Required]
        [ForeignKey("Seat"), Column(Order = 2)]
        public int ScheduleID { get; set; }
        public virtual Seat Seat { get; set; }
    }
}

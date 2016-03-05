using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem
{
    public class Schedule
    {
        [Key(), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ScheduleID { get; set; }

        public int StartCityID { get; set; }
        [ForeignKey("StartCityID")]
        public virtual City StartCity { get; set; }

        public int EndCityID { get; set; }
        [ForeignKey("EndCityID")]
        public virtual City EndCity { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public TimeSpan TravelTime { get; set; }

        [Required]
        public int TrainID { get; set; }
        [ForeignKey("TrainID")]
        public virtual Train Train { get; set; }

        [Required]
        public decimal TicketPrice { get; set; }
    }
}

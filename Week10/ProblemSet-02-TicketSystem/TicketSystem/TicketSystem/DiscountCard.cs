using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem
{
    public class DiscountCard
    {
        [Key]
        public string DiscountCardID { get; set; }
        [Required]
        public DiscountCardType Type { get; set; }
        [Required]
        public decimal Discount { get; set; }
        public List<SeatClass> AppliesTo { get; set; }
    }

    public enum DiscountCardType { Student = 1, ISIC}
}

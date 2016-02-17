using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem
{
    public class User
    {
        [Key(), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        [Required]
        public string UserName { get; internal set; }
        [Required]
        public string Password { get; internal set; }
        [Required]
        public string Salt { get; internal set; }
        [Required]
        public string Email { get; set; }

        public string DiscountCardID { get; set; }
        [ForeignKey("DiscountCardID")]
        public virtual DiscountCard DiscountCard { get; set; }
        [Required]
        public string CreditCardNumber { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public User() { }

        public User(string userName, string password)
        {
            string salt = TicketSystemSecurity.GenerateSalt(32);
            string hashedPassword = TicketSystemSecurity.GenerateSHA256Hash(password, salt);
            UserName = userName;
            Password = hashedPassword;
            Salt = salt;
        }
    }
}

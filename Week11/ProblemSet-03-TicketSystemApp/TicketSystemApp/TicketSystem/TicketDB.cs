namespace TicketSystem
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Data.Entity.Validation;
    using System.Diagnostics;
    using System.Linq;

    public class TicketDB : DbContext
    {
        public TicketDB()
            : base("name=TicketDB")
        {
        }

        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<DiscountCard> DiscountCards {get; set;}
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<Seat> Seats { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Train> Trains { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }        
    }
}
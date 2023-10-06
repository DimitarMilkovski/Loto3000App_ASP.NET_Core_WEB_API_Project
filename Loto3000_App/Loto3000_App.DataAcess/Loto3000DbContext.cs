using Loto3000_App.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000_App.DataAcess
{
    public class Loto3000DbContext : DbContext
    {
        public Loto3000DbContext(DbContextOptions options) :base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Combination> Combinations { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Winner> Winners { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //User
            modelBuilder.Entity<User>()
                .Property(x => x.Username)
                .HasMaxLength(30)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(x => x.Password)
                .HasMaxLength(30)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(x => x.Firstname)
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(x => x.Lastname)
                .HasMaxLength(50);

            //Ticket
            modelBuilder.Entity<Ticket>()
                .HasOne(x => x.User)
                .WithMany(x => x.Tickets)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<Ticket>()
                .HasOne(x=>x.Session)
                .WithMany(x=>x.TicketList)
                .HasForeignKey(x => x.SessionId);


            modelBuilder.HasSequence<int>("TicketNumber")
                .StartsAt(1000)
                .IncrementsBy(1);

            modelBuilder.Entity<Ticket>()
                .Property(t => t.TicketNumber)
                .HasDefaultValueSql("NEXT VALUE FOR TicketNumber");

            //Combination
            modelBuilder.Entity<Combination>()
                .HasOne(x => x.Ticket)
                .WithMany(x => x.Combinations)
                .HasForeignKey(x => x.TicketId);

            //Session
            //modelBuilder.Entity<Session>()
            //    .Property(x => x.EndDate)
            //    .IsRequired(false);

            //Winner
            modelBuilder.Entity<Winner>()
                .HasOne(x=>x.User)
                .WithMany(x=>x.WonPrizes)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<Winner>()
                .Property(x => x.Prize)
                .IsRequired();
            modelBuilder.Entity<Winner>()
                .HasOne(x=>x.Session)
                .WithMany(x=>x.WinnerList)
                .HasForeignKey(x => x.SessionId);

            

            







        }


    }
}

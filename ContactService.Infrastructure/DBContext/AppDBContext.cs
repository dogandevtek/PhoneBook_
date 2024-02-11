using ContactService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Infrastructure.DBContext {
    public class AppDBContext : DbContext {
        public AppDBContext() {
                    
        }

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserContact> UserContacts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) 
                optionsBuilder.UseNpgsql("Server=localhost;Port=5432;userid=postgres;Password=1234;Database=PhoneBook;");
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<UserContact>(e => {
                e.HasKey(e => e.Id);

                e.HasOne(c => c.User)
                    .WithMany(u => u.UserContactList)
                    .HasForeignKey(c => c.UserId);
            });


            modelBuilder.Entity<User>(e => {
                e.HasKey(e => e.Id);
            });
        }
    }
}

using ReportService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Infrastructure.DBContext {
    public class ReportDBContext : DbContext {
        public ReportDBContext() {
                    
        }

        public ReportDBContext(DbContextOptions<ReportDBContext> options) : base(options) {
        }

        public DbSet<Report> Reports { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) 
                optionsBuilder.UseNpgsql("Server=localhost;Port=5432;userid=postgres;Password=1234;Database=Reports;");            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Report>(e => {
                e.HasKey(e => e.Id);
            });
        }
    }
}

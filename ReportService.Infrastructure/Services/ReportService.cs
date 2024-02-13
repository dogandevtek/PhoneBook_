using ReportService.Application.Services;
using ReportService.Domain.Entities;
using ReportService.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Infrastructure.Services {
    public class ReportService : IReportService {
        private DBContext.ReportDBContext _dbContext { get; }

        public ReportService(DBContext.ReportDBContext dbContext) {
            _dbContext = dbContext;

            dbContext.Database.Migrate();
        }

        public async Task<List<Report>> GetAllAsync() {
            return await _dbContext.Reports.AsNoTracking().ToListAsync();
        }

        public async Task<Report> GetAsync(int id) {
            return await _dbContext.Reports.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Report> CreateAsync(Report report) {
            report.CreatedAt = report.UpdatedAt = DateTime.UtcNow;
            _dbContext.Reports.Add(report);

            await _dbContext.SaveChangesAsync();

            return report;
        }

        public async Task<Report> UpdateAsync(Report report) {
            report.UpdatedAt = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync(); 

            return report;
        }

        public async Task DeleteAsync(Report report) {
            _dbContext.Reports.Remove(report);
            await _dbContext.SaveChangesAsync();
        }
    }
}

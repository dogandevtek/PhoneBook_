
using ReportService.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Infrastructure {
    public static class DependencyInjection {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
#if DEBUG
            services.AddDbContext<ReportDBContext>(o => o.UseNpgsql(configuration["ConnectionStrings:PostgreSql_Debug"]));

            ReportDBContext.ConnectionString = configuration["ConnectionStrings:PostgreSql_Debug"];
            using (var dbContext = new ReportDBContext()) {
                dbContext.Database.Migrate();
            }

#else
            services.AddDbContext<ReportDBContext>(o => o.UseNpgsql(configuration["ConnectionStrings:PostgreSql_Release"]));
            
            ReportDBContext.ConnectionString = configuration["ConnectionStrings:PostgreSql_Release"];
            using (var dbContext = new ReportDBContext()) {
                dbContext.Database.Migrate();
            }
#endif

            return services;
        }
    }
}

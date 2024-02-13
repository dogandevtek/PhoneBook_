
using ContactService.Domain.Entities;
using ContactService.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Infrastructure {
    public static class DependencyInjection {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
#if DEBUG
            services.AddDbContext<AppDBContext>(o => o.UseNpgsql(configuration["ConnectionStrings:PostgreSql_Debug"]));


            AppDBContext.ConnectionString = configuration["ConnectionStrings:PostgreSql_Debug"];
            using (var dbContext = new AppDBContext()) {
                dbContext.Database.Migrate();

                insertInitialData(dbContext);
            }
#else
            services.AddDbContext<AppDBContext>(o => o.UseNpgsql(configuration["ConnectionStrings:PostgreSql_Release"]));
            
            AppDBContext.ConnectionString = configuration["ConnectionStrings:PostgreSql_Release"];
            using (var dbContext = new AppDBContext()) {
                dbContext.Database.Migrate();
                
                insertInitialData(dbContext);
            }
#endif

            return services;
        }

        private static void insertInitialData(AppDBContext dbContext) {
            if (dbContext.Users.Count() == 0) {
                var users = new List<Domain.Entities.User>()
                {
                            new Domain.Entities.User()
                            {
                                Name = "Ahmet",
                                Lastname = "Yılmaz",
                                Company = "ABC Şirketi",
                                CreatedAt = DateTime.UtcNow,
                                UpdatedAt = DateTime.UtcNow,
                                UserContactList = new List<Domain.Entities.UserContact>()
                                {
                                    new Domain.Entities.UserContact()
                                    {
                                        PhoneNumber = "0555 555 55 55",
                                        E_mail = "ahmet@example.com",
                                        Location = "İstanbul",
                                        CreatedAt = DateTime.UtcNow,
                                        UpdatedAt = DateTime.UtcNow
                                    },
                                    new Domain.Entities.UserContact()
                                    {
                                        PhoneNumber = "0555 444 44 44",
                                        E_mail = "ahmet.y@example.com",
                                        Location = "Ankara",
                                        CreatedAt = DateTime.UtcNow,
                                        UpdatedAt = DateTime.UtcNow
                                    }
                                }
                            },
                            new Domain.Entities.User()
                            {
                                Name = "Ayşe",
                                Lastname = "Kaya",
                                Company = "XYZ Şirketi",
                                CreatedAt = DateTime.UtcNow,
                                UpdatedAt = DateTime.UtcNow,
                                UserContactList = new List<Domain.Entities.UserContact>()
                                {
                                    new Domain.Entities.UserContact()
                                    {
                                        PhoneNumber = "0555 555 56 98",
                                        E_mail = "ayse.y@example.com",
                                        Location = "İstanbul",
                                        CreatedAt = DateTime.UtcNow,
                                        UpdatedAt = DateTime.UtcNow
                                    },
                                    new Domain.Entities.UserContact()
                                    {
                                        PhoneNumber = "0555 333 33 33",
                                        E_mail = "ayse@example.com",
                                        Location = "İzmir",
                                        CreatedAt = DateTime.UtcNow,
                                        UpdatedAt = DateTime.UtcNow
                                    }
                                }
                            },
                        };

                dbContext.Users.AddRange(users);
                dbContext.SaveChanges();
            }
        }
    }

}

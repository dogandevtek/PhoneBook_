using EventBus.Base.Abstraction;
using Newtonsoft.Json;
using ReportGeneratorService.API.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ReportGeneratorService.Services {

    public interface IReportGenerator {
        Task generate(int reportId);
    }

    public class ReportGenerator : IReportGenerator {
        private IEventBus _eventBus;
        public ReportGenerator(IEventBus eventBus) {
            _eventBus = eventBus;
        }

        public async Task generate(int reportId) {
            try {
                using (var client = new HttpClient()) {
#if DEBUG
                    client.BaseAddress = new Uri("http://localhost:5009/");
#else
                    client.BaseAddress = new Uri("http://contact_service_api:5009/");
#endif


                    // Make HTTP GET request to the API
                    HttpResponseMessage response = await client.GetAsync("api/User/GetAllWithDetails");

                    // If the response is successful
                    if (response.IsSuccessStatusCode) {
                        // Read the response content as a string
                        string responseBody = await response.Content.ReadAsStringAsync();

                        // Deserialize the JSON response to a list of UserEDTO objects
                        List<User> users = JsonConvert.DeserializeObject<List<User>>(responseBody);

                        var statistics = new LocationBasedStatistics {
                            Statistics = users.SelectMany(u => u.UserContactList, (user, contact) => new { user, contact })
                                              .GroupBy(x => x.contact.Location)
                                              .Select(g => new Statistic {
                                                  Location = g.Key,
                                                  TotalUsers = g.Select(x => x.user).Distinct().Count(),
                                                  TotalPhoneNumbers = g.Select(x => x.contact.PhoneNumber).Count()
                                              })
                                              .ToList()
                        };


                        //Docker uzerinde bir network diskini mount edip oraya yazacagim dosya path'ini paylasmak yerine pratik olmasi acisindan direk veriyi json olarak paylasmayi tercih ettim.
                        var json = JsonConvert.SerializeObject(statistics);
                        _eventBus.Publish(new ReportStatusChangedIntegrationEvent(reportId, Domain.Enum.ReportStatuses.Ready, json));

                    } else {
                        Console.WriteLine($"Failed to retrieve data. Status code: {response.StatusCode}");
                    }
                }
            } catch (Exception ex) {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }

    public class LocationBasedStatistics {
        public List<Statistic> Statistics { get; set; }
    }

    public class Statistic {
        public string Location { get; set; }
        public int TotalUsers { get; set; }
        public int TotalPhoneNumbers { get; set; }
    }

    public class User {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Company { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<UserContact> UserContactList { get; set; }
    }

    public class UserContact {

        public int Id { get; set; }
        public int UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string E_mail { get; set; }
        public string Location { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

using EventBus.Base.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportGeneratorService.Services {

    public interface IReportGenerator {
        Task run(int reportId);
    }

    public class ReportGenerator : IReportGenerator {
        private IEventBus _eventBus;
        public ReportGenerator(IEventBus eventBus) {
            _eventBus = eventBus;
        }

        public Task run(int reportId) {
            throw new NotImplementedException();
        }
    }
}

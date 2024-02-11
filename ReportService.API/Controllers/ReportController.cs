using AutoMapper;
using ReportService.Application.Models;
using ReportService.Application.Services;
using ReportService.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using EventBus.Base.Abstraction;
using ReportService.API.IntegrationEvents.Events;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReportService.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {

        private IReportService _reportService { get; }
        private IMapper _mapper { get; }
        private IEventBus _eventBus { get; }

        public UserController(IReportService reportService, IMapper mapper, IEventBus eventBus) {
            _reportService = reportService;
            _mapper = mapper;
        }

        //TODO pagination ve filtreleme eklenebilir
        [HttpGet("[action]")]
        public async Task<ActionResult<List<ReportDTO>>> GetAllAsync() {
            var reportList = await _reportService.GetAllAsync();

            return Ok(_mapper.Map<List<ReportDTO>>(reportList));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReportDTO>> GetAsync(int id) {
            var report = await _reportService.GetAsync(id);
            if (report == null)
                return NotFound("Report not found!");

            return Ok(_mapper.Map<ReportDTO>(report));
        }

        [HttpPost]
        public async Task<ActionResult<ReportDTO>> PostAsync() {
            var report = await _reportService.CreateAsync(new Report() { Status = Domain.Enum.ReportStatuses.Pending, Path = "" });
            _eventBus.Publish(new NewReportRequestedIntegrationEvent(report.Id));

            return Ok(_mapper.Map<ReportDTO>(report));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id) {
            var report = await _reportService.GetAsync(id);
            if (report == null)
                return NotFound("Report not found!");

            await _reportService.DeleteAsync(report);
            return Ok();
        }
    }
}

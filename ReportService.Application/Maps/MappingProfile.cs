using AutoMapper;
using ReportService.Application.Models;
using ReportService.Domain.Entities;

namespace ReportService.Application.Maps {
    public class MappingProfile : Profile {

        public MappingProfile()
        {
            CreateMap<Report, ReportDTO>().ReverseMap();
        }
    }
}

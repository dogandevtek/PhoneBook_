using AutoMapper;
using ContactService.Application.Models.Request;
using ContactService.Application.Models.Response;
using ContactService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Application.Maps {
    public class MappingProfile : Profile {

        public MappingProfile()
        {
            CreateMap<UserUDTO, User>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserEDTO>().ReverseMap();

            CreateMap<UserContactUDTO, UserContact>().ReverseMap();
            CreateMap<UserContact, UserContactDTO>().ReverseMap();
        }
    }
}

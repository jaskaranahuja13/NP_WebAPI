using AutoMapper;
using NP_WebAPI.DTOs;
using NP_WebAPI.Models;

namespace NP_WebAPI.DTOMapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<NationalParkDto, NationalPark>().ReverseMap();
            CreateMap<Trail, TrailDto>().ReverseMap();
        }
    }
}
//DB--MODEL--REPOSITORY--DTO--CLIENT
//CLIENT--DTO--REPOSITORY--MODEL--DB
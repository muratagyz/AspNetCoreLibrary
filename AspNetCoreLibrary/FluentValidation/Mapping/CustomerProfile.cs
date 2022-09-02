using AspNetCoreLibrary.FluentValidation.Dtos;
using AutoMapper;
using FluentValidation.Models;

namespace AspNetCoreLibrary.FluentValidation.AutoMapper.Mapping;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CustomerDto>()
            .ForMember(dest => dest.Isim, opt => opt.MapFrom(x => x.Name))
            .ForMember(dest => dest.Eposta, opt => opt.MapFrom(x => x.Email))
            .ForMember(dest => dest.Yas, opt => opt.MapFrom(x => x.Age))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(x => x.GetFullName()))
            .ReverseMap();
    }
}
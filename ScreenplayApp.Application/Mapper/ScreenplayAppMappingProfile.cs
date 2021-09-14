using AutoMapper;
using ScreenplayApp.Application.Commands;
using ScreenplayApp.Application.Responses;
using ScreenplayApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScreenplayApp.API.Extensions;

namespace ScreenplayApp.Application.Mapper
{
    public class ScreenplayAppMappingProfile : Profile
    {
        public ScreenplayAppMappingProfile()
        {
            CreateMap<Booking, CreateBookingCommand>().ReverseMap();
            CreateMap<Booking, BookingResponse>().ReverseMap();
            CreateMap<Screenplay, ScreenplayResponse>()
                .ForMember(dest => dest.AverageRate, opt => opt.MapFrom(src => src.Ratings.CalculateRate()))
                .ReverseMap();
            CreateMap<Screenplay, CreateScreenplayCommand>().ReverseMap();
            CreateMap<Rating, CreateRatingCommand>().ReverseMap();
            CreateMap<AppUser, AccountResponse>().ReverseMap();
            CreateMap<AppUser, CreateAccountCommand>().ReverseMap();
            CreateMap<AppUser, LoginAccountCommand>().ReverseMap();
        }
    }
}

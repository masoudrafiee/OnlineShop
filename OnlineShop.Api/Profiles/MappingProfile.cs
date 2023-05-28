using AutoMapper;
using OnlineShop.Api.Dto;
using OnlineShop.Application.Commands;

namespace OnlineShop.Api.Profiles
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<BasketDto, CreateBasketCommand>(); 
        }
    }
}

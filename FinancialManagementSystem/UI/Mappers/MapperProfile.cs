using AutoMapper;
using Core.Entities;

namespace UI.Models
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ChartOfAccount, AccountDto>();
        }

    }   
}
using AutoMapper;
using Models;
using Services.Cities;

namespace Services.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<City, City>();
        CreateMap<City, CityDto>();
    }
}


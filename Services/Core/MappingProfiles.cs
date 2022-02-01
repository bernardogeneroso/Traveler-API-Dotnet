using AutoMapper;
using Models;
using Services.Cities.DTOs;
using Services.CitiesDetails;
using Services.CitiesDetails.DTOs;

namespace Services.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        string currentOrigin = null;

        CreateMap<City, CityDtoQuery>()
                .ForMember(dest => dest.ImageName,
                    src => src.MapFrom(x =>
                        x.ImageName != null ?
                        $"{currentOrigin}/images/{x.ImageName}" :
                        $"{currentOrigin}/images/user.png"));
        CreateMap<CityDtoRequest, City>();
        CreateMap<CityDetail, CityDetailDtoQuery>();
        CreateMap<CityDetailDtoRequest, CityDetail>();
    }
}


using AutoMapper;
using Models;
using Services.Cities.DTOs;
using Services.CitiesCategories.DTOs;
using Services.CitiesDetails;
using Services.CitiesDetails.DTOs;
using Services.CitiesPlaces.DTOs;
using Services.CitiesPlacesSchedules.DTOs;

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

        CreateMap<CategoryCity, CategoryCityDtoQuery>()
                .ForMember(dest => dest.ImageName,
                    src => src.MapFrom(x =>
                        x.ImageName != null ?
                        $"{currentOrigin}/images/{x.ImageName}" : null));
        CreateMap<CategoryCityDtoRequest, CategoryCity>();

        CreateMap<CityPlaceDtoRequest, CityPlace>();
        CreateMap<CityPlace, CityPlaceDtoListQuery>()
                .ForMember(dest => dest.ImageName,
                    src => src.MapFrom(x =>
                        x.ImageName != null ?
                        $"{currentOrigin}/images/{x.ImageName}" : null));
        CreateMap<CityPlace, CityPlaceDtoQuery>()
                .ForMember(dest => dest.ImageName,
                    src => src.MapFrom(x =>
                        x.ImageName != null ?
                        $"{currentOrigin}/images/{x.ImageName}" : null));

        CreateMap<CityPlaceScheduleResult, CityPlaceSchedule>();
    }
}


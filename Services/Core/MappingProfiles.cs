using AutoMapper;
using Models;
using Models.Helpers;
using Services.Cities.DTOs;
using Services.CitiesCategories.DTOs;
using Services.CitiesDetails;
using Services.CitiesDetails.DTOs;
using Services.CitiesPlaces.DTOs;
using Services.CitiesPlacesSchedules.DTOs;
using Services.PlacesMessages.DTOs;

namespace Services.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        string currentOrigin = null;

        CreateMap<City, CityDtoQuery>()
                .ForMember(dest => dest.Image,
                opt => opt.MapFrom(src => src.ImageName != null ?
                    new Image { Name = src.ImageName, Url = $"{currentOrigin}/images/{src.ImageName}" }
                    : null
                ));
        CreateMap<CityDtoRequest, City>();

        CreateMap<CityDetail, CityDetailDtoQuery>();
        CreateMap<CityDetailDtoRequest, CityDetail>();

        CreateMap<CategoryCity, CategoryCityDtoQuery>()
                .ForMember(dest => dest.Image,
                opt => opt.MapFrom(src => src.ImageName != null ?
                    new Image { Name = src.ImageName, Url = $"{currentOrigin}/images/{src.ImageName}" }
                    : null
                ));
        CreateMap<CategoryCityDtoRequest, CategoryCity>();

        CreateMap<CityPlaceDtoRequest, CityPlace>();
        CreateMap<CityPlace, CityPlaceDtoListQuery>()
                .ForMember(dest => dest.Image,
                opt => opt.MapFrom(src => src.ImageName != null ?
                    new Image { Name = src.ImageName, Url = $"{currentOrigin}/images/{src.ImageName}" }
                    : null
                ));
        CreateMap<CityPlace, CityPlaceDtoQuery>()
                .ForMember(dest => dest.Image,
                opt => opt.MapFrom(src => src.ImageName != null ?
                    new Image { Name = src.ImageName, Url = $"{currentOrigin}/images/{src.ImageName}" }
                    : null
                ));

        CreateMap<CityPlaceSchedule, CityPlaceScheduleDtoQuery>();
        CreateMap<CityPlaceScheduleDtoResult, CityPlaceSchedule>();

        CreateMap<CityPlaceMessage, CityPlaceMessageDtoQuery>();
        CreateMap<CityPlaceMessageDtoResult, CityPlaceMessage>();
    }
}


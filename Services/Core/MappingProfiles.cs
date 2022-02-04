using AutoMapper;
using Models;
using Models.Helpers;
using Services.Cities.DTOs;
using Services.CitiesCategories.DTOs;
using Services.CitiesDetails;
using Services.CitiesPlaces.DTOs;
using Services.CitiesPlacesSchedules.DTOs;
using Services.PlacesMessages.DTOs;

namespace Services.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        string currentUrlCloudinary = null;

        CreateMap<City, CityDtoQuery>()
                .ForMember(dest => dest.Image,
                opt => opt.MapFrom(src => src.ImageName != null ?
                    new ImageDto { Name = src.ImageName, Url = $"{currentUrlCloudinary}/{src.ImagePublicId}", PublicId = src.ImagePublicId }
                    : null
                ));
        CreateMap<City, CityDtoDetailsQuery>()
                .ForMember(dest => dest.Image,
                opt => opt.MapFrom(src => src.ImageName != null ?
                    new ImageDto { Name = src.ImageName, Url = $"{currentUrlCloudinary}/{src.ImagePublicId}", PublicId = src.ImagePublicId }
                    : null
                ));
        CreateMap<CityDtoRequest, City>()
                .ForPath(dest => dest.Detail.Description, opt => opt.MapFrom(src => src.Description))
                .ForPath(dest => dest.Detail.SubDescription, opt => opt.MapFrom(src => src.SubDescription));
        CreateMap<CityDtoCreateRequest, City>()
                .ForPath(dest => dest.Detail.Description, opt => opt.MapFrom(src => src.Description))
                .ForPath(dest => dest.Detail.SubDescription, opt => opt.MapFrom(src => src.SubDescription));

        CreateMap<CityDetail, CityDetailDtoQuery>();

        CreateMap<CategoryCity, CategoryCityDtoQuery>()
                .ForMember(dest => dest.Image,
                opt => opt.MapFrom(src => src.ImageName != null ?
                    new ImageDto { Name = src.ImageName, Url = $"{currentUrlCloudinary}/{src.ImagePublicId}", PublicId = src.ImagePublicId }
                    : null
                ));
        CreateMap<CategoryCityDtoRequest, CategoryCity>();

        CreateMap<CityPlaceDtoRequest, CityPlace>();
        CreateMap<CityPlace, CityPlaceDtoListQuery>()
                .ForMember(dest => dest.Image,
                opt => opt.MapFrom(src => src.ImageName != null ?
                    new ImageDto { Name = src.ImageName, Url = $"{currentUrlCloudinary}/{src.ImageName}", PublicId = src.ImagePublicId }
                    : null
                ));
        CreateMap<CityPlace, CityPlaceDtoQuery>()
                .ForMember(dest => dest.Image,
                opt => opt.MapFrom(src => src.ImageName != null ?
                    new ImageDto { Name = src.ImageName, Url = $"{currentUrlCloudinary}/{src.ImageName}", PublicId = src.ImagePublicId }
                    : null
                ));
        CreateMap<CityPlace, CityPlaceDtoHighlightQuery>()
                .ForMember(dest => dest.Image,
                opt => opt.MapFrom(src => src.ImageName != null ?
                    new ImageDto { Name = src.ImageName, Url = $"{currentUrlCloudinary}/{src.ImageName}", PublicId = src.ImagePublicId }
                    : null
                ));

        CreateMap<CityPlaceSchedule, CityPlaceScheduleDtoQuery>();
        CreateMap<CityPlaceScheduleDtoResult, CityPlaceSchedule>();

        CreateMap<CityPlaceMessage, CityPlaceMessageDtoQuery>();
        CreateMap<CityPlaceMessageDtoResult, CityPlaceMessage>();
    }
}


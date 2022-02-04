using Application.Core;
using AutoMapper;
using Database;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Helpers;
using Services.Interfaces;
using Services.PlacesMessages.DTOs;

namespace Services.PlacesMessages;

public class Create
{
    public class Command : IRequest<Result<CityPlaceMessageDtoCommand>>
    {
        public Guid PlaceId { get; set; }
        public CityPlaceMessageDtoResult Message { get; set; }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Message).SetValidator(new CityPlaceMessageValidator()).NotEmpty();
        }
    }

    public class Handler : IRequestHandler<Command, Result<CityPlaceMessageDtoCommand>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IOriginAccessor _originAccessor;
        public Handler(DataContext context, IMapper mapper, IOriginAccessor originAccessor)
        {
            _originAccessor = originAccessor;
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<CityPlaceMessageDtoCommand>> Handle(Command request, CancellationToken cancellationToken)
        {
            if (!await _context.CityPlace.AnyAsync(x => x.Id == request.PlaceId, cancellationToken)) return Result<CityPlaceMessageDtoCommand>.Failure("Failed to create message");

            var cityPlaceMessage = _mapper.Map<CityPlaceMessage>(request.Message);

            cityPlaceMessage.PlaceId = request.PlaceId;

            _context.CityPlaceMessage.Add(cityPlaceMessage);

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<CityPlaceMessageDtoCommand>.Failure("Failed to create message");

            var averangeMessagesOfPlace = await _context.CityPlaceMessage
                    .Where(x => x.PlaceId == request.PlaceId)
                    .Select(x => x.Rating)
                    .AverageAsync(cancellationToken);

            var averageRoundMessage = (float)Math.Round(averangeMessagesOfPlace * 2, MidpointRounding.AwayFromZero) / 2;
            var averageRoundPlace = (float)Math.Round(averangeMessagesOfPlace, 1);

            var place = new CityPlace
            {
                Id = request.PlaceId,
                Rating = averageRoundPlace
            };

            _context.Attach(place);

            _context.Entry(place).Property(x => x.Rating).IsModified = true;

            var result2 = await _context.SaveChangesAsync() > 0;

            if (!result2) return Result<CityPlaceMessageDtoCommand>.Failure("Failed to create message");

            var cityPlaceMessageDto = _mapper.Map<CityPlaceMessageDtoQuery>(cityPlaceMessage,
                    opt => opt.AfterMap((src, dest) => dest.Avatar =
                    new Avatar { Name = "user.png", Url = $"{_originAccessor.GetOrigin()}/images/user.png" }));

            var cityPlaceMessageDtoCommand = new CityPlaceMessageDtoCommand
            {
                Message = cityPlaceMessageDto,
                PlaceRating = averageRoundMessage
            };

            return Result<CityPlaceMessageDtoCommand>.Success(cityPlaceMessageDtoCommand);
        }
    }
}
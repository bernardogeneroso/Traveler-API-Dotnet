using Application.Core;
using AutoMapper;
using Database;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models;
using Models.Helpers;
using Services.Interfaces;
using Services.PlacesMessages.DTOs;
using Services.SignalR;

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
            RuleFor(x => x.Message).SetValidator(new CityPlaceMessageValidator());
        }
    }

    public class Handler : IRequestHandler<Command, Result<CityPlaceMessageDtoCommand>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IHubContext<ChatHub, IChatHub> _hubContext;
        private readonly IImageAccessor _imageAccessor;
        private readonly IConfiguration _config;
        public Handler(DataContext context, IMapper mapper, IHubContext<ChatHub, IChatHub> hubContext, IImageAccessor imageAccessor, IConfiguration config)
        {
            _config = config;
            _imageAccessor = imageAccessor;
            _mapper = mapper;
            _hubContext = hubContext;
            _context = context;
        }

        public async Task<Result<CityPlaceMessageDtoCommand>> Handle(Command request, CancellationToken cancellationToken)
        {
            if (!await _context.CityPlace.AnyAsync(x => x.Id == request.PlaceId, cancellationToken)) return Result<CityPlaceMessageDtoCommand>.Failure("Failed to create message");

            var cityPlaceMessage = _mapper.Map<CityPlaceMessage>(request.Message);

            var uploadResult = await _imageAccessor.AddImage(request.Message.File, cancellationToken);

            if (uploadResult == null) return Result<CityPlaceMessageDtoCommand>.Failure("Failed to upload image");

            cityPlaceMessage.AvatarName = uploadResult.Filename;
            cityPlaceMessage.AvatarPublicId = uploadResult.PublicId;
            cityPlaceMessage.PlaceId = request.PlaceId;

            _context.CityPlaceMessage.Add(cityPlaceMessage);

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<CityPlaceMessageDtoCommand>.Failure("Failed to create message");

            var averangeMessagesOfPlace = await _context.CityPlaceMessage
                    .AsNoTracking()
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

            var result2 = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result2) return Result<CityPlaceMessageDtoCommand>.Failure("Failed to create message");

            var urlCloudinary = _config.GetSection("Cloudinary").GetValue<string>("Url");

            var cityPlaceMessageDto = _mapper.Map<CityPlaceMessageDtoQuery>(cityPlaceMessage,
                    opt => opt.AfterMap((src, dest) => dest.Avatar =
                    new AvatarDto { Name = cityPlaceMessage.AvatarName, Url = $"{urlCloudinary}/{cityPlaceMessage.AvatarPublicId}" }));

            var cityPlaceMessageDtoCommand = new CityPlaceMessageDtoCommand
            {
                Message = cityPlaceMessageDto,
                PlaceRating = averageRoundMessage
            };

            await _hubContext.Clients.Group(request.PlaceId.ToString()).ReceiveMessage(cityPlaceMessageDtoCommand);

            return Result<CityPlaceMessageDtoCommand>.Success(cityPlaceMessageDtoCommand);
        }
    }
}
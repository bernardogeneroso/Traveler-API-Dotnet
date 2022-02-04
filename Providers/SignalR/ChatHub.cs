using MediatR;
using Microsoft.AspNetCore.SignalR;
using Services.PlacesMessages;
using Services.PlacesMessages.DTOs;

namespace Providers.SignalR;

public class ChatHub : Hub
{
    private readonly IMediator _mediator;
    public ChatHub(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task SendMessage(Guid placeId, CityPlaceMessageDtoResult message)
    {
        var newMessage = await _mediator.Send(new Create.Command { Message = message });

        await Clients.Group(placeId.ToString()).SendAsync("ReceiveMessage", newMessage);
    }

    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var placeId = httpContext.Request.Query["placeId"];

        await Groups.AddToGroupAsync(Context.ConnectionId, placeId);

        var result = await _mediator.Send(new List.Query { PlaceId = Guid.Parse(placeId) });

        await Clients.Caller.SendAsync("LoadMessages", result.Value);
    }
}

using MediatR;
using Microsoft.AspNetCore.SignalR;
using Services.Interfaces;
using Services.PlacesMessages;

namespace Services.SignalR;

public class ChatHub : Hub<IChatHub>
{
    private readonly IMediator _mediator;
    public ChatHub(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var placeId = httpContext.Request.Query["placeId"];

        await Groups.AddToGroupAsync(Context.ConnectionId, placeId);

        var result = await _mediator.Send(new List.Query { PlaceId = Guid.Parse(placeId) });

        await Clients.Group(placeId.ToString()).LoadMessages(result.Value);
    }
}

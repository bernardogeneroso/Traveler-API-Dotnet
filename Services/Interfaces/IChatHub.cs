using Services.PlacesMessages.DTOs;

namespace Services.Interfaces;

public interface IChatHub
{
    Task LoadMessages(List<CityPlaceMessageDtoQuery> messages);
    Task ReceiveMessage(CityPlaceMessageDtoCommand placeMessage);
}

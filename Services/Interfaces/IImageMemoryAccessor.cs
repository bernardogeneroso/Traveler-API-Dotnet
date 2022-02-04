using Microsoft.AspNetCore.Http;

namespace Services.Interfaces;

public interface IImageMemoryAccessor
{
    Task<string> AddImage(IFormFile File, string fileName);
    bool DeleteImage(string imageName);
}

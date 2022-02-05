using Microsoft.AspNetCore.Http;

namespace Services.Interfaces;

public interface IImageMemoryAccessor
{
    Task<string> AddImageAsync(IFormFile File, string fileName);
    Task<bool> DeleteImageAsync(string imageName);
}

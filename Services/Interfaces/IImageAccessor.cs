using Microsoft.AspNetCore.Http;

namespace Services.Interfaces;

public interface IImageAccessor
{
    Task<ImageAccessorUploadResult> AddImageAsync(IFormFile File, CancellationToken? cancellationToken = null);
    Task<string> DeleteImageAsync(string publicId);
}

public class ImageAccessorUploadResult
{
    public string PublicId { get; set; }
    public string Filename { get; set; }
}

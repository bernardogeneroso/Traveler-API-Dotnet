using Microsoft.AspNetCore.Http;

namespace Services.Interfaces;

public interface IImageAccessor
{
    Task<ImageAccessorUploadResult> AddImage(IFormFile File);
    Task<string> DeleteImage(string publicId);
}

public class ImageAccessorUploadResult
{
    public string PublicId { get; set; }
    public string Filename { get; set; }
}

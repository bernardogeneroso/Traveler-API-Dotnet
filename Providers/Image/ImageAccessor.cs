using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Services.Interfaces;

namespace Providers.Image;

public class ImageAccessor : IImageAccessor
{
    private readonly Cloudinary _cloudinary;
    public ImageAccessor(IOptions<CloudinarySettings> config)
    {
        var account = new Account(
            config.Value.CloudName,
            config.Value.ApiKey,
            config.Value.ApiSecret
        );

        _cloudinary = new Cloudinary(account);
        _cloudinary.Api.Secure = true;
    }

    public async Task<ImageAccessorUploadResult> AddImage(IFormFile File, CancellationToken? cancellationToken = null)
    {
        if (File.Length > 0)
        {
            await using var stream = File.OpenReadStream();

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(File.FileName, stream),
                Transformation = new Transformation().Width(500).Height(500).Crop("fill")
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams, cancellationToken);

            if (uploadResult.Error != null) throw new Exception(uploadResult.Error.Message);

            return new ImageAccessorUploadResult
            {
                PublicId = uploadResult.PublicId,
                Filename = File.FileName
            };
        }

        return null;
    }

    public async Task<string> DeleteImage(string publicId)
    {
        var deleteParams = new DeletionParams(publicId);

        var result = await _cloudinary.DestroyAsync(deleteParams);

        if (result.Result != "ok") return null;

        return result.Result;
    }
}

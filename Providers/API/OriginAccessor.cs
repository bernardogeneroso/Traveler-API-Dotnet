using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Services.Interfaces;

namespace Providers.API;

public class OriginAccessor : IOriginAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _config;
    public OriginAccessor(IHttpContextAccessor httpContextAccessor, IConfiguration config)
    {
        _config = config;
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetOrigin()
    {
        var scheme = _httpContextAccessor?.HttpContext?.Request.Scheme ?? null;
        var host = _httpContextAccessor?.HttpContext?.Request.Host ?? null;

        return scheme + "://" + host;
    }

    public string GetCloudinaryUrl()
    {
        return _config.GetSection("Cloudinary").GetValue<string>("Url") ?? null;
    }

    public string GetRoutePath()
    {
        return _httpContextAccessor.HttpContext.Request.Path.Value.Replace("/api/", "");
    }
}

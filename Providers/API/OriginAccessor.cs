using Microsoft.AspNetCore.Http;
using Services.Interfaces;

namespace Providers.API;

public class OriginAccessor : IOriginAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public OriginAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetOrigin()
    {
        var scheme = _httpContextAccessor?.HttpContext?.Request.Scheme;
        var host = _httpContextAccessor?.HttpContext?.Request.Host;

        return scheme + "://" + host;
    }
}

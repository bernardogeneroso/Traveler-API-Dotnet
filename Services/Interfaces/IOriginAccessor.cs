namespace Services.Interfaces;

public interface IOriginAccessor
{
    string GetOrigin();
    string GetCloudinaryUrl();
    string GetRoutePath();
}

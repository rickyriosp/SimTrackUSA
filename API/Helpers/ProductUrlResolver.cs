using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers;

public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
{
    private readonly IConfiguration _config;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ProductUrlResolver(IConfiguration config, IHttpContextAccessor httpContext)
    {
        _config = config;
        _httpContextAccessor = httpContext;
    }

    public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
    {
        if (!string.IsNullOrWhiteSpace(source.PictureUrl))
        {
            var myHostUrl =
                $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/";
            return myHostUrl + source.PictureUrl;
            //return _config.GetValue<string>("ApiUrl") + source.PictureUrl;
        }

        return null;
    }
}

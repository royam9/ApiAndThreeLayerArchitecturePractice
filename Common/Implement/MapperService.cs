using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using ProjectN.Common.Interface;

namespace ProjectN.Common.Implement;

public class MapperService : IMapperService
{
    private readonly IServiceProvider _serviceProvider;

    public MapperService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IMapper CreateMapper()
    {
        var mapperConfiguration = _serviceProvider.GetService<IConfigurationProvider>();
        return mapperConfiguration.CreateMapper();
    }
}

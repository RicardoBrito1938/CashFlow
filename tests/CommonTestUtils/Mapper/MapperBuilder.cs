using AutoMapper;
using CashFlow.Application.AutoMapper;

namespace CommonTestUtils.Mapper;

public class MapperBuilder
{
    public static IMapper Build()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AutoMapping());
        });

        return config.CreateMapper();
    }
}
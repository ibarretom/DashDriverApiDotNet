using AutoMapper;
using Core.Domain.Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreTest.Domain.Services.Mapper;

public class AutoMapperFixture : IDisposable
{
    public IMapper Mapper { get; }

    public AutoMapperFixture()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(typeof(AutoMapperProfile).Assembly);
        });
        Mapper = configuration.CreateMapper();
    }
    public void Dispose()
    {
    }
}

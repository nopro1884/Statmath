using AutoMapper;
using Xunit;

namespace Statmath.Application.Mapping.Test
{
    public class MappingTest
    {
        [Fact]
        public void AutoMapperConfigurationValid()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfiles>());
        }


    }
}
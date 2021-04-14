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

        //[Fact]
        //public void AutoMapperConvertFromPlanToPlanViewModel()
        //{
        //    var source = new Plan
        //    {
        //        Id = Guid.NewGuid(),
        //        Job = 12345,
        //        Machine = "Machine1",
        //        StartedAt = new DateTime(1984, 08, 18, 18, 00, 0),
        //        EndedAt = new DateTime(1984, 08, 18, 18, 30, 0)
        //    };

        //    var config = new MapperConfiguration(cfg => cfg.AddProfile<ApplicationMappingProfile>());
        //    var mapper = config.CreateMapper();
        //    var converted = mapper.Map<Plan, PlanViewModel>(source);

        //    Assert.Equal("1984-08-18-18-00", converted.Start);
        //    Assert.Equal("1984-08-18-18-30", converted.End);
        //}
    }
}
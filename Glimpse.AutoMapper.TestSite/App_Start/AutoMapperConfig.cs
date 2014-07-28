using System;

using AutoMapper;

namespace Glimpse.AutoMapper.TestSite
{
    public static class AutoMapperConfig
    {
        public static void CreateMaps()
        {
            Mapper.CreateMap<byte, byte>();
            Mapper.AddProfile<TestProfile1>();
            Mapper.AddProfile<TestProfile2>();
        }

        private class TestProfile1 : Profile
        {
            protected override void Configure()
            {
                this.CreateMap<DateTime, DateTime>();
                this.CreateMap<int, int>().As<short>();
                this.CreateMap<string, string>();
            }
        }

        private class TestProfile2 : Profile
        {
            protected override void Configure()
            {
                this.CreateMap<short, short>();
                this.CreateMap<decimal, decimal>().As<float>();
            }
        }
    }
}
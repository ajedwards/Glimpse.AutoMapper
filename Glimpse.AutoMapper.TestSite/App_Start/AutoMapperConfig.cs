using System;

using AutoMapper;

namespace Glimpse.AutoMapper.TestSite
{
    public static class AutoMapperConfig
    {
        public static void CreateMaps()
        {
            Mapper.Initialize(configuration =>
            {
                configuration.CreateMap<byte, byte>();
                configuration.AddProfile<TestProfile1>();
                configuration.AddProfile<TestProfile2>();
            });
        }

        private class TestProfile1 : Profile
        {
            public TestProfile1()
                : base(@"TestProfile1")
            {
                this.CreateMap<DateTime, DateTime>();
                this.CreateMap<TestA, TestBaseB>().As<TestB>();
                this.CreateMap<string, string>();
            }
        }

        private class TestProfile2 : Profile
        {
            public TestProfile2()
                : base(@"TestProfile2")
            {
                this.CreateMap<short, short>();
                this.CreateMap<TestB, TestBaseA>().As<TestA>();
            }
        }
    }

    #region Test Classes

    internal abstract class TestBaseA
    {
        public string Id { get; set; }
    }

    internal class TestA : TestBaseA
    {
        public string Name { get; set; }
    }

    internal abstract class TestBaseB
    {
        public string Id { get; set; }
    }

    internal class TestB : TestBaseB
    {
        public string Name { get; set; }
    }

    #endregion
}
using System;
using System.Linq;

using AutoMapper;

using Glimpse.Core.Extensibility;
using Glimpse.Core.Tab.Assist;

using NUnit.Framework;

namespace Glimpse.AutoMapper.Tests
{
    [TestFixture]
    public class AutoMapperTabTests
    {
        [Test]
        public void TestConstructorThrowsArgumentNullExceptionForNullConfiguration()
        {
            Assert.Throws<ArgumentNullException>(() => new AutoMapperTab(null));
        }

        [Test]
        public void TestExecuteOnAlwaysReturnsEndRequest()
        {
            var testTab = new AutoMapperTab();

            Assert.AreEqual(RuntimeEvent.EndRequest, testTab.ExecuteOn);
        }

        [Test]
        public void TestGetDataReturnsTabSectionWithHeaderRowAndOneRowPerProfile()
        {
            var expectedProfiles = new Profile[] { new TestProfile1(), new TestProfile2(), new TestProfile3() };

            Mapper.Reset();
            Mapper.Initialize(configuration => expectedProfiles.ToList().ForEach(configuration.AddProfile));

            var actualTabSection = (TabSection)new AutoMapperTab(Mapper.Engine.ConfigurationProvider).GetData(null);

            Assert.AreEqual(1 + expectedProfiles.Length, actualTabSection.Rows.Count());
        }

        [Test]
        public void TestGetDataReturnsTabSectionWithHeaderRowOnlyForContextWithNoTypeMaps()
        {
            Mapper.Reset();

            var actualTabSection = (TabSection)new AutoMapperTab(Mapper.Engine.ConfigurationProvider).GetData(null);

            Assert.AreEqual(1, actualTabSection.Rows.Count());
        }

        [Test]
        public void TestGetDataReturnsTabSectionWithHeaderRowOnlyForNullContext()
        {
            Mapper.Reset();

            var actualTabSection = (TabSection)new AutoMapperTab().GetData(null);

            Assert.AreEqual(1, actualTabSection.Rows.Count());
        }

        [Test]
        public void TestNameAlwaysReturnsAutoMapper()
        {
            var testTab = new AutoMapperTab();

            Assert.AreEqual("AutoMapper", testTab.Name);
        }

        [Test]
        public void TestRequestContextTypeOnAlwaysReturnsNull()
        {
            var testTab = new AutoMapperTab();

            Assert.IsNull(testTab.RequestContextType);
        }

        private class TestProfile1 : Profile
        {
            protected override void Configure()
            {
                this.CreateMap<DateTime, DateTime>();
            }
        }

        private class TestProfile2 : Profile
        {
            protected override void Configure()
            {
                this.CreateMap<DateTime, DateTime>();
                this.CreateMap<int, int>();
            }
        }

        private class TestProfile3 : Profile
        {
            protected override void Configure()
            {
                this.CreateMap<DateTime, DateTime>();
                this.CreateMap<int, int>();
                this.CreateMap<string, string>();
            }
        }
    }
}

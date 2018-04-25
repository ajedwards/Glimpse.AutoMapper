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
            Mapper.Reset();
            Mapper.Initialize(configuration => { });

            var testTab = new AutoMapperTab();

            Assert.AreEqual(RuntimeEvent.EndRequest, testTab.ExecuteOn);
        }

        [Test]
        public void TestGetDataReturnsTabSectionWithHeaderRowAndOneRowPerProfileWithTypeMaps()
        {
            var expectedProfiles = new Profile[] { new TestProfile1(), new TestProfile2(), new TestProfile3() };

            Mapper.Reset();
            Mapper.Initialize(configuration => expectedProfiles.ToList().ForEach(configuration.AddProfile));

            var actualTabSection = (TabSection)new AutoMapperTab(Mapper.Configuration).GetData(null);

            Assert.AreEqual(1 + expectedProfiles.Length, actualTabSection.Rows.Count());
        }

        [Test]
        public void TestGetDataReturnsTabSectionWithHeaderRowOnlyForContextWithNoTypeMaps()
        {
            Mapper.Reset();
            Mapper.Initialize(configuration => { });

            var actualTabSection = (TabSection)new AutoMapperTab(Mapper.Configuration).GetData(null);

            Assert.AreEqual(1, actualTabSection.Rows.Count());
        }

        [Test]
        public void TestGetDataReturnsTabSectionWithHeaderRowOnlyForNullContext()
        {
            Mapper.Reset();
            Mapper.Initialize(configuration => { });

            var actualTabSection = (TabSection)new AutoMapperTab().GetData(null);

            Assert.AreEqual(1, actualTabSection.Rows.Count());
        }

        [Test]
        public void TestNameAlwaysReturnsAutoMapper()
        {
            Mapper.Reset();
            Mapper.Initialize(configuration => { });

            var testTab = new AutoMapperTab();

            Assert.AreEqual("AutoMapper", testTab.Name);
        }

        [Test]
        public void TestRequestContextTypeOnAlwaysReturnsNull()
        {
            Mapper.Reset();
            Mapper.Initialize(configuration => { });

            var testTab = new AutoMapperTab();

            Assert.IsNull(testTab.RequestContextType);
        }

        private class TestProfile1 : Profile
        {
            public TestProfile1()
            {
                this.CreateMap<DateTime, DateTime>();
            }
        }

        private class TestProfile2 : Profile
        {
            public TestProfile2()
            {
                this.CreateMap<Guid, Guid>();
                this.CreateMap<string, string>();
            }
        }

        private class TestProfile3 : Profile
        {
            public TestProfile3()
            {
                this.CreateMap<short, short>();
                this.CreateMap<decimal, decimal>();
                this.CreateMap<float, float>();
            }
        }
    }
}

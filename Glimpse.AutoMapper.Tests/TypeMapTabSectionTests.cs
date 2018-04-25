using System;
using System.Linq;

using AutoMapper;

using NUnit.Framework;

namespace Glimpse.AutoMapper.Tests
{
    [TestFixture]
    public class TypeMapTabSectionTests
    {
        [Test]
        public void TestConstructorReturnsNewTabSectionWithHeaderRowAndOneRowPerTypeMapForKnownProfile()
        {
            var expectedProfileName = typeof(TestProfile).FullName;

            Mapper.Reset();
            Mapper.Initialize(configuration => configuration.AddProfile<TestProfile>());

            var actualTabSection = new TypeMapTabSection(Mapper.Configuration, expectedProfileName);

            // ReSharper disable once PossibleNullReferenceException
            Assert.AreEqual(1 + TestProfile.TypeMapCount, actualTabSection.Rows.Count());
        }

        [Test]
        public void TestConstructorReturnsNewTabSectionWithHeaderRowAndOneRowPerTypeMapForEmptyProfileName()
        {
            var expectedMappingTypes = new[] { typeof(DateTime), typeof(int), typeof(string) };

            Mapper.Reset();
            Mapper.Initialize(
                configuration => expectedMappingTypes.ToList().ForEach(type => configuration.CreateMap(type, type)));

            var actualTabSection = new TypeMapTabSection(Mapper.Configuration, string.Empty);

            // ReSharper disable once PossibleNullReferenceException
            Assert.AreEqual(1 + expectedMappingTypes.Length, actualTabSection.Rows.Count());
        }

        [Test]
        public void TestConstructorReturnsNewTabSectionWithHeaderRowOnlyForKnownProfileWithNoTypeMaps()
        {
            var expectedProfileName = typeof(TestEmptyProfile).FullName;

            Mapper.Reset();
            Mapper.Initialize(configuration => configuration.AddProfile<TestEmptyProfile>());

            var actualTabSection = new TypeMapTabSection(Mapper.Configuration, expectedProfileName);

            // ReSharper disable once PossibleNullReferenceException
            Assert.AreEqual(1, actualTabSection.Rows.Count());
        }

        [Test]
        public void TestConstructorReturnsNewTabSectionWithHeaderRowOnlyForNullProfileName()
        {
            Mapper.Reset();
            Mapper.Initialize(configuration => {});

            var actualTabSection = new TypeMapTabSection(Mapper.Configuration, null);

            // ReSharper disable once PossibleNullReferenceException
            Assert.AreEqual(1, actualTabSection.Rows.Count());
        }

        [Test]
        public void TestConstructorReturnsNewTabSectionWithHeaderRowOnlyForUnknownProfileName()
        {
            var unknownProfileName = Guid.NewGuid().ToString();

            Mapper.Reset();
            Mapper.Initialize(configuration => { });

            var actualTabSection = new TypeMapTabSection(Mapper.Configuration, unknownProfileName);

            // ReSharper disable once PossibleNullReferenceException
            Assert.AreEqual(1, actualTabSection.Rows.Count());
        }

        [Test]
        public void TestConstructorThrowsArgumentNullExceptionForNullConfiguration()
        {
            var expectedProfileName = typeof(TestProfile).FullName;

            Assert.Throws<ArgumentNullException>(() => new TypeMapTabSection(null, expectedProfileName));
        }

        private class TestEmptyProfile : Profile
        {
        }

        private class TestProfile : Profile
        {
            public const int TypeMapCount = 3;

            public TestProfile()
            {
                this.CreateMap<DateTime, DateTime>();
                this.CreateMap<int, int>();
                this.CreateMap<string, string>();
            }
        }
    }
}
